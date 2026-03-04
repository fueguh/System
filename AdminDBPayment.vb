Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.Management

Public Class AdminDBPayment

    ' ===== PASSED FROM PREVIOUS FORM =====
    Public SelectedAppointmentID As Integer
    Public SelectedPatientID As Integer
    Public SelectedPatientName As String
    Private SelectedDentistName As String = ""

    ' ================= FORM LOAD =================
    Private Sub AdminDBPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If SelectedAppointmentID <= 0 Then
            MessageBox.Show("No appointment selected.")
            Me.Close()
            Exit Sub
        End If
        ' --- PUT THE FETCH LOGIC HERE ---
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim cmd As New SqlCommand("
                SELECT U.FullName 
                FROM Appointments A 
                INNER JOIN Users U ON A.UserID = U.UserID 
                WHERE A.AppointmentID = @AID", con)
            cmd.Parameters.AddWithValue("@AID", SelectedAppointmentID)

            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing Then
                SelectedDentistName = result.ToString()
            End If
        End Using
        ' --------------------------------
        LoadPatients()
        LoadAllServices()              ' for ComboBox
        LoadAppointmentServices()      ' for DataGridView
        LoadPaymentMethods()

        TextBoxTotal.ReadOnly = True
        DGVServices.ReadOnly = True
        DGVServices.AllowUserToAddRows = False
    End Sub

    ' ================= LOAD PATIENT =================
    Private Sub LoadPatients()
        patient_name.Text = SelectedPatientName
    End Sub

    ' ================= LOAD ALL SERVICES (COMBOBOX) =================
    Private Sub LoadAllServices()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim da As New SqlDataAdapter("SELECT ServiceID, ServiceName, Price FROM Services ORDER BY ServiceName ASC", con)
            Dim dt As New DataTable()
            da.Fill(dt)

            clbServices.DataSource = dt
            clbServices.DisplayMember = "ServiceName"
            clbServices.ValueMember = "ServiceID"

            ' Clear all checks
            For i As Integer = 0 To clbServices.Items.Count - 1
                clbServices.SetItemChecked(i, False)
            Next
        End Using
    End Sub


    ' ================= LOAD APPOINTMENT SERVICES (GRID) =================
    Private Sub LoadAppointmentServices()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            ' Get services linked to this appointment
            Dim cmd As New SqlCommand("
            SELECT 
                S.ServiceID,
                S.ServiceName,
                S.Price
            FROM AppointmentServices AS A
            INNER JOIN Services AS S ON A.ServiceID = S.ServiceID
            WHERE A.AppointmentID = @AppointmentID", con)
            cmd.Parameters.AddWithValue("@AppointmentID", SelectedAppointmentID)

            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            ' Show in DataGridView (read-only)
            DGVServices.DataSource = dt
            DGVServices.ReadOnly = True
            If DGVServices.Columns.Contains("ServiceID") Then
                DGVServices.Columns("ServiceID").Visible = False
            End If
            ' Check services in CheckedListBox
            For i As Integer = 0 To clbServices.Items.Count - 1
                Dim item As DataRowView = CType(clbServices.Items(i), DataRowView)
                If dt.AsEnumerable().Any(Function(r) r.Field(Of Integer)("ServiceID") = item("ServiceID")) Then
                    clbServices.SetItemChecked(i, True)
                Else
                    clbServices.SetItemChecked(i, False)
                End If
            Next

            ' Update total
            CalculateTotal()
        End Using
    End Sub

    ' ================= PAYMENT METHODS =================
    Private Sub LoadPaymentMethods()
        ComboBoxPaymentMethod.Items.Clear()
        ComboBoxPaymentMethod.Items.AddRange(
            New String() {"Cash", "Card", "Insurance"})
    End Sub

    ' ================= TOTAL CALCULATION =================
    Private Sub CalculateTotal()
        Dim total As Decimal = 0

        ' Loop through all checked items in the CheckedListBox
        For Each item In clbServices.CheckedItems
            Dim row As DataRowView = CType(item, DataRowView)
            total += Convert.ToDecimal(row("Price"))
        Next

        TextBoxTotal.Text = total.ToString("F2")
    End Sub


    ' ================= SAVE RECEIPT =================
    Private Sub ButtonGenerateReceipt_Click(sender As Object, e As EventArgs) Handles ButtonGenerateReceipt.Click
        ' ... [Keep your existing validation] ...

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim transaction = con.BeginTransaction() ' Use a transaction for safety

            Try
                ' 1. Insert the Receipt
                Dim cmdRec As New SqlCommand("
                    INSERT INTO Receipts (AppointmentID, PatientID, UserID, TotalAmount, PaymentMethod)
                    VALUES (@AID, @PID, @UID, @Total, @Method)", con, transaction)

                cmdRec.Parameters.AddWithValue("@AID", SelectedAppointmentID)
                cmdRec.Parameters.AddWithValue("@PID", SelectedPatientID)
                cmdRec.Parameters.AddWithValue("@UID", SystemSession.LoggedInUserID)
                cmdRec.Parameters.AddWithValue("@Total", CDec(TextBoxTotal.Text))
                cmdRec.Parameters.AddWithValue("@Method", ComboBoxPaymentMethod.Text)
                cmdRec.ExecuteNonQuery()

                ' 2. (Optional) Sync AppointmentServices 
                ' If you want the DB to remember the services checked here:
                Dim cmdDel As New SqlCommand("DELETE FROM AppointmentServices WHERE AppointmentID = @AID", con, transaction)
                cmdDel.Parameters.AddWithValue("@AID", SelectedAppointmentID)
                cmdDel.ExecuteNonQuery()

                For Each item In clbServices.CheckedItems
                    Dim row As DataRowView = CType(item, DataRowView)
                    Dim cmdIns As New SqlCommand("INSERT INTO AppointmentServices (AppointmentID, ServiceID) VALUES (@AID, @SID)", con, transaction)
                    cmdIns.Parameters.AddWithValue("@AID", SelectedAppointmentID)
                    cmdIns.Parameters.AddWithValue("@SID", row("ServiceID"))
                    cmdIns.ExecuteNonQuery()
                Next

                transaction.Commit()
                MessageBox.Show("Receipt generated and services synced!")
                ' ==========================================
                ' ADD THE AUDIT TRAIL LOG HERE
                ' ==========================================
                SystemSession.LogAudit("Generated Receipt for " & SelectedPatientName & " (Amt: " & TextBoxTotal.Text & ")",
                               "Payment Management",
                               SystemSession.LoggedInUserID,
                               SystemSession.LoggedInFullName,
                               SystemSession.LoggedInRole)
                ' ==========================================
            Catch ex As Exception
                transaction.Rollback()
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using
    End Sub

    ' ================= PRINT RECEIPT =================
    Private Sub ButtonPrintReceipt_Click(sender As Object, e As EventArgs) Handles ButtonPrintReceipt.Click

        Dim pd As New PrintDocument()
        ' --- FORCE THE PAPER SIZE ---
        ' 300 is roughly 80mm width. 1000 is length (it will auto-cut if your printer supports it)
        Dim ps As New PaperSize("Custom", 300, 1000)
        pd.DefaultPageSettings.PaperSize = ps
        ' ----------------------------
        Dim dlg As New PrintDialog()

        dlg.Document = pd

        ' Show printer selection dialog
        If dlg.ShowDialog() = DialogResult.OK Then
            Dim selectedPrinter As String = pd.PrinterSettings.PrinterName

            ' Check if printer exists
            If Not IsPrinterInstalled(selectedPrinter) Then
                MessageBox.Show("The selected printer is not installed.", "Printer Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ' Check if printer is online
            If Not IsPrinterOnline(selectedPrinter) Then
                MessageBox.Show("The selected printer is offline.", "Printer Offline", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            AddHandler pd.PrintPage, AddressOf PrintPageHandler

            Try
                pd.Print()

                ' ==========================================
                ' ADD AUDIT LOG FOR PRINTING ACTION
                ' ==========================================
                SystemSession.LogAudit("Printed Receipt for " & SelectedPatientName & " (Appt ID: " & SelectedAppointmentID & ")",
                                   "Payment Management",
                                   SystemSession.LoggedInUserID,
                                   SystemSession.LoggedInFullName,
                                   SystemSession.LoggedInRole)
                ' ==========================================

            Catch ex As Exception
                MessageBox.Show("Printing failed: " & ex.Message, "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub


    ' =========================
    ' CHECK IF PRINTER INSTALLED
    ' =========================
    Private Function IsPrinterInstalled(printerName As String) As Boolean
        For Each p As String In PrinterSettings.InstalledPrinters
            If p = printerName Then
                Return True
            End If
        Next
        Return False
    End Function


    ' =========================
    ' CHECK IF PRINTER ONLINE
    ' =========================
    Private Function IsPrinterOnline(printerName As String) As Boolean
        Try
            Dim searcher As New ManagementObjectSearcher(
            "SELECT * FROM Win32_Printer WHERE Name = '" & printerName.Replace("\", "\\") & "'")

            For Each printer As ManagementObject In searcher.Get()
                Return Not CBool(printer("WorkOffline"))
            Next

            Return False
        Catch
            Return False
        End Try
    End Function


    ' =========================
    ' RECEIPT LAYOUT
    ' =========================
    Private Sub PrintPageHandler(sender As Object, e As PrintPageEventArgs)
        Dim g As Graphics = e.Graphics
        Dim currentY As Integer = 10
        Dim fontBody As New Font("Consolas", 9)
        Dim leftMargin As Integer = 5
        Dim rightMargin As Integer = 180

        ' 1. Header & Info (Condensed to save space)
        g.DrawString("Dental Clinic Receipt", New Font("Arial", 12, FontStyle.Bold), Brushes.Black, leftMargin, currentY)
        currentY += 25
        g.DrawString("--------------------------------", fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 15
        g.DrawString("Patient: " & SelectedPatientName, fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 15
        g.DrawString("Dentist: " & SelectedDentistName, fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 15
        g.DrawString("Date: " & DateTime.Now.ToString("yyyy-MM-dd HH:mm"), fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 15
        g.DrawString("--------------------------------", fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 15

        ' 2. Services (Alphabetical loop)
        For Each item In clbServices.CheckedItems
            Dim row As DataRowView = CType(item, DataRowView)
            Dim serviceName As String = row("ServiceName").ToString()
            Dim price As String = "₱" & Convert.ToDecimal(row("Price")).ToString("F2")
            g.DrawString(serviceName, fontBody, Brushes.Black, leftMargin, currentY)
            Dim priceSize = g.MeasureString(price, fontBody)
            g.DrawString(price, fontBody, Brushes.Black, rightMargin - priceSize.Width, currentY)
            currentY += 15
        Next

        ' 3. Totals & Payment (FIXED CORRUPTION)
        currentY += 10
        g.DrawString("--------------------------------", fontBody, Brushes.Black, leftMargin, currentY)
        currentY += 15
        Dim totalAmt = "₱" & TextBoxTotal.Text
        g.DrawString("TOTAL:", New Font("Consolas", 9, FontStyle.Bold), Brushes.Black, leftMargin, currentY)
        g.DrawString(totalAmt, fontBody, Brushes.Black, rightMargin - g.MeasureString(totalAmt, fontBody).Width, currentY)

        currentY += 25
        ' Combined into one line to stop overlapping "corrupted" text
        g.DrawString("Method: " & ComboBoxPaymentMethod.Text, fontBody, Brushes.Black, leftMargin, currentY)

        ' 4. Footer
        currentY += 25
        g.DrawString("Thank you for visiting!", fontBody, Brushes.Black, leftMargin, currentY)

        ' 5. THE "FORCE FEED" TAIL
        ' We add a tiny dot 150 units down. This forces the printer to roll the 
        ' paper past the cutter so the "Thank You" isn't trapped inside.
        currentY += 45
        g.DrawString(".", New Font("Arial", 1), Brushes.Black, leftMargin, currentY)
    End Sub

    ' ================= NAVIGATION =================
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub
    Private Sub ClbServices_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles clbServices.ItemCheck
        ' Delay execution until the check state is updated
        Me.BeginInvoke(Sub() CalculateTotal())
    End Sub

End Class
