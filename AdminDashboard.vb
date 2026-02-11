Imports System.Data.SqlClient

Public Class AdminDashboard
    Private Sub Guna2PictureBox1_Click(sender As Object, e As EventArgs)
        AdminDBAppointments.Show()
        Me.Hide()
    End Sub

    Private Sub Guna2PictureBox2_Click(sender As Object, e As EventArgs)
        AdminDBPatients.Show()
        Me.Hide()
    End Sub

    Private Sub Guna2PictureBox3_Click(sender As Object, e As EventArgs)
        AdminDBDentists.Show()
        Me.Hide()
    End Sub
    Public Shared Dashboard As AdminDashboard

    Private Sub Guna2PictureBox4_Click(sender As Object, e As EventArgs)
        AdminDBServices.Show()
        Me.Hide()
    End Sub

    Private Sub Guna2PictureBox5_Click(sender As Object, e As EventArgs)
        AdminDBReports.Show()
        Me.Hide()
    End Sub
    Private Sub SystemOverviewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SystemOverviewToolStripMenuItem.Click
        AdminDBAppointments.Show()
        Me.Hide()
    End Sub
    Private Sub ReportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportsToolStripMenuItem.Click
        AdminDBReports.Show()
        Me.Hide()
    End Sub

    Private Sub UserManagementToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ManageUsersForm.Click
        AdminDBUsers.Show()
        Me.Hide()
    End Sub

    Private Sub DentistManagementToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ManageDentistsForm.Click
        AdminDBDentists.Show()
        Me.Hide()
    End Sub

    Private Sub PatientManagementToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ManagePatientsForm.Click
        AdminDBPatients.Show()
        Me.Hide()
    End Sub

    Private Sub ServicesManagementToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ManageServicesForm.Click
        AdminDBServices.Show()
        Me.Hide()
    End Sub

    Private Sub AdminDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDashboardStats()
    End Sub
    Public Sub LoadDashboardStats()
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
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

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        TreatmentRecords.Show()
        Me.Hide()
    End Sub

    Private Sub AuditTrailToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AuditTrailToolStripMenuItem.Click
        AdminAuditTrailForm.Show()
        Me.Hide()
    End Sub

    Private Sub ClinicSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClinicSettingsToolStripMenuItem.Click
        'show clinic settings form
        ClinicSettings.Show()
        Me.Hide()
    End Sub

    Private Sub LogoutPictureBox1_Click(sender As Object, e As EventArgs) Handles LogoutPictureBox1.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            SystemSession.PerformLogout(Me.Name)
            Me.Close()
        End If
    End Sub

    'change clinic name into the one from clinifInfo table
    Private Sub AdminDashboard_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
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

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs)
        AdminDBPayment.Show()
        Me.Hide()
    End Sub

    Private Sub ItemManagementToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ItemManagementToolStripMenuItem.Click
        AdminDBItemManagement.Show()
        Me.Hide()
    End Sub

    Private Sub StockTrackingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StockTrackingToolStripMenuItem.Click
        AdminDBStockTracking.Show()
        Me.Hide()
    End Sub

    Private Sub ReportsAnalyticsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportsAnalyticsToolStripMenuItem.Click
        AdminDBRepandAnalytics.Show()
        Me.Hide()
    End Sub
End Class