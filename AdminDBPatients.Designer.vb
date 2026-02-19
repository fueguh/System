<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdminDBPatients
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdminDBPatients))
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.txtFullName = New Guna.UI2.WinForms.Guna2TextBox()
        Me.dtpBirthDate = New Guna.UI2.WinForms.Guna2DateTimePicker()
        Me.txtContact = New Guna.UI2.WinForms.Guna2TextBox()
        Me.txtEmail = New Guna.UI2.WinForms.Guna2TextBox()
        Me.txtAddress = New Guna.UI2.WinForms.Guna2TextBox()
        Me.BTNAdd = New Guna.UI2.WinForms.Guna2Button()
        Me.BTNUpdate = New Guna.UI2.WinForms.Guna2Button()
        Me.BTNDelete = New Guna.UI2.WinForms.Guna2Button()
        Me.Guna2CirclePictureBox1 = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.DGVPatients = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.Guna2TextBox1 = New Guna.UI2.WinForms.Guna2TextBox()
        Me.Guna2HtmlLabel3 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2CustomGradientPanel1 = New Guna.UI2.WinForms.Guna2CustomGradientPanel()
        Me.Guna2HtmlLabel6 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.txtAllergy = New Guna.UI2.WinForms.Guna2TextBox()
        CType(Me.Guna2CirclePictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVPatients, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Guna2CustomGradientPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtFullName
        '
        Me.txtFullName.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtFullName.BorderRadius = 10
        Me.txtFullName.BorderThickness = 2
        Me.txtFullName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFullName.DefaultText = ""
        Me.txtFullName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtFullName.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtFullName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtFullName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtFullName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtFullName.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFullName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtFullName.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtFullName.Location = New System.Drawing.Point(32, 155)
        Me.txtFullName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtFullName.Name = "txtFullName"
        Me.txtFullName.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtFullName.PlaceholderText = "Full name"
        Me.txtFullName.SelectedText = ""
        Me.txtFullName.Size = New System.Drawing.Size(351, 50)
        Me.txtFullName.TabIndex = 0
        '
        'dtpBirthDate
        '
        Me.dtpBirthDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.dtpBirthDate.BorderRadius = 10
        Me.dtpBirthDate.BorderThickness = 2
        Me.dtpBirthDate.Checked = True
        Me.dtpBirthDate.FillColor = System.Drawing.Color.White
        Me.dtpBirthDate.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpBirthDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.dtpBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.dtpBirthDate.Location = New System.Drawing.Point(423, 155)
        Me.dtpBirthDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpBirthDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpBirthDate.Name = "dtpBirthDate"
        Me.dtpBirthDate.Size = New System.Drawing.Size(351, 50)
        Me.dtpBirthDate.TabIndex = 1
        Me.dtpBirthDate.Value = New Date(2025, 12, 18, 12, 14, 7, 394)
        '
        'txtContact
        '
        Me.txtContact.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtContact.BorderRadius = 10
        Me.txtContact.BorderThickness = 2
        Me.txtContact.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtContact.DefaultText = ""
        Me.txtContact.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtContact.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtContact.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtContact.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtContact.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtContact.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContact.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtContact.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtContact.Location = New System.Drawing.Point(816, 155)
        Me.txtContact.Margin = New System.Windows.Forms.Padding(4)
        Me.txtContact.MaxLength = 11
        Me.txtContact.Name = "txtContact"
        Me.txtContact.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtContact.PlaceholderText = "Contact no."
        Me.txtContact.SelectedText = ""
        Me.txtContact.Size = New System.Drawing.Size(351, 50)
        Me.txtContact.TabIndex = 2
        '
        'txtEmail
        '
        Me.txtEmail.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtEmail.BorderRadius = 10
        Me.txtEmail.BorderThickness = 2
        Me.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmail.DefaultText = ""
        Me.txtEmail.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtEmail.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtEmail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtEmail.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtEmail.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtEmail.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtEmail.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtEmail.Location = New System.Drawing.Point(32, 231)
        Me.txtEmail.Margin = New System.Windows.Forms.Padding(4)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtEmail.PlaceholderText = "Email"
        Me.txtEmail.SelectedText = ""
        Me.txtEmail.Size = New System.Drawing.Size(351, 50)
        Me.txtEmail.TabIndex = 3
        '
        'txtAddress
        '
        Me.txtAddress.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtAddress.BorderRadius = 10
        Me.txtAddress.BorderThickness = 2
        Me.txtAddress.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAddress.DefaultText = ""
        Me.txtAddress.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtAddress.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtAddress.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtAddress.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtAddress.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAddress.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!)
        Me.txtAddress.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtAddress.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAddress.Location = New System.Drawing.Point(423, 231)
        Me.txtAddress.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtAddress.PlaceholderText = "Address"
        Me.txtAddress.SelectedText = ""
        Me.txtAddress.Size = New System.Drawing.Size(351, 50)
        Me.txtAddress.TabIndex = 4
        '
        'BTNAdd
        '
        Me.BTNAdd.BorderRadius = 10
        Me.BTNAdd.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BTNAdd.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BTNAdd.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BTNAdd.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BTNAdd.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.BTNAdd.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNAdd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.BTNAdd.Location = New System.Drawing.Point(1178, 155)
        Me.BTNAdd.Name = "BTNAdd"
        Me.BTNAdd.Size = New System.Drawing.Size(92, 50)
        Me.BTNAdd.TabIndex = 5
        Me.BTNAdd.Text = "Add"
        '
        'BTNUpdate
        '
        Me.BTNUpdate.BorderRadius = 10
        Me.BTNUpdate.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BTNUpdate.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BTNUpdate.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BTNUpdate.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BTNUpdate.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.BTNUpdate.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNUpdate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.BTNUpdate.Location = New System.Drawing.Point(1276, 155)
        Me.BTNUpdate.Name = "BTNUpdate"
        Me.BTNUpdate.Size = New System.Drawing.Size(92, 50)
        Me.BTNUpdate.TabIndex = 6
        Me.BTNUpdate.Text = "Update"
        '
        'BTNDelete
        '
        Me.BTNDelete.BorderRadius = 10
        Me.BTNDelete.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BTNDelete.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BTNDelete.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BTNDelete.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BTNDelete.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.BTNDelete.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNDelete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.BTNDelete.Location = New System.Drawing.Point(1221, 231)
        Me.BTNDelete.Name = "BTNDelete"
        Me.BTNDelete.Size = New System.Drawing.Size(92, 50)
        Me.BTNDelete.TabIndex = 7
        Me.BTNDelete.Text = "Delete"
        '
        'Guna2CirclePictureBox1
        '
        Me.Guna2CirclePictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2CirclePictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2CirclePictureBox1.FillColor = System.Drawing.Color.Transparent
        Me.Guna2CirclePictureBox1.Image = CType(resources.GetObject("Guna2CirclePictureBox1.Image"), System.Drawing.Image)
        Me.Guna2CirclePictureBox1.ImageRotate = 0!
        Me.Guna2CirclePictureBox1.Location = New System.Drawing.Point(1290, 24)
        Me.Guna2CirclePictureBox1.Name = "Guna2CirclePictureBox1"
        Me.Guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.Guna2CirclePictureBox1.Size = New System.Drawing.Size(60, 58)
        Me.Guna2CirclePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Guna2CirclePictureBox1.TabIndex = 19
        Me.Guna2CirclePictureBox1.TabStop = False
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
        Me.DGVPatients.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVPatients.Location = New System.Drawing.Point(0, 382)
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
        Me.DGVPatients.Size = New System.Drawing.Size(1382, 501)
        Me.DGVPatients.TabIndex = 33
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
        'Guna2TextBox1
        '
        Me.Guna2TextBox1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2TextBox1.BorderRadius = 10
        Me.Guna2TextBox1.BorderThickness = 2
        Me.Guna2TextBox1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Guna2TextBox1.DefaultText = ""
        Me.Guna2TextBox1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.Guna2TextBox1.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.Guna2TextBox1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.Guna2TextBox1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.Guna2TextBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Guna2TextBox1.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!)
        Me.Guna2TextBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2TextBox1.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Guna2TextBox1.Location = New System.Drawing.Point(13, 325)
        Me.Guna2TextBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.Guna2TextBox1.Name = "Guna2TextBox1"
        Me.Guna2TextBox1.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2TextBox1.PlaceholderText = "Search patient.."
        Me.Guna2TextBox1.SelectedText = ""
        Me.Guna2TextBox1.Size = New System.Drawing.Size(530, 50)
        Me.Guna2TextBox1.TabIndex = 34
        '
        'Guna2HtmlLabel3
        '
        Me.Guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel3.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel3.Location = New System.Drawing.Point(423, 125)
        Me.Guna2HtmlLabel3.Name = "Guna2HtmlLabel3"
        Me.Guna2HtmlLabel3.Size = New System.Drawing.Size(89, 28)
        Me.Guna2HtmlLabel3.TabIndex = 43
        Me.Guna2HtmlLabel3.Text = "BirthDate:"
        '
        'Guna2CustomGradientPanel1
        '
        Me.Guna2CustomGradientPanel1.Controls.Add(Me.Guna2CirclePictureBox1)
        Me.Guna2CustomGradientPanel1.Controls.Add(Me.Guna2HtmlLabel6)
        Me.Guna2CustomGradientPanel1.FillColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.FillColor3 = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.FillColor4 = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.Location = New System.Drawing.Point(0, 0)
        Me.Guna2CustomGradientPanel1.Name = "Guna2CustomGradientPanel1"
        Me.Guna2CustomGradientPanel1.Size = New System.Drawing.Size(1382, 107)
        Me.Guna2CustomGradientPanel1.TabIndex = 49
        '
        'Guna2HtmlLabel6
        '
        Me.Guna2HtmlLabel6.AutoSize = False
        Me.Guna2HtmlLabel6.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel6.Font = New System.Drawing.Font("Mongolian Baiti", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.Guna2HtmlLabel6.Location = New System.Drawing.Point(0, -1)
        Me.Guna2HtmlLabel6.Name = "Guna2HtmlLabel6"
        Me.Guna2HtmlLabel6.Size = New System.Drawing.Size(1379, 107)
        Me.Guna2HtmlLabel6.TabIndex = 47
        Me.Guna2HtmlLabel6.Text = "Patient Maintenance"
        Me.Guna2HtmlLabel6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtAllergy
        '
        Me.txtAllergy.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtAllergy.BorderRadius = 10
        Me.txtAllergy.BorderThickness = 2
        Me.txtAllergy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAllergy.DefaultText = ""
        Me.txtAllergy.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtAllergy.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtAllergy.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtAllergy.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtAllergy.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAllergy.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!)
        Me.txtAllergy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtAllergy.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAllergy.Location = New System.Drawing.Point(816, 231)
        Me.txtAllergy.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAllergy.Name = "txtAllergy"
        Me.txtAllergy.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtAllergy.PlaceholderText = "Note Allergy"
        Me.txtAllergy.SelectedText = ""
        Me.txtAllergy.Size = New System.Drawing.Size(351, 50)
        Me.txtAllergy.TabIndex = 50
        '
        'AdminDBPatients
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1379, 881)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtAllergy)
        Me.Controls.Add(Me.Guna2CustomGradientPanel1)
        Me.Controls.Add(Me.Guna2HtmlLabel3)
        Me.Controls.Add(Me.Guna2TextBox1)
        Me.Controls.Add(Me.DGVPatients)
        Me.Controls.Add(Me.BTNDelete)
        Me.Controls.Add(Me.BTNUpdate)
        Me.Controls.Add(Me.BTNAdd)
        Me.Controls.Add(Me.txtAddress)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.txtContact)
        Me.Controls.Add(Me.dtpBirthDate)
        Me.Controls.Add(Me.txtFullName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "AdminDBPatients"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AdminDBPatients"
        CType(Me.Guna2CirclePictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVPatients, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Guna2CustomGradientPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtFullName As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents dtpBirthDate As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents txtContact As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents txtEmail As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents txtAddress As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents BTNAdd As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents BTNUpdate As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents BTNDelete As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Guna2CirclePictureBox1 As Guna.UI2.WinForms.Guna2CirclePictureBox
    Friend WithEvents DGVPatients As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents Guna2TextBox1 As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Guna2HtmlLabel3 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2CustomGradientPanel1 As Guna.UI2.WinForms.Guna2CustomGradientPanel
    Friend WithEvents Guna2HtmlLabel6 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents txtAllergy As Guna.UI2.WinForms.Guna2TextBox
End Class
