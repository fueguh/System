Imports System.Data.SqlClient

Public Class AuditTrailForm

    ' Form Load Event
    Private Sub AuditTrailForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAuditTrail()
    End Sub

    ' Load audit trail data from database
    Private Sub LoadAuditTrail()
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "
                SELECT userID, FullName, Role, Action, Module, Timestamp
                FROM AuditTrail
            "

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            dgvAuditLogs.DataSource = dt
        End Using
    End Sub



End Class
