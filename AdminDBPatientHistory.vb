Imports System.Data.SqlClient
Imports System.Text

Public Class AdminDBPatientHistory

    Private connectionString As String = My.Settings.DentalDBConnection2

    Private selectedPatientID As Integer = 0
    Private selectedAppointmentID As Integer = 0

    Private isLoading As Boolean = False

    Private dtAllPatients As DataTable

#Region "FORM LOAD"

    Private Sub AdminDBPatientHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadPatients()

            SafeResetUI()

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

        dtAllPatients = New DataTable()

        Try
            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, conn)
                    Dim da As New SqlDataAdapter(cmd)
                    da.Fill(dtAllPatients)
                End Using
            End Using

            cboPatients.DataSource = dtAllPatients
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

#Region "SEARCH (FIXED)"

    Private Sub txtSearchPatient_TextChanged(sender As Object, e As EventArgs) Handles txtSearchPatient.TextChanged
        If isLoading Or dtAllPatients Is Nothing Then Exit Sub

        isLoading = True

        Dim dv As New DataView(dtAllPatients)

        Dim searchText As String = txtSearchPatient.Text.Trim()

        If searchText <> "" Then
            dv.RowFilter = "FullName LIKE '%" & searchText.Replace("'", "''") & "%'"
        Else
            dv.RowFilter = ""
        End If

        ' IMPORTANT FIX:
        ' prevent SelectedIndexChanged firing while rebinding
        cboPatients.DataSource = Nothing

        cboPatients.DataSource = dv
        cboPatients.DisplayMember = "FullName"
        cboPatients.ValueMember = "PatientID"

        cboPatients.SelectedIndex = -1

        ' DO NOT clear again inside reset loop (prevents double behavior)
        ClearGridsAndTreatmentOnly()

        isLoading = False
    End Sub

#End Region

#Region "SELECTION (FIXED ANTI-LOOP)"

    Private Sub cboPatients_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPatients.SelectedIndexChanged

        If isLoading Then Exit Sub
        If cboPatients.SelectedValue Is Nothing Then Exit Sub
        If Not IsNumeric(cboPatients.SelectedValue) Then Exit Sub

        Dim newID As Integer = Convert.ToInt32(cboPatients.SelectedValue)

        If newID <= 0 Then Exit Sub

        ' PREVENT RELOAD LOOP
        If newID = selectedPatientID Then Exit Sub

        selectedPatientID = newID

        LoadPatientData(selectedPatientID)

    End Sub

#End Region

#Region "MASTER LOAD"

    Private Sub LoadPatientData(patientID As Integer)

        Try
            LoadAppointments(patientID)
            ClearGridsAndTreatmentOnly()

        Catch ex As Exception
            MessageBox.Show("Error loading patient data: " & ex.Message)
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

            dgvAppointments.ClearSelection()

        Catch ex As Exception
            MessageBox.Show("Error loading appointments: " & ex.Message)
        End Try

    End Sub

#End Region

#Region "APPOINTMENT CLICK"

    Private Sub dgvAppointments_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAppointments.CellClick

        If e.RowIndex < 0 Then Exit Sub

        selectedAppointmentID = Convert.ToInt32(dgvAppointments.Rows(e.RowIndex).Cells("AppointmentID").Value)

        LoadTreatment(selectedAppointmentID)
        LoadServices(selectedAppointmentID)

    End Sub

#End Region

#Region "TREATMENT + SERVICES"

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

                    Using r = cmd.ExecuteReader()

                        If r.Read() Then
                            txtNotes.Text = r("TreatmentNotes").ToString()
                            txtPrescriptions.Text = r("Prescriptions").ToString()
                            txtProcedures.Text = r("ProceduresDone").ToString()
                        Else
                            ClearGridsAndTreatmentOnly()
                        End If

                    End Using
                End Using
            End Using

        Catch ex As Exception
            ClearGridsAndTreatmentOnly()
        End Try

    End Sub

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

        Catch ex As Exception
            dgvServices.DataSource = Nothing
        End Try

    End Sub

#End Region

#Region "CLEAR (FIXED PROPER RESET)"

    Private Sub SafeResetUI()

        isLoading = True

        selectedPatientID = 0
        selectedAppointmentID = 0

        txtSearchPatient.Clear() ' ✅ FIX: ALWAYS CLEAR SEARCH BOX

        cboPatients.DataSource = Nothing
        cboPatients.Items.Clear()

        LoadPatients()

        dgvAppointments.DataSource = Nothing
        dgvServices.DataSource = Nothing

        ClearGridsAndTreatmentOnly()

        cboPatients.SelectedIndex = -1

        isLoading = False

    End Sub

    Private Sub ClearGridsAndTreatmentOnly()

        txtNotes.Clear()
        txtPrescriptions.Clear()
        txtProcedures.Clear()

        dgvAppointments.ClearSelection()
        dgvServices.ClearSelection()

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        SafeResetUI()
    End Sub

#End Region

End Class