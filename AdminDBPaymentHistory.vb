Imports System.Data.SqlClient

Public Class AdminDBPaymentHistory
    Private connectionString As String = My.Settings.DentalDBConnection2
    Private SelectedAppointmentID As Integer = 0
    Private SelectedPatientName As String = ""
    Private SelectedTreatmentNotes As String = ""
    Private SelectedTotalAmount As String = "0.00"
    Private SelectedPaymentMethod As String = ""
    Private dtServicesForPrinting As New DataTable()
    Private SelectedDentistName As String = ""
    Private SelectedRefNo As String = ""
    Private dtFollowUpsForPrinting As New DataTable()

    Private Sub AdminDBPaymentHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPaymentHistory()

        ' UI Cleanup
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
                    R.AmountPaid AS [Cash Tendered],
                    (R.AmountPaid - R.TotalAmount) AS [Change Given],
                    R.PaymentMethod AS [Method],
                    R.ReferenceNumber AS [Ref No],
                    R.DateIssued AS [Payment Date],
                    
                    ISNULL((
                        SELECT STRING_AGG(
                            CONVERT(VARCHAR, F.FollowUpDate, 101) + ' (' + F.Reason + ')', 
                            ', '
                        )
                        FROM PatientFollowUps F
                        WHERE F.AppointmentID = R.AppointmentID
                    ), 'None') AS [Follow-Ups]

                FROM Receipts R
                INNER JOIN Patients P ON R.PatientID = P.PatientID
                INNER JOIN Appointments A ON R.AppointmentID = A.AppointmentID
                INNER JOIN Users U ON A.UserID = U.UserID
                INNER JOIN Users U2 ON R.UserID = U2.UserID
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

                    ' Format Currency Columns in Grid
                    If dgvHistory.Columns.Contains("Total Bill") Then dgvHistory.Columns("Total Bill").DefaultCellStyle.Format = "N2"
                    If dgvHistory.Columns.Contains("Cash Tendered") Then dgvHistory.Columns("Cash Tendered").DefaultCellStyle.Format = "N2"
                    If dgvHistory.Columns.Contains("Change Given") Then dgvHistory.Columns("Change Given").DefaultCellStyle.Format = "N2"

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

    ' Action: Reprint Receipt with Flash Preview
    Private Sub btnReprint_Click(sender As Object, e As EventArgs) Handles btnReprint.Click
        If dgvHistory.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a record from the history list.")
            Exit Sub
        End If

        Dim row = dgvHistory.SelectedRows(0)
        SelectedAppointmentID = CInt(row.Cells("AppointmentID").Value)
        SelectedPatientName = row.Cells("Patient Name").Value.ToString()

        ' Pull money values
        Dim billTotal As String = CDec(row.Cells("Total Bill").Value).ToString("F2")
        Dim cashGiven As String = CDec(row.Cells("Cash Tendered").Value).ToString("F2")

        SelectedPaymentMethod = row.Cells("Method").Value.ToString()
        SelectedRefNo = If(row.Cells("Ref No").Value IsNot DBNull.Value, row.Cells("Ref No").Value.ToString(), "")

        FetchDetailsForReprint(SelectedAppointmentID)

        ' === FLASH PROMPT - Reuses ReceiptPrinter as single source of truth ===
        Dim flashMsg As String =
            ReceiptPrinter.GetReceiptHeader() & vbCrLf & vbCrLf &
            "Date: " & DateTime.Now.ToString("G") & vbCrLf &
            "Patient: " & SelectedPatientName & vbCrLf &
            "Doctor:  " & SelectedDentistName & vbCrLf

        If Not String.IsNullOrEmpty(SelectedRefNo) Then
            flashMsg &= "Ref No:  " & SelectedRefNo & vbCrLf
        End If

        flashMsg &= "--------------------------------" & vbCrLf & vbCrLf

        ' Services
        If dtServicesForPrinting.Rows.Count > 0 Then
            For Each rowSvc As DataRow In dtServicesForPrinting.Rows
                Dim sName As String = rowSvc("ServiceName").ToString()
                Dim sPrice As String = "P" & CDec(rowSvc("Price")).ToString("F2")
                flashMsg &= sName & vbTab & sPrice & vbCrLf
            Next
        End If

        flashMsg &= vbCrLf & "--------------------------------" & vbCrLf

        ' VAT Section (recalculated for consistency)
        Dim totalAmount As Decimal = 0
        Decimal.TryParse(billTotal, totalAmount)
        Dim vatable As Decimal = If(totalAmount > 0, totalAmount / 1.12D, 0)
        Dim vat As Decimal = totalAmount - vatable

        flashMsg &= "VATable Sales:      " & vatable.ToString("F2") & vbCrLf
        flashMsg &= "VAT (12%):          " & vat.ToString("F2") & vbCrLf & vbCrLf
        flashMsg &= "TOTAL AMOUNT: P" & totalAmount.ToString("F2") & vbCrLf & vbCrLf

        ' Payment Details
        flashMsg &= "Amount Paid:        " & cashGiven & vbCrLf
        flashMsg &= "CHANGE:             " & CDec(row.Cells("Change Given").Value).ToString("F2") & vbCrLf & vbCrLf

        ' Follow-Ups
        If dtFollowUpsForPrinting.Rows.Count > 0 Then
            flashMsg &= "FOLLOW-UP SCHEDULE:" & vbCrLf
            For Each rowFU As DataRow In dtFollowUpsForPrinting.Rows
                Dim fDate As String = Convert.ToDateTime(rowFU("FollowUpDate")).ToString("MM/dd/yyyy")
                Dim fReason As String = rowFU("Reason").ToString()
                flashMsg &= fDate & " - " & fReason & vbCrLf
            Next
            flashMsg &= vbCrLf
        End If

        ' Notes
        flashMsg &= "DENTIST NOTES:" & vbCrLf & SelectedTreatmentNotes & vbCrLf & vbCrLf

        flashMsg &= "--------------------------------" & vbCrLf
        flashMsg &= "TOTAL AMOUNT: P" & totalAmount.ToString("F2") & vbCrLf
        flashMsg &= "Method: " & SelectedPaymentMethod & vbCrLf & vbCrLf
        flashMsg &= "Thank you for visiting!"

        MessageBox.Show(flashMsg, "RECEIPT PREVIEW - This is exactly how it will be printed", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' Ask to print
        Dim askPrint As DialogResult = MessageBox.Show("Would you like to print the receipt now?",
                                      "Print Receipt", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If askPrint = DialogResult.Yes Then
            ReceiptPrinter.PrintReceipt(
                SelectedPatientName,
                SelectedDentistName,
                SelectedTreatmentNotes,
                billTotal,
                cashGiven,
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

            ' 1. Get Dentist Name and Treatment Notes
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

            ' 2. Get Services
            Dim cmdSvc As New SqlCommand("SELECT S.ServiceName, S.Price FROM AppointmentServices ASV " &
                                     "INNER JOIN Services S ON ASV.ServiceID = S.ServiceID WHERE ASV.AppointmentID = @AID", con)
            cmdSvc.Parameters.AddWithValue("@AID", apptID)

            Dim da As New SqlDataAdapter(cmdSvc)
            dtServicesForPrinting.Clear()
            da.Fill(dtServicesForPrinting)

            ' 3. Get Follow-Ups
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

    Private Sub clearform()
        txtSearchPatient.Clear()
        dgvHistory.ClearSelection()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

End Class