Imports System.Data.SqlClient
Imports System.Windows.Forms.DataVisualization.Charting

Public Class AdminDBRepandAnalytics
    Private Sub Guna2GroupBox1_Click(sender As Object, e As EventArgs) Handles GrpFilters.Click

    End Sub

    Private Sub AdminDBRepandAnalytics_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTransactions()
        LoadInventory()
        LoadStockLevelsChart()
        LoadSupplierContributionsChart()
        LoadTransactionTrendsChart()
        LoadSummaryBoxes()
    End Sub

    '======================================== LOADS '========================================
    '======================================== LOADS '========================================

    Private Sub LoadTransactions()
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()
            Dim query As String = "
            SELECT t.TransactionID, i.ItemName, t.TransactionType, 
                   t.Quantity, t.Supplier, t.TransactionDate
            FROM InventoryTransactions t
            INNER JOIN Inventory i ON t.ItemID = i.ItemID
            ORDER BY t.TransactionDate DESC"
            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVItemManagement.DataSource = dt
        End Using
    End Sub

    Private Sub LoadInventory()
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()
            Dim query As String = "
            SELECT ItemID, ItemName, Quantity, Supplier, ExpiryDate
            FROM Inventory
            ORDER BY ItemName"
            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVStockTrackTransaction.DataSource = dt
        End Using
    End Sub

    Private Sub LoadStockLevelsChart()
        ChartStockLevels.Series.Clear()
        ChartStockLevels.ChartAreas.Clear()
        ChartStockLevels.ChartAreas.Add("MainArea")
        ChartStockLevels.Series.Add("Stock")
        ChartStockLevels.Series("Stock").ChartType = SeriesChartType.Bar

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()
            Dim da As New SqlDataAdapter("SELECT ItemName, Quantity FROM Inventory", con)
            Dim dt As New DataTable()
            da.Fill(dt)

            ChartStockLevels.DataSource = dt
            ChartStockLevels.Series("Stock").XValueMember = "ItemName"
            ChartStockLevels.Series("Stock").YValueMembers = "Quantity"
            ChartStockLevels.DataBind()
        End Using
    End Sub

    Private Sub LoadSupplierContributionsChart()
        ChartSupplierContributions.Series.Clear()
        ChartSupplierContributions.ChartAreas.Clear()
        ChartSupplierContributions.ChartAreas.Add("MainArea")
        ChartSupplierContributions.Series.Add("Suppliers")
        ChartSupplierContributions.Series("Suppliers").ChartType = SeriesChartType.Pie

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()
            Dim da As New SqlDataAdapter("
            SELECT Supplier, SUM(Quantity) AS TotalQty
            FROM InventoryTransactions
            GROUP BY Supplier", con)
            Dim dt As New DataTable()
            da.Fill(dt)

            ChartSupplierContributions.DataSource = dt
            ChartSupplierContributions.Series("Suppliers").XValueMember = "Supplier"
            ChartSupplierContributions.Series("Suppliers").YValueMembers = "TotalQty"
            ChartSupplierContributions.DataBind()
        End Using
    End Sub

    Private Sub LoadTransactionTrendsChart()
        ChartTransactionTrends.Series.Clear()
        ChartTransactionTrends.ChartAreas.Clear()
        ChartTransactionTrends.ChartAreas.Add("MainArea")
        ChartTransactionTrends.Series.Add("Trends")
        ChartTransactionTrends.Series("Trends").ChartType = SeriesChartType.Line

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()
            Dim da As New SqlDataAdapter("
            SELECT CAST(TransactionDate AS DATE) AS DateOnly, COUNT(*) AS Count
            FROM InventoryTransactions
            GROUP BY CAST(TransactionDate AS DATE)
            ORDER BY DateOnly", con)
            Dim dt As New DataTable()
            da.Fill(dt)

            ChartTransactionTrends.DataSource = dt
            ChartTransactionTrends.Series("Trends").XValueMember = "DateOnly"
            ChartTransactionTrends.Series("Trends").YValueMembers = "Count"
            ChartTransactionTrends.DataBind()
        End Using
    End Sub

    Private Sub LoadSummaryBoxes()
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            ' Total IN
            Dim cmdIn As New SqlCommand("SELECT ISNULL(SUM(Quantity),0) FROM InventoryTransactions WHERE TransactionType='IN'", con)
            LBLTotalIn.Text = "Total IN: " & cmdIn.ExecuteScalar().ToString()

            ' Total OUT
            Dim cmdOut As New SqlCommand("SELECT ISNULL(SUM(Quantity),0) FROM InventoryTransactions WHERE TransactionType='OUT'", con)
            LBLTotalOut.Text = "Total OUT: " & cmdOut.ExecuteScalar().ToString()

            ' Current Stock
            Dim cmdStock As New SqlCommand("SELECT ISNULL(SUM(Quantity),0) FROM Inventory", con)
            LBLCurrentStock.Text = "Current Stock: " & cmdStock.ExecuteScalar().ToString()
        End Using
    End Sub

    Private Sub BtnGenerateReport_Click(sender As Object, e As EventArgs) Handles BtnGenerateReport.Click
        LoadTransactions()
        LoadInventory()
        LoadStockLevelsChart()
        LoadSupplierContributionsChart()
        LoadTransactionTrendsChart()
        LoadSummaryBoxes()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub
End Class