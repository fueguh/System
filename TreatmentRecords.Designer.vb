<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TreatmentRecords
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TreatmentRecords))
        Me.CmbPatient = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.CmbDentist = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.TxtTreatmentNotes = New Guna.UI2.WinForms.Guna2TextBox()
        Me.TxtPrescriptions = New Guna.UI2.WinForms.Guna2TextBox()
        Me.TxtProceduresDone = New Guna.UI2.WinForms.Guna2TextBox()
        Me.BtnAttachImage = New Guna.UI2.WinForms.Guna2Button()
        Me.PicXrayPreview = New Guna.UI2.WinForms.Guna2PictureBox()
        Me.BtnSaveRecord = New Guna.UI2.WinForms.Guna2Button()
        Me.DGVRecords = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.Guna2CirclePictureBox1 = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        CType(Me.PicXrayPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVRecords, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Guna2CirclePictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmbPatient
        '
        Me.CmbPatient.BackColor = System.Drawing.Color.Transparent
        Me.CmbPatient.BorderRadius = 10
        Me.CmbPatient.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbPatient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbPatient.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbPatient.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbPatient.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.CmbPatient.ForeColor = System.Drawing.Color.Black
        Me.CmbPatient.ItemHeight = 30
        Me.CmbPatient.Location = New System.Drawing.Point(38, 85)
        Me.CmbPatient.Name = "CmbPatient"
        Me.CmbPatient.Size = New System.Drawing.Size(286, 36)
        Me.CmbPatient.TabIndex = 0
        '
        'CmbDentist
        '
        Me.CmbDentist.BackColor = System.Drawing.Color.Transparent
        Me.CmbDentist.BorderRadius = 10
        Me.CmbDentist.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbDentist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbDentist.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbDentist.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbDentist.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.CmbDentist.ForeColor = System.Drawing.Color.Black
        Me.CmbDentist.ItemHeight = 30
        Me.CmbDentist.Location = New System.Drawing.Point(38, 158)
        Me.CmbDentist.Name = "CmbDentist"
        Me.CmbDentist.Size = New System.Drawing.Size(286, 36)
        Me.CmbDentist.TabIndex = 1
        '
        'TxtTreatmentNotes
        '
        Me.TxtTreatmentNotes.BorderRadius = 10
        Me.TxtTreatmentNotes.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtTreatmentNotes.DefaultText = ""
        Me.TxtTreatmentNotes.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TxtTreatmentNotes.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TxtTreatmentNotes.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtTreatmentNotes.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtTreatmentNotes.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtTreatmentNotes.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.TxtTreatmentNotes.ForeColor = System.Drawing.Color.Black
        Me.TxtTreatmentNotes.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtTreatmentNotes.Location = New System.Drawing.Point(38, 226)
        Me.TxtTreatmentNotes.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TxtTreatmentNotes.Multiline = True
        Me.TxtTreatmentNotes.Name = "TxtTreatmentNotes"
        Me.TxtTreatmentNotes.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.TxtTreatmentNotes.PlaceholderText = "Treatment note"
        Me.TxtTreatmentNotes.SelectedText = ""
        Me.TxtTreatmentNotes.Size = New System.Drawing.Size(286, 60)
        Me.TxtTreatmentNotes.TabIndex = 2
        '
        'TxtPrescriptions
        '
        Me.TxtPrescriptions.BorderRadius = 10
        Me.TxtPrescriptions.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtPrescriptions.DefaultText = ""
        Me.TxtPrescriptions.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TxtPrescriptions.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TxtPrescriptions.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtPrescriptions.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtPrescriptions.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtPrescriptions.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.TxtPrescriptions.ForeColor = System.Drawing.Color.Black
        Me.TxtPrescriptions.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtPrescriptions.Location = New System.Drawing.Point(376, 85)
        Me.TxtPrescriptions.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TxtPrescriptions.Multiline = True
        Me.TxtPrescriptions.Name = "TxtPrescriptions"
        Me.TxtPrescriptions.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.TxtPrescriptions.PlaceholderText = "Prescription"
        Me.TxtPrescriptions.SelectedText = ""
        Me.TxtPrescriptions.Size = New System.Drawing.Size(286, 105)
        Me.TxtPrescriptions.TabIndex = 3
        '
        'TxtProceduresDone
        '
        Me.TxtProceduresDone.BorderRadius = 10
        Me.TxtProceduresDone.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtProceduresDone.DefaultText = ""
        Me.TxtProceduresDone.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TxtProceduresDone.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TxtProceduresDone.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtProceduresDone.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtProceduresDone.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtProceduresDone.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.TxtProceduresDone.ForeColor = System.Drawing.Color.Black
        Me.TxtProceduresDone.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtProceduresDone.Location = New System.Drawing.Point(376, 213)
        Me.TxtProceduresDone.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TxtProceduresDone.Multiline = True
        Me.TxtProceduresDone.Name = "TxtProceduresDone"
        Me.TxtProceduresDone.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.TxtProceduresDone.PlaceholderText = "Procedure done"
        Me.TxtProceduresDone.SelectedText = ""
        Me.TxtProceduresDone.Size = New System.Drawing.Size(286, 105)
        Me.TxtProceduresDone.TabIndex = 4
        '
        'BtnAttachImage
        '
        Me.BtnAttachImage.BorderRadius = 10
        Me.BtnAttachImage.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BtnAttachImage.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BtnAttachImage.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BtnAttachImage.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BtnAttachImage.FillColor = System.Drawing.Color.White
        Me.BtnAttachImage.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.BtnAttachImage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.BtnAttachImage.Location = New System.Drawing.Point(38, 342)
        Me.BtnAttachImage.Name = "BtnAttachImage"
        Me.BtnAttachImage.Size = New System.Drawing.Size(286, 45)
        Me.BtnAttachImage.TabIndex = 5
        Me.BtnAttachImage.Text = "Attach image"
        '
        'PicXrayPreview
        '
        Me.PicXrayPreview.ImageRotate = 0!
        Me.PicXrayPreview.Location = New System.Drawing.Point(376, 342)
        Me.PicXrayPreview.Name = "PicXrayPreview"
        Me.PicXrayPreview.Size = New System.Drawing.Size(291, 273)
        Me.PicXrayPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicXrayPreview.TabIndex = 6
        Me.PicXrayPreview.TabStop = False
        '
        'BtnSaveRecord
        '
        Me.BtnSaveRecord.BorderRadius = 10
        Me.BtnSaveRecord.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BtnSaveRecord.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BtnSaveRecord.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BtnSaveRecord.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BtnSaveRecord.FillColor = System.Drawing.Color.White
        Me.BtnSaveRecord.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.BtnSaveRecord.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.BtnSaveRecord.Location = New System.Drawing.Point(264, 677)
        Me.BtnSaveRecord.Name = "BtnSaveRecord"
        Me.BtnSaveRecord.Size = New System.Drawing.Size(214, 45)
        Me.BtnSaveRecord.TabIndex = 7
        Me.BtnSaveRecord.Text = "Save"
        '
        'DGVRecords
        '
        Me.DGVRecords.AllowUserToAddRows = False
        Me.DGVRecords.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Padding = New System.Windows.Forms.Padding(100)
        Me.DGVRecords.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGVRecords.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGVRecords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVRecords.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGVRecords.ColumnHeadersHeight = 25
        Me.DGVRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVRecords.DefaultCellStyle = DataGridViewCellStyle3
        Me.DGVRecords.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVRecords.Location = New System.Drawing.Point(710, 85)
        Me.DGVRecords.Name = "DGVRecords"
        Me.DGVRecords.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.Padding = New System.Windows.Forms.Padding(50)
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVRecords.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DGVRecords.RowHeadersVisible = False
        Me.DGVRecords.RowHeadersWidth = 100
        Me.DGVRecords.Size = New System.Drawing.Size(823, 637)
        Me.DGVRecords.TabIndex = 8
        Me.DGVRecords.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVRecords.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.DGVRecords.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.DGVRecords.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.DGVRecords.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.DGVRecords.ThemeStyle.BackColor = System.Drawing.Color.White
        Me.DGVRecords.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVRecords.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVRecords.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DGVRecords.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVRecords.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White
        Me.DGVRecords.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        Me.DGVRecords.ThemeStyle.HeaderStyle.Height = 25
        Me.DGVRecords.ThemeStyle.ReadOnly = True
        Me.DGVRecords.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVRecords.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DGVRecords.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVRecords.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.DGVRecords.ThemeStyle.RowsStyle.Height = 22
        Me.DGVRecords.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVRecords.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        '
        'Guna2CirclePictureBox1
        '
        Me.Guna2CirclePictureBox1.FillColor = System.Drawing.Color.Transparent
        Me.Guna2CirclePictureBox1.Image = CType(resources.GetObject("Guna2CirclePictureBox1.Image"), System.Drawing.Image)
        Me.Guna2CirclePictureBox1.ImageRotate = 0!
        Me.Guna2CirclePictureBox1.Location = New System.Drawing.Point(1473, 12)
        Me.Guna2CirclePictureBox1.Name = "Guna2CirclePictureBox1"
        Me.Guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.Guna2CirclePictureBox1.Size = New System.Drawing.Size(60, 58)
        Me.Guna2CirclePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Guna2CirclePictureBox1.TabIndex = 21
        Me.Guna2CirclePictureBox1.TabStop = False
        '
        'TreatmentRecords
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(1559, 781)
        Me.ControlBox = False
        Me.Controls.Add(Me.Guna2CirclePictureBox1)
        Me.Controls.Add(Me.DGVRecords)
        Me.Controls.Add(Me.BtnSaveRecord)
        Me.Controls.Add(Me.PicXrayPreview)
        Me.Controls.Add(Me.BtnAttachImage)
        Me.Controls.Add(Me.TxtProceduresDone)
        Me.Controls.Add(Me.TxtPrescriptions)
        Me.Controls.Add(Me.TxtTreatmentNotes)
        Me.Controls.Add(Me.CmbDentist)
        Me.Controls.Add(Me.CmbPatient)
        Me.Name = "TreatmentRecords"
        Me.Text = "TreatmentRecords"
        CType(Me.PicXrayPreview, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVRecords, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Guna2CirclePictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CmbPatient As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents CmbDentist As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents TxtTreatmentNotes As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents TxtPrescriptions As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents TxtProceduresDone As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents BtnAttachImage As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents PicXrayPreview As Guna.UI2.WinForms.Guna2PictureBox
    Friend WithEvents BtnSaveRecord As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents DGVRecords As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents Guna2CirclePictureBox1 As Guna.UI2.WinForms.Guna2CirclePictureBox
End Class
