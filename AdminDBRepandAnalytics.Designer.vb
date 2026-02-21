<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdminDBRepandAnalytics
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdminDBRepandAnalytics))
        Me.GrpFilters = New Guna.UI2.WinForms.Guna2GroupBox()
        Me.Guna2HtmlLabel5 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.CmbSupplier = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.RBAll = New Guna.UI2.WinForms.Guna2RadioButton()
        Me.BtnGenerateReport = New Guna.UI2.WinForms.Guna2Button()
        Me.Guna2HtmlLabel3 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.RBOut = New Guna.UI2.WinForms.Guna2RadioButton()
        Me.BRIn = New Guna.UI2.WinForms.Guna2RadioButton()
        Me.Guna2HtmlLabel2 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.DtpTo = New Guna.UI2.WinForms.Guna2DateTimePicker()
        Me.Guna2HtmlLabel1 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.DtpFrom = New Guna.UI2.WinForms.Guna2DateTimePicker()
        Me.TebReports = New Guna.UI2.WinForms.Guna2TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.DGVItemManagement = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.DGVStockTrackTransaction = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.Guna2BorderlessForm1 = New Guna.UI2.WinForms.Guna2BorderlessForm(Me.components)
        Me.ChartStockLevels = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.ChartTransactionTrends = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.LBLTotalIn = New System.Windows.Forms.Label()
        Me.LBLTotalOut = New System.Windows.Forms.Label()
        Me.LBLCurrentStock = New System.Windows.Forms.Label()
        Me.btnBack = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.Guna2CustomGradientPanel1 = New Guna.UI2.WinForms.Guna2CustomGradientPanel()
        Me.Guna2HtmlLabel6 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GrpFilters.SuspendLayout()
        Me.TebReports.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DGVItemManagement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.DGVStockTrackTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChartStockLevels, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChartTransactionTrends, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Guna2CustomGradientPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GrpFilters
        '
        Me.GrpFilters.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.GrpFilters.BorderThickness = 0
        Me.GrpFilters.Controls.Add(Me.Guna2HtmlLabel5)
        Me.GrpFilters.Controls.Add(Me.CmbSupplier)
        Me.GrpFilters.Controls.Add(Me.RBAll)
        Me.GrpFilters.Controls.Add(Me.BtnGenerateReport)
        Me.GrpFilters.Controls.Add(Me.Guna2HtmlLabel3)
        Me.GrpFilters.Controls.Add(Me.RBOut)
        Me.GrpFilters.Controls.Add(Me.BRIn)
        Me.GrpFilters.Controls.Add(Me.Guna2HtmlLabel2)
        Me.GrpFilters.Controls.Add(Me.DtpTo)
        Me.GrpFilters.Controls.Add(Me.Guna2HtmlLabel1)
        Me.GrpFilters.Controls.Add(Me.DtpFrom)
        Me.GrpFilters.FillColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.GrpFilters.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpFilters.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.GrpFilters.Location = New System.Drawing.Point(0, 106)
        Me.GrpFilters.Name = "GrpFilters"
        Me.GrpFilters.Size = New System.Drawing.Size(1659, 238)
        Me.GrpFilters.TabIndex = 0
        Me.GrpFilters.Text = "Filter by:"
        '
        'Guna2HtmlLabel5
        '
        Me.Guna2HtmlLabel5.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel5.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel5.Location = New System.Drawing.Point(623, 78)
        Me.Guna2HtmlLabel5.Name = "Guna2HtmlLabel5"
        Me.Guna2HtmlLabel5.Size = New System.Drawing.Size(77, 28)
        Me.Guna2HtmlLabel5.TabIndex = 49
        Me.Guna2HtmlLabel5.Text = "Supplier:"
        '
        'CmbSupplier
        '
        Me.CmbSupplier.BackColor = System.Drawing.Color.Transparent
        Me.CmbSupplier.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.CmbSupplier.BorderRadius = 10
        Me.CmbSupplier.BorderThickness = 2
        Me.CmbSupplier.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSupplier.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbSupplier.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbSupplier.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbSupplier.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.CmbSupplier.ItemHeight = 30
        Me.CmbSupplier.Items.AddRange(New Object() {"Dental Supplies (gloves, masks, syringes)", "Medicines (pain relievers, antibiotics)", "Equipment (handpieces, sterilizers)", "Consumables (cotton rolls, gauze, alcohol)", "Office Supplies (paper, pens, printer ink)"})
        Me.CmbSupplier.Location = New System.Drawing.Point(749, 70)
        Me.CmbSupplier.Name = "CmbSupplier"
        Me.CmbSupplier.Size = New System.Drawing.Size(388, 36)
        Me.CmbSupplier.TabIndex = 48
        '
        'RBAll
        '
        Me.RBAll.AutoSize = True
        Me.RBAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.RBAll.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.RBAll.CheckedState.BorderThickness = 0
        Me.RBAll.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.RBAll.CheckedState.InnerColor = System.Drawing.Color.White
        Me.RBAll.CheckedState.InnerOffset = -4
        Me.RBAll.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!)
        Me.RBAll.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.RBAll.Location = New System.Drawing.Point(1050, 155)
        Me.RBAll.Name = "RBAll"
        Me.RBAll.Size = New System.Drawing.Size(57, 20)
        Me.RBAll.TabIndex = 47
        Me.RBAll.Text = "ALL"
        Me.RBAll.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.RBAll.UncheckedState.BorderThickness = 2
        Me.RBAll.UncheckedState.FillColor = System.Drawing.Color.Transparent
        Me.RBAll.UncheckedState.InnerColor = System.Drawing.Color.Transparent
        Me.RBAll.UseVisualStyleBackColor = False
        '
        'BtnGenerateReport
        '
        Me.BtnGenerateReport.BorderRadius = 10
        Me.BtnGenerateReport.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BtnGenerateReport.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BtnGenerateReport.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BtnGenerateReport.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BtnGenerateReport.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.BtnGenerateReport.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnGenerateReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.BtnGenerateReport.Location = New System.Drawing.Point(1431, 174)
        Me.BtnGenerateReport.Name = "BtnGenerateReport"
        Me.BtnGenerateReport.Size = New System.Drawing.Size(158, 45)
        Me.BtnGenerateReport.TabIndex = 46
        Me.BtnGenerateReport.Text = "Generate Report"
        '
        'Guna2HtmlLabel3
        '
        Me.Guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel3.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel3.Location = New System.Drawing.Point(623, 146)
        Me.Guna2HtmlLabel3.Name = "Guna2HtmlLabel3"
        Me.Guna2HtmlLabel3.Size = New System.Drawing.Size(147, 28)
        Me.Guna2HtmlLabel3.TabIndex = 43
        Me.Guna2HtmlLabel3.Text = "Transaction type:"
        '
        'RBOut
        '
        Me.RBOut.AutoSize = True
        Me.RBOut.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.RBOut.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.RBOut.CheckedState.BorderThickness = 0
        Me.RBOut.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.RBOut.CheckedState.InnerColor = System.Drawing.Color.White
        Me.RBOut.CheckedState.InnerOffset = -4
        Me.RBOut.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!)
        Me.RBOut.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.RBOut.Location = New System.Drawing.Point(928, 155)
        Me.RBOut.Name = "RBOut"
        Me.RBOut.Size = New System.Drawing.Size(59, 20)
        Me.RBOut.TabIndex = 42
        Me.RBOut.Text = "OUT"
        Me.RBOut.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.RBOut.UncheckedState.BorderThickness = 2
        Me.RBOut.UncheckedState.FillColor = System.Drawing.Color.Transparent
        Me.RBOut.UncheckedState.InnerColor = System.Drawing.Color.Transparent
        Me.RBOut.UseVisualStyleBackColor = False
        '
        'BRIn
        '
        Me.BRIn.AutoSize = True
        Me.BRIn.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.BRIn.CheckedState.BorderThickness = 0
        Me.BRIn.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.BRIn.CheckedState.InnerColor = System.Drawing.Color.White
        Me.BRIn.CheckedState.InnerOffset = -4
        Me.BRIn.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BRIn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.BRIn.Location = New System.Drawing.Point(809, 155)
        Me.BRIn.Name = "BRIn"
        Me.BRIn.Size = New System.Drawing.Size(42, 20)
        Me.BRIn.TabIndex = 41
        Me.BRIn.Text = "IN"
        Me.BRIn.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.BRIn.UncheckedState.BorderThickness = 2
        Me.BRIn.UncheckedState.FillColor = System.Drawing.Color.Transparent
        Me.BRIn.UncheckedState.InnerColor = System.Drawing.Color.Transparent
        '
        'Guna2HtmlLabel2
        '
        Me.Guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel2.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel2.Location = New System.Drawing.Point(27, 146)
        Me.Guna2HtmlLabel2.Name = "Guna2HtmlLabel2"
        Me.Guna2HtmlLabel2.Size = New System.Drawing.Size(31, 28)
        Me.Guna2HtmlLabel2.TabIndex = 40
        Me.Guna2HtmlLabel2.Text = "To:"
        '
        'DtpTo
        '
        Me.DtpTo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.DtpTo.BorderRadius = 10
        Me.DtpTo.BorderThickness = 2
        Me.DtpTo.Checked = True
        Me.DtpTo.FillColor = System.Drawing.Color.White
        Me.DtpTo.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!)
        Me.DtpTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.DtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.DtpTo.Location = New System.Drawing.Point(111, 135)
        Me.DtpTo.Margin = New System.Windows.Forms.Padding(4)
        Me.DtpTo.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.DtpTo.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpTo.Name = "DtpTo"
        Me.DtpTo.Size = New System.Drawing.Size(388, 39)
        Me.DtpTo.TabIndex = 39
        Me.DtpTo.Value = New Date(2026, 2, 19, 0, 0, 0, 0)
        '
        'Guna2HtmlLabel1
        '
        Me.Guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel1.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2HtmlLabel1.Location = New System.Drawing.Point(27, 78)
        Me.Guna2HtmlLabel1.Name = "Guna2HtmlLabel1"
        Me.Guna2HtmlLabel1.Size = New System.Drawing.Size(53, 28)
        Me.Guna2HtmlLabel1.TabIndex = 38
        Me.Guna2HtmlLabel1.Text = "From:"
        '
        'DtpFrom
        '
        Me.DtpFrom.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.DtpFrom.BorderRadius = 10
        Me.DtpFrom.BorderThickness = 2
        Me.DtpFrom.Checked = True
        Me.DtpFrom.FillColor = System.Drawing.Color.White
        Me.DtpFrom.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.DtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.DtpFrom.Location = New System.Drawing.Point(111, 67)
        Me.DtpFrom.Margin = New System.Windows.Forms.Padding(4)
        Me.DtpFrom.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.DtpFrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpFrom.Name = "DtpFrom"
        Me.DtpFrom.Size = New System.Drawing.Size(388, 39)
        Me.DtpFrom.TabIndex = 5
        Me.DtpFrom.Value = New Date(2026, 2, 19, 0, 0, 0, 0)
        '
        'TebReports
        '
        Me.TebReports.Controls.Add(Me.TabPage1)
        Me.TebReports.Controls.Add(Me.TabPage2)
        Me.TebReports.ItemSize = New System.Drawing.Size(180, 40)
        Me.TebReports.Location = New System.Drawing.Point(0, 350)
        Me.TebReports.Name = "TebReports"
        Me.TebReports.SelectedIndex = 0
        Me.TebReports.Size = New System.Drawing.Size(927, 678)
        Me.TebReports.TabButtonHoverState.BorderColor = System.Drawing.Color.Empty
        Me.TebReports.TabButtonHoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(52, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.TebReports.TabButtonHoverState.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.TebReports.TabButtonHoverState.ForeColor = System.Drawing.Color.White
        Me.TebReports.TabButtonHoverState.InnerColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(52, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.TebReports.TabButtonIdleState.BorderColor = System.Drawing.Color.Empty
        Me.TebReports.TabButtonIdleState.FillColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(57, Byte), Integer))
        Me.TebReports.TabButtonIdleState.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.TebReports.TabButtonIdleState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(156, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(167, Byte), Integer))
        Me.TebReports.TabButtonIdleState.InnerColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(57, Byte), Integer))
        Me.TebReports.TabButtonSelectedState.BorderColor = System.Drawing.Color.Empty
        Me.TebReports.TabButtonSelectedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(29, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(49, Byte), Integer))
        Me.TebReports.TabButtonSelectedState.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.TebReports.TabButtonSelectedState.ForeColor = System.Drawing.Color.White
        Me.TebReports.TabButtonSelectedState.InnerColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(132, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TebReports.TabButtonSize = New System.Drawing.Size(180, 40)
        Me.TebReports.TabIndex = 47
        Me.TebReports.TabMenuBackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.TebReports.TabMenuOrientation = Guna.UI2.WinForms.TabMenuOrientation.HorizontalTop
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.DGVItemManagement)
        Me.TabPage1.Location = New System.Drawing.Point(4, 44)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(919, 630)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Item Management"
        '
        'DGVItemManagement
        '
        Me.DGVItemManagement.AllowUserToAddRows = False
        Me.DGVItemManagement.AllowUserToDeleteRows = False
        Me.DGVItemManagement.AllowUserToResizeColumns = False
        Me.DGVItemManagement.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.DGVItemManagement.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(122, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(178, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVItemManagement.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGVItemManagement.ColumnHeadersHeight = 30
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVItemManagement.DefaultCellStyle = DataGridViewCellStyle3
        Me.DGVItemManagement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVItemManagement.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVItemManagement.Location = New System.Drawing.Point(3, 3)
        Me.DGVItemManagement.Name = "DGVItemManagement"
        Me.DGVItemManagement.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVItemManagement.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DGVItemManagement.RowHeadersVisible = False
        Me.DGVItemManagement.RowHeadersWidth = 51
        Me.DGVItemManagement.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.DGVItemManagement.Size = New System.Drawing.Size(913, 624)
        Me.DGVItemManagement.TabIndex = 35
        Me.DGVItemManagement.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVItemManagement.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.DGVItemManagement.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.DGVItemManagement.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.DGVItemManagement.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.DGVItemManagement.ThemeStyle.BackColor = System.Drawing.Color.White
        Me.DGVItemManagement.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVItemManagement.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVItemManagement.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DGVItemManagement.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVItemManagement.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White
        Me.DGVItemManagement.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DGVItemManagement.ThemeStyle.HeaderStyle.Height = 30
        Me.DGVItemManagement.ThemeStyle.ReadOnly = True
        Me.DGVItemManagement.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVItemManagement.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DGVItemManagement.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVItemManagement.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.DGVItemManagement.ThemeStyle.RowsStyle.Height = 22
        Me.DGVItemManagement.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVItemManagement.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.DGVStockTrackTransaction)
        Me.TabPage2.Font = New System.Drawing.Font("Mongolian Baiti", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage2.Location = New System.Drawing.Point(4, 44)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(919, 630)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Stock Tracking/Transaction"
        '
        'DGVStockTrackTransaction
        '
        Me.DGVStockTrackTransaction.AllowUserToAddRows = False
        Me.DGVStockTrackTransaction.AllowUserToDeleteRows = False
        Me.DGVStockTrackTransaction.AllowUserToResizeColumns = False
        Me.DGVStockTrackTransaction.AllowUserToResizeRows = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.White
        Me.DGVStockTrackTransaction.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Mongolian Baiti", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(122, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(178, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVStockTrackTransaction.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.DGVStockTrackTransaction.ColumnHeadersHeight = 30
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Mongolian Baiti", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVStockTrackTransaction.DefaultCellStyle = DataGridViewCellStyle7
        Me.DGVStockTrackTransaction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVStockTrackTransaction.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVStockTrackTransaction.Location = New System.Drawing.Point(3, 3)
        Me.DGVStockTrackTransaction.Name = "DGVStockTrackTransaction"
        Me.DGVStockTrackTransaction.ReadOnly = True
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Mongolian Baiti", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVStockTrackTransaction.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.DGVStockTrackTransaction.RowHeadersVisible = False
        Me.DGVStockTrackTransaction.RowHeadersWidth = 51
        Me.DGVStockTrackTransaction.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.DGVStockTrackTransaction.Size = New System.Drawing.Size(913, 624)
        Me.DGVStockTrackTransaction.TabIndex = 36
        Me.DGVStockTrackTransaction.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVStockTrackTransaction.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.DGVStockTrackTransaction.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.DGVStockTrackTransaction.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.DGVStockTrackTransaction.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.DGVStockTrackTransaction.ThemeStyle.BackColor = System.Drawing.Color.White
        Me.DGVStockTrackTransaction.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVStockTrackTransaction.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVStockTrackTransaction.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DGVStockTrackTransaction.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVStockTrackTransaction.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White
        Me.DGVStockTrackTransaction.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DGVStockTrackTransaction.ThemeStyle.HeaderStyle.Height = 30
        Me.DGVStockTrackTransaction.ThemeStyle.ReadOnly = True
        Me.DGVStockTrackTransaction.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVStockTrackTransaction.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DGVStockTrackTransaction.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVStockTrackTransaction.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.DGVStockTrackTransaction.ThemeStyle.RowsStyle.Height = 22
        Me.DGVStockTrackTransaction.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVStockTrackTransaction.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        '
        'Guna2BorderlessForm1
        '
        Me.Guna2BorderlessForm1.ContainerControl = Me
        Me.Guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6R
        Me.Guna2BorderlessForm1.TransparentWhileDrag = True
        '
        'ChartStockLevels
        '
        Me.ChartStockLevels.BorderlineColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.ChartStockLevels.BorderlineWidth = 3
        ChartArea2.AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal
        ChartArea2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        ChartArea2.Name = "ChartArea1"
        Me.ChartStockLevels.ChartAreas.Add(ChartArea2)
        Legend2.Name = "Legend1"
        Me.ChartStockLevels.Legends.Add(Legend2)
        Me.ChartStockLevels.Location = New System.Drawing.Point(926, 531)
        Me.ChartStockLevels.Name = "ChartStockLevels"
        Me.ChartStockLevels.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright
        Series2.ChartArea = "ChartArea1"
        Series2.Font = New System.Drawing.Font("Mongolian Baiti", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Series2.LabelForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Series2.Legend = "Legend1"
        Series2.Name = "Stocks"
        Me.ChartStockLevels.Series.Add(Series2)
        Me.ChartStockLevels.Size = New System.Drawing.Size(733, 255)
        Me.ChartStockLevels.TabIndex = 49
        Me.ChartStockLevels.Text = "Chart1"
        '
        'ChartTransactionTrends
        '
        ChartArea1.Name = "ChartArea1"
        Me.ChartTransactionTrends.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.ChartTransactionTrends.Legends.Add(Legend1)
        Me.ChartTransactionTrends.Location = New System.Drawing.Point(926, 786)
        Me.ChartTransactionTrends.Name = "ChartTransactionTrends"
        Me.ChartTransactionTrends.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.ChartTransactionTrends.Series.Add(Series1)
        Me.ChartTransactionTrends.Size = New System.Drawing.Size(733, 242)
        Me.ChartTransactionTrends.TabIndex = 51
        Me.ChartTransactionTrends.Text = "Chart3"
        '
        'LBLTotalIn
        '
        Me.LBLTotalIn.BackColor = System.Drawing.Color.FromArgb(CType(CType(122, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(178, Byte), Integer))
        Me.LBLTotalIn.Font = New System.Drawing.Font("Mongolian Baiti", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTotalIn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.LBLTotalIn.Location = New System.Drawing.Point(945, 379)
        Me.LBLTotalIn.Name = "LBLTotalIn"
        Me.LBLTotalIn.Size = New System.Drawing.Size(331, 50)
        Me.LBLTotalIn.TabIndex = 50
        Me.LBLTotalIn.Text = "Label1"
        Me.LBLTotalIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBLTotalOut
        '
        Me.LBLTotalOut.BackColor = System.Drawing.Color.FromArgb(CType(CType(122, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(178, Byte), Integer))
        Me.LBLTotalOut.Font = New System.Drawing.Font("Mongolian Baiti", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTotalOut.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.LBLTotalOut.Location = New System.Drawing.Point(1299, 379)
        Me.LBLTotalOut.Name = "LBLTotalOut"
        Me.LBLTotalOut.Size = New System.Drawing.Size(337, 50)
        Me.LBLTotalOut.TabIndex = 52
        Me.LBLTotalOut.Text = "Label2"
        Me.LBLTotalOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBLCurrentStock
        '
        Me.LBLCurrentStock.BackColor = System.Drawing.Color.FromArgb(CType(CType(122, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(178, Byte), Integer))
        Me.LBLCurrentStock.Font = New System.Drawing.Font("Mongolian Baiti", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLCurrentStock.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.LBLCurrentStock.Location = New System.Drawing.Point(945, 469)
        Me.LBLCurrentStock.Name = "LBLCurrentStock"
        Me.LBLCurrentStock.Size = New System.Drawing.Size(691, 50)
        Me.LBLCurrentStock.TabIndex = 53
        Me.LBLCurrentStock.Text = "Label3"
        Me.LBLCurrentStock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.FillColor = System.Drawing.Color.LightGray
        Me.btnBack.Image = CType(resources.GetObject("btnBack.Image"), System.Drawing.Image)
        Me.btnBack.ImageRotate = 0!
        Me.btnBack.Location = New System.Drawing.Point(1576, 26)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.btnBack.Size = New System.Drawing.Size(60, 58)
        Me.btnBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnBack.TabIndex = 50
        Me.btnBack.TabStop = False
        '
        'Guna2CustomGradientPanel1
        '
        Me.Guna2CustomGradientPanel1.Controls.Add(Me.btnBack)
        Me.Guna2CustomGradientPanel1.Controls.Add(Me.Guna2HtmlLabel6)
        Me.Guna2CustomGradientPanel1.FillColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.FillColor3 = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.FillColor4 = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Guna2CustomGradientPanel1.Location = New System.Drawing.Point(0, 0)
        Me.Guna2CustomGradientPanel1.Name = "Guna2CustomGradientPanel1"
        Me.Guna2CustomGradientPanel1.Size = New System.Drawing.Size(1659, 107)
        Me.Guna2CustomGradientPanel1.TabIndex = 54
        '
        'Guna2HtmlLabel6
        '
        Me.Guna2HtmlLabel6.AutoSize = False
        Me.Guna2HtmlLabel6.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel6.Font = New System.Drawing.Font("Mongolian Baiti", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.Guna2HtmlLabel6.Location = New System.Drawing.Point(0, -1)
        Me.Guna2HtmlLabel6.Name = "Guna2HtmlLabel6"
        Me.Guna2HtmlLabel6.Size = New System.Drawing.Size(1659, 107)
        Me.Guna2HtmlLabel6.TabIndex = 47
        Me.Guna2HtmlLabel6.Text = "Reports and Analytics"
        Me.Guna2HtmlLabel6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("Mongolian Baiti", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(945, 349)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(331, 31)
        Me.Label1.TabIndex = 55
        Me.Label1.Text = "Total IN:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("Mongolian Baiti", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(1299, 349)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(337, 31)
        Me.Label2.TabIndex = 56
        Me.Label2.Text = "Total OUT:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("Mongolian Baiti", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(945, 442)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(691, 31)
        Me.Label3.TabIndex = 57
        Me.Label3.Text = "Current Stock:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'AdminDBRepandAnalytics
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1659, 1029)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Guna2CustomGradientPanel1)
        Me.Controls.Add(Me.ChartTransactionTrends)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ChartStockLevels)
        Me.Controls.Add(Me.LBLTotalOut)
        Me.Controls.Add(Me.TebReports)
        Me.Controls.Add(Me.GrpFilters)
        Me.Controls.Add(Me.LBLCurrentStock)
        Me.Controls.Add(Me.LBLTotalIn)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "AdminDBRepandAnalytics"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AdminDBRepandAnalytics"
        Me.GrpFilters.ResumeLayout(False)
        Me.GrpFilters.PerformLayout()
        Me.TebReports.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.DGVItemManagement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.DGVStockTrackTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChartStockLevels, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChartTransactionTrends, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Guna2CustomGradientPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GrpFilters As Guna.UI2.WinForms.Guna2GroupBox
    Friend WithEvents DtpFrom As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents Guna2HtmlLabel2 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents DtpTo As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents Guna2HtmlLabel1 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel3 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents RBOut As Guna.UI2.WinForms.Guna2RadioButton
    Friend WithEvents BRIn As Guna.UI2.WinForms.Guna2RadioButton
    Friend WithEvents BtnGenerateReport As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents TebReports As Guna.UI2.WinForms.Guna2TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Guna2BorderlessForm1 As Guna.UI2.WinForms.Guna2BorderlessForm
    Friend WithEvents ChartStockLevels As DataVisualization.Charting.Chart
    Friend WithEvents ChartTransactionTrends As DataVisualization.Charting.Chart
    Friend WithEvents RBAll As Guna.UI2.WinForms.Guna2RadioButton
    Friend WithEvents Guna2HtmlLabel5 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents CmbSupplier As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents LBLCurrentStock As Label
    Friend WithEvents LBLTotalOut As Label
    Friend WithEvents LBLTotalIn As Label
    Friend WithEvents btnBack As Guna.UI2.WinForms.Guna2CirclePictureBox
    Friend WithEvents Guna2CustomGradientPanel1 As Guna.UI2.WinForms.Guna2CustomGradientPanel
    Friend WithEvents Guna2HtmlLabel6 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents DGVItemManagement As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents DGVStockTrackTransaction As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
End Class
