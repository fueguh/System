Imports System.Data.SqlClient

Public Class AdminDBAppointments

    ' ==========================================
    ' FIELDS & PROPERTIES
    ' ==========================================
    Private selectedAppointmentID As Integer = 0
    Private isFormLoading As Boolean = True
    Private selectedEndTime As TimeSpan
    Private selectedPatientID As Integer = 0
    Private dtAppointments As DataTable
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
            dtAppointments = New DataTable() ' Store in the class-level variable
            da.Fill(dtAppointments)
            DGVAppointments.DataSource = dtAppointments
        End Using

        ' Hide internal IDs
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
        Dim conflictMessage As String = IsConflict(0, startTimeValue, selectedEndTime)
        If conflictMessage <> "" Then
            MessageBox.Show(conflictMessage, "Schedule Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
        ' 1. INITIAL VALIDATION
        If selectedAppointmentID = 0 Then
            MessageBox.Show("Please select an appointment from the list first.")
            Exit Sub
        End If

        If Not ValidateFields() Then Exit Sub

        ' 2. PREPARE TIME AND CONFLICT CHECK
        Dim startTimeValue As TimeSpan = DateTime.Parse(cmbStartTime.Text).TimeOfDay

        Dim conflictMessage As String = IsConflict(selectedAppointmentID, startTimeValue, selectedEndTime)
        If conflictMessage <> "" Then
            MessageBox.Show(conflictMessage, "Schedule Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' 3. CAPTURE NAMES FOR AUDIT TRAIL (Before database changes)
        ' We use .Text to get the actual names instead of IDs
        Dim patientName As String = lblPatient.Text
        Dim dentistName As String = CmbDent.Text
        Dim apptDate As String = DtpDate.Value.ToString("MMM dd, yyyy")

        ' 4. DATABASE OPERATIONS
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            ' Start a transaction to ensure both Appointment and Services update together
            Using sqlTrans As SqlTransaction = con.BeginTransaction()
                Try
                    ' A. Update Main Appointment Table
                    Dim query As String = "UPDATE Appointments SET PatientID=@p, UserID=@d, Date=@date, " &
                                    "StartTime=@start, EndTime=@end, Status=@status WHERE AppointmentID=@id"

                    Using cmd As New SqlCommand(query, con, sqlTrans)
                        cmd.Parameters.AddWithValue("@id", selectedAppointmentID)
                        cmd.Parameters.AddWithValue("@p", selectedPatientID)
                        cmd.Parameters.AddWithValue("@d", CInt(CmbDent.SelectedValue))
                        cmd.Parameters.AddWithValue("@date", DtpDate.Value.Date)
                        cmd.Parameters.AddWithValue("@start", startTimeValue)
                        cmd.Parameters.AddWithValue("@end", selectedEndTime)
                        cmd.Parameters.AddWithValue("@status", cmbStatus.Text)
                        cmd.ExecuteNonQuery()
                    End Using

                    ' B. Update Services (Delete old, Insert new)
                    Using cmdDel As New SqlCommand("DELETE FROM AppointmentServices WHERE AppointmentID=@aid", con, sqlTrans)
                        cmdDel.Parameters.AddWithValue("@aid", selectedAppointmentID)
                        cmdDel.ExecuteNonQuery()
                    End Using

                    ' Note: Modified SaveAppointmentServices to accept connection and transaction 
                    ' to prevent "Connection Busy" or "Transaction Required" errors.
                    SaveAppointmentServicesInTransaction(selectedAppointmentID, con, sqlTrans)

                    ' C. COMMIT AND LOG
                    sqlTrans.Commit()

                    ' --- THE IMPROVED AUDIT LOG ---
                    Dim auditMsg As String = $"Updated Appointment #{selectedAppointmentID} | Patient: {patientName} | Dentist: {dentistName} | New Date: {apptDate}"
                    SystemSession.LogAudit(auditMsg, "Appointment Management", SystemSession.LoggedInUserID, SystemSession.LoggedInFullName, SystemSession.LoggedInRole)

                    MessageBox.Show("Appointment updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    RefreshUI()

                Catch ex As Exception
                    sqlTrans.Rollback()
                    MessageBox.Show("Error updating appointment: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

    ' Helper method to ensure services are saved within the same transaction
    Private Sub SaveAppointmentServicesInTransaction(appointmentID As Integer, con As SqlConnection, trans As SqlTransaction)
        For Each item As DataRowView In clbServices.CheckedItems
            Using cmd As New SqlCommand("INSERT INTO AppointmentServices (AppointmentID, ServiceID) VALUES (@aid, @sid)", con, trans)
                cmd.Parameters.AddWithValue("@aid", appointmentID)
                cmd.Parameters.AddWithValue("@sid", item("ServiceID"))
                cmd.ExecuteNonQuery()
            End Using
        Next
    End Sub

    ' ==========================================
    ' VALIDATION & CONFLICT LOGIC
    ' ==========================================
    Private Function ValidateFields() As Boolean
        If isFormLoading Then Return False

        ' 1. Check Required Fields
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

        ' 2. Date Check
        If DtpDate.Value.Date < DateTime.Today Then
            MessageBox.Show("Cannot schedule appointments in the past.")
            Return False
        End If

        ' --- FIX: Declare variables at the function level ---
        Dim shiftEnd As TimeSpan = New TimeSpan(20, 0, 0) ' Default fallback
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
                Dim queryPart = "SELECT EndTime FROM DentistAvailability WHERE DentistID = @id AND DayOfWeek = @day"
                Using cmdPart As New SqlCommand(queryPart, con)
                    cmdPart.Parameters.AddWithValue("@id", dentistID)
                    cmdPart.Parameters.AddWithValue("@day", dayName)

                    Dim resEnd = cmdPart.ExecuteScalar()
                    If resEnd IsNot Nothing AndAlso Not IsDBNull(resEnd) Then
                        shiftEnd = DirectCast(resEnd, TimeSpan)
                    Else
                        ' If part-time but no schedule for this day, they aren't working
                        MessageBox.Show("This dentist has no schedule set for " & dayName)
                        Return False
                    End If
                End Using
            Else
                ' Full-time: Default to clinic closing time
                shiftEnd = New TimeSpan(20, 0, 0)
            End If
        End Using

        ' 3. Final Comparison
        If selectedEndTime > shiftEnd Then
            MessageBox.Show("The services exceed the dentist's shift (Ends at " & DateTime.Today.Add(shiftEnd).ToString("hh:mm tt") & ").")
            Return False
        End If

        Return True
    End Function

    Private Function IsConflict(appointmentID As Integer, startT As TimeSpan, endT As TimeSpan) As String
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            ' Base query to check for time overlaps
            Dim baseQuery As String = "SELECT FullName FROM Appointments A " &
                                  "JOIN {0} T ON A.{1} = T.{1} " &
                                  "WHERE A.Date = @date AND A.AppointmentID <> @id " &
                                  "AND A.Status <> 'Cancelled' " &
                                  "AND (@start < A.EndTime AND @end > A.StartTime) " &
                                  "AND A.{1} = @targetID"

            ' 1. Check Dentist
            Using cmdDentist As New SqlCommand(String.Format(baseQuery, "Users", "UserID"), con)
                cmdDentist.Parameters.AddWithValue("@date", DtpDate.Value.Date)
                cmdDentist.Parameters.AddWithValue("@id", appointmentID)
                cmdDentist.Parameters.AddWithValue("@start", startT)
                cmdDentist.Parameters.AddWithValue("@end", endT)
                cmdDentist.Parameters.AddWithValue("@targetID", CInt(CmbDent.SelectedValue))

                Dim dentistName = cmdDentist.ExecuteScalar()
                If dentistName IsNot Nothing Then Return "Dentist (" & dentistName.ToString() & ") is already booked at this time."
            End Using

            ' 2. Check Patient
            Using cmdPatient As New SqlCommand(String.Format(baseQuery, "Patients", "PatientID"), con)
                cmdPatient.Parameters.AddWithValue("@date", DtpDate.Value.Date)
                cmdPatient.Parameters.AddWithValue("@id", appointmentID)
                cmdPatient.Parameters.AddWithValue("@start", startT)
                cmdPatient.Parameters.AddWithValue("@end", endT)
                cmdPatient.Parameters.AddWithValue("@targetID", selectedPatientID)

                Dim patientName = cmdPatient.ExecuteScalar()
                If patientName IsNot Nothing Then Return "Patient (" & patientName.ToString() & ") already has another appointment at this time."
            End Using

            Return "" ' No conflict
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

    Private Sub txtSearchAppointments_TextChanged(sender As Object, e As EventArgs) Handles txtSearchAppointments.TextChanged
        If dtAppointments Is Nothing Then Exit Sub

        Try
            ' This filters the Patient, Dentist, and Status columns simultaneously
            Dim filter As String = String.Format("Patient LIKE '%{0}%' OR Dentist LIKE '%{0}%' OR Status LIKE '%{0}%'",
                                                 txtSearchAppointments.Text.Replace("'", "''"))

            dtAppointments.DefaultView.RowFilter = filter
        Catch ex As Exception
            ' Silent catch to prevent crashes during rapid typing
        End Try
    End Sub
    Private Sub btnChoosePatient_Click(sender As Object, e As EventArgs) Handles btnChoosePatient.Click
        Using selectionForm As New AdminDBPatientSelection()
            If selectionForm.ShowDialog() = DialogResult.OK Then
                selectedPatientID = selectionForm.SelectedPatientId
                lblPatient.Text = selectionForm.SelectedPatientName

                ' --- ADDED LOGIC TO CLEAR SERVICES ---
                ' Uncheck all services in the list
                For i As Integer = 0 To clbServices.Items.Count - 1
                    clbServices.SetItemChecked(i, False)
                Next

                ' Reset duration and end time labels since no services are selected now
                CalculateTotalDuration()
                ' --------------------------------------
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
        txtSearchAppointments.Clear()
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
        ' FIX 1: Remove isFormLoading check here. 
        ' We want this to run even when loading data from the DataGridView.
        If CmbDent.SelectedValue Is Nothing Then
            cmbStartTime.Items.Clear()
            Exit Sub
        End If

        Dim startLoop As TimeSpan = New TimeSpan(8, 0, 0)
        Dim endLoop As TimeSpan = New TimeSpan(20, 0, 0)
        Dim dentistID As Integer

        ' Handle the SelectedValue safely
        If TypeOf CmbDent.SelectedValue Is DataRowView Then
            dentistID = CInt(CType(CmbDent.SelectedValue, DataRowView)("UserID"))
        Else
            dentistID = CInt(CmbDent.SelectedValue)
        End If

        cmbStartTime.Items.Clear()
        Dim selectedDate = DtpDate.Value.Date
        Dim dayName = selectedDate.ToString("dddd")
        Dim shiftType As String = ""

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            ' FIX 2: Use Parameterized queries to avoid syntax errors
            Using cmdShift As New SqlCommand("SELECT Availability FROM Users WHERE UserID = @uid", con)
                cmdShift.Parameters.AddWithValue("@uid", dentistID)
                shiftType = cmdShift.ExecuteScalar()?.ToString()
            End Using

            If shiftType = "Part-time" Then
                Dim availQuery As String = "SELECT StartTime, EndTime FROM DentistAvailability WHERE DentistID=@uid AND DayOfWeek=@day"
                Using cmdAvail As New SqlCommand(availQuery, con)
                    cmdAvail.Parameters.AddWithValue("@uid", dentistID)
                    cmdAvail.Parameters.AddWithValue("@day", dayName)
                    Using dr = cmdAvail.ExecuteReader()
                        If dr.Read() Then
                            startLoop = DirectCast(dr("StartTime"), TimeSpan)
                            endLoop = DirectCast(dr("EndTime"), TimeSpan)
                        Else
                            Exit Sub ' No availability set for this day
                        End If
                    End Using
                End Using
            End If

            ' FIX 3: Fetch busy slots, EXCLUDING the current appointment ID so it doesn't block itself
            Dim busySlots As New List(Of (S As TimeSpan, E As TimeSpan))
            Dim busyQuery As String = "SELECT StartTime, EndTime FROM Appointments " &
                                  "WHERE UserID=@uid AND Date=@date " &
                                  "AND Status<>'Cancelled' AND AppointmentID<>@aid"

            Using cmdBusy As New SqlCommand(busyQuery, con)
                cmdBusy.Parameters.AddWithValue("@uid", dentistID)
                cmdBusy.Parameters.AddWithValue("@date", selectedDate)
                cmdBusy.Parameters.AddWithValue("@aid", selectedAppointmentID) ' This allows the current time to show up

                Using dr = cmdBusy.ExecuteReader()
                    While dr.Read()
                        busySlots.Add((DirectCast(dr("StartTime"), TimeSpan), DirectCast(dr("EndTime"), TimeSpan)))
                    End While
                End Using
            End Using

            ' Populate the ComboBox
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
                        "(U.Availability = 'Full-time') OR " &
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