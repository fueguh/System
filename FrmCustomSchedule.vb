Imports System.Data.SqlClient

Public Class FrmCustomSchedule
    Public Property DentistID As Integer
    Public Property ScheduleSaved As Boolean = False
    Private schedules As New List(Of (day As String, startTime As TimeSpan, endTime As TimeSpan))

    ' --- Initialization ---
    Private Sub FrmCustomSchedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Add weekdays to CheckedListBox
        ClbDays.Items.Clear()
        ClbDays.Items.AddRange({"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"})

        FillTimeComboBoxes()
        LoadPartTimeDentists()
    End Sub

    Private Sub FillTimeComboBoxes()
        cmbStartCustom.Items.Clear()
        cmbEndCustom.Items.Clear()

        ' Start at 8:00 AM, End at 7:30 PM
        Dim current As New TimeSpan(8, 0, 0)
        Dim limit As New TimeSpan(19, 30, 0)

        While current <= limit
            Dim timeString As String = DateTime.Today.Add(current).ToString("hh:mm tt")
            cmbStartCustom.Items.Add(timeString)
            cmbEndCustom.Items.Add(timeString)
            current = current.Add(TimeSpan.FromMinutes(30))
        End While
    End Sub
    ' --- Data Loading ---
    Private Sub LoadPartTimeDentists()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            ' Use STRING_AGG to show a nice summary in the Grid
            Dim query As String = "
                SELECT u.UserID, u.FullName,
                ISNULL((SELECT STRING_AGG(da.DayOfWeek + ' ' + 
                       CONVERT(varchar(5), da.StartTime, 108) + '-' + 
                       CONVERT(varchar(5), da.EndTime, 108), '; ')
                FROM DentistAvailability da WHERE da.DentistID = u.UserID), '') AS ScheduleSummary
                FROM Users u WHERE u.Availability LIKE 'Part-time%'"

            Dim dt As New DataTable()
            Using cmd As New SqlCommand(query, con)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using

            DGVPartTimers.DataSource = dt
            If DGVPartTimers.Columns.Contains("UserID") Then DGVPartTimers.Columns("UserID").Visible = False
        End Using
    End Sub

    ' --- Selection Logic ---
    Private Sub DGVPartTimers_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVPartTimers.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Dim row As DataGridViewRow = DGVPartTimers.Rows(e.RowIndex)
        DentistID = CInt(row.Cells("UserID").Value)

        ' Reset UI for selection
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

                    ' Inside DGVPartTimers_CellClick, after filling the schedules list:
                    If schedules.Count > 0 Then
                        ' Set the start/end combos to the first schedule found for this dentist
                        Dim firstSched = schedules(0)
                        cmbStartCustom.Text = DateTime.Today.Add(firstSched.startTime).ToString("hh:mm tt")
                        cmbEndCustom.Text = DateTime.Today.Add(firstSched.endTime).ToString("hh:mm tt")
                    End If
                End Using
            End Using
        End Using
    End Sub

    ' --- Save Logic ---
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If DentistID = 0 Then
            MessageBox.Show("Please select a dentist from the list first.")
            Exit Sub
        End If

        If cmbStartCustom.SelectedIndex = -1 OrElse cmbEndCustom.SelectedIndex = -1 Then
            MessageBox.Show("Select both start and end times.")
            Exit Sub
        End If

        Dim startTime As TimeSpan = DateTime.Parse(cmbStartCustom.Text).TimeOfDay
        Dim endTime As TimeSpan = DateTime.Parse(cmbEndCustom.Text).TimeOfDay

        If endTime <= startTime Then
            MessageBox.Show("End time must be later than start time.")
            Exit Sub
        End If

        If ClbDays.CheckedItems.Count = 0 Then
            MessageBox.Show("Select at least one day.")
            Exit Sub
        End If

        Try
            Using con As New SqlConnection(My.Settings.DentalDBConnection2)
                con.Open()
                ' Transactional delete/insert
                Using trans = con.BeginTransaction()
                    Try
                        Using cmdDel As New SqlCommand("DELETE FROM DentistAvailability WHERE DentistID=@id", con, trans)
                            cmdDel.Parameters.AddWithValue("@id", DentistID)
                            cmdDel.ExecuteNonQuery()
                        End Using

                        For Each day As String In ClbDays.CheckedItems
                            Using cmdIns As New SqlCommand("INSERT INTO DentistAvailability (DentistID, DayOfWeek, StartTime, EndTime) VALUES (@id,@day,@start,@end)", con, trans)
                                cmdIns.Parameters.AddWithValue("@id", DentistID)
                                cmdIns.Parameters.AddWithValue("@day", day)
                                cmdIns.Parameters.AddWithValue("@start", startTime)
                                cmdIns.Parameters.AddWithValue("@end", endTime)
                                cmdIns.ExecuteNonQuery()
                            End Using
                        Next
                        trans.Commit()
                    Catch ex As Exception
                        trans.Rollback()
                        Throw ex
                    End Try
                End Using
            End Using

            MessageBox.Show("Dentist schedule updated successfully!")
            LoadPartTimeDentists()
            ClearForm()
        Catch ex As Exception
            MessageBox.Show("Error saving: " & ex.Message)
        End Try
    End Sub

    ' --- Cleanup & Navigation ---
    Private Sub ClearForm()
        DentistID = 0
        schedules.Clear()
        For i As Integer = 0 To ClbDays.Items.Count - 1
            ClbDays.SetItemChecked(i, False)
        Next
        If cmbStartCustom.Items.Count > 0 Then cmbStartCustom.SelectedIndex = 0
        If cmbEndCustom.Items.Count > 18 Then cmbEndCustom.SelectedIndex = 18
        DGVPartTimers.ClearSelection()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
    End Sub

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        SystemSession.NavigateToDashboard(Me)
        Me.Hide()
    End Sub
End Class