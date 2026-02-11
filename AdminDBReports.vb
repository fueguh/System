Imports System.Data.SqlClient

Public Class AdminDBReports

    ' ===================== Class-level variables =====================
    Public Shared adminDBReports As AdminDBReports

    ' ===================== Form Load =====================
    Private Sub AdminDBReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDailyAppointments()
        LoadDentistWorkload()
        LoadServiceUsage()
        LoadPatientSummary()
        LoadAppointmentHistory()
        LoadMonthlyRevenue()
        LoadDentistPerformance()
        LoadPatientCount()
    End Sub

    ' ===================== Public Methods =====================
    ' Refresh history (called from other forms)
    Public Sub RefreshHistory()
        LoadAppointmentHistory()
        LoadDailyAppointments()
    End Sub

    ' ===================== Data Loading Methods =====================

    ' Daily Appointments
    Private Sub LoadDailyAppointments()
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()
            Dim query As String = "
            SELECT 
                A.AppointmentID,
                P.FullName AS Patient,
                U.FullName AS Dentist,
                STRING_AGG(S.ServiceName, ', ') AS Services,
                A.Date,
                A.StartTime,
                A.EndTime,
                A.Status
            FROM Appointments A
            JOIN Patients P ON A.PatientID = P.PatientID
            JOIN Users U ON A.UserID = U.UserID AND U.Role = 'Dentist'
            LEFT JOIN AppointmentServices ASV ON A.AppointmentID = ASV.AppointmentID
            LEFT JOIN Services S ON ASV.ServiceID = S.ServiceID
            WHERE CAST(A.Date AS DATE) = CAST(GETDATE() AS DATE)
            GROUP BY A.AppointmentID, P.FullName, U.FullName, A.Date, A.StartTime, A.EndTime, A.Status
            ORDER BY A.StartTime
            "
            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVDaily.DataSource = dt
        End Using
    End Sub

    ' Dentist Workload
    Private Sub LoadDentistWorkload()
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()
            Dim query As String = "
            SELECT 
                U.FullName AS Dentist,
                COUNT(A.AppointmentID) AS TotalAppointments
            FROM Users U
            LEFT JOIN Appointments A ON U.UserID = A.UserID
            WHERE U.Role = 'Dentist'
            GROUP BY U.FullName
            ORDER BY TotalAppointments DESC
            "
            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DgvDentistWorkload.DataSource = dt
        End Using
    End Sub

    ' Service Usage
    Private Sub LoadServiceUsage()
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()
            Dim query As String = "
            SELECT 
                S.ServiceName,
                A.Date AS DateUsed,
                COUNT(*) AS TimesUsedOnDate
            FROM Services S
            LEFT JOIN AppointmentServices ASV ON S.ServiceID = ASV.ServiceID
            LEFT JOIN Appointments A ON ASV.AppointmentID = A.AppointmentID
            GROUP BY S.ServiceName, A.Date
            ORDER BY S.ServiceName, A.Date DESC
            "
            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DgvServiceUsage.DataSource = dt
        End Using
    End Sub

    ' Patient Summary
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

    ' Appointment History (Completed)
    Public Sub LoadAppointmentHistory()
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()
            Dim query As String = "
            SELECT 
                A.AppointmentID,
                P.FullName AS Patient,
                U.FullName AS Dentist,
                STRING_AGG(S.ServiceName, ', ') AS Services,
                A.Date,
                A.StartTime,
                A.EndTime,
                A.Status
            FROM Appointments A
            JOIN Patients P ON A.PatientID = P.PatientID
            JOIN Users U ON A.UserID = U.UserID AND U.Role = 'Dentist'
            LEFT JOIN AppointmentServices ASV ON A.AppointmentID = ASV.AppointmentID
            LEFT JOIN Services S ON ASV.ServiceID = S.ServiceID
            WHERE A.Status = 'Completed'
            GROUP BY A.AppointmentID, P.FullName, U.FullName, A.Date, A.StartTime, A.EndTime, A.Status
            ORDER BY A.Date DESC, A.StartTime ASC
            "
            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVAppointmentHistory.DataSource = dt
        End Using
    End Sub

    ' Monthly Revenue
    Private Sub LoadMonthlyRevenue()
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()
            Dim query As String = "
            SELECT 
                FORMAT(A.Date, 'MMMM yyyy') AS Month,
                SUM(S.Price) AS TotalRevenue
            FROM Appointments A
            JOIN AppointmentServices ASV ON A.AppointmentID = ASV.AppointmentID
            JOIN Services S ON ASV.ServiceID = S.ServiceID
            GROUP BY FORMAT(A.Date, 'MMMM yyyy')
            ORDER BY MIN(A.Date)
            "
            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVMonthly.DataSource = dt
        End Using
    End Sub

    ' Dentist Performance
    Private Sub LoadDentistPerformance()
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()
            Dim query As String = "
            SELECT 
                U.FullName AS Dentist,
                COUNT(A.AppointmentID) AS CompletedAppointments
            FROM Appointments A
            JOIN Users U ON A.UserID = U.UserID
            WHERE A.Status = 'Completed' AND U.Role = 'Dentist'
            GROUP BY U.FullName
            ORDER BY CompletedAppointments DESC
            "
            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVDentistPerformance.DataSource = dt
        End Using
    End Sub

    ' Patient Count
    Private Sub LoadPatientCount()
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()
            Dim query As String = "SELECT COUNT(PatientID) AS TotalPatients FROM Patients"
            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVPatientCount.DataSource = dt
        End Using
    End Sub

    ' ===================== UI Handlers =====================
    Private Sub TabRep_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabRep.SelectedIndexChanged
        Select Case TabRep.SelectedIndex
            Case 0 : LoadDailyAppointments()
            Case 1 : LoadDentistWorkload()
            Case 2 : LoadServiceUsage()
            Case 3 : LoadPatientSummary()
            Case 4 : LoadDentistPerformance()
            Case 5 : LoadMonthlyRevenue()
            Case 6 : LoadPatientCount()
        End Select

        If TabRep.SelectedTab.Name = "tabHistory" Then
            Dim historyForm As New AdminDBReports()
            historyForm.Show()
            historyForm.RefreshHistory()
        End If
    End Sub

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

End Class
