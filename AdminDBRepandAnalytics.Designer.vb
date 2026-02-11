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
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdminDBRepandAnalytics))
        Me.GrpFilters = New Guna.UI2.WinForms.Guna2GroupBox()
        Me.DtpFrom = New Guna.UI2.WinForms.Guna2DateTimePicker()
        Me.Guna2HtmlLabel1 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.DtpTo = New Guna.UI2.WinForms.Guna2DateTimePicker()
        Me.Guna2HtmlLabel2 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.RBOut = New Guna.UI2.WinForms.Guna2RadioButton()
        Me.BRIn = New Guna.UI2.WinForms.Guna2RadioButton()
        Me.Guna2HtmlLabel3 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2HtmlLabel4 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.CmbCategory = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.BtnGenerateReport = New Guna.UI2.WinForms.Guna2Button()
        Me.TebReports = New Guna.UI2.WinForms.Guna2TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Guna2BorderlessForm1 = New Guna.UI2.WinForms.Guna2BorderlessForm(Me.components)
        Me.DGVItemManagement = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.DGVStockTrackTransaction = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.ChartStockLevels = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.ChartSupplierContributions = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.ChartTransactionTrends = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.RBAll = New Guna.UI2.WinForms.Guna2RadioButton()
        Me.Guna2HtmlLabel5 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.CmbSupplier = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.LBLTotalIn = New System.Windows.Forms.Label()
        Me.LBLTotalOut = New System.Windows.Forms.Label()
        Me.LBLCurrentStock = New System.Windows.Forms.Label()
        Me.btnBack = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.GrpFilters.SuspendLayout()
        Me.TebReports.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.DGVItemManagement, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVStockTrackTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChartStockLevels, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChartSupplierContributions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChartTransactionTrends, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GrpFilters
        '
        Me.GrpFilters.Controls.Add(Me.Guna2HtmlLabel5)
        Me.GrpFilters.Controls.Add(Me.CmbSupplier)
        Me.GrpFilters.Controls.Add(Me.RBAll)
        Me.GrpFilters.Controls.Add(Me.BtnGenerateReport)
        Me.GrpFilters.Controls.Add(Me.Guna2HtmlLabel4)
        Me.GrpFilters.Controls.Add(Me.CmbCategory)
        Me.GrpFilters.Controls.Add(Me.Guna2HtmlLabel3)
        Me.GrpFilters.Controls.Add(Me.RBOut)
        Me.GrpFilters.Controls.Add(Me.BRIn)
        Me.GrpFilters.Controls.Add(Me.Guna2HtmlLabel2)
        Me.GrpFilters.Controls.Add(Me.DtpTo)
        Me.GrpFilters.Controls.Add(Me.Guna2HtmlLabel1)
        Me.GrpFilters.Controls.Add(Me.DtpFrom)
        Me.GrpFilters.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.GrpFilters.ForeColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.GrpFilters.Location = New System.Drawing.Point(54, 57)
        Me.GrpFilters.Name = "GrpFilters"
        Me.GrpFilters.Size = New System.Drawing.Size(1548, 384)
        Me.GrpFilters.TabIndex = 0
        Me.GrpFilters.Text = "Filter by:"
        '
        'DtpFrom
        '
        Me.DtpFrom.Checked = True
        Me.DtpFrom.FillColor = System.Drawing.Color.White
        Me.DtpFrom.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.DtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.DtpFrom.Location = New System.Drawing.Point(191, 60)
        Me.DtpFrom.Margin = New System.Windows.Forms.Padding(4)
        Me.DtpFrom.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.DtpFrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpFrom.Name = "DtpFrom"
        Me.DtpFrom.Size = New System.Drawing.Size(388, 46)
        Me.DtpFrom.TabIndex = 5
        Me.DtpFrom.Value = New Date(2025, 12, 18, 11, 1, 1, 395)
        '
        'Guna2HtmlLabel1
        '
        Me.Guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel1.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel1.Location = New System.Drawing.Point(27, 78)
        Me.Guna2HtmlLabel1.Name = "Guna2HtmlLabel1"
        Me.Guna2HtmlLabel1.Size = New System.Drawing.Size(53, 28)
        Me.Guna2HtmlLabel1.TabIndex = 38
        Me.Guna2HtmlLabel1.Text = "From:"
        '
        'DtpTo
        '
        Me.DtpTo.Checked = True
        Me.DtpTo.FillColor = System.Drawing.Color.White
        Me.DtpTo.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.DtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.DtpTo.Location = New System.Drawing.Point(191, 128)
        Me.DtpTo.Margin = New System.Windows.Forms.Padding(4)
        Me.DtpTo.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.DtpTo.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpTo.Name = "DtpTo"
        Me.DtpTo.Size = New System.Drawing.Size(388, 46)
        Me.DtpTo.TabIndex = 39
        Me.DtpTo.Value = New Date(2025, 12, 18, 11, 1, 1, 395)
        '
        'Guna2HtmlLabel2
        '
        Me.Guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel2.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel2.Location = New System.Drawing.Point(27, 146)
        Me.Guna2HtmlLabel2.Name = "Guna2HtmlLabel2"
        Me.Guna2HtmlLabel2.Size = New System.Drawing.Size(31, 28)
        Me.Guna2HtmlLabel2.TabIndex = 40
        Me.Guna2HtmlLabel2.Text = "To:"
        '
        'RBOut
        '
        Me.RBOut.AutoSize = True
        Me.RBOut.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RBOut.CheckedState.BorderThickness = 0
        Me.RBOut.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RBOut.CheckedState.InnerColor = System.Drawing.Color.White
        Me.RBOut.CheckedState.InnerOffset = -4
        Me.RBOut.Location = New System.Drawing.Point(352, 208)
        Me.RBOut.Name = "RBOut"
        Me.RBOut.Size = New System.Drawing.Size(49, 19)
        Me.RBOut.TabIndex = 42
        Me.RBOut.Text = "OUT"
        Me.RBOut.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.RBOut.UncheckedState.BorderThickness = 2
        Me.RBOut.UncheckedState.FillColor = System.Drawing.Color.Transparent
        Me.RBOut.UncheckedState.InnerColor = System.Drawing.Color.Transparent
        '
        'BRIn
        '
        Me.BRIn.AutoSize = True
        Me.BRIn.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BRIn.CheckedState.BorderThickness = 0
        Me.BRIn.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BRIn.CheckedState.InnerColor = System.Drawing.Color.White
        Me.BRIn.CheckedState.InnerOffset = -4
        Me.BRIn.Location = New System.Drawing.Point(191, 208)
        Me.BRIn.Name = "BRIn"
        Me.BRIn.Size = New System.Drawing.Size(37, 19)
        Me.BRIn.TabIndex = 41
        Me.BRIn.Text = "IN"
        Me.BRIn.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.BRIn.UncheckedState.BorderThickness = 2
        Me.BRIn.UncheckedState.FillColor = System.Drawing.Color.Transparent
        Me.BRIn.UncheckedState.InnerColor = System.Drawing.Color.Transparent
        '
        'Guna2HtmlLabel3
        '
        Me.Guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel3.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel3.Location = New System.Drawing.Point(27, 199)
        Me.Guna2HtmlLabel3.Name = "Guna2HtmlLabel3"
        Me.Guna2HtmlLabel3.Size = New System.Drawing.Size(147, 28)
        Me.Guna2HtmlLabel3.TabIndex = 43
        Me.Guna2HtmlLabel3.Text = "Transaction type:"
        '
        'Guna2HtmlLabel4
        '
        Me.Guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel4.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel4.Location = New System.Drawing.Point(27, 266)
        Me.Guna2HtmlLabel4.Name = "Guna2HtmlLabel4"
        Me.Guna2HtmlLabel4.Size = New System.Drawing.Size(82, 28)
        Me.Guna2HtmlLabel4.TabIndex = 45
        Me.Guna2HtmlLabel4.Text = "Category:"
        '
        'CmbCategory
        '
        Me.CmbCategory.BackColor = System.Drawing.Color.Transparent
        Me.CmbCategory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbCategory.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbCategory.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbCategory.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.CmbCategory.ForeColor = System.Drawing.Color.Gray
        Me.CmbCategory.ItemHeight = 30
        Me.CmbCategory.Items.AddRange(New Object() {"Dental Supplies (gloves, masks, syringes)", "Medicines (pain relievers, antibiotics)", "Equipment (handpieces, sterilizers)", "Consumables (cotton rolls, gauze, alcohol)", "Office Supplies (paper, pens, printer ink)"})
        Me.CmbCategory.Location = New System.Drawing.Point(191, 258)
        Me.CmbCategory.Name = "CmbCategory"
        Me.CmbCategory.Size = New System.Drawing.Size(388, 36)
        Me.CmbCategory.TabIndex = 44
        '
        'BtnGenerateReport
        '
        Me.BtnGenerateReport.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BtnGenerateReport.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BtnGenerateReport.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BtnGenerateReport.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BtnGenerateReport.FillColor = System.Drawing.Color.White
        Me.BtnGenerateReport.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.BtnGenerateReport.ForeColor = System.Drawing.Color.Black
        Me.BtnGenerateReport.Location = New System.Drawing.Point(1290, 275)
        Me.BtnGenerateReport.Name = "BtnGenerateReport"
        Me.BtnGenerateReport.Size = New System.Drawing.Size(158, 45)
        Me.BtnGenerateReport.TabIndex = 46
        Me.BtnGenerateReport.Text = "Generate Report"
        '
        'TebReports
        '
        Me.TebReports.Controls.Add(Me.TabPage1)
        Me.TebReports.Controls.Add(Me.TabPage2)
        Me.TebReports.ItemSize = New System.Drawing.Size(180, 40)
        Me.TebReports.Location = New System.Drawing.Point(54, 459)
        Me.TebReports.Name = "TebReports"
        Me.TebReports.SelectedIndex = 0
        Me.TebReports.Size = New System.Drawing.Size(998, 558)
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
        Me.TebReports.TabMenuBackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(57, Byte), Integer))
        Me.TebReports.TabMenuOrientation = Guna.UI2.WinForms.TabMenuOrientation.HorizontalTop
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.DGVItemManagement)
        Me.TabPage1.Location = New System.Drawing.Point(4, 44)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(990, 510)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Item Management"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.DGVStockTrackTransaction)
        Me.TabPage2.Location = New System.Drawing.Point(4, 44)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(990, 510)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Stock Tracking/Transaction"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Guna2BorderlessForm1
        '
        Me.Guna2BorderlessForm1.ContainerControl = Me
        Me.Guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6R
        Me.Guna2BorderlessForm1.TransparentWhileDrag = True
        '
        'DGVItemManagement
        '
        Me.DGVItemManagement.AllowUserToAddRows = False
        Me.DGVItemManagement.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.DGVItemManagement.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGVItemManagement.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVItemManagement.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGVItemManagement.ColumnHeadersHeight = 25
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVItemManagement.DefaultCellStyle = DataGridViewCellStyle3
        Me.DGVItemManagement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVItemManagement.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVItemManagement.Location = New System.Drawing.Point(3, 3)
        Me.DGVItemManagement.Margin = New System.Windows.Forms.Padding(4)
        Me.DGVItemManagement.Name = "DGVItemManagement"
        Me.DGVItemManagement.ReadOnly = True
        Me.DGVItemManagement.RowHeadersVisible = False
        Me.DGVItemManagement.RowHeadersWidth = 51
        Me.DGVItemManagement.Size = New System.Drawing.Size(984, 504)
        Me.DGVItemManagement.TabIndex = 48
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
        Me.DGVItemManagement.ThemeStyle.HeaderStyle.Height = 25
        Me.DGVItemManagement.ThemeStyle.ReadOnly = True
        Me.DGVItemManagement.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVItemManagement.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DGVItemManagement.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVItemManagement.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.DGVItemManagement.ThemeStyle.RowsStyle.Height = 22
        Me.DGVItemManagement.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVItemManagement.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        '
        'DGVStockTrackTransaction
        '
        Me.DGVStockTrackTransaction.AllowUserToAddRows = False
        Me.DGVStockTrackTransaction.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.White
        Me.DGVStockTrackTransaction.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.DGVStockTrackTransaction.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVStockTrackTransaction.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.DGVStockTrackTransaction.ColumnHeadersHeight = 25
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVStockTrackTransaction.DefaultCellStyle = DataGridViewCellStyle6
        Me.DGVStockTrackTransaction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVStockTrackTransaction.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVStockTrackTransaction.Location = New System.Drawing.Point(3, 3)
        Me.DGVStockTrackTransaction.Margin = New System.Windows.Forms.Padding(4)
        Me.DGVStockTrackTransaction.Name = "DGVStockTrackTransaction"
        Me.DGVStockTrackTransaction.ReadOnly = True
        Me.DGVStockTrackTransaction.RowHeadersVisible = False
        Me.DGVStockTrackTransaction.RowHeadersWidth = 51
        Me.DGVStockTrackTransaction.Size = New System.Drawing.Size(984, 504)
        Me.DGVStockTrackTransaction.TabIndex = 49
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
        Me.DGVStockTrackTransaction.ThemeStyle.HeaderStyle.Height = 25
        Me.DGVStockTrackTransaction.ThemeStyle.ReadOnly = True
        Me.DGVStockTrackTransaction.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVStockTrackTransaction.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DGVStockTrackTransaction.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVStockTrackTransaction.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.DGVStockTrackTransaction.ThemeStyle.RowsStyle.Height = 22
        Me.DGVStockTrackTransaction.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVStockTrackTransaction.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        '
        'ChartStockLevels
        '
        ChartArea3.Name = "ChartArea1"
        Me.ChartStockLevels.ChartAreas.Add(ChartArea3)
        Legend3.Name = "Legend1"
        Me.ChartStockLevels.Legends.Add(Legend3)
        Me.ChartStockLevels.Location = New System.Drawing.Point(1080, 459)
        Me.ChartStockLevels.Name = "ChartStockLevels"
        Series3.ChartArea = "ChartArea1"
        Series3.Legend = "Legend1"
        Series3.Name = "Series1"
        Me.ChartStockLevels.Series.Add(Series3)
        Me.ChartStockLevels.Size = New System.Drawing.Size(245, 175)
        Me.ChartStockLevels.TabIndex = 49
        Me.ChartStockLevels.Text = "Chart1"
        '
        'ChartSupplierContributions
        '
        ChartArea2.Name = "ChartArea1"
        Me.ChartSupplierContributions.ChartAreas.Add(ChartArea2)
        Legend2.Name = "Legend1"
        Me.ChartSupplierContributions.Legends.Add(Legend2)
        Me.ChartSupplierContributions.Location = New System.Drawing.Point(1357, 459)
        Me.ChartSupplierContributions.Name = "ChartSupplierContributions"
        Series2.ChartArea = "ChartArea1"
        Series2.Legend = "Legend1"
        Series2.Name = "Series1"
        Me.ChartSupplierContributions.Series.Add(Series2)
        Me.ChartSupplierContributions.Size = New System.Drawing.Size(245, 175)
        Me.ChartSupplierContributions.TabIndex = 50
        Me.ChartSupplierContributions.Text = "Chart2"
        '
        'ChartTransactionTrends
        '
        ChartArea1.Name = "ChartArea1"
        Me.ChartTransactionTrends.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.ChartTransactionTrends.Legends.Add(Legend1)
        Me.ChartTransactionTrends.Location = New System.Drawing.Point(1080, 656)
        Me.ChartTransactionTrends.Name = "ChartTransactionTrends"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.ChartTransactionTrends.Series.Add(Series1)
        Me.ChartTransactionTrends.Size = New System.Drawing.Size(522, 160)
        Me.ChartTransactionTrends.TabIndex = 51
        Me.ChartTransactionTrends.Text = "Chart3"
        '
        'RBAll
        '
        Me.RBAll.AutoSize = True
        Me.RBAll.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RBAll.CheckedState.BorderThickness = 0
        Me.RBAll.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RBAll.CheckedState.InnerColor = System.Drawing.Color.White
        Me.RBAll.CheckedState.InnerOffset = -4
        Me.RBAll.Location = New System.Drawing.Point(530, 208)
        Me.RBAll.Name = "RBAll"
        Me.RBAll.Size = New System.Drawing.Size(45, 19)
        Me.RBAll.TabIndex = 47
        Me.RBAll.Text = "ALL"
        Me.RBAll.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.RBAll.UncheckedState.BorderThickness = 2
        Me.RBAll.UncheckedState.FillColor = System.Drawing.Color.Transparent
        Me.RBAll.UncheckedState.InnerColor = System.Drawing.Color.Transparent
        '
        'Guna2HtmlLabel5
        '
        Me.Guna2HtmlLabel5.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel5.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel5.Location = New System.Drawing.Point(27, 331)
        Me.Guna2HtmlLabel5.Name = "Guna2HtmlLabel5"
        Me.Guna2HtmlLabel5.Size = New System.Drawing.Size(77, 28)
        Me.Guna2HtmlLabel5.TabIndex = 49
        Me.Guna2HtmlLabel5.Text = "Supplier:"
        '
        'CmbSupplier
        '
        Me.CmbSupplier.BackColor = System.Drawing.Color.Transparent
        Me.CmbSupplier.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSupplier.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbSupplier.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbSupplier.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.CmbSupplier.ForeColor = System.Drawing.Color.Gray
        Me.CmbSupplier.ItemHeight = 30
        Me.CmbSupplier.Items.AddRange(New Object() {"Dental Supplies (gloves, masks, syringes)", "Medicines (pain relievers, antibiotics)", "Equipment (handpieces, sterilizers)", "Consumables (cotton rolls, gauze, alcohol)", "Office Supplies (paper, pens, printer ink)"})
        Me.CmbSupplier.Location = New System.Drawing.Point(191, 323)
        Me.CmbSupplier.Name = "CmbSupplier"
        Me.CmbSupplier.Size = New System.Drawing.Size(388, 36)
        Me.CmbSupplier.TabIndex = 48
        '
        'LBLTotalIn
        '
        Me.LBLTotalIn.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTotalIn.Location = New System.Drawing.Point(1074, 839)
        Me.LBLTotalIn.Name = "LBLTotalIn"
        Me.LBLTotalIn.Size = New System.Drawing.Size(251, 55)
        Me.LBLTotalIn.TabIndex = 50
        Me.LBLTotalIn.Text = "Label1"
        Me.LBLTotalIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBLTotalOut
        '
        Me.LBLTotalOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTotalOut.Location = New System.Drawing.Point(1351, 839)
        Me.LBLTotalOut.Name = "LBLTotalOut"
        Me.LBLTotalOut.Size = New System.Drawing.Size(251, 55)
        Me.LBLTotalOut.TabIndex = 52
        Me.LBLTotalOut.Text = "Label2"
        Me.LBLTotalOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBLCurrentStock
        '
        Me.LBLCurrentStock.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLCurrentStock.Location = New System.Drawing.Point(1088, 914)
        Me.LBLCurrentStock.Name = "LBLCurrentStock"
        Me.LBLCurrentStock.Size = New System.Drawing.Size(514, 96)
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
        Me.btnBack.Location = New System.Drawing.Point(1571, 22)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.btnBack.Size = New System.Drawing.Size(60, 58)
        Me.btnBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnBack.TabIndex = 50
        Me.btnBack.TabStop = False
        '
        'AdminDBRepandAnalytics
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1659, 1029)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.LBLCurrentStock)
        Me.Controls.Add(Me.LBLTotalOut)
        Me.Controls.Add(Me.LBLTotalIn)
        Me.Controls.Add(Me.ChartTransactionTrends)
        Me.Controls.Add(Me.ChartSupplierContributions)
        Me.Controls.Add(Me.ChartStockLevels)
        Me.Controls.Add(Me.TebReports)
        Me.Controls.Add(Me.GrpFilters)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "AdminDBRepandAnalytics"
        Me.Text = "AdminDBRepandAnalytics"
        Me.GrpFilters.ResumeLayout(False)
        Me.GrpFilters.PerformLayout()
        Me.TebReports.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.DGVItemManagement, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVStockTrackTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChartStockLevels, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChartSupplierContributions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChartTransactionTrends, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Guna2HtmlLabel4 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents CmbCategory As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents BtnGenerateReport As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents TebReports As Guna.UI2.WinForms.Guna2TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Guna2BorderlessForm1 As Guna.UI2.WinForms.Guna2BorderlessForm
    Friend WithEvents DGVItemManagement As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents DGVStockTrackTransaction As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents ChartStockLevels As DataVisualization.Charting.Chart
    Friend WithEvents ChartTransactionTrends As DataVisualization.Charting.Chart
    Friend WithEvents ChartSupplierContributions As DataVisualization.Charting.Chart
    Friend WithEvents RBAll As Guna.UI2.WinForms.Guna2RadioButton
    Friend WithEvents Guna2HtmlLabel5 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents CmbSupplier As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents LBLCurrentStock As Label
    Friend WithEvents LBLTotalOut As Label
    Friend WithEvents LBLTotalIn As Label
    Friend WithEvents btnBack As Guna.UI2.WinForms.Guna2CirclePictureBox
End Class
