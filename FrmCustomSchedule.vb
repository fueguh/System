
Imports System.Data.SqlClient

Public Class FrmCustomSchedule
    Public Property DentistID As Integer
    Public Property ScheduleSaved As Boolean = False
    Private schedules As New List(Of (day As String, startTime As TimeSpan, endTime As TimeSpan))

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If DentistID = 0 Then
            MessageBox.Show("Select a dentist first")
            Exit Sub
        End If

        Dim newSchedules As New List(Of (day As String, startTime As TimeSpan, endTime As TimeSpan))

        For Each day As String In ClbDays.CheckedItems
            Dim startTime = dtpStartTime.Value.TimeOfDay
            Dim endTime = dtpEndTime.Value.TimeOfDay
            If endTime <= startTime Then
                MessageBox.Show("End time must be after start time")
                Exit Sub
            End If
            newSchedules.Add((day, startTime, endTime))
        Next

        If newSchedules.Count = 0 Then
            MessageBox.Show("Select at least one day")
            Exit Sub
        End If

        ' Save to DB
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            ' Delete old schedule
            Using cmdDel As New SqlCommand("DELETE FROM DentistAvailability WHERE DentistID=@id", con)
                cmdDel.Parameters.AddWithValue("@id", DentistID)
                cmdDel.ExecuteNonQuery()
            End Using

            ' Insert new schedule
            For Each s In newSchedules
                Using cmdIns As New SqlCommand("INSERT INTO DentistAvailability (DentistID, DayOfWeek, StartTime, EndTime) VALUES (@id,@day,@start,@end)", con)
                    cmdIns.Parameters.AddWithValue("@id", DentistID)
                    cmdIns.Parameters.AddWithValue("@day", s.day)
                    cmdIns.Parameters.AddWithValue("@start", s.startTime)
                    cmdIns.Parameters.AddWithValue("@end", s.endTime)
                    cmdIns.ExecuteNonQuery()
                End Using
            Next
        End Using

        MessageBox.Show("Schedule updated successfully")
        LoadPartTimeDentists() ' refresh grid
    End Sub

    Private Sub SaveCustomSchedule(dentistID As Integer, schedules As List(Of (day As String, startTime As TimeSpan, endTime As TimeSpan)))
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            ' Delete old availability
            Using cmdDelete As New SqlCommand("DELETE FROM DentistAvailability WHERE DentistID=@id", con)
                cmdDelete.Parameters.AddWithValue("@id", dentistID)
                cmdDelete.ExecuteNonQuery()
            End Using

            ' Insert new slots
            For Each slot In schedules
                Using cmdInsert As New SqlCommand("
                INSERT INTO DentistAvailability (DentistID, DayOfWeek, StartTime, EndTime)
                VALUES (@id, @day, @start, @end)", con)

                    cmdInsert.Parameters.AddWithValue("@id", dentistID)
                    cmdInsert.Parameters.AddWithValue("@day", slot.day)
                    cmdInsert.Parameters.AddWithValue("@start", slot.startTime)
                    cmdInsert.Parameters.AddWithValue("@end", slot.endTime)
                    cmdInsert.ExecuteNonQuery()
                End Using
            Next
        End Using
    End Sub

    Private Sub FrmCustomSchedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Add weekdays
        ClbDays.Items.AddRange({"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"})

        ' Load all part-time dentists
        LoadPartTimeDentists()
    End Sub

    Private Sub LoadPartTimeDentists()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            ' Get all part-time dentists
            Dim query As String = "
        SELECT 
            u.UserID, 
            u.FullName,
            ISNULL((
SELECT STRING_AGG(da.DayOfWeek + ' ' 
                  + CONVERT(varchar(5), da.StartTime, 108) + '-' 
                  + CONVERT(varchar(5), da.EndTime, 108), '; ')
FROM DentistAvailability da
WHERE da.DentistID = u.UserID

            ), '') AS ScheduleSummary
        FROM Users u
        WHERE u.Availability LIKE 'Part-time%'
        "

            Dim dt As New DataTable()
            Using cmd As New SqlCommand(query, con)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using

            DGVPartTimers.DataSource = dt
            DGVPartTimers.Columns("UserID").Visible = False ' hide internal ID
            DGVPartTimers.Columns("FullName").HeaderText = "Dentist Name"
            DGVPartTimers.Columns("ScheduleSummary").HeaderText = "Schedule"
        End Using
    End Sub


    Private Sub DGVPartTimers_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVPartTimers.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Dim row As DataGridViewRow = DGVPartTimers.Rows(e.RowIndex)
        DentistID = CInt(row.Cells("UserID").Value)

        ' Clear checkboxes
        For i = 0 To ClbDays.Items.Count - 1
            ClbDays.SetItemChecked(i, False)
        Next

        ' Load existing schedule from database
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim query As String = "SELECT DayOfWeek, StartTime, EndTime FROM DentistAvailability WHERE DentistID=@id"
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", DentistID)
                Using reader = cmd.ExecuteReader()
                    schedules.Clear()
                    While reader.Read()
                        Dim day = reader("DayOfWeek").ToString()
                        Dim startTime = CType(reader("StartTime"), TimeSpan)
                        Dim endTime = CType(reader("EndTime"), TimeSpan)
                        schedules.Add((day, startTime, endTime))

                        ' Check the day
                        Dim index = ClbDays.Items.IndexOf(day)
                        If index >= 0 Then ClbDays.SetItemChecked(index, True)
                    End While

                    ' Show first schedule in timepickers if exists
                    If schedules.Count > 0 Then
                        dtpStartTime.Value = DateTime.Today.Add(schedules(0).startTime)
                        dtpEndTime.Value = DateTime.Today.Add(schedules(0).endTime)
                    End If
                End Using
            End Using
        End Using
    End Sub

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        ' ✅ If no admin is logged in, go back to Login form
        If SystemSession.LoggedInUserID = 0 OrElse SystemSession.LoggedInRole <> "Admin" Then
            ' No active admin session → just redirect to Login without logging a logout
            Login.Show()
            Me.Hide()
            Exit Sub
        End If

        ' Otherwise, show user dashboard
        SystemSession.NavigateToDashboard(Me)
        Me.Hide()
    End Sub
End Class