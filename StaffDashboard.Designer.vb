<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StaffDashboard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StaffDashboard))
        Me.Guna2CustomGradientPanel1 = New Guna.UI2.WinForms.Guna2CustomGradientPanel()
        Me.LogoutPictureBox1 = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.lblClinicName = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.PatientManagementToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.AppointmentToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.BillingAssistance = New System.Windows.Forms.ToolStripMenuItem()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Guna2Panel4 = New Guna.UI2.WinForms.Guna2Panel()
        Me.label8 = New System.Windows.Forms.Label()
        Me.lblCompletedAppointments = New System.Windows.Forms.Label()
        Me.Guna2Panel3 = New Guna.UI2.WinForms.Guna2Panel()
        Me.label6 = New System.Windows.Forms.Label()
        Me.lblTotalDentists = New System.Windows.Forms.Label()
        Me.Guna2Panel2 = New Guna.UI2.WinForms.Guna2Panel()
        Me.label7 = New System.Windows.Forms.Label()
        Me.lblAppointmentsToday = New System.Windows.Forms.Label()
        Me.Guna2Panel1 = New Guna.UI2.WinForms.Guna2Panel()
        Me.label5 = New System.Windows.Forms.Label()
        Me.lblTotalPatients = New System.Windows.Forms.Label()
        Me.Guna2CustomGradientPanel1.SuspendLayout()
        CType(Me.LogoutPictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Guna2Panel4.SuspendLayout()
        Me.Guna2Panel3.SuspendLayout()
        Me.Guna2Panel2.SuspendLayout()
        Me.Guna2Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Guna2CustomGradientPanel1
        '
        Me.Guna2CustomGradientPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.Controls.Add(Me.PictureBox1)
        Me.Guna2CustomGradientPanel1.Controls.Add(Me.LogoutPictureBox1)
        Me.Guna2CustomGradientPanel1.Controls.Add(Me.lblClinicName)
        Me.Guna2CustomGradientPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Guna2CustomGradientPanel1.FillColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.FillColor3 = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.FillColor4 = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.Location = New System.Drawing.Point(0, 0)
        Me.Guna2CustomGradientPanel1.Name = "Guna2CustomGradientPanel1"
        Me.Guna2CustomGradientPanel1.Size = New System.Drawing.Size(1672, 124)
        Me.Guna2CustomGradientPanel1.TabIndex = 31
        '
        'LogoutPictureBox1
        '
        Me.LogoutPictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LogoutPictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.LogoutPictureBox1.FillColor = System.Drawing.Color.Transparent
        Me.LogoutPictureBox1.Image = CType(resources.GetObject("LogoutPictureBox1.Image"), System.Drawing.Image)
        Me.LogoutPictureBox1.ImageRotate = 0!
        Me.LogoutPictureBox1.Location = New System.Drawing.Point(1572, 29)
        Me.LogoutPictureBox1.Name = "LogoutPictureBox1"
        Me.LogoutPictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.LogoutPictureBox1.Size = New System.Drawing.Size(64, 64)
        Me.LogoutPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.LogoutPictureBox1.TabIndex = 33
        Me.LogoutPictureBox1.TabStop = False
        '
        'lblClinicName
        '
        Me.lblClinicName.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblClinicName.AutoSize = False
        Me.lblClinicName.BackColor = System.Drawing.Color.Transparent
        Me.lblClinicName.Font = New System.Drawing.Font("Mongolian Baiti", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClinicName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.lblClinicName.Location = New System.Drawing.Point(329, 1)
        Me.lblClinicName.Name = "lblClinicName"
        Me.lblClinicName.Size = New System.Drawing.Size(1342, 123)
        Me.lblClinicName.TabIndex = 21
        Me.lblClinicName.Text = "Dental Clinic System"
        Me.lblClinicName.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MenuStrip1
        '
        Me.MenuStrip1.AutoSize = False
        Me.MenuStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.MenuStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.MenuStrip1.Font = New System.Drawing.Font("Mongolian Baiti", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PatientManagementToolStripMenuItem1, Me.AppointmentToolStripMenuItem2, Me.BillingAssistance})
        Me.MenuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 124)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(327, 859)
        Me.MenuStrip1.TabIndex = 32
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'PatientManagementToolStripMenuItem1
        '
        Me.PatientManagementToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.PatientManagementToolStripMenuItem1.Name = "PatientManagementToolStripMenuItem1"
        Me.PatientManagementToolStripMenuItem1.Size = New System.Drawing.Size(323, 41)
        Me.PatientManagementToolStripMenuItem1.Text = "Patient Management"
        '
        'AppointmentToolStripMenuItem2
        '
        Me.AppointmentToolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.AppointmentToolStripMenuItem2.Name = "AppointmentToolStripMenuItem2"
        Me.AppointmentToolStripMenuItem2.Size = New System.Drawing.Size(217, 41)
        Me.AppointmentToolStripMenuItem2.Text = "Appointment"
        '
        'BillingAssistance
        '
        Me.BillingAssistance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.BillingAssistance.Name = "BillingAssistance"
        Me.BillingAssistance.Size = New System.Drawing.Size(290, 41)
        Me.BillingAssistance.Text = "Billing Assistance"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(0, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(327, 123)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 33
        Me.PictureBox1.TabStop = False
        '
        'Guna2Panel4
        '
        Me.Guna2Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.Guna2Panel4.Controls.Add(Me.label8)
        Me.Guna2Panel4.Controls.Add(Me.lblCompletedAppointments)
        Me.Guna2Panel4.Location = New System.Drawing.Point(1371, 214)
        Me.Guna2Panel4.Name = "Guna2Panel4"
        Me.Guna2Panel4.Size = New System.Drawing.Size(200, 210)
        Me.Guna2Panel4.TabIndex = 36
        '
        'label8
        '
        Me.label8.Font = New System.Drawing.Font("Mongolian Baiti", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
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
        Me.lblCompletedAppointments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.lblCompletedAppointments.Location = New System.Drawing.Point(0, 0)
        Me.lblCompletedAppointments.Name = "lblCompletedAppointments"
        Me.lblCompletedAppointments.Size = New System.Drawing.Size(200, 157)
        Me.lblCompletedAppointments.TabIndex = 28
        Me.lblCompletedAppointments.Text = "0"
        Me.lblCompletedAppointments.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Guna2Panel3
        '
        Me.Guna2Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.Guna2Panel3.Controls.Add(Me.label6)
        Me.Guna2Panel3.Controls.Add(Me.lblTotalDentists)
        Me.Guna2Panel3.Location = New System.Drawing.Point(743, 214)
        Me.Guna2Panel3.Name = "Guna2Panel3"
        Me.Guna2Panel3.Size = New System.Drawing.Size(200, 210)
        Me.Guna2Panel3.TabIndex = 35
        '
        'label6
        '
        Me.label6.Font = New System.Drawing.Font("Mongolian Baiti", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
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
        Me.lblTotalDentists.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.lblTotalDentists.Location = New System.Drawing.Point(0, 0)
        Me.lblTotalDentists.Name = "lblTotalDentists"
        Me.lblTotalDentists.Size = New System.Drawing.Size(200, 157)
        Me.lblTotalDentists.TabIndex = 26
        Me.lblTotalDentists.Text = "0"
        Me.lblTotalDentists.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Guna2Panel2
        '
        Me.Guna2Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.Guna2Panel2.Controls.Add(Me.label7)
        Me.Guna2Panel2.Controls.Add(Me.lblAppointmentsToday)
        Me.Guna2Panel2.Location = New System.Drawing.Point(1053, 214)
        Me.Guna2Panel2.Name = "Guna2Panel2"
        Me.Guna2Panel2.Size = New System.Drawing.Size(200, 210)
        Me.Guna2Panel2.TabIndex = 34
        '
        'label7
        '
        Me.label7.Font = New System.Drawing.Font("Mongolian Baiti", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
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
        Me.lblAppointmentsToday.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.lblAppointmentsToday.Location = New System.Drawing.Point(0, 0)
        Me.lblAppointmentsToday.Name = "lblAppointmentsToday"
        Me.lblAppointmentsToday.Size = New System.Drawing.Size(200, 157)
        Me.lblAppointmentsToday.TabIndex = 27
        Me.lblAppointmentsToday.Text = "0"
        Me.lblAppointmentsToday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Guna2Panel1
        '
        Me.Guna2Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.Guna2Panel1.BorderRadius = 10
        Me.Guna2Panel1.Controls.Add(Me.label5)
        Me.Guna2Panel1.Controls.Add(Me.lblTotalPatients)
        Me.Guna2Panel1.Location = New System.Drawing.Point(439, 214)
        Me.Guna2Panel1.Name = "Guna2Panel1"
        Me.Guna2Panel1.Size = New System.Drawing.Size(200, 210)
        Me.Guna2Panel1.TabIndex = 33
        '
        'label5
        '
        Me.label5.Font = New System.Drawing.Font("Mongolian Baiti", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
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
        Me.lblTotalPatients.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.lblTotalPatients.Location = New System.Drawing.Point(0, 0)
        Me.lblTotalPatients.Name = "lblTotalPatients"
        Me.lblTotalPatients.Size = New System.Drawing.Size(200, 157)
        Me.lblTotalPatients.TabIndex = 25
        Me.lblTotalPatients.Text = "0"
        Me.lblTotalPatients.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'StaffDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1672, 975)
        Me.Controls.Add(Me.Guna2Panel4)
        Me.Controls.Add(Me.Guna2Panel3)
        Me.Controls.Add(Me.Guna2Panel2)
        Me.Controls.Add(Me.Guna2Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.Guna2CustomGradientPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "StaffDashboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "StaffDashboard"
        Me.Guna2CustomGradientPanel1.ResumeLayout(False)
        CType(Me.LogoutPictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Guna2Panel4.ResumeLayout(False)
        Me.Guna2Panel3.ResumeLayout(False)
        Me.Guna2Panel2.ResumeLayout(False)
        Me.Guna2Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SystemOverviewToolStripMenuItem As ToolStripMenuItem

    Private Sub ManageForm_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs)

    End Sub

    Private Sub MenuStrip1_ItemClicked_1(sender As Object, e As ToolStripItemClickedEventArgs)

    End Sub

    Friend WithEvents Guna2CustomGradientPanel1 As Guna.UI2.WinForms.Guna2CustomGradientPanel
    Friend WithEvents lblClinicName As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents PatientManagementToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents AppointmentToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents BillingAssistance As ToolStripMenuItem
    Friend WithEvents LogoutPictureBox1 As Guna.UI2.WinForms.Guna2CirclePictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Guna2Panel4 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents label8 As Label
    Friend WithEvents lblCompletedAppointments As Label
    Friend WithEvents Guna2Panel3 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents label6 As Label
    Friend WithEvents lblTotalDentists As Label
    Friend WithEvents Guna2Panel2 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents label7 As Label
    Friend WithEvents lblAppointmentsToday As Label
    Friend WithEvents Guna2Panel1 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents label5 As Label
    Friend WithEvents lblTotalPatients As Label
End Class
