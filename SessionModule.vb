Imports System.Data.SqlClient

Module SessionModule
    ' Tracks the currently logged-in user
    Public LoggedInUserID As Integer = 0
    Public LoggedInFullName As String = ""
    Public LoggedInRole As String = ""

    ' Check if at least one Admin exists
    Public Function AdminExists() As Boolean
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim cmdCheckAdmin As New SqlCommand("SELECT COUNT(*) FROM Users WHERE Role = 'Admin'", con)
            Dim adminCount As Integer = CInt(cmdCheckAdmin.ExecuteScalar())
            Return adminCount > 0
        End Using
    End Function

    ' Enforce Admin registration on a ComboBox
    Public Sub EnforceAdminRole(combo As ComboBox)
        If Not AdminExists() Then
            MessageBox.Show("No Admin accounts exist. You must register an Admin.", "System Setup", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            combo.Text = "Admin"
            combo.Enabled = False   ' lock ComboBox to Admin
        Else
            combo.Enabled = True    ' allow normal selection
        End If
    End Sub

    ' ✅ New helper: Get the current role of a user by ID
    Public Function GetUserRole(userId As Integer) As String
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()
            Dim cmdGetRole As New SqlCommand("SELECT Role FROM Users WHERE UserID=@id", con)
            cmdGetRole.Parameters.AddWithValue("@id", userId)
            Dim roleObj As Object = cmdGetRole.ExecuteScalar()
            If roleObj IsNot Nothing Then
                Return roleObj.ToString()
            Else
                Return String.Empty
            End If
        End Using
    End Function
End Module