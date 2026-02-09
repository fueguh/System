Imports System.Data.SqlClient

Public Class AdminDBItemManagement

    Dim connString As String = "Data Source=RELICKNIGHT;Initial Catalog=Dental;Integrated Security=True"
    Dim conn As New SqlConnection(connString)

    Private Sub LoadInventory()

        Dim query As String = "SELECT * FROM Inventory"
        Dim adapter As New SqlDataAdapter(query, conn)
        Dim table As New DataTable()
        adapter.Fill(table)
        DataGridView1.DataSource = table

        For Each row As DataGridViewRow In DataGridView1.Rows
            If Convert.ToInt32(row.Cells("Quantity").Value) < Convert.ToInt32(row.Cells("ReorderLevel").Value) Then
                MessageBox.Show("Low stock alert: " & row.Cells("ItemName").Value)
            End If
        Next

    End Sub
    Private Sub TxtItemName_TextChanged(sender As Object, e As EventArgs) Handles TxtItemName.TextChanged, TxtItemName.TextChanged

    End Sub

    Private Sub TxtUnit_TextChanged(sender As Object, e As EventArgs) Handles TxtUnit.TextChanged, TxtUnit.TextChanged

    End Sub

    Private Sub BTNAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click

        Dim query As String = "INSERT INTO Inventory (ItemName, Category, Quantity, Unit, ExpiryDate, Supplier) 
                           VALUES (@ItemName, @Category, @Quantity, @Unit, @ExpiryDate, @Supplier)"
        Using cmd As New SqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@ItemName", TxtItemName.Text)
            cmd.Parameters.AddWithValue("@Category", CmbCategory.Text)
            cmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(TxtQuantity.Text))
            cmd.Parameters.AddWithValue("@Unit", TxtUnit.Text)
            cmd.Parameters.AddWithValue("@ExpiryDate", DtpExpiry.Value)
            cmd.Parameters.AddWithValue("@Supplier", TxtSupplier.Text)

            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()
        End Using
        LoadInventory()
        MessageBox.Show("Item added successfully!")

    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click, BtnUpdate.Click

        Dim query As String = "UPDATE Inventory SET ItemName=@ItemName, Category=@Category, Quantity=@Quantity, 
                           Unit=@Unit, ExpiryDate=@ExpiryDate, Supplier=@Supplier WHERE ItemID=@ItemID"
        Using cmd As New SqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@ItemID", Convert.ToInt32(DataGridView1.CurrentRow.Cells("ItemID").Value))
            cmd.Parameters.AddWithValue("@ItemName", TxtItemName.Text)
            cmd.Parameters.AddWithValue("@Category", CmbCategory.Text)
            cmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(TxtQuantity.Text))
            cmd.Parameters.AddWithValue("@Unit", TxtUnit.Text)
            cmd.Parameters.AddWithValue("@ExpiryDate", DtpExpiry.Value)
            cmd.Parameters.AddWithValue("@Supplier", TxtSupplier.Text)

            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()
        End Using
        LoadInventory()
        MessageBox.Show("Item updated successfully!")

    End Sub

    Private Sub AdminDBItemManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TxtSupplier_TextChanged(sender As Object, e As EventArgs) Handles TxtSupplier.TextChanged, TxtSupplier.TextChanged

    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click

        Dim query As String = "DELETE FROM Inventory WHERE ItemID=@ItemID"
        Using cmd As New SqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@ItemID", Convert.ToInt32(DataGridView1.CurrentRow.Cells("ItemID").Value))

            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()
        End Using
        LoadInventory()
        MessageBox.Show("Item deleted successfully!")

    End Sub
End Class