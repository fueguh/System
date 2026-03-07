Imports System.Data.SqlClient

Public Class AdminDBCategory
    ' Keep track of the selected ID for updates/deletes
    Private selectedCategoryID As Integer = 0

    Private Sub AdminDBCategory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCategories()
        clearform()
    End Sub

    ''' <summary>
    ''' Resets all input fields and the selected ID tracker.
    ''' </summary>
    Private Sub clearform()
        TextBoxCategoryName.Clear()
        TextBoxDescription.Clear()
        ' If you have a search box, clear it too
        If CategorySearch IsNot Nothing Then CategorySearch.Clear()

        selectedCategoryID = 0

        ' Optional: Deselect rows in the grid for a clean slate
        DataGridViewCategories.ClearSelection()

        ' Focus the first input for better UX
        TextBoxCategoryName.Focus()
    End Sub

    Private Sub LoadCategories()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim query As String = "SELECT CategoryID, CategoryName, Description, IsActive FROM Categories ORDER BY CategoryName"
            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DataGridViewCategories.DataSource = dt
            If DataGridViewCategories.Columns.Contains("CategoryID") Then
                DataGridViewCategories.Columns("CategoryID").Visible = False
            End If
        End Using
    End Sub
    Private Sub BtnAddCategory_Click(sender As Object, e As EventArgs) Handles BtnAddCategory.Click
        If TextBoxCategoryName.Text.Trim = "" Then
            MessageBox.Show("Category name is required.")
            Exit Sub
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim query As String = "INSERT INTO Categories (CategoryName, Description) VALUES (@name, @desc)"
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@name", TextBoxCategoryName.Text.Trim)
                cmd.Parameters.AddWithValue("@desc", TextBoxDescription.Text.Trim)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        ' ✅ AUDIT LOG: Log the addition
        SystemSession.LogAudit($"Added new category: {TextBoxCategoryName.Text.Trim}", "Inventory Management", SystemSession.LoggedInUserID)

        MessageBox.Show("Category added successfully.")
        LoadCategories()
        clearform()
    End Sub

    Private Sub BtnUpdateCategory_Click(sender As Object, e As EventArgs) Handles BtnUpdateCategory.Click
        If selectedCategoryID = 0 Then
            MessageBox.Show("Please select a category to update.")
            Exit Sub
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim query As String = "UPDATE Categories SET CategoryName=@name, Description=@desc WHERE CategoryID=@id"
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", selectedCategoryID)
                cmd.Parameters.AddWithValue("@name", TextBoxCategoryName.Text.Trim)
                cmd.Parameters.AddWithValue("@desc", TextBoxDescription.Text.Trim)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        ' ✅ AUDIT LOG: Log the update
        SystemSession.LogAudit($"Updated category ID {selectedCategoryID}: {TextBoxCategoryName.Text.Trim}", "Inventory Management", SystemSession.LoggedInUserID)

        MessageBox.Show("Category updated successfully.")
        LoadCategories()
        clearform()
    End Sub

    Private Sub BtnDeleteCategory_Click(sender As Object, e As EventArgs) Handles BtnDeleteCategory.Click
        If selectedCategoryID = 0 Then
            MessageBox.Show("Please select a category to delete.")
            Exit Sub
        End If

        If MessageBox.Show("Are you sure you want to delete this category?", "Confirm Delete", MessageBoxButtons.YesNo) = DialogResult.No Then
            Exit Sub
        End If

        ' Capture name for the audit log before it's deleted
        Dim categoryName As String = TextBoxCategoryName.Text

        Try
            Using con As New SqlConnection(My.Settings.DentalDBConnection2)
                con.Open()
                Dim query As String = "DELETE FROM Categories WHERE CategoryID=@id"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@id", selectedCategoryID)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ' ✅ AUDIT LOG: Log the deletion
            SystemSession.LogAudit($"Deleted category: {categoryName} (ID: {selectedCategoryID})", "Inventory Management", SystemSession.LoggedInUserID)

            MessageBox.Show("Category deleted successfully.")
            LoadCategories()
            clearform()

        Catch ex As SqlException
            ' Handle Foreign Key errors (e.g., if items are still assigned to this category)
            If ex.Number = 547 Then
                MessageBox.Show("Cannot delete this category because it is being used by existing items.", "Delete Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                SystemSession.LogAudit($"Delete failed: Category '{categoryName}' is in use.", "Inventory Management", SystemSession.LoggedInUserID)
            Else
                MessageBox.Show("Error: " & ex.Message)
            End If
        End Try
    End Sub

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

    Private Sub TextBoxCategoryName_TextChanged(sender As Object, e As EventArgs) Handles TextBoxCategoryName.TextChanged

    End Sub

    Private Sub DataGridViewCategories_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridViewCategories.SelectionChanged
        If DataGridViewCategories.SelectedRows.Count > 0 Then
            Dim row As DataGridViewRow = DataGridViewCategories.SelectedRows(0)

            ' Capture CategoryID for Update/Delete
            selectedCategoryID = Convert.ToInt32(row.Cells("CategoryID").Value)

            ' Populate textboxes safely
            If row.Cells("CategoryName").Value IsNot Nothing Then
                TextBoxCategoryName.Text = row.Cells("CategoryName").Value.ToString()
            Else
                TextBoxCategoryName.Clear()
            End If

            If DataGridViewCategories.Columns.Contains("Description") AndAlso row.Cells("Description").Value IsNot Nothing Then
                TextBoxDescription.Text = row.Cells("Description").Value.ToString()
            Else
                TextBoxDescription.Clear()
            End If
        End If
    End Sub

    Dim connectionString As String = My.Settings.DentalDBConnection2

    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs) Handles CategorySearch.TextChanged
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim query As String

            If CategorySearch.Text.Trim = "" Then
                query = "
                SELECT CategoryID, CategoryName, Description, IsActive
                FROM Categories
                ORDER BY CategoryName
            "
            Else
                query = "
                SELECT CategoryID, CategoryName, Description, IsActive
                FROM Categories
                WHERE COALESCE(CategoryName,'') LIKE @search
                   OR COALESCE(Description,'') LIKE @search
                ORDER BY CategoryName
            "
            End If

            Using cmd As New SqlCommand(query, con)
                If CategorySearch.Text.Trim <> "" Then
                    cmd.Parameters.AddWithValue("@search", "%" & CategorySearch.Text.Trim & "%")
                End If

                Dim adapter As New SqlDataAdapter(cmd)
                Dim table As New DataTable()
                adapter.Fill(table)

                DataGridViewCategories.DataSource = table
            End Using
        End Using
    End Sub

    Private Sub TextBoxCategoryName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxCategoryName.KeyPress
        ' Allow control keys (Backspace, Delete, etc.)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        ' Allow letters and spaces only
        If Not (Char.IsLetter(e.KeyChar) OrElse e.KeyChar = " "c) Then
            e.Handled = True ' Block invalid input
        End If
    End Sub

    Private Sub TextBoxDescription_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxDescription.KeyPress
        ' Allow control keys (Backspace, Delete, etc.)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        ' Allow letters and spaces only
        If Not (Char.IsLetter(e.KeyChar) OrElse e.KeyChar = " "c) Then
            e.Handled = True ' Block invalid input
        End If
    End Sub

    Private Sub DataGridViewCategories_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewCategories.CellClick

        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridViewCategories.Rows(e.RowIndex)

            ' Assign the CategoryID
            selectedCategoryID = Convert.ToInt32(row.Cells("CategoryID").Value)

            ' Populate textboxes
            TextBoxCategoryName.Text = row.Cells("CategoryName").Value.ToString()
            TextBoxDescription.Text = row.Cells("Description").Value.ToString()
        End If

    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        clearform()
    End Sub
End Class