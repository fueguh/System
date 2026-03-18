Imports System.Data.SqlClient

Public Class AvailableAppointments
    Private connectionString As String = My.Settings.DentalDBConnection2

    ' === 1. FORM LOAD ===
    Private Sub AvailableAppointments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTodaysAppointments()

    End Sub

    ' === 2. DATA LOADING ===
    Private Sub LoadTodaysAppointments()
        Try
            Using con As New SqlConnection(connectionString)
                con.Open()

                ' Base Query using STRING_AGG for services
                Dim query As String = "
                    SELECT  
                        A.AppointmentID,  
                        A.PatientID,  
                        A.UserID,
                        P.FullName AS Patient,  
                        A.StartTime,  
                        STRING_AGG(S.ServiceName, ', ') AS Services,
                        A.Status,
                        U.FullName AS DentistName
                    FROM Appointments A
                    LEFT JOIN Patients P ON A.PatientID = P.PatientID
                    LEFT JOIN Users U ON A.UserID = U.UserID
                    LEFT JOIN AppointmentServices ASV ON A.AppointmentID = ASV.AppointmentID
                    LEFT JOIN Services S ON ASV.ServiceID = S.ServiceID
                    WHERE A.Date = CAST(GETDATE() AS DATE)
                    AND A.Status <> 'Cancelled'
                    AND NOT EXISTS (SELECT 1 FROM Receipts R WHERE R.AppointmentID = A.AppointmentID)"

                ' ROLE CHECK: Filter for Dentists, Admins see all
                If SystemSession.LoggedInRole <> "Admin" Then
                    query &= " AND A.UserID = @dentistID"
                End If

                query &= " GROUP BY A.AppointmentID, A.PatientID, A.UserID, P.FullName, A.StartTime, A.Status, U.FullName
                           ORDER BY A.StartTime ASC"

                Using cmd As New SqlCommand(query, con)
                    If SystemSession.LoggedInRole <> "Admin" Then
                        cmd.Parameters.AddWithValue("@dentistID", SystemSession.LoggedInUserID)
                    End If

                    Dim da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    dgvTodaysAppointments.DataSource = dt

                    ' UI: Hide internal ID columns from the user
                    If dgvTodaysAppointments.Columns.Contains("AppointmentID") Then dgvTodaysAppointments.Columns("AppointmentID").Visible = False
                    If dgvTodaysAppointments.Columns.Contains("PatientID") Then dgvTodaysAppointments.Columns("PatientID").Visible = False
                    If dgvTodaysAppointments.Columns.Contains("UserID") Then dgvTodaysAppointments.Columns("UserID").Visible = False
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading appointments: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' === 3. ACTION: START TREATMENT ===
    Private Sub btnStartTreatment_Click(sender As Object, e As EventArgs) Handles btnStartTreatment.Click
        If dgvTodaysAppointments.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an appointment from the list first.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim row = dgvTodaysAppointments.SelectedRows(0)
        Dim apptID As Integer = CInt(row.Cells("AppointmentID").Value)
        Dim patID As Integer = CInt(row.Cells("PatientID").Value)
        Dim assignedDentistID As Integer = CInt(row.Cells("UserID").Value)
        Dim patName As String = row.Cells("Patient").Value.ToString()

        UpdateAppointmentStatus(apptID, "Ongoing")
        SystemSession.LogAudit($"Started Treatment for {patName}", "AvailableAppointments")

        ' --- THE FIX ---
        ' 1. Create form with NO arguments
        Dim frm As New TreatmentRecords()

        ' 2. Fill the public variables manually
        frm.PassedAppointmentID = apptID
        frm.PassedPatientID = patID
        frm.CurrentPatientName = patName
        frm.PassedDentistID = assignedDentistID

        ' 3. Show it
        Me.Hide()
        frm.Show()
        ' --- END FIX ---

        AddHandler frm.FormClosed, Sub(s, args)
                                       SystemSession.NavigateToDashboard(Me)
                                   End Sub
    End Sub

    ' === 4. NAVIGATION ===
    Private Sub btnBack1_Click(sender As Object, e As EventArgs) Handles btnBack1.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub
    ' === 5. DOUBLE CLICK SHORTCUT ===
    Private Sub dgvTodaysAppointments_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTodaysAppointments.CellDoubleClick
        ' Check if the user double-clicked a valid row (not the header)
        If e.RowIndex >= 0 Then
            ' Programmatically trigger the Start Treatment button click
            btnStartTreatment.PerformClick()
        End If
    End Sub
    ' === 6. DATABASE HELPERS ===

    Private Sub UpdateAppointmentStatus(id As Integer, status As String)
        Try
            Using con As New SqlConnection(connectionString)
                con.Open()
                Dim cmd As New SqlCommand("UPDATE Appointments SET Status = @status WHERE AppointmentID = @id", con)
                cmd.Parameters.AddWithValue("@status", status)
                cmd.Parameters.AddWithValue("@id", id)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MessageBox.Show("Failed to update appointment status: " & ex.Message)
        End Try
    End Sub
End Class