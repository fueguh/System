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
                SELECT STRING_AGG(
                    CONVERT(VARCHAR, F.FollowUpDate, 120) + ' - ' + ISNULL(F.Reason, ''), 
                    ' | '
                )
                FROM PatientFollowUps F 
                WHERE F.AppointmentID = A.AppointmentID
            ), 'No follow-ups') AS [Follow Ups],

            ISNULL(R.Status, 'Unpaid') AS [Payment Status]

        FROM Appointments A
        INNER JOIN Patients P ON A.PatientID = P.PatientID
        INNER JOIN Users U ON A.UserID = U.UserID
        LEFT JOIN TreatmentRecords T ON A.AppointmentID = T.AppointmentID
        LEFT JOIN AppointmentServices AP ON A.AppointmentID = AP.AppointmentID
        LEFT JOIN Services S ON AP.ServiceID = S.ServiceID
        LEFT JOIN Receipts R ON A.AppointmentID = R.AppointmentID

        WHERE A.Status = 'Completed'
          AND (R.Status IS NULL OR R.Status <> 'Active')

        GROUP BY 
            A.AppointmentID, 
            P.PatientID, 
            P.FullName, 
            U.FullName, 
            A.Date, 
            T.TreatmentNotes, 
            T.Prescriptions,
            R.Status

        ORDER BY A.Date DESC"

            Using da As New SqlDataAdapter(sql, con)
                Dim dt As New DataTable()
                da.Fill(dt)
                dgvPendingPayments.DataSource = dt
            End Using

            ' Hide IDs
            For Each col As DataGridViewColumn In dgvPendingPayments.Columns
                Select Case col.Name
                    Case "AppointmentID", "PatientID"
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

            If dgvServices.Columns.Contains("ServiceID") Then dgvServices.Columns("ServiceID").Visible = False
            If dgvServices.Columns.Contains("ServiceName") Then dgvServices.Columns("ServiceName").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

            CalculateServiceTotal()
        End Using
    End Sub

    Private Sub CalculateServiceTotal()
        Dim dt As DataTable = TryCast(dgvServices.DataSource, DataTable)
        currentTotal = If(dt Is Nothing, 0D,
                        If(IsDBNull(dt.Compute("SUM(Price)", "")), 0D, Convert.ToDecimal(dt.Compute("SUM(Price)", ""))))

        UpdateGrandTotalDisplay()
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
            UpdateGrandTotalDisplay()
        Else
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

        If SystemSession.LoggedInUserID <= 0 Then
            MessageBox.Show("Error: No logged-in User ID found. Please re-login.")
            Exit Sub
        End If

        If SelectedAppointmentID = 0 Then
            MessageBox.Show("Please select an appointment first.")
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtAmountPaid.Text) Then
            MessageBox.Show("Enter amount paid.")
            Exit Sub
        End If

        Dim serviceTotal As Decimal = currentTotal
        Dim itemTotal As Decimal = GetItemTotal()
        Dim totalAmount As Decimal = serviceTotal + itemTotal

        Dim amountPaid As Decimal
        Decimal.TryParse(txtAmountPaid.Text, amountPaid)

        If amountPaid < totalAmount Then
            MessageBox.Show("Amount paid cannot be less than total.")
            Exit Sub
        End If

        Dim changeAmount As Decimal = amountPaid - totalAmount
        Dim vatAmount As Decimal = If(totalAmount > 0, totalAmount - (totalAmount / 1.12D), 0D)
        Dim vatExempt As Decimal = totalAmount - vatAmount

        If String.IsNullOrWhiteSpace(ComboBoxPaymentMethod.Text) Then
            MessageBox.Show("Select payment method.")
            Exit Sub
        End If

        If ComboBoxPaymentMethod.Text = "Gcash" Then
            Dim refNo As String = txtReferenceNo.Text.Trim()

            If String.IsNullOrWhiteSpace(refNo) Then
                MessageBox.Show("Enter GCash reference number.")
                Exit Sub
            End If
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Using trans As SqlTransaction = con.BeginTransaction()

                Try
                    ' ================= CHECK DUPLICATE =================
                    Dim checkCmd As New SqlCommand("
                    SELECT ReceiptID FROM Receipts WHERE AppointmentID = @AID
                ", con, trans)

                    checkCmd.Parameters.AddWithValue("@AID", SelectedAppointmentID)

                    Dim existingID As Object = checkCmd.ExecuteScalar()

                    Dim receiptID As Integer = 0

                    If existingID IsNot Nothing Then
                        receiptID = Convert.ToInt32(existingID)

                        ' ================= UPDATE =================
                        Dim updateCmd As New SqlCommand("
                        UPDATE Receipts
                        SET TotalAmount = @Total,
                            VATableSales = @VATable,
                            VATAmount = @VAT,
                            PaymentMethod = @Method,
                            ReferenceNumber = @Ref,
                            AmountPaid = @Paid,
                            ChangeAmount = @Change,
                            Status = 'Completed'
                        WHERE ReceiptID = @RID
                    ", con, trans)

                        updateCmd.Parameters.AddWithValue("@RID", receiptID)

                        updateCmd.Parameters.AddWithValue("@Total", totalAmount)
                        updateCmd.Parameters.AddWithValue("@VATable", vatExempt)
                        updateCmd.Parameters.AddWithValue("@VAT", vatAmount)
                        updateCmd.Parameters.AddWithValue("@Method", ComboBoxPaymentMethod.Text)
                        updateCmd.Parameters.AddWithValue("@Ref",
                        If(ComboBoxPaymentMethod.Text = "Gcash", txtReferenceNo.Text.Trim(), DBNull.Value))
                        updateCmd.Parameters.AddWithValue("@Paid", amountPaid)
                        updateCmd.Parameters.AddWithValue("@Change", changeAmount)

                        updateCmd.ExecuteNonQuery()

                    Else
                        ' ================= INSERT =================
                        Dim insertCmd As New SqlCommand("
                        INSERT INTO Receipts
                        (AppointmentID, PatientID, UserID, TotalAmount, VATableSales, VATAmount,
                         PaymentMethod, ReferenceNumber, AmountPaid, ChangeAmount, Status)
                        VALUES
                        (@AID, @PID, @UID, @Total, @VATable, @VAT,
                         @Method, @Ref, @Paid, @Change, 'Completed')
                    ", con, trans)

                        insertCmd.Parameters.AddWithValue("@AID", SelectedAppointmentID)
                        insertCmd.Parameters.AddWithValue("@PID", SelectedPatientID)
                        insertCmd.Parameters.AddWithValue("@UID", SystemSession.LoggedInUserID)

                        insertCmd.Parameters.AddWithValue("@Total", totalAmount)
                        insertCmd.Parameters.AddWithValue("@VATable", vatExempt)
                        insertCmd.Parameters.AddWithValue("@VAT", vatAmount)

                        insertCmd.Parameters.AddWithValue("@Method", ComboBoxPaymentMethod.Text)
                        insertCmd.Parameters.AddWithValue("@Ref",
                        If(ComboBoxPaymentMethod.Text = "Gcash", txtReferenceNo.Text.Trim(), DBNull.Value))
                        insertCmd.Parameters.AddWithValue("@Paid", amountPaid)
                        insertCmd.Parameters.AddWithValue("@Change", changeAmount)

                        insertCmd.ExecuteNonQuery()
                    End If

                    ' ================= COMMIT =================
                    trans.Commit()

                Catch ex As Exception
                    trans.Rollback()
                    MessageBox.Show("Transaction Error: " & ex.Message)
                    Exit Sub
                End Try
            End Using
        End Using

        ' ================= AUDIT =================
        SystemSession.LogAudit(
        $"Payment P{totalAmount:N2} for {SelectedPatientName}",
        "Payment",
        SystemSession.LoggedInUserID,
        SystemSession.LoggedInFullName,
        SystemSession.LoggedInRole
    )

        LoadPendingPayments()
        ClearBillingUI()

        MessageBox.Show("Payment successful!", "Success")

    End Sub

    ' ==================================================================
    ' REGION: INVENTORY & PRESCRIPTION ITEMS (NEW FEATURE)
    ' ==================================================================
    ' (Your inventory code is fine - no changes needed here)

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
        Dim itemTotal As Decimal = GetItemTotal()
        Dim grandTotal As Decimal = currentTotal + itemTotal   ' This is the real Total Amount Due

        ' Correct VAT Breakdown (VAT Inclusive)
        Dim vatAmount As Decimal = If(grandTotal > 0, grandTotal - (grandTotal / 1.12D), 0D)
        Dim vatExempt As Decimal = grandTotal - vatAmount      ' VATable Sales

        ' Update UI Labels
        lblTotal.Text = "Total: PHP " & grandTotal.ToString("N2")
        lblSubtotal.Text = "Subtotal: " & grandTotal.ToString("N2")   ' Show Gross as Subtotal
        lblVATAmount.Text = "VAT (12%): " & vatAmount.ToString("N2")

        ' Change calculation
        Dim amountPaid As Decimal = 0
        Decimal.TryParse(txtAmountPaid.Text, amountPaid)

        Dim changeAmount As Decimal = amountPaid - grandTotal
        If changeAmount < 0 Then changeAmount = 0

        lblChange.Text = "Change: PHP " & changeAmount.ToString("N2")

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
        lblChange.Text = "Change: PHP 0.00"

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
        UpdateGrandTotalDisplay()
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