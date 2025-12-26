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
        Me.Guna2CustomGradientPanel1 = New Guna.UI2.WinForms.Guna2CustomGradientPanel()
        Me.LogoutPictureBox1 = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.Guna2HtmlLabel1 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.PatientManagementToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.AppointmentToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.TreatementRecordsToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Guna2CustomGradientPanel1.SuspendLayout()
        CType(Me.LogoutPictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
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
        'MenuStrip1
        '
        Me.MenuStrip1.AutoSize = False
        Me.MenuStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.MenuStrip1.Dock = System.Windows.Forms.DockStyle.Left
        Me.MenuStrip1.Font = New System.Drawing.Font("Times New Roman", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PatientManagementToolStripMenuItem1, Me.AppointmentToolStripMenuItem2, Me.TreatementRecordsToolStripMenuItem3})
        Me.MenuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 124)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(361, 639)
        Me.MenuStrip1.TabIndex = 33
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'PatientManagementToolStripMenuItem1
        '
        Me.PatientManagementToolStripMenuItem1.Name = "PatientManagementToolStripMenuItem1"
        Me.PatientManagementToolStripMenuItem1.Size = New System.Drawing.Size(319, 44)
        Me.PatientManagementToolStripMenuItem1.Text = "Patient Management"
        '
        'AppointmentToolStripMenuItem2
        '
        Me.AppointmentToolStripMenuItem2.Name = "AppointmentToolStripMenuItem2"
        Me.AppointmentToolStripMenuItem2.Size = New System.Drawing.Size(213, 44)
        Me.AppointmentToolStripMenuItem2.Text = "Appointment"
        '
        'TreatementRecordsToolStripMenuItem3
        '
        Me.TreatementRecordsToolStripMenuItem3.Name = "TreatementRecordsToolStripMenuItem3"
        Me.TreatementRecordsToolStripMenuItem3.Size = New System.Drawing.Size(295, 44)
        Me.TreatementRecordsToolStripMenuItem3.Text = "Treatment Records"
        '
        'DentistDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1396, 763)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.Guna2CustomGradientPanel1)
        Me.Name = "DentistDashboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DentistDashboard"
        Me.Guna2CustomGradientPanel1.ResumeLayout(False)
        CType(Me.LogoutPictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Guna2CustomGradientPanel1 As Guna.UI2.WinForms.Guna2CustomGradientPanel
    Friend WithEvents Guna2HtmlLabel1 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents PatientManagementToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents AppointmentToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents TreatementRecordsToolStripMenuItem3 As ToolStripMenuItem
    Friend WithEvents LogoutPictureBox1 As Guna.UI2.WinForms.Guna2CirclePictureBox
End Class
