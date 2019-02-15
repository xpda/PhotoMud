<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmMainf
#Region "Windows Form Designer generated code "
  <System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
    MyBase.New()
    'This call is required by the Windows Form Designer.
    InitializeComponent()
  End Sub
  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
    If Disposing Then
      If Not components IsNot Nothing Then
        components.Dispose()
      End If
    End If
    MyBase.Dispose(Disposing)
  End Sub
  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer
  Public ToolTip1 As ToolTip
  Public WithEvents mnu As MenuStrip
 'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainf))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdDeletePage = New System.Windows.Forms.Button()
    Me.cmdInsertPage = New System.Windows.Forms.Button()
    Me.cmdAnimate = New System.Windows.Forms.Button()
    Me.mnx = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.mnxSave = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnxSaveAs = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnxClose = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnxPrint = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnxSend = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuContextImageLine1 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnxCopy = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator20 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnxZoomIn = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnxZoomOut = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnxViewFit = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnxZoom100 = New System.Windows.Forms.ToolStripMenuItem()
    Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
    Me.lbStatus = New System.Windows.Forms.ToolStripStatusLabel()
    Me.lbPosition = New System.Windows.Forms.ToolStripStatusLabel()
    Me.mnu = New System.Windows.Forms.MenuStrip()
    Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuFileNew = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuFileOpen = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuFileClose = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuFileCloseAll = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuFileSave = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuFileSaveAs = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuFileSaveSelection = New System.Windows.Forms.ToolStripMenuItem()
    Me.fileline5 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuFilePrint = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuFileSend = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator16 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuFileExplore = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuFileExit = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator17 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuFileMru1 = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuFileMru2 = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuFileMru3 = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuFileMru4 = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuFileMru5 = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuFileMru6 = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuFileMru7 = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuFileMru8 = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuFileMru9 = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuEdit = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuEditUndo = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuEditRedo = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuEditRevert = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuEditRepeatCommand = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuEditLine3 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuEditCopy = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuEditCut = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuEditPaste = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuEditPasteNew = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuEditPasteV = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuEditLine2 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuEditDeleteSelection = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuEditDeleteFile = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuEditSelectRectangle = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuEditSelectEllipse = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuEditSelectFreehand = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator19 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuEditSelectSimilar = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuEditInvertSelection = New System.Windows.Forms.ToolStripMenuItem()
    Me.editline3 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuEditInsertPage = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuEditDeletePage = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuImage = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuImageResize = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuImageExpand = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuImageLine2 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuImageRotate = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuImageFlipHoriz = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuImageFlipVert = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuImageStretch = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuImageAlign = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuImageBlur = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuImageContrastStretch = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuImageSharpen = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuImageMedian = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuImageEdgeDetect = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuImageRedeye = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator21 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuImageEffects = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuImageArtEffects = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuImageBorder = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuImageSetCrop = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuImageCrop = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuColor = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuColorEnhance = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator18 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuColorBrightness = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuColorAdjust = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuColorHisto = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuImageLightAmp = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuColorHalftone = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuColorGrayscale = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuColorNegative = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuColorSepia = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuDraw = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuDrawText = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuDrawLine = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuDrawCurve = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuDrawArrow = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuDrawCircle = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuDrawEllipse = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuDrawBox = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuDrawLineOptions = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuDrawSketch = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuDrawFill = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuDrawFillSelection = New System.Windows.Forms.ToolStripMenuItem()
    Me.drawline2 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuDrawColor = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuDrawBackColor = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuColorSample = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuTools = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuToolsInfo = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuToolsComment = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuToolsBugPhotos = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuToolsMeasure = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuToolsSearch = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuToolsPicSearch = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuToolsCombine = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuToolsConcatenate = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuToolsPicturize = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuToolsFilter = New System.Windows.Forms.ToolStripMenuItem()
    Me.toolsline1 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuToolsAssoc = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuToolsFileFilter = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuToolsWallpaper = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator14 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuToolsToolbar = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuToolsOptions = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuview = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuViewZoomIn = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuViewZoomOut = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuViewZoomWindow = New System.Windows.Forms.ToolStripMenuItem()
    Me.viewline3 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuViewFullscreen = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuViewToolbar = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuViewRefresh = New System.Windows.Forms.ToolStripMenuItem()
    Me.viewline4 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuViewZoomFit = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuViewZoom25 = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuViewZoom50 = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuViewZoom100 = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuViewZoom200 = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator22 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuViewNext = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuHelpHelpTopics = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuHelpHelpIndex = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuHelpTips = New System.Windows.Forms.ToolStripMenuItem()
    Me.helpLine1 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuHelpRegister = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuHelpUpdate = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuHelpAbout = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
    Me.mnxT = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.mnxTSmallIcons = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnxTLargeIcons = New System.Windows.Forms.ToolStripMenuItem()
    Me.toolbarline3 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnxTCustomize = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnxTHide = New System.Windows.Forms.ToolStripMenuItem()
    Me.toolbarline2 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnxTHelp = New System.Windows.Forms.ToolStripMenuItem()
    Me.bkgDraw = New System.ComponentModel.BackgroundWorker()
    Me.nmPage = New System.Windows.Forms.NumericUpDown()
    Me.panelPage = New System.Windows.Forms.Panel()
    Me.lbPage = New System.Windows.Forms.Label()
    Me.mnx.SuspendLayout()
    Me.StatusStrip1.SuspendLayout()
    Me.mnu.SuspendLayout()
    Me.mnxT.SuspendLayout()
    CType(Me.nmPage, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.panelPage.SuspendLayout()
    Me.SuspendLayout()
    '
    'cmdDeletePage
    '
    Me.cmdDeletePage.Anchor = System.Windows.Forms.AnchorStyles.Right
    Me.cmdDeletePage.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdDeletePage.Location = New System.Drawing.Point(84, 2)
    Me.cmdDeletePage.Name = "cmdDeletePage"
    Me.cmdDeletePage.Size = New System.Drawing.Size(70, 22)
    Me.cmdDeletePage.TabIndex = 21
    Me.cmdDeletePage.Text = "Delete"
    Me.ToolTip1.SetToolTip(Me.cmdDeletePage, "Delete the current page")
    Me.cmdDeletePage.UseVisualStyleBackColor = True
    '
    'cmdInsertPage
    '
    Me.cmdInsertPage.Anchor = System.Windows.Forms.AnchorStyles.Right
    Me.cmdInsertPage.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdInsertPage.Location = New System.Drawing.Point(8, 2)
    Me.cmdInsertPage.Name = "cmdInsertPage"
    Me.cmdInsertPage.Size = New System.Drawing.Size(70, 22)
    Me.cmdInsertPage.TabIndex = 20
    Me.cmdInsertPage.Text = "Insert"
    Me.ToolTip1.SetToolTip(Me.cmdInsertPage, "Insert a page after the current page.")
    Me.cmdInsertPage.UseVisualStyleBackColor = True
    '
    'cmdAnimate
    '
    Me.cmdAnimate.Anchor = System.Windows.Forms.AnchorStyles.Right
    Me.cmdAnimate.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdAnimate.Location = New System.Drawing.Point(160, 1)
    Me.cmdAnimate.Name = "cmdAnimate"
    Me.cmdAnimate.Size = New System.Drawing.Size(70, 22)
    Me.cmdAnimate.TabIndex = 22
    Me.cmdAnimate.Text = "Animate"
    Me.ToolTip1.SetToolTip(Me.cmdAnimate, "Animate the image")
    Me.cmdAnimate.UseVisualStyleBackColor = True
    '
    'mnx
    '
    Me.mnx.ImageScalingSize = New System.Drawing.Size(20, 20)
    Me.mnx.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnxSave, Me.mnxSaveAs, Me.mnxClose, Me.ToolStripSeparator3, Me.mnxPrint, Me.mnxSend, Me.mnuContextImageLine1, Me.mnxCopy, Me.ToolStripSeparator20, Me.mnxZoomIn, Me.mnxZoomOut, Me.mnxViewFit, Me.mnxZoom100})
    Me.mnx.Name = "mnContextImage"
    Me.mnx.Size = New System.Drawing.Size(201, 262)
    '
    'mnxSave
    '
    Me.mnxSave.Name = "mnxSave"
    Me.mnxSave.Size = New System.Drawing.Size(200, 24)
    Me.mnxSave.Text = "&Save"
    '
    'mnxSaveAs
    '
    Me.mnxSaveAs.Name = "mnxSaveAs"
    Me.mnxSaveAs.Size = New System.Drawing.Size(200, 24)
    Me.mnxSaveAs.Text = "Save &As"
    '
    'mnxClose
    '
    Me.mnxClose.Name = "mnxClose"
    Me.mnxClose.Size = New System.Drawing.Size(200, 24)
    Me.mnxClose.Text = "&Close"
    '
    'ToolStripSeparator3
    '
    Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
    Me.ToolStripSeparator3.Size = New System.Drawing.Size(197, 6)
    '
    'mnxPrint
    '
    Me.mnxPrint.Name = "mnxPrint"
    Me.mnxPrint.Size = New System.Drawing.Size(200, 24)
    Me.mnxPrint.Text = "&Print"
    '
    'mnxSend
    '
    Me.mnxSend.Name = "mnxSend"
    Me.mnxSend.Size = New System.Drawing.Size(200, 24)
    Me.mnxSend.Text = "E&mail Photo"
    '
    'mnuContextImageLine1
    '
    Me.mnuContextImageLine1.Name = "mnuContextImageLine1"
    Me.mnuContextImageLine1.Size = New System.Drawing.Size(197, 6)
    '
    'mnxCopy
    '
    Me.mnxCopy.Name = "mnxCopy"
    Me.mnxCopy.Size = New System.Drawing.Size(200, 24)
    Me.mnxCopy.Text = "&Copy to Clipboard"
    '
    'ToolStripSeparator20
    '
    Me.ToolStripSeparator20.Name = "ToolStripSeparator20"
    Me.ToolStripSeparator20.Size = New System.Drawing.Size(197, 6)
    '
    'mnxZoomIn
    '
    Me.mnxZoomIn.Name = "mnxZoomIn"
    Me.mnxZoomIn.Size = New System.Drawing.Size(200, 24)
    Me.mnxZoomIn.Text = "Zoom &In"
    '
    'mnxZoomOut
    '
    Me.mnxZoomOut.Name = "mnxZoomOut"
    Me.mnxZoomOut.Size = New System.Drawing.Size(200, 24)
    Me.mnxZoomOut.Text = "Zoom &Out"
    '
    'mnxViewFit
    '
    Me.mnxViewFit.Name = "mnxViewFit"
    Me.mnxViewFit.Size = New System.Drawing.Size(200, 24)
    Me.mnxViewFit.Text = "Fit to &Window"
    '
    'mnxZoom100
    '
    Me.mnxZoom100.Name = "mnxZoom100"
    Me.mnxZoom100.Size = New System.Drawing.Size(200, 24)
    Me.mnxZoom100.Text = "&100% Zoom"
    '
    'StatusStrip1
    '
    Me.StatusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
    Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
    Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lbStatus, Me.lbPosition})
    Me.StatusStrip1.Location = New System.Drawing.Point(0, 669)
    Me.StatusStrip1.Name = "StatusStrip1"
    Me.StatusStrip1.Size = New System.Drawing.Size(990, 22)
    Me.StatusStrip1.SizingGrip = False
    Me.StatusStrip1.TabIndex = 11
    Me.StatusStrip1.Text = "this is some text"
    '
    'lbStatus
    '
    Me.lbStatus.Font = New System.Drawing.Font("Tahoma", 8.0!)
    Me.lbStatus.Name = "lbStatus"
    Me.lbStatus.Size = New System.Drawing.Size(89, 17)
    Me.lbStatus.Text = "Please Wait..."
    '
    'lbPosition
    '
    Me.lbPosition.Font = New System.Drawing.Font("Tahoma", 8.0!)
    Me.lbPosition.Name = "lbPosition"
    Me.lbPosition.Size = New System.Drawing.Size(12, 17)
    Me.lbPosition.Text = " "
    '
    'mnu
    '
    Me.mnu.AllowItemReorder = True
    Me.mnu.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.mnu.ImageScalingSize = New System.Drawing.Size(20, 20)
    Me.mnu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuEdit, Me.mnuImage, Me.mnuColor, Me.mnuDraw, Me.mnuTools, Me.mnuview, Me.mnuHelp})
    Me.mnu.Location = New System.Drawing.Point(0, 0)
    Me.mnu.Name = "mnu"
    Me.mnu.Size = New System.Drawing.Size(990, 28)
    Me.mnu.TabIndex = 12
    '
    'mnuFile
    '
    Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFileNew, Me.mnuFileOpen, Me.ToolStripSeparator2, Me.mnuFileClose, Me.mnuFileCloseAll, Me.ToolStripSeparator1, Me.mnuFileSave, Me.mnuFileSaveAs, Me.mnuFileSaveSelection, Me.fileline5, Me.mnuFilePrint, Me.mnuFileSend, Me.ToolStripSeparator16, Me.mnuFileExplore, Me.mnuFileExit, Me.ToolStripSeparator17, Me.mnuFileMru1, Me.mnuFileMru2, Me.mnuFileMru3, Me.mnuFileMru4, Me.mnuFileMru5, Me.mnuFileMru6, Me.mnuFileMru7, Me.mnuFileMru8, Me.mnuFileMru9})
    Me.mnuFile.Name = "mnuFile"
    Me.mnuFile.Size = New System.Drawing.Size(40, 24)
    Me.mnuFile.Text = "&File"
    '
    'mnuFileNew
    '
    Me.mnuFileNew.MergeIndex = 10
    Me.mnuFileNew.Name = "mnuFileNew"
    Me.mnuFileNew.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
    Me.mnuFileNew.Size = New System.Drawing.Size(211, 26)
    Me.mnuFileNew.Text = "&New..."
    Me.mnuFileNew.ToolTipText = "Create a new image in Photo Mud Editor."
    '
    'mnuFileOpen
    '
    Me.mnuFileOpen.MergeIndex = 20
    Me.mnuFileOpen.Name = "mnuFileOpen"
    Me.mnuFileOpen.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
    Me.mnuFileOpen.Size = New System.Drawing.Size(211, 26)
    Me.mnuFileOpen.Text = "&Open..."
    Me.mnuFileOpen.ToolTipText = "Open a file in Photo Mud Editor."
    '
    'ToolStripSeparator2
    '
    Me.ToolStripSeparator2.MergeAction = System.Windows.Forms.MergeAction.Insert
    Me.ToolStripSeparator2.MergeIndex = 2
    Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
    Me.ToolStripSeparator2.Size = New System.Drawing.Size(208, 6)
    '
    'mnuFileClose
    '
    Me.mnuFileClose.MergeIndex = 3
    Me.mnuFileClose.Name = "mnuFileClose"
    Me.mnuFileClose.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
    Me.mnuFileClose.Size = New System.Drawing.Size(211, 26)
    Me.mnuFileClose.Text = "&Close"
    Me.mnuFileClose.ToolTipText = "Close the current photo."
    '
    'mnuFileCloseAll
    '
    Me.mnuFileCloseAll.MergeIndex = 4
    Me.mnuFileCloseAll.Name = "mnuFileCloseAll"
    Me.mnuFileCloseAll.Size = New System.Drawing.Size(211, 26)
    Me.mnuFileCloseAll.Text = "C&lose All"
    Me.mnuFileCloseAll.ToolTipText = "Close all open photos in Photo Mud Editor."
    '
    'ToolStripSeparator1
    '
    Me.ToolStripSeparator1.MergeIndex = 5
    Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
    Me.ToolStripSeparator1.Size = New System.Drawing.Size(208, 6)
    '
    'mnuFileSave
    '
    Me.mnuFileSave.MergeIndex = 6
    Me.mnuFileSave.Name = "mnuFileSave"
    Me.mnuFileSave.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
    Me.mnuFileSave.Size = New System.Drawing.Size(211, 26)
    Me.mnuFileSave.Text = "&Save"
    Me.mnuFileSave.ToolTipText = "Save the currently open photo file."
    '
    'mnuFileSaveAs
    '
    Me.mnuFileSaveAs.MergeIndex = 7
    Me.mnuFileSaveAs.Name = "mnuFileSaveAs"
    Me.mnuFileSaveAs.Size = New System.Drawing.Size(211, 26)
    Me.mnuFileSaveAs.Text = "Save &As..."
    Me.mnuFileSaveAs.ToolTipText = "Save the current photo to a specified file."
    '
    'mnuFileSaveSelection
    '
    Me.mnuFileSaveSelection.MergeIndex = 8
    Me.mnuFileSaveSelection.Name = "mnuFileSaveSelection"
    Me.mnuFileSaveSelection.Size = New System.Drawing.Size(211, 26)
    Me.mnuFileSaveSelection.Text = "Sa&ve Selection As..."
    Me.mnuFileSaveSelection.ToolTipText = "Save the selected part of the current photo to a specified file."
    '
    'fileline5
    '
    Me.fileline5.MergeIndex = 9
    Me.fileline5.Name = "fileline5"
    Me.fileline5.Size = New System.Drawing.Size(208, 6)
    '
    'mnuFilePrint
    '
    Me.mnuFilePrint.MergeIndex = 10
    Me.mnuFilePrint.Name = "mnuFilePrint"
    Me.mnuFilePrint.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
    Me.mnuFilePrint.Size = New System.Drawing.Size(211, 26)
    Me.mnuFilePrint.Text = "&Print..."
    Me.mnuFilePrint.ToolTipText = "Print the current photo."
    '
    'mnuFileSend
    '
    Me.mnuFileSend.MergeIndex = 11
    Me.mnuFileSend.Name = "mnuFileSend"
    Me.mnuFileSend.Size = New System.Drawing.Size(211, 26)
    Me.mnuFileSend.Text = "E&mail Photo..."
    Me.mnuFileSend.ToolTipText = "Email the current photo."
    '
    'ToolStripSeparator16
    '
    Me.ToolStripSeparator16.Name = "ToolStripSeparator16"
    Me.ToolStripSeparator16.Size = New System.Drawing.Size(208, 6)
    '
    'mnuFileExplore
    '
    Me.mnuFileExplore.Name = "mnuFileExplore"
    Me.mnuFileExplore.ShortcutKeys = System.Windows.Forms.Keys.F6
    Me.mnuFileExplore.Size = New System.Drawing.Size(211, 26)
    Me.mnuFileExplore.Text = "&Explore Folders"
    Me.mnuFileExplore.ToolTipText = "Open Photo Mud Explorer."
    '
    'mnuFileExit
    '
    Me.mnuFileExit.MergeIndex = 130
    Me.mnuFileExit.Name = "mnuFileExit"
    Me.mnuFileExit.Size = New System.Drawing.Size(211, 26)
    Me.mnuFileExit.Text = "E&xit"
    Me.mnuFileExit.ToolTipText = "Exit the program."
    '
    'ToolStripSeparator17
    '
    Me.ToolStripSeparator17.Name = "ToolStripSeparator17"
    Me.ToolStripSeparator17.Size = New System.Drawing.Size(208, 6)
    '
    'mnuFileMru1
    '
    Me.mnuFileMru1.MergeIndex = 151
    Me.mnuFileMru1.Name = "mnuFileMru1"
    Me.mnuFileMru1.Size = New System.Drawing.Size(211, 26)
    Me.mnuFileMru1.Text = "1"
    Me.mnuFileMru1.Visible = False
    '
    'mnuFileMru2
    '
    Me.mnuFileMru2.MergeIndex = 152
    Me.mnuFileMru2.Name = "mnuFileMru2"
    Me.mnuFileMru2.Size = New System.Drawing.Size(211, 26)
    Me.mnuFileMru2.Text = "2"
    Me.mnuFileMru2.Visible = False
    '
    'mnuFileMru3
    '
    Me.mnuFileMru3.MergeIndex = 153
    Me.mnuFileMru3.Name = "mnuFileMru3"
    Me.mnuFileMru3.Size = New System.Drawing.Size(211, 26)
    Me.mnuFileMru3.Text = "3"
    Me.mnuFileMru3.Visible = False
    '
    'mnuFileMru4
    '
    Me.mnuFileMru4.MergeIndex = 154
    Me.mnuFileMru4.Name = "mnuFileMru4"
    Me.mnuFileMru4.Size = New System.Drawing.Size(211, 26)
    Me.mnuFileMru4.Text = "4"
    Me.mnuFileMru4.Visible = False
    '
    'mnuFileMru5
    '
    Me.mnuFileMru5.MergeIndex = 155
    Me.mnuFileMru5.Name = "mnuFileMru5"
    Me.mnuFileMru5.Size = New System.Drawing.Size(211, 26)
    Me.mnuFileMru5.Text = "5"
    Me.mnuFileMru5.Visible = False
    '
    'mnuFileMru6
    '
    Me.mnuFileMru6.MergeIndex = 156
    Me.mnuFileMru6.Name = "mnuFileMru6"
    Me.mnuFileMru6.Size = New System.Drawing.Size(211, 26)
    Me.mnuFileMru6.Text = "6"
    Me.mnuFileMru6.Visible = False
    '
    'mnuFileMru7
    '
    Me.mnuFileMru7.MergeIndex = 157
    Me.mnuFileMru7.Name = "mnuFileMru7"
    Me.mnuFileMru7.Size = New System.Drawing.Size(211, 26)
    Me.mnuFileMru7.Text = "7"
    Me.mnuFileMru7.Visible = False
    '
    'mnuFileMru8
    '
    Me.mnuFileMru8.MergeIndex = 158
    Me.mnuFileMru8.Name = "mnuFileMru8"
    Me.mnuFileMru8.Size = New System.Drawing.Size(211, 26)
    Me.mnuFileMru8.Text = "8"
    Me.mnuFileMru8.Visible = False
    '
    'mnuFileMru9
    '
    Me.mnuFileMru9.MergeIndex = 159
    Me.mnuFileMru9.Name = "mnuFileMru9"
    Me.mnuFileMru9.Size = New System.Drawing.Size(211, 26)
    Me.mnuFileMru9.Text = "9"
    Me.mnuFileMru9.Visible = False
    '
    'mnuEdit
    '
    Me.mnuEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEditUndo, Me.mnuEditRedo, Me.mnuEditRevert, Me.mnuEditRepeatCommand, Me.mnuEditLine3, Me.mnuEditCopy, Me.mnuEditCut, Me.mnuEditPaste, Me.mnuEditPasteNew, Me.mnuEditPasteV, Me.mnuEditLine2, Me.mnuEditDeleteSelection, Me.mnuEditDeleteFile, Me.ToolStripSeparator4, Me.mnuEditSelectRectangle, Me.mnuEditSelectEllipse, Me.mnuEditSelectFreehand, Me.ToolStripSeparator19, Me.mnuEditSelectSimilar, Me.mnuEditInvertSelection, Me.editline3, Me.mnuEditInsertPage, Me.mnuEditDeletePage})
    Me.mnuEdit.Name = "mnuEdit"
    Me.mnuEdit.Size = New System.Drawing.Size(43, 24)
    Me.mnuEdit.Text = "&Edit"
    '
    'mnuEditUndo
    '
    Me.mnuEditUndo.Name = "mnuEditUndo"
    Me.mnuEditUndo.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
    Me.mnuEditUndo.Size = New System.Drawing.Size(285, 26)
    Me.mnuEditUndo.Text = "&Undo"
    Me.mnuEditUndo.ToolTipText = "Undo the last operation."
    '
    'mnuEditRedo
    '
    Me.mnuEditRedo.Name = "mnuEditRedo"
    Me.mnuEditRedo.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Y), System.Windows.Forms.Keys)
    Me.mnuEditRedo.Size = New System.Drawing.Size(285, 26)
    Me.mnuEditRedo.Text = "R&edo"
    Me.mnuEditRedo.ToolTipText = "Redo the previously undone operation, or Undo the last Undo."
    '
    'mnuEditRevert
    '
    Me.mnuEditRevert.Name = "mnuEditRevert"
    Me.mnuEditRevert.Size = New System.Drawing.Size(285, 26)
    Me.mnuEditRevert.Text = "Revert to Last Saved Cop&y"
    Me.mnuEditRevert.ToolTipText = "Reload the last saved copy of the photo."
    '
    'mnuEditRepeatCommand
    '
    Me.mnuEditRepeatCommand.Name = "mnuEditRepeatCommand"
    Me.mnuEditRepeatCommand.ShortcutKeys = System.Windows.Forms.Keys.F3
    Me.mnuEditRepeatCommand.Size = New System.Drawing.Size(285, 26)
    Me.mnuEditRepeatCommand.Text = "Repeat Last Command"
    Me.mnuEditRepeatCommand.ToolTipText = "Repeat the last operation."
    '
    'mnuEditLine3
    '
    Me.mnuEditLine3.Name = "mnuEditLine3"
    Me.mnuEditLine3.Size = New System.Drawing.Size(282, 6)
    '
    'mnuEditCopy
    '
    Me.mnuEditCopy.Name = "mnuEditCopy"
    Me.mnuEditCopy.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
    Me.mnuEditCopy.Size = New System.Drawing.Size(285, 26)
    Me.mnuEditCopy.Text = "&Copy"
    Me.mnuEditCopy.ToolTipText = "Copy the current photo or selection to the Windows Clipboard."
    '
    'mnuEditCut
    '
    Me.mnuEditCut.Name = "mnuEditCut"
    Me.mnuEditCut.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
    Me.mnuEditCut.Size = New System.Drawing.Size(285, 26)
    Me.mnuEditCut.Text = "C&ut"
    Me.mnuEditCut.ToolTipText = "Cut (copy, then delete) the current selection to the Windows Clipboard."
    '
    'mnuEditPaste
    '
    Me.mnuEditPaste.Name = "mnuEditPaste"
    Me.mnuEditPaste.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
    Me.mnuEditPaste.Size = New System.Drawing.Size(285, 26)
    Me.mnuEditPaste.Text = "&Paste"
    Me.mnuEditPaste.ToolTipText = "Paste the image in the clipboard into the current photo."
    '
    'mnuEditPasteNew
    '
    Me.mnuEditPasteNew.Name = "mnuEditPasteNew"
    Me.mnuEditPasteNew.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
    Me.mnuEditPasteNew.Size = New System.Drawing.Size(285, 26)
    Me.mnuEditPasteNew.Text = "Paste into New &Window"
    Me.mnuEditPasteNew.ToolTipText = "Paste the image in the clipboard into a new Photo Mud Editor window."
    '
    'mnuEditPasteV
    '
    Me.mnuEditPasteV.Name = "mnuEditPasteV"
    Me.mnuEditPasteV.Size = New System.Drawing.Size(285, 26)
    Me.mnuEditPasteV.Text = "S&tretch Paste"
    Me.mnuEditPasteV.ToolTipText = "Paste the image using four corner points."
    '
    'mnuEditLine2
    '
    Me.mnuEditLine2.Name = "mnuEditLine2"
    Me.mnuEditLine2.Size = New System.Drawing.Size(282, 6)
    '
    'mnuEditDeleteSelection
    '
    Me.mnuEditDeleteSelection.Name = "mnuEditDeleteSelection"
    Me.mnuEditDeleteSelection.ShortcutKeyDisplayString = "Del"
    Me.mnuEditDeleteSelection.Size = New System.Drawing.Size(285, 26)
    Me.mnuEditDeleteSelection.Text = "&Delete Selection"
    Me.mnuEditDeleteSelection.ToolTipText = "Delete the current selection."
    '
    'mnuEditDeleteFile
    '
    Me.mnuEditDeleteFile.Name = "mnuEditDeleteFile"
    Me.mnuEditDeleteFile.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
    Me.mnuEditDeleteFile.Size = New System.Drawing.Size(285, 26)
    Me.mnuEditDeleteFile.Text = "Delete &File"
    Me.mnuEditDeleteFile.ToolTipText = "Delete the current photo from disk."
    '
    'ToolStripSeparator4
    '
    Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
    Me.ToolStripSeparator4.Size = New System.Drawing.Size(282, 6)
    '
    'mnuEditSelectRectangle
    '
    Me.mnuEditSelectRectangle.Name = "mnuEditSelectRectangle"
    Me.mnuEditSelectRectangle.Size = New System.Drawing.Size(285, 26)
    Me.mnuEditSelectRectangle.Text = "&Select Rectangle"
    Me.mnuEditSelectRectangle.ToolTipText = "Select a rectangular area in the photo."
    '
    'mnuEditSelectEllipse
    '
    Me.mnuEditSelectEllipse.Name = "mnuEditSelectEllipse"
    Me.mnuEditSelectEllipse.Size = New System.Drawing.Size(285, 26)
    Me.mnuEditSelectEllipse.Text = "Select &Ellipse"
    Me.mnuEditSelectEllipse.ToolTipText = "Select an elliptical area in the photo."
    '
    'mnuEditSelectFreehand
    '
    Me.mnuEditSelectFreehand.Name = "mnuEditSelectFreehand"
    Me.mnuEditSelectFreehand.Size = New System.Drawing.Size(285, 26)
    Me.mnuEditSelectFreehand.Text = "Free&hand Selection"
    Me.mnuEditSelectFreehand.ToolTipText = "Select an area inside a line you draw."
    '
    'ToolStripSeparator19
    '
    Me.ToolStripSeparator19.Name = "ToolStripSeparator19"
    Me.ToolStripSeparator19.Size = New System.Drawing.Size(282, 6)
    '
    'mnuEditSelectSimilar
    '
    Me.mnuEditSelectSimilar.Name = "mnuEditSelectSimilar"
    Me.mnuEditSelectSimilar.Size = New System.Drawing.Size(285, 26)
    Me.mnuEditSelectSimilar.Text = "Se&lect Similar Colors"
    Me.mnuEditSelectSimilar.ToolTipText = "Select an area in the photo that has similar colors to a designated point."
    '
    'mnuEditInvertSelection
    '
    Me.mnuEditInvertSelection.Name = "mnuEditInvertSelection"
    Me.mnuEditInvertSelection.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
    Me.mnuEditInvertSelection.Size = New System.Drawing.Size(285, 26)
    Me.mnuEditInvertSelection.Text = "In&vert Selection"
    Me.mnuEditInvertSelection.ToolTipText = "Select the area in the photo that is not currently selected, and deselect the res" & _
    "t."
    '
    'editline3
    '
    Me.editline3.Name = "editline3"
    Me.editline3.Size = New System.Drawing.Size(282, 6)
    '
    'mnuEditInsertPage
    '
    Me.mnuEditInsertPage.Name = "mnuEditInsertPage"
    Me.mnuEditInsertPage.Size = New System.Drawing.Size(285, 26)
    Me.mnuEditInsertPage.Text = "Insert Page"
    Me.mnuEditInsertPage.ToolTipText = "Add a page to the current image."
    '
    'mnuEditDeletePage
    '
    Me.mnuEditDeletePage.Name = "mnuEditDeletePage"
    Me.mnuEditDeletePage.Size = New System.Drawing.Size(285, 26)
    Me.mnuEditDeletePage.Text = "Delete Page"
    Me.mnuEditDeletePage.ToolTipText = "Remove a page from the current image."
    '
    'mnuImage
    '
    Me.mnuImage.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuImageResize, Me.mnuImageExpand, Me.mnuImageLine2, Me.mnuImageRotate, Me.mnuImageFlipHoriz, Me.mnuImageFlipVert, Me.mnuImageStretch, Me.mnuImageAlign, Me.ToolStripSeparator5, Me.mnuImageBlur, Me.mnuImageContrastStretch, Me.mnuImageSharpen, Me.mnuImageMedian, Me.mnuImageEdgeDetect, Me.mnuImageRedeye, Me.ToolStripSeparator21, Me.mnuImageEffects, Me.mnuImageArtEffects, Me.mnuImageBorder, Me.ToolStripSeparator6, Me.mnuImageSetCrop, Me.mnuImageCrop})
    Me.mnuImage.MergeIndex = 2
    Me.mnuImage.Name = "mnuImage"
    Me.mnuImage.Size = New System.Drawing.Size(63, 24)
    Me.mnuImage.Text = "&Image"
    '
    'mnuImageResize
    '
    Me.mnuImageResize.Name = "mnuImageResize"
    Me.mnuImageResize.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
    Me.mnuImageResize.Size = New System.Drawing.Size(279, 26)
    Me.mnuImageResize.Text = "Resi&ze..."
    Me.mnuImageResize.ToolTipText = "Change the size of the photo."
    '
    'mnuImageExpand
    '
    Me.mnuImageExpand.Name = "mnuImageExpand"
    Me.mnuImageExpand.Size = New System.Drawing.Size(279, 26)
    Me.mnuImageExpand.Text = "E&xpand..."
    Me.mnuImageExpand.ToolTipText = "Expand the area of a photo by added blank space."
    '
    'mnuImageLine2
    '
    Me.mnuImageLine2.Name = "mnuImageLine2"
    Me.mnuImageLine2.Size = New System.Drawing.Size(276, 6)
    '
    'mnuImageRotate
    '
    Me.mnuImageRotate.Name = "mnuImageRotate"
    Me.mnuImageRotate.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
    Me.mnuImageRotate.Size = New System.Drawing.Size(279, 26)
    Me.mnuImageRotate.Text = "&Rotate..."
    Me.mnuImageRotate.ToolTipText = "Rotate the photo."
    '
    'mnuImageFlipHoriz
    '
    Me.mnuImageFlipHoriz.Name = "mnuImageFlipHoriz"
    Me.mnuImageFlipHoriz.Size = New System.Drawing.Size(279, 26)
    Me.mnuImageFlipHoriz.Text = "Flip &Horizontally"
    Me.mnuImageFlipHoriz.ToolTipText = "Flip the photo horizontally to make a mirror image."
    '
    'mnuImageFlipVert
    '
    Me.mnuImageFlipVert.Name = "mnuImageFlipVert"
    Me.mnuImageFlipVert.Size = New System.Drawing.Size(279, 26)
    Me.mnuImageFlipVert.Text = "Flip &Vertically"
    Me.mnuImageFlipVert.ToolTipText = "Flip the photo vertically to make a mirror image."
    '
    'mnuImageStretch
    '
    Me.mnuImageStretch.Name = "mnuImageStretch"
    Me.mnuImageStretch.Size = New System.Drawing.Size(279, 26)
    Me.mnuImageStretch.Text = "Stret&ch"
    Me.mnuImageStretch.ToolTipText = "Stretch a photo by picking four points for its corners."
    '
    'mnuImageAlign
    '
    Me.mnuImageAlign.Name = "mnuImageAlign"
    Me.mnuImageAlign.Size = New System.Drawing.Size(279, 26)
    Me.mnuImageAlign.Text = "Ali&gn"
    '
    'ToolStripSeparator5
    '
    Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
    Me.ToolStripSeparator5.Size = New System.Drawing.Size(276, 6)
    '
    'mnuImageBlur
    '
    Me.mnuImageBlur.Name = "mnuImageBlur"
    Me.mnuImageBlur.Size = New System.Drawing.Size(279, 26)
    Me.mnuImageBlur.Text = "&Blur..."
    Me.mnuImageBlur.ToolTipText = "Blur a photo."
    '
    'mnuImageContrastStretch
    '
    Me.mnuImageContrastStretch.Name = "mnuImageContrastStretch"
    Me.mnuImageContrastStretch.Size = New System.Drawing.Size(279, 26)
    Me.mnuImageContrastStretch.Text = "Contras&t Stretch..."
    '
    'mnuImageSharpen
    '
    Me.mnuImageSharpen.Name = "mnuImageSharpen"
    Me.mnuImageSharpen.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U), System.Windows.Forms.Keys)
    Me.mnuImageSharpen.Size = New System.Drawing.Size(279, 26)
    Me.mnuImageSharpen.Text = "&Sharpen..."
    Me.mnuImageSharpen.ToolTipText = "Sharpen a photo."
    '
    'mnuImageMedian
    '
    Me.mnuImageMedian.Name = "mnuImageMedian"
    Me.mnuImageMedian.Size = New System.Drawing.Size(279, 26)
    Me.mnuImageMedian.Text = "&Median Filter Noise Removal..."
    Me.mnuImageMedian.ToolTipText = "Remove noise using a Median Filter."
    '
    'mnuImageEdgeDetect
    '
    Me.mnuImageEdgeDetect.Name = "mnuImageEdgeDetect"
    Me.mnuImageEdgeDetect.Size = New System.Drawing.Size(279, 26)
    Me.mnuImageEdgeDetect.Text = "Edge &Detection..."
    Me.mnuImageEdgeDetect.ToolTipText = "Detect edges in the photo using a selection of methods."
    '
    'mnuImageRedeye
    '
    Me.mnuImageRedeye.Name = "mnuImageRedeye"
    Me.mnuImageRedeye.Size = New System.Drawing.Size(279, 26)
    Me.mnuImageRedeye.Text = "Red E&ye Removal..."
    Me.mnuImageRedeye.ToolTipText = "Remove red eyes resulting from a flash photo."
    '
    'ToolStripSeparator21
    '
    Me.ToolStripSeparator21.Name = "ToolStripSeparator21"
    Me.ToolStripSeparator21.Size = New System.Drawing.Size(276, 6)
    '
    'mnuImageEffects
    '
    Me.mnuImageEffects.Name = "mnuImageEffects"
    Me.mnuImageEffects.Size = New System.Drawing.Size(279, 26)
    Me.mnuImageEffects.Text = "Special &Effects..."
    Me.mnuImageEffects.ToolTipText = "Apply selected special effects to a photo."
    '
    'mnuImageArtEffects
    '
    Me.mnuImageArtEffects.Name = "mnuImageArtEffects"
    Me.mnuImageArtEffects.Size = New System.Drawing.Size(279, 26)
    Me.mnuImageArtEffects.Text = "&Artistic Effects..."
    Me.mnuImageArtEffects.ToolTipText = "Apply selected artistic effects to a photo."
    '
    'mnuImageBorder
    '
    Me.mnuImageBorder.Name = "mnuImageBorder"
    Me.mnuImageBorder.Size = New System.Drawing.Size(279, 26)
    Me.mnuImageBorder.Text = "B&order..."
    Me.mnuImageBorder.ToolTipText = "Add a border to a photo."
    '
    'ToolStripSeparator6
    '
    Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
    Me.ToolStripSeparator6.Size = New System.Drawing.Size(276, 6)
    '
    'mnuImageSetCrop
    '
    Me.mnuImageSetCrop.Name = "mnuImageSetCrop"
    Me.mnuImageSetCrop.Size = New System.Drawing.Size(279, 26)
    Me.mnuImageSetCrop.Text = "Set Cro&pping Area"
    Me.mnuImageSetCrop.ToolTipText = "Select an area in the photo to be cropped."
    '
    'mnuImageCrop
    '
    Me.mnuImageCrop.Name = "mnuImageCrop"
    Me.mnuImageCrop.Size = New System.Drawing.Size(279, 26)
    Me.mnuImageCrop.Text = "&Crop"
    Me.mnuImageCrop.ToolTipText = "Finalize the cropping operation."
    '
    'mnuColor
    '
    Me.mnuColor.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColorEnhance, Me.ToolStripSeparator18, Me.mnuColorBrightness, Me.mnuColorAdjust, Me.mnuColorHisto, Me.ToolStripSeparator7, Me.mnuImageLightAmp, Me.ToolStripSeparator8, Me.mnuColorHalftone, Me.mnuColorGrayscale, Me.mnuColorNegative, Me.mnuColorSepia})
    Me.mnuColor.MergeIndex = 3
    Me.mnuColor.Name = "mnuColor"
    Me.mnuColor.Size = New System.Drawing.Size(52, 24)
    Me.mnuColor.Text = "&Color"
    '
    'mnuColorEnhance
    '
    Me.mnuColorEnhance.Name = "mnuColorEnhance"
    Me.mnuColorEnhance.Size = New System.Drawing.Size(338, 26)
    Me.mnuColorEnhance.Text = "&Auto Enhance"
    Me.mnuColorEnhance.ToolTipText = "Automatically perform a color and contrast correction."
    '
    'ToolStripSeparator18
    '
    Me.ToolStripSeparator18.Name = "ToolStripSeparator18"
    Me.ToolStripSeparator18.Size = New System.Drawing.Size(335, 6)
    '
    'mnuColorBrightness
    '
    Me.mnuColorBrightness.Name = "mnuColorBrightness"
    Me.mnuColorBrightness.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
    Me.mnuColorBrightness.Size = New System.Drawing.Size(338, 26)
    Me.mnuColorBrightness.Text = "&Brightness, Contrast, Saturation"
    Me.mnuColorBrightness.ToolTipText = "Adjust the Brightness, Contrast, and Saturation of a photo."
    '
    'mnuColorAdjust
    '
    Me.mnuColorAdjust.Name = "mnuColorAdjust"
    Me.mnuColorAdjust.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.J), System.Windows.Forms.Keys)
    Me.mnuColorAdjust.Size = New System.Drawing.Size(338, 26)
    Me.mnuColorAdjust.Text = "&Color Adjustment..."
    Me.mnuColorAdjust.ToolTipText = "Adjust the red, green, and blue values for shadow, midtones, and highlights."
    '
    'mnuColorHisto
    '
    Me.mnuColorHisto.Name = "mnuColorHisto"
    Me.mnuColorHisto.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.H), System.Windows.Forms.Keys)
    Me.mnuColorHisto.Size = New System.Drawing.Size(338, 26)
    Me.mnuColorHisto.Text = "&Histogram Adjustment..."
    Me.mnuColorHisto.ToolTipText = "Perform a variety of color and intensity adjustments on a photo."
    '
    'ToolStripSeparator7
    '
    Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
    Me.ToolStripSeparator7.Size = New System.Drawing.Size(335, 6)
    '
    'mnuImageLightAmp
    '
    Me.mnuImageLightAmp.Name = "mnuImageLightAmp"
    Me.mnuImageLightAmp.Size = New System.Drawing.Size(338, 26)
    Me.mnuImageLightAmp.Text = "Light Am&plifier..."
    Me.mnuImageLightAmp.ToolTipText = "Multiplies the intensity of an photo."
    '
    'ToolStripSeparator8
    '
    Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
    Me.ToolStripSeparator8.Size = New System.Drawing.Size(335, 6)
    '
    'mnuColorHalftone
    '
    Me.mnuColorHalftone.Name = "mnuColorHalftone"
    Me.mnuColorHalftone.Size = New System.Drawing.Size(338, 26)
    Me.mnuColorHalftone.Text = "Hal&ftone..."
    Me.mnuColorHalftone.ToolTipText = "Converts a photo to a halftone image."
    '
    'mnuColorGrayscale
    '
    Me.mnuColorGrayscale.Name = "mnuColorGrayscale"
    Me.mnuColorGrayscale.Size = New System.Drawing.Size(338, 26)
    Me.mnuColorGrayscale.Text = "Gra&yscale"
    Me.mnuColorGrayscale.ToolTipText = "Converts a photo from color to grayscale."
    '
    'mnuColorNegative
    '
    Me.mnuColorNegative.Name = "mnuColorNegative"
    Me.mnuColorNegative.Size = New System.Drawing.Size(338, 26)
    Me.mnuColorNegative.Text = "&Negative"
    Me.mnuColorNegative.ToolTipText = "Makes a negative image of a photo."
    '
    'mnuColorSepia
    '
    Me.mnuColorSepia.Name = "mnuColorSepia"
    Me.mnuColorSepia.Size = New System.Drawing.Size(338, 26)
    Me.mnuColorSepia.Text = "&Sepia"
    Me.mnuColorSepia.ToolTipText = "Converts a photo to sepia, making it look antique."
    '
    'mnuDraw
    '
    Me.mnuDraw.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDrawText, Me.mnuDrawLine, Me.mnuDrawCurve, Me.mnuDrawArrow, Me.mnuDrawCircle, Me.mnuDrawEllipse, Me.mnuDrawBox, Me.ToolStripSeparator9, Me.mnuDrawLineOptions, Me.ToolStripSeparator10, Me.mnuDrawSketch, Me.ToolStripSeparator11, Me.mnuDrawFill, Me.mnuDrawFillSelection, Me.drawline2, Me.mnuDrawColor, Me.mnuDrawBackColor, Me.mnuColorSample})
    Me.mnuDraw.MergeIndex = 4
    Me.mnuDraw.Name = "mnuDraw"
    Me.mnuDraw.Size = New System.Drawing.Size(53, 24)
    Me.mnuDraw.Text = "&Draw"
    '
    'mnuDrawText
    '
    Me.mnuDrawText.Name = "mnuDrawText"
    Me.mnuDrawText.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T), System.Windows.Forms.Keys)
    Me.mnuDrawText.Size = New System.Drawing.Size(231, 26)
    Me.mnuDrawText.Text = "&Text"
    Me.mnuDrawText.ToolTipText = "Add text to the current photo."
    '
    'mnuDrawLine
    '
    Me.mnuDrawLine.Name = "mnuDrawLine"
    Me.mnuDrawLine.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
    Me.mnuDrawLine.Size = New System.Drawing.Size(231, 26)
    Me.mnuDrawLine.Text = "&Line"
    Me.mnuDrawLine.ToolTipText = "Draw a line on the current photo."
    '
    'mnuDrawCurve
    '
    Me.mnuDrawCurve.Name = "mnuDrawCurve"
    Me.mnuDrawCurve.Size = New System.Drawing.Size(231, 26)
    Me.mnuDrawCurve.Text = "&Curve"
    Me.mnuDrawCurve.ToolTipText = "Draw a curve on the current photo."
    '
    'mnuDrawArrow
    '
    Me.mnuDrawArrow.Name = "mnuDrawArrow"
    Me.mnuDrawArrow.Size = New System.Drawing.Size(231, 26)
    Me.mnuDrawArrow.Text = "&Arrow"
    Me.mnuDrawArrow.ToolTipText = "Draw an arrow on the current photo."
    '
    'mnuDrawCircle
    '
    Me.mnuDrawCircle.Name = "mnuDrawCircle"
    Me.mnuDrawCircle.Size = New System.Drawing.Size(231, 26)
    Me.mnuDrawCircle.Text = "Ci&rcle"
    Me.mnuDrawCircle.ToolTipText = "Draw a circle on the current photo."
    '
    'mnuDrawEllipse
    '
    Me.mnuDrawEllipse.Name = "mnuDrawEllipse"
    Me.mnuDrawEllipse.Size = New System.Drawing.Size(231, 26)
    Me.mnuDrawEllipse.Text = "&Ellipse"
    '
    'mnuDrawBox
    '
    Me.mnuDrawBox.Name = "mnuDrawBox"
    Me.mnuDrawBox.Size = New System.Drawing.Size(231, 26)
    Me.mnuDrawBox.Text = "Bo&x"
    Me.mnuDrawBox.ToolTipText = "Draw a rectangle on the current photo."
    '
    'ToolStripSeparator9
    '
    Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
    Me.ToolStripSeparator9.Size = New System.Drawing.Size(228, 6)
    '
    'mnuDrawLineOptions
    '
    Me.mnuDrawLineOptions.Name = "mnuDrawLineOptions"
    Me.mnuDrawLineOptions.Size = New System.Drawing.Size(231, 26)
    Me.mnuDrawLineOptions.Text = "Line &Options..."
    Me.mnuDrawLineOptions.ToolTipText = "Select the line pattern (dashed, dotted, etc.) and thickness."
    '
    'ToolStripSeparator10
    '
    Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
    Me.ToolStripSeparator10.Size = New System.Drawing.Size(228, 6)
    '
    'mnuDrawSketch
    '
    Me.mnuDrawSketch.Name = "mnuDrawSketch"
    Me.mnuDrawSketch.Size = New System.Drawing.Size(231, 26)
    Me.mnuDrawSketch.Text = "S&ketch"
    Me.mnuDrawSketch.ToolTipText = "Draw a freehand line on the current photo."
    '
    'ToolStripSeparator11
    '
    Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
    Me.ToolStripSeparator11.Size = New System.Drawing.Size(228, 6)
    '
    'mnuDrawFill
    '
    Me.mnuDrawFill.Name = "mnuDrawFill"
    Me.mnuDrawFill.Size = New System.Drawing.Size(231, 26)
    Me.mnuDrawFill.Text = "&Fill Area of Similar Color"
    Me.mnuDrawFill.ToolTipText = "Fill an area in the photo that has similar colors to a designated point."
    '
    'mnuDrawFillSelection
    '
    Me.mnuDrawFillSelection.Name = "mnuDrawFillSelection"
    Me.mnuDrawFillSelection.Size = New System.Drawing.Size(231, 26)
    Me.mnuDrawFillSelection.Text = "F&ill Selected Area"
    Me.mnuDrawFillSelection.ToolTipText = "Fill an the selected area in the photo with a solid color."
    '
    'drawline2
    '
    Me.drawline2.Name = "drawline2"
    Me.drawline2.Size = New System.Drawing.Size(228, 6)
    '
    'mnuDrawColor
    '
    Me.mnuDrawColor.Name = "mnuDrawColor"
    Me.mnuDrawColor.Size = New System.Drawing.Size(231, 26)
    Me.mnuDrawColor.Text = "&Drawing Color..."
    Me.mnuDrawColor.ToolTipText = "Select the drawing (foreground) color."
    '
    'mnuDrawBackColor
    '
    Me.mnuDrawBackColor.Name = "mnuDrawBackColor"
    Me.mnuDrawBackColor.Size = New System.Drawing.Size(231, 26)
    Me.mnuDrawBackColor.Text = "&Background Color..."
    Me.mnuDrawBackColor.ToolTipText = "Select the background color."
    '
    'mnuColorSample
    '
    Me.mnuColorSample.Name = "mnuColorSample"
    Me.mnuColorSample.Size = New System.Drawing.Size(231, 26)
    Me.mnuColorSample.Text = "Color Sam&ple"
    Me.mnuColorSample.ToolTipText = "Get the color of a point in the photo."
    '
    'mnuTools
    '
    Me.mnuTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuToolsInfo, Me.mnuToolsComment, Me.mnuToolsBugPhotos, Me.mnuToolsMeasure, Me.ToolStripSeparator13, Me.mnuToolsSearch, Me.mnuToolsPicSearch, Me.ToolStripSeparator12, Me.mnuToolsCombine, Me.mnuToolsConcatenate, Me.mnuToolsPicturize, Me.mnuToolsFilter, Me.toolsline1, Me.mnuToolsAssoc, Me.mnuToolsFileFilter, Me.mnuToolsWallpaper, Me.ToolStripSeparator14, Me.mnuToolsToolbar, Me.mnuToolsOptions})
    Me.mnuTools.Name = "mnuTools"
    Me.mnuTools.Size = New System.Drawing.Size(55, 24)
    Me.mnuTools.Text = "&Tools"
    '
    'mnuToolsInfo
    '
    Me.mnuToolsInfo.Name = "mnuToolsInfo"
    Me.mnuToolsInfo.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
    Me.mnuToolsInfo.Size = New System.Drawing.Size(261, 26)
    Me.mnuToolsInfo.Text = "Photo &Information..."
    Me.mnuToolsInfo.ToolTipText = "Display information about a photo, including comments and camera settings."
    '
    'mnuToolsComment
    '
    Me.mnuToolsComment.Name = "mnuToolsComment"
    Me.mnuToolsComment.Size = New System.Drawing.Size(261, 26)
    Me.mnuToolsComment.Text = "Enter Photo &Comments..."
    Me.mnuToolsComment.ToolTipText = "Enter photo comments to be saved with the file."
    '
    'mnuToolsBugPhotos
    '
    Me.mnuToolsBugPhotos.Name = "mnuToolsBugPhotos"
    Me.mnuToolsBugPhotos.Size = New System.Drawing.Size(261, 26)
    Me.mnuToolsBugPhotos.Text = "&Bug Data Entry"
    '
    'mnuToolsMeasure
    '
    Me.mnuToolsMeasure.Name = "mnuToolsMeasure"
    Me.mnuToolsMeasure.Size = New System.Drawing.Size(261, 26)
    Me.mnuToolsMeasure.Text = "M&easure Distance"
    '
    'ToolStripSeparator13
    '
    Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
    Me.ToolStripSeparator13.Size = New System.Drawing.Size(258, 6)
    '
    'mnuToolsSearch
    '
    Me.mnuToolsSearch.Name = "mnuToolsSearch"
    Me.mnuToolsSearch.Size = New System.Drawing.Size(261, 26)
    Me.mnuToolsSearch.Text = "Search P&hoto Comments..."
    '
    'mnuToolsPicSearch
    '
    Me.mnuToolsPicSearch.Name = "mnuToolsPicSearch"
    Me.mnuToolsPicSearch.Size = New System.Drawing.Size(261, 26)
    Me.mnuToolsPicSearch.Text = "Search for &Duplicates..."
    Me.mnuToolsPicSearch.ToolTipText = "Search for duplicate photos on the hard drive."
    '
    'ToolStripSeparator12
    '
    Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
    Me.ToolStripSeparator12.Size = New System.Drawing.Size(258, 6)
    '
    'mnuToolsCombine
    '
    Me.mnuToolsCombine.Name = "mnuToolsCombine"
    Me.mnuToolsCombine.Size = New System.Drawing.Size(261, 26)
    Me.mnuToolsCombine.Text = "Co&mbine Photos..."
    Me.mnuToolsCombine.ToolTipText = "Combine two photos."
    '
    'mnuToolsConcatenate
    '
    Me.mnuToolsConcatenate.Name = "mnuToolsConcatenate"
    Me.mnuToolsConcatenate.Size = New System.Drawing.Size(261, 26)
    Me.mnuToolsConcatenate.Text = "Co&ncatenate Photos..."
    '
    'mnuToolsPicturize
    '
    Me.mnuToolsPicturize.Name = "mnuToolsPicturize"
    Me.mnuToolsPicturize.Size = New System.Drawing.Size(261, 26)
    Me.mnuToolsPicturize.Text = "&Photo Mosaic..."
    Me.mnuToolsPicturize.ToolTipText = "Create a photo mosaic made up of many small individual photos."
    '
    'mnuToolsFilter
    '
    Me.mnuToolsFilter.Name = "mnuToolsFilter"
    Me.mnuToolsFilter.Size = New System.Drawing.Size(261, 26)
    Me.mnuToolsFilter.Text = "&Filter Workshop..."
    Me.mnuToolsFilter.ToolTipText = "Apply custom image filters to a photo."
    '
    'toolsline1
    '
    Me.toolsline1.Name = "toolsline1"
    Me.toolsline1.Size = New System.Drawing.Size(258, 6)
    '
    'mnuToolsAssoc
    '
    Me.mnuToolsAssoc.Name = "mnuToolsAssoc"
    Me.mnuToolsAssoc.Size = New System.Drawing.Size(261, 26)
    Me.mnuToolsAssoc.Text = "&Associate File Types..."
    Me.mnuToolsAssoc.ToolTipText = "Select file types to be associated with Photo Mud."
    '
    'mnuToolsFileFilter
    '
    Me.mnuToolsFileFilter.Name = "mnuToolsFileFilter"
    Me.mnuToolsFileFilter.Size = New System.Drawing.Size(261, 26)
    Me.mnuToolsFileFilter.Text = "Select Photo File T&ypes..."
    Me.mnuToolsFileFilter.ToolTipText = "Select the file types to be displayed in dialogs and Photo Mud Explorer"
    '
    'mnuToolsWallpaper
    '
    Me.mnuToolsWallpaper.Name = "mnuToolsWallpaper"
    Me.mnuToolsWallpaper.Size = New System.Drawing.Size(261, 26)
    Me.mnuToolsWallpaper.Text = "Set as &Wallpaper"
    Me.mnuToolsWallpaper.ToolTipText = "Set the current photo as Windows Wallpaper."
    '
    'ToolStripSeparator14
    '
    Me.ToolStripSeparator14.Name = "ToolStripSeparator14"
    Me.ToolStripSeparator14.Size = New System.Drawing.Size(258, 6)
    '
    'mnuToolsToolbar
    '
    Me.mnuToolsToolbar.Name = "mnuToolsToolbar"
    Me.mnuToolsToolbar.Size = New System.Drawing.Size(261, 26)
    Me.mnuToolsToolbar.Text = "Customi&ze Toolbar..."
    Me.mnuToolsToolbar.ToolTipText = "Select tools for the toolbar, and other toolbar options."
    '
    'mnuToolsOptions
    '
    Me.mnuToolsOptions.Name = "mnuToolsOptions"
    Me.mnuToolsOptions.Size = New System.Drawing.Size(261, 26)
    Me.mnuToolsOptions.Text = "&Options..."
    Me.mnuToolsOptions.ToolTipText = "Set Photo Mud options and preferences."
    '
    'mnuview
    '
    Me.mnuview.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuViewZoomIn, Me.mnuViewZoomOut, Me.mnuViewZoomWindow, Me.viewline3, Me.mnuViewFullscreen, Me.mnuViewToolbar, Me.mnuViewRefresh, Me.viewline4, Me.mnuViewZoomFit, Me.mnuViewZoom25, Me.mnuViewZoom50, Me.mnuViewZoom100, Me.mnuViewZoom200, Me.ToolStripSeparator22, Me.mnuViewNext})
    Me.mnuview.Name = "mnuview"
    Me.mnuview.Size = New System.Drawing.Size(49, 24)
    Me.mnuview.Text = "&View"
    '
    'mnuViewZoomIn
    '
    Me.mnuViewZoomIn.Name = "mnuViewZoomIn"
    Me.mnuViewZoomIn.ShortcutKeyDisplayString = "+"
    Me.mnuViewZoomIn.Size = New System.Drawing.Size(207, 26)
    Me.mnuViewZoomIn.Text = "Zoom &In"
    Me.mnuViewZoomIn.ToolTipText = "Zoom in on the current photo."
    '
    'mnuViewZoomOut
    '
    Me.mnuViewZoomOut.Name = "mnuViewZoomOut"
    Me.mnuViewZoomOut.ShortcutKeyDisplayString = "-"
    Me.mnuViewZoomOut.Size = New System.Drawing.Size(207, 26)
    Me.mnuViewZoomOut.Text = "Zoom &Out"
    Me.mnuViewZoomOut.ToolTipText = "Zoom out on the current photo."
    '
    'mnuViewZoomWindow
    '
    Me.mnuViewZoomWindow.Name = "mnuViewZoomWindow"
    Me.mnuViewZoomWindow.ShortcutKeyDisplayString = "Z"
    Me.mnuViewZoomWindow.Size = New System.Drawing.Size(207, 26)
    Me.mnuViewZoomWindow.Text = "&Zoom Window"
    Me.mnuViewZoomWindow.ToolTipText = "Zoom into a rectangle dragged on the photo."
    '
    'viewline3
    '
    Me.viewline3.Name = "viewline3"
    Me.viewline3.Size = New System.Drawing.Size(204, 6)
    '
    'mnuViewFullscreen
    '
    Me.mnuViewFullscreen.Name = "mnuViewFullscreen"
    Me.mnuViewFullscreen.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
    Me.mnuViewFullscreen.Size = New System.Drawing.Size(207, 26)
    Me.mnuViewFullscreen.Text = "&Full Screen"
    Me.mnuViewFullscreen.ToolTipText = "View the current photo on the full screen."
    '
    'mnuViewToolbar
    '
    Me.mnuViewToolbar.Checked = True
    Me.mnuViewToolbar.CheckState = System.Windows.Forms.CheckState.Checked
    Me.mnuViewToolbar.MergeAction = System.Windows.Forms.MergeAction.Replace
    Me.mnuViewToolbar.Name = "mnuViewToolbar"
    Me.mnuViewToolbar.Size = New System.Drawing.Size(207, 26)
    Me.mnuViewToolbar.Text = "&Show Toolbar"
    Me.mnuViewToolbar.ToolTipText = "Show or hide the toolbar."
    '
    'mnuViewRefresh
    '
    Me.mnuViewRefresh.Name = "mnuViewRefresh"
    Me.mnuViewRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5
    Me.mnuViewRefresh.Size = New System.Drawing.Size(207, 26)
    Me.mnuViewRefresh.Text = "&Refresh"
    Me.mnuViewRefresh.ToolTipText = "Refresh the screen."
    '
    'viewline4
    '
    Me.viewline4.Name = "viewline4"
    Me.viewline4.Size = New System.Drawing.Size(204, 6)
    '
    'mnuViewZoomFit
    '
    Me.mnuViewZoomFit.Name = "mnuViewZoomFit"
    Me.mnuViewZoomFit.Size = New System.Drawing.Size(207, 26)
    Me.mnuViewZoomFit.Text = "Fit to &Window"
    Me.mnuViewZoomFit.ToolTipText = "Zoom the photo so it fits in the window at its maximum size."
    '
    'mnuViewZoom25
    '
    Me.mnuViewZoom25.Name = "mnuViewZoom25"
    Me.mnuViewZoom25.Size = New System.Drawing.Size(207, 26)
    Me.mnuViewZoom25.Text = "25% Zoo&m"
    Me.mnuViewZoom25.ToolTipText = "Show the photo at 25% zoom (1/4 size)."
    '
    'mnuViewZoom50
    '
    Me.mnuViewZoom50.Name = "mnuViewZoom50"
    Me.mnuViewZoom50.Size = New System.Drawing.Size(207, 26)
    Me.mnuViewZoom50.Text = "&50% Zoom"
    Me.mnuViewZoom50.ToolTipText = "Show the photo at 50% zoom (1/2 size)."
    '
    'mnuViewZoom100
    '
    Me.mnuViewZoom100.Name = "mnuViewZoom100"
    Me.mnuViewZoom100.Size = New System.Drawing.Size(207, 26)
    Me.mnuViewZoom100.Text = "&100% Zoom"
    Me.mnuViewZoom100.ToolTipText = "Show the photo at 100% zoom (1 photo pixel = 1 screen pixel)."
    '
    'mnuViewZoom200
    '
    Me.mnuViewZoom200.Name = "mnuViewZoom200"
    Me.mnuViewZoom200.Size = New System.Drawing.Size(207, 26)
    Me.mnuViewZoom200.Text = "&200%  Zoom"
    Me.mnuViewZoom200.ToolTipText = "Show the photo at 200% zoom. (1 photo pixel = 2 screen pixels)."
    '
    'ToolStripSeparator22
    '
    Me.ToolStripSeparator22.Name = "ToolStripSeparator22"
    Me.ToolStripSeparator22.Size = New System.Drawing.Size(204, 6)
    '
    'mnuViewNext
    '
    Me.mnuViewNext.Name = "mnuViewNext"
    Me.mnuViewNext.ShortcutKeyDisplayString = "Ctrl-Tab"
    Me.mnuViewNext.Size = New System.Drawing.Size(207, 26)
    Me.mnuViewNext.Text = "View &Next"
    Me.mnuViewNext.ToolTipText = "View the next open photo."
    '
    'mnuHelp
    '
    Me.mnuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuHelpHelpTopics, Me.mnuHelpHelpIndex, Me.mnuHelpTips, Me.helpLine1, Me.mnuHelpRegister, Me.mnuHelpUpdate, Me.mnuHelpAbout})
    Me.mnuHelp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.mnuHelp.Name = "mnuHelp"
    Me.mnuHelp.Size = New System.Drawing.Size(48, 24)
    Me.mnuHelp.Text = "&Help"
    '
    'mnuHelpHelpTopics
    '
    Me.mnuHelpHelpTopics.Name = "mnuHelpHelpTopics"
    Me.mnuHelpHelpTopics.ShortcutKeys = System.Windows.Forms.Keys.F1
    Me.mnuHelpHelpTopics.Size = New System.Drawing.Size(253, 26)
    Me.mnuHelpHelpTopics.Text = "&Help"
    Me.mnuHelpHelpTopics.ToolTipText = "Show the Help Contents."
    '
    'mnuHelpHelpIndex
    '
    Me.mnuHelpHelpIndex.Name = "mnuHelpHelpIndex"
    Me.mnuHelpHelpIndex.Size = New System.Drawing.Size(253, 26)
    Me.mnuHelpHelpIndex.Text = "Help Inde&x"
    Me.mnuHelpHelpIndex.ToolTipText = "Show the Help Index."
    '
    'mnuHelpTips
    '
    Me.mnuHelpTips.Name = "mnuHelpTips"
    Me.mnuHelpTips.Size = New System.Drawing.Size(253, 26)
    Me.mnuHelpTips.Text = "Show Photo Mud &Tips"
    Me.mnuHelpTips.ToolTipText = "Show the Photo Mud Tips."
    '
    'helpLine1
    '
    Me.helpLine1.Name = "helpLine1"
    Me.helpLine1.Size = New System.Drawing.Size(250, 6)
    '
    'mnuHelpRegister
    '
    Me.mnuHelpRegister.Name = "mnuHelpRegister"
    Me.mnuHelpRegister.Size = New System.Drawing.Size(253, 26)
    Me.mnuHelpRegister.Text = "&Register Photo Mud Online"
    Me.mnuHelpRegister.ToolTipText = "Go to the Photo Mud web site and register the program."
    '
    'mnuHelpUpdate
    '
    Me.mnuHelpUpdate.Name = "mnuHelpUpdate"
    Me.mnuHelpUpdate.Size = New System.Drawing.Size(253, 26)
    Me.mnuHelpUpdate.Text = "Check for &Updates Online"
    Me.mnuHelpUpdate.ToolTipText = "Go to the Photo Mud web site and check for updates."
    '
    'mnuHelpAbout
    '
    Me.mnuHelpAbout.Name = "mnuHelpAbout"
    Me.mnuHelpAbout.Size = New System.Drawing.Size(253, 26)
    Me.mnuHelpAbout.Text = "&About Photo Mud"
    '
    'ToolStrip1
    '
    Me.ToolStrip1.ContextMenuStrip = Me.mnxT
    Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
    Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
    Me.ToolStrip1.Location = New System.Drawing.Point(0, 28)
    Me.ToolStrip1.Name = "ToolStrip1"
    Me.ToolStrip1.Size = New System.Drawing.Size(990, 25)
    Me.ToolStrip1.TabIndex = 13
    Me.ToolStrip1.Text = "ToolStrip1"
    '
    'mnxT
    '
    Me.mnxT.ImageScalingSize = New System.Drawing.Size(20, 20)
    Me.mnxT.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnxTSmallIcons, Me.mnxTLargeIcons, Me.toolbarline3, Me.mnxTCustomize, Me.mnxTHide, Me.toolbarline2, Me.mnxTHelp})
    Me.mnxT.Name = "ContextMenuStrip1"
    Me.mnxT.Size = New System.Drawing.Size(204, 136)
    '
    'mnxTSmallIcons
    '
    Me.mnxTSmallIcons.CheckOnClick = True
    Me.mnxTSmallIcons.Name = "mnxTSmallIcons"
    Me.mnxTSmallIcons.Size = New System.Drawing.Size(203, 24)
    Me.mnxTSmallIcons.Text = "&Small Icons"
    '
    'mnxTLargeIcons
    '
    Me.mnxTLargeIcons.CheckOnClick = True
    Me.mnxTLargeIcons.Name = "mnxTLargeIcons"
    Me.mnxTLargeIcons.Size = New System.Drawing.Size(203, 24)
    Me.mnxTLargeIcons.Text = "&Large Icons"
    '
    'toolbarline3
    '
    Me.toolbarline3.Name = "toolbarline3"
    Me.toolbarline3.Size = New System.Drawing.Size(200, 6)
    '
    'mnxTCustomize
    '
    Me.mnxTCustomize.Name = "mnxTCustomize"
    Me.mnxTCustomize.Size = New System.Drawing.Size(203, 24)
    Me.mnxTCustomize.Text = "Customi&ze Toolbar"
    '
    'mnxTHide
    '
    Me.mnxTHide.Name = "mnxTHide"
    Me.mnxTHide.Size = New System.Drawing.Size(203, 24)
    Me.mnxTHide.Text = "&Hide Toolbar"
    '
    'toolbarline2
    '
    Me.toolbarline2.Name = "toolbarline2"
    Me.toolbarline2.Size = New System.Drawing.Size(200, 6)
    '
    'mnxTHelp
    '
    Me.mnxTHelp.Name = "mnxTHelp"
    Me.mnxTHelp.Size = New System.Drawing.Size(203, 24)
    Me.mnxTHelp.Text = "&Help"
    '
    'nmPage
    '
    Me.nmPage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.nmPage.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.nmPage.Location = New System.Drawing.Point(333, 0)
    Me.nmPage.Name = "nmPage"
    Me.nmPage.Size = New System.Drawing.Size(44, 23)
    Me.nmPage.TabIndex = 24
    '
    'panelPage
    '
    Me.panelPage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.panelPage.Controls.Add(Me.cmdDeletePage)
    Me.panelPage.Controls.Add(Me.cmdInsertPage)
    Me.panelPage.Controls.Add(Me.cmdAnimate)
    Me.panelPage.Controls.Add(Me.lbPage)
    Me.panelPage.Controls.Add(Me.nmPage)
    Me.panelPage.Location = New System.Drawing.Point(610, 667)
    Me.panelPage.Name = "panelPage"
    Me.panelPage.Size = New System.Drawing.Size(380, 24)
    Me.panelPage.TabIndex = 17
    '
    'lbPage
    '
    Me.lbPage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbPage.AutoSize = True
    Me.lbPage.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.lbPage.Location = New System.Drawing.Point(234, 5)
    Me.lbPage.Name = "lbPage"
    Me.lbPage.Size = New System.Drawing.Size(95, 16)
    Me.lbPage.TabIndex = 23
    Me.lbPage.Text = "Page (of 33): "
    '
    'frmMainf
    '
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.ClientSize = New System.Drawing.Size(990, 691)
    Me.Controls.Add(Me.ToolStrip1)
    Me.Controls.Add(Me.panelPage)
    Me.Controls.Add(Me.StatusStrip1)
    Me.Controls.Add(Me.mnu)
    Me.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(4, 23)
    Me.MainMenuStrip = Me.mnu
    Me.Name = "frmMainf"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Photo Mud Editor"
    Me.mnx.ResumeLayout(False)
    Me.StatusStrip1.ResumeLayout(False)
    Me.StatusStrip1.PerformLayout()
    Me.mnu.ResumeLayout(False)
    Me.mnu.PerformLayout()
    Me.mnxT.ResumeLayout(False)
    CType(Me.nmPage, System.ComponentModel.ISupportInitialize).EndInit()
    Me.panelPage.ResumeLayout(False)
    Me.panelPage.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
  Friend WithEvents lbStatus As System.Windows.Forms.ToolStripStatusLabel

  Public WithEvents mnuDrawColor As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuDrawBackColor As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuDrawLine As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuDrawCurve As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuDrawArrow As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuDrawCircle As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuDrawBox As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuEditCopy As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuEditCut As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuEditSelectRectangle As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuEditSelectEllipse As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuEditSelectFreehand As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuFileSaveAs As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuFileSaveSelection As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuImageFlipHoriz As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuImageFlipVert As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuImageRotate As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents drawline1 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents drawline2 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents editline3 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents fileline5 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents helpline2 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents imageline3 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents mnuColor As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuColorBrightness As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuColorEnhance As ToolStripMenuItem
  Public WithEvents mnuColorGrayscale As ToolStripMenuItem
  Public WithEvents mnuColorHalftone As ToolStripMenuItem
  Public WithEvents mnuColorHisto As ToolStripMenuItem
  Public WithEvents mnuColorLine1 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents mnuColorLine5 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents mnuColorNegative As ToolStripMenuItem
  Public WithEvents mnuColorSample As ToolStripMenuItem
  Public WithEvents mnuDraw As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuDrawFill As ToolStripMenuItem
  Public WithEvents mnuDrawFillSelection As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuDrawLineOptions As ToolStripMenuItem
  Public WithEvents mnuDrawSketch As ToolStripMenuItem
  Public WithEvents mnuDrawText As ToolStripMenuItem
  Public WithEvents mnuEdit As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuEditDeleteFile As ToolStripMenuItem
  Public WithEvents mnuEditDeleteSelection As ToolStripMenuItem
  Public WithEvents mnuEditInvertSelection As ToolStripMenuItem
  Public WithEvents mnueditline1 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents mnuEditLine2 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents mnuEditLine3 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents mnuEditPaste As ToolStripMenuItem
  Public WithEvents mnuEditPasteV As ToolStripMenuItem
  Public WithEvents mnuEditRedo As ToolStripMenuItem
  Public WithEvents mnuEditRepeatCommand As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuEditRevert As ToolStripMenuItem
  Public WithEvents mnuEditSelectSimilar As ToolStripMenuItem
  Public WithEvents mnuEditUndo As ToolStripMenuItem
  Public WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuFileClose As ToolStripMenuItem
  Public WithEvents mnufileline8 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents mnuFilePrint As ToolStripMenuItem
  Public WithEvents mnuFileSave As ToolStripMenuItem
  Public WithEvents mnuFileSend As ToolStripMenuItem
  Public WithEvents mnuImage As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuImageBlur As ToolStripMenuItem
  Public WithEvents mnuImageCrop As ToolStripMenuItem
  Public WithEvents mnuImageEdgeDetect As ToolStripMenuItem
  Public WithEvents mnuImageEffects As ToolStripMenuItem
  Public WithEvents mnuImageExpand As ToolStripMenuItem
  Public WithEvents mnuImageLightAmp As ToolStripMenuItem
  Public WithEvents mnuImageLine2 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents mnuImageMedian As ToolStripMenuItem
  Public WithEvents mnuImageRedeye As ToolStripMenuItem
  Public WithEvents mnuImageResize As ToolStripMenuItem
  Public WithEvents mnuImageSetCrop As ToolStripMenuItem
  Public WithEvents mnuImageSharpen As ToolStripMenuItem
  Public WithEvents mnuImageStretch As ToolStripMenuItem
  Public WithEvents mnuLine4 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents mnuTools As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuToolsAssoc As ToolStripMenuItem
  Public WithEvents mnuToolsCombine As ToolStripMenuItem
  Public WithEvents mnuToolsComment As ToolStripMenuItem
  Public WithEvents mnuToolsFilter As ToolStripMenuItem
  Public WithEvents mnuToolsInfo As ToolStripMenuItem
  Public WithEvents mnuToolsOptions As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuToolsPicSearch As ToolStripMenuItem
  Public WithEvents mnuToolsPicturize As ToolStripMenuItem
  Public WithEvents mnuToolsToolbar As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuToolsWallpaper As ToolStripMenuItem
  Public WithEvents mnuview As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuViewFullscreen As ToolStripMenuItem
  Public WithEvents mnuViewRefresh As ToolStripMenuItem
  Public WithEvents mnuViewToolbar As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuViewZoom100 As ToolStripMenuItem
  Public WithEvents mnuViewZoom200 As ToolStripMenuItem
  Public WithEvents mnuViewZoom25 As ToolStripMenuItem
  Public WithEvents mnuViewZoom50 As ToolStripMenuItem
  Public WithEvents mnuViewZoomFit As ToolStripMenuItem
  Public WithEvents mnuViewZoomIn As ToolStripMenuItem
  Public WithEvents mnuViewZoomOut As ToolStripMenuItem
  Public WithEvents mnuViewZoomWindow As ToolStripMenuItem
  Public WithEvents munViewZoomWindow As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents toolsline1 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents toolsline2 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents viewline3 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents viewline4 As System.Windows.Forms.ToolStripSeparator

  Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ToolStripSeparator14 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents mnuEditPasteNew As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
  Friend WithEvents mnx As System.Windows.Forms.ContextMenuStrip
  Public WithEvents mnxSave As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnxSaveAs As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents mnxPrint As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnxSend As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuContextImageLine1 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents mnxCopy As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnxT As System.Windows.Forms.ContextMenuStrip
  Public WithEvents mnxTSmallIcons As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnxTLargeIcons As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents toolbarline3 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents mnxTCustomize As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnxTHide As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents toolbarline2 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents mnxTHelp As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuFileNew As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuFileOpen As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuFileCloseAll As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator16 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents mnuFileExit As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuFileMru9 As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuFileMru8 As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuFileMru7 As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuFileMru6 As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuFileMru5 As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuFileMru4 As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuFileMru3 As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuFileMru2 As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator17 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents mnuFileMru1 As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuHelp As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuHelpHelpTopics As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuHelpHelpIndex As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuHelpTips As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents helpLine1 As System.Windows.Forms.ToolStripSeparator
  Public WithEvents mnuHelpRegister As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuHelpUpdate As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuHelpAbout As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents bkgDraw As System.ComponentModel.BackgroundWorker
  Friend WithEvents mnuFileExplore As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuColorSepia As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuColorAdjust As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator18 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents nmPage As System.Windows.Forms.NumericUpDown
  Friend WithEvents panelPage As System.Windows.Forms.Panel
  Friend WithEvents lbPage As System.Windows.Forms.Label
  Friend WithEvents cmdAnimate As System.Windows.Forms.Button
  Friend WithEvents cmdDeletePage As System.Windows.Forms.Button
  Friend WithEvents cmdInsertPage As System.Windows.Forms.Button
  Friend WithEvents ToolStripSeparator19 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents mnuEditInsertPage As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuEditDeletePage As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents lbPosition As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents mnxZoom100 As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnxViewFit As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator20 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents mnxClose As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnxZoomIn As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnxZoomOut As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator21 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents mnuImageArtEffects As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuImageBorder As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuToolsSearch As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator22 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents mnuViewNext As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuToolsFileFilter As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuToolsConcatenate As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuToolsMeasure As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuToolsBugPhotos As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuImageContrastStretch As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuDrawEllipse As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuImageAlign As System.Windows.Forms.ToolStripMenuItem
#End Region
End Class