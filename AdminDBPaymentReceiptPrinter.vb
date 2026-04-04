Imports System.Drawing.Printing
Imports System.Management

Public Module AdminDBPaymentReceiptPrinter

    ' Variables to hold data temporarily for the print job
    Private _PatientName As String
    Private _DentistName As String
    Private _Notes As String
    Private _Subtotal As String          ' Gross Subtotal (VAT Inclusive) = Total
    Private _VatExempt As String         ' VATable Sales (Net of VAT)
    Private _VatAmount As String
    Private _Total As String             ' Should be equal to Subtotal
    Private _AmountPaid As String
    Private _Change As String
    Private _Method As String
    Private _RefNo As String
    Private _ServicesDt As DataTable
    Private _FollowUpsDt As DataTable

    ' ================= CLINIC INFORMATION =================
    Public Const ClinicName As String = "ARG HEALTHY SMILE CLINIC"
    Public Const ClinicAddress As String = "14 St. Francis St., Taguig, 1632 Metro Manila"
    Public Const ClinicContact As String = "Contact: +63 917 123 4567"

    Public Function GetReceiptHeader() As String
        Return ClinicName & vbCrLf &
               ClinicAddress & vbCrLf &
               ClinicContact & vbCrLf & vbCrLf &
               "OFFICIAL RECEIPT"
    End Function

    ''' <summary>
    ''' Print Receipt - Receives pre-computed values
    ''' </summary>
    Public Sub PrintReceipt(
        patient As String,
        dentist As String,
        notes As String,
        subtotal As String,      ' Gross Subtotal (VAT Inclusive)
        vatExempt As String,     ' VATable Sales
        vatAmount As String,
        total As String,         ' Should be same as subtotal
        paid As String,
        change As String,
        method As String,
        ref As String,
        services As DataTable,
        followups As DataTable)

        _PatientName = patient
        _DentistName = dentist
        _Notes = notes
        _Subtotal = subtotal
        _VatExempt = vatExempt
        _VatAmount = vatAmount
        _Total = total
        _AmountPaid = paid
        _Change = change
        _Method = method
        _RefNo = ref
        _ServicesDt = services
        _FollowUpsDt = followups

        Dim pd As New PrintDocument()
        pd.DefaultPageSettings.PaperSize = New PaperSize("Custom", 300, 1000)
        AddHandler pd.PrintPage, AddressOf SharedPrintPageHandler

        Dim dlg As New PrintDialog()
        dlg.Document = pd

        If dlg.ShowDialog() = DialogResult.OK Then
            Dim pkName As String = pd.PrinterSettings.PrinterName
            If Not IsPrinterOnline(pkName) Then
                If MessageBox.Show($"Printer '{pkName}' is offline. Print anyway?", "Offline", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub
            End If
            Try
                pd.Print()
            Catch ex As Exception
                MessageBox.Show("Print Error: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub SharedPrintPageHandler(sender As Object, e As PrintPageEventArgs)
        Dim g As Graphics = e.Graphics
        Dim currentY As Integer = 80
        Dim fontBody As New Font("Consolas", 8)
        Dim leftMargin As Integer = 5
        Dim rightMargin As Integer = 185

        ' Header
        g.DrawString(ClinicName, New Font("Arial", 8, FontStyle.Bold), Brushes.Black, leftMargin, currentY)
        currentY += 18
        g.DrawString(ClinicAddress, New Font("Arial", 4), Brushes.Black, leftMargin, currentY)
        currentY += 15
        g.DrawString(ClinicContact, New Font("Arial", 5), Brushes.Black, leftMargin, currentY)
        currentY += 20

        g.DrawString("OFFICIAL RECEIPT", New Font("Arial", 10, FontStyle.Bold), Brushes.Black, leftMargin, currentY)
        currentY += 20

        g.DrawString("Date: " & DateTime.Now.ToString("G"), fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 15
        g.DrawString("Patient: " & _PatientName, fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 15
        g.DrawString("Doctor:  " & _DentistName, fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 15

        If Not String.IsNullOrEmpty(_RefNo) Then
            g.DrawString("Ref No:  " & _RefNo, fontBody, Brushes.Black, leftMargin, currentY)
            currentY += 15
        End If

        g.DrawString("--------------------------------", fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 15

        ' Services
        For Each row As DataRow In _ServicesDt.Rows
            Dim sName As String = row("ServiceName").ToString()
            Dim sPrice As String = "P" & CDec(row("Price")).ToString("F2")
            g.DrawString(sName, fontBody, Brushes.Black, leftMargin, currentY)
            g.DrawString(sPrice, fontBody, Brushes.Black, rightMargin - g.MeasureString(sPrice, fontBody).Width, currentY)
            currentY += 15
        Next

        g.DrawString("--------------------------------", fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 15

        ' Corrected VAT Section
        g.DrawString("SUBTOTAL:", fontBody, Brushes.Black, leftMargin, currentY)
        g.DrawString("P" & _Subtotal, fontBody, Brushes.Black, rightMargin - g.MeasureString("P" & _Subtotal, fontBody).Width, currentY)
        currentY += 15

        g.DrawString("VATable Sales:", fontBody, Brushes.Black, leftMargin, currentY)
        g.DrawString(_VatExempt, fontBody, Brushes.Black, rightMargin - g.MeasureString(_VatExempt, fontBody).Width, currentY)
        currentY += 15

        g.DrawString("VAT (12%):", fontBody, Brushes.Black, leftMargin, currentY)
        g.DrawString(_VatAmount, fontBody, Brushes.Black, rightMargin - g.MeasureString(_VatAmount, fontBody).Width, currentY)
        currentY += 15

        g.DrawString("TOTAL AMOUNT: P" & _Total, New Font("Consolas", 9, FontStyle.Bold), Brushes.Black, leftMargin, currentY)
        currentY += 25

        ' Payment Section
        g.DrawString("Amount Paid:", fontBody, Brushes.Black, leftMargin, currentY)
        g.DrawString(_AmountPaid, fontBody, Brushes.Black, rightMargin - g.MeasureString(_AmountPaid, fontBody).Width, currentY)
        currentY += 15

        g.DrawString("CHANGE:", fontBody, Brushes.Black, leftMargin, currentY)
        g.DrawString(_Change, fontBody, Brushes.Black, rightMargin - g.MeasureString(_Change, fontBody).Width, currentY)
        currentY += 25

        ' Follow-Ups & Notes (unchanged)
        If _FollowUpsDt IsNot Nothing AndAlso _FollowUpsDt.Rows.Count > 0 Then
            currentY += 10
            g.DrawString("FOLLOW-UP SCHEDULE:", New Font("Consolas", 8, FontStyle.Bold), Brushes.Black, leftMargin, currentY)
            currentY += 15
            For Each row As DataRow In _FollowUpsDt.Rows
                Dim fDate As String = Convert.ToDateTime(row("FollowUpDate")).ToString("MM/dd/yyyy")
                Dim fReason As String = row("Reason").ToString()
                g.DrawString(fDate & " - " & fReason, fontBody, Brushes.Black, leftMargin, currentY)
                currentY += 15
            Next
        End If

        currentY += 10
        g.DrawString("DENTIST NOTES:", New Font("Consolas", 8, FontStyle.Bold), Brushes.Black, leftMargin, currentY)
        currentY += 15
        Dim rectNotes As New RectangleF(leftMargin, currentY, rightMargin - leftMargin, 150)
        g.DrawString(_Notes, fontBody, Brushes.Black, rectNotes)
        Dim textSize = g.MeasureString(_Notes, fontBody, New SizeF(rightMargin - leftMargin, 150))
        currentY += CInt(textSize.Height) + 10

        ' Footer
        g.DrawString("--------------------------------", fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 15
        g.DrawString("TOTAL AMOUNT: P" & _Total, New Font("Consolas", 9, FontStyle.Bold), Brushes.Black, leftMargin, currentY)
        currentY += 25
        g.DrawString("Method: " & _Method, fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 30
        g.DrawString("Thank you for visiting!", fontBody, Brushes.Black, leftMargin, currentY)
    End Sub

    ' IsPrinterOnline and GetReceiptFlashPreview remain the same as your last version
    ' (I kept them unchanged for brevity - they are already correct)

    Public Function IsPrinterOnline(printerName As String) As Boolean
        Try
            Dim query As String = "SELECT * FROM Win32_Printer WHERE Name = '" & printerName.Replace("\", "\\") & "'"
            Using searcher As New ManagementObjectSearcher(query)
                For Each printer As ManagementObject In searcher.Get()
                    Return Not CBool(printer("WorkOffline"))
                Next
            End Using
        Catch
            Return False
        End Try
        Return False
    End Function

    Public Function GetReceiptFlashPreview(
        patientName As String,
        dentistName As String,
        notes As String,
        subtotal As String,
        vatExempt As String,
        vatAmount As String,
        total As String,
        amountPaid As String,
        change As String,
        method As String,
        refNo As String,
        servicesDt As DataTable,
        followUpsDt As DataTable) As String

        Dim flashMsg As String = GetReceiptHeader() & vbCrLf & vbCrLf

        flashMsg &= "Date: " & DateTime.Now.ToString("G") & vbCrLf
        flashMsg &= "Patient: " & patientName & vbCrLf
        flashMsg &= "Doctor:  " & dentistName & vbCrLf

        If Not String.IsNullOrEmpty(refNo) Then flashMsg &= "Ref No:  " & refNo & vbCrLf

        flashMsg &= "--------------------------------" & vbCrLf & vbCrLf

        If servicesDt IsNot Nothing Then
            For Each row As DataRow In servicesDt.Rows
                Dim sName As String = row("ServiceName").ToString()
                Dim sPrice As String = "P" & CDec(row("Price")).ToString("F2")
                flashMsg &= sName & vbTab & sPrice & vbCrLf
            Next
        End If

        flashMsg &= vbCrLf & "--------------------------------" & vbCrLf

        flashMsg &= "SUBTOTAL:           P" & subtotal & vbCrLf
        flashMsg &= "VATable Sales:      " & vatExempt & vbCrLf
        flashMsg &= "VAT (12%):          " & vatAmount & vbCrLf & vbCrLf
        flashMsg &= "TOTAL AMOUNT: P" & total & vbCrLf & vbCrLf

        flashMsg &= "Amount Paid:        " & amountPaid & vbCrLf
        flashMsg &= "CHANGE:             " & change & vbCrLf & vbCrLf

        If followUpsDt IsNot Nothing AndAlso followUpsDt.Rows.Count > 0 Then
            flashMsg &= "FOLLOW-UP SCHEDULE:" & vbCrLf
            For Each row As DataRow In followUpsDt.Rows
                Dim fDate As String = Convert.ToDateTime(row("FollowUpDate")).ToString("MM/dd/yyyy")
                Dim fReason As String = row("Reason").ToString()
                flashMsg &= fDate & " - " & fReason & vbCrLf
            Next
            flashMsg &= vbCrLf
        End If

        flashMsg &= "DENTIST NOTES:" & vbCrLf & notes & vbCrLf & vbCrLf
        flashMsg &= "--------------------------------" & vbCrLf
        flashMsg &= "TOTAL AMOUNT: P" & total & vbCrLf
        flashMsg &= "Method: " & method & vbCrLf & vbCrLf
        flashMsg &= "Thank you for visiting!"

        Return flashMsg
    End Function

End Module