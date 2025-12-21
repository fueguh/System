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
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            ' Patients
            Dim dtPatients As New DataTable()
            Dim da1 As New SqlDataAdapter("SELECT PatientID, FullName FROM Patients", con)
            da1.Fill(dtPatients)
            CmbPatient.DataSource = dtPatients
            CmbPatient.DisplayMember = "FullName"
            CmbPatient.ValueMember = "PatientID"

            ' Dentists
            Dim dtDentists As New DataTable()
            Dim da2 As New SqlDataAdapter("SELECT DentistID, FullName FROM Dentists", con)
            da2.Fill(dtDentists)
            CmbDentist.DataSource = dtDentists
            CmbDentist.DisplayMember = "FullName"
            CmbDentist.ValueMember = "DentistID"

            ' Services
            Dim dtServices As New DataTable()
            Dim da3 As New SqlDataAdapter("SELECT ServiceID, ServiceName FROM Services", con)
            da3.Fill(dtServices)
            CmbService.DataSource = dtServices
            CmbService.DisplayMember = "ServiceName"
            CmbService.ValueMember = "ServiceID"
        End Using
    End Sub

    Private Sub LoadAppointments()
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
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
            JOIN Dentists D ON A.DentistID = D.DentistID
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
        If CmbPatient.SelectedIndex = -1 Or CmbDentist.SelectedIndex = -1 Or CmbService.SelectedIndex = -1 Then
            MessageBox.Show("Please fill all fields.")
            Return False
        End If

        If DtpEndTime.Value <= dtpStartTime.Value Then
            MessageBox.Show("End time must be later than start time.")
            Return False
        End If

        Return True
    End Function

    Private Function IsConflict(Optional appointmentID As Integer = 0) As Boolean
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String = "
            SELECT * FROM Appointments
            WHERE DentistID = @dentist
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
        If Not ValidateFields() Then Exit Sub
        If IsConflict() Then
            MessageBox.Show("This dentist already has an appointment at this time.")
            Exit Sub
        End If

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String = "
            INSERT INTO Appointments (PatientID, DentistID, ServiceID, Date, StartTime, EndTime, Status)
            VALUES (@p, @d, @s, @date, @start, @end, @status)
        "

            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@p", CInt(CmbPatient.SelectedValue))
            cmd.Parameters.AddWithValue("@d", CInt(CmbDentist.SelectedValue))
            cmd.Parameters.AddWithValue("@s", CInt(CmbService.SelectedValue))
            cmd.Parameters.AddWithValue("@date", DtpDate.Value.Date)
            cmd.Parameters.AddWithValue("@start", dtpStartTime.Value.TimeOfDay)
            cmd.Parameters.AddWithValue("@end", DtpEndTime.Value.TimeOfDay)
            cmd.Parameters.AddWithValue("@status", cmbStatus.Text)
            cmd.ExecuteNonQuery()
        End Using

        MessageBox.Show("Appointment added successfully.")
        LoadAppointments()

        'to reload the system overview in admin dashboard after input
        Dashboard?.LoadDashboardStats()
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

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String = "
            UPDATE Appointments
            SET PatientID=@p, DentistID=@d, ServiceID=@s, Date=@date,
                StartTime=@start, EndTime=@end, Status=@status
            WHERE AppointmentID=@id
        "

            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@id", selectedAppointmentID)
            cmd.Parameters.AddWithValue("@p", CInt(CmbPatient.SelectedValue))
            cmd.Parameters.AddWithValue("@d", CInt(CmbDentist.SelectedValue))
            cmd.Parameters.AddWithValue("@s", CInt(CmbService.SelectedValue))
            cmd.Parameters.AddWithValue("@date", DtpDate.Value.Date)
            cmd.Parameters.AddWithValue("@start", dtpStartTime.Value.TimeOfDay)
            cmd.Parameters.AddWithValue("@end", DtpEndTime.Value.TimeOfDay)
            cmd.Parameters.AddWithValue("@status", cmbStatus.Text)
            cmd.ExecuteNonQuery()
        End Using

        MessageBox.Show("Appointment updated successfully.")
        LoadAppointments()
        'to reload the system overview in admin dashboard after input
        Dashboard?.LoadDashboardStats()
        ' Refresh reports tab if open
        AdminDBReports?.LoadAppointmentHistory()

        ClearForm()
    End Sub

    Private Sub BTNDelete_Click(sender As Object, e As EventArgs) Handles BTNDelete.Click
        If selectedAppointmentID = 0 Then
            MessageBox.Show("Please select an appointment to delete.")
            Exit Sub
        End If

        If MessageBox.Show("Are you sure you want to delete this appointment?",
                           "Confirm", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String = "DELETE FROM Appointments WHERE AppointmentID=@id"

            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@id", selectedAppointmentID)
            cmd.ExecuteNonQuery()
        End Using

        MessageBox.Show("Appointment deleted successfully.")
        LoadAppointments()
        'to reload the system overview in admin dashboard after input
        Dashboard?.LoadDashboardStats()
        ClearForm()
    End Sub
    Private Sub DGVAppointments_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVAppointments.CellContentClick

    End Sub

    Private Sub DGVAppointments_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVAppointments.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DGVAppointments.Rows(e.RowIndex)

            ' Capture the AppointmentID for update/delete
            selectedAppointmentID = CInt(row.Cells("AppointmentID").Value)

            ' Populate the form fields with the selected row values
            CmbPatient.Text = row.Cells("Patient").Value.ToString()
            CmbDentist.Text = row.Cells("Dentist").Value.ToString()
            CmbService.Text = row.Cells("Service").Value.ToString()
            DtpDate.Value = CDate(row.Cells("Date").Value)

            ' StartTime and EndTime are stored as TimeSpan in SQL, so combine with today's date
            dtpStartTime.Value = Date.Today.Add(TimeSpan.Parse(row.Cells("StartTime").Value.ToString()))
            DtpEndTime.Value = Date.Today.Add(TimeSpan.Parse(row.Cells("EndTime").Value.ToString()))

            cmbStatus.Text = row.Cells("Status").Value.ToString()
        End If
    End Sub

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        If Dashboard Is Nothing Then
            Dashboard = New AdminDashboard()
        End If

        Dashboard.Show()
        Me.Hide()
    End Sub

    Private Sub ClearForm()
        CmbPatient.SelectedIndex = -1
        CmbDentist.SelectedIndex = -1
        CmbService.SelectedIndex = -1
        DtpDate.Value = Date.Today
        dtpStartTime.Value = Date.Now
        DtpEndTime.Value = Date.Now.AddMinutes(30)
        cmbStatus.SelectedIndex = -1
        selectedAppointmentID = -1
    End Sub

    Private Sub DGVAppointments_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVAppointments.CellDoubleClick
        If e.RowIndex >= 0 Then
            Dim row = DGVAppointments.Rows(e.RowIndex)
            selectedAppointmentID = CInt(row.Cells("AppointmentID").Value)
            CmbPatient.Text = row.Cells("Patient").Value.ToString()
            CmbDentist.Text = row.Cells("Dentist").Value.ToString()
            CmbService.Text = row.Cells("Service").Value.ToString()
            DtpDate.Value = CDate(row.Cells("Date").Value)
            dtpStartTime.Value = Date.Today.Add(TimeSpan.Parse(row.Cells("StartTime").Value.ToString()))
            DtpEndTime.Value = Date.Today.Add(TimeSpan.Parse(row.Cells("EndTime").Value.ToString()))
            cmbStatus.Text = row.Cells("Status").Value.ToString()
        End If
    End Sub
End Class