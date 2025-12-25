Imports System.Data.SqlClient

Public Module SystemSession
    ' Current User Session
    Public LoggedInUserID As Integer = 0
    Public LoggedInFullName As String = ""
    Public LoggedInRole As String = ""
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

    'combobox enforcement for admin role
    Public Sub EnforceAdminRole(combo As ComboBox)
        If Not AdminExists() Then
            combo.SelectedItem = "Admin"
            combo.Enabled = False   ' lock ComboBox to Admin
        Else
            combo.Enabled = True
        End If
    End Sub

    ' Self-session enforcement (deletion or demotion)
    Public Sub EnforceSelfSessionRules(selectedUserID As Integer,
                                   newRole As String,
                                   currentForm As Form,
                                   loginForm As Form,
                                   Optional isDelete As Boolean = False)

        If selectedUserID = LoggedInUserID AndAlso isDelete Then
            LoggedInUserID = 0
            LoggedInRole = ""
            MessageBox.Show("Your account has been deleted. You will be logged out.",
                        "Session Ended", MessageBoxButtons.OK, MessageBoxIcon.Information)

            If loginForm IsNot Nothing Then
                loginForm.Show()
            Else
                Dim lf As New Login()
                lf.Show()
            End If
            currentForm.Hide()
            Exit Sub
        ElseIf selectedUserID = LoggedInUserID AndAlso newRole <> "Admin" Then
            LoggedInUserID = 0
            LoggedInRole = ""
            MessageBox.Show("Your role has been changed. You will be logged out.",
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
    Public Sub LogAudit(action As String, moduleName As String,
                        Optional userId As Integer = 0,
                        Optional fullName As String = "",
                        Optional role As String = "Unknown")

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
            cmd.Parameters.AddWithValue("@Action", action)
            cmd.Parameters.AddWithValue("@Module", moduleName)

            cmd.ExecuteNonQuery()
        End Using
    End Sub
End Module