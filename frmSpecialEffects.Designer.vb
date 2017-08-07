<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSpecialEffects
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
  Public WithEvents cmdCancel As Button
  Public WithEvents cmdOK As Button
  Public WithEvents optAddNoise As RadioButton
  Public WithEvents optIntensityDetect As RadioButton
  Public WithEvents optEmboss As RadioButton
  Public WithEvents optPosterize As RadioButton
  Public WithEvents optPixelate As RadioButton
  Public WithEvents optSolarize As RadioButton
  Public WithEvents Frame1 As GroupBox
  Public WithEvents lbEffect0 As Label
  Public WithEvents lbEffect1 As Label
  'Public WithEvents lead1 As AxLEADArray
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSpecialEffects))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.optAddNoise = New System.Windows.Forms.RadioButton()
    Me.optIntensityDetect = New System.Windows.Forms.RadioButton()
    Me.optEmboss = New System.Windows.Forms.RadioButton()
    Me.optPosterize = New System.Windows.Forms.RadioButton()
    Me.optPixelate = New System.Windows.Forms.RadioButton()
    Me.optSolarize = New System.Windows.Forms.RadioButton()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.cmdOK = New System.Windows.Forms.Button()
    Me.Frame1 = New System.Windows.Forms.GroupBox()
    Me.lbEffect0 = New System.Windows.Forms.Label()
    Me.lbEffect1 = New System.Windows.Forms.Label()
    Me.trkEffect1 = New System.Windows.Forms.TrackBar()
    Me.trkEffect0 = New System.Windows.Forms.TrackBar()
    Me.nmEffect1 = New System.Windows.Forms.NumericUpDown()
    Me.nmEffect0 = New System.Windows.Forms.NumericUpDown()
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.Combo1 = New System.Windows.Forms.ComboBox()
    Me.aview = New PhotoMud.ctlViewCompare()
    Me.Frame1.SuspendLayout()
    CType(Me.trkEffect1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkEffect0, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmEffect1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmEffect0, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'cmdHelp
    '
    Me.cmdHelp.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(785, 441)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 39)
    Me.cmdHelp.TabIndex = 12
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'optAddNoise
    '
    Me.optAddNoise.AutoSize = True
    Me.optAddNoise.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.optAddNoise.Location = New System.Drawing.Point(15, 153)
    Me.optAddNoise.Name = "optAddNoise"
    Me.optAddNoise.Size = New System.Drawing.Size(155, 21)
    Me.optAddNoise.TabIndex = 5
    Me.optAddNoise.TabStop = True
    Me.optAddNoise.Text = "&Add Random Noise"
    Me.ToolTip1.SetToolTip(Me.optAddNoise, "Add random noise to the photo")
    Me.optAddNoise.UseVisualStyleBackColor = False
    '
    'optIntensityDetect
    '
    Me.optIntensityDetect.AutoSize = True
    Me.optIntensityDetect.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.optIntensityDetect.Location = New System.Drawing.Point(15, 128)
    Me.optIntensityDetect.Name = "optIntensityDetect"
    Me.optIntensityDetect.Size = New System.Drawing.Size(129, 21)
    Me.optIntensityDetect.TabIndex = 4
    Me.optIntensityDetect.TabStop = True
    Me.optIntensityDetect.Text = "&Intensity Detect"
    Me.ToolTip1.SetToolTip(Me.optIntensityDetect, "Convert the photo to black and white based on a color threshold")
    Me.optIntensityDetect.UseVisualStyleBackColor = False
    '
    'optEmboss
    '
    Me.optEmboss.AutoSize = True
    Me.optEmboss.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.optEmboss.Location = New System.Drawing.Point(15, 28)
    Me.optEmboss.Name = "optEmboss"
    Me.optEmboss.Size = New System.Drawing.Size(84, 21)
    Me.optEmboss.TabIndex = 0
    Me.optEmboss.TabStop = True
    Me.optEmboss.Text = "&Emboss"
    Me.ToolTip1.SetToolTip(Me.optEmboss, "Emboss effect")
    Me.optEmboss.UseVisualStyleBackColor = False
    '
    'optPosterize
    '
    Me.optPosterize.AutoSize = True
    Me.optPosterize.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.optPosterize.Location = New System.Drawing.Point(15, 53)
    Me.optPosterize.Name = "optPosterize"
    Me.optPosterize.Size = New System.Drawing.Size(91, 21)
    Me.optPosterize.TabIndex = 1
    Me.optPosterize.TabStop = True
    Me.optPosterize.Text = "&Posterize"
    Me.ToolTip1.SetToolTip(Me.optPosterize, "Show the photo with a reduced " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "number of color levels.")
    Me.optPosterize.UseVisualStyleBackColor = False
    '
    'optPixelate
    '
    Me.optPixelate.AutoSize = True
    Me.optPixelate.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.optPixelate.Location = New System.Drawing.Point(15, 78)
    Me.optPixelate.Name = "optPixelate"
    Me.optPixelate.Size = New System.Drawing.Size(80, 21)
    Me.optPixelate.TabIndex = 2
    Me.optPixelate.TabStop = True
    Me.optPixelate.Text = "Pi&xelate"
    Me.ToolTip1.SetToolTip(Me.optPixelate, "Show the photo with ""giant pixels"" or a mosaic.")
    Me.optPixelate.UseVisualStyleBackColor = False
    '
    'optSolarize
    '
    Me.optSolarize.AutoSize = True
    Me.optSolarize.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.optSolarize.Location = New System.Drawing.Point(15, 103)
    Me.optSolarize.Name = "optSolarize"
    Me.optSolarize.Size = New System.Drawing.Size(82, 21)
    Me.optSolarize.TabIndex = 3
    Me.optSolarize.TabStop = True
    Me.optSolarize.Text = "Solari&ze"
    Me.ToolTip1.SetToolTip(Me.optSolarize, "Solarize special effect")
    Me.optSolarize.UseVisualStyleBackColor = False
    '
    'cmdCancel
    '
    Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdCancel.Location = New System.Drawing.Point(758, 546)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(96, 31)
    Me.cmdCancel.TabIndex = 14
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'cmdOK
    '
    Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdOK.Location = New System.Drawing.Point(758, 496)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(96, 31)
    Me.cmdOK.TabIndex = 13
    Me.cmdOK.Text = "&OK"
    Me.cmdOK.UseVisualStyleBackColor = False
    '
    'Frame1
    '
    Me.Frame1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.Frame1.Controls.Add(Me.optAddNoise)
    Me.Frame1.Controls.Add(Me.optIntensityDetect)
    Me.Frame1.Controls.Add(Me.optEmboss)
    Me.Frame1.Controls.Add(Me.optPosterize)
    Me.Frame1.Controls.Add(Me.optPixelate)
    Me.Frame1.Controls.Add(Me.optSolarize)
    Me.Frame1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Frame1.Location = New System.Drawing.Point(131, 414)
    Me.Frame1.Name = "Frame1"
    Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
    Me.Frame1.Size = New System.Drawing.Size(204, 189)
    Me.Frame1.TabIndex = 14
    Me.Frame1.TabStop = False
    Me.Frame1.Text = "Effects"
    '
    'lbEffect0
    '
    Me.lbEffect0.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.lbEffect0.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbEffect0.Location = New System.Drawing.Point(377, 515)
    Me.lbEffect0.Name = "lbEffect0"
    Me.lbEffect0.Size = New System.Drawing.Size(172, 22)
    Me.lbEffect0.TabIndex = 9
    Me.lbEffect0.Text = "&Strength (sample size)"
    '
    'lbEffect1
    '
    Me.lbEffect1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.lbEffect1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbEffect1.Location = New System.Drawing.Point(377, 440)
    Me.lbEffect1.Name = "lbEffect1"
    Me.lbEffect1.Size = New System.Drawing.Size(146, 21)
    Me.lbEffect1.TabIndex = 6
    Me.lbEffect1.Text = "&Light Angle"
    '
    'trkEffect1
    '
    Me.trkEffect1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.trkEffect1.AutoSize = False
    Me.trkEffect1.Location = New System.Drawing.Point(364, 467)
    Me.trkEffect1.Maximum = 360
    Me.trkEffect1.Minimum = -360
    Me.trkEffect1.Name = "trkEffect1"
    Me.trkEffect1.Size = New System.Drawing.Size(180, 24)
    Me.trkEffect1.TabIndex = 7
    Me.trkEffect1.TickStyle = System.Windows.Forms.TickStyle.None
    '
    'trkEffect0
    '
    Me.trkEffect0.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.trkEffect0.AutoSize = False
    Me.trkEffect0.Location = New System.Drawing.Point(364, 539)
    Me.trkEffect0.Maximum = 100
    Me.trkEffect0.Minimum = 1
    Me.trkEffect0.Name = "trkEffect0"
    Me.trkEffect0.Size = New System.Drawing.Size(180, 24)
    Me.trkEffect0.TabIndex = 10
    Me.trkEffect0.TickStyle = System.Windows.Forms.TickStyle.None
    Me.trkEffect0.Value = 1
    '
    'nmEffect1
    '
    Me.nmEffect1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.nmEffect1.Location = New System.Drawing.Point(545, 464)
    Me.nmEffect1.Name = "nmEffect1"
    Me.nmEffect1.Size = New System.Drawing.Size(62, 25)
    Me.nmEffect1.TabIndex = 8
    '
    'nmEffect0
    '
    Me.nmEffect0.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.nmEffect0.Location = New System.Drawing.Point(545, 536)
    Me.nmEffect0.Name = "nmEffect0"
    Me.nmEffect0.Size = New System.Drawing.Size(62, 25)
    Me.nmEffect0.TabIndex = 11
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.ProgressBar1.Location = New System.Drawing.Point(373, 584)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(233, 15)
    Me.ProgressBar1.TabIndex = 46
    Me.ProgressBar1.Visible = False
    '
    'Combo1
    '
    Me.Combo1.AllowDrop = True
    Me.Combo1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
    Me.Combo1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.Combo1.FormattingEnabled = True
    Me.Combo1.Location = New System.Drawing.Point(595, 397)
    Me.Combo1.Name = "Combo1"
    Me.Combo1.Size = New System.Drawing.Size(284, 25)
    Me.Combo1.TabIndex = 48
    '
    'aview
    '
    Me.aview.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.aview.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.aview.BackColor = System.Drawing.Color.Transparent
    Me.aview.Location = New System.Drawing.Point(3, 3)
    Me.aview.Name = "aview"
    Me.aview.NextButtons = False
    Me.aview.pCenter = New System.Drawing.Point(0, 0)
    Me.aview.SingleView = False
    Me.aview.Size = New System.Drawing.Size(974, 408)
    Me.aview.TabIndex = 47
    Me.aview.zoomFactor = 0.0R
    '
    'frmSpecialEffects
    '
    Me.AcceptButton = Me.cmdOK
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(979, 614)
    Me.Controls.Add(Me.Combo1)
    Me.Controls.Add(Me.aview)
    Me.Controls.Add(Me.ProgressBar1)
    Me.Controls.Add(Me.nmEffect0)
    Me.Controls.Add(Me.nmEffect1)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.trkEffect0)
    Me.Controls.Add(Me.trkEffect1)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.Frame1)
    Me.Controls.Add(Me.lbEffect0)
    Me.Controls.Add(Me.lbEffect1)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 30)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmSpecialEffects"
    Me.ShowInTaskbar = False
    Me.Text = "Special Effects"
    Me.Frame1.ResumeLayout(False)
    Me.Frame1.PerformLayout()
    CType(Me.trkEffect1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkEffect0, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmEffect1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmEffect0, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
 Friend WithEvents trkEffect1 As System.Windows.Forms.TrackBar
 Friend WithEvents trkEffect0 As System.Windows.Forms.TrackBar
 Public WithEvents cmdHelp As System.Windows.Forms.Button
 Friend WithEvents nmEffect1 As System.Windows.Forms.NumericUpDown
 Friend WithEvents nmEffect0 As System.Windows.Forms.NumericUpDown
 Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  Friend WithEvents Combo1 As System.Windows.Forms.ComboBox
  Friend WithEvents aview As PhotoMud.ctlViewCompare
#End Region
End Class