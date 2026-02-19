<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AdminDBAppointments
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdminDBAppointments))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.CmbPatient = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.DtpDate = New Guna.UI2.WinForms.Guna2DateTimePicker()
        Me.dtpStartTime = New Guna.UI2.WinForms.Guna2DateTimePicker()
        Me.DtpEndTime = New Guna.UI2.WinForms.Guna2DateTimePicker()
        Me.BTNAdd = New Guna.UI2.WinForms.Guna2Button()
        Me.BTNUpdate = New Guna.UI2.WinForms.Guna2Button()
        Me.BTNDelete = New Guna.UI2.WinForms.Guna2Button()
        Me.cmbStatus = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.Guna2CirclePictureBox1 = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.clbServices = New System.Windows.Forms.CheckedListBox()
        Me.Guna2GroupBox1 = New Guna.UI2.WinForms.Guna2GroupBox()
        Me.btnBack = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.CmbDent = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.BTNPayment = New Guna.UI2.WinForms.Guna2Button()
        Me.Guna2CustomGradientPanel1 = New Guna.UI2.WinForms.Guna2CustomGradientPanel()
        Me.Guna2HtmlLabel6 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.DGVAppointments = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.Guna2HtmlLabel3 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2HtmlLabel1 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2HtmlLabel2 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2HtmlLabel4 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2HtmlLabel5 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        CType(Me.Guna2CirclePictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Guna2GroupBox1.SuspendLayout()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Guna2CustomGradientPanel1.SuspendLayout()
        CType(Me.DGVAppointments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmbPatient
        '
        Me.CmbPatient.BackColor = System.Drawing.Color.Transparent
        Me.CmbPatient.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.CmbPatient.BorderRadius = 10
        Me.CmbPatient.BorderThickness = 2
        Me.CmbPatient.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbPatient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbPatient.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbPatient.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbPatient.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!)
        Me.CmbPatient.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.CmbPatient.ItemHeight = 30
        Me.CmbPatient.Location = New System.Drawing.Point(40, 159)
        Me.CmbPatient.Name = "CmbPatient"
        Me.CmbPatient.Size = New System.Drawing.Size(365, 36)
        Me.CmbPatient.TabIndex = 0
        '
        'DtpDate
        '
        Me.DtpDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.DtpDate.BorderRadius = 10
        Me.DtpDate.BorderThickness = 2
        Me.DtpDate.Checked = True
        Me.DtpDate.FillColor = System.Drawing.Color.White
        Me.DtpDate.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.DtpDate.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.DtpDate.Location = New System.Drawing.Point(432, 320)
        Me.DtpDate.Margin = New System.Windows.Forms.Padding(4)
        Me.DtpDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.DtpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpDate.Name = "DtpDate"
        Me.DtpDate.Size = New System.Drawing.Size(365, 46)
        Me.DtpDate.TabIndex = 3
        Me.DtpDate.Value = New Date(2025, 12, 18, 11, 1, 1, 395)
        '
        'dtpStartTime
        '
        Me.dtpStartTime.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.dtpStartTime.BorderRadius = 10
        Me.dtpStartTime.BorderThickness = 2
        Me.dtpStartTime.Checked = True
        Me.dtpStartTime.FillColor = System.Drawing.Color.White
        Me.dtpStartTime.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpStartTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.dtpStartTime.Location = New System.Drawing.Point(432, 237)
        Me.dtpStartTime.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpStartTime.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpStartTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStartTime.Name = "dtpStartTime"
        Me.dtpStartTime.Size = New System.Drawing.Size(365, 46)
        Me.dtpStartTime.TabIndex = 4
        Me.dtpStartTime.Value = New Date(2025, 12, 18, 11, 1, 1, 395)
        '
        'DtpEndTime
        '
        Me.DtpEndTime.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.DtpEndTime.BorderRadius = 10
        Me.DtpEndTime.BorderThickness = 2
        Me.DtpEndTime.Checked = True
        Me.DtpEndTime.FillColor = System.Drawing.Color.White
        Me.DtpEndTime.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpEndTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.DtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.DtpEndTime.Location = New System.Drawing.Point(827, 237)
        Me.DtpEndTime.Margin = New System.Windows.Forms.Padding(4)
        Me.DtpEndTime.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.DtpEndTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpEndTime.Name = "DtpEndTime"
        Me.DtpEndTime.Size = New System.Drawing.Size(365, 46)
        Me.DtpEndTime.TabIndex = 5
        Me.DtpEndTime.Value = New Date(2025, 12, 18, 11, 1, 1, 395)
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
        Me.BTNAdd.Location = New System.Drawing.Point(1215, 159)
        Me.BTNAdd.Margin = New System.Windows.Forms.Padding(4)
        Me.BTNAdd.Name = "BTNAdd"
        Me.BTNAdd.Size = New System.Drawing.Size(183, 45)
        Me.BTNAdd.TabIndex = 6
        Me.BTNAdd.Text = "Add Appointment"
        '
        'BTNUpdate
        '
        Me.BTNUpdate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.BTNUpdate.BorderRadius = 10
        Me.BTNUpdate.BorderThickness = 2
        Me.BTNUpdate.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BTNUpdate.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BTNUpdate.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BTNUpdate.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BTNUpdate.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.BTNUpdate.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!)
        Me.BTNUpdate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.BTNUpdate.Location = New System.Drawing.Point(1412, 159)
        Me.BTNUpdate.Margin = New System.Windows.Forms.Padding(4)
        Me.BTNUpdate.Name = "BTNUpdate"
        Me.BTNUpdate.Size = New System.Drawing.Size(183, 45)
        Me.BTNUpdate.TabIndex = 7
        Me.BTNUpdate.Text = "Update"
        '
        'BTNDelete
        '
        Me.BTNDelete.BorderColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.BTNDelete.BorderRadius = 10
        Me.BTNDelete.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BTNDelete.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BTNDelete.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BTNDelete.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BTNDelete.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.BTNDelete.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!)
        Me.BTNDelete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.BTNDelete.Location = New System.Drawing.Point(1215, 237)
        Me.BTNDelete.Margin = New System.Windows.Forms.Padding(4)
        Me.BTNDelete.Name = "BTNDelete"
        Me.BTNDelete.Size = New System.Drawing.Size(183, 45)
        Me.BTNDelete.TabIndex = 8
        Me.BTNDelete.Text = "Delete"
        '
        'cmbStatus
        '
        Me.cmbStatus.BackColor = System.Drawing.Color.Transparent
        Me.cmbStatus.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.cmbStatus.BorderRadius = 10
        Me.cmbStatus.BorderThickness = 2
        Me.cmbStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbStatus.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbStatus.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!)
        Me.cmbStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.cmbStatus.ItemHeight = 30
        Me.cmbStatus.Location = New System.Drawing.Point(827, 159)
        Me.cmbStatus.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(365, 36)
        Me.cmbStatus.TabIndex = 10
        '
        'Guna2CirclePictureBox1
        '
        Me.Guna2CirclePictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2CirclePictureBox1.FillColor = System.Drawing.Color.Transparent
        Me.Guna2CirclePictureBox1.ImageRotate = 0!
        Me.Guna2CirclePictureBox1.Location = New System.Drawing.Point(1529, 15)
        Me.Guna2CirclePictureBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.Guna2CirclePictureBox1.Name = "Guna2CirclePictureBox1"
        Me.Guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.Guna2CirclePictureBox1.Size = New System.Drawing.Size(60, 58)
        Me.Guna2CirclePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Guna2CirclePictureBox1.TabIndex = 19
        Me.Guna2CirclePictureBox1.TabStop = False
        '
        'clbServices
        '
        Me.clbServices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbServices.CheckOnClick = True
        Me.clbServices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbServices.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.clbServices.FormattingEnabled = True
        Me.clbServices.Location = New System.Drawing.Point(0, 40)
        Me.clbServices.Name = "clbServices"
        Me.clbServices.Size = New System.Drawing.Size(361, 113)
        Me.clbServices.TabIndex = 20
        '
        'Guna2GroupBox1
        '
        Me.Guna2GroupBox1.BorderRadius = 10
        Me.Guna2GroupBox1.Controls.Add(Me.clbServices)
        Me.Guna2GroupBox1.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2GroupBox1.Location = New System.Drawing.Point(44, 213)
        Me.Guna2GroupBox1.Name = "Guna2GroupBox1"
        Me.Guna2GroupBox1.Size = New System.Drawing.Size(361, 153)
        Me.Guna2GroupBox1.TabIndex = 21
        Me.Guna2GroupBox1.Text = "Services to be Performed"
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.FillColor = System.Drawing.Color.LightGray
        Me.btnBack.Image = CType(resources.GetObject("btnBack.Image"), System.Drawing.Image)
        Me.btnBack.ImageRotate = 0!
        Me.btnBack.Location = New System.Drawing.Point(1542, 25)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.btnBack.Size = New System.Drawing.Size(60, 58)
        Me.btnBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnBack.TabIndex = 22
        Me.btnBack.TabStop = False
        '
        'CmbDent
        '
        Me.CmbDent.BackColor = System.Drawing.Color.Transparent
        Me.CmbDent.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.CmbDent.BorderRadius = 10
        Me.CmbDent.BorderThickness = 2
        Me.CmbDent.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbDent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbDent.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbDent.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbDent.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!)
        Me.CmbDent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.CmbDent.ItemHeight = 30
        Me.CmbDent.Location = New System.Drawing.Point(432, 159)
        Me.CmbDent.Margin = New System.Windows.Forms.Padding(4)
        Me.CmbDent.Name = "CmbDent"
        Me.CmbDent.Size = New System.Drawing.Size(365, 36)
        Me.CmbDent.TabIndex = 23
        '
        'BTNPayment
        '
        Me.BTNPayment.BorderColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.BTNPayment.BorderRadius = 10
        Me.BTNPayment.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BTNPayment.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BTNPayment.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BTNPayment.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BTNPayment.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.BTNPayment.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!)
        Me.BTNPayment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.BTNPayment.Location = New System.Drawing.Point(1412, 237)
        Me.BTNPayment.Margin = New System.Windows.Forms.Padding(4)
        Me.BTNPayment.Name = "BTNPayment"
        Me.BTNPayment.Size = New System.Drawing.Size(183, 45)
        Me.BTNPayment.TabIndex = 24
        Me.BTNPayment.Text = "Payment"
        '
        'Guna2CustomGradientPanel1
        '
        Me.Guna2CustomGradientPanel1.Controls.Add(Me.btnBack)
        Me.Guna2CustomGradientPanel1.Controls.Add(Me.Guna2HtmlLabel6)
        Me.Guna2CustomGradientPanel1.FillColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.FillColor3 = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.FillColor4 = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.Location = New System.Drawing.Point(0, 0)
        Me.Guna2CustomGradientPanel1.Name = "Guna2CustomGradientPanel1"
        Me.Guna2CustomGradientPanel1.Size = New System.Drawing.Size(1627, 107)
        Me.Guna2CustomGradientPanel1.TabIndex = 48
        '
        'Guna2HtmlLabel6
        '
        Me.Guna2HtmlLabel6.AutoSize = False
        Me.Guna2HtmlLabel6.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel6.Font = New System.Drawing.Font("Mongolian Baiti", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.Guna2HtmlLabel6.Location = New System.Drawing.Point(0, -1)
        Me.Guna2HtmlLabel6.Name = "Guna2HtmlLabel6"
        Me.Guna2HtmlLabel6.Size = New System.Drawing.Size(1624, 107)
        Me.Guna2HtmlLabel6.TabIndex = 47
        Me.Guna2HtmlLabel6.Text = "Appointment"
        Me.Guna2HtmlLabel6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DGVAppointments
        '
        Me.DGVAppointments.AllowUserToAddRows = False
        Me.DGVAppointments.AllowUserToDeleteRows = False
        Me.DGVAppointments.AllowUserToResizeColumns = False
        Me.DGVAppointments.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.DGVAppointments.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(122, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(178, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVAppointments.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGVAppointments.ColumnHeadersHeight = 30
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVAppointments.DefaultCellStyle = DataGridViewCellStyle3
        Me.DGVAppointments.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVAppointments.Location = New System.Drawing.Point(0, 465)
        Me.DGVAppointments.Name = "DGVAppointments"
        Me.DGVAppointments.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVAppointments.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DGVAppointments.RowHeadersVisible = False
        Me.DGVAppointments.RowHeadersWidth = 51
        Me.DGVAppointments.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.DGVAppointments.Size = New System.Drawing.Size(1627, 530)
        Me.DGVAppointments.TabIndex = 49
        Me.DGVAppointments.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVAppointments.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.DGVAppointments.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.DGVAppointments.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.DGVAppointments.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.DGVAppointments.ThemeStyle.BackColor = System.Drawing.Color.White
        Me.DGVAppointments.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVAppointments.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVAppointments.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DGVAppointments.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVAppointments.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White
        Me.DGVAppointments.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DGVAppointments.ThemeStyle.HeaderStyle.Height = 30
        Me.DGVAppointments.ThemeStyle.ReadOnly = True
        Me.DGVAppointments.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVAppointments.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DGVAppointments.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVAppointments.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.DGVAppointments.ThemeStyle.RowsStyle.Height = 22
        Me.DGVAppointments.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVAppointments.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        '
        'Guna2HtmlLabel3
        '
        Me.Guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel3.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel3.Location = New System.Drawing.Point(432, 209)
        Me.Guna2HtmlLabel3.Name = "Guna2HtmlLabel3"
        Me.Guna2HtmlLabel3.Size = New System.Drawing.Size(88, 28)
        Me.Guna2HtmlLabel3.TabIndex = 50
        Me.Guna2HtmlLabel3.Text = "Start time:"
        '
        'Guna2HtmlLabel1
        '
        Me.Guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel1.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel1.Location = New System.Drawing.Point(827, 209)
        Me.Guna2HtmlLabel1.Name = "Guna2HtmlLabel1"
        Me.Guna2HtmlLabel1.Size = New System.Drawing.Size(84, 28)
        Me.Guna2HtmlLabel1.TabIndex = 51
        Me.Guna2HtmlLabel1.Text = "End time:"
        '
        'Guna2HtmlLabel2
        '
        Me.Guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel2.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel2.Location = New System.Drawing.Point(40, 131)
        Me.Guna2HtmlLabel2.Name = "Guna2HtmlLabel2"
        Me.Guna2HtmlLabel2.Size = New System.Drawing.Size(117, 28)
        Me.Guna2HtmlLabel2.TabIndex = 52
        Me.Guna2HtmlLabel2.Text = "Select patient:"
        '
        'Guna2HtmlLabel4
        '
        Me.Guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel4.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel4.Location = New System.Drawing.Point(432, 131)
        Me.Guna2HtmlLabel4.Name = "Guna2HtmlLabel4"
        Me.Guna2HtmlLabel4.Size = New System.Drawing.Size(116, 28)
        Me.Guna2HtmlLabel4.TabIndex = 53
        Me.Guna2HtmlLabel4.Text = "Select dentist:"
        '
        'Guna2HtmlLabel5
        '
        Me.Guna2HtmlLabel5.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel5.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel5.Location = New System.Drawing.Point(827, 131)
        Me.Guna2HtmlLabel5.Name = "Guna2HtmlLabel5"
        Me.Guna2HtmlLabel5.Size = New System.Drawing.Size(108, 28)
        Me.Guna2HtmlLabel5.TabIndex = 54
        Me.Guna2HtmlLabel5.Text = "Select status:"
        '
        'AdminDBAppointments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1625, 996)
        Me.ControlBox = False
        Me.Controls.Add(Me.Guna2HtmlLabel5)
        Me.Controls.Add(Me.Guna2HtmlLabel4)
        Me.Controls.Add(Me.Guna2HtmlLabel2)
        Me.Controls.Add(Me.Guna2HtmlLabel1)
        Me.Controls.Add(Me.Guna2HtmlLabel3)
        Me.Controls.Add(Me.DGVAppointments)
        Me.Controls.Add(Me.Guna2CustomGradientPanel1)
        Me.Controls.Add(Me.BTNPayment)
        Me.Controls.Add(Me.CmbDent)
        Me.Controls.Add(Me.Guna2GroupBox1)
        Me.Controls.Add(Me.Guna2CirclePictureBox1)
        Me.Controls.Add(Me.cmbStatus)
        Me.Controls.Add(Me.BTNDelete)
        Me.Controls.Add(Me.BTNUpdate)
        Me.Controls.Add(Me.BTNAdd)
        Me.Controls.Add(Me.DtpEndTime)
        Me.Controls.Add(Me.dtpStartTime)
        Me.Controls.Add(Me.DtpDate)
        Me.Controls.Add(Me.CmbPatient)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "AdminDBAppointments"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AdminDBAppointments"
        CType(Me.Guna2CirclePictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Guna2GroupBox1.ResumeLayout(False)
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Guna2CustomGradientPanel1.ResumeLayout(False)
        CType(Me.DGVAppointments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CmbPatient As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents DtpDate As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents dtpStartTime As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents DtpEndTime As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents BTNAdd As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents BTNUpdate As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents BTNDelete As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents cmbStatus As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents Guna2CirclePictureBox1 As Guna.UI2.WinForms.Guna2CirclePictureBox
    Friend WithEvents clbServices As CheckedListBox
    Friend WithEvents Guna2GroupBox1 As Guna.UI2.WinForms.Guna2GroupBox
    Friend WithEvents btnBack As Guna.UI2.WinForms.Guna2CirclePictureBox
    Friend WithEvents CmbDent As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents BTNPayment As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Guna2CustomGradientPanel1 As Guna.UI2.WinForms.Guna2CustomGradientPanel
    Friend WithEvents Guna2HtmlLabel6 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents DGVAppointments As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents Guna2HtmlLabel3 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel1 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel2 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel4 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel5 As Guna.UI2.WinForms.Guna2HtmlLabel
End Class
