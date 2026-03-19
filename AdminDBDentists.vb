Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class AdminDBDentists
    Private selectedDentistID As Integer = 0
    Private ReadOnly connString As String = My.Settings.DentalDBConnection2

#Region "Form Events"

    Private Sub AdminDBDentists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDentists()
        Clearform()
    End Sub

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        SystemSession.NavigateToDashboard(Me)
        Me.Hide()
    End Sub

#End Region

#Region "UI & State Management"

    Private Sub SetButtonState(isEditMode As Boolean)
        ' Add is for new records; Update/Delete are for existing ones
        BTNAdd.Enabled = Not isEditMode
        BtnUpdate.Enabled = isEditMode
        BtnDelete.Enabled = isEditMode
    End Sub

    Private Sub Clearform()
        selectedDentistID = 0
        TxtName.Clear()
        TxtSpecialization.Clear()
        TxtUsername.Clear()
        TxtPhone.Clear()
        TxtEmail.Clear()
        TxtPassword.Clear()
        cmbAvailability.SelectedIndex = -1
        cmbAvailability.Text = ""
        DGVDentists.ClearSelection()

        SetButtonState(False)
        TxtName.Focus()
    End Sub

    Private Sub RefreshData()
        LoadDentists()
        Clearform()
    End Sub

#End Region

#Region "Data Loading"

    Private Sub LoadDentists()
        Using con As New SqlConnection(connString)
            con.Open()
            Dim query As String = "SELECT UserID, FullName, Username, PhoneNumber, Email, Specialization, Availability FROM Users WHERE Role = 'Dentist' ORDER BY FullName"
            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVDentists.DataSource = dt
            If DGVDentists.Columns.Contains("UserID") Then DGVDentists.Columns("UserID").Visible = False
        End Using
    End Sub

#End Region

#Region "CRUD Operations"

    Private Sub BTNAdd_Click_1(sender As Object, e As EventArgs) Handles BTNAdd.Click
        If Not ValidateDentistFields() Then Exit Sub
        If IsUsernameTaken(TxtUsername.Text.Trim()) Then
            MessageBox.Show("Username already exists.")
            Exit Sub
        End If

        Try
            Using con As New SqlConnection(connString)
                con.Open()
                Dim query As String = "INSERT INTO Users (FullName, Username, Password, Role, PhoneNumber, Email, DateCreated, Specialization, Availability) 
                                       VALUES (@name, @username, @password, 'Dentist', @phone, @email, GETDATE(), @spec, @avail)"

                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@name", TxtName.Text.Trim)
                    cmd.Parameters.AddWithValue("@username", TxtUsername.Text.Trim)
                    cmd.Parameters.AddWithValue("@password", HashPassword(TxtPassword.Text))
                    cmd.Parameters.AddWithValue("@phone", If(String.IsNullOrWhiteSpace(TxtPhone.Text), DBNull.Value, TxtPhone.Text.Trim()))
                    cmd.Parameters.AddWithValue("@email", If(String.IsNullOrWhiteSpace(TxtEmail.Text), DBNull.Value, TxtEmail.Text.Trim()))
                    cmd.Parameters.AddWithValue("@spec", TxtSpecialization.Text.Trim)
                    cmd.Parameters.AddWithValue("@avail", cmbAvailability.Text)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            LogAudit("Created Dentist Account: " & TxtUsername.Text.Trim)
            MessageBox.Show("Dentist saved successfully.")
            RefreshData()
            Dashboard?.LoadDashboardStats()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        If Not ValidateDentistFields(selectedDentistID) Then Exit Sub

        Try
            Using con As New SqlConnection(connString)
                con.Open()
                Dim hasNewPass As Boolean = Not String.IsNullOrWhiteSpace(TxtPassword.Text)
                Dim query As String = If(hasNewPass,
                "UPDATE Users SET FullName=@n, Username=@u, PhoneNumber=@p, Email=@e, Specialization=@s, Availability=@a, Password=@pw WHERE UserID=@id",
                "UPDATE Users SET FullName=@n, Username=@u, PhoneNumber=@p, Email=@e, Specialization=@s, Availability=@a WHERE UserID=@id")

                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@id", selectedDentistID)
                    cmd.Parameters.AddWithValue("@n", TxtName.Text.Trim)
                    cmd.Parameters.AddWithValue("@u", TxtUsername.Text.Trim)
                    cmd.Parameters.AddWithValue("@p", TxtPhone.Text.Trim)
                    cmd.Parameters.AddWithValue("@e", TxtEmail.Text.Trim)
                    cmd.Parameters.AddWithValue("@s", TxtSpecialization.Text.Trim)
                    cmd.Parameters.AddWithValue("@a", cmbAvailability.Text)
                    If hasNewPass Then cmd.Parameters.AddWithValue("@pw", HashPassword(TxtPassword.Text))
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ' Audit now logs actual name
            LogAudit("Updated Dentist: " & TxtName.Text.Trim)

            MessageBox.Show("Dentist updated successfully.")
            RefreshData()
        Catch ex As Exception
            MessageBox.Show("Update Error: " & ex.Message)
        End Try
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If MessageBox.Show("Permanently delete this dentist record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Try
                Using con As New SqlConnection(connString)
                    con.Open()
                    Using cmdSess As New SqlCommand("DELETE FROM UserSessions WHERE UserID = @id", con)
                        cmdSess.Parameters.AddWithValue("@id", selectedDentistID)
                        cmdSess.ExecuteNonQuery()
                    End Using

                    Using cmdUser As New SqlCommand("DELETE FROM Users WHERE UserID = @id", con)
                        cmdUser.Parameters.AddWithValue("@id", selectedDentistID)
                        cmdUser.ExecuteNonQuery()
                    End Using
                End Using

                LogAudit("Deleted Dentist record ID: " & selectedDentistID)
                MessageBox.Show("Dentist deleted successfully.")
                RefreshData()
                Dashboard?.LoadDashboardStats()
            Catch ex As Exception
                MessageBox.Show("Delete Error: This dentist may have existing appointments.")
            End Try
        End If
    End Sub

#End Region

#Region "Search & Grid Logic"

    Private Sub DGVDentists_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVDentists.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DGVDentists.Rows(e.RowIndex)
            selectedDentistID = Convert.ToInt32(row.Cells("UserID").Value)

            TxtName.Text = row.Cells("FullName").Value.ToString()
            TxtUsername.Text = row.Cells("Username").Value.ToString()
            TxtPhone.Text = row.Cells("PhoneNumber").Value.ToString()
            TxtEmail.Text = row.Cells("Email").Value.ToString()
            TxtSpecialization.Text = row.Cells("Specialization").Value.ToString()
            cmbAvailability.Text = row.Cells("Availability").Value.ToString()

            TxtPassword.Clear()
            SetButtonState(True)
        End If
    End Sub

    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Guna2TextBox1.TextChanged
        Dim search As String = Guna2TextBox1.Text.Trim()
        Using con As New SqlConnection(connString)
            con.Open()
            Dim query As String = "SELECT UserID, FullName, Username, PhoneNumber, Email, Specialization, Availability FROM Users WHERE Role = 'Dentist'"
            If search <> "" Then
                query &= " AND (FullName LIKE @s OR Username LIKE @s OR PhoneNumber LIKE @s OR Email LIKE @s OR Specialization LIKE @s)"
            End If
            query &= " ORDER BY FullName"

            Using cmd As New SqlCommand(query, con)
                If search <> "" Then cmd.Parameters.AddWithValue("@s", "%" & search & "%")
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)
                DGVDentists.DataSource = dt
                If DGVDentists.Columns.Contains("UserID") Then DGVDentists.Columns("UserID").Visible = False
            End Using
        End Using
    End Sub

