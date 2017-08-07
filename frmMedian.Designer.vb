<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmMedian
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
  Public WithEvents lbBlur As Label
  'Public WithEvents lead1 As AxLEADArray
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMedian))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.trkSample = New System.Windows.Forms.TrackBar()
    Me.nmSample = New System.Windows.Forms.NumericUpDown()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.cmdOK = New System.Windows.Forms.Button()
    Me.lbBlur = New System.Windows.Forms.Label()
    Me.aview = New PhotoMud.ctlViewCompare()
    CType(Me.trkSample, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmSample, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'cmdHelp
    '
    Me.cmdHelp.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(849, 434)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 39)
    Me.cmdHelp.TabIndex = 5
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'trkSample
    '
    Me.trkSample.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.trkSample.AutoSize = False
    Me.trkSample.Location = New System.Drawing.Point(101, 440)
    Me.trkSample.Maximum = 30
    Me.trkSample.Minimum = 1
    Me.trkSample.Name = "trkSample"
    Me.trkSample.Size = New System.Drawing.Size(221, 23)
    Me.trkSample.TabIndex = 1
    Me.trkSample.TickStyle = System.Windows.Forms.TickStyle.None
    Me.ToolTip1.SetToolTip(Me.trkSample, "Sample size for the filter, in pixels")
    Me.trkSample.Value = 1
    '
    'nmSample
    '
    Me.nmSample.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.nmSample.Location = New System.Drawing.Point(326, 437)
    Me.nmSample.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
    Me.nmSample.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nmSample.Name = "nmSample"
    Me.nmSample.Size = New System.Drawing.Size(58, 25)
    Me.nmSample.TabIndex = 2
    Me.ToolTip1.SetToolTip(Me.nmSample, "Sample size for the filter, in pixels")
    Me.nmSample.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'cmdCancel
    '
    Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdCancel.Location = New System.Drawing.Point(732, 442)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(91, 31)
    Me.cmdCancel.TabIndex = 4
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'cmdOK
    '
    Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdOK.Location = New System.Drawing.Point(732, 401)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(91, 31)
    Me.cmdOK.TabIndex = 3
    Me.cmdOK.Text = "&OK"
    Me.cmdOK.UseVisualStyleBackColor = False
    '
    'lbBlur
    '
    Me.lbBlur.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.lbBlur.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbBlur.Location = New System.Drawing.Point(170, 420)
    Me.lbBlur.Name = "lbBlur"
    Me.lbBlur.Size = New System.Drawing.Size(166, 16)
    Me.lbBlur.TabIndex = 0
    Me.lbBlur.Text = "&Sample Size"
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
    Me.aview.Size = New System.Drawing.Size(975, 394)
    Me.aview.TabIndex = 55
    Me.aview.zoomFactor = 0.0R
    '
    'frmMedian
    '
    Me.AcceptButton = Me.cmdOK
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(980, 497)
    Me.Controls.Add(Me.aview)
    Me.Controls.Add(Me.nmSample)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.trkSample)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.lbBlur)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 30)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmMedian"
    Me.ShowInTaskbar = False
    Me.Text = "Median Filter Noise Removal"
    CType(Me.trkSample, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmSample, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

End Sub
  Friend WithEvents trkSample As System.Windows.Forms.TrackBar
  Public WithEvents cmdHelp As System.Windows.Forms.Button
  Friend WithEvents nmSample As System.Windows.Forms.NumericUpDown
  Friend WithEvents aview As PhotoMud.ctlViewCompare
#End Region
End Class