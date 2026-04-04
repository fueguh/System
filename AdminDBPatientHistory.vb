Imports System.Data.SqlClient
Imports System.Text

Public Class AdminDBPatientHistory

    Private connectionString As String = My.Settings.DentalDBConnection2

    Private selectedPatientID As Integer = 0
    Private selectedAppointmentID As Integer = 0
    Private dvPatients As DataView
    Private isLoading As Boolean = False
    Private dtAllPatients As DataTable

#Region "FORM LOAD"

    Private Sub AdminDBPatientHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadPatients()

            txtSearchPatient.Clear()
            txtSearchPatient.Focus()

            Try
                cboPatients.DropDownStyle = ComboBoxStyle.DropDownList
            Catch
            End Try

        Catch ex As Exception
            MessageBox.Show("Error loading form: " & ex.Message)
        End Try
    End Sub

#End Region

#Region "LOAD PATIENTS"

    Private Sub LoadPatients()

        isLoading = True

        Dim query As String = "
        SELECT PatientID, FullName
        FROM Patients
        ORDER BY FullName"

        Try
            dtAllPatients = New DataTable()

            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, conn)
                    Dim da As New SqlDataAdapter(cmd)
                    da.Fill(dtAllPatients)
                End Using
            End Using

            dvPatients = New DataView(dtAllPatients)

            cboPatients.DataSource = dvPatients
            cboPatients.DisplayMember = "FullName"
            cboPatients.ValueMember = "PatientID"

            cboPatients.SelectedIndex = -1

        Catch ex As Exception
            MessageBox.Show("Error loading patients: " & ex.Message)
        Finally
            isLoading = False
        End Try

    End Sub

#End Region

#Region "SEARCH"

    Private Sub txtSearchPatient_TextChanged(sender As Object, e As EventArgs) Handles txtSearchPatient.TextChanged

        If isLoading OrElse dvPatients Is Nothing Then Exit Sub

        Dim searchText As String = txtSearchPatient.Text.Trim().Replace("'", "''")

        If searchText = "" Then
            dvPatients.RowFilter = ""
        Else
            dvPatients.RowFilter = $"FullName LIKE '%{searchText}%'"
        End If

        cboPatients.DroppedDown = True

    End Sub

#End Region

#Region "SELECT PATIENT"

    Private Sub cboPatients_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPatients.SelectedIndexChanged

        If isLoading Then Exit Sub
        If cboPatients.SelectedValue Is Nothing OrElse IsDBNull(cboPatients.SelectedValue) Then Exit Sub

        Dim id As Integer
        If Not Integer.TryParse(cboPatients.SelectedValue.ToString(), id) Then Exit Sub

        If id <= 0 OrElse id = selectedPatientID Then Exit Sub

        selectedPatientID = id

        ClearTreatmentFields()
        selectedAppointmentID = 0

        LoadPatientData(selectedPatientID)

    End Sub

#End Region

#Region "LOAD DATA"

    Private Sub LoadPatientData(patientID As Integer)
        LoadAppointments(patientID)
        ClearTreatmentFields()
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
            FormatAppointmentsGrid()

        Catch ex As Exception
            MessageBox.Show("Error loading appointments: " & ex.Message)
        End Try

    End Sub

    Private Sub FormatAppointmentsGrid()

        If dgvAppointments.Columns.Count = 0 Then Exit Sub

        With dgvAppointments

            If .Columns.Contains("AppointmentID") Then
                .Columns("AppointmentID").Visible = False
            End If

            If .Columns.Contains("Date") Then
                .Columns("Date").HeaderText = "Date"
                .Columns("Date").DefaultCellStyle.Format = "dd MMM yyyy hh:mm tt"
            End If

            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .ReadOnly = True

        End With

    End Sub

#End Region

