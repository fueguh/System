Imports System.Data.SqlClient

Public Class AdminDBSupplier
    Private selectedSupplierID As Integer = 0

    Private Sub AdminDBSupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSuppliers()
        Clearform()
    End Sub
    Private Sub LoadSuppliers()
        Dim query As String = "SELECT SupplierID, SupplierName, ContactNumber, Address, Email 
                           FROM Suppliers"

        Using con As New SqlConnection(My.Settings.DentalDBConnection2),
              adapter As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            DataGridViewSuppliers.DataSource = dt
            DataGridViewSuppliers.Columns("SupplierID").Visible = False
        End Using
    End Sub

    Private Sub BtnAddSupplier_Click(sender As Object, e As EventArgs) Handles BtnAddSupplier.Click
        Dim query As String = "INSERT INTO Suppliers 
        (SupplierName, ContactNumber, Address, Email, IsActive) 
        VALUES (@SupplierName, @ContactNumber, @Address, @Email, 1)" ' Always active by default

        Using con As New SqlConnection(My.Settings.DentalDBConnection2),
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
        Clearform()
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

        Using con As New SqlConnection(My.Settings.DentalDBConnection2),
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
        Clearform()

    End Sub

    Private Sub BtnDeleteSupplier_Click(sender As Object, e As EventArgs) Handles BtnDeleteSupplier.Click
        If selectedSupplierID = 0 Then
            MessageBox.Show("Please select a supplier to delete.")
            Exit Sub
        End If

        ' ADD THIS CONFIRMATION:
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this supplier?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If result = DialogResult.No Then Exit Sub

        Dim query As String = "DELETE FROM Suppliers WHERE SupplierID=@SupplierID"

        Using con As New SqlConnection(My.Settings.DentalDBConnection2),
              cmd As New SqlCommand(query, con)

            cmd.Parameters.AddWithValue("@SupplierID", selectedSupplierID)

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        End Using

        MessageBox.Show("Supplier deleted successfully!")
        LoadSuppliers()
        Clearform()
    End Sub

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

    Private Sub SupplierSearch_TextChanged(sender As Object, e As EventArgs) Handles SupplierSearch.TextChanged
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
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

    Private Sub TextBoxSupplierName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxSupplierName.KeyPress
        If Char.IsControl(e.KeyChar) Then Return

        ' Allow letters and spaces
        If Char.IsLetter(e.KeyChar) OrElse e.KeyChar = " "c Then Return

        ' Allow Dot, but not at the very start
        If e.KeyChar = "."c Then
            If TextBoxSupplierName.Text.Length = 0 Then
                e.Handled = True
            End If
            Return
        End If

        e.Handled = True
    End Sub

    Private Sub TextBoxContact_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxContact.KeyPress
        If Char.IsControl(e.KeyChar) Then Return
        If Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBoxAddress_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxAddress.KeyPress
        ' 1. Allow Backspace/System keys
        If Char.IsControl(e.KeyChar) Then Return

        ' 2. Define allowed special characters
        Dim allowedChars As String = " ,.-/"

        ' 3. Check if it's a Letter, Digit, or one of our allowed symbols
        If Not (Char.IsLetterOrDigit(e.KeyChar) OrElse allowedChars.Contains(e.KeyChar)) Then
            e.Handled = True
            Return
        End If

        ' 4. Prevent "Symbol Spam" (Double dots, double commas, etc.)
        ' If the key just pressed is a symbol AND the last character was also a symbol
        If allowedChars.Contains(e.KeyChar) AndAlso TextBoxAddress.Text.Length > 0 Then
            Dim lastChar As Char = TextBoxAddress.Text.Last()
            If allowedChars.Contains(lastChar) Then
                e.Handled = True ' Block the second consecutive symbol
            End If
        End If
    End Sub

    Private Sub TextBoxEmail_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxEmail.KeyPress
        If Char.IsControl(e.KeyChar) Then Return

        ' Allow letters, digits, and @
        If Char.IsLetterOrDigit(e.KeyChar) OrElse e.KeyChar = "@"c Then Return

        ' Handle the Dot
        If e.KeyChar = "."c Then
            ' Block dot if it's the first character or if the previous character was also a dot
            If TextBoxEmail.Text.Length = 0 OrElse TextBoxEmail.Text.EndsWith(".") Then
                e.Handled = True
            End If
            Return
        End If

        ' Block everything else
        e.Handled = True
    End Sub

    Private Sub DataGridViewSuppliers_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewSuppliers.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridViewSuppliers.Rows(e.RowIndex)
            selectedSupplierID = Convert.ToInt32(row.Cells("SupplierID").Value)

            TextBoxSupplierName.Text = row.Cells("SupplierName").Value.ToString()
            TextBoxEmail.Text = row.Cells("Email").Value.ToString()
            TextBoxAddress.Text = row.Cells("Address").Value.ToString()
            TextBoxContact.Text = row.Cells("ContactNumber").Value.ToString()

            ' Enable editing mode
            BtnAddSupplier.Enabled = False
            BtnUpdateSupplier.Enabled = True
            BtnDeleteSupplier.Enabled = True
        End If
    End Sub

    Private Sub Clearform()
        ' Reset the ID tracker
        selectedSupplierID = 0

        ' Clear all input fields
        TextBoxSupplierName.Clear()
        TextBoxContact.Clear()
        TextBoxAddress.Clear()
        TextBoxEmail.Clear()

        ' Reset the search box (this triggers SupplierSearch_TextChanged)
        SupplierSearch.Text = ""

        ' Clear DataGridView selection
        DataGridViewSuppliers.ClearSelection()

        ' UI State Management
        BtnAddSupplier.Enabled = True
        BtnUpdateSupplier.Enabled = False
        BtnDeleteSupplier.Enabled = False

        ' Set focus back to the start
        TextBoxSupplierName.Focus()
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Clearform()
    End Sub
End Class
