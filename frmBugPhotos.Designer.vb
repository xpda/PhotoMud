<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBugPhotos
  Inherits System.Windows.Forms.Form

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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBugPhotos))
    Me.LbFname = New System.Windows.Forms.Label()
    Me.rText0 = New System.Windows.Forms.RichTextBox()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.mnxTagged = New System.Windows.Forms.CheckBox()
    Me.txiNaturalist = New System.Windows.Forms.TextBox()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.txElevation = New System.Windows.Forms.TextBox()
    Me.Label11 = New System.Windows.Forms.Label()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.txCountry = New System.Windows.Forms.TextBox()
    Me.txState = New System.Windows.Forms.TextBox()
    Me.txCounty = New System.Windows.Forms.TextBox()
    Me.cbImageSet = New System.Windows.Forms.ComboBox()
    Me.txImageSet = New System.Windows.Forms.TextBox()
    Me.cmdGPSLocate = New System.Windows.Forms.Button()
    Me.chkLink = New System.Windows.Forms.CheckBox()
    Me.txRemarks = New System.Windows.Forms.RichTextBox()
    Me.txBugguide = New System.Windows.Forms.TextBox()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.txPixelsPerMM = New System.Windows.Forms.TextBox()
    Me.txSize = New System.Windows.Forms.TextBox()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.lbConfidence = New System.Windows.Forms.Label()
    Me.txCommon = New System.Windows.Forms.TextBox()
    Me.txTaxon = New System.Windows.Forms.TextBox()
    Me.cmdTaxon = New System.Windows.Forms.Button()
    Me.txConfidence = New System.Windows.Forms.TextBox()
    Me.txDateAdded = New System.Windows.Forms.TextBox()
    Me.txOriginalPath = New System.Windows.Forms.TextBox()
    Me.txRating = New System.Windows.Forms.TextBox()
    Me.txGPS = New System.Windows.Forms.TextBox()
    Me.txLocation = New System.Windows.Forms.TextBox()
    Me.txDate = New System.Windows.Forms.TextBox()
    Me.txFileName = New System.Windows.Forms.TextBox()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.lbOriginalPath = New System.Windows.Forms.Label()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.lbDate2 = New System.Windows.Forms.Label()
    Me.cmdReadweb = New System.Windows.Forms.Button()
    Me.cmdImageUpdate = New System.Windows.Forms.Button()
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.Label0 = New System.Windows.Forms.Label()
    Me.cmdNext = New System.Windows.Forms.Button()
    Me.cmdBack = New System.Windows.Forms.Button()
    Me.cmdSaveAll = New System.Windows.Forms.Button()
    Me.Label2w = New System.Windows.Forms.Label()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.lbPicPath = New System.Windows.Forms.Label()
    Me.tvTaxon = New System.Windows.Forms.TreeView()
    Me.cmdCrop = New System.Windows.Forms.Button()
    Me.cmdCloseTree = New System.Windows.Forms.Button()
    Me.cmdSaveData = New System.Windows.Forms.Button()
    Me.cmdDelete = New System.Windows.Forms.Button()
    Me.cmdMeasure = New System.Windows.Forms.Button()
    Me.pView = New PhotoMud.pViewer()
    Me.cmdWikimedia = New System.Windows.Forms.Button()
    Me.GroupBox1.SuspendLayout()
    CType(Me.pView, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'LbFname
    '
    Me.LbFname.AutoSize = True
    Me.LbFname.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.LbFname.Location = New System.Drawing.Point(16, 355)
    Me.LbFname.Name = "LbFname"
    Me.LbFname.Size = New System.Drawing.Size(78, 17)
    Me.LbFname.TabIndex = 30
    Me.LbFname.Text = "File Name:"
    '
    'rText0
    '
    Me.rText0.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.rText0.BackColor = System.Drawing.SystemColors.Window
    Me.rText0.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.rText0.HideSelection = False
    Me.rText0.Location = New System.Drawing.Point(21, 38)
    Me.rText0.Name = "rText0"
    Me.rText0.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
    Me.rText0.Size = New System.Drawing.Size(489, 58)
    Me.rText0.TabIndex = 60
    Me.rText0.TabStop = False
    Me.rText0.Text = ""
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.mnxTagged)
    Me.GroupBox1.Controls.Add(Me.txiNaturalist)
    Me.GroupBox1.Controls.Add(Me.Label12)
    Me.GroupBox1.Controls.Add(Me.txElevation)
    Me.GroupBox1.Controls.Add(Me.Label11)
    Me.GroupBox1.Controls.Add(Me.Label10)
    Me.GroupBox1.Controls.Add(Me.txCountry)
    Me.GroupBox1.Controls.Add(Me.txState)
    Me.GroupBox1.Controls.Add(Me.txCounty)
    Me.GroupBox1.Controls.Add(Me.cbImageSet)
    Me.GroupBox1.Controls.Add(Me.txImageSet)
    Me.GroupBox1.Controls.Add(Me.cmdGPSLocate)
    Me.GroupBox1.Controls.Add(Me.chkLink)
    Me.GroupBox1.Controls.Add(Me.txRemarks)
    Me.GroupBox1.Controls.Add(Me.txBugguide)
    Me.GroupBox1.Controls.Add(Me.Label9)
    Me.GroupBox1.Controls.Add(Me.txPixelsPerMM)
    Me.GroupBox1.Controls.Add(Me.txSize)
    Me.GroupBox1.Controls.Add(Me.Label7)
    Me.GroupBox1.Controls.Add(Me.lbConfidence)
    Me.GroupBox1.Controls.Add(Me.txCommon)
    Me.GroupBox1.Controls.Add(Me.txTaxon)
    Me.GroupBox1.Controls.Add(Me.cmdTaxon)
    Me.GroupBox1.Controls.Add(Me.txConfidence)
    Me.GroupBox1.Controls.Add(Me.txDateAdded)
    Me.GroupBox1.Controls.Add(Me.txOriginalPath)
    Me.GroupBox1.Controls.Add(Me.txRating)
    Me.GroupBox1.Controls.Add(Me.txGPS)
    Me.GroupBox1.Controls.Add(Me.txLocation)
    Me.GroupBox1.Controls.Add(Me.txDate)
    Me.GroupBox1.Controls.Add(Me.txFileName)
    Me.GroupBox1.Controls.Add(Me.Label8)
    Me.GroupBox1.Controls.Add(Me.lbOriginalPath)
    Me.GroupBox1.Controls.Add(Me.Label6)
    Me.GroupBox1.Controls.Add(Me.Label5)
    Me.GroupBox1.Controls.Add(Me.Label4)
    Me.GroupBox1.Controls.Add(Me.Label3)
    Me.GroupBox1.Controls.Add(Me.Label2)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Controls.Add(Me.lbDate2)
    Me.GroupBox1.Controls.Add(Me.LbFname)
    Me.GroupBox1.Location = New System.Drawing.Point(21, 102)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(489, 584)
    Me.GroupBox1.TabIndex = 43
    Me.GroupBox1.TabStop = False
    '
    'mnxTagged
    '
    Me.mnxTagged.AutoSize = True
    Me.mnxTagged.Enabled = False
    Me.mnxTagged.Location = New System.Drawing.Point(21, 552)
    Me.mnxTagged.Name = "mnxTagged"
    Me.mnxTagged.Size = New System.Drawing.Size(77, 21)
    Me.mnxTagged.TabIndex = 51
    Me.mnxTagged.Text = "Tagged"
    Me.mnxTagged.UseVisualStyleBackColor = True
    '
    'txiNaturalist
    '
    Me.txiNaturalist.Location = New System.Drawing.Point(359, 388)
    Me.txiNaturalist.Name = "txiNaturalist"
    Me.txiNaturalist.Size = New System.Drawing.Size(113, 25)
    Me.txiNaturalist.TabIndex = 50
    '
    'Label12
    '
    Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label12.AutoSize = True
    Me.Label12.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label12.Location = New System.Drawing.Point(282, 391)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(76, 17)
    Me.Label12.TabIndex = 49
    Me.Label12.Text = "&iNaturalist:"
    Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
    '
    'txElevation
    '
    Me.txElevation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
    Me.txElevation.Location = New System.Drawing.Point(154, 185)
    Me.txElevation.Name = "txElevation"
    Me.txElevation.Size = New System.Drawing.Size(232, 25)
    Me.txElevation.TabIndex = 20
    '
    'Label11
    '
    Me.Label11.AutoSize = True
    Me.Label11.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label11.Location = New System.Drawing.Point(16, 187)
    Me.Label11.Name = "Label11"
    Me.Label11.Size = New System.Drawing.Size(71, 17)
    Me.Label11.TabIndex = 19
    Me.Label11.Text = "Ele&vation:"
    '
    'Label10
    '
    Me.Label10.AutoSize = True
    Me.Label10.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label10.Location = New System.Drawing.Point(16, 123)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(95, 17)
    Me.Label10.TabIndex = 12
    Me.Label10.Text = "Count&y, State"
    '
    'txCountry
    '
    Me.txCountry.Location = New System.Drawing.Point(378, 120)
    Me.txCountry.Name = "txCountry"
    Me.txCountry.Size = New System.Drawing.Size(94, 25)
    Me.txCountry.TabIndex = 15
    '
    'txState
    '
    Me.txState.Location = New System.Drawing.Point(320, 120)
    Me.txState.Name = "txState"
    Me.txState.Size = New System.Drawing.Size(39, 25)
    Me.txState.TabIndex = 14
    '
    'txCounty
    '
    Me.txCounty.Location = New System.Drawing.Point(154, 120)
    Me.txCounty.Name = "txCounty"
    Me.txCounty.Size = New System.Drawing.Size(148, 25)
    Me.txCounty.TabIndex = 13
    '
    'cbImageSet
    '
    Me.cbImageSet.FormattingEnabled = True
    Me.cbImageSet.Location = New System.Drawing.Point(271, 488)
    Me.cbImageSet.Name = "cbImageSet"
    Me.cbImageSet.Size = New System.Drawing.Size(201, 25)
    Me.cbImageSet.TabIndex = 47
    '
    'txImageSet
    '
    Me.txImageSet.Location = New System.Drawing.Point(154, 488)
    Me.txImageSet.Name = "txImageSet"
    Me.txImageSet.Size = New System.Drawing.Size(93, 25)
    Me.txImageSet.TabIndex = 46
    '
    'cmdGPSLocate
    '
    Me.cmdGPSLocate.Location = New System.Drawing.Point(403, 150)
    Me.cmdGPSLocate.Name = "cmdGPSLocate"
    Me.cmdGPSLocate.Size = New System.Drawing.Size(69, 26)
    Me.cmdGPSLocate.TabIndex = 18
    Me.cmdGPSLocate.Text = "l&ocate"
    Me.cmdGPSLocate.UseVisualStyleBackColor = True
    '
    'chkLink
    '
    Me.chkLink.AutoSize = True
    Me.chkLink.Location = New System.Drawing.Point(21, 523)
    Me.chkLink.Name = "chkLink"
    Me.chkLink.Size = New System.Drawing.Size(177, 21)
    Me.chkLink.TabIndex = 48
    Me.chkLink.Text = "Lin&k to Previous Image"
    Me.chkLink.UseVisualStyleBackColor = True
    '
    'txRemarks
    '
    Me.txRemarks.Location = New System.Drawing.Point(154, 250)
    Me.txRemarks.Name = "txRemarks"
    Me.txRemarks.Size = New System.Drawing.Size(318, 60)
    Me.txRemarks.TabIndex = 25
    Me.txRemarks.Text = ""
    '
    'txBugguide
    '
    Me.txBugguide.Location = New System.Drawing.Point(154, 389)
    Me.txBugguide.Name = "txBugguide"
    Me.txBugguide.Size = New System.Drawing.Size(94, 25)
    Me.txBugguide.TabIndex = 35
    '
    'Label9
    '
    Me.Label9.AutoSize = True
    Me.Label9.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label9.Location = New System.Drawing.Point(16, 391)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(91, 17)
    Me.Label9.TabIndex = 34
    Me.Label9.Text = "&Bugguide ID:"
    '
    'txPixelsPerMM
    '
    Me.txPixelsPerMM.Location = New System.Drawing.Point(403, 219)
    Me.txPixelsPerMM.Name = "txPixelsPerMM"
    Me.txPixelsPerMM.Size = New System.Drawing.Size(69, 25)
    Me.txPixelsPerMM.TabIndex = 23
    Me.txPixelsPerMM.TabStop = False
    '
    'txSize
    '
    Me.txSize.Location = New System.Drawing.Point(154, 219)
    Me.txSize.Name = "txSize"
    Me.txSize.Size = New System.Drawing.Size(232, 25)
    Me.txSize.TabIndex = 22
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label7.Location = New System.Drawing.Point(16, 221)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(41, 17)
    Me.Label7.TabIndex = 21
    Me.Label7.Text = "Si&ze:"
    '
    'lbConfidence
    '
    Me.lbConfidence.AutoSize = True
    Me.lbConfidence.Location = New System.Drawing.Point(16, 324)
    Me.lbConfidence.Name = "lbConfidence"
    Me.lbConfidence.Size = New System.Drawing.Size(104, 17)
    Me.lbConfidence.TabIndex = 26
    Me.lbConfidence.Text = "&ID Confidence:"
    '
    'txCommon
    '
    Me.txCommon.Enabled = False
    Me.txCommon.Location = New System.Drawing.Point(154, 59)
    Me.txCommon.Name = "txCommon"
    Me.txCommon.Size = New System.Drawing.Size(318, 25)
    Me.txCommon.TabIndex = 9
    '
    'txTaxon
    '
    Me.txTaxon.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
    Me.txTaxon.Location = New System.Drawing.Point(154, 28)
    Me.txTaxon.Name = "txTaxon"
    Me.txTaxon.Size = New System.Drawing.Size(282, 25)
    Me.txTaxon.TabIndex = 5
    '
    'cmdTaxon
    '
    Me.cmdTaxon.Location = New System.Drawing.Point(442, 27)
    Me.cmdTaxon.Name = "cmdTaxon"
    Me.cmdTaxon.Size = New System.Drawing.Size(30, 26)
    Me.cmdTaxon.TabIndex = 6
    Me.cmdTaxon.Text = "..."
    Me.cmdTaxon.UseVisualStyleBackColor = True
    '
    'txConfidence
    '
    Me.txConfidence.Location = New System.Drawing.Point(154, 322)
    Me.txConfidence.Name = "txConfidence"
    Me.txConfidence.Size = New System.Drawing.Size(93, 25)
    Me.txConfidence.TabIndex = 27
    '
    'txDateAdded
    '
    Me.txDateAdded.Enabled = False
    Me.txDateAdded.Location = New System.Drawing.Point(324, 455)
    Me.txDateAdded.Name = "txDateAdded"
    Me.txDateAdded.Size = New System.Drawing.Size(148, 25)
    Me.txDateAdded.TabIndex = 44
    Me.txDateAdded.TabStop = False
    '
    'txOriginalPath
    '
    Me.txOriginalPath.Font = New System.Drawing.Font("Arial Narrow", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txOriginalPath.Location = New System.Drawing.Point(154, 423)
    Me.txOriginalPath.Name = "txOriginalPath"
    Me.txOriginalPath.Size = New System.Drawing.Size(318, 22)
    Me.txOriginalPath.TabIndex = 41
    '
    'txRating
    '
    Me.txRating.Location = New System.Drawing.Point(379, 321)
    Me.txRating.Name = "txRating"
    Me.txRating.Size = New System.Drawing.Size(93, 25)
    Me.txRating.TabIndex = 29
    '
    'txGPS
    '
    Me.txGPS.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
    Me.txGPS.Location = New System.Drawing.Point(154, 151)
    Me.txGPS.Name = "txGPS"
    Me.txGPS.Size = New System.Drawing.Size(232, 25)
    Me.txGPS.TabIndex = 17
    '
    'txLocation
    '
    Me.txLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
    Me.txLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
    Me.txLocation.Location = New System.Drawing.Point(154, 90)
    Me.txLocation.Name = "txLocation"
    Me.txLocation.Size = New System.Drawing.Size(318, 25)
    Me.txLocation.TabIndex = 11
    '
    'txDate
    '
    Me.txDate.Enabled = False
    Me.txDate.Location = New System.Drawing.Point(154, 455)
    Me.txDate.Name = "txDate"
    Me.txDate.Size = New System.Drawing.Size(148, 25)
    Me.txDate.TabIndex = 43
    Me.txDate.TabStop = False
    '
    'txFileName
    '
    Me.txFileName.Location = New System.Drawing.Point(154, 353)
    Me.txFileName.Name = "txFileName"
    Me.txFileName.Size = New System.Drawing.Size(318, 25)
    Me.txFileName.TabIndex = 31
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label8.Location = New System.Drawing.Point(16, 491)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(96, 17)
    Me.Label8.TabIndex = 45
    Me.Label8.Text = "Image S&et ID:"
    '
    'lbOriginalPath
    '
    Me.lbOriginalPath.AutoSize = True
    Me.lbOriginalPath.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbOriginalPath.Location = New System.Drawing.Point(16, 427)
    Me.lbOriginalPath.Name = "lbOriginalPath"
    Me.lbOriginalPath.Size = New System.Drawing.Size(96, 17)
    Me.lbOriginalPath.TabIndex = 40
    Me.lbOriginalPath.Text = "Original Path:"
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label6.Location = New System.Drawing.Point(16, 252)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(72, 17)
    Me.Label6.TabIndex = 24
    Me.Label6.Text = "Re&marks:"
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label5.Location = New System.Drawing.Point(304, 324)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(54, 17)
    Me.Label5.TabIndex = 28
    Me.Label5.Text = "&Rating:"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label4.Location = New System.Drawing.Point(16, 153)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(102, 17)
    Me.Label4.TabIndex = 16
    Me.Label4.Text = "&GPS Location:"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label3.Location = New System.Drawing.Point(16, 92)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(67, 17)
    Me.Label3.TabIndex = 10
    Me.Label3.Text = "&Location:"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label2.Location = New System.Drawing.Point(16, 61)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(116, 17)
    Me.Label2.TabIndex = 8
    Me.Label2.Text = "Common Name:"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label1.Location = New System.Drawing.Point(16, 30)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(114, 17)
    Me.Label1.TabIndex = 4
    Me.Label1.Text = "Scientific &Name:"
    '
    'lbDate2
    '
    Me.lbDate2.AutoSize = True
    Me.lbDate2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbDate2.Location = New System.Drawing.Point(16, 459)
    Me.lbDate2.Name = "lbDate2"
    Me.lbDate2.Size = New System.Drawing.Size(133, 17)
    Me.lbDate2.TabIndex = 42
    Me.lbDate2.Text = "Photo, Added Date:"
    '
    'cmdReadweb
    '
    Me.cmdReadweb.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdReadweb.Location = New System.Drawing.Point(1209, 770)
    Me.cmdReadweb.Name = "cmdReadweb"
    Me.cmdReadweb.Size = New System.Drawing.Size(54, 46)
    Me.cmdReadweb.TabIndex = 89
    Me.cmdReadweb.TabStop = False
    Me.cmdReadweb.Text = "read web"
    Me.cmdReadweb.UseVisualStyleBackColor = True
    Me.cmdReadweb.Visible = False
    '
    'cmdImageUpdate
    '
    Me.cmdImageUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdImageUpdate.Location = New System.Drawing.Point(1055, 770)
    Me.cmdImageUpdate.Name = "cmdImageUpdate"
    Me.cmdImageUpdate.Size = New System.Drawing.Size(132, 46)
    Me.cmdImageUpdate.TabIndex = 88
    Me.cmdImageUpdate.Text = "update image counts"
    Me.cmdImageUpdate.UseVisualStyleBackColor = True
    '
    'Label0
    '
    Me.Label0.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label0.Location = New System.Drawing.Point(18, 16)
    Me.Label0.Name = "Label0"
    Me.Label0.Size = New System.Drawing.Size(219, 30)
    Me.Label0.TabIndex = 55
    Me.Label0.Text = "P&hoto Description:"
    '
    'cmdNext
    '
    Me.cmdNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdNext.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdNext.Image = CType(resources.GetObject("cmdNext.Image"), System.Drawing.Image)
    Me.cmdNext.ImageAlign = System.Drawing.ContentAlignment.TopCenter
    Me.cmdNext.Location = New System.Drawing.Point(107, 713)
    Me.cmdNext.Name = "cmdNext"
    Me.cmdNext.Size = New System.Drawing.Size(57, 43)
    Me.cmdNext.TabIndex = 81
    Me.cmdNext.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.cmdNext.UseVisualStyleBackColor = False
    '
    'cmdBack
    '
    Me.cmdBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdBack.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdBack.Image = CType(resources.GetObject("cmdBack.Image"), System.Drawing.Image)
    Me.cmdBack.ImageAlign = System.Drawing.ContentAlignment.TopCenter
    Me.cmdBack.Location = New System.Drawing.Point(32, 713)
    Me.cmdBack.Name = "cmdBack"
    Me.cmdBack.Size = New System.Drawing.Size(57, 43)
    Me.cmdBack.TabIndex = 80
    Me.cmdBack.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.cmdBack.UseVisualStyleBackColor = False
    '
    'cmdSaveAll
    '
    Me.cmdSaveAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdSaveAll.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdSaveAll.Location = New System.Drawing.Point(315, 713)
    Me.cmdSaveAll.Name = "cmdSaveAll"
    Me.cmdSaveAll.Size = New System.Drawing.Size(92, 43)
    Me.cmdSaveAll.TabIndex = 83
    Me.cmdSaveAll.Text = "&Save"
    Me.cmdSaveAll.UseVisualStyleBackColor = False
    '
    'Label2w
    '
    Me.Label2w.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label2w.AutoSize = True
    Me.Label2w.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label2w.Location = New System.Drawing.Point(541, 799)
    Me.Label2w.Name = "Label2w"
    Me.Label2w.Size = New System.Drawing.Size(276, 17)
    Me.Label2w.TabIndex = 50
    Me.Label2w.Text = "Press F2 to reset, F3 for previous values."
    Me.Label2w.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdClose.Location = New System.Drawing.Point(415, 769)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(92, 43)
    Me.cmdClose.TabIndex = 87
    Me.cmdClose.Text = "&Close"
    Me.cmdClose.UseVisualStyleBackColor = False
    '
    'lbPicPath
    '
    Me.lbPicPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbPicPath.Location = New System.Drawing.Point(541, 761)
    Me.lbPicPath.Name = "lbPicPath"
    Me.lbPicPath.Size = New System.Drawing.Size(508, 30)
    Me.lbPicPath.TabIndex = 51
    Me.lbPicPath.Text = "Labelg"
    '
    'tvTaxon
    '
    Me.tvTaxon.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.tvTaxon.LabelEdit = True
    Me.tvTaxon.Location = New System.Drawing.Point(638, 81)
    Me.tvTaxon.Name = "tvTaxon"
    Me.tvTaxon.ShowNodeToolTips = True
    Me.tvTaxon.Size = New System.Drawing.Size(525, 529)
    Me.tvTaxon.TabIndex = 90
    Me.tvTaxon.Visible = False
    '
    'cmdCrop
    '
    Me.cmdCrop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdCrop.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdCrop.Location = New System.Drawing.Point(415, 713)
    Me.cmdCrop.Name = "cmdCrop"
    Me.cmdCrop.Size = New System.Drawing.Size(92, 43)
    Me.cmdCrop.TabIndex = 84
    Me.cmdCrop.Text = "&Q Crop"
    Me.cmdCrop.UseVisualStyleBackColor = False
    '
    'cmdCloseTree
    '
    Me.cmdCloseTree.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdCloseTree.Font = New System.Drawing.Font("Bauhaus 93", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdCloseTree.ForeColor = System.Drawing.SystemColors.ControlDarkDark
    Me.cmdCloseTree.Location = New System.Drawing.Point(1113, 66)
    Me.cmdCloseTree.Name = "cmdCloseTree"
    Me.cmdCloseTree.Size = New System.Drawing.Size(40, 40)
    Me.cmdCloseTree.TabIndex = 54
    Me.cmdCloseTree.Text = "<"
    Me.cmdCloseTree.UseVisualStyleBackColor = True
    '
    'cmdSaveData
    '
    Me.cmdSaveData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdSaveData.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdSaveData.Location = New System.Drawing.Point(315, 769)
    Me.cmdSaveData.Name = "cmdSaveData"
    Me.cmdSaveData.Size = New System.Drawing.Size(92, 43)
    Me.cmdSaveData.TabIndex = 86
    Me.cmdSaveData.Text = "Save &Data"
    Me.cmdSaveData.UseVisualStyleBackColor = False
    '
    'cmdDelete
    '
    Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdDelete.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdDelete.Location = New System.Drawing.Point(215, 769)
    Me.cmdDelete.Name = "cmdDelete"
    Me.cmdDelete.Size = New System.Drawing.Size(92, 43)
    Me.cmdDelete.TabIndex = 85
    Me.cmdDelete.TabStop = False
    Me.cmdDelete.Text = "Delete"
    Me.cmdDelete.UseVisualStyleBackColor = False
    '
    'cmdMeasure
    '
    Me.cmdMeasure.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdMeasure.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdMeasure.Location = New System.Drawing.Point(215, 713)
    Me.cmdMeasure.Name = "cmdMeasure"
    Me.cmdMeasure.Size = New System.Drawing.Size(92, 43)
    Me.cmdMeasure.TabIndex = 82
    Me.cmdMeasure.Text = "Meas&ure"
    Me.cmdMeasure.UseVisualStyleBackColor = False
    '
    'pView
    '
    Me.pView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pView.BackColor = System.Drawing.SystemColors.ControlDarkDark
    Me.pView.BitmapPath = Nothing
    Me.pView.CurrentPage = -1
    Me.pView.DrawAngle = 0.0R
    Me.pView.DrawBackColor = System.Drawing.Color.White
    Me.pView.DrawBox = New System.Drawing.Rectangle(0, 0, 0, 0)
    Me.pView.DrawDashed = False
    Me.pView.DrawFilled = False
    Me.pView.DrawFont = Nothing
    Me.pView.DrawForeColor = System.Drawing.Color.Navy
    Me.pView.DrawLineWidth = 1.0R
    Me.pView.DrawPath = Nothing
    Me.pView.DrawPoints = CType(resources.GetObject("pView.DrawPoints"), System.Collections.Generic.List(Of System.Drawing.Point))
    Me.pView.DrawShape = PhotoMud.shape.Line
    Me.pView.DrawString = ""
    Me.pView.DrawTextFmt = Nothing
    Me.pView.FloaterOutline = False
    Me.pView.FloaterPath = Nothing
    Me.pView.FloaterPosition = New System.Drawing.Point(0, 0)
    Me.pView.FloaterVisible = True
    Me.pView.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High
    Me.pView.Location = New System.Drawing.Point(530, 10)
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
    Me.pView.Size = New System.Drawing.Size(784, 746)
    Me.pView.TabIndex = 91
    Me.pView.TabStop = False
    Me.pView.ZoomFactor = 1.0R
    '
    'cmdWikimedia
    '
    Me.cmdWikimedia.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdWikimedia.Location = New System.Drawing.Point(924, 772)
    Me.cmdWikimedia.Name = "cmdWikimedia"
    Me.cmdWikimedia.Size = New System.Drawing.Size(90, 39)
    Me.cmdWikimedia.TabIndex = 92
    Me.cmdWikimedia.Text = "&Wikimedia"
    Me.cmdWikimedia.UseVisualStyleBackColor = True
    '
    'frmBugPhotos
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.cmdClose
    Me.ClientSize = New System.Drawing.Size(1326, 828)
    Me.Controls.Add(Me.cmdWikimedia)
    Me.Controls.Add(Me.cmdImageUpdate)
    Me.Controls.Add(Me.cmdMeasure)
    Me.Controls.Add(Me.cmdDelete)
    Me.Controls.Add(Me.cmdReadweb)
    Me.Controls.Add(Me.cmdSaveData)
    Me.Controls.Add(Me.cmdCloseTree)
    Me.Controls.Add(Me.cmdCrop)
    Me.Controls.Add(Me.tvTaxon)
    Me.Controls.Add(Me.rText0)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.lbPicPath)
    Me.Controls.Add(Me.Label0)
    Me.Controls.Add(Me.cmdNext)
    Me.Controls.Add(Me.cmdBack)
    Me.Controls.Add(Me.cmdSaveAll)
    Me.Controls.Add(Me.Label2w)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.pView)
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "frmBugPhotos"
    Me.ShowIcon = False
    Me.ShowInTaskbar = False
    Me.Text = "Bug Pictures"
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    CType(Me.pView, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Public WithEvents LbFname As System.Windows.Forms.Label
  Public WithEvents rText0 As System.Windows.Forms.RichTextBox
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Public WithEvents ToolTip1 As System.Windows.Forms.ToolTip
  Public WithEvents lbDate2 As System.Windows.Forms.Label
  Public WithEvents Label0 As System.Windows.Forms.Label
  Public WithEvents cmdNext As System.Windows.Forms.Button
  Public WithEvents cmdBack As System.Windows.Forms.Button
  Public WithEvents cmdSaveAll As System.Windows.Forms.Button
  Public WithEvents Label2w As System.Windows.Forms.Label
  Public WithEvents cmdClose As System.Windows.Forms.Button
  Public WithEvents Label1 As System.Windows.Forms.Label
  Public WithEvents Label6 As System.Windows.Forms.Label
  Public WithEvents Label5 As System.Windows.Forms.Label
  Public WithEvents Label4 As System.Windows.Forms.Label
  Public WithEvents Label3 As System.Windows.Forms.Label
  Public WithEvents Label2 As System.Windows.Forms.Label
  Public WithEvents Label8 As System.Windows.Forms.Label
  Public WithEvents lbOriginalPath As System.Windows.Forms.Label
  Friend WithEvents txConfidence As System.Windows.Forms.TextBox
  Friend WithEvents txDateAdded As System.Windows.Forms.TextBox
  Friend WithEvents txOriginalPath As System.Windows.Forms.TextBox
  Friend WithEvents txRating As System.Windows.Forms.TextBox
  Friend WithEvents txGPS As System.Windows.Forms.TextBox
  Friend WithEvents txLocation As System.Windows.Forms.TextBox
  Friend WithEvents txDate As System.Windows.Forms.TextBox
  Friend WithEvents txFileName As System.Windows.Forms.TextBox
  Friend WithEvents lbPicPath As System.Windows.Forms.Label
  Friend WithEvents cmdTaxon As System.Windows.Forms.Button
  Friend WithEvents tvTaxon As System.Windows.Forms.TreeView
  Friend WithEvents txCommon As System.Windows.Forms.TextBox
  Friend WithEvents txTaxon As System.Windows.Forms.TextBox
  Public WithEvents cmdCrop As System.Windows.Forms.Button
  Friend WithEvents cmdCloseTree As System.Windows.Forms.Button
  Friend WithEvents lbConfidence As System.Windows.Forms.Label
  Public WithEvents cmdSaveData As System.Windows.Forms.Button
  Public WithEvents cmdDelete As System.Windows.Forms.Button
  Friend WithEvents txSize As System.Windows.Forms.TextBox
  Public WithEvents Label7 As System.Windows.Forms.Label
  Public WithEvents cmdMeasure As System.Windows.Forms.Button
  Friend WithEvents txPixelsPerMM As System.Windows.Forms.TextBox
  Friend WithEvents txBugguide As System.Windows.Forms.TextBox
  Public WithEvents Label9 As System.Windows.Forms.Label
  Friend WithEvents chkLink As System.Windows.Forms.CheckBox
  Friend WithEvents txRemarks As System.Windows.Forms.RichTextBox
  Friend WithEvents cmdImageUpdate As System.Windows.Forms.Button
  Friend WithEvents cmdReadweb As System.Windows.Forms.Button
  Friend WithEvents cmdGPSLocate As System.Windows.Forms.Button
  Friend WithEvents txImageSet As System.Windows.Forms.TextBox
  Friend WithEvents cbImageSet As System.Windows.Forms.ComboBox
  Public WithEvents Label10 As System.Windows.Forms.Label
  Friend WithEvents txCountry As System.Windows.Forms.TextBox
  Friend WithEvents txState As System.Windows.Forms.TextBox
  Friend WithEvents txCounty As System.Windows.Forms.TextBox
  Friend WithEvents txElevation As System.Windows.Forms.TextBox
  Public WithEvents Label11 As System.Windows.Forms.Label
  Friend WithEvents pView As PhotoMud.pViewer
  Friend WithEvents cmdWikimedia As System.Windows.Forms.Button
  Friend WithEvents txiNaturalist As System.Windows.Forms.TextBox
  Public WithEvents Label12 As System.Windows.Forms.Label
  Friend WithEvents mnxTagged As System.Windows.Forms.CheckBox
End Class
