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
                    R.TotalAmount AS [Total Bill],      -- Amount actually owed
                    R.AmountPaid AS [Cash Tendered],   -- Amount the patient handed over
                    (R.AmountPaid - R.TotalAmount) AS [Change Given], -- Math done in SQL
                    R.PaymentMethod AS [Method],
                    R.ReferenceNumber AS [Ref No],
                    R.DateIssued AS [Payment Date],    -- Removed comma error here
                    
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

    ' Action: Reprint Receipt
    Private Sub btnReprint_Click(sender As Object, e As EventArgs) Handles btnReprint.Click
        If dgvHistory.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a record from the history list.")
            Exit Sub
        End If

        Dim row = dgvHistory.SelectedRows(0)
        SelectedAppointmentID = CInt(row.Cells("AppointmentID").Value)
        SelectedPatientName = row.Cells("Patient Name").Value.ToString()

        ' Pull the two different money values
        Dim billTotal As String = CDec(row.Cells("Total Bill").Value).ToString("F2")
        Dim cashGiven As String = CDec(row.Cells("Cash Tendered").Value).ToString("F2")

        SelectedPaymentMethod = row.Cells("Method").Value.ToString()
        SelectedRefNo = row.Cells("Ref No").Value.ToString()

        FetchDetailsForReprint(SelectedAppointmentID)

        ' Send to printer - Slot 4 is the Bill, Slot 5 is the Cash handed over
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
    End Sub

    ' Helper Function to check if printer is actually on

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