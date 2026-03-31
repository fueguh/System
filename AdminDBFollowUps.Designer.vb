<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AdminDBFollowUps
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdminDBFollowUps))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnBack = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.Guna2HtmlLabel1 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.lblPatientName = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.dgvFollowUps = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.lblFollowUpDate = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.lblStatus = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.lblReason = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2HtmlLabel2 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2HtmlLabel3 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2HtmlLabel4 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2HtmlLabel5 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.btnDone = New Guna.UI2.WinForms.Guna2Button()
        Me.btnMissed = New Guna.UI2.WinForms.Guna2Button()
        Me.btnReschedule = New Guna.UI2.WinForms.Guna2Button()
        Me.dtpNewDate = New Guna.UI2.WinForms.Guna2DateTimePicker()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtSearch = New Guna.UI2.WinForms.Guna2TextBox()
        Me.Guna2HtmlLabel7 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnClear = New Guna.UI2.WinForms.Guna2Button()
        Me.Panel1.SuspendLayout()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvFollowUps, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Panel1.Controls.Add(Me.btnBack)
        Me.Panel1.Controls.Add(Me.Guna2HtmlLabel1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1449, 104)
        Me.Panel1.TabIndex = 0
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.FillColor = System.Drawing.Color.LightGray
        Me.btnBack.Image = CType(resources.GetObject("btnBack.Image"), System.Drawing.Image)
        Me.btnBack.ImageRotate = 0!
        Me.btnBack.Location = New System.Drawing.Point(1377, 24)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.btnBack.Size = New System.Drawing.Size(60, 58)
        Me.btnBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnBack.TabIndex = 23
        Me.btnBack.TabStop = False
        '
        'Guna2HtmlLabel1
        '
        Me.Guna2HtmlLabel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2HtmlLabel1.AutoSize = False
        Me.Guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel1.Font = New System.Drawing.Font("Mongolian Baiti", 26.25!, System.Drawing.FontStyle.Bold)
        Me.Guna2HtmlLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.Guna2HtmlLabel1.Location = New System.Drawing.Point(-2, 0)
        Me.Guna2HtmlLabel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Guna2HtmlLabel1.Name = "Guna2HtmlLabel1"
        Me.Guna2HtmlLabel1.Size = New System.Drawing.Size(1449, 104)
        Me.Guna2HtmlLabel1.TabIndex = 0
        Me.Guna2HtmlLabel1.Text = "Follow-ups"
        Me.Guna2HtmlLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPatientName
        '
        Me.lblPatientName.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientName.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.lblPatientName.Location = New System.Drawing.Point(151, 2)
        Me.lblPatientName.Margin = New System.Windows.Forms.Padding(2)
        Me.lblPatientName.Name = "lblPatientName"
        Me.lblPatientName.Size = New System.Drawing.Size(17, 28)
        Me.lblPatientName.TabIndex = 91
        Me.lblPatientName.Text = """"""
        '
        'dgvFollowUps
        '
        Me.dgvFollowUps.AllowUserToAddRows = False
        Me.dgvFollowUps.AllowUserToDeleteRows = False
        Me.dgvFollowUps.AllowUserToResizeColumns = False
        Me.dgvFollowUps.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.dgvFollowUps.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvFollowUps.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(122, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(178, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvFollowUps.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvFollowUps.ColumnHeadersHeight = 30
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvFollowUps.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvFollowUps.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvFollowUps.Location = New System.Drawing.Point(40, 146)
        Me.dgvFollowUps.Name = "dgvFollowUps"
        Me.dgvFollowUps.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvFollowUps.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvFollowUps.RowHeadersVisible = False
        Me.dgvFollowUps.RowHeadersWidth = 51
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!)
        Me.dgvFollowUps.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvFollowUps.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.dgvFollowUps.Size = New System.Drawing.Size(823, 412)
        Me.dgvFollowUps.TabIndex = 92
        Me.dgvFollowUps.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White
        Me.dgvFollowUps.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.dgvFollowUps.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.dgvFollowUps.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.dgvFollowUps.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.dgvFollowUps.ThemeStyle.BackColor = System.Drawing.Color.White
        Me.dgvFollowUps.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvFollowUps.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvFollowUps.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.dgvFollowUps.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvFollowUps.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White
        Me.dgvFollowUps.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvFollowUps.ThemeStyle.HeaderStyle.Height = 30
        Me.dgvFollowUps.ThemeStyle.ReadOnly = True
        Me.dgvFollowUps.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.dgvFollowUps.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvFollowUps.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvFollowUps.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.dgvFollowUps.ThemeStyle.RowsStyle.Height = 22
        Me.dgvFollowUps.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvFollowUps.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        '
        'lblFollowUpDate
        '
        Me.lblFollowUpDate.BackColor = System.Drawing.Color.Transparent
        Me.lblFollowUpDate.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFollowUpDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.lblFollowUpDate.Location = New System.Drawing.Point(164, 48)
        Me.lblFollowUpDate.Margin = New System.Windows.Forms.Padding(2)
        Me.lblFollowUpDate.Name = "lblFollowUpDate"
        Me.lblFollowUpDate.Size = New System.Drawing.Size(17, 28)
        Me.lblFollowUpDate.TabIndex = 91
        Me.lblFollowUpDate.Text = """"""
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblStatus.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.lblStatus.Location = New System.Drawing.Point(91, 95)
        Me.lblStatus.Margin = New System.Windows.Forms.Padding(2)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(17, 28)
        Me.lblStatus.TabIndex = 91
        Me.lblStatus.Text = """"""
        '
        'lblReason
        '
        Me.lblReason.BackColor = System.Drawing.Color.Transparent
        Me.lblReason.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReason.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.lblReason.Location = New System.Drawing.Point(103, 143)
        Me.lblReason.Margin = New System.Windows.Forms.Padding(2)
        Me.lblReason.Name = "lblReason"
        Me.lblReason.Size = New System.Drawing.Size(17, 28)
        Me.lblReason.TabIndex = 91
        Me.lblReason.Text = """"""
        '
        'Guna2HtmlLabel2
        '
        Me.Guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel2.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel2.Location = New System.Drawing.Point(13, 143)
        Me.Guna2HtmlLabel2.Margin = New System.Windows.Forms.Padding(2)
        Me.Guna2HtmlLabel2.Name = "Guna2HtmlLabel2"
        Me.Guna2HtmlLabel2.Size = New System.Drawing.Size(68, 28)
        Me.Guna2HtmlLabel2.TabIndex = 95
        Me.Guna2HtmlLabel2.Text = "Reason:"
        '
        'Guna2HtmlLabel3
        '
        Me.Guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel3.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel3.Location = New System.Drawing.Point(13, 95)
        Me.Guna2HtmlLabel3.Margin = New System.Windows.Forms.Padding(2)
        Me.Guna2HtmlLabel3.Name = "Guna2HtmlLabel3"
        Me.Guna2HtmlLabel3.Size = New System.Drawing.Size(58, 28)
        Me.Guna2HtmlLabel3.TabIndex = 96
        Me.Guna2HtmlLabel3.Text = "Status:"
        '
        'Guna2HtmlLabel4
        '
        Me.Guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel4.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel4.Location = New System.Drawing.Point(13, 48)
        Me.Guna2HtmlLabel4.Margin = New System.Windows.Forms.Padding(2)
        Me.Guna2HtmlLabel4.Name = "Guna2HtmlLabel4"
        Me.Guna2HtmlLabel4.Size = New System.Drawing.Size(136, 28)
        Me.Guna2HtmlLabel4.TabIndex = 97
        Me.Guna2HtmlLabel4.Text = "Follow-up Date:"
        '
        'Guna2HtmlLabel5
        '
        Me.Guna2HtmlLabel5.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel5.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel5.Location = New System.Drawing.Point(13, 2)
        Me.Guna2HtmlLabel5.Margin = New System.Windows.Forms.Padding(2)
        Me.Guna2HtmlLabel5.Name = "Guna2HtmlLabel5"
        Me.Guna2HtmlLabel5.Size = New System.Drawing.Size(122, 28)
        Me.Guna2HtmlLabel5.TabIndex = 98
        Me.Guna2HtmlLabel5.Text = "Patient Name:"
        '
        'btnDone
        '
        Me.btnDone.BorderRadius = 10
        Me.btnDone.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnDone.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnDone.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnDone.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnDone.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.btnDone.Font = New System.Drawing.Font("Mongolian Baiti", 15.75!, System.Drawing.FontStyle.Bold)
        Me.btnDone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.btnDone.Location = New System.Drawing.Point(34, 42)
        Me.btnDone.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.btnDone.Name = "btnDone"
        Me.btnDone.Size = New System.Drawing.Size(232, 45)
        Me.btnDone.TabIndex = 99
        Me.btnDone.Text = "Mark Done"
        '
        'btnMissed
        '
        Me.btnMissed.BorderRadius = 10
        Me.btnMissed.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnMissed.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnMissed.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnMissed.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnMissed.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.btnMissed.Font = New System.Drawing.Font("Mongolian Baiti", 15.75!, System.Drawing.FontStyle.Bold)
        Me.btnMissed.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.btnMissed.Location = New System.Drawing.Point(294, 42)
        Me.btnMissed.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.btnMissed.Name = "btnMissed"
        Me.btnMissed.Size = New System.Drawing.Size(232, 45)
        Me.btnMissed.TabIndex = 100
        Me.btnMissed.Text = "Missed"
        '
        'btnReschedule
        '
        Me.btnReschedule.BorderRadius = 10
        Me.btnReschedule.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnReschedule.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnReschedule.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnReschedule.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnReschedule.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.btnReschedule.Font = New System.Drawing.Font("Mongolian Baiti", 15.75!, System.Drawing.FontStyle.Bold)
        Me.btnReschedule.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.btnReschedule.Location = New System.Drawing.Point(549, 42)
        Me.btnReschedule.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.btnReschedule.Name = "btnReschedule"
        Me.btnReschedule.Size = New System.Drawing.Size(232, 45)
        Me.btnReschedule.TabIndex = 101
        Me.btnReschedule.Text = "Reschedule"
        '
        'dtpNewDate
        '
        Me.dtpNewDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.dtpNewDate.BorderRadius = 10
        Me.dtpNewDate.BorderThickness = 2
        Me.dtpNewDate.Checked = True
        Me.dtpNewDate.FillColor = System.Drawing.Color.White
        Me.dtpNewDate.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpNewDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.dtpNewDate.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.dtpNewDate.Location = New System.Drawing.Point(34, 137)
        Me.dtpNewDate.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpNewDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpNewDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpNewDate.Name = "dtpNewDate"
        Me.dtpNewDate.Size = New System.Drawing.Size(365, 46)
        Me.dtpNewDate.TabIndex = 102
        Me.dtpNewDate.Value = New Date(2025, 12, 18, 11, 1, 1, 395)
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Guna2HtmlLabel5)
        Me.Panel2.Controls.Add(Me.lblPatientName)
        Me.Panel2.Controls.Add(Me.lblFollowUpDate)
        Me.Panel2.Controls.Add(Me.lblStatus)
        Me.Panel2.Controls.Add(Me.lblReason)
        Me.Panel2.Controls.Add(Me.Guna2HtmlLabel2)
        Me.Panel2.Controls.Add(Me.Guna2HtmlLabel4)
        Me.Panel2.Controls.Add(Me.Guna2HtmlLabel3)
        Me.Panel2.Location = New System.Drawing.Point(885, 203)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(536, 230)
        Me.Panel2.TabIndex = 103
        '
        'txtSearch
        '
        Me.txtSearch.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtSearch.BorderRadius = 10
        Me.txtSearch.BorderThickness = 2
        Me.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSearch.DefaultText = ""
        Me.txtSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSearch.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!)
        Me.txtSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSearch.Location = New System.Drawing.Point(885, 146)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtSearch.PlaceholderText = "Search"
        Me.txtSearch.SelectedText = ""
        Me.txtSearch.Size = New System.Drawing.Size(334, 50)
        Me.txtSearch.TabIndex = 104
        '
        'Guna2HtmlLabel7
        '
        Me.Guna2HtmlLabel7.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel7.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel7.Location = New System.Drawing.Point(34, 103)
        Me.Guna2HtmlLabel7.Margin = New System.Windows.Forms.Padding(2)
        Me.Guna2HtmlLabel7.Name = "Guna2HtmlLabel7"
        Me.Guna2HtmlLabel7.Size = New System.Drawing.Size(136, 28)
        Me.Guna2HtmlLabel7.TabIndex = 106
        Me.Guna2HtmlLabel7.Text = "Follow-up Date:"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.btnClear)
        Me.Panel3.Controls.Add(Me.Guna2HtmlLabel7)
        Me.Panel3.Controls.Add(Me.btnReschedule)
        Me.Panel3.Controls.Add(Me.btnMissed)
        Me.Panel3.Controls.Add(Me.btnDone)
        Me.Panel3.Controls.Add(Me.dtpNewDate)
        Me.Panel3.Location = New System.Drawing.Point(49, 606)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1056, 216)
        Me.Panel3.TabIndex = 107
        '
        'btnClear
        '
        Me.btnClear.BorderRadius = 10
        Me.btnClear.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnClear.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnClear.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnClear.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnClear.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.btnClear.Font = New System.Drawing.Font("Mongolian Baiti", 15.75!, System.Drawing.FontStyle.Bold)
        Me.btnClear.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.btnClear.Location = New System.Drawing.Point(460, 138)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(232, 45)
        Me.btnClear.TabIndex = 107
        Me.btnClear.Text = "Clear"
        '
        'AdminDBFollowUps
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1449, 846)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.dgvFollowUps)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "AdminDBFollowUps"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AdminDBFollowUps"
        Me.Panel1.ResumeLayout(False)
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvFollowUps, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Guna2HtmlLabel1 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents lblPatientName As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents btnBack As Guna.UI2.WinForms.Guna2CirclePictureBox
    Friend WithEvents dgvFollowUps As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents lblFollowUpDate As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents lblStatus As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents lblReason As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel2 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel3 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel4 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel5 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents btnDone As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnMissed As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnReschedule As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents dtpNewDate As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents Panel2 As Panel
    Friend WithEvents txtSearch As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Guna2HtmlLabel7 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents btnClear As Guna.UI2.WinForms.Guna2Button
End Class
