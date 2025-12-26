Imports System.Data.SqlClient

Public Class TreatmentRecords
    Public Property currentUserRole As String
    Public Property currentUserID As Integer

    Private Sub TreatmentRecords_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadRecords()
        LoadPatients()
        LoadDentists()
        ClearForm()

        ' Apply role-based behavior
        If currentUserRole = "Dentist" Then
            CmbDentist.SelectedValue = currentUserID
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
        If currentUserRole <> "Dentist" Then
            CmbDentist.SelectedIndex = -1
        End If
    End Sub

    Private Sub LoadPatients()
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "SELECT PatientID, FullName FROM Patients"
            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)

            CmbPatient.DataSource = dt
            CmbPatient.DisplayMember = "FullName"
            CmbPatient.ValueMember = "PatientID"
        End Using
    End Sub
    Private Sub LoadDentists()
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "
            SELECT UserID, FullName
            FROM Users
            WHERE Role = 'Dentist'
        "

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)

            CmbDentist.DataSource = dt
            CmbDentist.DisplayMember = "FullName"
            CmbDentist.ValueMember = "UserID"
        End Using
    End Sub
    Private Sub LoadRecords()
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "
            SELECT TR.RecordID, P.FullName AS Patient, U.FullName AS Dentist,
                   TR.TreatmentNotes, TR.Prescriptions, TR.ProceduresDone, TR.ImagePath, TR.DateCreated
            FROM TreatmentRecords TR
            JOIN Patients P ON TR.PatientID = P.PatientID
            JOIN Users U ON TR.DentistID = U.UserID
        "

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVRecords.DataSource = dt
        End Using
    End Sub

    Private imagePath As String = ""

    Private Sub BtnSaveRecord_Click(sender As Object, e As EventArgs) Handles BtnSaveRecord.Click
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "
            INSERT INTO TreatmentRecords (PatientID, DentistID, TreatmentNotes, Prescriptions, ProceduresDone, ImagePath)
            VALUES (@patient, @dentist, @notes, @prescriptions, @procedures, @image)
        "

            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@patient", CInt(CmbPatient.SelectedValue))
            cmd.Parameters.AddWithValue("@dentist", CInt(CmbDentist.SelectedValue))
            cmd.Parameters.AddWithValue("@notes", TxtTreatmentNotes.Text)
            cmd.Parameters.AddWithValue("@prescriptions", TxtPrescriptions.Text)
            cmd.Parameters.AddWithValue("@procedures", TxtProceduresDone.Text)
            cmd.Parameters.AddWithValue("@image", imagePath) ' optional

            cmd.ExecuteNonQuery()
        End Using

        MessageBox.Show("Treatment record saved.")
        LoadRecords()
        ClearForm()
    End Sub

    Private Sub BtnAttachImage_Click(sender As Object, e As EventArgs) Handles BtnAttachImage.Click
        Dim ofd As New OpenFileDialog()
        ofd.Filter = "Image Files|*.jpg;*.png;*.jpeg"

        If ofd.ShowDialog() = DialogResult.OK Then
            imagePath = ofd.FileName
            PicXrayPreview.Image = Image.FromFile(imagePath)
        End If
    End Sub

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub
End Class