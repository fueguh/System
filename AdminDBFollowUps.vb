Imports System.Data.SqlClient
Imports System.Text

Public Class AdminDBFollowUps

    Private connectionString As String = My.Settings.DentalDBConnection2
    Private dtFollowUps As DataTable

    Public PassedPatientID As Integer = 0
    Public PassedAppointmentID As Integer = 0

    Private Sub AdminDBFollowUps_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadFollowUps()
    End Sub

    ' ==========================================
    ' LOAD FOLLOW-UPS INTO GRID (FIXED BINDING)
    ' ==========================================
    Private Sub LoadFollowUps()

        Dim dt As New DataTable()

        Using conn As New SqlConnection(connectionString)
            conn.Open()

            Dim query As New StringBuilder()

            query.AppendLine("SELECT ")
            query.AppendLine("    f.FollowUpID,")
            query.AppendLine("    f.PatientID,")
            query.AppendLine("    p.FullName AS PatientName,")
            query.AppendLine("    f.AppointmentID,")
            query.AppendLine("    f.FollowUpDate,")
            query.AppendLine("    f.Reason,")
            query.AppendLine("    f.Status,")
            query.AppendLine("    f.CreatedAt")
            query.AppendLine("FROM dbo.PatientFollowUps f")
            query.AppendLine("INNER JOIN dbo.Patients p ON f.PatientID = p.PatientID")

            If PassedPatientID > 0 Then
                query.AppendLine("WHERE f.PatientID = @PatientID")
            End If

            query.AppendLine("ORDER BY f.FollowUpDate ASC")

            Using cmd As New SqlCommand(query.ToString(), conn)

                If PassedPatientID > 0 Then
                    cmd.Parameters.AddWithValue("@PatientID", PassedPatientID)
                End If

                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using

            End Using
        End Using

        ' ==========================================
        ' SAFE REBIND (FIXES GRID GLITCHES)
        ' ==========================================
        dgvFollowUps.DataSource = Nothing
        dgvFollowUps.AutoGenerateColumns = True
        dgvFollowUps.DataSource = dt

        dtFollowUps = dt

        ' Only format AFTER columns exist
        If dgvFollowUps.Columns.Count > 0 Then
            FormatGrid()
        End If

    End Sub

    ' ==========================================
    ' COLUMN FORMATTING ONLY (NO STYLING)
    ' ==========================================
    Private Sub FormatGrid()

        With dgvFollowUps

            If .Columns.Contains("PatientID") Then
                .Columns("PatientID").Visible = False
            End If

            If .Columns.Contains("AppointmentID") Then
                .Columns("AppointmentID").Visible = False
            End If

            If .Columns.Contains("PatientName") Then
                .Columns("PatientName").HeaderText = "Patient"
            End If

            If .Columns.Contains("FollowUpDate") Then
                .Columns("FollowUpDate").HeaderText = "Date"
            End If

            If .Columns.Contains("Reason") Then
                .Columns("Reason").HeaderText = "Reason"
            End If

            If .Columns.Contains("Status") Then
                .Columns("Status").HeaderText = "Status"
            End If

        End With

    End Sub

    Private Sub btnBack_Click_1(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

End Class