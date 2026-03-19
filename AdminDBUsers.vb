Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class AdminDBUsers
    Private selectedUserID As Integer = 0

    Private Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
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

            ' Added WHERE clause to filter out Dentists
            Dim query As String = "
            SELECT UserID, FullName, Username, Role, PhoneNumber, Email, DateCreated
            FROM Users
            WHERE Role <> 'Dentist'
            ORDER BY FullName
        "

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVUsers.DataSource = dt
            DGVUsers.Columns("UserID").Visible = False
        End Using
    End Sub

    Private Sub Clearform()
        selectedUserID = 0
        TxtFullName.Text = ""
        TxtUsername.Text = ""
        txtPassword.Text = ""

        CmbRole.SelectedIndex = -1

        TxtPhoneNumber.Text = ""
        TxtEmail.Text = ""
        txtSearchUsers.Text = ""
        DGVUsers.ClearSelection()
        ' Enable/disable buttons
        BtnAddUser.Enabled = True
        BtnUpdate.Enabled = False
        BtnDelete.Enabled = False
        ' ✅ Moves the focus (blinking cursor) to the first box
        TxtFullName.Focus()
    End Sub

    Private Sub BtnAddUser_Click(sender As Object, e As EventArgs) Handles BtnAddUser.Click
        ' Validation
        If Not ValidateUserFields() Then Exit Sub
        If String.IsNullOrWhiteSpace(TxtFullName.Text) OrElse
       String.IsNullOrWhiteSpace(TxtUsername.Text) OrElse
       String.IsNullOrWhiteSpace(txtPassword.Text) OrElse
       String.IsNullOrWhiteSpace(CmbRole.Text) Then
            MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Only allow Staff/Dentist to be added by logged-in Admin
        If SystemSession.LoggedInRole <> "Admin" AndAlso Not CmbRole.Text.Equals("Admin", StringComparison.OrdinalIgnoreCase) Then
            MessageBox.Show("You must be logged in as an Admin to add Staff or Dentists.",
                    "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            INSERT INTO Users (FullName, Username, Password, Role, PhoneNumber, Email )
            VALUES (@fullname, @username, @password, @role, @phone, @email );
            SELECT SCOPE_IDENTITY()"
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@fullname", TxtFullName.Text)
                cmd.Parameters.AddWithValue("@username", TxtUsername.Text)
                cmd.Parameters.AddWithValue("@password", HashPassword(txtPassword.Text))
                cmd.Parameters.AddWithValue("@role", roleToAssign)
                cmd.Parameters.AddWithValue("@phone", TxtPhoneNumber.Text)
                cmd.Parameters.AddWithValue("@email", TxtEmail.Text)


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
                "Users Maintenance",                  ' Module (consistent with previous naming)
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
        If Not ValidateUserFields(selectedUserID) Then Exit Sub
        ' 1. Security & Selection Checks
        If Not SystemSession.RequireAdmin("update users") Then Exit Sub
        If selectedUserID = 0 Then
            MessageBox.Show("Please select a user to update.")
            Exit Sub
        End If

        If Not ValidateUserFields(selectedUserID) Then Exit Sub

        Try
            Dim changes As String = ""
            Dim targetName As String = TxtFullName.Text.Trim()
            Dim needsAdminWarning As Boolean = False

            Using con As New SqlConnection(My.Settings.DentalDBConnection2)
                con.Open()

                ' --- STEP A: FETCH OLD DATA FOR COMPARISON ---
                Dim oldName As String = "", oldUn As String = "", oldRole As String = "", oldPh As String = "", oldEm As String = ""
                Dim getOldQuery As String = "SELECT FullName, Username, Role, PhoneNumber, Email FROM Users WHERE UserID = @id"
                Using cmdOld As New SqlCommand(getOldQuery, con)
                    cmdOld.Parameters.AddWithValue("@id", selectedUserID)
                    Using dr As SqlDataReader = cmdOld.ExecuteReader()
                        If dr.Read() Then
                            oldName = dr("FullName").ToString()
                            oldUn = dr("Username").ToString()
                            oldRole = dr("Role").ToString()
                            oldPh = dr("PhoneNumber").ToString()
                            oldEm = dr("Email").ToString()
                        End If
                    End Using
                End Using

                ' Warn the user if they are about to demote themselves
                If selectedUserID = SystemSession.LoggedInUserID AndAlso oldRole = "Admin" AndAlso Not CmbRole.Text.Equals("Admin", StringComparison.OrdinalIgnoreCase) Then
                    Dim res = MessageBox.Show("You are about to change your own role from Admin to '" & CmbRole.Text & "'. This will end your session immediately and log you out. Continue?", "Confirm role change", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    If res <> DialogResult.Yes Then
                        Return
                    End If
                End If

                ' --- STEP B: COMPARE OLD VS NEW ---
                If oldName <> TxtFullName.Text.Trim() Then changes &= $"Name: {oldName} -> {TxtFullName.Text.Trim()}; "
                If oldUn <> TxtUsername.Text.Trim() Then changes &= $"Username: {oldUn} -> {TxtUsername.Text.Trim()}; "
                If oldRole <> CmbRole.Text.Trim() Then changes &= $"Role: {oldRole} -> {CmbRole.Text.Trim()}; "
                If oldPh <> TxtPhoneNumber.Text.Trim() Then changes &= $"Phone: {oldPh} -> {TxtPhoneNumber.Text.Trim()}; "
                If oldEm <> TxtEmail.Text.Trim() Then changes &= $"Email: {oldEm} -> {TxtEmail.Text.Trim()}; "

                If Not String.IsNullOrWhiteSpace(txtPassword.Text) Then
                    changes &= "Password was updated; "
                End If

                If String.IsNullOrEmpty(changes) Then
                    MessageBox.Show("No changes detected. Update cancelled.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                ' --- STEP C: EXECUTE UPDATE ---
                Dim query As String
                If String.IsNullOrWhiteSpace(txtPassword.Text) Then
                    query = "UPDATE Users SET FullName=@fullname, Username=@username, Role=@role, PhoneNumber=@phone, Email=@email WHERE UserID=@id"
                Else
                    query = "UPDATE Users SET FullName=@fullname, Username=@username, Password=@password, Role=@role, PhoneNumber=@phone, Email=@email WHERE UserID=@id"
                End If

                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@id", selectedUserID)
                    cmd.Parameters.AddWithValue("@fullname", TxtFullName.Text.Trim())
                    cmd.Parameters.AddWithValue("@username", TxtUsername.Text.Trim())
                    cmd.Parameters.AddWithValue("@role", CmbRole.Text)
                    cmd.Parameters.AddWithValue("@phone", TxtPhoneNumber.Text.Trim())
                    cmd.Parameters.AddWithValue("@email", TxtEmail.Text.Trim())

                    ' Check specifically for the password parameter defined in the query above
                    If Not String.IsNullOrWhiteSpace(txtPassword.Text) Then
                        cmd.Parameters.AddWithValue("@password", HashPassword(txtPassword.Text))
                    End If

                    cmd.ExecuteNonQuery()
                End Using

                ' --- STEP D: DETAILED AUDIT LOGGING ---
                Dim auditMsg As String = If(String.IsNullOrEmpty(changes),
                                        $"Updated user {targetName} (No changes made)",
                                        $"Updated user {targetName}. Changes: {changes}")

                SystemSession.LogAudit(auditMsg, "Users Maintenance",
                                   SystemSession.LoggedInUserID,
                                   SystemSession.LoggedInFullName,
                                   SystemSession.LoggedInRole)

                ' Special check for last Admin (your original logic)
                ' Don't show the warning yet — delay until after self-session enforcement so logout message appears first
                If oldRole = "Admin" AndAlso CmbRole.Text <> "Admin" AndAlso Not SystemSession.AdminExists() Then
                    needsAdminWarning = True
                End If
            End Using

            SystemSession.ShowSuccess("updated")
            SystemSession.EnforceSelfSessionRules(selectedUserID, CmbRole.Text, Me, Login)

            ' After enforcement (logout) or normal flow, optionally log a warning instead of showing a dialog
            If needsAdminWarning AndAlso Not SystemSession.AdminExists() Then
                SystemSession.LogAudit("Last Admin changed - no admins remain", "Users Maintenance")
            End If

            LoadUsers()
            Clearform()

        Catch ex As Exception
            MessageBox.Show("Update Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        ' Only Admin can delete users
        If Not SystemSession.RequireAdmin("delete users") Then Exit Sub

        If selectedUserID = 0 Then
            MessageBox.Show("Please select a user to delete.")
            Exit Sub
        End If

        ' Warn the user if they are about to delete their own account
        If selectedUserID = SystemSession.LoggedInUserID Then
            Dim confirmSelf = MessageBox.Show(
                "You are about to delete your own account. This will log you out immediately. Do you want to continue?",
                "Confirm Self-Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If confirmSelf <> DialogResult.Yes Then
                Exit Sub
            End If
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
            SystemSession.LogAudit("User Deleted", "Users Maintenance", selectedUserID)

            ' If the admin deleted themselves, enforce self-session immediately
            If selectedUserID = SystemSession.LoggedInUserID Then
                ' Do not show extra dialogs that may steal focus — enforce logout and then create admin quietly
                SystemSession.EnforceSelfSessionRules(selectedUserID, Nothing, Me, Login, True)
                ' Refresh users table (may not be reached if the form is closed)
                LoadUsers()
                Clearform()
                Return
            End If

            ' For non-self deletes, show success and warn if no admins remain
            SystemSession.ShowSuccess("deleted")

            ' Check if any Admins remain in the system
            If Not SystemSession.AdminExists() Then
                SystemSession.LogAudit("All Admins Deleted", "Users Maintenance")
                ' Warning removed: do not show blocking dialog here
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
                SystemSession.LogAudit("Delete failed: " & ex.Message, "Users Maintenance")
            End If

        Catch ex As Exception
            ' Catch any unexpected errors
            MessageBox.Show("Unexpected error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            SystemSession.LogAudit("Unexpected delete error: " & ex.Message, "Users Maintenance")
        End Try
    End Sub

    Private Sub DGVUsers_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVUsers.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DGVUsers.Rows(e.RowIndex)
            selectedUserID = CInt(row.Cells("UserID").Value)
            TxtFullName.Text = row.Cells("FullName").Value.ToString()
            TxtUsername.Text = row.Cells("Username").Value.ToString()
            txtPassword.Text = "" ' optional: don’t show password directly

            CmbRole.Text = row.Cells("Role").Value.ToString()
            TxtPhoneNumber.Text = row.Cells("PhoneNumber").Value.ToString()
            TxtEmail.Text = row.Cells("Email").Value.ToString()

            ' Update button states
            BtnAddUser.Enabled = False      ' Disable adding while editing
            BtnUpdate.Enabled = True        ' Enable update
            BtnDelete.Enabled = True        ' Enable delete
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

    Private Sub txtSearchUsers_TextChanged(sender As Object, e As EventArgs) Handles txtSearchUsers.TextChanged
        ' ✅ Wrap the search logic in parentheses and add the Dentist filter
        Dim query As String = "
        SELECT UserID, FullName, Username, Role, PhoneNumber, Email, DateCreated
        FROM dbo.Users
        WHERE Role <> 'Dentist' 
          AND (
               FullName LIKE @search 
               OR Username LIKE @search 
               OR Role LIKE @search 
               OR PhoneNumber LIKE @search 
               OR Email LIKE @search
          )"

        Using con As New SqlConnection(connectionString),
          cmd As New SqlCommand(query, con)

            cmd.Parameters.AddWithValue("@search", "%" & txtSearchUsers.Text.Trim() & "%")

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
        Else
            ' Hide the password
            txtPassword.UseSystemPasswordChar = True
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

        ' --- PHONE NUMBER VALIDATION (optional) ---
        Dim phone As String = TxtPhoneNumber.Text.Trim()
        If Not String.IsNullOrWhiteSpace(phone) Then
            If phone.Length <> 11 OrElse Not phone.All(AddressOf Char.IsDigit) Then
                MessageBox.Show("Phone Number must be exactly 11 digits.")
                TxtPhoneNumber.Focus()
                Return False
            ElseIf Not phone.StartsWith("09") Then
                MessageBox.Show("Invalid Phone Number. Must start with '09'.")
                TxtPhoneNumber.Focus()
                Return False
            End If
        End If
        ' --- EMAIL VALIDATION ---
        Dim email As String = TxtEmail.Text.Trim()

        ' General rules:
        ' - Must have one @ symbol
        ' - Local part: letters, digits, dot, underscore, hyphen (no consecutive dots)
        ' - Domain: letters, digits, hyphen, dot, at least 2 segments (e.g., gmail.com)
        ' - No starting or ending with special chars in local part

        Dim emailRegex As String = "^[A-Za-z0-9]+(?:[._-][A-Za-z0-9]+)*@[A-Za-z0-9-]+(?:\.[A-Za-z]{2,})+$"

        ' --- EMAIL VALIDATION (optional) ---
        If Not String.IsNullOrWhiteSpace(email) Then
            If Not System.Text.RegularExpressions.Regex.IsMatch(email, emailRegex) Then
                MessageBox.Show("Email is invalid. It must be in a proper format, e.g., name.lastname@gmail.com")
                TxtEmail.Focus()
                Return False
            End If

            ' Validate presence and position of '@' then get local part safely
            Dim atIndex As Integer = email.IndexOf("@"c)
            If atIndex <= 0 Then
                MessageBox.Show("Email is invalid. Missing or misplaced '@'.")
                TxtEmail.Focus()
                Return False
            End If

            Dim localPart As String = email.Substring(0, atIndex)
            If String.IsNullOrEmpty(localPart) OrElse Not localPart.All(Function(c) Char.IsLetterOrDigit(c)) Then
                MessageBox.Show("Email username must contain letters or digits only.")
                TxtEmail.Focus()
                Return False
            End If
        End If

        ' --- PASSWORD VALIDATION ---
        Dim password As String = txtPassword.Text.Trim()
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
        End If

        ' --- DUPLICATE CHECK ---
        Dim duplicateUsername As Boolean = False
        Dim duplicateEmail As Boolean = False

        CheckDuplicates(email, username, userID, duplicateUsername, duplicateEmail)

        If duplicateUsername Or duplicateEmail Then
            Dim msg As String = ""

            If duplicateUsername Then
                msg &= "Username already exists." & vbCrLf
            End If

            If duplicateEmail Then
                msg &= "Email already exists." & vbCrLf
            End If

            MessageBox.Show(msg, "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            If duplicateUsername Then
                TxtUsername.Focus()
            ElseIf duplicateEmail Then
                TxtEmail.Focus()
            End If

            Return False
        End If

        Return True
    End Function
    Private Sub CheckDuplicates(email As String, username As String, userID As Integer,
                            ByRef isUsernameDuplicate As Boolean,
                            ByRef isEmailDuplicate As Boolean)

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim query As String = "
        SELECT 
            SUM(CASE WHEN Username = @un THEN 1 ELSE 0 END) AS UsernameCount,
            SUM(CASE WHEN Email = @em THEN 1 ELSE 0 END) AS EmailCount
        FROM Users
        WHERE UserID <> @id"

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@un", username.Trim())
                cmd.Parameters.AddWithValue("@id", userID)

                If String.IsNullOrWhiteSpace(email) Then
                    cmd.Parameters.AddWithValue("@em", DBNull.Value)
                Else
                    cmd.Parameters.AddWithValue("@em", email.Trim())
                End If

                Using dr As SqlDataReader = cmd.ExecuteReader()
                    If dr.Read() Then
                        isUsernameDuplicate = Convert.ToInt32(dr("UsernameCount")) > 0
                        isEmailDuplicate = Convert.ToInt32(dr("EmailCount")) > 0
                    End If
                End Using
            End Using
        End Using
    End Sub
    
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

    ' --- TEXTBOX INPUT VALIDATION (Login Form) ---
    Private Sub TxtUsername_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtUsername.KeyPress
        ' 1. Always allow control keys (Backspace, etc.)
        If Char.IsControl(e.KeyChar) Then Return

        Dim allowedSpecialChars As String = "._"
        Dim currentText As String = TxtUsername.Text
        Dim lastChar As Char = If(currentText.Length > 0, currentText(currentText.Length - 1), ChrW(0))

        ' 2. Allow Letters and Digits
        If Char.IsLetterOrDigit(e.KeyChar) Then
            ' Logic check: The very first character MUST be a letter (based on your Regex)
            If currentText.Length = 0 AndAlso Not Char.IsLetter(e.KeyChar) Then
                e.Handled = True
            End If
            Return
        End If

        ' 3. Allow Dot or Underscore
        If allowedSpecialChars.Contains(e.KeyChar) Then
            ' Rule: Cannot start with a special char
            If currentText.Length = 0 Then
                e.Handled = True
                ' Rule: No consecutive special characters (e.g., ".." or "__")
            ElseIf lastChar = e.KeyChar Then
                e.Handled = True
            End If
            Return
        End If

        ' 4. Block everything else (spaces, symbols, etc.)
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
    Private Sub txtSpecialization_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Char.IsControl(e.KeyChar) Then Return
        If Not (Char.IsLetter(e.KeyChar) OrElse e.KeyChar = " "c) Then
            e.Handled = True
        End If
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        'reset input fields and selected user ID
        Clearform()
    End Sub
End Class