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
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
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
        ' Confirm password check
        If txtPassword.Text <> txtConfirmPassword.Text Then
            MessageBox.Show("Passwords do not match.")
            Exit Sub
        End If

        Dim hashedPassword As String = HashPassword(txtPassword.Text)

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String = "
            INSERT INTO Users (FullName, Username, Password, Role, Specialization, Availability, PhoneNumber, Email)
            VALUES (@fullname, @username, @password, @role, @specialization, @availability, @phone, @email)"

            Dim cmd As New SqlCommand(query, con)
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

        MessageBox.Show("User added successfully.")
        LoadUsers()
        Clearform()
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        If selectedUserID = 0 Then
            MessageBox.Show("Please select a user to update.")
            Exit Sub
        End If

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String = "
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
        LoadUsers()
        Clearform()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If selectedUserID = 0 Then
            MessageBox.Show("Please select a user to delete.")
            Exit Sub
        End If

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String = "DELETE FROM Users WHERE UserID=@id"
            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@id", selectedUserID)
            cmd.ExecuteNonQuery()
        End Using

        MessageBox.Show("User deleted successfully.")
        LoadUsers()
        Clearform()
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
        End If
    End Sub

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        If Dashboard Is Nothing Then
            Dashboard = New AdminDashboard()
        End If

        Dashboard.Show()
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