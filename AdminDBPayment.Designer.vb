<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdminDBPayment
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdminDBPayment))
        Me.Guna2AnimateWindow1 = New Guna.UI2.WinForms.Guna2AnimateWindow(Me.components)
        Me.TextBoxTotal = New Guna.UI2.WinForms.Guna2TextBox()
        Me.ComboBoxPaymentMethod = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.lblClinicName = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2HtmlLabel1 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.ButtonGenerateReceipt = New Guna.UI2.WinForms.Guna2Button()
        Me.ButtonPrintReceipt = New Guna.UI2.WinForms.Guna2Button()
        Me.DGVServices = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.CmbPatient = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.Guna2HtmlLabel2 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.btnBack = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.clbServices = New System.Windows.Forms.CheckedListBox()
        Me.grpServices = New Guna.UI2.WinForms.Guna2GroupBox()
        CType(Me.DGVServices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpServices.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxTotal
        '
        Me.TextBoxTotal.BorderRadius = 6
        Me.TextBoxTotal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextBoxTotal.DefaultText = ""
        Me.TextBoxTotal.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TextBoxTotal.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TextBoxTotal.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TextBoxTotal.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TextBoxTotal.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextBoxTotal.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.TextBoxTotal.ForeColor = System.Drawing.Color.Black
        Me.TextBoxTotal.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextBoxTotal.Location = New System.Drawing.Point(25, 397)
        Me.TextBoxTotal.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBoxTotal.Name = "TextBoxTotal"
        Me.TextBoxTotal.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.TextBoxTotal.PlaceholderText = "Amount"
        Me.TextBoxTotal.SelectedText = ""
        Me.TextBoxTotal.Size = New System.Drawing.Size(279, 50)
        Me.TextBoxTotal.TabIndex = 12
        '
        'ComboBoxPaymentMethod
        '
        Me.ComboBoxPaymentMethod.BackColor = System.Drawing.Color.Transparent
        Me.ComboBoxPaymentMethod.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ComboBoxPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxPaymentMethod.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ComboBoxPaymentMethod.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ComboBoxPaymentMethod.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.ComboBoxPaymentMethod.ForeColor = System.Drawing.Color.FromArgb(CType(CType(68, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(112, Byte), Integer))
        Me.ComboBoxPaymentMethod.ItemHeight = 30
        Me.ComboBoxPaymentMethod.Location = New System.Drawing.Point(25, 497)
        Me.ComboBoxPaymentMethod.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxPaymentMethod.Name = "ComboBoxPaymentMethod"
        Me.ComboBoxPaymentMethod.Size = New System.Drawing.Size(279, 36)
        Me.ComboBoxPaymentMethod.TabIndex = 13
        '
        'lblClinicName
        '
        Me.lblClinicName.BackColor = System.Drawing.Color.Transparent
        Me.lblClinicName.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClinicName.Location = New System.Drawing.Point(25, 121)
        Me.lblClinicName.Name = "lblClinicName"
        Me.lblClinicName.Size = New System.Drawing.Size(62, 28)
        Me.lblClinicName.TabIndex = 22
        Me.lblClinicName.Text = "Service"
        '
        'Guna2HtmlLabel1
        '
        Me.Guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel1.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel1.Location = New System.Drawing.Point(25, 462)
        Me.Guna2HtmlLabel1.Name = "Guna2HtmlLabel1"
        Me.Guna2HtmlLabel1.Size = New System.Drawing.Size(148, 28)
        Me.Guna2HtmlLabel1.TabIndex = 23
        Me.Guna2HtmlLabel1.Text = "Payment Method"
        '
        'ButtonGenerateReceipt
        '
        Me.ButtonGenerateReceipt.BorderRadius = 10
        Me.ButtonGenerateReceipt.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.ButtonGenerateReceipt.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.ButtonGenerateReceipt.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.ButtonGenerateReceipt.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.ButtonGenerateReceipt.FillColor = System.Drawing.Color.White
        Me.ButtonGenerateReceipt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonGenerateReceipt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ButtonGenerateReceipt.Location = New System.Drawing.Point(80, 553)
        Me.ButtonGenerateReceipt.Name = "ButtonGenerateReceipt"
        Me.ButtonGenerateReceipt.Size = New System.Drawing.Size(180, 45)
        Me.ButtonGenerateReceipt.TabIndex = 24
        Me.ButtonGenerateReceipt.Text = "Generate Receipt"
        '
        'ButtonPrintReceipt
        '
        Me.ButtonPrintReceipt.BorderRadius = 10
        Me.ButtonPrintReceipt.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.ButtonPrintReceipt.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.ButtonPrintReceipt.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.ButtonPrintReceipt.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.ButtonPrintReceipt.FillColor = System.Drawing.Color.White
        Me.ButtonPrintReceipt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonPrintReceipt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ButtonPrintReceipt.Location = New System.Drawing.Point(80, 625)
        Me.ButtonPrintReceipt.Name = "ButtonPrintReceipt"
        Me.ButtonPrintReceipt.Size = New System.Drawing.Size(180, 45)
        Me.ButtonPrintReceipt.TabIndex = 25
        Me.ButtonPrintReceipt.Text = "Print"
        '
        'DGVServices
        '
        Me.DGVServices.AllowUserToAddRows = False
        Me.DGVServices.AllowUserToDeleteRows = False
        Me.DGVServices.AllowUserToResizeColumns = False
        Me.DGVServices.AllowUserToResizeRows = False
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.White
        Me.DGVServices.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVServices.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.DGVServices.ColumnHeadersHeight = 25
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVServices.DefaultCellStyle = DataGridViewCellStyle9
        Me.DGVServices.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVServices.Location = New System.Drawing.Point(359, 102)
        Me.DGVServices.Name = "DGVServices"
        Me.DGVServices.ReadOnly = True
        Me.DGVServices.RowHeadersVisible = False
        Me.DGVServices.RowHeadersWidth = 51
        Me.DGVServices.Size = New System.Drawing.Size(771, 540)
        Me.DGVServices.TabIndex = 26
        Me.DGVServices.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVServices.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.DGVServices.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.DGVServices.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.DGVServices.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.DGVServices.ThemeStyle.BackColor = System.Drawing.Color.White
        Me.DGVServices.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVServices.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVServices.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DGVServices.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVServices.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White
        Me.DGVServices.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DGVServices.ThemeStyle.HeaderStyle.Height = 25
        Me.DGVServices.ThemeStyle.ReadOnly = True
        Me.DGVServices.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVServices.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DGVServices.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVServices.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.DGVServices.ThemeStyle.RowsStyle.Height = 22
        Me.DGVServices.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVServices.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        '
        'CmbPatient
        '
        Me.CmbPatient.BackColor = System.Drawing.Color.Transparent
        Me.CmbPatient.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbPatient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbPatient.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbPatient.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbPatient.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.CmbPatient.ForeColor = System.Drawing.Color.FromArgb(CType(CType(68, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(112, Byte), Integer))
        Me.CmbPatient.ItemHeight = 30
        Me.CmbPatient.Location = New System.Drawing.Point(25, 78)
        Me.CmbPatient.Margin = New System.Windows.Forms.Padding(4)
        Me.CmbPatient.Name = "CmbPatient"
        Me.CmbPatient.Size = New System.Drawing.Size(279, 36)
        Me.CmbPatient.TabIndex = 27
        '
        'Guna2HtmlLabel2
        '
        Me.Guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel2.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel2.Location = New System.Drawing.Point(25, 43)
        Me.Guna2HtmlLabel2.Name = "Guna2HtmlLabel2"
        Me.Guna2HtmlLabel2.Size = New System.Drawing.Size(117, 28)
        Me.Guna2HtmlLabel2.TabIndex = 28
        Me.Guna2HtmlLabel2.Text = "Patient Name"
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.FillColor = System.Drawing.Color.LightGray
        Me.btnBack.Image = CType(resources.GetObject("btnBack.Image"), System.Drawing.Image)
        Me.btnBack.ImageRotate = 0!
        Me.btnBack.Location = New System.Drawing.Point(1107, 12)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.btnBack.Size = New System.Drawing.Size(60, 58)
        Me.btnBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnBack.TabIndex = 29
        Me.btnBack.TabStop = False
        '
        'clbServices
        '
        Me.clbServices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbServices.FormattingEnabled = True
        Me.clbServices.Location = New System.Drawing.Point(0, 40)
        Me.clbServices.Name = "clbServices"
        Me.clbServices.Size = New System.Drawing.Size(279, 180)
        Me.clbServices.TabIndex = 30
        '
        'grpServices
        '
        Me.grpServices.Controls.Add(Me.clbServices)
        Me.grpServices.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.grpServices.ForeColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.grpServices.Location = New System.Drawing.Point(25, 155)
        Me.grpServices.Name = "grpServices"
        Me.grpServices.Size = New System.Drawing.Size(279, 220)
        Me.grpServices.TabIndex = 31
        Me.grpServices.Text = "Services"
        '
        'AdminDBPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1179, 702)
        Me.ControlBox = False
        Me.Controls.Add(Me.grpServices)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.Guna2HtmlLabel2)
        Me.Controls.Add(Me.CmbPatient)
        Me.Controls.Add(Me.DGVServices)
        Me.Controls.Add(Me.ButtonPrintReceipt)
        Me.Controls.Add(Me.ButtonGenerateReceipt)
        Me.Controls.Add(Me.Guna2HtmlLabel1)
        Me.Controls.Add(Me.lblClinicName)
        Me.Controls.Add(Me.ComboBoxPaymentMethod)
        Me.Controls.Add(Me.TextBoxTotal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "AdminDBPayment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AdminDBPayment"
        CType(Me.DGVServices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpServices.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Guna2AnimateWindow1 As Guna.UI2.WinForms.Guna2AnimateWindow
    Friend WithEvents TextBoxTotal As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents ComboBoxPaymentMethod As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents lblClinicName As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel1 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents ButtonGenerateReceipt As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents ButtonPrintReceipt As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents DGVServices As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents CmbPatient As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents Guna2HtmlLabel2 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents btnBack As Guna.UI2.WinForms.Guna2CirclePictureBox
    Friend WithEvents clbServices As CheckedListBox
    Friend WithEvents grpServices As Guna.UI2.WinForms.Guna2GroupBox
End Class
