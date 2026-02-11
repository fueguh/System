<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdminDBItemManagement
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdminDBItemManagement))
        Me.TextBoxItemName = New Guna.UI2.WinForms.Guna2TextBox()
        Me.BtnAdd = New Guna.UI2.WinForms.Guna2Button()
        Me.DGVInventory = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.BtnUpdate = New Guna.UI2.WinForms.Guna2Button()
        Me.BtnDelete = New Guna.UI2.WinForms.Guna2Button()
        Me.ComboBoxCategory = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.Guna2HtmlLabel2 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.DateTimePickerExpiry = New Guna.UI2.WinForms.Guna2DateTimePicker()
        Me.TextBoxPrice = New Guna.UI2.WinForms.Guna2TextBox()
        Me.Guna2HtmlLabel1 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.ComboBoxUnit = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.chkHasExpiry = New Guna.UI2.WinForms.Guna2CheckBox()
        Me.TextBoxSearch = New Guna.UI2.WinForms.Guna2TextBox()
        Me.ButtonSearch = New Guna.UI2.WinForms.Guna2Button()
        Me.btnBack = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.Guna2HtmlLabel3 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.ComboBoxSupplier = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.NumericUpDownQuantity = New Guna.UI2.WinForms.Guna2NumericUpDown()
        Me.Guna2HtmlLabel4 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        CType(Me.DGVInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxItemName
        '
        Me.TextBoxItemName.BorderRadius = 10
        Me.TextBoxItemName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextBoxItemName.DefaultText = ""
        Me.TextBoxItemName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TextBoxItemName.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TextBoxItemName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TextBoxItemName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TextBoxItemName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextBoxItemName.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.TextBoxItemName.ForeColor = System.Drawing.Color.Black
        Me.TextBoxItemName.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextBoxItemName.Location = New System.Drawing.Point(89, 70)
        Me.TextBoxItemName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBoxItemName.Name = "TextBoxItemName"
        Me.TextBoxItemName.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.TextBoxItemName.PlaceholderText = "Item Name"
        Me.TextBoxItemName.SelectedText = ""
        Me.TextBoxItemName.Size = New System.Drawing.Size(257, 50)
        Me.TextBoxItemName.TabIndex = 1
        '
        'BtnAdd
        '
        Me.BtnAdd.BorderRadius = 10
        Me.BtnAdd.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BtnAdd.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BtnAdd.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BtnAdd.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BtnAdd.FillColor = System.Drawing.Color.White
        Me.BtnAdd.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAdd.ForeColor = System.Drawing.Color.Black
        Me.BtnAdd.Location = New System.Drawing.Point(106, 272)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(158, 45)
        Me.BtnAdd.TabIndex = 6
        Me.BtnAdd.Text = "Add"
        '
        'DGVInventory
        '
        Me.DGVInventory.AllowUserToAddRows = False
        Me.DGVInventory.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.DGVInventory.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVInventory.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGVInventory.ColumnHeadersHeight = 25
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVInventory.DefaultCellStyle = DataGridViewCellStyle3
        Me.DGVInventory.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVInventory.Location = New System.Drawing.Point(30, 427)
        Me.DGVInventory.Name = "DGVInventory"
        Me.DGVInventory.ReadOnly = True
        Me.DGVInventory.RowHeadersVisible = False
        Me.DGVInventory.RowHeadersWidth = 51
        Me.DGVInventory.Size = New System.Drawing.Size(1492, 487)
        Me.DGVInventory.TabIndex = 9
        Me.DGVInventory.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVInventory.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.DGVInventory.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.DGVInventory.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.DGVInventory.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.DGVInventory.ThemeStyle.BackColor = System.Drawing.Color.White
        Me.DGVInventory.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVInventory.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVInventory.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DGVInventory.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVInventory.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White
        Me.DGVInventory.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DGVInventory.ThemeStyle.HeaderStyle.Height = 25
        Me.DGVInventory.ThemeStyle.ReadOnly = True
        Me.DGVInventory.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.DGVInventory.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DGVInventory.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVInventory.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.DGVInventory.ThemeStyle.RowsStyle.Height = 22
        Me.DGVInventory.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVInventory.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        '
        'BtnUpdate
        '
        Me.BtnUpdate.BorderRadius = 10
        Me.BtnUpdate.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BtnUpdate.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BtnUpdate.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BtnUpdate.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BtnUpdate.FillColor = System.Drawing.Color.White
        Me.BtnUpdate.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUpdate.ForeColor = System.Drawing.Color.Black
        Me.BtnUpdate.Location = New System.Drawing.Point(294, 272)
        Me.BtnUpdate.Name = "BtnUpdate"
        Me.BtnUpdate.Size = New System.Drawing.Size(158, 45)
        Me.BtnUpdate.TabIndex = 10
        Me.BtnUpdate.Text = "Update"
        '
        'BtnDelete
        '
        Me.BtnDelete.BorderRadius = 10
        Me.BtnDelete.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BtnDelete.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BtnDelete.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BtnDelete.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BtnDelete.FillColor = System.Drawing.Color.White
        Me.BtnDelete.Font = New System.Drawing.Font("Mongolian Baiti", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDelete.ForeColor = System.Drawing.Color.Black
        Me.BtnDelete.Location = New System.Drawing.Point(491, 272)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(158, 45)
        Me.BtnDelete.TabIndex = 11
        Me.BtnDelete.Text = "Delete"
        '
        'ComboBoxCategory
        '
        Me.ComboBoxCategory.BackColor = System.Drawing.Color.Transparent
        Me.ComboBoxCategory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ComboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxCategory.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ComboBoxCategory.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ComboBoxCategory.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.ComboBoxCategory.ForeColor = System.Drawing.Color.Gray
        Me.ComboBoxCategory.ItemHeight = 30
        Me.ComboBoxCategory.Items.AddRange(New Object() {"Dental Supplies (gloves, masks, syringes)", "Medicines (pain relievers, antibiotics)", "Equipment (handpieces, sterilizers)", "Consumables (cotton rolls, gauze, alcohol)", "Office Supplies (paper, pens, printer ink)"})
        Me.ComboBoxCategory.Location = New System.Drawing.Point(89, 171)
        Me.ComboBoxCategory.Name = "ComboBoxCategory"
        Me.ComboBoxCategory.Size = New System.Drawing.Size(257, 36)
        Me.ComboBoxCategory.TabIndex = 25
        '
        'Guna2HtmlLabel2
        '
        Me.Guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel2.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel2.Location = New System.Drawing.Point(89, 141)
        Me.Guna2HtmlLabel2.Name = "Guna2HtmlLabel2"
        Me.Guna2HtmlLabel2.Size = New System.Drawing.Size(77, 28)
        Me.Guna2HtmlLabel2.TabIndex = 29
        Me.Guna2HtmlLabel2.Text = "Category"
        '
        'DateTimePickerExpiry
        '
        Me.DateTimePickerExpiry.Checked = True
        Me.DateTimePickerExpiry.FillColor = System.Drawing.Color.White
        Me.DateTimePickerExpiry.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.DateTimePickerExpiry.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.DateTimePickerExpiry.Location = New System.Drawing.Point(671, 166)
        Me.DateTimePickerExpiry.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePickerExpiry.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.DateTimePickerExpiry.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DateTimePickerExpiry.Name = "DateTimePickerExpiry"
        Me.DateTimePickerExpiry.Size = New System.Drawing.Size(247, 46)
        Me.DateTimePickerExpiry.TabIndex = 32
        Me.DateTimePickerExpiry.Value = New Date(2025, 12, 18, 11, 1, 1, 395)
        '
        'TextBoxPrice
        '
        Me.TextBoxPrice.BorderRadius = 10
        Me.TextBoxPrice.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextBoxPrice.DefaultText = ""
        Me.TextBoxPrice.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TextBoxPrice.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TextBoxPrice.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TextBoxPrice.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TextBoxPrice.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextBoxPrice.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.TextBoxPrice.ForeColor = System.Drawing.Color.Black
        Me.TextBoxPrice.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextBoxPrice.Location = New System.Drawing.Point(385, 162)
        Me.TextBoxPrice.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBoxPrice.Name = "TextBoxPrice"
        Me.TextBoxPrice.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.TextBoxPrice.PlaceholderText = "Price"
        Me.TextBoxPrice.SelectedText = ""
        Me.TextBoxPrice.Size = New System.Drawing.Size(257, 50)
        Me.TextBoxPrice.TabIndex = 34
        '
        'Guna2HtmlLabel1
        '
        Me.Guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel1.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel1.Location = New System.Drawing.Point(671, 54)
        Me.Guna2HtmlLabel1.Name = "Guna2HtmlLabel1"
        Me.Guna2HtmlLabel1.Size = New System.Drawing.Size(40, 28)
        Me.Guna2HtmlLabel1.TabIndex = 36
        Me.Guna2HtmlLabel1.Text = "Unit"
        '
        'ComboBoxUnit
        '
        Me.ComboBoxUnit.BackColor = System.Drawing.Color.Transparent
        Me.ComboBoxUnit.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ComboBoxUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxUnit.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ComboBoxUnit.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ComboBoxUnit.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.ComboBoxUnit.ForeColor = System.Drawing.Color.Gray
        Me.ComboBoxUnit.ItemHeight = 30
        Me.ComboBoxUnit.Items.AddRange(New Object() {"Piece", "Box", "Pack", "Bottle", "Tube", "Set", "Dozen"})
        Me.ComboBoxUnit.Location = New System.Drawing.Point(671, 84)
        Me.ComboBoxUnit.Name = "ComboBoxUnit"
        Me.ComboBoxUnit.Size = New System.Drawing.Size(257, 36)
        Me.ComboBoxUnit.TabIndex = 35
        '
        'chkHasExpiry
        '
        Me.chkHasExpiry.AutoSize = True
        Me.chkHasExpiry.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkHasExpiry.CheckedState.BorderRadius = 0
        Me.chkHasExpiry.CheckedState.BorderThickness = 0
        Me.chkHasExpiry.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkHasExpiry.Location = New System.Drawing.Point(949, 181)
        Me.chkHasExpiry.Name = "chkHasExpiry"
        Me.chkHasExpiry.Size = New System.Drawing.Size(113, 17)
        Me.chkHasExpiry.TabIndex = 37
        Me.chkHasExpiry.Text = "Guna2CheckBox1"
        Me.chkHasExpiry.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.chkHasExpiry.UncheckedState.BorderRadius = 0
        Me.chkHasExpiry.UncheckedState.BorderThickness = 0
        Me.chkHasExpiry.UncheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        '
        'TextBoxSearch
        '
        Me.TextBoxSearch.BorderRadius = 10
        Me.TextBoxSearch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextBoxSearch.DefaultText = ""
        Me.TextBoxSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TextBoxSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TextBoxSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TextBoxSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TextBoxSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextBoxSearch.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.TextBoxSearch.ForeColor = System.Drawing.Color.Black
        Me.TextBoxSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextBoxSearch.Location = New System.Drawing.Point(30, 357)
        Me.TextBoxSearch.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBoxSearch.Name = "TextBoxSearch"
        Me.TextBoxSearch.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.TextBoxSearch.PlaceholderText = "Search item.."
        Me.TextBoxSearch.SelectedText = ""
        Me.TextBoxSearch.Size = New System.Drawing.Size(706, 50)
        Me.TextBoxSearch.TabIndex = 38
        '
        'ButtonSearch
        '
        Me.ButtonSearch.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.ButtonSearch.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.ButtonSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.ButtonSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.ButtonSearch.FillColor = System.Drawing.Color.White
        Me.ButtonSearch.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.ButtonSearch.ForeColor = System.Drawing.Color.Black
        Me.ButtonSearch.Location = New System.Drawing.Point(752, 357)
        Me.ButtonSearch.Name = "ButtonSearch"
        Me.ButtonSearch.Size = New System.Drawing.Size(101, 50)
        Me.ButtonSearch.TabIndex = 39
        Me.ButtonSearch.Text = "Search"
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.FillColor = System.Drawing.Color.LightGray
        Me.btnBack.Image = CType(resources.GetObject("btnBack.Image"), System.Drawing.Image)
        Me.btnBack.ImageRotate = 0!
        Me.btnBack.Location = New System.Drawing.Point(1472, 12)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.btnBack.Size = New System.Drawing.Size(60, 58)
        Me.btnBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnBack.TabIndex = 40
        Me.btnBack.TabStop = False
        '
        'Guna2HtmlLabel3
        '
        Me.Guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel3.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel3.Location = New System.Drawing.Point(949, 54)
        Me.Guna2HtmlLabel3.Name = "Guna2HtmlLabel3"
        Me.Guna2HtmlLabel3.Size = New System.Drawing.Size(72, 28)
        Me.Guna2HtmlLabel3.TabIndex = 42
        Me.Guna2HtmlLabel3.Text = "Supplier"
        '
        'ComboBoxSupplier
        '
        Me.ComboBoxSupplier.BackColor = System.Drawing.Color.Transparent
        Me.ComboBoxSupplier.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ComboBoxSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxSupplier.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ComboBoxSupplier.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ComboBoxSupplier.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.ComboBoxSupplier.ForeColor = System.Drawing.Color.Gray
        Me.ComboBoxSupplier.ItemHeight = 30
        Me.ComboBoxSupplier.Items.AddRange(New Object() {"Piece", "Box", "Pack", "Bottle", "Tube", "Set", "Dozen"})
        Me.ComboBoxSupplier.Location = New System.Drawing.Point(949, 84)
        Me.ComboBoxSupplier.Name = "ComboBoxSupplier"
        Me.ComboBoxSupplier.Size = New System.Drawing.Size(257, 36)
        Me.ComboBoxSupplier.TabIndex = 41
        '
        'NumericUpDownQuantity
        '
        Me.NumericUpDownQuantity.BackColor = System.Drawing.Color.Transparent
        Me.NumericUpDownQuantity.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.NumericUpDownQuantity.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.NumericUpDownQuantity.Location = New System.Drawing.Point(385, 70)
        Me.NumericUpDownQuantity.Name = "NumericUpDownQuantity"
        Me.NumericUpDownQuantity.Size = New System.Drawing.Size(257, 50)
        Me.NumericUpDownQuantity.TabIndex = 43
        '
        'Guna2HtmlLabel4
        '
        Me.Guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel4.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel4.Location = New System.Drawing.Point(385, 42)
        Me.Guna2HtmlLabel4.Name = "Guna2HtmlLabel4"
        Me.Guna2HtmlLabel4.Size = New System.Drawing.Size(76, 28)
        Me.Guna2HtmlLabel4.TabIndex = 44
        Me.Guna2HtmlLabel4.Text = "Quantity"
        '
        'AdminDBItemManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(1544, 926)
        Me.Controls.Add(Me.Guna2HtmlLabel4)
        Me.Controls.Add(Me.NumericUpDownQuantity)
        Me.Controls.Add(Me.Guna2HtmlLabel3)
        Me.Controls.Add(Me.ComboBoxSupplier)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.ButtonSearch)
        Me.Controls.Add(Me.TextBoxSearch)
        Me.Controls.Add(Me.chkHasExpiry)
        Me.Controls.Add(Me.Guna2HtmlLabel1)
        Me.Controls.Add(Me.ComboBoxUnit)
        Me.Controls.Add(Me.TextBoxPrice)
        Me.Controls.Add(Me.DateTimePickerExpiry)
        Me.Controls.Add(Me.Guna2HtmlLabel2)
        Me.Controls.Add(Me.ComboBoxCategory)
        Me.Controls.Add(Me.BtnDelete)
        Me.Controls.Add(Me.BtnUpdate)
        Me.Controls.Add(Me.DGVInventory)
        Me.Controls.Add(Me.BtnAdd)
        Me.Controls.Add(Me.TextBoxItemName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "AdminDBItemManagement"
        Me.Text = "AdminDBItemManagement"
        CType(Me.DGVInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxItemName As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents BtnAdd As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents DGVInventory As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents BtnUpdate As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents BtnDelete As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents ComboBoxCategory As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents Guna2HtmlLabel2 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents DateTimePickerExpiry As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents TextBoxPrice As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Guna2HtmlLabel1 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents ComboBoxUnit As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents chkHasExpiry As Guna.UI2.WinForms.Guna2CheckBox
    Friend WithEvents TextBoxSearch As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents ButtonSearch As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnBack As Guna.UI2.WinForms.Guna2CirclePictureBox
    Friend WithEvents Guna2HtmlLabel3 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents ComboBoxSupplier As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents NumericUpDownQuantity As Guna.UI2.WinForms.Guna2NumericUpDown
    Friend WithEvents Guna2HtmlLabel4 As Guna.UI2.WinForms.Guna2HtmlLabel
End Class
