Imports System.Data.SqlClient
Imports System.Text

Public Class AdminDBFollowUps

    Private connectionString As String = My.Settings.DentalDBConnection2
    Private dtFollowUps As DataTable

    Public PassedPatientID As Integer = 0
    Public PassedAppointmentID As Integer = 0
    Private selectedFollowUpID As Integer = 0
    Private selectedPatientName As String = ""
    Private selectedReason As String = ""
    Private selectedStatus As String = ""
    Private dvFollowUps As DataView
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
        dtFollowUps = dt
        dvFollowUps = dtFollowUps.DefaultView

        dgvFollowUps.AutoGenerateColumns = True
        dgvFollowUps.DataSource = dvFollowUps

        ' Only format AFTER columns exist
        If dgvFollowUps.Columns.Count > 0 Then
            FormatGrid()
        End If
        ClearForm()
    End Sub

    ' ==========================================
    ' COLUMN FORMATTING ONLY (NO STYLING)
    ' ==========================================
    Private Sub FormatGrid()
        With dgvFollowUps

            ' Hide the ID columns
            If .Columns.Contains("FollowUpID") Then
                .Columns("FollowUpID").Visible = False
            End If

            If .Columns.Contains("PatientID") Then
                .Columns("PatientID").Visible = False
            End If

            If .Columns.Contains("AppointmentID") Then
                .Columns("AppointmentID").Visible = False
            End If

            ' Optional: Clean headers (you can keep or remove these)
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

            If .Columns.Contains("CreatedAt") Then
                .Columns("CreatedAt").HeaderText = "Created At"
            End If

        End With
    End Sub
    Private Sub dgvFollowUps_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFollowUps.CellClick

        If e.RowIndex < 0 Then Exit Sub

        Dim row As DataGridViewRow = dgvFollowUps.Rows(e.RowIndex)

        ' Store ONLY internal ID (hidden use, not UI)
        selectedFollowUpID = Convert.ToInt32(row.Cells("FollowUpID").Value)

        ' =========================
        ' POPULATE DETAIL PANEL
        ' =========================
        lblPatientName.Text = row.Cells("PatientName").Value.ToString()

        lblFollowUpDate.Text = Convert.ToDateTime(row.Cells("FollowUpDate").Value).ToString("MMMM dd, yyyy")

        lblStatus.Text = row.Cells("Status").Value.ToString()

        lblReason.Text = row.Cells("Reason").Value.ToString()


    End Sub
    Private Sub ClearForm()

        ' Reset internal state
        selectedFollowUpID = 0
        selectedPatientName = ""
        selectedReason = ""
        selectedStatus = ""

        ' Clear detail panel
        lblPatientName.Text = ""
        lblFollowUpDate.Text = ""
        lblStatus.Text = ""
        lblReason.Text = ""

        ' Clear grid selection
        dgvFollowUps.ClearSelection()
        dgvFollowUps.CurrentCell = Nothing

    End Sub
    Private Sub btnBack_Click_1(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

    Private Sub btnDone_Click(sender As Object, e As EventArgs) Handles btnDone.Click

        If Not EnsureSelection() Then Exit Sub

        Using conn As New SqlConnection(connectionString)
            conn.Open()

            Dim query As String = "UPDATE dbo.PatientFollowUps SET Status = 'Done' WHERE FollowUpID = @id"

            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", selectedFollowUpID)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        LoadFollowUps()
        ClearForm()

    End Sub
    Private Function EnsureSelection() As Boolean
        If selectedFollowUpID = 0 Then
            MessageBox.Show("Please select a follow-up first.")
            Return False
        End If
        Return True
    End Function
    Private Sub btnMissed_Click(sender As Object, e As EventArgs) Handles btnMissed.Click

        If Not EnsureSelection() Then Exit Sub

        Using conn As New SqlConnection(connectionString)
            conn.Open()

            Dim query As String = "UPDATE dbo.PatientFollowUps SET Status = 'Missed' WHERE FollowUpID = @id"

            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", selectedFollowUpID)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        LoadFollowUps()
        ClearForm()

    End Sub
    Private Sub btnReschedule_Click(sender As Object, e As EventArgs) Handles btnReschedule.Click

        If Not EnsureSelection() Then Exit Sub

        Using conn As New SqlConnection(connectionString)
            conn.Open()

            Dim query As String =
        "UPDATE dbo.PatientFollowUps 
         SET FollowUpDate = @date, Status = 'Scheduled'
         WHERE FollowUpID = @id"

            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@date", dtpNewDate.Value.Date)
                cmd.Parameters.AddWithValue("@id", selectedFollowUpID)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        LoadFollowUps()
        ClearForm()

    End Sub
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

        If dvFollowUps Is Nothing Then Exit Sub

        Dim search As String = txtSearch.Text.Replace("'", "''")

        dvFollowUps.RowFilter =
        "PatientName LIKE '%" & search & "%' OR Reason LIKE '%" & search & "%' OR Status LIKE '%" & search & "%'"

    End Sub
End Class