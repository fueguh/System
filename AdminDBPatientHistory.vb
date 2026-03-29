Imports System.Data.SqlClient
Imports System.Text

Public Class AdminDBPatientHistory
    Private connectionString As String = My.Settings.DentalDBConnection2

    Private selectedPatientID As Integer = 0
    Private selectedAppointmentID As Integer = 0

    Private isLoading As Boolean = False

#Region "FORM LOAD"

    Private Sub AdminDBPatientHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadPatients()

            If selectedPatientID > 0 Then
                cboPatients.SelectedValue = selectedPatientID
                LoadPatientData(selectedPatientID)
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "LOAD PATIENTS"

    Private dtAllPatients As DataTable   ' <-- Add this private field at class level

    Private Sub LoadPatients()
        isLoading = True

        Dim query As String = "
        SELECT PatientID, FullName
        FROM Patients
        ORDER BY FullName"

        dtAllPatients = New DataTable()

        Try
            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, conn)
                    Dim da As New SqlDataAdapter(cmd)
                    da.Fill(dtAllPatients)
                End Using
            End Using

            ' Initial binding - show all patients
            cboPatients.DataSource = dtAllPatients
            cboPatients.DisplayMember = "FullName"
            cboPatients.ValueMember = "PatientID"
            cboPatients.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show("Error loading patients: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            isLoading = False
        End Try
    End Sub


#End Region

#Region "PATIENT SEARCH"

    Private Sub txtSearchPatient_TextChanged(sender As Object, e As EventArgs) Handles txtSearchPatient.TextChanged
        If isLoading Then Exit Sub
        If dtAllPatients Is Nothing Then Exit Sub

        Try
            isLoading = True

            Dim dv As New DataView(dtAllPatients)

            If String.IsNullOrWhiteSpace(txtSearchPatient.Text) Then
                dv.RowFilter = ""   ' Show all
            Else
                ' Safe filter - escape single quotes
                Dim searchText As String = txtSearchPatient.Text.Trim().Replace("'", "''")
                dv.RowFilter = "FullName LIKE '%" & searchText & "%'"
            End If

            cboPatients.DataSource = dv
            cboPatients.DisplayMember = "FullName"
            cboPatients.ValueMember = "PatientID"

            ' Optional: Auto-open dropdown when typing
            ' cboPatients.DroppedDown = True

        Catch ex As Exception
            MessageBox.Show("Error filtering patients: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            isLoading = False
        End Try
    End Sub

#End Region

#Region "PATIENT SELECTION"

    Private Sub cboPatients_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPatients.SelectedIndexChanged
        If isLoading Then Exit Sub
        If cboPatients.SelectedValue Is Nothing Then Exit Sub

        Dim value As Object = cboPatients.SelectedValue
        If Not IsNumeric(value) Then Exit Sub

        selectedPatientID = Convert.ToInt32(value)

        ResetAllSelections()
        LoadPatientData(selectedPatientID)
    End Sub

#End Region

#Region "MASTER LOADER"

    Private Sub LoadPatientData(patientID As Integer)
        Try
            LoadAppointments(patientID)
            ClearTreatmentFields()
        Catch ex As Exception
            MessageBox.Show("Error loading patient data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "APPOINTMENTS"

    Private Sub LoadAppointments(patientID As Integer)
        Dim query As String = "
        SELECT AppointmentID, [Date], Status
        FROM Appointments
        WHERE PatientID = @PatientID
        ORDER BY [Date] DESC"

        Dim dt As New DataTable()

        Try
            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@PatientID", patientID)

                    Dim da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using

            dgvAppointments.DataSource = dt

            ' Format the grid
            FormatAppointmentsGrid()

            dgvAppointments.ClearSelection()
            dgvAppointments.CurrentCell = Nothing

            If dt.Rows.Count = 0 Then
                ' Optional: show a label "No appointments found"
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading appointments: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FormatAppointmentsGrid()
        If dgvAppointments.Columns.Count = 0 Then Exit Sub

        With dgvAppointments
            .Columns("AppointmentID").Visible = False
            .Columns("Date").HeaderText = "Appointment Date"
            .Columns("Date").DefaultCellStyle.Format = "dd MMM yyyy  hh:mm tt"   ' Customize as needed
            .Columns("Status").HeaderText = "Status"

            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        End With
    End Sub

#End Region

#Region "APPOINTMENT CLICK"

    Private Sub dgvAppointments_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAppointments.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Try
            selectedAppointmentID = Convert.ToInt32(dgvAppointments.Rows(e.RowIndex).Cells("AppointmentID").Value)

            LoadTreatment(selectedAppointmentID)
            LoadServices(selectedAppointmentID)
        Catch ex As Exception
            MessageBox.Show("Error selecting appointment: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "TREATMENT"

    Private Sub LoadTreatment(appointmentID As Integer)
        Dim query As String = "
        SELECT TreatmentNotes, Prescriptions, ProceduresDone
        FROM TreatmentRecords
        WHERE AppointmentID = @AppointmentID"

        Try
            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@AppointmentID", appointmentID)

                    conn.Open()
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            txtNotes.Text = reader("TreatmentNotes").ToString()
                            txtPrescriptions.Text = reader("Prescriptions").ToString()
                            txtProcedures.Text = reader("ProceduresDone").ToString()
                        Else
                            ClearTreatmentFields()
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading treatment: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearTreatmentFields()
        End Try
    End Sub

#End Region

#Region "SERVICES"

    Private Sub LoadServices(appointmentID As Integer)
        Dim query As String = "
        SELECT s.ServiceName, s.Price
        FROM AppointmentServices aps
        INNER JOIN Services s ON aps.ServiceID = s.ServiceID
        WHERE aps.AppointmentID = @AppointmentID"

        Dim dt As New DataTable()

        Try
            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@AppointmentID", appointmentID)

                    Dim da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using

            dgvServices.DataSource = dt

            ' Format the services grid
            FormatServicesGrid()

            dgvServices.ClearSelection()
            dgvServices.CurrentCell = Nothing
        Catch ex As Exception
            MessageBox.Show("Error loading services: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            dgvServices.DataSource = Nothing
        End Try
    End Sub

    Private Sub FormatServicesGrid()
        If dgvServices.Columns.Count = 0 Then Exit Sub

        With dgvServices
            .Columns("ServiceName").HeaderText = "Service"
            .Columns("Price").HeaderText = "Price"
            .Columns("Price").DefaultCellStyle.Format = "C2"   ' Currency format
            .Columns("Price").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .ReadOnly = True
        End With
    End Sub

#End Region

#Region "CLEAR FUNCTIONS"

    Private Sub ClearTreatmentFields()
        txtNotes.Clear()
        txtPrescriptions.Clear()
        txtProcedures.Clear()

        dgvServices.DataSource = Nothing
        dgvServices.ClearSelection()
        dgvServices.CurrentCell = Nothing
    End Sub

    Private Sub ResetAllSelections()
        selectedAppointmentID = 0

        txtNotes.Clear()
        txtPrescriptions.Clear()
        txtProcedures.Clear()

        dgvAppointments.ClearSelection()
        dgvAppointments.CurrentCell = Nothing

        dgvServices.DataSource = Nothing
        dgvServices.ClearSelection()
        dgvServices.CurrentCell = Nothing
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ResetAllSelections()
    End Sub

#End Region

#Region "FULL HISTORY"

    Private Sub btnFullHistory_Click(sender As Object, e As EventArgs) Handles btnFullHistory.Click
        If selectedPatientID = 0 Then
            MessageBox.Show("Please select a patient first.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim sb As New StringBuilder()
        sb.AppendLine("FULL PATIENT HISTORY")
        sb.AppendLine("Patient ID: " & selectedPatientID)
        sb.AppendLine("==================================================")
        sb.AppendLine()

        Dim query As String = "
        SELECT tr.DateCreated, tr.ProceduresDone, tr.TreatmentNotes, tr.Prescriptions,
               STRING_AGG(s.ServiceName, ', ') AS Services
        FROM TreatmentRecords tr
        LEFT JOIN AppointmentServices aps ON tr.AppointmentID = aps.AppointmentID
        LEFT JOIN Services s ON aps.ServiceID = s.ServiceID
        WHERE tr.PatientID = @PatientID
        GROUP BY tr.DateCreated, tr.ProceduresDone, tr.TreatmentNotes, tr.Prescriptions
        ORDER BY tr.DateCreated DESC"

        Try
            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@PatientID", selectedPatientID)

                    conn.Open()
                    Using reader = cmd.ExecuteReader()
                        Dim hasRecords As Boolean = False

                        While reader.Read()
                            hasRecords = True
                            sb.AppendLine("Date: " & Convert.ToDateTime(reader("DateCreated")).ToString("dd MMM yyyy hh:mm tt"))
                            sb.AppendLine("Procedures: " & reader("ProceduresDone").ToString())
                            sb.AppendLine("Notes: " & reader("TreatmentNotes").ToString())
                            sb.AppendLine("Prescriptions: " & reader("Prescriptions").ToString())
                            sb.AppendLine("Services: " & reader("Services").ToString())
                            sb.AppendLine("--------------------------------------------------")
                        End While

                        If Not hasRecords Then
                            sb.AppendLine("No treatment records found for this patient.")
                        End If
                    End Using
                End Using
            End Using

            MessageBox.Show(sb.ToString(), "Full Patient History", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error retrieving full history: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "NAVIGATION"

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

#End Region

End Class