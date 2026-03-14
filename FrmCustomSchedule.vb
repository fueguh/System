Imports System.Data.SqlClient

Public Class FrmCustomSchedule
    Public Property DentistID As Integer
    Public Property ScheduleSaved As Boolean = False
    Private schedules As New List(Of (day As String, startTime As TimeSpan, endTime As TimeSpan))

    ' ==========================================
    ' INITIALIZATION & LOAD
    ' ==========================================
    Private Sub FrmCustomSchedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Add weekdays to CheckedListBox
        ClbDays.Items.Clear()
        ClbDays.Items.AddRange({"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"})

        FillTimeComboBoxes()
        LoadPartTimeDentists()
    End Sub

    Private Sub FillTimeComboBoxes()
        cmbStartCustom.Items.Clear()
        cmbEndCustom.Items.Clear()

        ' Start at 8:00 AM, End at 7:30 PM
        Dim current As New TimeSpan(8, 0, 0)
        Dim limit As New TimeSpan(20, 0, 0)

        While current <= limit
            Dim timeString As String = DateTime.Today.Add(current).ToString("hh:mm tt")
            cmbStartCustom.Items.Add(timeString)
            cmbEndCustom.Items.Add(timeString)
            current = current.Add(TimeSpan.FromMinutes(30))
        End While
    End Sub

    ' ==========================================
    ' DATA LOADING
    ' ==========================================
    Private Sub LoadPartTimeDentists()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            ' Refined query: specifically targeting 'Part-time' and cleaning up the aggregate summary
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

        ' Reset UI for new selection
        For i = 0 To ClbDays.Items.Count - 1
            ClbDays.SetItemChecked(i, False)
        Next

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim query As String = "SELECT DayOfWeek, StartTime, EndTime FROM DentistAvailability WHERE DentistID=@id"
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", DentistID)
                Using reader = cmd.ExecuteReader()
                    schedules.Clear()
                    While reader.Read()
                        Dim day = reader("DayOfWeek").ToString()
                        Dim startT = CType(reader("StartTime"), TimeSpan)
                        Dim endT = CType(reader("EndTime"), TimeSpan)
                        schedules.Add((day, startT, endT))

                        Dim index = ClbDays.Items.IndexOf(day)
                        If index >= 0 Then ClbDays.SetItemChecked(index, True)
                    End While

                    ' Set combos based on existing schedule if available
                    If schedules.Count > 0 Then
                        Dim firstSched = schedules(0)
                        cmbStartCustom.Text = DateTime.Today.Add(firstSched.startTime).ToString("hh:mm tt")
                        cmbEndCustom.Text = DateTime.Today.Add(firstSched.endTime).ToString("hh:mm tt")
                    End If
                End Using
            End Using
        End Using
    End Sub

    ' ==========================================
    ' SAVE LOGIC (WITH 3-HOUR VALIDATION)
    ' ==========================================
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        ' 1. Selection Check
        If DentistID = 0 Then
            MessageBox.Show("Please select a dentist from the list first.")
            Exit Sub
        End If

        ' 2. Input Check
        If cmbStartCustom.SelectedIndex = -1 OrElse cmbEndCustom.SelectedIndex = -1 Then
            MessageBox.Show("Select both start and end times.")
            Exit Sub
        End If

        Dim startTime As TimeSpan = DateTime.Parse(cmbStartCustom.Text).TimeOfDay
        Dim endTime As TimeSpan = DateTime.Parse(cmbEndCustom.Text).TimeOfDay

        ' 3. Chronological Validation
        If endTime <= startTime Then
            MessageBox.Show("End time must be later than start time.")
            Exit Sub
        End If

        ' 4. Duration Validation (Minimum 3 Hours)
        Dim duration As TimeSpan = endTime - startTime
        If duration.TotalHours < 3.0 Then
            MessageBox.Show("Invalid Schedule: Part-time dentists must work at least 3 hours per shift." & vbCrLf &
                            "Selected duration: " & duration.TotalHours.ToString("N1") & " hours.")
            Exit Sub
        End If

        ' 5. Days Selected Check
        If ClbDays.CheckedItems.Count = 0 Then
            MessageBox.Show("Select at least one day for this schedule.")
            Exit Sub
        End If

        Try
            Using con As New SqlConnection(My.Settings.DentalDBConnection2)
                con.Open()
                Using trans = con.BeginTransaction()
                    Try
                        ' Remove old schedule first
                        Using cmdDel As New SqlCommand("DELETE FROM DentistAvailability WHERE DentistID=@id", con, trans)
                            cmdDel.Parameters.AddWithValue("@id", DentistID)
                            cmdDel.ExecuteNonQuery()
                        End Using

                        ' Insert new schedule for each checked day
                        For Each day As String In ClbDays.CheckedItems
                            Using cmdIns As New SqlCommand("INSERT INTO DentistAvailability (DentistID, DayOfWeek, StartTime, EndTime) VALUES (@id, @day, @start, @end)", con, trans)
                                cmdIns.Parameters.AddWithValue("@id", DentistID)
                                cmdIns.Parameters.AddWithValue("@day", day)
                                cmdIns.Parameters.AddWithValue("@start", startTime)
                                cmdIns.Parameters.AddWithValue("@end", endTime)
                                cmdIns.ExecuteNonQuery()
                            End Using
                        Next

                        trans.Commit()
                        MessageBox.Show("Dentist schedule updated successfully!")
                        LoadPartTimeDentists()
                        ClearForm()
                    Catch ex As Exception
                        trans.Rollback()
                        MessageBox.Show("Error during save process: " & ex.Message)
                    End Try
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Connection error: " & ex.Message)
        End Try
    End Sub

    ' ==========================================
    ' CLEANUP & NAVIGATION
    ' ==========================================
    Private Sub ClearForm()
        DentistID = 0
        schedules.Clear()

        ' Clear Checkboxes
        For i As Integer = 0 To ClbDays.Items.Count - 1
            ClbDays.SetItemChecked(i, False)
        Next

        ' Reset ComboBoxes
        cmbStartCustom.SelectedIndex = -1
        cmbEndCustom.SelectedIndex = -1
        cmbStartCustom.Text = ""
        cmbEndCustom.Text = ""

        DGVPartTimers.ClearSelection()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
    End Sub

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub
End Class