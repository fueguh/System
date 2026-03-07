Imports System.Data.SqlClient

Public Class AdminDBAppointments

    ' ==========================================
    ' FIELDS & PROPERTIES
    ' ==========================================
    Private selectedAppointmentID As Integer = 0
    Private isFormLoading As Boolean = True
    Private selectedEndTime As TimeSpan
    Private selectedPatientID As Integer = 0

    Public Shared Dashboard As AdminDashboard
    Public Shared AdminDBReports As AdminDBReports

    ' ==========================================
    ' INITIALIZATION & LOAD
    ' ==========================================
    Private Sub AdminDBAppointments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        isFormLoading = True

        SetupStatusCombo()
        LoadComboBoxes()

        ' Initial data load
        LoadAvailableDentistsForDay()
        LoadAppointments()

        isFormLoading = False
        ClearForm() ' Ensure clean slate on startup

        BTNAdd.Enabled = True
        BTNUpdate.Enabled = False

        If Not (SystemSession.LoggedInRole = "Admin" OrElse SystemSession.LoggedInRole = "Staff") Then
            SystemSession.SetFormReadOnly(Me)
        End If
    End Sub

    Private Sub SetupStatusCombo()
        cmbStatus.Items.Clear()
        cmbStatus.Items.AddRange({"Confirmed", "Ongoing", "Completed", "Cancelled"})
        cmbStatus.SelectedIndex = -1
    End Sub

    ' ==========================================
    ' DATA LOADING (READ)
    ' ==========================================
    Private Sub LoadComboBoxes()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim dtServices As New DataTable()
            Dim daServices As New SqlDataAdapter("SELECT ServiceID, ServiceName, Duration FROM Services ORDER BY ServiceName", con)
            daServices.Fill(dtServices)
            clbServices.DataSource = dtServices
            clbServices.DisplayMember = "ServiceName"
            clbServices.ValueMember = "ServiceID"
        End Using
    End Sub

    Private Sub LoadAppointments()
        Dim query As String = "
            SELECT A.AppointmentID, P.PatientID, P.FullName AS Patient, D.UserID AS DentistID,
                   D.FullName AS Dentist, STRING_AGG(S.ServiceName, ', ') AS Services, 
                   A.Date, A.StartTime, A.EndTime, A.Status
            FROM Appointments A
            JOIN Patients P ON A.PatientID = P.PatientID
            JOIN Users D ON A.UserID = D.UserID AND D.Role = 'Dentist'
            JOIN AppointmentServices ASV ON A.AppointmentID = ASV.AppointmentID
            JOIN Services S ON ASV.ServiceID = S.ServiceID
            GROUP BY A.AppointmentID, P.PatientID, P.FullName, D.UserID, D.FullName, A.Date, A.StartTime, A.EndTime, A.Status
            ORDER BY A.Date DESC"

        Using da As New SqlDataAdapter(query, My.Settings.DentalDBConnection2)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVAppointments.DataSource = dt
        End Using

        For Each colName In {"AppointmentID", "PatientID", "DentistID"}
            If DGVAppointments.Columns.Contains(colName) Then DGVAppointments.Columns(colName).Visible = False
        Next
    End Sub

    ' ==========================================
    ' CORE ACTIONS (ADD, UPDATE)
    ' ==========================================
    Private Sub BTNAdd_Click(sender As Object, e As EventArgs) Handles BTNAdd.Click
        If Not ValidateFields() Then Exit Sub

        Dim startTimeValue As TimeSpan = DateTime.Parse(cmbStartTime.Text).TimeOfDay

        ' Updated logic to reflect that both Dentist and Patient are checked
        If IsConflict(0, startTimeValue, selectedEndTime) Then
            MessageBox.Show("Schedule Conflict: Either the Dentist or the Patient is already booked during this time.", "Conflict Detected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim query As String = "INSERT INTO Appointments (PatientID, UserID, Date, StartTime, EndTime, Status) " &
                               "OUTPUT INSERTED.AppointmentID VALUES (@p, @d, @date, @start, @end, @status)"
            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@p", selectedPatientID)
            cmd.Parameters.AddWithValue("@d", CInt(CmbDent.SelectedValue))
            cmd.Parameters.AddWithValue("@date", DtpDate.Value.Date)
            cmd.Parameters.AddWithValue("@start", startTimeValue)
            cmd.Parameters.AddWithValue("@end", selectedEndTime)
            cmd.Parameters.AddWithValue("@status", cmbStatus.Text)

            Try
                Dim newID As Integer = CInt(cmd.ExecuteScalar())
                SaveAppointmentServices(newID)
                MessageBox.Show("Appointment added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                SystemSession.LogAudit("Added Appointment for Patient ID: " & selectedPatientID, "Appointment Management", SystemSession.LoggedInUserID, SystemSession.LoggedInFullName, SystemSession.LoggedInRole)
                RefreshUI()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub BTNUpdate_Click(sender As Object, e As EventArgs) Handles BTNUpdate.Click
        If selectedAppointmentID = 0 Then Exit Sub
        If Not ValidateFields() Then Exit Sub

        Dim startTimeValue As TimeSpan = DateTime.Parse(cmbStartTime.Text).TimeOfDay

        If IsConflict(selectedAppointmentID, startTimeValue, selectedEndTime) Then
            MessageBox.Show("Overlap detected. Please choose a different slot.")
            Exit Sub
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim query As String = "UPDATE Appointments SET PatientID=@p, UserID=@d, Date=@date, StartTime=@start, EndTime=@end, Status=@status WHERE AppointmentID=@id"
            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@id", selectedAppointmentID)
            cmd.Parameters.AddWithValue("@p", selectedPatientID)
            cmd.Parameters.AddWithValue("@d", CInt(CmbDent.SelectedValue))
            cmd.Parameters.AddWithValue("@date", DtpDate.Value.Date)
            cmd.Parameters.AddWithValue("@start", startTimeValue)
            cmd.Parameters.AddWithValue("@end", selectedEndTime)
            cmd.Parameters.AddWithValue("@status", cmbStatus.Text)

            Try
                cmd.ExecuteNonQuery()
                Using cmdDel As New SqlCommand("DELETE FROM AppointmentServices WHERE AppointmentID=@aid", con)
                    cmdDel.Parameters.AddWithValue("@aid", selectedAppointmentID)
                    cmdDel.ExecuteNonQuery()
                End Using
                SaveAppointmentServices(selectedAppointmentID)

                MessageBox.Show("Appointment updated.")
                SystemSession.LogAudit("Updated Appointment ID: " & selectedAppointmentID, "Appointment Management", SystemSession.LoggedInUserID, SystemSession.LoggedInFullName, SystemSession.LoggedInRole)
                RefreshUI()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using
    End Sub

    ' ==========================================
    ' VALIDATION & CONFLICT LOGIC
    ' ==========================================
    Private Function ValidateFields() As Boolean
        If isFormLoading Then Return False

        If selectedPatientID = 0 OrElse CmbDent.SelectedValue Is Nothing OrElse cmbStartTime.SelectedIndex = -1 Then
            MessageBox.Show("Please fill all required fields (Patient, Dentist, and Time).")
            Return False
        End If

        If cmbStatus.SelectedIndex = -1 Then
            MessageBox.Show("Please select an appointment status.")
            Return False
        End If

        If clbServices.CheckedItems.Count = 0 Then
            MessageBox.Show("Select at least one service.")
            Return False
        End If

        If DtpDate.Value.Date < DateTime.Today Then
            MessageBox.Show("Cannot schedule appointments in the past.")
            Return False
        End If

        If DtpDate.Value.DayOfWeek = DayOfWeek.Sunday Then
            MessageBox.Show("The clinic is closed on Sundays.")
            Return False
        End If

        ' Shift Validation (Partial/Full Logic)
        Dim shiftEnd As TimeSpan = New TimeSpan(19, 30, 0)
        Dim dentistID As Integer = CInt(CmbDent.SelectedValue)
        Dim dayName = DtpDate.Value.ToString("dddd")

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim shiftType As String = ""
            Using cmdShift As New SqlCommand("SELECT Availability FROM Users WHERE UserID = @id", con)
                cmdShift.Parameters.AddWithValue("@id", dentistID)
                shiftType = cmdShift.ExecuteScalar()?.ToString()
            End Using

            If shiftType = "Part-time" Then
                Using cmdPart As New SqlCommand("SELECT EndTime FROM DentistAvailability WHERE DentistID = @id AND DayOfWeek = @day", con)
                    cmdPart.Parameters.AddWithValue("@id", dentistID)
                    cmdPart.Parameters.AddWithValue("@day", dayName)
                    Dim resEnd = cmdPart.ExecuteScalar()
                    If resEnd IsNot Nothing Then shiftEnd = DirectCast(resEnd, TimeSpan)
                End Using
            Else
                Select Case shiftType?.ToLower()
                    Case "morning shift" : shiftEnd = New TimeSpan(12, 0, 0)
                    Case "afternoon shift", "full-time" : shiftEnd = New TimeSpan(19, 30, 0)
                End Select
            End If
        End Using

        If selectedEndTime > shiftEnd Then
            MessageBox.Show("The services exceed the dentist's shift (Ends at " & DateTime.Today.Add(shiftEnd).ToString("hh:mm tt") & ").")
            Return False
        End If

        Return True
    End Function

    Private Function IsConflict(appointmentID As Integer, startT As TimeSpan, endT As TimeSpan) As Boolean
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            ' This query checks for any overlap AND looks for BOTH the Dentist or the Patient
            Dim query As String = "SELECT 1 FROM Appointments " &
                             "WHERE Date = @date " &
                             "AND AppointmentID <> @id " &
                             "AND Status <> 'Cancelled' " &
                             "AND (@start < EndTime AND @end > StartTime) " &
                             "AND (UserID = @dentist OR PatientID = @patient)"

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@dentist", CInt(CmbDent.SelectedValue))
                cmd.Parameters.AddWithValue("@patient", selectedPatientID)
                cmd.Parameters.AddWithValue("@date", DtpDate.Value.Date)
                cmd.Parameters.AddWithValue("@id", appointmentID)
                cmd.Parameters.AddWithValue("@start", startT)
                cmd.Parameters.AddWithValue("@end", endT)

                Return cmd.ExecuteScalar() IsNot Nothing
            End Using
        End Using
    End Function

    ' ==========================================
    ' UI EVENT HANDLERS
    ' ==========================================
    Private Sub DGVAppointments_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVAppointments.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Try
            isFormLoading = True
            Dim row = DGVAppointments.Rows(e.RowIndex)
            selectedAppointmentID = CInt(row.Cells("AppointmentID").Value)
            selectedPatientID = CInt(row.Cells("PatientID").Value)
            Dim dentistIDFromGrid = CInt(row.Cells("DentistID").Value)

            DtpDate.Value = CDate(row.Cells("Date").Value)
            lblPatient.Text = row.Cells("Patient").Value.ToString()

            LoadAvailableDentistsForDay(dentistIDFromGrid)
            CmbDent.SelectedValue = dentistIDFromGrid

            PopulateTimeSlots()

            Dim savedStart = TimeSpan.Parse(row.Cells("StartTime").Value.ToString())
            Dim formatted = DateTime.Today.Add(savedStart).ToString("hh:mm tt")

            If Not cmbStartTime.Items.Contains(formatted) Then cmbStartTime.Items.Add(formatted)
            cmbStartTime.SelectedItem = formatted
            cmbStatus.Text = row.Cells("Status").Value.ToString()

            LoadCheckedServices(selectedAppointmentID)
            CalculateTotalDuration()

            BTNAdd.Enabled = False
            BTNUpdate.Enabled = (cmbStatus.Text <> "Cancelled")
        Catch ex As Exception
            MessageBox.Show("Error loading selection: " & ex.Message)
        Finally
            isFormLoading = False
        End Try
    End Sub

    Private Sub btnChoosePatient_Click(sender As Object, e As EventArgs) Handles btnChoosePatient.Click
        Using selectionForm As New AdminDBPatientSelection()
            If selectionForm.ShowDialog() = DialogResult.OK Then
                selectedPatientID = selectionForm.SelectedPatientId
                lblPatient.Text = selectionForm.SelectedPatientName
            End If
        End Using
    End Sub

    Private Sub DtpDate_ValueChanged(sender As Object, e As EventArgs) Handles DtpDate.ValueChanged
        If isFormLoading Then Exit Sub
        LoadAvailableDentistsForDay()
        PopulateTimeSlots()
    End Sub

    Private Sub CmbDent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbDent.SelectedIndexChanged
        If isFormLoading Then Exit Sub
        PopulateTimeSlots()
    End Sub

    Private Sub clbServices_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles clbServices.ItemCheck
        Me.BeginInvoke(Sub() CalculateTotalDuration())
    End Sub

    Private Sub cmbStartTime_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStartTime.SelectedIndexChanged
        If isFormLoading Then Exit Sub
        CalculateTotalDuration()
    End Sub

    ' ==========================================
    ' REUSABLE HELPERS
    ' ==========================================
    Private Sub RefreshUI()
        LoadAppointments()
        Dashboard?.LoadDashboardStats()
        AdminDBReports?.LoadAppointmentHistory()
        ClearForm()
    End Sub

    Private Sub ClearForm()
        isFormLoading = True
        lblPatient.Text = ""

        ' Reset Dentist ComboBox fully
        CmbDent.DataSource = Nothing
        CmbDent.Items.Clear()
        CmbDent.SelectedIndex = -1
        CmbDent.Text = ""

        cmbStatus.SelectedIndex = -1
        cmbStartTime.Items.Clear()
        cmbStartTime.Text = ""
        DtpDate.Value = Date.Today

        For i As Integer = 0 To clbServices.Items.Count - 1
            clbServices.SetItemChecked(i, False)
        Next

        selectedAppointmentID = 0
        selectedPatientID = 0
        selectedEndTime = TimeSpan.Zero
        lblEndTime.Text = "End Time: --:--"
        lblTotalDuration.Text = "Total Duration: 0 mins"
        BTNAdd.Enabled = True
        BTNUpdate.Enabled = False

        isFormLoading = False
        ' Reload dentists for the reset date (Today)
        LoadAvailableDentistsForDay()
    End Sub

    Private Sub PopulateTimeSlots()
        If isFormLoading OrElse CmbDent.SelectedValue Is Nothing Then
            cmbStartTime.Items.Clear()
            Exit Sub
        End If

        Dim dentistID As Integer
        If TypeOf CmbDent.SelectedValue Is DataRowView Then
            dentistID = CInt(CType(CmbDent.SelectedValue, DataRowView)("UserID"))
        Else
            dentistID = CInt(CmbDent.SelectedValue)
        End If

        cmbStartTime.Items.Clear()
        Dim selectedDate = DtpDate.Value.Date
        Dim dayName = selectedDate.ToString("dddd")
        Dim startLoop, endLoop As TimeSpan
        Dim shiftType As String = ""

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            shiftType = New SqlCommand("SELECT Availability FROM Users WHERE UserID=" & dentistID, con).ExecuteScalar()?.ToString()

            If shiftType = "Part-time" Then
                Using dr = New SqlCommand("SELECT StartTime, EndTime FROM DentistAvailability WHERE DentistID=" & dentistID & " AND DayOfWeek='" & dayName & "'", con).ExecuteReader()
                    If dr.Read() Then
                        startLoop = DirectCast(dr("StartTime"), TimeSpan)
                        endLoop = DirectCast(dr("EndTime"), TimeSpan)
                    Else
                        Exit Sub
                    End If
                End Using
            Else
                startLoop = New TimeSpan(8, 0, 0)
                endLoop = New TimeSpan(19, 0, 0)
            End If

            Dim busySlots As New List(Of (S As TimeSpan, E As TimeSpan))
            Using dr = New SqlCommand("SELECT StartTime, EndTime FROM Appointments WHERE UserID=" & dentistID & " AND Date='" & selectedDate.ToString("yyyy-MM-dd") & "' AND Status<>'Cancelled' AND AppointmentID<>" & selectedAppointmentID, con).ExecuteReader()
                While dr.Read() : busySlots.Add((DirectCast(dr("StartTime"), TimeSpan), DirectCast(dr("EndTime"), TimeSpan))) : End While
            End Using

            Dim current = startLoop
            While current < endLoop
                Dim isLunch = (shiftType = "Full-time" AndAlso current >= New TimeSpan(12, 0, 0) AndAlso current < New TimeSpan(13, 0, 0))
                Dim isBooked = busySlots.Any(Function(s) current >= s.S AndAlso current < s.E)

                If Not isLunch AndAlso Not isBooked Then
                    cmbStartTime.Items.Add(DateTime.Today.Add(current).ToString("hh:mm tt"))
                End If
                current = current.Add(TimeSpan.FromMinutes(30))
            End While
        End Using
    End Sub

    Private Sub LoadAvailableDentistsForDay(Optional currentDentistID As Integer = 0)
        Dim dayName = DtpDate.Value.ToString("dddd")
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim query = "SELECT DISTINCT U.UserID, U.FullName FROM Users U " &
                        "LEFT JOIN DentistAvailability DA ON U.UserID = DA.DentistID " &
                        "WHERE U.Role = 'Dentist' AND (" &
                        "(U.Availability = 'Full-time' AND @day != 'Sunday') OR " &
                        "(U.Availability = 'Part-time' AND DA.DayOfWeek = @day) OR " &
                        "(U.UserID = @currentID)) ORDER BY U.FullName"

            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter(query, con)
            da.SelectCommand.Parameters.AddWithValue("@day", dayName)
            da.SelectCommand.Parameters.AddWithValue("@currentID", currentDentistID)
            da.Fill(dt)

            RemoveHandler CmbDent.SelectedIndexChanged, AddressOf CmbDent_SelectedIndexChanged

            ' Set binding properties BEFORE assigning DataSource
            CmbDent.DisplayMember = "FullName"
            CmbDent.ValueMember = "UserID"
            CmbDent.DataSource = dt
            CmbDent.SelectedIndex = -1

            AddHandler CmbDent.SelectedIndexChanged, AddressOf CmbDent_SelectedIndexChanged
        End Using
    End Sub

    Private Sub CalculateTotalDuration()
        Dim totalMinutes As Integer = 0
        For Each item As DataRowView In clbServices.CheckedItems
            totalMinutes += CInt(item("Duration"))
        Next

        lblTotalDuration.Text = "Total Duration: " & totalMinutes & " mins"

        Dim startTime As DateTime
        If cmbStartTime.SelectedIndex <> -1 AndAlso DateTime.TryParse(cmbStartTime.Text, startTime) Then
            Dim endTime = startTime.AddMinutes(totalMinutes)
            selectedEndTime = endTime.TimeOfDay
            lblEndTime.Text = "End Time: " & endTime.ToString("hh:mm tt")
        Else
            selectedEndTime = TimeSpan.Zero
            lblEndTime.Text = "End Time: --:--"
        End If
    End Sub

    Private Sub SaveAppointmentServices(appointmentID As Integer)
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            For Each item As DataRowView In clbServices.CheckedItems
                Dim cmd As New SqlCommand("INSERT INTO AppointmentServices (AppointmentID, ServiceID) VALUES (@aid, @sid)", con)
                cmd.Parameters.AddWithValue("@aid", appointmentID)
                cmd.Parameters.AddWithValue("@sid", item("ServiceID"))
                cmd.ExecuteNonQuery()
            Next
        End Using
    End Sub

    Private Sub LoadCheckedServices(appointmentID As Integer)
        For i = 0 To clbServices.Items.Count - 1 : clbServices.SetItemChecked(i, False) : Next
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim cmd As New SqlCommand("SELECT ServiceID FROM AppointmentServices WHERE AppointmentID=@aid", con)
            cmd.Parameters.AddWithValue("@aid", appointmentID)
            Using dr = cmd.ExecuteReader()
                While dr.Read()
                    Dim id = CInt(dr("ServiceID"))
                    For i = 0 To clbServices.Items.Count - 1
                        If CInt(CType(clbServices.Items(i), DataRowView)("ServiceID")) = id Then clbServices.SetItemChecked(i, True)
                    Next
                End While
            End Using
        End Using
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click, Guna2CirclePictureBox1.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
    End Sub

End Class