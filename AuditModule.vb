Imports System.Data.SqlClient
Module AuditModule
    Public Sub LogAudit(message As string)
            userId As Integer,
            username As String,
            action As String,
            tableName As String,
            recordId As String,
            oldValue As String,
            newValue As String
        )
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            Dim cmd As New SqlCommand("
                INSERT INTO AuditTrail 
                (UserID, Username, Action, TableName, RecordID, OldValue, NewValue)
                VALUES 
                (@UserID, @Username, @Action, @TableName, @RecordID, @OldValue, @NewValue)", con)

            cmd.Parameters.AddWithValue("@UserID", userId)
            cmd.Parameters.AddWithValue("@Username", username)
            cmd.Parameters.AddWithValue("@Action", action)
            cmd.Parameters.AddWithValue("@TableName", tableName)
            cmd.Parameters.AddWithValue("@RecordID", recordId)
            cmd.Parameters.AddWithValue("@OldValue", oldValue)
            cmd.Parameters.AddWithValue("@NewValue", newValue)

            con.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
End Module
