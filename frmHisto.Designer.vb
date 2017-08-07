<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmHisto
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
  Public WithEvents chkFreeForm As CheckBox
  Public WithEvents picRight1 As PictureBox
  Public WithEvents picRight0 As PictureBox
  Public WithEvents picUp1 As PictureBox
  Public WithEvents picUp0 As PictureBox
  Public WithEvents optColorBlue As RadioButton
  Public WithEvents optColorGreen As RadioButton
  Public WithEvents optColorRed As RadioButton
  Public WithEvents optColorAll As RadioButton
  Public WithEvents cmdReset As Button
  Public WithEvents cmdOK As Button
  Public WithEvents cmdCancel As Button
  Public WithEvents Label9 As Label
  Public WithEvents Label7 As Label
  Public WithEvents Label6 As Label
  Public WithEvents Label4 As Label
  Public WithEvents Label2 As Label
  Public WithEvents lbPct3 As Label
  Public WithEvents lbPct2 As Label
  Public WithEvents lbPct1 As Label
  Public WithEvents lbPct0 As Label
  'Public WithEvents lead1 As AxLEADArray
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHisto))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.chkFreeForm = New System.Windows.Forms.CheckBox()
    Me.picRight1 = New System.Windows.Forms.PictureBox()
    Me.picRight0 = New System.Windows.Forms.PictureBox()
    Me.picUp1 = New System.Windows.Forms.PictureBox()
    Me.picUp0 = New System.Windows.Forms.PictureBox()
    Me.optColorBlue = New System.Windows.Forms.RadioButton()
    Me.optColorGreen = New System.Windows.Forms.RadioButton()
    Me.optColorRed = New System.Windows.Forms.RadioButton()
    Me.optColorAll = New System.Windows.Forms.RadioButton()
    Me.cmdReset = New System.Windows.Forms.Button()
    Me.TrackBar1 = New System.Windows.Forms.TrackBar()
    Me.nmSlide3 = New System.Windows.Forms.NumericUpDown()
    Me.nmSlide2 = New System.Windows.Forms.NumericUpDown()
    Me.nmSlide0 = New System.Windows.Forms.NumericUpDown()
    Me.nmSlide1 = New System.Windows.Forms.NumericUpDown()
    Me.nmTrackbar = New System.Windows.Forms.NumericUpDown()
    Me.cmdOK = New System.Windows.Forms.Button()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.lbPct3 = New System.Windows.Forms.Label()
    Me.lbPct2 = New System.Windows.Forms.Label()
    Me.lbPct1 = New System.Windows.Forms.Label()
    Me.lbPct0 = New System.Windows.Forms.Label()
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.aview = New PhotoMud.ctlViewCompare()
    Me.pView2 = New PhotoMud.pViewer()
    Me.pView3 = New PhotoMud.pViewer()
    CType(Me.picRight1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.picRight0, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.picUp1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.picUp0, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmSlide3, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmSlide2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmSlide0, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmSlide1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmTrackbar, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pView2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pView3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'cmdHelp
    '
    Me.cmdHelp.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(919, 438)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 39)
    Me.cmdHelp.TabIndex = 17
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'chkFreeForm
    '
    Me.chkFreeForm.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.chkFreeForm.AutoSize = True
    Me.chkFreeForm.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkFreeForm.Location = New System.Drawing.Point(19, 648)
    Me.chkFreeForm.Name = "chkFreeForm"
    Me.chkFreeForm.Size = New System.Drawing.Size(163, 21)
    Me.chkFreeForm.TabIndex = 7
    Me.chkFreeForm.Text = "Use &Freeform Curve"
    Me.ToolTip1.SetToolTip(Me.chkFreeForm, "Drag a freeform curve in the distribution box to specify the color distribution")
    Me.chkFreeForm.UseVisualStyleBackColor = False
    '
    'picRight1
    '
    Me.picRight1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.picRight1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.picRight1.Image = CType(resources.GetObject("picRight1.Image"), System.Drawing.Image)
    Me.picRight1.InitialImage = CType(resources.GetObject("picRight1.InitialImage"), System.Drawing.Image)
    Me.picRight1.Location = New System.Drawing.Point(261, 471)
    Me.picRight1.Name = "picRight1"
    Me.picRight1.Size = New System.Drawing.Size(24, 24)
    Me.picRight1.TabIndex = 33
    Me.picRight1.TabStop = False
    Me.ToolTip1.SetToolTip(Me.picRight1, "Limit the high (bright) end of the colors")
    '
    'picRight0
    '
    Me.picRight0.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.picRight0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.picRight0.Image = CType(resources.GetObject("picRight0.Image"), System.Drawing.Image)
    Me.picRight0.InitialImage = CType(resources.GetObject("picRight0.InitialImage"), System.Drawing.Image)
    Me.picRight0.Location = New System.Drawing.Point(261, 601)
    Me.picRight0.Name = "picRight0"
    Me.picRight0.Size = New System.Drawing.Size(24, 24)
    Me.picRight0.TabIndex = 32
    Me.picRight0.TabStop = False
    Me.ToolTip1.SetToolTip(Me.picRight0, "Limit the low (dark) end of the colors")
    '
    'picUp1
    '
    Me.picUp1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.picUp1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.picUp1.Image = CType(resources.GetObject("picUp1.Image"), System.Drawing.Image)
    Me.picUp1.InitialImage = CType(resources.GetObject("picUp1.InitialImage"), System.Drawing.Image)
    Me.picUp1.Location = New System.Drawing.Point(552, 637)
    Me.picUp1.Name = "picUp1"
    Me.picUp1.Size = New System.Drawing.Size(24, 24)
    Me.picUp1.TabIndex = 31
    Me.picUp1.TabStop = False
    Me.ToolTip1.SetToolTip(Me.picUp1, "Set the maximum of the linear spread")
    '
    'picUp0
    '
    Me.picUp0.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.picUp0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.picUp0.Image = CType(resources.GetObject("picUp0.Image"), System.Drawing.Image)
    Me.picUp0.InitialImage = CType(resources.GetObject("picUp0.InitialImage"), System.Drawing.Image)
    Me.picUp0.Location = New System.Drawing.Point(285, 637)
    Me.picUp0.Name = "picUp0"
    Me.picUp0.Size = New System.Drawing.Size(24, 24)
    Me.picUp0.TabIndex = 30
    Me.picUp0.TabStop = False
    Me.ToolTip1.SetToolTip(Me.picUp0, "Set the minimum of the linear spread.")
    '
    'optColorBlue
    '
    Me.optColorBlue.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.optColorBlue.AutoSize = True
    Me.optColorBlue.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.optColorBlue.Location = New System.Drawing.Point(19, 590)
    Me.optColorBlue.Name = "optColorBlue"
    Me.optColorBlue.Size = New System.Drawing.Size(58, 21)
    Me.optColorBlue.TabIndex = 6
    Me.optColorBlue.TabStop = True
    Me.optColorBlue.Text = "&Blue"
    Me.ToolTip1.SetToolTip(Me.optColorBlue, "Apply to blue only")
    Me.optColorBlue.UseVisualStyleBackColor = False
    '
    'optColorGreen
    '
    Me.optColorGreen.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.optColorGreen.AutoSize = True
    Me.optColorGreen.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.optColorGreen.Location = New System.Drawing.Point(19, 565)
    Me.optColorGreen.Name = "optColorGreen"
    Me.optColorGreen.Size = New System.Drawing.Size(69, 21)
    Me.optColorGreen.TabIndex = 5
    Me.optColorGreen.TabStop = True
    Me.optColorGreen.Text = "&Green"
    Me.ToolTip1.SetToolTip(Me.optColorGreen, "Apply to green only")
    Me.optColorGreen.UseVisualStyleBackColor = False
    '
    'optColorRed
    '
    Me.optColorRed.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.optColorRed.AutoSize = True
    Me.optColorRed.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.optColorRed.Location = New System.Drawing.Point(19, 540)
    Me.optColorRed.Name = "optColorRed"
    Me.optColorRed.Size = New System.Drawing.Size(56, 21)
    Me.optColorRed.TabIndex = 4
    Me.optColorRed.TabStop = True
    Me.optColorRed.Text = "&Red"
    Me.ToolTip1.SetToolTip(Me.optColorRed, "Apply to red only")
    Me.optColorRed.UseVisualStyleBackColor = False
    '
    'optColorAll
    '
    Me.optColorAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.optColorAll.AutoSize = True
    Me.optColorAll.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.optColorAll.Location = New System.Drawing.Point(19, 515)
    Me.optColorAll.Name = "optColorAll"
    Me.optColorAll.Size = New System.Drawing.Size(91, 21)
    Me.optColorAll.TabIndex = 3
    Me.optColorAll.TabStop = True
    Me.optColorAll.Text = "&All Colors"
    Me.ToolTip1.SetToolTip(Me.optColorAll, "Apply to all colors")
    Me.optColorAll.UseVisualStyleBackColor = False
    '
    'cmdReset
    '
    Me.cmdReset.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdReset.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdReset.Location = New System.Drawing.Point(891, 598)
    Me.cmdReset.Name = "cmdReset"
    Me.cmdReset.Size = New System.Drawing.Size(94, 34)
    Me.cmdReset.TabIndex = 16
    Me.cmdReset.Text = "Rese&t"
    Me.ToolTip1.SetToolTip(Me.cmdReset, "Reset to original colors")
    Me.cmdReset.UseVisualStyleBackColor = False
    '
    'TrackBar1
    '
    Me.TrackBar1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.TrackBar1.AutoSize = False
    Me.TrackBar1.Location = New System.Drawing.Point(13, 428)
    Me.TrackBar1.Maximum = 100
    Me.TrackBar1.Minimum = -100
    Me.TrackBar1.Name = "TrackBar1"
    Me.TrackBar1.Size = New System.Drawing.Size(211, 25)
    Me.TrackBar1.TabIndex = 2
    Me.TrackBar1.TickStyle = System.Windows.Forms.TickStyle.None
    Me.ToolTip1.SetToolTip(Me.TrackBar1, "Expand (or reduce) midtones." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "A larger value enhances the color," & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "a smaller value" & _
        " makes the color flatter.")
    '
    'nmSlide3
    '
    Me.nmSlide3.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.nmSlide3.AutoSize = True
    Me.nmSlide3.Location = New System.Drawing.Point(510, 671)
    Me.nmSlide3.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
    Me.nmSlide3.Name = "nmSlide3"
    Me.nmSlide3.Size = New System.Drawing.Size(58, 25)
    Me.nmSlide3.TabIndex = 13
    Me.ToolTip1.SetToolTip(Me.nmSlide3, "Set the maximum of the linear spread")
    Me.nmSlide3.Value = New Decimal(New Integer() {255, 0, 0, 0})
    '
    'nmSlide2
    '
    Me.nmSlide2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.nmSlide2.Location = New System.Drawing.Point(290, 669)
    Me.nmSlide2.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
    Me.nmSlide2.Name = "nmSlide2"
    Me.nmSlide2.Size = New System.Drawing.Size(58, 25)
    Me.nmSlide2.TabIndex = 11
    Me.ToolTip1.SetToolTip(Me.nmSlide2, "Set the minimum of the linear spread.")
    Me.nmSlide2.Value = New Decimal(New Integer() {255, 0, 0, 0})
    '
    'nmSlide0
    '
    Me.nmSlide0.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.nmSlide0.Location = New System.Drawing.Point(197, 606)
    Me.nmSlide0.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
    Me.nmSlide0.Name = "nmSlide0"
    Me.nmSlide0.Size = New System.Drawing.Size(58, 25)
    Me.nmSlide0.TabIndex = 10
    Me.ToolTip1.SetToolTip(Me.nmSlide0, "Limit the low (dark) end of the colors")
    Me.nmSlide0.Value = New Decimal(New Integer() {255, 0, 0, 0})
    '
    'nmSlide1
    '
    Me.nmSlide1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.nmSlide1.Location = New System.Drawing.Point(197, 492)
    Me.nmSlide1.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
    Me.nmSlide1.Name = "nmSlide1"
    Me.nmSlide1.Size = New System.Drawing.Size(58, 25)
    Me.nmSlide1.TabIndex = 8
    Me.ToolTip1.SetToolTip(Me.nmSlide1, "Limit the high (bright) end of the colors")
    Me.nmSlide1.Value = New Decimal(New Integer() {255, 0, 0, 0})
    '
    'nmTrackbar
    '
    Me.nmTrackbar.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.nmTrackbar.Location = New System.Drawing.Point(19, 454)
    Me.nmTrackbar.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
    Me.nmTrackbar.Name = "nmTrackbar"
    Me.nmTrackbar.Size = New System.Drawing.Size(75, 25)
    Me.nmTrackbar.TabIndex = 1
    Me.ToolTip1.SetToolTip(Me.nmTrackbar, "Expand (or reduce) midtones." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "A larger value enhances the color," & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "a smaller value" & _
        " makes the color flatter.")
    Me.nmTrackbar.Value = New Decimal(New Integer() {100, 0, 0, 0})
    '
    'cmdOK
    '
    Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdOK.Location = New System.Drawing.Point(891, 498)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(94, 34)
    Me.cmdOK.TabIndex = 14
    Me.cmdOK.Text = "&OK"
    Me.cmdOK.UseVisualStyleBackColor = False
    '
    'cmdCancel
    '
    Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdCancel.Location = New System.Drawing.Point(891, 548)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(94, 34)
    Me.cmdCancel.TabIndex = 15
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'Label9
    '
    Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.Label9.AutoSize = True
    Me.Label9.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label9.Location = New System.Drawing.Point(16, 408)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(119, 17)
    Me.Label9.TabIndex = 0
    Me.Label9.Text = "&Expand Midtones"
    Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label7
    '
    Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label7.Location = New System.Drawing.Point(375, 666)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(106, 21)
    Me.Label7.TabIndex = 12
    Me.Label7.Text = "&Linear Spread"
    Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopCenter
    '
    'Label6
    '
    Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label6.Location = New System.Drawing.Point(166, 551)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(96, 21)
    Me.Label6.TabIndex = 9
    Me.Label6.Text = "&Clipping"
    Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
    '
    'Label4
    '
    Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.Label4.AutoSize = True
    Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label4.Location = New System.Drawing.Point(581, 452)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(138, 17)
    Me.Label4.TabIndex = 27
    Me.Label4.Text = "Modified Distribution"
    '
    'Label2
    '
    Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label2.Location = New System.Drawing.Point(287, 453)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(135, 17)
    Me.Label2.TabIndex = 26
    Me.Label2.Text = "Original Distribution"
    '
    'lbPct3
    '
    Me.lbPct3.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.lbPct3.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbPct3.Location = New System.Drawing.Point(574, 673)
    Me.lbPct3.Name = "lbPct3"
    Me.lbPct3.Size = New System.Drawing.Size(76, 16)
    Me.lbPct3.TabIndex = 25
    Me.lbPct3.Text = "10%"
    '
    'lbPct2
    '
    Me.lbPct2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.lbPct2.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbPct2.Location = New System.Drawing.Point(227, 671)
    Me.lbPct2.Name = "lbPct2"
    Me.lbPct2.Size = New System.Drawing.Size(58, 16)
    Me.lbPct2.TabIndex = 24
    Me.lbPct2.Text = "10%"
    Me.lbPct2.TextAlign = System.Drawing.ContentAlignment.TopRight
    '
    'lbPct1
    '
    Me.lbPct1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.lbPct1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbPct1.Location = New System.Drawing.Point(128, 494)
    Me.lbPct1.Name = "lbPct1"
    Me.lbPct1.Size = New System.Drawing.Size(64, 16)
    Me.lbPct1.TabIndex = 23
    Me.lbPct1.Text = "10%"
    Me.lbPct1.TextAlign = System.Drawing.ContentAlignment.TopRight
    '
    'lbPct0
    '
    Me.lbPct0.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.lbPct0.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbPct0.Location = New System.Drawing.Point(131, 608)
    Me.lbPct0.Name = "lbPct0"
    Me.lbPct0.Size = New System.Drawing.Size(62, 16)
    Me.lbPct0.TabIndex = 22
    Me.lbPct0.Text = "10%"
    Me.lbPct0.TextAlign = System.Drawing.ContentAlignment.TopRight
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.ProgressBar1.Location = New System.Drawing.Point(748, 671)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(212, 15)
    Me.ProgressBar1.TabIndex = 54
    Me.ProgressBar1.Visible = False
    '
    'aview
    '
    Me.aview.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.aview.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.aview.Location = New System.Drawing.Point(3, 3)
    Me.aview.Name = "aview"
    Me.aview.NextButtons = False
    Me.aview.pCenter = New System.Drawing.Point(0, 0)
    Me.aview.SingleView = False
    Me.aview.Size = New System.Drawing.Size(992, 399)
    Me.aview.TabIndex = 55
    Me.aview.zoomFactor = 0.0R
    '
    'pView2
    '
    Me.pView2.BackColor = System.Drawing.SystemColors.Window
    Me.pView2.BitmapPath = Nothing
    Me.pView2.CurrentPage = -1
    Me.pView2.DrawAngle = 0.0R
    Me.pView2.DrawBackColor = System.Drawing.Color.White
    Me.pView2.DrawBox = New System.Drawing.Rectangle(0, 0, 0, 0)
    Me.pView2.DrawDashed = False
    Me.pView2.DrawFilled = False
    Me.pView2.DrawFont = Nothing
    Me.pView2.DrawForeColor = System.Drawing.Color.Navy
    Me.pView2.DrawLineWidth = 1.0!
    Me.pView2.DrawPath = Nothing
    Me.pView2.DrawPoints = CType(resources.GetObject("pView2.DrawPoints"), System.Collections.Generic.List(Of System.Drawing.Point))
    Me.pView2.DrawShape = PhotoMud.shape.Line
    Me.pView2.DrawString = ""
    Me.pView2.DrawTextFmt = Nothing
    Me.pView2.FloaterOutline = False
    Me.pView2.FloaterPath = Nothing
    Me.pView2.FloaterPosition = New System.Drawing.Point(0, 0)
    Me.pView2.FloaterVisible = True
    Me.pView2.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High
    Me.pView2.Location = New System.Drawing.Point(291, 473)
    Me.pView2.Name = "pView2"
    Me.pView2.pageBmp = CType(resources.GetObject("pView2.pageBmp"), System.Collections.Generic.List(Of System.Drawing.Bitmap))
    Me.pView2.PageCount = 0
    Me.pView2.RubberAngle = 0.0R
    Me.pView2.rubberBackColor = System.Drawing.Color.White
    Me.pView2.RubberBox = New System.Drawing.Rectangle(0, 0, 0, 0)
    Me.pView2.RubberBoxCrop = False
    Me.pView2.RubberColor = System.Drawing.Color.Navy
    Me.pView2.RubberDashed = False
    Me.pView2.RubberEnabled = False
    Me.pView2.RubberFilled = False
    Me.pView2.RubberFont = Nothing
    Me.pView2.RubberLineWidth = 1.0R
    Me.pView2.RubberPath = Nothing
    Me.pView2.RubberPoints = CType(resources.GetObject("pView2.RubberPoints"), System.Collections.Generic.List(Of System.Drawing.Point))
    Me.pView2.RubberShape = PhotoMud.shape.Curve
    Me.pView2.RubberString = ""
    Me.pView2.RubberTextFmt = Nothing
    Me.pView2.SelectionVisible = True
    Me.pView2.Size = New System.Drawing.Size(277, 158)
    Me.pView2.TabIndex = 0
    Me.pView2.TabStop = False
    Me.pView2.ZoomFactor = 1.0R
    '
    'pView3
    '
    Me.pView3.BackColor = System.Drawing.SystemColors.Window
    Me.pView3.BitmapPath = Nothing
    Me.pView3.CurrentPage = -1
    Me.pView3.DrawAngle = 0.0R
    Me.pView3.DrawBackColor = System.Drawing.Color.White
    Me.pView3.DrawBox = New System.Drawing.Rectangle(0, 0, 0, 0)
    Me.pView3.DrawDashed = False
    Me.pView3.DrawFilled = False
    Me.pView3.DrawFont = Nothing
    Me.pView3.DrawForeColor = System.Drawing.Color.Navy
    Me.pView3.DrawLineWidth = 1.0!
    Me.pView3.DrawPath = Nothing
    Me.pView3.DrawPoints = CType(resources.GetObject("pView3.DrawPoints"), System.Collections.Generic.List(Of System.Drawing.Point))
    Me.pView3.DrawShape = PhotoMud.shape.Line
    Me.pView3.DrawString = ""
    Me.pView3.DrawTextFmt = Nothing
    Me.pView3.FloaterOutline = False
    Me.pView3.FloaterPath = Nothing
    Me.pView3.FloaterPosition = New System.Drawing.Point(0, 0)
    Me.pView3.FloaterVisible = True
    Me.pView3.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High
    Me.pView3.Location = New System.Drawing.Point(585, 472)
    Me.pView3.Name = "pView3"
    Me.pView3.pageBmp = CType(resources.GetObject("pView3.pageBmp"), System.Collections.Generic.List(Of System.Drawing.Bitmap))
    Me.pView3.PageCount = 0
    Me.pView3.RubberAngle = 0.0R
    Me.pView3.rubberBackColor = System.Drawing.Color.White
    Me.pView3.RubberBox = New System.Drawing.Rectangle(0, 0, 0, 0)
    Me.pView3.RubberBoxCrop = False
    Me.pView3.RubberColor = System.Drawing.Color.Navy
    Me.pView3.RubberDashed = False
    Me.pView3.RubberEnabled = False
    Me.pView3.RubberFilled = False
    Me.pView3.RubberFont = Nothing
    Me.pView3.RubberLineWidth = 1.0R
    Me.pView3.RubberPath = Nothing
    Me.pView3.RubberPoints = CType(resources.GetObject("pView3.RubberPoints"), System.Collections.Generic.List(Of System.Drawing.Point))
    Me.pView3.RubberShape = PhotoMud.shape.Curve
    Me.pView3.RubberString = ""
    Me.pView3.RubberTextFmt = Nothing
    Me.pView3.SelectionVisible = True
    Me.pView3.Size = New System.Drawing.Size(277, 158)
    Me.pView3.TabIndex = 56
    Me.pView3.TabStop = False
    Me.pView3.ZoomFactor = 1.0R
    '
    'frmHisto
    '
    Me.AcceptButton = Me.cmdOK
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(997, 710)
    Me.Controls.Add(Me.pView3)
    Me.Controls.Add(Me.pView2)
    Me.Controls.Add(Me.aview)
    Me.Controls.Add(Me.ProgressBar1)
    Me.Controls.Add(Me.nmTrackbar)
    Me.Controls.Add(Me.nmSlide1)
    Me.Controls.Add(Me.nmSlide0)
    Me.Controls.Add(Me.nmSlide2)
    Me.Controls.Add(Me.nmSlide3)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.TrackBar1)
    Me.Controls.Add(Me.chkFreeForm)
    Me.Controls.Add(Me.picRight1)
    Me.Controls.Add(Me.picRight0)
    Me.Controls.Add(Me.picUp1)
    Me.Controls.Add(Me.picUp0)
    Me.Controls.Add(Me.optColorBlue)
    Me.Controls.Add(Me.optColorGreen)
    Me.Controls.Add(Me.optColorRed)
    Me.Controls.Add(Me.optColorAll)
    Me.Controls.Add(Me.cmdReset)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.Label9)
    Me.Controls.Add(Me.Label7)
    Me.Controls.Add(Me.Label6)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.lbPct3)
    Me.Controls.Add(Me.lbPct2)
    Me.Controls.Add(Me.lbPct1)
    Me.Controls.Add(Me.lbPct0)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 30)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmHisto"
    Me.ShowInTaskbar = False
    Me.Text = "Histogram Color Adjustment"
    CType(Me.picRight1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.picRight0, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.picUp1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.picUp0, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmSlide3, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmSlide2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmSlide0, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmSlide1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmTrackbar, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pView2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pView3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
  Public WithEvents cmdHelp As System.Windows.Forms.Button
  Friend WithEvents nmSlide3 As System.Windows.Forms.NumericUpDown
  Friend WithEvents nmSlide2 As System.Windows.Forms.NumericUpDown
  Friend WithEvents nmSlide0 As System.Windows.Forms.NumericUpDown
  Friend WithEvents nmSlide1 As System.Windows.Forms.NumericUpDown
  Friend WithEvents nmTrackbar As System.Windows.Forms.NumericUpDown
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  Friend WithEvents aview As PhotoMud.ctlViewCompare
  Friend WithEvents pView2 As PhotoMud.pViewer
  Friend WithEvents pView3 As PhotoMud.pViewer
#End Region
End Class