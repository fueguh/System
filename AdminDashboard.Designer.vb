<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AdminDashboard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdminDashboard))
        Me.Guna2PictureBox6 = New Guna.UI2.WinForms.Guna2PictureBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ManageForm = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManageUsersForm = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManageDentistsForm = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManagePatientsForm = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManageServicesForm = New System.Windows.Forms.ToolStripMenuItem()
        Me.AuditTrailToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SystemOverviewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Guna2CustomGradientPanel1 = New Guna.UI2.WinForms.Guna2CustomGradientPanel()
        Me.Guna2HtmlLabel1 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2Panel1 = New Guna.UI2.WinForms.Guna2Panel()
        Me.label5 = New System.Windows.Forms.Label()
        Me.lblTotalPatients = New System.Windows.Forms.Label()
        Me.Guna2Panel2 = New Guna.UI2.WinForms.Guna2Panel()
        Me.label7 = New System.Windows.Forms.Label()
        Me.lblAppointmentsToday = New System.Windows.Forms.Label()
        Me.Guna2Panel3 = New Guna.UI2.WinForms.Guna2Panel()
        Me.label6 = New System.Windows.Forms.Label()
        Me.lblTotalDentists = New System.Windows.Forms.Label()
        Me.Guna2Panel4 = New Guna.UI2.WinForms.Guna2Panel()
        Me.label8 = New System.Windows.Forms.Label()
        Me.lblCompletedAppointments = New System.Windows.Forms.Label()
        CType(Me.Guna2PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.Guna2CustomGradientPanel1.SuspendLayout()
        Me.Guna2Panel1.SuspendLayout()
        Me.Guna2Panel2.SuspendLayout()
        Me.Guna2Panel3.SuspendLayout()
        Me.Guna2Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Guna2PictureBox6
        '
        Me.Guna2PictureBox6.Image = CType(resources.GetObject("Guna2PictureBox6.Image"), System.Drawing.Image)
        Me.Guna2PictureBox6.ImageRotate = 0!
        Me.Guna2PictureBox6.Location = New System.Drawing.Point(1, -2)
        Me.Guna2PictureBox6.Name = "Guna2PictureBox6"
        Me.Guna2PictureBox6.Size = New System.Drawing.Size(135, 124)
        Me.Guna2PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Guna2PictureBox6.TabIndex = 18
        Me.Guna2PictureBox6.TabStop = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.AutoSize = False
        Me.MenuStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.MenuStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.MenuStrip1.Font = New System.Drawing.Font("Times New Roman", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ManageForm, Me.AuditTrailToolStripMenuItem, Me.SystemOverviewToolStripMenuItem, Me.ReportsToolStripMenuItem, Me.ToolStripMenuItem1})
        Me.MenuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.MenuStrip1.Location = New System.Drawing.Point(1, 122)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(279, 770)
        Me.MenuStrip1.TabIndex = 19
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ManageForm
        '
        Me.ManageForm.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ManageUsersForm, Me.ManageDentistsForm, Me.ManagePatientsForm, Me.ManageServicesForm})
        Me.ManageForm.Name = "ManageForm"
        Me.ManageForm.Size = New System.Drawing.Size(212, 44)
        Me.ManageForm.Text = "Management"
        '
        'ManageUsersForm
        '
        Me.ManageUsersForm.Name = "ManageUsersForm"
        Me.ManageUsersForm.Size = New System.Drawing.Size(401, 44)
        Me.ManageUsersForm.Text = "User Management"
        '
        'ManageDentistsForm
        '
        Me.ManageDentistsForm.Name = "ManageDentistsForm"
        Me.ManageDentistsForm.Size = New System.Drawing.Size(401, 44)
        Me.ManageDentistsForm.Text = "Dentist Management"
        '
        'ManagePatientsForm
        '
        Me.ManagePatientsForm.Name = "ManagePatientsForm"
        Me.ManagePatientsForm.Size = New System.Drawing.Size(401, 44)
        Me.ManagePatientsForm.Text = "Patient Management"
        '
        'ManageServicesForm
        '
        Me.ManageServicesForm.Name = "ManageServicesForm"
        Me.ManageServicesForm.Size = New System.Drawing.Size(401, 44)
        Me.ManageServicesForm.Text = "Services Management"
        '
        'AuditTrailToolStripMenuItem
        '
        Me.AuditTrailToolStripMenuItem.Name = "AuditTrailToolStripMenuItem"
        Me.AuditTrailToolStripMenuItem.Size = New System.Drawing.Size(182, 44)
        Me.AuditTrailToolStripMenuItem.Text = "Audit Trail"
        '
        'SystemOverviewToolStripMenuItem
        '
        Me.SystemOverviewToolStripMenuItem.Name = "SystemOverviewToolStripMenuItem"
        Me.SystemOverviewToolStripMenuItem.Size = New System.Drawing.Size(213, 44)
        Me.SystemOverviewToolStripMenuItem.Text = "Appointment"
        '
        'ReportsToolStripMenuItem
        '
        Me.ReportsToolStripMenuItem.Name = "ReportsToolStripMenuItem"
        Me.ReportsToolStripMenuItem.ShowShortcutKeys = False
        Me.ReportsToolStripMenuItem.Size = New System.Drawing.Size(138, 44)
        Me.ReportsToolStripMenuItem.Text = "Reports"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(281, 44)
        Me.ToolStripMenuItem1.Text = "Treatment Record"
        '
        'Guna2CustomGradientPanel1
        '
        Me.Guna2CustomGradientPanel1.Controls.Add(Me.Guna2HtmlLabel1)
        Me.Guna2CustomGradientPanel1.Location = New System.Drawing.Point(136, -2)
        Me.Guna2CustomGradientPanel1.Name = "Guna2CustomGradientPanel1"
        Me.Guna2CustomGradientPanel1.Size = New System.Drawing.Size(1177, 124)
        Me.Guna2CustomGradientPanel1.TabIndex = 20
        '
        'Guna2HtmlLabel1
        '
        Me.Guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel1.Font = New System.Drawing.Font("Palatino Linotype", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel1.Location = New System.Drawing.Point(364, 41)
        Me.Guna2HtmlLabel1.Name = "Guna2HtmlLabel1"
        Me.Guna2HtmlLabel1.Size = New System.Drawing.Size(371, 46)
        Me.Guna2HtmlLabel1.TabIndex = 21
        Me.Guna2HtmlLabel1.Text = "Eme Dental Clinic System"
        '
        'Guna2Panel1
        '
        Me.Guna2Panel1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Guna2Panel1.Controls.Add(Me.label5)
        Me.Guna2Panel1.Controls.Add(Me.lblTotalPatients)
        Me.Guna2Panel1.Location = New System.Drawing.Point(310, 165)
        Me.Guna2Panel1.Name = "Guna2Panel1"
        Me.Guna2Panel1.Size = New System.Drawing.Size(200, 210)
        Me.Guna2Panel1.TabIndex = 21
        '
        'label5
        '
        Me.label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label5.Location = New System.Drawing.Point(0, 157)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(200, 53)
        Me.label5.TabIndex = 25
        Me.label5.Text = "Total Patients"
        Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTotalPatients
        '
        Me.lblTotalPatients.Font = New System.Drawing.Font("Microsoft Sans Serif", 72.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalPatients.Location = New System.Drawing.Point(0, 0)
        Me.lblTotalPatients.Name = "lblTotalPatients"
        Me.lblTotalPatients.Size = New System.Drawing.Size(200, 157)
        Me.lblTotalPatients.TabIndex = 25
        Me.lblTotalPatients.Text = "0"
        Me.lblTotalPatients.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Guna2Panel2
        '
        Me.Guna2Panel2.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Guna2Panel2.Controls.Add(Me.label7)
        Me.Guna2Panel2.Controls.Add(Me.lblAppointmentsToday)
        Me.Guna2Panel2.Location = New System.Drawing.Point(824, 165)
        Me.Guna2Panel2.Name = "Guna2Panel2"
        Me.Guna2Panel2.Size = New System.Drawing.Size(200, 210)
        Me.Guna2Panel2.TabIndex = 22
        '
        'label7
        '
        Me.label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label7.Location = New System.Drawing.Point(0, 149)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(200, 61)
        Me.label7.TabIndex = 27
        Me.label7.Text = "Appointments Today"
        Me.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAppointmentsToday
        '
        Me.lblAppointmentsToday.Font = New System.Drawing.Font("Microsoft Sans Serif", 72.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppointmentsToday.Location = New System.Drawing.Point(0, 0)
        Me.lblAppointmentsToday.Name = "lblAppointmentsToday"
        Me.lblAppointmentsToday.Size = New System.Drawing.Size(200, 157)
        Me.lblAppointmentsToday.TabIndex = 27
        Me.lblAppointmentsToday.Text = "0"
        Me.lblAppointmentsToday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Guna2Panel3
        '
        Me.Guna2Panel3.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Guna2Panel3.Controls.Add(Me.label6)
        Me.Guna2Panel3.Controls.Add(Me.lblTotalDentists)
        Me.Guna2Panel3.Location = New System.Drawing.Point(569, 165)
        Me.Guna2Panel3.Name = "Guna2Panel3"
        Me.Guna2Panel3.Size = New System.Drawing.Size(200, 210)
        Me.Guna2Panel3.TabIndex = 23
        '
        'label6
        '
        Me.label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label6.Location = New System.Drawing.Point(0, 157)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(200, 53)
        Me.label6.TabIndex = 26
        Me.label6.Text = "Total Dentists"
        Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTotalDentists
        '
        Me.lblTotalDentists.Font = New System.Drawing.Font("Microsoft Sans Serif", 72.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalDentists.Location = New System.Drawing.Point(0, 0)
        Me.lblTotalDentists.Name = "lblTotalDentists"
        Me.lblTotalDentists.Size = New System.Drawing.Size(200, 157)
        Me.lblTotalDentists.TabIndex = 26
        Me.lblTotalDentists.Text = "0"
        Me.lblTotalDentists.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Guna2Panel4
        '
        Me.Guna2Panel4.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Guna2Panel4.Controls.Add(Me.label8)
        Me.Guna2Panel4.Controls.Add(Me.lblCompletedAppointments)
        Me.Guna2Panel4.Location = New System.Drawing.Point(1081, 165)
        Me.Guna2Panel4.Name = "Guna2Panel4"
        Me.Guna2Panel4.Size = New System.Drawing.Size(200, 210)
        Me.Guna2Panel4.TabIndex = 24
        '
        'label8
        '
        Me.label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label8.Location = New System.Drawing.Point(0, 149)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(200, 61)
        Me.label8.TabIndex = 28
        Me.label8.Text = "Completed Appointments"
        Me.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCompletedAppointments
        '
        Me.lblCompletedAppointments.Font = New System.Drawing.Font("Microsoft Sans Serif", 72.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompletedAppointments.Location = New System.Drawing.Point(0, 0)
        Me.lblCompletedAppointments.Name = "lblCompletedAppointments"
        Me.lblCompletedAppointments.Size = New System.Drawing.Size(200, 157)
        Me.lblCompletedAppointments.TabIndex = 28
        Me.lblCompletedAppointments.Text = "0"
        Me.lblCompletedAppointments.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'AdminDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(1311, 857)
        Me.Controls.Add(Me.Guna2Panel4)
        Me.Controls.Add(Me.Guna2Panel3)
        Me.Controls.Add(Me.Guna2Panel2)
        Me.Controls.Add(Me.Guna2Panel1)
        Me.Controls.Add(Me.Guna2CustomGradientPanel1)
        Me.Controls.Add(Me.Guna2PictureBox6)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "AdminDashboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AdminDashboard"
        CType(Me.Guna2PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Guna2CustomGradientPanel1.ResumeLayout(False)
        Me.Guna2CustomGradientPanel1.PerformLayout()
        Me.Guna2Panel1.ResumeLayout(False)
        Me.Guna2Panel2.ResumeLayout(False)
        Me.Guna2Panel3.ResumeLayout(False)
        Me.Guna2Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Guna2PictureBox6 As Guna.UI2.WinForms.Guna2PictureBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents SystemOverviewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ManageForm As ToolStripMenuItem
    Friend WithEvents AuditTrailToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReportsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Guna2CustomGradientPanel1 As Guna.UI2.WinForms.Guna2CustomGradientPanel
    Friend WithEvents Guna2HtmlLabel1 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents ManageUsersForm As ToolStripMenuItem
    Friend WithEvents ManageDentistsForm As ToolStripMenuItem
    Friend WithEvents ManagePatientsForm As ToolStripMenuItem
    Friend WithEvents ManageServicesForm As ToolStripMenuItem
    Friend WithEvents Guna2Panel1 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Guna2Panel2 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents label5 As Label
    Friend WithEvents lblTotalPatients As Label
    Friend WithEvents lblAppointmentsToday As Label
    Friend WithEvents Guna2Panel3 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblTotalDentists As Label
    Friend WithEvents Guna2Panel4 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblCompletedAppointments As Label
    Friend WithEvents label7 As Label
    Friend WithEvents label6 As Label
    Friend WithEvents label8 As Label
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
End Class
