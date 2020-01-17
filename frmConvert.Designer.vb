<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmConvert
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
  Public WithEvents txPrefix As TextBox
  Public WithEvents chkSubfolders As CheckBox
  Public WithEvents cmdStart As Button
  Public WithEvents cmdColor As Button
  Public WithEvents cmdCancel As Button
  Public WithEvents cmbFiletype As ComboBox
  Public WithEvents txCurrentPath As TextBox
  Public WithEvents chkExpandImages As CheckBox
  Public WithEvents chkResize As CheckBox
  Public WithEvents lbYres As Label
  Public WithEvents lbXres As Label
  Public WithEvents fmResize As GroupBox
  Public WithEvents Label1 As Label
  Public WithEvents lbCompression As Label
  Public WithEvents lbQuality1 As Label
  Public WithEvents label3 As Label
  Public WithEvents Label4 As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConvert))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdColor = New System.Windows.Forms.Button()
    Me.txPrefix = New System.Windows.Forms.TextBox()
    Me.chkSubfolders = New System.Windows.Forms.CheckBox()
    Me.cmdStart = New System.Windows.Forms.Button()
    Me.cmbFiletype = New System.Windows.Forms.ComboBox()
    Me.txCurrentPath = New System.Windows.Forms.TextBox()
    Me.nmYres = New System.Windows.Forms.NumericUpDown()
    Me.nmXres = New System.Windows.Forms.NumericUpDown()
    Me.chkExpandImages = New System.Windows.Forms.CheckBox()
    Me.chkResize = New System.Windows.Forms.CheckBox()
    Me.lbYres = New System.Windows.Forms.Label()
    Me.lbXres = New System.Windows.Forms.Label()
    Me.lbQuality1 = New System.Windows.Forms.Label()
    Me.nmQuality = New System.Windows.Forms.NumericUpDown()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.nmPngCompression = New System.Windows.Forms.NumericUpDown()
    Me.chkExif = New System.Windows.Forms.CheckBox()
    Me.cmdWatermark = New System.Windows.Forms.Button()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.fmResize = New System.Windows.Forms.GroupBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.lbCompression = New System.Windows.Forms.Label()
    Me.label3 = New System.Windows.Forms.Label()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.TreeView = New System.Windows.Forms.TreeView()
    Me.imgTreeView = New System.Windows.Forms.ImageList(Me.components)
    Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
    Me.ProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
    Me.lbCount = New System.Windows.Forms.ToolStripStatusLabel()
    Me.fmPng = New System.Windows.Forms.GroupBox()
    Me.chkPngIndexed = New System.Windows.Forms.CheckBox()
    Me.fmJPG = New System.Windows.Forms.GroupBox()
    Me.bkgSave = New System.ComponentModel.BackgroundWorker()
    Me.chkAutocrop = New System.Windows.Forms.CheckBox()
    CType(Me.nmYres, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmXres, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmQuality, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmPngCompression, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmResize.SuspendLayout()
    Me.StatusStrip1.SuspendLayout()
    Me.fmPng.SuspendLayout()
    Me.fmJPG.SuspendLayout()
    Me.SuspendLayout()
    '
    'cmdColor
    '
    Me.cmdColor.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdColor.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdColor.Location = New System.Drawing.Point(427, 619)
    Me.cmdColor.Name = "cmdColor"
    Me.cmdColor.Size = New System.Drawing.Size(101, 46)
    Me.cmdColor.TabIndex = 50
    Me.cmdColor.Text = "Ad&just Colors"
    Me.ToolTip1.SetToolTip(Me.cmdColor, "Set a color adjustment for the photos being converted")
    Me.cmdColor.UseVisualStyleBackColor = False
    '
    'txPrefix
    '
    Me.txPrefix.AcceptsReturn = True
    Me.txPrefix.BackColor = System.Drawing.SystemColors.Window
    Me.txPrefix.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txPrefix.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txPrefix.Location = New System.Drawing.Point(161, 84)
    Me.txPrefix.MaxLength = 0
    Me.txPrefix.Name = "txPrefix"
    Me.txPrefix.Size = New System.Drawing.Size(234, 25)
    Me.txPrefix.TabIndex = 3
    Me.ToolTip1.SetToolTip(Me.txPrefix, "You can enter a prefix to be added to the" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "beginning of the file names as they ar" & _
        "e saved.")
    '
    'chkSubfolders
    '
    Me.chkSubfolders.AutoSize = True
    Me.chkSubfolders.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkSubfolders.Location = New System.Drawing.Point(30, 478)
    Me.chkSubfolders.Name = "chkSubfolders"
    Me.chkSubfolders.Size = New System.Drawing.Size(137, 21)
    Me.chkSubfolders.TabIndex = 12
    Me.chkSubfolders.Text = "Save S&ubfolders"
    Me.ToolTip1.SetToolTip(Me.chkSubfolders, "If you have tagged photos from multiple folders," & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "you can save them under their s" & _
        "ubfolder names.")
    Me.chkSubfolders.UseVisualStyleBackColor = False
    '
    'cmdStart
    '
    Me.cmdStart.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdStart.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdStart.Location = New System.Drawing.Point(711, 619)
    Me.cmdStart.Name = "cmdStart"
    Me.cmdStart.Size = New System.Drawing.Size(101, 46)
    Me.cmdStart.TabIndex = 52
    Me.cmdStart.Text = "&Start Conversion"
    Me.ToolTip1.SetToolTip(Me.cmdStart, "Begin conversion." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "You will be asked before a file is overwritten.")
    Me.cmdStart.UseVisualStyleBackColor = False
    '
    'cmbFiletype
    '
    Me.cmbFiletype.BackColor = System.Drawing.SystemColors.Window
    Me.cmbFiletype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cmbFiletype.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmbFiletype.Location = New System.Drawing.Point(161, 44)
    Me.cmbFiletype.Name = "cmbFiletype"
    Me.cmbFiletype.Size = New System.Drawing.Size(236, 25)
    Me.cmbFiletype.TabIndex = 1
    Me.ToolTip1.SetToolTip(Me.cmbFiletype, "File type of the saved photos, usually .jpg")
    '
    'txCurrentPath
    '
    Me.txCurrentPath.AcceptsReturn = True
    Me.txCurrentPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txCurrentPath.BackColor = System.Drawing.SystemColors.Window
    Me.txCurrentPath.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txCurrentPath.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txCurrentPath.Location = New System.Drawing.Point(427, 42)
    Me.txCurrentPath.MaxLength = 0
    Me.txCurrentPath.Name = "txCurrentPath"
    Me.txCurrentPath.Size = New System.Drawing.Size(529, 25)
    Me.txCurrentPath.TabIndex = 14
    Me.ToolTip1.SetToolTip(Me.txCurrentPath, "Folder where the converted photos will be saved")
    '
    'nmYres
    '
    Me.nmYres.Location = New System.Drawing.Point(262, 102)
    Me.nmYres.Maximum = New Decimal(New Integer() {20000, 0, 0, 0})
    Me.nmYres.Minimum = New Decimal(New Integer() {20, 0, 0, 0})
    Me.nmYres.Name = "nmYres"
    Me.nmYres.Size = New System.Drawing.Size(91, 25)
    Me.nmYres.TabIndex = 10
    Me.ToolTip1.SetToolTip(Me.nmYres, "The new file will be limited to this vertical resolution.")
    Me.nmYres.Value = New Decimal(New Integer() {20000, 0, 0, 0})
    '
    'nmXres
    '
    Me.nmXres.Location = New System.Drawing.Point(262, 67)
    Me.nmXres.Maximum = New Decimal(New Integer() {20000, 0, 0, 0})
    Me.nmXres.Minimum = New Decimal(New Integer() {20, 0, 0, 0})
    Me.nmXres.Name = "nmXres"
    Me.nmXres.Size = New System.Drawing.Size(91, 25)
    Me.nmXres.TabIndex = 8
    Me.ToolTip1.SetToolTip(Me.nmXres, "The new file will be limited to this horizontal resolution.")
    Me.nmXres.Value = New Decimal(New Integer() {20000, 0, 0, 0})
    '
    'chkExpandImages
    '
    Me.chkExpandImages.AutoSize = True
    Me.chkExpandImages.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkExpandImages.Location = New System.Drawing.Point(25, 137)
    Me.chkExpandImages.Name = "chkExpandImages"
    Me.chkExpandImages.Size = New System.Drawing.Size(183, 21)
    Me.chkExpandImages.TabIndex = 11
    Me.chkExpandImages.Text = "E&xpand Smaller Photos"
    Me.ToolTip1.SetToolTip(Me.chkExpandImages, "Check if you want smaller photos, if any, " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "to be expanded to fit the maximum res" & _
        "olution.")
    Me.chkExpandImages.UseVisualStyleBackColor = False
    '
    'chkResize
    '
    Me.chkResize.AutoSize = True
    Me.chkResize.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkResize.Location = New System.Drawing.Point(25, 30)
    Me.chkResize.Name = "chkResize"
    Me.chkResize.Size = New System.Drawing.Size(126, 21)
    Me.chkResize.TabIndex = 6
    Me.chkResize.Text = "&Resize Photos"
    Me.ToolTip1.SetToolTip(Me.chkResize, "Change the size of the output files")
    Me.chkResize.UseVisualStyleBackColor = False
    '
    'lbYres
    '
    Me.lbYres.AutoSize = True
    Me.lbYres.Enabled = False
    Me.lbYres.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbYres.Location = New System.Drawing.Point(25, 105)
    Me.lbYres.Name = "lbYres"
    Me.lbYres.Size = New System.Drawing.Size(162, 17)
    Me.lbYres.TabIndex = 9
    Me.lbYres.Text = "Max &Vertical Resolution:"
    Me.ToolTip1.SetToolTip(Me.lbYres, "The new file will be limited to this vertical resolution.")
    '
    'lbXres
    '
    Me.lbXres.AutoSize = True
    Me.lbXres.Enabled = False
    Me.lbXres.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbXres.Location = New System.Drawing.Point(25, 70)
    Me.lbXres.Name = "lbXres"
    Me.lbXres.Size = New System.Drawing.Size(180, 17)
    Me.lbXres.TabIndex = 7
    Me.lbXres.Text = "Max &Horizontal Resolution:"
    Me.ToolTip1.SetToolTip(Me.lbXres, "The new file will be limited to this horizontal resolution.")
    '
    'lbQuality1
    '
    Me.lbQuality1.AutoSize = True
    Me.lbQuality1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbQuality1.Location = New System.Drawing.Point(18, 21)
    Me.lbQuality1.Name = "lbQuality1"
    Me.lbQuality1.Size = New System.Drawing.Size(89, 17)
    Me.lbQuality1.TabIndex = 4
    Me.lbQuality1.Text = "&JPG Quality:"
    Me.ToolTip1.SetToolTip(Me.lbQuality1, "A low number is higher quality, large file size." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "A larger number is lower qualit" & _
        "y, smaller file size." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "15 is a good value in most cases.")
    '
    'nmQuality
    '
    Me.nmQuality.Location = New System.Drawing.Point(190, 18)
    Me.nmQuality.Name = "nmQuality"
    Me.nmQuality.Size = New System.Drawing.Size(70, 25)
    Me.nmQuality.TabIndex = 5
    Me.ToolTip1.SetToolTip(Me.nmQuality, "A low number is higher quality, large file size." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "A larger number is lower qualit" & _
        "y, smaller file size." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "15 is a good value in most cases.")
    Me.nmQuality.Value = New Decimal(New Integer() {95, 0, 0, 0})
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label2.Location = New System.Drawing.Point(36, 38)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(135, 17)
    Me.Label2.TabIndex = 6
    Me.Label2.Text = "&Compression (0-9):"
    Me.ToolTip1.SetToolTip(Me.Label2, "A low number is higher quality, large file size." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "A larger number is lower qualit" & _
        "y, smaller file size." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "15 is a good value in most cases.")
    '
    'nmPngCompression
    '
    Me.nmPngCompression.Location = New System.Drawing.Point(191, 38)
    Me.nmPngCompression.Maximum = New Decimal(New Integer() {9, 0, 0, 0})
    Me.nmPngCompression.Name = "nmPngCompression"
    Me.nmPngCompression.Size = New System.Drawing.Size(70, 25)
    Me.nmPngCompression.TabIndex = 7
    Me.ToolTip1.SetToolTip(Me.nmPngCompression, "A low number is higher quality, large file size." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "A larger number is lower qualit" & _
        "y, smaller file size." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "15 is a good value in most cases.")
    Me.nmPngCompression.Value = New Decimal(New Integer() {7, 0, 0, 0})
    '
    'chkExif
    '
    Me.chkExif.AutoSize = True
    Me.chkExif.Checked = True
    Me.chkExif.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkExif.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkExif.Location = New System.Drawing.Point(21, 54)
    Me.chkExif.Name = "chkExif"
    Me.chkExif.Size = New System.Drawing.Size(154, 21)
    Me.chkExif.TabIndex = 20
    Me.chkExif.Text = "Save Exif &Metadata"
    Me.ToolTip1.SetToolTip(Me.chkExif, "Change the size of the output files")
    Me.chkExif.UseVisualStyleBackColor = False
    '
    'cmdWatermark
    '
    Me.cmdWatermark.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdWatermark.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdWatermark.Location = New System.Drawing.Point(569, 619)
    Me.cmdWatermark.Name = "cmdWatermark"
    Me.cmdWatermark.Size = New System.Drawing.Size(101, 46)
    Me.cmdWatermark.TabIndex = 51
    Me.cmdWatermark.Text = "&Watermark"
    Me.ToolTip1.SetToolTip(Me.cmdWatermark, "Set a color adjustment for the photos being converted")
    Me.cmdWatermark.UseVisualStyleBackColor = False
    '
    'cmdCancel
    '
    Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdCancel.Location = New System.Drawing.Point(853, 619)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(101, 46)
    Me.cmdCancel.TabIndex = 53
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'fmResize
    '
    Me.fmResize.Controls.Add(Me.nmYres)
    Me.fmResize.Controls.Add(Me.nmXres)
    Me.fmResize.Controls.Add(Me.chkExpandImages)
    Me.fmResize.Controls.Add(Me.chkResize)
    Me.fmResize.Controls.Add(Me.lbYres)
    Me.fmResize.Controls.Add(Me.lbXres)
    Me.fmResize.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.fmResize.Location = New System.Drawing.Point(20, 283)
    Me.fmResize.Name = "fmResize"
    Me.fmResize.Padding = New System.Windows.Forms.Padding(0)
    Me.fmResize.Size = New System.Drawing.Size(393, 181)
    Me.fmResize.TabIndex = 18
    Me.fmResize.TabStop = False
    Me.fmResize.Text = "Resize"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label1.Location = New System.Drawing.Point(22, 87)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(107, 17)
    Me.Label1.TabIndex = 2
    Me.Label1.Text = "Op&tional Prefix:"
    '
    'lbCompression
    '
    Me.lbCompression.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbCompression.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.lbCompression.Location = New System.Drawing.Point(6, 89)
    Me.lbCompression.Name = "lbCompression"
    Me.lbCompression.Size = New System.Drawing.Size(376, 70)
    Me.lbCompression.TabIndex = 19
    Me.lbCompression.Text = "(see form load for caption)"
    '
    'label3
    '
    Me.label3.AutoSize = True
    Me.label3.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.label3.Location = New System.Drawing.Point(22, 48)
    Me.label3.Name = "label3"
    Me.label3.Size = New System.Drawing.Size(118, 17)
    Me.label3.TabIndex = 0
    Me.label3.Text = "&Output File Type:"
    '
    'Label4
    '
    Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label4.AutoSize = True
    Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label4.Location = New System.Drawing.Point(424, 18)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(130, 17)
    Me.Label4.TabIndex = 13
    Me.Label4.Text = "&Destination Folder:"
    '
    'TreeView
    '
    Me.TreeView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TreeView.ImageIndex = 0
    Me.TreeView.ImageList = Me.imgTreeView
    Me.TreeView.Location = New System.Drawing.Point(427, 77)
    Me.TreeView.Name = "TreeView"
    Me.TreeView.SelectedImageIndex = 0
    Me.TreeView.Size = New System.Drawing.Size(529, 526)
    Me.TreeView.TabIndex = 27
    Me.TreeView.TabStop = False
    '
    'imgTreeView
    '
    Me.imgTreeView.ImageStream = CType(resources.GetObject("imgTreeView.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.imgTreeView.TransparentColor = System.Drawing.Color.Transparent
    Me.imgTreeView.Images.SetKeyName(0, "ClosedFolder")
    Me.imgTreeView.Images.SetKeyName(1, "OpenFolder")
    '
    'StatusStrip1
    '
    Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
    Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProgressBar1, Me.lbCount})
    Me.StatusStrip1.Location = New System.Drawing.Point(0, 681)
    Me.StatusStrip1.Name = "StatusStrip1"
    Me.StatusStrip1.Size = New System.Drawing.Size(978, 24)
    Me.StatusStrip1.TabIndex = 28
    Me.StatusStrip1.Text = "StatusStrip1"
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(200, 18)
    '
    'lbCount
    '
    Me.lbCount.Font = New System.Drawing.Font("Tahoma", 8.0!)
    Me.lbCount.Name = "lbCount"
    Me.lbCount.Size = New System.Drawing.Size(761, 19)
    Me.lbCount.Spring = True
    Me.lbCount.Text = "429 photos are tagged to be converted."
    '
    'fmPng
    '
    Me.fmPng.Controls.Add(Me.chkPngIndexed)
    Me.fmPng.Controls.Add(Me.Label2)
    Me.fmPng.Controls.Add(Me.nmPngCompression)
    Me.fmPng.Location = New System.Drawing.Point(22, 563)
    Me.fmPng.Name = "fmPng"
    Me.fmPng.Size = New System.Drawing.Size(393, 107)
    Me.fmPng.TabIndex = 30
    Me.fmPng.TabStop = False
    Me.fmPng.Text = "PNG Options"
    '
    'chkPngIndexed
    '
    Me.chkPngIndexed.AutoSize = True
    Me.chkPngIndexed.Location = New System.Drawing.Point(39, 75)
    Me.chkPngIndexed.Name = "chkPngIndexed"
    Me.chkPngIndexed.Size = New System.Drawing.Size(80, 21)
    Me.chkPngIndexed.TabIndex = 8
    Me.chkPngIndexed.Text = "Indexed"
    Me.chkPngIndexed.UseVisualStyleBackColor = True
    '
    'fmJPG
    '
    Me.fmJPG.Controls.Add(Me.chkExif)
    Me.fmJPG.Controls.Add(Me.lbQuality1)
    Me.fmJPG.Controls.Add(Me.lbCompression)
    Me.fmJPG.Controls.Add(Me.nmQuality)
    Me.fmJPG.Location = New System.Drawing.Point(22, 125)
    Me.fmJPG.Name = "fmJPG"
    Me.fmJPG.Size = New System.Drawing.Size(391, 172)
    Me.fmJPG.TabIndex = 31
    Me.fmJPG.TabStop = False
    '
    'bkgSave
    '
    '
    'chkAutocrop
    '
    Me.chkAutocrop.AutoSize = True
    Me.chkAutocrop.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkAutocrop.Location = New System.Drawing.Point(30, 515)
    Me.chkAutocrop.Name = "chkAutocrop"
    Me.chkAutocrop.Size = New System.Drawing.Size(95, 21)
    Me.chkAutocrop.TabIndex = 54
    Me.chkAutocrop.Text = "&Auto Crop"
    Me.ToolTip1.SetToolTip(Me.chkAutocrop, "If you have tagged photos from multiple folders," & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "you can save them under their s" & _
        "ubfolder names.")
    Me.chkAutocrop.UseVisualStyleBackColor = False
    '
    'frmConvert
    '
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(978, 705)
    Me.Controls.Add(Me.chkAutocrop)
    Me.Controls.Add(Me.cmdWatermark)
    Me.Controls.Add(Me.fmJPG)
    Me.Controls.Add(Me.fmPng)
    Me.Controls.Add(Me.StatusStrip1)
    Me.Controls.Add(Me.TreeView)
    Me.Controls.Add(Me.txPrefix)
    Me.Controls.Add(Me.chkSubfolders)
    Me.Controls.Add(Me.cmdStart)
    Me.Controls.Add(Me.cmdColor)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.cmbFiletype)
    Me.Controls.Add(Me.txCurrentPath)
    Me.Controls.Add(Me.fmResize)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.label3)
    Me.Controls.Add(Me.Label4)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 28)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmConvert"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Convert Selected Photos"
    CType(Me.nmYres, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmXres, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmQuality, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmPngCompression, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmResize.ResumeLayout(False)
    Me.fmResize.PerformLayout()
    Me.StatusStrip1.ResumeLayout(False)
    Me.StatusStrip1.PerformLayout()
    Me.fmPng.ResumeLayout(False)
    Me.fmPng.PerformLayout()
    Me.fmJPG.ResumeLayout(False)
    Me.fmJPG.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TreeView As System.Windows.Forms.TreeView
  Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ToolStripProgressBar
  Friend WithEvents lbCount As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents imgTreeView As System.Windows.Forms.ImageList
  Friend WithEvents nmYres As System.Windows.Forms.NumericUpDown
  Friend WithEvents nmXres As System.Windows.Forms.NumericUpDown
  Friend WithEvents nmQuality As System.Windows.Forms.NumericUpDown
 Friend WithEvents fmPng As System.Windows.Forms.GroupBox
  Friend WithEvents fmJPG As System.Windows.Forms.GroupBox
  Friend WithEvents chkPngIndexed As System.Windows.Forms.CheckBox
  Public WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents nmPngCompression As System.Windows.Forms.NumericUpDown
  Public WithEvents chkExif As System.Windows.Forms.CheckBox
  Friend WithEvents bkgSave As System.ComponentModel.BackgroundWorker
  Public WithEvents cmdWatermark As System.Windows.Forms.Button
  Public WithEvents chkAutocrop As System.Windows.Forms.CheckBox
#End Region
End Class