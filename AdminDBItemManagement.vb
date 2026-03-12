Imports System.Data.SqlClient

Public Class AdminDBItemManagement
    Private selectedItemID As Integer

    ' ===========================
    ' Form Load
    ' ===========================
    Private Sub AdminDBItemManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePickerExpiry.Enabled = False
        DateTimePickerExpiry.Value = DateTime.Now
        LoadInventory()
        LoadSuppliers()
        LoadCategories()
        ClearInputs()
    End Sub

    ' ===========================
    ' Load Inventory
    ' ===========================
    Private Sub LoadInventory(Optional searchText As String = "")
        Dim query As String = "SELECT i.ItemID, i.ItemName, i.Price, c.CategoryName, 
                                      s.SupplierName, i.Quantity, i.ExpirationDate, i.HasExpiry
                               FROM ItemManagement i
                               INNER JOIN Categories c ON i.CategoryID = c.CategoryID
                               INNER JOIN Suppliers s ON i.SupplierID = s.SupplierID"

        If Not String.IsNullOrEmpty(searchText) Then
            query &= " WHERE i.ItemName LIKE @Search OR c.CategoryName LIKE @Search OR s.SupplierName LIKE @Search"
        End If

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
              cmd As New SqlCommand(query, connection),
              adapter As New SqlDataAdapter(cmd)
            If Not String.IsNullOrEmpty(searchText) Then
                cmd.Parameters.AddWithValue("@Search", "%" & searchText & "%")
            End If

            Dim dt As New DataTable()
            adapter.Fill(dt)
            DgvItems.DataSource = dt
        End Using
    End Sub

    ' ===========================
    ' Load Suppliers
    ' ===========================
    Private Sub LoadSuppliers()
        Dim query As String = "SELECT SupplierID, SupplierName FROM Suppliers"
        Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
              adapter As New SqlDataAdapter(query, connection)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ComboBoxSupplier.DataSource = dt
            ComboBoxSupplier.DisplayMember = "SupplierName"
            ComboBoxSupplier.ValueMember = "SupplierID"
            ComboBoxSupplier.SelectedIndex = -1
        End Using
    End Sub

    ' ===========================
    ' Load Categories
    ' ===========================
    Private Sub LoadCategories()
        Dim query As String = "SELECT CategoryID, CategoryName FROM Categories"
        Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
              adapter As New SqlDataAdapter(query, connection)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ComboBoxCategory.DataSource = dt
            ComboBoxCategory.DisplayMember = "CategoryName"
            ComboBoxCategory.ValueMember = "CategoryID"
            ComboBoxCategory.SelectedIndex = -1
        End Using
    End Sub

    ' ===========================
    ' Clear Inputs
    ' ===========================
    Private Sub ClearInputs()
        TextBoxItemName.Clear()
        TextBoxPrice.Clear()
        ComboBoxCategory.SelectedIndex = -1
        ComboBoxSupplier.SelectedIndex = -1
        DateTimePickerExpiry.Value = DateTime.Now
        DateTimePickerExpiry.Enabled = False
        chkHasExpiry.Checked = False
        selectedItemID = 0
        DgvItems.ClearSelection()

        ' BUTTON LOGIC: Allow adding new items, but hide edit options
        BtnAdd.Enabled = True
        BtnUpdate.Enabled = False
        BtnDelete.Enabled = False
    End Sub

    ' ===========================
    ' Add Item
    ' ===========================
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Dim price As Decimal
        If Not Decimal.TryParse(TextBoxPrice.Text.Trim(), price) OrElse price < 0 Then
            MessageBox.Show("Please enter a valid price.")
            Exit Sub
        End If

        Dim query As String = "INSERT INTO ItemManagement 
                               (ItemName, Price, CategoryID, SupplierID, Quantity, ExpirationDate, HasExpiry) 
                               VALUES (@ItemName, @Price, @CategoryID, @SupplierID, @Quantity, @ExpirationDate, @HasExpiry);
                               SELECT SCOPE_IDENTITY();"

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
              cmd As New SqlCommand(query, connection)

            cmd.Parameters.AddWithValue("@ItemName", TextBoxItemName.Text.Trim())
            cmd.Parameters.AddWithValue("@Price", price)
            cmd.Parameters.AddWithValue("@CategoryID", ComboBoxCategory.SelectedValue)
            cmd.Parameters.AddWithValue("@SupplierID", ComboBoxSupplier.SelectedValue)
            cmd.Parameters.AddWithValue("@Quantity", 0)
            cmd.Parameters.AddWithValue("@HasExpiry", chkHasExpiry.Checked)
            cmd.Parameters.AddWithValue("@ExpirationDate", If(chkHasExpiry.Checked, CType(DateTimePickerExpiry.Value, Object), DBNull.Value))

            connection.Open()
            Dim newID As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            connection.Close()

            ' Audit log
            SystemSession.LogAudit($"Added Item ID: {newID} - {TextBoxItemName.Text}", "Item Management", SystemSession.LoggedInUserID, SystemSession.LoggedInFullName, SystemSession.LoggedInRole)
        End Using

        MessageBox.Show("Item added successfully!")
        LoadInventory()
        ClearInputs()
    End Sub

    ' ===========================
    ' Update Item
    ' ===========================
    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        If selectedItemID = 0 Then
            MessageBox.Show("Please select an item to update.")
            Exit Sub
        End If

        Dim price As Decimal
        If Not Decimal.TryParse(TextBoxPrice.Text.Trim(), price) OrElse price < 0 Then
            MessageBox.Show("Please enter a valid price.")
            Exit Sub
        End If

        Dim query As String = "UPDATE ItemManagement SET 
                               ItemName=@ItemName, Price=@Price, CategoryID=@CategoryID, SupplierID=@SupplierID, 
                               ExpirationDate=@ExpirationDate, HasExpiry=@HasExpiry
                               WHERE ItemID=@ItemID"

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
              cmd As New SqlCommand(query, connection)

            cmd.Parameters.AddWithValue("@ItemID", selectedItemID)
            cmd.Parameters.AddWithValue("@ItemName", TextBoxItemName.Text.Trim())
            cmd.Parameters.AddWithValue("@Price", price)
            cmd.Parameters.AddWithValue("@CategoryID", ComboBoxCategory.SelectedValue)
            cmd.Parameters.AddWithValue("@SupplierID", ComboBoxSupplier.SelectedValue)
            cmd.Parameters.AddWithValue("@HasExpiry", chkHasExpiry.Checked)
            cmd.Parameters.AddWithValue("@ExpirationDate", If(chkHasExpiry.Checked, CType(DateTimePickerExpiry.Value, Object), DBNull.Value))

            connection.Open()
            cmd.ExecuteNonQuery()
            connection.Close()

            ' Audit log
            SystemSession.LogAudit($"Updated Item: {TextBoxItemName.Text}", "Item Management", SystemSession.LoggedInUserID, SystemSession.LoggedInFullName, SystemSession.LoggedInRole)
        End Using

        MessageBox.Show("Item updated successfully!")
        LoadInventory()
        ClearInputs()
    End Sub

    ' ===========================
    ' Delete Item
    ' ===========================
    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If selectedItemID = 0 Then
            MessageBox.Show("Please select an item to delete.")
            Exit Sub
        End If

        Dim query As String = "DELETE FROM ItemManagement WHERE ItemID=@ItemID"

        Try
            Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
                  cmd As New SqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@ItemID", selectedItemID)
                connection.Open()
                cmd.ExecuteNonQuery()
                connection.Close()

                ' Audit log
                SystemSession.LogAudit($"Deleted Item: {selectedItemID}", "Item Management", SystemSession.LoggedInUserID, SystemSession.LoggedInFullName, SystemSession.LoggedInRole)
            End Using

            MessageBox.Show("Item deleted successfully!")
        Catch ex As SqlException
            MessageBox.Show("Cannot delete this item because it is used in stock transactions.")
        End Try

        LoadInventory()
        ClearInputs()
    End Sub

    ' ===========================
    ' Search Item
    ' ===========================
    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        LoadInventory(TextBoxSearch.Text.Trim())
    End Sub

    ' ===========================
    ' Back Button
    ' ===========================
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

    ' ===========================
    ' Clear Button
    ' ===========================
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearInputs()
        TextBoxSearch.Clear()
    End Sub

    ' ===========================
    ' Handle Expiry Checkbox
    ' ===========================
    Private Sub chkHasExpiry_CheckedChanged(sender As Object, e As EventArgs) Handles chkHasExpiry.CheckedChanged
        DateTimePickerExpiry.Enabled = chkHasExpiry.Checked
        If chkHasExpiry.Checked Then DateTimePickerExpiry.Value = DateTime.Now
    End Sub

    ' ===========================
    ' Handle Selection
    ' ===========================
    Private Sub DgvItems_SelectionChanged(sender As Object, e As EventArgs) Handles DgvItems.SelectionChanged
        ' If no row is selected, lock the buttons and exit
        If DgvItems.SelectedRows.Count = 0 Then
            BtnUpdate.Enabled = False
            BtnDelete.Enabled = False
            BtnAdd.Enabled = True
            Return
        End If

        ' A row IS selected: Lock "Add" and unlock "Update/Delete"
        BtnAdd.Enabled = False
        BtnUpdate.Enabled = True
        BtnDelete.Enabled = True

        Dim row As DataGridViewRow = DgvItems.SelectedRows(0)
        selectedItemID = Convert.ToInt32(row.Cells("ItemID").Value)

        TextBoxItemName.Text = row.Cells("ItemName").Value.ToString()
        TextBoxPrice.Text = row.Cells("Price").Value.ToString()
        ComboBoxCategory.Text = row.Cells("CategoryName").Value.ToString()
        ComboBoxSupplier.Text = row.Cells("SupplierName").Value.ToString()

        chkHasExpiry.Checked = Convert.ToBoolean(row.Cells("HasExpiry").Value)
        DateTimePickerExpiry.Value = If(IsDBNull(row.Cells("ExpirationDate").Value), DateTime.Now, Convert.ToDateTime(row.Cells("ExpirationDate").Value))
    End Sub

    ' ===========================
    ' Price Input Validation
    ' ===========================
    Private Sub TextBoxPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxPrice.KeyPress
        If Char.IsControl(e.KeyChar) Then Return
        If Char.IsDigit(e.KeyChar) Then Return
        If e.KeyChar = "."c AndAlso Not TextBoxPrice.Text.Contains(".") Then Return
        e.Handled = True
    End Sub
End Class