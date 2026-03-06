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
        ' 1. Initialize variables
        isFormLoading = True

        ' 2. Setup the UI
        SetupStatusCombo()
        LoadComboBoxes()

        ' 3. FORCE the initial load by temporarily bypassing the flag
        isFormLoading = False
        LoadAvailableDentistsForDay()
        isFormLoading = True ' Turn it back on for the rest of the setup

        LoadAppointments()
        ClearForm() ' This will eventually set isFormLoading to False

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

            ' Patients
            Dim dtPatients As New DataTable()
            Dim daPatients As New SqlDataAdapter("SELECT PatientID, FullName FROM Patients WHERE IsActive = 1 ORDER BY FullName", con)
            daPatients.Fill(dtPatients)
            ' Services
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
    ' CORE ACTIONS (ADD, UPDATE, DELETE)
    ' ==========================================
    Private Sub BTNAdd_Click(sender As Object, e As EventArgs) Handles BTNAdd.Click
        ' Enforce status is set

        If Not ValidateFields() Then Exit Sub
        Dim startTimeValue As TimeSpan = DateTime.Parse(cmbStartTime.Text).TimeOfDay

        If IsConflict(0, startTimeValue, selectedEndTime) Then
            MessageBox.Show("This dentist already has an appointment during this time.")
            Exit Sub
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim query As String = "INSERT INTO Appointments (PatientID, UserID, Date, StartTime, EndTime, Status) 
                                   OUTPUT INSERTED.AppointmentID VALUES (@p, @d, @date, @start, @end, @status)"
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
                MessageBox.Show("Appointment added.")
                SystemSession.LogAudit("Appointment " & cmbStatus.Text, "Appointment Management", SystemSession.LoggedInUserID, SystemSession.LoggedInFullName, SystemSession.LoggedInRole)
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
                Exit Sub
            End Try
        End Using

        RefreshUI()
    End Sub

    Private Sub BTNUpdate_Click(sender As Object, e As EventArgs) Handles BTNUpdate.Click
        If selectedAppointmentID = 0 Then Exit Sub

        ' 1. ValidateFields checks: Nulls, Status, and if EndTime > ShiftEnd
        If Not ValidateFields() Then Exit Sub

        Dim startTimeValue As TimeSpan = DateTime.Parse(cmbStartTime.Text).TimeOfDay

        ' 2. IsConflict checks if OTHER appointments overlap with this new time range
        If IsConflict(selectedAppointmentID, startTimeValue, selectedEndTime) Then
            MessageBox.Show("This dentist has another appointment that overlaps with this time range. Please choose a different slot.")
            Exit Sub
        End If

        ' 3. Proceed with Database Update
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
                ' Clear and Refresh Services
                Using cmdDel As New SqlCommand("DELETE FROM AppointmentServices WHERE AppointmentID=@aid", con)
                    cmdDel.Parameters.AddWithValue("@aid", selectedAppointmentID)
                    cmdDel.ExecuteNonQuery()
                End Using
                SaveAppointmentServices(selectedAppointmentID)

                MessageBox.Show("Appointment updated successfully.")
                SystemSession.LogAudit("Updated Appointment ID: " & selectedAppointmentID, "Appointment Management", SystemSession.LoggedInUserID, SystemSession.LoggedInFullName, SystemSession.LoggedInRole)
            Catch ex As Exception
                MessageBox.Show("Error updating appointment: " & ex.Message)
            End Try
        End Using

        RefreshUI()
    End Sub

    Private Sub BTNDelete_Click(sender As Object, e As EventArgs)
        If selectedAppointmentID = 0 Then Exit Sub
        If MessageBox.Show("Delete this appointment?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Using trans = con.BeginTransaction()
                Try
                    Dim cmd1 As New SqlCommand("DELETE FROM AppointmentServices WHERE AppointmentID=@id", con, trans)
                    cmd1.Parameters.AddWithValue("@id", selectedAppointmentID)
                    cmd1.ExecuteNonQuery()

                    Dim cmd2 As New SqlCommand("DELETE FROM Appointments WHERE AppointmentID=@id", con, trans)
                    cmd2.Parameters.AddWithValue("@id", selectedAppointmentID)
                    cmd2.ExecuteNonQuery()

                    trans.Commit()
                    MessageBox.Show("Deleted Appointment.")
                Catch ex As Exception
                    trans.Rollback()
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End Using
        End Using

        RefreshUI()
    End Sub

    ' ==========================================
    ' VALIDATION & CONFLICT LOGIC
    ' ==========================================
    Private Function ValidateFields() As Boolean
        If isFormLoading Then Return False

        ' 1. Check basic dropdowns
        If selectedPatientID = 0 Or CmbDent.SelectedValue Is Nothing Or cmbStartTime.SelectedIndex = -1 Then
            MessageBox.Show("Please fill all required fields.")
            Return False
        End If

        ' 2. Check Status (The fix is here!)
        If cmbStatus.SelectedIndex = -1 Then
            MessageBox.Show("Please select an appointment status.")
            Return False ' <--- ADD THIS LINE
        End If

        ' 3. Check Services
        If clbServices.CheckedItems.Count = 0 Then
            MessageBox.Show("Select at least one service.")
            Return False
        End If

        ' 4. Clinic Hours Check
        If DtpDate.Value.DayOfWeek = DayOfWeek.Sunday Then
            MessageBox.Show("Closed on Sundays.")
            Return False
        End If

        If selectedEndTime > New TimeSpan(19, 0, 0) Then
            MessageBox.Show("Appointment exceeds clinic hours.")
            Return False
        End If
        ' Get the specific shift end for this dentist on this day
        Dim shiftEnd As TimeSpan = New TimeSpan(19, 30, 0) ' Default clinic max
        Dim dentistID As Integer = CInt(CmbDent.SelectedValue)
        Dim dayName = DtpDate.Value.ToString("dddd")

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            ' 1. Get Shift Type
            Dim shiftType As String = ""
            Using cmdShift As New SqlCommand("SELECT Availability FROM Users WHERE UserID = @id", con)
                cmdShift.Parameters.AddWithValue("@id", dentistID)
                Dim res = cmdShift.ExecuteScalar()
                shiftType = If(res IsNot Nothing, res.ToString(), "")
            End Using

            ' 2. Determine the actual end of their workday
            If shiftType = "Part-time" Then
                Using cmdPart As New SqlCommand("SELECT EndTime FROM DentistAvailability WHERE DentistID = @id AND DayOfWeek = @day", con)
                    cmdPart.Parameters.AddWithValue("@id", dentistID)
                    cmdPart.Parameters.AddWithValue("@day", dayName)
                    Dim resEnd = cmdPart.ExecuteScalar()
                    If resEnd IsNot Nothing Then shiftEnd = DirectCast(resEnd, TimeSpan)
                End Using
            Else
                Select Case shiftType.ToLower()
                    Case "morning shift" : shiftEnd = New TimeSpan(12, 0, 0)
                    Case "afternoon shift", "full-time" : shiftEnd = New TimeSpan(19, 30, 0)
                End Select
            End If
        End Using

        ' FINAL ENFORCEMENT
        If selectedEndTime > shiftEnd Then
            MessageBox.Show("The total duration of services exceeds the dentist's work schedule. " &
                        "The dentist finishes at " & DateTime.Today.Add(shiftEnd).ToString("hh:mm tt") & ".")
            Return False
        End If

        Return True
    End Function

    Private Function IsConflict(appointmentID As Integer, startT As TimeSpan, endT As TimeSpan) As Boolean
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim query As String = "SELECT 1 FROM Appointments WHERE UserID = @dentist AND Date = @date AND AppointmentID <> @id AND Status <> 'Cancelled' AND (@start < EndTime AND @end > StartTime)"
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@dentist", CInt(CmbDent.SelectedValue))
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
            ' 1. Lock the form events
            isFormLoading = True

            Dim row = DGVAppointments.Rows(e.RowIndex)
            selectedAppointmentID = CInt(row.Cells("AppointmentID").Value)
            selectedPatientID = CInt(row.Cells("PatientID").Value)
            Dim dentistIDFromGrid = CInt(row.Cells("DentistID").Value)

            ' 2. Set the Date (This triggers ValueChanged, but isFormLoading is True, so it's safe)
            DtpDate.Value = CDate(row.Cells("Date").Value)

            ' 3. Load the Dentists for that specific day, forcing the current one to stay in the list
            ' Temporarily flip the flag to allow the DB query to run inside the sub
            isFormLoading = False
            LoadAvailableDentistsForDay(dentistIDFromGrid)
            isFormLoading = True

            ' 4. Select the Dentist in the UI
            CmbDent.SelectedValue = dentistIDFromGrid
            lblPatient.Text = row.Cells("Patient").Value.ToString()

            ' 5. NOW generate the full list of time slots
            ' We flip the flag again so PopulateTimeSlots doesn't "Exit Sub"
            isFormLoading = False
            PopulateTimeSlots()
            isFormLoading = True

            ' 6. Handle the specific saved Start Time
            Dim savedStart = TimeSpan.Parse(row.Cells("StartTime").Value.ToString())
            Dim formatted = DateTime.Today.Add(savedStart).ToString("hh:mm tt")

            ' If the saved time isn't in the list (e.g., it's a conflict or weird offset), add it
            If Not cmbStartTime.Items.Contains(formatted) Then
                cmbStartTime.Items.Add(formatted)
            End If

            cmbStartTime.SelectedItem = formatted
            cmbStatus.Text = row.Cells("Status").Value.ToString()

            LoadCheckedServices(selectedAppointmentID)

            BTNAdd.Enabled = False
            BTNUpdate.Enabled = (cmbStatus.Text <> "Cancelled")

        Catch ex As Exception
            MessageBox.Show("Error loading selection: " & ex.Message)
        Finally
            ' 7. Always unlock the form
            isFormLoading = False
        End Try
    End Sub

    Private Sub btnChoosePatient_Click(sender As Object, e As EventArgs) Handles btnChoosePatient.Click
        ' Create an instance of the selection form
        Using selectionForm As New AdminDBPatientSelection()
            ' Show the form and wait for the user to click "Add" (DialogResult.OK)
            If selectionForm.ShowDialog() = DialogResult.OK Then
                ' Get the ID from the property you defined
                selectedPatientID = selectionForm.SelectedPatientId

                ' IMPORTANT: You also need the Name for the label! 
                ' Update this line to pull the name from the selection form's grid
                lblPatient.Text = selectionForm.SelectedPatientName
            End If
        End Using
    End Sub
    Private Sub DtpDate_ValueChanged(sender As Object, e As EventArgs) Handles DtpDate.ValueChanged
        LoadAvailableDentistsForDay()
        PopulateTimeSlots()
    End Sub

    Private Sub CmbDent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbDent.SelectedIndexChanged
        PopulateTimeSlots()
    End Sub

    Private Sub clbServices_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles clbServices.ItemCheck
        Me.BeginInvoke(Sub() CalculateTotalDuration())
    End Sub

    Private Sub cmbStartTime_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStartTime.SelectedIndexChanged
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
        If isFormLoading Then Exit Sub

        ' Lock events to prevent cascading triggers during clear
        isFormLoading = True

        lblPatient.Text = ""

        ' 1. Clear Dentist Selection properly
        CmbDent.DataSource = Nothing ' Optional: if you want it totally empty
        ' OR if keeping the list, do this:
        CmbDent.SelectedIndex = -1
        CmbDent.SelectedValue = DBNull.Value

        cmbStatus.SelectedIndex = -1
        cmbStartTime.Items.Clear()
        cmbStartTime.Text = "" ' Ensure text area is empty

        DtpDate.Value = Date.Today

        ' 2. Uncheck services
        For i As Integer = 0 To clbServices.Items.Count - 1
            clbServices.SetItemChecked(i, False)
        Next

        clbServices.ClearSelected()
        DGVAppointments.ClearSelection()

        ' 3. Reset Variables
        selectedAppointmentID = 0
        selectedPatientID = 0
        selectedEndTime = TimeSpan.Zero
        lblEndTime.Text = "End Time: --:--"
        lblTotalDuration.Text = "Total Duration: 0 mins"

        BTNAdd.Enabled = True
        BTNUpdate.Enabled = False

        ' Unlock events
        isFormLoading = False
    End Sub

    Private Sub PopulateTimeSlots()
        ' If the form is clearing/loading, or nothing is actually selected, STOP.
        If isFormLoading OrElse CmbDent.SelectedIndex = -1 OrElse CmbDent.SelectedValue Is Nothing Then
            cmbStartTime.Items.Clear()
            Exit Sub
        End If

        ' Handle the common DataRowView binding delay in WinForms
        Dim dentistID As Integer
        If TypeOf CmbDent.SelectedValue Is DataRowView Then
            dentistID = CInt(CType(CmbDent.SelectedValue, DataRowView)("UserID"))
        Else
            dentistID = CInt(CmbDent.SelectedValue)
        End If

        cmbStartTime.Items.Clear()
        Dim selectedDate = DtpDate.Value.Date
        Dim dayName = selectedDate.ToString("dddd") ' Matches "Monday", "Tuesday", etc.

        Dim startLoop, endLoop As TimeSpan
        Dim foundSchedule As Boolean = False
        Dim shiftType As String = ""

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            ' 2. Get the general shift type
            Using cmdShift As New SqlCommand("SELECT Availability FROM Users WHERE UserID = @id", con)
                cmdShift.Parameters.AddWithValue("@id", dentistID)
                ' This is the safe, compatible way to handle potential NULL values
                Dim result = cmdShift.ExecuteScalar()
                If result IsNot Nothing AndAlso result IsNot DBNull.Value Then
                    shiftType = result.ToString().Trim()
                Else
                    shiftType = ""
                End If
            End Using

            ' 3. Identify the window of time
            If shiftType.Equals("Part-time", StringComparison.OrdinalIgnoreCase) Then
                ' Pull from your DentistAvailability table
                Dim query = "SELECT StartTime, EndTime FROM DentistAvailability WHERE DentistID = @id AND DayOfWeek = @day"
                Using cmdPart As New SqlCommand(query, con)
                    cmdPart.Parameters.AddWithValue("@id", dentistID)
                    cmdPart.Parameters.AddWithValue("@day", dayName)
                    Using dr = cmdPart.ExecuteReader()
                        If dr.Read() Then
                            startLoop = DirectCast(dr("StartTime"), TimeSpan)
                            endLoop = DirectCast(dr("EndTime"), TimeSpan)
                            foundSchedule = True
                        End If
                    End Using
                End Using
            ElseIf shiftType.Equals("Full-time", StringComparison.OrdinalIgnoreCase) Then
                ' Fixed Shift for Full-time
                startLoop = New TimeSpan(8, 0, 0)
                endLoop = New TimeSpan(19, 0, 0)
                foundSchedule = True
            End If

            ' 4. Exit if no schedule found for this specific day
            If Not foundSchedule Then Exit Sub

            ' 5. Get Booked Time Ranges (Start and End)
            Dim busySlots As New List(Of (StartTime As TimeSpan, EndTime As TimeSpan))
            Dim bQuery = "SELECT StartTime, EndTime FROM Appointments WHERE UserID = @d AND Date = @date AND Status NOT IN ('Cancelled') AND AppointmentID <> @id"

            Using bCmd As New SqlCommand(bQuery, con)
                bCmd.Parameters.AddWithValue("@d", dentistID)
                bCmd.Parameters.AddWithValue("@date", selectedDate)
                bCmd.Parameters.AddWithValue("@id", selectedAppointmentID)
                Using dr = bCmd.ExecuteReader()
                    While dr.Read()
                        busySlots.Add((DirectCast(dr("StartTime"), TimeSpan), DirectCast(dr("EndTime"), TimeSpan)))
                    End While
                End Using
            End Using

            ' 6. Populate ComboBox (30-min intervals)
            Dim current = startLoop
            While current < endLoop
                ' Check if current time is during Lunch
                Dim isLunch = (shiftType.ToLower() = "full-time" AndAlso current >= New TimeSpan(12, 0, 0) AndAlso current < New TimeSpan(13, 0, 0))

                ' Check if current time falls WITHIN any booked appointment range
                Dim isBooked As Boolean = False
                For Each slot In busySlots
                    ' If the current time is between a start and end time, it's taken
                    If current >= slot.StartTime AndAlso current < slot.EndTime Then
                        isBooked = True
                        Exit For
                    End If
                Next

                If Not isLunch AndAlso Not isBooked Then
                    cmbStartTime.Items.Add(DateTime.Today.Add(current).ToString("hh:mm tt"))
                End If
                current = current.Add(TimeSpan.FromMinutes(30))
            End While
        End Using
    End Sub

    ' Add (Optional currentDentistID As Integer = 0) to the signature
    Private Sub LoadAvailableDentistsForDay(Optional currentDentistID As Integer = 0)
        ' We bypass the flag ONLY for this load to ensure the first open works
        ' But we still need to know the day
        Dim dayName = DtpDate.Value.ToString("dddd")

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            ' The query now uses the parameter passed into the SUB, not the Grid directly
            Dim query = "SELECT DISTINCT U.UserID, U.FullName FROM Users U " &
                    "LEFT JOIN DentistAvailability DA ON U.UserID = DA.DentistID " &
                    "WHERE U.Role = 'Dentist' AND (" &
                    "   (U.Availability = 'Full-time' AND @day != 'Sunday') OR " &
                    "   (U.Availability = 'Part-time' AND DA.DayOfWeek = @day) OR " &
                    "   (U.UserID = @currentID)" &
                    ") ORDER BY U.FullName"

            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter(query, con)
            da.SelectCommand.Parameters.AddWithValue("@day", dayName)
            da.SelectCommand.Parameters.AddWithValue("@currentID", currentDentistID)

            da.Fill(dt)

            ' Temporarily stop events so we don't trigger time-slot logic while binding
            RemoveHandler CmbDent.SelectedIndexChanged, AddressOf CmbDent_SelectedIndexChanged
            CmbDent.DataSource = dt
            CmbDent.DisplayMember = "FullName"
            CmbDent.ValueMember = "UserID"
            CmbDent.SelectedIndex = -1
            AddHandler CmbDent.SelectedIndexChanged, AddressOf CmbDent_SelectedIndexChanged
        End Using
        cmbStartTime.Items.Clear()
    End Sub

    Private Sub CalculateTotalDuration()
        ' 1. Always calculate total minutes regardless of Start Time
        Dim totalMinutes As Integer = 0
        For Each item As DataRowView In clbServices.CheckedItems
            totalMinutes += CInt(item("Duration"))
        Next

        ' 2. Update the Duration Label immediately
        lblTotalDuration.Text = "Total Duration: " & totalMinutes & " mins"

        ' 3. Handle End Time calculation ONLY if Start Time is selected
        Dim startTime As DateTime
        If cmbStartTime.SelectedIndex <> -1 AndAlso DateTime.TryParse(cmbStartTime.Text, startTime) Then
            If totalMinutes > 0 Then
                Dim endTime = startTime.AddMinutes(totalMinutes)
                selectedEndTime = endTime.TimeOfDay
                lblEndTime.Text = "End Time: " & endTime.ToString("hh:mm tt")
            Else
                selectedEndTime = TimeSpan.Zero
                lblEndTime.Text = "End Time: --:--"
            End If
        Else
            ' Reset End Time if no start time is picked
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

    ' ==========================================
    ' NAVIGATION
    ' ==========================================
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click, Guna2CirclePictureBox1.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
    End Sub

End Class