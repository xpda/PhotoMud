<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmColorBatchAdjust
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
  Public WithEvents blBright As Label
  Public WithEvents _lbColor_0 As Label
  Public WithEvents _lbColor_1 As Label
  Public WithEvents _lbColor_2 As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmColorBatchAdjust))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdOK = New System.Windows.Forms.Button()
    Me.trkGreen = New System.Windows.Forms.TrackBar()
    Me.trkRed = New System.Windows.Forms.TrackBar()
    Me.trkBright = New System.Windows.Forms.TrackBar()
    Me.nmBright = New System.Windows.Forms.NumericUpDown()
    Me.nmRed = New System.Windows.Forms.NumericUpDown()
    Me.nmGreen = New System.Windows.Forms.NumericUpDown()
    Me.nmBlue = New System.Windows.Forms.NumericUpDown()
    Me.trkBlue = New System.Windows.Forms.TrackBar()
    Me.nmContrast = New System.Windows.Forms.NumericUpDown()
    Me.trkContrast = New System.Windows.Forms.TrackBar()
    Me.nmSaturation = New System.Windows.Forms.NumericUpDown()
    Me.trkSaturation = New System.Windows.Forms.TrackBar()
    Me.chkAutoAdjust = New System.Windows.Forms.CheckBox()
    Me.cmdReset = New System.Windows.Forms.Button()
    Me.chkPreserveIntensity = New System.Windows.Forms.CheckBox()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.blBright = New System.Windows.Forms.Label()
    Me._lbColor_0 = New System.Windows.Forms.Label()
    Me._lbColor_1 = New System.Windows.Forms.Label()
    Me._lbColor_2 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.aview = New PhotoMud.ctlViewCompare()
    CType(Me.trkGreen, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkRed, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkBright, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmBright, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmRed, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmGreen, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmBlue, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkBlue, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmContrast, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkContrast, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmSaturation, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkSaturation, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'cmdOK
    '
    Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdOK.Location = New System.Drawing.Point(825, 521)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(101, 33)
    Me.cmdOK.TabIndex = 21
    Me.cmdOK.Text = "&OK"
    Me.ToolTip1.SetToolTip(Me.cmdOK, "Save changes and return")
    Me.cmdOK.UseVisualStyleBackColor = False
    '
    'trkGreen
    '
    Me.trkGreen.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.trkGreen.AutoSize = False
    Me.trkGreen.Location = New System.Drawing.Point(493, 513)
    Me.trkGreen.Maximum = 99
    Me.trkGreen.Minimum = -99
    Me.trkGreen.Name = "trkGreen"
    Me.trkGreen.Size = New System.Drawing.Size(210, 22)
    Me.trkGreen.TabIndex = 14
    Me.trkGreen.TickFrequency = 10
    Me.trkGreen.TickStyle = System.Windows.Forms.TickStyle.None
    Me.ToolTip1.SetToolTip(Me.trkGreen, "Green change, -100 to 100")
    '
    'trkRed
    '
    Me.trkRed.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.trkRed.AutoSize = False
    Me.trkRed.Location = New System.Drawing.Point(493, 482)
    Me.trkRed.Maximum = 99
    Me.trkRed.Minimum = -99
    Me.trkRed.Name = "trkRed"
    Me.trkRed.Size = New System.Drawing.Size(210, 22)
    Me.trkRed.TabIndex = 11
    Me.trkRed.TickFrequency = 10
    Me.trkRed.TickStyle = System.Windows.Forms.TickStyle.None
    Me.ToolTip1.SetToolTip(Me.trkRed, "Red change, -100 to 100")
    '
    'trkBright
    '
    Me.trkBright.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.trkBright.AutoSize = False
    Me.trkBright.Location = New System.Drawing.Point(119, 482)
    Me.trkBright.Maximum = 100
    Me.trkBright.Minimum = -100
    Me.trkBright.Name = "trkBright"
    Me.trkBright.Size = New System.Drawing.Size(210, 22)
    Me.trkBright.TabIndex = 1
    Me.trkBright.TickFrequency = 10
    Me.trkBright.TickStyle = System.Windows.Forms.TickStyle.None
    Me.ToolTip1.SetToolTip(Me.trkBright, "Brightness change, -100 to 100")
    '
    'nmBright
    '
    Me.nmBright.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.nmBright.Location = New System.Drawing.Point(335, 480)
    Me.nmBright.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
    Me.nmBright.Name = "nmBright"
    Me.nmBright.Size = New System.Drawing.Size(58, 25)
    Me.nmBright.TabIndex = 2
    Me.ToolTip1.SetToolTip(Me.nmBright, "Brightness change, -100 to 100")
    Me.nmBright.Value = New Decimal(New Integer() {100, 0, 0, -2147483648})
    '
    'nmRed
    '
    Me.nmRed.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.nmRed.Location = New System.Drawing.Point(709, 480)
    Me.nmRed.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
    Me.nmRed.Minimum = New Decimal(New Integer() {99, 0, 0, -2147483648})
    Me.nmRed.Name = "nmRed"
    Me.nmRed.Size = New System.Drawing.Size(58, 25)
    Me.nmRed.TabIndex = 12
    Me.ToolTip1.SetToolTip(Me.nmRed, "Red change, -100 to 100")
    Me.nmRed.Value = New Decimal(New Integer() {99, 0, 0, -2147483648})
    '
    'nmGreen
    '
    Me.nmGreen.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.nmGreen.Location = New System.Drawing.Point(709, 511)
    Me.nmGreen.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
    Me.nmGreen.Minimum = New Decimal(New Integer() {99, 0, 0, -2147483648})
    Me.nmGreen.Name = "nmGreen"
    Me.nmGreen.Size = New System.Drawing.Size(58, 25)
    Me.nmGreen.TabIndex = 15
    Me.ToolTip1.SetToolTip(Me.nmGreen, "Green change, -100 to 100")
    Me.nmGreen.Value = New Decimal(New Integer() {99, 0, 0, -2147483648})
    '
    'nmBlue
    '
    Me.nmBlue.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.nmBlue.Location = New System.Drawing.Point(709, 542)
    Me.nmBlue.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
    Me.nmBlue.Minimum = New Decimal(New Integer() {99, 0, 0, -2147483648})
    Me.nmBlue.Name = "nmBlue"
    Me.nmBlue.Size = New System.Drawing.Size(58, 25)
    Me.nmBlue.TabIndex = 18
    Me.ToolTip1.SetToolTip(Me.nmBlue, "Blue change, -100 to 100")
    Me.nmBlue.Value = New Decimal(New Integer() {99, 0, 0, -2147483648})
    '
    'trkBlue
    '
    Me.trkBlue.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.trkBlue.AutoSize = False
    Me.trkBlue.Location = New System.Drawing.Point(493, 544)
    Me.trkBlue.Maximum = 99
    Me.trkBlue.Minimum = -99
    Me.trkBlue.Name = "trkBlue"
    Me.trkBlue.Size = New System.Drawing.Size(210, 22)
    Me.trkBlue.TabIndex = 17
    Me.trkBlue.TickFrequency = 10
    Me.trkBlue.TickStyle = System.Windows.Forms.TickStyle.None
    Me.ToolTip1.SetToolTip(Me.trkBlue, "Blue change, -100 to 100")
    '
    'nmContrast
    '
    Me.nmContrast.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.nmContrast.Location = New System.Drawing.Point(335, 511)
    Me.nmContrast.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
    Me.nmContrast.Name = "nmContrast"
    Me.nmContrast.Size = New System.Drawing.Size(58, 25)
    Me.nmContrast.TabIndex = 5
    Me.ToolTip1.SetToolTip(Me.nmContrast, "Contrast change, -100 to 100")
    Me.nmContrast.Value = New Decimal(New Integer() {100, 0, 0, -2147483648})
    '
    'trkContrast
    '
    Me.trkContrast.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.trkContrast.AutoSize = False
    Me.trkContrast.Location = New System.Drawing.Point(119, 513)
    Me.trkContrast.Maximum = 100
    Me.trkContrast.Minimum = -100
    Me.trkContrast.Name = "trkContrast"
    Me.trkContrast.Size = New System.Drawing.Size(210, 22)
    Me.trkContrast.TabIndex = 4
    Me.trkContrast.TickFrequency = 10
    Me.trkContrast.TickStyle = System.Windows.Forms.TickStyle.None
    Me.ToolTip1.SetToolTip(Me.trkContrast, "Contrast change, -100 to 100")
    '
    'nmSaturation
    '
    Me.nmSaturation.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.nmSaturation.Location = New System.Drawing.Point(335, 542)
    Me.nmSaturation.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
    Me.nmSaturation.Name = "nmSaturation"
    Me.nmSaturation.Size = New System.Drawing.Size(58, 25)
    Me.nmSaturation.TabIndex = 8
    Me.ToolTip1.SetToolTip(Me.nmSaturation, "Saturation change, -100 to 100")
    Me.nmSaturation.Value = New Decimal(New Integer() {100, 0, 0, -2147483648})
    '
    'trkSaturation
    '
    Me.trkSaturation.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.trkSaturation.AutoSize = False
    Me.trkSaturation.Location = New System.Drawing.Point(119, 544)
    Me.trkSaturation.Maximum = 100
    Me.trkSaturation.Minimum = -100
    Me.trkSaturation.Name = "trkSaturation"
    Me.trkSaturation.Size = New System.Drawing.Size(210, 22)
    Me.trkSaturation.TabIndex = 7
    Me.trkSaturation.TickFrequency = 10
    Me.trkSaturation.TickStyle = System.Windows.Forms.TickStyle.None
    Me.ToolTip1.SetToolTip(Me.trkSaturation, "Saturation change, -100 to 100")
    '
    'chkAutoAdjust
    '
    Me.chkAutoAdjust.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.chkAutoAdjust.AutoSize = True
    Me.chkAutoAdjust.Location = New System.Drawing.Point(231, 585)
    Me.chkAutoAdjust.Name = "chkAutoAdjust"
    Me.chkAutoAdjust.Size = New System.Drawing.Size(150, 21)
    Me.chkAutoAdjust.TabIndex = 9
    Me.chkAutoAdjust.Text = "&Auto adjust photos"
    Me.ToolTip1.SetToolTip(Me.chkAutoAdjust, "Automatic hue and contrast adjustment")
    Me.chkAutoAdjust.UseVisualStyleBackColor = True
    '
    'cmdReset
    '
    Me.cmdReset.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdReset.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdReset.Location = New System.Drawing.Point(823, 472)
    Me.cmdReset.Name = "cmdReset"
    Me.cmdReset.Size = New System.Drawing.Size(101, 33)
    Me.cmdReset.TabIndex = 20
    Me.cmdReset.Text = "Rese&t"
    Me.ToolTip1.SetToolTip(Me.cmdReset, "Reset values to zero")
    Me.cmdReset.UseVisualStyleBackColor = False
    '
    'chkPreserveIntensity
    '
    Me.chkPreserveIntensity.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.chkPreserveIntensity.AutoSize = True
    Me.chkPreserveIntensity.Location = New System.Drawing.Point(493, 585)
    Me.chkPreserveIntensity.Name = "chkPreserveIntensity"
    Me.chkPreserveIntensity.Size = New System.Drawing.Size(146, 21)
    Me.chkPreserveIntensity.TabIndex = 24
    Me.chkPreserveIntensity.Text = "&Preserve intensity"
    Me.ToolTip1.SetToolTip(Me.chkPreserveIntensity, "Normalize the color adjustments to keep the same image brightness.")
    Me.chkPreserveIntensity.UseVisualStyleBackColor = True
    '
    'cmdCancel
    '
    Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdCancel.Location = New System.Drawing.Point(825, 571)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(101, 33)
    Me.cmdCancel.TabIndex = 22
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'blBright
    '
    Me.blBright.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.blBright.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.blBright.Location = New System.Drawing.Point(34, 480)
    Me.blBright.Name = "blBright"
    Me.blBright.Size = New System.Drawing.Size(86, 21)
    Me.blBright.TabIndex = 0
    Me.blBright.Text = "Br&ightness"
    '
    '_lbColor_0
    '
    Me._lbColor_0.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me._lbColor_0.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me._lbColor_0.Location = New System.Drawing.Point(438, 544)
    Me._lbColor_0.Name = "_lbColor_0"
    Me._lbColor_0.Size = New System.Drawing.Size(56, 21)
    Me._lbColor_0.TabIndex = 16
    Me._lbColor_0.Text = "&Blue"
    '
    '_lbColor_1
    '
    Me._lbColor_1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me._lbColor_1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me._lbColor_1.Location = New System.Drawing.Point(438, 515)
    Me._lbColor_1.Name = "_lbColor_1"
    Me._lbColor_1.Size = New System.Drawing.Size(56, 21)
    Me._lbColor_1.TabIndex = 13
    Me._lbColor_1.Text = "&Green"
    '
    '_lbColor_2
    '
    Me._lbColor_2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me._lbColor_2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me._lbColor_2.Location = New System.Drawing.Point(438, 482)
    Me._lbColor_2.Name = "_lbColor_2"
    Me._lbColor_2.Size = New System.Drawing.Size(56, 21)
    Me._lbColor_2.TabIndex = 10
    Me._lbColor_2.Text = "&Red"
    '
    'Label1
    '
    Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label1.Location = New System.Drawing.Point(34, 511)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(86, 21)
    Me.Label1.TabIndex = 3
    Me.Label1.Text = "Co&ntrast"
    '
    'Label2
    '
    Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label2.Location = New System.Drawing.Point(34, 542)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(86, 21)
    Me.Label2.TabIndex = 6
    Me.Label2.Text = "Sat&uration"
    '
    'aview
    '
    Me.aview.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.aview.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.aview.Location = New System.Drawing.Point(1, 1)
    Me.aview.Name = "aview"
    Me.aview.NextButtons = False
    Me.aview.pCenter = New System.Drawing.Point(0, 0)
    Me.aview.SingleView = False
    Me.aview.Size = New System.Drawing.Size(960, 464)
    Me.aview.TabIndex = 23
    Me.aview.zoomFactor = 0.0R
    '
    'frmColorBatchAdjust
    '
    Me.AcceptButton = Me.cmdOK
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(963, 631)
    Me.Controls.Add(Me.chkPreserveIntensity)
    Me.Controls.Add(Me.aview)
    Me.Controls.Add(Me.cmdReset)
    Me.Controls.Add(Me.chkAutoAdjust)
    Me.Controls.Add(Me.nmSaturation)
    Me.Controls.Add(Me.trkSaturation)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.nmContrast)
    Me.Controls.Add(Me.trkContrast)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.nmBlue)
    Me.Controls.Add(Me.nmGreen)
    Me.Controls.Add(Me.nmRed)
    Me.Controls.Add(Me.nmBright)
    Me.Controls.Add(Me.trkBright)
    Me.Controls.Add(Me.trkRed)
    Me.Controls.Add(Me.trkGreen)
    Me.Controls.Add(Me.trkBlue)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.blBright)
    Me.Controls.Add(Me._lbColor_0)
    Me.Controls.Add(Me._lbColor_1)
    Me.Controls.Add(Me._lbColor_2)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 22)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmColorBatchAdjust"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Color Adjustment"
    CType(Me.trkGreen, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkRed, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkBright, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmBright, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmRed, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmGreen, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmBlue, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkBlue, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmContrast, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkContrast, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmSaturation, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkSaturation, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

End Sub
 Friend WithEvents trkGreen As System.Windows.Forms.TrackBar
 Friend WithEvents trkRed As System.Windows.Forms.TrackBar
 Friend WithEvents trkBright As System.Windows.Forms.TrackBar
 Friend WithEvents nmBright As System.Windows.Forms.NumericUpDown
 Friend WithEvents nmRed As System.Windows.Forms.NumericUpDown
 Friend WithEvents nmGreen As System.Windows.Forms.NumericUpDown
 Friend WithEvents nmBlue As System.Windows.Forms.NumericUpDown
 Friend WithEvents trkBlue As System.Windows.Forms.TrackBar
 Friend WithEvents nmContrast As System.Windows.Forms.NumericUpDown
 Friend WithEvents trkContrast As System.Windows.Forms.TrackBar
 Public WithEvents Label1 As System.Windows.Forms.Label
 Friend WithEvents nmSaturation As System.Windows.Forms.NumericUpDown
 Friend WithEvents trkSaturation As System.Windows.Forms.TrackBar
 Public WithEvents Label2 As System.Windows.Forms.Label
 Friend WithEvents chkAutoAdjust As System.Windows.Forms.CheckBox
 Public WithEvents cmdReset As System.Windows.Forms.Button
 Friend WithEvents aview As PhotoMud.ctlViewCompare
 Friend WithEvents chkPreserveIntensity As System.Windows.Forms.CheckBox
#End Region
End Class