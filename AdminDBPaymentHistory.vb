Imports System.Data.SqlClient

Public Class AdminDBPaymentHistory

    Private connectionString As String = My.Settings.DentalDBConnection2

    ' Selected Data for Reprint
    Private SelectedAppointmentID As Integer = 0
    Private SelectedReceiptID As Integer = 0
    Private SelectedPatientName As String = ""
    Private SelectedTreatmentNotes As String = ""
    Private SelectedDentistName As String = ""
    Private SelectedRefNo As String = ""
    Private SelectedPaymentMethod As String = ""

    ' Receipt values
    Private SelectedTotalAmount As Decimal = 0D
    Private SelectedVatExempt As Decimal = 0D
    Private SelectedVatAmount As Decimal = 0D
    Private SelectedAmountPaid As Decimal = 0D
    Private SelectedChange As Decimal = 0D

    Private dtServicesForPrinting As New DataTable()
    Private dtItemsForPrinting As New DataTable()
    Private dtFollowUpsForPrinting As New DataTable()

    ' ================= LOAD =================
    Private Sub AdminDBPaymentHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPaymentHistory()
        dgvHistory.ReadOnly = True
        dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvHistory.AllowUserToAddRows = False
        clearform()
    End Sub

    ' ================= LOAD HISTORY =================
    Private Sub LoadPaymentHistory(Optional searchName As String = "")
        Try
            Using con As New SqlConnection(connectionString)
                con.Open()

                Dim sql As String = "
                SELECT 
                    R.ReceiptID,
                    R.AppointmentID,
                    P.FullName AS [Patient Name],
                    U.FullName AS [Dentist],
                    R.TotalAmount AS [Total Bill],
                    R.VATableSales AS [VATable Sales],
                    R.VATAmount,
                    R.AmountPaid AS [Cash Tendered],
                    R.ChangeAmount AS [Change Given],
                    R.PaymentMethod AS [Method],
                    R.ReferenceNumber AS [Ref No],
                    R.DateIssued AS [Payment Date],
                    R.Status,
                    ISNULL((
                        SELECT STRING_AGG(
                            CONVERT(VARCHAR(20), F.FollowUpDate, 101) + ' (' + ISNULL(F.Reason, '') + ')', 
                            ', '
                        )
                        FROM PatientFollowUps F
                        WHERE F.AppointmentID = R.AppointmentID
                    ), 'None') AS [Follow-Ups]
                FROM Receipts R
                INNER JOIN Patients P ON R.PatientID = P.PatientID
                INNER JOIN Appointments A ON R.AppointmentID = A.AppointmentID
                INNER JOIN Users U ON A.UserID = U.UserID"

                If Not String.IsNullOrWhiteSpace(searchName) Then
                    sql &= " WHERE (P.FullName LIKE @search 
                              OR U.FullName LIKE @search 
                              OR R.ReferenceNumber LIKE @search
                              OR R.PaymentMethod LIKE @search)"
                End If

                sql &= " ORDER BY R.DateIssued DESC"

                Using cmd As New SqlCommand(sql, con)
                    If Not String.IsNullOrWhiteSpace(searchName) Then
                        cmd.Parameters.AddWithValue("@search", "%" & searchName & "%")
                    End If

                    Dim dt As New DataTable()
                    Dim da As New SqlDataAdapter(cmd)
                    da.Fill(dt)

                    dgvHistory.DataSource = dt

                    ' Hide ID columns
                    If dgvHistory.Columns.Contains("ReceiptID") Then dgvHistory.Columns("ReceiptID").Visible = False
                    If dgvHistory.Columns.Contains("AppointmentID") Then dgvHistory.Columns("AppointmentID").Visible = False

                    ' Highlight voided rows
                    For Each row As DataGridViewRow In dgvHistory.Rows
                        If row.Cells("Status").Value?.ToString() = "Voided" Then
                            row.DefaultCellStyle.BackColor = Color.LightGray
                            row.DefaultCellStyle.ForeColor = Color.Red
                        End If
                    Next

                    ' Number formatting
                    If dgvHistory.Columns.Contains("Total Bill") Then dgvHistory.Columns("Total Bill").DefaultCellStyle.Format = "N2"
                    If dgvHistory.Columns.Contains("Cash Tendered") Then dgvHistory.Columns("Cash Tendered").DefaultCellStyle.Format = "N2"
                    If dgvHistory.Columns.Contains("Change Given") Then dgvHistory.Columns("Change Given").DefaultCellStyle.Format = "N2"
                    If dgvHistory.Columns.Contains("VATable Sales") Then dgvHistory.Columns("VATable Sales").DefaultCellStyle.Format = "N2"
                    If dgvHistory.Columns.Contains("VATAmount") Then dgvHistory.Columns("VATAmount").DefaultCellStyle.Format = "N2"
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading history: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ================= SEARCH =================
    Private Sub txtSearchPatient_TextChanged(sender As Object, e As EventArgs) Handles txtSearchPatient.TextChanged
        LoadPaymentHistory(txtSearchPatient.Text.Trim())
    End Sub

    ' ================= REPRINT - WITH DEBUG =================
    Private Sub btnReprint_Click(sender As Object, e As EventArgs) Handles btnReprint.Click
        If dgvHistory.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a record to reprint.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim row = dgvHistory.SelectedRows(0)

        If row.Cells("Status").Value?.ToString() = "Voided" Then
            MessageBox.Show("Cannot reprint a voided receipt.", "Voided Receipt", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Capture basic info
        SelectedReceiptID = CInt(row.Cells("ReceiptID").Value)
        SelectedAppointmentID = CInt(row.Cells("AppointmentID").Value)
        SelectedPatientName = row.Cells("Patient Name").Value.ToString()
        SelectedPaymentMethod = row.Cells("Method").Value.ToString()
        SelectedRefNo = If(row.Cells("Ref No").Value IsNot DBNull.Value, row.Cells("Ref No").Value.ToString(), "")

        SelectedTotalAmount = Convert.ToDecimal(row.Cells("Total Bill").Value)
        SelectedAmountPaid = Convert.ToDecimal(row.Cells("Cash Tendered").Value)
        SelectedChange = Convert.ToDecimal(row.Cells("Change Given").Value)

        SelectedVatAmount = If(row.Cells("VATAmount").Value Is DBNull.Value, 0D, Convert.ToDecimal(row.Cells("VATAmount").Value))

        If SelectedVatAmount > 0 Then
            SelectedVatExempt = SelectedTotalAmount - SelectedVatAmount
        Else
            SelectedVatExempt = Math.Round(SelectedTotalAmount / 1.12D, 2)
        End If

        ' Fetch details
        FetchDetailsForReprint(SelectedAppointmentID, SelectedReceiptID)

        ' ================= DEBUG: Check what was loaded =================
        Dim msg As String = $"ReceiptID: {SelectedReceiptID}" & vbCrLf &
                           $"Services: {dtServicesForPrinting.Rows.Count} row(s)" & vbCrLf &
                           $"Items: {dtItemsForPrinting.Rows.Count} row(s)" & vbCrLf &
                           $"Follow-ups: {dtFollowUpsForPrinting.Rows.Count} row(s)"

        MessageBox.Show(msg, "Debug - Data Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information)

        If dtItemsForPrinting.Rows.Count = 0 Then
            MessageBox.Show("No items were found for this receipt in the ReceiptItems table." & vbCrLf &
                           "Make sure items were saved with ItemType = 'Item' when the payment was created.",
                           "No Items Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        ' Generate preview
        Dim flashMsg As String = AdminDBPaymentReceiptPrinter.GetReceiptFlashPreview(
            SelectedPatientName,
            SelectedDentistName,
            SelectedTreatmentNotes,
            SelectedTotalAmount.ToString("F2"),
            SelectedVatExempt.ToString("F2"),
            SelectedVatAmount.ToString("F2"),
            SelectedTotalAmount.ToString("F2"),
            SelectedAmountPaid.ToString("F2"),
            SelectedChange.ToString("F2"),
            SelectedPaymentMethod,
            SelectedRefNo,
            dtServicesForPrinting,
            dtItemsForPrinting,        ' ← Items DataTable
            dtFollowUpsForPrinting
        )

        MessageBox.Show(flashMsg, "Receipt Preview", MessageBoxButtons.OK, MessageBoxIcon.Information)

        If MessageBox.Show("Do you want to print this receipt?", "Confirm Print",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            AdminDBPaymentReceiptPrinter.PrintReceipt(
                SelectedPatientName,
                SelectedDentistName,
                SelectedTreatmentNotes,
                SelectedTotalAmount.ToString("F2"),
                SelectedVatExempt.ToString("F2"),
                SelectedVatAmount.ToString("F2"),
                SelectedTotalAmount.ToString("F2"),
                SelectedAmountPaid.ToString("F2"),
                SelectedChange.ToString("F2"),
                SelectedPaymentMethod,
                SelectedRefNo,
                dtServicesForPrinting,
                dtFollowUpsForPrinting,
                dtItemsForPrinting
            )
        End If
    End Sub

    ' ================= FETCH DETAILS FOR REPRINT =================
    Private Sub FetchDetailsForReprint(apptID As Integer, receiptID As Integer)
        dtServicesForPrinting.Clear()
        dtItemsForPrinting.Clear()
        dtFollowUpsForPrinting.Clear()

        Using con As New SqlConnection(connectionString)
            con.Open()

            ' 1. Dentist + Notes
            Dim sqlInfo As String = "
            SELECT 
                U.FullName AS DentistName,
                ISNULL(T.TreatmentNotes, 'No notes available.') AS Notes
            FROM Appointments A
            INNER JOIN Users U ON A.UserID = U.UserID
            LEFT JOIN TreatmentRecords T ON A.AppointmentID = T.AppointmentID
            WHERE A.AppointmentID = @AID"

            Using cmd As New SqlCommand(sqlInfo, con)
                cmd.Parameters.AddWithValue("@AID", apptID)
                Using r = cmd.ExecuteReader()
                    If r.Read() Then
                        SelectedDentistName = r("DentistName").ToString()
                        SelectedTreatmentNotes = r("Notes").ToString()
                    End If
                End Using
            End Using

            ' 2. Services
            Dim cmdSvc As New SqlCommand("
                SELECT S.ServiceName, S.Price 
                FROM AppointmentServices ASV
                INNER JOIN Services S ON ASV.ServiceID = S.ServiceID
                WHERE ASV.AppointmentID = @AID", con)
            cmdSvc.Parameters.AddWithValue("@AID", apptID)
            Dim daSvc As New SqlDataAdapter(cmdSvc)
            daSvc.Fill(dtServicesForPrinting)

            ' 3. ITEMS - FIXED to match your saving logic
            Dim cmdItems As New SqlCommand("
                SELECT 
                    ItemName,
                    Quantity,
                    UnitPrice AS Price
                FROM ReceiptItems 
                WHERE ReceiptID = @ReceiptID 
                  AND ItemType IN ('Item', 'Inventory')   -- ← Changed here
                ORDER BY ReceiptItemID", con)

            cmdItems.Parameters.AddWithValue("@ReceiptID", receiptID)
            Dim daItems As New SqlDataAdapter(cmdItems)
            daItems.Fill(dtItemsForPrinting)

            ' Safety: Ensure "Price" column exists for the preview/print
            If Not dtItemsForPrinting.Columns.Contains("Price") Then
                If dtItemsForPrinting.Columns.Contains("UnitPrice") Then
                    dtItemsForPrinting.Columns("UnitPrice").ColumnName = "Price"
                End If
            End If

            ' 4. Follow-ups
            Dim cmdFU As New SqlCommand("
                SELECT FollowUpDate, Reason
                FROM PatientFollowUps
                WHERE AppointmentID = @AID
                ORDER BY FollowUpDate", con)
            cmdFU.Parameters.AddWithValue("@AID", apptID)
            Dim daFU As New SqlDataAdapter(cmdFU)
            daFU.Fill(dtFollowUpsForPrinting)

        End Using
    End Sub

    ' ================= VOID ================= (unchanged)
    Private Sub VoidReceipt(receiptID As Integer)
        If SystemSession.LoggedInRole <> "Admin" Then
            MessageBox.Show("Only Admins can void receipts.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MessageBox.Show("Are you sure you want to void this receipt?", "Confirm Void",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
            Exit Sub
        End If

        Try
            Using con As New SqlConnection(connectionString)
                con.Open()
                Using trans = con.BeginTransaction()

                    Dim checkCmd As New SqlCommand("SELECT Status FROM Receipts WHERE ReceiptID=@id", con, trans)
                    checkCmd.Parameters.AddWithValue("@id", receiptID)
                    Dim status = If(checkCmd.ExecuteScalar(), "").ToString()

                    If status = "Voided" Then
                        MessageBox.Show("This receipt is already voided.", "Already Voided")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim voidCmd As New SqlCommand("
                        UPDATE Receipts 
                        SET Status = 'Voided',
                            VoidedAt = GETDATE(),
                            VoidedBy = @user
                        WHERE ReceiptID = @id", con, trans)

                    voidCmd.Parameters.AddWithValue("@id", receiptID)
                    voidCmd.Parameters.AddWithValue("@user", SystemSession.LoggedInFullName)
                    voidCmd.ExecuteNonQuery()

                    trans.Commit()
                End Using
            End Using

            SystemSession.LogAudit("Voided Receipt #" & receiptID, "Payment History",
                                   SystemSession.LoggedInUserID, SystemSession.LoggedInFullName, SystemSession.LoggedInRole)

            LoadPaymentHistory()
            MessageBox.Show("Receipt has been voided successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error voiding receipt: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ================= RIGHT CLICK VOID =================
    Private Sub dgvHistory_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvHistory.CellMouseClick
        If e.RowIndex < 0 Then Exit Sub

        If e.Button = MouseButtons.Right Then
            Dim row = dgvHistory.Rows(e.RowIndex)
            Dim receiptID = CInt(row.Cells("ReceiptID").Value)

            If row.Cells("Status").Value?.ToString() = "Voided" Then
                MessageBox.Show("This receipt is already voided.")
                Exit Sub
            End If

            VoidReceipt(receiptID)
        End If
    End Sub

    ' ================= CLEAR & BACK =================
    Private Sub clearform()
        txtSearchPatient.Clear()
        dgvHistory.ClearSelection()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

End Class