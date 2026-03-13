Imports System.Data.SqlClient

Public Class AdminDBServices
    Private selectedServiceID As Integer = 0

    Private Sub LoadServices()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim query As String = "SELECT * FROM Services ORDER BY ServiceID"

            Using da As New SqlDataAdapter(query, con)
                Dim dt As New DataTable()
                da.Fill(dt)
                DGVService.DataSource = dt
                DGVService.Columns("ServiceID").Visible = False
            End Using
        End Using
    End Sub
    Private Function IsDuplicateService(name As String, id As Integer) As Boolean
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            ' Checks if the name exists, excluding the current service ID
            Dim query As String = "SELECT COUNT(*) FROM Services WHERE ServiceName = @name AND ServiceID <> @id"
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@name", name.Trim())
                cmd.Parameters.AddWithValue("@id", id)
                con.Open()
                Return CInt(cmd.ExecuteScalar()) > 0
            End Using
        End Using
    End Function
    Private Sub AdminDBServices_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadServices()
        Clearform()
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
        Dim serviceName As String = txtServiceName.Text.Trim()

        ' 1. Basic Validation
        If serviceName = "" Then
            MessageBox.Show("Service name is required.")
            Return
        End If

        ' 2. DUPLICATE CHECK
        If IsDuplicateService(serviceName, 0) Then
            MessageBox.Show("A service with this name already exists.")
            txtServiceName.Focus()
            Return
        End If

        ' 3. Format Validation
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

        ' 4. Database Insertion
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim query As String = "INSERT INTO Services (ServiceName, Price, Duration) VALUES (@name, @price, @duration)"

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = serviceName

                Dim pParam = cmd.Parameters.Add("@price", SqlDbType.Decimal)
                pParam.Precision = 10
                pParam.Scale = 2
                pParam.Value = price

                ' Use the parsed minutes, not the raw text
                cmd.Parameters.Add("@duration", SqlDbType.Int).Value = durationMinutes

                cmd.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("Service added successfully.")
        LogAudit("Added new service: " & serviceName, "Service Management")
        LoadServices()
        Clearform() ' This will reset the UI and buttons
    End Sub

    Private Sub BTNUpdate_Click(sender As Object, e As EventArgs) Handles BTNUpdate.Click
        If selectedServiceID = 0 Then
            MessageBox.Show("Please select a service to update.")
            Exit Sub
        End If

        Dim price As Decimal
        Dim durationMinutes As Integer
        Dim serviceName As String = txtServiceName.Text.Trim()

        ' 1. Basic Validation
        If serviceName = "" Then
            MessageBox.Show("Service name is required.")
            Return
        End If

        ' 2. DUPLICATE CHECK (Specify current ID to ignore self)
        If IsDuplicateService(serviceName, selectedServiceID) Then
            MessageBox.Show("Another service with this name already exists.")
            txtServiceName.Focus()
            Return
        End If

        ' 3. Format Validation (Prevents crashes)
        If Not Decimal.TryParse(txtPrice.Text.Trim(), price) Then
            MessageBox.Show("Please enter a valid price.")
            Return
        End If

        If Not TryParseDuration(txtDuration.Text, durationMinutes) Then
            MessageBox.Show("Please enter a valid duration.")
            Return
        End If

        ' 4. Database Update
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim query As String = "UPDATE Services SET ServiceName=@name, Price=@price, Duration=@duration WHERE ServiceID=@id"

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", selectedServiceID)
                cmd.Parameters.AddWithValue("@name", serviceName)
                cmd.Parameters.AddWithValue("@price", price)
                cmd.Parameters.AddWithValue("@duration", durationMinutes)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("Service updated successfully.")
        LogAudit("Updated service: " & serviceName, "Service Management")
        LoadServices()
        Clearform()
    End Sub

    Private Sub Clearform()
        txtServiceName.Clear()
        txtPrice.Clear()
        txtDuration.Clear()
        ServiceSearch.Clear()
        selectedServiceID = 0

        DGVService.ClearSelection()

        ' Ensure these exact names match your Design view
        BTNAdd.Enabled = True      ' Add should be clickable for a new service
        BTNUpdate.Enabled = False  ' Cannot update nothing
        BTNDelete.Enabled = False  ' Cannot delete nothing

        txtServiceName.Focus()
    End Sub
    Private Sub BTNDelete_Click(sender As Object, e As EventArgs) Handles BTNDelete.Click
        If selectedServiceID = 0 Then
            MessageBox.Show("Please select a service to delete.")
            Exit Sub
        End If

        If MessageBox.Show("Are you sure you want to delete this service?",
                       "Confirm", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
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
    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        If Dashboard Is Nothing Then
            Dashboard = New AdminDashboard()
        End If

        Dashboard.Show()
        Me.Hide()
    End Sub

    Private Sub ServiceSearch_TextChanged(sender As Object, e As EventArgs) Handles ServiceSearch.TextChanged
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
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
        ' 1. Allow Backspace and other control keys
        If Char.IsControl(e.KeyChar) Then Return

        ' 2. If the user types a DOT
        If e.KeyChar = "."c Then
            ' If the textbox already has a dot, block the second one (prevent spam)
            If txtPrice.Text.Contains(".") Then
                e.Handled = True
            End If
            ' If it's the first dot, let it pass
            Return
        End If

        ' 3. If it's not a digit, block it
        If Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
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
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DGVService.Rows(e.RowIndex)

            selectedServiceID = Convert.ToInt32(row.Cells("ServiceID").Value)

            txtServiceName.Text = row.Cells("ServiceName").Value.ToString()
            txtPrice.Text = row.Cells("Price").Value.ToString()
            txtDuration.Text = row.Cells("Duration").Value.ToString()

            ' FIX: Toggle button states for "Edit Mode"
            BTNAdd.Enabled = False
            BTNUpdate.Enabled = True
            BTNDelete.Enabled = True
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Clearform()
    End Sub
End Class