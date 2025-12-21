Imports System.Data.SqlClient

Public Class AdminDBDentists
    Private selectedDentistID As Integer = 0
    Private Sub AdminDBDentists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDentists()
        Clearform()
    End Sub

    Private Sub LoadDentists()
        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String = "SELECT * FROM Dentists ORDER BY FullName"

            Using da As New SqlDataAdapter(query, con)
                Dim dt As New DataTable()
                da.Fill(dt)
                DGVDentists.DataSource = dt
            End Using
        End Using
    End Sub

    Private Sub DGVDentists_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVDentists.CellContentClick
        If e.RowIndex >= 0 Then
            Dim row = DGVDentists.Rows(e.RowIndex)

            selectedDentistID = row.Cells("DentistID").Value
            txtFullName.Text = row.Cells("FullName").Value.ToString()
            txtSpecialization.Text = row.Cells("Specialization").Value.ToString()
            txtContact.Text = row.Cells("ContactNumber").Value.ToString()
            txtEmail.Text = row.Cells("Email").Value.ToString()
            cmbAvailability.Text = row.Cells("Availability").Value.ToString()
        End If

    End Sub

    Private Sub BTNAdd_Click(sender As Object, e As EventArgs) Handles BTNAdd.Click
        If txtFullName.Text.Trim = "" Or txtSpecialization.Text.Trim = "" Then
            MessageBox.Show("Full name and specialization are required.")
            Exit Sub
        End If

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String = "
            INSERT INTO Dentists (FullName, Specialization, ContactNumber, Email, Availability)
            VALUES (@name, @spec, @contact, @email, @avail)
        "

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@name", txtFullName.Text)
                cmd.Parameters.AddWithValue("@spec", txtSpecialization.Text)
                cmd.Parameters.AddWithValue("@contact", txtContact.Text)
                cmd.Parameters.AddWithValue("@email", txtEmail.Text)
                cmd.Parameters.AddWithValue("@avail", cmbAvailability.Text)
                cmd.ExecuteNonQuery()
            End Using
        End Using


        MessageBox.Show("Dentist added successfully.")
        LoadDentists()
        Clearform()

        'to reload the system overview in admin dashboard after input
        Dashboard?.LoadDashboardStats()
    End Sub

    Private Sub BTNUpdate_Click(sender As Object, e As EventArgs) Handles BTNUpdate.Click
        If selectedDentistID = 0 Then
            MessageBox.Show("Please select a dentist to update.")
            Exit Sub
        End If

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String = "
            UPDATE Dentists
            SET FullName=@name, Specialization=@spec, ContactNumber=@contact,
                Email=@email, Availability=@avail
            WHERE DentistID=@id
        "

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", selectedDentistID)
                cmd.Parameters.AddWithValue("@name", txtFullName.Text)
                cmd.Parameters.AddWithValue("@spec", txtSpecialization.Text)
                cmd.Parameters.AddWithValue("@contact", txtContact.Text)
                cmd.Parameters.AddWithValue("@email", txtEmail.Text)
                cmd.Parameters.AddWithValue("@avail", cmbAvailability.Text)
                cmd.ExecuteNonQuery()
            End Using
        End Using


        MessageBox.Show("Dentist updated successfully.")
        LoadDentists()
        Clearform()
        'to reload the system overview in admin dashboard after input
        Dashboard?.LoadDashboardStats()
    End Sub

    Private Sub BTNDelete_Click(sender As Object, e As EventArgs) Handles BTNDelete.Click
        If selectedDentistID = 0 Then
            MessageBox.Show("Please select a dentist to delete.")
            Exit Sub
        End If

        If MessageBox.Show("Are you sure you want to delete this dentist?",
                       "Confirm", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Using con As New SqlConnection("Server=FUEGA\SQLEXPRESS;Database=Dental;Trusted_Connection=True;")
            con.Open()

            Dim query As String = "DELETE FROM Dentists WHERE DentistID=@id"

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", selectedDentistID)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("Dentist deleted successfully.")
        LoadDentists()
        Clearform()
        'to reload the system overview in admin dashboard after input
        Dashboard?.LoadDashboardStats()
    End Sub

    Private Sub Clearform()
        txtFullName.Text = ""
        txtSpecialization.Text = ""
        txtContact.Text = ""
        txtEmail.Text = ""
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
End Class