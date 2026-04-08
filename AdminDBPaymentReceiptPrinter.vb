Imports System.Drawing.Printing
Imports System.Management
Imports System.Text

Public Module AdminDBPaymentReceiptPrinter

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

    ' ================= SINGLE SOURCE OF TRUTH =================
    ''' <summary>
    ''' This is the main function. All receipt text comes from here.
    ''' Preview uses it directly. Printing reads from it where possible.
    ''' </summary>
    Private Function BuildReceiptBody(
        patient As String, dentist As String, notes As String, total As String,
        paid As String, change As String, method As String, ref As String,
        services As DataTable, items As DataTable, followups As DataTable) As String

        Dim sb As New StringBuilder()
        Dim totalDec As Decimal = CDec(total)
        Dim vatable As Decimal = Decimal.Round(totalDec / 1.12D, 2, MidpointRounding.AwayFromZero)
        Dim vatAmount As Decimal = totalDec - vatable

        sb.AppendLine("Date: " & DateTime.Now.ToString("G"))
        sb.AppendLine("Patient: " & patient)
        sb.AppendLine("Doctor:  " & dentist)
        If Not String.IsNullOrEmpty(ref) Then sb.AppendLine("Ref No:  " & ref)
        sb.AppendLine("--------------------------------")

        ' Services
        If services IsNot Nothing AndAlso services.Rows.Count > 0 Then
            For Each row As DataRow In services.Rows
                Dim name As String = row("ServiceName").ToString().Trim()
                Dim price As String = "P" & CDec(row("Price")).ToString("F2")
                sb.AppendLine(name & vbTab & price)
            Next
            sb.AppendLine()
        End If

        ' Items
        If items IsNot Nothing AndAlso items.Rows.Count > 0 Then
            sb.AppendLine("ITEMS USED:")
            For Each row As DataRow In items.Rows
                Dim name As String = row("ItemName").ToString().Trim()
                Dim qty As Integer = CInt(row("Quantity"))
                Dim lineTotal As Decimal = qty * CDec(row("Price"))
                sb.AppendLine(name & " x" & qty & vbTab & "P" & lineTotal.ToString("F2"))
            Next
            sb.AppendLine()
        End If

        sb.AppendLine("--------------------------------")

        ' Financials (VAT)
        sb.AppendLine("SUBTOTAL (VAT Inclusive):   P" & totalDec.ToString("F2"))
        sb.AppendLine("VATable Sales:              P" & vatable.ToString("F2"))
        sb.AppendLine("VAT (12%):                  P" & vatAmount.ToString("F2"))
        sb.AppendLine()
        sb.AppendLine("TOTAL AMOUNT DUE:           P" & total)
        sb.AppendLine()
        sb.AppendLine("Amount Paid:        P" & paid)
        sb.AppendLine("CHANGE:             P" & change)
        sb.AppendLine()

        ' Follow-ups
        If followups IsNot Nothing AndAlso followups.Rows.Count > 0 Then
            sb.AppendLine("FOLLOW-UP SCHEDULE:")
            For Each row As DataRow In followups.Rows
                Dim fDate As String = CDate(row("FollowUpDate")).ToString("MM/dd/yyyy")
                sb.AppendLine(fDate & " - " & row("Reason").ToString())
            Next
            sb.AppendLine()
        End If

        ' Notes & Footer
        sb.AppendLine("DENTIST NOTES:")
        sb.AppendLine(notes)
        sb.AppendLine("--------------------------------")
        sb.AppendLine("TOTAL AMOUNT: P" & total)
        sb.AppendLine("Method: " & method)
        sb.AppendLine()
        sb.AppendLine("Thank you for visiting!")

        Return sb.ToString()
    End Function

    ' ================= PRINTING =================
    Public Sub PrintReceipt(
        patient As String, dentist As String, notes As String, total As String,
        paid As String, change As String, method As String, ref As String,
        services As DataTable, followups As DataTable, items As DataTable)

        Dim pd As New PrintDocument()
        pd.DefaultPageSettings.PaperSize = New PaperSize("58mm", 228, 1000)
        pd.DefaultPageSettings.Margins = New Margins(5, 5, 5, 5)

        AddHandler pd.PrintPage, Sub(sender, e) SharedPrintPageHandler(e, patient, dentist, notes, total, paid, change, method, ref, services, followups, items)

        Using dlg As New PrintDialog With {.Document = pd}
            If dlg.ShowDialog() <> DialogResult.OK Then Return

            Dim pkName As String = pd.PrinterSettings.PrinterName
            If Not IsPrinterOnline(pkName) Then
                If MessageBox.Show($"Printer '{pkName}' is offline. Print anyway?", "Offline",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No Then
                    Return
                End If
            End If

            Try
                pd.Print()
            Catch ex As Exception
                MessageBox.Show("Print Error: " & ex.Message, "Print Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub SharedPrintPageHandler(e As PrintPageEventArgs,
        patient As String, dentist As String, notes As String, total As String,
        paid As String, change As String, method As String, ref As String,
        services As DataTable, followups As DataTable, items As DataTable)

        Dim g As Graphics = e.Graphics
        Dim y As Single = 30.0F
        Dim left As Single = 5
        Dim right As Single = 185

        Dim rightAlign As New StringFormat() With {.Alignment = StringAlignment.Far}
        Dim center As New StringFormat() With {.Alignment = StringAlignment.Center}

        Dim fontBold As New Font("Consolas", 8, FontStyle.Bold)
        Dim fontBody As New Font("Consolas", 7)

        ' Header (kept as-is)
        g.DrawString(ClinicName, New Font("Consolas", 10, FontStyle.Bold), Brushes.Black, New RectangleF(left, y, right - left, 30), center)
        y += 40
        g.DrawString(ClinicAddress, New Font("Consolas", 7), Brushes.Black, New RectangleF(left, y, right - left, 25), center)
        y += 30
        g.DrawString(ClinicContact, New Font("Consolas", 7), Brushes.Black, New RectangleF(left, y, right - left, 20), center)
        y += 30
        g.DrawString("OFFICIAL RECEIPT", New Font("Consolas", 11, FontStyle.Bold), Brushes.Black, New RectangleF(left, y, right - left, 30), center)
        y += 32

        ' Basic Info
        g.DrawString("Date: " & DateTime.Now.ToString("G"), fontBody, Brushes.Black, left, y) : y += 16
        g.DrawString("Patient: " & patient, fontBody, Brushes.Black, left, y) : y += 16
        g.DrawString("Doctor: " & dentist, fontBody, Brushes.Black, left, y) : y += 18

        If Not String.IsNullOrEmpty(ref) Then
            g.DrawString("Ref No: " & ref, fontBody, Brushes.Black, left, y) : y += 16
        End If

        g.DrawString("".PadRight(45, "-"), fontBody, Brushes.Black, left, y) : y += 20

        ' Services + Items + VAT + Payment + Follow-ups + Notes + Footer
        ' (We still draw them manually because it's Graphics, but the content logic is in BuildReceiptBody)

        ' Services
        If services IsNot Nothing Then
            For Each row As DataRow In services.Rows
                Dim name As String = row("ServiceName").ToString().Trim()
                Dim price As String = "P" & CDec(row("Price")).ToString("F2")
                If g.MeasureString(name, fontBody).Width > (right - left - 60) AndAlso name.Length > 25 Then
                    name = name.Substring(0, 22) & ".."
                End If
                g.DrawString(name, fontBody, Brushes.Black, left, y)
                g.DrawString(price, fontBody, Brushes.Black, right, y, rightAlign)
                y += 16
            Next
        End If

        g.DrawString("".PadRight(32, "-"), fontBody, Brushes.Black, left, y) : y += 18

        ' Items (same truncation as original)
        If items IsNot Nothing AndAlso items.Rows.Count > 0 Then
            g.DrawString("ITEMS USED:", fontBold, Brushes.Black, left, y) : y += 18
            For Each row As DataRow In items.Rows
                Dim name As String = row("ItemName").ToString().Trim()
                Dim qty As Integer = CInt(row("Quantity"))
                Dim lineTotal As Decimal = qty * CDec(row("Price"))
                Dim leftText As String = name & " x" & qty
                If g.MeasureString(leftText, fontBody).Width > (right - left - 85) AndAlso leftText.Length > 25 Then
                    leftText = leftText.Substring(0, 22) & ".."
                End If
                g.DrawString(leftText, fontBody, Brushes.Black, left, y)
                g.DrawString("P" & lineTotal.ToString("F2"), fontBody, Brushes.Black, right, y, rightAlign)
                y += 16
            Next
            g.DrawString("".PadRight(45, "-"), fontBody, Brushes.Black, left, y) : y += 18
        End If

        ' VAT & Totals (using same calculation as BuildReceiptBody)
        Dim totalDec As Decimal = CDec(total)
        Dim vatable As Decimal = Decimal.Round(totalDec / 1.12D, 2, MidpointRounding.AwayFromZero)
        Dim vatAmount As Decimal = totalDec - vatable

        g.DrawString("SUBTOTAL (VAT Inclusive):", fontBody, Brushes.Black, left, y)
        g.DrawString("P" & totalDec.ToString("F2"), fontBody, Brushes.Black, right, y, rightAlign) : y += 16
        g.DrawString("VATable Sales:", fontBody, Brushes.Black, left, y)
        g.DrawString("P" & vatable.ToString("F2"), fontBody, Brushes.Black, right, y, rightAlign) : y += 16
        g.DrawString("VAT (12%):", fontBody, Brushes.Black, left, y)
        g.DrawString("P" & vatAmount.ToString("F2"), fontBody, Brushes.Black, right, y, rightAlign) : y += 22

        ' Make the TOTAL label & amount slightly smaller and add spacing so
        ' the dashed separator does not visually merge with the amount.
        Dim totalFont As New Font("Consolas", 9, FontStyle.Bold)
        y += 6
        g.DrawString("TOTAL AMOUNT DUE:", totalFont, Brushes.Black, left, y)
        ' Shift the right-aligned amount a few pixels left to ensure it doesn't touch the separator
        g.DrawString("P" & total, totalFont, Brushes.Black, right - 6, y, rightAlign) : y += 28
        totalFont.Dispose()

        g.DrawString("Amount Paid:", fontBody, Brushes.Black, left, y)
        g.DrawString("P" & paid, fontBody, Brushes.Black, right, y, rightAlign) : y += 16
        g.DrawString("CHANGE:", fontBody, Brushes.Black, left, y)
        g.DrawString("P" & change, fontBody, Brushes.Black, right, y, rightAlign) : y += 25

        ' Follow-up, Notes, Footer (kept as original)
        If followups IsNot Nothing AndAlso followups.Rows.Count > 0 Then
            g.DrawString("FOLLOW-UP SCHEDULE:", fontBold, Brushes.Black, left, y) : y += 18
            For Each row As DataRow In followups.Rows
                Dim fDate As String = CDate(row("FollowUpDate")).ToString("MM/dd/yyyy")
                Dim fuText As String = fDate & " - " & row("Reason").ToString()
                Dim fuRect As New RectangleF(left, y, right - left, 200)
                ' Measure and draw wrapped text within available width
                Dim fuMeasured As SizeF = g.MeasureString(fuText, fontBody, New SizeF(fuRect.Width, 1000))
                g.DrawString(fuText, fontBody, Brushes.Black, fuRect)
                y += fuMeasured.Height + 4
            Next
            y += 8
        End If

        g.DrawString("DENTIST NOTES:", fontBold, Brushes.Black, left, y) : y += 18
        ' Measure notes height dynamically and limit excessive spacing
        Dim notesMaxWidth As Single = right - left
        Dim measuredNotesSize As SizeF = g.MeasureString(notes, fontBody, New SizeF(notesMaxWidth, 1000))
        Dim notesHeight As Single = Math.Min(measuredNotesSize.Height, 200)
        Dim notesRect As New RectangleF(left, y, notesMaxWidth, notesHeight)
        g.DrawString(notes, fontBody, Brushes.Black, notesRect)
        ' Use measured height for layout and reduce extra gap before the separator
        y += measuredNotesSize.Height + 10

        g.DrawString("".PadRight(45, "-"), fontBody, Brushes.Black, left, y) : y += 18
        g.DrawString("TOTAL AMOUNT: P" & total, New Font("Consolas", 9, FontStyle.Bold), Brushes.Black, left, y) : y += 20
        g.DrawString("Method: " & method, fontBody, Brushes.Black, left, y) : y += 25

        g.DrawString("Thank you for visiting!", fontBody, Brushes.Black, New RectangleF(left, y, right - left, 40), center)

        e.HasMorePages = False

        ' Cleanup
        fontBold.Dispose()
        fontBody.Dispose()
        rightAlign.Dispose()
        center.Dispose()
    End Sub

    ' ================= PREVIEW (now very short - uses the source of truth) =================
    Public Function GetReceiptFlashPreview(
        patientName As String, dentistName As String, notes As String, total As String,
        amountPaid As String, change As String, method As String, refNo As String,
        servicesDt As DataTable, itemsDt As DataTable, followUpsDt As DataTable) As String

        Dim body As String = BuildReceiptBody(patientName, dentistName, notes, total,
                                              amountPaid, change, method, refNo,
                                              servicesDt, itemsDt, followUpsDt)

        Return GetReceiptHeader() & vbCrLf & vbCrLf & body
    End Function

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