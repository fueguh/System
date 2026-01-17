Imports System.Data.SqlClient

Module Startup
    Sub Main()
        ' Determine device name
        Dim deviceName As String = Environment.MachineName

        ' Try to find an active session for this device
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim cmd As New SqlCommand("
                SELECT us.SessionToken, u.UserID, u.FullName, u.Role
                FROM UserSessions us
                INNER JOIN Users u ON us.UserID = u.UserID
                WHERE us.DeviceName = @deviceName
                  AND us.IsActive = 1", con)

            cmd.Parameters.AddWithValue("@deviceName", deviceName)

            Using dr As SqlDataReader = cmd.ExecuteReader()
                If dr.Read() Then
                    ' Restore session
                    SystemSession.LoggedInUserID = CInt(dr("UserID"))
                    SystemSession.LoggedInFullName = dr("FullName").ToString()
                    SystemSession.LoggedInRole = dr("Role").ToString()
                    SystemSession.CurrentSessionToken = dr("SessionToken").ToString()

                    ' Navigate to dashboard
                    Select Case SystemSession.LoggedInRole
                        Case "Admin"
                            Dim dash As New AdminDashboard()
                            dash.Show()
                        Case "Dentist"
                            DentistDashboard.Show()
                        Case "Staff"
                            StaffDashboard.Show()
                        Case Else
                            Dim login As New Login()
                            login.Show()
                    End Select
                    Exit Sub ' ✅ Prevent login form from opening
                Else
                    Dim login As New Login()
                    login.Show()
                End If
            End Using
        End Using

        ' Start application message loop
        Application.Run()
    End Sub
End Module
