<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AdminAuditTrailForm
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdminAuditTrailForm))
        Me.dgvAuditLogs = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.RefreshButton = New Guna.UI2.WinForms.Guna2Button()
        Me.txtSearch = New Guna.UI2.WinForms.Guna2TextBox()
        Me.btnBack = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.Guna2CustomGradientPanel1 = New Guna.UI2.WinForms.Guna2CustomGradientPanel()
        Me.Guna2HtmlLabel1 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        CType(Me.dgvAuditLogs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Guna2CustomGradientPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvAuditLogs
        '
        Me.dgvAuditLogs.AllowUserToResizeColumns = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.White
        Me.dgvAuditLogs.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvAuditLogs.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvAuditLogs.ColumnHeadersHeight = 25
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvAuditLogs.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgvAuditLogs.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dgvAuditLogs.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvAuditLogs.Location = New System.Drawing.Point(0, 265)
        Me.dgvAuditLogs.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.dgvAuditLogs.Name = "dgvAuditLogs"
        Me.dgvAuditLogs.ReadOnly = True
        Me.dgvAuditLogs.RowHeadersVisible = False
        Me.dgvAuditLogs.RowHeadersWidth = 51
        Me.dgvAuditLogs.RowTemplate.Height = 24
        Me.dgvAuditLogs.Size = New System.Drawing.Size(1082, 501)
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
        Me.dgvAuditLogs.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvAuditLogs.ThemeStyle.HeaderStyle.Height = 25
        Me.dgvAuditLogs.ThemeStyle.ReadOnly = True
        Me.dgvAuditLogs.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.dgvAuditLogs.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvAuditLogs.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvAuditLogs.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.dgvAuditLogs.ThemeStyle.RowsStyle.Height = 24
        Me.dgvAuditLogs.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvAuditLogs.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
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
        Me.RefreshButton.Location = New System.Drawing.Point(460, 178)
        Me.RefreshButton.Name = "RefreshButton"
        Me.RefreshButton.Size = New System.Drawing.Size(136, 45)
        Me.RefreshButton.TabIndex = 10
        Me.RefreshButton.Text = "Refresh"
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
        Me.txtSearch.Location = New System.Drawing.Point(622, 178)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.txtSearch.PlaceholderText = "Search"
        Me.txtSearch.SelectedText = ""
        Me.txtSearch.Size = New System.Drawing.Size(447, 50)
        Me.txtSearch.TabIndex = 13
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.BackColor = System.Drawing.SystemColors.ControlLight
        Me.btnBack.FillColor = System.Drawing.Color.Transparent
        Me.btnBack.Image = CType(resources.GetObject("btnBack.Image"), System.Drawing.Image)
        Me.btnBack.ImageRotate = 0!
        Me.btnBack.Location = New System.Drawing.Point(964, 25)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.btnBack.Size = New System.Drawing.Size(60, 58)
        Me.btnBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnBack.TabIndex = 21
        Me.btnBack.TabStop = False
        '
        'Guna2CustomGradientPanel1
        '
        Me.Guna2CustomGradientPanel1.Controls.Add(Me.Guna2HtmlLabel1)
        Me.Guna2CustomGradientPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Guna2CustomGradientPanel1.FillColor = System.Drawing.Color.LightGray
        Me.Guna2CustomGradientPanel1.FillColor3 = System.Drawing.Color.Silver
        Me.Guna2CustomGradientPanel1.FillColor4 = System.Drawing.Color.Silver
        Me.Guna2CustomGradientPanel1.Location = New System.Drawing.Point(0, 0)
        Me.Guna2CustomGradientPanel1.Name = "Guna2CustomGradientPanel1"
        Me.Guna2CustomGradientPanel1.Size = New System.Drawing.Size(1082, 124)
        Me.Guna2CustomGradientPanel1.TabIndex = 22
        '
        'Guna2HtmlLabel1
        '
        Me.Guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel1.Font = New System.Drawing.Font("Palatino Linotype", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel1.Location = New System.Drawing.Point(436, 37)
        Me.Guna2HtmlLabel1.Name = "Guna2HtmlLabel1"
        Me.Guna2HtmlLabel1.Size = New System.Drawing.Size(160, 46)
        Me.Guna2HtmlLabel1.TabIndex = 21
        Me.Guna2HtmlLabel1.Text = "Audit Trail"
        '
        'AdminAuditTrailForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(1082, 766)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.dgvAuditLogs)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.RefreshButton)
        Me.Controls.Add(Me.Guna2CustomGradientPanel1)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "AdminAuditTrailForm"
        Me.Text = "AuditTrailForm"
        CType(Me.dgvAuditLogs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Guna2CustomGradientPanel1.ResumeLayout(False)
        Me.Guna2CustomGradientPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvAuditLogs As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents RefreshButton As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents txtSearch As Guna.UI2.WinForms.Guna2TextBox

    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub

    Friend WithEvents btnBack As Guna.UI2.WinForms.Guna2CirclePictureBox
    Friend WithEvents Guna2CustomGradientPanel1 As Guna.UI2.WinForms.Guna2CustomGradientPanel
    Friend WithEvents Guna2HtmlLabel1 As Guna.UI2.WinForms.Guna2HtmlLabel
End Class
