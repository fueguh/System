Imports System.Data.SqlClient

Public Class AdminDBAppointments

    Private selectedAppointmentID As Integer = 0
    Public Shared Dashboard As AdminDashboard
    Public Shared AdminDBReports As AdminDBReports

    Private Sub AdminDBAppointments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupStatusCombo()
        LoadComboBoxes()
        LoadAppointments()
        ClearForm()


        If Not (SystemSession.LoggedInRole = "Admin" OrElse SystemSession.LoggedInRole = "Staff") Then
            SystemSession.SetFormReadOnly(Me)
        End If

        dtpStartTime.Format = DateTimePickerFormat.Time
        dtpStartTime.ShowUpDown = True

        DtpEndTime.Format = DateTimePickerFormat.Time
        DtpEndTime.ShowUpDown = True
    End Sub

    Private Sub SetupStatusCombo()
        cmbStatus.Items.Clear()

        cmbStatus.Items.Add("Confirmed")
        cmbStatus.Items.Add("Ongoing")
        cmbStatus.Items.Add("Completed")
        cmbStatus.Items.Add("Cancelled")
        cmbStatus.SelectedIndex = 0 ' start with placeholder
    End Sub

    Private Sub LoadComboBoxes()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            ' ================= PATIENTS =================
            Dim dtPatients As New DataTable()
            Dim daPatients As New SqlDataAdapter(
            "SELECT PatientID, FullName FROM Patients WHERE IsActive = 1 ORDER BY FullName", con)
            Dim v1 = daPatients.Fill(dtPatients)
            CmbPatient.DataSource = dtPatients
            CmbPatient.DisplayMember = "FullName"
            CmbPatient.ValueMember = "PatientID"
            CmbPatient.SelectedIndex = -1   ' no default selection


            ' ================= DENTISTS =================
            Dim dtDentists As New DataTable()
            Dim daDentists As New SqlDataAdapter(
            "SELECT UserID, FullName FROM Users WHERE Role = 'Dentist' ORDER BY FullName", con)
            Dim v = daDentists.Fill(dtDentists)
            CmbDent.DataSource = dtDentists
            CmbDent.DisplayMember = "FullName"
            CmbDent.ValueMember = "UserID"
            CmbDent.SelectedIndex = -1      ' no default selection


            ' ================= SERVICES (MULTI) =================
            Dim dtServices As New DataTable()
            Dim daServices As New SqlDataAdapter(
                "SELECT ServiceID, ServiceName FROM Services ORDER BY ServiceName", con)
            daServices.Fill(dtServices)

            clbServices.DataSource = dtServices
            clbServices.DisplayMember = "ServiceName"
            clbServices.ValueMember = "ServiceID"
        End Using
    End Sub

    Private Sub LoadAppointments()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim query As String = "
            SELECT A.AppointmentID, P.FullName AS Patient, D.FullName AS Dentist, STRING_AGG(S.ServiceName, ', ') AS Services, A.Date, A.StartTime, A.EndTime, A.Status
            FROM Appointments A
            JOIN Patients P ON A.PatientID = P.PatientID
            JOIN Users D ON A.UserID = D.UserID AND D.Role = 'Dentist'
            JOIN AppointmentServices ASV ON A.AppointmentID = ASV.AppointmentID
            JOIN Services S ON ASV.ServiceID = S.ServiceID
            GROUP BY A.AppointmentID, P.FullName, D.FullName, A.Date, A.StartTime, A.EndTime, A.Status
            ORDER BY A.Date DESC;"

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVAppointments.DataSource = dt

        End Using
    End Sub

    Private Function ValidateFields() As Boolean
        If CmbPatient.SelectedValue Is Nothing Then
            MessageBox.Show("Please select a patient.")

            Return False
        End If

        If CmbDent.SelectedValue Is Nothing Then
            MessageBox.Show("Please select a dentist.")

            Return False
        End If


        If clbServices.CheckedItems.Count = 0 Then
            MessageBox.Show("Please select at least one service.")

            Return False
        End If


        If DtpEndTime.Value <= dtpStartTime.Value Then
            MessageBox.Show("End time must be later than start time.")

            Return False
        End If

        Dim dayOfWeek As DayOfWeek = DtpDate.Value.DayOfWeek
        Dim startTime As TimeSpan = dtpStartTime.Value.TimeOfDay
        Dim endTime As TimeSpan = DtpEndTime.Value.TimeOfDay

        If dayOfWeek >= dayOfWeek.Monday AndAlso dayOfWeek <= dayOfWeek.Friday Then
            If startTime < New TimeSpan(17, 0, 0) OrElse endTime > New TimeSpan(20, 0, 0) Then
                MessageBox.Show("Appointments from Monday to Friday are only Available between 5:00 PM and 8:00 PM.")
                Return False
            End If
        ElseIf dayOfWeek = dayOfWeek.Saturday Then
            If startTime < New TimeSpan(8, 0, 0) OrElse endTime > New TimeSpan(17, 0, 0) Then
                MessageBox.Show("Appointments on Saturday is only Available between 8:00 AM and 5:00 PM.")
                Return False
            End If
        Else
            MessageBox.Show("Appointments are only available Monday–Saturday.")
            Return False
        End If

        If String.IsNullOrWhiteSpace(cmbStatus.Text) Then
            MessageBox.Show("Please select a status.")

            Return False
        End If

        Return True
    End Function

    Private Function IsConflict(Optional appointmentID As Integer = 0) As Boolean
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim query As String = "
            SELECT 1
            FROM Appointments
            WHERE UserID = @dentist
              AND Date = @date
              AND AppointmentID <> @id
              AND (
                    (@start BETWEEN StartTime AND EndTime)
                 OR (@end BETWEEN StartTime AND EndTime)
                 OR (StartTime BETWEEN @start AND @end)
                 OR (EndTime BETWEEN @start AND @end)
                  )
        "

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@dentist", CInt(CmbDent.SelectedValue))
                cmd.Parameters.AddWithValue("@date", DtpDate.Value.Date)
                cmd.Parameters.AddWithValue("@start", dtpStartTime.Value.TimeOfDay)
                cmd.Parameters.AddWithValue("@end", DtpEndTime.Value.TimeOfDay)
                cmd.Parameters.AddWithValue("@id", appointmentID)

                Using dr As SqlDataReader = cmd.ExecuteReader()
                    Return dr.HasRows
                End Using
            End Using
        End Using
    End Function

    Private Sub BTNAdd_Click(sender As Object, e As EventArgs) Handles BTNAdd.Click
        ' ✅ Validate required fields
        If Not ValidateFields() Then Exit Sub

        ' ✅ Check for conflicts
        If IsConflict() Then
            MessageBox.Show("This dentist already has an appointment at this time.")
            Exit Sub
        End If

        ' ✅ Validate allowed time ranges
        Dim dayOfWeek As DayOfWeek = DtpDate.Value.DayOfWeek
        Dim startTime As TimeSpan = dtpStartTime.Value.TimeOfDay
        Dim endTime As TimeSpan = DtpEndTime.Value.TimeOfDay

        If dayOfWeek >= dayOfWeek.Monday AndAlso dayOfWeek <= dayOfWeek.Friday Then
            If startTime < New TimeSpan(17, 0, 0) OrElse endTime > New TimeSpan(20, 0, 0) Then
                MessageBox.Show("Appointments from Monday to Friday are only Available between 5:00 PM and 8:00 PM.")
                Exit Sub
            End If
        ElseIf dayOfWeek = dayOfWeek.Saturday Then
            If startTime < New TimeSpan(8, 0, 0) OrElse endTime > New TimeSpan(17, 0, 0) Then
                MessageBox.Show("Appointments on Saturday is only Available between 8:00 AM and 5:00 PM.")
                Exit Sub
            End If
        Else
            MessageBox.Show("Appointments are only available every Monday to Saturday.")
            Exit Sub
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            ' --- Insert appointment ---
            Dim query As String = "
            INSERT INTO Appointments (PatientID, UserID, Date, StartTime, EndTime, Status)
            OUTPUT INSERTED.AppointmentID
            VALUES (@p, @d, @date, @start, @end, @status)
        "

            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@p", CInt(CmbPatient.SelectedValue))
            cmd.Parameters.AddWithValue("@d", CInt(CmbDent.SelectedValue))
            cmd.Parameters.AddWithValue("@date", DtpDate.Value.Date)
            cmd.Parameters.AddWithValue("@start", dtpStartTime.Value.TimeOfDay)
            cmd.Parameters.AddWithValue("@end", DtpEndTime.Value.TimeOfDay)
            cmd.Parameters.AddWithValue("@status", cmbStatus.Text)

            Try
                ' ✅ Get the new AppointmentID
                Dim newAppointmentID As Integer = CInt(cmd.ExecuteScalar())

                ' ✅ Save all checked services for this appointment
                SaveAppointmentServices(newAppointmentID)

                MessageBox.Show("Appointment added successfully.")

                ' ✅ Audit log entry
                SystemSession.LogAudit("Appointment " & cmbStatus.Text,
                                       "Appointment Management",
                                       SystemSession.LoggedInUserID,
                                       SystemSession.LoggedInFullName,
                                       SystemSession.LoggedInRole)

            Catch ex As Exception
                MessageBox.Show("Error adding appointment: " & ex.Message)
            End Try
        End Using

        ' ✅ Refresh UI
        LoadAppointments()

        ' ✅ Refresh reports
        Dim historyForm As New AdminDBReports()
        historyForm.RefreshHistory()

        ' ✅ Reload dashboard stats
        Dashboard?.LoadDashboardStats()

        ' ✅ Clear form inputs
        ClearForm()

    End Sub


    Private Sub BTNUpdate_Click(sender As Object, e As EventArgs) Handles BTNUpdate.Click
        If selectedAppointmentID = 0 Then
            MessageBox.Show("Please select an appointment to update.")
            Exit Sub
        End If

        If Not ValidateFields() Then Exit Sub
        If IsConflict(selectedAppointmentID) Then
            MessageBox.Show("This dentist already has an appointment at this time.")
            Exit Sub
        End If

        ' ✅ Validate allowed time ranges
        Dim dayOfWeek As DayOfWeek = DtpDate.Value.DayOfWeek
        Dim startTime As TimeSpan = dtpStartTime.Value.TimeOfDay
        Dim endTime As TimeSpan = DtpEndTime.Value.TimeOfDay

        If dayOfWeek >= dayOfWeek.Monday AndAlso dayOfWeek <= dayOfWeek.Friday Then
            If startTime < New TimeSpan(17, 0, 0) OrElse endTime > New TimeSpan(20, 0, 0) Then
                MessageBox.Show("Appointments from Monday to Friday are only Available between 5:00 PM and 8:00 PM.")
                Exit Sub
            End If
        ElseIf dayOfWeek = dayOfWeek.Saturday Then
            If startTime < New TimeSpan(8, 0, 0) OrElse endTime > New TimeSpan(17, 0, 0) Then
                MessageBox.Show("Appointments on Saturday is only Available between 8:00 AM and 5:00 PM.")
                Exit Sub
            End If
        Else
            MessageBox.Show("Appointments are only available every Monday to Saturday.")
            Exit Sub
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            ' Update appointment
            Dim query As String = "
            UPDATE Appointments
            SET PatientID=@p, UserID=@d, Date=@date,
                StartTime=@start, EndTime=@end, Status=@status
            WHERE AppointmentID=@id
        "

            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@id", selectedAppointmentID)
            cmd.Parameters.AddWithValue("@p", CInt(CmbPatient.SelectedValue))
            cmd.Parameters.AddWithValue("@d", CInt(CmbDent.SelectedValue))
            cmd.Parameters.AddWithValue("@date", DtpDate.Value.Date)
            cmd.Parameters.AddWithValue("@start", dtpStartTime.Value.TimeOfDay)
            cmd.Parameters.AddWithValue("@end", DtpEndTime.Value.TimeOfDay)
            cmd.Parameters.AddWithValue("@status", cmbStatus.Text)

            Try
                cmd.ExecuteNonQuery()

                ' --- Delete old services ---
                Using cmdDelete As New SqlCommand("DELETE FROM AppointmentServices WHERE AppointmentID=@aid", con)
                    cmdDelete.Parameters.AddWithValue("@aid", selectedAppointmentID)
                    cmdDelete.ExecuteNonQuery()
                End Using

                ' --- Insert new checked services ---
                SaveAppointmentServices(selectedAppointmentID)

                MessageBox.Show("Appointment updated successfully.")

                ' Audit log
                SystemSession.LogAudit("Appointment " & cmbStatus.Text & " (Updated)",
                                   "Appointment Management",
                                   SystemSession.LoggedInUserID,
                                   SystemSession.LoggedInFullName,
                                   SystemSession.LoggedInRole)
            Catch ex As Exception
                MessageBox.Show("Error updating appointment: " & ex.Message)
            End Try
        End Using


        ' ✅ Refresh UI
        LoadAppointments()
        Dashboard?.LoadDashboardStats()
        AdminDBReports?.LoadAppointmentHistory()
        ClearForm()

    End Sub

    Private Sub BTNDelete_Click(sender As Object, e As EventArgs) Handles BTNDelete.Click
        If selectedAppointmentID = 0 Then
            MessageBox.Show("Please select an appointment to delete.")
            Exit Sub
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            If MessageBox.Show("Are you sure you want to delete this appointment?",
                           "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

            Using trans = con.BeginTransaction()
                Try
                    ' 1️⃣ Delete linked services first
                    Using cmdServices As New SqlCommand(
                    "DELETE FROM AppointmentServices WHERE AppointmentID=@id", con, trans)
                        cmdServices.Parameters.AddWithValue("@id", selectedAppointmentID)
                        cmdServices.ExecuteNonQuery()
                    End Using

                    ' 2️⃣ Delete appointment
                    Using cmdAppt As New SqlCommand(
                    "DELETE FROM Appointments WHERE AppointmentID=@id", con, trans)
                        cmdAppt.Parameters.AddWithValue("@id", selectedAppointmentID)
                        cmdAppt.ExecuteNonQuery()
                    End Using

                    ' ✅ Commit transaction
                    trans.Commit()

                    ' Log audit after commit
                    Try
                        SystemSession.LogAudit(
                        "Appointment Deleted",
                        "Appointment Management",
                        SystemSession.LoggedInUserID,
                        SystemSession.LoggedInFullName,
                        SystemSession.LoggedInRole)
                    Catch ex As Exception
                        MessageBox.Show("Error logging audit: " & ex.Message)
                    End Try

                    MessageBox.Show("Appointment deleted successfully.")

                Catch ex As Exception
                    ' Rollback if anything fails
                    trans.Rollback()
                    MessageBox.Show("Error deleting appointment: " & ex.Message)
                End Try
            End Using

            ' ✅ Refresh UI
            LoadAppointments()
            Dashboard?.LoadDashboardStats()
            ClearForm()
        End Using
    End Sub

    Private Sub DGVAppointments_CellClick(sender As Object, e As DataGridViewCellEventArgs) _
    Handles DGVAppointments.CellClick


        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DGVAppointments.Rows(e.RowIndex)
            selectedAppointmentID = CInt(row.Cells("AppointmentID").Value)

            CmbPatient.Text = row.Cells("Patient").Value.ToString()
            CmbDent.Text = row.Cells("Dentist").Value.ToString()
            DtpDate.Value = CDate(row.Cells("Date").Value)

            Try
                dtpStartTime.Value = Date.Today.Add(TimeSpan.Parse(row.Cells("StartTime").Value.ToString()))
                DtpEndTime.Value = Date.Today.Add(TimeSpan.Parse(row.Cells("EndTime").Value.ToString()))
            Catch ex As Exception
                dtpStartTime.Value = CDate(row.Cells("StartTime").Value)
                DtpEndTime.Value = CDate(row.Cells("EndTime").Value)
            End Try

            ' Set status
            Dim statusValue As String = row.Cells("Status").Value.ToString()
            If cmbStatus.Items.Contains(statusValue) Then
                cmbStatus.SelectedItem = statusValue
            Else
                cmbStatus.SelectedIndex = 0
            End If

            ' Load services
            LoadCheckedServices(selectedAppointmentID)
        End If

    End Sub



    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

    Private Sub ClearForm()
        CmbPatient.SelectedIndex = -1
        CmbDent.SelectedIndex = -1
        cmbStatus.SelectedIndex = -1
        DtpDate.Value = Date.Today

        ' Apply default times based on today
        DtpDate_ValueChanged(Nothing, Nothing)
        For i As Integer = 0 To clbServices.Items.Count - 1
            clbServices.SetItemChecked(i, False)
        Next


        selectedAppointmentID = 0
    End Sub


    Private Sub DGVAppointments_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DGVAppointments.Rows(e.RowIndex)
            selectedAppointmentID = CInt(row.Cells("AppointmentID").Value)

            CmbPatient.Text = row.Cells("Patient").Value.ToString()
            CmbDent.Text = row.Cells("Dentist").Value.ToString()
            DtpDate.Value = CDate(row.Cells("Date").Value)

            dtpStartTime.Value = CDate(row.Cells("StartTime").Value)
            DtpEndTime.Value = CDate(row.Cells("EndTime").Value)


            cmbStatus.Text = row.Cells("Status").Value.ToString()


            LoadCheckedServices(selectedAppointmentID)
        End If

    End Sub

    Private Sub SaveAppointmentServices(appointmentID As Integer)
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            For Each item As DataRowView In clbServices.CheckedItems
                Using cmd As New SqlCommand(
                "INSERT INTO AppointmentServices (AppointmentID, ServiceID)
                 VALUES (@aid, @sid)", con)

                    cmd.Parameters.AddWithValue("@aid", appointmentID)
                    cmd.Parameters.AddWithValue("@sid", item("ServiceID"))

                    cmd.ExecuteNonQuery()
                End Using
            Next
        End Using
    End Sub

    Private Sub LoadCheckedServices(appointmentID As Integer)
        ' Clear first
        For i As Integer = 0 To clbServices.Items.Count - 1
            clbServices.SetItemChecked(i, False)
        Next

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim cmd As New SqlCommand(
            "SELECT ServiceID FROM AppointmentServices WHERE AppointmentID=@aid", con)
            cmd.Parameters.AddWithValue("@aid", appointmentID)
            Dim reader = cmd.ExecuteReader()

            Dim serviceIDs As New List(Of Integer)
            While reader.Read()
                serviceIDs.Add(CInt(reader("ServiceID")))
            End While
            reader.Close()

            ' Check the items
            For i As Integer = 0 To clbServices.Items.Count - 1
                Dim item As DataRowView = CType(clbServices.Items(i), DataRowView)
                If serviceIDs.Contains(CInt(item("ServiceID"))) Then
                    clbServices.SetItemChecked(i, True)
                End If
            Next
        End Using
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

    Private Sub BTNPayment_Click(sender As Object, e As EventArgs) Handles BTNPayment.Click

        If DGVAppointments.CurrentRow Is Nothing Then
            MessageBox.Show("Please select an appointment first.")
            Exit Sub
        End If

        Dim dayOfWeek As DayOfWeek = DtpDate.Value.DayOfWeek

        Dim appointmentID As Integer = CInt(DGVAppointments.CurrentRow.Cells("AppointmentID").Value)
        Dim patientID As Integer = CInt(DGVAppointments.CurrentRow.Cells("PatientID").Value)
        Dim patientName As String = DGVAppointments.CurrentRow.Cells("Patient").Value.ToString()

        Dim paymentForm As New AdminDBPayment With {
            .SelectedAppointmentID = appointmentID,
            .SelectedPatientID = patientID,
            .SelectedPatientName = patientName
        }
        paymentForm.ShowDialog()


    End Sub

    Private Sub DtpDate_ValueChanged(sender As Object, e As EventArgs) Handles DtpDate.ValueChanged
        Dim selectedDate As DateTime = DtpDate.Value
        Dim dayOfWeek As DayOfWeek = selectedDate.DayOfWeek

        If dayOfWeek >= dayOfWeek.Monday AndAlso dayOfWeek <= dayOfWeek.Friday Then
            ' Monday to Friday: 5:00 PM – 8:00 PM
            dtpStartTime.Value = New DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, 17, 0, 0)
            DtpEndTime.Value = New DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, 20, 0, 0)



        ElseIf dayOfWeek = dayOfWeek.Saturday Then
            ' Saturday: 8:00 AM – 5:00 PM
            dtpStartTime.Value = New DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, 8, 0, 0)
            DtpEndTime.Value = New DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, 17, 0, 0)



        Else
            ' Sunday: disable scheduling
            dtpStartTime.Enabled = False
            DtpEndTime.Enabled = False
            MessageBox.Show("Appointments are not allowed on Sundays.")
        End If
    End Sub
End Class