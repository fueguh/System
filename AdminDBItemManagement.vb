Imports System.Data.SqlClient

Public Class AdminDBItemManagement
    Private selectedItemID As Integer

    Private Sub LoadInventory(Optional searchText As String = "")
        Dim query As String = "SELECT i.ItemID, i.ItemName, i.Price, c.CategoryName, 
                                  s.SupplierName, i.Quantity, i.ExpirationDate, i.HasExpiry
                           FROM ItemManagement i
                           INNER JOIN Categories c ON i.CategoryID = c.CategoryID
                           INNER JOIN Suppliers s ON i.SupplierID = s.SupplierID"

        Using connection As New SqlConnection(My.Settings.DentalDBConnection),
              adapter As New SqlDataAdapter(query, connection)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            DgvItems.DataSource = dt
        End Using
    End Sub

    Private Sub LoadSuppliers()
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim da As New SqlDataAdapter("SELECT SupplierID, SupplierName FROM Suppliers WHERE IsActive=1", con)
            Dim dt As New DataTable()
            da.Fill(dt)
            ComboBoxSupplier.DataSource = dt
            ComboBoxSupplier.DisplayMember = "SupplierName"
            ComboBoxSupplier.ValueMember = "SupplierID"
        End Using
    End Sub

    Private Sub LoadCategories()
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim da As New SqlDataAdapter("SELECT CategoryID, CategoryName FROM Categories WHERE IsActive=1", con)
            Dim dt As New DataTable()
            da.Fill(dt)
            ComboBoxCategory.DataSource = dt
            ComboBoxCategory.DisplayMember = "CategoryName"
            ComboBoxCategory.ValueMember = "CategoryID"
        End Using
    End Sub

    Private Sub ClearInputs()
        ' Clear textboxes
        TextBoxItemName.Clear()
        TextBoxPrice.Clear()

        ' Reset comboboxes
        ComboBoxCategory.SelectedIndex = -1
        ComboBoxSupplier.SelectedIndex = -1

        ' Reset numeric up-down
        NumericUpDownQuantity.Value = 0

        ' Reset DateTimePicker
        DateTimePickerExpiry.Value = DateTime.Now
        DateTimePickerExpiry.Enabled = False ' optional: disable until HasExpiry is checked

        ' Reset checkbox
        chkHasExpiry.Checked = False

        ' Reset selectedItemID
        selectedItemID = 0
    End Sub

    Private Sub BTNAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Dim qty As Integer = CInt(NumericUpDownQuantity.Value)
        Dim price As Decimal
        If Not Decimal.TryParse(TextBoxPrice.Text.Trim(), price) OrElse price < 0 Then Exit Sub

        Dim query As String = "INSERT INTO ItemManagement 
        (ItemName, Price, CategoryID, SupplierID, Quantity, ExpirationDate, HasExpiry) 
        VALUES (@ItemName, @Price, @CategoryID, @SupplierID, @Quantity, @ExpirationDate, @HasExpiry)"

        Using connection As New SqlConnection(My.Settings.DentalDBConnection),
              cmd As New SqlCommand(query, connection)

            cmd.Parameters.AddWithValue("@ItemName", TextBoxItemName.Text)
            cmd.Parameters.AddWithValue("@Price", price)
            cmd.Parameters.AddWithValue("@CategoryID", ComboBoxCategory.SelectedValue)
            cmd.Parameters.AddWithValue("@SupplierID", ComboBoxSupplier.SelectedValue)
            cmd.Parameters.AddWithValue("@Quantity", NumericUpDownQuantity.Value)
            cmd.Parameters.AddWithValue("@HasExpiry", chkHasExpiry.Checked)

            ' Only add ExpirationDate once, depending on checkbox
            If chkHasExpiry.Checked Then
                cmd.Parameters.AddWithValue("@ExpirationDate", DateTimePickerExpiry.Value)
            Else
                cmd.Parameters.AddWithValue("@ExpirationDate", DBNull.Value)
            End If

            connection.Open()
            cmd.ExecuteNonQuery()
            connection.Close()
        End Using

        MessageBox.Show("Item added successfully!")
        LoadSuppliers()
        LoadInventory()
        LoadCategories()
        ClearInputs()
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click, BtnUpdate.Click
        If DgvItems.CurrentRow Is Nothing Then Exit Sub
        Dim itemID As Integer = CInt(DgvItems.CurrentRow.Cells("ItemID").Value)
        Dim qty As Integer = CInt(NumericUpDownQuantity.Value)
        Dim price As Decimal
        If Not Decimal.TryParse(TextBoxPrice.Text.Trim(), price) OrElse price < 0 Then Exit Sub

        If selectedItemID = 0 Then
            MessageBox.Show("Please select an item to update.")
            Exit Sub
        End If

        Dim query As String = "UPDATE ItemManagement SET 
        ItemName=@ItemName, Price=@Price, CategoryID=@CategoryID, SupplierID=@SupplierID, 
        Quantity=@Quantity, ExpirationDate=@ExpirationDate, HasExpiry=@HasExpiry 
        WHERE ItemID=@ItemID"

        Using connection As New SqlConnection(My.Settings.DentalDBConnection),
              cmd As New SqlCommand(query, connection)

            cmd.Parameters.AddWithValue("@ItemID", selectedItemID)
            cmd.Parameters.AddWithValue("@ItemName", TextBoxItemName.Text)
            cmd.Parameters.AddWithValue("@Price", price)
            cmd.Parameters.AddWithValue("@CategoryID", ComboBoxCategory.SelectedValue)
            cmd.Parameters.AddWithValue("@SupplierID", ComboBoxSupplier.SelectedValue) ' ✅ ensure bound
            cmd.Parameters.AddWithValue("@Quantity", NumericUpDownQuantity.Value)
            cmd.Parameters.AddWithValue("@HasExpiry", chkHasExpiry.Checked)

            ' Only add ExpirationDate once
            If chkHasExpiry.Checked Then
                cmd.Parameters.AddWithValue("@ExpirationDate", DateTimePickerExpiry.Value)
            Else
                cmd.Parameters.AddWithValue("@ExpirationDate", DBNull.Value)
            End If

            connection.Open()
            cmd.ExecuteNonQuery()
            connection.Close()
        End Using

        MessageBox.Show("Item updated successfully!")
        LoadInventory()
        LoadSuppliers()
        LoadCategories()
        ClearInputs()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If selectedItemID = 0 Then
            MessageBox.Show("Please select an item to delete.")
            Exit Sub
        End If

        Dim query As String = "DELETE FROM ItemManagement WHERE ItemID=@ItemID"

        Using connection As New SqlConnection(My.Settings.DentalDBConnection),
          cmd As New SqlCommand(query, connection)

            cmd.Parameters.AddWithValue("@ItemID", selectedItemID)

            connection.Open()
            cmd.ExecuteNonQuery()
            connection.Close()

            If chkHasExpiry.Checked Then
                cmd.Parameters.AddWithValue("@ExpirationDate", DateTimePickerExpiry.Value)
            Else
                cmd.Parameters.AddWithValue("@ExpirationDate", DBNull.Value)
            End If
        End Using

        MessageBox.Show("Item deleted successfully!")
        LoadInventory()
        LoadSuppliers()
        LoadCategories()
        ClearInputs()
    End Sub

    Private Sub AdminDBItemManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePickerExpiry.Enabled = False
        DateTimePickerExpiry.Value = DateTimePickerExpiry.MinDate
        LoadInventory()
        'LoadSuppliers()
        'LoadCategories()
        ClearInputs()
        ' Load suppliers into ComboBox
        Dim query As String = "SELECT SupplierID, SupplierName FROM Suppliers"
        Using connection As New SqlConnection(My.Settings.DentalDBConnection),
          adapter As New SqlDataAdapter(query, connection)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ComboBoxSupplier.DataSource = dt
            ComboBoxSupplier.DisplayMember = "SupplierName"
            ComboBoxSupplier.ValueMember = "SupplierID"
        End Using

        ' Load categories into ComboBox
        Dim queryCat As String = "SELECT CategoryID, CategoryName FROM Categories"
        Using connection As New SqlConnection(My.Settings.DentalDBConnection),
          adapter As New SqlDataAdapter(queryCat, connection)
            Dim dtCat As New DataTable()
            adapter.Fill(dtCat)

            ComboBoxCategory.DataSource = dtCat
            ComboBoxCategory.DisplayMember = "CategoryName"
            ComboBoxCategory.ValueMember = "CategoryID"
        End Using
    End Sub

    Private Sub ComboBoxSupplier_TextChanged(sender As Object, e As EventArgs) Handles ComboBoxSupplier.TextChanged

    End Sub

    Private Sub ChkHasExpiry_CheckedChanged(sender As Object, e As EventArgs) Handles chkHasExpiry.CheckedChanged
        If chkHasExpiry.Checked Then
            DateTimePickerExpiry.Enabled = True
            DateTimePickerExpiry.Value = DateTime.Now ' safe default when enabled
        Else
            DateTimePickerExpiry.Enabled = False
            ' Reset to MinDate so you don’t hit the ArgumentOutOfRangeException
            DateTimePickerExpiry.Value = DateTimePickerExpiry.MinDate
        End If
    End Sub

    Private Sub DgvItems_SelectionChanged(sender As Object, e As EventArgs) Handles DgvItems.SelectionChanged
        If DgvItems.SelectedRows.Count > 0 Then
            Dim row As DataGridViewRow = DgvItems.SelectedRows(0)

            ' Store the ItemID for Update/Delete operations
            selectedItemID = Convert.ToInt32(row.Cells("ItemID").Value)

            ' Populate form fields with the selected row’s values
            TextBoxPrice.Text = row.Cells("ItemName").Value.ToString()
            TextBoxPrice.Text = row.Cells("Price").Value.ToString()
            ComboBoxCategory.Text = row.Cells("CategoryName").Value.ToString()
            ComboBoxSupplier.Text = row.Cells("SupplierName").Value.ToString()
            NumericUpDownQuantity.Value = Convert.ToInt32(row.Cells("Quantity").Value)

            ' Handle nullable ExpirationDate
            If IsDBNull(row.Cells("ExpirationDate").Value) Then
                DateTimePickerExpiry.Value = DateTime.Now
            Else
                DateTimePickerExpiry.Value = Convert.ToDateTime(row.Cells("ExpirationDate").Value)
            End If

            chkHasExpiry.Checked = Convert.ToBoolean(row.Cells("HasExpiry").Value)
        End If
    End Sub

    Private Sub DGVInventory_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvItems.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DgvItems.Rows(e.RowIndex)
            selectedItemID = Convert.ToInt32(row.Cells("ItemID").Value)

            ' Optional: populate form fields with selected row values
            TextBoxItemName.Text = row.Cells("ItemName").Value.ToString()
            TextBoxPrice.Text = row.Cells("Price").Value.ToString()
            ComboBoxCategory.Text = row.Cells("CategoryName").Value.ToString()
            ComboBoxCategory.Text = row.Cells("SupplierName").Value.ToString()
            NumericUpDownQuantity.Value = Convert.ToInt32(row.Cells("Quantity").Value)
            DateTimePickerExpiry.Value = If(IsDBNull(row.Cells("ExpirationDate").Value), DateTime.Now, Convert.ToDateTime(row.Cells("ExpirationDate").Value))
            chkHasExpiry.Checked = Convert.ToBoolean(row.Cells("HasExpiry").Value)
        End If
    End Sub

    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        LoadInventory(TextBoxSearch.Text.Trim())
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

    Private Sub TextBoxPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxPrice.KeyPress
        ' Allow control keys (Backspace, Delete, etc.)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        ' Allow digits
        If Char.IsDigit(e.KeyChar) Then
            Return
        End If

        ' Allow one decimal point
        If e.KeyChar = "."c AndAlso Not TextBoxPrice.Text.Contains(".") Then
            Return
        End If

        ' Block everything else
        e.Handled = True
    End Sub

    Private Sub ComboBoxSupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxSupplier.SelectedIndexChanged

    End Sub

    Private Sub DateTimePickerExpiry_CheckedChanged(sender As Object, e As EventArgs) Handles DateTimePickerExpiry.CheckedChanged

    End Sub
End Class