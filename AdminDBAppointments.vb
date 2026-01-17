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

        'only Admin/Staff can edit, dentists have read-only access
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
        cmbStatus.Items.Add("Pending")
        cmbStatus.Items.Add("Confirmed")
        cmbStatus.Items.Add("Completed")
        cmbStatus.Items.Add("Cancelled")
        cmbStatus.Items.Add("No-Show")
        cmbStatus.SelectedIndex = 0
    End Sub

    Private Sub LoadComboBoxes()
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            ' ================= PATIENTS =================
            Dim dtPatients As New DataTable()
            Dim daPatients As New SqlDataAdapter(
            "SELECT PatientID, FullName FROM Patients", con)
            daPatients.Fill(dtPatients)

            Dim rowPatient As DataRow = dtPatients.NewRow()
            rowPatient("PatientID") = 0
            rowPatient("FullName") = "— Select Patient —"
            dtPatients.Rows.InsertAt(rowPatient, 0)

            CmbPatient.DataSource = dtPatients
            CmbPatient.DisplayMember = "FullName"
            CmbPatient.ValueMember = "PatientID"
            CmbPatient.SelectedIndex = 0


            ' ================= DENTISTS =================
            Dim dtDentists As New DataTable()
            Dim daDentists As New SqlDataAdapter(
            "SELECT UserID, FullName FROM Users WHERE Role = 'Dentist'", con)
            daDentists.Fill(dtDentists)

            Dim rowDentist As DataRow = dtDentists.NewRow()
            rowDentist("UserID") = 0
            rowDentist("FullName") = "— Select Dentist —"
            dtDentists.Rows.InsertAt(rowDentist, 0)

            CmbDentist.DataSource = dtDentists
            CmbDentist.DisplayMember = "FullName"
            CmbDentist.ValueMember = "UserID"
            CmbDentist.SelectedIndex = 0



            ' ================= SERVICES =================
            Dim dtServices As New DataTable()
            Dim daServices As New SqlDataAdapter(
            "SELECT ServiceID, ServiceName FROM Services", con)
            daServices.Fill(dtServices)

            Dim rowService As DataRow = dtServices.NewRow()
            rowService("ServiceID") = 0
            rowService("ServiceName") = "— Select Service —"
            dtServices.Rows.InsertAt(rowService, 0)

            CmbService.DataSource = dtServices
            CmbService.DisplayMember = "ServiceName"
            CmbService.ValueMember = "ServiceID"
            CmbService.SelectedIndex = 0
        End Using
    End Sub

    Private Sub LoadAppointments()
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "
            SELECT A.AppointmentID,
                   P.FullName AS Patient,
                   D.FullName AS Dentist,
                   S.ServiceName AS Service,
                   A.Date,
                   A.StartTime,
                   A.EndTime,
                   A.Status
            FROM Appointments A
            JOIN Patients P ON A.PatientID = P.PatientID
            JOIN Users D ON A.UserID = D.UserID AND D.Role = 'Dentist'
            JOIN Services S ON A.ServiceID = S.ServiceID
            ORDER BY A.Date DESC, A.StartTime ASC
        "

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVAppointments.DataSource = dt
        End Using
    End Sub

    Private Function ValidateFields() As Boolean

        ' Patient
        If CmbPatient.SelectedValue Is Nothing OrElse CInt(CmbPatient.SelectedValue) = 0 Then
            MessageBox.Show("Please select a patient.")
            CmbPatient.Focus()
            Return False
        End If

        ' Dentist
        If CmbDentist.SelectedValue Is Nothing OrElse CInt(CmbDentist.SelectedValue) = 0 Then
            MessageBox.Show("Please select a dentist.")
            CmbDentist.Focus()
            Return False
        End If

        ' Service
        If CmbService.SelectedValue Is Nothing OrElse CInt(CmbService.SelectedValue) = 0 Then
            MessageBox.Show("Please select a service.")
            CmbService.Focus()
            Return False
        End If

        ' Time validation
        If DtpEndTime.Value <= dtpStartTime.Value Then
            MessageBox.Show("End time must be later than start time.")
            DtpEndTime.Focus()
            Return False
        End If

        ' Status
        If String.IsNullOrWhiteSpace(cmbStatus.Text) Then
            MessageBox.Show("Please select a status.")
            cmbStatus.Focus()
            Return False
        End If

        Return True
    End Function

    Private Function IsConflict(Optional appointmentID As Integer = 0) As Boolean
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "
            SELECT * FROM Appointments
            WHERE UserID = @dentist
            AND Date = @date
            AND AppointmentID <> @id
            AND (
                (@start BETWEEN StartTime AND EndTime)
                OR (@end BETWEEN StartTime AND EndTime)
                OR (StartTime BETWEEN @start AND @end)
            )
        "

            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@dentist", CInt(CmbDentist.SelectedValue))
            cmd.Parameters.AddWithValue("@date", DtpDate.Value.Date)
            cmd.Parameters.AddWithValue("@start", dtpStartTime.Value.TimeOfDay)
            cmd.Parameters.AddWithValue("@end", DtpEndTime.Value.TimeOfDay)
            cmd.Parameters.AddWithValue("@id", appointmentID)

            Dim dr = cmd.ExecuteReader()
            Return dr.HasRows
        End Using
    End Function

    Private Sub Guna2DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DtpDate.ValueChanged

    End Sub

    Private Sub BTNAdd_Click(sender As Object, e As EventArgs) Handles BTNAdd.Click
        ' ✅ Validate required fields
        If Not ValidateFields() Then Exit Sub

        ' ✅ Check for conflicts
        If IsConflict() Then
            MessageBox.Show("This dentist already has an appointment at this time.")
            Exit Sub
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            ' ✅ Corrected column name: UserID instead of UsersID
            Dim query As String = "
            INSERT INTO Appointments (PatientID, UserID, ServiceID, Date, StartTime, EndTime, Status)
            VALUES (@p, @d, @s, @date, @start, @end, @status)
        "

            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@p", CInt(CmbPatient.SelectedValue))
            cmd.Parameters.AddWithValue("@d", CInt(CmbDentist.SelectedValue))
            cmd.Parameters.AddWithValue("@s", CInt(CmbService.SelectedValue))
            cmd.Parameters.AddWithValue("@date", DtpDate.Value.Date)

            ' ⚠️ Use .Value if StartTime/EndTime are DATETIME in SQL
            ' Use .TimeOfDay if they are TIME in SQL
            cmd.Parameters.AddWithValue("@start", dtpStartTime.Value.TimeOfDay)
            cmd.Parameters.AddWithValue("@end", DtpEndTime.Value.TimeOfDay)

            cmd.Parameters.AddWithValue("@status", cmbStatus.Text)

            Try
                cmd.ExecuteNonQuery()
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

        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "
            UPDATE Appointments
            SET PatientID=@p, UserID=@d, ServiceID=@s, Date=@date,
                StartTime=@start, EndTime=@end, Status=@status
            WHERE AppointmentID=@id
        "

            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@id", selectedAppointmentID)
            cmd.Parameters.AddWithValue("@p", CInt(CmbPatient.SelectedValue))
            cmd.Parameters.AddWithValue("@d", CInt(CmbDentist.SelectedValue))
            cmd.Parameters.AddWithValue("@s", CInt(CmbService.SelectedValue))
            cmd.Parameters.AddWithValue("@date", DtpDate.Value.Date)

            ' ⚠️ Adjust depending on SQL column type
            cmd.Parameters.AddWithValue("@start", dtpStartTime.Value.TimeOfDay)
            cmd.Parameters.AddWithValue("@end", DtpEndTime.Value.TimeOfDay)

            cmd.Parameters.AddWithValue("@status", cmbStatus.Text)

            Try
                cmd.ExecuteNonQuery()
                MessageBox.Show("Appointment updated successfully.")

                ' ✅ Audit log entry
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

        If MessageBox.Show("Are you sure you want to delete this appointment?",
                           "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "DELETE FROM Appointments WHERE AppointmentID=@id"
            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@id", selectedAppointmentID)

            Try
                cmd.ExecuteNonQuery()
                MessageBox.Show("Appointment deleted successfully.")
                ' ✅ Audit log entry
                SystemSession.LogAudit("Appointment Deleted", "Appointment Management",
                           SystemSession.LoggedInUserID,
                           SystemSession.LoggedInFullName,
                           SystemSession.LoggedInRole)

            Catch ex As Exception
                MessageBox.Show("Error deleting appointment: " & ex.Message)
            End Try
        End Using

        ' ✅ Refresh UI
        LoadAppointments()
        Dashboard?.LoadDashboardStats()
        ClearForm()
    End Sub
    Private Sub DGVAppointments_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVAppointments.CellContentClick

    End Sub

    Private Sub DGVAppointments_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVAppointments.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DGVAppointments.Rows(e.RowIndex)

            ' ✅ Capture the AppointmentID for update/delete
            selectedAppointmentID = CInt(row.Cells("AppointmentID").Value)

            ' ✅ Populate ComboBoxes (use SelectedValue if bound)
            CmbPatient.Text = row.Cells("Patient").Value.ToString()
            CmbDentist.Text = row.Cells("Dentist").Value.ToString()
            CmbService.Text = row.Cells("Service").Value.ToString()

            ' ✅ Date
            DtpDate.Value = CDate(row.Cells("Date").Value)

            ' ✅ Time handling
            Try
                ' If stored as TIME in SQL
                dtpStartTime.Value = Date.Today.Add(TimeSpan.Parse(row.Cells("StartTime").Value.ToString()))
                DtpEndTime.Value = Date.Today.Add(TimeSpan.Parse(row.Cells("EndTime").Value.ToString()))
            Catch ex As Exception
                ' If stored as DATETIME in SQL
                dtpStartTime.Value = CDate(row.Cells("StartTime").Value)
                DtpEndTime.Value = CDate(row.Cells("EndTime").Value)
            End Try

            ' ✅ Status
            cmbStatus.Text = row.Cells("Status").Value.ToString()
        End If
    End Sub

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

    Private Sub ClearForm()
        CmbPatient.SelectedIndex = 0
        CmbDentist.SelectedIndex = 0
        CmbService.SelectedIndex = 0
        DtpDate.Value = Date.Today
        dtpStartTime.Value = Date.Now
        DtpEndTime.Value = Date.Now.AddMinutes(30)
        cmbStatus.SelectedIndex = 0
        selectedAppointmentID = 0
    End Sub

    Private Sub DGVAppointments_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVAppointments.CellDoubleClick
        If e.RowIndex >= 0 Then
            Dim row = DGVAppointments.Rows(e.RowIndex)

            ' ✅ Capture AppointmentID
            selectedAppointmentID = CInt(row.Cells("AppointmentID").Value)

            ' ✅ Populate ComboBoxes (use SelectedValue if bound)
            CmbPatient.Text = row.Cells("Patient").Value.ToString()
            CmbDentist.Text = row.Cells("Dentist").Value.ToString()
            CmbService.Text = row.Cells("Service").Value.ToString()

            ' ✅ Date
            DtpDate.Value = CDate(row.Cells("Date").Value)

            ' ✅ Time handling
            Try
                dtpStartTime.Value = Date.Today.Add(TimeSpan.Parse(row.Cells("StartTime").Value.ToString()))
                DtpEndTime.Value = Date.Today.Add(TimeSpan.Parse(row.Cells("EndTime").Value.ToString()))
            Catch ex As Exception
                ' If stored as DATETIME instead of TIME
                dtpStartTime.Value = CDate(row.Cells("StartTime").Value)
                DtpEndTime.Value = CDate(row.Cells("EndTime").Value)
            End Try

            ' ✅ Status
            cmbStatus.Text = row.Cells("Status").Value.ToString()
        End If

    End Sub

    Private Sub AdminDBAppointments_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        LoadComboBoxes()
    End Sub
End Class