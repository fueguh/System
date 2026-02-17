Imports System.Data.SqlClient

Public Class DentistDashboard
    Private Sub DentistDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDashboardStats()
    End Sub

    Private Sub LogoutPictureBox1_Click(sender As Object, e As EventArgs) Handles LogoutPictureBox1.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            SystemSession.PerformLogout(Me.Name)
            Me.Close()
        End If
    End Sub

    Private Sub DentistDashboard_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
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

    Private Sub DenTab_SelectedIndexChanged(sender As Object, e As EventArgs) Handles denTab.SelectedIndexChanged
        Select Case denTab.SelectedTab.Name
            Case "tabTreatmentRecords"
                ' Open Treatment Records form
                TreatmentRecords.Show()
                Me.Hide()

            Case "tabAppointment"
                ' Refresh the Appointment DataGridView
                LoadAppointmentsGrid()

            Case "tabPatientManagement"
                ' Refresh the Patient Management DataGridView
                LoadPatientsGrid()

        End Select
    End Sub

    ' === Load Appointments into DataGridView ===
    Private Sub LoadAppointmentsGrid()
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "
             SELECT
    A.AppointmentID,
    A.PatientID,
    A.UserID,
    P.FullName AS Patient,
    D.FullName AS Dentist,
    STRING_AGG(S.ServiceName, ', ') AS Services,
    A.Date,
    A.StartTime,
    A.EndTime,
    A.Status
FROM Appointments A
JOIN Patients P ON A.PatientID = P.PatientID
JOIN Users D ON A.UserID = D.UserID AND D.Role = 'Dentist'
JOIN AppointmentServices ASV ON A.AppointmentID = ASV.AppointmentID
JOIN Services S ON ASV.ServiceID = S.ServiceID
GROUP BY
    A.AppointmentID,
    A.PatientID,
    A.UserID,
    P.FullName,
    D.FullName,
    A.Date,
    A.StartTime,
    A.EndTime,
    A.Status
ORDER BY A.Date DESC;


       "

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            dgvAppointments.DataSource = dt
        End Using
    End Sub


    ' === Load Patients into DataGridView ===
    Private Sub LoadPatientsGrid()
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim query As String = "
                SELECT PatientID, FullName, BirthDate, ContactNumber, Email,Address,DateRegistered
                FROM Patients
                where IsActive=1
                ORDER BY PatientID
            "
            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            dgvPatients.DataSource = dt
        End Using
    End Sub


    Private Sub Guna2CustomGradientPanel1_Paint(sender As Object, e As PaintEventArgs) Handles Guna2CustomGradientPanel1.Paint

    End Sub

    Private Sub DgvPatients_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPatients.CellContentClick

    End Sub

    Private Sub TabTreatmentRecords_Click(sender As Object, e As EventArgs) Handles tabTreatmentRecords.Click

    End Sub
End Class