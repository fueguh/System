Public Class StaffDashboard
    Private Sub StaffDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub PatientManagementToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PatientManagementToolStripMenuItem1.Click
        AdminDBPatients.Show()
    End Sub

    Private Sub AppointmentToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles AppointmentToolStripMenuItem2.Click
        AdminDBAppointments.Show()
        Me.Hide()
    End Sub
    Private Sub BillingAndPaymentsToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles BillingAndPaymentsToolStripMenuItem3.Click
        AdminAuditTrailForm.Show()
        Me.Hide()
    End Sub
    Private Sub LogoutPictureBox1_Click(sender As Object, e As EventArgs) Handles LogoutPictureBox1.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            SystemSession.PerformLogout(Me.Name)
            Me.Close()
            Login.Show()
        End If
    End Sub
End Class