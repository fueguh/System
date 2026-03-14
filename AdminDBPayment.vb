Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.Management

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

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            Try
                con.Open()
                Using trans As SqlTransaction = con.BeginTransaction()
                    Try
                        ' 2. Database Insert
                        Dim sql As String = "INSERT INTO Receipts (AppointmentID, PatientID, UserID, TotalAmount, PaymentMethod) " &
                                        "VALUES (@AID, @PID, @UID, @Total, @Method)"

                        Using cmd As New SqlCommand(sql, con, trans)
                            cmd.Parameters.Add("@AID", SqlDbType.Int).Value = SelectedAppointmentID
                            cmd.Parameters.Add("@PID", SqlDbType.Int).Value = SelectedPatientID
                            cmd.Parameters.Add("@UID", SqlDbType.Int).Value = SystemSession.LoggedInUserID
                            cmd.Parameters.Add("@Total", SqlDbType.Decimal).Value = CDec(TextBoxTotal.Text)
                            cmd.Parameters.Add("@Method", SqlDbType.VarChar).Value = ComboBoxPaymentMethod.Text

                            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                            If rowsAffected > 0 Then
                                trans.Commit()

                                ' Audit Logging
                                Dim auditMsg As String = String.Format("Processed payment of P{0} for patient {1} (Appt ID: {2})",
                                       TextBoxTotal.Text, SelectedPatientName, SelectedAppointmentID)
                                SystemSession.LogAudit(auditMsg, "Billing", SystemSession.LoggedInUserID,
                                       SystemSession.LoggedInFullName, SystemSession.LoggedInRole)

                                ' 3. Prompt to Print
                                Dim askPrint As DialogResult = MessageBox.Show("Payment Successful! Would you like to print the receipt now?",
                                               "Print Receipt", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                                If askPrint = DialogResult.Yes Then
                                    ' Call the shared printing logic to avoid code duplication
                                    PrintWithStatusCheck()
                                End If

                                ' Jump to cleanup after successful print/skip
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

    ' --- SHARED PRINTING LOGIC ---
    Private Sub PrintWithStatusCheck()
        Dim pd As New PrintDocument()
        pd.DefaultPageSettings.PaperSize = New PaperSize("Custom", 300, 1000)
        AddHandler pd.PrintPage, AddressOf PrintPageHandler

        Dim dlg As New PrintDialog()
        dlg.Document = pd

        ' User interacts with Windows Print Dialog first
        If dlg.ShowDialog() = DialogResult.OK Then
            ' NOW check if the selected printer is actually reachable
            Dim pkName As String = pd.PrinterSettings.PrinterName

            If Not IsPrinterOnline(pkName) Then
                Dim ans As DialogResult = MessageBox.Show($"The printer '{pkName}' appears to be offline. Try printing anyway?",
                                                 "Printer Offline", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If ans = DialogResult.No Then Exit Sub
            End If

            Try
                pd.Print()
            Catch ex As Exception
                MessageBox.Show("A hardware error occurred: " & ex.Message, "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    ' WMI Helper for Printer Status
    Private Function IsPrinterOnline(printerName As String) As Boolean
        Try
            ' Escape backslashes for the SQL-like WMI query
            Dim query As String = "SELECT * FROM Win32_Printer WHERE Name = '" & printerName.Replace("\", "\\") & "'"
            Using searcher As New ManagementObjectSearcher(query)
                For Each printer As ManagementObject In searcher.Get()
                    ' Check if it's manually set to "Work Offline"
                    Dim isWorkOffline As Boolean = CBool(printer("WorkOffline"))

                    ' Check the extended status (3 = Idle/Ready, 4 = Printing, 5 = Warming Up)
                    ' Status codes 1 (Other), 2 (Unknown), 7 (Offline) usually mean trouble
                    Dim status As Integer = If(printer("PrinterStatus") IsNot Nothing, CInt(printer("PrinterStatus")), 0)

                    ' If it's offline OR status is 7 (Power off/Disconnected)
                    If isWorkOffline OrElse status = 7 Then
                        Return False
                    End If

                    Return True ' Printer is likely okay
                Next
            End Using
        Catch
            Return False ' Error accessing WMI, assume offline for safety
        End Try
        Return False
    End Function
    ' ================= PRINTER LOGIC =================
    Private Sub ButtonPrintReceipt_Click(sender As Object, e As EventArgs) Handles ButtonPrintReceipt.Click
        ' Stop using local logic; call the shared sub that has the warning check
        PrintWithStatusCheck()
    End Sub

    Private Sub PrintPageHandler(sender As Object, e As PrintPageEventArgs)
        Dim g As Graphics = e.Graphics
        Dim currentY As Integer = 40
        Dim fontBody As New Font("Consolas", 8)
        Dim leftMargin As Integer = 5
        Dim rightMargin As Integer = 185

        ' 1. Header
        g.DrawString("DENTAL CLINIC RECEIPT", New Font("Arial", 10, FontStyle.Bold), Brushes.Black, leftMargin, currentY)
        currentY += 20
        g.DrawString("Date: " & DateTime.Now.ToString("G"), fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 15
        g.DrawString("Patient: " & SelectedPatientName, fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 15
        g.DrawString("Doctor:  " & SelectedDentistName, fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 15
        g.DrawString("--------------------------------", fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 15

        ' 2. Services List (Updated to use dgvServices)
        For Each row As DataGridViewRow In dgvServices.Rows
            If Not row.IsNewRow Then
                Dim sName As String = row.Cells("ServiceName").Value.ToString()
                Dim sPrice As String = "P" & CDec(row.Cells("Price").Value).ToString("F2")

                g.DrawString(sName, fontBody, Brushes.Black, leftMargin, currentY)
                g.DrawString(sPrice, fontBody, Brushes.Black, rightMargin - g.MeasureString(sPrice, fontBody).Width, currentY)
                currentY += 15
            End If
        Next

        ' 3. MERGED TREATMENT NOTES (The Merged Part)
        currentY += 10
        g.DrawString("DENTIST NOTES:", New Font("Consolas", 8, FontStyle.Bold), Brushes.Black, leftMargin, currentY)
        currentY += 15
        ' Use RectangleF for wrapping text
        Dim rectNotes As New RectangleF(leftMargin, currentY, rightMargin - leftMargin, 150)
        g.DrawString(SelectedTreatmentNotes, fontBody, Brushes.Black, rectNotes)

        ' Dynamic Y shift based on note length
        Dim textSize = g.MeasureString(SelectedTreatmentNotes, fontBody, New SizeF(rightMargin - leftMargin, 150))
        currentY += CInt(textSize.Height) + 10

        ' 4. Total
        g.DrawString("--------------------------------", fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 15
        g.DrawString("TOTAL AMOUNT:", New Font("Consolas", 9, FontStyle.Bold), Brushes.Black, leftMargin, currentY)
        g.DrawString("P" & TextBoxTotal.Text, New Font("Consolas", 9, FontStyle.Bold), Brushes.Black, rightMargin - g.MeasureString("P" & TextBoxTotal.Text, fontBody).Width, currentY)

        currentY += 25
        g.DrawString("Method: " & ComboBoxPaymentMethod.Text, fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 30
        g.DrawString("Thank you for visiting!", fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 40
        g.DrawString(".", New Font("Arial", 1), Brushes.Black, leftMargin, currentY) ' Force Feed
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

        ' Reset selection in main grid
        dgvPendingPayments.ClearSelection()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearBillingUI()
    End Sub
End Class