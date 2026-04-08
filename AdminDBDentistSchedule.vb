Imports System.Data.SqlClient

Public Class AdminDBDentistSchedule
    Public Property DentistID As Integer
    Public Property ScheduleSaved As Boolean = False

    ' ==========================================
    ' INITIALIZATION & LOAD
    ' ==========================================
    Private Sub FrmCustomSchedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupScheduleGrid()          ' Creates the grid + time dropdowns
        PopulateEmptyGrid()          ' Shows all 7 days ready to edit
        LoadPartTimeDentists()
        ClearForm()
    End Sub

    Private Sub SetupScheduleGrid()
        DgvSchedule.Columns.Clear()

        ' Day column
        Dim colDay As New DataGridViewTextBoxColumn With {
        .Name = "colDay",
        .HeaderText = "Day",
        .ReadOnly = True,
        .Width = 110
    }
        DgvSchedule.Columns.Add(colDay)

        ' Start Time
        Dim colStart As New DataGridViewComboBoxColumn With {
        .Name = "colStart",
        .HeaderText = "Start Time",
        .Width = 130
    }

        ' End Time
        Dim colEnd As New DataGridViewComboBoxColumn With {
        .Name = "colEnd",
        .HeaderText = "End Time",
        .Width = 130
    }

        ' Populate times (8:00 AM – 8:00 PM)
        Dim current As New TimeSpan(8, 0, 0)
        Dim limit As New TimeSpan(20, 0, 0)
        While current <= limit
            Dim timeString As String = DateTime.Today.Add(current).ToString("hh:mm tt")
            colStart.Items.Add(timeString)
            colEnd.Items.Add(timeString)
            cmbBulkStart.Items.Add(timeString)
            cmbBulkEnd.Items.Add(timeString)
            current = current.Add(TimeSpan.FromMinutes(30))
        End While

        DgvSchedule.Columns.Add(colStart)
        DgvSchedule.Columns.Add(colEnd)

        ' Clear button column – NO custom color
        Dim colClear As New DataGridViewButtonColumn With {
        .Name = "colClear",
        .HeaderText = "Action",
        .Text = "Clear",
        .UseColumnTextForButtonValue = True,
        .Width = 90,
        .FlatStyle = FlatStyle.Flat        ' ← helps it look more native
    }
        DgvSchedule.Columns.Add(colClear)

        DgvSchedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DgvSchedule.AllowUserToAddRows = False

        ' Optional: make header a bit clearer
        DgvSchedule.Columns("colClear").HeaderText = "Clear"
    End Sub

    Private Sub PopulateEmptyGrid()
        DgvSchedule.Rows.Clear()
        Dim days As String() = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}
        For Each d In days
            DgvSchedule.Rows.Add(d, Nothing, Nothing)
        Next
    End Sub

    ' ==========================================
    ' DATA LOADING
    ' ==========================================
    Private Sub LoadPartTimeDentists()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim query As String = "
                SELECT u.UserID, u.FullName,
                ISNULL(
                (
                    SELECT STRING_AGG(Days + ' (' + TimeRange + ')', '; ')
                    FROM (
                        SELECT 
                            STRING_AGG(da.DayOfWeek, ', ') AS Days,
                            FORMAT(CAST(da.StartTime AS datetime), 'hh:mm tt') + '-' +
                            FORMAT(CAST(da.EndTime AS datetime), 'hh:mm tt') AS TimeRange
                        FROM DentistAvailability da
                        WHERE da.DentistID = u.UserID
                        GROUP BY da.StartTime, da.EndTime
                    ) grouped
                ),
                'No Schedule Set') AS ScheduleSummary
                FROM Users u
                WHERE u.Role = 'Dentist' AND u.Availability = 'Part-time'"

            Dim dt As New DataTable()
            Using da As New SqlDataAdapter(query, con)
                da.Fill(dt)
            End Using

            DGVPartTimers.DataSource = dt
            If DGVPartTimers.Columns.Contains("UserID") Then DGVPartTimers.Columns("UserID").Visible = False
        End Using
    End Sub

    ' ==========================================
    ' SELECTION LOGIC
    ' ==========================================
    Private Sub DGVPartTimers_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVPartTimers.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Dim row As DataGridViewRow = DGVPartTimers.Rows(e.RowIndex)
        DentistID = CInt(row.Cells("UserID").Value)

        ' Reset grid to all 7 days
        PopulateEmptyGrid()

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim query As String = "SELECT DayOfWeek, StartTime, EndTime FROM DentistAvailability WHERE DentistID=@id"
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", DentistID)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim day = reader("DayOfWeek").ToString()
                        Dim startT = CType(reader("StartTime"), TimeSpan)
                        Dim endT = CType(reader("EndTime"), TimeSpan)

                        ' Find the matching row and fill the times
                        For Each r As DataGridViewRow In DgvSchedule.Rows
                            If r.Cells("colDay").Value.ToString() = day Then
                                r.Cells("colStart").Value = DateTime.Today.Add(startT).ToString("hh:mm tt")
                                r.Cells("colEnd").Value = DateTime.Today.Add(endT).ToString("hh:mm tt")
                                Exit For
                            End If
                        Next
                    End While
                End Using
            End Using
        End Using
    End Sub
    ' ==========================================
    ' GRID CELL CLICK - CLEAR BUTTON
    ' ==========================================
    Private Sub DgvSchedule_CellClick(sender As Object, e As DataGridViewCellEventArgs) _
    Handles DgvSchedule.CellClick

        If e.RowIndex < 0 Then Exit Sub
        If e.ColumnIndex <> DgvSchedule.Columns("colClear").Index Then Exit Sub

        Dim row = DgvSchedule.Rows(e.RowIndex)
        Dim btnCell = DirectCast(row.Cells("colClear"), DataGridViewButtonCell)

        ' Don't do anything if the button is disabled
        If btnCell.ReadOnly Then Exit Sub

        Dim day As String = row.Cells("colDay").Value.ToString()

        row.Cells("colStart").Value = Nothing
        row.Cells("colEnd").Value = Nothing

        ' Optional: small visual feedback (disappears after CellFormatting runs again)
        btnCell.Value = "Cleared"
        DgvSchedule.InvalidateCell(e.ColumnIndex, e.RowIndex)
    End Sub
    ' ==========================================
    ' SAVE LOGIC (now per-day with 3-hour validation)
    ' ==========================================
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If DentistID = 0 Then
            MessageBox.Show("Please select a dentist from the list first.")
            Exit Sub
        End If

        Dim validSchedules As New List(Of (day As String, startTime As TimeSpan, endTime As TimeSpan))

        For Each row As DataGridViewRow In DgvSchedule.Rows
            Dim day As String = row.Cells("colDay").Value.ToString()
            Dim startStr As String = If(row.Cells("colStart").Value, "").ToString().Trim()
            Dim endStr As String = If(row.Cells("colEnd").Value, "").ToString().Trim()

            If String.IsNullOrEmpty(startStr) OrElse String.IsNullOrEmpty(endStr) Then Continue For

            Try
                Dim startTime As TimeSpan = DateTime.Parse(startStr).TimeOfDay
                Dim endTime As TimeSpan = DateTime.Parse(endStr).TimeOfDay

                If endTime <= startTime Then
                    MessageBox.Show($"For {day}: End time must be later than start time.")
                    Exit Sub
                End If

                Dim duration = endTime - startTime
                If duration.TotalHours < 3.0 Then
                    MessageBox.Show($"For {day}: Minimum 3 hours required." & vbCrLf &
                                    "Selected: " & duration.TotalHours.ToString("N1") & " hours.")
                    Exit Sub
                End If

                validSchedules.Add((day, startTime, endTime))
            Catch
                MessageBox.Show($"Invalid time format on {day}. Please choose from the dropdown.")
                Exit Sub
            End Try
        Next

        If validSchedules.Count = 0 Then
            MessageBox.Show("Please set at least one day with start and end times.")
            Exit Sub
        End If

        Try
            Using con As New SqlConnection(My.Settings.DentalDBConnection2)
                con.Open()
                Using trans = con.BeginTransaction()
                    Try
                        ' Delete old schedule
                        Using cmdDel As New SqlCommand("DELETE FROM DentistAvailability WHERE DentistID=@id", con, trans)
                            cmdDel.Parameters.AddWithValue("@id", DentistID)
                            cmdDel.ExecuteNonQuery()
                        End Using

                        ' Insert each day's schedule
                        For Each s In validSchedules
                            Using cmdIns As New SqlCommand("INSERT INTO DentistAvailability (DentistID, DayOfWeek, StartTime, EndTime) VALUES (@id, @day, @start, @end)", con, trans)
                                cmdIns.Parameters.AddWithValue("@id", DentistID)
                                cmdIns.Parameters.AddWithValue("@day", s.day)
                                cmdIns.Parameters.AddWithValue("@start", s.startTime)
                                cmdIns.Parameters.AddWithValue("@end", s.endTime)
                                cmdIns.ExecuteNonQuery()
                            End Using
                        Next

                        trans.Commit()
                        MessageBox.Show("Dentist schedule updated successfully!")
                        LoadPartTimeDentists()
                        ClearForm()
                    Catch ex As Exception
                        trans.Rollback()
                        MessageBox.Show("Error during save: " & ex.Message)
                    End Try
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Connection error: " & ex.Message)
        End Try
    End Sub
    '===================================================================
    ' COMPLETE REMOVAL LOGIC (DELETE ALL DAYS) for the selected dentist
    '===================================================================
    Private Sub btnRemoveAll_Click(sender As Object, e As EventArgs) Handles btnRemoveAll.Click
        If DentistID = 0 Then
            MessageBox.Show("Please select a dentist first.")
            Exit Sub
        End If

        Dim answer = MessageBox.Show(
        "Remove ALL scheduled days for this dentist?" & vbCrLf & vbCrLf &
        "This cannot be undone.",
        "Confirm",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning)

        If answer <> DialogResult.Yes Then Exit Sub

        Try
            Using con As New SqlConnection(My.Settings.DentalDBConnection2)
                con.Open()
                Using cmd As New SqlCommand("DELETE FROM DentistAvailability WHERE DentistID = @id", con)
                    cmd.Parameters.AddWithValue("@id", DentistID)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Schedule cleared successfully.")
            LoadPartTimeDentists()   ' update the list summary
            ClearForm()              ' reset grid and selection

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub
    ' ==========================================
    ' BULK APPLY LOGIC
    ' ==========================================
    Private Sub btnApplyToSelected_Click(sender As Object, e As EventArgs) Handles btnApplyToSelected.Click
        If DgvSchedule.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select at least one day (row) in the grid.")
            Exit Sub
        End If

        If cmbBulkStart.SelectedIndex = -1 OrElse cmbBulkEnd.SelectedIndex = -1 Then
            MessageBox.Show("Please select both start and end times for bulk apply.")
            Exit Sub
        End If

        Dim startStr As String = cmbBulkStart.Text
        Dim endStr As String = cmbBulkEnd.Text

        Try
            Dim startTime As TimeSpan = DateTime.Parse(startStr).TimeOfDay
            Dim endTime As TimeSpan = DateTime.Parse(endStr).TimeOfDay

            If endTime <= startTime Then
                MessageBox.Show("End time must be later than start time.")
                Exit Sub
            End If

            Dim duration = endTime - startTime
            If duration.TotalHours < 3.0 Then
                MessageBox.Show("Minimum 3 hours required." & vbCrLf &
                            "Selected: " & duration.TotalHours.ToString("N1") & " hours.")
                Exit Sub
            End If

            ' Apply to all selected rows
            For Each row As DataGridViewRow In DgvSchedule.SelectedRows
                row.Cells("colStart").Value = startStr
                row.Cells("colEnd").Value = endStr
            Next

            MessageBox.Show($"Times applied to {DgvSchedule.SelectedRows.Count} day(s).")

            ' Optional: clear bulk combos after apply
            ' cmbBulkStart.SelectedIndex = -1
            ' cmbBulkEnd.SelectedIndex = -1

        Catch ex As Exception
            MessageBox.Show("Invalid time format: " & ex.Message)
        End Try
    End Sub
    ' ========================================================
    ' CELL FORMATTING - DISABLE CLEAR BUTTON WHEN NO SCHEDULE
    ' ========================================================
    Private Sub DgvSchedule_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) _
    Handles DgvSchedule.CellFormatting

        If e.ColumnIndex = DgvSchedule.Columns("colClear").Index AndAlso e.RowIndex >= 0 Then
            Dim row = DgvSchedule.Rows(e.RowIndex)

            Dim hasStart = row.Cells("colStart").Value IsNot Nothing AndAlso
                       Not String.IsNullOrWhiteSpace(row.Cells("colStart").Value.ToString())
            Dim hasEnd = row.Cells("colEnd").Value IsNot Nothing AndAlso
                     Not String.IsNullOrWhiteSpace(row.Cells("colEnd").Value.ToString())

            Dim cell = DirectCast(e.CellStyle, DataGridViewCellStyle)
            Dim btnCell = DirectCast(row.Cells("colClear"), DataGridViewButtonCell)

            If hasStart AndAlso hasEnd Then
                ' Has schedule → button active
                btnCell.ReadOnly = False
                btnCell.Style.BackColor = Color.Empty          ' default
                btnCell.Style.ForeColor = Color.Empty
                e.Value = "Clear"
            Else
                ' No schedule → disable button
                btnCell.ReadOnly = True
                btnCell.Style.BackColor = SystemColors.Control ' or Color.FromArgb(240,240,240)
                btnCell.Style.ForeColor = SystemColors.GrayText
                e.Value = ""                                   ' ← hides text when disabled
            End If
        End If
    End Sub
    ' ==========================================
    ' CLEANUP & NAVIGATION
    ' ==========================================
    Private Sub ClearForm()
        DentistID = 0
        PopulateEmptyGrid()
        DGVPartTimers.ClearSelection()
        cmbBulkStart.SelectedIndex = -1
        cmbBulkEnd.SelectedIndex = -1
        DgvSchedule.ClearSelection()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
    End Sub

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub
End Class