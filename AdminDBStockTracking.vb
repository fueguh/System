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
    End Sub

    ' =========================
    ' LOAD TRANSACTIONS
    ' =========================
    Private Sub LoadTransactions()
        Dim query As String = "SELECT t.TransactionID, 
                                  i.ItemName, 
                                  t.TransactionType, 
                                  t.Quantity, 
                                  t.TransactionDate,
                                  t.ItemID
                           FROM StockTransactions t
                           INNER JOIN ItemManagement i ON t.ItemID = i.ItemID
                           ORDER BY t.TransactionDate DESC"

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
              adapter As New SqlDataAdapter(query, connection)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            DGVTransactions.DataSource = dt
        End Using

        ' Hide IDs
        If DGVTransactions.Columns.Contains("TransactionID") Then DGVTransactions.Columns("TransactionID").Visible = False
        If DGVTransactions.Columns.Contains("ItemID") Then DGVTransactions.Columns("ItemID").Visible = False

        ' Format the TransactionDate column
        If DGVTransactions.Columns.Contains("TransactionDate") Then
            DGVTransactions.Columns("TransactionDate").DefaultCellStyle.Format = "dd/MM/yyyy"
        End If
    End Sub

    Private Sub DGVTransactions_SelectionChanged(sender As Object, e As EventArgs) Handles DGVTransactions.SelectionChanged
        If DGVTransactions.SelectedRows.Count > 0 Then
            Dim row As DataGridViewRow = DGVTransactions.SelectedRows(0)
            ' Set ComboBox item
            If DGVTransactions.Columns.Contains("ItemID") Then
                Dim itemID As Integer = Convert.ToInt32(row.Cells("ItemID").Value)
                ComboBoxItem.SelectedValue = itemID
            End If
            ' Set TransactionDate
            If DGVTransactions.Columns.Contains("TransactionDate") Then
                TransactionDate.Value = CDate(row.Cells("TransactionDate").Value)
            End If
        End If
    End Sub

    ' =========================
    ' RECORD NEW TRANSACTION
    ' =========================
    Private Sub ButtonRecord_Click(sender As Object, e As EventArgs) Handles ButtonRecord.Click
        ' Validation
        If ComboBoxItem.SelectedValue Is Nothing OrElse Not TypeOf ComboBoxItem.SelectedValue Is Integer Then
            MessageBox.Show("Please select a valid item.")
            Exit Sub
        End If

        If NumericUpDownQuantity.Value <= 0 Then
            MessageBox.Show("Quantity must be greater than zero.")
            Exit Sub
        End If

        Dim itemID As Integer = CInt(ComboBoxItem.SelectedValue)
        Dim itemName As String = ComboBoxItem.Text ' Capture for logging
        Dim qty As Integer = CInt(NumericUpDownQuantity.Value)
        Dim transType As String = If(RadioIn.Checked, "IN", If(RadioOut.Checked, "OUT", ""))
        Dim transDate As Date = TransactionDate.Value.Date

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2)
            connection.Open()

            ' Check stock if OUT
            If transType = "OUT" Then
                Using cmdCheck As New SqlCommand("SELECT Quantity FROM ItemManagement WHERE ItemID=@ItemID", connection)
                    cmdCheck.Parameters.AddWithValue("@ItemID", itemID)
                    Dim currentQty As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())
                    If qty > currentQty Then
                        MessageBox.Show($"Cannot deduct {qty} items. Only {currentQty} in stock.")
                        Exit Sub
                    End If
                End Using
            End If

            Using sqlTrans As SqlTransaction = connection.BeginTransaction()
                Try
                    ' 1. Insert transaction
                    Using cmdTrans As New SqlCommand(
                    "INSERT INTO StockTransactions (ItemID, TransactionType, Quantity, TransactionDate) " &
                    "VALUES (@ItemID, @TransType, @Qty, @Date)", connection, sqlTrans)
                        cmdTrans.Parameters.AddWithValue("@ItemID", itemID)
                        cmdTrans.Parameters.AddWithValue("@TransType", transType)
                        cmdTrans.Parameters.AddWithValue("@Qty", qty)
                        cmdTrans.Parameters.AddWithValue("@Date", transDate)
                        cmdTrans.ExecuteNonQuery()
                    End Using

                    ' 2. Update stock
                    Dim queryUpdate As String = If(transType = "IN",
                                               "UPDATE ItemManagement SET Quantity = Quantity + @Qty WHERE ItemID = @ItemID",
                                               "UPDATE ItemManagement SET Quantity = Quantity - @Qty WHERE ItemID = @ItemID")
                    Using cmdUpdate As New SqlCommand(queryUpdate, connection, sqlTrans)
                        cmdUpdate.Parameters.AddWithValue("@ItemID", itemID)
                        cmdUpdate.Parameters.AddWithValue("@Qty", qty)
                        cmdUpdate.ExecuteNonQuery()
                    End Using

                    ' 3. AUDIT LOG: Record the stock movement
                    Dim auditMsg As String = $"Stock {transType}: {qty} units of {itemName} on {transDate.ToShortDateString()}"
                    SystemSession.LogAudit(auditMsg, "Stock Tracking", SystemSession.LoggedInUserID, SystemSession.LoggedInFullName, SystemSession.LoggedInRole)

                    sqlTrans.Commit()
                    MessageBox.Show("Stock updated successfully!")
                Catch ex As Exception
                    sqlTrans.Rollback()
                    MessageBox.Show("Transaction failed: " & ex.Message)
                End Try
            End Using
        End Using

        LoadTransactions()
        LoadInventory()
        ClearInputs()
    End Sub

    ' =========================
    ' DELETE TRANSACTION
    ' =========================
    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click
        If DGVTransactions.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a transaction to delete.")
            Exit Sub
        End If

        Dim row As DataGridViewRow = DGVTransactions.SelectedRows(0)
        Dim transID As Integer = Convert.ToInt32(row.Cells("TransactionID").Value)
        Dim transType As String = row.Cells("TransactionType").Value.ToString()
        Dim qty As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
        Dim itemID As Integer = Convert.ToInt32(row.Cells("ItemID").Value)
        Dim itemName As String = row.Cells("ItemName").Value.ToString()

        Dim queryUpdate As String = If(transType = "IN",
                                   "UPDATE ItemManagement SET Quantity = Quantity - @Quantity WHERE ItemID=@ItemID",
                                   "UPDATE ItemManagement SET Quantity = Quantity + @Quantity WHERE ItemID=@ItemID")

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2)
            connection.Open()
            Using sqlTrans As SqlTransaction = connection.BeginTransaction()
                Try
                    ' 1. Update stock (Reverse the transaction)
                    Using cmdUpdate As New SqlCommand(queryUpdate, connection, sqlTrans)
                        cmdUpdate.Parameters.AddWithValue("@ItemID", itemID)
                        cmdUpdate.Parameters.AddWithValue("@Quantity", qty)
                        cmdUpdate.ExecuteNonQuery()
                    End Using

                    ' 2. Delete transaction
                    Using cmdDelete As New SqlCommand("DELETE FROM StockTransactions WHERE TransactionID=@TransactionID", connection, sqlTrans)
                        cmdDelete.Parameters.AddWithValue("@TransactionID", transID)
                        cmdDelete.ExecuteNonQuery()
                    End Using

                    ' 3. AUDIT LOG: Record that a transaction was deleted/voided
                    Dim auditMsg As String = $"Deleted/Voided {transType} Transaction (ID: {transID}) for {qty} units of {itemName}"
                    SystemSession.LogAudit(auditMsg, "Stock Tracking", SystemSession.LoggedInUserID, SystemSession.LoggedInFullName, SystemSession.LoggedInRole)

                    sqlTrans.Commit()
                    MessageBox.Show("Transaction deleted and stock adjusted successfully!")
                Catch ex As Exception
                    sqlTrans.Rollback()
                    MessageBox.Show("Failed to delete transaction: " & ex.Message)
                End Try
            End Using
        End Using

        LoadTransactions()
        LoadInventory()
        ClearInputs()
    End Sub

    ' =========================
    ' FORM LOAD
    ' =========================
    Private Sub AdminDBStockTracking_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTransactions()
        LoadInventory()
        LoadItem()
        ClearInputs()
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

End Class