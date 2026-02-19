Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class AdminDBStaffMaintenance

    Private Sub Guna2CirclePictureBox1_Click_1(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
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
    Public Function HashPassword(password As String) As String
        Using sha256 As SHA256 = sha256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
            Dim hash As Byte() = sha256.ComputeHash(bytes)
            Return BitConverter.ToString(hash).Replace("-", "").ToLower()
        End Using
    End Function

    Private Sub LoadStaffs()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim query As String = "
            SELECT UserID, FullName, Username, PhoneNumber, Email
            FROM Users
            WHERE Role = 'Staff'
            ORDER BY FullName
        "

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DgvStaffs.DataSource = dt
        End Using
    End Sub

    Private Sub ClearStaffInputs()
        TxtName.Text = ""
        TxtUsername.Text = ""
        TxtPhone.Text = ""
        TxtPassword.Text = ""
        TxtConfirmPassword.Text = ""
        TxtEmail.Text = ""
        chkShowPassword.Checked = False
        TxtPassword.UseSystemPasswordChar = True
        TxtConfirmPassword.UseSystemPasswordChar = True
    End Sub

    Private Sub AdminDBStaffMaintenance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStaffs()
        ClearStaffInputs()
    End Sub

    Private Sub ChkShowPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPassword.CheckedChanged
        TxtPassword.UseSystemPasswordChar = Not chkShowPassword.Checked
        TxtConfirmPassword.UseSystemPasswordChar = Not chkShowPassword.Checked
    End Sub

    Private Sub BTNAdd_Click(sender As Object, e As EventArgs) Handles BTNAdd.Click
        If TxtPassword.Text.Trim <> TxtConfirmPassword.Text.Trim Then
            MessageBox.Show("Passwords do not match.")
            Exit Sub
        End If

        If Not ValidateStaffFields() Then Exit Sub

        If IsDuplicateEmailOrUsername(TxtEmail.Text.Trim(), TxtUsername.Text.Trim()) Then
            MessageBox.Show("Email or Username already exists. Please choose another.")
            Exit Sub
        End If

        Dim hashedPassword As String = HashPassword(TxtPassword.Text)
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            ' Insert into Users
            Dim queryUser As String = "
            INSERT INTO Users (FullName, Username, Password, Role, PhoneNumber, Email, DateCreated)
            OUTPUT INSERTED.UserID
            VALUES (@fullname, @username, @password, 'Staff', @phone, @email, GETDATE())
        "

            Dim userId As Integer
            Using cmdUser As New SqlCommand(queryUser, con)
                cmdUser.Parameters.AddWithValue("@fullname", TxtName.Text.Trim)
                cmdUser.Parameters.AddWithValue("@username", TxtUsername.Text.Trim)
                cmdUser.Parameters.AddWithValue("@password", hashedPassword) ' ideally hash this
                cmdUser.Parameters.AddWithValue("@phone", TxtPhone.Text.Trim)
                cmdUser.Parameters.AddWithValue("@email", TxtUsername.Text.Trim & "@staff.com") ' or use a textbox if you want

                userId = Convert.ToInt32(cmdUser.ExecuteScalar())
            End Using
        End Using

        MessageBox.Show("Staff account added successfully.")
        LoadStaffs()
        ClearStaffInputs()
    End Sub

    Private Sub SearchStaff_TextChanged(sender As Object, e As EventArgs) Handles SearchStaff.TextChanged
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim query As String
            If SearchStaff.Text.Trim = "" Then
                query = "
                SELECT U.UserID, U.FullName, U.Username, U.PhoneNumber, U.Email
                FROM Users U
                INNER JOIN Staffs S ON U.UserID = S.UserID
                WHERE U.Role = 'Staff'
                ORDER BY U.FullName
            "
            Else
                query = "
                SELECT U.UserID, U.FullName, U.Username, U.PhoneNumber, U.Email
                FROM Users U
                INNER JOIN Staffs S ON U.UserID = S.UserID
                WHERE U.Role = 'Staff'
                  AND (
                        COALESCE(U.FullName,'') LIKE @search
                     OR COALESCE(U.Username,'') LIKE @search
                     OR COALESCE(U.PhoneNumber,'') LIKE @search
                     OR COALESCE(U.Email,'') LIKE @search
                  )
                ORDER BY U.FullName
            "
            End If

            Using cmd As New SqlCommand(query, con)
                If SearchStaff.Text.Trim <> "" Then
                    cmd.Parameters.AddWithValue("@search", "%" & SearchStaff.Text.Trim & "%")
                End If

                Dim adapter As New SqlDataAdapter(cmd)
                Dim table As New DataTable()
                adapter.Fill(table)

                DgvStaffs.DataSource = table
            End Using
        End Using
    End Sub
    Private Function ValidateStaffFields(Optional staffID As Integer = 0) As Boolean
        ' Full Name: letters only
        If String.IsNullOrWhiteSpace(TxtName.Text) OrElse
       Not TxtName.Text.All(Function(c) Char.IsLetter(c) OrElse c = " "c) Then
            MessageBox.Show("Full Name must contain letters only.")
            TxtName.Focus()
            Return False
        End If

        ' Phone Number: digits only
        If String.IsNullOrWhiteSpace(TxtPhone.Text) OrElse
       Not TxtPhone.Text.All(Function(c) Char.IsDigit(c)) Then
            MessageBox.Show("Phone Number must contain digits only.")
            TxtPhone.Focus()
            Return False
        End If

        ' Username: letters and numbers only
        If String.IsNullOrWhiteSpace(TxtUsername.Text) OrElse
       Not TxtUsername.Text.All(Function(c) Char.IsLetterOrDigit(c)) Then
            MessageBox.Show("Username must contain only letters and numbers.")
            TxtUsername.Focus()
            Return False
        End If

        ' Email: must end with @gmail.com and alphanumeric before domain
        Dim email As String = TxtEmail.Text.Trim()
        If String.IsNullOrWhiteSpace(email) OrElse Not email.ToLower().EndsWith("@gmail.com") Then
            MessageBox.Show("Email must end with '@gmail.com'.")
            TxtEmail.Focus()
            Return False
        End If

        Dim localPart As String = email.Substring(0, email.Length - 10)
        If Not localPart.All(Function(c) Char.IsLetterOrDigit(c)) Then
            MessageBox.Show("Email username must contain only letters and numbers.")
            TxtEmail.Focus()
            Return False
        End If

        ' Password: at least 8 characters and 1 uppercase
        Dim password As String = TxtPassword.Text.Trim()
        Dim confirmPassword As String = TxtConfirmPassword.Text.Trim()

        If password.Length < 8 Then
            MessageBox.Show("Password must be at least 8 characters long.")
            TxtPassword.Focus()
            Return False
        End If
        If Not password.Any(Function(c) Char.IsUpper(c)) Then
            MessageBox.Show("Password must contain at least one uppercase letter.")
            TxtPassword.Focus()
            Return False
        End If
        If Not password.Equals(confirmPassword) Then
            MessageBox.Show("Passwords do not match.")
            TxtConfirmPassword.Focus()
            Return False
        End If

        ' Duplicate check for Email and Username
        If IsDuplicateEmailOrUsername(email, TxtUsername.Text.Trim(), staffID) Then
            MessageBox.Show("Email or Username already exists. Please choose another.")
            Return False
        End If

        Return True
    End Function
    Private Function IsDuplicateEmailOrUsername(email As String, username As String, Optional recordID As Integer = 0) As Boolean
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
                cmd.Parameters.AddWithValue("@id", recordID)

                Dim count As Integer = CInt(cmd.ExecuteScalar())
                Return count > 0
            End Using
        End Using
    End Function

    Private Sub TxtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtName.KeyPress
        ' Allow control keys (Backspace, Delete, etc.)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        ' Allow letters and spaces only
        If Not (Char.IsLetter(e.KeyChar) OrElse e.KeyChar = " "c) Then
            e.Handled = True ' Block invalid input
        End If
    End Sub

    Private Sub TxtUsername_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtUsername.KeyPress
        If Char.IsControl(e.KeyChar) Then Return
        If Not Char.IsLetterOrDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TxtPhone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPhone.KeyPress
        ' Allow control keys (Backspace, Delete, etc.)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        ' Allow digits only
        If Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True ' Block invalid input
        End If
    End Sub

    Private Sub TxtEmail_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtEmail.KeyPress
        If Char.IsControl(e.KeyChar) Then Return
        If Not (Char.IsLetterOrDigit(e.KeyChar) OrElse e.KeyChar = "@"c OrElse e.KeyChar = "."c) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TxtPhone_TextChanged(sender As Object, e As EventArgs) Handles TxtPhone.TextChanged

    End Sub
End Class