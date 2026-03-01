Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class AdminDBUsers
    Private selectedUserID As Integer = 0

    Private Function HashPassword(password As String) As String
        Using sha256 As SHA256 = sha256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
            Dim hash As Byte() = sha256.ComputeHash(bytes)
            Return BitConverter.ToString(hash).Replace("-", "").ToLower()
        End Using
    End Function

    Private Sub AdminDBUsers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadUsers()
        Clearform()

    End Sub

    Private Sub LoadUsers()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim query As String = "
            SELECT UserID, FullName, Username, Role, Specialization, Availability, PhoneNumber, Email, Password, DateCreated
            FROM Users
        "

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVUsers.DataSource = dt
        End Using
    End Sub

    Private Sub Clearform()
        selectedUserID = 0
        TxtFullName.Text = ""
        TxtUsername.Text = ""
        txtPassword.Text = ""
        txtConfirmPassword.Text = ""
        CmbRole.SelectedIndex = -1
        txtSpecialization.Text = ""
        cmbAvailability.SelectedIndex = -1
        TxtPhoneNumber.Text = ""
        TxtEmail.Text = ""
    End Sub

    Private Sub BtnAddUser_Click(sender As Object, e As EventArgs) Handles BtnAddUser.Click
        ' Validation
        If String.IsNullOrWhiteSpace(TxtFullName.Text) OrElse
       String.IsNullOrWhiteSpace(TxtUsername.Text) OrElse
       String.IsNullOrWhiteSpace(txtPassword.Text) OrElse
       String.IsNullOrWhiteSpace(txtConfirmPassword.Text) OrElse
       String.IsNullOrWhiteSpace(CmbRole.Text) Then
            MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If txtPassword.Text <> txtConfirmPassword.Text Then
            MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If IsUsernameTaken(TxtUsername.Text) Then
            MessageBox.Show("Username already exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Only allow Staff/Dentist to be added by logged-in Admin
        If SystemSession.LoggedInRole <> "Admin" AndAlso Not CmbRole.Text.Equals("Admin", StringComparison.OrdinalIgnoreCase) Then
            MessageBox.Show("You must be logged in as an Admin to add Staff or Dentists.",
                    "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Not ValidateUserFields() Then Exit Sub

        If IsDuplicateEmailOrUsername(TxtEmail.Text.Trim(), TxtUsername.Text.Trim()) Then
            MessageBox.Show("Email or Username already exists. Please choose another.")
            Exit Sub
        End If

        ' Determine if this is the first admin BEFORE inserting
        Dim isFirstAdmin As Boolean = Not SystemSession.AdminExists()
        Dim roleToAssign As String = If(isFirstAdmin, "Admin", CmbRole.Text)

        Dim newUserID As Integer

        ' Insert into database and get the new UserID
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim query As String = "
            INSERT INTO Users (FullName, Username, Password, Role, PhoneNumber, Email, Specialization, Availability)
            VALUES (@fullname, @username, @password, @role, @phone, @email, @specialization, @availability);
            SELECT SCOPE_IDENTITY()"
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@fullname", TxtFullName.Text)
                cmd.Parameters.AddWithValue("@username", TxtUsername.Text)
                cmd.Parameters.AddWithValue("@password", HashPassword(txtPassword.Text))
                cmd.Parameters.AddWithValue("@role", roleToAssign)
                cmd.Parameters.AddWithValue("@phone", TxtPhoneNumber.Text)
                cmd.Parameters.AddWithValue("@email", TxtEmail.Text)
                cmd.Parameters.AddWithValue("@specialization", txtSpecialization.Text)
                cmd.Parameters.AddWithValue("@availability", cmbAvailability.Text)
                ' Get the inserted UserID
                newUserID = Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using

        ' Audit logging with actual UserID
        If isFirstAdmin Then
            SystemSession.LogAudit("First Admin Created", "Registration", newUserID, TxtFullName.Text, roleToAssign)
        Else
            ' Updated
            SystemSession.LogAudit(
                $"Added user {TxtFullName.Text}",   ' Message now mentions the new user
                "User Management",                  ' Module
                SystemSession.LoggedInUserID,      ' Your admin UserID
                SystemSession.LoggedInFullName,    ' Your admin name
                SystemSession.LoggedInRole          ' Your admin role
            )

        End If

        SystemSession.ShowSuccess("added")
        LoadUsers()
        Clearform()
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        ' ✅ Only Admin can update users
        If Not SystemSession.RequireAdmin("update users") Then Exit Sub
        If Not SystemSession.RequireSelectedUser(selectedUserID, "update") Then Exit Sub
        ' Get old role
        Dim oldRole As String = SystemSession.GetUserRole(selectedUserID)
        Dim hashedPassword As String = HashPassword(txtPassword.Text)

        If Not ValidateUserFields(selectedUserID) Then Exit Sub

        If IsDuplicateEmailOrUsername(TxtEmail.Text.Trim(), TxtUsername.Text.Trim(), selectedUserID) Then
            MessageBox.Show("Email or Username already exists. Please choose another.")
            Exit Sub
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            ' Decide query depending on whether password is entered, to avoid updating password with blank.
            Dim query As String
            If String.IsNullOrWhiteSpace(txtPassword.Text) Then
                ' Password not changed
                query = "
            UPDATE Users
            SET FullName=@fullname,
                Username=@username,
                Role=@role,
                Specialization=@specialization,
                Availability=@availability,
                PhoneNumber=@phone,
                Email=@email
            WHERE UserID=@id"
            Else
                ' Password changed
                query = "
            UPDATE Users
            SET FullName=@fullname,
                Username=@username,
                Password=@password,
                Role=@role,
                Specialization=@specialization,
                Availability=@availability,
                PhoneNumber=@phone,
                Email=@email
            WHERE UserID=@id"
            End If

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", selectedUserID)
                cmd.Parameters.AddWithValue("@fullname", TxtFullName.Text)
                cmd.Parameters.AddWithValue("@username", TxtUsername.Text)
                cmd.Parameters.AddWithValue("@password", hashedPassword)
                cmd.Parameters.AddWithValue("@role", CmbRole.Text)
                cmd.Parameters.AddWithValue("@specialization", txtSpecialization.Text)
                cmd.Parameters.AddWithValue("@availability", cmbAvailability.Text)
                cmd.Parameters.AddWithValue("@phone", TxtPhoneNumber.Text)
                cmd.Parameters.AddWithValue("@email", TxtEmail.Text)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        SystemSession.ShowSuccess("updated")

        ' Audit logging
        Dim actorID As Integer = SystemSession.LoggedInUserID
        Dim actorName As String = SystemSession.LoggedInFullName
        Dim actorRole As String = SystemSession.LoggedInRole
        Dim targetName As String = TxtFullName.Text
        Dim targetRole As String = CmbRole.Text

        If oldRole = "Admin" AndAlso targetRole <> "Admin" AndAlso Not SystemSession.AdminExists() Then
            SystemSession.LogAudit($"Changed last Admin account: {targetName}", "User Management",
        actorID, actorName, actorRole)
            MessageBox.Show("The last Admin account has been changed. Register a new Admin immediately.",
                    "System Setup", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        ElseIf targetRole = "Admin" Then
            SystemSession.LogAudit($"Updated Admin account: {targetName}", "User Management",
        actorID, actorName, actorRole)
        Else
            SystemSession.LogAudit($"Updated user: {targetName}", "User Management",
        actorID, actorName, actorRole)
        End If


        ' Enforce session rules if the logged-in user updates their own role
        SystemSession.EnforceSelfSessionRules(selectedUserID, CmbRole.Text, Me, Login)
        LoadUsers()
        Clearform()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        ' Only Admin can delete users
        If Not SystemSession.RequireAdmin("delete users") Then Exit Sub

        If selectedUserID = 0 Then
            MessageBox.Show("Please select a user to delete.")
            Exit Sub
        End If

        Try
            Using con As New SqlConnection(My.Settings.DentalDBConnection2)
                con.Open()

                ' First, remove any active sessions for this user to avoid locked sessions
                Using cmd As New SqlCommand("DELETE FROM UserSessions WHERE UserID=@id", con)
                    cmd.Parameters.AddWithValue("@id", selectedUserID)
                    cmd.ExecuteNonQuery()
                End Using

                ' Then delete the user
                Using cmd As New SqlCommand("DELETE FROM Users WHERE UserID=@id", con)
                    cmd.Parameters.AddWithValue("@id", selectedUserID)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ' Always log the deletion BEFORE enforcing self-session
            SystemSession.LogAudit("User Deleted", "User Management", selectedUserID)

            ' Show success message
            SystemSession.ShowSuccess("deleted")

            ' Check if any Admins remain in the system
            If Not SystemSession.AdminExists() Then
                SystemSession.LogAudit("All Admins Deleted", "User Management")
                MessageBox.Show("Warning: No Admin accounts remain! Create a new Admin immediately.", "System Setup", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

            ' Self-session enforcement (logs out if deleted yourself)
            SystemSession.EnforceSelfSessionRules(selectedUserID, Nothing, Me, Login)

            ' Refresh users table
            LoadUsers()
            Clearform()

        Catch ex As SqlException
            ' Handle foreign key violations (linked data like appointments)
            If ex.Number = 547 Then
                Dim tableName As String = "unknown table"
                Dim match = System.Text.RegularExpressions.Regex.Match(ex.Message, "constraint ""?(\w+)""?")
                If match.Success Then
                    Dim fkName = match.Groups(1).Value
                    ' Look up parent table
                    Using con As New SqlConnection(My.Settings.DentalDBConnection2)
                        con.Open()
                        Using cmd As New SqlCommand("SELECT OBJECT_NAME(parent_object_id) FROM sys.foreign_keys WHERE name=@fkName", con)
                            cmd.Parameters.AddWithValue("@fkName", fkName)
                            Dim obj = cmd.ExecuteScalar()
                            If obj IsNot Nothing Then tableName = obj.ToString()
                        End Using
                    End Using
                End If

                MessageBox.Show($"Cannot delete this user because they have linked data in table: {tableName}.", "Delete Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                SystemSession.LogAudit($"Delete blocked due to linked data in {tableName}", "User Management")

            Else
                MessageBox.Show("Database error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                SystemSession.LogAudit("Delete failed: " & ex.Message, "User Management")
            End If

        Catch ex As Exception
            ' Catch any unexpected errors
            MessageBox.Show("Unexpected error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            SystemSession.LogAudit("Unexpected delete error: " & ex.Message, "User Management")
        End Try
    End Sub

    Private Function IsUsernameTaken(username As String) As Boolean
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim query As String = "SELECT COUNT(*) FROM Users WHERE Username = @username"
            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@username", username)
            Dim count As Integer = CInt(cmd.ExecuteScalar())
            Return count > 0
        End Using
    End Function
    Private Sub CmbRole_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbRole.SelectedIndexChanged

        If CmbRole.Text.Equals("Dentist", StringComparison.OrdinalIgnoreCase) Then
            ' Enable dentist-only fields
            cmbAvailability.Enabled = True
            txtSpecialization.Enabled = True
        Else
            ' Disable dentist-only fields
            cmbAvailability.Enabled = False
            cmbAvailability.SelectedIndex = -1

            txtSpecialization.Enabled = False
            txtSpecialization.Clear()
        End If

    End Sub


    Private Sub DGVUsers_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVUsers.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DGVUsers.Rows(e.RowIndex)
            selectedUserID = CInt(row.Cells("UserID").Value)
            TxtFullName.Text = row.Cells("FullName").Value.ToString()
            TxtUsername.Text = row.Cells("Username").Value.ToString()
            txtPassword.Text = "" ' optional: don’t show password directly
            txtSpecialization.Text = row.Cells("Specialization").Value.ToString()
            cmbAvailability.Text = row.Cells("Availability").Value.ToString()
            CmbRole.Text = row.Cells("Role").Value.ToString()
            TxtPhoneNumber.Text = row.Cells("PhoneNumber").Value.ToString()
            TxtEmail.Text = row.Cells("Email").Value.ToString()
        End If
    End Sub

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

    Dim connectionString As String = My.Settings.DentalDBConnection2

    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Guna2TextBox1.TextChanged
        Dim query As String = "SELECT UserID, FullName, Username, Password, Role, PhoneNumber, Email, DateCreated, Specialization, Availability
                               FROM dbo.Users
                               WHERE FullName LIKE @search OR Username LIKE @search OR Role LIKE @search OR PhoneNumber LIKE @search OR Email LIKE @search OR Specialization LIKE @search OR Availability LIKE @search"
        Using con As New SqlConnection(connectionString),
              cmd As New SqlCommand(query, con)

            cmd.Parameters.AddWithValue("@search", "%" & Guna2TextBox1.Text & "%")

            Dim adapter As New SqlDataAdapter(cmd)
            Dim table As New DataTable()
            adapter.Fill(table)

            DGVUsers.DataSource = table
        End Using
    End Sub

    Private Sub ChkShowPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPassword.CheckedChanged
        If chkShowPassword.Checked Then
            ' Show the password
            txtPassword.UseSystemPasswordChar = False
            txtConfirmPassword.UseSystemPasswordChar = False
        Else
            ' Hide the password
            txtPassword.UseSystemPasswordChar = True
            txtConfirmPassword.UseSystemPasswordChar = True
        End If
    End Sub

    Private Function ValidateUserFields(Optional userID As Integer = 0) As Boolean
        ' --- FULL NAME VALIDATION ---
        ' Trim leading/trailing spaces
        Dim fullName As String = TxtFullName.Text.Trim()

        ' Regex explanation:
        ' ^[A-Za-zÀ-ÖØ-öø-ÿ]+                  -> First segment must start with a letter
        ' (?:                                  -> Start of non-capturing group for additional segments
        '   [ .'-]                             -> Single space, dot, hyphen, or apostrophe
        '   (?:[A-Za-zÀ-ÖØ-öø-ÿ]+|[A-Za-z]\.) -> Either a normal word or a single-letter initial with dot
        ' )*                                   -> Zero or more additional segments
        ' $                                    -> End of string
        If String.IsNullOrWhiteSpace(fullName) OrElse
   Not System.Text.RegularExpressions.Regex.IsMatch(fullName, "^[A-Za-zÀ-ÖØ-öø-ÿ]+(?:[ .'-](?:[A-Za-zÀ-ÖØ-öø-ÿ]+|[A-Za-z]\.))*$") Then
            MessageBox.Show("Full Name must start and end with a letter. You can use single-letter initials with a dot, spaces, hyphens, or apostrophes. No consecutive special chars.")
            TxtFullName.Focus()
            Return False
        End If

        ' --- USERNAME VALIDATION ---
        ' Trim leading/trailing spaces
        Dim username As String = TxtUsername.Text.Trim()
        ' Regex explanation:
        ' ^[A-Za-z]                   -> Must start with a letter
        ' (?!.*[._]{2})               -> No consecutive dot/underscore
        ' [A-Za-z0-9._]{2,19}         -> Allow letters, digits, dot, underscore (total 3-20 chars)
        ' $                            -> End of string
        If String.IsNullOrWhiteSpace(username) OrElse
   Not System.Text.RegularExpressions.Regex.IsMatch(username, "^[A-Za-z](?!.*[._]{2})[A-Za-z0-9._]{2,19}$") Then
            MessageBox.Show("Username must start with a letter, 3-20 chars, letters/digits/dot/underscore only, no consecutive dots/underscores, cannot end with dot/underscore.")
            TxtUsername.Focus()
            Return False
        End If

        ' --- PHONE NUMBER VALIDATION ---
        If String.IsNullOrWhiteSpace(TxtPhoneNumber.Text) OrElse
       Not TxtPhoneNumber.Text.All(Function(c) Char.IsDigit(c)) Then
            MessageBox.Show("Phone Number must contain digits only.")
            TxtPhoneNumber.Focus()
            Return False
        End If

        ' --- EMAIL VALIDATION ---
        Dim email As String = TxtEmail.Text.Trim()

        ' General rules:
        ' - Must have one @ symbol
        ' - Local part: letters, digits, dot, underscore, hyphen (no consecutive dots)
        ' - Domain: letters, digits, hyphen, dot, at least 2 segments (e.g., gmail.com)
        ' - No starting or ending with special chars in local part

        Dim emailRegex As String = "^[A-Za-z0-9]+(?:[._-][A-Za-z0-9]+)*@[A-Za-z0-9-]+(?:\.[A-Za-z]{2,})+$"

        If String.IsNullOrWhiteSpace(email) OrElse
   Not System.Text.RegularExpressions.Regex.IsMatch(email, emailRegex) Then
            MessageBox.Show("Email is invalid. It must be in a proper format, e.g., name.lastname@gmail.com")
            TxtEmail.Focus()
            Return False
        End If
        Dim localPart As String = email.Substring(0, email.Length - 10)
        If Not localPart.All(Function(c) Char.IsLetterOrDigit(c)) Then
            MessageBox.Show("Email username must contain letters or digits only.")
            TxtEmail.Focus()
            Return False
        End If

        ' --- PASSWORD VALIDATION ---
        Dim password As String = txtPassword.Text.Trim()
        Dim confirmPassword As String = txtConfirmPassword.Text.Trim()
        If password <> "" Then
            If password.Length < 8 Then
                MessageBox.Show("Password must be at least 8 characters long.")
                txtPassword.Focus()
                Return False
            End If
            If Not password.Any(Function(c) Char.IsUpper(c)) Then
                MessageBox.Show("Password must contain at least one uppercase letter.")
                txtPassword.Focus()
                Return False
            End If
            If password <> confirmPassword Then
                MessageBox.Show("Passwords do not match.")
                txtConfirmPassword.Focus()
                Return False
            End If
        End If

        ' --- DUPLICATE CHECK ---
        If IsDuplicateEmailOrUsername(email, username, userID) Then
            MessageBox.Show("Email or Username already exists. Please choose another.")
            Return False
        End If

        Return True
    End Function
    Private Function IsDuplicateEmailOrUsername(email As String, username As String, Optional userID As Integer = 0) As Boolean
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            ' Query checks if email OR username already exists, excluding the current record if updating
            Dim query As String = "
            SELECT COUNT(*) 
            FROM Users 
            WHERE (Email = @em OR Username = @un) 
              AND UserID <> @id
        "

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@em", email)
                cmd.Parameters.AddWithValue("@un", username)
                cmd.Parameters.AddWithValue("@id", userID)

                Dim count As Integer = CInt(cmd.ExecuteScalar())
                Return count > 0
            End Using
        End Using
    End Function


    Private Sub TxtFullName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtFullName.KeyPress
        If Char.IsControl(e.KeyChar) Then Return ' Allow backspace, delete

        Dim allowedChars As String = " .'-" ' space, dot, hyphen, apostrophe
        Dim lastChar As Char = If(TxtFullName.Text.Length > 0, TxtFullName.Text(TxtFullName.Text.Length - 1), ChrW(0))

        ' Allow letters
        If Char.IsLetter(e.KeyChar) Then Return

        ' Allow special chars but prevent consecutive ones
        If allowedChars.Contains(e.KeyChar) Then
            If lastChar = e.KeyChar Then
                e.Handled = True ' Block consecutive special chars
            ElseIf TxtFullName.Text.Length = 0 AndAlso e.KeyChar = " "c Then
                e.Handled = True ' Cannot start with space
            End If
            Return
        End If

        ' Block everything else
        e.Handled = True
    End Sub

    Private Sub TxtUsername_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtUsername.KeyPress
        If Char.IsControl(e.KeyChar) Then Return ' Allow backspace

        Dim allowedChars As String = "._" ' dot, underscore
        Dim lastChar As Char = If(TxtUsername.Text.Length > 0, TxtUsername.Text(TxtUsername.Text.Length - 1), ChrW(0))

        ' Allow letters and digits
        If Char.IsLetterOrDigit(e.KeyChar) Then Return

        ' Allow dot or underscore but prevent consecutive ones or starting char
        If allowedChars.Contains(e.KeyChar) Then
            If TxtUsername.Text.Length = 0 Then
                e.Handled = True ' Cannot start with dot/underscore
            ElseIf lastChar = e.KeyChar Then
                e.Handled = True ' No consecutive dot/underscore
            End If
            Return
        End If

        ' Block everything else
        e.Handled = True
    End Sub

    Private Sub TxtPhoneNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPhoneNumber.KeyPress
        If Char.IsControl(e.KeyChar) Then Return
        If Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TxtEmail_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtEmail.KeyPress
        If Char.IsControl(e.KeyChar) Then Return
        If Not (Char.IsLetterOrDigit(e.KeyChar) OrElse e.KeyChar = "@"c OrElse e.KeyChar = "."c) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtSpecialization_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSpecialization.KeyPress
        If Char.IsControl(e.KeyChar) Then Return
        If Not (Char.IsLetter(e.KeyChar) OrElse e.KeyChar = " "c) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtConfirmPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtConfirmPassword.KeyPress

    End Sub
    Private Sub txtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassword.KeyPress

    End Sub

    Private Sub TxtUsername_TextChanged(sender As Object, e As EventArgs) Handles TxtUsername.TextChanged

    End Sub
End Class