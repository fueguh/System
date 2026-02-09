Imports System.Data.SqlClient

Public Module SystemSession

    Public Dashboard As AdminDashboard
    Public AdminDBReports As AdminDBReports
    ' Current User Session
    Public CurrentSessionToken As String = ""
    Public CurrentDeviceName As String = Environment.MachineName
    Public LoggedInUserID As Integer = 0
    Public LoggedInFullName As String = ""
    Public LoggedInRole As String = ""
    ' Audit Logging function for various actions
    Public Sub LogAudit(action As String, moduleName As String,
                    Optional userId As Integer = 0,
                    Optional fullName As String = "",
                    Optional role As String = "Unknown")

        ' Use provided info, or fallback to session info
        Dim auditUserID As Integer = If(userId <> 0, userId, LoggedInUserID)
        Dim auditFullName As String = If(fullName <> "", fullName, LoggedInFullName)
        Dim auditRole As String = If(role <> "Unknown", role, LoggedInRole)

        ' If still invalid, use SYSTEM fallback
        If auditUserID <= 0 Then
            auditUserID = -1
            auditFullName = "SYSTEM"
            auditRole = "System"
        End If

        Try
            Using con As New SqlConnection(My.Settings.DentalDBConnection)
                con.Open()
                Dim cmd As New SqlCommand("
                INSERT INTO AuditTrail
                (UserID, FullName, Role, Action, Module, Timestamp)
                VALUES
                (@UserID, @FullName, @Role, @Action, @Module, GETDATE())", con)
                cmd.Parameters.AddWithValue("@UserID", auditUserID)
                cmd.Parameters.AddWithValue("@FullName", auditFullName)
                cmd.Parameters.AddWithValue("@Role", auditRole)
                cmd.Parameters.AddWithValue("@Action", action)
                cmd.Parameters.AddWithValue("@Module", moduleName)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            ' If audit logging fails, write to debug console
            Debug.Print($"Failed to log audit: {ex.Message}")
        End Try
    End Sub


    ' Convenience wrappers
    Public Sub LogLogin()
        If LoggedInUserID <> 0 AndAlso Not String.IsNullOrEmpty(LoggedInFullName) Then
            LogAudit("Login Success", "Login", LoggedInUserID, LoggedInFullName, LoggedInRole)
        End If
    End Sub

    Public Sub PerformLogout(moduleName As String)
        ' Capture user info before clearing session
        Dim userId As Integer = LoggedInUserID
        Dim fullName As String = LoggedInFullName
        Dim role As String = LoggedInRole

        Try
            ' 1️⃣ End DB session for this device
            Using con As New SqlConnection(My.Settings.DentalDBConnection)
                con.Open()
                Dim cmd As New SqlCommand("
                UPDATE UserSessions
                SET IsActive = 0
                WHERE DeviceName = @deviceName AND IsActive = 1", con)
                cmd.Parameters.AddWithValue("@deviceName", CurrentDeviceName)
                cmd.ExecuteNonQuery()
            End Using

            ' 2️⃣ Log the logout event
            LogAudit("Logout Success", moduleName, userId, fullName, role)

        Catch ex As Exception
            MessageBox.Show("Error during logout: " & ex.Message)
        Finally
            ' 3️⃣ Clear in-memory session
            LoggedInUserID = 0
            LoggedInFullName = ""
            LoggedInRole = ""
            CurrentSessionToken = ""

            ' 4️⃣ Show login form
            Login.Show()
        End Try
    End Sub

    ' Navigation to dashboards based on role
    Public Sub NavigateToDashboard(currentForm As Form)
        Select Case LoggedInRole
            Case "Admin" : AdminDashboard.Show()
            Case "Dentist" : DentistDashboard.Show()
            Case "Staff" : StaffDashboard.Show()
        End Select
        currentForm.Close()
    End Sub
    ' Admin Guards
    Public Function RequireAdmin(actionName As String) As Boolean
        If LoggedInUserID = 0 OrElse LoggedInRole <> "Admin" Then
            MessageBox.Show($"Only an Admin can {actionName}. Please log in as an Admin.",
                            "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Return True
    End Function
    Public Function RequireSelectedUser(selectedUserID As Integer, actionName As String) As Boolean
        If selectedUserID = 0 Then
            MessageBox.Show($"Please select a user to {actionName}.")
            Return False
        End If
        Return True
    End Function
    Public Sub ShowSuccess(actionName As String)
        MessageBox.Show($"User {actionName} successfully.")
    End Sub
    ' Self-session enforcement (deletion or demotion)
    Public Sub EnforceSelfSessionRules(selectedUserID As Integer,
                                   newRole As String,
                                   currentForm As Form,
                                   loginForm As Form,
                                   Optional isDelete As Boolean = False)
        ' Cache the current session BEFORE making changes
        Dim oldUserID As Integer = LoggedInUserID
        Dim oldFullName As String = LoggedInFullName
        Dim oldRole As String = LoggedInRole

        ' Only enforce rules if the affected user is the logged-in user
        If selectedUserID = oldUserID AndAlso oldUserID > 0 Then

            ' Audit the action BEFORE clearing session
            If isDelete Then
                LogAudit("Deleted own account", "Users", oldUserID, oldFullName, oldRole)
            ElseIf newRole <> "Admin" Then
                LogAudit($"Role changed to {newRole}", "Users", oldUserID, oldFullName, oldRole)
            End If

            ' Perform logout BEFORE clearing session
            SystemSession.PerformLogout(currentForm.Name)

            ' Now clear session safely
            LoggedInUserID = 0
            LoggedInFullName = "Unknown"
            LoggedInRole = "Unknown"

            ' Close current form and show message
            currentForm.Close()
            MessageBox.Show("Your session has ended. You have been logged out.",
                        "Session Ended", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'log audit for forced logout
            LogAudit("Forced Logout after user account change", "Users", oldUserID, oldFullName, oldRole)
        End If
    End Sub

    ' This is for checking if at least one admin exists in the database when deleting or demoting an admin user
    Public Function AdminExists() As Boolean
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim cmdCheckAdmin As New SqlCommand("SELECT COUNT(*) FROM Users WHERE Role = 'Admin'", con)
            Dim adminCount As Integer = CInt(cmdCheckAdmin.ExecuteScalar())
            Return adminCount > 0
        End Using
    End Function
    ' This is for getting the user role based on user id
    Public Function GetUserRole(userId As Integer) As String
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim cmdGetRole As New SqlCommand("SELECT Role FROM Users WHERE UserID=@id", con)
            cmdGetRole.Parameters.AddWithValue("@id", userId)
            Dim roleObj As Object = cmdGetRole.ExecuteScalar()
            Return If(roleObj IsNot Nothing, roleObj.ToString(), String.Empty)
        End Using
    End Function
    ' Set all controls in a form to read-only and disable buttons except "Back" and "Logout"
    Public Sub SetFormReadOnly(frm As Form)
        LockControls(frm.Controls)
    End Sub
    ' For recursive locking of controls for users with view-only access
    Private Sub LockControls(ctrls As Control.ControlCollection)
        For Each ctrl As Control In ctrls
            ' TextBoxes
            If TypeOf ctrl Is TextBox OrElse TypeOf ctrl Is Guna.UI2.WinForms.Guna2TextBox Then
                Try
                    DirectCast(ctrl, TextBox).ReadOnly = True
                Catch
                    DirectCast(ctrl, Guna.UI2.WinForms.Guna2TextBox).ReadOnly = True
                End Try
                ' ComboBoxes and DateTimePickers
            ElseIf TypeOf ctrl Is ComboBox OrElse TypeOf ctrl Is Guna.UI2.WinForms.Guna2ComboBox _
                OrElse TypeOf ctrl Is DateTimePicker OrElse TypeOf ctrl Is Guna.UI2.WinForms.Guna2DateTimePicker Then
                ctrl.Enabled = False
                ' DataGridViews
            ElseIf TypeOf ctrl Is DataGridView OrElse TypeOf ctrl Is Guna.UI2.WinForms.Guna2DataGridView Then
                Dim dgv As DataGridView
                If TypeOf ctrl Is DataGridView Then
                    dgv = DirectCast(ctrl, DataGridView)
                Else
                    dgv = DirectCast(ctrl, Guna.UI2.WinForms.Guna2DataGridView)
                End If
                dgv.ReadOnly = True
                dgv.AllowUserToAddRows = False
                dgv.AllowUserToDeleteRows = False
                ' Buttons
            ElseIf TypeOf ctrl Is Button OrElse TypeOf ctrl Is Guna.UI2.WinForms.Guna2Button Then
                If Not ctrl.Name.Contains("Back") AndAlso Not ctrl.Name.Contains("Logout") Then
                    ctrl.Enabled = False
                End If
            End If
            ' Recurse into child controls (panels, groupboxes, tabpages, etc.)
            If ctrl.HasChildren Then
                LockControls(ctrl.Controls)
            End If
        Next
    End Sub
End Module