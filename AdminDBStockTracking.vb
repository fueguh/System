Imports System.ComponentModel.Design
Imports System.Data.SqlClient

Public Class AdminDBStockTracking
    Private Sub LoadInventory()
        Dim query As String = "SELECT ItemID, ItemName, Quantity, Price FROM ItemManagement"

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
          adapter As New SqlDataAdapter(query, connection)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            ' DGVInventory.DataSource = dt   ' <-- bind to a separate grid if needed
        End Using
    End Sub
    Private Sub LoadItem()
        Dim query As String = "SELECT ItemID, ItemName FROM ItemManagement"

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
          adapter As New SqlDataAdapter(query, connection)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ComboBoxItem.DataSource = dt
            ComboBoxItem.DisplayMember = "ItemName"   ' what the user sees
            ComboBoxItem.ValueMember = "ItemID"       ' what the code uses internally
        End Using
    End Sub

    Private Sub ClearInputs()
        ComboBoxItem.SelectedIndex = -1
        RadioIn.Checked = False
        RadioOut.Checked = False
        NumericUpDownQuantity.Value = 0
        TransactionDate.Value = DateTime.Now.Date
    End Sub

    ' Only load transactions into DGVTransactions
    Private Sub LoadTransactions()
        Dim query As String = "SELECT t.TransactionID, 
                                  i.ItemName, 
                                  t.TransactionType, 
                                  t.Quantity, 
                                  t.TransactionDate,
                                  t.ItemID
                           FROM StockTransactions t
                           INNER JOIN ItemManagement i ON t.ItemID = i.ItemID"

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2),
          adapter As New SqlDataAdapter(query, connection)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            DGVTransactions.DataSource = dt
        End Using

        ' Hide IDs for user-friendliness
        If DGVTransactions.Columns.Contains("TransactionID") Then DGVTransactions.Columns("TransactionID").Visible = False
        If DGVTransactions.Columns.Contains("ItemID") Then DGVTransactions.Columns("ItemID").Visible = False
    End Sub
    Private Sub DGVTransactions_SelectionChanged(sender As Object, e As EventArgs) Handles DGVTransactions.SelectionChanged
        If DGVTransactions.SelectedRows.Count > 0 Then
            Dim row As DataGridViewRow = DGVTransactions.SelectedRows(0)

            ' Make sure the column exists
            If DGVTransactions.Columns.Contains("ItemID") Then
                Dim itemID As Integer = Convert.ToInt32(row.Cells("ItemID").Value)
                ComboBoxItem.SelectedValue = itemID
            End If
        End If
    End Sub
    Private Sub ButtonRecord_Click(sender As Object, e As EventArgs) Handles ButtonRecord.Click
        ' 1. Basic Validation
        If ComboBoxItem.SelectedValue Is Nothing OrElse Not TypeOf ComboBoxItem.SelectedValue Is Integer Then
            MessageBox.Show("Please select a valid item.")
            Exit Sub
        End If

        If NumericUpDownQuantity.Value <= 0 Then
            MessageBox.Show("Quantity must be greater than zero.")
            Exit Sub
        End If

        ' 2. Variable Setup
        Dim itemID As Integer = CInt(ComboBoxItem.SelectedValue)
        Dim qty As Integer = CInt(NumericUpDownQuantity.Value)
        Dim transType As String = If(RadioIn.Checked, "IN", If(RadioOut.Checked, "OUT", ""))

        If transType = "" Then
            MessageBox.Show("Please select a transaction type (IN or OUT).")
            Exit Sub
        End If

        Using connection As New SqlConnection(My.Settings.DentalDBConnection2)
            connection.Open()

            ' 3. Check stock if OUT
            If transType = "OUT" Then
                Using cmdCheck As New SqlCommand("SELECT Quantity FROM ItemManagement WHERE ItemID=@ItemID", connection)
                    cmdCheck.Parameters.AddWithValue("@ItemID", itemID)
                    Dim currentQty As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())

                    If qty > currentQty Then
                        MessageBox.Show($"Cannot deduct {qty} items. Only {currentQty} in stock.")
                        Exit Sub
                    End If
                End Using
            End If

            ' Start a transaction to ensure data integrity
            Using sqlTrans As SqlTransaction = connection.BeginTransaction()
                Try
                    ' Insert transaction
                    Dim queryTrans As String = "INSERT INTO StockTransactions (ItemID, TransactionType, Quantity, TransactionDate) " &
                                       "VALUES (@ItemID, @TransType, @Qty, @Date)"

                    Using cmdTrans As New SqlCommand(queryTrans, connection, sqlTrans)
                        cmdTrans.Parameters.AddWithValue("@ItemID", itemID)
                        cmdTrans.Parameters.AddWithValue("@TransType", transType)
                        cmdTrans.Parameters.AddWithValue("@Qty", qty)
                        cmdTrans.Parameters.AddWithValue("@Date", TransactionDate.Value.Date)
                        cmdTrans.ExecuteNonQuery()
                    End Using

                    ' Update stock
                    Dim queryUpdate As String = If(transType = "IN",
                                          "UPDATE ItemManagement SET Quantity = Quantity + @Qty WHERE ItemID = @ItemID",
                                          "UPDATE ItemManagement SET Quantity = Quantity - @Qty WHERE ItemID = @ItemID")

                    Using cmdUpdate As New SqlCommand(queryUpdate, connection, sqlTrans)
                        cmdUpdate.Parameters.AddWithValue("@ItemID", itemID)
                        cmdUpdate.Parameters.AddWithValue("@Qty", qty)
                        cmdUpdate.ExecuteNonQuery()
                    End Using

                    sqlTrans.Commit()
                    MessageBox.Show("Stock updated successfully!")
                Catch ex As Exception
                    sqlTrans.Rollback()
                    MessageBox.Show("Transaction failed: " & ex.Message)
                End Try
            End Using
        End Using

        ' 4. Refresh UI
        LoadTransactions()
        LoadInventory()
        ClearInputs()
    End Sub

    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click
        ' 1. Ensure a row is selected
        If DGVTransactions.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a transaction to delete.")
            Exit Sub
        End If

        Dim row As DataGridViewRow = DGVTransactions.SelectedRows(0)

        ' 2. Ensure the columns exist
        If Not (DGVTransactions.Columns.Contains("TransactionID") AndAlso
            DGVTransactions.Columns.Contains("TransactionType") AndAlso
            DGVTransactions.Columns.Contains("Quantity") AndAlso
            DGVTransactions.Columns.Contains("ItemID")) Then
            MessageBox.Show("Required columns not found.")
            Exit Sub
        End If

        ' 3. Read values safely
        Dim transID As Integer = Convert.ToInt32(row.Cells("TransactionID").Value)
        Dim transType As String = row.Cells("TransactionType").Value.ToString()
        Dim qty As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
        Dim itemID As Integer = Convert.ToInt32(row.Cells("ItemID").Value)

        ' 4. Determine stock adjustment
        Dim queryUpdate As String
        If transType = "IN" Then
            queryUpdate = "UPDATE ItemManagement SET Quantity = Quantity - @Quantity WHERE ItemID=@ItemID"
        ElseIf transType = "OUT" Then
            queryUpdate = "UPDATE ItemManagement SET Quantity = Quantity + @Quantity WHERE ItemID=@ItemID"
        Else
            MessageBox.Show("Invalid transaction type.")
            Exit Sub
        End If

        ' 5. Execute deletion safely
        Using connection As New SqlConnection(My.Settings.DentalDBConnection2)
            connection.Open()
            Using sqlTrans As SqlTransaction = connection.BeginTransaction()
                Try
                    ' Update stock
                    Using cmdUpdate As New SqlCommand(queryUpdate, connection, sqlTrans)
                        cmdUpdate.Parameters.AddWithValue("@ItemID", itemID)
                        cmdUpdate.Parameters.AddWithValue("@Quantity", qty)
                        cmdUpdate.ExecuteNonQuery()
                    End Using

                    ' Delete transaction
                    Using cmdDelete As New SqlCommand("DELETE FROM StockTransactions WHERE TransactionID=@TransactionID", connection, sqlTrans)
                        cmdDelete.Parameters.AddWithValue("@TransactionID", transID)
                        cmdDelete.ExecuteNonQuery()
                    End Using

                    sqlTrans.Commit()
                    MessageBox.Show("Transaction deleted successfully!")

                Catch ex As Exception
                    sqlTrans.Rollback()
                    MessageBox.Show("Failed to delete transaction: " & ex.Message)
                End Try
            End Using
        End Using

        ' 6. Refresh UI
        LoadTransactions()
        LoadInventory()
        ClearInputs()
    End Sub

    Private Sub AdminDBStockTracking_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTransactions()
        LoadInventory()
        LoadItem()
        ComboBoxItem.SelectedIndex = -1
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        SystemSession.NavigateToDashboard(Me)
    End Sub
End Class