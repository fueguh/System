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

    ' Receipt values
    Private SelectedTotalAmount As Decimal = 0D
    Private SelectedVatExempt As Decimal = 0D
    Private SelectedVatAmount As Decimal = 0D
    Private SelectedAmountPaid As Decimal = 0D
    Private SelectedChange As Decimal = 0D

    Private dtServicesForPrinting As New DataTable()
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

                    If dgvHistory.Columns.Contains("ReceiptID") Then dgvHistory.Columns("ReceiptID").Visible = False
                    If dgvHistory.Columns.Contains("AppointmentID") Then dgvHistory.Columns("AppointmentID").Visible = False

                    ' highlight voided
                    For Each row As DataGridViewRow In dgvHistory.Rows
                        If row.Cells("Status").Value IsNot Nothing AndAlso
                           row.Cells("Status").Value.ToString() = "Voided" Then
                            row.DefaultCellStyle.BackColor = Color.LightGray
                            row.DefaultCellStyle.ForeColor = Color.Red
                        End If
                    Next

                    ' formatting
                    If dgvHistory.Columns.Contains("Total Bill") Then dgvHistory.Columns("Total Bill").DefaultCellStyle.Format = "N2"
                    If dgvHistory.Columns.Contains("Cash Tendered") Then dgvHistory.Columns("Cash Tendered").DefaultCellStyle.Format = "N2"
                    If dgvHistory.Columns.Contains("Change Given") Then dgvHistory.Columns("Change Given").DefaultCellStyle.Format = "N2"
                    If dgvHistory.Columns.Contains("VATable Sales") Then dgvHistory.Columns("VATable Sales").DefaultCellStyle.Format = "N2"
                    If dgvHistory.Columns.Contains("VATAmount") Then dgvHistory.Columns("VATAmount").DefaultCellStyle.Format = "N2"

                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading history: " & ex.Message)
        End Try
    End Sub

    ' ================= SEARCH =================
    Private Sub txtSearchPatient_TextChanged(sender As Object, e As EventArgs) Handles txtSearchPatient.TextChanged
        LoadPaymentHistory(txtSearchPatient.Text.Trim())
    End Sub

    ' ================= REPRINT =================
    Private Sub btnReprint_Click(sender As Object, e As EventArgs) Handles btnReprint.Click

        If dgvHistory.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a record.")
            Exit Sub
        End If

        Dim row = dgvHistory.SelectedRows(0)

        If row.Cells("Status").Value?.ToString() = "Voided" Then
            MessageBox.Show("Cannot reprint a voided receipt.")
            Exit Sub
        End If

        SelectedAppointmentID = CInt(row.Cells("AppointmentID").Value)
        SelectedPatientName = row.Cells("Patient Name").Value.ToString()
        SelectedPaymentMethod = row.Cells("Method").Value.ToString()
        SelectedRefNo = If(row.Cells("Ref No").Value IsNot DBNull.Value, row.Cells("Ref No").Value.ToString(), "")

        SelectedTotalAmount = Convert.ToDecimal(row.Cells("Total Bill").Value)
        SelectedAmountPaid = Convert.ToDecimal(row.Cells("Cash Tendered").Value)
        SelectedChange = Convert.ToDecimal(row.Cells("Change Given").Value)

        SelectedVatAmount = If(row.Cells("VATAmount").Value Is DBNull.Value,
                               0D,
                               Convert.ToDecimal(row.Cells("VATAmount").Value))

        SelectedVatExempt = If(SelectedTotalAmount > 0,
                               SelectedTotalAmount / 1.12D,
                               0D)

        FetchDetailsForReprint(SelectedAppointmentID)

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
            dtFollowUpsForPrinting
        )

        MessageBox.Show(flashMsg, "PREVIEW", MessageBoxButtons.OK, MessageBoxIcon.Information)

        If MessageBox.Show("Print receipt?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.Yes Then

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
                dtFollowUpsForPrinting
            )

        End If

    End Sub

    ' ================= DETAILS =================
    Private Sub FetchDetailsForReprint(apptID As Integer)

        Using con As New SqlConnection(connectionString)
            con.Open()

            Dim sql As String = "
            SELECT 
                U.FullName AS DentistName,
                ISNULL(T.TreatmentNotes,'No notes') AS Notes
            FROM Appointments A
            INNER JOIN Users U ON A.UserID = U.UserID
            LEFT JOIN TreatmentRecords T ON A.AppointmentID = T.AppointmentID
            WHERE A.AppointmentID = @AID"

            Using cmd As New SqlCommand(sql, con)
                cmd.Parameters.AddWithValue("@AID", apptID)

                Using r = cmd.ExecuteReader()
                    If r.Read() Then
                        SelectedDentistName = r("DentistName").ToString()
                        SelectedTreatmentNotes = r("Notes").ToString()
                    End If
                End Using
            End Using

            Dim cmdSvc As New SqlCommand("
                SELECT S.ServiceName, S.Price
                FROM AppointmentServices ASV
                INNER JOIN Services S ON ASV.ServiceID = S.ServiceID
                WHERE ASV.AppointmentID = @AID", con)

            cmdSvc.Parameters.AddWithValue("@AID", apptID)

            dtServicesForPrinting.Clear()
            Dim da As New SqlDataAdapter(cmdSvc)
            da.Fill(dtServicesForPrinting)

            Dim cmdFU As New SqlCommand("
                SELECT FollowUpDate, Reason
                FROM PatientFollowUps
                WHERE AppointmentID = @AID
                ORDER BY FollowUpDate", con)

            cmdFU.Parameters.AddWithValue("@AID", apptID)

            dtFollowUpsForPrinting.Clear()
            Dim da2 As New SqlDataAdapter(cmdFU)
            da2.Fill(dtFollowUpsForPrinting)

        End Using

    End Sub

    ' ================= VOID =================
    Private Sub VoidReceipt(receiptID As Integer)

        If SystemSession.LoggedInRole <> "Admin" Then
            MessageBox.Show("Admins only.")
            Exit Sub
        End If

        Try
            Using con As New SqlConnection(connectionString)
                con.Open()
                Using trans = con.BeginTransaction()

                    Dim check As New SqlCommand("
                        SELECT Status FROM Receipts WHERE ReceiptID=@id",
                        con, trans)

                    check.Parameters.AddWithValue("@id", receiptID)

                    Dim status = If(check.ExecuteScalar(), "").ToString()

                    If status = "Voided" Then
                        MessageBox.Show("Already voided.")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim cmd As New SqlCommand("
                        UPDATE Receipts
                        SET Status='Voided',
                            VoidedAt=GETDATE(),
                            VoidedBy=@user
                        WHERE ReceiptID=@id", con, trans)

                    cmd.Parameters.AddWithValue("@id", receiptID)
                    cmd.Parameters.AddWithValue("@user", SystemSession.LoggedInFullName)
                    cmd.ExecuteNonQuery()

                    trans.Commit()
                End Using
            End Using

            SystemSession.LogAudit("Voided Receipt #" & receiptID,
                                   "Payment History",
                                   SystemSession.LoggedInUserID,
                                   SystemSession.LoggedInFullName,
                                   SystemSession.LoggedInRole)

            LoadPaymentHistory()

        Catch ex As Exception
            MessageBox.Show("Void error: " & ex.Message)
        End Try

    End Sub

    ' ================= RIGHT CLICK VOID =================
    Private Sub dgvHistory_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvHistory.CellMouseClick

        If e.RowIndex < 0 Then Exit Sub

        If e.Button = MouseButtons.Right Then

            Dim row = dgvHistory.Rows(e.RowIndex)
            Dim id = CInt(row.Cells("ReceiptID").Value)

            If row.Cells("Status").Value?.ToString() = "Voided" Then
                MessageBox.Show("Already voided.")
                Exit Sub
            End If

            If MessageBox.Show("Void this receipt?", "Confirm",
                               MessageBoxButtons.YesNo) = DialogResult.Yes Then
                VoidReceipt(id)
            End If

        End If

    End Sub

    ' ================= CLEAR =================
    Private Sub clearform()
        txtSearchPatient.Clear()
        dgvHistory.ClearSelection()
    End Sub

    ' ================= BACK =================
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

End Class