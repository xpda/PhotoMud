<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmDupSearch
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
  Public WithEvents cmdBrowse As Button
  Public WithEvents cmdClose As Button
  Public WithEvents ListView1 As ListView
  Public WithEvents cmdStart As Button
  Public WithEvents chkSubfolders As CheckBox
  Public WithEvents txSearch As TextBox
  Public WithEvents _lbTolerance_2 As Label
  Public WithEvents _lbTolerance_1 As Label
  Public WithEvents _lbTolerance_0 As Label
  Public WithEvents lbSearch As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDupSearch))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.cmdBrowse = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.cmdStart = New System.Windows.Forms.Button()
    Me.chkSubfolders = New System.Windows.Forms.CheckBox()
    Me.txSearch = New System.Windows.Forms.TextBox()
    Me.trkTolerance = New System.Windows.Forms.TrackBar()
    Me.cmdOpenFile = New System.Windows.Forms.Button()
    Me.txFilename = New System.Windows.Forms.TextBox()
    Me.cmdDelete1 = New System.Windows.Forms.Button()
    Me.cmdDelete0 = New System.Windows.Forms.Button()
    Me.ListView1 = New System.Windows.Forms.ListView()
    Me._lbTolerance_2 = New System.Windows.Forms.Label()
    Me._lbTolerance_1 = New System.Windows.Forms.Label()
    Me._lbTolerance_0 = New System.Windows.Forms.Label()
    Me.lbSearch = New System.Windows.Forms.Label()
    Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
    Me.ProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
    Me.lbFile = New System.Windows.Forms.Label()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.pView1 = New PhotoMud.pViewer()
    Me.pView0 = New PhotoMud.pViewer()
    Me.lbFilename1 = New System.Windows.Forms.Label()
    Me.lbFilename0 = New System.Windows.Forms.Label()
    CType(Me.trkTolerance, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.StatusStrip1.SuspendLayout()
    Me.Panel1.SuspendLayout()
    CType(Me.pView1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pView0, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'cmdHelp
    '
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(225, 282)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 41)
    Me.cmdHelp.TabIndex = 6
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'cmdBrowse
    '
    Me.cmdBrowse.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdBrowse.Location = New System.Drawing.Point(201, 25)
    Me.cmdBrowse.Name = "cmdBrowse"
    Me.cmdBrowse.Size = New System.Drawing.Size(79, 25)
    Me.cmdBrowse.TabIndex = 1
    Me.cmdBrowse.Text = "&Browse..."
    Me.ToolTip1.SetToolTip(Me.cmdBrowse, "Browse for the path")
    Me.cmdBrowse.UseVisualStyleBackColor = False
    '
    'cmdClose
    '
    Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdClose.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdClose.Location = New System.Drawing.Point(121, 290)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(91, 33)
    Me.cmdClose.TabIndex = 8
    Me.cmdClose.Text = "&Close"
    Me.ToolTip1.SetToolTip(Me.cmdClose, "Return")
    Me.cmdClose.UseVisualStyleBackColor = False
    '
    'cmdStart
    '
    Me.cmdStart.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdStart.Location = New System.Drawing.Point(20, 290)
    Me.cmdStart.Name = "cmdStart"
    Me.cmdStart.Size = New System.Drawing.Size(91, 33)
    Me.cmdStart.TabIndex = 7
    Me.cmdStart.Text = "&Search"
    Me.ToolTip1.SetToolTip(Me.cmdStart, "Begin the search")
    Me.cmdStart.UseVisualStyleBackColor = False
    '
    'chkSubfolders
    '
    Me.chkSubfolders.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkSubfolders.Location = New System.Drawing.Point(12, 93)
    Me.chkSubfolders.Name = "chkSubfolders"
    Me.chkSubfolders.Size = New System.Drawing.Size(198, 21)
    Me.chkSubfolders.TabIndex = 3
    Me.chkSubfolders.Text = "Search S&ubfolders"
    Me.ToolTip1.SetToolTip(Me.chkSubfolders, "Check to search the subfolders under the above path")
    Me.chkSubfolders.UseVisualStyleBackColor = False
    '
    'txSearch
    '
    Me.txSearch.AcceptsReturn = True
    Me.txSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
    Me.txSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories
    Me.txSearch.BackColor = System.Drawing.SystemColors.Window
    Me.txSearch.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txSearch.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txSearch.Location = New System.Drawing.Point(12, 53)
    Me.txSearch.MaxLength = 0
    Me.txSearch.Name = "txSearch"
    Me.txSearch.Size = New System.Drawing.Size(269, 25)
    Me.txSearch.TabIndex = 2
    Me.txSearch.Text = ""
    Me.ToolTip1.SetToolTip(Me.txSearch, "This folder will be searched for duplicates")
    '
    'trkTolerance
    '
    Me.trkTolerance.AutoSize = False
    Me.trkTolerance.LargeChange = 1
    Me.trkTolerance.Location = New System.Drawing.Point(12, 208)
    Me.trkTolerance.Maximum = 6
    Me.trkTolerance.Minimum = 1
    Me.trkTolerance.Name = "trkTolerance"
    Me.trkTolerance.Size = New System.Drawing.Size(277, 23)
    Me.trkTolerance.TabIndex = 5
    Me.ToolTip1.SetToolTip(Me.trkTolerance, "A high tolerance results in more matches, requires less similarity." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "A lower tole" & _
        "race gives fewer matches and requires a closer match.")
    Me.trkTolerance.Value = 4
    '
    'cmdOpenFile
    '
    Me.cmdOpenFile.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdOpenFile.Location = New System.Drawing.Point(201, 130)
    Me.cmdOpenFile.Name = "cmdOpenFile"
    Me.cmdOpenFile.Size = New System.Drawing.Size(79, 25)
    Me.cmdOpenFile.TabIndex = 29
    Me.cmdOpenFile.Text = "&Browse..."
    Me.ToolTip1.SetToolTip(Me.cmdOpenFile, "Browse for the path")
    Me.cmdOpenFile.UseVisualStyleBackColor = False
    '
    'txFilename
    '
    Me.txFilename.AcceptsReturn = True
    Me.txFilename.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
    Me.txFilename.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem
    Me.txFilename.BackColor = System.Drawing.SystemColors.Window
    Me.txFilename.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txFilename.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txFilename.Location = New System.Drawing.Point(12, 158)
    Me.txFilename.MaxLength = 0
    Me.txFilename.Name = "txFilename"
    Me.txFilename.Size = New System.Drawing.Size(269, 25)
    Me.txFilename.TabIndex = 30
    Me.ToolTip1.SetToolTip(Me.txFilename, "This folder will be searched for duplicates")
    '
    'cmdDelete1
    '
    Me.cmdDelete1.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.cmdDelete1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdDelete1.Image = CType(resources.GetObject("cmdDelete1.Image"), System.Drawing.Image)
    Me.cmdDelete1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.cmdDelete1.Location = New System.Drawing.Point(361, 267)
    Me.cmdDelete1.Name = "cmdDelete1"
    Me.cmdDelete1.Size = New System.Drawing.Size(36, 36)
    Me.cmdDelete1.TabIndex = 34
    Me.cmdDelete1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdDelete1, "Delete this file from the disk")
    Me.cmdDelete1.UseVisualStyleBackColor = False
    '
    'cmdDelete0
    '
    Me.cmdDelete0.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.cmdDelete0.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdDelete0.Image = CType(resources.GetObject("cmdDelete0.Image"), System.Drawing.Image)
    Me.cmdDelete0.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.cmdDelete0.Location = New System.Drawing.Point(0, 267)
    Me.cmdDelete0.Name = "cmdDelete0"
    Me.cmdDelete0.Size = New System.Drawing.Size(36, 36)
    Me.cmdDelete0.TabIndex = 33
    Me.cmdDelete0.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdDelete0, "Delete this file from the disk")
    Me.cmdDelete0.UseVisualStyleBackColor = False
    '
    'ListView1
    '
    Me.ListView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ListView1.BackColor = System.Drawing.SystemColors.Window
    Me.ListView1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.ListView1.FullRowSelect = True
    Me.ListView1.Location = New System.Drawing.Point(0, 341)
    Me.ListView1.MultiSelect = False
    Me.ListView1.Name = "ListView1"
    Me.ListView1.Size = New System.Drawing.Size(1016, 312)
    Me.ListView1.TabIndex = 5
    Me.ListView1.UseCompatibleStateImageBehavior = False
    '
    '_lbTolerance_2
    '
    Me._lbTolerance_2.Font = New System.Drawing.Font("Arial", 9.0!)
    Me._lbTolerance_2.Location = New System.Drawing.Point(205, 234)
    Me._lbTolerance_2.Name = "_lbTolerance_2"
    Me._lbTolerance_2.Size = New System.Drawing.Size(58, 21)
    Me._lbTolerance_2.TabIndex = 18
    Me._lbTolerance_2.Text = "&High"
    Me._lbTolerance_2.TextAlign = System.Drawing.ContentAlignment.TopRight
    '
    '_lbTolerance_1
    '
    Me._lbTolerance_1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me._lbTolerance_1.Location = New System.Drawing.Point(9, 234)
    Me._lbTolerance_1.Name = "_lbTolerance_1"
    Me._lbTolerance_1.Size = New System.Drawing.Size(53, 21)
    Me._lbTolerance_1.TabIndex = 17
    Me._lbTolerance_1.Text = "&Low"
    '
    '_lbTolerance_0
    '
    Me._lbTolerance_0.AutoSize = True
    Me._lbTolerance_0.Font = New System.Drawing.Font("Arial", 9.0!)
    Me._lbTolerance_0.Location = New System.Drawing.Point(72, 235)
    Me._lbTolerance_0.Name = "_lbTolerance_0"
    Me._lbTolerance_0.Size = New System.Drawing.Size(122, 17)
    Me._lbTolerance_0.TabIndex = 4
    Me._lbTolerance_0.Text = "Search &Tolerance"
    Me._lbTolerance_0.TextAlign = System.Drawing.ContentAlignment.TopCenter
    '
    'lbSearch
    '
    Me.lbSearch.AutoSize = True
    Me.lbSearch.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbSearch.Location = New System.Drawing.Point(9, 33)
    Me.lbSearch.Name = "lbSearch"
    Me.lbSearch.Size = New System.Drawing.Size(104, 17)
    Me.lbSearch.TabIndex = 0
    Me.lbSearch.Text = "Search &Folder:"
    '
    'StatusStrip1
    '
    Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
    Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProgressBar1})
    Me.StatusStrip1.Location = New System.Drawing.Point(0, 656)
    Me.StatusStrip1.Name = "StatusStrip1"
    Me.StatusStrip1.Size = New System.Drawing.Size(1016, 22)
    Me.StatusStrip1.TabIndex = 27
    Me.StatusStrip1.Text = "StatusStrip1"
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(300, 16)
    '
    'lbFile
    '
    Me.lbFile.AutoSize = True
    Me.lbFile.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbFile.Location = New System.Drawing.Point(9, 138)
    Me.lbFile.Name = "lbFile"
    Me.lbFile.Size = New System.Drawing.Size(150, 17)
    Me.lbFile.TabIndex = 28
    Me.lbFile.Text = "Search F&ile (optional):"
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.pView1)
    Me.Panel1.Controls.Add(Me.pView0)
    Me.Panel1.Controls.Add(Me.cmdDelete1)
    Me.Panel1.Controls.Add(Me.cmdDelete0)
    Me.Panel1.Controls.Add(Me.lbFilename1)
    Me.Panel1.Controls.Add(Me.lbFilename0)
    Me.Panel1.Location = New System.Drawing.Point(299, 16)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(710, 306)
    Me.Panel1.TabIndex = 31
    '
    'pView1
    '
    Me.pView1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pView1.BackColor = System.Drawing.SystemColors.ControlDarkDark
    Me.pView1.BitmapPath = Nothing
    Me.pView1.CurrentPage = -1
    Me.pView1.DrawAngle = 0.0R
    Me.pView1.DrawBackColor = System.Drawing.Color.White
    Me.pView1.DrawBox = New System.Drawing.Rectangle(0, 0, 0, 0)
    Me.pView1.DrawDashed = False
    Me.pView1.DrawFilled = False
    Me.pView1.DrawFont = Nothing
    Me.pView1.DrawForeColor = System.Drawing.Color.Navy
    Me.pView1.DrawLineWidth = 1.0!
    Me.pView1.DrawPath = Nothing
    Me.pView1.DrawPoints = CType(resources.GetObject("pView1.DrawPoints"), System.Collections.Generic.List(Of System.Drawing.Point))
    Me.pView1.DrawShape = PhotoMud.shape.Line
    Me.pView1.DrawString = ""
    Me.pView1.DrawTextFmt = Nothing
    Me.pView1.FloaterOutline = False
    Me.pView1.FloaterPath = Nothing
    Me.pView1.FloaterPosition = New System.Drawing.Point(0, 0)
    Me.pView1.FloaterVisible = True
    Me.pView1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low
    Me.pView1.Location = New System.Drawing.Point(361, 3)
    Me.pView1.Name = "pView1"
    Me.pView1.pageBmp = CType(resources.GetObject("pView1.pageBmp"), System.Collections.Generic.List(Of System.Drawing.Bitmap))
    Me.pView1.PageCount = 0
    Me.pView1.RubberAngle = 0.0R
    Me.pView1.rubberBackColor = System.Drawing.Color.White
    Me.pView1.RubberBox = New System.Drawing.Rectangle(0, 0, 0, 0)
    Me.pView1.RubberBoxCrop = False
    Me.pView1.RubberColor = System.Drawing.Color.Navy
    Me.pView1.RubberDashed = False
    Me.pView1.RubberEnabled = False
    Me.pView1.RubberFilled = False
    Me.pView1.RubberFont = Nothing
    Me.pView1.RubberLineWidth = 1.0R
    Me.pView1.RubberPath = Nothing
    Me.pView1.RubberPoints = CType(resources.GetObject("pView1.RubberPoints"), System.Collections.Generic.List(Of System.Drawing.Point))
    Me.pView1.RubberShape = PhotoMud.shape.Curve
    Me.pView1.RubberString = ""
    Me.pView1.RubberTextFmt = Nothing
    Me.pView1.SelectionVisible = True
    Me.pView1.Size = New System.Drawing.Size(350, 258)
    Me.pView1.TabIndex = 38
    Me.pView1.TabStop = False
    Me.pView1.ZoomFactor = 1.0R
    '
    'pView0
    '
    Me.pView0.BackColor = System.Drawing.SystemColors.ControlDarkDark
    Me.pView0.BitmapPath = Nothing
    Me.pView0.CurrentPage = -1
    Me.pView0.DrawAngle = 0.0R
    Me.pView0.DrawBackColor = System.Drawing.Color.White
    Me.pView0.DrawBox = New System.Drawing.Rectangle(0, 0, 0, 0)
    Me.pView0.DrawDashed = False
    Me.pView0.DrawFilled = False
    Me.pView0.DrawFont = Nothing
    Me.pView0.DrawForeColor = System.Drawing.Color.Navy
    Me.pView0.DrawLineWidth = 1.0!
    Me.pView0.DrawPath = Nothing
    Me.pView0.DrawPoints = CType(resources.GetObject("pView0.DrawPoints"), System.Collections.Generic.List(Of System.Drawing.Point))
    Me.pView0.DrawShape = PhotoMud.shape.Line
    Me.pView0.DrawString = ""
    Me.pView0.DrawTextFmt = Nothing
    Me.pView0.FloaterOutline = False
    Me.pView0.FloaterPath = Nothing
    Me.pView0.FloaterPosition = New System.Drawing.Point(0, 0)
    Me.pView0.FloaterVisible = True
    Me.pView0.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low
    Me.pView0.Location = New System.Drawing.Point(3, 3)
    Me.pView0.Name = "pView0"
    Me.pView0.pageBmp = CType(resources.GetObject("pView0.pageBmp"), System.Collections.Generic.List(Of System.Drawing.Bitmap))
    Me.pView0.PageCount = 0
    Me.pView0.RubberAngle = 0.0R
    Me.pView0.rubberBackColor = System.Drawing.Color.White
    Me.pView0.RubberBox = New System.Drawing.Rectangle(0, 0, 0, 0)
    Me.pView0.RubberBoxCrop = False
    Me.pView0.RubberColor = System.Drawing.Color.Navy
    Me.pView0.RubberDashed = False
    Me.pView0.RubberEnabled = False
    Me.pView0.RubberFilled = False
    Me.pView0.RubberFont = Nothing
    Me.pView0.RubberLineWidth = 1.0R
    Me.pView0.RubberPath = Nothing
    Me.pView0.RubberPoints = CType(resources.GetObject("pView0.RubberPoints"), System.Collections.Generic.List(Of System.Drawing.Point))
    Me.pView0.RubberShape = PhotoMud.shape.Curve
    Me.pView0.RubberString = ""
    Me.pView0.RubberTextFmt = Nothing
    Me.pView0.SelectionVisible = True
    Me.pView0.Size = New System.Drawing.Size(350, 258)
    Me.pView0.TabIndex = 37
    Me.pView0.TabStop = False
    Me.pView0.ZoomFactor = 1.0R
    '
    'lbFilename1
    '
    Me.lbFilename1.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.lbFilename1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbFilename1.Location = New System.Drawing.Point(403, 268)
    Me.lbFilename1.Name = "lbFilename1"
    Me.lbFilename1.Size = New System.Drawing.Size(304, 54)
    Me.lbFilename1.TabIndex = 36
    Me.lbFilename1.Text = "lbFilename"
    '
    'lbFilename0
    '
    Me.lbFilename0.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.lbFilename0.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbFilename0.Location = New System.Drawing.Point(42, 268)
    Me.lbFilename0.Name = "lbFilename0"
    Me.lbFilename0.Size = New System.Drawing.Size(307, 54)
    Me.lbFilename0.TabIndex = 35
    Me.lbFilename0.Text = "lbFilename"
    '
    'frmDupSearch
    '
    Me.AcceptButton = Me.cmdStart
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdClose
    Me.ClientSize = New System.Drawing.Size(1016, 678)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.cmdOpenFile)
    Me.Controls.Add(Me.txFilename)
    Me.Controls.Add(Me.lbFile)
    Me.Controls.Add(Me.trkTolerance)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.cmdBrowse)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.cmdStart)
    Me.Controls.Add(Me.chkSubfolders)
    Me.Controls.Add(Me.txSearch)
    Me.Controls.Add(Me._lbTolerance_2)
    Me.Controls.Add(Me._lbTolerance_1)
    Me.Controls.Add(Me._lbTolerance_0)
    Me.Controls.Add(Me.lbSearch)
    Me.Controls.Add(Me.ListView1)
    Me.Controls.Add(Me.StatusStrip1)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(4, 38)
    Me.Name = "frmDupSearch"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Search for Duplicates"
    CType(Me.trkTolerance, System.ComponentModel.ISupportInitialize).EndInit()
    Me.StatusStrip1.ResumeLayout(False)
    Me.StatusStrip1.PerformLayout()
    Me.Panel1.ResumeLayout(False)
    CType(Me.pView1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pView0, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

End Sub
 Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
 Friend WithEvents trkTolerance As System.Windows.Forms.TrackBar
 Friend WithEvents ProgressBar1 As System.Windows.Forms.ToolStripProgressBar
 Public WithEvents cmdOpenFile As System.Windows.Forms.Button
 Public WithEvents txFilename As System.Windows.Forms.TextBox
  Public WithEvents lbFile As System.Windows.Forms.Label
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents pView1 As PhotoMud.pViewer
  Friend WithEvents pView0 As PhotoMud.pViewer
  Public WithEvents cmdDelete1 As System.Windows.Forms.Button
  Public WithEvents cmdDelete0 As System.Windows.Forms.Button
  Public WithEvents lbFilename1 As System.Windows.Forms.Label
  Public WithEvents lbFilename0 As System.Windows.Forms.Label
#End Region
End Class