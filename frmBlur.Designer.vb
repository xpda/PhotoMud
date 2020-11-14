<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmBlur
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
  Public WithEvents Option2 As RadioButton
  Public WithEvents Option1 As RadioButton
  Public WithEvents Option0 As RadioButton
  Public WithEvents Frame1 As GroupBox
  Public WithEvents cmdOK As Button
  Public WithEvents cmdCancel As Button
  Public WithEvents lbAngle As Label
  Public WithEvents lbBlur As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBlur))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.Option2 = New System.Windows.Forms.RadioButton()
    Me.Option1 = New System.Windows.Forms.RadioButton()
    Me.Option0 = New System.Windows.Forms.RadioButton()
    Me.trkAngle = New System.Windows.Forms.TrackBar()
    Me.trkBlur = New System.Windows.Forms.TrackBar()
    Me.nmAngle = New System.Windows.Forms.NumericUpDown()
    Me.nmBlur = New System.Windows.Forms.NumericUpDown()
    Me.Option3 = New System.Windows.Forms.RadioButton()
    Me.Option4 = New System.Windows.Forms.RadioButton()
    Me.cmdOK = New System.Windows.Forms.Button()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.Frame1 = New System.Windows.Forms.GroupBox()
    Me.lbAngle = New System.Windows.Forms.Label()
    Me.lbBlur = New System.Windows.Forms.Label()
    Me.aView = New PhotoMud.ctlViewCompare()
        CType(Me.trkAngle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.trkBlur, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmAngle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmBlur, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdHelp
        '
        Me.cmdHelp.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
        Me.cmdHelp.Location = New System.Drawing.Point(814, 421)
        Me.cmdHelp.Name = "cmdHelp"
        Me.cmdHelp.Size = New System.Drawing.Size(41, 39)
        Me.cmdHelp.TabIndex = 11
        Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
        Me.cmdHelp.UseVisualStyleBackColor = False
        '
        'Option2
        '
        Me.Option2.AutoSize = True
        Me.Option2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Option2.Location = New System.Drawing.Point(15, 85)
        Me.Option2.Name = "Option2"
        Me.Option2.Size = New System.Drawing.Size(101, 21)
        Me.Option2.TabIndex = 2
        Me.Option2.TabStop = True
        Me.Option2.Text = "&Motion Blur"
        Me.ToolTip1.SetToolTip(Me.Option2, "Use motion blur at a " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "specified angle")
        Me.Option2.UseVisualStyleBackColor = False
        '
        'Option1
        '
        Me.Option1.AutoSize = True
        Me.Option1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Option1.Location = New System.Drawing.Point(15, 55)
        Me.Option1.Name = "Option1"
        Me.Option1.Size = New System.Drawing.Size(121, 21)
        Me.Option1.TabIndex = 1
        Me.Option1.TabStop = True
        Me.Option1.Text = "&Gaussian Blur"
        Me.ToolTip1.SetToolTip(Me.Option1, "Use the Gaussian blur algorithm, for higher quality.")
        Me.Option1.UseVisualStyleBackColor = False
        '
        'Option0
        '
        Me.Option0.AutoSize = True
        Me.Option0.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Option0.Location = New System.Drawing.Point(15, 28)
        Me.Option0.Name = "Option0"
        Me.Option0.Size = New System.Drawing.Size(106, 21)
        Me.Option0.TabIndex = 0
        Me.Option0.TabStop = True
        Me.Option0.Text = "&Normal Blur"
        Me.ToolTip1.SetToolTip(Me.Option0, "Blur by averaging adjacent pixels")
        Me.Option0.UseVisualStyleBackColor = False
        '
        'trkAngle
        '
        Me.trkAngle.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.trkAngle.AutoSize = False
        Me.trkAngle.Location = New System.Drawing.Point(363, 454)
        Me.trkAngle.Maximum = 45
        Me.trkAngle.Minimum = -45
        Me.trkAngle.Name = "trkAngle"
        Me.trkAngle.Size = New System.Drawing.Size(195, 22)
        Me.trkAngle.TabIndex = 4
        Me.trkAngle.TickFrequency = 5
        Me.trkAngle.TickStyle = System.Windows.Forms.TickStyle.None
        Me.ToolTip1.SetToolTip(Me.trkAngle, "Angle for motion blur")
        '
        'trkBlur
        '
        Me.trkBlur.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.trkBlur.AutoSize = False
        Me.trkBlur.Location = New System.Drawing.Point(363, 523)
        Me.trkBlur.Maximum = 45
        Me.trkBlur.Minimum = -45
        Me.trkBlur.Name = "trkBlur"
        Me.trkBlur.Size = New System.Drawing.Size(195, 22)
        Me.trkBlur.TabIndex = 7
        Me.trkBlur.TickFrequency = 5
        Me.trkBlur.TickStyle = System.Windows.Forms.TickStyle.None
        Me.ToolTip1.SetToolTip(Me.trkBlur, "Sample size (pixels) for blurring")
        '
        'nmAngle
        '
        Me.nmAngle.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.nmAngle.Location = New System.Drawing.Point(561, 451)
        Me.nmAngle.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmAngle.Name = "nmAngle"
        Me.nmAngle.Size = New System.Drawing.Size(59, 25)
        Me.nmAngle.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.nmAngle, "Angle for motion blur")
        Me.nmAngle.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nmBlur
        '
        Me.nmBlur.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.nmBlur.Location = New System.Drawing.Point(561, 519)
        Me.nmBlur.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmBlur.Name = "nmBlur"
        Me.nmBlur.Size = New System.Drawing.Size(59, 25)
        Me.nmBlur.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.nmBlur, "Sample size (pixels) for blurring")
        Me.nmBlur.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Option3
        '
        Me.Option3.AutoSize = True
        Me.Option3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Option3.Location = New System.Drawing.Point(16, 112)
        Me.Option3.Name = "Option3"
        Me.Option3.Size = New System.Drawing.Size(124, 21)
        Me.Option3.TabIndex = 3
        Me.Option3.TabStop = True
        Me.Option3.Text = "&Rotational Blur"
        Me.ToolTip1.SetToolTip(Me.Option3, "Use motion blur at a " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "specified angle")
        Me.Option3.UseVisualStyleBackColor = False
        '
        'Option4
        '
        Me.Option4.AutoSize = True
        Me.Option4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Option4.Location = New System.Drawing.Point(16, 140)
        Me.Option4.Name = "Option4"
        Me.Option4.Size = New System.Drawing.Size(114, 21)
        Me.Option4.TabIndex = 4
        Me.Option4.TabStop = True
        Me.Option4.Text = "&Adaptive Blur"
        Me.ToolTip1.SetToolTip(Me.Option4, "Use adaptive blur to leave the sharp edges intact.")
        Me.Option4.UseVisualStyleBackColor = False
        '
        'cmdOK
        '
        Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.cmdOK.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOK.Location = New System.Drawing.Point(792, 481)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(91, 31)
        Me.cmdOK.TabIndex = 12
        Me.cmdOK.Text = "&OK"
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.Location = New System.Drawing.Point(792, 531)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(91, 31)
        Me.cmdCancel.TabIndex = 13
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Frame1.Controls.Add(Me.Option4)
        Me.Frame1.Controls.Add(Me.Option3)
        Me.Frame1.Controls.Add(Me.Option2)
        Me.Frame1.Controls.Add(Me.Option1)
        Me.Frame1.Controls.Add(Me.Option0)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.Location = New System.Drawing.Point(107, 404)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.Size = New System.Drawing.Size(166, 173)
        Me.Frame1.TabIndex = 17
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Blur Method"
        '
        'lbAngle
        '
        Me.lbAngle.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.lbAngle.AutoSize = True
        Me.lbAngle.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.lbAngle.Location = New System.Drawing.Point(360, 432)
        Me.lbAngle.Name = "lbAngle"
        Me.lbAngle.Size = New System.Drawing.Size(89, 17)
        Me.lbAngle.TabIndex = 3
        Me.lbAngle.Text = "M&otion Angle"
        Me.lbAngle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbBlur
        '
        Me.lbBlur.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.lbBlur.AutoSize = True
        Me.lbBlur.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.lbBlur.Location = New System.Drawing.Point(359, 498)
        Me.lbBlur.Name = "lbBlur"
        Me.lbBlur.Size = New System.Drawing.Size(186, 17)
        Me.lbBlur.TabIndex = 6
        Me.lbBlur.Text = "&Blur Strength (sample size)"
        Me.lbBlur.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'aView
        '
        Me.aView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.aView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.aView.Location = New System.Drawing.Point(3, 5)
        Me.aView.Name = "aView"
        Me.aView.NextButtons = False
        Me.aView.pCenter = New System.Drawing.Point(0, 0)
        Me.aView.SingleView = False
        Me.aView.Size = New System.Drawing.Size(962, 388)
        Me.aView.TabIndex = 37
        Me.aView.zoomFactor = 0R
        '
        'frmBlur
        '
        Me.AcceptButton = Me.cmdOK
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(968, 599)
        Me.Controls.Add(Me.aView)
        Me.Controls.Add(Me.nmBlur)
        Me.Controls.Add(Me.nmAngle)
        Me.Controls.Add(Me.cmdHelp)
        Me.Controls.Add(Me.trkBlur)
        Me.Controls.Add(Me.trkAngle)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.lbAngle)
        Me.Controls.Add(Me.lbBlur)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 30)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBlur"
        Me.ShowInTaskbar = False
        Me.Text = "Image Blur"
        CType(Me.trkAngle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.trkBlur, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmAngle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmBlur, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents trkAngle As System.Windows.Forms.TrackBar
 Friend WithEvents trkBlur As System.Windows.Forms.TrackBar
 Public WithEvents cmdHelp As System.Windows.Forms.Button
 Friend WithEvents nmAngle As System.Windows.Forms.NumericUpDown
 Friend WithEvents nmBlur As System.Windows.Forms.NumericUpDown
 Public WithEvents Option3 As System.Windows.Forms.RadioButton
 Public WithEvents Option4 As System.Windows.Forms.RadioButton
  Public WithEvents aView As PhotoMud.ctlViewCompare
#End Region
End Class