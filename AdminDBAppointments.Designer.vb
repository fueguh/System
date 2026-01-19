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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdminDBAppointments))
        Me.CmbPatient = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.CmbDentist = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.DtpDate = New Guna.UI2.WinForms.Guna2DateTimePicker()
        Me.dtpStartTime = New Guna.UI2.WinForms.Guna2DateTimePicker()
        Me.DtpEndTime = New Guna.UI2.WinForms.Guna2DateTimePicker()
        Me.BTNAdd = New Guna.UI2.WinForms.Guna2Button()
        Me.BTNUpdate = New Guna.UI2.WinForms.Guna2Button()
        Me.BTNDelete = New Guna.UI2.WinForms.Guna2Button()
        Me.DGVAppointments = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.cmbStatus = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.Guna2CirclePictureBox1 = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.clbServices = New System.Windows.Forms.CheckedListBox()
        Me.Guna2GroupBox1 = New Guna.UI2.WinForms.Guna2GroupBox()
        CType(Me.DGVAppointments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Guna2CirclePictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Guna2GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'CmbPatient
        '
        Me.CmbPatient.BackColor = System.Drawing.Color.Transparent
        Me.CmbPatient.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbPatient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbPatient.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbPatient.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbPatient.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.CmbPatient.ForeColor = System.Drawing.Color.FromArgb(CType(CType(68, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(112, Byte), Integer))
        Me.CmbPatient.ItemHeight = 30
        Me.CmbPatient.Location = New System.Drawing.Point(74, 51)
        Me.CmbPatient.Name = "CmbPatient"
        Me.CmbPatient.Size = New System.Drawing.Size(365, 36)
        Me.CmbPatient.TabIndex = 0
        '
        'CmbDentist
        '
        Me.CmbDentist.BackColor = System.Drawing.Color.Transparent
        Me.CmbDentist.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbDentist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbDentist.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbDentist.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbDentist.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.CmbDentist.ForeColor = System.Drawing.Color.FromArgb(CType(CType(68, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(112, Byte), Integer))
        Me.CmbDentist.ItemHeight = 30
        Me.CmbDentist.Location = New System.Drawing.Point(74, 95)
        Me.CmbDentist.Name = "CmbDentist"
        Me.CmbDentist.Size = New System.Drawing.Size(365, 36)
        Me.CmbDentist.TabIndex = 1
        '
        'DtpDate
        '
        Me.DtpDate.Checked = True
        Me.DtpDate.FillColor = System.Drawing.Color.White
        Me.DtpDate.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.DtpDate.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.DtpDate.Location = New System.Drawing.Point(74, 411)
        Me.DtpDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.DtpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpDate.Name = "DtpDate"
        Me.DtpDate.Size = New System.Drawing.Size(365, 46)
        Me.DtpDate.TabIndex = 3
        Me.DtpDate.Value = New Date(2025, 12, 18, 11, 1, 1, 395)
        '
        'dtpStartTime
        '
        Me.dtpStartTime.Checked = True
        Me.dtpStartTime.FillColor = System.Drawing.Color.White
        Me.dtpStartTime.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.dtpStartTime.Location = New System.Drawing.Point(74, 466)
        Me.dtpStartTime.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpStartTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStartTime.Name = "dtpStartTime"
        Me.dtpStartTime.Size = New System.Drawing.Size(365, 46)
        Me.dtpStartTime.TabIndex = 4
        Me.dtpStartTime.Value = New Date(2025, 12, 18, 11, 1, 1, 395)
        '
        'DtpEndTime
        '
        Me.DtpEndTime.Checked = True
        Me.DtpEndTime.FillColor = System.Drawing.Color.White
        Me.DtpEndTime.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.DtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.DtpEndTime.Location = New System.Drawing.Point(74, 529)
        Me.DtpEndTime.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.DtpEndTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpEndTime.Name = "DtpEndTime"
        Me.DtpEndTime.Size = New System.Drawing.Size(365, 46)
        Me.DtpEndTime.TabIndex = 5
        Me.DtpEndTime.Value = New Date(2025, 12, 18, 11, 1, 1, 395)
        '
        'BTNAdd
        '
        Me.BTNAdd.BorderColor = System.Drawing.SystemColors.ActiveCaption
        Me.BTNAdd.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BTNAdd.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BTNAdd.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BTNAdd.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BTNAdd.FillColor = System.Drawing.Color.WhiteSmoke
        Me.BTNAdd.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNAdd.ForeColor = System.Drawing.Color.DimGray
        Me.BTNAdd.Location = New System.Drawing.Point(47, 640)
        Me.BTNAdd.Name = "BTNAdd"
        Me.BTNAdd.Size = New System.Drawing.Size(180, 45)
        Me.BTNAdd.TabIndex = 6
        Me.BTNAdd.Text = "Add Appointment"
        '
        'BTNUpdate
        '
        Me.BTNUpdate.BorderColor = System.Drawing.SystemColors.ActiveCaption
        Me.BTNUpdate.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BTNUpdate.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BTNUpdate.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BTNUpdate.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BTNUpdate.FillColor = System.Drawing.Color.WhiteSmoke
        Me.BTNUpdate.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNUpdate.ForeColor = System.Drawing.Color.DimGray
        Me.BTNUpdate.Location = New System.Drawing.Point(287, 640)
        Me.BTNUpdate.Name = "BTNUpdate"
        Me.BTNUpdate.Size = New System.Drawing.Size(180, 45)
        Me.BTNUpdate.TabIndex = 7
        Me.BTNUpdate.Text = "Update"
        '
        'BTNDelete
        '
        Me.BTNDelete.BorderColor = System.Drawing.SystemColors.ActiveCaption
        Me.BTNDelete.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BTNDelete.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BTNDelete.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BTNDelete.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BTNDelete.FillColor = System.Drawing.Color.WhiteSmoke
        Me.BTNDelete.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNDelete.ForeColor = System.Drawing.Color.DimGray
        Me.BTNDelete.Location = New System.Drawing.Point(165, 695)
        Me.BTNDelete.Name = "BTNDelete"
        Me.BTNDelete.Size = New System.Drawing.Size(180, 45)
        Me.BTNDelete.TabIndex = 8
        Me.BTNDelete.Text = "Delete"
        '
        'DGVAppointments
        '
        Me.DGVAppointments.AllowUserToAddRows = False
        Me.DGVAppointments.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.DGVAppointments.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGVAppointments.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGVAppointments.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVAppointments.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGVAppointments.ColumnHeadersHeight = 25
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVAppointments.DefaultCellStyle = DataGridViewCellStyle3
        Me.DGVAppointments.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVAppointments.Location = New System.Drawing.Point(491, 95)
        Me.DGVAppointments.Name = "DGVAppointments"
        Me.DGVAppointments.ReadOnly = True
        Me.DGVAppointments.RowHeadersVisible = False
        Me.DGVAppointments.RowHeadersWidth = 51
        Me.DGVAppointments.Size = New System.Drawing.Size(887, 645)
        Me.DGVAppointments.TabIndex = 9
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
        Me.DGVAppointments.ThemeStyle.HeaderStyle.Height = 25
        Me.DGVAppointments.ThemeStyle.ReadOnly = True
        Me.DGVAppointments.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVAppointments.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DGVAppointments.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVAppointments.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.DGVAppointments.ThemeStyle.RowsStyle.Height = 22
        Me.DGVAppointments.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVAppointments.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        '
        'cmbStatus
        '
        Me.cmbStatus.BackColor = System.Drawing.Color.Transparent
        Me.cmbStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbStatus.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbStatus.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbStatus.ForeColor = System.Drawing.Color.Black
        Me.cmbStatus.ItemHeight = 30
        Me.cmbStatus.Location = New System.Drawing.Point(74, 592)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(365, 36)
        Me.cmbStatus.TabIndex = 10
        '
        'Guna2CirclePictureBox1
        '
        Me.Guna2CirclePictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2CirclePictureBox1.FillColor = System.Drawing.Color.Transparent
        Me.Guna2CirclePictureBox1.Image = CType(resources.GetObject("Guna2CirclePictureBox1.Image"), System.Drawing.Image)
        Me.Guna2CirclePictureBox1.ImageRotate = 0!
        Me.Guna2CirclePictureBox1.Location = New System.Drawing.Point(1338, 12)
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
        Me.clbServices.FormattingEnabled = True
        Me.clbServices.Location = New System.Drawing.Point(0, 40)
        Me.clbServices.Name = "clbServices"
        Me.clbServices.Size = New System.Drawing.Size(365, 214)
        Me.clbServices.TabIndex = 20
        '
        'Guna2GroupBox1
        '
        Me.Guna2GroupBox1.Controls.Add(Me.clbServices)
        Me.Guna2GroupBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Guna2GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.Guna2GroupBox1.Location = New System.Drawing.Point(74, 141)
        Me.Guna2GroupBox1.Name = "Guna2GroupBox1"
        Me.Guna2GroupBox1.Size = New System.Drawing.Size(365, 254)
        Me.Guna2GroupBox1.TabIndex = 21
        Me.Guna2GroupBox1.Text = "Services to be Performed"
        '
        'AdminDBAppointments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1410, 770)
        Me.ControlBox = False
        Me.Controls.Add(Me.Guna2GroupBox1)
        Me.Controls.Add(Me.Guna2CirclePictureBox1)
        Me.Controls.Add(Me.cmbStatus)
        Me.Controls.Add(Me.DGVAppointments)
        Me.Controls.Add(Me.BTNDelete)
        Me.Controls.Add(Me.BTNUpdate)
        Me.Controls.Add(Me.BTNAdd)
        Me.Controls.Add(Me.DtpEndTime)
        Me.Controls.Add(Me.dtpStartTime)
        Me.Controls.Add(Me.DtpDate)
        Me.Controls.Add(Me.CmbDentist)
        Me.Controls.Add(Me.CmbPatient)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "AdminDBAppointments"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AdminDBAppointments"
        CType(Me.DGVAppointments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Guna2CirclePictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Guna2GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CmbPatient As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents CmbDentist As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents DtpDate As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents dtpStartTime As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents DtpEndTime As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents BTNAdd As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents BTNUpdate As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents BTNDelete As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents DGVAppointments As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents cmbStatus As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents Guna2CirclePictureBox1 As Guna.UI2.WinForms.Guna2CirclePictureBox
    Friend WithEvents clbServices As CheckedListBox
    Friend WithEvents Guna2GroupBox1 As Guna.UI2.WinForms.Guna2GroupBox
End Class
