Imports System.Data.SqlClient

Public Class TreatmentRecords
    ' Variables to hold passed-in context from the Appointment selection
    Private PassedAppointmentID As Integer = 0
    Private PassedPatientID As Integer = 0
    Private PassedDentistID As Integer = 0
    Public Sub New()
        InitializeComponent()
    End Sub

    ' Constructor: Receive the names and IDs when the form opens
    ' Usage: Dim frm As New TreatmentRecords(101, 5, "John Doe", 2, "Dr. Smith")
    Public Sub New(ByVal apptID As Integer, ByVal patID As Integer, ByVal patName As String, ByVal denID As Integer, ByVal denName As String)
        InitializeComponent()

        Me.PassedAppointmentID = apptID
        Me.PassedPatientID = patID
        Me.PassedDentistID = denID

        ' Set the Labels directly
        lblPatientName.Text = patName
        lblDentistName.Text = denName
    End Sub

    Private Sub TreatmentRecords_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadRecords()
        ClearForm()
    End Sub

    Private Sub ClearForm()
        TxtTreatmentNotes.Clear()
        TxtPrescriptions.Clear()
        TxtProceduresDone.Clear()
        ' Labels remain set to the current patient/dentist from the constructor
    End Sub

    Private Sub LoadRecords()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            ' Joining Appointments to show the visit date in the grid
            Dim query As String = "
                SELECT TR.RecordID,
                       P.FullName AS Patient,
                       U.FullName AS Dentist,
                       TR.TreatmentNotes,
                       TR.Prescriptions,
                       TR.ProceduresDone,
                       TR.DateCreated
                FROM TreatmentRecords TR
                JOIN Patients P ON TR.PatientID = P.PatientID
                JOIN Users U ON TR.UserID = U.UserID
                ORDER BY TR.DateCreated DESC"

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)

            Guna2DataGridView1.DataSource = dt

            ' Hide the Primary Key
            If Guna2DataGridView1.Columns.Contains("RecordID") Then
                Guna2DataGridView1.Columns("RecordID").Visible = False
            End If
        End Using
    End Sub

    Private Sub BtnSaveRecord_Click(sender As Object, e As EventArgs) Handles BtnSaveRecord.Click
        ' Validation: Ensure notes aren't empty
        If String.IsNullOrWhiteSpace(TxtTreatmentNotes.Text) Then
            MessageBox.Show("Please enter treatment notes.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Using con As New SqlConnection(My.Settings.DentalDBConnection2)
                con.Open()

                ' 1. Insert the Treatment Record using the IDs passed to the labels
                Dim sqlInsert As String = "
                    INSERT INTO TreatmentRecords 
                        (AppointmentID, PatientID, UserID, TreatmentNotes, Prescriptions, ProceduresDone, DateCreated)
                    VALUES (@apptID, @patient, @dentist, @treatment, @prescriptions, @procedures, GETDATE())"

                Using cmd As New SqlCommand(sqlInsert, con)
                    cmd.Parameters.AddWithValue("@apptID", PassedAppointmentID)
                    cmd.Parameters.AddWithValue("@patient", PassedPatientID)
                    cmd.Parameters.AddWithValue("@dentist", PassedDentistID)
                    cmd.Parameters.AddWithValue("@treatment", TxtTreatmentNotes.Text.Trim())
                    cmd.Parameters.AddWithValue("@prescriptions", TxtPrescriptions.Text.Trim())
                    cmd.Parameters.AddWithValue("@procedures", TxtProceduresDone.Text.Trim())
                    cmd.ExecuteNonQuery()
                End Using

                ' 2. Optional: Automatically mark the appointment as 'Completed'
                Dim sqlUpdate As String = "UPDATE Appointments SET Status = 'Completed' WHERE AppointmentID = @apptID"
                Using cmdUpdate As New SqlCommand(sqlUpdate, con)
                    cmdUpdate.Parameters.AddWithValue("@apptID", PassedAppointmentID)
                    cmdUpdate.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Treatment record saved and appointment completed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Audit Log
            SystemSession.LogAudit("Insert Treatment Record", "TreatmentRecords", SystemSession.LoggedInUserID, SystemSession.LoggedInFullName, SystemSession.LoggedInRole)

            LoadRecords()
            ClearForm()

        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnBack1_Click(sender As Object, e As EventArgs) Handles btnBack1.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

    ' Rest of your navigation logic (Back buttons, etc.)


End Class