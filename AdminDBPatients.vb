Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class AdminDBPatients
    Private selectedPatientID As Integer = 0
    Dim connectionString As String = My.Settings.DentalDBConnection2

    Private Sub AdminDBPatients_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPatients()
        Clearform()

        ' Only Admin/Staff access, dentists have read-only access
        If Not (SystemSession.LoggedInRole = "Admin" OrElse SystemSession.LoggedInRole = "Staff") Then
            SystemSession.SetFormReadOnly(Me)
        End If
    End Sub
    Private Sub LoadPatients()
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim query As String = "
            SELECT PatientID, FullName, BirthDate, ContactNumber, Email, Address,DateRegistered, NoteAllergy
            FROM Patients
            WHERE IsActive = 1
            ORDER BY PatientID
        "

            Dim da As New SqlDataAdapter(query, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DGVPatients.DataSource = dt
            DGVPatients.Columns("PatientID").Visible = False
        End Using
    End Sub

    Private Sub Clearform()
        selectedPatientID = 0
        txtFullName.Clear()
        DtpBirthDate.Value = DateTime.Today          ' ← Updated for DateTimePicker
        txtContact.Clear()
        txtEmail.Clear()
        txtAddress.Clear()
        txtAllergy.Clear()
        Guna2TextBox1.Clear() ' This is your search box
        DGVPatients.ClearSelection()
        ' Reset Button States
        BTNAdd.Enabled = True
        BTNUpdate.Enabled = False
        BTNDelete.Enabled = False

        txtFullName.Focus()
    End Sub

    Private Sub BTNAdd_Click(sender As Object, e As EventArgs) Handles BTNAdd.Click
        If txtFullName.Text.Trim = "" Or txtContact.Text.Trim = "" Then
            MessageBox.Show("Full name and contact number are required.")
            Exit Sub
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim query As String = "
            INSERT INTO Patients (FullName, BirthDate, ContactNumber, Email, Address, NoteAllergy)
            VALUES (@name, @birth, @contact, @email, @address, @allergy)
        "
            If Not ValidatePatientFields() Then Exit Sub

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@name", txtFullName.Text)
                cmd.Parameters.AddWithValue("@birth", DtpBirthDate.Value)   ' ← Updated (no ParseExact needed)
                cmd.Parameters.AddWithValue("@contact", txtContact.Text)
                If txtEmail.Text.Trim = "" Then
                    cmd.Parameters.AddWithValue("@email", DBNull.Value)
                Else
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text)
                End If
                cmd.Parameters.AddWithValue("@address", txtAddress.Text)
                ' New allergy note parameter (optional)
                If txtAllergy.Text.Trim = "" Then
                    cmd.Parameters.AddWithValue("@allergy", DBNull.Value)
                Else
                    cmd.Parameters.AddWithValue("@allergy", txtAllergy.Text)
                End If

                cmd.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("Patient added successfully.")
        SystemSession.LogAudit("Patient Added", "Patient Maintenance",
                       SystemSession.LoggedInUserID,
                       SystemSession.LoggedInFullName,
                       SystemSession.LoggedInRole)

        LoadPatients()
        Clearform()
        'to reload the system overview in admin dashboard after input
        Dashboard?.LoadDashboardStats()
    End Sub

    Private Sub BTNUpdate_Click(sender As Object, e As EventArgs) Handles BTNUpdate.Click
        If selectedPatientID = 0 Then
            MessageBox.Show("Please select a patient to update.")
            Exit Sub
        End If

        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()

            Dim query As String = "
            UPDATE Patients
            SET FullName=@name, BirthDate=@birth, ContactNumber=@contact,
                Email=@email, Address=@address, NoteAllergy=@allergy
            WHERE PatientID=@id
        "
            If Not ValidatePatientFields(selectedPatientID) Then Exit Sub

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", selectedPatientID)
                cmd.Parameters.AddWithValue("@name", txtFullName.Text)
                cmd.Parameters.AddWithValue("@birth", DtpBirthDate.Value)   ' ← Updated
                cmd.Parameters.AddWithValue("@contact", txtContact.Text)
                If txtEmail.Text.Trim = "" Then
                    cmd.Parameters.AddWithValue("@email", DBNull.Value)
                Else
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text)
                End If
                cmd.Parameters.AddWithValue("@address", txtAddress.Text)
                ' New allergy note parameter (optional)
                If txtAllergy.Text.Trim = "" Then
                    cmd.Parameters.AddWithValue("@allergy", DBNull.Value)
                Else
                    cmd.Parameters.AddWithValue("@allergy", txtAllergy.Text)
                End If

                cmd.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("Patient updated successfully.")
        SystemSession.LogAudit("Patient Updated", "Patient Maintenance",
                       SystemSession.LoggedInUserID,
                       SystemSession.LoggedInFullName,
                       SystemSession.LoggedInRole)
        LoadPatients()
        Clearform()

        'to reload the system overview in admin dashboard after input
        Dashboard?.LoadDashboardStats()
    End Sub

    Private Sub BTNDelete_Click(sender As Object, e As EventArgs) Handles BTNDelete.Click
        If selectedPatientID = 0 Then Return

        If MessageBox.Show("Are you sure you want to deactivate this patient record?",
                       "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then

            DeactivatePatient(selectedPatientID) ' Call the helper sub you already wrote

            MessageBox.Show("Patient record deactivated.")

            ' Logging and Refreshing
            SystemSession.LogAudit("Patient Deleted", "Patient Maintenance", SystemSession.LoggedInUserID, SystemSession.LoggedInFullName, SystemSession.LoggedInRole)
            LoadPatients()
            Clearform()
            Dashboard?.LoadDashboardStats()
        End If
    End Sub

    Private Sub DeactivatePatient(patientId As Integer)
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim query As String = "UPDATE Patients SET IsActive = 0 WHERE PatientID = @PatientID"
            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@PatientID", patientId)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub

    Private Sub DGVPatients_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVPatients.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Dim row As DataGridViewRow = DGVPatients.Rows(e.RowIndex)
        selectedPatientID = Convert.ToInt32(row.Cells("PatientID").Value)

        txtFullName.Text = row.Cells("FullName").Value.ToString()
        Dim bDate As Date = Convert.ToDateTime(row.Cells("BirthDate").Value)
        DtpBirthDate.Value = bDate                     ' ← Updated (no more manual formatting)
        txtContact.Text = row.Cells("ContactNumber").Value.ToString()
        txtEmail.Text = row.Cells("Email").Value.ToString()
        txtAddress.Text = row.Cells("Address").Value.ToString()
        txtAllergy.Text = If(row.Cells("NoteAllergy").Value Is DBNull.Value, "", row.Cells("NoteAllergy").Value.ToString())

        ' Switch UI to Update Mode
        BTNAdd.Enabled = False
        BTNUpdate.Enabled = True
        BTNDelete.Enabled = True
    End Sub

    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Guna2TextBox1.TextChanged
        ' Use your existing query but add the IsActive filter so deactivated patients don't reappear
        Dim query As String = "SELECT PatientID, FullName, BirthDate, ContactNumber, Email, Address, DateRegistered, NoteAllergy " &
                         "FROM Patients " &
                         "WHERE IsActive = 1 AND (FullName LIKE @search OR ContactNumber LIKE @search OR Email LIKE @search)"

        Using con As New SqlConnection(connectionString),
          cmd As New SqlCommand(query, con)

            cmd.Parameters.AddWithValue("@search", "%" & Guna2TextBox1.Text & "%")
            Dim adapter As New SqlDataAdapter(cmd)
            Dim table As New DataTable()
            adapter.Fill(table)

            DGVPatients.DataSource = table

            ' Re-hide the ID column after search refresh
            If DGVPatients.Columns.Contains("PatientID") Then DGVPatients.Columns("PatientID").Visible = False
        End Using
    End Sub
    Private Function ValidatePatientFields(Optional patientID As Integer = 0) As Boolean
        ' 1. Full Name: Basic Character Validation
        Dim fullName As String = txtFullName.Text.Trim()
        If String.IsNullOrWhiteSpace(fullName) OrElse
       Not fullName.All(Function(c) Char.IsLetter(c) OrElse c = " "c OrElse c = "-"c OrElse c = "'"c OrElse c = "."c) Then
            MessageBox.Show("Full Name must contain letters, spaces, '-', ''' or '.' only.")
            txtFullName.Focus()
            Return False
        End If

        ' 2. Duplicate Name Check (Warning only)
        Using con As New SqlConnection(My.Settings.DentalDBConnection2)
            con.Open()
            Dim nameCheckQuery As String = "SELECT COUNT(*) FROM Patients WHERE FullName = @name AND IsActive = 1 AND PatientID <> @id"
            Using cmdName As New SqlCommand(nameCheckQuery, con)
                cmdName.Parameters.AddWithValue("@name", fullName)
                cmdName.Parameters.AddWithValue("@id", patientID)

                Dim nameCount As Integer = CInt(cmdName.ExecuteScalar())
                If nameCount > 0 Then
                    Dim result As DialogResult = MessageBox.Show("A patient named '" & fullName & "' is already registered. Is this a different person?",
                "Potential Duplicate Name", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                    If result = DialogResult.No Then
                        txtFullName.Focus()
                        Return False
                    End If
                End If
            End Using

            ' === 3. Email: NOW OPTIONAL ===
            Dim email As String = txtEmail.Text.Trim()
            If Not String.IsNullOrWhiteSpace(email) Then
                Dim emailPattern As String = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"

                If Not System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern) Then
                    MessageBox.Show("Please enter a valid email address (or leave it blank).")
                    txtEmail.Focus()
                    Return False
                End If

                ' Duplicate email check (only if email was entered)
                Dim emailCheckQuery As String = "
    SELECT COUNT(*) 
    FROM Patients 
    WHERE Email = @em 
    AND IsActive = 1
    AND PatientID <> @id"
                Using cmdEmail As New SqlCommand(emailCheckQuery, con)
                    cmdEmail.Parameters.AddWithValue("@em", email)
                    cmdEmail.Parameters.AddWithValue("@id", patientID)
                    If CInt(cmdEmail.ExecuteScalar()) > 0 Then
                        MessageBox.Show("This email is already registered to another patient.")
                        txtEmail.Focus()
                        Return False
                    End If
                End Using
            End If

            ' 4. Birth Date Validation (using DateTimePicker from previous update)
            Dim birthDate As Date = DtpBirthDate.Value
            If birthDate > DateTime.Now Then
                MessageBox.Show("Birth date cannot be in the future.")
                DtpBirthDate.Focus()
                Return False
            End If

            ' 5. Address Validation
            If String.IsNullOrWhiteSpace(txtAddress.Text) OrElse
           Not txtAddress.Text.All(Function(c) Char.IsLetterOrDigit(c) OrElse
           " -@.,/".Contains(c)) Then
                MessageBox.Show("Address contains invalid characters.")
                txtAddress.Focus()
                Return False
            End If

            ' 6. Allergy Note (Optional)
            If Not String.IsNullOrWhiteSpace(txtAllergy.Text) Then
                If Not txtAllergy.Text.All(Function(c) Char.IsLetter(c) OrElse c = " "c) Then
                    MessageBox.Show("Allergy note must contain letters only.")
                    txtAllergy.Focus()
                    Return False
                End If
            End If

            ' 7. Contact Number Validation
            If String.IsNullOrWhiteSpace(txtContact.Text) OrElse Not txtContact.Text.All(Function(c) Char.IsDigit(c)) Then
                MessageBox.Show("Contact Number must contain digits only.")
                txtContact.Focus()
                Return False
            End If

        End Using

        Return True
    End Function
    Private Sub txtFullName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFullName.KeyPress
        ' Allow control keys (Backspace, Delete, etc.)
        If Char.IsControl(e.KeyChar) Then Return

        ' Allow letters, spaces, hyphens, apostrophes, and dots
        If Not (Char.IsLetter(e.KeyChar) OrElse e.KeyChar = " "c OrElse e.KeyChar = "-"c OrElse e.KeyChar = "'"c OrElse e.KeyChar = "."c) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtEmail_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmail.KeyPress
        ' Allow control keys like Backspace
        If Char.IsControl(e.KeyChar) Then Return

        ' Allow letters, digits, @ and .
        If Not (Char.IsLetterOrDigit(e.KeyChar) OrElse e.KeyChar = "@"c OrElse e.KeyChar = "."c) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAddress_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAddress.KeyPress
        ' Allow control keys (Backspace, Delete)
        If Char.IsControl(e.KeyChar) Then Return

        ' Allow letters, digits, spaces, and common address txtBirthDate
        If Not (Char.IsLetterOrDigit(e.KeyChar) OrElse e.KeyChar = " "c OrElse e.KeyChar = "-"c OrElse e.KeyChar = "@"c _
        OrElse e.KeyChar = "."c OrElse e.KeyChar = ","c OrElse e.KeyChar = "/"c) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAllergy_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAllergy.KeyPress
        If Char.IsControl(e.KeyChar) Then Return

        If Not (Char.IsLetter(e.KeyChar) OrElse e.KeyChar = " "c) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtContact_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtContact.KeyPress
        If Char.IsControl(e.KeyChar) Then Return

        If Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtEmail_TextChanged(sender As Object, e As EventArgs) Handles txtEmail.TextChanged

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Clearform()
    End Sub
End Class