#End Region

#Region "Security & Validation"

    Private Sub LogAudit(action As String)
        SystemSession.LogAudit(action, "Dentist Maintenance",
                               SystemSession.LoggedInUserID,
                               SystemSession.LoggedInFullName,
                               SystemSession.LoggedInRole)
    End Sub

    Public Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
            Dim hash As Byte() = sha256.ComputeHash(bytes)
            Return BitConverter.ToString(hash).Replace("-", "").ToLower()
        End Using
    End Function

    Private Function IsUsernameTaken(username As String) As Boolean
        Using con As New SqlConnection(connString)
            con.Open()
            Dim cmd As New SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @u", con)
            cmd.Parameters.AddWithValue("@u", username)
            Return CInt(cmd.ExecuteScalar()) > 0
        End Using
    End Function

    Private Function ValidateDentistFields(Optional dentistID As Integer = 0) As Boolean
        ' --- 1. FULL NAME VALIDATION ---
        Dim fullName As String = TxtName.Text.Trim()
        ' Regex: Starts with letter, single spaces/dots/hyphens allowed between words
        Dim nameRegex As String = "^[A-Za-z]+(?:[ .'-][A-Za-z]+)*[.]?$"

        If String.IsNullOrWhiteSpace(fullName) OrElse Not System.Text.RegularExpressions.Regex.IsMatch(fullName, nameRegex) Then
            MessageBox.Show("Full Name must start with a letter. Use single spaces/dots only between words.")
            TxtName.Focus()
            Return False
        End If

        ' --- 2. USERNAME VALIDATION ---
        Dim username As String = TxtUsername.Text.Trim()
        ' Regex: Starts/ends with alphanumeric, allows single dots/underscores in between
        Dim userRegex As String = "^[A-Za-z0-9](?:[._]?[A-Za-z0-9])*$"

        If username.Length < 5 Then
            MessageBox.Show("Username must be at least 5 characters long.")
            TxtUsername.Focus()
            Return False
        ElseIf Not System.Text.RegularExpressions.Regex.IsMatch(username, userRegex) Then
            MessageBox.Show("Username can only contain letters, numbers, and single dots/underscores.")
            TxtUsername.Focus()
            Return False
        End If

        ' --- 3. PHONE NUMBER VALIDATION (OPTIONAL)(PH 11-Digit Format) ---
        Dim phone As String = TxtPhone.Text.Trim()

        If Not String.IsNullOrWhiteSpace(phone) Then 'Optional field, only validate if not empty
            If phone.Length <> 11 OrElse Not phone.All(AddressOf Char.IsDigit) Then
                MessageBox.Show("Phone Number must be exactly 11 digits.")
                TxtPhone.Focus()
                Return False
            ElseIf Not phone.StartsWith("09") Then
                MessageBox.Show("Invalid Phone Number. Must start with '09'.")
                TxtPhone.Focus()
                Return False
            End If
        End If
        ' --- 4. EMAIL VALIDATION (OPTIONAL) ---
        Dim email As String = TxtEmail.Text.Trim()
        Dim emailPattern As String = "^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"

        If Not String.IsNullOrWhiteSpace(email) Then
            If Not System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern) OrElse email.Contains("..") Then
                MessageBox.Show("Please enter a valid email address (e.g., name@example.com).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TxtEmail.Focus()
                Return False
            End If

            Dim atIndex As Integer = email.IndexOf("@"c)
            Dim localPart As String = email.Substring(0, atIndex)

            If localPart.Length < 1 OrElse Not Char.IsLetterOrDigit(localPart(0)) Then
                MessageBox.Show("Email username must start with a letter or digit.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TxtEmail.Focus()
                Return False
            End If
        End If

        ' --- 5. PASSWORD VALIDATION ---
        ' Only validates if it's a new record (ID=0) or if the user typed something in the password field
        If dentistID = 0 OrElse TxtPassword.Text.Length > 0 Then
            If TxtPassword.Text.Length < 8 OrElse Not TxtPassword.Text.Any(AddressOf Char.IsUpper) Then
                MessageBox.Show("Password must be at least 8 characters long and contain at least one uppercase letter.")
                TxtPassword.Focus()
                Return False
            End If

        End If

        Return True
    End Function
    Private Function IsDuplicateEmailOrUsername(email As String, username As String, Optional userID As Integer = 0) As Boolean
        Using con As New SqlConnection(connString)
            con.Open()

            Dim query As String

            ' If email is empty → only check username
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

                Return CInt(cmd.ExecuteScalar()) > 0
            End Using
        End Using
    End Function
#End Region

#Region "KeyPress Restrictions"

    Private Sub TxtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtName.KeyPress
        ' Allow backspace and control keys
        If Char.IsControl(e.KeyChar) Then Return

        Dim allowedChars As String = " .'-" ' Space, dot, hyphen, apostrophe
        Dim lastChar As Char = If(TxtName.Text.Length > 0, TxtName.Text.Last(), ChrW(0))

        ' Allow letters
        If Char.IsLetter(e.KeyChar) Then Return

        ' Allow special characters but prevent consecutive ones
        If allowedChars.Contains(e.KeyChar) Then
            If lastChar = e.KeyChar Then
                e.Handled = True ' Block consecutive (e.g., "  ")
            ElseIf TxtName.Text.Length = 0 AndAlso e.KeyChar = " "c Then
                e.Handled = True ' Cannot start with a space
            End If
            Return
        End If

        ' Block everything else
        e.Handled = True
    End Sub
    Private Sub TxtUsername_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtUsername.KeyPress
        ' Block spaces entirely for usernames
        If e.KeyChar = " "c Then
            e.Handled = True
            Return
        End If

        ' Allow letters, digits, and control keys (backspace)
        If Char.IsLetterOrDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Then Return

        ' Allow specific special characters (optional: dot or underscore) 
        ' but prevent them from being the first character or consecutive
        Dim allowedSpecial As String = "._"
        If allowedSpecial.Contains(e.KeyChar) Then
            Dim lastChar As Char = If(TxtUsername.Text.Length > 0, TxtUsername.Text.Last(), ChrW(0))
            If TxtUsername.Text.Length = 0 OrElse lastChar = e.KeyChar Then
                e.Handled = True
            End If
            Return
        End If

        ' Block everything else
        e.Handled = True
    End Sub
    Private Sub TxtPhone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPhone.KeyPress
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then e.Handled = True
    End Sub
    Private Sub TxtSpecialization_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtSpecialization.KeyPress
        ' Allow backspace/control keys
        If Char.IsControl(e.KeyChar) Then Return

        ' Allow letters
        If Char.IsLetter(e.KeyChar) Then Return

        ' Allow space or hyphen, but prevent consecutive ones and starting with them
        Dim allowedChars As String = " -"
        If allowedChars.Contains(e.KeyChar) Then
            Dim lastChar As Char = If(TxtSpecialization.Text.Length > 0, TxtSpecialization.Text.Last(), ChrW(0))
            If TxtSpecialization.Text.Length = 0 OrElse lastChar = e.KeyChar Then
                e.Handled = True
            End If
            Return
        End If

        ' Block numbers and other symbols
        e.Handled = True
    End Sub
#End Region

#Region "Input Formatting & Extra Actions"

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        RefreshData()
        Guna2TextBox1.Clear()
    End Sub

    Private Sub ChkShowPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPassword.CheckedChanged
        TxtPassword.UseSystemPasswordChar = Not chkShowPassword.Checked
    End Sub

#End Region

End Class