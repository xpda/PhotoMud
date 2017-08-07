<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPrint
#Region "Windows Form Designer generated code "
  <System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
    MyBase.New()
    'This call is required by the Windows Form Designer.
    InitializeComponent()
  End Sub
  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
    If Disposing Then
      If Not components Is Nothing Then
        components.Dispose()
      End If
    End If
    MyBase.Dispose(Disposing)
  End Sub
  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer
  Public ToolTip1 As ToolTip
  Public WithEvents chkMulti As CheckBox
  Public WithEvents txtBorder As TextBox
  Public WithEvents lbBorder As Label
  Public WithEvents lbRows As Label
  Public WithEvents lbCols As Label
  Public WithEvents Frame5 As GroupBox
  Public WithEvents cmdHelp As Button
  Public WithEvents cmdPreferences As Button
  Public WithEvents cmbPrinter As ComboBox
  Public WithEvents chkSaveSettings As CheckBox
  Public WithEvents optPortrait As RadioButton
  Public WithEvents optLandscape As RadioButton
  Public WithEvents Frame1 As GroupBox
  Public WithEvents cmdActualSize As Button
  Public WithEvents chkFitPage As CheckBox
  Public WithEvents txtWidth As TextBox
  Public WithEvents txtHeight As TextBox
  Public WithEvents lbUnits1 As Label
  Public WithEvents lbUnits0 As Label
  Public WithEvents lbWidth As Label
  Public WithEvents lbHeight As Label
  Public WithEvents Frame2 As GroupBox
  Public WithEvents cmbVertical As ComboBox
  Public WithEvents cmbHorizontal As ComboBox
  Public WithEvents Label1 As Label
  Public WithEvents Label2 As Label
  Public WithEvents Frame3 As GroupBox
  Public WithEvents optMillimeters As RadioButton
  Public WithEvents optInches As RadioButton
  Public WithEvents Frame4 As GroupBox
  Public WithEvents cmdPrint As Button
  Public WithEvents cmdCancel As Button
  Public WithEvents lbCount As Label
  Public WithEvents lbCopies As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrint))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.nmRows = New System.Windows.Forms.NumericUpDown()
    Me.nmCols = New System.Windows.Forms.NumericUpDown()
    Me.chkMulti = New System.Windows.Forms.CheckBox()
    Me.txtBorder = New System.Windows.Forms.TextBox()
    Me.cmdPreferences = New System.Windows.Forms.Button()
    Me.cmbPrinter = New System.Windows.Forms.ComboBox()
    Me.chkSaveSettings = New System.Windows.Forms.CheckBox()
    Me.chkOverlap = New System.Windows.Forms.CheckBox()
    Me.chkTile = New System.Windows.Forms.CheckBox()
    Me.cmdActualSize = New System.Windows.Forms.Button()
    Me.chkFitPage = New System.Windows.Forms.CheckBox()
    Me.txtWidth = New System.Windows.Forms.TextBox()
    Me.txtHeight = New System.Windows.Forms.TextBox()
    Me.Frame5 = New System.Windows.Forms.GroupBox()
    Me.lbBorder = New System.Windows.Forms.Label()
    Me.lbRows = New System.Windows.Forms.Label()
    Me.lbCols = New System.Windows.Forms.Label()
    Me.Frame1 = New System.Windows.Forms.GroupBox()
    Me.optPortrait = New System.Windows.Forms.RadioButton()
    Me.optLandscape = New System.Windows.Forms.RadioButton()
    Me.Frame2 = New System.Windows.Forms.GroupBox()
    Me.lbTile = New System.Windows.Forms.Label()
    Me.lbUnits1 = New System.Windows.Forms.Label()
    Me.lbUnits0 = New System.Windows.Forms.Label()
    Me.lbWidth = New System.Windows.Forms.Label()
    Me.lbHeight = New System.Windows.Forms.Label()
    Me.Frame3 = New System.Windows.Forms.GroupBox()
    Me.cmbVertical = New System.Windows.Forms.ComboBox()
    Me.cmbHorizontal = New System.Windows.Forms.ComboBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Frame4 = New System.Windows.Forms.GroupBox()
    Me.optMillimeters = New System.Windows.Forms.RadioButton()
    Me.optInches = New System.Windows.Forms.RadioButton()
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.lbCount = New System.Windows.Forms.Label()
    Me.lbCopies = New System.Windows.Forms.Label()
    Me.pnShadow = New System.Windows.Forms.Panel()
    Me.pnPaper = New System.Windows.Forms.Panel()
    Me.pView = New PhotoMud.pViewer()
    Me.pnPaperFrame = New System.Windows.Forms.Panel()
    Me.nmNcopies = New System.Windows.Forms.NumericUpDown()
    CType(Me.nmRows, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmCols, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Frame5.SuspendLayout()
    Me.Frame1.SuspendLayout()
    Me.Frame2.SuspendLayout()
    Me.Frame3.SuspendLayout()
    Me.Frame4.SuspendLayout()
    Me.pnPaper.SuspendLayout()
    CType(Me.pView, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPaperFrame.SuspendLayout()
    CType(Me.nmNcopies, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'cmdHelp
    '
    Me.cmdHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(577, 554)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 41)
    Me.cmdHelp.TabIndex = 28
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'nmRows
    '
    Me.nmRows.Location = New System.Drawing.Point(123, 93)
    Me.nmRows.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nmRows.Name = "nmRows"
    Me.nmRows.Size = New System.Drawing.Size(54, 25)
    Me.nmRows.TabIndex = 25
    Me.ToolTip1.SetToolTip(Me.nmRows, "Number of rows of photos per page")
    Me.nmRows.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'nmCols
    '
    Me.nmCols.Location = New System.Drawing.Point(123, 62)
    Me.nmCols.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nmCols.Name = "nmCols"
    Me.nmCols.Size = New System.Drawing.Size(54, 25)
    Me.nmCols.TabIndex = 23
    Me.ToolTip1.SetToolTip(Me.nmCols, "Number of columns of photos per page")
    Me.nmCols.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'chkMulti
    '
    Me.chkMulti.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkMulti.Location = New System.Drawing.Point(18, 21)
    Me.chkMulti.Name = "chkMulti"
    Me.chkMulti.Size = New System.Drawing.Size(214, 31)
    Me.chkMulti.TabIndex = 21
    Me.chkMulti.Text = "Multiple Photos per Page"
    Me.ToolTip1.SetToolTip(Me.chkMulti, "Print multiple photos per page")
    Me.chkMulti.UseVisualStyleBackColor = False
    '
    'txtBorder
    '
    Me.txtBorder.AcceptsReturn = True
    Me.txtBorder.BackColor = System.Drawing.SystemColors.Window
    Me.txtBorder.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txtBorder.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txtBorder.Location = New System.Drawing.Point(123, 124)
    Me.txtBorder.MaxLength = 0
    Me.txtBorder.Name = "txtBorder"
    Me.txtBorder.Size = New System.Drawing.Size(54, 25)
    Me.txtBorder.TabIndex = 27
    Me.txtBorder.Text = "0.125"
    Me.ToolTip1.SetToolTip(Me.txtBorder, "The border size of each photo")
    '
    'cmdPreferences
    '
    Me.cmdPreferences.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdPreferences.Location = New System.Drawing.Point(362, 15)
    Me.cmdPreferences.Name = "cmdPreferences"
    Me.cmdPreferences.Size = New System.Drawing.Size(121, 26)
    Me.cmdPreferences.TabIndex = 1
    Me.cmdPreferences.Text = "P&references..."
    Me.ToolTip1.SetToolTip(Me.cmdPreferences, "Printer preferences")
    Me.cmdPreferences.UseVisualStyleBackColor = False
    '
    'cmbPrinter
    '
    Me.cmbPrinter.BackColor = System.Drawing.SystemColors.Window
    Me.cmbPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cmbPrinter.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmbPrinter.Location = New System.Drawing.Point(16, 17)
    Me.cmbPrinter.Name = "cmbPrinter"
    Me.cmbPrinter.Size = New System.Drawing.Size(336, 25)
    Me.cmbPrinter.TabIndex = 0
    Me.ToolTip1.SetToolTip(Me.cmbPrinter, "Select the printer to be used.")
    '
    'chkSaveSettings
    '
    Me.chkSaveSettings.AutoSize = True
    Me.chkSaveSettings.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkSaveSettings.Location = New System.Drawing.Point(24, 520)
    Me.chkSaveSettings.Name = "chkSaveSettings"
    Me.chkSaveSettings.Size = New System.Drawing.Size(120, 21)
    Me.chkSaveSettings.TabIndex = 24
    Me.chkSaveSettings.Text = "Save Settings"
    Me.ToolTip1.SetToolTip(Me.chkSaveSettings, "Save the current settings as default print settings")
    Me.chkSaveSettings.UseVisualStyleBackColor = False
    '
    'chkOverlap
    '
    Me.chkOverlap.AutoSize = True
    Me.chkOverlap.Location = New System.Drawing.Point(18, 169)
    Me.chkOverlap.Name = "chkOverlap"
    Me.chkOverlap.Size = New System.Drawing.Size(144, 21)
    Me.chkOverlap.TabIndex = 19
    Me.chkOverlap.Text = "Overlap Tiles 1/8"""
    Me.ToolTip1.SetToolTip(Me.chkOverlap, "Check to overlap tiled pages to aid in attaching tile pages.")
    Me.chkOverlap.UseVisualStyleBackColor = True
    '
    'chkTile
    '
    Me.chkTile.AutoSize = True
    Me.chkTile.Location = New System.Drawing.Point(18, 142)
    Me.chkTile.Name = "chkTile"
    Me.chkTile.Size = New System.Drawing.Size(218, 21)
    Me.chkTile.TabIndex = 18
    Me.chkTile.Text = "Print on Multiple Pages (tiled)"
    Me.ToolTip1.SetToolTip(Me.chkTile, "You can print a single photo tiled on multiple pages " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "by checking this box and s" & _
        "pecifying a size larger than the page.")
    Me.chkTile.UseVisualStyleBackColor = True
    '
    'cmdActualSize
    '
    Me.cmdActualSize.Enabled = False
    Me.cmdActualSize.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdActualSize.Location = New System.Drawing.Point(70, 246)
    Me.cmdActualSize.Name = "cmdActualSize"
    Me.cmdActualSize.Size = New System.Drawing.Size(106, 27)
    Me.cmdActualSize.TabIndex = 20
    Me.cmdActualSize.Text = "Actual Size"
    Me.ToolTip1.SetToolTip(Me.cmdActualSize, "Print at actual size, based on the photo's DPI.")
    Me.cmdActualSize.UseVisualStyleBackColor = False
    '
    'chkFitPage
    '
    Me.chkFitPage.AutoSize = True
    Me.chkFitPage.Checked = True
    Me.chkFitPage.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkFitPage.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkFitPage.Location = New System.Drawing.Point(18, 31)
    Me.chkFitPage.Name = "chkFitPage"
    Me.chkFitPage.Size = New System.Drawing.Size(100, 21)
    Me.chkFitPage.TabIndex = 13
    Me.chkFitPage.Text = "&Fit to Page"
    Me.ToolTip1.SetToolTip(Me.chkFitPage, "Scale the photo so it fits the page.")
    Me.chkFitPage.UseVisualStyleBackColor = False
    '
    'txtWidth
    '
    Me.txtWidth.AcceptsReturn = True
    Me.txtWidth.BackColor = System.Drawing.SystemColors.Window
    Me.txtWidth.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txtWidth.Enabled = False
    Me.txtWidth.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txtWidth.Location = New System.Drawing.Point(75, 63)
    Me.txtWidth.MaxLength = 0
    Me.txtWidth.Name = "txtWidth"
    Me.txtWidth.Size = New System.Drawing.Size(61, 25)
    Me.txtWidth.TabIndex = 15
    Me.txtWidth.Text = "Text1"
    Me.ToolTip1.SetToolTip(Me.txtWidth, "Output size of the photo or photos printed.")
    '
    'txtHeight
    '
    Me.txtHeight.AcceptsReturn = True
    Me.txtHeight.BackColor = System.Drawing.SystemColors.Window
    Me.txtHeight.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txtHeight.Enabled = False
    Me.txtHeight.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txtHeight.Location = New System.Drawing.Point(75, 97)
    Me.txtHeight.MaxLength = 0
    Me.txtHeight.Name = "txtHeight"
    Me.txtHeight.Size = New System.Drawing.Size(61, 25)
    Me.txtHeight.TabIndex = 17
    Me.txtHeight.Text = "Text1"
    Me.ToolTip1.SetToolTip(Me.txtHeight, "Output size of the photo or photos printed.")
    '
    'Frame5
    '
    Me.Frame5.Controls.Add(Me.nmRows)
    Me.Frame5.Controls.Add(Me.nmCols)
    Me.Frame5.Controls.Add(Me.chkMulti)
    Me.Frame5.Controls.Add(Me.txtBorder)
    Me.Frame5.Controls.Add(Me.lbBorder)
    Me.Frame5.Controls.Add(Me.lbRows)
    Me.Frame5.Controls.Add(Me.lbCols)
    Me.Frame5.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Frame5.Location = New System.Drawing.Point(279, 422)
    Me.Frame5.Name = "Frame5"
    Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
    Me.Frame5.Size = New System.Drawing.Size(251, 171)
    Me.Frame5.TabIndex = 37
    Me.Frame5.TabStop = False
    '
    'lbBorder
    '
    Me.lbBorder.AutoSize = True
    Me.lbBorder.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbBorder.Location = New System.Drawing.Point(15, 128)
    Me.lbBorder.Name = "lbBorder"
    Me.lbBorder.Size = New System.Drawing.Size(89, 17)
    Me.lbBorder.TabIndex = 26
    Me.lbBorder.Text = "Border Si&ze:"
    '
    'lbRows
    '
    Me.lbRows.AutoSize = True
    Me.lbRows.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbRows.Location = New System.Drawing.Point(15, 96)
    Me.lbRows.Name = "lbRows"
    Me.lbRows.Size = New System.Drawing.Size(50, 17)
    Me.lbRows.TabIndex = 24
    Me.lbRows.Text = "&Rows:"
    '
    'lbCols
    '
    Me.lbCols.AutoSize = True
    Me.lbCols.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbCols.Location = New System.Drawing.Point(15, 65)
    Me.lbCols.Name = "lbCols"
    Me.lbCols.Size = New System.Drawing.Size(71, 17)
    Me.lbCols.TabIndex = 22
    Me.lbCols.Text = "Col&umns:"
    '
    'Frame1
    '
    Me.Frame1.Controls.Add(Me.optPortrait)
    Me.Frame1.Controls.Add(Me.optLandscape)
    Me.Frame1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Frame1.Location = New System.Drawing.Point(16, 115)
    Me.Frame1.Name = "Frame1"
    Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
    Me.Frame1.Size = New System.Drawing.Size(242, 91)
    Me.Frame1.TabIndex = 33
    Me.Frame1.TabStop = False
    Me.Frame1.Text = "Page Orientation"
    '
    'optPortrait
    '
    Me.optPortrait.AutoSize = True
    Me.optPortrait.Checked = True
    Me.optPortrait.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.optPortrait.Location = New System.Drawing.Point(20, 30)
    Me.optPortrait.Name = "optPortrait"
    Me.optPortrait.Size = New System.Drawing.Size(76, 21)
    Me.optPortrait.TabIndex = 4
    Me.optPortrait.TabStop = True
    Me.optPortrait.Text = "Por&trait"
    Me.optPortrait.UseVisualStyleBackColor = False
    '
    'optLandscape
    '
    Me.optLandscape.AutoSize = True
    Me.optLandscape.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.optLandscape.Location = New System.Drawing.Point(20, 55)
    Me.optLandscape.Name = "optLandscape"
    Me.optLandscape.Size = New System.Drawing.Size(101, 21)
    Me.optLandscape.TabIndex = 5
    Me.optLandscape.TabStop = True
    Me.optLandscape.Text = "&Landscape"
    Me.optLandscape.UseVisualStyleBackColor = False
    '
    'Frame2
    '
    Me.Frame2.Controls.Add(Me.chkOverlap)
    Me.Frame2.Controls.Add(Me.lbTile)
    Me.Frame2.Controls.Add(Me.chkTile)
    Me.Frame2.Controls.Add(Me.cmdActualSize)
    Me.Frame2.Controls.Add(Me.chkFitPage)
    Me.Frame2.Controls.Add(Me.txtWidth)
    Me.Frame2.Controls.Add(Me.txtHeight)
    Me.Frame2.Controls.Add(Me.lbUnits1)
    Me.Frame2.Controls.Add(Me.lbUnits0)
    Me.Frame2.Controls.Add(Me.lbWidth)
    Me.Frame2.Controls.Add(Me.lbHeight)
    Me.Frame2.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Frame2.Location = New System.Drawing.Point(279, 115)
    Me.Frame2.Name = "Frame2"
    Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
    Me.Frame2.Size = New System.Drawing.Size(251, 287)
    Me.Frame2.TabIndex = 32
    Me.Frame2.TabStop = False
    Me.Frame2.Text = "Output Size"
    '
    'lbTile
    '
    Me.lbTile.AutoSize = True
    Me.lbTile.Location = New System.Drawing.Point(34, 201)
    Me.lbTile.Name = "lbTile"
    Me.lbTile.Size = New System.Drawing.Size(105, 17)
    Me.lbTile.TabIndex = 43
    Me.lbTile.Text = "12 pages (4x3)"
    '
    'lbUnits1
    '
    Me.lbUnits1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbUnits1.Location = New System.Drawing.Point(140, 100)
    Me.lbUnits1.Name = "lbUnits1"
    Me.lbUnits1.Size = New System.Drawing.Size(36, 21)
    Me.lbUnits1.TabIndex = 40
    Me.lbUnits1.Text = "(in.)"
    '
    'lbUnits0
    '
    Me.lbUnits0.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbUnits0.Location = New System.Drawing.Point(140, 66)
    Me.lbUnits0.Name = "lbUnits0"
    Me.lbUnits0.Size = New System.Drawing.Size(36, 21)
    Me.lbUnits0.TabIndex = 39
    Me.lbUnits0.Text = "(in.)"
    '
    'lbWidth
    '
    Me.lbWidth.AutoSize = True
    Me.lbWidth.Enabled = False
    Me.lbWidth.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbWidth.Location = New System.Drawing.Point(15, 66)
    Me.lbWidth.Name = "lbWidth"
    Me.lbWidth.Size = New System.Drawing.Size(50, 17)
    Me.lbWidth.TabIndex = 14
    Me.lbWidth.Text = "&Width:"
    '
    'lbHeight
    '
    Me.lbHeight.AutoSize = True
    Me.lbHeight.Enabled = False
    Me.lbHeight.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbHeight.Location = New System.Drawing.Point(15, 100)
    Me.lbHeight.Name = "lbHeight"
    Me.lbHeight.Size = New System.Drawing.Size(53, 17)
    Me.lbHeight.TabIndex = 16
    Me.lbHeight.Text = "&Height:"
    '
    'Frame3
    '
    Me.Frame3.Controls.Add(Me.cmbVertical)
    Me.Frame3.Controls.Add(Me.cmbHorizontal)
    Me.Frame3.Controls.Add(Me.Label1)
    Me.Frame3.Controls.Add(Me.Label2)
    Me.Frame3.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Frame3.Location = New System.Drawing.Point(16, 378)
    Me.Frame3.Name = "Frame3"
    Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
    Me.Frame3.Size = New System.Drawing.Size(242, 116)
    Me.Frame3.TabIndex = 31
    Me.Frame3.TabStop = False
    Me.Frame3.Text = "Justification"
    '
    'cmbVertical
    '
    Me.cmbVertical.BackColor = System.Drawing.SystemColors.Window
    Me.cmbVertical.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cmbVertical.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmbVertical.Location = New System.Drawing.Point(107, 69)
    Me.cmbVertical.Name = "cmbVertical"
    Me.cmbVertical.Size = New System.Drawing.Size(123, 25)
    Me.cmbVertical.TabIndex = 11
    '
    'cmbHorizontal
    '
    Me.cmbHorizontal.BackColor = System.Drawing.SystemColors.Window
    Me.cmbHorizontal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cmbHorizontal.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmbHorizontal.Location = New System.Drawing.Point(107, 29)
    Me.cmbHorizontal.Name = "cmbHorizontal"
    Me.cmbHorizontal.Size = New System.Drawing.Size(123, 25)
    Me.cmbHorizontal.TabIndex = 9
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label1.Location = New System.Drawing.Point(15, 72)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(59, 17)
    Me.Label1.TabIndex = 10
    Me.Label1.Text = "&Vertical:"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label2.Location = New System.Drawing.Point(15, 32)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(77, 17)
    Me.Label2.TabIndex = 8
    Me.Label2.Text = "&Horizontal:"
    '
    'Frame4
    '
    Me.Frame4.Controls.Add(Me.optMillimeters)
    Me.Frame4.Controls.Add(Me.optInches)
    Me.Frame4.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Frame4.Location = New System.Drawing.Point(16, 239)
    Me.Frame4.Name = "Frame4"
    Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
    Me.Frame4.Size = New System.Drawing.Size(242, 96)
    Me.Frame4.TabIndex = 30
    Me.Frame4.TabStop = False
    Me.Frame4.Text = "Units"
    '
    'optMillimeters
    '
    Me.optMillimeters.AutoSize = True
    Me.optMillimeters.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.optMillimeters.Location = New System.Drawing.Point(20, 60)
    Me.optMillimeters.Name = "optMillimeters"
    Me.optMillimeters.Size = New System.Drawing.Size(98, 21)
    Me.optMillimeters.TabIndex = 7
    Me.optMillimeters.TabStop = True
    Me.optMillimeters.Text = "&Millimeters"
    Me.optMillimeters.UseVisualStyleBackColor = False
    '
    'optInches
    '
    Me.optInches.AutoSize = True
    Me.optInches.Checked = True
    Me.optInches.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.optInches.Location = New System.Drawing.Point(20, 30)
    Me.optInches.Name = "optInches"
    Me.optInches.Size = New System.Drawing.Size(72, 21)
    Me.optInches.TabIndex = 6
    Me.optInches.TabStop = True
    Me.optInches.Text = "&Inches"
    Me.optInches.UseVisualStyleBackColor = False
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdPrint.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdPrint.Location = New System.Drawing.Point(753, 563)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(92, 32)
    Me.cmdPrint.TabIndex = 30
    Me.cmdPrint.Text = "&Print"
    Me.cmdPrint.UseVisualStyleBackColor = False
    '
    'cmdCancel
    '
    Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdCancel.Location = New System.Drawing.Point(636, 563)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(92, 32)
    Me.cmdCancel.TabIndex = 29
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'lbCount
    '
    Me.lbCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbCount.AutoSize = True
    Me.lbCount.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbCount.Location = New System.Drawing.Point(13, 63)
    Me.lbCount.Name = "lbCount"
    Me.lbCount.Size = New System.Drawing.Size(252, 17)
    Me.lbCount.TabIndex = 35
    Me.lbCount.Text = "255 photos are selected to be printed."
    '
    'lbCopies
    '
    Me.lbCopies.AutoSize = True
    Me.lbCopies.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbCopies.Location = New System.Drawing.Point(293, 63)
    Me.lbCopies.Name = "lbCopies"
    Me.lbCopies.Size = New System.Drawing.Size(130, 17)
    Me.lbCopies.TabIndex = 2
    Me.lbCopies.Text = "Number of &Copies:"
    '
    'pnShadow
    '
    Me.pnShadow.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pnShadow.BackColor = System.Drawing.SystemColors.AppWorkspace
    Me.pnShadow.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.pnShadow.Location = New System.Drawing.Point(15, 15)
    Me.pnShadow.Name = "pnShadow"
    Me.pnShadow.Size = New System.Drawing.Size(317, 460)
    Me.pnShadow.TabIndex = 28
    '
    'pnPaper
    '
    Me.pnPaper.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pnPaper.BackColor = System.Drawing.SystemColors.Window
    Me.pnPaper.Controls.Add(Me.pView)
    Me.pnPaper.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.pnPaper.Location = New System.Drawing.Point(5, 4)
    Me.pnPaper.Name = "pnPaper"
    Me.pnPaper.Size = New System.Drawing.Size(317, 460)
    Me.pnPaper.TabIndex = 29
    '
    'pView
    '
    Me.pView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pView.BackColor = System.Drawing.SystemColors.Window
    Me.pView.BitmapPath = Nothing
    Me.pView.CurrentPage = 0
    Me.pView.DrawAngle = 0.0R
    Me.pView.DrawBackColor = System.Drawing.Color.White
    Me.pView.DrawBox = New System.Drawing.Rectangle(0, 0, 0, 0)
    Me.pView.DrawDashed = False
    Me.pView.DrawFilled = False
    Me.pView.DrawFont = Nothing
    Me.pView.DrawForeColor = System.Drawing.Color.Navy
    Me.pView.DrawLineWidth = 1.0!
    Me.pView.DrawPath = Nothing
    Me.pView.DrawPoints = CType(resources.GetObject("pView.DrawPoints"), System.Collections.Generic.List(Of System.Drawing.Point))
    Me.pView.DrawShape = PhotoMud.shape.Line
    Me.pView.DrawString = ""
    Me.pView.DrawTextFmt = Nothing
    Me.pView.FloaterOutline = False
    Me.pView.FloaterPath = Nothing
    Me.pView.FloaterPosition = New System.Drawing.Point(0, 0)
    Me.pView.FloaterVisible = True
    Me.pView.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low
    Me.pView.Location = New System.Drawing.Point(20, 15)
    Me.pView.Name = "pView"
    Me.pView.pageBmp = CType(resources.GetObject("pView.pageBmp"), System.Collections.Generic.List(Of System.Drawing.Bitmap))
    Me.pView.PageCount = 0
    Me.pView.RubberAngle = 0.0R
    Me.pView.rubberBackColor = System.Drawing.Color.White
    Me.pView.RubberBox = New System.Drawing.Rectangle(0, 0, 0, 0)
    Me.pView.RubberBoxCrop = False
    Me.pView.RubberColor = System.Drawing.Color.Navy
    Me.pView.RubberDashed = False
    Me.pView.RubberEnabled = False
    Me.pView.RubberFilled = False
    Me.pView.RubberFont = Nothing
    Me.pView.RubberLineWidth = 1.0R
    Me.pView.RubberPath = Nothing
    Me.pView.RubberPoints = CType(resources.GetObject("pView.RubberPoints"), System.Collections.Generic.List(Of System.Drawing.Point))
    Me.pView.RubberShape = PhotoMud.shape.Curve
    Me.pView.RubberString = ""
    Me.pView.RubberTextFmt = Nothing
    Me.pView.SelectionVisible = True
    Me.pView.Size = New System.Drawing.Size(274, 428)
    Me.pView.TabIndex = 0
    Me.pView.TabStop = False
    Me.pView.ZoomFactor = 1.0R
    '
    'pnPaperFrame
    '
    Me.pnPaperFrame.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pnPaperFrame.Controls.Add(Me.pnPaper)
    Me.pnPaperFrame.Controls.Add(Me.pnShadow)
    Me.pnPaperFrame.Location = New System.Drawing.Point(546, 20)
    Me.pnPaperFrame.Name = "pnPaperFrame"
    Me.pnPaperFrame.Size = New System.Drawing.Size(336, 478)
    Me.pnPaperFrame.TabIndex = 38
    '
    'nmNcopies
    '
    Me.nmNcopies.Location = New System.Drawing.Point(429, 61)
    Me.nmNcopies.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
    Me.nmNcopies.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nmNcopies.Name = "nmNcopies"
    Me.nmNcopies.Size = New System.Drawing.Size(54, 25)
    Me.nmNcopies.TabIndex = 3
    Me.nmNcopies.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'frmPrint
    '
    Me.AcceptButton = Me.cmdPrint
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(894, 632)
    Me.Controls.Add(Me.nmNcopies)
    Me.Controls.Add(Me.pnPaperFrame)
    Me.Controls.Add(Me.Frame5)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.cmdPreferences)
    Me.Controls.Add(Me.cmbPrinter)
    Me.Controls.Add(Me.chkSaveSettings)
    Me.Controls.Add(Me.Frame1)
    Me.Controls.Add(Me.Frame2)
    Me.Controls.Add(Me.Frame3)
    Me.Controls.Add(Me.Frame4)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.lbCount)
    Me.Controls.Add(Me.lbCopies)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 28)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmPrint"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Print"
    CType(Me.nmRows, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmCols, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Frame5.ResumeLayout(False)
    Me.Frame5.PerformLayout()
    Me.Frame1.ResumeLayout(False)
    Me.Frame1.PerformLayout()
    Me.Frame2.ResumeLayout(False)
    Me.Frame2.PerformLayout()
    Me.Frame3.ResumeLayout(False)
    Me.Frame3.PerformLayout()
    Me.Frame4.ResumeLayout(False)
    Me.Frame4.PerformLayout()
    Me.pnPaper.ResumeLayout(False)
    CType(Me.pView, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPaperFrame.ResumeLayout(False)
    CType(Me.nmNcopies, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

End Sub
 Public WithEvents pnShadow As System.Windows.Forms.Panel
 Public WithEvents pnPaper As System.Windows.Forms.Panel
 Friend WithEvents pnPaperFrame As System.Windows.Forms.Panel
 Friend WithEvents nmRows As System.Windows.Forms.NumericUpDown
 Friend WithEvents nmCols As System.Windows.Forms.NumericUpDown
 Friend WithEvents nmNcopies As System.Windows.Forms.NumericUpDown
 Friend WithEvents chkTile As System.Windows.Forms.CheckBox
 Friend WithEvents lbTile As System.Windows.Forms.Label
 Friend WithEvents chkOverlap As System.Windows.Forms.CheckBox
 Friend WithEvents pView As PhotoMud.pViewer
#End Region
End Class