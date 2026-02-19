Imports System.Data.SqlClient
Imports System.Windows.Forms.DataVisualization.Charting

Public Class AdminDBRepandAnalytics
    Private Sub Guna2GroupBox1_Click(sender As Object, e As EventArgs) Handles GrpFilters.Click

    End Sub

    Private Sub AdminDBRepandAnalytics_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTransactions()
        LoadItem()
        LoadStockLevelsChart()
        LoadSupplierContributionsChart()
        LoadTransactionTrendsChart()
        LoadSummaryBoxes()
        LoadSuppliers()
    End Sub

    Private Function GetFilteredData() As DataTable
        ' Build base query joining ckTransactions with ItemManagement
        Dim query As String = "
        SELECT 
            t.TransactionDate,
            t.TransactionType,
            t.Quantity,
            i.ItemName,
            i.SupplierID,
            s.SupplierName
        FROM StockTransactions t
        INNER JOIN ItemManagement i ON t.ItemID = i.ItemID
        INNER JOIN Suppliers s ON i.SupplierID = s.SupplierID
        WHERE t.TransactionDate BETWEEN @From AND @To
    "

        ' Supplier filter
        If CmbSupplier.SelectedIndex > 0 Then ' Skip "All"
            query &= " AND s.SupplierName = @Supplier"
        End If

        ' Transaction type filter
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

                Dim adapter As New SqlDataAdapter(cmd)
                adapter.Fill(dt)
            End Using
        End Using
        Return dt
    End Function

    Private Sub LoadSuppliers()
        Dim query As String = "SELECT DISTINCT SupplierName FROM Suppliers ORDER BY SupplierName"

        Using conn As New SqlConnection(My.Settings.DentalDBConnection2)
            Using cmd As New SqlCommand(query, conn)
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                CmbSupplier.Items.Clear()
                While reader.Read()
                    CmbSupplier.Items.Add(reader("SupplierName").ToString())
                End While
            End Using
        End Using

        ' Optional: Add "All Suppliers" choice
        CmbSupplier.Items.Insert(0, "All")
        CmbSupplier.SelectedIndex = 0
    End Sub

    Private Sub LoadTransactions()
        Dim query As String = "SELECT t.TransactionID, i.ItemName, t.TransactionType, 
                                  t.Quantity, t.TransactionDate
                           FROM StockTransactions t
                           INNER JOIN ItemManagement i ON t.ItemID = i.ItemID"

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
          adapter As New SqlDataAdapter(query, connection)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            DGVStockTrackTransaction.DataSource = dt
        End Using

        ' Hide internal IDs
        If DGVStockTrackTransaction.Columns.Contains("TransactionID") Then
            DGVStockTrackTransaction.Columns("TransactionID").Visible = False
        End If
    End Sub

    Private Sub LoadItem()
        Dim query As String = "SELECT ItemID, ItemName, Quantity, Price 
                           FROM ItemManagement"

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
          adapter As New SqlDataAdapter(query, connection)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            DGVItemManagement.DataSource = dt
        End Using

        If DGVItemManagement.Columns.Contains("ItemID") Then
            DGVItemManagement.Columns("ItemID").Visible = False
        End If

    End Sub

    Private Sub LoadStockLevelsChart()
        Dim query As String = "SELECT ItemName, Quantity FROM ItemManagement"

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
          adapter As New SqlDataAdapter(query, connection)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ChartStockLevels.Series.Clear()
            ChartStockLevels.Series.Add("Stock")
            ChartStockLevels.Series("Stock").ChartType = DataVisualization.Charting.SeriesChartType.Bar

            For Each row As DataRow In dt.Rows
                ChartStockLevels.Series("Stock").Points.AddXY(row("ItemName"), row("Quantity"))
            Next
        End Using
    End Sub

    Private Sub LoadSupplierContributionsChart()
        Dim query As String = "SELECT s.SupplierName, SUM(i.Quantity) AS TotalSupplied
                           FROM ItemManagement i
                           INNER JOIN Suppliers s ON i.SupplierID = s.SupplierID
                           GROUP BY s.SupplierName"

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
          adapter As New SqlDataAdapter(query, connection)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ChartSupplierContributions.Series.Clear()
            ChartSupplierContributions.Series.Add("Suppliers")
            ChartSupplierContributions.Series("Suppliers").ChartType = DataVisualization.Charting.SeriesChartType.Column

            For Each row As DataRow In dt.Rows
                ChartSupplierContributions.Series("Suppliers").Points.AddXY(row("SupplierName"), row("TotalSupplied"))
            Next
        End Using
    End Sub

    Private Sub LoadTransactionTrendsChart()
        Dim query As String = "SELECT TransactionDate, SUM(CASE WHEN TransactionType='IN' THEN Quantity ELSE -Quantity END) AS NetChange
                           FROM StockTransactions
                           GROUP BY TransactionDate
                           ORDER BY TransactionDate"

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
          adapter As New SqlDataAdapter(query, connection)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ChartTransactionTrends.Series.Clear()
            ChartTransactionTrends.Series.Add("Trends")
            ChartTransactionTrends.Series("Trends").ChartType = DataVisualization.Charting.SeriesChartType.Line

            For Each row As DataRow In dt.Rows
                ChartTransactionTrends.Series("Trends").Points.AddXY(row("TransactionDate"), row("NetChange"))
            Next
        End Using
    End Sub

    Private Sub LoadSummaryBoxes()
        Dim query As String = "
        SELECT 
            (SELECT ISNULL(SUM(Quantity),0) FROM StockTransactions WHERE TransactionType='IN') AS TotalIn,
            (SELECT ISNULL(SUM(Quantity),0) FROM StockTransactions WHERE TransactionType='OUT') AS TotalOut,
            (SELECT ISNULL(SUM(Quantity),0) FROM ItemManagement) AS CurrentStock
    "

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
              cmd As New SqlCommand(query, connection)
            connection.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                LBLTotalIn.Text = reader("TotalIn").ToString()
                LBLTotalOut.Text = reader("TotalOut").ToString()
                LBLCurrentStock.Text = reader("CurrentStock").ToString()
            End If
            connection.Close()
        End Using
    End Sub

    Private Sub BtnGenerateReport_Click(sender As Object, e As EventArgs) Handles BtnGenerateReport.Click
        ' Get filtered dataset
        Dim dt As DataTable = GetFilteredData()

        ' --- Chart 1: Stock Levels ---
        If ChartStockLevels.Series.IndexOf("Stocks") = -1 Then
            ChartStockLevels.Series.Add("Stocks")
            ChartStockLevels.Series("Stocks").ChartType = SeriesChartType.Bar
        End If
        ChartStockLevels.DataSource = dt
        ChartStockLevels.Series("Stocks").XValueMember = "TransactionDate"
        ChartStockLevels.Series("Stocks").YValueMembers = "Quantity"
        ChartStockLevels.DataBind()

        ' --- Chart 2: Supplier Contributions ---
        If ChartSupplierContributions.Series.IndexOf("Contributions") = -1 Then
            ChartSupplierContributions.Series.Add("Contributions")
            ChartSupplierContributions.Series("Contributions").ChartType = SeriesChartType.Column
        End If
        ChartSupplierContributions.DataSource = dt
        ChartSupplierContributions.Series("Contributions").XValueMember = "SupplierName"
        ChartSupplierContributions.Series("Contributions").YValueMembers = "Quantity"
        ChartSupplierContributions.DataBind()

        ' --- Chart 3: Transaction Trends ---
        ' Add two series: IN vs OUT
        If ChartTransactionTrends.Series.IndexOf("InTransactions") = -1 Then
            ChartTransactionTrends.Series.Add("InTransactions")
            ChartTransactionTrends.Series("InTransactions").ChartType = SeriesChartType.Line
        End If
        If ChartTransactionTrends.Series.IndexOf("OutTransactions") = -1 Then
            ChartTransactionTrends.Series.Add("OutTransactions")
            ChartTransactionTrends.Series("OutTransactions").ChartType = SeriesChartType.Line
        End If

        ' Filter IN transactions
        Dim inRows = dt.Select("TransactionType = 'IN'")
        If inRows.Length > 0 Then
            ChartTransactionTrends.Series("InTransactions").Points.Clear()
            For Each row As DataRow In inRows
                ChartTransactionTrends.Series("InTransactions").Points.AddXY(row("TransactionDate"), row("Quantity"))
            Next
        End If

        ' Filter OUT transactions
        Dim outRows = dt.Select("TransactionType = 'OUT'")
        If outRows.Length > 0 Then
            ChartTransactionTrends.Series("OutTransactions").Points.Clear()
            For Each row As DataRow In outRows
                ChartTransactionTrends.Series("OutTransactions").Points.AddXY(row("TransactionDate"), row("Quantity"))
            Next
        End If
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

    Private Sub DGVStockTrackTransaction_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub DGVItemManagement_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVItemManagement.CellContentClick

    End Sub
End Class