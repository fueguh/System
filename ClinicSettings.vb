Imports System.Data.SqlClient

Public Class ClinicSettings
    Private con As New SqlConnection(My.Settings.DentalDBConnection)
    'fill textboxes with existing data
    Private Sub ClinicSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim cmd As New SqlCommand("SELECT ClinicName, ClinicAddress, ContactNumber, Email, OperatingHours FROM ClinicInfo WHERE ClinicID=1", con)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                txtClinicName.Text = reader("ClinicName").ToString()
                txtClinicAddress.Text = reader("ClinicAddress").ToString()
                txtContactNumber.Text = reader("ContactNumber").ToString()
                txtEmail.Text = reader("Email").ToString()
                txtOperatingHours.Text = reader("OperatingHours").ToString()
            End If
        End Using
    End Sub
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            ' Check if row exists
            Dim checkCmd As New SqlCommand("SELECT COUNT(*) FROM ClinicInfo WHERE ClinicID=1", con)
            Dim exists As Integer = CInt(checkCmd.ExecuteScalar())

            Dim cmd As SqlCommand
            If exists > 0 Then
                ' Update existing row
                cmd = New SqlCommand("UPDATE ClinicInfo SET ClinicName=@name, ClinicAddress=@address, ContactNumber=@contact, Email=@mail, OperatingHours=@hours WHERE ClinicID=1", con)
            Else
                ' Insert new row
                cmd = New SqlCommand("INSERT INTO ClinicInfo (ClinicID, ClinicName, ClinicAddress, ContactNumber, Email, OperatingHours) VALUES (1, @name, @address, @contact, @mail, @hours)", con)
            End If

            cmd.Parameters.AddWithValue("@name", txtClinicName.Text)
            cmd.Parameters.AddWithValue("@address", txtClinicAddress.Text)
            cmd.Parameters.AddWithValue("@contact", txtContactNumber.Text)
            cmd.Parameters.AddWithValue("@mail", txtEmail.Text)
            cmd.Parameters.AddWithValue("@hours", txtOperatingHours.Text)

            cmd.ExecuteNonQuery()
        End Using
        'audit log
        SystemSession.LogAudit("Updated clinic settings.", "ClinicSettings")

        MessageBox.Show("Clinic settings saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        If SystemSession.LoggedInUserID = 0 OrElse SystemSession.LoggedInRole <> "Admin" Then
            Login.Show()
            Me.Hide()
            Exit Sub
        End If
        SystemSession.NavigateToDashboard(Me)
        Me.Hide()
    End Sub
End Class