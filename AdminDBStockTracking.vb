Imports System.ComponentModel.Design
Imports System.Data.SqlClient

Public Class AdminDBStockTracking
    Private Sub LoadTransactions()
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()
            Dim da As New SqlDataAdapter("
            SELECT t.TransactionID, i.ItemName, t.TransactionType, t.Supplier, t.Quantity, t.TransactionDate
            FROM InventoryTransactions t
            INNER JOIN Inventory i ON t.ItemID = i.ItemID
            ORDER BY t.TransactionDate DESC", con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVTransactions.DataSource = dt
        End Using
    End Sub

    Private Sub LoadItems()
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()
            Dim da As New SqlDataAdapter("SELECT ItemID, ItemName FROM Inventory", con)
            Dim dt As New DataTable()
            da.Fill(dt)

            ComboBoxItem.DataSource = dt
            ComboBoxItem.DisplayMember = "ItemName"   ' what user sees
            ComboBoxItem.ValueMember = "ItemID"       ' actual ID used in DB
        End Using
    End Sub

    Private Sub TransactionDate_ValueChanged(sender As Object, e As EventArgs) Handles TransactionDate.ValueChanged

    End Sub

    Private Sub ButtonRecord_Click(sender As Object, e As EventArgs) Handles ButtonRecord.Click
        ' Get quantity directly from NumericUpDown
        Dim qty As Integer = CInt(NumericUpDownQuantity.Value)
        If qty <= 0 Then
            MessageBox.Show("Please enter a valid numeric quantity greater than 0.")
            Exit Sub
        End If

        Dim type As String
        If RadioIn.Checked Then
            type = "IN"
        ElseIf RadioOut.Checked Then
            type = "OUT"
        Else
            MessageBox.Show("Please select a transaction type.")
            Exit Sub
        End If

        Dim itemID As Integer = CInt(ComboBoxItem.SelectedValue)
        Dim supplierName As String = TextBoxSupplier.Text.Trim()

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            ' Insert transaction
            Dim cmd As New SqlCommand("
            INSERT INTO InventoryTransactions (ItemID, TransactionType, Quantity, Supplier, TransactionDate)
            VALUES (@ItemID, @Type, @Qty, @Supplier, GETDATE())", con)
            cmd.Parameters.AddWithValue("@ItemID", itemID)
            cmd.Parameters.AddWithValue("@Type", type)
            cmd.Parameters.AddWithValue("@Qty", qty)
            cmd.Parameters.AddWithValue("@Supplier", supplierName)
            cmd.ExecuteNonQuery()

            ' Update Inventory quantity and supplier
            Dim updateQuery As String
            If type = "IN" Then
                updateQuery = "UPDATE Inventory SET Quantity = Quantity + @Qty, Supplier=@Supplier WHERE ItemID=@ItemID"
            Else
                updateQuery = "UPDATE Inventory SET Quantity = Quantity - @Qty, Supplier=@Supplier WHERE ItemID=@ItemID"
            End If

            Dim updateCmd As New SqlCommand(updateQuery, con)
            updateCmd.Parameters.AddWithValue("@Qty", qty)
            updateCmd.Parameters.AddWithValue("@ItemID", itemID)
            updateCmd.Parameters.AddWithValue("@Supplier", supplierName)
            updateCmd.ExecuteNonQuery()
        End Using

        LoadTransactions()
        LoadItems()
        MessageBox.Show("Transaction recorded successfully!")

        TextBoxSupplier.Clear()
        NumericUpDownQuantity.Value = 0
        ComboBoxItem.SelectedIndex = 0
    End Sub

    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click
        If DGVTransactions.CurrentRow Is Nothing Then Exit Sub
        Dim transID As Integer = CInt(DGVTransactions.CurrentRow.Cells("TransactionID").Value)

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()
            Dim cmd As New SqlCommand("DELETE FROM InventoryTransactions WHERE TransactionID=@TransID", con)
            cmd.Parameters.AddWithValue("@TransID", transID)
            cmd.ExecuteNonQuery()
        End Using

        LoadTransactions()
        LoadItems()
        MessageBox.Show("Transaction deleted successfully!")
    End Sub

    Private Sub AdminDBStockTracking_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTransactions()
        LoadItems()
        ComboBoxItem.SelectedIndex = -1
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub
End Class