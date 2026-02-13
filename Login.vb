Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Public Class Login

    Public Shared Dashboard As AdminDashboard

    ' Flag to indicate a restored session (from Remember Me)
    Private isRestoredSession As Boolean = False

    ' --- FORM LOAD ---
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()
            Dim cmdCheckAdmin As New SqlCommand("SELECT COUNT(*) FROM Users WHERE Role = 'Admin'", con)
            Dim adminCount As Integer = CInt(cmdCheckAdmin.ExecuteScalar())
            If adminCount = 0 Then
                MessageBox.Show("No admin account found. Please create an admin account.")
            End If
        End Using
    End Sub

    ' --- FORM SHOWN ---
    Private Sub Login_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            ' Check for active session on this device
            Dim cmdActive As New SqlCommand("
                SELECT us.SessionToken, u.UserID, u.FullName, u.Role
                FROM UserSessions us
                INNER JOIN Users u ON us.UserID = u.UserID
                WHERE us.DeviceName = @deviceName AND us.IsActive = 1", con)
            cmdActive.Parameters.AddWithValue("@deviceName", Environment.MachineName)

            Using dr As SqlDataReader = cmdActive.ExecuteReader()
                If dr.Read() Then
                    ' Restore session WITHOUT logging it
                    isRestoredSession = True
                    SystemSession.LoggedInUserID = CInt(dr("UserID"))
                    SystemSession.LoggedInFullName = dr("FullName").ToString()
                    SystemSession.LoggedInRole = dr("Role").ToString()
                    SystemSession.CurrentSessionToken = dr("SessionToken").ToString()

                    ' Open correct dashboard
                    OpenDashboard(SystemSession.LoggedInRole)
                    Me.Hide()
                End If
            End Using
        End Using
    End Sub

    ' --- LOGIN BUTTON ---
    Private Sub BtnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If String.IsNullOrWhiteSpace(txtUsername.Text) OrElse String.IsNullOrWhiteSpace(txtPassword.Text) Then
            MessageBox.Show("Please enter both username and password.")
            Exit Sub
        End If

        Dim hashedPassword As String = HashPassword(txtPassword.Text)

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim cmd As New SqlCommand("
                SELECT UserID, Username, Role, FullName
                FROM Users
                WHERE Username=@username AND Password=@password", con)
            cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim())
            cmd.Parameters.AddWithValue("@password", hashedPassword)

            Using dr As SqlDataReader = cmd.ExecuteReader()
                If dr.Read() Then
                    ' --- SUCCESSFUL LOGIN ---
                    SystemSession.LoggedInUserID = CInt(dr("UserID"))
                    SystemSession.LoggedInFullName = dr("FullName").ToString()
                    SystemSession.LoggedInRole = dr("Role").ToString()

                    ' Only log login if this is NOT a restored session
                    If Not isRestoredSession Then
                        SystemSession.LogLogin()
                    End If

                    ' Handle Remember Me
                    If chkRememberMe.Checked Then
                        SaveSession(SystemSession.LoggedInUserID)
                    End If

                    ' Open dashboard
                    OpenDashboard(SystemSession.LoggedInRole)

                    ClearForm()
                    Me.Hide()
                Else
                    ' --- FAILED LOGIN ---
                    SystemSession.LogAudit("Login Failed", "Login", 0, txtUsername.Text, "Unknown")
                    MessageBox.Show("Invalid username or password.")
                    ClearForm()
                End If
            End Using
        End Using
    End Sub

    ' --- OPEN DASHBOARD ---
    Private Sub OpenDashboard(role As String)
        Select Case role
            Case "Admin"
                Dashboard = New AdminDashboard()
                Dashboard.Show()
            Case "Dentist"
                DentistDashboard.Show()
            Case "Staff"
                StaffDashboard.Show()
        End Select
    End Sub

    ' --- SAVE SESSION (Remember Me) ---
    Private Sub SaveSession(userId As Integer)
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            ' End existing session for this device
            Dim cmdReset As New SqlCommand("
                UPDATE UserSessions
                SET IsActive = 0
                WHERE DeviceName = @deviceName AND IsActive = 1", con)
            cmdReset.Parameters.AddWithValue("@deviceName", Environment.MachineName)
            cmdReset.ExecuteNonQuery()

            ' Create new session
            Dim cmdInsert As New SqlCommand("
                INSERT INTO UserSessions (UserID, SessionToken, DeviceName, IsActive)
                VALUES (@uid, @token, @deviceName, 1)", con)
            cmdInsert.Parameters.AddWithValue("@uid", userId)
            cmdInsert.Parameters.AddWithValue("@token", Guid.NewGuid().ToString())
            cmdInsert.Parameters.AddWithValue("@deviceName", Environment.MachineName)
            cmdInsert.ExecuteNonQuery()
        End Using
    End Sub

    ' --- PASSWORD HASHING ---
    Private Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
            Dim hash As Byte() = sha256.ComputeHash(bytes)
            Return BitConverter.ToString(hash).Replace("-", "").ToLower()
        End Using
    End Function

    ' --- CLEAR FORM ---
    Private Sub ClearForm()
        txtUsername.Text = ""
        txtPassword.Text = ""
        isRestoredSession = False
    End Sub

    ' --- TEXTBOX INPUT VALIDATION ---
    Private Sub TxtUsername_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUsername.KeyPress
        If Not Char.IsLetterOrDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub CheckBoxShowPassword_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxShowPassword.CheckedChanged
        txtPassword.UseSystemPasswordChar = Not CheckBoxShowPassword.Checked
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) 
        Dim reg As New AdminDBUsers()
        reg.ShowDialog()
    End Sub
End Class
