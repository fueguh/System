Imports System.Data.SqlClient
Imports System.Data
Imports System.Windows.Forms

Public Class AdminAuditTrailForm

    ' Class-level DataTable and DataView
    Private auditTable As DataTable
    Private auditView As DataView
    Private autoRefreshTimer As Timer

    ' ----------------------------
    ' Form Load
    ' ----------------------------
    Private Sub AdminAuditTrailForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAuditTrail()
        SetupAutoRefresh()
    End Sub

    ' ----------------------------
    ' Load fresh audit trail from DB
    ' ----------------------------
    Private Sub LoadAuditTrail()
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "
                SELECT UserID, FullName, Role, Action, Module, Timestamp
                FROM AuditTrail
                ORDER BY Timestamp DESC
            "

            auditTable = New DataTable()
            Using da As New SqlDataAdapter(query, con)
                da.Fill(auditTable)
            End Using

            ' Bind DataView to grid
            auditView = New DataView(auditTable)
            dgvAuditLogs.DataSource = auditView
        End Using
    End Sub

    ' ----------------------------
    ' Live search filter
    ' ----------------------------
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If auditView Is Nothing Then Return

        If String.IsNullOrWhiteSpace(txtSearch.Text) Then
            auditView.RowFilter = ""
        Else
            ' Filter by FullName, Role, Action, Module
            auditView.RowFilter =
                $"Convert(FullName, 'System.String') LIKE '%{txtSearch.Text}%' " &
                $"OR Convert(Role, 'System.String') LIKE '%{txtSearch.Text}%' " &
                $"OR Convert(Action, 'System.String') LIKE '%{txtSearch.Text}%' " &
                $"OR Convert(Module, 'System.String') LIKE '%{txtSearch.Text}%'"
        End If
    End Sub

    ' ----------------------------
    ' Clear search and reload data
    ' ----------------------------
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        txtSearch.Text = String.Empty
        LoadAuditTrail()
    End Sub

    Private Sub SetupAutoRefresh()
        autoRefreshTimer = New Timer()
        autoRefreshTimer.Interval = 10000 ' 10 seconds
        AddHandler autoRefreshTimer.Tick, Sub()
                                              LoadAuditTrail()
                                          End Sub
        autoRefreshTimer.Start()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub
End Class
