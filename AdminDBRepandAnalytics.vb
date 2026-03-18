Imports System.Data.SqlClient
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Linq

Public Class AdminDBRepandAnalytics

    Private filteredTransactions As DataTable
    Private filteredItems As DataTable

    ' =========================
    ' FORM LOAD
    ' =========================
    Private Sub AdminDBRepandAnalytics_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Default dates to today
        DtpFrom.Value = DateTime.Now
        DtpTo.Value = DateTime.Now

        LoadSuppliers()
        RefreshAllData()
    End Sub

    ' =========================
    ' MASTER REFRESH
    ' =========================
    Private Sub RefreshAllData()
        ' Load filtered data once
        filteredTransactions = GetFilteredTransactions()
        filteredItems = GetFilteredItems()

        ' Update UI
        LoadTransactionGrid()
        LoadItemGrid()
        LoadStockLevelsChart()
        LoadTransactionTrendsChart()
        LoadSummaryBoxes()
    End Sub

    ' =========================
    ' DATA RETRIEVAL
    ' =========================
    Private Function GetFilteredTransactions() As DataTable
        Dim query As String = "
            SELECT t.TransactionID, t.TransactionDate, t.TransactionType, t.Quantity,
                   i.ItemName, s.SupplierName
            FROM StockTransactions t
            INNER JOIN ItemManagement i ON t.ItemID = i.ItemID
            INNER JOIN Suppliers s ON i.SupplierID = s.SupplierID
            WHERE t.TransactionDate BETWEEN @From AND @To
        "

        If CmbSupplier.SelectedIndex > 0 Then query &= " AND s.SupplierName = @Supplier"
        If BRIn.Checked Then query &= " AND t.TransactionType = 'IN'"
        If RBOut.Checked Then query &= " AND t.TransactionType = 'OUT'"

        Dim dt As New DataTable()
        Using conn As New SqlConnection(My.Settings.DentalDBConnection2),
              cmd As New SqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@From", DtpFrom.Value.Date)
            cmd.Parameters.AddWithValue("@To", DtpTo.Value.Date)
            If CmbSupplier.SelectedIndex > 0 Then cmd.Parameters.AddWithValue("@Supplier", CmbSupplier.Text)

            Using adapter As New SqlDataAdapter(cmd)
                adapter.Fill(dt)
            End Using
        End Using
        Return dt
    End Function

    Private Function GetFilteredItems() As DataTable
        Dim query As String = "
            SELECT i.ItemID, i.ItemName, i.Quantity, i.Price
            FROM ItemManagement i
            INNER JOIN Suppliers s ON i.SupplierID = s.SupplierID
        "

        If CmbSupplier.SelectedIndex > 0 Then query &= " WHERE s.SupplierName = @Supplier"

        Dim dt As New DataTable()
        Using conn As New SqlConnection(My.Settings.DentalDBConnection2),
              cmd As New SqlCommand(query, conn)
            If CmbSupplier.SelectedIndex > 0 Then cmd.Parameters.AddWithValue("@Supplier", CmbSupplier.Text)

            Using adapter As New SqlDataAdapter(cmd)
                adapter.Fill(dt)
            End Using
        End Using
        Return dt
    End Function

    ' =========================
    ' SUPPLIERS
    ' =========================
    Private Sub LoadSuppliers()
        Dim query As String = "
            SELECT DISTINCT s.SupplierName
            FROM Suppliers s
            INNER JOIN ItemManagement i ON s.SupplierID = i.SupplierID
            ORDER BY s.SupplierName
        "

        Using conn As New SqlConnection(My.Settings.DentalDBConnection2),
              cmd As New SqlCommand(query, conn)
            conn.Open()
            Using reader = cmd.ExecuteReader()
                CmbSupplier.Items.Clear()
                CmbSupplier.Items.Add("All")
                While reader.Read()
                    CmbSupplier.Items.Add(reader("SupplierName").ToString())
                End While
            End Using
        End Using

        CmbSupplier.SelectedIndex = 0
    End Sub

    ' =========================
    ' DATAGRIDS
    ' =========================
    Private Sub LoadTransactionGrid()
        DGVStockTrackTransaction.DataSource = filteredTransactions
        If DGVStockTrackTransaction.Columns.Contains("TransactionID") Then DGVStockTrackTransaction.Columns("TransactionID").Visible = False
        FormatDGV(DGVStockTrackTransaction)
        If DGVStockTrackTransaction.Columns.Contains("TransactionDate") Then DGVStockTrackTransaction.Columns("TransactionDate").DefaultCellStyle.Format = "dd/MM/yyyy"
    End Sub

    Private Sub LoadItemGrid()
        DGVItemManagement.DataSource = filteredItems
        If DGVItemManagement.Columns.Contains("ItemID") Then DGVItemManagement.Columns("ItemID").Visible = False
        FormatDGV(DGVItemManagement)
    End Sub

    Private Sub FormatDGV(dgv As DataGridView)
        dgv.ReadOnly = True
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv.AllowUserToAddRows = False
    End Sub

    ' =========================
    ' CHARTS
    ' =========================
    Private Sub LoadStockLevelsChart()
        ChartStockLevels.Series.Clear()
        Dim series = ChartStockLevels.Series.Add("Stock")
        series.ChartType = SeriesChartType.Bar
        series.Color = Color.SeaGreen

        If filteredItems IsNot Nothing Then
            For Each row As DataRow In filteredItems.Rows
                series.Points.AddXY(row("ItemName"), row("Quantity"))
            Next
        End If
    End Sub

    Private Sub LoadTransactionTrendsChart()
        ChartTransactionTrends.Series.Clear()
        Dim sIn = ChartTransactionTrends.Series.Add("IN")
        Dim sOut = ChartTransactionTrends.Series.Add("OUT")
        sIn.ChartType = SeriesChartType.Column
        sOut.ChartType = SeriesChartType.Column
        sIn.Color = Color.Green
        sOut.Color = Color.Red

        ' Aggregate by date
        If filteredTransactions IsNot Nothing AndAlso filteredTransactions.Rows.Count > 0 Then
            Dim grouped = From row In filteredTransactions.AsEnumerable()
                          Group row By tDate = CDate(row("TransactionDate")).Date, tType = row("TransactionType").ToString()
                          Into TotalQty = Sum(Convert.ToInt32(row("Quantity")))
                          Order By tDate
                          Select tDate, tType, TotalQty

            For Each g In grouped
                If g.tType = "IN" Then
                    sIn.Points.AddXY(g.tDate, g.TotalQty)
                Else
                    sOut.Points.AddXY(g.tDate, g.TotalQty)
                End If
            Next
        End If
    End Sub

    ' =========================
    ' SUMMARY BOXES
    ' =========================
    Private Sub LoadSummaryBoxes()
        Dim totalIn = If(filteredTransactions IsNot Nothing, filteredTransactions.AsEnumerable().Where(Function(r) r("TransactionType").ToString() = "IN").Sum(Function(r) Convert.ToInt32(r("Quantity"))), 0)
        Dim totalOut = If(filteredTransactions IsNot Nothing, filteredTransactions.AsEnumerable().Where(Function(r) r("TransactionType").ToString() = "OUT").Sum(Function(r) Convert.ToInt32(r("Quantity"))), 0)
        Dim currentStock = If(filteredItems IsNot Nothing, filteredItems.AsEnumerable().Sum(Function(r) Convert.ToInt32(r("Quantity"))), 0)

        LBLTotalIn.Text = totalIn.ToString()
        LBLTotalOut.Text = totalOut.ToString()
        LBLCurrentStock.Text = currentStock.ToString()
    End Sub

    ' =========================
    ' BUTTONS
    ' =========================
    Private Sub BtnGenerateReport_Click(sender As Object, e As EventArgs) Handles BtnGenerateReport.Click
        RefreshAllData()
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

End Class