Imports System.Data.SqlClient
Imports System.Windows.Forms.DataVisualization.Charting

Public Class AdminDBRepandAnalytics

    ' =========================
    ' FORM LOAD
    ' =========================
    Private Sub AdminDBRepandAnalytics_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set default dates to today so it's not empty on start
        DtpFrom.Value = DateTime.Now
        DtpTo.Value = DateTime.Now

        LoadSuppliers()
        RefreshAllData()
    End Sub

    ' The "Master Switch" that updates everything
    Private Sub RefreshAllData()
        LoadTransactions()
        LoadItem()
        LoadStockLevelsChart()
        LoadTransactionTrendsChart()
        LoadSummaryBoxes()
    End Sub

    ' =========================
    ' DATA RETRIEVAL (The Filter Engine)
    ' =========================
    Private Function GetFilteredData() As DataTable
        ' This is the heart of your filters
        Dim query As String = "
            SELECT 
                t.TransactionID,
                t.TransactionDate,
                t.TransactionType,
                t.Quantity,
                i.ItemName, 
                s.SupplierName
            FROM StockTransactions t
            INNER JOIN ItemManagement i ON t.ItemID = i.ItemID
            INNER JOIN Suppliers s ON i.SupplierID = s.SupplierID
            WHERE t.TransactionDate BETWEEN @From AND @To
        "

        If CmbSupplier.SelectedIndex > 0 Then
            query &= " AND s.SupplierName = @Supplier"
        End If

        If BRIn.Checked Then
            query &= " AND t.TransactionType = 'IN'"
        ElseIf RBOut.Checked Then
            query &= " AND t.TransactionType = 'OUT'"
        End If

        Dim dt As New DataTable()
        Using conn As New SqlConnection(My.Settings.DentalDBConnection2)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@From", DtpFrom.Value.Date)
                cmd.Parameters.AddWithValue("@To", DtpTo.Value.Date)
                If CmbSupplier.SelectedIndex > 0 Then
                    cmd.Parameters.AddWithValue("@Supplier", CmbSupplier.Text)
                End If
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    ' =========================
    ' SUPPLIER LOADING
    ' =========================
    Private Sub LoadSuppliers()
        ' Only get suppliers that actually have items in ItemManagement
        Dim query As String = "
        SELECT DISTINCT s.SupplierName
        FROM Suppliers s
        INNER JOIN ItemManagement i ON s.SupplierID = i.SupplierID
        ORDER BY s.SupplierName
    "

        Using conn As New SqlConnection(My.Settings.DentalDBConnection2)
            Using cmd As New SqlCommand(query, conn)
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                CmbSupplier.Items.Clear()
                CmbSupplier.Items.Add("All") ' Add "All" first
                While reader.Read()
                    CmbSupplier.Items.Add(reader("SupplierName").ToString())
                End While
            End Using
        End Using
        CmbSupplier.SelectedIndex = 0
    End Sub

    ' =========================
    ' DATAGRIDVIEW LOADING
    ' =========================
    Private Sub LoadTransactions()
        ' Now using the FILTERED data instead of a blind SELECT
        DGVStockTrackTransaction.DataSource = GetFilteredData()

        If DGVStockTrackTransaction.Columns.Contains("TransactionID") Then
            DGVStockTrackTransaction.Columns("TransactionID").Visible = False
        End If
        FormatDGV(DGVStockTrackTransaction)
        If DGVStockTrackTransaction.Columns.Contains("TransactionDate") Then
            DGVStockTrackTransaction.Columns("TransactionDate").DefaultCellStyle.Format = "dd/MM/yyyy"
        End If
    End Sub

    Private Sub LoadItem()
        ' Items should also filter by Supplier
        Dim query As String = "
            SELECT i.ItemID, i.ItemName, i.Quantity, i.Price 
            FROM ItemManagement i
            INNER JOIN Suppliers s ON i.SupplierID = s.SupplierID
        "

        If CmbSupplier.SelectedIndex > 0 Then
            query &= " WHERE s.SupplierName = @Supplier"
        End If

        Using conn As New SqlConnection(My.Settings.DentalDBConnection2)
            Using cmd As New SqlCommand(query, conn)
                If CmbSupplier.SelectedIndex > 0 Then
                    cmd.Parameters.AddWithValue("@Supplier", CmbSupplier.Text)
                End If
                Dim dt As New DataTable()
                Dim adapter As New SqlDataAdapter(cmd)
                adapter.Fill(dt)
                DGVItemManagement.DataSource = dt
            End Using
        End Using

        If DGVItemManagement.Columns.Contains("ItemID") Then
            DGVItemManagement.Columns("ItemID").Visible = False
        End If
        FormatDGV(DGVItemManagement)
    End Sub

    Private Sub FormatDGV(ByRef dgv As DataGridView)
        dgv.ReadOnly = True
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv.AllowUserToAddRows = False
    End Sub

    ' =========================
    ' CHART LOADING
    ' =========================
    Private Sub LoadStockLevelsChart()
        ' Link chart to the filtered Item view
        Dim dt As DataTable = CType(DGVItemManagement.DataSource, DataTable)

        ChartStockLevels.Series.Clear()
        Dim series = ChartStockLevels.Series.Add("Stock")
        series.ChartType = SeriesChartType.Bar
        series.Color = Color.SeaGreen

        If dt IsNot Nothing Then
            For Each row As DataRow In dt.Rows
                series.Points.AddXY(row("ItemName"), row("Quantity"))
            Next
        End If
    End Sub

    Private Sub LoadTransactionTrendsChart()
        Dim dtTrans As DataTable = GetFilteredData()

        ChartTransactionTrends.Series.Clear()
        Dim sIn = ChartTransactionTrends.Series.Add("IN")
        Dim sOut = ChartTransactionTrends.Series.Add("OUT")

        sIn.ChartType = SeriesChartType.Column
        sIn.Color = Color.Green
        sOut.ChartType = SeriesChartType.Column
        sOut.Color = Color.Red

        For Each row As DataRow In dtTrans.Rows
            Dim dateVal As Date = CDate(row("TransactionDate"))
            Dim qty As Integer = CInt(row("Quantity"))
            If row("TransactionType").ToString() = "IN" Then
                sIn.Points.AddXY(dateVal, qty)
            Else
                sOut.Points.AddXY(dateVal, qty)
            End If
        Next
    End Sub

    ' =========================
    ' SUMMARY BOXES (Filtered)
    ' =========================
    Private Sub LoadSummaryBoxes()
        Dim dt As DataTable = GetFilteredData()

        ' Calculate from the already filtered table for consistency
        Dim totalIn As Integer = 0
        Dim totalOut As Integer = 0

        For Each row As DataRow In dt.Rows
            If row("TransactionType").ToString() = "IN" Then
                totalIn += CInt(row("Quantity"))
            Else
                totalOut += CInt(row("Quantity"))
            End If
        Next

        LBLTotalIn.Text = totalIn.ToString()
        LBLTotalOut.Text = totalOut.ToString()

        ' Current Stock matches the filtered DGVItemManagement
        Dim currentStock As Integer = 0
        Dim dtItems As DataTable = CType(DGVItemManagement.DataSource, DataTable)
        If dtItems IsNot Nothing Then
            For Each row As DataRow In dtItems.Rows
                currentStock += CInt(row("Quantity"))
            Next
        End If
        LBLCurrentStock.Text = currentStock.ToString()
    End Sub

    ' =========================
    ' BUTTON EVENTS
    ' =========================
    Private Sub BtnGenerateReport_Click(sender As Object, e As EventArgs) Handles BtnGenerateReport.Click
        RefreshAllData()
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub
End Class