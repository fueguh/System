Imports System.Drawing.Printing
Imports System.Management
Imports System.Web.UI

Public Module ReceiptPrinter

    ' Variables to hold data temporarily for the print job
    Private _PatientName As String
    Private _DentistName As String
    Private _Notes As String
    Private _Total As String
    Private _Method As String
    Private _RefNo As String
    Private _ServicesDt As DataTable
    Private _FollowUpsDt As DataTable

    ' Payment variables
    Private _VatAmount As String
    Private _VatExempt As String
    Private _AmountPaid As String
    Private _Change As String

    ' ================= CLINIC INFORMATION (Single Source of Truth) =================
    Public Const ClinicName As String = "ARG HEALTHY SMILE CLINIC"
    Public Const ClinicAddress As String = "14 St. Francis St., Taguig, 1632 Metro Manila"
    Public Const ClinicContact As String = "Contact: (02) 8123-4567 | +63 917 123 4567"

    ''' <summary>
    ''' Returns the full header text (clinic name + address + contact) for reuse in flash prompt
    ''' </summary>
    Public Function GetReceiptHeader() As String
        Return ClinicName & vbCrLf &
               ClinicAddress & vbCrLf &
               ClinicContact & vbCrLf & vbCrLf &
               "OFFICIAL RECEIPT"
    End Function

    ''' <summary>
    ''' Call this from any form to start a print job.
    ''' </summary>
    Public Sub PrintReceipt(patient As String, dentist As String, notes As String, total As String, paid As String, method As String, ref As String, services As DataTable, followups As DataTable)
        ' Assign values
        _PatientName = patient
        _DentistName = dentist
        _Notes = notes
        _Total = total
        _Method = method
        _RefNo = ref
        _ServicesDt = services
        _FollowUpsDt = followups
        _AmountPaid = paid

        ' Calculations
        Dim totalVal As Decimal = 0
        Dim paidVal As Decimal = 0
        Decimal.TryParse(total, totalVal)
        Decimal.TryParse(paid, paidVal)

        _Change = (paidVal - totalVal).ToString("F2")

        If totalVal > 0 Then
            Dim vatableSales As Decimal = totalVal / 1.12D
            Dim vatAmount As Decimal = totalVal - vatableSales
            _VatExempt = vatableSales.ToString("F2")
            _VatAmount = vatAmount.ToString("F2")
        Else
            _VatExempt = "0.00"
            _VatAmount = "0.00"
        End If

        ' Print setup
        Dim pd As New PrintDocument()
        pd.DefaultPageSettings.PaperSize = New PaperSize("Custom", 300, 1000)
        AddHandler pd.PrintPage, AddressOf SharedPrintPageHandler

        Dim dlg As New PrintDialog()
        dlg.Document = pd

        If dlg.ShowDialog() = DialogResult.OK Then
            Dim pkName As String = pd.PrinterSettings.PrinterName
            If Not IsPrinterOnline(pkName) Then
                Dim ans As DialogResult = MessageBox.Show($"Printer '{pkName}' is offline. Print anyway?", "Offline", MessageBoxButtons.YesNo)
                If ans = DialogResult.No Then Exit Sub
            End If

            Try
                pd.Print()
            Catch ex As Exception
                MessageBox.Show("Print Error: " & ex.Message)
            End Try
        End If
    End Sub

    ' The Actual Layout Logic
    Private Sub SharedPrintPageHandler(sender As Object, e As PrintPageEventArgs)
        Dim g As Graphics = e.Graphics
        Dim currentY As Integer = 80
        Dim fontBody As New Font("Consolas", 8)
        Dim leftMargin As Integer = 5
        Dim rightMargin As Integer = 185

        ' Header (uses constants from this module)
        g.DrawString(ClinicName, New Font("Arial", 8, FontStyle.Bold), Brushes.Black, leftMargin, currentY)
        currentY += 18
        g.DrawString(ClinicAddress, New Font("Arial", 5), Brushes.Black, leftMargin, currentY)
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

        ' VAT SECTION
        g.DrawString("--------------------------------", fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 15

        g.DrawString("VATable Sales:", fontBody, Brushes.Black, leftMargin, currentY)
        g.DrawString(_VatExempt, fontBody, Brushes.Black, rightMargin - g.MeasureString(_VatExempt, fontBody).Width, currentY)
        currentY += 15

        g.DrawString("VAT (12%):", fontBody, Brushes.Black, leftMargin, currentY)
        g.DrawString(_VatAmount, fontBody, Brushes.Black, rightMargin - g.MeasureString(_VatAmount, fontBody).Width, currentY)
        currentY += 15

        g.DrawString("TOTAL AMOUNT: P" & _Total, New Font("Consolas", 9, FontStyle.Bold), Brushes.Black, leftMargin, currentY)
        currentY += 25

        ' Payment
        g.DrawString("Amount Paid:", fontBody, Brushes.Black, leftMargin, currentY)
        g.DrawString(_AmountPaid, fontBody, Brushes.Black, rightMargin - g.MeasureString(_AmountPaid, fontBody).Width, currentY)
        currentY += 15

        g.DrawString("CHANGE:", fontBody, Brushes.Black, leftMargin, currentY)
        g.DrawString(_Change, fontBody, Brushes.Black, rightMargin - g.MeasureString(_Change, fontBody).Width, currentY)
        currentY += 25

        ' Follow-Ups
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

        ' Notes
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
        currentY += 40
        g.DrawString(".", New Font("Arial", 1), Brushes.Black, leftMargin, currentY)
    End Sub

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
End Module