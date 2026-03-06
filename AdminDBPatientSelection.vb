Public Class AdminDBPatientSelection
    Public Property SelectedPatientId As Integer
    Private Sub BTNAdd_Click(sender As Object, e As EventArgs) Handles BTNAdd.Click
        ' Example: get ID from selected row
        SelectedPatientId = CInt(DGVPatients.CurrentRow.Cells("PatientID").Value)
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

End Class