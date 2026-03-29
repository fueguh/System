Imports System.Data.SqlClient

Public Class AdminDBTreatmentRecords

    ' =======================
    ' VARIABLES
    ' =======================
    Public PassedAppointmentID As Integer = 0
    Public PassedPatientID As Integer = 0
    Public PassedDentistID As Integer = 0
    Public CurrentPatientName As String = ""


    Public Sub New()
        InitializeComponent()
    End Sub


    ' =======================
    ' FORM LOAD
    ' =======================
    Private Sub TreatmentRecords_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lblPatientName.Text = If(String.IsNullOrEmpty(CurrentPatientName), "Unknown Patient", CurrentPatientName)

        If PassedAppointmentID <= 0 Then
            lblPatientName.Text = "No Appointment Selected (Manual View Mode)"
            lblPatientName.ForeColor = Color.Red
            BtnSaveRecord.Enabled = False
            Return
        End If

        BtnSaveRecord.Enabled = True

        ' Follow-up defaults
        chkNeedsFollowUp.Checked = False
        dtpFollowUpDate.Enabled = False
        txtFollowUpReason.Enabled = False
        dtpFollowUpDate.Value = DateTime.Today.AddDays(1)

        ' Load data
        LoadAppointmentStatus()
        FetchAssignedDentist()
        LoadExistingTreatmentData()
        LoadFollowUp()

    End Sub


    ' =======================
    ' DATA LOADING
    ' =======================
    Private Sub LoadAppointmentStatus()
        Try
            Using con As New SqlConnection(My.Settings.DentalDBConnection2)
                con.Open()

                Dim sql As String = "SELECT Status FROM Appointments WHERE AppointmentID = @appt"

                Using cmd As New SqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@appt", PassedAppointmentID)
                    Dim result = cmd.ExecuteScalar()

                    lblStatus.Text = If(result IsNot Nothing, result.ToString(), "Unknown")
                End Using
            End Using

        Catch
            lblStatus.Text = "Error"
        End Try
    End Sub


    Private Sub FetchAssignedDentist()
        Try
            Using con As New SqlConnection(My.Settings.DentalDBConnection2)
                con.Open()

                Dim sql As String =
                    "SELECT A.UserID, U.FullName FROM Appointments A " &
                    "JOIN Users U ON A.UserID = U.UserID " &
                    "WHERE A.AppointmentID = @appt"

                Using cmd As New SqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@appt", PassedAppointmentID)

                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            PassedDentistID = CInt(reader("UserID"))
                            lblDentistName.Text = reader("FullName").ToString()
                        End If
                    End Using
                End Using
            End Using

        Catch
            lblDentistName.Text = "Dentist Not Found"
        End Try
    End Sub


    Private Sub LoadExistingTreatmentData()
        Try
            Using con As New SqlConnection(My.Settings.DentalDBConnection2)
                con.Open()

                Dim sql As String =
                    "SELECT TreatmentNotes, ProceduresDone, Prescriptions " &
                    "FROM TreatmentRecords WHERE AppointmentID = @appt"

                Using cmd As New SqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@appt", PassedAppointmentID)

                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            TxtTreatmentNotes.Text = reader("TreatmentNotes").ToString()
                            TxtProceduresDone.Text = reader("ProceduresDone").ToString()
                            TxtPrescriptions.Text = reader("Prescriptions").ToString()

                            BtnSaveRecord.Text = "Update Record"
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
        End Try
    End Sub


    Private Sub LoadFollowUp()
        Try
            Using con As New SqlConnection(My.Settings.DentalDBConnection2)
                con.Open()

                Dim sql As String =
                    "SELECT TOP 1 FollowUpDate, Reason 
                     FROM PatientFollowUps 
                     WHERE AppointmentID = @appt
                     ORDER BY FollowUpDate DESC"

                Using cmd As New SqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@appt", PassedAppointmentID)

                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            chkNeedsFollowUp.Checked = True
                            dtpFollowUpDate.Enabled = True
                            txtFollowUpReason.Enabled = True

                            dtpFollowUpDate.Value = CDate(reader("FollowUpDate"))
                            txtFollowUpReason.Text = reader("Reason").ToString()
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading follow-up: " & ex.Message)
        End Try
    End Sub


    ' =======================
    ' SAVE LOGIC (WITH AUDIT)
    ' =======================
    Private Sub BtnSaveRecord_Click(sender As Object, e As EventArgs) Handles BtnSaveRecord.Click

        ' Validate IDs
        If PassedPatientID <= 0 Or PassedAppointmentID <= 0 Or PassedDentistID <= 0 Then
            MessageBox.Show("Cannot save: Missing record IDs.", "Invalid ID")
            Exit Sub
        End If

        ' Follow-up validation
        If chkNeedsFollowUp.Checked Then
            If txtFollowUpReason.Text.Trim() = "" Then
                MessageBox.Show("Please enter follow-up reason.")
                Exit Sub
            End If

            If dtpFollowUpDate.Value < DateTime.Today Then
                MessageBox.Show("Follow-up date cannot be in the past.")
                Exit Sub
            End If
        End If


        ' =======================
        ' GET OLD VALUES (AUDIT)
        ' =======================
        Dim oldNotes As String = ""
        Dim oldProcedures As String = ""
        Dim oldPrescriptions As String = ""

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim sqlOld As String =
                "SELECT TreatmentNotes, ProceduresDone, Prescriptions 
                 FROM TreatmentRecords WHERE AppointmentID = @appt"

            Using cmdOld As New SqlCommand(sqlOld, con)
                cmdOld.Parameters.AddWithValue("@appt", PassedAppointmentID)

                Using reader = cmdOld.ExecuteReader()
                    If reader.Read() Then
                        oldNotes = reader("TreatmentNotes").ToString()
                        oldProcedures = reader("ProceduresDone").ToString()
                        oldPrescriptions = reader("Prescriptions").ToString()
                    End If
                End Using
            End Using
        End Using


        ' =======================
        ' SAVE TO DATABASE
        ' =======================
        Try
            Using con As New SqlConnection(My.Settings.DentalDBConnection2)
                con.Open()

                ' UPSERT Treatment
                Dim sqlTreat As String =
                    "IF EXISTS (SELECT 1 FROM TreatmentRecords WHERE AppointmentID = @appt) " &
                    "UPDATE TreatmentRecords SET TreatmentNotes=@notes, Prescriptions=@presc, ProceduresDone=@proc, DateCreated=GETDATE() WHERE AppointmentID=@appt " &
                    "ELSE " &
                    "INSERT INTO TreatmentRecords (AppointmentID, PatientID, UserID, TreatmentNotes, Prescriptions, ProceduresDone, DateCreated) " &
                    "VALUES (@appt, @pat, @user, @notes, @presc, @proc, GETDATE())"

                Using cmd As New SqlCommand(sqlTreat, con)
                    cmd.Parameters.AddWithValue("@appt", PassedAppointmentID)
                    cmd.Parameters.AddWithValue("@pat", PassedPatientID)
                    cmd.Parameters.AddWithValue("@user", PassedDentistID)
                    cmd.Parameters.AddWithValue("@notes", TxtTreatmentNotes.Text.Trim())
                    cmd.Parameters.AddWithValue("@proc", TxtProceduresDone.Text.Trim())
                    cmd.Parameters.AddWithValue("@presc", TxtPrescriptions.Text.Trim())
                    cmd.ExecuteNonQuery()
                End Using

                ' Update Appointment Status
                Using cmdStatus As New SqlCommand("UPDATE Appointments SET Status='Completed' WHERE AppointmentID=@appt", con)
                    cmdStatus.Parameters.AddWithValue("@appt", PassedAppointmentID)
                    cmdStatus.ExecuteNonQuery()
                End Using

                ' UPSERT Follow-up
                If chkNeedsFollowUp.Checked Then
                    Dim sqlFollow As String =
                        "IF EXISTS (SELECT 1 FROM PatientFollowUps WHERE AppointmentID = @appt)
                            UPDATE PatientFollowUps 
                            SET FollowUpDate = @date, Reason = @reason
                            WHERE AppointmentID = @appt
                         ELSE
                            INSERT INTO PatientFollowUps (PatientID, AppointmentID, FollowUpDate, Reason)
                            VALUES (@pat, @appt, @date, @reason)"

                    Using cmdFollow As New SqlCommand(sqlFollow, con)
                        cmdFollow.Parameters.AddWithValue("@pat", PassedPatientID)
                        cmdFollow.Parameters.AddWithValue("@appt", PassedAppointmentID)
                        cmdFollow.Parameters.AddWithValue("@date", dtpFollowUpDate.Value)
                        cmdFollow.Parameters.AddWithValue("@reason", txtFollowUpReason.Text.Trim())
                        cmdFollow.ExecuteNonQuery()
                    End Using
                End If

            End Using


            ' =======================
            ' REFRESH UI
            ' =======================
            LoadAppointmentStatus()
            LoadExistingTreatmentData()
            LoadFollowUp()


            ' =======================
            ' AUDIT LOG
            ' =======================
            Dim changes As New List(Of String)

            If oldNotes <> TxtTreatmentNotes.Text.Trim() Then changes.Add("Notes updated")
            If oldProcedures <> TxtProceduresDone.Text.Trim() Then changes.Add("Procedures updated")
            If oldPrescriptions <> TxtPrescriptions.Text.Trim() Then changes.Add("Prescriptions updated")

            If chkNeedsFollowUp.Checked Then
                changes.Add("Follow-up on " & dtpFollowUpDate.Value.ToShortDateString())
            End If

            Dim actionType As String =
                If(oldNotes = "" AndAlso oldProcedures = "" AndAlso oldPrescriptions = "", "Added", "Updated")

            Dim changeSummary As String =
                If(changes.Count > 0, String.Join(", ", changes), "No changes")

            Dim auditMsg As String =
                $"{actionType} Treatment | Patient: {lblPatientName.Text} | {changeSummary}"

            If auditMsg.Length > 300 Then auditMsg = auditMsg.Substring(0, 297) & "..."

            SystemSession.LogAudit(
                auditMsg,
                "Treatment Records",
                SystemSession.LoggedInUserID,
                SystemSession.LoggedInFullName,
                SystemSession.LoggedInRole
            )

            MessageBox.Show("Record saved successfully!", "Success")

        Catch ex As Exception
            MessageBox.Show("Save failed: " & ex.Message)
        End Try

    End Sub


    ' =======================
    ' CLEAR FORM (SAFE)
    ' =======================
    Private Sub ClearForm()

        TxtTreatmentNotes.Clear()
        TxtProceduresDone.Clear()
        TxtPrescriptions.Clear()

        chkNeedsFollowUp.Checked = False
        txtFollowUpReason.Clear()

        dtpFollowUpDate.Enabled = False
        txtFollowUpReason.Enabled = False
        dtpFollowUpDate.Value = DateTime.Today.AddDays(1)

        BtnSaveRecord.Text = "Save Record"

    End Sub


    ' =======================
    ' UI EVENTS
    ' =======================
    Private Sub chkNeedsFollowUp_CheckedChanged(sender As Object, e As EventArgs) Handles chkNeedsFollowUp.CheckedChanged
        dtpFollowUpDate.Enabled = chkNeedsFollowUp.Checked
        txtFollowUpReason.Enabled = chkNeedsFollowUp.Checked

        If Not chkNeedsFollowUp.Checked Then
            txtFollowUpReason.Clear()
            dtpFollowUpDate.Value = DateTime.Today.AddDays(1)
        End If
    End Sub


    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
    End Sub


    Private Sub btnBack1_Click(sender As Object, e As EventArgs) Handles btnBack1.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

End Class