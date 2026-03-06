<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdminDBPatientSelection
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Guna2GradientPanel1 = New Guna.UI2.WinForms.Guna2GradientPanel()
        Me.frmLabel = New System.Windows.Forms.Label()
        Me.txtSearchPatients = New Guna.UI2.WinForms.Guna2TextBox()
        Me.BTNAdd = New Guna.UI2.WinForms.Guna2Button()
        Me.BtnClear = New Guna.UI2.WinForms.Guna2Button()
        Me.DGVPatients = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.Guna2GradientPanel1.SuspendLayout()
        CType(Me.DGVPatients, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Guna2GradientPanel1
        '
        Me.Guna2GradientPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2GradientPanel1.Controls.Add(Me.frmLabel)
        Me.Guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Guna2GradientPanel1.Location = New System.Drawing.Point(0, 0)
        Me.Guna2GradientPanel1.Name = "Guna2GradientPanel1"
        Me.Guna2GradientPanel1.Size = New System.Drawing.Size(788, 80)
        Me.Guna2GradientPanel1.TabIndex = 50
        '
        'frmLabel
        '
        Me.frmLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.frmLabel.Font = New System.Drawing.Font("Mongolian Baiti", 26.25!, System.Drawing.FontStyle.Bold)
        Me.frmLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.frmLabel.Location = New System.Drawing.Point(0, 0)
        Me.frmLabel.Name = "frmLabel"
        Me.frmLabel.Size = New System.Drawing.Size(788, 76)
        Me.frmLabel.TabIndex = 51
        Me.frmLabel.Text = "Patients Selection"
        Me.frmLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSearchPatients
        '
        Me.txtSearchPatients.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearchPatients.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtSearchPatients.BorderRadius = 10
        Me.txtSearchPatients.BorderThickness = 2
        Me.txtSearchPatients.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSearchPatients.DefaultText = ""
        Me.txtSearchPatients.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtSearchPatients.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtSearchPatients.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtSearchPatients.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtSearchPatients.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSearchPatients.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!)
        Me.txtSearchPatients.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtSearchPatients.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSearchPatients.Location = New System.Drawing.Point(13, 96)
        Me.txtSearchPatients.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSearchPatients.Name = "txtSearchPatients"
        Me.txtSearchPatients.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtSearchPatients.PlaceholderText = "Search Patient"
        Me.txtSearchPatients.SelectedText = ""
        Me.txtSearchPatients.Size = New System.Drawing.Size(374, 50)
        Me.txtSearchPatients.TabIndex = 51
        '
        'BTNAdd
        '
        Me.BTNAdd.BorderColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.BTNAdd.BorderRadius = 10
        Me.BTNAdd.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BTNAdd.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BTNAdd.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BTNAdd.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BTNAdd.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.BTNAdd.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!)
        Me.BTNAdd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.BTNAdd.Location = New System.Drawing.Point(13, 164)
        Me.BTNAdd.Margin = New System.Windows.Forms.Padding(4)
        Me.BTNAdd.Name = "BTNAdd"
        Me.BTNAdd.Size = New System.Drawing.Size(374, 39)
        Me.BTNAdd.TabIndex = 52
        Me.BTNAdd.Text = "Select Patient"
        '
        'BtnClear
        '
        Me.BtnClear.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.BtnClear.BorderColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.BtnClear.BorderRadius = 10
        Me.BtnClear.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BtnClear.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BtnClear.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BtnClear.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BtnClear.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.BtnClear.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!)
        Me.BtnClear.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.BtnClear.Location = New System.Drawing.Point(429, 96)
        Me.BtnClear.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnClear.Name = "BtnClear"
        Me.BtnClear.Size = New System.Drawing.Size(183, 45)
        Me.BtnClear.TabIndex = 53
        Me.BtnClear.Text = "Clear"
        '
        'DGVPatients
        '
        Me.DGVPatients.AllowUserToAddRows = False
        Me.DGVPatients.AllowUserToDeleteRows = False
        Me.DGVPatients.AllowUserToResizeColumns = False
        Me.DGVPatients.AllowUserToResizeRows = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.White
        Me.DGVPatients.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(122, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(178, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVPatients.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.DGVPatients.ColumnHeadersHeight = 30
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVPatients.DefaultCellStyle = DataGridViewCellStyle7
        Me.DGVPatients.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DGVPatients.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVPatients.Location = New System.Drawing.Point(0, 230)
        Me.DGVPatients.Name = "DGVPatients"
        Me.DGVPatients.ReadOnly = True
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVPatients.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.DGVPatients.RowHeadersVisible = False
        Me.DGVPatients.RowHeadersWidth = 51
        Me.DGVPatients.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.DGVPatients.Size = New System.Drawing.Size(788, 252)
        Me.DGVPatients.TabIndex = 54
        Me.DGVPatients.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVPatients.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.DGVPatients.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.DGVPatients.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.DGVPatients.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.DGVPatients.ThemeStyle.BackColor = System.Drawing.Color.White
        Me.DGVPatients.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVPatients.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVPatients.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DGVPatients.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVPatients.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White
        Me.DGVPatients.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DGVPatients.ThemeStyle.HeaderStyle.Height = 30
        Me.DGVPatients.ThemeStyle.ReadOnly = True
        Me.DGVPatients.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVPatients.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DGVPatients.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVPatients.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.DGVPatients.ThemeStyle.RowsStyle.Height = 22
        Me.DGVPatients.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVPatients.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        '
        'AdminDBPatientSelection
        '
        Me.AccessibleName = ""
        Me.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(788, 482)
        Me.Controls.Add(Me.DGVPatients)
        Me.Controls.Add(Me.BtnClear)
        Me.Controls.Add(Me.BTNAdd)
        Me.Controls.Add(Me.txtSearchPatients)
        Me.Controls.Add(Me.Guna2GradientPanel1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AdminDBPatientSelection"
        Me.ShowIcon = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Guna2GradientPanel1.ResumeLayout(False)
        CType(Me.DGVPatients, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Guna2GradientPanel1 As Guna.UI2.WinForms.Guna2GradientPanel
    Friend WithEvents frmLabel As Label
    Friend WithEvents txtSearchPatients As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents BTNAdd As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents BtnClear As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents DGVPatients As Guna.UI2.WinForms.Guna2DataGridView
End Class
