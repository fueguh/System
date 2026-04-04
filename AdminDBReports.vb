Imports System.Data.SqlClient

Public Class AdminDBReports

    Public Shared adminDBReports As AdminDBReports

    ' ===================== FORM LOAD =====================
    Private Sub AdminDBReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        cmbRevenueFilter.Items.Clear()
        cmbRevenueFilter.Items.AddRange(New String() {"Weekly", "Monthly", "Yearly"})
        cmbRevenueFilter.SelectedIndex = 1

        LoadPaymentHistory()
        LoadServiceUsage()
        LoadPatientHistory()
        LoadAppointmentHistory()
        LoadMonthlyRevenue()
        LoadItemReports()
    End Sub

    ' ===================== COMMON QUERY =====================
    Private Function ExecuteQuery(query As String) As DataTable
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            Using da As New SqlDataAdapter(query, con)
                Dim dt As New DataTable()
                da.Fill(dt)
                Return dt
            End Using
        End Using
    End Function

    ' ===================== PAYMENT HISTORY =====================
    Private Sub LoadPaymentHistory()

        Dim query As String = "
            SELECT 
                R.ReceiptID,
                A.AppointmentID,
                P.FullName AS Patient,
                U.FullName AS Dentist,
                R.TotalAmount,
                R.VATableSales,
                R.VATAmount,
                R.PaymentMethod,
                R.AmountPaid,
                R.ChangeAmount,
                R.DateIssued,
                R.Status
            FROM Receipts R
            INNER JOIN Appointments A ON R.AppointmentID = A.AppointmentID
            INNER JOIN Patients P ON A.PatientID = P.PatientID
            INNER JOIN Users U ON A.UserID = U.UserID
            WHERE U.Role = 'Dentist'
              AND R.Status = 'Completed'   -- ✅ FIXED
            ORDER BY R.DateIssued DESC"

        DGVDaily.DataSource = ExecuteQuery(query)

        If DGVDaily.Columns.Contains("ReceiptID") Then DGVDaily.Columns("ReceiptID").Visible = False
        If DGVDaily.Columns.Contains("AppointmentID") Then DGVDaily.Columns("AppointmentID").Visible = False

        For Each col In {"TotalAmount", "VATableSales", "VATAmount", "AmountPaid", "ChangeAmount"}
            If DGVDaily.Columns.Contains(col) Then
                DGVDaily.Columns(col).DefaultCellStyle.Format = "N2"
            End If
        Next
    End Sub

    ' ===================== SERVICE USAGE (FIXED) =====================
    Private Sub LoadServiceUsage()

        Dim query As String = "
        WITH ValidServices AS (
            SELECT 
                ASV.ServiceID,
                S.Price
            FROM Receipts R
            INNER JOIN AppointmentServices ASV 
                ON R.AppointmentID = ASV.AppointmentID
            INNER JOIN Services S 
                ON ASV.ServiceID = S.ServiceID
            WHERE R.Status = 'Completed'
        )
        SELECT 
            S.ServiceName AS [Service Name],
            COUNT(VS.ServiceID) AS [Total Procedures],
            ISNULL(SUM(VS.Price), 0) AS [Gross Revenue],
            DENSE_RANK() OVER (ORDER BY ISNULL(SUM(VS.Price), 0) DESC) AS [Rank]

        FROM Services S
        LEFT JOIN ValidServices VS 
            ON S.ServiceID = VS.ServiceID

        GROUP BY S.ServiceName
        ORDER BY [Gross Revenue] DESC"

        DgvServiceUsage.DataSource = ExecuteQuery(query)

        If DgvServiceUsage.Columns.Contains("Gross Revenue") Then
            DgvServiceUsage.Columns("Gross Revenue").DefaultCellStyle.Format = "N2"
        End If

    End Sub

    ' ===================== PATIENT HISTORY =====================
    Private Sub LoadPatientHistory()

        Dim query As String = "
            SELECT 
                P.PatientID,
                P.FullName AS Patient,
                P.DateRegistered,
                COUNT(A.AppointmentID) AS TotalAppointments,
                MAX(A.Date) AS LastVisit
            FROM Patients P
            LEFT JOIN Appointments A 
                ON P.PatientID = A.PatientID AND A.Status = 'Completed'
            GROUP BY P.PatientID, P.FullName, P.DateRegistered
            ORDER BY LastVisit DESC"

        DgvPatientSummary.DataSource = ExecuteQuery(query)

        If DgvPatientSummary.Columns.Contains("PatientID") Then
            DgvPatientSummary.Columns("PatientID").Visible = False
        End If
    End Sub

    ' ===================== APPOINTMENT HISTORY =====================
    Public Sub LoadAppointmentHistory()

        Dim query As String = "
            SELECT 
                A.AppointmentID,
                P.FullName AS Patient,
                U.FullName AS Dentist,

                ISNULL((
                    SELECT STRING_AGG(S2.ServiceName, ', ')
                    FROM AppointmentServices ASV2
                    INNER JOIN Services S2 ON ASV2.ServiceID = S2.ServiceID
                    WHERE ASV2.AppointmentID = A.AppointmentID
                ), '') AS Services,

                A.Date,
                A.StartTime,
                A.EndTime,
                A.Status

            FROM Appointments A
            JOIN Patients P ON A.PatientID = P.PatientID
            JOIN Users U ON A.UserID = U.UserID 

            WHERE A.Status = 'Completed'

            ORDER BY A.Date DESC, A.StartTime ASC"

        DgvAppointmentHistory.DataSource = ExecuteQuery(query)

        If DgvAppointmentHistory.Columns.Contains("AppointmentID") Then
            DgvAppointmentHistory.Columns("AppointmentID").Visible = False
        End If
    End Sub

    ' ===================== REVENUE REPORT (CLEANED) =====================
    Private Sub LoadMonthlyRevenue()

        Dim query As String = ""

        Select Case cmbRevenueFilter.Text

            Case "Weekly"
                query = "
                SELECT 
                    CONCAT('Week ', DATEPART(WEEK, R.DateIssued), ' - ', YEAR(R.DateIssued)) AS Period,
                    COUNT(R.ReceiptID) AS [Total Receipts],
                    COUNT(R.AppointmentID) AS [Total Appointments],
                    SUM(R.TotalAmount) AS [Gross Revenue]

                FROM Receipts R
                WHERE R.Status = 'Completed'
                GROUP BY DATEPART(WEEK, R.DateIssued), YEAR(R.DateIssued)
                ORDER BY YEAR(R.DateIssued) DESC, DATEPART(WEEK, R.DateIssued) DESC"

            Case "Yearly"
                query = "
                SELECT 
                    YEAR(R.DateIssued) AS Period,
                    COUNT(R.ReceiptID) AS [Total Receipts],
                    COUNT(R.AppointmentID) AS [Total Appointments],
                    SUM(R.TotalAmount) AS [Gross Revenue]

                FROM Receipts R
                WHERE R.Status = 'Completed'
                GROUP BY YEAR(R.DateIssued)
                ORDER BY YEAR(R.DateIssued) DESC"

            Case Else
                query = "
                SELECT 
                    CONCAT(YEAR(R.DateIssued), '-', RIGHT('0' + CAST(MONTH(R.DateIssued) AS VARCHAR), 2)) AS Period,
                    COUNT(R.ReceiptID) AS [Total Receipts],
                    COUNT(R.AppointmentID) AS [Total Appointments],
                    SUM(R.TotalAmount) AS [Gross Revenue]

                FROM Receipts R
                WHERE R.Status = 'Completed'
                GROUP BY YEAR(R.DateIssued), MONTH(R.DateIssued)
                ORDER BY YEAR(R.DateIssued) DESC, MONTH(R.DateIssued) DESC"
        End Select

        DGVMonthly.DataSource = ExecuteQuery(query)

        If DGVMonthly.Columns.Contains("Gross Revenue") Then
            DGVMonthly.Columns("Gross Revenue").DefaultCellStyle.Format = "N2"
        End If

    End Sub
    ' ===================== ITEM REPORTS (FULL FIXED VERSION) =====================
    Private Sub LoadItemReports()

        Dim query As String = "
    SELECT 
        I.ItemID,
        I.ItemName,
        I.Price,
        I.Quantity AS CurrentStock,

        -- total quantity used (ONLY valid receipts)
        ISNULL(SUM(CASE WHEN R.Status = 'Completed' THEN RI.Quantity ELSE 0 END), 0) AS TotalQuantityUsed,

        -- only count valid receipts
        ISNULL(COUNT(DISTINCT CASE WHEN R.Status = 'Completed' THEN RI.ReceiptID END), 0) AS TimesSold,

        -- revenue ONLY from completed receipts
        ISNULL(SUM(CASE WHEN R.Status = 'Completed' THEN RI.Quantity * I.Price ELSE 0 END), 0) AS TotalRevenue,

        -- stock value always current
        ISNULL(I.Price * I.Quantity, 0) AS StockValue

    FROM ItemManagement I

    LEFT JOIN ReceiptItems RI 
        ON I.ItemID = RI.ItemID 
        AND RI.ItemType = 'Inventory'

    LEFT JOIN Receipts R 
        ON RI.ReceiptID = R.ReceiptID

    GROUP BY 
        I.ItemID, I.ItemName, I.Price, I.Quantity

    ORDER BY TotalRevenue DESC"

        DGVItemReports.DataSource = ExecuteQuery(query)

        ' Hide ID
        If DGVItemReports.Columns.Contains("ItemID") Then
            DGVItemReports.Columns("ItemID").Visible = False
        End If

        ' Format currency
        For Each col In {"Price", "TotalRevenue", "StockValue"}
            If DGVItemReports.Columns.Contains(col) Then
                DGVItemReports.Columns(col).DefaultCellStyle.Format = "N2"
            End If
        Next

    End Sub
    ' ===================== EVENTS =====================
    Private Sub cmbRevenueFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRevenueFilter.SelectedIndexChanged
        LoadMonthlyRevenue()
    End Sub

    Private Sub TabRep_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabRep.SelectedIndexChanged
        Select Case TabRep.SelectedIndex
            Case 0 : LoadPaymentHistory()
            Case 1 : LoadServiceUsage()
            Case 2 : LoadPatientHistory()
            Case 3 : LoadMonthlyRevenue()
            Case 4 : LoadAppointmentHistory()
            Case 5 : LoadItemReports()   ' ✅ NEW FIX
        End Select
    End Sub

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

End Class