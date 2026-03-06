Imports System.Data.SqlClient

Public Class AdminDBPatientSelection

    ' ==========================================
    ' PROPERTIES (Data to pass back)
    ' ==========================================
    Public Property SelectedPatientId As Integer
    Public Property SelectedPatientName As String

    ' ==========================================
    ' FORM EVENTS
    ' ==========================================
    Public Sub New()
        InitializeComponent()
        ' This allows the form to open even if no appointment is passed
    End Sub
    Private Sub AdminDBPatientSelection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPatients()
    End Sub

    ' ==========================================
    ' DATA LOADING
    ' ==========================================
    Private Sub LoadPatients()
        ' 1. Clean query (removed extra spaces/line breaks that can sometimes cause issues)
        Dim query As String = "SELECT PatientID, FullName, ContactNumber FROM Patients WHERE IsActive = 1 ORDER BY FullName"

        Try
            Using con As New SqlConnection(My.Settings.DentalDBConnection2)
                ' 2. Ensure connection is open
                If con.State = ConnectionState.Closed Then con.Open()

                Using da As New SqlDataAdapter(query, con)
                    Dim dt As New DataTable()
                    da.Fill(dt)

                    ' 3. Explicitly reset and rebind
                    DGVPatients.DataSource = Nothing ' Clear previous binding
                    DGVPatients.AutoGenerateColumns = True ' Let the grid build columns from the SQL names
                    DGVPatients.DataSource = dt

                    ' 4. Force UI refresh
                    DGVPatients.Refresh()
                End Using
            End Using

            ' 5. Formatting (Hide ID and clean up look)
            If DGVPatients.Columns.Contains("PatientID") Then
                DGVPatients.Columns("PatientID").Visible = False
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading patients: " & ex.Message)
        End Try
    End Sub

    ' ==========================================
    ' SELECTION LOGIC
    ' ==========================================
    Private Sub BTNAdd_Click(sender As Object, e As EventArgs) Handles BTNAdd.Click
        If DGVPatients.CurrentRow IsNot Nothing Then
            ' Capture the selected patient details
            SelectedPatientId = CInt(DGVPatients.CurrentRow.Cells("PatientID").Value)
            SelectedPatientName = DGVPatients.CurrentRow.Cells("FullName").Value.ToString()

            ' Set result to OK so the calling form knows a selection was made
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            MessageBox.Show("Please select a patient from the list first.")
        End If
    End Sub

    ' Shortcut: Double-clicking a row triggers the Add button logic
    Private Sub DGVPatients_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVPatients.CellDoubleClick
        If e.RowIndex >= 0 Then
            BTNAdd_Click(Nothing, Nothing)
        End If
    End Sub

    ' ==========================================
    ' SEARCH & FILTERING
    ' ==========================================
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim dt As DataTable = TryCast(DGVPatients.DataSource, DataTable)

        If dt IsNot Nothing Then
            ' Use RowFilter for real-time searching without re-querying the database
            dt.DefaultView.RowFilter = String.Format("FullName LIKE '%{0}%'", txtSearch.Text.Replace("'", "''"))
        End If
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        txtSearch.Clear()
        If DGVPatients.Rows.Count > 0 Then
            DGVPatients.ClearSelection()
        End If
    End Sub

End Class