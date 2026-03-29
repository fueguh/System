Imports System.Data.SqlClient

Public Class AdminDBTreatmentRecords
    ' Variables passed from the Appointments List
    Public PassedAppointmentID As Integer = 0
    Public PassedPatientID As Integer = 0
    Public PassedDentistID As Integer = 0
    Public CurrentPatientName As String = ""

    Public Sub New()
        InitializeComponent()
    End Sub

    ' Inside TreatmentRecords.vb
    ' Inside TreatmentRecords.vb
    Private Sub TreatmentRecords_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 1. Display the Patient's Name if we have one
        lblPatientName.Text = If(String.IsNullOrEmpty(CurrentPatientName), "Unknown Patient", CurrentPatientName)

        ' 2. THE FIX: Remove the MessageBox and the NavigateToDashboard.
        ' Instead of kicking you out, we just check if data exists.
        If PassedAppointmentID <= 0 Then
            ' Just show a warning on the label and stop the DB from loading.
            lblPatientName.Text = "No Appointment Selected (Manual View Mode)"
            lblPatientName.ForeColor = Color.Red

            ' Disable the save button so you don't get SQL errors, but STAY on the form.
            BtnSaveRecord.Enabled = False
            Return ' This stops the rest of the sub from running without closing the form.
        End If

        ' 3. Normal flow - only runs if PassedAppointmentID is greater than 0
        BtnSaveRecord.Enabled = True

        FetchAssignedDentist()
        LoadExistingTreatmentData()
    End Sub

    Private Sub FetchAssignedDentist()
        Try
            Using con As New SqlConnection(My.Settings.DentalDBConnection2)
                con.Open()
                ' Join Appointments with Users to get the assigned Dentist's name
                Dim sql As String = "SELECT A.UserID, U.FullName FROM Appointments A " &
                                   "JOIN Users U ON A.UserID = U.UserID " &
                                   "WHERE A.AppointmentID = @appt"

                Using cmd As New SqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@appt", PassedAppointmentID)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            ' Set the ID to ensure the save logic uses the assigned dentist
                            PassedDentistID = CInt(reader("UserID"))
                            ' Display the assigned dentist's name in your label
                            lblDentistName.Text = reader("FullName").ToString()
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            lblDentistName.Text = "Dentist Not Found"
        End Try
    End Sub

    Private Sub LoadExistingTreatmentData()
        Try
            Using con As New SqlConnection(My.Settings.DentalDBConnection2)
                con.Open()
                ' Using your actual table structure (Prescriptions is a column)
                Dim sql As String = "SELECT TreatmentNotes, ProceduresDone, Prescriptions FROM TreatmentRecords WHERE AppointmentID = @appt"
                Using cmd As New SqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@appt", PassedAppointmentID)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            TxtTreatmentNotes.Text = reader("TreatmentNotes").ToString()
                            TxtProceduresDone.Text = reader("ProceduresDone").ToString()
                            TxtPrescriptions.Text = reader("Prescriptions").ToString()

                            ' Change button text to show we are editing
                            BtnSaveRecord.Text = "Update Record"
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
        End Try
    End Sub

    Private Sub BtnSaveRecord_Click(sender As Object, e As EventArgs) Handles BtnSaveRecord.Click
        ' Validation to prevent SQL Foreign Key errors
        If PassedPatientID <= 0 Or PassedAppointmentID <= 0 Or PassedDentistID <= 0 Then
            MessageBox.Show("Cannot save: Missing record IDs. Please return to appointments.", "Invalid ID", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Try
            Using con As New SqlConnection(My.Settings.DentalDBConnection2)
                con.Open()

                ' 1. UPSERT logic
                Dim sqlTreat As String = "IF EXISTS (SELECT 1 FROM TreatmentRecords WHERE AppointmentID = @appt) " &
                                         "UPDATE TreatmentRecords SET TreatmentNotes = @notes, Prescriptions = @presc, ProceduresDone = @proc, DateCreated = GETDATE() WHERE AppointmentID = @appt " &
                                         "ELSE " &
                                         "INSERT INTO TreatmentRecords (AppointmentID, PatientID, UserID, TreatmentNotes, Prescriptions, ProceduresDone, DateCreated) " &
                                         "VALUES (@appt, @pat, @user, @notes, @presc, @proc, GETDATE())"

                Using cmd As New SqlCommand(sqlTreat, con)
                    cmd.Parameters.AddWithValue("@appt", PassedAppointmentID)
                    cmd.Parameters.AddWithValue("@pat", PassedPatientID)
                    cmd.Parameters.AddWithValue("@user", PassedDentistID) ' Uses the assigned doctor
                    cmd.Parameters.AddWithValue("@notes", TxtTreatmentNotes.Text.Trim())
                    cmd.Parameters.AddWithValue("@proc", TxtProceduresDone.Text.Trim())
                    cmd.Parameters.AddWithValue("@presc", TxtPrescriptions.Text.Trim())
                    cmd.ExecuteNonQuery()
                End Using

                ' 2. Mark Appointment as Completed
                Dim sqlStatus As String = "UPDATE Appointments SET Status = 'Completed' WHERE AppointmentID = @appt"
                Using cmdStatus As New SqlCommand(sqlStatus, con)
                    cmdStatus.Parameters.AddWithValue("@appt", PassedAppointmentID)
                    cmdStatus.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Record saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            SystemSession.NavigateToDashboard(Me)

        Catch ex As Exception
            MessageBox.Show("Save failed: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnBack1_Click(sender As Object, e As EventArgs) Handles btnBack1.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub
End Class