Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class AdminDBDentists
    Private selectedDentistID As Integer = 0
    Private Sub AdminDBDentists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDentists()
        Clearform()
    End Sub

    Public Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
            Dim hash As Byte() = sha256.ComputeHash(bytes)
            Return BitConverter.ToString(hash).Replace("-", "").ToLower()
        End Using
    End Function

    Private Sub LoadDentists()
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String = "
            SELECT UserID, FullName, Username, PhoneNumber, Email, Specialization, Availability
            FROM Users
            WHERE Role = 'Dentist'
        "

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)

            DGVDentists.DataSource = dt
        End Using
    End Sub

    Private Sub DGVDentists_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
        ' ✅ Make sure the click is valid (not header row)
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DGVDentists.Rows(e.RowIndex)

            ' ✅ Populate textboxes/comboboxes with selected row values
            TxtName.Text = row.Cells("FullName").Value.ToString()
            TxtUsername.Text = row.Cells("Username").Value.ToString()
            TxtPhone.Text = row.Cells("PhoneNumber").Value.ToString()
            TxtEmail.Text = row.Cells("Email").Value.ToString()
            TxtSpecialization.Text = row.Cells("Specialization").Value.ToString()
            cmbAvailability.Text = row.Cells("Availability").Value.ToString()
        End If

    End Sub

    Private Sub Clearform()
        TxtName.Text = ""
        TxtSpecialization.Text = ""
        TxtUsername.Text = ""
        TxtPhone.Text = ""
        TxtEmail.Text = ""
        TxtPassword.Text = ""
        TxtConfirmPassword.Text = ""
        cmbAvailability.SelectedIndex = -1
        cmbAvailability.Text = ""
        selectedDentistID = 0
    End Sub

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

    Private Sub BTNAdd_Click_1(sender As Object, e As EventArgs) Handles BTNAdd.Click
        ' ✅ Confirm password check
        If TxtPassword.Text <> TxtConfirmPassword.Text Then
            MessageBox.Show("Passwords do not match. Please re-enter.")
            Exit Sub
        End If

        'checks if user alreadey exixts
        If IsUsernameTaken(TxtUsername.Text) Then
            MessageBox.Show("Username already exists. Please choose a different one.")
            Exit Sub
        End If

        If Not ValidateDentistFields() Then Exit Sub

        ' ✅ Hash password
        Dim hashedPassword As String = HashPassword(TxtPassword.Text)

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String = "
            INSERT INTO Users (FullName, Username, Password, Role, PhoneNumber, Email, DateCreated, Specialization, Availability)
            VALUES (@name, @username, @password, 'Dentist', @phone, @email, GETDATE(), @spec, @avail)
        "

            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@name", TxtName.Text)
            cmd.Parameters.AddWithValue("@username", TxtUsername.Text)
            cmd.Parameters.AddWithValue("@password", hashedPassword)
            cmd.Parameters.AddWithValue("@phone", TxtPhone.Text)
            cmd.Parameters.AddWithValue("@email", TxtEmail.Text)
            cmd.Parameters.AddWithValue("@spec", TxtSpecialization.Text)
            cmd.Parameters.AddWithValue("@avail", cmbAvailability.Text)

            cmd.ExecuteNonQuery()
        End Using


        MessageBox.Show("Dentist saved successfully.")
        SystemSession.LogAudit("Dentist Account Created", "Dentist Management",
                           SystemSession.LoggedInUserID,
                           SystemSession.LoggedInFullName,
                           SystemSession.LoggedInRole)

        LoadDentists()
        Clearform()

        'to reload the system overview in admin dashboard after input
        Dashboard?.LoadDashboardStats()
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

    Dim connectionString As String = "Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;"

    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Guna2TextBox1.TextChanged
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String

            ' Show all dentists if search box is empty
            If Guna2TextBox1.Text.Trim = "" Then
                query = "
                SELECT UserID AS DentistID, FullName, Username, PhoneNumber, Email, Specialization
                FROM Users
                WHERE Role = 'Dentist'
            "
            Else
                query = "
                SELECT UserID AS DentistID, FullName, Username, PhoneNumber, Email, Specialization
                FROM Users
                WHERE Role = 'Dentist'
                  AND (
                        COALESCE(FullName,'') LIKE @search
                     OR COALESCE(Username,'') LIKE @search
                     OR COALESCE(PhoneNumber,'') LIKE @search
                     OR COALESCE(Email,'') LIKE @search
                     OR COALESCE(Specialization,'') LIKE @search
                  )
            "
            End If

            Using cmd As New SqlCommand(query, con)
                If Guna2TextBox1.Text.Trim <> "" Then
                    cmd.Parameters.AddWithValue("@search", "%" & Guna2TextBox1.Text.Trim & "%")
                End If

                Dim adapter As New SqlDataAdapter(cmd)
                Dim table As New DataTable()
                adapter.Fill(table)

                DGVDentists.DataSource = table
            End Using
        End Using
    End Sub

    Private Sub ChkShowPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPassword.CheckedChanged
        If chkShowPassword.Checked Then
            ' Show the password
            TxtPassword.UseSystemPasswordChar = False
            TxtConfirmPassword.UseSystemPasswordChar = False
        Else
            ' Hide the password
            TxtPassword.UseSystemPasswordChar = True
            TxtConfirmPassword.UseSystemPasswordChar = True
        End If
    End Sub
    Private Function ValidateDentistFields(Optional dentistID As Integer = 0) As Boolean
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

        ' Specialization: letters only
        If String.IsNullOrWhiteSpace(TxtSpecialization.Text) OrElse
       Not TxtSpecialization.Text.All(Function(c) Char.IsLetter(c) OrElse c = " "c) Then
            MessageBox.Show("Specialization must contain letters only.")
            TxtSpecialization.Focus()
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
        If IsDuplicateEmailOrUsername(email, TxtUsername.Text.Trim(), dentistID) Then
            MessageBox.Show("Email or Username already exists. Please choose another.")
            Return False
        End If

        Return True
    End Function

    Private Function IsDuplicateEmailOrUsername(email As String, username As String, Optional userID As Integer = 0) As Boolean
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
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

    Private Sub TxtUsername_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtUsername.KeyPress
        If Char.IsControl(e.KeyChar) Then Return
        If Not Char.IsLetterOrDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TxtEmail_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtEmail.KeyPress
        If Char.IsControl(e.KeyChar) Then Return
        If Not (Char.IsLetterOrDigit(e.KeyChar) OrElse e.KeyChar = "@"c OrElse e.KeyChar = "."c) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TxtSpecialization_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtSpecialization.KeyPress
        If Char.IsControl(e.KeyChar) Then Return
        If Not (Char.IsLetter(e.KeyChar) OrElse e.KeyChar = " "c) Then
            e.Handled = True
        End If
    End Sub
End Class