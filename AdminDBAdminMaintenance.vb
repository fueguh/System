Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class AdminDBAdminMaintenance
    Public Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
            Dim hash As Byte() = sha256.ComputeHash(bytes)
            Return BitConverter.ToString(hash).Replace("-", "").ToLower()
        End Using
    End Function

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        ' ✅ If no admin is logged in, go back to Login form
        If SystemSession.LoggedInUserID = 0 OrElse SystemSession.LoggedInRole <> "Admin" Then
            ' No active admin session → just redirect to Login without logging a logout
            Login.Show()
            Me.Hide()
            Exit Sub
        End If

        ' Otherwise, show user dashboard
        SystemSession.NavigateToDashboard(Me)
        Me.Hide()
    End Sub
    Private Sub ClearAdminInputs()
        TxtName.Clear()
        TxtUsername.Clear()
        TxtPhone.Clear()
        TxtPassword.Clear()
        TxtEmail.Clear()
        chkShowPassword.Checked = False
        TxtPassword.UseSystemPasswordChar = True
        ' Reset logic state
        selectedAdminID = 0
        BTNAdd.Enabled = True
        BtnUpdate.Enabled = False
        BtnDelete.Enabled = False
    End Sub

    Private Sub LoadAdmins()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim query As String = "
            SELECT UserID, FullName, Username, PhoneNumber, Email
            FROM Users
            WHERE Role = 'Admin'
            ORDER BY FullName
        "

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DataGridViewAdmins.DataSource = dt
            DataGridViewAdmins.Columns("UserID").Visible = False
        End Using
    End Sub


    Private Sub AdminDBAdminMaintenance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAdmins()
        ClearAdminInputs()
    End Sub

    Private Sub ChkShowPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPassword.CheckedChanged
        TxtPassword.UseSystemPasswordChar = Not chkShowPassword.Checked
    End Sub

    Private Sub BTNAdd_Click(sender As Object, e As EventArgs) Handles BTNAdd.Click

        ' Validate fields first
        If Not ValidateUsername() Then Exit Sub
        If Not ValidatePassword() Then Exit Sub
        If Not ValidateFullName() Then Exit Sub
        ' Check duplicates
        If IsDuplicateEmailOrUsername(TxtEmail.Text.Trim(), TxtUsername.Text.Trim()) Then
            MessageBox.Show("Email or Username already exists. Please choose another.")
            Exit Sub
        End If

        ' Flexible Email Format Check (Only if not empty)
        If Not String.IsNullOrWhiteSpace(TxtEmail.Text) Then
            If Not ValidateEmail() Then Exit Sub
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            ' Insert into Users
            Dim queryUser As String = "
    INSERT INTO Users (FullName, Username, Password, Role, PhoneNumber, Email, DateCreated)
    VALUES (@fullname, @username, @password, 'Admin', @phone, @email, GETDATE())
