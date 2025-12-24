Imports System.Data.SqlClient

Public Class AdminAuditTrailForm
    Private Sub AdminAuditTrailForm_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        LoadAuditTrail()
    End Sub

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

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If Dashboard Is Nothing Then
            Dashboard = New AdminDashboard()
        End If

        Dashboard.Show()
        Me.Hide()
    End Sub
End Class
