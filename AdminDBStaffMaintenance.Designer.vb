<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AdminDBStaffMaintenance
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdminDBStaffMaintenance))
        Me.chkShowPassword = New Guna.UI2.WinForms.Guna2RadioButton()
        Me.SearchStaff = New Guna.UI2.WinForms.Guna2TextBox()
        Me.DgvStaffs = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.BTNAdd = New Guna.UI2.WinForms.Guna2Button()
        Me.TxtConfirmPassword = New Guna.UI2.WinForms.Guna2TextBox()
        Me.TxtPassword = New Guna.UI2.WinForms.Guna2TextBox()
        Me.TxtEmail = New Guna.UI2.WinForms.Guna2TextBox()
        Me.TxtPhone = New Guna.UI2.WinForms.Guna2TextBox()
        Me.TxtUsername = New Guna.UI2.WinForms.Guna2TextBox()
        Me.TxtName = New Guna.UI2.WinForms.Guna2TextBox()
        Me.Guna2CustomGradientPanel1 = New Guna.UI2.WinForms.Guna2CustomGradientPanel()
        Me.Guna2CirclePictureBox1 = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.Guna2HtmlLabel6 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        CType(Me.DgvStaffs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Guna2CustomGradientPanel1.SuspendLayout()
        CType(Me.Guna2CirclePictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkShowPassword
        '
        Me.chkShowPassword.AutoSize = True
        Me.chkShowPassword.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.chkShowPassword.CheckedState.BorderThickness = 0
        Me.chkShowPassword.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.chkShowPassword.CheckedState.InnerColor = System.Drawing.Color.White
        Me.chkShowPassword.CheckedState.InnerOffset = -4
        Me.chkShowPassword.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!)
        Me.chkShowPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.chkShowPassword.Location = New System.Drawing.Point(982, 170)
        Me.chkShowPassword.Name = "chkShowPassword"
        Me.chkShowPassword.Size = New System.Drawing.Size(126, 20)
        Me.chkShowPassword.TabIndex = 66
        Me.chkShowPassword.Text = "Show password"
        Me.chkShowPassword.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.chkShowPassword.UncheckedState.BorderThickness = 2
        Me.chkShowPassword.UncheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.chkShowPassword.UncheckedState.InnerColor = System.Drawing.Color.Transparent
        '
        'SearchStaff
        '
        Me.SearchStaff.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.SearchStaff.BorderRadius = 10
        Me.SearchStaff.BorderThickness = 2
        Me.SearchStaff.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.SearchStaff.DefaultText = ""
        Me.SearchStaff.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.SearchStaff.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.SearchStaff.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.SearchStaff.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.SearchStaff.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SearchStaff.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!)
        Me.SearchStaff.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.SearchStaff.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SearchStaff.Location = New System.Drawing.Point(13, 342)
        Me.SearchStaff.Margin = New System.Windows.Forms.Padding(4)
        Me.SearchStaff.Name = "SearchStaff"
        Me.SearchStaff.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.SearchStaff.PlaceholderText = "Search staff.."
        Me.SearchStaff.SelectedText = ""
        Me.SearchStaff.Size = New System.Drawing.Size(653, 50)
        Me.SearchStaff.TabIndex = 65
        '
        'DgvStaffs
        '
        Me.DgvStaffs.AllowUserToAddRows = False
        Me.DgvStaffs.AllowUserToDeleteRows = False
        Me.DgvStaffs.AllowUserToResizeColumns = False
        Me.DgvStaffs.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.DgvStaffs.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(122, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(178, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvStaffs.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DgvStaffs.ColumnHeadersHeight = 30
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvStaffs.DefaultCellStyle = DataGridViewCellStyle3
        Me.DgvStaffs.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DgvStaffs.Location = New System.Drawing.Point(-3, 399)
        Me.DgvStaffs.Name = "DgvStaffs"
        Me.DgvStaffs.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvStaffs.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DgvStaffs.RowHeadersVisible = False
        Me.DgvStaffs.RowHeadersWidth = 51
        Me.DgvStaffs.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.DgvStaffs.Size = New System.Drawing.Size(1583, 485)
        Me.DgvStaffs.TabIndex = 64
        Me.DgvStaffs.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White
        Me.DgvStaffs.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.DgvStaffs.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.DgvStaffs.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.DgvStaffs.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.DgvStaffs.ThemeStyle.BackColor = System.Drawing.Color.White
        Me.DgvStaffs.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DgvStaffs.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DgvStaffs.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DgvStaffs.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvStaffs.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White
        Me.DgvStaffs.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DgvStaffs.ThemeStyle.HeaderStyle.Height = 30
        Me.DgvStaffs.ThemeStyle.ReadOnly = True
        Me.DgvStaffs.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.DgvStaffs.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DgvStaffs.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvStaffs.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.DgvStaffs.ThemeStyle.RowsStyle.Height = 22
        Me.DgvStaffs.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DgvStaffs.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        '
        'BTNAdd
        '
        Me.BTNAdd.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.BTNAdd.BorderRadius = 10
        Me.BTNAdd.BorderThickness = 2
        Me.BTNAdd.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BTNAdd.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BTNAdd.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BTNAdd.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BTNAdd.FillColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.BTNAdd.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!)
        Me.BTNAdd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.BTNAdd.Location = New System.Drawing.Point(1387, 342)
        Me.BTNAdd.Name = "BTNAdd"
        Me.BTNAdd.Size = New System.Drawing.Size(180, 50)
        Me.BTNAdd.TabIndex = 62
        Me.BTNAdd.Text = "Save Staff"
        '
        'TxtConfirmPassword
        '
        Me.TxtConfirmPassword.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtConfirmPassword.BorderRadius = 10
        Me.TxtConfirmPassword.BorderThickness = 2
        Me.TxtConfirmPassword.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtConfirmPassword.DefaultText = ""
        Me.TxtConfirmPassword.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TxtConfirmPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TxtConfirmPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtConfirmPassword.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtConfirmPassword.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtConfirmPassword.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtConfirmPassword.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtConfirmPassword.Location = New System.Drawing.Point(686, 243)
        Me.TxtConfirmPassword.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TxtConfirmPassword.Name = "TxtConfirmPassword"
        Me.TxtConfirmPassword.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtConfirmPassword.PlaceholderText = "Confirm password"
        Me.TxtConfirmPassword.SelectedText = ""
        Me.TxtConfirmPassword.Size = New System.Drawing.Size(280, 50)
        Me.TxtConfirmPassword.TabIndex = 61
        Me.TxtConfirmPassword.UseSystemPasswordChar = True
        '
        'TxtPassword
        '
        Me.TxtPassword.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtPassword.BorderRadius = 10
        Me.TxtPassword.BorderThickness = 2
        Me.TxtPassword.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtPassword.DefaultText = ""
        Me.TxtPassword.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TxtPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TxtPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtPassword.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtPassword.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtPassword.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtPassword.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtPassword.Location = New System.Drawing.Point(686, 156)
        Me.TxtPassword.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TxtPassword.Name = "TxtPassword"
        Me.TxtPassword.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtPassword.PlaceholderText = "Password"
        Me.TxtPassword.SelectedText = ""
        Me.TxtPassword.Size = New System.Drawing.Size(280, 50)
        Me.TxtPassword.TabIndex = 60
        Me.TxtPassword.UseSystemPasswordChar = True
        '
        'TxtEmail
        '
        Me.TxtEmail.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtEmail.BorderRadius = 10
        Me.TxtEmail.BorderThickness = 2
        Me.TxtEmail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtEmail.DefaultText = ""
        Me.TxtEmail.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TxtEmail.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TxtEmail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtEmail.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtEmail.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtEmail.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEmail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtEmail.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtEmail.Location = New System.Drawing.Point(366, 243)
        Me.TxtEmail.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TxtEmail.Name = "TxtEmail"
        Me.TxtEmail.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtEmail.PlaceholderText = "Email"
        Me.TxtEmail.SelectedText = ""
        Me.TxtEmail.Size = New System.Drawing.Size(280, 50)
        Me.TxtEmail.TabIndex = 57
        '
        'TxtPhone
        '
        Me.TxtPhone.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtPhone.BorderRadius = 10
        Me.TxtPhone.BorderThickness = 2
        Me.TxtPhone.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtPhone.DefaultText = ""
        Me.TxtPhone.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TxtPhone.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TxtPhone.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtPhone.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtPhone.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtPhone.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPhone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtPhone.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtPhone.Location = New System.Drawing.Point(366, 156)
        Me.TxtPhone.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TxtPhone.Name = "TxtPhone"
        Me.TxtPhone.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtPhone.PlaceholderText = "Phone number"
        Me.TxtPhone.SelectedText = ""
        Me.TxtPhone.Size = New System.Drawing.Size(280, 50)
        Me.TxtPhone.TabIndex = 56
        '
        'TxtUsername
        '
        Me.TxtUsername.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtUsername.BorderRadius = 10
        Me.TxtUsername.BorderThickness = 2
        Me.TxtUsername.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtUsername.DefaultText = ""
        Me.TxtUsername.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TxtUsername.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TxtUsername.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtUsername.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtUsername.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtUsername.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUsername.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtUsername.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtUsername.Location = New System.Drawing.Point(52, 243)
        Me.TxtUsername.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TxtUsername.Name = "TxtUsername"
        Me.TxtUsername.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtUsername.PlaceholderText = "Username"
        Me.TxtUsername.SelectedText = ""
        Me.TxtUsername.Size = New System.Drawing.Size(280, 50)
        Me.TxtUsername.TabIndex = 55
        '
        'TxtName
        '
        Me.TxtName.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtName.BorderRadius = 10
        Me.TxtName.BorderThickness = 2
        Me.TxtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtName.DefaultText = ""
        Me.TxtName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TxtName.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TxtName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtName.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!)
        Me.TxtName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtName.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtName.Location = New System.Drawing.Point(52, 156)
        Me.TxtName.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtName.Name = "TxtName"
        Me.TxtName.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.TxtName.PlaceholderText = "Full name"
        Me.TxtName.SelectedText = ""
        Me.TxtName.Size = New System.Drawing.Size(280, 50)
        Me.TxtName.TabIndex = 54
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
        Me.Guna2CustomGradientPanel1.Size = New System.Drawing.Size(1580, 107)
        Me.Guna2CustomGradientPanel1.TabIndex = 67
        '
        'Guna2CirclePictureBox1
        '
        Me.Guna2CirclePictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2CirclePictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2CirclePictureBox1.FillColor = System.Drawing.Color.Transparent
        Me.Guna2CirclePictureBox1.Image = CType(resources.GetObject("Guna2CirclePictureBox1.Image"), System.Drawing.Image)
        Me.Guna2CirclePictureBox1.ImageRotate = 0!
        Me.Guna2CirclePictureBox1.Location = New System.Drawing.Point(1495, 26)
        Me.Guna2CirclePictureBox1.Name = "Guna2CirclePictureBox1"
        Me.Guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.Guna2CirclePictureBox1.Size = New System.Drawing.Size(60, 58)
        Me.Guna2CirclePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Guna2CirclePictureBox1.TabIndex = 19
        Me.Guna2CirclePictureBox1.TabStop = False
        '
        'Guna2HtmlLabel6
        '
        Me.Guna2HtmlLabel6.AutoSize = False
        Me.Guna2HtmlLabel6.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel6.Font = New System.Drawing.Font("Mongolian Baiti", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.Guna2HtmlLabel6.Location = New System.Drawing.Point(0, -1)
        Me.Guna2HtmlLabel6.Name = "Guna2HtmlLabel6"
        Me.Guna2HtmlLabel6.Size = New System.Drawing.Size(1580, 107)
        Me.Guna2HtmlLabel6.TabIndex = 47
        Me.Guna2HtmlLabel6.Text = "Staff Maintenance"
        Me.Guna2HtmlLabel6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'AdminDBStaffMaintenance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1579, 883)
        Me.Controls.Add(Me.Guna2CustomGradientPanel1)
        Me.Controls.Add(Me.chkShowPassword)
        Me.Controls.Add(Me.SearchStaff)
        Me.Controls.Add(Me.DgvStaffs)
        Me.Controls.Add(Me.BTNAdd)
        Me.Controls.Add(Me.TxtConfirmPassword)
        Me.Controls.Add(Me.TxtPassword)
        Me.Controls.Add(Me.TxtEmail)
        Me.Controls.Add(Me.TxtPhone)
        Me.Controls.Add(Me.TxtUsername)
        Me.Controls.Add(Me.TxtName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "AdminDBStaffMaintenance"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AdminDBStaffMaintenance"
        CType(Me.DgvStaffs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Guna2CustomGradientPanel1.ResumeLayout(False)
        CType(Me.Guna2CirclePictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents chkShowPassword As Guna.UI2.WinForms.Guna2RadioButton
    Friend WithEvents SearchStaff As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents DgvStaffs As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents BTNAdd As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents TxtConfirmPassword As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents TxtPassword As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents TxtEmail As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents TxtPhone As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents TxtUsername As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents TxtName As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Guna2CustomGradientPanel1 As Guna.UI2.WinForms.Guna2CustomGradientPanel
    Friend WithEvents Guna2CirclePictureBox1 As Guna.UI2.WinForms.Guna2CirclePictureBox
    Friend WithEvents Guna2HtmlLabel6 As Guna.UI2.WinForms.Guna2HtmlLabel
End Class
