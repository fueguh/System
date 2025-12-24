Imports System.Data.SqlClient
Module AuditModule
    Public Sub LogAudit(
            userId As Integer,
            username As String,
            fullName As String,
            role As String,
            action As String,
            moduleName As String
        )

        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            Dim cmd As New SqlCommand("
                INSERT INTO AuditTrail 
                (UserID, FullName, Role, Action, Module)
                VALUES 
                (@UserID, @FullName, @Role, @Action, @Module)", con)

            cmd.Parameters.AddWithValue("@UserID", userId)
            cmd.Parameters.AddWithValue("@FullName", fullName)
            cmd.Parameters.AddWithValue("@Role", role)
            cmd.Parameters.AddWithValue("@Action", action)
            cmd.Parameters.AddWithValue("@Module", moduleName)

            con.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
End Module
