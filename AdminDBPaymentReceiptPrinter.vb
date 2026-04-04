Imports System.Drawing.Printing
Imports System.Management

Public Module AdminDBPaymentReceiptPrinter

    ' Variables to hold data temporarily for the print job
    Private _PatientName As String
    Private _DentistName As String
    Private _Notes As String
    Private _Subtotal As String
    Private _VatExempt As String
    Private _VatAmount As String
    Private _Total As String
    Private _AmountPaid As String
    Private _Change As String
    Private _Method As String
    Private _RefNo As String
    Private _ServicesDt As DataTable
    Private _FollowUpsDt As DataTable
    Private _ItemsDt As DataTable

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
    ''' Print Receipt - Now includes Items from ReceiptItems table
    ''' </summary>
    Public Sub PrintReceipt(
    patient As String,
    dentist As String,
    notes As String,
    total As String,
    paid As String,
    change As String,
    method As String,
    ref As String,
    services As DataTable,
    followups As DataTable,
    items As DataTable)

        ' Assign values
        _PatientName = patient
        _DentistName = dentist
        _Notes = notes
        _Total = total
        _AmountPaid = paid
        _Change = change
        _Method = method
        _RefNo = ref
        _ServicesDt = services
        _FollowUpsDt = followups
        _ItemsDt = items

        ' Print setup - Better for 80mm thermal printer
        Dim pd As New PrintDocument()
        pd.DefaultPageSettings.PaperSize = New PaperSize("58mm", 228, 1000) ' Continuous roll
        pd.DefaultPageSettings.Margins = New Margins(5, 5, 5, 5)

        AddHandler pd.PrintPage, AddressOf SharedPrintPageHandler

        Dim dlg As New PrintDialog()
        dlg.Document = pd

        If dlg.ShowDialog() = DialogResult.OK Then
            Dim pkName As String = pd.PrinterSettings.PrinterName
            If Not IsPrinterOnline(pkName) Then
                If MessageBox.Show($"Printer '{pkName}' is offline. Print anyway?", "Offline",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No Then
                    Exit Sub
                End If
            End If

            Try
                pd.Print()
            Catch ex As Exception
                MessageBox.Show("Print Error: " & ex.Message, "Print Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub SharedPrintPageHandler(sender As Object, e As PrintPageEventArgs)
        Dim g As Graphics = e.Graphics
        Dim y As Single = 30.0F
        Dim fontBold As New Font("Consolas", 8, FontStyle.Bold)
        Dim fontBody As New Font("Consolas", 7)
        Dim left As Single = 5
        Dim right As Single = 185

        Dim rightAlign As New StringFormat() With {.Alignment = StringAlignment.Far}
        Dim center As New StringFormat() With {.Alignment = StringAlignment.Center}

        ' ================= HEADER =================
        g.DrawString(ClinicName, New Font("Consolas", 10, FontStyle.Bold), Brushes.Black,
                     New RectangleF(left, y, right - left, 30), center)
        y += 40

        g.DrawString(ClinicAddress, New Font("Consolas", 7), Brushes.Black,
                     New RectangleF(left, y, right - left, 25), center)
        y += 30

        g.DrawString(ClinicContact, New Font("Consolas", 7), Brushes.Black,
                     New RectangleF(left, y, right - left, 20), center)
        y += 30

        g.DrawString("OFFICIAL RECEIPT", New Font("Consolas", 11, FontStyle.Bold), Brushes.Black,
                     New RectangleF(left, y, right - left, 30), center)
        y += 32

        ' Info
        g.DrawString("Date: " & DateTime.Now.ToString("G"), fontBody, Brushes.Black, left, y)
        y += 16
        g.DrawString("Patient: " & _PatientName, fontBody, Brushes.Black, left, y)
        y += 16
        g.DrawString("Doctor: " & _DentistName, fontBody, Brushes.Black, left, y)
        y += 18

        If Not String.IsNullOrEmpty(_RefNo) Then
            g.DrawString("Ref No: " & _RefNo, fontBody, Brushes.Black, left, y)
            y += 16
        End If

        g.DrawString("".PadRight(45, "-"), fontBody, Brushes.Black, left, y)
        y += 20

        ' Services
        For Each row As DataRow In _ServicesDt.Rows
            Dim name As String = row("ServiceName").ToString().Trim()
            Dim price As String = "P" & CDec(row("Price")).ToString("F2")

            ' Services Truncation
            If g.MeasureString(name, fontBody).Width > (right - left - 60) Then
                ' Only substring if the name is actually longer than 25 chars
                If name.Length > 25 Then
                    name = name.Substring(0, 22) & ".."
                End If
            End If

            g.DrawString(name, fontBody, Brushes.Black, left, y)
            g.DrawString(price, fontBody, Brushes.Black, right, y, rightAlign)
            y += 16
        Next

        g.DrawString("".PadRight(32, "-"), fontBody, Brushes.Black, left, y)
        y += 18

        ' Items
        If _ItemsDt IsNot Nothing AndAlso _ItemsDt.Rows.Count > 0 Then
            g.DrawString("ITEMS USED:", fontBold, Brushes.Black, left, y)
            y += 18

            For Each row As DataRow In _ItemsDt.Rows
                Dim name As String = row("ItemName").ToString().Trim()
                Dim qty As Integer = CInt(row("Quantity"))
                Dim lineTotal As Decimal = qty * CDec(row("Price"))

                Dim leftText As String = name & " x" & qty
                Dim rightText As String = "P" & lineTotal.ToString("F2")
                ' Items Truncation
                If g.MeasureString(leftText, fontBody).Width > (right - left - 85) Then
                    ' Only substring if the text is actually longer than 25 chars
                    If leftText.Length > 25 Then
                        leftText = leftText.Substring(0, 22) & ".."
                    End If
                End If

                g.DrawString(leftText, fontBody, Brushes.Black, left, y)
                g.DrawString(rightText, fontBody, Brushes.Black, right, y, rightAlign)
                y += 16
            Next

            g.DrawString("".PadRight(45, "-"), fontBody, Brushes.Black, left, y)
            y += 18
        End If
        Dim totalDec As Decimal = CDec(_Total)
        Dim vatExemptDec As Decimal = Math.Round(totalDec / 1.12D, 2)
        Dim vatAmountDec As Decimal = Math.Round(totalDec - vatExemptDec, 2)
        ' ================= VAT SECTION - CLEAN & PROFESSIONAL =================
        g.DrawString("SUBTOTAL:", fontBody, Brushes.Black, left, y)
        g.DrawString("P" & totalDec.ToString("F2"), fontBody, Brushes.Black, right, y, rightAlign)
        y += 16

        g.DrawString("VATable Sales:", fontBody, Brushes.Black, left, y)
        g.DrawString("P" & vatExemptDec.ToString("F2"), fontBody, Brushes.Black, right, y, rightAlign)
        y += 16

        g.DrawString("VAT (12%):", fontBody, Brushes.Black, left, y)
        g.DrawString("P" & vatAmountDec.ToString("F2"), fontBody, Brushes.Black, right, y, rightAlign)
        y += 22

        ' Grand Total
        g.DrawString("TOTAL AMOUNT:", New Font("Consolas", 10, FontStyle.Bold), Brushes.Black, left, y)
        g.DrawString("P" & _Total, New Font("Consolas", 10, FontStyle.Bold), Brushes.Black, right, y, rightAlign)
        y += 28

        ' Payment
        g.DrawString("Amount Paid:", fontBody, Brushes.Black, left, y)
        g.DrawString("P" & _AmountPaid, fontBody, Brushes.Black, right, y, rightAlign)
        y += 16

        g.DrawString("CHANGE:", fontBody, Brushes.Black, left, y)
        g.DrawString("P" & _Change, fontBody, Brushes.Black, right, y, rightAlign)
        y += 25

        ' Follow-up
        If _FollowUpsDt IsNot Nothing AndAlso _FollowUpsDt.Rows.Count > 0 Then
            g.DrawString("FOLLOW-UP SCHEDULE:", fontBold, Brushes.Black, left, y)
            y += 18
            For Each row As DataRow In _FollowUpsDt.Rows
                Dim fDate As String = Convert.ToDateTime(row("FollowUpDate")).ToString("MM/dd/yyyy")
                Dim reason As String = row("Reason").ToString()
                g.DrawString(fDate & " - " & reason, fontBody, Brushes.Black, left, y)
                y += 16
            Next
            y += 12
        End If

        ' Notes
        g.DrawString("DENTIST NOTES:", fontBold, Brushes.Black, left, y)
        y += 18
        Dim notesRect As New RectangleF(left, y, right - left, 140)
        g.DrawString(_Notes, fontBody, Brushes.Black, notesRect)
        Dim measured = g.MeasureString(_Notes, fontBody, notesRect.Size)
        y += measured.Height + 20

        ' Footer
        g.DrawString("".PadRight(45, "-"), fontBody, Brushes.Black, left, y)
        y += 18

        g.DrawString("TOTAL AMOUNT: P" & _Total, New Font("Consolas", 9, FontStyle.Bold), Brushes.Black, left, y)
        y += 20
        g.DrawString("Method: " & _Method, fontBody, Brushes.Black, left, y)
        y += 25

        g.DrawString("Thank you for visiting!", fontBody, Brushes.Black,
                     New RectangleF(left, y, right - left, 40), center)
        y += 65
        e.HasMorePages = False
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

    ''' <summary>
    ''' Flash Preview - Updated to include Items and consistent formatting
    ''' </summary>
    Public Function GetReceiptFlashPreview(
        patientName As String,
        dentistName As String,
        notes As String,
        total As String,
        amountPaid As String,
        change As String,
        method As String,
        refNo As String,
        servicesDt As DataTable,
        itemsDt As DataTable,
        followUpsDt As DataTable) As String
        Dim totalDec As Decimal = CDec(total)
        Dim vatExemptDec As Decimal = Math.Round(totalDec / 1.12D, 2)
        Dim vatAmountDec As Decimal = Math.Round(totalDec - vatExemptDec, 2)
        Dim flashMsg As String = GetReceiptHeader() & vbCrLf & vbCrLf

        flashMsg &= "Date: " & DateTime.Now.ToString("G") & vbCrLf
        flashMsg &= "Patient: " & patientName & vbCrLf
        flashMsg &= "Doctor:  " & dentistName & vbCrLf

        If Not String.IsNullOrEmpty(refNo) Then flashMsg &= "Ref No:  " & refNo & vbCrLf

        flashMsg &= "--------------------------------" & vbCrLf & vbCrLf

        ' Services
        If servicesDt IsNot Nothing AndAlso servicesDt.Rows.Count > 0 Then
            For Each row As DataRow In servicesDt.Rows
                Dim sName As String = row("ServiceName").ToString()
                Dim sPrice As String = "P" & CDec(row("Price")).ToString("F2")
                flashMsg &= sName & vbTab & sPrice & vbCrLf
            Next
            flashMsg &= vbCrLf
        End If

        ' Items
        If itemsDt IsNot Nothing AndAlso itemsDt.Rows.Count > 0 Then
            flashMsg &= "ITEMS USED:" & vbCrLf
            For Each row As DataRow In itemsDt.Rows
                Dim itemName As String = row("ItemName").ToString()
                Dim qty As Integer = Convert.ToInt32(row("Quantity"))
                Dim lineTotal As Decimal = qty * Convert.ToDecimal(row("Price"))
                flashMsg &= itemName & " x" & qty & vbTab & "P" & lineTotal.ToString("F2") & vbCrLf
            Next
            flashMsg &= vbCrLf
        End If

        flashMsg &= "--------------------------------" & vbCrLf

        ' Financials
        flashMsg &= "SUBTOTAL:           P" & totalDec.ToString("F2") & vbCrLf
        flashMsg &= "VATable Sales:      P" & vatExemptDec.ToString("F2") & vbCrLf
        flashMsg &= "VAT (12%):          P" & vatAmountDec.ToString("F2") & vbCrLf & vbCrLf
        flashMsg &= "TOTAL AMOUNT:       P" & total & vbCrLf & vbCrLf

        ' Payment
        flashMsg &= "Amount Paid:        P" & amountPaid & vbCrLf
        flashMsg &= "CHANGE:             P" & change & vbCrLf & vbCrLf

        ' Follow-ups
        If followUpsDt IsNot Nothing AndAlso followUpsDt.Rows.Count > 0 Then
            flashMsg &= "FOLLOW-UP SCHEDULE:" & vbCrLf
            For Each row As DataRow In followUpsDt.Rows
                Dim fDate As String = Convert.ToDateTime(row("FollowUpDate")).ToString("MM/dd/yyyy")
                Dim fReason As String = row("Reason").ToString()
                flashMsg &= fDate & " - " & fReason & vbCrLf
            Next
            flashMsg &= vbCrLf
        End If

        ' Notes & Footer
        flashMsg &= "DENTIST NOTES:" & vbCrLf & notes & vbCrLf & vbCrLf
        flashMsg &= "--------------------------------" & vbCrLf
        flashMsg &= "TOTAL AMOUNT: P" & total & vbCrLf
        flashMsg &= "Method: " & method & vbCrLf & vbCrLf
        flashMsg &= "Thank you for visiting!"

        Return flashMsg
    End Function

End Module