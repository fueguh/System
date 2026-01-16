<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Reports
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
        Me.BtnDailyAppointments = New Guna.UI2.WinForms.Guna2Button()
        Me.BtnMonthlyRevenue = New Guna.UI2.WinForms.Guna2Button()
        Me.BtnDentistPerformance = New Guna.UI2.WinForms.Guna2Button()
        Me.BtnPatientCount = New Guna.UI2.WinForms.Guna2Button()
        Me.DGVReports = New Guna.UI2.WinForms.Guna2DataGridView()
        CType(Me.DGVReports, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnDailyAppointments
        '
        Me.BtnDailyAppointments.BorderRadius = 10
        Me.BtnDailyAppointments.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BtnDailyAppointments.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BtnDailyAppointments.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BtnDailyAppointments.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BtnDailyAppointments.FillColor = System.Drawing.Color.White
        Me.BtnDailyAppointments.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.BtnDailyAppointments.ForeColor = System.Drawing.Color.Gray
        Me.BtnDailyAppointments.Location = New System.Drawing.Point(48, 101)
        Me.BtnDailyAppointments.Name = "BtnDailyAppointments"
        Me.BtnDailyAppointments.Size = New System.Drawing.Size(227, 45)
        Me.BtnDailyAppointments.TabIndex = 0
        Me.BtnDailyAppointments.Text = "Daily Appointments"
        '
        'BtnMonthlyRevenue
        '
        Me.BtnMonthlyRevenue.BorderRadius = 10
        Me.BtnMonthlyRevenue.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BtnMonthlyRevenue.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BtnMonthlyRevenue.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BtnMonthlyRevenue.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BtnMonthlyRevenue.FillColor = System.Drawing.Color.White
        Me.BtnMonthlyRevenue.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.BtnMonthlyRevenue.ForeColor = System.Drawing.Color.Gray
        Me.BtnMonthlyRevenue.Location = New System.Drawing.Point(48, 171)
        Me.BtnMonthlyRevenue.Name = "BtnMonthlyRevenue"
        Me.BtnMonthlyRevenue.Size = New System.Drawing.Size(227, 45)
        Me.BtnMonthlyRevenue.TabIndex = 1
        Me.BtnMonthlyRevenue.Text = "Monthly Revenue"
        '
        'BtnDentistPerformance
        '
        Me.BtnDentistPerformance.BorderRadius = 10
        Me.BtnDentistPerformance.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BtnDentistPerformance.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BtnDentistPerformance.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BtnDentistPerformance.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BtnDentistPerformance.FillColor = System.Drawing.Color.White
        Me.BtnDentistPerformance.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.BtnDentistPerformance.ForeColor = System.Drawing.Color.Gray
        Me.BtnDentistPerformance.Location = New System.Drawing.Point(48, 242)
        Me.BtnDentistPerformance.Name = "BtnDentistPerformance"
        Me.BtnDentistPerformance.Size = New System.Drawing.Size(227, 45)
        Me.BtnDentistPerformance.TabIndex = 2
        Me.BtnDentistPerformance.Text = "Dentist Performance"
        '
        'BtnPatientCount
        '
        Me.BtnPatientCount.BorderRadius = 10
        Me.BtnPatientCount.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BtnPatientCount.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BtnPatientCount.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BtnPatientCount.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BtnPatientCount.FillColor = System.Drawing.Color.White
        Me.BtnPatientCount.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.BtnPatientCount.ForeColor = System.Drawing.Color.Gray
        Me.BtnPatientCount.Location = New System.Drawing.Point(48, 316)
        Me.BtnPatientCount.Name = "BtnPatientCount"
        Me.BtnPatientCount.Size = New System.Drawing.Size(227, 45)
        Me.BtnPatientCount.TabIndex = 3
        Me.BtnPatientCount.Text = "Patient counts"
        '
        'DGVReports
        '
        Me.DGVReports.AllowUserToAddRows = False
        Me.DGVReports.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.DGVReports.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVReports.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGVReports.ColumnHeadersHeight = 25
        Me.DGVReports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVReports.DefaultCellStyle = DataGridViewCellStyle3
        Me.DGVReports.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVReports.Location = New System.Drawing.Point(331, 101)
        Me.DGVReports.Name = "DGVReports"
        Me.DGVReports.ReadOnly = True
        Me.DGVReports.RowHeadersVisible = False
        Me.DGVReports.Size = New System.Drawing.Size(1190, 595)
        Me.DGVReports.TabIndex = 4
        Me.DGVReports.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVReports.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.DGVReports.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.DGVReports.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.DGVReports.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.DGVReports.ThemeStyle.BackColor = System.Drawing.Color.White
        Me.DGVReports.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVReports.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVReports.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DGVReports.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVReports.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White
        Me.DGVReports.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        Me.DGVReports.ThemeStyle.HeaderStyle.Height = 25
        Me.DGVReports.ThemeStyle.ReadOnly = True
        Me.DGVReports.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVReports.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DGVReports.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVReports.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.DGVReports.ThemeStyle.RowsStyle.Height = 22
        Me.DGVReports.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVReports.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        '
        'Reports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1571, 740)
        Me.ControlBox = False
        Me.Controls.Add(Me.DGVReports)
        Me.Controls.Add(Me.BtnPatientCount)
        Me.Controls.Add(Me.BtnDentistPerformance)
        Me.Controls.Add(Me.BtnMonthlyRevenue)
        Me.Controls.Add(Me.BtnDailyAppointments)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Reports"
        Me.Text = "Reports"
        CType(Me.DGVReports, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnDailyAppointments As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents BtnMonthlyRevenue As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents BtnDentistPerformance As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents BtnPatientCount As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents DGVReports As Guna.UI2.WinForms.Guna2DataGridView
End Class
