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
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
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
            If DGVDaily.Columns.Contains("AppointmentID") Then
                DGVDaily.Columns("AppointmentID").Visible = False
            End If
        End Using
    End Sub

    ' Dentist Workload
    Private Sub LoadDentistWorkload()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
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
    ' Service Usage - Fixed to show 0 revenue for unused services
    Private Sub LoadServiceUsage()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim query As String = "
        -- 1. Get the total clinic revenue first to calculate shares
        DECLARE @GrandTotal DECIMAL(18,2);
        SELECT @GrandTotal = SUM(S.Price) 
        FROM AppointmentServices ASV
        INNER JOIN Services S ON ASV.ServiceID = S.ServiceID
        INNER JOIN Appointments A ON ASV.AppointmentID = A.AppointmentID
        INNER JOIN Receipts R ON A.AppointmentID = R.AppointmentID
        WHERE A.Status = 'Completed';

        -- 2. Generate the descriptive report
        SELECT 
            S.ServiceName AS [Service Name],
            COUNT(ASV.ServiceID) AS [Total Procedures],
            SUM(S.Price) AS [Gross Revenue],
            -- Descriptive: Shows importance of service to the business
            CAST((SUM(S.Price) / NULLIF(@GrandTotal, 0)) * 100 AS DECIMAL(10,2)) AS [% Contribution],
            -- Descriptive: Ranking (1 = Most Profitable)
            DENSE_RANK() OVER (ORDER BY SUM(S.Price) DESC) AS [Profit Rank]
        FROM Services S
        INNER JOIN AppointmentServices ASV ON S.ServiceID = ASV.ServiceID
        INNER JOIN Appointments A ON ASV.AppointmentID = A.AppointmentID
        INNER JOIN Receipts R ON A.AppointmentID = R.AppointmentID
        WHERE A.Status = 'Completed'
        GROUP BY S.ServiceName
        ORDER BY [Gross Revenue] DESC"

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DgvServiceUsage.DataSource = dt

            ' Format columns for clarity
            If DgvServiceUsage.Columns.Contains("Gross Revenue") Then
                DgvServiceUsage.Columns("Gross Revenue").DefaultCellStyle.Format = "N2"
            End If
            If DgvServiceUsage.Columns.Contains("% Contribution") Then
                DgvServiceUsage.Columns("% Contribution").DefaultCellStyle.Format = "0.0'%'"
            End If
        End Using
    End Sub

    ' Patient Summary
    Private Sub LoadPatientSummary()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            ' This query groups registrations by month to show clinic growth
            Dim query As String = "
        SELECT 
            FORMAT(DateRegistered, 'MMMM yyyy') AS [Month Joined],
            COUNT(PatientID) AS [New Patients]
        FROM Patients
        GROUP BY FORMAT(DateRegistered, 'MMMM yyyy'), YEAR(DateRegistered), MONTH(DateRegistered)
        ORDER BY YEAR(DateRegistered) DESC, MONTH(DateRegistered) DESC"

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DgvPatientSummary.DataSource = dt
        End Using
    End Sub

    ' Appointment History (Completed)
    Public Sub LoadAppointmentHistory()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
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
            DgvAppointmentHistory.DataSource = dt
            If DgvAppointmentHistory.Columns.Contains("AppointmentID") Then
                DgvAppointmentHistory.Columns("AppointmentID").Visible = False
            End If
        End Using
    End Sub

    ' Monthly Revenue
    Private Sub LoadMonthlyRevenue()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim query As String = "
        WITH MonthlyStats AS (
            SELECT 
                FORMAT(A.Date, 'MMMM yyyy') AS [Month],
                YEAR(A.Date) as [YearNum],
                MONTH(A.Date) as [MonthNum],
                COUNT(DISTINCT A.AppointmentID) AS [Total Appointments],
                SUM(R.TotalAmount) AS [Gross Revenue]
            FROM Appointments A
            INNER JOIN Receipts R ON A.AppointmentID = R.AppointmentID
            WHERE A.Status = 'Completed'
            GROUP BY FORMAT(A.Date, 'MMMM yyyy'), YEAR(A.Date), MONTH(A.Date)
        )
        SELECT 
            [Month],
            [Total Appointments],
            [Gross Revenue],
            CAST([Gross Revenue] / NULLIF([Total Appointments], 0) AS DECIMAL(10,2)) AS [Avg Per Patient]
        FROM MonthlyStats
        ORDER BY [YearNum] DESC, [MonthNum] DESC"

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVMonthly.DataSource = dt

            ' UI Enhancement: Auto-format currency
            If DGVMonthly.Columns.Contains("Gross Revenue") Then
                DGVMonthly.Columns("Gross Revenue").DefaultCellStyle.Format = "C2"
            End If
            If DGVMonthly.Columns.Contains("Avg Per Patient") Then
                DGVMonthly.Columns("Avg Per Patient").DefaultCellStyle.Format = "C2"
            End If
        End Using
    End Sub

    ' Dentist Performance
    Private Sub LoadDentistPerformance()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
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
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
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

    Private Sub PnlHeader_Paint(sender As Object, e As PaintEventArgs) Handles pnlHeader.Paint

    End Sub

    Private Sub DGVPatientCount_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub DGVDentistPerformance_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub
End Class
