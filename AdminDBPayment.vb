Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.Management

Public Class AdminDBPayment

    ' ===== PASSED FROM PREVIOUS FORM =====
    Public SelectedAppointmentID As Integer
    Public SelectedPatientID As Integer
    Public SelectedPatientName As String

    ' ================= FORM LOAD =================
    Private Sub AdminDBPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If SelectedAppointmentID <= 0 Then
            MessageBox.Show("No appointment selected.")
            Me.Close()
            Exit Sub
        End If

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
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim da As New SqlDataAdapter(
                "SELECT PatientID, FullName FROM Patients", con)

            Dim dt As New DataTable()
            da.Fill(dt)

            CmbPatient.DataSource = dt
            CmbPatient.DisplayMember = "FullName"
            CmbPatient.ValueMember = "PatientID"
            CmbPatient.SelectedValue = SelectedPatientID

            SelectedPatientName = CmbPatient.Text
        End Using
    End Sub

    ' ================= LOAD ALL SERVICES (COMBOBOX) =================
    Private Sub LoadAllServices()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim da As New SqlDataAdapter("SELECT ServiceID, ServiceName, Price FROM Services", con)
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
        For Each row As DataGridViewRow In DGVServices.Rows
            If Not row.IsNewRow AndAlso row.Cells("Price").Value IsNot DBNull.Value Then
                total += Convert.ToDecimal(row.Cells("Price").Value)
            End If
        Next
        TextBoxTotal.Text = total.ToString("F2")
    End Sub


    ' ================= SAVE RECEIPT =================
    Private Sub ButtonGenerateReceipt_Click(sender As Object, e As EventArgs) Handles ButtonGenerateReceipt.Click

        If ComboBoxPaymentMethod.SelectedIndex = -1 Then
            MessageBox.Show("Please select a payment method.")
            Exit Sub
        End If

        Dim totalAmount As Decimal
        If Not Decimal.TryParse(TextBoxTotal.Text, totalAmount) Then
            MessageBox.Show("Invalid total amount.")
            Exit Sub
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim cmd As New SqlCommand("
                INSERT INTO Receipts
                (AppointmentID, PatientID, UserID, TotalAmount, PaymentMethod)
                VALUES
                (@AppointmentID, @PatientID, @UserID, @TotalAmount, @PaymentMethod)", con)

            cmd.Parameters.AddWithValue("@AppointmentID", SelectedAppointmentID)
            cmd.Parameters.AddWithValue("@PatientID", SelectedPatientID)
            cmd.Parameters.AddWithValue("@UserID", SystemSession.LoggedInUserID)
            cmd.Parameters.AddWithValue("@TotalAmount", totalAmount)
            cmd.Parameters.AddWithValue("@PaymentMethod", ComboBoxPaymentMethod.Text)

            cmd.ExecuteNonQuery()
        End Using

        MessageBox.Show("Receipt saved successfully!")
    End Sub

    ' ================= PRINT RECEIPT =================
    Private Sub ButtonPrintReceipt_Click(sender As Object, e As EventArgs) Handles ButtonPrintReceipt.Click

        Dim pd As New PrintDocument()
        Dim dlg As New PrintDialog()

        dlg.Document = pd

        ' Show printer selection dialog
        If dlg.ShowDialog() = DialogResult.OK Then

            Dim selectedPrinter As String = pd.PrinterSettings.PrinterName

            ' Check if printer exists
            If Not IsPrinterInstalled(selectedPrinter) Then
                MessageBox.Show("The selected printer is not installed.",
                            "Printer Not Found",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
                Exit Sub
            End If

            ' Check if printer is online
            If Not IsPrinterOnline(selectedPrinter) Then
                MessageBox.Show("The selected printer is offline or not available.",
                            "Printer Offline",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
                Exit Sub
            End If

            AddHandler pd.PrintPage, AddressOf PrintPageHandler

            Try
                pd.Print()
            Catch ex As Exception
                MessageBox.Show("Printing failed: " & ex.Message,
                            "Print Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
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
        Dim y As Integer = 10

        Dim fontTitle As New Font("Arial", 12, FontStyle.Bold)
        Dim fontBody As New Font("Consolas", 9)

        Dim leftMargin As Integer = 5
        Dim rightMargin As Integer = 180

        g.DrawString("Dental Clinic Receipt", fontTitle, Brushes.Black, leftMargin, y)
        y += 25

        g.DrawString("--------------------------------", fontBody, Brushes.Black, leftMargin, y)
        y += 15

        g.DrawString("Patient: " & SelectedPatientName, fontBody, Brushes.Black, leftMargin, y)
        y += 15

        g.DrawString("Date: " & DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                 fontBody, Brushes.Black, leftMargin, y)
        y += 15

        g.DrawString("--------------------------------", fontBody, Brushes.Black, leftMargin, y)
        y += 15

        For Each row As DataGridViewRow In DGVServices.Rows
            If Not row.IsNewRow Then

                Dim serviceName As String = row.Cells("ServiceName").Value.ToString()
                Dim price As String = "₱" & row.Cells("Price").Value.ToString()

                g.DrawString(serviceName, fontBody, Brushes.Black, leftMargin, y)

                Dim priceSize = g.MeasureString(price, fontBody)
                g.DrawString(price, fontBody, Brushes.Black,
                         rightMargin - priceSize.Width, y)

                y += 15
            End If
        Next

        y += 10
        g.DrawString("--------------------------------", fontBody, Brushes.Black, leftMargin, y)
        y += 15

        Dim totalText As String = "TOTAL:"
        Dim totalAmount As String = "₱" & TextBoxTotal.Text

        g.DrawString(totalText, fontBody, Brushes.Black, leftMargin, y)

        Dim totalSize = g.MeasureString(totalAmount, fontBody)
        g.DrawString(totalAmount, fontBody, Brushes.Black,
                 rightMargin - totalSize.Width, y)

        y += 15

        g.DrawString("Payment: " & ComboBoxPaymentMethod.Text,
                 fontBody, Brushes.Black, leftMargin, y)

        y += 20
        g.DrawString("Thank you for visiting!", fontBody, Brushes.Black, leftMargin, y)

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
