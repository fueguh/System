Imports System.Data.SqlClient

Public Class AdminDBItemManagement

    Dim connString As String = My.Settings.DentalDBConnection
    Dim conn As New SqlConnection(connString)

    Private Sub LoadInventory()
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim da As New SqlDataAdapter("SELECT * FROM Inventory", con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVInventory.DataSource = dt
        End Using
    End Sub

    Private Sub LoadSuppliers()
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim da As New SqlDataAdapter("SELECT DISTINCT Supplier FROM Inventory WHERE Supplier IS NOT NULL ORDER BY Supplier", con)
            Dim dt As New DataTable()
            da.Fill(dt)

            ComboBoxSupplier.DataSource = dt
            ComboBoxSupplier.DisplayMember = "Supplier"
            ComboBoxSupplier.ValueMember = "Supplier"
        End Using
    End Sub

    Private Sub ClearInputs()
        TextBoxItemName.Clear()
        ComboBoxCategory.SelectedIndex = -1
        NumericUpDownQuantity.Value = 0
        ComboBoxUnit.SelectedIndex = -1
        TextBoxPrice.Clear()
        ComboBoxSupplier.SelectedIndex = -1

        ' Reset expiry controls
        chkHasExpiry.Checked = False
        DateTimePickerExpiry.Value = DateTime.Now
        DateTimePickerExpiry.Enabled = False
    End Sub

    Private Sub BTNAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Dim qty As Integer
        If Not Integer.TryParse(NumericUpDownQuantity.Value, qty) Then
            MessageBox.Show("Enter a valid quantity.")
            Exit Sub
        End If

        Dim price As Decimal
        If Not Decimal.TryParse(TextBoxPrice.Text, price) Then
            MessageBox.Show("Enter a valid price.")
            Exit Sub
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim query As String = "
            INSERT INTO Inventory (ItemName, Category, Quantity, Unit, Price, Supplier, ExpiryDate)
            VALUES (@ItemName, @Category, @Quantity, @Unit, @Price, @Supplier, @ExpiryDate)"
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@ItemName", TextBoxItemName.Text)
                cmd.Parameters.AddWithValue("@Category", ComboBoxCategory.Text)
                cmd.Parameters.AddWithValue("@Quantity", qty)
                cmd.Parameters.AddWithValue("@Unit", ComboBoxUnit.Text)
                cmd.Parameters.AddWithValue("@Price", price)
                cmd.Parameters.AddWithValue("@Supplier", ComboBoxSupplier.Text)

                ' ✅ Only save expiry if checkbox is checked
                If chkHasExpiry.Checked Then
                    cmd.Parameters.AddWithValue("@ExpiryDate", DateTimePickerExpiry.Value)
                Else
                    cmd.Parameters.AddWithValue("@ExpiryDate", DBNull.Value)
                End If

                cmd.ExecuteNonQuery()
            End Using
        End Using
        MessageBox.Show("Item added successfully!")
        LoadInventory()
        LoadSuppliers()
        ClearInputs()
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click, BtnUpdate.Click
        Dim qty As Integer
        If Not Integer.TryParse(NumericUpDownQuantity.Value, qty) Then
            MessageBox.Show("Enter a valid quantity.")
            Exit Sub
        End If

        Dim price As Decimal
        If Not Decimal.TryParse(TextBoxPrice.Text.Trim(), Globalization.NumberStyles.Any,
                        Globalization.CultureInfo.InvariantCulture, price) Then

            Exit Sub
        End If

        ' Get selected ItemID from DataGridView
        If DGVInventory.CurrentRow Is Nothing Then
            MessageBox.Show("Please select an item to update.")
            Exit Sub
        End If
        Dim itemID As Integer = CInt(DGVInventory.CurrentRow.Cells("ItemID").Value)

        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim query As String = "
            UPDATE Inventory
            SET ItemName=@ItemName, Category=@Category, Quantity=@Quantity,
                Unit=@Unit, Price=@Price, Supplier=@Supplier, ExpiryDate=@ExpiryDate
            WHERE ItemID=@ItemID"
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@ItemName", TextBoxItemName.Text)
                cmd.Parameters.AddWithValue("@Category", ComboBoxCategory.Text)
                cmd.Parameters.AddWithValue("@Quantity", qty)
                cmd.Parameters.AddWithValue("@Unit", ComboBoxUnit.Text)
                cmd.Parameters.AddWithValue("@Price", price)
                cmd.Parameters.AddWithValue("@Supplier", ComboBoxSupplier.Text)

                ' ✅ Only save expiry if checkbox is checked
                If chkHasExpiry.Checked Then
                    cmd.Parameters.AddWithValue("@ExpiryDate", DateTimePickerExpiry.Value)
                Else
                    cmd.Parameters.AddWithValue("@ExpiryDate", DBNull.Value)
                End If

                cmd.Parameters.AddWithValue("@ItemID", itemID)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        LoadInventory()
        LoadSuppliers()
        MessageBox.Show("Item updated successfully!")
        ClearInputs()
    End Sub

    Private Sub NewMethod(qty As Integer, price As Decimal, itemID As Integer, con As SqlConnection, query As String)
        Using cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@ItemName", TextBoxItemName.Text)
            cmd.Parameters.AddWithValue("@Category", ComboBoxCategory.Text)
            cmd.Parameters.AddWithValue("@Quantity", qty)
            cmd.Parameters.AddWithValue("@Unit", ComboBoxUnit.Text)
            cmd.Parameters.AddWithValue("@Price", price)
            cmd.Parameters.AddWithValue("@Supplier", ComboBoxSupplier.Text)
            cmd.Parameters.AddWithValue("@ExpiryDate", DateTimePickerExpiry.Value)
            cmd.Parameters.AddWithValue("@ItemID", itemID)

            ' Handle expiry date
            If chkHasExpiry.Checked Then
                cmd.Parameters.AddWithValue("@ExpiryDate", DateTimePickerExpiry.Value)
            Else
                cmd.Parameters.AddWithValue("@ExpiryDate", DBNull.Value)
            End If

            cmd.Parameters.AddWithValue("@ItemID", itemID)
        End Using
    End Sub

    Private Sub AdminDBItemManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClearInputs()
        LoadInventory()
        LoadSuppliers()
        ComboBoxSupplier.SelectedIndex = -1
        chkHasExpiry.Checked = False
    End Sub

    Private Sub LoadInventory(Optional searchText As String = "")
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String
            If String.IsNullOrWhiteSpace(searchText) Then
                query = "SELECT * FROM Inventory"
            Else
                query = "SELECT * FROM Inventory WHERE ItemName LIKE @Search OR Category LIKE @Search"
            End If

            Dim cmd As New SqlCommand(query, con)
            If Not String.IsNullOrWhiteSpace(searchText) Then
                cmd.Parameters.AddWithValue("@Search", "%" & searchText & "%")
            End If

            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVInventory.DataSource = dt
        End Using
    End Sub

    Private Sub ComboBoxSupplier_TextChanged(sender As Object, e As EventArgs) Handles ComboBoxSupplier.TextChanged

    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If DGVInventory.CurrentRow Is Nothing Then Exit Sub
        Dim itemID As Integer = CInt(DGVInventory.CurrentRow.Cells("ItemID").Value)

        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            ' Because of FK with ON DELETE CASCADE, deleting Inventory will auto-delete transactions
            Dim cmdItem As New SqlCommand("DELETE FROM Inventory WHERE ItemID=@ItemID", con)
            cmdItem.Parameters.AddWithValue("@ItemID", itemID)
            cmdItem.ExecuteNonQuery()
            ' ✅ Only save expiry if checkbox is checked
            If chkHasExpiry.Checked Then
                cmdItem.Parameters.AddWithValue("@ExpiryDate", DateTimePickerExpiry.Value)
            Else
                cmdItem.Parameters.AddWithValue("@ExpiryDate", DBNull.Value)
            End If
        End Using

        LoadInventory()
        LoadSuppliers()
        MessageBox.Show("Item deleted successfully!")
        ClearInputs()
    End Sub

    Private Sub ChkHasExpiry_CheckedChanged(sender As Object, e As EventArgs) Handles chkHasExpiry.CheckedChanged
        ' Enable/disable DateTimePicker based on checkbox
        DateTimePickerExpiry.Enabled = chkHasExpiry.Checked
    End Sub

    Private Sub DGVInventory_SelectionChanged(sender As Object, e As EventArgs) Handles DGVInventory.SelectionChanged
        If IsDBNull(DGVInventory.CurrentRow.Cells("ExpiryDate").Value) Then
            chkHasExpiry.Checked = False
            DateTimePickerExpiry.Enabled = False
        Else
            chkHasExpiry.Checked = True
            DateTimePickerExpiry.Value = CDate(DGVInventory.CurrentRow.Cells("ExpiryDate").Value)
        End If
    End Sub

    Private Sub DGVInventory_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVInventory.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DGVInventory.Rows(e.RowIndex)

            ' ✅ Load basic fields
            TextBoxItemName.Text = row.Cells("ItemName").Value.ToString()
            ComboBoxCategory.Text = row.Cells("Category").Value.ToString()
            NumericUpDownQuantity.Value = row.Cells("Quantity").Value.ToString()
            ComboBoxUnit.Text = row.Cells("Unit").Value.ToString()
            TextBoxPrice.Text = row.Cells("Price").Value.ToString()
            ComboBoxSupplier.Text = row.Cells("Supplier").Value.ToString()

            ' ✅ Handle expiry toggle
            If IsDBNull(row.Cells("ExpiryDate").Value) Then
                chkHasExpiry.Checked = False
                DateTimePickerExpiry.Enabled = False
            Else
                chkHasExpiry.Checked = True
                DateTimePickerExpiry.Enabled = True
                DateTimePickerExpiry.Value = CDate(row.Cells("ExpiryDate").Value)
            End If
        End If
    End Sub

    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        LoadInventory(TextBoxSearch.Text)   ' ✅ reload grid with filtered results
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

    Private Sub TextBoxPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxPrice.KeyPress
        ' Allow digits, one decimal point, and control keys
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If

        ' Only allow one decimal point
        If e.KeyChar = "."c AndAlso CType(sender, TextBox).Text.Contains(".") Then
            e.Handled = True
        End If

    End Sub
End Class