#Region "APPOINTMENT CLICK"

    Private Sub dgvAppointments_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAppointments.CellClick

        If e.RowIndex < 0 Then Exit Sub

        Dim val = dgvAppointments.Rows(e.RowIndex).Cells("AppointmentID").Value

        If val Is Nothing OrElse IsDBNull(val) Then Exit Sub

        selectedAppointmentID = Convert.ToInt32(val)

        LoadTreatment(selectedAppointmentID)
        LoadServices(selectedAppointmentID)

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
                            txtNotes.Text = If(IsDBNull(reader("TreatmentNotes")), "", reader("TreatmentNotes").ToString())
                            txtPrescriptions.Text = If(IsDBNull(reader("Prescriptions")), "", reader("Prescriptions").ToString())
                            txtProcedures.Text = If(IsDBNull(reader("ProceduresDone")), "", reader("ProceduresDone").ToString())
                        Else
                            ClearTreatmentFields()
                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading treatment: " & ex.Message)
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
            FormatServicesGrid()

        Catch ex As Exception
            MessageBox.Show("Error loading services: " & ex.Message)
        End Try

    End Sub

    Private Sub FormatServicesGrid()

        If dgvServices.Columns.Count = 0 Then Exit Sub

        With dgvServices
            If .Columns.Contains("Price") Then
                .Columns("Price").DefaultCellStyle.Format = "C2"
            End If

            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .ReadOnly = True
        End With

    End Sub

#End Region

#Region "CLEAR"

    Private Sub ClearTreatmentFields()

        txtNotes.Clear()
        txtPrescriptions.Clear()
        txtProcedures.Clear()

        dgvServices.DataSource = Nothing

    End Sub

#End Region

#Region "FULL HISTORY (WITH PAYMENT STATUS FIXED)"

    Private Sub btnFullHistory_Click(sender As Object, e As EventArgs) Handles btnFullHistory.Click

        If selectedPatientID = 0 Then
            MessageBox.Show("Select a patient first.")
            Exit Sub
        End If

        Dim sb As New StringBuilder()

        Try
            Using conn As New SqlConnection(connectionString)
                conn.Open()

                Dim query As String = "
                SELECT 
                    tr.DateCreated,
                    tr.ProceduresDone,
                    tr.TreatmentNotes,
                    tr.Prescriptions,
                    ISNULL(STRING_AGG(s.ServiceName, ', '), 'None') AS Services,

                    (
                        SELECT TOP 1 r.Status
                        FROM Receipts r
                        WHERE r.AppointmentID = tr.AppointmentID
                        ORDER BY r.ReceiptID DESC
                    ) AS PaymentStatus

                FROM TreatmentRecords tr
                LEFT JOIN AppointmentServices aps ON tr.AppointmentID = aps.AppointmentID
                LEFT JOIN Services s ON aps.ServiceID = s.ServiceID
                WHERE tr.PatientID = @PatientID

                GROUP BY 
                    tr.DateCreated,
                    tr.ProceduresDone,
                    tr.TreatmentNotes,
                    tr.Prescriptions,
                    tr.AppointmentID

                ORDER BY tr.DateCreated DESC"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@PatientID", selectedPatientID)

                    Using reader = cmd.ExecuteReader()

                        sb.AppendLine("FULL PATIENT HISTORY")
                        sb.AppendLine("==================================================")

                        Dim hasRecords As Boolean = False

                        While reader.Read()
                            hasRecords = True

                            Dim paymentStatus As String =
                                If(IsDBNull(reader("PaymentStatus")), "UNPAID", reader("PaymentStatus").ToString())

                            sb.AppendLine("Date: " & Convert.ToDateTime(reader("DateCreated")).ToString("dd MMM yyyy"))
                            sb.AppendLine("Procedures: " & reader("ProceduresDone").ToString())
                            sb.AppendLine("Notes: " & reader("TreatmentNotes").ToString())
                            sb.AppendLine("Prescriptions: " & reader("Prescriptions").ToString())
                            sb.AppendLine("Services: " & reader("Services").ToString())
                            sb.AppendLine("Payment Status: " & paymentStatus)
                            sb.AppendLine("--------------------------------------------------")
                        End While

                        If Not hasRecords Then
                            sb.AppendLine("No records found.")
                        End If

                    End Using
                End Using

            End Using

            MessageBox.Show(sb.ToString(), "Full History")

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

    End Sub

#End Region

End Class