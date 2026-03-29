<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AdminDBTreatmentRecords
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdminDBTreatmentRecords))
        Me.TxtPrescriptions = New Guna.UI2.WinForms.Guna2TextBox()
        Me.TxtProceduresDone = New Guna.UI2.WinForms.Guna2TextBox()
        Me.BtnSaveRecord = New Guna.UI2.WinForms.Guna2Button()
        Me.Guna2CirclePictureBox1 = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.Guna2HtmlLabel2 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2HtmlLabel1 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.btnBack = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.Guna2CustomGradientPanel1 = New Guna.UI2.WinForms.Guna2CustomGradientPanel()
        Me.btnBack1 = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.Guna2HtmlLabel6 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.btnClear = New Guna.UI2.WinForms.Guna2Button()
        Me.lblPatientName = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.lblDentistName = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2HtmlLabel3 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.lblStatus = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2HtmlLabel4 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2HtmlLabel5 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2HtmlLabel7 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.TxtTreatmentNotes = New Guna.UI2.WinForms.Guna2TextBox()
        Me.dtpFollowUpDate = New Guna.UI2.WinForms.Guna2DateTimePicker()
        Me.txtFollowUpReason = New Guna.UI2.WinForms.Guna2TextBox()
        Me.chkNeedsFollowUp = New System.Windows.Forms.CheckBox()
        Me.grpFollowup = New System.Windows.Forms.GroupBox()
        Me.Guna2HtmlLabel11 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2HtmlLabel10 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2HtmlLabel8 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2HtmlLabel9 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.grpTreatment = New System.Windows.Forms.GroupBox()
        CType(Me.Guna2CirclePictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Guna2CustomGradientPanel1.SuspendLayout()
        CType(Me.btnBack1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFollowup.SuspendLayout()
        Me.grpTreatment.SuspendLayout()
        Me.SuspendLayout()
        '
        'TxtPrescriptions
        '
        Me.TxtPrescriptions.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtPrescriptions.BorderRadius = 10
        Me.TxtPrescriptions.BorderThickness = 2
        Me.TxtPrescriptions.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtPrescriptions.DefaultText = ""
        Me.TxtPrescriptions.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TxtPrescriptions.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TxtPrescriptions.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtPrescriptions.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtPrescriptions.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtPrescriptions.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!)
        Me.TxtPrescriptions.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtPrescriptions.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtPrescriptions.Location = New System.Drawing.Point(9, 367)
        Me.TxtPrescriptions.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.TxtPrescriptions.Multiline = True
        Me.TxtPrescriptions.Name = "TxtPrescriptions"
        Me.TxtPrescriptions.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtPrescriptions.PlaceholderText = "Prescriptions..."
        Me.TxtPrescriptions.SelectedText = ""
        Me.TxtPrescriptions.Size = New System.Drawing.Size(532, 104)
        Me.TxtPrescriptions.TabIndex = 3
        '
        'TxtProceduresDone
        '
        Me.TxtProceduresDone.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtProceduresDone.BorderRadius = 10
        Me.TxtProceduresDone.BorderThickness = 2
        Me.TxtProceduresDone.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtProceduresDone.DefaultText = ""
        Me.TxtProceduresDone.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TxtProceduresDone.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TxtProceduresDone.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtProceduresDone.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtProceduresDone.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtProceduresDone.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!)
        Me.TxtProceduresDone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtProceduresDone.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtProceduresDone.Location = New System.Drawing.Point(10, 217)
        Me.TxtProceduresDone.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.TxtProceduresDone.Multiline = True
        Me.TxtProceduresDone.Name = "TxtProceduresDone"
        Me.TxtProceduresDone.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtProceduresDone.PlaceholderText = "Procedures..."
        Me.TxtProceduresDone.SelectedText = ""
        Me.TxtProceduresDone.Size = New System.Drawing.Size(532, 104)
        Me.TxtProceduresDone.TabIndex = 4
        '
        'BtnSaveRecord
        '
        Me.BtnSaveRecord.BorderRadius = 10
        Me.BtnSaveRecord.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BtnSaveRecord.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BtnSaveRecord.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BtnSaveRecord.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BtnSaveRecord.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.BtnSaveRecord.Font = New System.Drawing.Font("Mongolian Baiti", 15.75!, System.Drawing.FontStyle.Bold)
        Me.BtnSaveRecord.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.BtnSaveRecord.Location = New System.Drawing.Point(25, 732)
        Me.BtnSaveRecord.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.BtnSaveRecord.Name = "BtnSaveRecord"
        Me.BtnSaveRecord.Size = New System.Drawing.Size(232, 45)
        Me.BtnSaveRecord.TabIndex = 7
        Me.BtnSaveRecord.Text = "Save"
        '
        'Guna2CirclePictureBox1
        '
        Me.Guna2CirclePictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2CirclePictureBox1.FillColor = System.Drawing.Color.Transparent
        Me.Guna2CirclePictureBox1.Image = CType(resources.GetObject("Guna2CirclePictureBox1.Image"), System.Drawing.Image)
        Me.Guna2CirclePictureBox1.ImageRotate = 0!
        Me.Guna2CirclePictureBox1.Location = New System.Drawing.Point(2395, 17)
        Me.Guna2CirclePictureBox1.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Guna2CirclePictureBox1.Name = "Guna2CirclePictureBox1"
        Me.Guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.Guna2CirclePictureBox1.Size = New System.Drawing.Size(80, 85)
        Me.Guna2CirclePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Guna2CirclePictureBox1.TabIndex = 21
        Me.Guna2CirclePictureBox1.TabStop = False
        '
        'Guna2HtmlLabel2
        '
        Me.Guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel2.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel2.Location = New System.Drawing.Point(25, 126)
        Me.Guna2HtmlLabel2.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Guna2HtmlLabel2.Name = "Guna2HtmlLabel2"
        Me.Guna2HtmlLabel2.Size = New System.Drawing.Size(122, 28)
        Me.Guna2HtmlLabel2.TabIndex = 29
        Me.Guna2HtmlLabel2.Text = "Patient Name:"
        '
        'Guna2HtmlLabel1
        '
        Me.Guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel1.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel1.Location = New System.Drawing.Point(25, 164)
        Me.Guna2HtmlLabel1.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Guna2HtmlLabel1.Name = "Guna2HtmlLabel1"
        Me.Guna2HtmlLabel1.Size = New System.Drawing.Size(124, 28)
        Me.Guna2HtmlLabel1.TabIndex = 30
        Me.Guna2HtmlLabel1.Text = "Dentist Name:"
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.FillColor = System.Drawing.Color.LightGray
        Me.btnBack.Image = CType(resources.GetObject("btnBack.Image"), System.Drawing.Image)
        Me.btnBack.ImageRotate = 0!
        Me.btnBack.Location = New System.Drawing.Point(1594, 25)
        Me.btnBack.Margin = New System.Windows.Forms.Padding(5)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.btnBack.Size = New System.Drawing.Size(60, 58)
        Me.btnBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnBack.TabIndex = 32
        Me.btnBack.TabStop = False
        '
        'Guna2CustomGradientPanel1
        '
        Me.Guna2CustomGradientPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2CustomGradientPanel1.Controls.Add(Me.btnBack1)
        Me.Guna2CustomGradientPanel1.Controls.Add(Me.btnBack)
        Me.Guna2CustomGradientPanel1.Controls.Add(Me.Guna2HtmlLabel6)
        Me.Guna2CustomGradientPanel1.FillColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.FillColor3 = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.FillColor4 = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.Location = New System.Drawing.Point(0, 0)
        Me.Guna2CustomGradientPanel1.Name = "Guna2CustomGradientPanel1"
        Me.Guna2CustomGradientPanel1.Size = New System.Drawing.Size(1680, 107)
        Me.Guna2CustomGradientPanel1.TabIndex = 47
        '
        'btnBack1
        '
        Me.btnBack1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack1.BackColor = System.Drawing.Color.Transparent
        Me.btnBack1.FillColor = System.Drawing.Color.Transparent
        Me.btnBack1.Image = CType(resources.GetObject("btnBack1.Image"), System.Drawing.Image)
        Me.btnBack1.ImageRotate = 0!
        Me.btnBack1.Location = New System.Drawing.Point(1254, 23)
        Me.btnBack1.Name = "btnBack1"
        Me.btnBack1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.btnBack1.Size = New System.Drawing.Size(60, 58)
        Me.btnBack1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnBack1.TabIndex = 48
        Me.btnBack1.TabStop = False
        '
        'Guna2HtmlLabel6
        '
        Me.Guna2HtmlLabel6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2HtmlLabel6.AutoSize = False
        Me.Guna2HtmlLabel6.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel6.Font = New System.Drawing.Font("Mongolian Baiti", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.Guna2HtmlLabel6.Location = New System.Drawing.Point(0, 0)
        Me.Guna2HtmlLabel6.Name = "Guna2HtmlLabel6"
        Me.Guna2HtmlLabel6.Size = New System.Drawing.Size(1342, 107)
        Me.Guna2HtmlLabel6.TabIndex = 47
        Me.Guna2HtmlLabel6.Text = "Treatment Record"
        Me.Guna2HtmlLabel6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.btnClear.Location = New System.Drawing.Point(675, 732)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(232, 45)
        Me.btnClear.TabIndex = 63
        Me.btnClear.Text = "Clear"
        '
        'lblPatientName
        '
        Me.lblPatientName.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientName.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.lblPatientName.Location = New System.Drawing.Point(161, 126)
        Me.lblPatientName.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.lblPatientName.Name = "lblPatientName"
        Me.lblPatientName.Size = New System.Drawing.Size(88, 28)
        Me.lblPatientName.TabIndex = 29
        Me.lblPatientName.Text = "Patient ID"
        '
        'lblDentistName
        '
        Me.lblDentistName.BackColor = System.Drawing.Color.Transparent
        Me.lblDentistName.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDentistName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.lblDentistName.Location = New System.Drawing.Point(161, 164)
        Me.lblDentistName.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.lblDentistName.Name = "lblDentistName"
        Me.lblDentistName.Size = New System.Drawing.Size(90, 28)
        Me.lblDentistName.TabIndex = 30
        Me.lblDentistName.Text = "Dentist ID"
        '
        'Guna2HtmlLabel3
        '
        Me.Guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel3.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel3.Location = New System.Drawing.Point(675, 126)
        Me.Guna2HtmlLabel3.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Guna2HtmlLabel3.Name = "Guna2HtmlLabel3"
        Me.Guna2HtmlLabel3.Size = New System.Drawing.Size(58, 28)
        Me.Guna2HtmlLabel3.TabIndex = 64
        Me.Guna2HtmlLabel3.Text = "Status:"
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblStatus.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.lblStatus.Location = New System.Drawing.Point(739, 126)
        Me.lblStatus.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(74, 28)
        Me.lblStatus.TabIndex = 65
        Me.lblStatus.Text = "lblStatus"
        '
        'Guna2HtmlLabel4
        '
        Me.Guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel4.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel4.Location = New System.Drawing.Point(10, 43)
        Me.Guna2HtmlLabel4.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Guna2HtmlLabel4.Name = "Guna2HtmlLabel4"
        Me.Guna2HtmlLabel4.Size = New System.Drawing.Size(149, 28)
        Me.Guna2HtmlLabel4.TabIndex = 66
        Me.Guna2HtmlLabel4.Text = "Treatment Notes:"
        '
        'Guna2HtmlLabel5
        '
        Me.Guna2HtmlLabel5.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel5.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel5.Location = New System.Drawing.Point(11, 188)
        Me.Guna2HtmlLabel5.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Guna2HtmlLabel5.Name = "Guna2HtmlLabel5"
        Me.Guna2HtmlLabel5.Size = New System.Drawing.Size(150, 28)
        Me.Guna2HtmlLabel5.TabIndex = 67
        Me.Guna2HtmlLabel5.Text = "Procedures Done:"
        '
        'Guna2HtmlLabel7
        '
        Me.Guna2HtmlLabel7.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel7.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel7.Location = New System.Drawing.Point(11, 338)
        Me.Guna2HtmlLabel7.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Guna2HtmlLabel7.Name = "Guna2HtmlLabel7"
        Me.Guna2HtmlLabel7.Size = New System.Drawing.Size(115, 28)
        Me.Guna2HtmlLabel7.TabIndex = 68
        Me.Guna2HtmlLabel7.Text = "Prescriptions:"
        '
        'TxtTreatmentNotes
        '
        Me.TxtTreatmentNotes.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtTreatmentNotes.BorderRadius = 10
        Me.TxtTreatmentNotes.BorderThickness = 2
        Me.TxtTreatmentNotes.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtTreatmentNotes.DefaultText = ""
        Me.TxtTreatmentNotes.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TxtTreatmentNotes.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TxtTreatmentNotes.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtTreatmentNotes.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtTreatmentNotes.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtTreatmentNotes.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!)
        Me.TxtTreatmentNotes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtTreatmentNotes.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtTreatmentNotes.Location = New System.Drawing.Point(9, 72)
        Me.TxtTreatmentNotes.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.TxtTreatmentNotes.Multiline = True
        Me.TxtTreatmentNotes.Name = "TxtTreatmentNotes"
        Me.TxtTreatmentNotes.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtTreatmentNotes.PlaceholderText = "Notes..."
        Me.TxtTreatmentNotes.SelectedText = ""
        Me.TxtTreatmentNotes.Size = New System.Drawing.Size(532, 104)
        Me.TxtTreatmentNotes.TabIndex = 2
        '
        'dtpFollowUpDate
        '
        Me.dtpFollowUpDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.dtpFollowUpDate.BorderRadius = 10
        Me.dtpFollowUpDate.BorderThickness = 2
        Me.dtpFollowUpDate.Checked = True
        Me.dtpFollowUpDate.FillColor = System.Drawing.Color.White
        Me.dtpFollowUpDate.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFollowUpDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.dtpFollowUpDate.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.dtpFollowUpDate.Location = New System.Drawing.Point(31, 232)
        Me.dtpFollowUpDate.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpFollowUpDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpFollowUpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFollowUpDate.Name = "dtpFollowUpDate"
        Me.dtpFollowUpDate.Size = New System.Drawing.Size(365, 46)
        Me.dtpFollowUpDate.TabIndex = 70
        Me.dtpFollowUpDate.Value = New Date(2025, 12, 18, 11, 1, 1, 395)
        '
        'txtFollowUpReason
        '
        Me.txtFollowUpReason.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtFollowUpReason.BorderRadius = 10
        Me.txtFollowUpReason.BorderThickness = 2
        Me.txtFollowUpReason.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFollowUpReason.DefaultText = ""
        Me.txtFollowUpReason.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtFollowUpReason.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtFollowUpReason.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtFollowUpReason.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtFollowUpReason.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtFollowUpReason.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!)
        Me.txtFollowUpReason.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtFollowUpReason.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtFollowUpReason.Location = New System.Drawing.Point(31, 343)
        Me.txtFollowUpReason.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtFollowUpReason.Multiline = True
        Me.txtFollowUpReason.Name = "txtFollowUpReason"
        Me.txtFollowUpReason.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtFollowUpReason.PlaceholderText = "Reason..."
        Me.txtFollowUpReason.SelectedText = ""
        Me.txtFollowUpReason.Size = New System.Drawing.Size(532, 104)
        Me.txtFollowUpReason.TabIndex = 90
        '
        'chkNeedsFollowUp
        '
        Me.chkNeedsFollowUp.AutoSize = True
        Me.chkNeedsFollowUp.Font = New System.Drawing.Font("Palatino Linotype", 14.25!)
        Me.chkNeedsFollowUp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.chkNeedsFollowUp.Location = New System.Drawing.Point(31, 130)
        Me.chkNeedsFollowUp.Name = "chkNeedsFollowUp"
        Me.chkNeedsFollowUp.Size = New System.Drawing.Size(310, 30)
        Me.chkNeedsFollowUp.TabIndex = 69
        Me.chkNeedsFollowUp.Text = "Schedule Follow-up Appointment"
        Me.chkNeedsFollowUp.UseVisualStyleBackColor = True
        '
        'grpFollowup
        '
        Me.grpFollowup.Controls.Add(Me.Guna2HtmlLabel11)
        Me.grpFollowup.Controls.Add(Me.Guna2HtmlLabel10)
        Me.grpFollowup.Controls.Add(Me.chkNeedsFollowUp)
        Me.grpFollowup.Controls.Add(Me.txtFollowUpReason)
        Me.grpFollowup.Controls.Add(Me.dtpFollowUpDate)
        Me.grpFollowup.Font = New System.Drawing.Font("Palatino Linotype", 14.25!)
        Me.grpFollowup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.grpFollowup.Location = New System.Drawing.Point(675, 200)
        Me.grpFollowup.Name = "grpFollowup"
        Me.grpFollowup.Size = New System.Drawing.Size(652, 509)
        Me.grpFollowup.TabIndex = 91
        Me.grpFollowup.TabStop = False
        Me.grpFollowup.Text = "Follow-up Options"
        '
        'Guna2HtmlLabel11
        '
        Me.Guna2HtmlLabel11.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel11.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel11.Location = New System.Drawing.Point(31, 195)
        Me.Guna2HtmlLabel11.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Guna2HtmlLabel11.Name = "Guna2HtmlLabel11"
        Me.Guna2HtmlLabel11.Size = New System.Drawing.Size(136, 28)
        Me.Guna2HtmlLabel11.TabIndex = 92
        Me.Guna2HtmlLabel11.Text = "Follow-up Date:"
        '
        'Guna2HtmlLabel10
        '
        Me.Guna2HtmlLabel10.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel10.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel10.Location = New System.Drawing.Point(31, 303)
        Me.Guna2HtmlLabel10.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Guna2HtmlLabel10.Name = "Guna2HtmlLabel10"
        Me.Guna2HtmlLabel10.Size = New System.Drawing.Size(68, 28)
        Me.Guna2HtmlLabel10.TabIndex = 91
        Me.Guna2HtmlLabel10.Text = "Reason:"
        '
        'Guna2HtmlLabel8
        '
        Me.Guna2HtmlLabel8.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel8.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel8.Location = New System.Drawing.Point(31, 119)
        Me.Guna2HtmlLabel8.Name = "Guna2HtmlLabel8"
        Me.Guna2HtmlLabel8.Size = New System.Drawing.Size(136, 28)
        Me.Guna2HtmlLabel8.TabIndex = 88
        Me.Guna2HtmlLabel8.Text = "Follow-up Date:"
        '
        'Guna2HtmlLabel9
        '
        Me.Guna2HtmlLabel9.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel9.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel9.Location = New System.Drawing.Point(31, 270)
        Me.Guna2HtmlLabel9.Name = "Guna2HtmlLabel9"
        Me.Guna2HtmlLabel9.Size = New System.Drawing.Size(68, 28)
        Me.Guna2HtmlLabel9.TabIndex = 89
        Me.Guna2HtmlLabel9.Text = "Reason:"
        '
        'grpTreatment
        '
        Me.grpTreatment.Controls.Add(Me.Guna2HtmlLabel4)
        Me.grpTreatment.Controls.Add(Me.TxtTreatmentNotes)
        Me.grpTreatment.Controls.Add(Me.Guna2HtmlLabel7)
        Me.grpTreatment.Controls.Add(Me.TxtPrescriptions)
        Me.grpTreatment.Controls.Add(Me.Guna2HtmlLabel5)
        Me.grpTreatment.Controls.Add(Me.TxtProceduresDone)
        Me.grpTreatment.Font = New System.Drawing.Font("Palatino Linotype", 14.25!)
        Me.grpTreatment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.grpTreatment.Location = New System.Drawing.Point(12, 200)
        Me.grpTreatment.Name = "grpTreatment"
        Me.grpTreatment.Size = New System.Drawing.Size(620, 509)
        Me.grpTreatment.TabIndex = 92
        Me.grpTreatment.TabStop = False
        Me.grpTreatment.Text = "Treatment Details"
        '
        'AdminDBTreatmentRecords
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1339, 833)
        Me.ControlBox = False
        Me.Controls.Add(Me.grpTreatment)
        Me.Controls.Add(Me.grpFollowup)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.Guna2HtmlLabel3)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.Guna2CustomGradientPanel1)
        Me.Controls.Add(Me.lblDentistName)
        Me.Controls.Add(Me.lblPatientName)
        Me.Controls.Add(Me.Guna2HtmlLabel1)
        Me.Controls.Add(Me.Guna2HtmlLabel2)
        Me.Controls.Add(Me.Guna2CirclePictureBox1)
        Me.Controls.Add(Me.BtnSaveRecord)
        Me.Font = New System.Drawing.Font("Microsoft Yi Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Name = "AdminDBTreatmentRecords"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TreatmentRecords"
        CType(Me.Guna2CirclePictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Guna2CustomGradientPanel1.ResumeLayout(False)
        CType(Me.btnBack1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFollowup.ResumeLayout(False)
        Me.grpFollowup.PerformLayout()
        Me.grpTreatment.ResumeLayout(False)
        Me.grpTreatment.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TxtPrescriptions As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents TxtProceduresDone As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents BtnSaveRecord As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Guna2CirclePictureBox1 As Guna.UI2.WinForms.Guna2CirclePictureBox
    Friend WithEvents Guna2HtmlLabel2 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel1 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents btnBack As Guna.UI2.WinForms.Guna2CirclePictureBox
    Friend WithEvents Guna2CustomGradientPanel1 As Guna.UI2.WinForms.Guna2CustomGradientPanel
    Friend WithEvents Guna2HtmlLabel6 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents btnBack1 As Guna.UI2.WinForms.Guna2CirclePictureBox
    Friend WithEvents btnClear As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents lblPatientName As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents lblDentistName As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel3 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents lblStatus As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel4 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel5 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel7 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents TxtTreatmentNotes As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents dtpFollowUpDate As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents txtFollowUpReason As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents chkNeedsFollowUp As CheckBox
    Friend WithEvents grpFollowup As GroupBox
    Friend WithEvents Guna2HtmlLabel8 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel9 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel11 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel10 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents grpTreatment As GroupBox
End Class
