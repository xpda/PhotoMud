<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmWebPage
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
  Public WithEvents cmdSort As Button
  Public WithEvents chkCapMaplink As CheckBox
  Public WithEvents chkCapDescription As CheckBox
  Public WithEvents chkCapDate As CheckBox
  Public WithEvents chkCapName As CheckBox
  Public WithEvents chkCapLatLon As CheckBox
  Public WithEvents chkCapTime As CheckBox
  Public WithEvents chkCapAltitude As CheckBox
  Public WithEvents Label13 As Label
  Public WithEvents Frame1 As GroupBox
  Public WithEvents cmdHelp As Button
  Public WithEvents cmdDel As Button
  Public WithEvents txCaption As RichTextBox
  Public WithEvents cmdDefaults As Button
  Public WithEvents chkHtmlOnly As CheckBox
  Public WithEvents cmdView As Button
  Public WithEvents txTitle As TextBox
  Public WithEvents cmdMoveDown As Button
  Public WithEvents cmdMoveUp As Button
  Public WithEvents picWeb As PictureBox
  Public WithEvents lstFiles As ListBox
  Public WithEvents cmdClose As Button
  Public WithEvents cmdSave As Button
  Public WithEvents Label2 As Label
  Public WithEvents lbStatus As Label
  Public WithEvents Label11 As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWebPage))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdSort = New System.Windows.Forms.Button()
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.cmdDel = New System.Windows.Forms.Button()
    Me.cmdView = New System.Windows.Forms.Button()
    Me.cmdMoveDown = New System.Windows.Forms.Button()
    Me.cmdMoveUp = New System.Windows.Forms.Button()
    Me.cmdSave = New System.Windows.Forms.Button()
    Me.chkCapResolution = New System.Windows.Forms.CheckBox()
    Me.nmTimeOffset = New System.Windows.Forms.NumericUpDown()
    Me.chkCapDescription = New System.Windows.Forms.CheckBox()
    Me.chkCapDate = New System.Windows.Forms.CheckBox()
    Me.chkCapName = New System.Windows.Forms.CheckBox()
    Me.chkCapTime = New System.Windows.Forms.CheckBox()
    Me.txCaption = New System.Windows.Forms.RichTextBox()
    Me.cmdDefaults = New System.Windows.Forms.Button()
    Me.txTitle = New System.Windows.Forms.TextBox()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.cmdOptions = New System.Windows.Forms.Button()
    Me.chkCapMaplink = New System.Windows.Forms.CheckBox()
    Me.chkCapLatLon = New System.Windows.Forms.CheckBox()
    Me.chkCapAltitude = New System.Windows.Forms.CheckBox()
    Me.txSubtitle = New System.Windows.Forms.TextBox()
    Me.chkCapLocation = New System.Windows.Forms.CheckBox()
    Me.Frame1 = New System.Windows.Forms.GroupBox()
    Me.Label13 = New System.Windows.Forms.Label()
    Me.chkHtmlOnly = New System.Windows.Forms.CheckBox()
    Me.picWeb = New System.Windows.Forms.PictureBox()
    Me.lstFiles = New System.Windows.Forms.ListBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.lbStatus = New System.Windows.Forms.Label()
    Me.Label11 = New System.Windows.Forms.Label()
    Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
    Me.lbTagged = New System.Windows.Forms.ToolStripStatusLabel()
    Me.Progressbar1 = New System.Windows.Forms.ToolStripProgressBar()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.pView = New PhotoMud.pViewer()
    CType(Me.nmTimeOffset, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Frame1.SuspendLayout()
    CType(Me.picWeb, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.StatusStrip1.SuspendLayout()
    CType(Me.pView, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'cmdSort
    '
    Me.cmdSort.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdSort.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdSort.Location = New System.Drawing.Point(13, 569)
    Me.cmdSort.Name = "cmdSort"
    Me.cmdSort.Size = New System.Drawing.Size(97, 45)
    Me.cmdSort.TabIndex = 12
    Me.cmdSort.Text = "&Order by Photo Date"
    Me.ToolTip1.SetToolTip(Me.cmdSort, "Order the photos in the web page by date")
    Me.cmdSort.UseVisualStyleBackColor = False
    '
    'cmdHelp
    '
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(215, 214)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(44, 44)
    Me.cmdHelp.TabIndex = 4
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'cmdDel
    '
    Me.cmdDel.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdDel.Image = CType(resources.GetObject("cmdDel.Image"), System.Drawing.Image)
    Me.cmdDel.Location = New System.Drawing.Point(215, 158)
    Me.cmdDel.Name = "cmdDel"
    Me.cmdDel.Size = New System.Drawing.Size(44, 44)
    Me.cmdDel.TabIndex = 3
    Me.cmdDel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdDel, "Remove photo from web page")
    Me.cmdDel.UseVisualStyleBackColor = False
    '
    'cmdView
    '
    Me.cmdView.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdView.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdView.Location = New System.Drawing.Point(13, 625)
    Me.cmdView.Name = "cmdView"
    Me.cmdView.Size = New System.Drawing.Size(97, 45)
    Me.cmdView.TabIndex = 15
    Me.cmdView.Text = "&View Web Page"
    Me.ToolTip1.SetToolTip(Me.cmdView, "View the web page in your web browser")
    Me.cmdView.UseVisualStyleBackColor = False
    '
    'cmdMoveDown
    '
    Me.cmdMoveDown.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdMoveDown.Image = CType(resources.GetObject("cmdMoveDown.Image"), System.Drawing.Image)
    Me.cmdMoveDown.Location = New System.Drawing.Point(215, 102)
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
    Me.cmdMoveUp.Location = New System.Drawing.Point(215, 46)
    Me.cmdMoveUp.Name = "cmdMoveUp"
    Me.cmdMoveUp.Size = New System.Drawing.Size(44, 44)
    Me.cmdMoveUp.TabIndex = 1
    Me.cmdMoveUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdMoveUp, "Move photo up")
    Me.cmdMoveUp.UseVisualStyleBackColor = False
    '
    'cmdSave
    '
    Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdSave.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdSave.Location = New System.Drawing.Point(219, 569)
    Me.cmdSave.Name = "cmdSave"
    Me.cmdSave.Size = New System.Drawing.Size(97, 45)
    Me.cmdSave.TabIndex = 14
    Me.cmdSave.Text = "&Save Web Page..."
    Me.ToolTip1.SetToolTip(Me.cmdSave, "Save the web page and photos")
    Me.cmdSave.UseVisualStyleBackColor = False
    '
    'chkCapResolution
    '
    Me.chkCapResolution.AutoSize = True
    Me.chkCapResolution.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.chkCapResolution.Location = New System.Drawing.Point(15, 80)
    Me.chkCapResolution.Name = "chkCapResolution"
    Me.chkCapResolution.Size = New System.Drawing.Size(97, 20)
    Me.chkCapResolution.TabIndex = 18
    Me.chkCapResolution.Text = "R&esolution"
    Me.ToolTip1.SetToolTip(Me.chkCapResolution, "Add photo resolution to the captions")
    Me.chkCapResolution.UseVisualStyleBackColor = False
    '
    'nmTimeOffset
    '
    Me.nmTimeOffset.Location = New System.Drawing.Point(107, 162)
    Me.nmTimeOffset.Maximum = New Decimal(New Integer() {24, 0, 0, 0})
    Me.nmTimeOffset.Minimum = New Decimal(New Integer() {24, 0, 0, -2147483648})
    Me.nmTimeOffset.Name = "nmTimeOffset"
    Me.nmTimeOffset.Size = New System.Drawing.Size(56, 25)
    Me.nmTimeOffset.TabIndex = 22
    Me.ToolTip1.SetToolTip(Me.nmTimeOffset, "Add this many hours to the photo time. This is in addition " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "to any UTC adjustmen" & _
        "t specified in the web page options." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
    '
    'chkCapDescription
    '
    Me.chkCapDescription.AutoSize = True
    Me.chkCapDescription.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.chkCapDescription.Location = New System.Drawing.Point(15, 26)
    Me.chkCapDescription.Name = "chkCapDescription"
    Me.chkCapDescription.Size = New System.Drawing.Size(138, 20)
    Me.chkCapDescription.TabIndex = 16
    Me.chkCapDescription.Text = "&Photo Comments"
    Me.ToolTip1.SetToolTip(Me.chkCapDescription, "Add photo comments to the captions")
    Me.chkCapDescription.UseVisualStyleBackColor = False
    '
    'chkCapDate
    '
    Me.chkCapDate.AutoSize = True
    Me.chkCapDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.chkCapDate.Location = New System.Drawing.Point(15, 108)
    Me.chkCapDate.Name = "chkCapDate"
    Me.chkCapDate.Size = New System.Drawing.Size(101, 20)
    Me.chkCapDate.TabIndex = 19
    Me.chkCapDate.Text = "Photo &Date"
    Me.ToolTip1.SetToolTip(Me.chkCapDate, "Add photo date to the captions")
    Me.chkCapDate.UseVisualStyleBackColor = False
    '
    'chkCapName
    '
    Me.chkCapName.AutoSize = True
    Me.chkCapName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.chkCapName.Location = New System.Drawing.Point(15, 52)
    Me.chkCapName.Name = "chkCapName"
    Me.chkCapName.Size = New System.Drawing.Size(93, 20)
    Me.chkCapName.TabIndex = 17
    Me.chkCapName.Text = "File &Name"
    Me.ToolTip1.SetToolTip(Me.chkCapName, "Add photo file names to the captions")
    Me.chkCapName.UseVisualStyleBackColor = False
    '
    'chkCapTime
    '
    Me.chkCapTime.AutoSize = True
    Me.chkCapTime.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.chkCapTime.Location = New System.Drawing.Point(15, 134)
    Me.chkCapTime.Name = "chkCapTime"
    Me.chkCapTime.Size = New System.Drawing.Size(101, 20)
    Me.chkCapTime.TabIndex = 20
    Me.chkCapTime.Text = "Photo &Time"
    Me.ToolTip1.SetToolTip(Me.chkCapTime, "Add photo time to the captions")
    Me.chkCapTime.UseVisualStyleBackColor = False
    '
    'txCaption
    '
    Me.txCaption.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txCaption.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txCaption.Location = New System.Drawing.Point(289, 352)
    Me.txCaption.Name = "txCaption"
    Me.txCaption.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
    Me.txCaption.Size = New System.Drawing.Size(401, 161)
    Me.txCaption.TabIndex = 6
    Me.txCaption.Text = ""
    Me.ToolTip1.SetToolTip(Me.txCaption, "Caption to appear underneath the photo")
    '
    'cmdDefaults
    '
    Me.cmdDefaults.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdDefaults.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdDefaults.Location = New System.Drawing.Point(116, 625)
    Me.cmdDefaults.Name = "cmdDefaults"
    Me.cmdDefaults.Size = New System.Drawing.Size(97, 45)
    Me.cmdDefaults.TabIndex = 16
    Me.cmdDefaults.Text = "&Reset"
    Me.ToolTip1.SetToolTip(Me.cmdDefaults, "Reset to default options")
    Me.cmdDefaults.UseVisualStyleBackColor = False
    '
    'txTitle
    '
    Me.txTitle.AcceptsReturn = True
    Me.txTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txTitle.BackColor = System.Drawing.SystemColors.Window
    Me.txTitle.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txTitle.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txTitle.Location = New System.Drawing.Point(444, 566)
    Me.txTitle.MaxLength = 0
    Me.txTitle.Name = "txTitle"
    Me.txTitle.Size = New System.Drawing.Size(237, 25)
    Me.txTitle.TabIndex = 8
    Me.txTitle.Text = "Photos"
    Me.ToolTip1.SetToolTip(Me.txTitle, "Title to appear at the top of the web page")
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdClose.Location = New System.Drawing.Point(219, 625)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(97, 45)
    Me.cmdClose.TabIndex = 17
    Me.cmdClose.Text = "Cl&ose"
    Me.ToolTip1.SetToolTip(Me.cmdClose, "Close this dialog and return")
    Me.cmdClose.UseVisualStyleBackColor = False
    '
    'cmdOptions
    '
    Me.cmdOptions.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdOptions.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdOptions.Location = New System.Drawing.Point(116, 569)
    Me.cmdOptions.Name = "cmdOptions"
    Me.cmdOptions.Size = New System.Drawing.Size(97, 45)
    Me.cmdOptions.TabIndex = 13
    Me.cmdOptions.Text = "&Options..."
    Me.ToolTip1.SetToolTip(Me.cmdOptions, "Web page options")
    Me.cmdOptions.UseVisualStyleBackColor = False
    '
    'chkCapMaplink
    '
    Me.chkCapMaplink.AutoSize = True
    Me.chkCapMaplink.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.chkCapMaplink.Location = New System.Drawing.Point(15, 276)
    Me.chkCapMaplink.Name = "chkCapMaplink"
    Me.chkCapMaplink.Size = New System.Drawing.Size(137, 20)
    Me.chkCapMaplink.TabIndex = 25
    Me.chkCapMaplink.Text = "&Google Map Link"
    Me.ToolTip1.SetToolTip(Me.chkCapMaplink, "Add a Google map link to the photo captions")
    Me.chkCapMaplink.UseVisualStyleBackColor = False
    '
    'chkCapLatLon
    '
    Me.chkCapLatLon.AutoSize = True
    Me.chkCapLatLon.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.chkCapLatLon.Location = New System.Drawing.Point(15, 198)
    Me.chkCapLatLon.Name = "chkCapLatLon"
    Me.chkCapLatLon.Size = New System.Drawing.Size(140, 20)
    Me.chkCapLatLon.TabIndex = 23
    Me.chkCapLatLon.Text = "&GPS Coordinates"
    Me.ToolTip1.SetToolTip(Me.chkCapLatLon, "Add GPS latitude and longitude to the photo captions")
    Me.chkCapLatLon.UseVisualStyleBackColor = False
    '
    'chkCapAltitude
    '
    Me.chkCapAltitude.AutoSize = True
    Me.chkCapAltitude.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.chkCapAltitude.Location = New System.Drawing.Point(15, 224)
    Me.chkCapAltitude.Name = "chkCapAltitude"
    Me.chkCapAltitude.Size = New System.Drawing.Size(109, 20)
    Me.chkCapAltitude.TabIndex = 24
    Me.chkCapAltitude.Text = "GPS &Altitude"
    Me.ToolTip1.SetToolTip(Me.chkCapAltitude, "Add GPS altitude to the photo captions")
    Me.chkCapAltitude.UseVisualStyleBackColor = False
    '
    'txSubtitle
    '
    Me.txSubtitle.AcceptsReturn = True
    Me.txSubtitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txSubtitle.BackColor = System.Drawing.SystemColors.Window
    Me.txSubtitle.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txSubtitle.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txSubtitle.Location = New System.Drawing.Point(444, 597)
    Me.txSubtitle.MaxLength = 0
    Me.txSubtitle.Name = "txSubtitle"
    Me.txSubtitle.Size = New System.Drawing.Size(237, 25)
    Me.txSubtitle.TabIndex = 10
    Me.ToolTip1.SetToolTip(Me.txSubtitle, "Title to appear at the top of the web page")
    '
    'chkCapLocation
    '
    Me.chkCapLocation.AutoSize = True
    Me.chkCapLocation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.chkCapLocation.Location = New System.Drawing.Point(15, 250)
    Me.chkCapLocation.Name = "chkCapLocation"
    Me.chkCapLocation.Size = New System.Drawing.Size(124, 20)
    Me.chkCapLocation.TabIndex = 26
    Me.chkCapLocation.Text = "&Location Name"
    Me.ToolTip1.SetToolTip(Me.chkCapLocation, "Add a Google map link to the photo captions")
    Me.chkCapLocation.UseVisualStyleBackColor = False
    '
    'Frame1
    '
    Me.Frame1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Frame1.Controls.Add(Me.chkCapLocation)
    Me.Frame1.Controls.Add(Me.chkCapResolution)
    Me.Frame1.Controls.Add(Me.nmTimeOffset)
    Me.Frame1.Controls.Add(Me.chkCapMaplink)
    Me.Frame1.Controls.Add(Me.chkCapDescription)
    Me.Frame1.Controls.Add(Me.chkCapDate)
    Me.Frame1.Controls.Add(Me.chkCapName)
    Me.Frame1.Controls.Add(Me.chkCapLatLon)
    Me.Frame1.Controls.Add(Me.chkCapTime)
    Me.Frame1.Controls.Add(Me.chkCapAltitude)
    Me.Frame1.Controls.Add(Me.Label13)
    Me.Frame1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Frame1.Location = New System.Drawing.Point(714, 361)
    Me.Frame1.Name = "Frame1"
    Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
    Me.Frame1.Size = New System.Drawing.Size(186, 318)
    Me.Frame1.TabIndex = 53
    Me.Frame1.TabStop = False
    Me.Frame1.Text = "Add to All Captions"
    '
    'Label13
    '
    Me.Label13.AutoSize = True
    Me.Label13.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label13.Location = New System.Drawing.Point(15, 166)
    Me.Label13.Name = "Label13"
    Me.Label13.Size = New System.Drawing.Size(84, 16)
    Me.Label13.TabIndex = 21
    Me.Label13.Text = "&Hour Offset:"
    '
    'chkHtmlOnly
    '
    Me.chkHtmlOnly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.chkHtmlOnly.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkHtmlOnly.Location = New System.Drawing.Point(332, 637)
    Me.chkHtmlOnly.Name = "chkHtmlOnly"
    Me.chkHtmlOnly.Size = New System.Drawing.Size(285, 21)
    Me.chkHtmlOnly.TabIndex = 11
    Me.chkHtmlOnly.Text = "Save HTML file &only, no photos"
    Me.chkHtmlOnly.UseVisualStyleBackColor = False
    '
    'picWeb
    '
    Me.picWeb.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.picWeb.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.picWeb.Location = New System.Drawing.Point(12, 316)
    Me.picWeb.Name = "picWeb"
    Me.picWeb.Size = New System.Drawing.Size(196, 171)
    Me.picWeb.TabIndex = 50
    Me.picWeb.TabStop = False
    '
    'lstFiles
    '
    Me.lstFiles.BackColor = System.Drawing.SystemColors.Window
    Me.lstFiles.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lstFiles.ItemHeight = 17
    Me.lstFiles.Location = New System.Drawing.Point(13, 12)
    Me.lstFiles.Name = "lstFiles"
    Me.lstFiles.Size = New System.Drawing.Size(196, 293)
    Me.lstFiles.TabIndex = 0
    '
    'Label2
    '
    Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label2.Location = New System.Drawing.Point(329, 569)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(80, 17)
    Me.Label2.TabIndex = 7
    Me.Label2.Text = "Page &Title: "
    '
    'lbStatus
    '
    Me.lbStatus.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbStatus.Location = New System.Drawing.Point(10, 695)
    Me.lbStatus.Name = "lbStatus"
    Me.lbStatus.Size = New System.Drawing.Size(366, 21)
    Me.lbStatus.TabIndex = 49
    Me.lbStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter
    '
    'Label11
    '
    Me.Label11.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label11.AutoSize = True
    Me.Label11.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label11.Location = New System.Drawing.Point(286, 516)
    Me.Label11.Name = "Label11"
    Me.Label11.Size = New System.Drawing.Size(158, 17)
    Me.Label11.TabIndex = 5
    Me.Label11.Text = "&Caption (html tags OK)"
    '
    'StatusStrip1
    '
    Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
    Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lbTagged, Me.Progressbar1})
    Me.StatusStrip1.Location = New System.Drawing.Point(0, 686)
    Me.StatusStrip1.Name = "StatusStrip1"
    Me.StatusStrip1.Size = New System.Drawing.Size(912, 26)
    Me.StatusStrip1.TabIndex = 55
    Me.StatusStrip1.Text = "StatusStrip1"
    '
    'lbTagged
    '
    Me.lbTagged.Name = "lbTagged"
    Me.lbTagged.Size = New System.Drawing.Size(154, 21)
    Me.lbTagged.Text = "ToolStripStatusLabel1"
    '
    'Progressbar1
    '
    Me.Progressbar1.Name = "Progressbar1"
    Me.Progressbar1.Size = New System.Drawing.Size(180, 20)
    '
    'Label1
    '
    Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label1.Location = New System.Drawing.Point(329, 600)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(102, 17)
    Me.Label1.TabIndex = 9
    Me.Label1.Text = "Page S&ubtitle: "
    '
    'pView
    '
    Me.pView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pView.BackColor = System.Drawing.SystemColors.ControlDarkDark
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
    Me.pView.Location = New System.Drawing.Point(289, 7)
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
    Me.pView.Size = New System.Drawing.Size(611, 339)
    Me.pView.TabIndex = 56
    Me.pView.TabStop = False
    Me.pView.ZoomFactor = 1.0R
    '
    'frmWebPage
    '
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdClose
    Me.ClientSize = New System.Drawing.Size(912, 712)
    Me.Controls.Add(Me.pView)
    Me.Controls.Add(Me.txSubtitle)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.cmdOptions)
    Me.Controls.Add(Me.StatusStrip1)
    Me.Controls.Add(Me.cmdSort)
    Me.Controls.Add(Me.Frame1)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.cmdDel)
    Me.Controls.Add(Me.txCaption)
    Me.Controls.Add(Me.cmdDefaults)
    Me.Controls.Add(Me.chkHtmlOnly)
    Me.Controls.Add(Me.cmdView)
    Me.Controls.Add(Me.txTitle)
    Me.Controls.Add(Me.cmdMoveDown)
    Me.Controls.Add(Me.cmdMoveUp)
    Me.Controls.Add(Me.picWeb)
    Me.Controls.Add(Me.lstFiles)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.cmdSave)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.lbStatus)
    Me.Controls.Add(Me.Label11)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 8.25!)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 28)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmWebPage"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Make Web Page"
    CType(Me.nmTimeOffset, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Frame1.ResumeLayout(False)
    Me.Frame1.PerformLayout()
    CType(Me.picWeb, System.ComponentModel.ISupportInitialize).EndInit()
    Me.StatusStrip1.ResumeLayout(False)
    Me.StatusStrip1.PerformLayout()
    CType(Me.pView, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

End Sub
 Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
 Friend WithEvents lbTagged As System.Windows.Forms.ToolStripStatusLabel
 Friend WithEvents Progressbar1 As System.Windows.Forms.ToolStripProgressBar
 Public WithEvents cmdOptions As System.Windows.Forms.Button
 Friend WithEvents nmTimeOffset As System.Windows.Forms.NumericUpDown
 Public WithEvents chkCapResolution As System.Windows.Forms.CheckBox
 Public WithEvents txSubtitle As System.Windows.Forms.TextBox
 Public WithEvents Label1 As System.Windows.Forms.Label
 Public WithEvents chkCapLocation As System.Windows.Forms.CheckBox
 Friend WithEvents pView As PhotoMud.pViewer
#End Region
End Class