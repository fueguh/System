Imports System.Data.SqlClient

Public Class AdminDashboard
    Private Sub AdminDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDashboardStats()
        AutoUpdateFollowUpStatuses()
    End Sub
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
    Private Sub Appointment_Click(sender As Object, e As EventArgs) Handles Appointment.Click
        AdminDBAppointments.Show()
        Me.Hide()
    End Sub
    Private Sub Reports_Click(sender As Object, e As EventArgs) Handles Reports.Click
        AdminDBReports.Show()
        Me.Hide()
    End Sub

    Private Sub User_Maintenance_Click(sender As Object, e As EventArgs) Handles User_Maintenance.Click
        AdminDBUsers.Show()
        Me.Hide()
    End Sub

    Private Sub Staff_Maintenance_Click(sender As Object, e As EventArgs) Handles Staff_Maintenance.Click
        AdminDBStaffMaintenance.Show()
        Me.Hide()
    End Sub

    Private Sub Patient_Maintenance_Click(sender As Object, e As EventArgs) Handles Patient_Maintenance.Click
        AdminDBPatients.Show()
        Me.Hide()
    End Sub

    Private Sub Service_Maintenance_Click(sender As Object, e As EventArgs) Handles Service_Maintenance.Click
        Me.Hide()
        AdminDBServices.Show()
    End Sub

    Public Sub LoadDashboardStats()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
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

            ' Missed Appointments
            lblCompletedAppointments.Text = cmd4.ExecuteScalar().ToString()
            Dim cmd8 As New SqlCommand("
            SELECT COUNT(*) FROM Appointments 
            WHERE Status = 'Pending'
            ", con)

        End Using
    End Sub

    Private Sub Treatment_Record_Click(sender As Object, e As EventArgs) Handles Treatment_Record.Click
        ' Create the form as an "object" first to avoid the reference error
        Dim frm As New AdminDBTreatmentRecords()
        frm.Show()
        Me.Hide()
    End Sub

    Private Sub AuditTrail_Click(sender As Object, e As EventArgs) Handles AuditTrail.Click
        AdminAuditTrailForm.Show()
        Me.Hide()
    End Sub

    Private Sub LogoutPictureBox1_Click(sender As Object, e As EventArgs)
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            SystemSession.PerformLogout(Me.Name)
            Me.Close()
        End If
    End Sub

    Private Sub Item_Management_Click(sender As Object, e As EventArgs) Handles Item_Management.Click
        AdminDBItemManagement.Show()
        Me.Hide()
    End Sub

    Private Sub Stock_Tracking_Click(sender As Object, e As EventArgs) Handles Stock_Tracking.Click
        AdminDBStockTracking.Show()
        Me.Hide()
    End Sub

    Private Sub Analytics_Click(sender As Object, e As EventArgs) Handles Analytics.Click
        AdminDBRepandAnalytics.Show()
        Me.Hide()
    End Sub

    Private Sub BtnLogout_Click(sender As Object, e As EventArgs)
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            SystemSession.PerformLogout(Me.Name)
            Me.Close()
        End If
    End Sub

    Private Sub Supplier_Maintenance_Click(sender As Object, e As EventArgs) Handles Supplier_Maintenance.Click
        AdminDBSupplier.Show()
        Me.Hide()
    End Sub

    Private Sub Category_Maintenance_Click(sender As Object, e As EventArgs) Handles Category_Maintenance.Click
        AdminDBCategory.Show()
        Me.Hide()
    End Sub

    Private Sub Dentist_Maintenance_Click(sender As Object, e As EventArgs) Handles Dentist_Maintenance.Click
        AdminDBDentists.Show()
        Me.Hide()
    End Sub

    Private Sub Admin_Maintenance_Click(sender As Object, e As EventArgs) Handles Admin_Maintenance.Click
        AdminDBAdminMaintenance.Show()
        Me.Hide()
    End Sub

    Private Sub Availability_Maintenance_Click(sender As Object, e As EventArgs) Handles Availability_Maintenance.Click
        AdminDBDentistSchedule.Show()
        Me.Hide()
    End Sub

    Private Sub TodaysAppointment_Click(sender As Object, e As EventArgs) Handles TodaysAppointment.Click
        AdminDBAppointmentsToday.Show()
        Me.Hide()
    End Sub

    Private Sub Payment_Click(sender As Object, e As EventArgs) Handles Payment.Click
        AdminDBPayment.Show()
        Me.Hide()
    End Sub

    Private Sub Payment_History_Click_1(sender As Object, e As EventArgs) Handles Payment_History.Click
        AdminDBPaymentHistory.Show()
        Me.Hide()
    End Sub

    Private Sub Patient_History_Click(sender As Object, e As EventArgs) Handles Patient_History.Click
        AdminDBPatientHistory.Show()
        Me.Hide()
    End Sub

    Private Sub Follow_up_Click(sender As Object, e As EventArgs) Handles Follow_up.Click
        AdminDBFollowUps.Show()
        Me.Hide()
    End Sub
    Private Sub AutoUpdateFollowUpStatuses()

        Using conn As New SqlConnection(My.Settings.DentalDBConnection2)
            conn.Open()

            Dim query As String =
            "
        UPDATE dbo.PatientFollowUps
        SET Status = 
            CASE
                WHEN FollowUpDate < CAST(GETDATE() AS DATE)
                     AND Status = 'Scheduled'
                     THEN 'Overdue'

                WHEN FollowUpDate < DATEADD(DAY, -1, CAST(GETDATE() AS DATE))
                     AND Status = 'Overdue'
                     THEN 'Missed'

                ELSE Status
            END
        "

            Using cmd As New SqlCommand(query, conn)
                cmd.ExecuteNonQuery()
            End Using

        End Using

    End Sub

    Private Sub Logout_Click(sender As Object, e As EventArgs) Handles Logout.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            SystemSession.PerformLogout(Me.Name)
            Me.Close()
        End If
    End Sub
End Class