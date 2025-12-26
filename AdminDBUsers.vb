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
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "
            SELECT UserID, FullName, Username, Role, Specialization, Availability, PhoneNum, Email, Password, DateCreated
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
        ' Restrict creation of Staff/Dentist if no Admin exists yet
        If Not SystemSession.AdminExists() AndAlso Not CmbRole.Text.Equals("Admin", StringComparison.OrdinalIgnoreCase) Then
            MessageBox.Show("You must create an Admin account first before adding Staff or Dentists.",
                        "Restriction", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' Determine if this is the first admin BEFORE inserting
        Dim isFirstAdmin As Boolean = Not SystemSession.AdminExists()
        Dim roleToAssign As String = If(isFirstAdmin, "Admin", CmbRole.Text)

        ' Insert into database
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim query As String = "
            INSERT INTO Users (FullName, Username, Password, Role, PhoneNum, Email, Specialization, Availability)
            VALUES (@fullname, @username, @password, @role, @phone, @email, @specialization, @availability)"
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@fullname", TxtFullName.Text)
                cmd.Parameters.AddWithValue("@username", TxtUsername.Text)
                cmd.Parameters.AddWithValue("@password", HashPassword(txtPassword.Text))
                cmd.Parameters.AddWithValue("@role", roleToAssign)
                cmd.Parameters.AddWithValue("@phone", TxtPhoneNumber.Text)
                cmd.Parameters.AddWithValue("@email", TxtEmail.Text)
                cmd.Parameters.AddWithValue("@specialization", txtSpecialization.Text)
                cmd.Parameters.AddWithValue("@availability", cmbAvailability.Text)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        ' Audit logging
        If isFirstAdmin Then
            SystemSession.LogAudit("First Admin Created", "Registration")
        Else
            SystemSession.LogAudit("Add user", "User Management")
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

        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "
            UPDATE Users
            SET FullName=@fullname,
                Username=@username,
                Password=@password,
                Role=@role,
                Specialization=@specialization,
                Availability=@availability,
                PhoneNum=@phone,
                Email=@email
            WHERE UserID=@id"

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
        ' Prevent deletion unless an Admin is logged in
        If SystemSession.LoggedInUserID = 0 OrElse SystemSession.LoggedInRole <> "Admin" Then
            MessageBox.Show("Only an Admin can delete users. Please log in as an Admin.",
                        "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If selectedUserID = 0 Then
            MessageBox.Show("Please select a user to delete.")
            Exit Sub
        End If

        ' Delete the user
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim query As String = "DELETE FROM Users WHERE UserID=@id"
            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@id", selectedUserID)
            cmd.ExecuteNonQuery()
        End Using

        ' Check if Admins still exist
        If Not SystemSession.AdminExists() Then
            SystemSession.LogAudit("All Admins Deleted", "User Management")
        Else
            SystemSession.ShowSuccess("deleted")
            SystemSession.LogAudit("User Deleted", "User Management")
        End If
        ' Self-session enforcement
        SystemSession.EnforceSelfSessionRules(selectedUserID, Nothing, Me, Login)
        LoadUsers()
        Clearform()
    End Sub



    Private Function IsUsernameTaken(username As String) As Boolean
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
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
            CmbRole.Text = row.Cells("Role").Value.ToString()
            TxtPhoneNumber.Text = row.Cells("PhoneNum").Value.ToString()
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

    Private Sub CmbRole_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbRole.SelectedIndexChanged
        If CmbRole.Text = "Dentist" Then
            txtSpecialization.Enabled = True
            txtSpecialization.Visible = True
        Else
            txtSpecialization.Enabled = False
            txtSpecialization.Visible = False
            txtSpecialization.Text = ""
        End If
    End Sub

End Class