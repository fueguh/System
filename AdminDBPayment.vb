Imports System.Data.SqlClient

Public Class AdminDBPayment

    ' ===== PASSED OR SELECTED DATA =====
    Public SelectedAppointmentID As Integer = 0
    Public SelectedPatientID As Integer = 0
    Public SelectedPatientName As String = ""
    Private SelectedDentistName As String = ""
    Private SelectedTreatmentNotes As String = "" ' Merged Notes Variable

    ' ================= FORM LOAD =================
    Private Sub AdminDBPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPendingPayments()
        LoadPaymentMethods()
        txtReferenceNo.Enabled = False ' Ensure it starts disabled
        ' Formatting the Grid for Word Wrap (For those long dentist notes)
        dgvPendingPayments.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        dgvPendingPayments.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

        TextBoxTotal.ReadOnly = True
        dgvPendingPayments.ReadOnly = True
        dgvPendingPayments.AllowUserToAddRows = False
        dgvPendingPayments.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub

    ' ================= MERGED DATA FETCH (GRID) =================
    Private Sub LoadPendingPayments()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            ' This Query merges Appointments, Patients, and TreatmentRecords
            ' It uses STRING_AGG to list all services in one cell for the grid
            ' Inside LoadPendingPayments()
            Dim sql As String = "
    SELECT 
        A.AppointmentID, 
        P.PatientID,
        P.FullName AS [Patient Name], 
        U.FullName AS [Dentist], -- Added Dentist Name
        A.Date, 
        ISNULL(T.TreatmentNotes, 'No notes recorded') AS [Dentist Notes],
        (SELECT STRING_AGG(S.ServiceName, ', ') 
         FROM AppointmentServices AS [AS] 
         JOIN Services S ON [AS].ServiceID = S.ServiceID 
         WHERE [AS].AppointmentID = A.AppointmentID) AS [Services Done]
    FROM Appointments A
    INNER JOIN Patients P ON A.PatientID = P.PatientID
    INNER JOIN Users U ON A.UserID = U.UserID -- Join to get Dentist Name
    LEFT JOIN TreatmentRecords T ON A.AppointmentID = T.AppointmentID
    WHERE A.Status = 'Completed' 
    AND NOT EXISTS (SELECT 1 FROM Receipts R WHERE R.AppointmentID = A.AppointmentID)"

            Dim da As New SqlDataAdapter(sql, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            dgvPendingPayments.DataSource = dt

            ' Hide IDs to keep it clean
            If dgvPendingPayments.Columns.Contains("AppointmentID") Then dgvPendingPayments.Columns("AppointmentID").Visible = False
            If dgvPendingPayments.Columns.Contains("PatientID") Then dgvPendingPayments.Columns("PatientID").Visible = False
        End Using
    End Sub

    ' ================= GRID SELECTION LOGIC =================
    Private Sub dgvPendingPayments_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPendingPayments.CellClick
        If e.RowIndex >= 0 Then
            Dim row = dgvPendingPayments.Rows(e.RowIndex)

            ' 1. Transfer IDs
            SelectedAppointmentID = CInt(row.Cells("AppointmentID").Value)
            SelectedPatientID = CInt(row.Cells("PatientID").Value)

            ' 2. Update Labels directly from Grid data
            SelectedPatientName = row.Cells("Patient Name").Value.ToString()
            SelectedDentistName = row.Cells("Dentist").Value.ToString() ' Get from Grid
            SelectedTreatmentNotes = row.Cells("Dentist Notes").Value.ToString()

            ' 3. Sync the UI Labels
            patient_name.Text = SelectedPatientName
            dentist_name.Text = SelectedDentistName ' Update your dentist label here

            ' 4. Sync Services
            LoadAppointmentServices()
        End If
    End Sub

    Private Sub FetchDentistName()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim cmd As New SqlCommand("SELECT U.FullName FROM Appointments A INNER JOIN Users U ON A.UserID = U.UserID WHERE A.AppointmentID = @AID", con)
            cmd.Parameters.AddWithValue("@AID", SelectedAppointmentID)
            Dim res = cmd.ExecuteScalar()
            SelectedDentistName = If(res IsNot Nothing, res.ToString(), "N/A")
        End Using
    End Sub

    ' ================= SERVICE LOADING & CALCULATION =================
    Private Sub LoadAllServices()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim da As New SqlDataAdapter("SELECT ServiceID, ServiceName, Price FROM Services ORDER BY ServiceName ASC", con)
            Dim dt As New DataTable()
            da.Fill(dt)

        End Using
    End Sub

    Private Sub LoadAppointmentServices()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim cmd As New SqlCommand("SELECT S.ServiceID, S.ServiceName, S.Price FROM AppointmentServices AS A INNER JOIN Services AS S ON A.ServiceID = S.ServiceID WHERE A.AppointmentID = @AID", con)
            cmd.Parameters.AddWithValue("@AID", SelectedAppointmentID)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            ' Display in the detail grid
            dgvServices.DataSource = dt

            ' === HIDE SERVICE ID COLUMN ===
            If dgvServices.Columns.Contains("ServiceID") Then
                dgvServices.Columns("ServiceID").Visible = False
            End If

            ' Optional: Make the service name fill the remaining space
            If dgvServices.Columns.Contains("ServiceName") Then
                dgvServices.Columns("ServiceName").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If

            CalculateTotal()
        End Using
    End Sub

    Private Sub CalculateTotal()
        Dim total As Decimal = 0
        ' Loop through the second grid (dgvServices) instead of clbServices
        For Each row As DataGridViewRow In dgvServices.Rows
            If Not row.IsNewRow Then
                total += Convert.ToDecimal(row.Cells("Price").Value)
            End If
        Next
        TextBoxTotal.Text = total.ToString("F2")
    End Sub

    Private Sub LoadPaymentMethods()
        ComboBoxPaymentMethod.Items.Clear()
        ComboBoxPaymentMethod.Items.AddRange(New String() {"Cash", "Gcash"})
    End Sub
    Private Sub ComboBoxPaymentMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPaymentMethod.SelectedIndexChanged
        If ComboBoxPaymentMethod.Text = "Gcash" Then
            txtReferenceNo.Enabled = True
            txtReferenceNo.PlaceholderText = "Enter Reference No." ' If using Guna or modern controls
        Else
            txtReferenceNo.Enabled = False
            txtReferenceNo.Clear()
        End If
    End Sub
    Private Sub ButtonGenerateReceipt_Click(sender As Object, e As EventArgs) Handles ButtonGenerateReceipt.Click
        ' 1. Security & Validation
        If SystemSession.LoggedInUserID <= 0 Then
            MessageBox.Show("Error: No logged-in User ID found. Please re-login.")
            Exit Sub
        End If

        If SelectedAppointmentID = 0 Then
            MessageBox.Show("Please select an appointment from the list first.")
            Exit Sub
        End If

        If String.IsNullOrEmpty(ComboBoxPaymentMethod.Text) Then
            MessageBox.Show("Please select a payment method.")
            Exit Sub
        End If

        ' --- GCash Validation Check ---
        If ComboBoxPaymentMethod.Text = "Gcash" Then
            Dim refNo As String = txtReferenceNo.Text.Trim()

            If String.IsNullOrWhiteSpace(refNo) Then
                MessageBox.Show("Please enter the GCash Reference Number.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtReferenceNo.Focus()
                Exit Sub
            End If

            ' Philippines standard GCash Ref is 13 digits
            If Not System.Text.RegularExpressions.Regex.IsMatch(refNo, "^\d{13}$") Then
                Dim confirm = MessageBox.Show("Standard GCash reference numbers are 13 digits. Proceed anyway?",
                                    "Format Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If confirm = DialogResult.No Then
                    txtReferenceNo.Focus()
                    Exit Sub
                End If
            End If
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            Try
                con.Open()
                Using trans As SqlTransaction = con.BeginTransaction()
                    Try
                        ' 2. Database Insert
                        Dim sql As String = "INSERT INTO Receipts (AppointmentID, PatientID, UserID, TotalAmount, PaymentMethod, ReferenceNumber) " &
                                        "VALUES (@AID, @PID, @UID, @Total, @Method, @Ref)"

                        Using cmd As New SqlCommand(sql, con, trans)
                            cmd.Parameters.Add("@AID", SqlDbType.Int).Value = SelectedAppointmentID
                            cmd.Parameters.Add("@PID", SqlDbType.Int).Value = SelectedPatientID
                            cmd.Parameters.Add("@UID", SqlDbType.Int).Value = SystemSession.LoggedInUserID
                            cmd.Parameters.Add("@Total", SqlDbType.Decimal).Value = CDec(TextBoxTotal.Text)
                            cmd.Parameters.Add("@Method", SqlDbType.VarChar).Value = ComboBoxPaymentMethod.Text

                            ' Pass Reference Number or DBNull
                            cmd.Parameters.Add("@Ref", SqlDbType.VarChar).Value = If(ComboBoxPaymentMethod.Text = "Gcash", txtReferenceNo.Text.Trim(), DBNull.Value)

                            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                            If rowsAffected > 0 Then
                                trans.Commit()

                                ' Audit Logging
                                Dim auditMsg As String = String.Format("Processed payment of P{0} for patient {1}", TextBoxTotal.Text, SelectedPatientName)
                                SystemSession.LogAudit(auditMsg, "Payment", SystemSession.LoggedInUserID, SystemSession.LoggedInFullName, SystemSession.LoggedInRole)

                                ' 3. Unified Printing Call
                                Dim askPrint As DialogResult = MessageBox.Show("Payment Successful! Would you like to print the receipt now?",
                                               "Print Receipt", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                                ' Inside ButtonGenerateReceipt_Click
                                If askPrint = DialogResult.Yes Then
                                    ' 1. Grab the table from the grid
                                    Dim dtServices As DataTable = CType(dgvServices.DataSource, DataTable)

                                    ' 2. Call the Unified Module
                                    ReceiptPrinter.PrintReceipt(SelectedPatientName,
                              SelectedDentistName,
                              SelectedTreatmentNotes,
                              TextBoxTotal.Text,
                              ComboBoxPaymentMethod.Text,
                              txtReferenceNo.Text,
                              dtServices)
                                End If

                                ' Jump to cleanup
                                GoTo SuccessCleanup
                            Else
                                trans.Rollback()
                                MessageBox.Show("Save failed: No rows were affected.")
                            End If
                        End Using
                    Catch ex As Exception
                        If trans.Connection IsNot Nothing Then trans.Rollback()
                        MessageBox.Show("Transaction Error: " & ex.Message)
                    End Try
                End Using
            Catch ex As Exception
                MessageBox.Show("Connection Error: " & ex.Message)
            End Try
        End Using
        Exit Sub

SuccessCleanup:
        LoadPendingPayments()
        ClearBillingUI()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

    ' Helper to clean up the screen after saving
    Private Sub ClearBillingUI()
        ' Reset IDs
        SelectedAppointmentID = 0
        SelectedPatientID = 0

        ' Reset Labels (Assuming these are your Label names)
        patient_name.Text = "---"
        dentist_name.Text = "---"
        ' Reset Numeric fields
        TextBoxTotal.Text = "0.00"

        ' Clear the Itemized Grid
        dgvServices.DataSource = Nothing
        txtReferenceNo.Clear()
        ' Clear reference number and disable it until GCash is selected again
        txtReferenceNo.Enabled = False
        ' Reset selection in main grid
        dgvPendingPayments.ClearSelection()
    End Sub
    ' Only allows numbers and backspace in the Reference Number box
    Private Sub txtReferenceNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtReferenceNo.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearBillingUI()
    End Sub
End Class