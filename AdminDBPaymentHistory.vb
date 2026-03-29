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

                ' Base Query
                Dim sql As String = "
                    SELECT 
                        R.ReceiptID,
                        R.AppointmentID,
                        P.FullName AS [Patient Name],
                        R.TotalAmount AS [Amount Paid],
                        R.PaymentMethod AS [Method],
                        R.ReferenceNumber AS [Ref No],
                        R.DateIssued AS [Payment Date], 
                        U.FullName AS [Processed By]
                    FROM Receipts R
                    INNER JOIN Patients P ON R.PatientID = P.PatientID
                    INNER JOIN Users U ON R.UserID = U.UserID"

                ' --- MULTI-FIELD SEARCH LOGIC ---
                ' This now checks: Name, Receipt ID, Ref Number, and Payment Method
                If Not String.IsNullOrEmpty(searchName) Then
                    sql &= " WHERE (P.FullName LIKE @search 
                                OR CAST(R.ReceiptID AS VARCHAR) LIKE @search 
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
        SelectedTotalAmount = CDec(row.Cells("Amount Paid").Value).ToString("F2")
        SelectedPaymentMethod = row.Cells("Method").Value.ToString()
        SelectedRefNo = row.Cells("Ref No").Value.ToString() '
        FetchDetailsForReprint(SelectedAppointmentID)

        ReceiptPrinter.PrintReceipt(SelectedPatientName, SelectedDentistName, SelectedTreatmentNotes, SelectedTotalAmount, SelectedPaymentMethod, SelectedRefNo, dtServicesForPrinting)

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