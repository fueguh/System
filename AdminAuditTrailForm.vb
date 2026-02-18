Imports System.Data.SqlClient
Imports System.Data
Imports System.Windows.Forms

Public Class AdminAuditTrailForm

    ' Class-level DataTable and DataView
    Private auditTable As DataTable
    Private auditView As DataView
    ' Form Load
    Private Sub AdminAuditTrailForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAuditTrail()
    End Sub
    ' Load fresh audit trail from DB
    Private Sub LoadAuditTrail()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
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
    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
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
    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        txtSearch.Text = String.Empty
        LoadAuditTrail()
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
        'clean up
    End Sub

    Private Sub Guna2CustomGradientPanel1_Paint(sender As Object, e As PaintEventArgs) Handles Guna2CustomGradientPanel1.Paint

    End Sub
End Class
