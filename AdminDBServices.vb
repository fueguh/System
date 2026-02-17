Imports System.Data.SqlClient

Public Class AdminDBServices
    Private selectedServiceID As Integer = 0
    Private Sub LoadServices()
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "SELECT * FROM Services ORDER BY ServiceID"

            Using da As New SqlDataAdapter(query, con)
                Dim dt As New DataTable()
                da.Fill(dt)
                DGVService.DataSource = dt
            End Using
        End Using
    End Sub

    Private Sub AdminDBServices_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadServices()
        Clearform()
    End Sub

    Private Sub DGVService_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then
            Dim row = DGVService.Rows(e.RowIndex)

            selectedServiceID = row.Cells("ServiceID").Value
            txtServiceName.Text = row.Cells("ServiceName").Value.ToString()
            txtPrice.Text = row.Cells("Price").Value.ToString()
            txtDuration.Text = row.Cells("Duration").Value.ToString()
        End If

    End Sub
    Private Function TryParseDuration(input As String, ByRef minutes As Integer) As Boolean
        minutes = 0
        If String.IsNullOrWhiteSpace(input) Then Return False

        Dim s As String = input.Trim().ToLowerInvariant()

        ' Remove commas and extra spaces
        s = s.Replace(",", " ").Replace("  ", " ").Trim()

        ' Quick numeric-only case: "30" or " 30 "
        Dim n As Integer
        If Integer.TryParse(s, n) Then
            minutes = n
            Return True
        End If

        ' Remove common words and symbols
        s = s.Replace("minutes", "min").Replace("minute", "min").Replace("mins", "min")
        s = s.Replace("hours", "h").Replace("hour", "h").Replace("hrs", "h").Replace("hr", "h")
        s = s.Replace(" ", "")

        ' Pattern 1: "30min" or "30min." or "30min,"
        Dim m As Integer = 0
        If s.EndsWith("min") Then
            Dim numPart = s.Substring(0, s.Length - 3)
            If Integer.TryParse(numPart, m) Then
                minutes = m
                Return True
            End If
        End If

        Return False

    End Function

    Private Sub BTNAdd_Click(sender As Object, e As EventArgs) Handles BTNAdd.Click
        Dim price As Decimal
        Dim durationMinutes As Integer

        If txtServiceName.Text.Trim() = "" Then
            MessageBox.Show("Service name is required.")
            Return
        End If

        If Not Decimal.TryParse(txtPrice.Text.Trim(), price) Then
            MessageBox.Show("Please enter a valid price.")
            txtPrice.Focus()
            Return
        End If

        If Not TryParseDuration(txtDuration.Text, durationMinutes) Then
            MessageBox.Show("Please enter a valid duration (e.g., 30, 30 min, 1h30).")
            txtDuration.Focus()
            Return
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim query As String = "
            INSERT INTO Services (ServiceName, Price, Duration)
            VALUES (@name, @price, @duration)
        "
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = txtServiceName.Text.Trim()
                Dim pParam = cmd.Parameters.Add("@price", SqlDbType.Decimal)
                pParam.Precision = 10
                pParam.Scale = 2
                pParam.Value = price
                cmd.Parameters.Add("@duration", SqlDbType.Int).Value = durationMinutes
                cmd.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("Service added successfully.")
        'audit log
        LogAudit("Added new service: ", "Service Management")
        LoadServices()
        Clearform()
    End Sub

    Private Sub BTNUpdate_Click(sender As Object, e As EventArgs) Handles BTNUpdate.Click
        If selectedServiceID = 0 Then
            MessageBox.Show("Please select a service to update.")
            Exit Sub
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "
            UPDATE Services
            SET ServiceName=@name, Price=@price, Duration=@duration
            WHERE ServiceID=@id
        "

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", selectedServiceID)
                cmd.Parameters.AddWithValue("@name", txtServiceName.Text)
                cmd.Parameters.AddWithValue("@price", Convert.ToDecimal(txtPrice.Text))
                cmd.Parameters.AddWithValue("@duration", Convert.ToInt32(txtDuration.Text))
                cmd.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("Service updated successfully.")
        LogAudit("Updated service ", "Service Management")
        LoadServices()
        Clearform()
    End Sub
    Private Sub Clearform()
        txtServiceName.Text = ""
        txtPrice.Text = ""
        txtDuration.Text = ""
        selectedServiceID = 0
    End Sub
    Private Sub BTNDelete_Click(sender As Object, e As EventArgs) Handles BTNDelete.Click
        If selectedServiceID = 0 Then
            MessageBox.Show("Please select a service to delete.")
            Exit Sub
        End If

        If MessageBox.Show("Are you sure you want to delete this service?",
                       "Confirm", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            ' Check if service is used in appointments
            Using checkCmd As New SqlCommand("SELECT COUNT(*) FROM AppointmentServices WHERE ServiceID=@id", con)
                checkCmd.Parameters.AddWithValue("@id", selectedServiceID)
                Dim count As Integer = CInt(checkCmd.ExecuteScalar())

                If count > 0 Then
                    MessageBox.Show("This service is still used in appointments and cannot be deleted.")
                    Exit Sub
                End If
            End Using

            ' Safe delete
            Using deleteCmd As New SqlCommand("DELETE FROM Services WHERE ServiceID=@id", con)
                deleteCmd.Parameters.AddWithValue("@id", selectedServiceID)
                deleteCmd.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("Service deleted successfully.")
        LogAudit("Deleted service ", "Service Management")
        LoadServices()
        Clearform()
    End Sub


    Private Sub DGVService_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If DGVService.Columns(e.ColumnIndex).Name = "Duration" AndAlso e.Value IsNot Nothing AndAlso Not IsDBNull(e.Value) Then
            Dim mins As Integer
            If Integer.TryParse(e.Value.ToString(), mins) Then
                e.Value = FormatMinutesFriendly(mins) ' returns "30 mins" or "1h 30m"
                e.FormattingApplied = True
            End If
        End If
    End Sub
    Private Function FormatMinutesFriendly(mins As Integer) As String
        If mins <= 0 Then Return ""
        If mins < 60 Then Return $"{mins} mins"
        Dim h = mins \ 60
        Dim m = mins Mod 60
        If m = 0 Then
            Return $"{h}h"
        Else
            Return $"{h}h {m}m"
        End If
    End Function

    Private Sub DGVService_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return
        Dim row = DGVService.Rows(e.RowIndex)
        selectedServiceID = If(IsDBNull(row.Cells("ServiceID").Value), 0, Convert.ToInt32(row.Cells("ServiceID").Value))
        txtServiceName.Text = If(IsDBNull(row.Cells("ServiceName").Value), "", row.Cells("ServiceName").Value.ToString())
        txtPrice.Text = If(IsDBNull(row.Cells("Price").Value), "", row.Cells("Price").Value.ToString())
        txtDuration.Text = If(IsDBNull(row.Cells("Duration").Value), "", row.Cells("Duration").Value.ToString())
    End Sub

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        If Dashboard Is Nothing Then
            Dashboard = New AdminDashboard()
        End If

        Dashboard.Show()
        Me.Hide()
    End Sub

    Private Sub ServiceSearch_TextChanged(sender As Object, e As EventArgs) Handles ServiceSearch.TextChanged
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String

            ' Show all services if search box is empty
            If ServiceSearch.Text.Trim = "" Then
                query = "
                SELECT ServiceID, ServiceName, Price, Duration
                FROM Services
                ORDER BY ServiceName
            "
            Else
                query = "
                SELECT ServiceID, ServiceName, Price, Duration
                FROM Services
                WHERE COALESCE(ServiceName,'') LIKE @search
                   OR COALESCE(CAST(Price AS VARCHAR),'') LIKE @search
                   OR COALESCE(CAST(Duration AS VARCHAR),'') LIKE @search
                ORDER BY ServiceName
            "
            End If

            Using cmd As New SqlCommand(query, con)
                If ServiceSearch.Text.Trim <> "" Then
                    cmd.Parameters.AddWithValue("@search", "%" & ServiceSearch.Text.Trim & "%")
                End If

                Dim adapter As New SqlDataAdapter(cmd)
                Dim table As New DataTable()
                adapter.Fill(table)

                DGVService.DataSource = table
            End Using
        End Using

    End Sub

    Private Sub txtServiceName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtServiceName.KeyPress
        ' Allow control keys (Backspace, Delete, etc.)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        ' Allow letters and spaces only
        If Not (Char.IsLetter(e.KeyChar) OrElse e.KeyChar = " "c) Then
            e.Handled = True ' Block invalid input
        End If
    End Sub

    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrice.KeyPress
        ' Allow control keys (Backspace, Delete, etc.)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        ' Allow digits only
        If Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True ' Block invalid input
        End If
    End Sub

    Private Sub txtDuration_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDuration.KeyPress
        ' Allow control keys (Backspace, Delete, etc.)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        ' Allow digits only
        If Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True ' Block invalid input
        End If
    End Sub

    Private Sub DGVService_CellClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles DGVService.CellClick
        ' Ensure the click is on a valid row (not header)
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DGVService.Rows(e.RowIndex)

            ' Populate your textboxes/combos with values from the grid
            txtServiceName.Text = row.Cells("ServiceName").Value.ToString()
            txtDuration.Text = row.Cells("Duration").Value.ToString()
            txtPrice.Text = row.Cells("Price").Value.ToString()

            ' Example for combo box (if you have one for Category)
            'CmbCategory.Text = row.Cells("Category").Value.ToString()
            ' Or if bound to IDs:
            ' CmbCategory.SelectedValue = row.Cells("CategoryID").Value
        End If

    End Sub
End Class