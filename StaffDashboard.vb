Imports System.Data.SqlClient

Public Class StaffDashboard
    Private Sub StaffDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDashboardStats()
    End Sub

    Public Sub LoadDashboardStats()
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            ' Total Patients

            Dim cmd1 As New SqlCommand("SELECT COUNT(*) FROM Patients", con)
            lblTotalPatients.Text = cmd1.ExecuteScalar().ToString()

            ' Total Dentists from Users table
            Dim cmd2 As New SqlCommand("SeLECT COUNT(*) FROM Users WHERE Role = 'Dentist'", con)
            lblTotalDentists.Text = cmd2.ExecuteScalar().ToString()

            ' Appointments Today
            Dim cmd3 As New SqlCommand("
            SELECT COUNT(*) FROM Appointments 
            WHERE Date = CAST(GETDATE() AS DATE)
        ", con)
            lblAppointmentsToday.Text = cmd3.ExecuteScalar().ToString()

            ' Completed Appointments
            Dim cmd4 As New SqlCommand("
            SELECT COUNT(*) FROM Appointments 
            WHERE Status = 'Completed'
        ", con)
            lblCompletedAppointments.Text = cmd4.ExecuteScalar().ToString()
        End Using
    End Sub

    Private Sub PatientManagementToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PatientManagementToolStripMenuItem1.Click
        AdminDBPatients.Show()
        Me.Hide()
    End Sub

    Private Sub AppointmentToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles AppointmentToolStripMenuItem2.Click
        AdminDBAppointments.Show()
        Me.Hide()
    End Sub
    Private Sub BillingAssistance_Click(sender As Object, e As EventArgs) Handles BillingAssistance.Click
        MessageBox.Show("Billing and Payments screen not yet implemented.", "Coming Soon")
    End Sub

    Private Sub LogoutPictureBox1_Click(sender As Object, e As EventArgs) Handles LogoutPictureBox1.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            SystemSession.PerformLogout(Me.Name)
            Me.Close()
        End If
    End Sub

    Private Sub StaffDashboard_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()
            Dim cmd As New SqlCommand("SELECT ClinicName FROM ClinicInfo WHERE ClinicID=1", con)
            Dim clinicName As Object = cmd.ExecuteScalar()
            If clinicName IsNot Nothing Then
                lblClinicName.Text = clinicName.ToString()
            Else
                lblClinicName.Text = "Dental Clinic Management System"
            End If
        End Using
    End Sub
End Class