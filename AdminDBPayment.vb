Imports System.Data.SqlClient

Public Class AdminDBPayment

    ' ===== PASSED OR SELECTED DATA =====
    Public SelectedAppointmentID As Integer = 0
    Public SelectedPatientID As Integer = 0
    Public SelectedPatientName As String = ""
    Private SelectedDentistName As String = ""
    Private SelectedTreatmentNotes As String = ""
    Private currentTotal As Decimal = 0

    ' ================= FORM LOAD =================
    Private Sub AdminDBPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPendingPayments()
        LoadPaymentMethods()
        LoadInventoryItems()
        SetupReceiptGrid()
        txtReferenceNo.Enabled = False
        SetPrescriptionControlsEnabled(False)
        ClearBillingUI()
    End Sub

    ' ==================================================================
    ' REGION: PENDING PAYMENTS GRID
    ' ==================================================================
    Private Sub LoadPendingPayments()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim sql As String = "
            SELECT 
                A.AppointmentID, 
                P.PatientID,
                P.FullName AS [Patient Name], 
                U.FullName AS [Dentist], 
                A.Date, 
                ISNULL(T.TreatmentNotes, 'No notes recorded') AS [Dentist Notes],
                ISNULL(T.Prescriptions, 'No prescription') AS [Prescription],
                ISNULL(STRING_AGG(S.ServiceName, ', '), '') AS [Services Done],
                ISNULL((
                    SELECT STRING_AGG(CONVERT(VARCHAR, F.FollowUpDate, 120) + ' - ' + F.Reason, ' | ')
                    FROM PatientFollowUps F 
                    WHERE F.AppointmentID = A.AppointmentID
                ), 'No follow-ups') AS [Follow Ups]
            FROM Appointments A
            INNER JOIN Patients P ON A.PatientID = P.PatientID
            INNER JOIN Users U ON A.UserID = U.UserID
            LEFT JOIN TreatmentRecords T ON A.AppointmentID = T.AppointmentID
            LEFT JOIN AppointmentServices AP ON A.AppointmentID = AP.AppointmentID
            LEFT JOIN Services S ON AP.ServiceID = S.ServiceID
            WHERE A.Status = 'Completed'
            AND NOT EXISTS (SELECT 1 FROM Receipts R WHERE R.AppointmentID = A.AppointmentID)
            GROUP BY 
                A.AppointmentID, P.PatientID, P.FullName, U.FullName, 
                A.Date, T.TreatmentNotes, T.Prescriptions
            ORDER BY A.Date DESC"

            Using da As New SqlDataAdapter(sql, con)
                Dim dt As New DataTable()
                da.Fill(dt)
                dgvPendingPayments.DataSource = dt
            End Using

            ' Hide columns
            For Each col As DataGridViewColumn In dgvPendingPayments.Columns
                Select Case col.Name
                    Case "AppointmentID", "PatientID", "Prescription"
                        col.Visible = False
                End Select
            Next
        End Using
    End Sub

    ' ==================================================================
    ' REGION: GRID SELECTION & DATA FETCHING
    ' ==================================================================
    Private Sub dgvPendingPayments_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPendingPayments.CellClick
        Try
            If e.RowIndex < 0 OrElse dgvPendingPayments.Rows.Count = 0 Then Exit Sub

            Dim row = dgvPendingPayments.Rows(e.RowIndex)
            If row Is Nothing OrElse row.Cells("AppointmentID").Value Is Nothing Then Exit Sub

            SelectedAppointmentID = Convert.ToInt32(row.Cells("AppointmentID").Value)
            SelectedPatientID = Convert.ToInt32(row.Cells("PatientID").Value)

            SelectedPatientName = Convert.ToString(row.Cells("Patient Name").Value)
            SelectedDentistName = Convert.ToString(row.Cells("Dentist").Value)
            SelectedTreatmentNotes = Convert.ToString(row.Cells("Dentist Notes").Value)

            patient_name.Text = SelectedPatientName
            dentist_name.Text = SelectedDentistName
            TextBoxPrescriptionNotes.Text = Convert.ToString(row.Cells("Prescription").Value)

            dgvReceiptItems.Rows.Clear()

            LoadAppointmentServices()
            UpdateGrandTotalDisplay()
            SetPrescriptionControlsEnabled(True)
            ClearAllSelections()
        Catch ex As Exception
            MessageBox.Show("Selection error: " & ex.Message)
        End Try
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

    ' ==================================================================
    ' REGION: SERVICES LOADING & CALCULATION
    ' ==================================================================
    Private Sub LoadAppointmentServices()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim cmd As New SqlCommand("SELECT S.ServiceID, S.ServiceName, S.Price FROM AppointmentServices AS A INNER JOIN Services AS S ON A.ServiceID = S.ServiceID WHERE A.AppointmentID = @AID", con)
            cmd.Parameters.AddWithValue("@AID", SelectedAppointmentID)

            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            dgvServices.DataSource = dt

            ' Hide ServiceID column
            If dgvServices.Columns.Contains("ServiceID") Then
                dgvServices.Columns("ServiceID").Visible = False
            End If

            If dgvServices.Columns.Contains("ServiceName") Then
                dgvServices.Columns("ServiceName").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If

            ' === IMPORTANT: Calculate service total and update UI ===
            CalculateServiceTotal()
        End Using
    End Sub

    Private Sub CalculateServiceTotal()
        Dim dt As DataTable = TryCast(dgvServices.DataSource, DataTable)
        If dt Is Nothing Then
            currentTotal = 0D
        Else
            Dim result = dt.Compute("SUM(Price)", "")
            currentTotal = If(IsDBNull(result), 0D, Convert.ToDecimal(result))
        End If

        UpdateGrandTotalDisplay()   ' This will now correctly include services + items
    End Sub

    ' ==================================================================
    ' REGION: PAYMENT METHODS & INPUT HANDLING
    ' ==================================================================
    Private Sub LoadPaymentMethods()
        ComboBoxPaymentMethod.Items.Clear()
        ComboBoxPaymentMethod.Items.AddRange(New String() {"Cash", "Gcash"})
    End Sub

    Private Sub ComboBoxPaymentMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPaymentMethod.SelectedIndexChanged
        Dim isGcash As Boolean = (ComboBoxPaymentMethod.Text = "Gcash")

        txtReferenceNo.Enabled = isGcash
        If Not isGcash Then txtReferenceNo.Clear()

        txtAmountPaid.ReadOnly = isGcash

        If isGcash Then
            UpdateGrandTotalDisplay()        ' Clean & consistent
        Else
            ' For Cash: preserve user input when possible
            If String.IsNullOrWhiteSpace(txtAmountPaid.Text) OrElse
           Decimal.TryParse(txtAmountPaid.Text, 0D) = currentTotal Then
                txtAmountPaid.Clear()
            End If
        End If

        UpdateButtonState()
    End Sub

    ' ==================================================================
    ' REGION: RECEIPT GENERATION & DATABASE OPERATIONS
    ' ==================================================================
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

        If String.IsNullOrWhiteSpace(txtAmountPaid.Text) Then
            MessageBox.Show("Please enter the amount paid.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtAmountPaid.Focus()
            Exit Sub
        End If

        ' --- Calculations ---
        Dim serviceTotal As Decimal = currentTotal
        Dim itemTotal As Decimal = GetItemTotal()
        Dim totalAmount As Decimal = serviceTotal + itemTotal

        Dim amountPaid As Decimal = 0
        Decimal.TryParse(txtAmountPaid.Text, amountPaid)

        Dim changeAmount As Decimal = amountPaid - totalAmount
        If changeAmount < 0 Then changeAmount = 0

        Dim subTotal As Decimal = totalAmount / 1.12D
        Dim vatAmount As Decimal = totalAmount - subTotal

        If amountPaid < totalAmount Then
            MessageBox.Show("Amount paid cannot be less than the total amount.", "Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtAmountPaid.Focus()
            Exit Sub
        End If

        If String.IsNullOrEmpty(ComboBoxPaymentMethod.Text) Then
            MessageBox.Show("Please select a payment method.")
            Exit Sub
        End If

        ' GCash Validation
        If ComboBoxPaymentMethod.Text = "Gcash" Then
            Dim refNo As String = txtReferenceNo.Text.Trim()
            If String.IsNullOrWhiteSpace(refNo) Then
                MessageBox.Show("Please enter the GCash Reference Number.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtReferenceNo.Focus()
                Exit Sub
            End If

            If Not System.Text.RegularExpressions.Regex.IsMatch(refNo, "^\d{13}$") Then
                Dim confirm = MessageBox.Show("Standard GCash reference numbers are 13 digits. Proceed anyway?",
                                    "Format Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If confirm = DialogResult.No Then
                    txtReferenceNo.Focus()
                    Exit Sub
                End If
            End If
        End If

        ' 2. Database Operation
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            Try
                con.Open()
                Using trans As SqlTransaction = con.BeginTransaction()
                    Try
                        Dim sql As String = "INSERT INTO Receipts (AppointmentID, PatientID, UserID, TotalAmount, PaymentMethod, ReferenceNumber, SubTotal, VATAmount, GrandTotal, AmountPaid, ChangeAmount) " &
                                        "VALUES (@AID, @PID, @UID, @Total, @Method, @Ref, @Sub, @Vat, @Grand, @Paid, @Change)"

                        Using cmd As New SqlCommand(sql, con, trans)
                            cmd.Parameters.Add("@AID", SqlDbType.Int).Value = SelectedAppointmentID
                            cmd.Parameters.Add("@PID", SqlDbType.Int).Value = SelectedPatientID
                            cmd.Parameters.Add("@UID", SqlDbType.Int).Value = SystemSession.LoggedInUserID
                            cmd.Parameters.Add("@Total", SqlDbType.Decimal).Value = totalAmount
                            cmd.Parameters("@Total").Precision = 18
                            cmd.Parameters("@Total").Scale = 2
                            cmd.Parameters.Add("@Method", SqlDbType.VarChar).Value = ComboBoxPaymentMethod.Text
                            cmd.Parameters.Add("@Ref", SqlDbType.VarChar).Value = If(ComboBoxPaymentMethod.Text = "Gcash", txtReferenceNo.Text.Trim(), DBNull.Value)
                            cmd.Parameters.Add("@Sub", SqlDbType.Decimal).Value = subTotal
                            cmd.Parameters.Add("@Vat", SqlDbType.Decimal).Value = vatAmount
                            cmd.Parameters.Add("@Grand", SqlDbType.Decimal).Value = totalAmount
                            cmd.Parameters.Add("@Paid", SqlDbType.Decimal).Value = amountPaid
                            cmd.Parameters.Add("@Change", SqlDbType.Decimal).Value = changeAmount

                            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                            If rowsAffected > 0 Then
                                trans.Commit()

                                ' Audit Logging
                                Dim auditMsg As String = String.Format("Processed payment of P{0} for patient {1}. Change: P{2}",
                                                                  totalAmount.ToString("N2"),
                                                                  SelectedPatientName,
                                                                  changeAmount.ToString("N2"))
                                SystemSession.LogAudit(auditMsg, "Payment", SystemSession.LoggedInUserID, SystemSession.LoggedInFullName, SystemSession.LoggedInRole)

                                ' Flash Preview
                                Dim flashMsg As String = AdminDBPaymentReceiptPrinter.GetReceiptFlashPreview(
                                    SelectedPatientName,
                                    SelectedDentistName,
                                    SelectedTreatmentNotes,
                                    totalAmount.ToString("F2"),
                                    amountPaid.ToString("F2"),
                                    ComboBoxPaymentMethod.Text,
                                    txtReferenceNo.Text.Trim(),
                                    TryCast(dgvServices.DataSource, DataTable),
                                    GetFollowUps()
                                )

                                MessageBox.Show(flashMsg, "RECEIPT PREVIEW - This is exactly how it will be printed",
                                 MessageBoxButtons.OK, MessageBoxIcon.Information)

                                ' Ask to Print
                                Dim askPrint As DialogResult = MessageBox.Show("Would you like to print the receipt now?",
                                              "Print Receipt", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                                If askPrint = DialogResult.Yes Then
                                    Dim dtServicesPrint As DataTable = TryCast(dgvServices.DataSource, DataTable)
                                    If dtServicesPrint Is Nothing Then dtServicesPrint = New DataTable()
                                    Dim dtFollowUpsPrint As DataTable = GetFollowUps()

                                    AdminDBPaymentReceiptPrinter.PrintReceipt(
                                        SelectedPatientName,
                                        SelectedDentistName,
                                        SelectedTreatmentNotes,
                                        totalAmount.ToString("F2"),
                                        amountPaid.ToString("F2"),
                                        ComboBoxPaymentMethod.Text,
                                        txtReferenceNo.Text,
                                        dtServicesPrint,
                                        dtFollowUpsPrint
                                    )
                                End If

                                GoTo SuccessCleanup
                            Else
                                trans.Rollback()
                                MessageBox.Show("Save failed: No database rows were affected.")
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

    ' ==================================================================
    ' REGION: INVENTORY & PRESCRIPTION ITEMS (NEW FEATURE)
    ' ==================================================================
    Private Sub SetupReceiptGrid()
        dgvReceiptItems.Columns.Clear()

        dgvReceiptItems.Columns.Add("ItemID", "ItemID")
        dgvReceiptItems.Columns.Add("ItemName", "Item Name")
        dgvReceiptItems.Columns.Add("Price", "Price")
        dgvReceiptItems.Columns.Add("Quantity", "Qty")
        dgvReceiptItems.Columns.Add("Subtotal", "Subtotal")

        dgvReceiptItems.Columns("ItemName").HeaderText = "Item"
        dgvReceiptItems.Columns("Quantity").HeaderText = "Quantity"
        dgvReceiptItems.Columns("Price").HeaderText = "Price"
        dgvReceiptItems.Columns("Subtotal").HeaderText = "Total"

        dgvReceiptItems.Columns("ItemID").Visible = False

        ' Important for right-click functionality
        dgvReceiptItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvReceiptItems.AllowUserToAddRows = False
        dgvReceiptItems.ReadOnly = True
    End Sub
    ' ====================== RIGHT-CLICK TO REMOVE ITEM ======================
    Private Sub dgvReceiptItems_MouseDown(sender As Object, e As MouseEventArgs) Handles dgvReceiptItems.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim hit As DataGridView.HitTestInfo = dgvReceiptItems.HitTest(e.X, e.Y)

            If hit.RowIndex >= 0 AndAlso hit.RowIndex < dgvReceiptItems.Rows.Count Then
                dgvReceiptItems.ClearSelection()
                dgvReceiptItems.Rows(hit.RowIndex).Selected = True

                ' Show confirmation before deleting
                Dim result As DialogResult = MessageBox.Show("Remove this item from the receipt?",
                                                       "Remove Item",
                                                       MessageBoxButtons.YesNo,
                                                       MessageBoxIcon.Question)

                If result = DialogResult.Yes Then
                    dgvReceiptItems.Rows.RemoveAt(hit.RowIndex)
                    UpdateGrandTotalDisplay()   ' Refresh total after removal
                End If
            End If
        End If
    End Sub
    Private Sub LoadInventoryItems(Optional search As String = "")
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            Dim sql As String = "
                SELECT 
                    ItemID,
                    ItemName AS [Item],
                    Price AS [Unit Price],
                    Quantity AS [Stock]
                FROM ItemManagement
                WHERE ItemName LIKE @Search OR CONVERT(VARCHAR(50), ItemID) LIKE @Search
            "

            Using cmd As New SqlCommand(sql, con)
                cmd.Parameters.AddWithValue("@Search", "%" & search & "%")

                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)
                dgvInventoryItems.DataSource = dt

                If dgvInventoryItems.Columns.Contains("ItemID") Then
                    dgvInventoryItems.Columns("ItemID").Visible = False
                End If
            End Using
        End Using
    End Sub

    Private Sub dgvInventoryItems_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInventoryItems.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Dim row = dgvInventoryItems.Rows(e.RowIndex)

        Dim id As Integer = CInt(row.Cells("ItemID").Value)
        Dim name As String = row.Cells("Item").Value.ToString()
        Dim price As Decimal = CDec(row.Cells("Unit Price").Value)
        Dim availableStock As Integer = CInt(row.Cells("Stock").Value)

        Dim qtyStr As String = InputBox($"Enter quantity for {name} (Available: {availableStock})", "Prescription Item", "1")
        Dim qty As Integer

        If Not Integer.TryParse(qtyStr, qty) OrElse qty <= 0 Then Exit Sub

        ' Block if not enough stock
        If qty > availableStock Then
            MessageBox.Show($"Not enough stock! Available only: {availableStock}", "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Check if item already exists
        For Each r As DataGridViewRow In dgvReceiptItems.Rows
            If Not r.IsNewRow AndAlso CInt(r.Cells("ItemID").Value) = id Then
                Dim existingQty As Integer = CInt(r.Cells("Quantity").Value)
                Dim newQty As Integer = existingQty + qty

                If newQty > availableStock Then
                    MessageBox.Show($"Total exceeds stock! Available: {availableStock}", "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If

                r.Cells("Quantity").Value = newQty
                r.Cells("Subtotal").Value = price * newQty
                UpdateGrandTotalDisplay()
                ClearAllSelections()
                Exit Sub
            End If
        Next

        ' Add new row
        Dim subtotal As Decimal = price * qty
        dgvReceiptItems.Rows.Add(id, name, price, qty, subtotal)

        UpdateGrandTotalDisplay()
        ClearAllSelections()
    End Sub

    ' ==================================================================
    ' REGION: CALCULATION HELPERS (Single Source of Truth)
    ' ==================================================================
    Private Function GetItemTotal() As Decimal
        Dim total As Decimal = 0D
        For Each row As DataGridViewRow In dgvReceiptItems.Rows
            If Not row.IsNewRow Then
                total += Convert.ToDecimal(row.Cells("Subtotal").Value)
            End If
        Next
        Return total
    End Function

    Private Sub UpdateGrandTotalDisplay()
        Dim grandTotal As Decimal = currentTotal + GetItemTotal()

        ' VAT Calculation (12%)
        Dim subTotal As Decimal = If(grandTotal > 0, grandTotal / 1.12D, 0D)
        Dim vatAmount As Decimal = grandTotal - subTotal

        ' Update UI Labels
        lblTotal.Text = "Total: PHP " & grandTotal.ToString("N2")
        lblSubtotal.Text = "Subtotal: " & subTotal.ToString("N2")
        lblVATAmount.Text = "VAT (12%): " & vatAmount.ToString("N2")

        ' Auto-fill for GCash
        If ComboBoxPaymentMethod.Text = "Gcash" Then
            txtAmountPaid.Text = grandTotal.ToString("F2")
        End If

        UpdateButtonState()
    End Sub

    ' ==================================================================
    ' REGION: FOLLOW-UPS & HELPER METHODS
    ' ==================================================================
    Private Function GetFollowUps() As DataTable
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim sql As String = "SELECT FollowUpDate, Reason FROM PatientFollowUps WHERE AppointmentID = @AID"

            Using cmd As New SqlCommand(sql, con)
                cmd.Parameters.AddWithValue("@AID", SelectedAppointmentID)
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)
                Return dt
            End Using
        End Using
    End Function

    ' ==================================================================
    ' REGION: UI HELPERS & EVENT HANDLERS
    ' ==================================================================
    Private Sub ClearAllSelections()
        dgvInventoryItems.ClearSelection()
        dgvReceiptItems.ClearSelection()

        If dgvServices IsNot Nothing Then
            dgvServices.ClearSelection()
        End If
    End Sub
    Private Sub ClearBillingUI()
        SelectedAppointmentID = 0
        SelectedPatientID = 0

        patient_name.Text = "---"
        dentist_name.Text = "---"

        txtAmountPaid.Clear()
        txtReferenceNo.Clear()
        dgvReceiptItems.Rows.Clear()
        TextBoxPrescriptionNotes.Clear()

        currentTotal = 0D

        lblTotal.Text = "Total Amount: PHP 0.00"
        lblSubtotal.Text = "Subtotal: 0.00"
        lblVATAmount.Text = "VAT (12%): 0.00"

        txtReferenceNo.Enabled = False
        SetPrescriptionControlsEnabled(False)
        dgvServices.DataSource = Nothing

        ' Keep services and all other grids unselected
        ClearAllSelections()
        dgvPendingPayments.ClearSelection()
        UpdateButtonState()
    End Sub

    Private Sub SetPrescriptionControlsEnabled(enabled As Boolean)
        dgvInventoryItems.Enabled = enabled
        dgvReceiptItems.Enabled = enabled
        ItemSearch.Enabled = enabled
    End Sub

    Private Sub UpdateButtonState()
        Dim hasMethod As Boolean = Not String.IsNullOrEmpty(ComboBoxPaymentMethod.Text)
        Dim hasAmount As Boolean = Not String.IsNullOrWhiteSpace(txtAmountPaid.Text)

        Dim gcashValid As Boolean = True
        If ComboBoxPaymentMethod.Text = "Gcash" Then
            gcashValid = System.Text.RegularExpressions.Regex.IsMatch(txtReferenceNo.Text.Trim(), "^\d{13}$")
        End If

        ButtonGenerateReceipt.Enabled = hasMethod AndAlso hasAmount AndAlso gcashValid
    End Sub

    ' ==================================================================
    ' REGION: INPUT VALIDATION (KeyPress & TextChanged)
    ' ==================================================================
    Private Sub txtReferenceNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtReferenceNo.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtReferenceNo_TextChanged(sender As Object, e As EventArgs) Handles txtReferenceNo.TextChanged
        UpdateButtonState()
    End Sub
    Private Sub txtAmountPaid_TextChanged(sender As Object, e As EventArgs) Handles txtAmountPaid.TextChanged
        UpdateButtonState()
    End Sub

    Private Sub txtAmountPaid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmountPaid.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso (e.KeyChar <> "."c) Then
            e.Handled = True
        End If

        If (e.KeyChar = "."c) AndAlso (DirectCast(sender, TextBox).Text.IndexOf("."c) > -1) Then
            e.Handled = True
        End If
    End Sub


    Private Sub ItemSearch_TextChanged(sender As Object, e As EventArgs) Handles ItemSearch.TextChanged
        LoadInventoryItems(ItemSearch.Text.Trim())
    End Sub

    ' ==================================================================
    ' REGION: NAVIGATION & MISC BUTTONS
    ' ==================================================================
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearBillingUI()
    End Sub

    ' Empty handler (kept for compatibility)

End Class