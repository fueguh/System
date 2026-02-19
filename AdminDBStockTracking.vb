Imports System.ComponentModel.Design
Imports System.Data.SqlClient

Public Class AdminDBStockTracking
    Private Sub LoadInventory()
        Dim query As String = "SELECT ItemID, ItemName, Quantity, Price 
                           FROM ItemManagement"

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
          adapter As New SqlDataAdapter(query, connection)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            DGVTransactions.DataSource = dt
        End Using
    End Sub
    Private Sub LoadItem()
        Dim query As String = "SELECT ItemID, ItemName FROM ItemManagement"

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
          adapter As New SqlDataAdapter(query, connection)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ComboBoxItem.DataSource = dt
            ComboBoxItem.DisplayMember = "ItemName"   ' what the user sees
            ComboBoxItem.ValueMember = "ItemID"       ' what the code uses internally
        End Using
    End Sub

    Private Sub ClearInputs()
        ComboBoxItem.SelectedIndex = -1
        RadioIn.Checked = False
        RadioOut.Checked = False
        NumericUpDownQuantity.Value = 0
        TransactionDate.Value = DateTime.Now.Date
    End Sub

    Private Sub LoadTransactions()
        Dim query As String = "SELECT t.TransactionID, 
                                  i.ItemName, 
                                  t.TransactionType, 
                                  t.Quantity, 
                                  t.TransactionDate,
                                  t.ItemID
                           FROM StockTransactions t
                           INNER JOIN ItemManagement i ON t.ItemID = i.ItemID"

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
          adapter As New SqlDataAdapter(query, connection)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            DGVTransactions.DataSource = dt
        End Using

        ' Optional: hide internal IDs so users only see friendly columns
        If DGVTransactions.Columns.Contains("TransactionID") Then
            DGVTransactions.Columns("TransactionID").Visible = False
        End If
        If DGVTransactions.Columns.Contains("ItemID") Then
            DGVTransactions.Columns("ItemID").Visible = False
        End If
    End Sub

    Private Sub TransactionDate_ValueChanged(sender As Object, e As EventArgs) Handles TransactionDate.ValueChanged

    End Sub

    Private Sub ButtonRecord_Click(sender As Object, e As EventArgs) Handles ButtonRecord.Click

        If ComboBoxItem.SelectedValue Is Nothing Then
            MessageBox.Show("Please select an item.")
            Exit Sub
        End If

        Dim itemID As Integer = CInt(ComboBoxItem.SelectedValue)
        Dim qty As Integer = CInt(NumericUpDownQuantity.Value)
        Dim transType As String

        If RadioIn.Checked Then
            transType = "IN"
        ElseIf RadioOut.Checked Then
            transType = "OUT"
        Else
            MessageBox.Show("Please select a transaction type.")
            Exit Sub
        End If

        Dim transDate As Date = TransactionDate.Value.Date

        ' Insert into StockTransactions
        Dim queryTrans As String = "INSERT INTO StockTransactions 
        (ItemID, TransactionType, Quantity, TransactionDate) 
        VALUES (@ItemID, @TransactionType, @Quantity, @TransactionDate)"

        ' Update ItemManagement stock
        Dim queryUpdate As String
        If transType = "IN" Then
            queryUpdate = "UPDATE ItemManagement SET Quantity = Quantity + @Quantity WHERE ItemID=@ItemID"
        Else
            queryUpdate = "UPDATE ItemManagement SET Quantity = Quantity - @Quantity WHERE ItemID=@ItemID"
        End If

        If qty <= 0 Then
            MessageBox.Show("Quantity must be greater than zero.")
            Exit Sub
        End If
        Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
          cmdTrans As New SqlCommand(queryTrans, connection),
          cmdUpdate As New SqlCommand(queryUpdate, connection)

            cmdTrans.Parameters.AddWithValue("@ItemID", itemID)
            cmdTrans.Parameters.AddWithValue("@TransactionType", transType)
            cmdTrans.Parameters.AddWithValue("@Quantity", qty)
            cmdTrans.Parameters.AddWithValue("@TransactionDate", transDate)

            cmdUpdate.Parameters.AddWithValue("@ItemID", itemID)
            cmdUpdate.Parameters.AddWithValue("@Quantity", qty)

            connection.Open()
            cmdTrans.ExecuteNonQuery()
            cmdUpdate.ExecuteNonQuery()
            connection.Close()

        End Using

        MessageBox.Show("Transaction recorded successfully!")
        LoadTransactions()
        LoadInventory()
        ClearInputs()
    End Sub

    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click
        If DGVTransactions.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a transaction to delete.")
            Exit Sub
        End If

        ' Get selected transaction details from DataGridView
        Dim row As DataGridViewRow = DGVTransactions.SelectedRows(0)
        Dim transID As Integer = CInt(row.Cells("TransactionID").Value)
        Dim itemID As Integer = CInt(row.Cells("ItemID").Value)
        Dim qty As Integer = CInt(row.Cells("Quantity").Value)
        Dim transType As String = row.Cells("TransactionType").Value.ToString()

        ' Reverse stock adjustment
        Dim queryUpdate As String
        If transType = "IN" Then
            ' If it was IN, subtract the quantity back out
            queryUpdate = "UPDATE ItemManagement SET Quantity = Quantity - @Quantity WHERE ItemID=@ItemID"
        ElseIf transType = "OUT" Then
            ' If it was OUT, add the quantity back in
            queryUpdate = "UPDATE ItemManagement SET Quantity = Quantity + @Quantity WHERE ItemID=@ItemID"
        Else
            MessageBox.Show("Invalid transaction type.")
            Exit Sub
        End If

        ' Delete transaction record
        Dim queryDelete As String = "DELETE FROM StockTransactions WHERE TransactionID=@TransactionID"

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
              cmdUpdate As New SqlCommand(queryUpdate, connection),
              cmdDelete As New SqlCommand(queryDelete, connection)

            cmdUpdate.Parameters.AddWithValue("@ItemID", itemID)
            cmdUpdate.Parameters.AddWithValue("@Quantity", qty)

            cmdDelete.Parameters.AddWithValue("@TransactionID", transID)

            connection.Open()
            cmdUpdate.ExecuteNonQuery()
            cmdDelete.ExecuteNonQuery()
            connection.Close()
        End Using

        MessageBox.Show("Transaction deleted successfully!")
        LoadTransactions()
        LoadInventory() ' refresh stock levels
        ClearInputs()
    End Sub

    Private Sub AdminDBStockTracking_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTransactions()
        LoadInventory()
        LoadItem()
        ComboBoxItem.SelectedIndex = -1
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub
End Class