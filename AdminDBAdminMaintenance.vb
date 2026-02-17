Imports System.Data.SqlClient

Public Class AdminDBAdminMaintenance
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
        TxtName.Clear()
        TxtUsername.Clear()
        TxtPhone.Clear()
        TxtPassword.Clear()
        TxtConfirmPassword.Clear()
        cmbAvailability.SelectedIndex = -1
        chkShowPassword.Checked = False
        TxtPassword.UseSystemPasswordChar = True
        TxtConfirmPassword.UseSystemPasswordChar = True
    End Sub

    Private Sub LoadAdmins()
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String = "
            SELECT U.UserID, U.FullName, U.Username, U.PhoneNumber, U.Email, U.Availability
            FROM Users U
            INNER JOIN Admins A ON U.UserID = A.UserID
            WHERE U.Role = 'Admin'
            ORDER BY U.FullName
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

    Private Sub chkShowPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPassword.CheckedChanged
        TxtPassword.UseSystemPasswordChar = Not chkShowPassword.Checked
        TxtConfirmPassword.UseSystemPasswordChar = Not chkShowPassword.Checked
    End Sub

    Private Sub BTNAdd_Click(sender As Object, e As EventArgs) Handles BTNAdd.Click
        If TxtPassword.Text.Trim <> TxtConfirmPassword.Text.Trim Then
            MessageBox.Show("Passwords do not match.")
            Exit Sub
        End If

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            ' Insert into Users
            Dim queryUser As String = "
            INSERT INTO Users (FullName, Username, Password, Role, PhoneNumber, Email, DateCreated, Availability)
            OUTPUT INSERTED.UserID
            VALUES (@fullname, @username, @password, 'Admin', @phone, @email, GETDATE(), @availability)
        "

            Dim userId As Integer
            Using cmdUser As New SqlCommand(queryUser, con)
                cmdUser.Parameters.AddWithValue("@fullname", TxtName.Text.Trim)
                cmdUser.Parameters.AddWithValue("@username", TxtUsername.Text.Trim)
                cmdUser.Parameters.AddWithValue("@password", TxtPassword.Text.Trim) ' ideally hash this
                cmdUser.Parameters.AddWithValue("@phone", TxtPhone.Text.Trim)
                cmdUser.Parameters.AddWithValue("@email", TxtUsername.Text.Trim & "@admin.com") ' or use a textbox if you want
                cmdUser.Parameters.AddWithValue("@availability", cmbAvailability.Text)

                userId = Convert.ToInt32(cmdUser.ExecuteScalar())
            End Using

            ' Insert into Admins
            Dim queryAdmin As String = "
            INSERT INTO Admins (UserID)
            VALUES (@userid)
        "
            Using cmdAdmin As New SqlCommand(queryAdmin, con)
                cmdAdmin.Parameters.AddWithValue("@userid", userId)
                cmdAdmin.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("Admin account added successfully.")
        LoadAdmins()
        ClearAdminInputs()
    End Sub

    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs) Handles AdminSearch.TextChanged
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String
            If AdminSearch.Text.Trim = "" Then
                query = "
                SELECT U.UserID, U.FullName, U.Username, U.PhoneNumber, U.Email, U.Availability
                FROM Users U
                INNER JOIN Admins A ON U.UserID = A.UserID
                WHERE U.Role = 'Admin'
                ORDER BY U.FullName
            "
            Else
                query = "
                SELECT U.UserID, U.FullName, U.Username, U.PhoneNumber, U.Email, U.Availability
                FROM Users U
                INNER JOIN Admins A ON U.UserID = A.UserID
                WHERE U.Role = 'Admin'
                  AND (
                        COALESCE(U.FullName,'') LIKE @search
                     OR COALESCE(U.Username,'') LIKE @search
                     OR COALESCE(U.PhoneNumber,'') LIKE @search
                     OR COALESCE(U.Email,'') LIKE @search
                     OR COALESCE(U.Availability,'') LIKE @search
                  )
                ORDER BY U.FullName
            "
            End If

            Using cmd As New SqlCommand(query, con)
                If AdminSearch.Text.Trim <> "" Then
                    cmd.Parameters.AddWithValue("@search", "%" & AdminSearch.Text.Trim & "%")
                End If

                Dim adapter As New SqlDataAdapter(cmd)
                Dim table As New DataTable()
                adapter.Fill(table)

                DataGridViewAdmins.DataSource = table
            End Using
        End Using

    End Sub
End Class