<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmRedeye
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
  Public WithEvents _lbRedeye_0 As Label
  ' Public WithEvents lead1 As AxLEADArray
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRedeye))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.trkThreshold = New System.Windows.Forms.TrackBar()
    Me.nmThreshold = New System.Windows.Forms.NumericUpDown()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.cmdOK = New System.Windows.Forms.Button()
    Me._lbRedeye_0 = New System.Windows.Forms.Label()
    Me.cmdRemoveRedeye = New System.Windows.Forms.Button()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.aview = New PhotoMud.ctlViewCompare()
    CType(Me.trkThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'cmdHelp
    '
    Me.cmdHelp.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(552, 484)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 41)
    Me.cmdHelp.TabIndex = 10
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'trkThreshold
    '
    Me.trkThreshold.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.trkThreshold.AutoSize = False
    Me.trkThreshold.Location = New System.Drawing.Point(140, 492)
    Me.trkThreshold.Maximum = 255
    Me.trkThreshold.Minimum = 1
    Me.trkThreshold.Name = "trkThreshold"
    Me.trkThreshold.Size = New System.Drawing.Size(171, 22)
    Me.trkThreshold.TabIndex = 1
    Me.trkThreshold.TickStyle = System.Windows.Forms.TickStyle.None
    Me.ToolTip1.SetToolTip(Me.trkThreshold, "Threshold for red-eye. Use a lower number to include more area to be changed.")
    Me.trkThreshold.Value = 1
    '
    'nmThreshold
    '
    Me.nmThreshold.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.nmThreshold.Location = New System.Drawing.Point(311, 489)
    Me.nmThreshold.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
    Me.nmThreshold.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nmThreshold.Name = "nmThreshold"
    Me.nmThreshold.Size = New System.Drawing.Size(60, 25)
    Me.nmThreshold.TabIndex = 2
    Me.ToolTip1.SetToolTip(Me.nmThreshold, "Threshold for red-eye. Use a lower number to include more area to be changed.")
    Me.nmThreshold.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'cmdCancel
    '
    Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdCancel.Location = New System.Drawing.Point(866, 476)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(101, 49)
    Me.cmdCancel.TabIndex = 9
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'cmdOK
    '
    Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdOK.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdOK.Location = New System.Drawing.Point(742, 476)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(101, 49)
    Me.cmdOK.TabIndex = 8
    Me.cmdOK.Text = "&OK"
    Me.cmdOK.UseVisualStyleBackColor = False
    '
    '_lbRedeye_0
    '
    Me._lbRedeye_0.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me._lbRedeye_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me._lbRedeye_0.Location = New System.Drawing.Point(140, 471)
    Me._lbRedeye_0.Name = "_lbRedeye_0"
    Me._lbRedeye_0.Size = New System.Drawing.Size(166, 21)
    Me._lbRedeye_0.TabIndex = 0
    Me._lbRedeye_0.Text = "&Threshold"
    '
    'cmdRemoveRedeye
    '
    Me.cmdRemoveRedeye.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdRemoveRedeye.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdRemoveRedeye.Location = New System.Drawing.Point(618, 476)
    Me.cmdRemoveRedeye.Name = "cmdRemoveRedeye"
    Me.cmdRemoveRedeye.Size = New System.Drawing.Size(101, 49)
    Me.cmdRemoveRedeye.TabIndex = 59
    Me.cmdRemoveRedeye.Text = "&Remove Redeye"
    Me.cmdRemoveRedeye.UseVisualStyleBackColor = False
    '
    'Label2
    '
    Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label2.Location = New System.Drawing.Point(108, 372)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(263, 17)
    Me.Label2.TabIndex = 14
    Me.Label2.Text = "Zoom to the eye or eyes to be modified."
    '
    'aview
    '
    Me.aview.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.aview.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.aview.Location = New System.Drawing.Point(3, 2)
    Me.aview.Name = "aview"
    Me.aview.NextButtons = False
    Me.aview.pCenter = New System.Drawing.Point(0, 0)
    Me.aview.SingleView = False
    Me.aview.Size = New System.Drawing.Size(976, 425)
    Me.aview.TabIndex = 60
    Me.aview.zoomFactor = 0.0R
    '
    'frmRedeye
    '
    Me.AcceptButton = Me.cmdOK
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(982, 564)
    Me.Controls.Add(Me.cmdRemoveRedeye)
    Me.Controls.Add(Me.nmThreshold)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.trkThreshold)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me._lbRedeye_0)
    Me.Controls.Add(Me.aview)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 30)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmRedeye"
    Me.ShowInTaskbar = False
    Me.Text = "Red Eye Removal"
    CType(Me.trkThreshold, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmThreshold, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

End Sub
 Friend WithEvents trkThreshold As System.Windows.Forms.TrackBar
  Public WithEvents cmdHelp As System.Windows.Forms.Button
 Friend WithEvents nmThreshold As System.Windows.Forms.NumericUpDown
  Public WithEvents cmdRemoveRedeye As System.Windows.Forms.Button
  Friend WithEvents aview As PhotoMud.ctlViewCompare
  Public WithEvents Label2 As System.Windows.Forms.Label
#End Region
End Class