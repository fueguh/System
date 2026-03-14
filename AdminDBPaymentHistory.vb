Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.Management
Public Class AdminDBPaymentHistory
    Private connectionString As String = My.Settings.DentalDBConnection2
    Private SelectedAppointmentID As Integer = 0
    Private SelectedPatientName As String = ""
    Private SelectedTreatmentNotes As String = ""
    Private SelectedTotalAmount As String = "0.00"
    Private SelectedPaymentMethod As String = ""
    Private dtServicesForPrinting As New DataTable()
    Private SelectedDentistName As String = ""
    Private Sub AdminDBPaymentHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPaymentHistory()

        ' UI Cleanup
        dgvHistory.ReadOnly = True
        dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvHistory.AllowUserToAddRows = False
    End Sub

    Private Sub LoadPaymentHistory(Optional searchName As String = "")
        Try
            Using con As New SqlConnection(connectionString)
                con.Open()
                ' Changed R.DateCreated to R.DateIssued to match your SQL schema
                Dim sql As String = "
    SELECT 
        R.ReceiptID,
        R.AppointmentID,
        P.FullName AS [Patient Name],
        R.TotalAmount AS [Amount Paid],
        R.PaymentMethod AS [Method],
        R.DateIssued AS [Payment Date], 
        U.FullName AS [Processed By]
    FROM Receipts R
    INNER JOIN Patients P ON R.PatientID = P.PatientID
    INNER JOIN Users U ON R.UserID = U.UserID"

                ' Improved Search: Checks both Name and Receipt ID
                If Not String.IsNullOrEmpty(searchName) Then
                    sql &= " WHERE (P.FullName LIKE @search OR CAST(R.ReceiptID AS VARCHAR) LIKE @search)"
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

        FetchDetailsForReprint(SelectedAppointmentID)

        Dim pd As New PrintDocument()
        pd.DefaultPageSettings.PaperSize = New PaperSize("Custom", 300, 1000)
        AddHandler pd.PrintPage, AddressOf PrintPageHandler

        Dim dlg As New PrintDialog()
        dlg.Document = pd

        ' 1. User selects their printer and clicks "Print" in the dialog
        If dlg.ShowDialog() = DialogResult.OK Then

            ' 2. NOW we check if the SPECIFIC printer they chose is online
            Dim pkName As String = pd.PrinterSettings.PrinterName

            If Not IsPrinterOnline(pkName) Then
                Dim ans As DialogResult = MessageBox.Show($"The printer '{pkName}' is offline or disconnected. Try to print anyway?",
                                                 "Printer Offline", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If ans = DialogResult.No Then Exit Sub
            End If

            ' 3. Actually send the data to the printer
            Try
                pd.Print()
            Catch ex As Exception
                MessageBox.Show("A hardware error occurred: " & ex.Message, "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    ' Helper Function to check if printer is actually on
    Private Function IsPrinterOnline(printerName As String) As Boolean
        Dim strQuery As String = "SELECT * FROM Win32_Printer WHERE Name = '" & printerName.Replace("\", "\\") & "'"
        Try
            Using searcher As New ManagementObjectSearcher(strQuery)
                For Each printer As ManagementObject In searcher.Get()
                    ' WorkOffline property: True if printer is not communicating
                    Return Not CBool(printer("WorkOffline"))
                Next
            End Using
        Catch
            Return False ' If check fails, assume offline for safety
        End Try
        Return False
    End Function

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

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
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
        currentY += 15 ' Incremented to prevent overlap
        g.DrawString("Doctor:  " & SelectedDentistName, fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 15
        g.DrawString("--------------------------------", fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 15

        ' 2. Services List (Updated to loop through the DataTable variable)
        For Each row As DataRow In dtServicesForPrinting.Rows
            Dim sName As String = row("ServiceName").ToString()
            Dim sPrice As String = "P" & CDec(row("Price")).ToString("F2")

            g.DrawString(sName, fontBody, Brushes.Black, leftMargin, currentY)
            g.DrawString(sPrice, fontBody, Brushes.Black, rightMargin - g.MeasureString(sPrice, fontBody).Width, currentY)
            currentY += 15
        Next

        ' 3. Dentist Notes
        currentY += 10
        g.DrawString("DENTIST NOTES:", New Font("Consolas", 8, FontStyle.Bold), Brushes.Black, leftMargin, currentY)
        currentY += 15
        Dim rectNotes As New RectangleF(leftMargin, currentY, rightMargin - leftMargin, 150)
        g.DrawString(SelectedTreatmentNotes, fontBody, Brushes.Black, rectNotes)
        Dim textSize = g.MeasureString(SelectedTreatmentNotes, fontBody, New SizeF(rightMargin - leftMargin, 150))
        currentY += CInt(textSize.Height) + 10

        ' 4. Total (Using variable instead of TextBox)
        g.DrawString("--------------------------------", fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 15
        g.DrawString("TOTAL AMOUNT:", New Font("Consolas", 9, FontStyle.Bold), Brushes.Black, leftMargin, currentY)
        g.DrawString("P" & SelectedTotalAmount, New Font("Consolas", 9, FontStyle.Bold), Brushes.Black, rightMargin - g.MeasureString("P" & SelectedTotalAmount, fontBody).Width, currentY)

        currentY += 25
        g.DrawString("Method: " & SelectedPaymentMethod, fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 30
        g.DrawString("Thank you for visiting!", fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 40
        g.DrawString(".", New Font("Arial", 1), Brushes.Black, leftMargin, currentY)
    End Sub

End Class