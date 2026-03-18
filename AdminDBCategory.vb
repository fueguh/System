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

        ' Button States
        BtnAddCategory.Enabled = True
        BtnUpdateCategory.Enabled = False
        BtnDeleteCategory.Enabled = False

        DataGridViewCategories.ClearSelection()
        TextBoxCategoryName.Focus()
    End Sub

    Private Sub LoadCategories()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim query As String = "SELECT CategoryID, CategoryName, Description FROM Categories ORDER BY CategoryName"
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
        ' NEW DUPLICATE CHECK
        If IsDuplicateCategory(TextBoxCategoryName.Text.Trim(), 0) Then
            MessageBox.Show("A category with this name already exists.", "Duplicate Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBoxCategoryName.Focus()
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
        SystemSession.LogAudit($"Added new category: {TextBoxCategoryName.Text.Trim}", "Category Maintenance", SystemSession.LoggedInUserID)

        MessageBox.Show("Category added successfully.")
        LoadCategories()
        clearform()
    End Sub

    Private Sub BtnUpdateCategory_Click(sender As Object, e As EventArgs) Handles BtnUpdateCategory.Click
        ' 1. Selection Check
        If selectedCategoryID = 0 Then
            MessageBox.Show("Please select a category to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' 2. Basic Validation
        Dim catName As String = TextBoxCategoryName.Text.Trim()
        If catName = "" Then
            MessageBox.Show("Category name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBoxCategoryName.Focus()
            Exit Sub
        End If

        ' 3. DUPLICATE CHECK (Specify current ID to ignore the record being edited)
        If IsDuplicateCategory(catName, selectedCategoryID) Then
            MessageBox.Show($"The category name '{catName}' is already in use by another record.",
                        "Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBoxCategoryName.Focus()
            Exit Sub
        End If

        ' 4. Database Update
        Try
            Using con As New SqlConnection(My.Settings.DentalDBConnection2)
                con.Open()
                Dim query As String = "UPDATE Categories SET CategoryName=@name, Description=@desc WHERE CategoryID=@id"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@id", selectedCategoryID)
                    cmd.Parameters.AddWithValue("@name", catName)
                    cmd.Parameters.AddWithValue("@desc", TextBoxDescription.Text.Trim)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ' Audit Log
            SystemSession.LogAudit($"Updated category: {catName}", "Category Maintenance", SystemSession.LoggedInUserID)

            MessageBox.Show("Category updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadCategories()
            clearform()
        Catch ex As Exception
            MessageBox.Show("An error occurred while updating: " & ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnDeleteCategory_Click(sender As Object, e As EventArgs) Handles BtnDeleteCategory.Click
        If selectedCategoryID = 0 Then
            MessageBox.Show("Please select a category to delete.")
            Exit Sub
        End If

        ' Capture the name for the audit log BEFORE the confirmation or deletion
        ' This ensures the name is saved even if the user clicks a different row
        Dim deletedName As String = TextBoxCategoryName.Text.Trim()

        If MessageBox.Show($"Are you sure you want to delete the category '{deletedName}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Exit Sub
        End If

        Try
            Using con As New SqlConnection(My.Settings.DentalDBConnection2)
                con.Open()
                Dim query As String = "DELETE FROM Categories WHERE CategoryID=@id"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@id", selectedCategoryID)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ' ✅ CORRECTED AUDIT LOG: Use the captured name and correct action text
            SystemSession.LogAudit($"Deleted category: {deletedName}", "Category Maintenance", SystemSession.LoggedInUserID)

            MessageBox.Show("Category deleted successfully.")
            LoadCategories()
            clearform()

        Catch ex As SqlException
            If ex.Number = 547 Then
                MessageBox.Show("Cannot delete this category because it is being used by existing items.", "Delete Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ' Log the failure too
                SystemSession.LogAudit($"Delete failed: Category '{deletedName}' is in use.", "Category Maintenance", SystemSession.LoggedInUserID)
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
            ' --- BUTTON STATES ---
            BtnAddCategory.Enabled = False ' Prevent adding when a record is selected
            BtnUpdateCategory.Enabled = True
            BtnDeleteCategory.Enabled = True
        End If
    End Sub

    Dim connectionString As String = My.Settings.DentalDBConnection2

    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs) Handles CategorySearch.TextChanged
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim query As String

            If CategorySearch.Text.Trim = "" Then
                query = "
                SELECT CategoryID, CategoryName, Description
                FROM Categories
                ORDER BY CategoryName
            "
            Else
                query = "
                SELECT CategoryID, CategoryName, Description
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
    Private Function IsDuplicateCategory(name As String, id As Integer) As Boolean
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            ' Check if name exists excluding the current ID
            Dim query As String = "SELECT COUNT(*) FROM Categories WHERE CategoryName = @name AND CategoryID <> @id"
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@name", name.Trim())
                cmd.Parameters.AddWithValue("@id", id)
                Return CInt(cmd.ExecuteScalar()) > 0
            End Using
        End Using
    End Function
    Private Sub TextBoxCategoryName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxCategoryName.KeyPress
        ' 1. Always allow Backspace (KeyChar 8)
        If Char.IsControl(e.KeyChar) Then Return

        ' 2. Define the allowed list clearly
        ' Specifically check for: Space, Ampersand, Comma, Hyphen, Slash
        Dim allowedSpecial As String = " &,-/"

        ' 3. Check if the character is a Letter, a Digit, or in our special list
        If Char.IsLetterOrDigit(e.KeyChar) Then
            Return
        ElseIf allowedSpecial.Contains(e.KeyChar) Then
            ' 4. Additional Rule: Don't allow a symbol at the very beginning
            If TextBoxCategoryName.Text.Length = 0 Then
                e.Handled = True
            End If
            Return
        Else
            ' If it's anything else, block it
            e.Handled = True
        End If
    End Sub
    Private Sub TextBoxCategoryName_Leave(sender As Object, e As EventArgs) Handles TextBoxCategoryName.Leave
        If Not String.IsNullOrWhiteSpace(TextBoxCategoryName.Text) Then
            ' Trims leading/trailing spaces and capitalizes correctly
            ' "hygiene & oral care" -> "Hygiene & Oral Care"
            TextBoxCategoryName.Text = StrConv(TextBoxCategoryName.Text.Trim(), VbStrConv.ProperCase)
        End If
    End Sub
    Private Sub TextBoxDescription_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxDescription.KeyPress
        ' 1. Allow Backspace and other control keys
        If Char.IsControl(e.KeyChar) Then Return

        ' 2. Define allowed special characters (Comma, Dot, Hyphen, Slash, Parentheses)
        Dim allowedChars As String = " ,.-/()"

        ' 3. Allow Letters, Digits, and our allowed special characters
        If Not (Char.IsLetterOrDigit(e.KeyChar) OrElse allowedChars.Contains(e.KeyChar)) Then
            e.Handled = True
            Return
        End If

        ' 4. Optional: Prevent starting the description with a symbol or space
        If TextBoxDescription.Text.Length = 0 AndAlso Not Char.IsLetterOrDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub TextBoxDescription_Leave(sender As Object, e As EventArgs) Handles TextBoxDescription.Leave
        If Not String.IsNullOrWhiteSpace(TextBoxDescription.Text) Then
            ' Trims extra spaces and ensures the first letter is capitalized
            Dim text As String = TextBoxDescription.Text.Trim()
            TextBoxDescription.Text = Char.ToUpper(text(0)) & text.Substring(1)
        End If
    End Sub
    Private Sub DataGridViewCategories_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewCategories.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridViewCategories.Rows(e.RowIndex)

            selectedCategoryID = Convert.ToInt32(row.Cells("CategoryID").Value)

            ' Use If-Then or Coalesce logic to handle potential Nulls
            TextBoxCategoryName.Text = If(row.Cells("CategoryName").Value Is DBNull.Value, "", row.Cells("CategoryName").Value.ToString())
            TextBoxDescription.Text = If(row.Cells("Description").Value Is DBNull.Value, "", row.Cells("Description").Value.ToString())
        End If
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        clearform()
    End Sub
End Class