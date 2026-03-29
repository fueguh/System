Imports System.ComponentModel.Design
Imports System.Data.SqlClient

Public Class AdminDBStockTracking

    ' =========================
    ' LOAD INVENTORY AND ITEMS
    ' =========================
    Private Sub LoadInventory()
        Dim query As String = "SELECT ItemID, ItemName, Quantity, Price FROM ItemManagement"

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
              adapter As New SqlDataAdapter(query, connection)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            ' Optionally bind to a grid if needed
            ' DGVInventory.DataSource = dt
        End Using
    End Sub

    Private Sub LoadItem()
        Dim query As String = "SELECT ItemID, ItemName FROM ItemManagement"

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
              adapter As New SqlDataAdapter(query, connection)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ComboBoxItem.DataSource = dt
            ComboBoxItem.DisplayMember = "ItemName"
            ComboBoxItem.ValueMember = "ItemID"
        End Using
    End Sub

    Private Sub ClearInputs()
        ComboBoxItem.SelectedIndex = -1
        RadioIn.Checked = False
        RadioOut.Checked = False
        NumericUpDownQuantity.Value = 0
        TransactionDate.Value = DateTime.Now.Date
        DGVTransactions.ClearSelection()
    End Sub

    ' =========================
    ' LOAD TRANSACTIONS
    ' =========================
    Private Sub LoadSummarizedReport()
        Dim query As String = "
        SELECT 
            i.ItemID, 
            i.ItemName, 
            ISNULL(SUM(CASE WHEN t.TransactionType = 'IN' THEN t.Quantity ELSE 0 END), 0) AS [Total Stock In],
            ISNULL(SUM(CASE WHEN t.TransactionType = 'OUT' THEN t.Quantity ELSE 0 END), 0) AS [Total Stock Out],
            -- Mathematically calculate the balance right here:
            ISNULL(SUM(CASE WHEN t.TransactionType = 'IN' THEN t.Quantity ELSE 0 END), 0) - 
            ISNULL(SUM(CASE WHEN t.TransactionType = 'OUT' THEN t.Quantity ELSE 0 END), 0) AS [Current Balance]
        FROM ItemManagement i
        LEFT JOIN StockTransactions t ON i.ItemID = t.ItemID
        GROUP BY i.ItemID, i.ItemName
        ORDER BY i.ItemName ASC"

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
          adapter As New SqlDataAdapter(query, connection)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            DGVTransactions.DataSource = dt
        End Using

        If DGVTransactions.Columns.Contains("ItemID") Then DGVTransactions.Columns("ItemID").Visible = False
    End Sub

    Private Sub DGVTransactions_SelectionChanged(sender As Object, e As EventArgs) Handles DGVTransactions.SelectionChanged
        If DGVTransactions.SelectedRows.Count > 0 Then
            Dim row As DataGridViewRow = DGVTransactions.SelectedRows(0)

            ' We can still auto-select the item for the user to add more stock
            If DGVTransactions.Columns.Contains("ItemID") Then
                Dim itemID As Integer = Convert.ToInt32(row.Cells("ItemID").Value)
                ComboBoxItem.SelectedValue = itemID
            End If

            ' Note: TransactionDate is removed here because a summary row 
            ' represents many dates at once.
        End If
    End Sub

    ' =========================
    ' RECORD NEW TRANSACTION
    ' =========================
    Private Sub ButtonRecord_Click(sender As Object, e As EventArgs) Handles ButtonRecord.Click
        ' --- VALIDATION ---
        If ComboBoxItem.SelectedValue Is Nothing Then
            MessageBox.Show("Please select an item.")
            Exit Sub
        End If

        If Not RadioIn.Checked AndAlso Not RadioOut.Checked Then
            MessageBox.Show("Please select if the stock is coming IN or going OUT.")
            Exit Sub
        End If

        If NumericUpDownQuantity.Value <= 0 Then
            MessageBox.Show("Quantity must be greater than zero.")
            Exit Sub
        End If

        ' --- VARIABLES ---
        Dim itemID As Integer = CInt(ComboBoxItem.SelectedValue)
        Dim itemName As String = ComboBoxItem.Text
        Dim qty As Integer = CInt(NumericUpDownQuantity.Value)
        Dim transType As String = If(RadioIn.Checked, "IN", "OUT")
        Dim transDate As Date = TransactionDate.Value.Date

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2)
            connection.Open()

            ' --- STOCK CHECK ---
            If transType = "OUT" Then
                ' Check balance from the calculated history
                Dim balQuery As String = "SELECT ISNULL(SUM(CASE WHEN TransactionType='IN' THEN Quantity ELSE -Quantity END), 0) FROM StockTransactions WHERE ItemID=@ID"
                Using cmdCheck As New SqlCommand(balQuery, connection)
                    cmdCheck.Parameters.AddWithValue("@ID", itemID)
                    Dim currentBalance As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())
                    If qty > currentBalance Then
                        MessageBox.Show($"Cannot deduct {qty}. Current balance is only {currentBalance}.")
                        Exit Sub
                    End If
                End Using
            End If

            ' --- DATABASE TRANSACTION ---
            Using sqlTrans As SqlTransaction = connection.BeginTransaction()
                Try
                    ' 1. ONLY Insert transaction record
                    ' THE SQL TRIGGER HANDLES THE MATH AUTOMATICALLY NOW
                    Dim insertSql As String = "INSERT INTO StockTransactions (ItemID, TransactionType, Quantity, TransactionDate) VALUES (@ItemID, @TransType, @Qty, @Date)"
                    Using cmdTrans As New SqlCommand(insertSql, connection, sqlTrans)
                        cmdTrans.Parameters.AddWithValue("@ItemID", itemID)
                        cmdTrans.Parameters.AddWithValue("@TransType", transType)
                        cmdTrans.Parameters.AddWithValue("@Qty", qty)
                        cmdTrans.Parameters.AddWithValue("@Date", transDate)
                        cmdTrans.ExecuteNonQuery()
                    End Using

                    ' 2. Audit Log
                    Dim auditMsg As String = $"Stock {transType}: {qty} {itemName}"
                    SystemSession.LogAudit(auditMsg, "Stock Tracking", SystemSession.LoggedInUserID, SystemSession.LoggedInFullName, SystemSession.LoggedInRole)

                    sqlTrans.Commit()
                    MessageBox.Show("Stock updated successfully!")

                Catch ex As Exception
                    sqlTrans.Rollback()
                    MessageBox.Show("Transaction failed: " & ex.Message)
                End Try
            End Using
        End Using

        LoadSummarizedReport()
        ClearInputs()
    End Sub

    ' =========================
    ' FORM LOAD
    ' =========================
    Private Sub AdminDBStockTracking_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSummarizedReport()
        LoadInventory()
        LoadItem()
        ClearInputs()
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

End Class