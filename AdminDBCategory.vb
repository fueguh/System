Imports System.Data.SqlClient

Public Class AdminDBCategory
    Private Sub AdminDBCategory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCategories()
        ClearCategoryInputs()
    End Sub

    Private Sub LoadCategories()
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String = "
            SELECT CategoryID, CategoryName, Description, IsActive
            FROM Categories
            ORDER BY CategoryName
        "

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DataGridViewCategories.DataSource = dt
        End Using
    End Sub
    Private Sub ClearCategoryInputs()
        TextBoxCategoryName.Clear()
        TextBoxDescription.Clear()
        selectedCategoryID = 0
    End Sub

    Private selectedCategoryID As Integer = 0



    Private Sub BtnAddCategory_Click(sender As Object, e As EventArgs) Handles BtnAddCategory.Click
        If TextBoxCategoryName.Text.Trim = "" Then
            MessageBox.Show("Category name is required.")
            Exit Sub
        End If

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String = "
            INSERT INTO Categories (CategoryName, Description)
            VALUES (@name, @desc)
        "

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@name", TextBoxCategoryName.Text.Trim)
                cmd.Parameters.AddWithValue("@desc", TextBoxDescription.Text.Trim)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("Category added successfully.")
        LoadCategories()
        ClearCategoryInputs()
    End Sub

    Private Sub BtnUpdateCategory_Click(sender As Object, e As EventArgs) Handles BtnUpdateCategory.Click
        If selectedCategoryID = 0 Then
            MessageBox.Show("Please select a category to update.")
            Exit Sub
        End If

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String = "
            UPDATE Categories
            SET CategoryName=@name, Description=@desc
            WHERE CategoryID=@id
        "

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", selectedCategoryID)
                cmd.Parameters.AddWithValue("@name", TextBoxCategoryName.Text.Trim)
                cmd.Parameters.AddWithValue("@desc", TextBoxDescription.Text.Trim)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("Category updated successfully.")
        LoadCategories()
        ClearCategoryInputs()
    End Sub

    Private Sub BtnDeleteCategory_Click(sender As Object, e As EventArgs) Handles BtnDeleteCategory.Click
        If selectedCategoryID = 0 Then
            MessageBox.Show("Please select a category to delete.")
            Exit Sub
        End If

        If MessageBox.Show("Are you sure you want to delete this category?", "Confirm Delete", MessageBoxButtons.YesNo) = DialogResult.No Then
            Exit Sub
        End If

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String = "
            DELETE FROM Categories
            WHERE CategoryID=@id
        "

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", selectedCategoryID)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("Category deleted successfully.")
        LoadCategories()
        ClearCategoryInputs()
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

    Dim connectionString As String = "Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;"

    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs) Handles CategorySearch.TextChanged
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
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
End Class