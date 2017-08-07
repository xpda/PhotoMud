<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSlideShow
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
  Public WithEvents ImageList1 As ImageList
  Public WithEvents cmdHelp As Button
  Public WithEvents cmdPrevious As Button
  Public WithEvents cmdPlay As Button
  Public WithEvents cmdNext As Button
  Public WithEvents cmdExit As Button
  Public WithEvents Label1 As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSlideShow))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.cmdPrevious = New System.Windows.Forms.Button()
    Me.cmdPlay = New System.Windows.Forms.Button()
    Me.cmdNext = New System.Windows.Forms.Button()
    Me.cmdExit = New System.Windows.Forms.Button()
    Me.cmdOptions = New System.Windows.Forms.Button()
    Me.nmSlideRate = New System.Windows.Forms.NumericUpDown()
    Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
    Me.contextMnu = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.mnuPlay = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuPause = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuNext = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuPrevious = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuStop = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuRandom = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuOrderFilename = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuOrderPhotoDate = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuOrderFiledate = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuHideDescription = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuHideFilename = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuHidePhotoDate = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuOptions = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
    Me.lbDescription = New System.Windows.Forms.ToolStripStatusLabel()
    Me.lbFilename = New System.Windows.Forms.ToolStripStatusLabel()
    Me.lbPhotoDate = New System.Windows.Forms.ToolStripStatusLabel()
    Me.bkgLoad = New System.ComponentModel.BackgroundWorker()
    Me.Picture1 = New System.Windows.Forms.PictureBox()
    Me.Panel1 = New System.Windows.Forms.Panel()
    CType(Me.nmSlideRate, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.contextMnu.SuspendLayout()
    Me.StatusStrip1.SuspendLayout()
    CType(Me.Picture1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'cmdHelp
    '
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(254, 4)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(36, 36)
    Me.cmdHelp.TabIndex = 5
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'cmdPrevious
    '
    Me.cmdPrevious.BackgroundImage = CType(resources.GetObject("cmdPrevious.BackgroundImage"), System.Drawing.Image)
    Me.cmdPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
    Me.cmdPrevious.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdPrevious.Location = New System.Drawing.Point(3, 4)
    Me.cmdPrevious.Name = "cmdPrevious"
    Me.cmdPrevious.Size = New System.Drawing.Size(36, 36)
    Me.cmdPrevious.TabIndex = 0
    Me.cmdPrevious.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdPrevious, "Previous Slide")
    Me.cmdPrevious.UseVisualStyleBackColor = False
    '
    'cmdPlay
    '
    Me.cmdPlay.BackgroundImage = CType(resources.GetObject("cmdPlay.BackgroundImage"), System.Drawing.Image)
    Me.cmdPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
    Me.cmdPlay.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdPlay.Location = New System.Drawing.Point(45, 4)
    Me.cmdPlay.Name = "cmdPlay"
    Me.cmdPlay.Size = New System.Drawing.Size(36, 36)
    Me.cmdPlay.TabIndex = 1
    Me.cmdPlay.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdPlay, "Pause")
    Me.cmdPlay.UseVisualStyleBackColor = False
    '
    'cmdNext
    '
    Me.cmdNext.BackgroundImage = CType(resources.GetObject("cmdNext.BackgroundImage"), System.Drawing.Image)
    Me.cmdNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
    Me.cmdNext.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdNext.Location = New System.Drawing.Point(87, 4)
    Me.cmdNext.Name = "cmdNext"
    Me.cmdNext.Size = New System.Drawing.Size(36, 36)
    Me.cmdNext.TabIndex = 2
    Me.cmdNext.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdNext, "Next Slide")
    Me.cmdNext.UseVisualStyleBackColor = False
    '
    'cmdExit
    '
    Me.cmdExit.BackgroundImage = CType(resources.GetObject("cmdExit.BackgroundImage"), System.Drawing.Image)
    Me.cmdExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
    Me.cmdExit.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdExit.Location = New System.Drawing.Point(129, 4)
    Me.cmdExit.Name = "cmdExit"
    Me.cmdExit.Size = New System.Drawing.Size(36, 36)
    Me.cmdExit.TabIndex = 3
    Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdExit, "Stop")
    Me.cmdExit.UseVisualStyleBackColor = False
    '
    'cmdOptions
    '
    Me.cmdOptions.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdOptions.Location = New System.Drawing.Point(172, 4)
    Me.cmdOptions.Name = "cmdOptions"
    Me.cmdOptions.Size = New System.Drawing.Size(75, 36)
    Me.cmdOptions.TabIndex = 4
    Me.cmdOptions.TabStop = False
    Me.cmdOptions.Text = "&Options"
    Me.ToolTip1.SetToolTip(Me.cmdOptions, "Slide show options")
    Me.cmdOptions.UseVisualStyleBackColor = False
    '
    'nmSlideRate
    '
    Me.nmSlideRate.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.nmSlideRate.Location = New System.Drawing.Point(438, 12)
    Me.nmSlideRate.Maximum = New Decimal(New Integer() {240, 0, 0, 0})
    Me.nmSlideRate.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nmSlideRate.Name = "nmSlideRate"
    Me.nmSlideRate.Size = New System.Drawing.Size(50, 25)
    Me.nmSlideRate.TabIndex = 7
    Me.ToolTip1.SetToolTip(Me.nmSlideRate, "Number of seconds each photo is displayed")
    Me.nmSlideRate.Value = New Decimal(New Integer() {60, 0, 0, 0})
    '
    'ImageList1
    '
    Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.ImageList1.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.ImageList1.Images.SetKeyName(0, "pause")
    Me.ImageList1.Images.SetKeyName(1, "play")
    '
    'contextMnu
    '
    Me.contextMnu.ImageScalingSize = New System.Drawing.Size(20, 20)
    Me.contextMnu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPlay, Me.mnuPause, Me.mnuNext, Me.mnuPrevious, Me.mnuStop, Me.ToolStripSeparator1, Me.mnuRandom, Me.mnuOrderFilename, Me.mnuOrderPhotoDate, Me.mnuOrderFiledate, Me.mnuHideDescription, Me.mnuHideFilename, Me.mnuHidePhotoDate, Me.ToolStripSeparator2, Me.mnuOptions, Me.ToolStripSeparator3, Me.mnuHelp})
    Me.contextMnu.Name = "contextMnu"
    Me.contextMnu.Size = New System.Drawing.Size(222, 386)
    '
    'mnuPlay
    '
    Me.mnuPlay.Name = "mnuPlay"
    Me.mnuPlay.Size = New System.Drawing.Size(221, 26)
    Me.mnuPlay.Text = "Pla&y"
    '
    'mnuPause
    '
    Me.mnuPause.Name = "mnuPause"
    Me.mnuPause.Size = New System.Drawing.Size(221, 26)
    Me.mnuPause.Text = "&Pause"
    '
    'mnuNext
    '
    Me.mnuNext.Name = "mnuNext"
    Me.mnuNext.Size = New System.Drawing.Size(221, 26)
    Me.mnuNext.Text = "Ne&xt"
    '
    'mnuPrevious
    '
    Me.mnuPrevious.Name = "mnuPrevious"
    Me.mnuPrevious.Size = New System.Drawing.Size(221, 26)
    Me.mnuPrevious.Text = "Pre&vious"
    '
    'mnuStop
    '
    Me.mnuStop.Name = "mnuStop"
    Me.mnuStop.Size = New System.Drawing.Size(221, 26)
    Me.mnuStop.Text = "&Stop"
    '
    'ToolStripSeparator1
    '
    Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
    Me.ToolStripSeparator1.Size = New System.Drawing.Size(218, 6)
    '
    'mnuRandom
    '
    Me.mnuRandom.Name = "mnuRandom"
    Me.mnuRandom.Size = New System.Drawing.Size(221, 26)
    Me.mnuRandom.Text = "&Random Order"
    '
    'mnuOrderFilename
    '
    Me.mnuOrderFilename.Name = "mnuOrderFilename"
    Me.mnuOrderFilename.Size = New System.Drawing.Size(221, 26)
    Me.mnuOrderFilename.Text = "&Order by File Name"
    '
    'mnuOrderPhotoDate
    '
    Me.mnuOrderPhotoDate.Name = "mnuOrderPhotoDate"
    Me.mnuOrderPhotoDate.Size = New System.Drawing.Size(221, 26)
    Me.mnuOrderPhotoDate.Text = "O&rder by &Photo Date"
    '
    'mnuOrderFiledate
    '
    Me.mnuOrderFiledate.Name = "mnuOrderFiledate"
    Me.mnuOrderFiledate.Size = New System.Drawing.Size(221, 26)
    Me.mnuOrderFiledate.Text = "Order by File D&ate"
    '
    'mnuHideDescription
    '
    Me.mnuHideDescription.Name = "mnuHideDescription"
    Me.mnuHideDescription.Size = New System.Drawing.Size(221, 26)
    Me.mnuHideDescription.Text = "&Hide Description"
    '
    'mnuHideFilename
    '
    Me.mnuHideFilename.Name = "mnuHideFilename"
    Me.mnuHideFilename.Size = New System.Drawing.Size(221, 26)
    Me.mnuHideFilename.Text = "Hide &File Name"
    '
    'mnuHidePhotoDate
    '
    Me.mnuHidePhotoDate.Name = "mnuHidePhotoDate"
    Me.mnuHidePhotoDate.Size = New System.Drawing.Size(221, 26)
    Me.mnuHidePhotoDate.Text = "H&ide Photo Date"
    '
    'ToolStripSeparator2
    '
    Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
    Me.ToolStripSeparator2.Size = New System.Drawing.Size(218, 6)
    '
    'mnuOptions
    '
    Me.mnuOptions.Name = "mnuOptions"
    Me.mnuOptions.Size = New System.Drawing.Size(221, 26)
    Me.mnuOptions.Text = "&Options"
    '
    'ToolStripSeparator3
    '
    Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
    Me.ToolStripSeparator3.Size = New System.Drawing.Size(218, 6)
    '
    'mnuHelp
    '
    Me.mnuHelp.Name = "mnuHelp"
    Me.mnuHelp.Size = New System.Drawing.Size(221, 26)
    Me.mnuHelp.Text = "&Help"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.BackColor = System.Drawing.Color.Transparent
    Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label1.Location = New System.Drawing.Point(296, 15)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(129, 17)
    Me.Label1.TabIndex = 6
    Me.Label1.Text = "&Seconds per slide:"
    Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
    '
    'StatusStrip1
    '
    Me.StatusStrip1.ContextMenuStrip = Me.contextMnu
    Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
    Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lbDescription, Me.lbFilename, Me.lbPhotoDate})
    Me.StatusStrip1.Location = New System.Drawing.Point(0, 605)
    Me.StatusStrip1.Name = "StatusStrip1"
    Me.StatusStrip1.Size = New System.Drawing.Size(1005, 25)
    Me.StatusStrip1.TabIndex = 17
    Me.StatusStrip1.Text = "StatusStrip1"
    '
    'lbDescription
    '
    Me.lbDescription.Name = "lbDescription"
    Me.lbDescription.Size = New System.Drawing.Size(85, 20)
    Me.lbDescription.Text = "Description"
    '
    'lbFilename
    '
    Me.lbFilename.Name = "lbFilename"
    Me.lbFilename.Size = New System.Drawing.Size(69, 20)
    Me.lbFilename.Text = "Filename"
    '
    'lbPhotoDate
    '
    Me.lbPhotoDate.Name = "lbPhotoDate"
    Me.lbPhotoDate.Size = New System.Drawing.Size(80, 20)
    Me.lbPhotoDate.Text = "PhotoDate"
    '
    'bkgLoad
    '
    Me.bkgLoad.WorkerSupportsCancellation = True
    '
    'Picture1
    '
    Me.Picture1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Picture1.Location = New System.Drawing.Point(0, 0)
    Me.Picture1.Name = "Picture1"
    Me.Picture1.Size = New System.Drawing.Size(1005, 604)
    Me.Picture1.TabIndex = 18
    Me.Picture1.TabStop = False
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.cmdHelp)
    Me.Panel1.Controls.Add(Me.cmdPlay)
    Me.Panel1.Controls.Add(Me.cmdOptions)
    Me.Panel1.Controls.Add(Me.cmdNext)
    Me.Panel1.Controls.Add(Me.Label1)
    Me.Panel1.Controls.Add(Me.cmdPrevious)
    Me.Panel1.Controls.Add(Me.nmSlideRate)
    Me.Panel1.Controls.Add(Me.cmdExit)
    Me.Panel1.Location = New System.Drawing.Point(511, 586)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(494, 44)
    Me.Panel1.TabIndex = 19
    '
    'frmSlideShow
    '
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.ClientSize = New System.Drawing.Size(1005, 630)
    Me.ContextMenuStrip = Me.contextMnu
    Me.ControlBox = False
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.Picture1)
    Me.Controls.Add(Me.StatusStrip1)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(1, 1)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmSlideShow"
    Me.ShowInTaskbar = False
    CType(Me.nmSlideRate, System.ComponentModel.ISupportInitialize).EndInit()
    Me.contextMnu.ResumeLayout(False)
    Me.StatusStrip1.ResumeLayout(False)
    Me.StatusStrip1.PerformLayout()
    CType(Me.Picture1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

End Sub
 Friend WithEvents nmSlideRate As System.Windows.Forms.NumericUpDown
 Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
 Friend WithEvents lbDescription As System.Windows.Forms.ToolStripStatusLabel
 Friend WithEvents lbFilename As System.Windows.Forms.ToolStripStatusLabel
 Public WithEvents cmdOptions As System.Windows.Forms.Button
 Friend WithEvents contextMnu As System.Windows.Forms.ContextMenuStrip
 Friend WithEvents mnuPlay As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuPause As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuNext As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuPrevious As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuStop As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuOptions As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
 Friend WithEvents mnuRandom As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuHideDescription As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuHideFilename As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
 Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
 Friend WithEvents mnuHelp As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents bkgLoad As System.ComponentModel.BackgroundWorker
 Friend WithEvents Picture1 As System.Windows.Forms.PictureBox
 Friend WithEvents Panel1 As System.Windows.Forms.Panel
 Friend WithEvents lbPhotoDate As System.Windows.Forms.ToolStripStatusLabel
 Friend WithEvents mnuOrderFilename As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuOrderPhotoDate As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuOrderFiledate As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuHidePhotoDate As System.Windows.Forms.ToolStripMenuItem
#End Region
End Class