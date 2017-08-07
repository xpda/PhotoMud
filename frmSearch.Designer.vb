<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSearch
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
  Public WithEvents optAnyWord As RadioButton
  Public WithEvents optAllWords As RadioButton
  Public WithEvents txSearch As TextBox
  Public WithEvents chkSubfolders As CheckBox
  Public WithEvents cmdBrowse As Button
  Public WithEvents lbSearch As Label
  Public WithEvents Frame1 As GroupBox
  Public WithEvents txMaxDate As TextBox
  Public WithEvents txMinDate As TextBox
  Public WithEvents txFindText As TextBox
  Public WithEvents cmdTag As Button
  Public WithEvents cmdHelp As Button
  Public WithEvents cmdCancel As Button
  Public WithEvents ListView1 As ListView
  Public WithEvents cmdStart As Button
  Public WithEvents Label3 As Label
  Public WithEvents Label2 As Label
  Public WithEvents Label1 As Label
  Public WithEvents lbFound As Label
  Public WithEvents lbFilename As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSearch))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.txSearch = New System.Windows.Forms.TextBox()
    Me.txFindText = New System.Windows.Forms.TextBox()
    Me.chkSubfolders = New System.Windows.Forms.CheckBox()
    Me.cmdBrowse = New System.Windows.Forms.Button()
    Me.cmdTag = New System.Windows.Forms.Button()
    Me.cmdStart = New System.Windows.Forms.Button()
    Me.chkFilenames = New System.Windows.Forms.CheckBox()
    Me.cmdOpen = New System.Windows.Forms.Button()
    Me.optAnyWord = New System.Windows.Forms.RadioButton()
    Me.optAllWords = New System.Windows.Forms.RadioButton()
    Me.txMaxDate = New System.Windows.Forms.TextBox()
    Me.txMinDate = New System.Windows.Forms.TextBox()
    Me.Frame1 = New System.Windows.Forms.GroupBox()
    Me.lbSearch = New System.Windows.Forms.Label()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.ListView1 = New System.Windows.Forms.ListView()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.lbFound = New System.Windows.Forms.Label()
    Me.lbFilename = New System.Windows.Forms.Label()
    Me.FolderDialog1 = New System.Windows.Forms.FolderBrowserDialog()
    Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
    Me.ProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
    Me.rView = New PhotoMud.pViewer()
    Me.Frame1.SuspendLayout()
    Me.StatusStrip1.SuspendLayout()
    CType(Me.rView, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'cmdHelp
    '
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(373, 99)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 41)
    Me.cmdHelp.TabIndex = 9
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'txSearch
    '
    Me.txSearch.AcceptsReturn = True
    Me.txSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
    Me.txSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories
    Me.txSearch.BackColor = System.Drawing.SystemColors.Window
    Me.txSearch.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txSearch.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txSearch.Location = New System.Drawing.Point(15, 45)
    Me.txSearch.MaxLength = 0
    Me.txSearch.Name = "txSearch"
    Me.txSearch.Size = New System.Drawing.Size(256, 25)
    Me.txSearch.TabIndex = 1
    Me.txSearch.Text = "c:\down\tmp"
    Me.ToolTip1.SetToolTip(Me.txSearch, "Folder to be searched")
    '
    'txFindText
    '
    Me.txFindText.AcceptsReturn = True
    Me.txFindText.BackColor = System.Drawing.SystemColors.Window
    Me.txFindText.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txFindText.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txFindText.Location = New System.Drawing.Point(20, 40)
    Me.txFindText.MaxLength = 0
    Me.txFindText.Name = "txFindText"
    Me.txFindText.Size = New System.Drawing.Size(296, 25)
    Me.txFindText.TabIndex = 1
    Me.ToolTip1.SetToolTip(Me.txFindText, "Photo comments will be searched for this text.")
    '
    'chkSubfolders
    '
    Me.chkSubfolders.AutoSize = True
    Me.chkSubfolders.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkSubfolders.Location = New System.Drawing.Point(15, 86)
    Me.chkSubfolders.Name = "chkSubfolders"
    Me.chkSubfolders.Size = New System.Drawing.Size(149, 21)
    Me.chkSubfolders.TabIndex = 3
    Me.chkSubfolders.Text = "Search sub&folders"
    Me.ToolTip1.SetToolTip(Me.chkSubfolders, "Search for photos in the subfolders as well as the specified folder.")
    Me.chkSubfolders.UseVisualStyleBackColor = False
    '
    'cmdBrowse
    '
    Me.cmdBrowse.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdBrowse.Location = New System.Drawing.Point(189, 18)
    Me.cmdBrowse.Name = "cmdBrowse"
    Me.cmdBrowse.Size = New System.Drawing.Size(81, 25)
    Me.cmdBrowse.TabIndex = 2
    Me.cmdBrowse.Text = "&Browse..."
    Me.ToolTip1.SetToolTip(Me.cmdBrowse, "Select the search folder")
    Me.cmdBrowse.UseVisualStyleBackColor = False
    '
    'cmdTag
    '
    Me.cmdTag.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdTag.Location = New System.Drawing.Point(333, 189)
    Me.cmdTag.Name = "cmdTag"
    Me.cmdTag.Size = New System.Drawing.Size(118, 31)
    Me.cmdTag.TabIndex = 11
    Me.cmdTag.Text = "&Tag Checked"
    Me.ToolTip1.SetToolTip(Me.cmdTag, "Tag or select the checked photos and exit.")
    Me.cmdTag.UseVisualStyleBackColor = False
    '
    'cmdStart
    '
    Me.cmdStart.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdStart.Location = New System.Drawing.Point(333, 149)
    Me.cmdStart.Name = "cmdStart"
    Me.cmdStart.Size = New System.Drawing.Size(118, 31)
    Me.cmdStart.TabIndex = 10
    Me.cmdStart.Text = "&Search"
    Me.ToolTip1.SetToolTip(Me.cmdStart, "Begin searching")
    Me.cmdStart.UseVisualStyleBackColor = False
    '
    'chkFilenames
    '
    Me.chkFilenames.AutoSize = True
    Me.chkFilenames.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkFilenames.Location = New System.Drawing.Point(33, 154)
    Me.chkFilenames.Name = "chkFilenames"
    Me.chkFilenames.Size = New System.Drawing.Size(211, 21)
    Me.chkFilenames.TabIndex = 4
    Me.chkFilenames.Text = "Include file names in search"
    Me.ToolTip1.SetToolTip(Me.chkFilenames, "Search the file names for the search text in addition to the photo comments.")
    Me.chkFilenames.UseVisualStyleBackColor = False
    '
    'cmdOpen
    '
    Me.cmdOpen.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdOpen.Location = New System.Drawing.Point(333, 230)
    Me.cmdOpen.Name = "cmdOpen"
    Me.cmdOpen.Size = New System.Drawing.Size(118, 31)
    Me.cmdOpen.TabIndex = 12
    Me.cmdOpen.Text = "&Open Checked"
    Me.ToolTip1.SetToolTip(Me.cmdOpen, "Open the checked photos and exit")
    Me.cmdOpen.UseVisualStyleBackColor = False
    '
    'optAnyWord
    '
    Me.optAnyWord.AutoSize = True
    Me.optAnyWord.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.optAnyWord.Location = New System.Drawing.Point(335, 65)
    Me.optAnyWord.Name = "optAnyWord"
    Me.optAnyWord.Size = New System.Drawing.Size(124, 21)
    Me.optAnyWord.TabIndex = 8
    Me.optAnyWord.TabStop = True
    Me.optAnyWord.Text = "Find Any Word"
    Me.ToolTip1.SetToolTip(Me.optAnyWord, "Search results should include all photos containing any of the words in the searc" & _
        "h text.")
    Me.optAnyWord.UseVisualStyleBackColor = False
    '
    'optAllWords
    '
    Me.optAllWords.AutoSize = True
    Me.optAllWords.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.optAllWords.Location = New System.Drawing.Point(335, 40)
    Me.optAllWords.Name = "optAllWords"
    Me.optAllWords.Size = New System.Drawing.Size(123, 21)
    Me.optAllWords.TabIndex = 7
    Me.optAllWords.TabStop = True
    Me.optAllWords.Text = "Find All Words"
    Me.ToolTip1.SetToolTip(Me.optAllWords, "Search results should include only photos containing all the words in the search " & _
        "text.")
    Me.optAllWords.UseVisualStyleBackColor = False
    '
    'txMaxDate
    '
    Me.txMaxDate.AcceptsReturn = True
    Me.txMaxDate.BackColor = System.Drawing.SystemColors.Window
    Me.txMaxDate.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txMaxDate.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txMaxDate.Location = New System.Drawing.Point(195, 112)
    Me.txMaxDate.MaxLength = 0
    Me.txMaxDate.Name = "txMaxDate"
    Me.txMaxDate.Size = New System.Drawing.Size(121, 25)
    Me.txMaxDate.TabIndex = 5
    Me.ToolTip1.SetToolTip(Me.txMaxDate, "The latest photo date to be included in the search results.")
    '
    'txMinDate
    '
    Me.txMinDate.AcceptsReturn = True
    Me.txMinDate.BackColor = System.Drawing.SystemColors.Window
    Me.txMinDate.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txMinDate.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txMinDate.Location = New System.Drawing.Point(195, 77)
    Me.txMinDate.MaxLength = 0
    Me.txMinDate.Name = "txMinDate"
    Me.txMinDate.Size = New System.Drawing.Size(121, 25)
    Me.txMinDate.TabIndex = 3
    Me.ToolTip1.SetToolTip(Me.txMinDate, "The earliest photo date to be included in the search results.")
    '
    'Frame1
    '
    Me.Frame1.Controls.Add(Me.txSearch)
    Me.Frame1.Controls.Add(Me.chkSubfolders)
    Me.Frame1.Controls.Add(Me.cmdBrowse)
    Me.Frame1.Controls.Add(Me.lbSearch)
    Me.Frame1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Frame1.Location = New System.Drawing.Point(20, 184)
    Me.Frame1.Name = "Frame1"
    Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
    Me.Frame1.Size = New System.Drawing.Size(284, 125)
    Me.Frame1.TabIndex = 6
    Me.Frame1.TabStop = False
    '
    'lbSearch
    '
    Me.lbSearch.AutoSize = True
    Me.lbSearch.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbSearch.Location = New System.Drawing.Point(12, 22)
    Me.lbSearch.Name = "lbSearch"
    Me.lbSearch.Size = New System.Drawing.Size(104, 17)
    Me.lbSearch.TabIndex = 0
    Me.lbSearch.Text = "Search &Folder:"
    '
    'cmdCancel
    '
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdCancel.Location = New System.Drawing.Point(333, 272)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(118, 31)
    Me.cmdCancel.TabIndex = 13
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'ListView1
    '
    Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ListView1.BackColor = System.Drawing.SystemColors.Window
    Me.ListView1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.ListView1.FullRowSelect = True
    Me.ListView1.Location = New System.Drawing.Point(12, 322)
    Me.ListView1.Name = "ListView1"
    Me.ListView1.Size = New System.Drawing.Size(811, 326)
    Me.ListView1.TabIndex = 12
    Me.ListView1.UseCompatibleStateImageBehavior = False
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label3.Location = New System.Drawing.Point(30, 115)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(124, 17)
    Me.Label3.TabIndex = 4
    Me.Label3.Text = "L&atest photo date:"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label2.Location = New System.Drawing.Point(30, 80)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(133, 17)
    Me.Label2.TabIndex = 2
    Me.Label2.Text = "&Earliest photo date:"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label1.Location = New System.Drawing.Point(17, 18)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(89, 17)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "Search Te&xt:"
    '
    'lbFound
    '
    Me.lbFound.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbFound.Location = New System.Drawing.Point(70, 520)
    Me.lbFound.Name = "lbFound"
    Me.lbFound.Size = New System.Drawing.Size(631, 26)
    Me.lbFound.TabIndex = 20
    Me.lbFound.TextAlign = System.Drawing.ContentAlignment.TopCenter
    '
    'lbFilename
    '
    Me.lbFilename.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbFilename.Location = New System.Drawing.Point(485, 268)
    Me.lbFilename.Name = "lbFilename"
    Me.lbFilename.Size = New System.Drawing.Size(336, 51)
    Me.lbFilename.TabIndex = 19
    Me.lbFilename.Text = "lbFilename"
    Me.lbFilename.TextAlign = System.Drawing.ContentAlignment.TopCenter
    '
    'StatusStrip1
    '
    Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
    Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProgressBar1})
    Me.StatusStrip1.Location = New System.Drawing.Point(0, 651)
    Me.StatusStrip1.Name = "StatusStrip1"
    Me.StatusStrip1.Size = New System.Drawing.Size(835, 22)
    Me.StatusStrip1.TabIndex = 23
    Me.StatusStrip1.Text = "StatusStrip1"
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(300, 16)
    '
    'rView
    '
    Me.rView.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.rView.BackColor = System.Drawing.SystemColors.ControlDarkDark
    Me.rView.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low
    Me.rView.Location = New System.Drawing.Point(483, 12)
    Me.rView.Name = "rView"
    Me.rView.Size = New System.Drawing.Size(340, 246)
    Me.rView.TabIndex = 24
    Me.rView.TabStop = False
    Me.rView.ZoomFactor = 1.0R
    '
    'frmSearch
    '
    Me.AcceptButton = Me.cmdStart
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(835, 673)
    Me.Controls.Add(Me.rView)
    Me.Controls.Add(Me.cmdOpen)
    Me.Controls.Add(Me.chkFilenames)
    Me.Controls.Add(Me.StatusStrip1)
    Me.Controls.Add(Me.optAnyWord)
    Me.Controls.Add(Me.optAllWords)
    Me.Controls.Add(Me.Frame1)
    Me.Controls.Add(Me.txMaxDate)
    Me.Controls.Add(Me.txMinDate)
    Me.Controls.Add(Me.txFindText)
    Me.Controls.Add(Me.cmdTag)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.ListView1)
    Me.Controls.Add(Me.cmdStart)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.lbFound)
    Me.Controls.Add(Me.lbFilename)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(4, 38)
    Me.Name = "frmSearch"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Photo Search"
    Me.Frame1.ResumeLayout(False)
    Me.Frame1.PerformLayout()
    Me.StatusStrip1.ResumeLayout(False)
    Me.StatusStrip1.PerformLayout()
    CType(Me.rView, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

End Sub
 Friend WithEvents FolderDialog1 As System.Windows.Forms.FolderBrowserDialog
 Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
 Friend WithEvents ProgressBar1 As System.Windows.Forms.ToolStripProgressBar
 Public WithEvents chkFilenames As System.Windows.Forms.CheckBox
 Public WithEvents cmdOpen As System.Windows.Forms.Button
 Friend WithEvents rView As PhotoMud.pViewer
#End Region
End Class