Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class AdminDBStaffMaintenance
    Private selectedStaffID As Integer = 0
    Private ReadOnly connString As String = My.Settings.DentalDBConnection2

#Region "Form Events"

    Private Sub AdminDBStaffMaintenance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStaffs()
        ClearStaffInputs()
    End Sub

    Private Sub Guna2CirclePictureBox1_Click_1(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        If SystemSession.LoggedInUserID = 0 OrElse SystemSession.LoggedInRole <> "Admin" Then
            Login.Show()
            Me.Hide()
            Exit Sub
        End If

        SystemSession.NavigateToDashboard(Me)
        Me.Hide()
    End Sub

#End Region

#Region "Data Loading & UI Management"

    ' Handles enabling/disabling buttons based on whether a record is selected
    Private Sub SetButtonState(isEditMode As Boolean)
        BTNAdd.Enabled = Not isEditMode
        BtnUpdate.Enabled = isEditMode
        BtnDelete.Enabled = isEditMode
    End Sub

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

        ' Return to "Add Mode"
        SetButtonState(False)
        TxtName.Focus()
    End Sub

    Private Sub RefreshData()
        LoadStaffs()
        ClearStaffInputs()
    End Sub

#End Region

#Region "CRUD Operations with Audit Trail"

    Private Sub BTNAdd_Click(sender As Object, e As EventArgs) Handles BTNAdd.Click
        If Not ValidateStaffFields() Then Exit Sub

        If IsDuplicateEmailOrUsername(TxtEmail.Text.Trim(), TxtUsername.Text.Trim()) Then
            MessageBox.Show("Email or Username already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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

            LogAudit("Created new staff account: " & TxtUsername.Text.Trim)
            MessageBox.Show("Staff account created successfully.")
            RefreshData()
        Catch ex As Exception
            MessageBox.Show("Error adding staff: " & ex.Message)
        End Try
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        ' Validation (selectedStaffID is already checked by button state)
        If Not ValidateStaffFields(selectedStaffID) Then Exit Sub

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

            LogAudit("Updated staff ID: " & selectedStaffID)
            MessageBox.Show("Staff updated successfully.")
            RefreshData()
        Catch ex As Exception
            MessageBox.Show("Update Error: " & ex.Message)
        End Try
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If MessageBox.Show("Are you sure you want to delete this staff member?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Try
                Using con As New SqlConnection(connString)
                    con.Open()
                    Using cmdSess As New SqlCommand("DELETE FROM UserSessions WHERE UserID = @id", con)
                        cmdSess.Parameters.AddWithValue("@id", selectedStaffID)
                        cmdSess.ExecuteNonQuery()
                    End Using

                    Using cmdUser As New SqlCommand("DELETE FROM Users WHERE UserID = @id", con)
                        cmdUser.Parameters.AddWithValue("@id", selectedStaffID)
                        cmdUser.ExecuteNonQuery()
                    End Using
                End Using

                LogAudit("Deleted staff ID: " & selectedStaffID)
                MessageBox.Show("Staff member deleted.")
                RefreshData()
            Catch ex As Exception
                MessageBox.Show("Delete Error: User may have linked records.")
            End Try
        End If
    End Sub

#End Region

#Region "Search & Grid Logic"

    Private Sub SearchStaff_TextChanged(sender As Object, e As EventArgs) Handles SearchStaff.TextChanged
        Dim filter As String = SearchStaff.Text.Trim()

        ' Deselect if user starts searching to prevent accidental updates
        If filter <> "" Then ClearStaffInputs()

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

            ' Switch to "Edit Mode"
            SetButtonState(True)
        End If
    End Sub

#End Region

#Region "Security & Validation"

    Private Function ValidateStaffFields(Optional staffID As Integer = 0) As Boolean
        If Not TxtName.Text.Replace(" ", "").All(AddressOf Char.IsLetter) OrElse TxtName.Text = "" Then
            MessageBox.Show("Full Name must contain letters only.")
            Return False
        End If

        If Not TxtEmail.Text.Trim().ToLower().EndsWith("@gmail.com") Then
            MessageBox.Show("Email must be a valid @gmail.com address.")
            Return False
        End If

        Dim pass As String = TxtPassword.Text
        ' Password required for Add (staffID=0) or if user typed something in Update mode
        If staffID = 0 OrElse pass.Length > 0 Then
            If pass.Length < 8 OrElse Not pass.Any(AddressOf Char.IsUpper) Then
                MessageBox.Show("Password must be 8+ characters with an uppercase letter.")
                Return False
            End If
            If pass <> TxtConfirmPassword.Text Then
                MessageBox.Show("Passwords do not match.")
                Return False
            End If
        End If
        Return True
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

    Public Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
            Dim hash As Byte() = sha256.ComputeHash(bytes)
            Return BitConverter.ToString(hash).Replace("-", "").ToLower()
        End Using
    End Function

    Private Sub LogAudit(actionDescription As String)
        Try
            Using con As New SqlConnection(connString)
                con.Open()
                Dim query As String = "INSERT INTO AuditLogs (AdminID, ActionTaken, LogDate) VALUES (@aid, @act, GETDATE())"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@aid", SystemSession.LoggedInUserID)
                    cmd.Parameters.AddWithValue("@act", actionDescription)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch
            ' Silent fail for logs
        End Try
    End Sub

#End Region

#Region "Input Formatting"

    Private Sub ChkShowPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPassword.CheckedChanged
        TxtPassword.UseSystemPasswordChar = Not chkShowPassword.Checked
        TxtConfirmPassword.UseSystemPasswordChar = Not chkShowPassword.Checked
    End Sub

    Private Sub TxtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtName.KeyPress
        If Not (Char.IsLetter(e.KeyChar) OrElse e.KeyChar = " "c OrElse Char.IsControl(e.KeyChar)) Then e.Handled = True
    End Sub

    Private Sub TxtPhone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPhone.KeyPress
        If Not (Char.IsDigit(e.KeyChar) OrElse Char.IsControl(e.KeyChar)) Then e.Handled = True
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        RefreshData()
        SearchStaff.Clear()
    End Sub

#End Region

End Class