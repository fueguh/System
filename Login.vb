Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel

Public Class Login
    Private Sub Login_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            ' Check for active session
            Dim cmdActive As New SqlCommand("
            SELECT us.SessionToken, u.UserID, u.FullName, u.Role
            FROM UserSessions us
            INNER JOIN Users u ON us.UserID = u.UserID
            WHERE us.DeviceName = @deviceName AND us.IsActive = 1", con)
            cmdActive.Parameters.AddWithValue("@deviceName", Environment.MachineName)

            Using dr As SqlDataReader = cmdActive.ExecuteReader()
                If dr.Read() Then
                    ' Restore session
                    SystemSession.LoggedInUserID = CInt(dr("UserID"))
                    SystemSession.LoggedInFullName = dr("FullName").ToString()
                    SystemSession.LoggedInRole = dr("Role").ToString()
                    SystemSession.CurrentSessionToken = dr("SessionToken").ToString()

                    ' Open the correct dashboard
                    Select Case SystemSession.LoggedInRole
                        Case "Admin"
                            Dashboard = New AdminDashboard()
                            Dashboard.Show()
                        Case "Dentist"
                            DentistDashboard.Show()
                        Case "Staff"
                            StaffDashboard.Show()
                    End Select

                    ' ✅ Hide login form AFTER it has fully loaded
                    Me.Hide()
                End If
            End Using
        End Using
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim cmdCheckAdmin As New SqlCommand("SELECT COUNT(*) FROM Users WHERE Role = 'Admin'", con)
            Dim adminCount As Integer = CInt(cmdCheckAdmin.ExecuteScalar())
            If adminCount = 0 Then
                MessageBox.Show("No admin account found. Please create an admin account.")
            End If
        End Using
    End Sub

    Public Shared Dashboard As AdminDashboard
    Private Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
            Dim hash As Byte() = sha256.ComputeHash(bytes)
            Return BitConverter.ToString(hash).Replace("-", "").ToLower() ' ✅ hex format
        End Using
    End Function

    Private Sub Clearform()
        txtUsername.Text = ""
        txtPassword.Text = ""
    End Sub
    Private Sub BtnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If String.IsNullOrWhiteSpace(txtUsername.Text) OrElse String.IsNullOrWhiteSpace(txtPassword.Text) Then
            MessageBox.Show("Please enter both username and password.")
            Exit Sub
        End If

        Dim hashed = HashPassword(txtPassword.Text)

        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim cmd As New SqlCommand("
            SELECT UserID, Username, Role, FullName
            FROM Users
            WHERE Username=@username AND Password=@password", con)
            cmd.Parameters.Add("@username", SqlDbType.VarChar, 50).Value = txtUsername.Text.Trim()
            cmd.Parameters.Add("@password", SqlDbType.VarChar, 64).Value = hashed

            Using dr As SqlDataReader = cmd.ExecuteReader()
                If dr.Read() Then
                    ' Set current session
                    SystemSession.LoggedInUserID = CInt(dr("UserID"))
                    SystemSession.LoggedInFullName = dr("FullName").ToString()
                    SystemSession.LoggedInRole = dr("Role").ToString()

                    ' Audit
                    SystemSession.LogLogin()

                    ' ✅ Remember Me logic AFTER login
                    If chkRememberMe.Checked Then
                        Using con2 As New SqlConnection(My.Settings.DentalDBConnection)
                            con2.Open()

                            ' End any existing session for this device
                            Dim cmdReset As New SqlCommand("
                            UPDATE UserSessions
                            SET IsActive = 0
                            WHERE DeviceName = @deviceName AND IsActive = 1", con2)
                            cmdReset.Parameters.AddWithValue("@deviceName", Environment.MachineName)
                            cmdReset.ExecuteNonQuery()

                            ' Create new session
                            Dim cmdInsert As New SqlCommand("
                            INSERT INTO UserSessions (UserID, SessionToken, DeviceName, IsActive)
                            VALUES (@uid, @token, @deviceName, 1)", con2)
                            cmdInsert.Parameters.AddWithValue("@uid", SystemSession.LoggedInUserID)
                            cmdInsert.Parameters.AddWithValue("@token", Guid.NewGuid().ToString())
                            cmdInsert.Parameters.AddWithValue("@deviceName", Environment.MachineName)
                            cmdInsert.ExecuteNonQuery()
                        End Using
                    End If

                    ' Open the correct dashboard
                    Select Case SystemSession.LoggedInRole
                        Case "Admin"
                            Dashboard = New AdminDashboard()
                            Dashboard.Show()
                        Case "Dentist"
                            DentistDashboard.Show()
                        Case "Staff"
                            StaffDashboard.Show()
                    End Select

                    Clearform()
                    Me.Hide()
                Else
                    ' Failed login
                    SystemSession.LogAudit("Login Failed", "Login", 0, txtUsername.Text, "Unknown")
                    MessageBox.Show("Invalid username or password.")
                    Clearform()
                End If
            End Using
        End Using
    End Sub




    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Dim reg As New AdminDBUsers()
        reg.ShowDialog()
    End Sub

End Class
