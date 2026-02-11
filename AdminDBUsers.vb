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
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
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

        ' Determine if this is the first admin BEFORE inserting
        Dim isFirstAdmin As Boolean = Not SystemSession.AdminExists()
        Dim roleToAssign As String = If(isFirstAdmin, "Admin", CmbRole.Text)

        Dim newUserID As Integer

        ' Insert into database and get the new UserID
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
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
            SystemSession.LogAudit("Add user", "User Management", newUserID, TxtFullName.Text, roleToAssign)
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

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
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
        If oldRole = "Admin" AndAlso CmbRole.Text <> "Admin" AndAlso Not SystemSession.AdminExists() Then
            SystemSession.LogAudit("Last Admin Role Changed", "User Management",
            selectedUserID, TxtFullName.Text, oldRole)
            MessageBox.Show("The last Admin account has been changed. Register a new Admin immediately.",
                    "System Setup", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        ElseIf CmbRole.Text = "Admin" Then
            SystemSession.LogAudit("Admin Account Updated", "User Management",
            selectedUserID, TxtFullName.Text, CmbRole.Text)
        Else
            SystemSession.LogAudit("User Updated", "User Management",
            selectedUserID, TxtFullName.Text, CmbRole.Text)
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
            Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
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
                    Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
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
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()
            Dim query As String = "SELECT COUNT(*) FROM Users WHERE Username = @username"
            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@username", username)
            Dim count As Integer = CInt(cmd.ExecuteScalar())
            Return count > 0
        End Using
    End Function

    Private Sub DGVUsers_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVUsers.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DGVUsers.Rows(e.RowIndex)
            selectedUserID = CInt(row.Cells("UserID").Value)
            TxtFullName.Text = row.Cells("FullName").Value.ToString()
            TxtUsername.Text = row.Cells("Username").Value.ToString()
            txtPassword.Text = "" ' optional: don’t show password directly
            txtSpecialization.Text = row.Cells("Specialization").Value.ToString()
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

End Class