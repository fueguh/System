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
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "
            SELECT UserID, FullName, Username, PhoneNum, Email, Specialization, Availability
            FROM Users
            WHERE Role = 'Dentist'
        "

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)

            DGVDentists.DataSource = dt
        End Using
    End Sub

    Private Sub DGVDentists_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVDentists.CellContentClick
        ' ✅ Make sure the click is valid (not header row)
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DGVDentists.Rows(e.RowIndex)

            ' ✅ Populate textboxes/comboboxes with selected row values
            TxtName.Text = row.Cells("FullName").Value.ToString()
            TxtUsername.Text = row.Cells("Username").Value.ToString()
            TxtPhone.Text = row.Cells("PhoneNum").Value.ToString()
            TxtEmail.Text = row.Cells("Email").Value.ToString()
            TxtSpecialization.Text = row.Cells("Specialization").Value.ToString()
            cmbAvailability.Text = row.Cells("Availability").Value.ToString()
        End If

    End Sub

    Private Sub Clearform()
        TxtName.Text = ""
        txtSpecialization.Text = ""
        TxtPhone.Text = ""
        TxtEmail.Text = ""
        cmbAvailability.Text = ""
        selectedDentistID = 0
    End Sub

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        If Dashboard Is Nothing Then
            Dashboard = New AdminDashboard()
        End If

        Dashboard.Show()
        Me.Hide()
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

        ' ✅ Hash password
        Dim hashedPassword As String = HashPassword(TxtPassword.Text)

        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "
            INSERT INTO Users (FullName, Username, Password, Role, PhoneNum, Email, DateCreated, Specialization, Availability)
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
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim query As String = "SELECT COUNT(*) FROM Users WHERE Username = @username"
            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@username", username)
            Dim count As Integer = CInt(cmd.ExecuteScalar())
            Return count > 0
        End Using
    End Function

End Class