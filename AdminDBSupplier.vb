Imports System.Data.SqlClient

Public Class AdminDBSupplier
    Private selectedSupplierID As Integer = 0

    Private Sub AdminDBSupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSuppliers()
        ClearSupplierInputs()
    End Sub

    Private Sub LoadSuppliers()
        Dim query As String = "SELECT SupplierID, SupplierName, ContactNumber, Address, Email 
                           FROM Suppliers"

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;"),
              adapter As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            DataGridViewSuppliers.DataSource = dt
        End Using
    End Sub

    Private Sub BtnAddSupplier_Click(sender As Object, e As EventArgs) Handles BtnAddSupplier.Click
        Dim query As String = "INSERT INTO Suppliers 
        (SupplierName, ContactNumber, Address, Email, IsActive) 
        VALUES (@SupplierName, @ContactNumber, @Address, @Email, 1)" ' Always active by default

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;"),
              cmd As New SqlCommand(query, con)

            cmd.Parameters.AddWithValue("@SupplierName", TextBoxSupplierName.Text)
            cmd.Parameters.AddWithValue("@ContactNumber", TextBoxContact.Text)
            cmd.Parameters.AddWithValue("@Address", TextBoxAddress.Text)
            cmd.Parameters.AddWithValue("@Email", TextBoxEmail.Text)

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        End Using

        MessageBox.Show("Supplier added successfully!")
        LoadSuppliers()
        ClearSupplierInputs()
    End Sub

    Private Sub BtnUpdateSupplier_Click(sender As Object, e As EventArgs) Handles BtnUpdateSupplier.Click
        If selectedSupplierID = 0 Then
            MessageBox.Show("Please select a supplier to update.")
            Exit Sub
        End If

        Dim query As String = "UPDATE Suppliers SET 
        SupplierName=@SupplierName, ContactNumber=@ContactNumber, Address=@Address, 
        Email=@Email 
        WHERE SupplierID=@SupplierID"

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;"),
          cmd As New SqlCommand(query, con)

            cmd.Parameters.AddWithValue("@SupplierID", selectedSupplierID)
            cmd.Parameters.AddWithValue("@SupplierName", TextBoxSupplierName.Text)
            cmd.Parameters.AddWithValue("@ContactNumber", TextBoxContact.Text)
            cmd.Parameters.AddWithValue("@Address", TextBoxAddress.Text)
            cmd.Parameters.AddWithValue("@Email", TextBoxEmail.Text)

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        End Using

        MessageBox.Show("Supplier updated successfully!")
        LoadSuppliers()
        ClearSupplierInputs()
    End Sub

    Private Sub BtnDeleteSupplier_Click(sender As Object, e As EventArgs) Handles BtnDeleteSupplier.Click
        If selectedSupplierID = 0 Then
            MessageBox.Show("Please select a supplier to delete.")
            Exit Sub
        End If

        Dim query As String = "DELETE FROM Suppliers WHERE SupplierID=@SupplierID"

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;"),
              cmd As New SqlCommand(query, con)

            cmd.Parameters.AddWithValue("@SupplierID", selectedSupplierID)

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        End Using

        MessageBox.Show("Supplier deleted successfully!")
        LoadSuppliers()
        ClearSupplierInputs()
    End Sub

    Private Sub ClearSupplierInputs()
        TextBoxSupplierName.Clear()
        TextBoxContact.Clear()
        TextBoxAddress.Clear()
        TextBoxEmail.Clear()
        selectedSupplierID = 0
    End Sub

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

    Private Sub DataGridViewSuppliers_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridViewSuppliers.SelectionChanged
        If DataGridViewSuppliers.SelectedRows.Count > 0 Then
            Dim row As DataGridViewRow = DataGridViewSuppliers.SelectedRows(0)
            selectedSupplierID = Convert.ToInt32(row.Cells("SupplierID").Value)

            ' Populate form fields
            TextBoxSupplierName.Text = row.Cells("SupplierName").Value.ToString()
            TextBoxContact.Text = row.Cells("ContactNumber").Value.ToString()
            TextBoxAddress.Text = row.Cells("Address").Value.ToString()
            TextBoxEmail.Text = row.Cells("Email").Value.ToString()
        End If
    End Sub

    Private Sub SupplierSearch_TextChanged(sender As Object, e As EventArgs) Handles SupplierSearch.TextChanged
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String

            ' Show all suppliers if search box is empty
            If SupplierSearch.Text.Trim = "" Then
                query = "
                SELECT SupplierID, SupplierName, ContactNumber, Address, Email, IsActive
                FROM Suppliers
                ORDER BY SupplierName
            "
            Else
                query = "
                SELECT SupplierID, SupplierName, ContactNumber, Address, Email, IsActive
                FROM Suppliers
                WHERE COALESCE(SupplierName,'') LIKE @search
                   OR COALESCE(ContactNumber,'') LIKE @search
                   OR COALESCE(Address,'') LIKE @search
                   OR COALESCE(Email,'') LIKE @search
                   OR COALESCE(CAST(IsActive AS VARCHAR),'') LIKE @search
                ORDER BY SupplierName
            "
            End If

            Using cmd As New SqlCommand(query, con)
                If SupplierSearch.Text.Trim <> "" Then
                    cmd.Parameters.AddWithValue("@search", "%" & SupplierSearch.Text.Trim & "%")
                End If

                Dim adapter As New SqlDataAdapter(cmd)
                Dim table As New DataTable()
                adapter.Fill(table)

                DataGridViewSuppliers.DataSource = table
            End Using
        End Using

    End Sub
End Class