Imports System.Data.SqlClient

Public Class AdminDBReports

    Public Shared adminDBReports As AdminDBReports

    Private Sub AdminDBReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        cmbRevenueFilter.Items.Clear()
        cmbRevenueFilter.Items.AddRange(New String() {"Weekly", "Monthly", "Yearly"})
        cmbRevenueFilter.SelectedIndex = 1

        LoadPaymentHistory()
        LoadServiceUsage()
        LoadPatientHistory()
        LoadAppointmentHistory()
        LoadMonthlyRevenue()

    End Sub

    Public Sub RefreshHistory()
        LoadAppointmentHistory()
        LoadPaymentHistory()
    End Sub

    ' ===================== PAYMENT HISTORY =====================
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
                R.Status
            FROM Receipts R
            INNER JOIN Appointments A ON R.AppointmentID = A.AppointmentID
            INNER JOIN Patients P ON A.PatientID = P.PatientID
            INNER JOIN Users U ON A.UserID = U.UserID
            WHERE U.Role = 'Dentist'
              AND R.ReceiptStatus = 'Valid'
              AND R.Status = 'Completed'
              AND R.VoidedAt IS NULL
              AND A.Status = 'Completed'
            ORDER BY R.DateIssued DESC"

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)

            DGVDaily.DataSource = dt

            If DGVDaily.Columns.Contains("ReceiptID") Then
                DGVDaily.Columns("ReceiptID").Visible = False
            End If
            If DGVDaily.Columns.Contains("AppointmentID") Then
                DGVDaily.Columns("AppointmentID").Visible = False
            End If

        End Using
    End Sub

    ' ===================== SERVICE USAGE (FIXED) =====================
    Private Sub LoadServiceUsage()

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim query As String = "
            WITH ValidAppointments AS (
                SELECT 
                    A.AppointmentID,
                    ASV.ServiceID
                FROM Appointments A
                INNER JOIN AppointmentServices ASV 
                    ON A.AppointmentID = ASV.AppointmentID
                WHERE A.Status = 'Completed'
            ),

            ValidReceipts AS (
                SELECT 
                    R.AppointmentID,
                    R.TotalAmount
                FROM Receipts R
                INNER JOIN Appointments A ON R.AppointmentID = A.AppointmentID
                WHERE R.ReceiptStatus = 'Valid'
                  AND R.Status = 'Completed'
                  AND R.VoidedAt IS NULL
                  AND A.Status = 'Completed'
            )

            SELECT 
                S.ServiceName AS [Service Name],
                COUNT(VA.ServiceID) AS [Total Procedures],
                ISNULL(SUM(VR.TotalAmount), 0) AS [Gross Revenue],
                DENSE_RANK() OVER (ORDER BY ISNULL(SUM(VR.TotalAmount), 0) DESC) AS [Profit Rank]

            FROM Services S

            LEFT JOIN ValidAppointments VA 
                ON S.ServiceID = VA.ServiceID

            LEFT JOIN ValidReceipts VR 
                ON VA.AppointmentID = VR.AppointmentID

            GROUP BY S.ServiceName
            ORDER BY [Gross Revenue] DESC;
            "

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)

            DgvServiceUsage.DataSource = dt

            If DgvServiceUsage.Columns.Contains("Gross Revenue") Then
                DgvServiceUsage.Columns("Gross Revenue").DefaultCellStyle.Format = "N2"
            End If

        End Using
    End Sub

    ' ===================== PATIENT HISTORY =====================
    Private Sub LoadPatientHistory()

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim query As String = "
            SELECT 
                P.PatientID,
                P.FullName AS Patient,
                P.DateRegistered,
                COUNT(A.AppointmentID) AS TotalAppointments,
                MAX(A.Date) AS LastVisit
            FROM Patients P
            LEFT JOIN Appointments A ON P.PatientID = A.PatientID
            WHERE A.Status = 'Completed' OR A.Status IS NULL
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

    ' ===================== APPOINTMENT HISTORY =====================
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
            ORDER BY A.Date DESC, A.StartTime ASC"

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)

            DgvAppointmentHistory.DataSource = dt

            If DgvAppointmentHistory.Columns.Contains("AppointmentID") Then
                DgvAppointmentHistory.Columns("AppointmentID").Visible = False
            End If

        End Using
    End Sub

    ' ===================== MONTHLY REVENUE =====================
    Private Sub LoadMonthlyRevenue()

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim query As String = ""

            Select Case cmbRevenueFilter.Text

                Case "Weekly"
                    query = "
                    SELECT 
                        CONCAT('Week ', DATEPART(WEEK, R.DateIssued), ' - ', YEAR(R.DateIssued)) AS Period,
                        COUNT(DISTINCT R.AppointmentID) AS [Total Appointments],
                        SUM(R.TotalAmount) AS [Gross Revenue]
                    FROM Receipts R
                    INNER JOIN Appointments A ON R.AppointmentID = A.AppointmentID
                    WHERE R.ReceiptStatus = 'Valid'
                      AND R.Status = 'Completed'
                      AND R.VoidedAt IS NULL
                      AND A.Status = 'Completed'
                    GROUP BY DATEPART(WEEK, R.DateIssued), YEAR(R.DateIssued)
                    ORDER BY YEAR(R.DateIssued) DESC, DATEPART(WEEK, R.DateIssued) DESC"

                Case "Yearly"
                    query = "
                    SELECT 
                        CAST(YEAR(R.DateIssued) AS VARCHAR(4)) AS Period,
                        COUNT(DISTINCT R.AppointmentID) AS [Total Appointments],
                        SUM(R.TotalAmount) AS [Gross Revenue]
                    FROM Receipts R
                    INNER JOIN Appointments A ON R.AppointmentID = A.AppointmentID
                    WHERE R.ReceiptStatus = 'Valid'
                      AND R.Status = 'Completed'
                      AND R.VoidedAt IS NULL
                      AND A.Status = 'Completed'
                    GROUP BY YEAR(R.DateIssued)
                    ORDER BY YEAR(R.DateIssued) DESC"

                Case Else
                    query = "
                    SELECT 
                        CAST(YEAR(R.DateIssued) AS VARCHAR(4)) + '-' + 
                        RIGHT('0' + CAST(MONTH(R.DateIssued) AS VARCHAR(2)), 2) AS Period,
                        COUNT(DISTINCT R.AppointmentID) AS [Total Appointments],
                        SUM(R.TotalAmount) AS [Gross Revenue]
                    FROM Receipts R
                    INNER JOIN Appointments A ON R.AppointmentID = A.AppointmentID
                    WHERE R.ReceiptStatus = 'Valid'
                      AND R.Status = 'Completed'
                      AND R.VoidedAt IS NULL
                      AND A.Status = 'Completed'
                    GROUP BY YEAR(R.DateIssued), MONTH(R.DateIssued)
                    ORDER BY YEAR(R.DateIssued) DESC, MONTH(R.DateIssued) DESC"
            End Select

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)

            DGVMonthly.DataSource = dt

            If DGVMonthly.Columns.Contains("Gross Revenue") Then
                DGVMonthly.Columns("Gross Revenue").DefaultCellStyle.Format = "C2"
            End If

        End Using
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
        End Select
    End Sub

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

End Class