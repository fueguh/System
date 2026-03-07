<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AvailableAppointments
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AvailableAppointments))
        Me.Guna2Panel1 = New Guna.UI2.WinForms.Guna2Panel()
        Me.lblAvailableAppointments = New System.Windows.Forms.Label()
        Me.btnStartTreatment = New Guna.UI2.WinForms.Guna2Button()
        Me.dgvTodaysAppointments = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.btnBack1 = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.Guna2Panel1.SuspendLayout()
        CType(Me.dgvTodaysAppointments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Guna2Panel1
        '
        Me.Guna2Panel1.Controls.Add(Me.btnBack1)
        Me.Guna2Panel1.Controls.Add(Me.lblAvailableAppointments)
        Me.Guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Guna2Panel1.FillColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Guna2Panel1.Name = "Guna2Panel1"
        Me.Guna2Panel1.Size = New System.Drawing.Size(903, 100)
        Me.Guna2Panel1.TabIndex = 0
        '
        'lblAvailableAppointments
        '
        Me.lblAvailableAppointments.BackColor = System.Drawing.Color.Transparent
        Me.lblAvailableAppointments.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblAvailableAppointments.Font = New System.Drawing.Font("Mongolian Baiti", 26.25!, System.Drawing.FontStyle.Bold)
        Me.lblAvailableAppointments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.lblAvailableAppointments.Location = New System.Drawing.Point(0, 0)
        Me.lblAvailableAppointments.Name = "lblAvailableAppointments"
        Me.lblAvailableAppointments.Size = New System.Drawing.Size(903, 100)
        Me.lblAvailableAppointments.TabIndex = 0
        Me.lblAvailableAppointments.Text = "Available Appointments"
        Me.lblAvailableAppointments.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnStartTreatment
        '
        Me.btnStartTreatment.BorderColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.btnStartTreatment.BorderRadius = 10
        Me.btnStartTreatment.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnStartTreatment.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnStartTreatment.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnStartTreatment.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnStartTreatment.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.btnStartTreatment.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!)
        Me.btnStartTreatment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.btnStartTreatment.Location = New System.Drawing.Point(268, 268)
        Me.btnStartTreatment.Margin = New System.Windows.Forms.Padding(4)
        Me.btnStartTreatment.Name = "btnStartTreatment"
        Me.btnStartTreatment.Size = New System.Drawing.Size(356, 59)
        Me.btnStartTreatment.TabIndex = 7
        Me.btnStartTreatment.Text = "Start Treatment"
        '
        'dgvTodaysAppointments
        '
        Me.dgvTodaysAppointments.AllowUserToAddRows = False
        Me.dgvTodaysAppointments.AllowUserToDeleteRows = False
        Me.dgvTodaysAppointments.AllowUserToResizeColumns = False
        Me.dgvTodaysAppointments.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.dgvTodaysAppointments.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvTodaysAppointments.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(122, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(178, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvTodaysAppointments.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvTodaysAppointments.ColumnHeadersHeight = 30
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTodaysAppointments.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvTodaysAppointments.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dgvTodaysAppointments.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvTodaysAppointments.Location = New System.Drawing.Point(0, 368)
        Me.dgvTodaysAppointments.Name = "dgvTodaysAppointments"
        Me.dgvTodaysAppointments.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTodaysAppointments.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvTodaysAppointments.RowHeadersVisible = False
        Me.dgvTodaysAppointments.RowHeadersWidth = 51
        Me.dgvTodaysAppointments.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.dgvTodaysAppointments.Size = New System.Drawing.Size(903, 317)
        Me.dgvTodaysAppointments.TabIndex = 50
        Me.dgvTodaysAppointments.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White
        Me.dgvTodaysAppointments.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.dgvTodaysAppointments.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.dgvTodaysAppointments.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.dgvTodaysAppointments.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.dgvTodaysAppointments.ThemeStyle.BackColor = System.Drawing.Color.White
        Me.dgvTodaysAppointments.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvTodaysAppointments.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvTodaysAppointments.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.dgvTodaysAppointments.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvTodaysAppointments.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White
        Me.dgvTodaysAppointments.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvTodaysAppointments.ThemeStyle.HeaderStyle.Height = 30
        Me.dgvTodaysAppointments.ThemeStyle.ReadOnly = True
        Me.dgvTodaysAppointments.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.dgvTodaysAppointments.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvTodaysAppointments.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvTodaysAppointments.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.dgvTodaysAppointments.ThemeStyle.RowsStyle.Height = 22
        Me.dgvTodaysAppointments.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvTodaysAppointments.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        '
        'btnBack1
        '
        Me.btnBack1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack1.BackColor = System.Drawing.Color.Transparent
        Me.btnBack1.FillColor = System.Drawing.Color.Transparent
        Me.btnBack1.Image = CType(resources.GetObject("btnBack1.Image"), System.Drawing.Image)
        Me.btnBack1.ImageRotate = 0!
        Me.btnBack1.Location = New System.Drawing.Point(822, 22)
        Me.btnBack1.Name = "btnBack1"
        Me.btnBack1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.btnBack1.Size = New System.Drawing.Size(60, 58)
        Me.btnBack1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnBack1.TabIndex = 49
        Me.btnBack1.TabStop = False
        '
        'AvailableAppointments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(903, 685)
        Me.ControlBox = False
        Me.Controls.Add(Me.dgvTodaysAppointments)
        Me.Controls.Add(Me.btnStartTreatment)
        Me.Controls.Add(Me.Guna2Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AvailableAppointments"
        Me.ShowIcon = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Guna2Panel1.ResumeLayout(False)
        CType(Me.dgvTodaysAppointments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Guna2Panel1 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblAvailableAppointments As Label
    Friend WithEvents btnStartTreatment As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents dgvTodaysAppointments As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents btnBack1 As Guna.UI2.WinForms.Guna2CirclePictureBox
End Class