"
            ' ✅ Hash password
            Dim hashedPassword As String = HashPassword(TxtPassword.Text)

            Using cmdUser As New SqlCommand(queryUser, con)
                cmdUser.Parameters.AddWithValue("@fullname", TxtName.Text.Trim)
                cmdUser.Parameters.AddWithValue("@username", TxtUsername.Text.Trim)
                cmdUser.Parameters.AddWithValue("@password", hashedPassword) ' ideally hash this
                ' Use DBNull if the fields are empty
                cmdUser.Parameters.AddWithValue("@phone", If(String.IsNullOrWhiteSpace(TxtPhone.Text), DBNull.Value, TxtPhone.Text.Trim))
                cmdUser.Parameters.AddWithValue("@email", If(String.IsNullOrWhiteSpace(TxtEmail.Text), DBNull.Value, TxtEmail.Text.Trim))
                cmdUser.ExecuteNonQuery()

                ' ✅ ADD THIS LOGGING LINE
                SystemSession.LogAudit($"Added new Admin: {TxtName.Text.Trim}", "Admin Maintenance",
                                       SystemSession.LoggedInUserID, SystemSession.LoggedInFullName, SystemSession.LoggedInRole)
            End Using

        End Using

        MessageBox.Show("Admin account added successfully.")
        LoadAdmins()
        ClearAdminInputs()
    End Sub
    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        If selectedAdminID = 0 Then
            MessageBox.Show("Please select an admin to update.")
            Exit Sub
        End If

        ' 1. Validate Email ONLY if it's not empty
        If Not String.IsNullOrWhiteSpace(TxtEmail.Text) Then
            If Not ValidateEmail(selectedAdminID) Then Exit Sub
        End If

        ' 2. Validate Username duplicates (ignoring current ID)
        If IsDuplicateEmailOrUsername(TxtEmail.Text.Trim(), TxtUsername.Text.Trim(), selectedAdminID) Then
            MessageBox.Show("Email or Username already exists for another account.")
            Exit Sub
        End If

        ' 3. If they typed a password, validate it meets requirements
        If Not String.IsNullOrWhiteSpace(TxtPassword.Text) Then
            If Not ValidatePassword() Then Exit Sub
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim query As String

            If String.IsNullOrWhiteSpace(TxtPassword.Text) Then
                query = "UPDATE Users SET FullName=@fn, Username=@un, PhoneNumber=@ph, Email=@em WHERE UserID=@id"
            Else
                query = "UPDATE Users SET FullName=@fn, Username=@un, PhoneNumber=@ph, Email=@em, Password=@pw WHERE UserID=@id"
            End If

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", selectedAdminID)
                cmd.Parameters.AddWithValue("@fn", TxtName.Text.Trim)
                cmd.Parameters.AddWithValue("@un", TxtUsername.Text.Trim)
                ' Allow saving as NULL/Empty
                cmd.Parameters.AddWithValue("@ph", If(String.IsNullOrWhiteSpace(TxtPhone.Text), DBNull.Value, TxtPhone.Text.Trim))
                cmd.Parameters.AddWithValue("@em", If(String.IsNullOrWhiteSpace(TxtEmail.Text), DBNull.Value, TxtEmail.Text.Trim))

                If Not String.IsNullOrWhiteSpace(TxtPassword.Text) Then
                    cmd.Parameters.AddWithValue("@pw", HashPassword(TxtPassword.Text))
                End If

                cmd.ExecuteNonQuery()
                ' ✅ ADD THIS LOGGING LINE
                SystemSession.LogAudit($"Updated Admin details for: {TxtName.Text.Trim}", "Admin Maintenance",
                       SystemSession.LoggedInUserID, SystemSession.LoggedInFullName, SystemSession.LoggedInRole)
            End Using
        End Using

        MessageBox.Show("Admin updated successfully.")

        ' Sync current session if the admin edited their own profile
        If selectedAdminID = SystemSession.LoggedInUserID Then
            SystemSession.LoggedInFullName = TxtName.Text.Trim
        End If

        LoadAdmins()
        ClearAdminInputs()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If selectedAdminID = 0 Then
            MessageBox.Show("Please select an admin to delete.")
            Exit Sub
        End If

        ' Prevent accidental self-deletion without serious warning
        If selectedAdminID = SystemSession.LoggedInUserID Then
            Dim result = MessageBox.Show("You are about to delete your own account. You will be logged out immediately. Proceed?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = DialogResult.No Then Exit Sub
        Else
            Dim result = MessageBox.Show("Are you sure you want to delete this admin?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then Exit Sub
        End If

        Try
            Using con As New SqlConnection(My.Settings.DentalDBConnection2)
                con.Open()
                Using cmd As New SqlCommand("DELETE FROM Users WHERE UserID=@id", con)
                    cmd.Parameters.AddWithValue("@id", selectedAdminID)
                    cmd.ExecuteNonQuery()
                    ' ✅ ADD THIS LOGGING LINE
                    SystemSession.LogAudit($"Deleted Admin account: {TxtName.Text.Trim}", "Admin Maintenance",
                                           SystemSession.LoggedInUserID, SystemSession.LoggedInFullName, SystemSession.LoggedInRole)
                End Using
            End Using

            MessageBox.Show("Admin deleted successfully.")

            ' If you deleted yourself, go back to login
            If selectedAdminID = SystemSession.LoggedInUserID Then
                Login.Show()
                Me.Hide()
            Else
                LoadAdmins()
                ClearAdminInputs()
            End If

        Catch ex As Exception
            MessageBox.Show("Error deleting admin. They might have linked records (Audit logs, etc).")
        End Try
    End Sub
    Private Sub AdminSearch_TextChanged(sender As Object, e As EventArgs) Handles AdminSearch.TextChanged
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim query As String
            If String.IsNullOrWhiteSpace(AdminSearch.Text) Then
                query = "
                SELECT UserID, FullName, Username, PhoneNumber, Email
                FROM Users
                WHERE Role = 'Admin'
                ORDER BY FullName
            "
            Else
                query = "
                SELECT UserID, FullName, Username, PhoneNumber, Email
                FROM Users
                WHERE Role = 'Admin'
                  AND (
                        COALESCE(FullName,'') LIKE @search
                     OR COALESCE(Username,'') LIKE @search
                     OR COALESCE(PhoneNumber,'') LIKE @search
                     OR COALESCE(Email,'') LIKE @search
                  )
                ORDER BY FullName
            "
            End If

            Using cmd As New SqlCommand(query, con)
                If Not String.IsNullOrWhiteSpace(AdminSearch.Text) Then
                    cmd.Parameters.AddWithValue("@search", "%" & AdminSearch.Text.Trim() & "%")
                End If

                Dim adapter As New SqlDataAdapter(cmd)
                Dim table As New DataTable()
                adapter.Fill(table)

                DataGridViewAdmins.DataSource = table
            End Using
        End Using
    End Sub


    '========================================================= VALIDATIONS ======================================================
    Private Function ValidateFullName() As Boolean
        Dim fullName As String = TxtName.Text.Trim()

        If String.IsNullOrWhiteSpace(fullName) Then
            MessageBox.Show("Full Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtName.Focus()
            Return False
        End If

        If fullName.Length < 2 Then
            MessageBox.Show("Full Name must be at least 2 characters long.")
            TxtName.Focus()
            Return False
        End If

        Return True
    End Function
    Private Function ValidateUsername() As Boolean
        Dim username As String = TxtUsername.Text.Trim()

        If String.IsNullOrWhiteSpace(username) Then
            MessageBox.Show("Username is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtUsername.Focus()
            Return False
        End If

        If username.Length < 3 Then
            MessageBox.Show("Username must be at least 3 characters long.")
            TxtUsername.Focus()
            Return False
        End If

        ' Optional: Check for invalid characters (already somewhat handled by KeyPress, but good to double-check)
        If username.Contains(" ") Then
            MessageBox.Show("Username cannot contain spaces.")
            TxtUsername.Focus()
            Return False
        End If

        Return True
    End Function
    Private Function ValidatePassword() As Boolean
        Dim password As String = TxtPassword.Text.Trim()

        ' Check length
        If password.Length < 8 Then
            MessageBox.Show("Password must be at least 8 characters long.")
            TxtPassword.Focus()
            Return False
        End If

        ' Check for at least one uppercase letter
        If Not password.Any(Function(c) Char.IsUpper(c)) Then
            MessageBox.Show("Password must contain at least one uppercase letter.")
            TxtPassword.Focus()
            Return False
        End If

        Return True
    End Function

    Private Function ValidateEmail(Optional userID As Integer = 0) As Boolean
        Dim email As String = TxtEmail.Text.Trim()

        ' If it's empty, it's valid (because it's optional)
        If String.IsNullOrWhiteSpace(email) Then Return True

        ' 1. Flexible Format Check
        Dim emailPattern As String = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
        If Not System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern) Then
            MessageBox.Show("Please enter a valid email address (e.g., name@example.com).")
            TxtEmail.Focus()
            Return False
        End If

        ' 2. "Smart" Duplicate Check
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            ' The "AND UserID <> @id" part is the secret sauce. 
            ' If userID is 0 (New User), it checks everyone. 
            ' If userID is > 0 (Update), it ignores the current person.
            Dim query As String = "SELECT COUNT(*) FROM Users WHERE Email=@em AND UserID <> @id"

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@em", email)
                cmd.Parameters.AddWithValue("@id", userID)

                Dim count As Integer = CInt(cmd.ExecuteScalar())
                If count > 0 Then
                    MessageBox.Show("This email is already registered to another account.")
                    TxtEmail.Focus()
                    Return False
                End If
            End Using
        End Using

        Return True
    End Function


    Private Function IsDuplicateEmailOrUsername(email As String, username As String, Optional userID As Integer = 0) As Boolean
        ' Early exit if username is missing (should never reach here due to validation, but safety)
        If String.IsNullOrWhiteSpace(username) Then
            Return True
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim query As String
            If String.IsNullOrWhiteSpace(email) Then
                query = "SELECT COUNT(*) FROM Users WHERE Username = @un AND UserID <> @id"
            Else
                query = "SELECT COUNT(*) FROM Users WHERE (Email = @em OR Username = @un) AND UserID <> @id"
            End If

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@un", username.Trim())
                cmd.Parameters.AddWithValue("@id", userID)

                If Not String.IsNullOrWhiteSpace(email) Then
                    cmd.Parameters.AddWithValue("@em", email.Trim())
                End If

                Dim count As Integer = CInt(cmd.ExecuteScalar())
                Return count > 0
            End Using
        End Using
    End Function

    Private Sub TxtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtName.KeyPress
        If Char.IsControl(e.KeyChar) Then Return

        ' Allow letters
        If Char.IsLetter(e.KeyChar) Then Return

        ' Allow single space/dot/hyphen, but prevent consecutive or starting with them
        Dim allowedChars As String = " .'-"
        If allowedChars.Contains(e.KeyChar) Then
            Dim lastChar As Char = If(TxtName.Text.Length > 0, TxtName.Text.Last(), ChrW(0))
            If TxtName.Text.Length = 0 OrElse allowedChars.Contains(lastChar) Then
                e.Handled = True
            End If
            Return
        End If

        e.Handled = True
    End Sub

    Private Sub TxtPhone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPhone.KeyPress
        ' Allow backspace
        If Char.IsControl(e.KeyChar) Then Return

        ' Allow digits only and limit to 11 characters
        If Char.IsDigit(e.KeyChar) AndAlso TxtPhone.Text.Length < 11 Then
            Return
        End If

        e.Handled = True
    End Sub

    Private Sub TxtUsername_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtUsername.KeyPress
        If Char.IsControl(e.KeyChar) Then Return

        ' Block spaces
        If e.KeyChar = " "c Then
            e.Handled = True
            Return
        End If

        ' Allow letters and numbers
        If Char.IsLetterOrDigit(e.KeyChar) Then Return

        ' Allow dot or underscore (but not as first char or consecutive)
        Dim allowedSymbols As String = "._"
        If allowedSymbols.Contains(e.KeyChar) Then
            Dim lastChar As Char = If(TxtUsername.Text.Length > 0, TxtUsername.Text.Last(), ChrW(0))
            If TxtUsername.Text.Length = 0 OrElse allowedSymbols.Contains(lastChar) Then
                e.Handled = True
            End If
            Return
        End If

        e.Handled = True
    End Sub

    Private Sub TxtEmail_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtEmail.KeyPress
        If Char.IsControl(e.KeyChar) Then Return

        ' Block spaces
        If e.KeyChar = " "c Then
            e.Handled = True
            Return
        End If

        ' Allow standard email characters
        Dim allowedEmailChars As String = "@._-"
        If Char.IsLetterOrDigit(e.KeyChar) OrElse allowedEmailChars.Contains(e.KeyChar) Then
            Return
        End If

        e.Handled = True
    End Sub
    Private Sub DataGridViewAdmins_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewAdmins.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridViewAdmins.Rows(e.RowIndex)

            ' Store the ID
            selectedAdminID = CInt(row.Cells("UserID").Value)

            ' Load data into textboxes
            TxtName.Text = row.Cells("FullName").Value.ToString()
            TxtUsername.Text = row.Cells("Username").Value.ToString()
            TxtPhone.Text = row.Cells("PhoneNumber").Value.ToString()
            TxtEmail.Text = row.Cells("Email").Value.ToString()

            ' Clear password fields for security (admin must re-type to change)
            TxtPassword.Text = ""

            ' Switch button states
            BTNAdd.Enabled = False
            BtnUpdate.Enabled = True
            BtnDelete.Enabled = True
        End If
    End Sub
    Private selectedAdminID As Integer = 0 ' Add this line at the top

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearAdminInputs()
        AdminSearch.Text = ""
        DataGridViewAdmins.ClearSelection()
        TxtName.Focus()
    End Sub

End Class