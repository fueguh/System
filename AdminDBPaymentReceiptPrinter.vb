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
        subtotal As String,
        vatExempt As String,
        vatAmount As String,
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
        _ItemsDt = items

        ' Print setup - Better for 80mm thermal printer
        Dim pd As New PrintDocument()
        pd.DefaultPageSettings.PaperSize = New PaperSize("80mm Thermal", 315, 0)  ' Continuous roll
        pd.DefaultPageSettings.Margins = New Margins(10, 10, 10, 10)

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
        Dim currentY As Single = 15.0F
        Dim fontHeader As New Font("Consolas", 9, FontStyle.Bold)
        Dim fontBody As New Font("Consolas", 8)
        Dim fontBold As New Font("Consolas", 8, FontStyle.Bold)
        Dim leftMargin As Single = 8
        Dim rightMargin As Single = 290

        Dim sfRight As New StringFormat() With {.Alignment = StringAlignment.Far}
        Dim sfCenter As New StringFormat() With {.Alignment = StringAlignment.Center}

        ' ================= HEADER =================
        g.DrawString(ClinicName, New Font("Consolas", 10, FontStyle.Bold), Brushes.Black,
                     New RectangleF(leftMargin, currentY, rightMargin - leftMargin, 30), sfCenter)
        currentY += 28

        g.DrawString(ClinicAddress, New Font("Consolas", 7), Brushes.Black,
                     New RectangleF(leftMargin, currentY, rightMargin - leftMargin, 30), sfCenter)
        currentY += 18

        g.DrawString(ClinicContact, New Font("Consolas", 7), Brushes.Black,
                     New RectangleF(leftMargin, currentY, rightMargin - leftMargin, 20), sfCenter)
        currentY += 22

        g.DrawString("OFFICIAL RECEIPT", New Font("Consolas", 11, FontStyle.Bold), Brushes.Black,
                     New RectangleF(leftMargin, currentY, rightMargin - leftMargin, 30), sfCenter)
        currentY += 30

        ' Basic Info
        g.DrawString("Date: " & DateTime.Now.ToString("G"), fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 16
        g.DrawString("Patient: " & _PatientName, fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 16
        g.DrawString("Doctor: " & _DentistName, fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 16

        If Not String.IsNullOrEmpty(_RefNo) Then
            g.DrawString("Ref No: " & _RefNo, fontBody, Brushes.Black, leftMargin, currentY)
            currentY += 16
        End If

        g.DrawString("".PadRight(45, "-"), fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 20

        ' ================= SERVICES =================
        For Each row As DataRow In _ServicesDt.Rows
            Dim sName As String = row("ServiceName").ToString().Trim()
            Dim sPrice As String = "P" & CDec(row("Price")).ToString("F2")

            If g.MeasureString(sName, fontBody).Width > rightMargin - leftMargin - 70 Then
                sName = sName.Substring(0, 28) & ".."
            End If

            g.DrawString(sName, fontBody, Brushes.Black, leftMargin, currentY)
            g.DrawString(sPrice, fontBody, Brushes.Black, rightMargin, currentY, sfRight)
            currentY += 16
        Next

        g.DrawString("".PadRight(45, "-"), fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 18

        ' ================= ITEMS (from ReceiptItems) =================
        If _ItemsDt IsNot Nothing AndAlso _ItemsDt.Rows.Count > 0 Then
            g.DrawString("ITEMS USED:", fontBold, Brushes.Black, leftMargin, currentY)
            currentY += 18

            For Each row As DataRow In _ItemsDt.Rows
                Dim itemName As String = row("ItemName").ToString().Trim()
                Dim qty As Integer = Convert.ToInt32(row("Quantity"))
                Dim lineTotal As Decimal = qty * Convert.ToDecimal(row("Price"))

                Dim leftText As String = itemName & " x" & qty
                Dim rightText As String = "P" & lineTotal.ToString("F2")

                If g.MeasureString(leftText, fontBody).Width > rightMargin - leftMargin - 70 Then
                    leftText = leftText.Substring(0, 25) & ".. x" & qty
                End If

                g.DrawString(leftText, fontBody, Brushes.Black, leftMargin, currentY)
                g.DrawString(rightText, fontBody, Brushes.Black, rightMargin, currentY, sfRight)
                currentY += 16
            Next

            g.DrawString("".PadRight(45, "-"), fontBody, Brushes.Black, leftMargin, currentY)
            currentY += 18
        End If

        ' ================= VAT & TOTAL =================
        g.DrawString("SUBTOTAL:", fontBody, Brushes.Black, leftMargin, currentY)
        g.DrawString("P" & _Subtotal, fontBody, Brushes.Black, rightMargin, currentY, sfRight)
        currentY += 16

        g.DrawString("VATable Sales:", fontBody, Brushes.Black, leftMargin, currentY)
        g.DrawString("P" & _VatExempt, fontBody, Brushes.Black, rightMargin, currentY, sfRight)
        currentY += 16

        g.DrawString("VAT (12%):", fontBody, Brushes.Black, leftMargin, currentY)
        g.DrawString("P" & _VatAmount, fontBody, Brushes.Black, rightMargin, currentY, sfRight)
        currentY += 20

        g.DrawString("TOTAL AMOUNT:", fontBold, Brushes.Black, leftMargin, currentY)
        g.DrawString("P" & _Total, New Font("Consolas", 9, FontStyle.Bold), Brushes.Black, rightMargin, currentY, sfRight)
        currentY += 28

        ' ================= PAYMENT =================
        g.DrawString("Amount Paid:", fontBody, Brushes.Black, leftMargin, currentY)
        g.DrawString("P" & _AmountPaid, fontBody, Brushes.Black, rightMargin, currentY, sfRight)
        currentY += 16

        g.DrawString("CHANGE:", fontBody, Brushes.Black, leftMargin, currentY)
        g.DrawString("P" & _Change, fontBody, Brushes.Black, rightMargin, currentY, sfRight)
        currentY += 28

        ' ================= FOLLOW-UPS =================
        If _FollowUpsDt IsNot Nothing AndAlso _FollowUpsDt.Rows.Count > 0 Then
            g.DrawString("FOLLOW-UP SCHEDULE:", fontBold, Brushes.Black, leftMargin, currentY)
            currentY += 18

            For Each row As DataRow In _FollowUpsDt.Rows
                Dim fDate As String = Convert.ToDateTime(row("FollowUpDate")).ToString("MM/dd/yyyy")
                Dim fReason As String = row("Reason").ToString()
                g.DrawString(fDate & " - " & fReason, fontBody, Brushes.Black, leftMargin, currentY)
                currentY += 16
            Next
            currentY += 10
        End If

        ' ================= NOTES =================
        g.DrawString("DENTIST NOTES:", fontBold, Brushes.Black, leftMargin, currentY)
        currentY += 18

        Dim notesRect As New RectangleF(leftMargin, currentY, rightMargin - leftMargin, 160)
        g.DrawString(_Notes, fontBody, Brushes.Black, notesRect)
        Dim measured = g.MeasureString(_Notes, fontBody, notesRect.Size)
        currentY += measured.Height + 20

        ' ================= FOOTER =================
        g.DrawString("".PadRight(45, "-"), fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 18

        g.DrawString("TOTAL AMOUNT: P" & _Total, New Font("Consolas", 9, FontStyle.Bold), Brushes.Black, leftMargin, currentY)
        currentY += 22
        g.DrawString("Method: " & _Method, fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 25

        g.DrawString("Thank you for visiting!", fontBody, Brushes.Black,
                     New RectangleF(leftMargin, currentY, rightMargin - leftMargin, 40), sfCenter)

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
        subtotal As String,
        vatExempt As String,
        vatAmount As String,
        total As String,
        amountPaid As String,
        change As String,
        method As String,
        refNo As String,
        servicesDt As DataTable,
        itemsDt As DataTable,
        followUpsDt As DataTable) As String

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
        flashMsg &= "SUBTOTAL:           P" & subtotal & vbCrLf
        flashMsg &= "VATable Sales:      P" & vatExempt & vbCrLf
        flashMsg &= "VAT (12%):          P" & vatAmount & vbCrLf & vbCrLf
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