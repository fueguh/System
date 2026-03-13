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
        ' 1. Basic Validation
        If TextBoxSupplierName.Text.Trim = "" Then
            MessageBox.Show("Supplier name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBoxSupplierName.Focus()
            Exit Sub
        End If

        If Not TextBoxEmail.Text.Contains("@") OrElse Not TextBoxEmail.Text.Contains(".") Then
            MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBoxEmail.Focus()
            Exit Sub
        End If

        ' Capture the name for the audit log
        Dim sName As String = TextBoxSupplierName.Text.Trim()

        Try
            Dim query As String = "INSERT INTO Suppliers (SupplierName, ContactNumber, Address, Email, IsActive) 
                               VALUES (@SupplierName, @ContactNumber, @Address, @Email, 1)"

            Using con As New SqlConnection(My.Settings.DentalDBConnection2),
              cmd As New SqlCommand(query, con)

                cmd.Parameters.AddWithValue("@SupplierName", sName)
                cmd.Parameters.AddWithValue("@ContactNumber", TextBoxContact.Text.Trim())
                cmd.Parameters.AddWithValue("@Address", TextBoxAddress.Text.Trim())
                cmd.Parameters.AddWithValue("@Email", TextBoxEmail.Text.Trim())

                con.Open()
                cmd.ExecuteNonQuery()
            End Using

            ' ✅ AUDIT LOG: Record the addition
            SystemSession.LogAudit($"Added new supplier: {sName}", "Supplier Maintenance", SystemSession.LoggedInUserID)

            MessageBox.Show("Supplier added successfully!")

            LoadSuppliers()
            Clearform()

        Catch ex As Exception
            MessageBox.Show("Error adding supplier: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnUpdateSupplier_Click(sender As Object, e As EventArgs) Handles BtnUpdateSupplier.Click
        ' 1. Basic Validations
        If selectedSupplierID = 0 Then
            MessageBox.Show("Please select a supplier to update.")
            Exit Sub
        End If

        If Not TextBoxEmail.Text.Contains("@") OrElse Not TextBoxEmail.Text.Contains(".") Then
            MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBoxEmail.Focus()
            Exit Sub
        End If

        Try
            Dim changes As String = ""
            Dim targetName As String = TextBoxSupplierName.Text.Trim()

            Using con As New SqlConnection(My.Settings.DentalDBConnection2)
                con.Open()

                ' --- STEP A: FETCH OLD DATA FOR COMPARISON ---
                Dim oldName As String = "", oldPh As String = "", oldAdd As String = "", oldEm As String = ""
                Dim getOldQuery As String = "SELECT SupplierName, ContactNumber, Address, Email FROM Suppliers WHERE SupplierID = @id"

                Using cmdOld As New SqlCommand(getOldQuery, con)
                    cmdOld.Parameters.AddWithValue("@id", selectedSupplierID)
                    Using dr As SqlDataReader = cmdOld.ExecuteReader()
                        If dr.Read() Then
                            oldName = dr("SupplierName").ToString()
                            oldPh = dr("ContactNumber").ToString()
                            oldAdd = dr("Address").ToString()
                            oldEm = dr("Email").ToString()
                        End If
                    End Using
                End Using

                ' --- STEP B: COMPARE OLD VS NEW ---
                If oldName <> TextBoxSupplierName.Text.Trim() Then changes &= $"Name: {oldName} -> {TextBoxSupplierName.Text.Trim()}; "
                If oldPh <> TextBoxContact.Text.Trim() Then changes &= $"Phone: {oldPh} -> {TextBoxContact.Text.Trim()}; "
                If oldAdd <> TextBoxAddress.Text.Trim() Then changes &= $"Address: {oldAdd} -> {TextBoxAddress.Text.Trim()}; "
                If oldEm <> TextBoxEmail.Text.Trim() Then changes &= $"Email: {oldEm} -> {TextBoxEmail.Text.Trim()}; "

                ' --- STEP C: EXECUTE UPDATE ---
                Dim query As String = "UPDATE Suppliers SET SupplierName=@SupplierName, ContactNumber=@ContactNumber, 
                                   Address=@Address, Email=@Email WHERE SupplierID=@SupplierID"

                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@SupplierID", selectedSupplierID)
                    cmd.Parameters.AddWithValue("@SupplierName", TextBoxSupplierName.Text.Trim())
                    cmd.Parameters.AddWithValue("@ContactNumber", TextBoxContact.Text.Trim())
                    cmd.Parameters.AddWithValue("@Address", TextBoxAddress.Text.Trim())
                    cmd.Parameters.AddWithValue("@Email", TextBoxEmail.Text.Trim())
                    cmd.ExecuteNonQuery()
                End Using

                ' --- STEP D: LOG AUDIT ---
                Dim auditMsg As String = If(String.IsNullOrEmpty(changes),
                                    $"Updated supplier {targetName} (No changes made)",
                                    $"Updated supplier {targetName}. Changes: {changes}")

                SystemSession.LogAudit(auditMsg, "Supplier Maintenance", SystemSession.LoggedInUserID)
            End Using

            MessageBox.Show("Supplier updated successfully!")

            LoadSuppliers()
            Clearform()

        Catch ex As Exception
            MessageBox.Show("Update Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnDeleteSupplier_Click(sender As Object, e As EventArgs) Handles BtnDeleteSupplier.Click
        If selectedSupplierID = 0 Then
            MessageBox.Show("Please select a supplier to delete.")
            Exit Sub
        End If

        ' Capture the name for the audit log before the delete happens
        Dim deletedName As String = TextBoxSupplierName.Text.Trim()

        ' ADD THIS CONFIRMATION:
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this supplier?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If result = DialogResult.No Then Exit Sub

        Try
            Dim query As String = "DELETE FROM Suppliers WHERE SupplierID=@SupplierID"

            Using con As New SqlConnection(My.Settings.DentalDBConnection2),
              cmd As New SqlCommand(query, con)

                cmd.Parameters.AddWithValue("@SupplierID", selectedSupplierID)

                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using

            ' ✅ AUDIT LOG: Only adding this line
            SystemSession.LogAudit($"Deleted supplier: {deletedName}", "Supplier Maintenance", SystemSession.LoggedInUserID)

            MessageBox.Show("Supplier deleted successfully!")
            LoadSuppliers()
            Clearform()

        Catch ex As SqlException
            ' Optional: Log the failure if it's a Foreign Key error
            If ex.Number = 547 Then
                SystemSession.LogAudit($"Delete failed: Supplier '{deletedName}' is in use.", "Supplier Maintenance", SystemSession.LoggedInUserID)
            End If
            MessageBox.Show("Error: " & ex.Message)
        End Try
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
        ' 1. Allow Backspace and System keys
        If Char.IsControl(e.KeyChar) Then Return

        ' 2. Allowed special characters for addresses
        ' Added: # (for unit numbers) and () (for landmarks/districts)
        Dim allowedChars As String = " ,.-/#()"

        ' 3. Validate character
        If Not (Char.IsLetterOrDigit(e.KeyChar) OrElse allowedChars.Contains(e.KeyChar)) Then
            e.Handled = True
            Return
        End If

        ' 4. Prevent consecutive symbols (e.g., prevent ",," or "++")
        ' We allow spaces to be consecutive if needed, but block double dots/commas
        Dim symbolsToLimit As String = ",.-/#"
        If symbolsToLimit.Contains(e.KeyChar) AndAlso TextBoxAddress.Text.Length > 0 Then
            Dim lastChar As Char = TextBoxAddress.Text.Last()
            If symbolsToLimit.Contains(lastChar) Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub TextBoxAddress_Leave(sender As Object, e As EventArgs) Handles TextBoxAddress.Leave
        If Not String.IsNullOrWhiteSpace(TextBoxAddress.Text) Then
            ' Automatically converts "manila, philippines" to "Manila, Philippines"
            TextBoxAddress.Text = StrConv(TextBoxAddress.Text, VbStrConv.ProperCase)
        End If
    End Sub
    Private Sub TextBoxEmail_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxEmail.KeyPress
        ' 1. Allow Backspace/System keys
        If Char.IsControl(e.KeyChar) Then Return

        ' 2. Define extra allowed characters for emails
        Dim allowedSpecial As String = "@._-"

        ' 3. Check if it's a letter, digit, or one of the allowed symbols
        If Not (Char.IsLetterOrDigit(e.KeyChar) OrElse allowedSpecial.Contains(e.KeyChar)) Then
            e.Handled = True
            Return
        End If

        ' 4. Specific Logic for '@'
        If e.KeyChar = "@"c Then
            ' Block if it's the first character OR if an @ already exists
            If TextBoxEmail.Text.Length = 0 OrElse TextBoxEmail.Text.Contains("@") Then
                e.Handled = True
            End If
            Return
        End If

        ' 5. Specific Logic for Dot '.'
        If e.KeyChar = "."c Then
            ' Block if it's the first character OR the last character was also a dot
            If TextBoxEmail.Text.Length = 0 OrElse TextBoxEmail.Text.EndsWith(".") Then
                e.Handled = True
            End If
            Return
        End If
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
