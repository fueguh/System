Imports System.Data.SqlClient

Module SystemSession
    ' =========================
    ' Current User Session
    ' =========================
    Public LoggedInUserID As Integer = 0
    Public LoggedInFullName As String = ""
    Public LoggedInRole As String = ""

    ' =========================
    ' Admin Checks
    ' =========================
    Public Function AdminExists() As Boolean
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim cmdCheckAdmin As New SqlCommand("SELECT COUNT(*) FROM Users WHERE Role = 'Admin'", con)
            Dim adminCount As Integer = CInt(cmdCheckAdmin.ExecuteScalar())
            Return adminCount > 0
        End Using
    End Function

    Public Sub EnforceAdminRole(combo As ComboBox)
        If Not AdminExists() Then
            MessageBox.Show("No Admin accounts exist. You must register an Admin.",
                            "System Setup", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            combo.Text = "Admin"
            combo.Enabled = False   ' lock ComboBox to Admin
        Else
            combo.Enabled = True
        End If
    End Sub

    ' =========================
    ' User Role Helpers
    ' =========================
    Public Function GetUserRole(userId As Integer) As String
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim cmdGetRole As New SqlCommand("SELECT Role FROM Users WHERE UserID=@id", con)
            cmdGetRole.Parameters.AddWithValue("@id", userId)
            Dim roleObj As Object = cmdGetRole.ExecuteScalar()
            Return If(roleObj IsNot Nothing, roleObj.ToString(), String.Empty)
        End Using
    End Function

    ' =========================
    ' Audit Trail
    ' Extended overload with optional parameters
    ' =========================
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