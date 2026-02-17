Imports System.Data.SqlClient

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

    Private Sub LoadStaffs()
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "
            SELECT U.UserID, U.FullName, U.Username, U.PhoneNumber, U.Email, U.Availability
            FROM Users U
            INNER JOIN Staffs S ON U.UserID = S.UserID
            WHERE U.Role = 'Staff'
            ORDER BY U.FullName
        "

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DgvStaffs.DataSource = dt
        End Using
    End Sub
    Private Sub ClearStaffInputs()
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

    Private Sub AdminDBStaffMaintenance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStaffs()
        ClearStaffInputs()
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

        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            ' Insert into Users
            Dim queryUser As String = "
            INSERT INTO Users (FullName, Username, Password, Role, PhoneNumber, Email, DateCreated, Availability)
            OUTPUT INSERTED.UserID
            VALUES (@fullname, @username, @password, 'Staff', @phone, @email, GETDATE(), @availability)
        "

            Dim userId As Integer
            Using cmdUser As New SqlCommand(queryUser, con)
                cmdUser.Parameters.AddWithValue("@fullname", TxtName.Text.Trim)
                cmdUser.Parameters.AddWithValue("@username", TxtUsername.Text.Trim)
                cmdUser.Parameters.AddWithValue("@password", TxtPassword.Text.Trim) ' ideally hash this
                cmdUser.Parameters.AddWithValue("@phone", TxtPhone.Text.Trim)
                cmdUser.Parameters.AddWithValue("@email", TxtUsername.Text.Trim & "@staff.com") ' or use a textbox if you want
                cmdUser.Parameters.AddWithValue("@availability", cmbAvailability.Text)

                userId = Convert.ToInt32(cmdUser.ExecuteScalar())
            End Using

            ' Insert into Staffs
            Dim queryStaff As String = "
            INSERT INTO Staffs (UserID)
            VALUES (@userid)
        "
            Using cmdStaff As New SqlCommand(queryStaff, con)
                cmdStaff.Parameters.AddWithValue("@userid", userId)
                cmdStaff.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("Staff account added successfully.")
        LoadStaffs()
        ClearStaffInputs()
    End Sub

    Private Sub SearchStaff_TextChanged(sender As Object, e As EventArgs) Handles SearchStaff.TextChanged
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String
            If SearchStaff.Text.Trim = "" Then
                query = "
                SELECT U.UserID, U.FullName, U.Username, U.PhoneNumber, U.Email, U.Availability
                FROM Users U
                INNER JOIN Staffs S ON U.UserID = S.UserID
                WHERE U.Role = 'Staff'
                ORDER BY U.FullName
            "
            Else
                query = "
                SELECT U.UserID, U.FullName, U.Username, U.PhoneNumber, U.Email, U.Availability
                FROM Users U
                INNER JOIN Staffs S ON U.UserID = S.UserID
                WHERE U.Role = 'Staff'
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
End Class