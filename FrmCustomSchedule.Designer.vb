<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmCustomSchedule
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCustomSchedule))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnUpdate = New Guna.UI2.WinForms.Guna2Button()
        Me.Guna2HtmlLabel1 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2HtmlLabel3 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2CustomGradientPanel1 = New Guna.UI2.WinForms.Guna2CustomGradientPanel()
        Me.Guna2CirclePictureBox1 = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.Guna2HtmlLabel6 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2GroupBox1 = New Guna.UI2.WinForms.Guna2GroupBox()
        Me.ClbDays = New System.Windows.Forms.CheckedListBox()
        Me.DGVPartTimers = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.btnClear = New Guna.UI2.WinForms.Guna2Button()
        Me.cmbStartCustom = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.cmbEndCustom = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.Guna2CustomGradientPanel1.SuspendLayout()
        CType(Me.Guna2CirclePictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Guna2GroupBox1.SuspendLayout()
        CType(Me.DGVPartTimers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnUpdate
        '
        Me.btnUpdate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.btnUpdate.BorderRadius = 10
        Me.btnUpdate.BorderThickness = 2
        Me.btnUpdate.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnUpdate.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnUpdate.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnUpdate.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnUpdate.FillColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.btnUpdate.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!)
        Me.btnUpdate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.btnUpdate.Location = New System.Drawing.Point(29, 630)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(365, 50)
        Me.btnUpdate.TabIndex = 29
        Me.btnUpdate.Text = "Update"
        '
        'Guna2HtmlLabel1
        '
        Me.Guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel1.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel1.Location = New System.Drawing.Point(29, 207)
        Me.Guna2HtmlLabel1.Name = "Guna2HtmlLabel1"
        Me.Guna2HtmlLabel1.Size = New System.Drawing.Size(84, 28)
        Me.Guna2HtmlLabel1.TabIndex = 53
        Me.Guna2HtmlLabel1.Text = "End time:"
        '
        'Guna2HtmlLabel3
        '
        Me.Guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel3.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel3.Location = New System.Drawing.Point(29, 137)
        Me.Guna2HtmlLabel3.Name = "Guna2HtmlLabel3"
        Me.Guna2HtmlLabel3.Size = New System.Drawing.Size(88, 28)
        Me.Guna2HtmlLabel3.TabIndex = 52
        Me.Guna2HtmlLabel3.Text = "Start time:"
        '
        'Guna2CustomGradientPanel1
        '
        Me.Guna2CustomGradientPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2CustomGradientPanel1.Controls.Add(Me.Guna2CirclePictureBox1)
        Me.Guna2CustomGradientPanel1.Controls.Add(Me.Guna2HtmlLabel6)
        Me.Guna2CustomGradientPanel1.FillColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.FillColor3 = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.FillColor4 = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.Location = New System.Drawing.Point(-203, 0)
        Me.Guna2CustomGradientPanel1.Name = "Guna2CustomGradientPanel1"
        Me.Guna2CustomGradientPanel1.Size = New System.Drawing.Size(1638, 107)
        Me.Guna2CustomGradientPanel1.TabIndex = 54
        '
        'Guna2CirclePictureBox1
        '
        Me.Guna2CirclePictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2CirclePictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2CirclePictureBox1.FillColor = System.Drawing.Color.Transparent
        Me.Guna2CirclePictureBox1.Image = CType(resources.GetObject("Guna2CirclePictureBox1.Image"), System.Drawing.Image)
        Me.Guna2CirclePictureBox1.ImageRotate = 0!
        Me.Guna2CirclePictureBox1.Location = New System.Drawing.Point(1555, 22)
        Me.Guna2CirclePictureBox1.Name = "Guna2CirclePictureBox1"
        Me.Guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.Guna2CirclePictureBox1.Size = New System.Drawing.Size(60, 58)
        Me.Guna2CirclePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Guna2CirclePictureBox1.TabIndex = 57
        Me.Guna2CirclePictureBox1.TabStop = False
        '
        'Guna2HtmlLabel6
        '
        Me.Guna2HtmlLabel6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2HtmlLabel6.AutoSize = False
        Me.Guna2HtmlLabel6.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel6.Font = New System.Drawing.Font("Mongolian Baiti", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.Guna2HtmlLabel6.Location = New System.Drawing.Point(204, 1)
        Me.Guna2HtmlLabel6.Name = "Guna2HtmlLabel6"
        Me.Guna2HtmlLabel6.Size = New System.Drawing.Size(1431, 106)
        Me.Guna2HtmlLabel6.TabIndex = 48
        Me.Guna2HtmlLabel6.Text = "Part-time Schedule"
        Me.Guna2HtmlLabel6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Guna2GroupBox1
        '
        Me.Guna2GroupBox1.BorderRadius = 10
        Me.Guna2GroupBox1.Controls.Add(Me.ClbDays)
        Me.Guna2GroupBox1.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2GroupBox1.Location = New System.Drawing.Point(28, 338)
        Me.Guna2GroupBox1.Name = "Guna2GroupBox1"
        Me.Guna2GroupBox1.Size = New System.Drawing.Size(375, 241)
        Me.Guna2GroupBox1.TabIndex = 56
        Me.Guna2GroupBox1.Text = "Days Available"
        '
        'ClbDays
        '
        Me.ClbDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ClbDays.CheckOnClick = True
        Me.ClbDays.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ClbDays.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.ClbDays.FormattingEnabled = True
        Me.ClbDays.Location = New System.Drawing.Point(0, 40)
        Me.ClbDays.Name = "ClbDays"
        Me.ClbDays.Size = New System.Drawing.Size(375, 201)
        Me.ClbDays.TabIndex = 20
        '
        'DGVPartTimers
        '
        Me.DGVPartTimers.AllowUserToAddRows = False
        Me.DGVPartTimers.AllowUserToDeleteRows = False
        Me.DGVPartTimers.AllowUserToResizeColumns = False
        Me.DGVPartTimers.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.DGVPartTimers.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGVPartTimers.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(122, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(178, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVPartTimers.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGVPartTimers.ColumnHeadersHeight = 30
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVPartTimers.DefaultCellStyle = DataGridViewCellStyle3
        Me.DGVPartTimers.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVPartTimers.Location = New System.Drawing.Point(433, 107)
        Me.DGVPartTimers.Name = "DGVPartTimers"
        Me.DGVPartTimers.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVPartTimers.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DGVPartTimers.RowHeadersVisible = False
        Me.DGVPartTimers.RowHeadersWidth = 51
        Me.DGVPartTimers.Size = New System.Drawing.Size(1002, 626)
        Me.DGVPartTimers.TabIndex = 57
        Me.DGVPartTimers.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVPartTimers.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.DGVPartTimers.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.DGVPartTimers.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.DGVPartTimers.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.DGVPartTimers.ThemeStyle.BackColor = System.Drawing.Color.White
        Me.DGVPartTimers.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVPartTimers.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVPartTimers.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DGVPartTimers.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVPartTimers.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White
        Me.DGVPartTimers.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DGVPartTimers.ThemeStyle.HeaderStyle.Height = 30
        Me.DGVPartTimers.ThemeStyle.ReadOnly = True
        Me.DGVPartTimers.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVPartTimers.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DGVPartTimers.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVPartTimers.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.DGVPartTimers.ThemeStyle.RowsStyle.Height = 22
        Me.DGVPartTimers.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVPartTimers.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        '
        'btnClear
        '
        Me.btnClear.BorderRadius = 10
        Me.btnClear.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnClear.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnClear.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnClear.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnClear.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.btnClear.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!)
        Me.btnClear.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.btnClear.Location = New System.Drawing.Point(29, 283)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(89, 49)
        Me.btnClear.TabIndex = 84
        Me.btnClear.Text = "Clear"
        '
        'cmbStartCustom
        '
        Me.cmbStartCustom.BackColor = System.Drawing.Color.Transparent
        Me.cmbStartCustom.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.cmbStartCustom.BorderRadius = 10
        Me.cmbStartCustom.BorderThickness = 2
        Me.cmbStartCustom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbStartCustom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStartCustom.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbStartCustom.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbStartCustom.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!)
        Me.cmbStartCustom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.cmbStartCustom.ItemHeight = 20
        Me.cmbStartCustom.Location = New System.Drawing.Point(29, 172)
        Me.cmbStartCustom.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbStartCustom.Name = "cmbStartCustom"
        Me.cmbStartCustom.Size = New System.Drawing.Size(365, 26)
        Me.cmbStartCustom.TabIndex = 86
        '
        'cmbEndCustom
        '
        Me.cmbEndCustom.BackColor = System.Drawing.Color.Transparent
        Me.cmbEndCustom.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.cmbEndCustom.BorderRadius = 10
        Me.cmbEndCustom.BorderThickness = 2
        Me.cmbEndCustom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbEndCustom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEndCustom.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbEndCustom.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbEndCustom.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!)
        Me.cmbEndCustom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.cmbEndCustom.ItemHeight = 20
        Me.cmbEndCustom.Location = New System.Drawing.Point(29, 242)
        Me.cmbEndCustom.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbEndCustom.Name = "cmbEndCustom"
        Me.cmbEndCustom.Size = New System.Drawing.Size(365, 26)
        Me.cmbEndCustom.TabIndex = 87
        '
        'FrmCustomSchedule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1433, 732)
        Me.Controls.Add(Me.cmbEndCustom)
        Me.Controls.Add(Me.cmbStartCustom)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.DGVPartTimers)
        Me.Controls.Add(Me.Guna2GroupBox1)
        Me.Controls.Add(Me.Guna2CustomGradientPanel1)
        Me.Controls.Add(Me.Guna2HtmlLabel1)
        Me.Controls.Add(Me.Guna2HtmlLabel3)
        Me.Controls.Add(Me.btnUpdate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmCustomSchedule"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FrmCustomSchedule"
        Me.Guna2CustomGradientPanel1.ResumeLayout(False)
        CType(Me.Guna2CirclePictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Guna2GroupBox1.ResumeLayout(False)
        CType(Me.DGVPartTimers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnUpdate As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Guna2HtmlLabel1 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel3 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2CustomGradientPanel1 As Guna.UI2.WinForms.Guna2CustomGradientPanel
    Friend WithEvents Guna2HtmlLabel6 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2GroupBox1 As Guna.UI2.WinForms.Guna2GroupBox
    Friend WithEvents ClbDays As CheckedListBox
    Friend WithEvents Guna2CirclePictureBox1 As Guna.UI2.WinForms.Guna2CirclePictureBox
    Friend WithEvents DGVPartTimers As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents btnClear As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents cmbStartCustom As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents cmbEndCustom As Guna.UI2.WinForms.Guna2ComboBox
End Class
