<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmBorder
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
  Public WithEvents cmdOK As Button
  Public WithEvents cmdCancel As Button
  Public WithEvents lbInner As Label
  Public WithEvents lbOuter As Label
  Public WithEvents lbColor As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBorder))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.trkOuter = New System.Windows.Forms.TrackBar()
    Me.trkInner = New System.Windows.Forms.TrackBar()
    Me.nmOuter = New System.Windows.Forms.NumericUpDown()
    Me.nmInner = New System.Windows.Forms.NumericUpDown()
    Me.opt3D = New System.Windows.Forms.RadioButton()
    Me.optRaise = New System.Windows.Forms.RadioButton()
    Me.optButton = New System.Windows.Forms.RadioButton()
    Me.opt2D = New System.Windows.Forms.RadioButton()
    Me.cmdOK = New System.Windows.Forms.Button()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.lbInner = New System.Windows.Forms.Label()
    Me.lbOuter = New System.Windows.Forms.Label()
    Me.lbColor = New System.Windows.Forms.Label()
    Me.cmdBorderColor = New System.Windows.Forms.Button()
    Me.aView = New PhotoMud.ctlViewCompare()
    CType(Me.trkOuter, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkInner, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmOuter, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmInner, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'cmdHelp
    '
    Me.cmdHelp.Anchor = System.Windows.Forms.AnchorStyles.Left
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(236, 456)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 39)
    Me.cmdHelp.TabIndex = 11
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'trkOuter
    '
    Me.trkOuter.Anchor = System.Windows.Forms.AnchorStyles.Left
    Me.trkOuter.AutoSize = False
    Me.trkOuter.Location = New System.Drawing.Point(15, 157)
    Me.trkOuter.Maximum = 100
    Me.trkOuter.Minimum = 1
    Me.trkOuter.Name = "trkOuter"
    Me.trkOuter.Size = New System.Drawing.Size(178, 24)
    Me.trkOuter.TabIndex = 3
    Me.trkOuter.TickStyle = System.Windows.Forms.TickStyle.None
    Me.ToolTip1.SetToolTip(Me.trkOuter, "Thickness of the outer border")
    Me.trkOuter.Value = 1
    '
    'trkInner
    '
    Me.trkInner.Anchor = System.Windows.Forms.AnchorStyles.Left
    Me.trkInner.AutoSize = False
    Me.trkInner.Location = New System.Drawing.Point(15, 229)
    Me.trkInner.Maximum = 100
    Me.trkInner.Minimum = 1
    Me.trkInner.Name = "trkInner"
    Me.trkInner.Size = New System.Drawing.Size(178, 24)
    Me.trkInner.TabIndex = 6
    Me.trkInner.TickStyle = System.Windows.Forms.TickStyle.None
    Me.ToolTip1.SetToolTip(Me.trkInner, "Thickness of the inner border")
    Me.trkInner.Value = 1
    '
    'nmOuter
    '
    Me.nmOuter.Anchor = System.Windows.Forms.AnchorStyles.Left
    Me.nmOuter.Location = New System.Drawing.Point(202, 155)
    Me.nmOuter.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nmOuter.Name = "nmOuter"
    Me.nmOuter.Size = New System.Drawing.Size(59, 25)
    Me.nmOuter.TabIndex = 4
    Me.ToolTip1.SetToolTip(Me.nmOuter, "Thickness of the outer border")
    Me.nmOuter.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'nmInner
    '
    Me.nmInner.Anchor = System.Windows.Forms.AnchorStyles.Left
    Me.nmInner.Location = New System.Drawing.Point(202, 228)
    Me.nmInner.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nmInner.Name = "nmInner"
    Me.nmInner.Size = New System.Drawing.Size(59, 25)
    Me.nmInner.TabIndex = 7
    Me.ToolTip1.SetToolTip(Me.nmInner, "Thickness of the inner border")
    Me.nmInner.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'opt3D
    '
    Me.opt3D.AutoSize = True
    Me.opt3D.Location = New System.Drawing.Point(38, 324)
    Me.opt3D.Name = "opt3D"
    Me.opt3D.Size = New System.Drawing.Size(95, 21)
    Me.opt3D.TabIndex = 37
    Me.opt3D.TabStop = True
    Me.opt3D.Text = "&3D Frame"
    Me.ToolTip1.SetToolTip(Me.opt3D, "Option for 3-dimenional appearance of the border")
    Me.opt3D.UseVisualStyleBackColor = True
    '
    'optRaise
    '
    Me.optRaise.AutoSize = True
    Me.optRaise.Location = New System.Drawing.Point(38, 354)
    Me.optRaise.Name = "optRaise"
    Me.optRaise.Size = New System.Drawing.Size(119, 21)
    Me.optRaise.TabIndex = 38
    Me.optRaise.TabStop = True
    Me.optRaise.Text = "&Bevel (raised)"
    Me.ToolTip1.SetToolTip(Me.optRaise, "Option for a Windows button-style border")
    Me.optRaise.UseVisualStyleBackColor = True
    '
    'optButton
    '
    Me.optButton.AutoSize = True
    Me.optButton.Location = New System.Drawing.Point(38, 384)
    Me.optButton.Name = "optButton"
    Me.optButton.Size = New System.Drawing.Size(71, 21)
    Me.optButton.TabIndex = 39
    Me.optButton.TabStop = True
    Me.optButton.Text = "&Button"
    Me.ToolTip1.SetToolTip(Me.optButton, "Option for a single-line border")
    Me.optButton.UseVisualStyleBackColor = True
    '
    'opt2D
    '
    Me.opt2D.AutoSize = True
    Me.opt2D.Location = New System.Drawing.Point(38, 294)
    Me.opt2D.Name = "opt2D"
    Me.opt2D.Size = New System.Drawing.Size(96, 21)
    Me.opt2D.TabIndex = 40
    Me.opt2D.TabStop = True
    Me.opt2D.Text = "&2D Border"
    Me.ToolTip1.SetToolTip(Me.opt2D, "Option for flat or 2-dimenional appearance of the border")
    Me.opt2D.UseVisualStyleBackColor = True
    '
    'cmdOK
    '
    Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Left
    Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdOK.Location = New System.Drawing.Point(18, 459)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(91, 36)
    Me.cmdOK.TabIndex = 9
    Me.cmdOK.Text = "&OK"
    Me.cmdOK.UseVisualStyleBackColor = False
    '
    'cmdCancel
    '
    Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Left
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdCancel.Location = New System.Drawing.Point(128, 459)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(91, 36)
    Me.cmdCancel.TabIndex = 10
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'lbInner
    '
    Me.lbInner.Anchor = System.Windows.Forms.AnchorStyles.Left
    Me.lbInner.AutoSize = True
    Me.lbInner.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbInner.Location = New System.Drawing.Point(25, 210)
    Me.lbInner.Name = "lbInner"
    Me.lbInner.Size = New System.Drawing.Size(162, 17)
    Me.lbInner.TabIndex = 5
    Me.lbInner.Text = "&Inner Thickness (pixels)"
    '
    'lbOuter
    '
    Me.lbOuter.Anchor = System.Windows.Forms.AnchorStyles.Left
    Me.lbOuter.AutoSize = True
    Me.lbOuter.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbOuter.Location = New System.Drawing.Point(25, 137)
    Me.lbOuter.Name = "lbOuter"
    Me.lbOuter.Size = New System.Drawing.Size(167, 17)
    Me.lbOuter.TabIndex = 2
    Me.lbOuter.Text = "&Outer Thickness (pixels)"
    '
    'lbColor
    '
    Me.lbColor.Anchor = System.Windows.Forms.AnchorStyles.Left
    Me.lbColor.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbColor.Location = New System.Drawing.Point(25, 71)
    Me.lbColor.Name = "lbColor"
    Me.lbColor.Size = New System.Drawing.Size(116, 21)
    Me.lbColor.TabIndex = 0
    Me.lbColor.Text = "&Border Color:"
    '
    'cmdBorderColor
    '
    Me.cmdBorderColor.Anchor = System.Windows.Forms.AnchorStyles.Left
    Me.cmdBorderColor.BackColor = System.Drawing.SystemColors.HotTrack
    Me.cmdBorderColor.Location = New System.Drawing.Point(143, 63)
    Me.cmdBorderColor.Name = "cmdBorderColor"
    Me.cmdBorderColor.Size = New System.Drawing.Size(61, 33)
    Me.cmdBorderColor.TabIndex = 1
    Me.cmdBorderColor.UseVisualStyleBackColor = False
    '
    'aView
    '
    Me.aView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.aView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.aView.Location = New System.Drawing.Point(303, 12)
    Me.aView.Name = "aView"
    Me.aView.NextButtons = False
    Me.aView.SingleView = False
    Me.aView.Size = New System.Drawing.Size(525, 501)
    Me.aView.TabIndex = 41
    Me.aView.zoomFactor = 0.0R
    '
    'frmBorder
    '
    Me.AcceptButton = Me.cmdOK
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(840, 525)
    Me.Controls.Add(Me.aView)
    Me.Controls.Add(Me.opt2D)
    Me.Controls.Add(Me.optButton)
    Me.Controls.Add(Me.optRaise)
    Me.Controls.Add(Me.opt3D)
    Me.Controls.Add(Me.cmdBorderColor)
    Me.Controls.Add(Me.nmInner)
    Me.Controls.Add(Me.nmOuter)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.trkInner)
    Me.Controls.Add(Me.trkOuter)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.lbInner)
    Me.Controls.Add(Me.lbOuter)
    Me.Controls.Add(Me.lbColor)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 22)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmBorder"
    Me.ShowInTaskbar = False
    Me.Text = "Photo Border"
    CType(Me.trkOuter, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkInner, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmOuter, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmInner, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

End Sub
 Friend WithEvents trkOuter As System.Windows.Forms.TrackBar
 Friend WithEvents trkInner As System.Windows.Forms.TrackBar
 Public WithEvents cmdHelp As System.Windows.Forms.Button
 Friend WithEvents nmOuter As System.Windows.Forms.NumericUpDown
 Friend WithEvents nmInner As System.Windows.Forms.NumericUpDown
 Friend WithEvents cmdBorderColor As System.Windows.Forms.Button
 Friend WithEvents opt3D As System.Windows.Forms.RadioButton
 Friend WithEvents optRaise As System.Windows.Forms.RadioButton
 Friend WithEvents optButton As System.Windows.Forms.RadioButton
 Friend WithEvents opt2D As System.Windows.Forms.RadioButton
 Friend WithEvents aView As PhotoMud.ctlViewCompare
#End Region
End Class