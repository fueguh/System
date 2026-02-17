Imports System.Data.SqlClient

Public Class Reports
    Private Sub BtnDailyAppointments_Click(sender As Object, e As EventArgs) Handles BtnDailyAppointments.Click
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "
            SELECT A.AppointmentID,
                   P.FullName AS Patient,
                   U.FullName AS Dentist,
                   A.Service,
                   A.DateCreated,
                   A.Status
            FROM Appointments A
            JOIN Patients P ON A.PatientID = P.PatientID
            JOIN Users U ON A.DentistID = U.UserID
            WHERE CAST(A.DateCreated AS DATE) = CAST(GETDATE() AS DATE)
            ORDER BY A.DateCreated
        "

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVReports.DataSource = dt
        End Using

    End Sub

    Private Sub BtnMonthlyRevenue_Click(sender As Object, e As EventArgs) Handles BtnMonthlyRevenue.Click
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "
            SELECT FORMAT(DateCreated, 'MMMM yyyy') AS Month,
                   SUM(Fee) AS TotalRevenue
            FROM Appointments
            GROUP BY FORMAT(DateCreated, 'MMMM yyyy')
            ORDER BY Month
        "

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVReports.DataSource = dt
        End Using

    End Sub

    Private Sub BtnDentistPerformance_Click(sender As Object, e As EventArgs) Handles BtnDentistPerformance.Click
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "
            SELECT U.FullName AS Dentist,
                   COUNT(A.AppointmentID) AS TotalAppointments
            FROM Appointments A
            JOIN Users U ON A.DentistID = U.UserID
            WHERE A.Status = 'Completed'
            GROUP BY U.FullName
            ORDER BY TotalAppointments DESC
        "

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVReports.DataSource = dt
        End Using

    End Sub

    Private Sub BtnPatientCount_Click(sender As Object, e As EventArgs) Handles BtnPatientCount.Click
        Using con As New SqlConnection(My.Settings.DentalDBConnection)
            con.Open()

            Dim query As String = "SELECT COUNT(PatientID) AS TotalPatients FROM Patients"

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVReports.DataSource = dt
        End Using

    End Sub

    Private Sub Reports_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class