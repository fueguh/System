Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class Login
    Public Shared Dashboard As AdminDashboard
    Private Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
            Dim hash As Byte() = sha256.ComputeHash(bytes)
            Return BitConverter.ToString(hash).Replace("-", "").ToLower() ' ✅ hex format
        End Using
    End Function

    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtUsername.TextChanged

    End Sub

    Private Sub Guna2TextBox2_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged

    End Sub
    Private Sub Clearform()
        txtUsername.Text = ""
        txtPassword.Text = ""
    End Sub
    Private Sub BtnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If txtUsername.Text.Trim = "" Or txtPassword.Text.Trim = "" Then
            MessageBox.Show("Please enter both username and password.")
            Exit Sub
        End If

        Dim hashed = HashPassword(txtPassword.Text)

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim cmd As New SqlCommand("
            SELECT UserID, Username, Role
            FROM Users
            WHERE Username=@username AND Password=@password", con)


            cmd.Parameters.AddWithValue("@username", txtUsername.Text)
            cmd.Parameters.AddWithValue("@password", hashed)

            Using dr As SqlDataReader = cmd.ExecuteReader()
                If dr.Read() Then
                    Dim role As String = dr("Role").ToString()


                    ' Redirect based on role
                    Select Case role
                        Case "Admin"
                            Dashboard = New AdminDashboard()
                            Dashboard.Show()

                        Case "Dentist"
                            DentistDashboard.Show()

                        Case "Staff"
                            StaffDashboard.Show()

                    End Select
                    Me.Hide()
                Else
                    MessageBox.Show("Invalid username or password.")
                End If

            End Using
        End Using
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Dim reg As New AdminDBUsers()
        reg.ShowDialog()
    End Sub
End Class
