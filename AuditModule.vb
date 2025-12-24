Imports System.Data.SqlClient

Module AuditModule

    Public Sub LogAudit(
        userId As Integer,
        fullName As String,
        role As String,
        action As String,
        moduleName As String)

        ' =========================
        ' SAFETY CHECK (CRITICAL)
        ' =========================
        If userId = 0 OrElse String.IsNullOrWhiteSpace(fullName) Then
            Exit Sub   ' prevents DBNull + missing parameter crashes
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim cmd As New SqlCommand("
                INSERT INTO AuditTrail
                (UserID, FullName, Role, Action, Module, Timestamp)
                VALUES
                (@UserID, @FullName, @Role, @Action, @Module, GETDATE())", con)

            cmd.Parameters.AddWithValue("@UserID", userId)
            cmd.Parameters.AddWithValue("@FullName", fullName)
            cmd.Parameters.AddWithValue("@Role", role)
            cmd.Parameters.AddWithValue("@Action", action)
            cmd.Parameters.AddWithValue("@Module", moduleName)

            cmd.ExecuteNonQuery()
        End Using

    End Sub

End Module
