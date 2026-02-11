<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DentistDashboard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DentistDashboard))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Guna2CustomGradientPanel1 = New Guna.UI2.WinForms.Guna2CustomGradientPanel()
        Me.LogoutPictureBox1 = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.Guna2HtmlLabel1 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.denTab = New Guna.UI2.WinForms.Guna2TabControl()
        Me.tabPatientManagement = New System.Windows.Forms.TabPage()
        Me.dgvPatients = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.tabAppointment = New System.Windows.Forms.TabPage()
        Me.dgvAppointments = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.tabTreatmentRecords = New System.Windows.Forms.TabPage()
        Me.Guna2CustomGradientPanel1.SuspendLayout()
        CType(Me.LogoutPictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.denTab.SuspendLayout()
        Me.tabPatientManagement.SuspendLayout()
        CType(Me.dgvPatients, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabAppointment.SuspendLayout()
        CType(Me.dgvAppointments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Guna2CustomGradientPanel1
        '
        Me.Guna2CustomGradientPanel1.Controls.Add(Me.LogoutPictureBox1)
        Me.Guna2CustomGradientPanel1.Controls.Add(Me.Guna2HtmlLabel1)
        Me.Guna2CustomGradientPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Guna2CustomGradientPanel1.FillColor2 = System.Drawing.Color.Gray
        Me.Guna2CustomGradientPanel1.FillColor3 = System.Drawing.Color.Silver
        Me.Guna2CustomGradientPanel1.Location = New System.Drawing.Point(0, 0)
        Me.Guna2CustomGradientPanel1.Name = "Guna2CustomGradientPanel1"
        Me.Guna2CustomGradientPanel1.Size = New System.Drawing.Size(1396, 124)
        Me.Guna2CustomGradientPanel1.TabIndex = 32
        '
        'LogoutPictureBox1
        '
        Me.LogoutPictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LogoutPictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.LogoutPictureBox1.FillColor = System.Drawing.Color.Transparent
        Me.LogoutPictureBox1.Image = CType(resources.GetObject("LogoutPictureBox1.Image"), System.Drawing.Image)
        Me.LogoutPictureBox1.ImageRotate = 0!
        Me.LogoutPictureBox1.Location = New System.Drawing.Point(1291, 27)
        Me.LogoutPictureBox1.Name = "LogoutPictureBox1"
        Me.LogoutPictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.LogoutPictureBox1.Size = New System.Drawing.Size(64, 64)
        Me.LogoutPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.LogoutPictureBox1.TabIndex = 34
        Me.LogoutPictureBox1.TabStop = False
        '
        'Guna2HtmlLabel1
        '
        Me.Guna2HtmlLabel1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Guna2HtmlLabel1.AutoSize = False
        Me.Guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel1.Font = New System.Drawing.Font("Palatino Linotype", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel1.Location = New System.Drawing.Point(528, 40)
        Me.Guna2HtmlLabel1.Name = "Guna2HtmlLabel1"
        Me.Guna2HtmlLabel1.Size = New System.Drawing.Size(472, 66)
        Me.Guna2HtmlLabel1.TabIndex = 21
        Me.Guna2HtmlLabel1.Text = "Dental Clinic System"
        '
        'denTab
        '
        Me.denTab.Alignment = System.Windows.Forms.TabAlignment.Left
        Me.denTab.Controls.Add(Me.tabPatientManagement)
        Me.denTab.Controls.Add(Me.tabAppointment)
        Me.denTab.Controls.Add(Me.tabTreatmentRecords)
        Me.denTab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.denTab.ItemSize = New System.Drawing.Size(180, 40)
        Me.denTab.Location = New System.Drawing.Point(0, 124)
        Me.denTab.Name = "denTab"
        Me.denTab.SelectedIndex = 0
        Me.denTab.Size = New System.Drawing.Size(1396, 639)
        Me.denTab.TabButtonHoverState.BorderColor = System.Drawing.Color.Empty
        Me.denTab.TabButtonHoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(52, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.denTab.TabButtonHoverState.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.denTab.TabButtonHoverState.ForeColor = System.Drawing.Color.White
        Me.denTab.TabButtonHoverState.InnerColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(52, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.denTab.TabButtonIdleState.BorderColor = System.Drawing.Color.Empty
        Me.denTab.TabButtonIdleState.FillColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(57, Byte), Integer))
        Me.denTab.TabButtonIdleState.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.denTab.TabButtonIdleState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(156, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(167, Byte), Integer))
        Me.denTab.TabButtonIdleState.InnerColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(57, Byte), Integer))
        Me.denTab.TabButtonSelectedState.BorderColor = System.Drawing.Color.Empty
        Me.denTab.TabButtonSelectedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(29, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(49, Byte), Integer))
        Me.denTab.TabButtonSelectedState.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.denTab.TabButtonSelectedState.ForeColor = System.Drawing.Color.White
        Me.denTab.TabButtonSelectedState.InnerColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(132, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.denTab.TabButtonSize = New System.Drawing.Size(180, 40)
        Me.denTab.TabIndex = 34
        Me.denTab.TabMenuBackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(57, Byte), Integer))
        '
        'tabPatientManagement
        '
        Me.tabPatientManagement.Controls.Add(Me.dgvPatients)
        Me.tabPatientManagement.Location = New System.Drawing.Point(184, 4)
        Me.tabPatientManagement.Name = "tabPatientManagement"
        Me.tabPatientManagement.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPatientManagement.Size = New System.Drawing.Size(1208, 631)
        Me.tabPatientManagement.TabIndex = 0
        Me.tabPatientManagement.Text = "Patient Management"
        Me.tabPatientManagement.UseVisualStyleBackColor = True
        '
        'dgvPatients
        '
        Me.dgvPatients.AllowUserToAddRows = False
        Me.dgvPatients.AllowUserToDeleteRows = False
        Me.dgvPatients.AllowUserToResizeColumns = False
        Me.dgvPatients.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.dgvPatients.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvPatients.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPatients.BackgroundColor = System.Drawing.SystemColors.ControlDark
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPatients.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvPatients.ColumnHeadersHeight = 25
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvPatients.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvPatients.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvPatients.Location = New System.Drawing.Point(0, 0)
        Me.dgvPatients.Name = "dgvPatients"
        Me.dgvPatients.ReadOnly = True
        Me.dgvPatients.RowHeadersVisible = False
        Me.dgvPatients.Size = New System.Drawing.Size(1208, 628)
        Me.dgvPatients.TabIndex = 0
        Me.dgvPatients.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White
        Me.dgvPatients.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.dgvPatients.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.dgvPatients.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.dgvPatients.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.dgvPatients.ThemeStyle.BackColor = System.Drawing.SystemColors.ControlDark
        Me.dgvPatients.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvPatients.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvPatients.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.dgvPatients.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvPatients.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White
        Me.dgvPatients.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvPatients.ThemeStyle.HeaderStyle.Height = 25
        Me.dgvPatients.ThemeStyle.ReadOnly = True
        Me.dgvPatients.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.dgvPatients.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvPatients.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvPatients.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.dgvPatients.ThemeStyle.RowsStyle.Height = 22
        Me.dgvPatients.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvPatients.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        '
        'tabAppointment
        '
        Me.tabAppointment.Controls.Add(Me.dgvAppointments)
        Me.tabAppointment.Location = New System.Drawing.Point(184, 4)
        Me.tabAppointment.Name = "tabAppointment"
        Me.tabAppointment.Padding = New System.Windows.Forms.Padding(3)
        Me.tabAppointment.Size = New System.Drawing.Size(1208, 631)
        Me.tabAppointment.TabIndex = 1
        Me.tabAppointment.Text = "Appointment"
        Me.tabAppointment.UseVisualStyleBackColor = True
        '
        'dgvAppointments
        '
        Me.dgvAppointments.AllowUserToAddRows = False
        Me.dgvAppointments.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvAppointments.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvAppointments.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvAppointments.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.dgvAppointments.BackgroundColor = System.Drawing.SystemColors.ControlDark
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvAppointments.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvAppointments.ColumnHeadersHeight = 25
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvAppointments.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgvAppointments.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvAppointments.Location = New System.Drawing.Point(2, 1)
        Me.dgvAppointments.Name = "dgvAppointments"
        Me.dgvAppointments.ReadOnly = True
        Me.dgvAppointments.RowHeadersVisible = False
        Me.dgvAppointments.Size = New System.Drawing.Size(1210, 628)
        Me.dgvAppointments.TabIndex = 1
        Me.dgvAppointments.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White
        Me.dgvAppointments.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.dgvAppointments.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.dgvAppointments.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.dgvAppointments.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.dgvAppointments.ThemeStyle.BackColor = System.Drawing.SystemColors.ControlDark
        Me.dgvAppointments.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvAppointments.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvAppointments.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.dgvAppointments.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvAppointments.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White
        Me.dgvAppointments.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvAppointments.ThemeStyle.HeaderStyle.Height = 25
        Me.dgvAppointments.ThemeStyle.ReadOnly = True
        Me.dgvAppointments.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.dgvAppointments.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvAppointments.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvAppointments.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.dgvAppointments.ThemeStyle.RowsStyle.Height = 22
        Me.dgvAppointments.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvAppointments.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        '
        'tabTreatmentRecords
        '
        Me.tabTreatmentRecords.Location = New System.Drawing.Point(184, 4)
        Me.tabTreatmentRecords.Name = "tabTreatmentRecords"
        Me.tabTreatmentRecords.Padding = New System.Windows.Forms.Padding(3)
        Me.tabTreatmentRecords.Size = New System.Drawing.Size(1208, 631)
        Me.tabTreatmentRecords.TabIndex = 2
        Me.tabTreatmentRecords.Text = "Treatment Records"
        Me.tabTreatmentRecords.UseVisualStyleBackColor = True
        '
        'DentistDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1396, 763)
        Me.ControlBox = False
        Me.Controls.Add(Me.denTab)
        Me.Controls.Add(Me.Guna2CustomGradientPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "DentistDashboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DentistDashboard"
        Me.Guna2CustomGradientPanel1.ResumeLayout(False)
        CType(Me.LogoutPictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.denTab.ResumeLayout(False)
        Me.tabPatientManagement.ResumeLayout(False)
        CType(Me.dgvPatients, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabAppointment.ResumeLayout(False)
        CType(Me.dgvAppointments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Guna2CustomGradientPanel1 As Guna.UI2.WinForms.Guna2CustomGradientPanel
    Friend WithEvents Guna2HtmlLabel1 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents LogoutPictureBox1 As Guna.UI2.WinForms.Guna2CirclePictureBox
    Friend WithEvents denTab As Guna.UI2.WinForms.Guna2TabControl
    Friend WithEvents tabPatientManagement As TabPage
    Friend WithEvents tabAppointment As TabPage
    Friend WithEvents tabTreatmentRecords As TabPage
    Friend WithEvents dgvPatients As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents dgvAppointments As Guna.UI2.WinForms.Guna2DataGridView
End Class
