<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSharpen
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
  Public WithEvents cmdHelp As Button
  Public WithEvents cmdCancel As Button
  Public WithEvents cmdOK As Button
  Public WithEvents lbUnsharp2 As Label
  Public WithEvents lbUnsharp1 As Label
  Public WithEvents lbUnsharp0 As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSharpen))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.trkSharpen0 = New System.Windows.Forms.TrackBar()
    Me.trkSharpen1 = New System.Windows.Forms.TrackBar()
    Me.trkSharpen2 = New System.Windows.Forms.TrackBar()
    Me.nmSharpen0 = New System.Windows.Forms.NumericUpDown()
    Me.nmSharpen1 = New System.Windows.Forms.NumericUpDown()
    Me.nmSharpen2 = New System.Windows.Forms.NumericUpDown()
    Me.optSharpen = New System.Windows.Forms.RadioButton()
    Me.optUnsharp = New System.Windows.Forms.RadioButton()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.cmdOK = New System.Windows.Forms.Button()
    Me.lbUnsharp2 = New System.Windows.Forms.Label()
    Me.lbUnsharp1 = New System.Windows.Forms.Label()
    Me.lbUnsharp0 = New System.Windows.Forms.Label()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.aview = New PhotoMud.ctlViewCompare()
    CType(Me.trkSharpen0, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkSharpen1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkSharpen2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmSharpen0, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmSharpen1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmSharpen2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBox1.SuspendLayout()
    Me.SuspendLayout()
    '
    'cmdHelp
    '
    Me.cmdHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(884, 505)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 41)
    Me.cmdHelp.TabIndex = 11
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'trkSharpen0
    '
    Me.trkSharpen0.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.trkSharpen0.AutoSize = False
    Me.trkSharpen0.Location = New System.Drawing.Point(446, 536)
    Me.trkSharpen0.Name = "trkSharpen0"
    Me.trkSharpen0.Size = New System.Drawing.Size(204, 25)
    Me.trkSharpen0.TabIndex = 3
    Me.trkSharpen0.TickStyle = System.Windows.Forms.TickStyle.None
    Me.ToolTip1.SetToolTip(Me.trkSharpen0, "Amount of sharpening")
    '
    'trkSharpen1
    '
    Me.trkSharpen1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.trkSharpen1.AutoSize = False
    Me.trkSharpen1.Location = New System.Drawing.Point(446, 576)
    Me.trkSharpen1.Name = "trkSharpen1"
    Me.trkSharpen1.Size = New System.Drawing.Size(204, 25)
    Me.trkSharpen1.TabIndex = 6
    Me.trkSharpen1.TickStyle = System.Windows.Forms.TickStyle.None
    Me.ToolTip1.SetToolTip(Me.trkSharpen1, "Sample size radius")
    '
    'trkSharpen2
    '
    Me.trkSharpen2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.trkSharpen2.AutoSize = False
    Me.trkSharpen2.Location = New System.Drawing.Point(446, 616)
    Me.trkSharpen2.Name = "trkSharpen2"
    Me.trkSharpen2.Size = New System.Drawing.Size(204, 25)
    Me.trkSharpen2.TabIndex = 9
    Me.trkSharpen2.TickStyle = System.Windows.Forms.TickStyle.None
    Me.ToolTip1.SetToolTip(Me.trkSharpen2, "Sharpening threshold")
    '
    'nmSharpen0
    '
    Me.nmSharpen0.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.nmSharpen0.Location = New System.Drawing.Point(656, 533)
    Me.nmSharpen0.Name = "nmSharpen0"
    Me.nmSharpen0.Size = New System.Drawing.Size(80, 25)
    Me.nmSharpen0.TabIndex = 4
    Me.ToolTip1.SetToolTip(Me.nmSharpen0, "Amount of sharpening")
    '
    'nmSharpen1
    '
    Me.nmSharpen1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.nmSharpen1.Location = New System.Drawing.Point(656, 573)
    Me.nmSharpen1.Name = "nmSharpen1"
    Me.nmSharpen1.Size = New System.Drawing.Size(80, 25)
    Me.nmSharpen1.TabIndex = 7
    Me.ToolTip1.SetToolTip(Me.nmSharpen1, "Sample size radius")
    '
    'nmSharpen2
    '
    Me.nmSharpen2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.nmSharpen2.Location = New System.Drawing.Point(656, 613)
    Me.nmSharpen2.Name = "nmSharpen2"
    Me.nmSharpen2.Size = New System.Drawing.Size(80, 25)
    Me.nmSharpen2.TabIndex = 10
    Me.ToolTip1.SetToolTip(Me.nmSharpen2, "Sharpening threshold")
    '
    'optSharpen
    '
    Me.optSharpen.AutoSize = True
    Me.optSharpen.Location = New System.Drawing.Point(27, 37)
    Me.optSharpen.Name = "optSharpen"
    Me.optSharpen.Size = New System.Drawing.Size(154, 21)
    Me.optSharpen.TabIndex = 0
    Me.optSharpen.TabStop = True
    Me.optSharpen.Text = "Normal Sharpening"
    Me.ToolTip1.SetToolTip(Me.optSharpen, "Normal sharpening method")
    Me.optSharpen.UseVisualStyleBackColor = True
    '
    'optUnsharp
    '
    Me.optUnsharp.AutoSize = True
    Me.optUnsharp.Location = New System.Drawing.Point(27, 64)
    Me.optUnsharp.Name = "optUnsharp"
    Me.optUnsharp.Size = New System.Drawing.Size(122, 21)
    Me.optUnsharp.TabIndex = 1
    Me.optUnsharp.TabStop = True
    Me.optUnsharp.Text = "Unsharp Mask"
    Me.ToolTip1.SetToolTip(Me.optUnsharp, """Unsharp Mask"" sharpening method")
    Me.optUnsharp.UseVisualStyleBackColor = True
    '
    'cmdCancel
    '
    Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdCancel.Location = New System.Drawing.Point(859, 615)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(96, 31)
    Me.cmdCancel.TabIndex = 13
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'cmdOK
    '
    Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdOK.Location = New System.Drawing.Point(859, 565)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(96, 31)
    Me.cmdOK.TabIndex = 12
    Me.cmdOK.Text = "&OK"
    Me.cmdOK.UseVisualStyleBackColor = False
    '
    'lbUnsharp2
    '
    Me.lbUnsharp2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbUnsharp2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbUnsharp2.Location = New System.Drawing.Point(368, 620)
    Me.lbUnsharp2.Name = "lbUnsharp2"
    Me.lbUnsharp2.Size = New System.Drawing.Size(76, 16)
    Me.lbUnsharp2.TabIndex = 8
    Me.lbUnsharp2.Text = "&Power"
    '
    'lbUnsharp1
    '
    Me.lbUnsharp1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbUnsharp1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbUnsharp1.Location = New System.Drawing.Point(368, 580)
    Me.lbUnsharp1.Name = "lbUnsharp1"
    Me.lbUnsharp1.Size = New System.Drawing.Size(76, 16)
    Me.lbUnsharp1.TabIndex = 5
    Me.lbUnsharp1.Text = "&Radius"
    '
    'lbUnsharp0
    '
    Me.lbUnsharp0.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbUnsharp0.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbUnsharp0.Location = New System.Drawing.Point(368, 540)
    Me.lbUnsharp0.Name = "lbUnsharp0"
    Me.lbUnsharp0.Size = New System.Drawing.Size(76, 16)
    Me.lbUnsharp0.TabIndex = 2
    Me.lbUnsharp0.Text = "&Amount"
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.optUnsharp)
    Me.GroupBox1.Controls.Add(Me.optSharpen)
    Me.GroupBox1.Location = New System.Drawing.Point(56, 521)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(214, 103)
    Me.GroupBox1.TabIndex = 45
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Sharpening Method"
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
    Me.aview.Size = New System.Drawing.Size(987, 501)
    Me.aview.TabIndex = 46
    Me.aview.zoomFactor = 0.0R
    '
    'frmSharpen
    '
    Me.AcceptButton = Me.cmdOK
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(993, 669)
    Me.Controls.Add(Me.aview)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.nmSharpen2)
    Me.Controls.Add(Me.nmSharpen1)
    Me.Controls.Add(Me.nmSharpen0)
    Me.Controls.Add(Me.trkSharpen2)
    Me.Controls.Add(Me.trkSharpen1)
    Me.Controls.Add(Me.trkSharpen0)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.lbUnsharp2)
    Me.Controls.Add(Me.lbUnsharp1)
    Me.Controls.Add(Me.lbUnsharp0)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 22)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmSharpen"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Sharpen"
    CType(Me.trkSharpen0, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkSharpen1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkSharpen2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmSharpen0, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmSharpen1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmSharpen2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.ResumeLayout(False)

End Sub
 Friend WithEvents trkSharpen0 As System.Windows.Forms.TrackBar
 Friend WithEvents trkSharpen1 As System.Windows.Forms.TrackBar
 Friend WithEvents trkSharpen2 As System.Windows.Forms.TrackBar
 Friend WithEvents nmSharpen0 As System.Windows.Forms.NumericUpDown
 Friend WithEvents nmSharpen1 As System.Windows.Forms.NumericUpDown
 Friend WithEvents nmSharpen2 As System.Windows.Forms.NumericUpDown
 Friend WithEvents optSharpen As System.Windows.Forms.RadioButton
 Friend WithEvents optUnsharp As System.Windows.Forms.RadioButton
 Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
 Friend WithEvents aview As PhotoMud.ctlViewCompare
#End Region
End Class