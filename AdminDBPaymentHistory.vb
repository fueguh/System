Imports System.Data.SqlClient

Public Class AdminDBPaymentHistory

    Private connectionString As String = My.Settings.DentalDBConnection2

    ' ================= LOAD =================
    Private Sub AdminDBPaymentHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPaymentHistory()
        dgvHistory.ReadOnly = True
        dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvHistory.AllowUserToAddRows = False
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

    ' ================= REPRINT =================
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

        ' Extract data directly from selected row
        Dim appointmentID As Integer = CInt(row.Cells("AppointmentID").Value)
        Dim receiptID As Integer = CInt(row.Cells("ReceiptID").Value)

        Dim patientName As String = row.Cells("Patient Name").Value.ToString()
        Dim dentistName As String = row.Cells("Dentist").Value.ToString()
        Dim paymentMethod As String = row.Cells("Method").Value.ToString()
        Dim refNo As String = If(row.Cells("Ref No").Value IsNot DBNull.Value, row.Cells("Ref No").Value.ToString(), "")

        Dim totalAmount As Decimal = Convert.ToDecimal(row.Cells("Total Bill").Value)
        Dim amountPaid As Decimal = Convert.ToDecimal(row.Cells("Cash Tendered").Value)
        Dim changeAmount As Decimal = Convert.ToDecimal(row.Cells("Change Given").Value)

        ' Fetch detailed data
        Dim dtServices As New DataTable()
        Dim dtItems As New DataTable()
        Dim dtFollowUps As New DataTable()
        Dim treatmentNotes As String = ""

        FetchDetailsForReprint(appointmentID, receiptID, dtServices, dtItems, dtFollowUps, treatmentNotes)

        ' ================= PREVIEW (using printer module) =================
        Dim flashMsg As String = AdminDBPaymentReceiptPrinter.GetReceiptFlashPreview(
            patientName, dentistName, treatmentNotes,
            totalAmount.ToString("F2"), amountPaid.ToString("F2"), changeAmount.ToString("F2"),
            paymentMethod, refNo, dtServices, dtItems, dtFollowUps)

        MessageBox.Show(flashMsg, "Receipt Preview", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' ================= PRINT (using printer module) =================
        If MessageBox.Show("Do you want to print this receipt?", "Confirm Print",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            AdminDBPaymentReceiptPrinter.PrintReceipt(
                patientName, dentistName, treatmentNotes,
                totalAmount.ToString("F2"), amountPaid.ToString("F2"), changeAmount.ToString("F2"),
                paymentMethod, refNo, dtServices, dtFollowUps, dtItems)
        End If
    End Sub

    ' ================= FETCH DETAILS FOR REPRINT =================
    Private Sub FetchDetailsForReprint(apptID As Integer, receiptID As Integer,
                                      ByRef dtServices As DataTable,
                                      ByRef dtItems As DataTable,
                                      ByRef dtFollowUps As DataTable,
                                      ByRef treatmentNotes As String)

        dtServices.Clear()
        dtItems.Clear()
        dtFollowUps.Clear()

        Using con As New SqlConnection(connectionString)
            con.Open()

            ' 1. Dentist Name + Treatment Notes
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
                        treatmentNotes = r("Notes").ToString()
                    End If
                End Using
            End Using

            ' 2. Services
            Using cmdSvc As New SqlCommand("
                SELECT ServiceName, Price 
                FROM AppointmentServices ASV
                INNER JOIN Services S ON ASV.ServiceID = S.ServiceID
                WHERE ASV.AppointmentID = @AID", con)
                cmdSvc.Parameters.AddWithValue("@AID", apptID)
                Dim da As New SqlDataAdapter(cmdSvc)
                da.Fill(dtServices)
            End Using

            ' 3. Items
            Using cmdItems As New SqlCommand("
                SELECT ItemName, Quantity, UnitPrice AS Price
                FROM ReceiptItems 
                WHERE ReceiptID = @ReceiptID 
                  AND ItemType IN ('Item', 'Inventory')
                ORDER BY ReceiptItemID", con)
                cmdItems.Parameters.AddWithValue("@ReceiptID", receiptID)
                Dim da As New SqlDataAdapter(cmdItems)
                da.Fill(dtItems)
            End Using

            ' 4. Follow-ups
            Using cmdFU As New SqlCommand("
                SELECT FollowUpDate, Reason
                FROM PatientFollowUps
                WHERE AppointmentID = @AID
                ORDER BY FollowUpDate", con)
                cmdFU.Parameters.AddWithValue("@AID", apptID)
                Dim da As New SqlDataAdapter(cmdFU)
                da.Fill(dtFollowUps)
            End Using
        End Using
    End Sub

    ' ================= VOID =================
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

    ' ================= BACK =================
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

End Class