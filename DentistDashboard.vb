Imports System.Data.SqlClient

Public Class DentistDashboard

    Private Sub LogoutPictureBox1_Click(sender As Object, e As EventArgs) Handles LogoutPictureBox1.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            SystemSession.PerformLogout(Me.Name)
            Me.Close()
            Login.Show()
        End If
    End Sub
    Private Sub denTab_SelectedIndexChanged(sender As Object, e As EventArgs) Handles denTab.SelectedIndexChanged
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
                SELECT A.AppointmentID, P.FullName AS Patient, U.FullName AS Dentist,
                       S.ServiceName, A.Date, A.StartTime, A.EndTime, A.Status
                FROM Appointments A
                JOIN Patients P ON A.PatientID = P.PatientID
                JOIN Users U ON A.UserID = U.UserID AND U.Role = 'Dentist'
                JOIN Services S ON A.ServiceID = S.ServiceID
                ORDER BY A.Date DESC, A.StartTime ASC
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
                ORDER BY FullName
            "
            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            dgvPatients.DataSource = dt
        End Using
    End Sub


    Private Sub Guna2CustomGradientPanel1_Paint(sender As Object, e As PaintEventArgs) Handles Guna2CustomGradientPanel1.Paint

    End Sub
End Class