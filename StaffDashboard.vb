Public Class StaffDashboard
    Private Sub PatientManagementToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PatientManagementToolStripMenuItem1.Click
        AdminDBPatients.Show()
    End Sub

    Private Sub AppointmentToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles AppointmentToolStripMenuItem2.Click
        AdminDBAppointments.Show()
        Me.Hide()
    End Sub
    Private Sub BillingAndPaymentsToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles BillingAndPaymentsToolStripMenuItem3.Click
        StaffBillingPayments.Show()
        Me.Hide()
    End Sub
    Private Sub LogoutPictureBox1_Click(sender As Object, e As EventArgs) Handles LogoutPictureBox1.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Me.Close()
            LoginForm.Show()
        End If
    End Sub

End Class