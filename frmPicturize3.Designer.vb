<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPicturize3
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
  Public WithEvents cmdPrevious As Button
  Public WithEvents cmdFinish As Button
  Public WithEvents cmdStart As Button
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPicturize3))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.cmdStart = New System.Windows.Forms.Button()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.cmdPrevious = New System.Windows.Forms.Button()
    Me.cmdFinish = New System.Windows.Forms.Button()
    Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
    Me.ProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
    Me.pView = New PhotoMud.pViewer()
    Me.StatusStrip1.SuspendLayout()
    CType(Me.pView, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'cmdHelp
    '
    Me.cmdHelp.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(61, 460)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 41)
    Me.cmdHelp.TabIndex = 0
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'cmdStart
    '
    Me.cmdStart.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdStart.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdStart.Location = New System.Drawing.Point(249, 465)
    Me.cmdStart.Name = "cmdStart"
    Me.cmdStart.Size = New System.Drawing.Size(91, 31)
    Me.cmdStart.TabIndex = 2
    Me.cmdStart.Text = "&Start"
    Me.ToolTip1.SetToolTip(Me.cmdStart, "Create the photo mosaic")
    Me.cmdStart.UseVisualStyleBackColor = False
    '
    'cmdCancel
    '
    Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdCancel.Location = New System.Drawing.Point(368, 465)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(91, 31)
    Me.cmdCancel.TabIndex = 3
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'cmdPrevious
    '
    Me.cmdPrevious.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdPrevious.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdPrevious.Location = New System.Drawing.Point(131, 465)
    Me.cmdPrevious.Name = "cmdPrevious"
    Me.cmdPrevious.Size = New System.Drawing.Size(91, 31)
    Me.cmdPrevious.TabIndex = 1
    Me.cmdPrevious.Text = "&Previous"
    Me.cmdPrevious.UseVisualStyleBackColor = False
    '
    'cmdFinish
    '
    Me.cmdFinish.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdFinish.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdFinish.Location = New System.Drawing.Point(486, 465)
    Me.cmdFinish.Name = "cmdFinish"
    Me.cmdFinish.Size = New System.Drawing.Size(91, 31)
    Me.cmdFinish.TabIndex = 4
    Me.cmdFinish.Text = "&Finish"
    Me.cmdFinish.UseVisualStyleBackColor = False
    '
    'StatusStrip1
    '
    Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
    Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProgressBar1})
    Me.StatusStrip1.Location = New System.Drawing.Point(0, 521)
    Me.StatusStrip1.Name = "StatusStrip1"
    Me.StatusStrip1.Size = New System.Drawing.Size(643, 24)
    Me.StatusStrip1.TabIndex = 15
    Me.StatusStrip1.Text = "StatusStrip1"
    '
    'ProgressBar1
    '
    Me.ProgressBar1.AutoSize = False
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(500, 18)
    '
    'pView
    '
    Me.pView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pView.BackColor = System.Drawing.SystemColors.ControlDarkDark
    Me.pView.BitmapPath = Nothing
    Me.pView.FloaterOutline = False
    Me.pView.FloaterPath = Nothing
    Me.pView.FloaterPosition = New System.Drawing.Point(0, 0)
    Me.pView.FloaterVisible = True
    Me.pView.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low
    Me.pView.Location = New System.Drawing.Point(12, 13)
    Me.pView.Name = "pView"
    Me.pView.SelectionVisible = True
    Me.pView.Size = New System.Drawing.Size(615, 426)
    Me.pView.TabIndex = 16
    Me.pView.TabStop = False
    Me.pView.ZoomFactor = 1.0R
    '
    'frmPicturize3
    '
    Me.AcceptButton = Me.cmdFinish
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(643, 545)
    Me.Controls.Add(Me.pView)
    Me.Controls.Add(Me.StatusStrip1)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.cmdPrevious)
    Me.Controls.Add(Me.cmdFinish)
    Me.Controls.Add(Me.cmdStart)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 23)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmPicturize3"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Photo Mosaic"
    Me.StatusStrip1.ResumeLayout(False)
    Me.StatusStrip1.PerformLayout()
    CType(Me.pView, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ToolStripProgressBar
  Friend WithEvents pView As PhotoMud.pViewer
#End Region
End Class