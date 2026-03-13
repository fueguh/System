Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions

Public Class AdminDBStaffMaintenance
    Private selectedStaffID As Integer = 0
    Private ReadOnly connString As String = My.Settings.DentalDBConnection2

#Region "Form Events"

    Private Sub AdminDBStaffMaintenance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStaffs()
        ClearStaffInputs()
    End Sub

    Private Sub Guna2CirclePictureBox1_Click_1(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        SystemSession.NavigateToDashboard(Me)
        Me.Hide()
    End Sub

#End Region

#Region "UI & State Management"

    Private Sub SetButtonState(isEditMode As Boolean)
        BTNAdd.Enabled = Not isEditMode
        BtnUpdate.Enabled = isEditMode
        BtnDelete.Enabled = isEditMode
    End Sub

    Private Sub ClearStaffInputs()
        selectedStaffID = 0
        TxtName.Clear()
        TxtUsername.Clear()
        TxtPhone.Clear()
        TxtPassword.Clear()
        TxtConfirmPassword.Clear()
        TxtEmail.Clear()
        chkShowPassword.Checked = False
        TxtPassword.UseSystemPasswordChar = True
        TxtConfirmPassword.UseSystemPasswordChar = True
        DgvStaffs.ClearSelection()

        SetButtonState(False)
        TxtName.Focus()
    End Sub

    Private Sub RefreshData()
        LoadStaffs()
        ClearStaffInputs()
    End Sub

#End Region

#Region "Data Loading"

    Private Sub LoadStaffs()
        Using con As New SqlConnection(connString)
            con.Open()
            Dim query As String = "SELECT UserID, FullName, Username, PhoneNumber, Email FROM Users WHERE Role = 'Staff' ORDER BY FullName"
            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DgvStaffs.DataSource = dt
            If DgvStaffs.Columns.Contains("UserID") Then DgvStaffs.Columns("UserID").Visible = False
        End Using
    End Sub

#End Region

#Region "CRUD Operations"

    Private Sub BTNAdd_Click(sender As Object, e As EventArgs) Handles BTNAdd.Click
        If Not ValidateStaffFields() Then Exit Sub

        If IsDuplicateEmailOrUsername(TxtEmail.Text.Trim(), TxtUsername.Text.Trim()) Then
            MessageBox.Show("Email or Username already exists.")
            Exit Sub
        End If

        Try
            Using con As New SqlConnection(connString)
                con.Open()
                Dim query As String = "INSERT INTO Users (FullName, Username, Password, Role, PhoneNumber, Email, DateCreated) 
                                       VALUES (@fullname, @username, @password, 'Staff', @phone, @email, GETDATE())"

                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@fullname", TxtName.Text.Trim)
                    cmd.Parameters.AddWithValue("@username", TxtUsername.Text.Trim)
                    cmd.Parameters.AddWithValue("@password", HashPassword(TxtPassword.Text))
                    cmd.Parameters.AddWithValue("@phone", TxtPhone.Text.Trim)
                    cmd.Parameters.AddWithValue("@email", TxtEmail.Text.Trim)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ' FIXED: Using the standardized Audit helper
            LogAuditTrail("Created Staff Account: " & TxtUsername.Text.Trim)

            MessageBox.Show("Staff account created successfully.")
            RefreshData()
            Dashboard?.LoadDashboardStats()
        Catch ex As Exception
            MessageBox.Show("Error adding staff: " & ex.Message)
        End Try
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        If Not ValidateStaffFields(selectedStaffID) Then Exit Sub

        If IsDuplicateEmailOrUsername(TxtEmail.Text.Trim(), TxtUsername.Text.Trim(), selectedStaffID) Then
            MessageBox.Show("This Email or Username is already taken by another staff member.", "Duplicate Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        Try
            Using con As New SqlConnection(connString)
                con.Open()
                Dim updatePassword As Boolean = Not String.IsNullOrWhiteSpace(TxtPassword.Text)
                Dim query As String = If(updatePassword,
                    "UPDATE Users SET FullName=@name, Username=@un, PhoneNumber=@ph, Email=@em, Password=@pw WHERE UserID=@id",
                    "UPDATE Users SET FullName=@name, Username=@un, PhoneNumber=@ph, Email=@em WHERE UserID=@id")

                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@id", selectedStaffID)
                    cmd.Parameters.AddWithValue("@name", TxtName.Text.Trim)
                    cmd.Parameters.AddWithValue("@un", TxtUsername.Text.Trim)
                    cmd.Parameters.AddWithValue("@ph", TxtPhone.Text.Trim)
                    cmd.Parameters.AddWithValue("@em", TxtEmail.Text.Trim)
                    If updatePassword Then cmd.Parameters.AddWithValue("@pw", HashPassword(TxtPassword.Text))
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            LogAuditTrail("Updated Staff ID: " & selectedStaffID)
            MessageBox.Show("Staff updated successfully.")
            RefreshData()
        Catch ex As Exception
            MessageBox.Show("Update Error: " & ex.Message)
        End Try
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If MessageBox.Show("Permanently delete this staff member?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Try
                Using con As New SqlConnection(connString)
                    con.Open()
                    ' Clean up sessions first to avoid foreign key errors
                    Dim cmdSess As New SqlCommand("DELETE FROM UserSessions WHERE UserID = @id", con)
                    cmdSess.Parameters.AddWithValue("@id", selectedStaffID)
                    cmdSess.ExecuteNonQuery()

                    Dim cmdUser As New SqlCommand("DELETE FROM Users WHERE UserID = @id", con)
                    cmdUser.Parameters.AddWithValue("@id", selectedStaffID)
                    cmdUser.ExecuteNonQuery()
                End Using

                LogAuditTrail("Deleted Staff ID: " & selectedStaffID)
                MessageBox.Show("Staff member deleted.")
                RefreshData()
                Dashboard?.LoadDashboardStats()
            Catch ex As Exception
                MessageBox.Show("Delete Error: This staff member may be linked to transaction records.")
            End Try
        End If
    End Sub

#End Region

#Region "Search & Grid Logic"

    Private Sub SearchStaff_TextChanged(sender As Object, e As EventArgs) Handles SearchStaff.TextChanged
        Dim filter As String = SearchStaff.Text.Trim()
        Using con As New SqlConnection(connString)
            con.Open()
            Dim query As String = "SELECT UserID, FullName, Username, PhoneNumber, Email FROM Users WHERE Role = 'Staff'"
            If filter <> "" Then
                query &= " AND (FullName LIKE @search OR Username LIKE @search OR PhoneNumber LIKE @search OR Email LIKE @search)"
            End If
            query &= " ORDER BY FullName"

            Using cmd As New SqlCommand(query, con)
                If filter <> "" Then cmd.Parameters.AddWithValue("@search", "%" & filter & "%")
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)
                DgvStaffs.DataSource = dt
            End Using
        End Using
    End Sub

    Private Sub DgvStaffs_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvStaffs.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DgvStaffs.Rows(e.RowIndex)
            selectedStaffID = CInt(row.Cells("UserID").Value)
            TxtName.Text = row.Cells("FullName").Value.ToString()
            TxtUsername.Text = row.Cells("Username").Value.ToString()
            TxtPhone.Text = row.Cells("PhoneNumber").Value.ToString()
            TxtEmail.Text = row.Cells("Email").Value.ToString()

            TxtPassword.Clear()
            TxtConfirmPassword.Clear()
            SetButtonState(True)
        End If
    End Sub

#End Region

#Region "Security & Validation"

    Private Sub LogAuditTrail(actionDescription As String)
        ' Calling the shared SystemSession helper ensures it matches your database columns
        SystemSession.LogAudit(actionDescription, "Staff Maintenance",
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

    Private Function IsDuplicateEmailOrUsername(email As String, username As String, Optional id As Integer = 0) As Boolean
        Using con As New SqlConnection(connString)
            con.Open()
            Dim query As String = "SELECT COUNT(*) FROM Users WHERE (Email = @em OR Username = @un) AND UserID <> @id"
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@em", email)
                cmd.Parameters.AddWithValue("@un", username)
                cmd.Parameters.AddWithValue("@id", id)
                Return CInt(cmd.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

    ' ✅ IMPROVED: Unified Validation Logic
    Private Function ValidateStaffFields(Optional staffID As Integer = 0) As Boolean
        ' 1. Name Validation (Allows letters, single spaces, dots, and hyphens)
        Dim nameRegex As String = "^[A-Za-z]+(?:[ .'-][A-Za-z]+)*[.]?$"
        If String.IsNullOrWhiteSpace(TxtName.Text) OrElse Not Regex.IsMatch(TxtName.Text.Trim(), nameRegex) Then
            MessageBox.Show("Please enter a valid Full Name (Letters, dots, and hyphens only).")
            TxtName.Focus() : Return False
        End If

        ' 2. Username Length
        If TxtUsername.Text.Trim().Length < 5 Then
            MessageBox.Show("Username must be at least 5 characters long.")
            TxtUsername.Focus() : Return False
        End If

        ' 3. PH Phone Format (11 digits, starts with 09)
        Dim phone As String = TxtPhone.Text.Trim()
        If phone.Length <> 11 OrElse Not phone.StartsWith("09") Then
            MessageBox.Show("Phone Number must be exactly 11 digits and start with '09'.")
            TxtPhone.Focus() : Return False
        End If

        ' 4. IMPROVED: Flexible Email Format (No longer Gmail-only)
        Dim emailPattern As String = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
        If Not Regex.IsMatch(TxtEmail.Text.Trim(), emailPattern) Then
            MessageBox.Show("Please enter a valid email address.")
            TxtEmail.Focus() : Return False
        End If

        ' 5. Password Check
        If staffID = 0 OrElse TxtPassword.Text.Length > 0 Then
            If TxtPassword.Text.Length < 8 OrElse Not TxtPassword.Text.Any(AddressOf Char.IsUpper) Then
                MessageBox.Show("Password must be at least 8 characters with one uppercase letter.")
                TxtPassword.Focus() : Return False
            End If
            If TxtPassword.Text <> TxtConfirmPassword.Text Then
                MessageBox.Show("Passwords do not match.")
                TxtConfirmPassword.Focus() : Return False
            End If
        End If

        Return True
    End Function

#End Region

#Region "KeyPress & Formatting"

    ' ✅ IMPROVED: Strict KeyPress handling to prevent invalid characters in real-time

    Private Sub TxtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtName.KeyPress
        If Char.IsControl(e.KeyChar) Then Return
        If Char.IsLetter(e.KeyChar) Then Return

        ' Allow space, dot, hyphen, apostrophe but prevent consecutive symbols
        Dim allowedChars As String = " .'-"
        If allowedChars.Contains(e.KeyChar) Then
            Dim lastChar As Char = If(TxtName.Text.Length > 0, TxtName.Text.Last(), ChrW(0))
            If TxtName.Text.Length = 0 OrElse allowedChars.Contains(lastChar) Then
                e.Handled = True
            End If
            Return
        End If
        e.Handled = True
    End Sub
    Private Sub TxtUsername_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtUsername.KeyPress
        If Char.IsControl(e.KeyChar) Then Return
        If e.KeyChar = " "c Then e.Handled = True : Return ' No spaces in username

        If Char.IsLetterOrDigit(e.KeyChar) Then Return

        ' Allow dot or underscore but not at the start or consecutively
        Dim allowedSymbols As String = "._"
        If allowedSymbols.Contains(e.KeyChar) Then
            Dim lastChar As Char = If(TxtUsername.Text.Length > 0, TxtUsername.Text.Last(), ChrW(0))
            If TxtUsername.Text.Length = 0 OrElse allowedSymbols.Contains(lastChar) Then
                e.Handled = True
            End If
            Return
        End If
        e.Handled = True
    End Sub
    Private Sub TxtPhone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPhone.KeyPress
        If Char.IsControl(e.KeyChar) Then Return
        ' Only allow digits and limit to 11
        If Char.IsDigit(e.KeyChar) AndAlso TxtPhone.Text.Length < 11 Then Return
        e.Handled = True
    End Sub
    Private Sub TxtEmail_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtEmail.KeyPress
        If Char.IsControl(e.KeyChar) Then Return
        If e.KeyChar = " "c Then e.Handled = True : Return

        ' Standard email characters
        Dim allowedEmailChars As String = "@._-"
        If Char.IsLetterOrDigit(e.KeyChar) OrElse allowedEmailChars.Contains(e.KeyChar) Then
            Return
        End If
        e.Handled = True
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        RefreshData()
        SearchStaff.Clear()
    End Sub

    Private Sub ChkShowPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPassword.CheckedChanged
        TxtPassword.UseSystemPasswordChar = Not chkShowPassword.Checked
        TxtConfirmPassword.UseSystemPasswordChar = Not chkShowPassword.Checked
    End Sub

#End Region

End Class