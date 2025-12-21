Imports System.Data.SqlClient

Public Class AdminDBReports
    Public Shared adminDBReports As AdminDBReports
    Private Sub AdminDBReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDailyAppointments()
        LoadDentistWorkload()
        LoadServiceUsage()
        LoadPatientSummary()
        LoadAppointmentHistory()

    End Sub
    Private Sub LoadDailyAppointments()
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String = "
            SELECT Date, COUNT(*) AS TotalAppointments
            FROM Appointments
            GROUP BY Date
            ORDER BY Date DESC
        "

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DgvDailyAppointments.DataSource = dt
        End Using
    End Sub
    Private Sub LoadDentistWorkload()
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String = "
            SELECT D.FullName AS Dentist, COUNT(A.AppointmentID) AS TotalAppointments
            FROM Dentists D
            LEFT JOIN Appointments A ON D.DentistID = A.DentistID
            GROUP BY D.FullName
            ORDER BY TotalAppointments DESC
        "

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DgvDentistWorkload.DataSource = dt
        End Using
    End Sub
    Private Sub LoadServiceUsage()
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String = "
            SELECT 
                S.ServiceName,
                A.Date AS DateUsed,
                COUNT(*) OVER (PARTITION BY S.ServiceName, A.Date) AS TimesUsedOnDate
            FROM Services S
            LEFT JOIN Appointments A ON S.ServiceID = A.ServiceID
            ORDER BY S.ServiceName, A.Date DESC
        "

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DgvServiceUsage.DataSource = dt

        End Using
    End Sub
    Private Sub LoadPatientSummary()
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String = "
            SELECT 
                P.FullName,
                DATENAME(MONTH, P.DateRegistered) AS RegistrationMonth,
                COUNT(*) OVER (PARTITION BY MONTH(P.DateRegistered)) AS MonthlyCount
            FROM Patients P
            ORDER BY MONTH(P.DateRegistered), P.FullName
        "

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DgvPatientSummary.DataSource = dt

        End Using
    End Sub

    Private Sub TabRep_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabRep.SelectedIndexChanged
        Select Case TabRep.SelectedIndex
            Case 0 : LoadDailyAppointments()
            Case 1 : LoadDentistWorkload()
            Case 2 : LoadServiceUsage()
            Case 3 : LoadPatientSummary()
        End Select
    End Sub

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        AdminDashboard.Show()
        Me.Hide()
    End Sub
    Public Sub LoadAppointmentHistory()
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
            DGVAppointmentHistory.DataSource = dt
        End Using
    End Sub
    Private Sub DGVAppointmentHistory_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVAppointmentHistory.CellContentClick

    End Sub
End Class