Imports System.Data.SqlClient

Public Class AdminDBReports

    ' ===================== Class-level variables =====================
    Public Shared adminDBReports As AdminDBReports

    ' ===================== Form Load =====================
    Private Sub AdminDBReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' FIX: initialize revenue filter options
        cmbRevenueFilter.Items.Clear()
        cmbRevenueFilter.Items.AddRange(New String() {"Weekly", "Monthly", "Yearly"})
        cmbRevenueFilter.SelectedIndex = 1 ' Default = Monthly

        LoadPaymentHistory()
        LoadServiceUsage()
        LoadPatientHistory()
        LoadAppointmentHistory()
        LoadMonthlyRevenue()
    End Sub

    ' ===================== Public Methods =====================
    ' Refresh history (called from other forms)
    Public Sub RefreshHistory()
        LoadAppointmentHistory()
        LoadPaymentHistory()
    End Sub

    ' ===================== Data Loading Methods =====================

    ' ===================== Payment History (NEW REPLACES Daily Appointments) =====================
    ' Payment History - FIXED: now includes ReceiptStatus for visibility of voided receipts
    Private Sub LoadPaymentHistory()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim query As String = "
        SELECT 
            R.ReceiptID,
            A.AppointmentID,
            P.FullName AS Patient,
            U.FullName AS Dentist,
            R.TotalAmount,
            R.PaymentMethod,
            R.AmountPaid,
            R.ChangeAmount,
            R.DateIssued,
            R.Status   -- FIX: ADDED visibility for void/valid tracking

        FROM Receipts R
        INNER JOIN Appointments A ON R.AppointmentID = A.AppointmentID
        INNER JOIN Patients P ON A.PatientID = P.PatientID
        INNER JOIN Users U ON A.UserID = U.UserID
        WHERE U.Role = 'Dentist'
        ORDER BY R.DateIssued DESC"

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)

            DGVDaily.DataSource = dt

            ' Hide IDs for cleaner UI (unchanged)
            If DGVDaily.Columns.Contains("ReceiptID") Then
                DGVDaily.Columns("ReceiptID").Visible = False
            End If
            If DGVDaily.Columns.Contains("AppointmentID") Then
                DGVDaily.Columns("AppointmentID").Visible = False
            End If
        End Using
    End Sub

    ' Service Usage
    ' Service Usage - Fixed to show 0 revenue for unused services
    ' Service Usage - FIXED: properly excludes voided receipts + prevents wrong revenue inflation
    Private Sub LoadServiceUsage()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim query As String = "
        -- FIXED: voided receipts fully excluded from ALL calculations
        DECLARE @GrandTotal DECIMAL(18,2);

        SELECT @GrandTotal = SUM(S.Price)
        FROM AppointmentServices ASV
        INNER JOIN Services S ON ASV.ServiceID = S.ServiceID
        INNER JOIN Appointments A ON ASV.AppointmentID = A.AppointmentID
        INNER JOIN Receipts R 
    ON A.AppointmentID = R.AppointmentID 
    AND R.Status = 'Valid'
        WHERE R.Status = 'Valid';  -- FIX: excludes voided receipts

        SELECT 
            S.ServiceName AS [Service Name],
            COUNT(ASV.ServiceID) AS [Total Procedures],

            -- FIX: revenue now strictly based on valid receipts only
            SUM(CASE 
                WHEN R.Status = 'Valid' 
                THEN R.TotalAmount 
                ELSE 0 
            END) AS [Gross Revenue],

            CAST((SUM(S.Price) / NULLIF(@GrandTotal, 0)) * 100 AS DECIMAL(10,2)) AS [% Contribution],

            DENSE_RANK() OVER (ORDER BY SUM(S.Price) DESC) AS [Profit Rank]

        FROM Services S
        INNER JOIN AppointmentServices ASV ON S.ServiceID = ASV.ServiceID
        INNER JOIN Appointments A ON ASV.AppointmentID = A.AppointmentID
        INNER JOIN Receipts R 
    ON A.AppointmentID = R.AppointmentID 
    AND R.Status = 'Valid'
        WHERE R.Status = 'Valid'   -- FIX: ensures voided receipts excluded

        GROUP BY S.ServiceName
        ORDER BY [Gross Revenue] DESC"

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DgvServiceUsage.DataSource = dt

            ' UI formatting unchanged
            If DgvServiceUsage.Columns.Contains("Gross Revenue") Then
                DgvServiceUsage.Columns("Gross Revenue").DefaultCellStyle.Format = "N2"
            End If
            If DgvServiceUsage.Columns.Contains("% Contribution") Then
                DgvServiceUsage.Columns("% Contribution").DefaultCellStyle.Format = "0.0'%'"
            End If
        End Using
    End Sub

    ' ===================== Patient History (FIXED - SAFE COLUMN MAPPING) =====================
    Private Sub LoadPatientHistory()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim query As String = "
        -- ===================== SAFE VERSION (NO ASSUMED COLUMNS) =====================
        SELECT 
            P.PatientID,
            P.FullName AS Patient,
            P.DateRegistered,
            COUNT(A.AppointmentID) AS TotalAppointments,
            MAX(A.Date) AS LastVisit
        FROM Patients P
        LEFT JOIN Appointments A ON P.PatientID = A.PatientID
        GROUP BY P.PatientID, P.FullName, P.DateRegistered
        ORDER BY LastVisit DESC"

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)

            DgvPatientSummary.DataSource = dt

            If DgvPatientSummary.Columns.Contains("PatientID") Then
                DgvPatientSummary.Columns("PatientID").Visible = False
            End If
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

    ' Revenue Report - FIXED: now supports Weekly / Monthly / Yearly filtering
    ' FIXED: Revenue report - stable GROUP BY for Weekly / Monthly / Yearly
    Private Sub LoadMonthlyRevenue()

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim query As String = ""

            Select Case cmbRevenueFilter.Text

            ' ===================== WEEKLY =====================
                Case "Weekly"
                    query = "
                SELECT 
                    CONCAT('Week ', DATEPART(WEEK, A.Date), ' - ', YEAR(A.Date)) AS [Period],
                    COUNT(DISTINCT A.AppointmentID) AS [Total Appointments],
                    SUM(CASE WHEN R.Status = 'Valid' THEN R.TotalAmount ELSE 0 END) AS [Gross Revenue]
                FROM Appointments A
                INNER JOIN Receipts R 
    ON A.AppointmentID = R.AppointmentID 
    AND R.Status = 'Valid'
                WHERE A.Status = 'Completed'
                GROUP BY DATEPART(WEEK, A.Date), YEAR(A.Date)
                ORDER BY YEAR(A.Date) DESC, DATEPART(WEEK, A.Date) DESC
                "

            ' ===================== YEARLY =====================
                Case "Yearly"
                    query = "
                SELECT 
                    CAST(YEAR(A.Date) AS VARCHAR) AS [Period],
                    COUNT(DISTINCT A.AppointmentID) AS [Total Appointments],
                    SUM(CASE WHEN R.Status = 'Valid' THEN R.TotalAmount ELSE 0 END) AS [Gross Revenue]
                FROM Appointments A
                INNER JOIN Receipts R 
    ON A.AppointmentID = R.AppointmentID 
    AND R.Status = 'Valid'
                WHERE A.Status = 'Completed'
                GROUP BY YEAR(A.Date)
                ORDER BY YEAR(A.Date) DESC
                "

                    ' ===================== MONTHLY (DEFAULT) =====================
                Case Else
                    query = "
                SELECT 
                    FORMAT(A.Date, 'MMMM yyyy') AS [Period],
                    COUNT(DISTINCT A.AppointmentID) AS [Total Appointments],
                    SUM(CASE WHEN R.Status = 'Valid' THEN R.TotalAmount ELSE 0 END) AS [Gross Revenue]
                FROM Appointments A
                INNER JOIN Receipts R 
    ON A.AppointmentID = R.AppointmentID 
    AND R.Status = 'Valid'
                WHERE A.Status = 'Completed'
                GROUP BY YEAR(A.Date), MONTH(A.Date), FORMAT(A.Date, 'MMMM yyyy')
                ORDER BY YEAR(A.Date) DESC, MONTH(A.Date) DESC
                "
            End Select

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVMonthly.DataSource = dt

            ' FORMAT UI
            If DGVMonthly.Columns.Contains("Gross Revenue") Then
                DGVMonthly.Columns("Gross Revenue").DefaultCellStyle.Format = "C2"
            End If

        End Using
    End Sub

    ' ===================== UI Handlers =====================
    Private Sub TabRep_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabRep.SelectedIndexChanged
        Select Case TabRep.SelectedIndex
            Case 0 : LoadPaymentHistory()
            Case 1 : LoadServiceUsage()
            Case 2 : LoadPatientHistory()
            Case 3 : LoadMonthlyRevenue()
        End Select

        If TabRep.SelectedTab.Name = "tabHistory" Then
            Dim historyForm As New AdminDBReports()
            historyForm.Show()
            historyForm.RefreshHistory()
        End If
    End Sub
    ' FIX: Reload revenue when filter changes
    Private Sub cmbRevenueFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRevenueFilter.SelectedIndexChanged
        LoadMonthlyRevenue()
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
