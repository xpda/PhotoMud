<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCalendar
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCalendar))
    Me.tabControl1 = New System.Windows.Forms.TabControl()
    Me.TabPage1 = New System.Windows.Forms.TabPage()
    Me.cmdShuffle = New System.Windows.Forms.Button()
    Me.chkDaily = New System.Windows.Forms.CheckBox()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.txCaption = New System.Windows.Forms.RichTextBox()
    Me.cmdNextTab = New System.Windows.Forms.Button()
    Me.cmdCancel1 = New System.Windows.Forms.Button()
    Me.lbNCalPaths = New System.Windows.Forms.Label()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.cmBeginMonth = New System.Windows.Forms.ComboBox()
    Me.nmMonths = New System.Windows.Forms.NumericUpDown()
    Me.nmBeginYear = New System.Windows.Forms.NumericUpDown()
    Me.lbMonth = New System.Windows.Forms.Label()
    Me.cmdHelp1 = New System.Windows.Forms.Button()
    Me.cmdDel = New System.Windows.Forms.Button()
    Me.cmdMoveDown = New System.Windows.Forms.Button()
    Me.cmdMoveUp = New System.Windows.Forms.Button()
    Me.lstFiles = New System.Windows.Forms.ListBox()
    Me.TabPage2 = New System.Windows.Forms.TabPage()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.optPortrait = New System.Windows.Forms.RadioButton()
    Me.optLandscape = New System.Windows.Forms.RadioButton()
    Me.cmdPreviousTab = New System.Windows.Forms.Button()
    Me.Frame3 = New System.Windows.Forms.GroupBox()
    Me.cmbVertical = New System.Windows.Forms.ComboBox()
    Me.cmbHorizontal = New System.Windows.Forms.ComboBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.cmbFonts = New System.Windows.Forms.ComboBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.txTitle = New System.Windows.Forms.TextBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.cmdEditCustom = New System.Windows.Forms.Button()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.txHMargin = New System.Windows.Forms.TextBox()
    Me.txVMargin = New System.Windows.Forms.TextBox()
    Me.lbUnits1 = New System.Windows.Forms.Label()
    Me.lbUnits0 = New System.Windows.Forms.Label()
    Me.lbHMargin = New System.Windows.Forms.Label()
    Me.lbVMargin = New System.Windows.Forms.Label()
    Me.nmNcopies = New System.Windows.Forms.NumericUpDown()
    Me.lbCopies = New System.Windows.Forms.Label()
    Me.Frame1 = New System.Windows.Forms.GroupBox()
    Me.optEvenPages = New System.Windows.Forms.RadioButton()
    Me.optOddPages = New System.Windows.Forms.RadioButton()
    Me.optAllPages = New System.Windows.Forms.RadioButton()
    Me.chkCollate = New System.Windows.Forms.CheckBox()
    Me.chkSamePage = New System.Windows.Forms.CheckBox()
    Me.cmdPreferences = New System.Windows.Forms.Button()
    Me.cmbPrinter = New System.Windows.Forms.ComboBox()
    Me.pnPaperFrame = New System.Windows.Forms.Panel()
    Me.pnPaper = New System.Windows.Forms.Panel()
    Me.Picture1 = New System.Windows.Forms.PictureBox()
    Me.pnShadow = New System.Windows.Forms.Panel()
    Me.cmdNextMonth = New System.Windows.Forms.Button()
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.cmdPreviousMonth = New System.Windows.Forms.Button()
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.pView = New PhotoMud.pViewer()
    Me.tabControl1.SuspendLayout()
    Me.TabPage1.SuspendLayout()
    CType(Me.nmMonths, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmBeginYear, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabPage2.SuspendLayout()
    Me.GroupBox1.SuspendLayout()
    Me.Frame3.SuspendLayout()
    CType(Me.nmNcopies, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Frame1.SuspendLayout()
    Me.pnPaperFrame.SuspendLayout()
    Me.pnPaper.SuspendLayout()
    CType(Me.Picture1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pView, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'tabControl1
    '
    Me.tabControl1.Controls.Add(Me.TabPage1)
    Me.tabControl1.Controls.Add(Me.TabPage2)
    Me.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tabControl1.Location = New System.Drawing.Point(0, 0)
    Me.tabControl1.Name = "tabControl1"
    Me.tabControl1.SelectedIndex = 0
    Me.tabControl1.Size = New System.Drawing.Size(986, 676)
    Me.tabControl1.TabIndex = 64
    '
    'TabPage1
    '
    Me.TabPage1.Controls.Add(Me.pView)
    Me.TabPage1.Controls.Add(Me.cmdShuffle)
    Me.TabPage1.Controls.Add(Me.chkDaily)
    Me.TabPage1.Controls.Add(Me.Label6)
    Me.TabPage1.Controls.Add(Me.txCaption)
    Me.TabPage1.Controls.Add(Me.cmdNextTab)
    Me.TabPage1.Controls.Add(Me.cmdCancel1)
    Me.TabPage1.Controls.Add(Me.lbNCalPaths)
    Me.TabPage1.Controls.Add(Me.Label7)
    Me.TabPage1.Controls.Add(Me.Label8)
    Me.TabPage1.Controls.Add(Me.cmBeginMonth)
    Me.TabPage1.Controls.Add(Me.nmMonths)
    Me.TabPage1.Controls.Add(Me.nmBeginYear)
    Me.TabPage1.Controls.Add(Me.lbMonth)
    Me.TabPage1.Controls.Add(Me.cmdHelp1)
    Me.TabPage1.Controls.Add(Me.cmdDel)
    Me.TabPage1.Controls.Add(Me.cmdMoveDown)
    Me.TabPage1.Controls.Add(Me.cmdMoveUp)
    Me.TabPage1.Controls.Add(Me.lstFiles)
    Me.TabPage1.Location = New System.Drawing.Point(4, 26)
    Me.TabPage1.Name = "TabPage1"
    Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage1.Size = New System.Drawing.Size(978, 646)
    Me.TabPage1.TabIndex = 0
    Me.TabPage1.Text = "Photos"
    Me.TabPage1.UseVisualStyleBackColor = True
    '
    'cmdShuffle
    '
    Me.cmdShuffle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdShuffle.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdShuffle.Location = New System.Drawing.Point(533, 559)
    Me.cmdShuffle.Name = "cmdShuffle"
    Me.cmdShuffle.Size = New System.Drawing.Size(92, 41)
    Me.cmdShuffle.TabIndex = 82
    Me.cmdShuffle.Text = "Sh&uffle"
    Me.cmdShuffle.UseVisualStyleBackColor = False
    '
    'chkDaily
    '
    Me.chkDaily.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.chkDaily.AutoSize = True
    Me.chkDaily.Location = New System.Drawing.Point(50, 606)
    Me.chkDaily.Name = "chkDaily"
    Me.chkDaily.Size = New System.Drawing.Size(125, 21)
    Me.chkDaily.TabIndex = 81
    Me.chkDaily.Text = "Dail&y Calendar"
    Me.ToolTip1.SetToolTip(Me.chkDaily, "Print pages ordered so they can be stapled into a calendar")
    Me.chkDaily.UseVisualStyleBackColor = True
    '
    'Label6
    '
    Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label6.AutoSize = True
    Me.Label6.Location = New System.Drawing.Point(44, 385)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(86, 17)
    Me.Label6.TabIndex = 4
    Me.Label6.Text = "&Description:"
    '
    'txCaption
    '
    Me.txCaption.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.txCaption.Location = New System.Drawing.Point(47, 407)
    Me.txCaption.Name = "txCaption"
    Me.txCaption.Size = New System.Drawing.Size(318, 81)
    Me.txCaption.TabIndex = 5
    Me.txCaption.Text = "Description of photo to be displayed on the calendar"
    '
    'cmdNextTab
    '
    Me.cmdNextTab.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdNextTab.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdNextTab.Location = New System.Drawing.Point(857, 558)
    Me.cmdNextTab.Name = "cmdNextTab"
    Me.cmdNextTab.Size = New System.Drawing.Size(92, 41)
    Me.cmdNextTab.TabIndex = 13
    Me.cmdNextTab.Text = "&Next"
    Me.cmdNextTab.UseVisualStyleBackColor = False
    '
    'cmdCancel1
    '
    Me.cmdCancel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdCancel1.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdCancel1.Location = New System.Drawing.Point(747, 559)
    Me.cmdCancel1.Name = "cmdCancel1"
    Me.cmdCancel1.Size = New System.Drawing.Size(92, 41)
    Me.cmdCancel1.TabIndex = 12
    Me.cmdCancel1.Text = "Cancel"
    Me.ToolTip1.SetToolTip(Me.cmdCancel1, "Cancel calendar and return")
    Me.cmdCancel1.UseVisualStyleBackColor = False
    '
    'lbNCalPaths
    '
    Me.lbNCalPaths.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbNCalPaths.AutoSize = True
    Me.lbNCalPaths.Location = New System.Drawing.Point(74, 345)
    Me.lbNCalPaths.Name = "lbNCalPaths"
    Me.lbNCalPaths.Size = New System.Drawing.Size(130, 17)
    Me.lbNCalPaths.TabIndex = 80
    Me.lbNCalPaths.Text = "Number of Photos:"
    '
    'Label7
    '
    Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label7.AutoSize = True
    Me.Label7.Location = New System.Drawing.Point(44, 570)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(131, 17)
    Me.Label7.TabIndex = 9
    Me.Label7.Text = "Number of &Months:"
    '
    'Label8
    '
    Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(44, 536)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(111, 17)
    Me.Label8.TabIndex = 6
    Me.Label8.Text = "&Beginning Date:"
    '
    'cmBeginMonth
    '
    Me.cmBeginMonth.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmBeginMonth.FormattingEnabled = True
    Me.cmBeginMonth.Items.AddRange(New Object() {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"})
    Me.cmBeginMonth.Location = New System.Drawing.Point(188, 533)
    Me.cmBeginMonth.Name = "cmBeginMonth"
    Me.cmBeginMonth.Size = New System.Drawing.Size(166, 25)
    Me.cmBeginMonth.TabIndex = 7
    Me.ToolTip1.SetToolTip(Me.cmBeginMonth, "Beginning month for calendar")
    '
    'nmMonths
    '
    Me.nmMonths.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.nmMonths.Location = New System.Drawing.Point(188, 568)
    Me.nmMonths.Maximum = New Decimal(New Integer() {1200, 0, 0, 0})
    Me.nmMonths.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nmMonths.Name = "nmMonths"
    Me.nmMonths.Size = New System.Drawing.Size(56, 25)
    Me.nmMonths.TabIndex = 10
    Me.ToolTip1.SetToolTip(Me.nmMonths, "Number of months to be included in the calendar")
    Me.nmMonths.Value = New Decimal(New Integer() {12, 0, 0, 0})
    '
    'nmBeginYear
    '
    Me.nmBeginYear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.nmBeginYear.Location = New System.Drawing.Point(360, 533)
    Me.nmBeginYear.Maximum = New Decimal(New Integer() {3000, 0, 0, 0})
    Me.nmBeginYear.Minimum = New Decimal(New Integer() {2000, 0, 0, -2147483648})
    Me.nmBeginYear.Name = "nmBeginYear"
    Me.nmBeginYear.Size = New System.Drawing.Size(76, 25)
    Me.nmBeginYear.TabIndex = 8
    Me.ToolTip1.SetToolTip(Me.nmBeginYear, "Beginning year for calendar")
    Me.nmBeginYear.Value = New Decimal(New Integer() {2009, 0, 0, 0})
    '
    'lbMonth
    '
    Me.lbMonth.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbMonth.AutoSize = True
    Me.lbMonth.Location = New System.Drawing.Point(390, 491)
    Me.lbMonth.Name = "lbMonth"
    Me.lbMonth.Size = New System.Drawing.Size(71, 17)
    Me.lbMonth.TabIndex = 73
    Me.lbMonth.Text = "Photo for "
    '
    'cmdHelp1
    '
    Me.cmdHelp1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdHelp1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdHelp1.Image = CType(resources.GetObject("cmdHelp1.Image"), System.Drawing.Image)
    Me.cmdHelp1.Location = New System.Drawing.Point(681, 555)
    Me.cmdHelp1.Name = "cmdHelp1"
    Me.cmdHelp1.Size = New System.Drawing.Size(44, 44)
    Me.cmdHelp1.TabIndex = 11
    Me.cmdHelp1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp1, "Help")
    Me.cmdHelp1.UseVisualStyleBackColor = False
    '
    'cmdDel
    '
    Me.cmdDel.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdDel.Image = CType(resources.GetObject("cmdDel.Image"), System.Drawing.Image)
    Me.cmdDel.Location = New System.Drawing.Point(279, 196)
    Me.cmdDel.Name = "cmdDel"
    Me.cmdDel.Size = New System.Drawing.Size(44, 44)
    Me.cmdDel.TabIndex = 3
    Me.cmdDel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdDel, "Remove photo from calendar")
    Me.cmdDel.UseVisualStyleBackColor = False
    '
    'cmdMoveDown
    '
    Me.cmdMoveDown.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdMoveDown.Image = CType(resources.GetObject("cmdMoveDown.Image"), System.Drawing.Image)
    Me.cmdMoveDown.Location = New System.Drawing.Point(279, 140)
    Me.cmdMoveDown.Name = "cmdMoveDown"
    Me.cmdMoveDown.Size = New System.Drawing.Size(44, 44)
    Me.cmdMoveDown.TabIndex = 2
    Me.cmdMoveDown.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdMoveDown, "Move photo down")
    Me.cmdMoveDown.UseVisualStyleBackColor = False
    '
    'cmdMoveUp
    '
    Me.cmdMoveUp.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdMoveUp.Image = CType(resources.GetObject("cmdMoveUp.Image"), System.Drawing.Image)
    Me.cmdMoveUp.Location = New System.Drawing.Point(279, 84)
    Me.cmdMoveUp.Name = "cmdMoveUp"
    Me.cmdMoveUp.Size = New System.Drawing.Size(44, 44)
    Me.cmdMoveUp.TabIndex = 1
    Me.cmdMoveUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdMoveUp, "Move photo up")
    Me.cmdMoveUp.UseVisualStyleBackColor = False
    '
    'lstFiles
    '
    Me.lstFiles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lstFiles.BackColor = System.Drawing.SystemColors.Window
    Me.lstFiles.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lstFiles.ItemHeight = 17
    Me.lstFiles.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "1", "2"})
    Me.lstFiles.Location = New System.Drawing.Point(77, 50)
    Me.lstFiles.Name = "lstFiles"
    Me.lstFiles.Size = New System.Drawing.Size(196, 276)
    Me.lstFiles.TabIndex = 0
    '
    'TabPage2
    '
    Me.TabPage2.Controls.Add(Me.GroupBox1)
    Me.TabPage2.Controls.Add(Me.cmdPreviousTab)
    Me.TabPage2.Controls.Add(Me.Frame3)
    Me.TabPage2.Controls.Add(Me.Label3)
    Me.TabPage2.Controls.Add(Me.cmbFonts)
    Me.TabPage2.Controls.Add(Me.Label2)
    Me.TabPage2.Controls.Add(Me.txTitle)
    Me.TabPage2.Controls.Add(Me.Label1)
    Me.TabPage2.Controls.Add(Me.cmdEditCustom)
    Me.TabPage2.Controls.Add(Me.Panel1)
    Me.TabPage2.Controls.Add(Me.txHMargin)
    Me.TabPage2.Controls.Add(Me.txVMargin)
    Me.TabPage2.Controls.Add(Me.lbUnits1)
    Me.TabPage2.Controls.Add(Me.lbUnits0)
    Me.TabPage2.Controls.Add(Me.lbHMargin)
    Me.TabPage2.Controls.Add(Me.lbVMargin)
    Me.TabPage2.Controls.Add(Me.nmNcopies)
    Me.TabPage2.Controls.Add(Me.lbCopies)
    Me.TabPage2.Controls.Add(Me.Frame1)
    Me.TabPage2.Controls.Add(Me.cmdPreferences)
    Me.TabPage2.Controls.Add(Me.cmbPrinter)
    Me.TabPage2.Controls.Add(Me.pnPaperFrame)
    Me.TabPage2.Controls.Add(Me.cmdNextMonth)
    Me.TabPage2.Controls.Add(Me.cmdHelp)
    Me.TabPage2.Controls.Add(Me.cmdPrint)
    Me.TabPage2.Controls.Add(Me.cmdCancel)
    Me.TabPage2.Controls.Add(Me.cmdPreviousMonth)
    Me.TabPage2.Location = New System.Drawing.Point(4, 26)
    Me.TabPage2.Name = "TabPage2"
    Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage2.Size = New System.Drawing.Size(978, 646)
    Me.TabPage2.TabIndex = 1
    Me.TabPage2.Text = "Print Options"
    Me.TabPage2.UseVisualStyleBackColor = True
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.optPortrait)
    Me.GroupBox1.Controls.Add(Me.optLandscape)
    Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.GroupBox1.Location = New System.Drawing.Point(325, 430)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
    Me.GroupBox1.Size = New System.Drawing.Size(238, 91)
    Me.GroupBox1.TabIndex = 90
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Page Orientation"
    '
    'optPortrait
    '
    Me.optPortrait.Checked = True
    Me.optPortrait.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.optPortrait.Location = New System.Drawing.Point(24, 29)
    Me.optPortrait.Name = "optPortrait"
    Me.optPortrait.Size = New System.Drawing.Size(126, 22)
    Me.optPortrait.TabIndex = 5
    Me.optPortrait.TabStop = True
    Me.optPortrait.Text = "Por&trait"
    Me.optPortrait.UseVisualStyleBackColor = False
    '
    'optLandscape
    '
    Me.optLandscape.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.optLandscape.Location = New System.Drawing.Point(24, 54)
    Me.optLandscape.Name = "optLandscape"
    Me.optLandscape.Size = New System.Drawing.Size(131, 22)
    Me.optLandscape.TabIndex = 6
    Me.optLandscape.TabStop = True
    Me.optLandscape.Text = "&Landscape"
    Me.optLandscape.UseVisualStyleBackColor = False
    '
    'cmdPreviousTab
    '
    Me.cmdPreviousTab.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdPreviousTab.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdPreviousTab.Location = New System.Drawing.Point(389, 576)
    Me.cmdPreviousTab.Name = "cmdPreviousTab"
    Me.cmdPreviousTab.Size = New System.Drawing.Size(93, 41)
    Me.cmdPreviousTab.TabIndex = 89
    Me.cmdPreviousTab.Text = "Pre&vious"
    Me.ToolTip1.SetToolTip(Me.cmdPreviousTab, "Go back to the previous tab")
    Me.cmdPreviousTab.UseVisualStyleBackColor = False
    '
    'Frame3
    '
    Me.Frame3.Controls.Add(Me.cmbVertical)
    Me.Frame3.Controls.Add(Me.cmbHorizontal)
    Me.Frame3.Controls.Add(Me.Label4)
    Me.Frame3.Controls.Add(Me.Label5)
    Me.Frame3.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Frame3.Location = New System.Drawing.Point(325, 313)
    Me.Frame3.Name = "Frame3"
    Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
    Me.Frame3.Size = New System.Drawing.Size(238, 98)
    Me.Frame3.TabIndex = 88
    Me.Frame3.TabStop = False
    Me.Frame3.Text = "Photo Justification"
    '
    'cmbVertical
    '
    Me.cmbVertical.BackColor = System.Drawing.SystemColors.Window
    Me.cmbVertical.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cmbVertical.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmbVertical.Items.AddRange(New Object() {"Center", "Top", "Bottom"})
    Me.cmbVertical.Location = New System.Drawing.Point(107, 61)
    Me.cmbVertical.Name = "cmbVertical"
    Me.cmbVertical.Size = New System.Drawing.Size(123, 25)
    Me.cmbVertical.TabIndex = 12
    '
    'cmbHorizontal
    '
    Me.cmbHorizontal.BackColor = System.Drawing.SystemColors.Window
    Me.cmbHorizontal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cmbHorizontal.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmbHorizontal.Items.AddRange(New Object() {"Center", "Left", "Right"})
    Me.cmbHorizontal.Location = New System.Drawing.Point(107, 29)
    Me.cmbHorizontal.Name = "cmbHorizontal"
    Me.cmbHorizontal.Size = New System.Drawing.Size(123, 25)
    Me.cmbHorizontal.TabIndex = 10
    '
    'Label4
    '
    Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label4.Location = New System.Drawing.Point(13, 64)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(85, 18)
    Me.Label4.TabIndex = 11
    Me.Label4.Text = "&Vertical:"
    '
    'Label5
    '
    Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label5.Location = New System.Drawing.Point(13, 32)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(86, 19)
    Me.Label5.TabIndex = 9
    Me.Label5.Text = "&Horizontal:"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(12, 58)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(41, 17)
    Me.Label3.TabIndex = 87
    Me.Label3.Text = "Font:"
    '
    'cmbFonts
    '
    Me.cmbFonts.FormattingEnabled = True
    Me.cmbFonts.Location = New System.Drawing.Point(72, 55)
    Me.cmbFonts.Name = "cmbFonts"
    Me.cmbFonts.Size = New System.Drawing.Size(222, 25)
    Me.cmbFonts.TabIndex = 86
    Me.ToolTip1.SetToolTip(Me.cmbFonts, "Calendar font")
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(12, 210)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(101, 17)
    Me.Label2.TabIndex = 85
    Me.Label2.Text = "Calendar Title:"
    '
    'txTitle
    '
    Me.txTitle.Location = New System.Drawing.Point(15, 232)
    Me.txTitle.Name = "txTitle"
    Me.txTitle.Size = New System.Drawing.Size(279, 25)
    Me.txTitle.TabIndex = 84
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(13, 290)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(163, 17)
    Me.Label1.TabIndex = 83
    Me.Label1.Text = "Categories for Calender"
    '
    'cmdEditCustom
    '
    Me.cmdEditCustom.Font = New System.Drawing.Font("Arial", 7.8!)
    Me.cmdEditCustom.Location = New System.Drawing.Point(192, 281)
    Me.cmdEditCustom.Name = "cmdEditCustom"
    Me.cmdEditCustom.Size = New System.Drawing.Size(61, 28)
    Me.cmdEditCustom.TabIndex = 66
    Me.cmdEditCustom.Text = "&Edit..."
    Me.ToolTip1.SetToolTip(Me.cmdEditCustom, "Add or remove date categories")
    Me.cmdEditCustom.UseVisualStyleBackColor = True
    '
    'Panel1
    '
    Me.Panel1.AutoScroll = True
    Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Panel1.Location = New System.Drawing.Point(15, 313)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(279, 304)
    Me.Panel1.TabIndex = 82
    Me.ToolTip1.SetToolTip(Me.Panel1, "Categories of dates for the calendar")
    '
    'txHMargin
    '
    Me.txHMargin.AcceptsReturn = True
    Me.txHMargin.BackColor = System.Drawing.SystemColors.Window
    Me.txHMargin.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txHMargin.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txHMargin.Location = New System.Drawing.Point(156, 121)
    Me.txHMargin.MaxLength = 0
    Me.txHMargin.Name = "txHMargin"
    Me.txHMargin.Size = New System.Drawing.Size(61, 25)
    Me.txHMargin.TabIndex = 77
    Me.txHMargin.Text = "0"
    '
    'txVMargin
    '
    Me.txVMargin.AcceptsReturn = True
    Me.txVMargin.BackColor = System.Drawing.SystemColors.Window
    Me.txVMargin.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txVMargin.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txVMargin.Location = New System.Drawing.Point(156, 152)
    Me.txVMargin.MaxLength = 0
    Me.txVMargin.Name = "txVMargin"
    Me.txVMargin.Size = New System.Drawing.Size(61, 25)
    Me.txVMargin.TabIndex = 79
    Me.txVMargin.Text = "0"
    '
    'lbUnits1
    '
    Me.lbUnits1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbUnits1.Location = New System.Drawing.Point(221, 155)
    Me.lbUnits1.Name = "lbUnits1"
    Me.lbUnits1.Size = New System.Drawing.Size(36, 21)
    Me.lbUnits1.TabIndex = 81
    Me.lbUnits1.Text = "(in.)"
    '
    'lbUnits0
    '
    Me.lbUnits0.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbUnits0.Location = New System.Drawing.Point(221, 124)
    Me.lbUnits0.Name = "lbUnits0"
    Me.lbUnits0.Size = New System.Drawing.Size(36, 21)
    Me.lbUnits0.TabIndex = 80
    Me.lbUnits0.Text = "(in.)"
    '
    'lbHMargin
    '
    Me.lbHMargin.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbHMargin.Location = New System.Drawing.Point(12, 125)
    Me.lbHMargin.Name = "lbHMargin"
    Me.lbHMargin.Size = New System.Drawing.Size(139, 21)
    Me.lbHMargin.TabIndex = 76
    Me.lbHMargin.Text = "Horizontal Margin:"
    '
    'lbVMargin
    '
    Me.lbVMargin.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbVMargin.Location = New System.Drawing.Point(12, 155)
    Me.lbVMargin.Name = "lbVMargin"
    Me.lbVMargin.Size = New System.Drawing.Size(139, 21)
    Me.lbVMargin.TabIndex = 78
    Me.lbVMargin.Text = "Vertical Margin:"
    '
    'nmNcopies
    '
    Me.nmNcopies.Location = New System.Drawing.Point(156, 90)
    Me.nmNcopies.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
    Me.nmNcopies.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nmNcopies.Name = "nmNcopies"
    Me.nmNcopies.Size = New System.Drawing.Size(61, 25)
    Me.nmNcopies.TabIndex = 75
    Me.nmNcopies.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'lbCopies
    '
    Me.lbCopies.AutoSize = True
    Me.lbCopies.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbCopies.Location = New System.Drawing.Point(12, 92)
    Me.lbCopies.Name = "lbCopies"
    Me.lbCopies.Size = New System.Drawing.Size(130, 17)
    Me.lbCopies.TabIndex = 74
    Me.lbCopies.Text = "Number of &Copies:"
    '
    'Frame1
    '
    Me.Frame1.Controls.Add(Me.optEvenPages)
    Me.Frame1.Controls.Add(Me.optOddPages)
    Me.Frame1.Controls.Add(Me.optAllPages)
    Me.Frame1.Controls.Add(Me.chkCollate)
    Me.Frame1.Controls.Add(Me.chkSamePage)
    Me.Frame1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Frame1.Location = New System.Drawing.Point(325, 70)
    Me.Frame1.Name = "Frame1"
    Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
    Me.Frame1.Size = New System.Drawing.Size(238, 207)
    Me.Frame1.TabIndex = 70
    Me.Frame1.TabStop = False
    Me.Frame1.Text = "Printing Options"
    '
    'optEvenPages
    '
    Me.optEvenPages.AutoSize = True
    Me.optEvenPages.Location = New System.Drawing.Point(16, 135)
    Me.optEvenPages.Name = "optEvenPages"
    Me.optEvenPages.Size = New System.Drawing.Size(138, 21)
    Me.optEvenPages.TabIndex = 66
    Me.optEvenPages.TabStop = True
    Me.optEvenPages.Text = "Print even pages"
    Me.ToolTip1.SetToolTip(Me.optEvenPages, "Print every other page, even pages only")
    Me.optEvenPages.UseVisualStyleBackColor = True
    '
    'optOddPages
    '
    Me.optOddPages.AutoSize = True
    Me.optOddPages.Location = New System.Drawing.Point(16, 108)
    Me.optOddPages.Name = "optOddPages"
    Me.optOddPages.Size = New System.Drawing.Size(131, 21)
    Me.optOddPages.TabIndex = 65
    Me.optOddPages.TabStop = True
    Me.optOddPages.Text = "Print odd pages"
    Me.ToolTip1.SetToolTip(Me.optOddPages, "Print every other page, odd pages only")
    Me.optOddPages.UseVisualStyleBackColor = True
    '
    'optAllPages
    '
    Me.optAllPages.AutoSize = True
    Me.optAllPages.Location = New System.Drawing.Point(16, 81)
    Me.optAllPages.Name = "optAllPages"
    Me.optAllPages.Size = New System.Drawing.Size(121, 21)
    Me.optAllPages.TabIndex = 64
    Me.optAllPages.TabStop = True
    Me.optAllPages.Text = "Print all pages"
    Me.optAllPages.UseVisualStyleBackColor = True
    '
    'chkCollate
    '
    Me.chkCollate.AutoSize = True
    Me.chkCollate.Location = New System.Drawing.Point(16, 165)
    Me.chkCollate.Name = "chkCollate"
    Me.chkCollate.Size = New System.Drawing.Size(187, 21)
    Me.chkCollate.TabIndex = 63
    Me.chkCollate.Text = "Order pages for stapling"
    Me.ToolTip1.SetToolTip(Me.chkCollate, "Print pages ordered so they can be stapled into a calendar")
    Me.chkCollate.UseVisualStyleBackColor = True
    '
    'chkSamePage
    '
    Me.chkSamePage.Location = New System.Drawing.Point(16, 27)
    Me.chkSamePage.Name = "chkSamePage"
    Me.chkSamePage.Size = New System.Drawing.Size(218, 46)
    Me.chkSamePage.TabIndex = 7
    Me.chkSamePage.Text = "Photo and calendar on the same page"
    Me.chkSamePage.UseVisualStyleBackColor = True
    '
    'cmdPreferences
    '
    Me.cmdPreferences.Font = New System.Drawing.Font("Arial", 7.8!)
    Me.cmdPreferences.Location = New System.Drawing.Point(300, 19)
    Me.cmdPreferences.Name = "cmdPreferences"
    Me.cmdPreferences.Size = New System.Drawing.Size(113, 25)
    Me.cmdPreferences.TabIndex = 69
    Me.cmdPreferences.Text = "P&references..."
    Me.ToolTip1.SetToolTip(Me.cmdPreferences, "Printer preferences")
    Me.cmdPreferences.UseVisualStyleBackColor = False
    '
    'cmbPrinter
    '
    Me.cmbPrinter.BackColor = System.Drawing.SystemColors.Window
    Me.cmbPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cmbPrinter.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmbPrinter.Location = New System.Drawing.Point(16, 19)
    Me.cmbPrinter.Name = "cmbPrinter"
    Me.cmbPrinter.Size = New System.Drawing.Size(278, 25)
    Me.cmbPrinter.TabIndex = 68
    Me.ToolTip1.SetToolTip(Me.cmbPrinter, "Printer to be used to print calendar")
    '
    'pnPaperFrame
    '
    Me.pnPaperFrame.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pnPaperFrame.Controls.Add(Me.pnPaper)
    Me.pnPaperFrame.Controls.Add(Me.pnShadow)
    Me.pnPaperFrame.Location = New System.Drawing.Point(585, 19)
    Me.pnPaperFrame.Name = "pnPaperFrame"
    Me.pnPaperFrame.Size = New System.Drawing.Size(382, 526)
    Me.pnPaperFrame.TabIndex = 67
    '
    'pnPaper
    '
    Me.pnPaper.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pnPaper.BackColor = System.Drawing.SystemColors.Window
    Me.pnPaper.Controls.Add(Me.Picture1)
    Me.pnPaper.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.pnPaper.Location = New System.Drawing.Point(5, 4)
    Me.pnPaper.Name = "pnPaper"
    Me.pnPaper.Size = New System.Drawing.Size(363, 508)
    Me.pnPaper.TabIndex = 29
    '
    'Picture1
    '
    Me.Picture1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Picture1.Location = New System.Drawing.Point(14, 18)
    Me.Picture1.Name = "Picture1"
    Me.Picture1.Size = New System.Drawing.Size(336, 478)
    Me.Picture1.TabIndex = 0
    Me.Picture1.TabStop = False
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
    Me.pnShadow.Size = New System.Drawing.Size(363, 508)
    Me.pnShadow.TabIndex = 28
    '
    'cmdNextMonth
    '
    Me.cmdNextMonth.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdNextMonth.Image = CType(resources.GetObject("cmdNextMonth.Image"), System.Drawing.Image)
    Me.cmdNextMonth.Location = New System.Drawing.Point(902, 576)
    Me.cmdNextMonth.Name = "cmdNextMonth"
    Me.cmdNextMonth.Size = New System.Drawing.Size(41, 41)
    Me.cmdNextMonth.TabIndex = 65
    Me.ToolTip1.SetToolTip(Me.cmdNextMonth, "Next month")
    Me.cmdNextMonth.UseVisualStyleBackColor = True
    '
    'cmdHelp
    '
    Me.cmdHelp.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(325, 576)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 41)
    Me.cmdHelp.TabIndex = 71
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdPrint.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdPrint.Location = New System.Drawing.Point(585, 576)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(93, 41)
    Me.cmdPrint.TabIndex = 72
    Me.cmdPrint.Text = "&Print"
    Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print the calendar")
    Me.cmdPrint.UseVisualStyleBackColor = False
    '
    'cmdCancel
    '
    Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdCancel.Location = New System.Drawing.Point(487, 576)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(93, 41)
    Me.cmdCancel.TabIndex = 73
    Me.cmdCancel.Text = "Cancel"
    Me.ToolTip1.SetToolTip(Me.cmdCancel, "Cancel the calendar and return")
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'cmdPreviousMonth
    '
    Me.cmdPreviousMonth.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdPreviousMonth.Image = CType(resources.GetObject("cmdPreviousMonth.Image"), System.Drawing.Image)
    Me.cmdPreviousMonth.Location = New System.Drawing.Point(834, 576)
    Me.cmdPreviousMonth.Name = "cmdPreviousMonth"
    Me.cmdPreviousMonth.Size = New System.Drawing.Size(41, 41)
    Me.cmdPreviousMonth.TabIndex = 64
    Me.ToolTip1.SetToolTip(Me.cmdPreviousMonth, "Previous month")
    Me.cmdPreviousMonth.UseVisualStyleBackColor = True
    '
    'pView
    '
    Me.pView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pView.BackColor = System.Drawing.SystemColors.ControlDarkDark
    Me.pView.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low
    Me.pView.Location = New System.Drawing.Point(389, 37)
    Me.pView.Name = "pView"
    Me.pView.Size = New System.Drawing.Size(560, 451)
    Me.pView.TabIndex = 83
    Me.pView.TabStop = False
    Me.pView.ZoomFactor = 1.0R
    '
    'frmCalendar
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(986, 676)
    Me.Controls.Add(Me.tabControl1)
    Me.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "frmCalendar"
    Me.ShowInTaskbar = False
    Me.Text = "Create Calendar"
    Me.tabControl1.ResumeLayout(False)
    Me.TabPage1.ResumeLayout(False)
    Me.TabPage1.PerformLayout()
    CType(Me.nmMonths, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmBeginYear, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabPage2.ResumeLayout(False)
    Me.TabPage2.PerformLayout()
    Me.GroupBox1.ResumeLayout(False)
    Me.Frame3.ResumeLayout(False)
    CType(Me.nmNcopies, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Frame1.ResumeLayout(False)
    Me.Frame1.PerformLayout()
    Me.pnPaperFrame.ResumeLayout(False)
    Me.pnPaper.ResumeLayout(False)
    CType(Me.Picture1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pView, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

End Sub
    Friend WithEvents tabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents optPortrait As System.Windows.Forms.RadioButton
    Public WithEvents optLandscape As System.Windows.Forms.RadioButton
    Public WithEvents cmdPreviousTab As System.Windows.Forms.Button
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents cmbVertical As System.Windows.Forms.ComboBox
    Public WithEvents cmbHorizontal As System.Windows.Forms.ComboBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbFonts As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdEditCustom As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents txHMargin As System.Windows.Forms.TextBox
    Public WithEvents txVMargin As System.Windows.Forms.TextBox
    Public WithEvents lbUnits1 As System.Windows.Forms.Label
    Public WithEvents lbUnits0 As System.Windows.Forms.Label
    Public WithEvents lbHMargin As System.Windows.Forms.Label
    Public WithEvents lbVMargin As System.Windows.Forms.Label
    Friend WithEvents nmNcopies As System.Windows.Forms.NumericUpDown
    Public WithEvents lbCopies As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Friend WithEvents optEvenPages As System.Windows.Forms.RadioButton
    Friend WithEvents optOddPages As System.Windows.Forms.RadioButton
    Friend WithEvents optAllPages As System.Windows.Forms.RadioButton
    Friend WithEvents chkCollate As System.Windows.Forms.CheckBox
    Friend WithEvents chkSamePage As System.Windows.Forms.CheckBox
    Public WithEvents cmdPreferences As System.Windows.Forms.Button
    Public WithEvents cmbPrinter As System.Windows.Forms.ComboBox
    Friend WithEvents pnPaperFrame As System.Windows.Forms.Panel
    Public WithEvents pnPaper As System.Windows.Forms.Panel
    Friend WithEvents Picture1 As System.Windows.Forms.PictureBox
    Public WithEvents pnShadow As System.Windows.Forms.Panel
    Friend WithEvents cmdNextMonth As System.Windows.Forms.Button
    Public WithEvents cmdHelp As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdPreviousMonth As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txCaption As System.Windows.Forms.RichTextBox
    Public WithEvents cmdNextTab As System.Windows.Forms.Button
    Public WithEvents cmdCancel1 As System.Windows.Forms.Button
    Friend WithEvents lbNCalPaths As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmBeginMonth As System.Windows.Forms.ComboBox
    Friend WithEvents nmMonths As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmBeginYear As System.Windows.Forms.NumericUpDown
    Friend WithEvents lbMonth As System.Windows.Forms.Label
    Public WithEvents cmdHelp1 As System.Windows.Forms.Button
    Public WithEvents cmdDel As System.Windows.Forms.Button
    Public WithEvents cmdMoveDown As System.Windows.Forms.Button
    Public WithEvents cmdMoveUp As System.Windows.Forms.Button
    Public WithEvents lstFiles As System.Windows.Forms.ListBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents chkDaily As System.Windows.Forms.CheckBox
    Public WithEvents cmdShuffle As System.Windows.Forms.Button
    Friend WithEvents pView As PhotoMud.pViewer
End Class
