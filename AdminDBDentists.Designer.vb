<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdminDBDentists
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdminDBDentists))
        Me.TxtName = New Guna.UI2.WinForms.Guna2TextBox()
        Me.DGVDentists = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.Guna2CirclePictureBox1 = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.TxtUsername = New Guna.UI2.WinForms.Guna2TextBox()
        Me.TxtPhone = New Guna.UI2.WinForms.Guna2TextBox()
        Me.TxtEmail = New Guna.UI2.WinForms.Guna2TextBox()
        Me.TxtSpecialization = New Guna.UI2.WinForms.Guna2TextBox()
        Me.cmbAvailability = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.TxtPassword = New Guna.UI2.WinForms.Guna2TextBox()
        Me.TxtConfirmPassword = New Guna.UI2.WinForms.Guna2TextBox()
        Me.BTNAdd = New Guna.UI2.WinForms.Guna2Button()
        CType(Me.DGVDentists, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Guna2CirclePictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtName
        '
        Me.TxtName.BorderRadius = 10
        Me.TxtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtName.DefaultText = ""
        Me.TxtName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TxtName.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TxtName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtName.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtName.ForeColor = System.Drawing.Color.Black
        Me.TxtName.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtName.Location = New System.Drawing.Point(61, 76)
        Me.TxtName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TxtName.Name = "TxtName"
        Me.TxtName.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.TxtName.PlaceholderText = "Full name"
        Me.TxtName.SelectedText = ""
        Me.TxtName.Size = New System.Drawing.Size(257, 50)
        Me.TxtName.TabIndex = 0
        '
        'DGVDentists
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.DGVDentists.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVDentists.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGVDentists.ColumnHeadersHeight = 4
        Me.DGVDentists.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVDentists.DefaultCellStyle = DataGridViewCellStyle3
        Me.DGVDentists.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVDentists.Location = New System.Drawing.Point(430, 135)
        Me.DGVDentists.Name = "DGVDentists"
        Me.DGVDentists.RowHeadersVisible = False
        Me.DGVDentists.Size = New System.Drawing.Size(843, 492)
        Me.DGVDentists.TabIndex = 9
        Me.DGVDentists.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVDentists.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.DGVDentists.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.DGVDentists.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.DGVDentists.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.DGVDentists.ThemeStyle.BackColor = System.Drawing.Color.White
        Me.DGVDentists.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVDentists.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVDentists.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DGVDentists.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVDentists.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White
        Me.DGVDentists.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        Me.DGVDentists.ThemeStyle.HeaderStyle.Height = 4
        Me.DGVDentists.ThemeStyle.ReadOnly = False
        Me.DGVDentists.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVDentists.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DGVDentists.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVDentists.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.DGVDentists.ThemeStyle.RowsStyle.Height = 22
        Me.DGVDentists.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVDentists.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        '
        'Guna2CirclePictureBox1
        '
        Me.Guna2CirclePictureBox1.FillColor = System.Drawing.Color.Transparent
        Me.Guna2CirclePictureBox1.Image = CType(resources.GetObject("Guna2CirclePictureBox1.Image"), System.Drawing.Image)
        Me.Guna2CirclePictureBox1.ImageRotate = 0!
        Me.Guna2CirclePictureBox1.Location = New System.Drawing.Point(1290, 12)
        Me.Guna2CirclePictureBox1.Name = "Guna2CirclePictureBox1"
        Me.Guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.Guna2CirclePictureBox1.Size = New System.Drawing.Size(60, 58)
        Me.Guna2CirclePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Guna2CirclePictureBox1.TabIndex = 19
        Me.Guna2CirclePictureBox1.TabStop = False
        '
        'TxtUsername
        '
        Me.TxtUsername.BorderRadius = 10
        Me.TxtUsername.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtUsername.DefaultText = ""
        Me.TxtUsername.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TxtUsername.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TxtUsername.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtUsername.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtUsername.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtUsername.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.TxtUsername.ForeColor = System.Drawing.Color.Black
        Me.TxtUsername.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtUsername.Location = New System.Drawing.Point(61, 158)
        Me.TxtUsername.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TxtUsername.Name = "TxtUsername"
        Me.TxtUsername.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.TxtUsername.PlaceholderText = "Username"
        Me.TxtUsername.SelectedText = ""
        Me.TxtUsername.Size = New System.Drawing.Size(257, 50)
        Me.TxtUsername.TabIndex = 20
        '
        'TxtPhone
        '
        Me.TxtPhone.BorderRadius = 10
        Me.TxtPhone.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtPhone.DefaultText = ""
        Me.TxtPhone.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TxtPhone.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TxtPhone.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtPhone.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtPhone.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtPhone.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.TxtPhone.ForeColor = System.Drawing.Color.Black
        Me.TxtPhone.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtPhone.Location = New System.Drawing.Point(61, 240)
        Me.TxtPhone.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TxtPhone.Name = "TxtPhone"
        Me.TxtPhone.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.TxtPhone.PlaceholderText = "Phone number"
        Me.TxtPhone.SelectedText = ""
        Me.TxtPhone.Size = New System.Drawing.Size(257, 50)
        Me.TxtPhone.TabIndex = 21
        '
        'TxtEmail
        '
        Me.TxtEmail.BorderRadius = 10
        Me.TxtEmail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtEmail.DefaultText = ""
        Me.TxtEmail.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TxtEmail.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TxtEmail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtEmail.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtEmail.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtEmail.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.TxtEmail.ForeColor = System.Drawing.Color.Black
        Me.TxtEmail.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtEmail.Location = New System.Drawing.Point(61, 317)
        Me.TxtEmail.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TxtEmail.Name = "TxtEmail"
        Me.TxtEmail.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.TxtEmail.PlaceholderText = "Email"
        Me.TxtEmail.SelectedText = ""
        Me.TxtEmail.Size = New System.Drawing.Size(257, 50)
        Me.TxtEmail.TabIndex = 22
        '
        'TxtSpecialization
        '
        Me.TxtSpecialization.BorderRadius = 10
        Me.TxtSpecialization.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtSpecialization.DefaultText = ""
        Me.TxtSpecialization.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TxtSpecialization.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TxtSpecialization.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtSpecialization.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtSpecialization.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtSpecialization.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.TxtSpecialization.ForeColor = System.Drawing.Color.Black
        Me.TxtSpecialization.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtSpecialization.Location = New System.Drawing.Point(61, 381)
        Me.TxtSpecialization.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TxtSpecialization.Name = "TxtSpecialization"
        Me.TxtSpecialization.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.TxtSpecialization.PlaceholderText = "Specialization"
        Me.TxtSpecialization.SelectedText = ""
        Me.TxtSpecialization.Size = New System.Drawing.Size(257, 50)
        Me.TxtSpecialization.TabIndex = 23
        '
        'cmbAvailability
        '
        Me.cmbAvailability.BackColor = System.Drawing.Color.Transparent
        Me.cmbAvailability.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbAvailability.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAvailability.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbAvailability.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbAvailability.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.cmbAvailability.ForeColor = System.Drawing.Color.Gray
        Me.cmbAvailability.ItemHeight = 30
        Me.cmbAvailability.Items.AddRange(New Object() {"Full-time", "Part-time", "Morning Shift", "Afternoon Shift"})
        Me.cmbAvailability.Location = New System.Drawing.Point(61, 451)
        Me.cmbAvailability.Name = "cmbAvailability"
        Me.cmbAvailability.Size = New System.Drawing.Size(257, 36)
        Me.cmbAvailability.TabIndex = 24
        '
        'TxtPassword
        '
        Me.TxtPassword.BorderRadius = 10
        Me.TxtPassword.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtPassword.DefaultText = ""
        Me.TxtPassword.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TxtPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TxtPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtPassword.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtPassword.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtPassword.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.TxtPassword.ForeColor = System.Drawing.Color.Black
        Me.TxtPassword.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtPassword.Location = New System.Drawing.Point(61, 494)
        Me.TxtPassword.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TxtPassword.Name = "TxtPassword"
        Me.TxtPassword.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.TxtPassword.PlaceholderText = "Password"
        Me.TxtPassword.SelectedText = ""
        Me.TxtPassword.Size = New System.Drawing.Size(257, 50)
        Me.TxtPassword.TabIndex = 25
        '
        'TxtConfirmPassword
        '
        Me.TxtConfirmPassword.BorderRadius = 10
        Me.TxtConfirmPassword.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtConfirmPassword.DefaultText = ""
        Me.TxtConfirmPassword.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TxtConfirmPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TxtConfirmPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtConfirmPassword.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtConfirmPassword.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtConfirmPassword.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.TxtConfirmPassword.ForeColor = System.Drawing.Color.Black
        Me.TxtConfirmPassword.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtConfirmPassword.Location = New System.Drawing.Point(61, 557)
        Me.TxtConfirmPassword.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TxtConfirmPassword.Name = "TxtConfirmPassword"
        Me.TxtConfirmPassword.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.TxtConfirmPassword.PlaceholderText = "Confirm password"
        Me.TxtConfirmPassword.SelectedText = ""
        Me.TxtConfirmPassword.Size = New System.Drawing.Size(257, 50)
        Me.TxtConfirmPassword.TabIndex = 26
        '
        'BTNAdd
        '
        Me.BTNAdd.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BTNAdd.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BTNAdd.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BTNAdd.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BTNAdd.FillColor = System.Drawing.Color.White
        Me.BTNAdd.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.BTNAdd.ForeColor = System.Drawing.Color.Gray
        Me.BTNAdd.Location = New System.Drawing.Point(109, 641)
        Me.BTNAdd.Name = "BTNAdd"
        Me.BTNAdd.Size = New System.Drawing.Size(180, 45)
        Me.BTNAdd.TabIndex = 27
        Me.BTNAdd.Text = "Add"
        '
        'AdminDBDentists
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1362, 727)
        Me.Controls.Add(Me.BTNAdd)
        Me.Controls.Add(Me.TxtConfirmPassword)
        Me.Controls.Add(Me.TxtPassword)
        Me.Controls.Add(Me.cmbAvailability)
        Me.Controls.Add(Me.TxtSpecialization)
        Me.Controls.Add(Me.TxtEmail)
        Me.Controls.Add(Me.TxtPhone)
        Me.Controls.Add(Me.TxtUsername)
        Me.Controls.Add(Me.Guna2CirclePictureBox1)
        Me.Controls.Add(Me.DGVDentists)
        Me.Controls.Add(Me.TxtName)
        Me.Name = "AdminDBDentists"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AdminDBDentists"
        CType(Me.DGVDentists, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Guna2CirclePictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TxtName As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents DGVDentists As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents Guna2CirclePictureBox1 As Guna.UI2.WinForms.Guna2CirclePictureBox
    Friend WithEvents TxtUsername As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents TxtPhone As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents TxtEmail As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents TxtSpecialization As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents cmbAvailability As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents TxtPassword As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents TxtConfirmPassword As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents BTNAdd As Guna.UI2.WinForms.Guna2Button
End Class
