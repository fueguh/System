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

        ' Call the centralized enforcement logics
        SystemSession.EnforceAdminRole(CmbRole)
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

        If String.IsNullOrWhiteSpace(TxtFullName.Text) OrElse
       String.IsNullOrWhiteSpace(TxtUsername.Text) OrElse
       String.IsNullOrWhiteSpace(txtPassword.Text) OrElse
       String.IsNullOrWhiteSpace(txtConfirmPassword.Text) OrElse
       String.IsNullOrWhiteSpace(CmbRole.Text) Then

            MessageBox.Show("Please fill in all required fields before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If


        ' Confirm password check
        If txtPassword.Text <> txtConfirmPassword.Text Then
            MessageBox.Show("Passwords do not match.")
            Exit Sub
        End If

        If IsUsernameTaken(TxtUsername.Text) Then
            MessageBox.Show("Username already exists. Please choose a different one.")
            Exit Sub
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "
            INSERT INTO Users (FullName, Username, Password, Role, PhoneNum, Email, Specialization, Availability)
            VALUES (@fullname, @username, @password, @role, @phone, @email, @specialization, @availability)
        "

            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@fullname", TxtFullName.Text)
            cmd.Parameters.AddWithValue("@username", TxtUsername.Text)

            Dim hashedPassword As String = HashPassword(txtPassword.Text)
            cmd.Parameters.AddWithValue("@password", hashedPassword)

            ' Check if there is at least one Admin in the system
            Dim roleToAssign As String
            If Not SystemSession.AdminExists() Then
                roleToAssign = "Admin"
                MessageBox.Show("An Admin account must be created before adding Staff or Dentist. This user will be set as Admin.", "System Setup", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                roleToAssign = CmbRole.Text

            End If

            cmd.Parameters.AddWithValue("@role", roleToAssign)
            cmd.Parameters.AddWithValue("@phone", TxtPhoneNumber.Text)
            cmd.Parameters.AddWithValue("@email", TxtEmail.Text)
            cmd.Parameters.AddWithValue("@specialization", txtSpecialization.Text)
            cmd.Parameters.AddWithValue("@availability", cmbAvailability.Text)

            cmd.ExecuteNonQuery()

            If Not SystemSession.AdminExists() Then
                SystemSession.LogAudit("First Admin Created", "Registration")
            Else
                SystemSession.LogAudit("Add user", "User Management")
            End If

        End Using

        MessageBox.Show("User added successfully.")
        LoadUsers()
        Clearform()
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        If selectedUserID = 0 Then
            MessageBox.Show("Please select a user to update.")
            Exit Sub
        End If

        ' Get old role using SystemSession helper
        Dim oldRole As String = SystemSession.GetUserRole(selectedUserID)

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

            Dim hashedPassword As String = HashPassword(txtPassword.Text)

            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@id", selectedUserID)
            cmd.Parameters.AddWithValue("@fullname", TxtFullName.Text)
            cmd.Parameters.AddWithValue("@username", TxtUsername.Text)
            cmd.Parameters.AddWithValue("@role", CmbRole.Text)
            cmd.Parameters.AddWithValue("@specialization", txtSpecialization.Text)
            cmd.Parameters.AddWithValue("@availability", cmbAvailability.Text)
            cmd.Parameters.AddWithValue("@phone", TxtPhoneNumber.Text)
            cmd.Parameters.AddWithValue("@email", TxtEmail.Text)
            cmd.Parameters.AddWithValue("@password", hashedPassword)

            cmd.ExecuteNonQuery()
        End Using

        MessageBox.Show("User updated successfully.")
        ' ✅ Audit logging with last Admin check
        If oldRole = "Admin" AndAlso CmbRole.Text <> "Admin" AndAlso Not SystemSession.AdminExists() Then
            SystemSession.LogAudit("Last Admin Role Changed", "User Management")
            MessageBox.Show("The last Admin account has been changed. Please register a new Admin immediately.", "System Setup", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            SystemSession.EnforceAdminRole(CmbRole)
        ElseIf CmbRole.Text = "Admin" Then
            SystemSession.LogAudit("Admin Account Updated", "User Management")
        Else
            SystemSession.LogAudit("User Updated", "User Management")
        End If

        LoadUsers()
        Clearform()
    End Sub


    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If selectedUserID = 0 Then
            MessageBox.Show("Please select a user to delete.")
            Exit Sub
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "DELETE FROM Users WHERE UserID=@id"
            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@id", selectedUserID)
            cmd.ExecuteNonQuery()
        End Using

        If Not SystemSession.AdminExists() Then
            MessageBox.Show("All Admin accounts have been deleted. You must register a new Admin before adding Staff or Dentists.", "System Setup", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            ' Log this critical event
            SystemSession.LogAudit("All Admins Deleted", "User Management")

            ' Optionally lock the ComboBox again
            SystemSession.EnforceAdminRole(CmbRole)
        End If


        MessageBox.Show("User deleted successfully.")
        ' Re-check if Admins still exist
        SystemSession.EnforceAdminRole(CmbRole)

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
        If Dashboard Is Nothing Then
            Dashboard = New AdminDashboard()
        End If

        Dashboard.Show()
        Me.Close()
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

    Private Sub TxtPhoneNumber_TextChanged(sender As Object, e As EventArgs) Handles TxtPhoneNumber.TextChanged

    End Sub
End Class