<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Login
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
        Me.txtUsername = New Guna.UI2.WinForms.Guna2TextBox()
        Me.txtPassword = New Guna.UI2.WinForms.Guna2TextBox()
        Me.btnLogin = New Guna.UI2.WinForms.Guna2Button()
        Me.Guna2MessageDialog1 = New Guna.UI2.WinForms.Guna2MessageDialog()
        Me.CheckBoxShowPassword = New Guna.UI2.WinForms.Guna2CheckBox()
        Me.chkRememberMe = New Guna.UI2.WinForms.Guna2CheckBox()
        Me.lbl_clinicName = New System.Windows.Forms.Label()
        Me.lbl_Location = New System.Windows.Forms.Label()
        Me.lbl_contact = New System.Windows.Forms.Label()
        Me.lbl_email = New System.Windows.Forms.Label()
        Me.lbl_schedule = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtUsername
        '
        Me.txtUsername.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtUsername.BorderRadius = 15
        Me.txtUsername.BorderThickness = 2
        Me.txtUsername.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUsername.DefaultText = ""
        Me.txtUsername.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtUsername.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtUsername.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtUsername.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtUsername.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUsername.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!)
        Me.txtUsername.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtUsername.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUsername.Location = New System.Drawing.Point(117, 225)
        Me.txtUsername.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtUsername.PlaceholderText = "Username"
        Me.txtUsername.SelectedText = ""
        Me.txtUsername.Size = New System.Drawing.Size(257, 50)
        Me.txtUsername.TabIndex = 0
        '
        'txtPassword
        '
        Me.txtPassword.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtPassword.BorderRadius = 15
        Me.txtPassword.BorderThickness = 2
        Me.txtPassword.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPassword.DefaultText = ""
        Me.txtPassword.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtPassword.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtPassword.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPassword.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!)
        Me.txtPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtPassword.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPassword.Location = New System.Drawing.Point(117, 317)
        Me.txtPassword.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.txtPassword.PlaceholderText = "Password"
        Me.txtPassword.SelectedText = ""
        Me.txtPassword.Size = New System.Drawing.Size(257, 50)
        Me.txtPassword.TabIndex = 1
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'btnLogin
        '
        Me.btnLogin.BorderColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.btnLogin.BorderRadius = 10
        Me.btnLogin.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnLogin.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnLogin.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnLogin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnLogin.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.btnLogin.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!)
        Me.btnLogin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.btnLogin.Location = New System.Drawing.Point(153, 456)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(180, 45)
        Me.btnLogin.TabIndex = 3
        Me.btnLogin.Text = "Login"
        '
        'Guna2MessageDialog1
        '
        Me.Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
        Me.Guna2MessageDialog1.Caption = Nothing
        Me.Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.None
        Me.Guna2MessageDialog1.Parent = Nothing
        Me.Guna2MessageDialog1.Style = Guna.UI2.WinForms.MessageDialogStyle.[Default]
        Me.Guna2MessageDialog1.Text = Nothing
        '
        'CheckBoxShowPassword
        '
        Me.CheckBoxShowPassword.AutoSize = True
        Me.CheckBoxShowPassword.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.CheckBoxShowPassword.CheckedState.BorderRadius = 7
        Me.CheckBoxShowPassword.CheckedState.BorderThickness = 1
        Me.CheckBoxShowPassword.CheckedState.FillColor = System.Drawing.Color.White
        Me.CheckBoxShowPassword.CheckMarkColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.CheckBoxShowPassword.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!)
        Me.CheckBoxShowPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.CheckBoxShowPassword.Location = New System.Drawing.Point(117, 374)
        Me.CheckBoxShowPassword.Name = "CheckBoxShowPassword"
        Me.CheckBoxShowPassword.Size = New System.Drawing.Size(128, 20)
        Me.CheckBoxShowPassword.TabIndex = 7
        Me.CheckBoxShowPassword.Text = "Show Password"
        Me.CheckBoxShowPassword.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.CheckBoxShowPassword.UncheckedState.BorderRadius = 7
        Me.CheckBoxShowPassword.UncheckedState.BorderThickness = 1
        Me.CheckBoxShowPassword.UncheckedState.FillColor = System.Drawing.Color.White
        '
        'chkRememberMe
        '
        Me.chkRememberMe.AutoSize = True
        Me.chkRememberMe.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.chkRememberMe.CheckedState.BorderRadius = 7
        Me.chkRememberMe.CheckedState.BorderThickness = 1
        Me.chkRememberMe.CheckedState.FillColor = System.Drawing.Color.White
        Me.chkRememberMe.CheckMarkColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.chkRememberMe.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRememberMe.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.chkRememberMe.Location = New System.Drawing.Point(179, 507)
        Me.chkRememberMe.Name = "chkRememberMe"
        Me.chkRememberMe.Size = New System.Drawing.Size(118, 20)
        Me.chkRememberMe.TabIndex = 2
        Me.chkRememberMe.Text = "Remember me"
        Me.chkRememberMe.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.chkRememberMe.UncheckedState.BorderRadius = 7
        Me.chkRememberMe.UncheckedState.BorderThickness = 1
        Me.chkRememberMe.UncheckedState.FillColor = System.Drawing.Color.White
        '
        'lbl_clinicName
        '
        Me.lbl_clinicName.Font = New System.Drawing.Font("Georgia", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_clinicName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.lbl_clinicName.Location = New System.Drawing.Point(-4, 42)
        Me.lbl_clinicName.Name = "lbl_clinicName"
        Me.lbl_clinicName.Size = New System.Drawing.Size(517, 59)
        Me.lbl_clinicName.TabIndex = 8
        Me.lbl_clinicName.Text = "Clinic Name:"
        Me.lbl_clinicName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Location
        '
        Me.lbl_Location.Font = New System.Drawing.Font("Georgia", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Location.ForeColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.lbl_Location.Location = New System.Drawing.Point(3, 101)
        Me.lbl_Location.Name = "lbl_Location"
        Me.lbl_Location.Size = New System.Drawing.Size(510, 46)
        Me.lbl_Location.TabIndex = 8
        Me.lbl_Location.Text = "Location:"
        Me.lbl_Location.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_contact
        '
        Me.lbl_contact.Font = New System.Drawing.Font("Georgia", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_contact.ForeColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.lbl_contact.Location = New System.Drawing.Point(3, 645)
        Me.lbl_contact.Name = "lbl_contact"
        Me.lbl_contact.Size = New System.Drawing.Size(510, 37)
        Me.lbl_contact.TabIndex = 8
        Me.lbl_contact.Text = "Contact number:"
        Me.lbl_contact.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_email
        '
        Me.lbl_email.Font = New System.Drawing.Font("Georgia", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_email.ForeColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.lbl_email.Location = New System.Drawing.Point(3, 682)
        Me.lbl_email.Name = "lbl_email"
        Me.lbl_email.Size = New System.Drawing.Size(510, 37)
        Me.lbl_email.TabIndex = 8
        Me.lbl_email.Text = "Email address:"
        Me.lbl_email.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_schedule
        '
        Me.lbl_schedule.Font = New System.Drawing.Font("Georgia", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_schedule.ForeColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.lbl_schedule.Location = New System.Drawing.Point(3, 147)
        Me.lbl_schedule.Name = "lbl_schedule"
        Me.lbl_schedule.Size = New System.Drawing.Size(510, 53)
        Me.lbl_schedule.TabIndex = 8
        Me.lbl_schedule.Text = "Schedule:"
        Me.lbl_schedule.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Login
        '
        Me.AcceptButton = Me.btnLogin
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(512, 719)
        Me.Controls.Add(Me.lbl_email)
        Me.Controls.Add(Me.lbl_contact)
        Me.Controls.Add(Me.lbl_schedule)
        Me.Controls.Add(Me.lbl_Location)
        Me.Controls.Add(Me.lbl_clinicName)
        Me.Controls.Add(Me.CheckBoxShowPassword)
        Me.Controls.Add(Me.chkRememberMe)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUsername)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Login"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtUsername As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents txtPassword As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents btnLogin As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Guna2MessageDialog1 As Guna.UI2.WinForms.Guna2MessageDialog
    Friend WithEvents CheckBoxShowPassword As Guna.UI2.WinForms.Guna2CheckBox
    Friend WithEvents chkRememberMe As Guna.UI2.WinForms.Guna2CheckBox
    Friend WithEvents lbl_clinicName As Label
    Friend WithEvents lbl_Location As Label
    Friend WithEvents lbl_contact As Label
    Friend WithEvents lbl_email As Label
    Friend WithEvents lbl_schedule As Label
End Class
