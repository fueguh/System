Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports Guna.UI2.WinForms
Imports Microsoft.VisualBasic.Devices

Public Class AdminDBPayment
    Public SelectedAppointmentID As Integer
    Public SelectedPatientID As Integer
    Public SelectedPatientName As String
    Public SelectedServiceName As String

    Private Sub AdminDBPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPatients()
        LoadAppointmentServices()
        LoadServices()
        ComboBoxPaymentMethod.Items.AddRange(New String() {"Cash", "Card", "Insurance"})
        CmbPatient.SelectedValue = SelectedPatientID
        MessageBox.Show("Loading services for AppointmentID: " & SelectedAppointmentID)
    End Sub

    Private Sub LoadPatients()
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim query As String = "SELECT PatientID, FullName FROM Patients"
            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)

            CmbPatient.DataSource = dt
            CmbPatient.DisplayMember = "FullName"
            CmbPatient.ValueMember = "PatientID"
            CmbPatient.SelectedValue = SelectedPatientID
            DGVServices.DataSource = dt
        End Using
    End Sub

    Private Sub LoadServices()
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim query As String = "SELECT ServiceID, ServiceName, Price FROM Services"
            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            CmbService.DataSource = dt
            CmbService.DisplayMember = "ServiceName"
            CmbService.ValueMember = "ServiceID"
            DGVServices.DataSource = dt
            CalculateTotal()
        End Using
    End Sub

    Private Sub LoadAppointmentServices()
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim query As String = "
            SELECT S.ServiceID, S.ServiceName, S.Price
            FROM AppointmentServices AS ASV
            INNER JOIN Services AS S ON ASV.ServiceID = S.ServiceID
            WHERE ASV.AppointmentID = @AppointmentID
            "
            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@AppointmentID", SelectedAppointmentID)

            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            CmbService.DataSource = dt
            CmbService.DisplayMember = "ServiceName"
            CmbService.ValueMember = "ServiceID"

            If dt.Rows.Count > 0 Then
                TextBoxTotal.Text = dt.Rows(0)("Price").ToString()
            End If

            DGVServices.DataSource = dt
            CalculateTotal()
        End Using
    End Sub

    Private Sub CalculateTotal()
        Dim total As Decimal = 0
        For Each row As DataGridViewRow In DGVServices.Rows
            total += Convert.ToDecimal(row.Cells("Price").Value)
        Next
        TextBoxTotal.Text = total.ToString("F2")
    End Sub

    Private Sub ButtonGenerateReceipt_Click(sender As Object, e As EventArgs) Handles ButtonGenerateReceipt.Click
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim query As String = "
            INSERT INTO Receipts (AppointmentID, PatientID, UserID, TotalAmount, PaymentMethod)
            VALUES (@AppointmentID, @PatientID, @UserID, @TotalAmount, @PaymentMethod)
        "
            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@AppointmentID", SelectedAppointmentID)
            cmd.Parameters.AddWithValue("@PatientID", SelectedPatientID)
            cmd.Parameters.AddWithValue("@UserID", SystemSession.LoggedInUserID)

            If SelectedAppointmentID <= 0 Then
                MessageBox.Show("Invalid AppointmentID. Please select a valid appointment.")
                Exit Sub
            End If

            Dim totalAmount As Decimal
            If Decimal.TryParse(TextBoxTotal.Text, totalAmount) Then
                cmd.Parameters.AddWithValue("@TotalAmount", totalAmount)
            Else
                MessageBox.Show("Invalid total amount.")
                Exit Sub
            End If

            cmd.Parameters.AddWithValue("@PaymentMethod", ComboBoxPaymentMethod.Text)
            cmd.ExecuteNonQuery()
        End Using

        MessageBox.Show("Receipt saved successfully!")
    End Sub

    Private Sub ButtonPrintReceipt_Click(sender As Object, e As EventArgs) Handles ButtonPrintReceipt.Click
        Dim pd As New PrintDocument()

        ' Set printer name (use your thermal printer’s name from Control Panel → Printers)
        pd.PrinterSettings.PrinterName = "Receipt-Printer-58"  ' Example name

        AddHandler pd.PrintPage, AddressOf Me.PrintPageHandler
        pd.Print()
    End Sub

    Private Sub PrintPageHandler(sender As Object, e As PrintPageEventArgs)
        Dim g As Graphics = e.Graphics
        Dim fontTitle As New Font("Arial", 12, FontStyle.Bold)
        Dim fontBody As New Font("Arial", 10)

        Dim y As Integer = 20

        g.DrawString("Dental Clinic Receipt", fontTitle, Brushes.Black, 10, y)
        y += 30
        g.DrawString("Patient: " & SelectedPatientName, fontBody, Brushes.Black, 10, y)
        y += 20
        For Each row As DataGridViewRow In DGVServices.Rows
            g.DrawString("Service: " & row.Cells("ServiceName").Value.ToString() &
                 " ₱" & row.Cells("Price").Value.ToString(),
                 fontBody, Brushes.Black, 10, y)
            y += 20
        Next

        g.DrawString("Total: ₱" & TextBoxTotal.Text, fontBody, Brushes.Black, 10, y)
        y += 20
        g.DrawString("Payment Method: " & ComboBoxPaymentMethod.Text, fontBody, Brushes.Black, 10, y)
        y += 20
        g.DrawString("Date: " & DateTime.Now.ToString("yyyy-MM-dd HH:mm"), fontBody, Brushes.Black, 10, y)
    End Sub

    Private Sub CmbService_RightToLeftChanged(sender As Object, e As EventArgs) Handles CmbService.RightToLeftChanged

    End Sub

    Private Sub CmbService_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbService.SelectedIndexChanged
        Dim drv As DataRowView = TryCast(CmbService.SelectedItem, DataRowView)
        If drv IsNot Nothing Then
            TextBoxTotal.Text = drv("Price").ToString()
        End If
    End Sub
End Class