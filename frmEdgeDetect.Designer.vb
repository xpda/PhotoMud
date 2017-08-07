<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmEdgeDetect
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
  Public WithEvents lbIntensity As Label
  ' Public WithEvents lead1 As AxLEADArray
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEdgeDetect))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.trkIntensity = New System.Windows.Forms.TrackBar()
    Me.nmIntensity = New System.Windows.Forms.NumericUpDown()
    Me.nmMinThreshold = New System.Windows.Forms.NumericUpDown()
    Me.trkMinThreshold = New System.Windows.Forms.TrackBar()
    Me.nmMaxThreshold = New System.Windows.Forms.NumericUpDown()
    Me.trkMaxThreshold = New System.Windows.Forms.TrackBar()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.cmdOK = New System.Windows.Forms.Button()
    Me.lbIntensity = New System.Windows.Forms.Label()
    Me.aView = New PhotoMud.ctlViewCompare()
    Me.lbMinThreshold = New System.Windows.Forms.Label()
    Me.lbMaxThreshold = New System.Windows.Forms.Label()
    CType(Me.trkIntensity, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmIntensity, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmMinThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkMinThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmMaxThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkMaxThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'cmdHelp
    '
    Me.cmdHelp.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(904, 455)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(46, 46)
    Me.cmdHelp.TabIndex = 11
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'trkIntensity
    '
    Me.trkIntensity.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.trkIntensity.AutoSize = False
    Me.trkIntensity.Location = New System.Drawing.Point(342, 460)
    Me.trkIntensity.Maximum = 100
    Me.trkIntensity.Name = "trkIntensity"
    Me.trkIntensity.Size = New System.Drawing.Size(201, 22)
    Me.trkIntensity.TabIndex = 5
    Me.trkIntensity.TickStyle = System.Windows.Forms.TickStyle.None
    Me.ToolTip1.SetToolTip(Me.trkIntensity, "The strength of the filter used. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "A higher value is usually more restrictive.")
    '
    'nmIntensity
    '
    Me.nmIntensity.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.nmIntensity.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.nmIntensity.Location = New System.Drawing.Point(546, 455)
    Me.nmIntensity.Name = "nmIntensity"
    Me.nmIntensity.Size = New System.Drawing.Size(56, 25)
    Me.nmIntensity.TabIndex = 6
    Me.ToolTip1.SetToolTip(Me.nmIntensity, "The strength of the filter used. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "A higher value is usually more restrictive.")
    '
    'nmMinThreshold
    '
    Me.nmMinThreshold.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.nmMinThreshold.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.nmMinThreshold.Location = New System.Drawing.Point(546, 501)
    Me.nmMinThreshold.Name = "nmMinThreshold"
    Me.nmMinThreshold.Size = New System.Drawing.Size(56, 25)
    Me.nmMinThreshold.TabIndex = 49
    Me.ToolTip1.SetToolTip(Me.nmMinThreshold, "The strength of the filter used. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "A higher value is usually more restrictive.")
    '
    'trkMinThreshold
    '
    Me.trkMinThreshold.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.trkMinThreshold.AutoSize = False
    Me.trkMinThreshold.Location = New System.Drawing.Point(342, 506)
    Me.trkMinThreshold.Maximum = 100
    Me.trkMinThreshold.Name = "trkMinThreshold"
    Me.trkMinThreshold.Size = New System.Drawing.Size(201, 22)
    Me.trkMinThreshold.TabIndex = 48
    Me.trkMinThreshold.TickStyle = System.Windows.Forms.TickStyle.None
    Me.ToolTip1.SetToolTip(Me.trkMinThreshold, "The strength of the filter used. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "A higher value is usually more restrictive.")
    '
    'nmMaxThreshold
    '
    Me.nmMaxThreshold.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.nmMaxThreshold.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.nmMaxThreshold.Location = New System.Drawing.Point(546, 546)
    Me.nmMaxThreshold.Name = "nmMaxThreshold"
    Me.nmMaxThreshold.Size = New System.Drawing.Size(56, 25)
    Me.nmMaxThreshold.TabIndex = 52
    Me.ToolTip1.SetToolTip(Me.nmMaxThreshold, "The strength of the filter used. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "A higher value is usually more restrictive.")
    '
    'trkMaxThreshold
    '
    Me.trkMaxThreshold.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.trkMaxThreshold.AutoSize = False
    Me.trkMaxThreshold.Location = New System.Drawing.Point(342, 551)
    Me.trkMaxThreshold.Maximum = 100
    Me.trkMaxThreshold.Name = "trkMaxThreshold"
    Me.trkMaxThreshold.Size = New System.Drawing.Size(201, 22)
    Me.trkMaxThreshold.TabIndex = 51
    Me.trkMaxThreshold.TickStyle = System.Windows.Forms.TickStyle.None
    Me.ToolTip1.SetToolTip(Me.trkMaxThreshold, "The strength of the filter used. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "A higher value is usually more restrictive.")
    '
    'cmdCancel
    '
    Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdCancel.Location = New System.Drawing.Point(760, 510)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(121, 46)
    Me.cmdCancel.TabIndex = 10
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'cmdOK
    '
    Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdOK.Location = New System.Drawing.Point(760, 455)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(121, 46)
    Me.cmdOK.TabIndex = 9
    Me.cmdOK.Text = "&OK"
    Me.cmdOK.UseVisualStyleBackColor = False
    '
    'lbIntensity
    '
    Me.lbIntensity.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.lbIntensity.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbIntensity.Location = New System.Drawing.Point(348, 443)
    Me.lbIntensity.Name = "lbIntensity"
    Me.lbIntensity.Size = New System.Drawing.Size(159, 21)
    Me.lbIntensity.TabIndex = 4
    Me.lbIntensity.Text = "&Intensity"
    '
    'aView
    '
    Me.aView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.aView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.aView.Location = New System.Drawing.Point(2, 2)
    Me.aView.Name = "aView"
    Me.aView.NextButtons = False
    Me.aView.pCenter = New System.Drawing.Point(0, 0)
    Me.aView.SingleView = False
    Me.aView.Size = New System.Drawing.Size(975, 426)
    Me.aView.TabIndex = 46
    Me.aView.zoomFactor = 0.0R
    '
    'lbMinThreshold
    '
    Me.lbMinThreshold.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.lbMinThreshold.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbMinThreshold.Location = New System.Drawing.Point(348, 489)
    Me.lbMinThreshold.Name = "lbMinThreshold"
    Me.lbMinThreshold.Size = New System.Drawing.Size(159, 21)
    Me.lbMinThreshold.TabIndex = 47
    Me.lbMinThreshold.Text = "Mi&nimum Threshold"
    '
    'lbMaxThreshold
    '
    Me.lbMaxThreshold.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.lbMaxThreshold.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbMaxThreshold.Location = New System.Drawing.Point(348, 534)
    Me.lbMaxThreshold.Name = "lbMaxThreshold"
    Me.lbMaxThreshold.Size = New System.Drawing.Size(159, 21)
    Me.lbMaxThreshold.TabIndex = 50
    Me.lbMaxThreshold.Text = "Ma&ximum Threshold"
    '
    'frmEdgeDetect
    '
    Me.AcceptButton = Me.cmdOK
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(978, 595)
    Me.Controls.Add(Me.nmMaxThreshold)
    Me.Controls.Add(Me.trkMaxThreshold)
    Me.Controls.Add(Me.lbMaxThreshold)
    Me.Controls.Add(Me.nmMinThreshold)
    Me.Controls.Add(Me.trkMinThreshold)
    Me.Controls.Add(Me.lbMinThreshold)
    Me.Controls.Add(Me.aView)
    Me.Controls.Add(Me.nmIntensity)
    Me.Controls.Add(Me.trkIntensity)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.lbIntensity)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 30)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmEdgeDetect"
    Me.ShowInTaskbar = False
    Me.Text = "Edge Detection"
    CType(Me.trkIntensity, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmIntensity, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmMinThreshold, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkMinThreshold, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmMaxThreshold, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkMaxThreshold, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

End Sub
  Public WithEvents cmdHelp As System.Windows.Forms.Button
  Friend WithEvents trkIntensity As System.Windows.Forms.TrackBar
  Friend WithEvents nmIntensity As System.Windows.Forms.NumericUpDown
  Friend WithEvents aView As PhotoMud.ctlViewCompare
  Friend WithEvents nmMinThreshold As System.Windows.Forms.NumericUpDown
  Friend WithEvents trkMinThreshold As System.Windows.Forms.TrackBar
  Public WithEvents lbMinThreshold As System.Windows.Forms.Label
  Friend WithEvents nmMaxThreshold As System.Windows.Forms.NumericUpDown
  Friend WithEvents trkMaxThreshold As System.Windows.Forms.TrackBar
  Public WithEvents lbMaxThreshold As System.Windows.Forms.Label
#End Region
End Class