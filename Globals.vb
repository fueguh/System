Imports System.Data.SqlClient

Public Module SystemSession

    Public Dashboard As AdminDashboard
    Public AdminDBReports As AdminDBReports

    ' Current User Session
    Public LoggedInUserID As Integer = 0
    Public LoggedInFullName As String = ""
    Public LoggedInRole As String = ""
    ' Core audit method
    Public Sub LogAudit(action As String, moduleName As String,
                        Optional userId As Integer = 0,
                        Optional fullName As String = "",
                        Optional role As String = "Unknown")

       Dim message As String = action

        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim cmd As New SqlCommand("
                INSERT INTO AuditTrail
                (UserID, FullName, Role, Action, Module, Timestamp)
                VALUES
                (@UserID, @FullName, @Role, @Action, @Module, GETDATE())", con)

            cmd.Parameters.AddWithValue("@UserID", If(userId = 0, LoggedInUserID, userId))
            cmd.Parameters.AddWithValue("@FullName", If(fullName = "", LoggedInFullName, fullName))
            cmd.Parameters.AddWithValue("@Role", If(role = "Unknown", LoggedInRole, role))
            cmd.Parameters.AddWithValue("@Action", message)
            cmd.Parameters.AddWithValue("@Module", moduleName)

            cmd.ExecuteNonQuery()
        End Using
    End Sub
    ' Convenience wrappers
    Public Sub LogLogin()
        If LoggedInUserID <> 0 AndAlso Not String.IsNullOrEmpty(LoggedInFullName) Then
            LogAudit("Login Success", "Login", LoggedInUserID, LoggedInFullName, LoggedInRole)
        End If
    End Sub
    Public Sub PerformLogout(moduleName As String)
        ' Log the logout event
        LogAudit("Logout Success", moduleName)

        ' Clear the session values
        LoggedInUserID = 0
        LoggedInFullName = ""
        LoggedInRole = ""
    End Sub

    Public Sub NavigateToDashboard(currentForm As Form)
        Select Case LoggedInRole
            Case "Admin" : AdminDashboard.Show()
            Case "Dentist" : DentistDashboard.Show()
            Case "Staff" : StaffDashboard.Show()
            Case Else : Login.Show()
        End Select
        currentForm.Hide()
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

        If selectedUserID = LoggedInUserID Then
            If isDelete Then
                ' Audit deletion only
                LogAudit("Deleted own account", "Users", oldUserID, oldFullName, oldRole)
            ElseIf newRole <> "Admin" Then
                ' Audit role change only
                LogAudit($"Role changed to {newRole}", "Users", oldUserID, oldFullName, oldRole)
            End If

            ' Clear session after logging
            LoggedInUserID = 0
            LoggedInFullName = "Unknown"
            LoggedInRole = "Unknown"

            MessageBox.Show("Your session has ended. You will be logged out.",
                            "Session Ended", MessageBoxButtons.OK, MessageBoxIcon.Information)

            If loginForm IsNot Nothing Then
                loginForm.Show()
            Else
                Dim lf As New Login()
                lf.Show()
            End If

            currentForm.Hide()
            Exit Sub
        End If
    End Sub



    ' Admin Checks
    Public Function AdminExists() As Boolean
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim cmdCheckAdmin As New SqlCommand("SELECT COUNT(*) FROM Users WHERE Role = 'Admin'", con)
            Dim adminCount As Integer = CInt(cmdCheckAdmin.ExecuteScalar())
            Return adminCount > 0
        End Using
    End Function


    ' User Role Helpers
    Public Function GetUserRole(userId As Integer) As String
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim cmdGetRole As New SqlCommand("SELECT Role FROM Users WHERE UserID=@id", con)
            cmdGetRole.Parameters.AddWithValue("@id", userId)
            Dim roleObj As Object = cmdGetRole.ExecuteScalar()
            Return If(roleObj IsNot Nothing, roleObj.ToString(), String.Empty)
        End Using
    End Function

    ' Audit Trail
End Module