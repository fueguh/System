Imports System.Data.SqlClient

Public Class TreatmentRecords
    Private Sub TreatmentRecords_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadRecords()
        LoadPatients()
        LoadDentists()
        ClearForm()

        ' Apply role-based behavior using SystemSession directly
        If SystemSession.LoggedInRole = "Dentist" Then
            CmbDentist.SelectedValue = SystemSession.LoggedInUserID
            CmbDentist.Enabled = False
        End If
    End Sub
    Private Sub ClearForm()
        TxtTreatmentNotes.Clear()
        TxtPrescriptions.Clear()
        TxtProceduresDone.Clear()
        PicXrayPreview.Image = Nothing
        imagePath = ""

        ' Reset combo boxes
        CmbPatient.SelectedIndex = -1

        ' Only reset dentist combo if NOT dentist
        If SystemSession.LoggedInRole <> "Dentist" Then
            CmbDentist.SelectedIndex = -1
        End If
    End Sub
    Private Sub LoadComboBox(combo As ComboBox, query As String, display As String, value As String)
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim dt As New DataTable()
            Using da As New SqlDataAdapter(query, con)
                da.Fill(dt)
            End Using
            combo.DataSource = dt
            combo.DisplayMember = display
            combo.ValueMember = value
            combo.SelectedIndex = -1
        End Using
    End Sub

    Private Sub LoadPatients()
        LoadComboBox(CmbPatient, "SELECT PatientID, FullName FROM Patients", "FullName", "PatientID")
    End Sub

    Private Sub LoadDentists()
        Dim query As String = ""

        If SystemSession.LoggedInRole = "Dentist" Then
            query = $"SELECT UserID, FullName FROM Users WHERE UserID = {SystemSession.LoggedInUserID}"
        ElseIf SystemSession.LoggedInRole = "Admin" Then
            query = "SELECT UserID, FullName FROM Users WHERE Role='Dentist'"
        Else
            ' fallback empty
            query = "SELECT TOP 0 UserID, FullName FROM Users"
        End If

        LoadComboBox(CmbDentist, query, "FullName", "UserID")

        ' Lock or enable
        If SystemSession.LoggedInRole = "Dentist" Then
            CmbDentist.SelectedValue = SystemSession.LoggedInUserID
            CmbDentist.Enabled = False
        Else
            CmbDentist.Enabled = True
        End If
    End Sub
    Private Sub LoadRecords()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim query As String = "
 SELECT PD.RecordID,
        P.FullName AS Patient,
        U.FullName AS Dentist,
        PD.TreatmentNotes,
        PD.Prescriptions,
        PD.ProceduresDone,
        PD.ImagePath,
        PD.DateCreated
 FROM TreatmentRecords PD
JOIN Patients P ON PD.PatientID = P.PatientID
JOIN Users U ON PD.UserID = U.UserID

"
            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            Guna2DataGridView1.DataSource = dt
        End Using
    End Sub

    Private imagePath As String = ""

    Private Sub BtnSaveRecord_Click(sender As Object, e As EventArgs) Handles BtnSaveRecord.Click
        If CmbPatient.SelectedIndex = -1 Or CmbDentist.SelectedIndex = -1 Then
            MessageBox.Show("Please select a patient and dentist.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Using con As New SqlConnection(My.Settings.DentalDBConnection2)
                con.Open()
                Using cmd As New SqlCommand("
                INSERT INTO TreatmentRecords 
                    (PatientID, UserID, TreatmentNotes, Prescriptions, ProceduresDone, ImagePath)
                VALUES (@patient, @dentist, @treatment, @prescriptions, @procedures, @image)
            ", con)
                    cmd.Parameters.AddWithValue("@patient", CInt(CmbPatient.SelectedValue))
                    cmd.Parameters.AddWithValue("@dentist", CInt(CmbDentist.SelectedValue))
                    cmd.Parameters.AddWithValue("@treatment", TxtTreatmentNotes.Text.Trim())
                    cmd.Parameters.AddWithValue("@prescriptions", TxtPrescriptions.Text.Trim())
                    cmd.Parameters.AddWithValue("@procedures", TxtProceduresDone.Text.Trim())
                    cmd.Parameters.AddWithValue("@image", imagePath)

                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Treatment record saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            SystemSession.LogAudit("Insert Treatment Record", "TreatmentRecords", SystemSession.LoggedInUserID, SystemSession.LoggedInFullName, SystemSession.LoggedInRole)
            LoadRecords()
            ClearForm()
        Catch ex As SqlException
            MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnAttachImage_Click(sender As Object, e As EventArgs) Handles BtnAttachImage.Click
        Dim ofd As New OpenFileDialog With {
        .Filter = "Image Files|*.jpg;*.png;*.jpeg"
    }

        If ofd.ShowDialog() = DialogResult.OK Then
            ' Load image safely into memory (avoids locking the original file)
            Dim tempImage As Image = Image.FromFile(ofd.FileName)
            PicXrayPreview.Image = New Bitmap(tempImage)
            tempImage.Dispose()  ' release file lock

            ' Optional: save a copy to a dedicated folder for your app
            Dim imagesFolder As String = "C:\DentalRecords\Images"
            If Not IO.Directory.Exists(imagesFolder) Then IO.Directory.CreateDirectory(imagesFolder)

            Dim destPath As String = IO.Path.Combine(imagesFolder, IO.Path.GetFileName(ofd.FileName))
            IO.File.Copy(ofd.FileName, destPath, True)  ' overwrite if exists

            ' Store the path of the copied image for saving in the database
            imagePath = destPath
        End If
    End Sub

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

    Private Sub Guna2HtmlLabel1_Click(sender As Object, e As EventArgs) Handles Guna2HtmlLabel1.Click

    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

    Private Sub Guna2CirclePictureBox2_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox2.Click
        SystemSession.NavigateToDashboard(Me)
        Me.Hide()
    End Sub
End Class