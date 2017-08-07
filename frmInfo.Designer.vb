<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmInfo
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
  Public WithEvents cmdNext As Button
  Public WithEvents cmdPrevious As Button
  Public WithEvents Text1 As TextBox
  Public WithEvents chkDump As CheckBox
  Public WithEvents chkJpgTags As CheckBox
  Public WithEvents chkMakernote As CheckBox
  Public WithEvents cmdSave As Button
  Public WithEvents cmdCopy As Button
  Public WithEvents cmdHelp As Button
  Public WithEvents cmdOK As Button
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInfo))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.cmdNext = New System.Windows.Forms.Button()
    Me.cmdPrevious = New System.Windows.Forms.Button()
    Me.chkDump = New System.Windows.Forms.CheckBox()
    Me.chkJpgTags = New System.Windows.Forms.CheckBox()
    Me.chkMakernote = New System.Windows.Forms.CheckBox()
    Me.cmdSave = New System.Windows.Forms.Button()
    Me.cmdCopy = New System.Windows.Forms.Button()
    Me.chkXmpInfo = New System.Windows.Forms.CheckBox()
    Me.Text1 = New System.Windows.Forms.TextBox()
    Me.cmdOK = New System.Windows.Forms.Button()
    Me.rtext1 = New System.Windows.Forms.RichTextBox()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.pView = New PhotoMud.pViewer()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.Panel1.SuspendLayout()
    CType(Me.pView, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel2.SuspendLayout()
    Me.SuspendLayout()
    '
    'cmdHelp
    '
    Me.cmdHelp.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(141, 459)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 41)
    Me.cmdHelp.TabIndex = 11
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'cmdNext
    '
    Me.cmdNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdNext.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdNext.Image = CType(resources.GetObject("cmdNext.Image"), System.Drawing.Image)
    Me.cmdNext.Location = New System.Drawing.Point(281, 271)
    Me.cmdNext.Name = "cmdNext"
    Me.cmdNext.Size = New System.Drawing.Size(36, 36)
    Me.cmdNext.TabIndex = 4
    Me.cmdNext.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdNext, "Next photo")
    Me.cmdNext.UseVisualStyleBackColor = False
    '
    'cmdPrevious
    '
    Me.cmdPrevious.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdPrevious.Image = CType(resources.GetObject("cmdPrevious.Image"), System.Drawing.Image)
    Me.cmdPrevious.Location = New System.Drawing.Point(10, 271)
    Me.cmdPrevious.Name = "cmdPrevious"
    Me.cmdPrevious.Size = New System.Drawing.Size(36, 36)
    Me.cmdPrevious.TabIndex = 3
    Me.cmdPrevious.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdPrevious, "Previous photo")
    Me.cmdPrevious.UseVisualStyleBackColor = False
    '
    'chkDump
    '
    Me.chkDump.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.chkDump.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkDump.Location = New System.Drawing.Point(71, 406)
    Me.chkDump.Name = "chkDump"
    Me.chkDump.Size = New System.Drawing.Size(196, 26)
    Me.chkDump.TabIndex = 7
    Me.chkDump.Text = "Dump &All Tags"
    Me.ToolTip1.SetToolTip(Me.chkDump, "Output information for all tags by tag number")
    Me.chkDump.UseVisualStyleBackColor = False
    '
    'chkJpgTags
    '
    Me.chkJpgTags.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.chkJpgTags.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkJpgTags.Location = New System.Drawing.Point(71, 342)
    Me.chkJpgTags.Name = "chkJpgTags"
    Me.chkJpgTags.Size = New System.Drawing.Size(196, 26)
    Me.chkJpgTags.TabIndex = 6
    Me.chkJpgTags.Text = "Include JPEG &Details"
    Me.ToolTip1.SetToolTip(Me.chkJpgTags, "Include JPEG details in the output")
    Me.chkJpgTags.UseVisualStyleBackColor = False
    '
    'chkMakernote
    '
    Me.chkMakernote.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.chkMakernote.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkMakernote.Location = New System.Drawing.Point(71, 310)
    Me.chkMakernote.Name = "chkMakernote"
    Me.chkMakernote.Size = New System.Drawing.Size(196, 26)
    Me.chkMakernote.TabIndex = 5
    Me.chkMakernote.Text = "Include &Maker Note"
    Me.ToolTip1.SetToolTip(Me.chkMakernote, "Include the maker note in the output")
    Me.chkMakernote.UseVisualStyleBackColor = False
    '
    'cmdSave
    '
    Me.cmdSave.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.cmdSave.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdSave.Location = New System.Drawing.Point(116, 609)
    Me.cmdSave.Name = "cmdSave"
    Me.cmdSave.Size = New System.Drawing.Size(91, 31)
    Me.cmdSave.TabIndex = 10
    Me.cmdSave.Text = "&Save"
    Me.ToolTip1.SetToolTip(Me.cmdSave, "Save the information to a disk text file")
    Me.cmdSave.UseVisualStyleBackColor = False
    '
    'cmdCopy
    '
    Me.cmdCopy.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.cmdCopy.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdCopy.Location = New System.Drawing.Point(116, 564)
    Me.cmdCopy.Name = "cmdCopy"
    Me.cmdCopy.Size = New System.Drawing.Size(91, 31)
    Me.cmdCopy.TabIndex = 9
    Me.cmdCopy.Text = "&Copy"
    Me.ToolTip1.SetToolTip(Me.cmdCopy, "Copy the information to the clipboard")
    Me.cmdCopy.UseVisualStyleBackColor = False
    '
    'chkXmpInfo
    '
    Me.chkXmpInfo.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.chkXmpInfo.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkXmpInfo.Location = New System.Drawing.Point(71, 374)
    Me.chkXmpInfo.Name = "chkXmpInfo"
    Me.chkXmpInfo.Size = New System.Drawing.Size(196, 26)
    Me.chkXmpInfo.TabIndex = 12
    Me.chkXmpInfo.Text = "Include &XMP Info"
    Me.ToolTip1.SetToolTip(Me.chkXmpInfo, "Include JPEG details in the output")
    Me.chkXmpInfo.UseVisualStyleBackColor = False
    '
    'Text1
    '
    Me.Text1.AcceptsReturn = True
    Me.Text1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Text1.BackColor = System.Drawing.SystemColors.Menu
    Me.Text1.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.Text1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Text1.Location = New System.Drawing.Point(3, 241)
    Me.Text1.MaxLength = 0
    Me.Text1.Name = "Text1"
    Me.Text1.ReadOnly = True
    Me.Text1.Size = New System.Drawing.Size(320, 25)
    Me.Text1.TabIndex = 2
    Me.Text1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    '
    'cmdOK
    '
    Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdOK.Location = New System.Drawing.Point(116, 519)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(91, 31)
    Me.cmdOK.TabIndex = 8
    Me.cmdOK.Text = "&OK"
    Me.cmdOK.UseVisualStyleBackColor = False
    '
    'rtext1
    '
    Me.rtext1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.rtext1.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.rtext1.DetectUrls = False
    Me.rtext1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.rtext1.Location = New System.Drawing.Point(9, 3)
    Me.rtext1.Name = "rtext1"
    Me.rtext1.Size = New System.Drawing.Size(706, 642)
    Me.rtext1.TabIndex = 12
    Me.rtext1.Text = ""
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.pView)
    Me.Panel1.Controls.Add(Me.chkXmpInfo)
    Me.Panel1.Controls.Add(Me.chkJpgTags)
    Me.Panel1.Controls.Add(Me.cmdOK)
    Me.Panel1.Controls.Add(Me.chkMakernote)
    Me.Panel1.Controls.Add(Me.cmdNext)
    Me.Panel1.Controls.Add(Me.chkDump)
    Me.Panel1.Controls.Add(Me.cmdHelp)
    Me.Panel1.Controls.Add(Me.cmdSave)
    Me.Panel1.Controls.Add(Me.cmdPrevious)
    Me.Panel1.Controls.Add(Me.Text1)
    Me.Panel1.Controls.Add(Me.cmdCopy)
    Me.Panel1.Location = New System.Drawing.Point(726, -3)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(326, 667)
    Me.Panel1.TabIndex = 13
    '
    'pView
    '
    Me.pView.BackColor = System.Drawing.SystemColors.ControlDarkDark
    Me.pView.BitmapPath = Nothing
    Me.pView.CurrentPage = 0
    Me.pView.DrawAngle = 0.0R
    Me.pView.DrawBackColor = System.Drawing.Color.White
    Me.pView.DrawBox = New System.Drawing.Rectangle(0, 0, 0, 0)
    Me.pView.DrawDashed = False
    Me.pView.DrawFilled = False
    Me.pView.DrawFont = Nothing
    Me.pView.DrawForeColor = System.Drawing.Color.Navy
    Me.pView.DrawLineWidth = 1.0!
    Me.pView.DrawPath = Nothing
    Me.pView.DrawPoints = CType(resources.GetObject("pView.DrawPoints"), System.Collections.Generic.List(Of System.Drawing.Point))
    Me.pView.DrawShape = PhotoMud.shape.Line
    Me.pView.DrawString = ""
    Me.pView.DrawTextFmt = Nothing
    Me.pView.FloaterOutline = False
    Me.pView.FloaterPath = Nothing
    Me.pView.FloaterPosition = New System.Drawing.Point(0, 0)
    Me.pView.FloaterVisible = True
    Me.pView.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low
    Me.pView.Location = New System.Drawing.Point(0, 0)
    Me.pView.Name = "pView"
    Me.pView.pageBmp = CType(resources.GetObject("pView.pageBmp"), System.Collections.Generic.List(Of System.Drawing.Bitmap))
    Me.pView.PageCount = 0
    Me.pView.RubberAngle = 0.0R
    Me.pView.rubberBackColor = System.Drawing.Color.White
    Me.pView.RubberBox = New System.Drawing.Rectangle(0, 0, 0, 0)
    Me.pView.RubberBoxCrop = False
    Me.pView.RubberColor = System.Drawing.Color.Navy
    Me.pView.RubberDashed = False
    Me.pView.RubberEnabled = False
    Me.pView.RubberFilled = False
    Me.pView.RubberFont = Nothing
    Me.pView.RubberLineWidth = 1.0R
    Me.pView.RubberPath = Nothing
    Me.pView.RubberPoints = CType(resources.GetObject("pView.RubberPoints"), System.Collections.Generic.List(Of System.Drawing.Point))
    Me.pView.RubberShape = PhotoMud.shape.Curve
    Me.pView.RubberString = ""
    Me.pView.RubberTextFmt = Nothing
    Me.pView.SelectionVisible = True
    Me.pView.Size = New System.Drawing.Size(326, 218)
    Me.pView.TabIndex = 13
    Me.pView.TabStop = False
    Me.pView.ZoomFactor = 1.0R
    '
    'Panel2
    '
    Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel2.BackColor = System.Drawing.SystemColors.Window
    Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Panel2.Controls.Add(Me.rtext1)
    Me.Panel2.Location = New System.Drawing.Point(2, 12)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(718, 652)
    Me.Panel2.TabIndex = 12
    '
    'frmInfo
    '
    Me.AcceptButton = Me.cmdOK
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdOK
    Me.ClientSize = New System.Drawing.Size(1053, 666)
    Me.Controls.Add(Me.Panel2)
    Me.Controls.Add(Me.Panel1)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(4, 38)
    Me.Name = "frmInfo"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Photo Information"
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    CType(Me.pView, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel2.ResumeLayout(False)
    Me.ResumeLayout(False)

End Sub
 Public WithEvents rtext1 As System.Windows.Forms.RichTextBox
 Friend WithEvents Panel1 As System.Windows.Forms.Panel
 Friend WithEvents Panel2 As System.Windows.Forms.Panel
 Public WithEvents chkXmpInfo As System.Windows.Forms.CheckBox
 Friend WithEvents pView As PhotoMud.pViewer
#End Region
End Class