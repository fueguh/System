Imports System.Data.SqlClient

Public Class AdminDBPaymentHistory

    Private connectionString As String = My.Settings.DentalDBConnection2

    ' Selected Data for Reprint
    Private SelectedAppointmentID As Integer = 0
    Private SelectedPatientName As String = ""
    Private SelectedTreatmentNotes As String = ""
    Private SelectedDentistName As String = ""
    Private SelectedRefNo As String = ""
    Private SelectedPaymentMethod As String = ""

    ' Correct variables from Receipts table (matching your current table)
    Private SelectedTotalAmount As String = "0.00"     ' Gross Total (what patient paid)
    Private SelectedVatExempt As String = "0.00"      ' VATableSales (Net of VAT)
    Private SelectedVatAmount As String = "0.00"
    Private SelectedAmountPaid As String = "0.00"
    Private SelectedChange As String = "0.00"

    Private dtServicesForPrinting As New DataTable()
    Private dtFollowUpsForPrinting As New DataTable()

    Private Sub AdminDBPaymentHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPaymentHistory()

        dgvHistory.ReadOnly = True
        dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvHistory.AllowUserToAddRows = False
        clearform()
    End Sub

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
                    INNER JOIN Users U ON A.UserID = U.UserID
                    "

                If Not String.IsNullOrEmpty(searchName) Then
                    sql &= " WHERE (P.FullName LIKE @search 
                            OR U.FullName LIKE @search 
                            OR R.ReferenceNumber LIKE @search
                            OR R.PaymentMethod LIKE @search)"
                End If

                sql &= " ORDER BY R.DateIssued DESC"

                Using cmd As New SqlCommand(sql, con)
                    If Not String.IsNullOrEmpty(searchName) Then
                        cmd.Parameters.AddWithValue("@search", "%" & searchName & "%")
                    End If

                    Dim da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    dgvHistory.DataSource = dt

                    ' Highlight VOIDED rows
                    For Each row As DataGridViewRow In dgvHistory.Rows
                        If row.Cells("Status").Value IsNot Nothing AndAlso
                           row.Cells("Status").Value.ToString() = "Voided" Then
                            row.DefaultCellStyle.BackColor = Color.LightGray
                            row.DefaultCellStyle.ForeColor = Color.Red
                        End If
                    Next

                    ' Format Currency Columns
                    If dgvHistory.Columns.Contains("Total Bill") Then dgvHistory.Columns("Total Bill").DefaultCellStyle.Format = "N2"
                    If dgvHistory.Columns.Contains("Cash Tendered") Then dgvHistory.Columns("Cash Tendered").DefaultCellStyle.Format = "N2"
                    If dgvHistory.Columns.Contains("Change Given") Then dgvHistory.Columns("Change Given").DefaultCellStyle.Format = "N2"
                    If dgvHistory.Columns.Contains("VATable Sales") Then dgvHistory.Columns("VATable Sales").DefaultCellStyle.Format = "N2"
                    If dgvHistory.Columns.Contains("VATAmount") Then dgvHistory.Columns("VATAmount").DefaultCellStyle.Format = "N2"

                    ' Hide ID columns
                    If dgvHistory.Columns.Contains("ReceiptID") Then dgvHistory.Columns("ReceiptID").Visible = False
                    If dgvHistory.Columns.Contains("AppointmentID") Then dgvHistory.Columns("AppointmentID").Visible = False
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading history: " & ex.Message)
        End Try
    End Sub

    Private Sub txtSearchPatient_TextChanged(sender As Object, e As EventArgs) Handles txtSearchPatient.TextChanged
        LoadPaymentHistory(txtSearchPatient.Text.Trim())
    End Sub

    ' ====================== REPRINT FUNCTION ======================
    Private Sub btnReprint_Click(sender As Object, e As EventArgs) Handles btnReprint.Click
        If dgvHistory.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a record from the history list.")
            Exit Sub
        End If

        Dim row = dgvHistory.SelectedRows(0)

        ' Prevent reprint if voided
        If row.Cells("Status").Value IsNot Nothing AndAlso
           row.Cells("Status").Value.ToString() = "Voided" Then
            MessageBox.Show("Cannot reprint a voided receipt.")
            Exit Sub
        End If

        SelectedAppointmentID = CInt(row.Cells("AppointmentID").Value)
        SelectedPatientName = row.Cells("Patient Name").Value.ToString()
        SelectedPaymentMethod = row.Cells("Method").Value.ToString()
        SelectedRefNo = If(row.Cells("Ref No").Value IsNot DBNull.Value, row.Cells("Ref No").Value.ToString(), "")

        ' Get values from DB
        SelectedTotalAmount = CDec(row.Cells("Total Bill").Value).ToString("F2")
        SelectedAmountPaid = CDec(row.Cells("Cash Tendered").Value).ToString("F2")
        SelectedChange = CDec(row.Cells("Change Given").Value).ToString("F2")
        SelectedVatAmount = If(row.Cells("VATAmount").Value IsNot DBNull.Value,
                              CDec(row.Cells("VATAmount").Value).ToString("F2"), "0.00")

        ' Calculate VATable Sales from Total (safest for reprint)
        Dim totalVal As Decimal = CDec(SelectedTotalAmount)
        Dim vatExemptVal As Decimal = If(totalVal > 0, totalVal / 1.12D, 0D)
        SelectedVatExempt = vatExemptVal.ToString("F2")

        ' We use TotalAmount as Gross Subtotal
        Dim selectedSubTotal As String = SelectedTotalAmount

        FetchDetailsForReprint(SelectedAppointmentID)

        ' === FLASH PREVIEW ===
        Dim flashMsg As String = AdminDBPaymentReceiptPrinter.GetReceiptFlashPreview(
            SelectedPatientName,
            SelectedDentistName,
            SelectedTreatmentNotes,
            selectedSubTotal,          ' Gross Subtotal
            SelectedVatExempt,         ' VATable Sales
            SelectedVatAmount,         ' VAT Amount
            SelectedTotalAmount,       ' Total
            SelectedAmountPaid,
            SelectedChange,
            SelectedPaymentMethod,
            SelectedRefNo,
            dtServicesForPrinting,
            dtFollowUpsForPrinting
        )

        MessageBox.Show(flashMsg, "RECEIPT PREVIEW - This is exactly how it will be printed",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' Ask to print
        Dim askPrint As DialogResult = MessageBox.Show("Would you like to print the receipt now?",
                                      "Print Receipt", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If askPrint = DialogResult.Yes Then
            AdminDBPaymentReceiptPrinter.PrintReceipt(
                SelectedPatientName,
                SelectedDentistName,
                SelectedTreatmentNotes,
                selectedSubTotal,
                SelectedVatExempt,
                SelectedVatAmount,
                SelectedTotalAmount,
                SelectedAmountPaid,
                SelectedChange,
                SelectedPaymentMethod,
                SelectedRefNo,
                dtServicesForPrinting,
                dtFollowUpsForPrinting
            )
        End If
    End Sub

    Private Sub FetchDetailsForReprint(apptID As Integer)
        Using con As New SqlConnection(connectionString)
            con.Open()

            ' Get Dentist Name and Treatment Notes
            Dim sqlDetails As String = "
            SELECT 
                U.FullName AS DentistName, 
                ISNULL(T.TreatmentNotes, 'No notes recorded') AS Notes 
            FROM Appointments A
            INNER JOIN Users U ON A.UserID = U.UserID
            LEFT JOIN TreatmentRecords T ON A.AppointmentID = T.AppointmentID
            WHERE A.AppointmentID = @AID"

            Using cmd As New SqlCommand(sqlDetails, con)
                cmd.Parameters.AddWithValue("@AID", apptID)
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        SelectedDentistName = reader("DentistName").ToString()
                        SelectedTreatmentNotes = reader("Notes").ToString()
                    Else
                        SelectedDentistName = "N/A"
                        SelectedTreatmentNotes = "No notes recorded"
                    End If
                End Using
            End Using

            ' Get Services
            Dim cmdSvc As New SqlCommand("SELECT S.ServiceName, S.Price 
                                          FROM AppointmentServices ASV 
                                          INNER JOIN Services S ON ASV.ServiceID = S.ServiceID 
                                          WHERE ASV.AppointmentID = @AID", con)
            cmdSvc.Parameters.AddWithValue("@AID", apptID)

            Dim da As New SqlDataAdapter(cmdSvc)
            dtServicesForPrinting.Clear()
            da.Fill(dtServicesForPrinting)

            ' Get Follow-Ups
            Dim cmdFU As New SqlCommand("
            SELECT FollowUpDate, Reason 
            FROM PatientFollowUps 
            WHERE AppointmentID = @AID
            ORDER BY FollowUpDate ASC", con)

            cmdFU.Parameters.AddWithValue("@AID", apptID)

            Dim daFU As New SqlDataAdapter(cmdFU)
            dtFollowUpsForPrinting.Clear()
            daFU.Fill(dtFollowUpsForPrinting)
        End Using
    End Sub

    Private Sub VoidReceipt(receiptID As Integer)
        If SystemSession.LoggedInRole <> "Admin" Then
            MessageBox.Show("Only admins can void receipts.")
            Exit Sub
        End If

        Using con As New SqlConnection(connectionString)
            con.Open()
            Using trans As SqlTransaction = con.BeginTransaction()
                Try
                    Dim checkCmd As New SqlCommand("SELECT Status FROM Receipts WHERE ReceiptID=@id", con, trans)
                    checkCmd.Parameters.AddWithValue("@id", receiptID)
                    Dim status = checkCmd.ExecuteScalar()?.ToString()

                    If status = "Voided" Then
                        MessageBox.Show("This receipt is already voided.")
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

                    SystemSession.LogAudit($"Voided Receipt #{receiptID}", "Payment History",
                        SystemSession.LoggedInUserID, SystemSession.LoggedInFullName, SystemSession.LoggedInRole)

                    MessageBox.Show("Receipt voided successfully.")
                    LoadPaymentHistory()

                Catch ex As Exception
                    If trans.Connection IsNot Nothing Then trans.Rollback()
                    MessageBox.Show("Error voiding receipt: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Private Sub dgvHistory_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvHistory.CellMouseClick
        If e.RowIndex < 0 Then Exit Sub

        If e.Button = MouseButtons.Right Then
            Dim row = dgvHistory.Rows(e.RowIndex)
            Dim receiptID = CInt(row.Cells("ReceiptID").Value)
            Dim status = If(row.Cells("Status").Value, "").ToString()

            If status = "Voided" Then
                MessageBox.Show("This receipt is already voided.")
                Exit Sub
            End If

            If MessageBox.Show("Void this receipt?", "Confirm Void",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                VoidReceipt(receiptID)
            End If
        End If
    End Sub

    Private Sub clearform()
        txtSearchPatient.Clear()
        dgvHistory.ClearSelection()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

End Class