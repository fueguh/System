Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class AdminDBAdminMaintenance
    Public Function HashPassword(password As String) As String
        Using sha256 As SHA256 = sha256.Create()
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
        TxtName.Text = ""
        TxtUsername.Text = ""
        TxtPhone.Text = ""
        TxtPassword.Text = ""
        TxtEmail.Text = ""
        TxtConfirmPassword.Text = ""

        chkShowPassword.Checked = False
        TxtPassword.UseSystemPasswordChar = True
        TxtConfirmPassword.UseSystemPasswordChar = True
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
        End Using
    End Sub


    Private Sub AdminDBAdminMaintenance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAdmins()
        ClearAdminInputs()
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

        ' Validate fields first
        If Not ValidatePassword() Then Exit Sub

        ' Check duplicates
        If IsDuplicateEmailOrUsername(TxtEmail.Text.Trim(), TxtUsername.Text.Trim()) Then
            MessageBox.Show("Email or Username already exists. Please choose another.")
            Exit Sub
        End If

        If Not ValidateEmail() Then Exit Sub

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            ' Insert into Users
            Dim queryUser As String = "
    INSERT INTO Users (FullName, Username, Password, Role, PhoneNumber, Email, DateCreated)
    VALUES (@fullname, @username, @password, 'Admin', @phone, @email, GETDATE())
"
            ' ✅ Hash password
            Dim hashedPassword As String = HashPassword(TxtPassword.Text)

            Dim userId As Integer
            Using cmdUser As New SqlCommand(queryUser, con)
                cmdUser.Parameters.AddWithValue("@fullname", TxtName.Text.Trim)
                cmdUser.Parameters.AddWithValue("@username", TxtUsername.Text.Trim)
                cmdUser.Parameters.AddWithValue("@password", hashedPassword) ' ideally hash this
                cmdUser.Parameters.AddWithValue("@phone", TxtPhone.Text.Trim)
                cmdUser.Parameters.AddWithValue("@email", TxtUsername.Text.Trim & "@gmail.com") ' or use a textbox if you want

                userId = Convert.ToInt32(cmdUser.ExecuteScalar())

            End Using

        End Using

        MessageBox.Show("Admin account added successfully.")
        LoadAdmins()
        ClearAdminInputs()
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
    Private Function ValidatePassword() As Boolean
        Dim password As String = TxtPassword.Text.Trim()
        Dim confirmPassword As String = TxtConfirmPassword.Text.Trim()

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

        ' Confirm password match
        If Not password.Equals(confirmPassword) Then
            MessageBox.Show("Passwords do not match.")
            TxtConfirmPassword.Focus()
            Return False
        End If

        Return True
    End Function

    Private Function ValidateEmail() As Boolean
        Dim email As String = TxtEmail.Text.Trim()

        ' Must end with @gmail.com
        If Not email.ToLower().EndsWith("@gmail.com") Then
            MessageBox.Show("Email must end with '@gmail.com'.")
            TxtEmail.Focus()
            Return False
        End If

        ' Duplicate check
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim cmd As New SqlCommand("SELECT COUNT(*) FROM Users WHERE Email=@em", con)
            cmd.Parameters.AddWithValue("@em", email)
            Dim count As Integer = CInt(cmd.ExecuteScalar())
            If count > 0 Then
                MessageBox.Show("This email is already registered.")
                TxtEmail.Focus()
                Return False
            End If
        End Using

        Return True
    End Function


    Private Function IsDuplicateEmailOrUsername(email As String, username As String, Optional userID As Integer = 0) As Boolean
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

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

    Private Sub TxtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtName.KeyPress
        ' Allow control keys (Backspace, Delete, etc.)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        ' Allow letters and spaces only
        If Not (Char.IsLetter(e.KeyChar) OrElse e.KeyChar = " "c) Then
            e.Handled = True ' Block the input
        End If
    End Sub

    Private Sub TxtPhone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPhone.KeyPress
        ' Allow control keys (Backspace, Delete, etc.)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        ' Allow digits only
        If Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True ' Block the input
        End If
    End Sub

    Private Sub TxtUsername_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtUsername.KeyPress
        ' Allow control keys (Backspace, Delete, etc.)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        ' Allow letters and digits only
        If Not (Char.IsLetterOrDigit(e.KeyChar)) Then
            e.Handled = True ' Block the input
        End If
    End Sub

    Private Sub TxtEmail_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtEmail.KeyPress
        ' Allow control keys (Backspace, Delete, etc.)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        ' Allow letters, digits, @, and .
        If Not (Char.IsLetterOrDigit(e.KeyChar) OrElse e.KeyChar = "@"c OrElse e.KeyChar = "."c) Then
            e.Handled = True ' Block invalid input
        End If
    End Sub

    Private Sub DataGridViewAdmins_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewAdmins.CellClick

    End Sub

    Private Sub TxtPhone_TextChanged(sender As Object, e As EventArgs) Handles TxtPhone.TextChanged

    End Sub
End Class