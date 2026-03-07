Imports System.Data.SqlClient

Public Class DentistDashboard
    Private connectionString As String = My.Settings.DentalDBConnection2

    ' Variables to hold selected data
    Public PassedAppointmentID As Integer = 0
    Public PassedPatientID As Integer = 0
    Public CurrentPatientName As String = ""

    Private Sub DentistDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDashboardStats()
    End Sub

    ' === THIS PART CLEARS THE SELECTION WHEN YOU RETURN ===
    Private Sub DentistDashboard_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        ' Reset the tab selection to the first tab (Dashboard Home) 
        ' or set to -1 if you want nothing selected at all.
        denTab.SelectedIndex = 0

        ' Reload stats to show updated counts
        LoadDashboardStats()
    End Sub

    ' === SIDEBAR NAVIGATION ===
    Private Sub DenTab_SelectedIndexChanged(sender As Object, e As EventArgs) Handles denTab.SelectedIndexChanged
        ' We only trigger navigation if a tab is actually clicked
        ' This prevents loops when we reset the index in the Activated event
        If denTab.SelectedIndex = -1 Or denTab.SelectedIndex = 0 Then Exit Sub

        Select Case denTab.SelectedTab.Name
            Case "tabAppointment"
                Dim frmAppt As New AvailableAppointments()
                frmAppt.Show()
                Me.Hide()

            Case "tabPatientManagement"
                LoadPatientsGrid()

            Case "tabTreatmentRecords"
                Dim frmTreatment As New TreatmentRecords()

                ' Pass the data
                frmTreatment.PassedAppointmentID = Me.PassedAppointmentID
                frmTreatment.PassedPatientID = Me.PassedPatientID
                frmTreatment.CurrentPatientName = Me.CurrentPatientName

                frmTreatment.Show()
                Me.Hide()
        End Select
    End Sub

    ' === DASHBOARD STATS ===
    Public Sub LoadDashboardStats()
        Try
            Using con As New SqlConnection(connectionString)
                con.Open()
                lblTotalPatients.Text = New SqlCommand("SELECT COUNT(*) FROM Patients WHERE IsActive=1", con).ExecuteScalar().ToString()
                lblTotalDentists.Text = New SqlCommand("SELECT COUNT(*) FROM Users WHERE Role = 'Dentist'", con).ExecuteScalar().ToString()

                Dim sqlPending As String = "SELECT COUNT(*) FROM Appointments WHERE UserID = @uid AND Date = CAST(GETDATE() AS DATE) AND Status <> 'Completed' AND Status <> 'Cancelled'"
                Using cmdToday As New SqlCommand(sqlPending, con)
                    cmdToday.Parameters.AddWithValue("@uid", SystemSession.LoggedInUserID)
                    lblAppointmentsToday.Text = cmdToday.ExecuteScalar().ToString()
                End Using

                Dim sqlDone As String = "SELECT COUNT(*) FROM Appointments WHERE UserID = @uid AND Date = CAST(GETDATE() AS DATE) AND Status = 'Completed'"
                Using cmdDone As New SqlCommand(sqlDone, con)
                    cmdDone.Parameters.AddWithValue("@uid", SystemSession.LoggedInUserID)
                    lblCompletedAppointments.Text = cmdDone.ExecuteScalar().ToString()
                End Using
            End Using
        Catch ex As Exception
            ' Silent fail
        End Try
    End Sub

    Private Sub LoadPatientsGrid()
        Try
            Using con As New SqlConnection(connectionString)
                con.Open()
                Dim da As New SqlDataAdapter("SELECT FullName, ContactNumber, Email FROM Patients WHERE IsActive=1", con)
                Dim dt As New DataTable()
                da.Fill(dt)
                dgvPatients.DataSource = dt
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LogoutPictureBox1_Click(sender As Object, e As EventArgs) Handles LogoutPictureBox1.Click
        If MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.OKCancel) = DialogResult.OK Then
            SystemSession.PerformLogout(Me.Name)
            Me.Close()
        End If
    End Sub
End Class