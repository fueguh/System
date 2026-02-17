Imports System.Data.SqlClient

Public Class AdminDBPatients
    Private selectedPatientID As Integer = 0
    Dim connectionString As String = My.Settings.DentalDBConnection

    Private Sub AdminDBPatients_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPatients()
        Clearform()

        ' Only Admin/Staff access, dentists have read-only access
        If Not (SystemSession.LoggedInRole = "Admin" OrElse SystemSession.LoggedInRole = "Staff") Then
            SystemSession.SetFormReadOnly(Me)
        End If
    End Sub
    Private Sub LoadPatients()
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "
            SELECT PatientID, FullName, BirthDate, ContactNumber, Email, Address,DateRegistered, NoteAllergy
            FROM Patients
            WHERE IsActive = 1
            ORDER BY PatientID
        "

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVPatients.DataSource = dt
        End Using
    End Sub

    Private Sub Clearform()
        selectedPatientID = 0
        txtFullName.Text = ""
        dtpBirthDate.Value = DateTime.Now
        txtContact.Text = ""
        txtEmail.Text = ""
        txtAddress.Text = ""
        txtAllergy.Text = ""
    End Sub
    Private Sub DGVPatients_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then
            Dim row = DGVPatients.Rows(e.RowIndex)

            selectedPatientID = row.Cells("PatientID").Value
            txtFullName.Text = row.Cells("FullName").Value.ToString()
            dtpBirthDate.Value = row.Cells("BirthDate").Value
            txtContact.Text = row.Cells("ContactNumber").Value.ToString()
            txtEmail.Text = row.Cells("Email").Value.ToString()
            txtAddress.Text = row.Cells("Address").Value.ToString()
            txtAllergy.Text = If(row.Cells("NoteAllergy").Value IsNot DBNull.Value, row.Cells("NoteAllergy").Value.ToString(), "")
        End If

    End Sub

    Private Sub BTNAdd_Click(sender As Object, e As EventArgs) Handles BTNAdd.Click
        If txtFullName.Text.Trim = "" Or txtContact.Text.Trim = "" Then
            MessageBox.Show("Full name and contact number are required.")
            Exit Sub
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "
            INSERT INTO Patients (FullName, BirthDate, ContactNumber, Email, Address, NoteAllergy)
            VALUES (@name, @birth, @contact, @email, @address, @allergy)
        "

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@name", txtFullName.Text)
                cmd.Parameters.AddWithValue("@birth", dtpBirthDate.Value.Date)
                cmd.Parameters.AddWithValue("@contact", txtContact.Text)
                cmd.Parameters.AddWithValue("@email", txtEmail.Text)
                cmd.Parameters.AddWithValue("@address", txtAddress.Text)
                ' New allergy note parameter (optional)
                If txtAllergy.Text.Trim = "" Then
                    cmd.Parameters.AddWithValue("@allergy", DBNull.Value)
                Else
                    cmd.Parameters.AddWithValue("@allergy", txtAllergy.Text)
                End If

                cmd.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("Patient added successfully.")
        SystemSession.LogAudit("Patient Added", "Patient Management",
                       SystemSession.LoggedInUserID,
                       SystemSession.LoggedInFullName,
                       SystemSession.LoggedInRole)

        LoadPatients()
        Clearform()
        'to reload the system overview in admin dashboard after input
        Dashboard?.LoadDashboardStats()
    End Sub

    Private Sub BTNUpdate_Click(sender As Object, e As EventArgs) Handles BTNUpdate.Click
        If selectedPatientID = 0 Then
            MessageBox.Show("Please select a patient to update.")
            Exit Sub
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "
            UPDATE Patients
            SET FullName=@name, BirthDate=@birth, ContactNumber=@contact,
                Email=@email, Address=@address, NoteAllergy=@allergy
            WHERE PatientID=@id
        "

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", selectedPatientID)
                cmd.Parameters.AddWithValue("@name", txtFullName.Text)
                cmd.Parameters.AddWithValue("@birth", dtpBirthDate.Value.Date)
                cmd.Parameters.AddWithValue("@contact", txtContact.Text)
                cmd.Parameters.AddWithValue("@email", txtEmail.Text)
                cmd.Parameters.AddWithValue("@address", txtAddress.Text)
                ' New allergy note parameter (optional)
                If txtAllergy.Text.Trim = "" Then
                    cmd.Parameters.AddWithValue("@allergy", DBNull.Value)
                Else
                    cmd.Parameters.AddWithValue("@allergy", txtAllergy.Text)
                End If

                cmd.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("Patient updated successfully.")
        SystemSession.LogAudit("Patient Updated", "Patient Management",
                       SystemSession.LoggedInUserID,
                       SystemSession.LoggedInFullName,
                       SystemSession.LoggedInRole)
        LoadPatients()
        Clearform()

        'to reload the system overview in admin dashboard after input
        Dashboard?.LoadDashboardStats()
    End Sub

    Private Sub BTNDelete_Click(sender As Object, e As EventArgs) Handles BTNDelete.Click
        If selectedPatientID = 0 Then
            MessageBox.Show("Please select a patient to delete.")
            Exit Sub
        End If

        If MessageBox.Show("Are you sure you want to delete this patient?",
                       "Confirm", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "UPDATE Patients SET IsActive = 0 WHERE PatientID = @PatientID"

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@PatientID", selectedPatientID)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("Patient deleted successfully.")
        SystemSession.LogAudit("Patient Deleted", "Patient Management",
                       SystemSession.LoggedInUserID,
                       SystemSession.LoggedInFullName,
                       SystemSession.LoggedInRole)


        LoadPatients()
        Clearform()

        'to reload the system overview in admin dashboard after input
        Dashboard?.LoadDashboardStats()
    End Sub

    Private Sub DeactivatePatient(patientId As Integer)
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim query As String = "UPDATE Patients SET IsActive = 0 WHERE PatientID = @PatientID"
            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@PatientID", patientId)
            cmd.ExecuteNonQuery()
        End Using
    End Sub


    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

    Private Sub DGVPatients_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles DGVPatients.CellContentClick
        ' Prevent errors if user clicks the header row
        If e.RowIndex < 0 Then Exit Sub

        Dim row As DataGridViewRow = DGVPatients.Rows(e.RowIndex)

        ' Assuming your DataGridView columns match your table fields
        selectedPatientID = Convert.ToInt32(row.Cells("PatientID").Value)

        txtFullName.Text = row.Cells("FullName").Value.ToString()
        dtpBirthDate.Value = Convert.ToDateTime(row.Cells("BirthDate").Value)
        txtContact.Text = row.Cells("ContactNumber").Value.ToString()
        txtEmail.Text = row.Cells("Email").Value.ToString()
        txtAddress.Text = row.Cells("Address").Value.ToString()

        ' New allergy field
        If row.Cells("NoteAllergy").Value IsNot Nothing Then
            txtAllergy.Text = row.Cells("NoteAllergy").Value.ToString()
        Else
            txtAllergy.Clear()
        End If
    End Sub

    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Guna2TextBox1.TextChanged
        Dim query As String = "SELECT PatientID, FullName, BirthDate, ContactNumber, Email, Address, DateRegistered, IsActive, NoteAllergy
                               FROM dbo.Patients 
                               WHERE FullName LIKE @search OR ContactNumber LIKE @search OR Email LIKE @search OR NoteAllergy LIKE @search"

        Using con As New SqlConnection(connectionString),
              cmd As New SqlCommand(query, con)

            cmd.Parameters.AddWithValue("@search", "%" & Guna2TextBox1.Text & "%")

            Dim adapter As New SqlDataAdapter(cmd)
            Dim table As New DataTable()
            adapter.Fill(table)

            DGVPatients.DataSource = table
        End Using
    End Sub
End Class