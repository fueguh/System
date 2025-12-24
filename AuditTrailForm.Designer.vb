<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AuditTrailForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgvAuditLogs = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.Guna2DateTimePicker1 = New Guna.UI2.WinForms.Guna2DateTimePicker()
        Me.RefreshButton = New Guna.UI2.WinForms.Guna2Button()
        Me.cmbUser = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.cmbAction = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.txtSearch = New Guna.UI2.WinForms.Guna2TextBox()
        CType(Me.dgvAuditLogs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvAuditLogs
        '
        Me.dgvAuditLogs.AllowUserToResizeColumns = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.dgvAuditLogs.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvAuditLogs.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvAuditLogs.ColumnHeadersHeight = 4
        Me.dgvAuditLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvAuditLogs.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvAuditLogs.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dgvAuditLogs.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvAuditLogs.Location = New System.Drawing.Point(0, 313)
        Me.dgvAuditLogs.Name = "dgvAuditLogs"
        Me.dgvAuditLogs.ReadOnly = True
        Me.dgvAuditLogs.RowHeadersVisible = False
        Me.dgvAuditLogs.RowHeadersWidth = 51
        Me.dgvAuditLogs.RowTemplate.Height = 24
        Me.dgvAuditLogs.Size = New System.Drawing.Size(1426, 387)
        Me.dgvAuditLogs.TabIndex = 0
        Me.dgvAuditLogs.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White
        Me.dgvAuditLogs.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.dgvAuditLogs.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.dgvAuditLogs.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.dgvAuditLogs.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.dgvAuditLogs.ThemeStyle.BackColor = System.Drawing.Color.White
        Me.dgvAuditLogs.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvAuditLogs.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvAuditLogs.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.dgvAuditLogs.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvAuditLogs.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White
        Me.dgvAuditLogs.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        Me.dgvAuditLogs.ThemeStyle.HeaderStyle.Height = 4
        Me.dgvAuditLogs.ThemeStyle.ReadOnly = True
        Me.dgvAuditLogs.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.dgvAuditLogs.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvAuditLogs.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvAuditLogs.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.dgvAuditLogs.ThemeStyle.RowsStyle.Height = 24
        Me.dgvAuditLogs.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvAuditLogs.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        '
        'Guna2DateTimePicker1
        '
        Me.Guna2DateTimePicker1.Checked = True
        Me.Guna2DateTimePicker1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Guna2DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.Guna2DateTimePicker1.Location = New System.Drawing.Point(873, 31)
        Me.Guna2DateTimePicker1.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.Guna2DateTimePicker1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.Guna2DateTimePicker1.Name = "Guna2DateTimePicker1"
        Me.Guna2DateTimePicker1.Size = New System.Drawing.Size(341, 36)
        Me.Guna2DateTimePicker1.TabIndex = 2
        Me.Guna2DateTimePicker1.Value = New Date(2025, 12, 24, 8, 37, 3, 900)
        '
        'RefreshButton
        '
        Me.RefreshButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.RefreshButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.RefreshButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.RefreshButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.RefreshButton.FillColor = System.Drawing.Color.White
        Me.RefreshButton.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RefreshButton.ForeColor = System.Drawing.Color.DimGray
        Me.RefreshButton.Location = New System.Drawing.Point(525, 116)
        Me.RefreshButton.Margin = New System.Windows.Forms.Padding(4)
        Me.RefreshButton.Name = "RefreshButton"
        Me.RefreshButton.Size = New System.Drawing.Size(181, 55)
        Me.RefreshButton.TabIndex = 10
        Me.RefreshButton.Text = "Refresh"
        '
        'cmbUser
        '
        Me.cmbUser.BackColor = System.Drawing.Color.Transparent
        Me.cmbUser.BorderRadius = 15
        Me.cmbUser.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUser.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbUser.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbUser.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.cmbUser.ForeColor = System.Drawing.Color.Black
        Me.cmbUser.ItemHeight = 30
        Me.cmbUser.Items.AddRange(New Object() {"Full-time", "Part-time", "Morning Shift", "Afternoon Shift"})
        Me.cmbUser.Location = New System.Drawing.Point(76, 31)
        Me.cmbUser.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbUser.Name = "cmbUser"
        Me.cmbUser.Size = New System.Drawing.Size(290, 36)
        Me.cmbUser.TabIndex = 11
        '
        'cmbAction
        '
        Me.cmbAction.BackColor = System.Drawing.Color.Transparent
        Me.cmbAction.BorderRadius = 15
        Me.cmbAction.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAction.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbAction.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbAction.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.cmbAction.ForeColor = System.Drawing.Color.Black
        Me.cmbAction.ItemHeight = 30
        Me.cmbAction.Items.AddRange(New Object() {"Full-time", "Part-time", "Morning Shift", "Afternoon Shift"})
        Me.cmbAction.Location = New System.Drawing.Point(525, 31)
        Me.cmbAction.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbAction.Name = "cmbAction"
        Me.cmbAction.Size = New System.Drawing.Size(283, 36)
        Me.cmbAction.TabIndex = 12
        '
        'txtSearch
        '
        Me.txtSearch.BorderRadius = 15
        Me.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSearch.DefaultText = ""
        Me.txtSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSearch.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSearch.Location = New System.Drawing.Point(76, 116)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(5)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.txtSearch.PlaceholderText = "Search"
        Me.txtSearch.SelectedText = ""
        Me.txtSearch.Size = New System.Drawing.Size(283, 62)
        Me.txtSearch.TabIndex = 13
        '
        'AuditTrailForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(1426, 700)
        Me.Controls.Add(Me.dgvAuditLogs)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.cmbAction)
        Me.Controls.Add(Me.cmbUser)
        Me.Controls.Add(Me.RefreshButton)
        Me.Controls.Add(Me.Guna2DateTimePicker1)
        Me.Name = "AuditTrailForm"
        Me.Text = "AuditTrailForm"
        CType(Me.dgvAuditLogs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvAuditLogs As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents Guna2DateTimePicker1 As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents RefreshButton As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents cmbUser As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents cmbAction As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents txtSearch As Guna.UI2.WinForms.Guna2TextBox

    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub

End Class
