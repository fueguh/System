<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdminDBStockTracking
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdminDBStockTracking))
        Me.DGVTransactions = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.RadioIn = New Guna.UI2.WinForms.Guna2RadioButton()
        Me.NumericUpDownQuantity = New Guna.UI2.WinForms.Guna2NumericUpDown()
        Me.ButtonRecord = New Guna.UI2.WinForms.Guna2Button()
        Me.ButtonDelete = New Guna.UI2.WinForms.Guna2Button()
        Me.TransactionDate = New Guna.UI2.WinForms.Guna2DateTimePicker()
        Me.RadioOut = New Guna.UI2.WinForms.Guna2RadioButton()
        Me.ComboBoxItem = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.Guna2HtmlLabel1 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2HtmlLabel2 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.btnBack = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.TextBoxSupplier = New Guna.UI2.WinForms.Guna2TextBox()
        CType(Me.DGVTransactions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGVTransactions
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.DGVTransactions.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVTransactions.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGVTransactions.ColumnHeadersHeight = 4
        Me.DGVTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVTransactions.DefaultCellStyle = DataGridViewCellStyle3
        Me.DGVTransactions.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVTransactions.Location = New System.Drawing.Point(427, 24)
        Me.DGVTransactions.Name = "DGVTransactions"
        Me.DGVTransactions.RowHeadersVisible = False
        Me.DGVTransactions.Size = New System.Drawing.Size(1187, 820)
        Me.DGVTransactions.TabIndex = 0
        Me.DGVTransactions.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVTransactions.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.DGVTransactions.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.DGVTransactions.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.DGVTransactions.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.DGVTransactions.ThemeStyle.BackColor = System.Drawing.Color.White
        Me.DGVTransactions.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVTransactions.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVTransactions.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DGVTransactions.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVTransactions.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White
        Me.DGVTransactions.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        Me.DGVTransactions.ThemeStyle.HeaderStyle.Height = 4
        Me.DGVTransactions.ThemeStyle.ReadOnly = False
        Me.DGVTransactions.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVTransactions.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DGVTransactions.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVTransactions.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.DGVTransactions.ThemeStyle.RowsStyle.Height = 22
        Me.DGVTransactions.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVTransactions.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        '
        'RadioIn
        '
        Me.RadioIn.AutoSize = True
        Me.RadioIn.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadioIn.CheckedState.BorderThickness = 0
        Me.RadioIn.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadioIn.CheckedState.InnerColor = System.Drawing.Color.White
        Me.RadioIn.CheckedState.InnerOffset = -4
        Me.RadioIn.Location = New System.Drawing.Point(108, 94)
        Me.RadioIn.Name = "RadioIn"
        Me.RadioIn.Size = New System.Drawing.Size(36, 17)
        Me.RadioIn.TabIndex = 1
        Me.RadioIn.Text = "IN"
        Me.RadioIn.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.RadioIn.UncheckedState.BorderThickness = 2
        Me.RadioIn.UncheckedState.FillColor = System.Drawing.Color.Transparent
        Me.RadioIn.UncheckedState.InnerColor = System.Drawing.Color.Transparent
        '
        'NumericUpDownQuantity
        '
        Me.NumericUpDownQuantity.BackColor = System.Drawing.Color.Transparent
        Me.NumericUpDownQuantity.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.NumericUpDownQuantity.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.NumericUpDownQuantity.Location = New System.Drawing.Point(108, 187)
        Me.NumericUpDownQuantity.Name = "NumericUpDownQuantity"
        Me.NumericUpDownQuantity.Size = New System.Drawing.Size(100, 36)
        Me.NumericUpDownQuantity.TabIndex = 3
        '
        'ButtonRecord
        '
        Me.ButtonRecord.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.ButtonRecord.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.ButtonRecord.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.ButtonRecord.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.ButtonRecord.FillColor = System.Drawing.Color.White
        Me.ButtonRecord.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.ButtonRecord.ForeColor = System.Drawing.Color.Black
        Me.ButtonRecord.Location = New System.Drawing.Point(90, 408)
        Me.ButtonRecord.Name = "ButtonRecord"
        Me.ButtonRecord.Size = New System.Drawing.Size(158, 45)
        Me.ButtonRecord.TabIndex = 7
        Me.ButtonRecord.Text = "Record"
        '
        'ButtonDelete
        '
        Me.ButtonDelete.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.ButtonDelete.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.ButtonDelete.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.ButtonDelete.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.ButtonDelete.FillColor = System.Drawing.Color.White
        Me.ButtonDelete.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.ButtonDelete.ForeColor = System.Drawing.Color.Black
        Me.ButtonDelete.Location = New System.Drawing.Point(72, 600)
        Me.ButtonDelete.Name = "ButtonDelete"
        Me.ButtonDelete.Size = New System.Drawing.Size(158, 45)
        Me.ButtonDelete.TabIndex = 8
        Me.ButtonDelete.Text = "Delete"
        '
        'TransactionDate
        '
        Me.TransactionDate.Checked = True
        Me.TransactionDate.FillColor = System.Drawing.Color.White
        Me.TransactionDate.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TransactionDate.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.TransactionDate.Location = New System.Drawing.Point(47, 316)
        Me.TransactionDate.Margin = New System.Windows.Forms.Padding(4)
        Me.TransactionDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.TransactionDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.TransactionDate.Name = "TransactionDate"
        Me.TransactionDate.Size = New System.Drawing.Size(247, 46)
        Me.TransactionDate.TabIndex = 33
        Me.TransactionDate.Value = New Date(2025, 12, 18, 11, 1, 1, 395)
        '
        'RadioOut
        '
        Me.RadioOut.AutoSize = True
        Me.RadioOut.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadioOut.CheckedState.BorderThickness = 0
        Me.RadioOut.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadioOut.CheckedState.InnerColor = System.Drawing.Color.White
        Me.RadioOut.CheckedState.InnerOffset = -4
        Me.RadioOut.Location = New System.Drawing.Point(108, 117)
        Me.RadioOut.Name = "RadioOut"
        Me.RadioOut.Size = New System.Drawing.Size(48, 17)
        Me.RadioOut.TabIndex = 34
        Me.RadioOut.Text = "OUT"
        Me.RadioOut.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.RadioOut.UncheckedState.BorderThickness = 2
        Me.RadioOut.UncheckedState.FillColor = System.Drawing.Color.Transparent
        Me.RadioOut.UncheckedState.InnerColor = System.Drawing.Color.Transparent
        '
        'ComboBoxItem
        '
        Me.ComboBoxItem.BackColor = System.Drawing.Color.Transparent
        Me.ComboBoxItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ComboBoxItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxItem.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ComboBoxItem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ComboBoxItem.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.ComboBoxItem.ForeColor = System.Drawing.Color.Gray
        Me.ComboBoxItem.ItemHeight = 30
        Me.ComboBoxItem.Items.AddRange(New Object() {"Full-time", "Part-time", "Morning Shift", "Afternoon Shift"})
        Me.ComboBoxItem.Location = New System.Drawing.Point(61, 41)
        Me.ComboBoxItem.Name = "ComboBoxItem"
        Me.ComboBoxItem.Size = New System.Drawing.Size(257, 36)
        Me.ComboBoxItem.TabIndex = 35
        '
        'Guna2HtmlLabel1
        '
        Me.Guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel1.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel1.Location = New System.Drawing.Point(61, 12)
        Me.Guna2HtmlLabel1.Name = "Guna2HtmlLabel1"
        Me.Guna2HtmlLabel1.Size = New System.Drawing.Size(42, 28)
        Me.Guna2HtmlLabel1.TabIndex = 37
        Me.Guna2HtmlLabel1.Text = "Item"
        '
        'Guna2HtmlLabel2
        '
        Me.Guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel2.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel2.Location = New System.Drawing.Point(47, 288)
        Me.Guna2HtmlLabel2.Name = "Guna2HtmlLabel2"
        Me.Guna2HtmlLabel2.Size = New System.Drawing.Size(144, 28)
        Me.Guna2HtmlLabel2.TabIndex = 38
        Me.Guna2HtmlLabel2.Text = "Transaction Date"
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.FillColor = System.Drawing.Color.LightGray
        Me.btnBack.Image = CType(resources.GetObject("btnBack.Image"), System.Drawing.Image)
        Me.btnBack.ImageRotate = 0!
        Me.btnBack.Location = New System.Drawing.Point(12, 786)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.btnBack.Size = New System.Drawing.Size(60, 58)
        Me.btnBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnBack.TabIndex = 39
        Me.btnBack.TabStop = False
        '
        'TextBoxSupplier
        '
        Me.TextBoxSupplier.BorderRadius = 10
        Me.TextBoxSupplier.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextBoxSupplier.DefaultText = ""
        Me.TextBoxSupplier.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TextBoxSupplier.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TextBoxSupplier.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TextBoxSupplier.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TextBoxSupplier.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextBoxSupplier.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.TextBoxSupplier.ForeColor = System.Drawing.Color.Black
        Me.TextBoxSupplier.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextBoxSupplier.Location = New System.Drawing.Point(47, 230)
        Me.TextBoxSupplier.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBoxSupplier.Name = "TextBoxSupplier"
        Me.TextBoxSupplier.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.TextBoxSupplier.PlaceholderText = "Supplier"
        Me.TextBoxSupplier.SelectedText = ""
        Me.TextBoxSupplier.Size = New System.Drawing.Size(257, 50)
        Me.TextBoxSupplier.TabIndex = 40
        '
        'AdminDBStockTracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(1655, 856)
        Me.Controls.Add(Me.TextBoxSupplier)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.Guna2HtmlLabel2)
        Me.Controls.Add(Me.Guna2HtmlLabel1)
        Me.Controls.Add(Me.ComboBoxItem)
        Me.Controls.Add(Me.RadioOut)
        Me.Controls.Add(Me.TransactionDate)
        Me.Controls.Add(Me.ButtonDelete)
        Me.Controls.Add(Me.ButtonRecord)
        Me.Controls.Add(Me.NumericUpDownQuantity)
        Me.Controls.Add(Me.RadioIn)
        Me.Controls.Add(Me.DGVTransactions)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "AdminDBStockTracking"
        Me.Text = "AdminDBStockTracking"
        CType(Me.DGVTransactions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DGVTransactions As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents RadioIn As Guna.UI2.WinForms.Guna2RadioButton
    Friend WithEvents NumericUpDownQuantity As Guna.UI2.WinForms.Guna2NumericUpDown
    Friend WithEvents ButtonRecord As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents ButtonDelete As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents TransactionDate As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents RadioOut As Guna.UI2.WinForms.Guna2RadioButton
    Friend WithEvents ComboBoxItem As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents Guna2HtmlLabel1 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel2 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents btnBack As Guna.UI2.WinForms.Guna2CirclePictureBox
    Friend WithEvents TextBoxSupplier As Guna.UI2.WinForms.Guna2TextBox
End Class
