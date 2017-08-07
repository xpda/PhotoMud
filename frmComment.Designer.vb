<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmComment
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
  Public WithEvents cmdBack As Button
  Public WithEvents cmdHelp As Button
  Public WithEvents cmdOK As Button
  Public WithEvents cmdCancel As Button
  Public WithEvents rText0 As RichTextBox
  Public WithEvents rText1 As RichTextBox
  Public WithEvents LbDate As Label
  Public WithEvents Label2w As Label
  Public WithEvents Label0 As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmComment))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.rText1 = New System.Windows.Forms.RichTextBox()
    Me.rText3 = New System.Windows.Forms.RichTextBox()
    Me.rText2 = New System.Windows.Forms.RichTextBox()
    Me.cmdOK = New System.Windows.Forms.Button()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.cmdNext = New System.Windows.Forms.Button()
    Me.cmdBack = New System.Windows.Forms.Button()
    Me.rText0 = New System.Windows.Forms.RichTextBox()
    Me.LbDate = New System.Windows.Forms.Label()
    Me.Label2w = New System.Windows.Forms.Label()
    Me.Label0 = New System.Windows.Forms.Label()
    Me.lbPicPath = New System.Windows.Forms.Label()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.lbDate2 = New System.Windows.Forms.Label()
    Me.lbGPSLocation = New System.Windows.Forms.Label()
    Me.pView = New PhotoMud.pViewer()
    Me.GroupBox1.SuspendLayout()
    CType(Me.pView, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'cmdHelp
    '
    Me.cmdHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(419, 464)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(46, 46)
    Me.cmdHelp.TabIndex = 12
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'rText1
    '
    Me.rText1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.rText1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.rText1.Location = New System.Drawing.Point(128, 28)
    Me.rText1.Multiline = False
    Me.rText1.Name = "rText1"
    Me.rText1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
    Me.rText1.Size = New System.Drawing.Size(253, 27)
    Me.rText1.TabIndex = 3
    Me.rText1.Text = "asdfg"
    Me.ToolTip1.SetToolTip(Me.rText1, "Date the photo was taken")
    '
    'rText3
    '
    Me.rText3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.rText3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.rText3.Location = New System.Drawing.Point(128, 94)
    Me.rText3.Multiline = False
    Me.rText3.Name = "rText3"
    Me.rText3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
    Me.rText3.Size = New System.Drawing.Size(253, 27)
    Me.rText3.TabIndex = 6
    Me.rText3.Text = "lat-lon"
    Me.ToolTip1.SetToolTip(Me.rText3, "Date the photo was taken")
    '
    'rText2
    '
    Me.rText2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.rText2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.rText2.Location = New System.Drawing.Point(128, 61)
    Me.rText2.Multiline = False
    Me.rText2.Name = "rText2"
    Me.rText2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
    Me.rText2.Size = New System.Drawing.Size(253, 27)
    Me.rText2.TabIndex = 5
    Me.rText2.Text = "asdfg"
    Me.ToolTip1.SetToolTip(Me.rText2, "Date the photo was taken")
    '
    'cmdOK
    '
    Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdOK.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdOK.Location = New System.Drawing.Point(173, 468)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(105, 40)
    Me.cmdOK.TabIndex = 10
    Me.cmdOK.Text = "&OK"
    Me.cmdOK.UseVisualStyleBackColor = False
    '
    'cmdCancel
    '
    Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdCancel.Location = New System.Drawing.Point(295, 468)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(105, 40)
    Me.cmdCancel.TabIndex = 11
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'cmdNext
    '
    Me.cmdNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdNext.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdNext.Image = CType(resources.GetObject("cmdNext.Image"), System.Drawing.Image)
    Me.cmdNext.ImageAlign = System.Drawing.ContentAlignment.TopCenter
    Me.cmdNext.Location = New System.Drawing.Point(104, 464)
    Me.cmdNext.Name = "cmdNext"
    Me.cmdNext.Size = New System.Drawing.Size(46, 46)
    Me.cmdNext.TabIndex = 9
    Me.cmdNext.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.cmdNext.UseVisualStyleBackColor = False
    '
    'cmdBack
    '
    Me.cmdBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdBack.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdBack.Image = CType(resources.GetObject("cmdBack.Image"), System.Drawing.Image)
    Me.cmdBack.ImageAlign = System.Drawing.ContentAlignment.TopCenter
    Me.cmdBack.Location = New System.Drawing.Point(45, 464)
    Me.cmdBack.Name = "cmdBack"
    Me.cmdBack.Size = New System.Drawing.Size(46, 46)
    Me.cmdBack.TabIndex = 8
    Me.cmdBack.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.cmdBack.UseVisualStyleBackColor = False
    '
    'rText0
    '
    Me.rText0.AcceptsTab = True
    Me.rText0.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.rText0.DetectUrls = False
    Me.rText0.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.rText0.Location = New System.Drawing.Point(15, 42)
    Me.rText0.Name = "rText0"
    Me.rText0.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
    Me.rText0.Size = New System.Drawing.Size(450, 379)
    Me.rText0.TabIndex = 1
    Me.rText0.Text = ""
    '
    'LbDate
    '
    Me.LbDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.LbDate.AutoSize = True
    Me.LbDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.LbDate.Location = New System.Drawing.Point(20, 33)
    Me.LbDate.Name = "LbDate"
    Me.LbDate.Size = New System.Drawing.Size(43, 17)
    Me.LbDate.TabIndex = 2
    Me.LbDate.Text = "&Date:"
    '
    'Label2w
    '
    Me.Label2w.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label2w.AutoSize = True
    Me.Label2w.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label2w.Location = New System.Drawing.Point(82, 522)
    Me.Label2w.Name = "Label2w"
    Me.Label2w.Size = New System.Drawing.Size(274, 17)
    Me.Label2w.TabIndex = 25
    Me.Label2w.Text = "Press F2 to clear, F3 for previous values."
    Me.Label2w.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label0
    '
    Me.Label0.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label0.Location = New System.Drawing.Point(12, 21)
    Me.Label0.Name = "Label0"
    Me.Label0.Size = New System.Drawing.Size(220, 28)
    Me.Label0.TabIndex = 0
    Me.Label0.Text = "&Photo Description:"
    '
    'lbPicPath
    '
    Me.lbPicPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbPicPath.Location = New System.Drawing.Point(494, 513)
    Me.lbPicPath.Name = "lbPicPath"
    Me.lbPicPath.Size = New System.Drawing.Size(460, 20)
    Me.lbPicPath.TabIndex = 27
    Me.lbPicPath.Text = "Labelg"
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.rText2)
    Me.GroupBox1.Controls.Add(Me.lbDate2)
    Me.GroupBox1.Controls.Add(Me.rText3)
    Me.GroupBox1.Controls.Add(Me.lbGPSLocation)
    Me.GroupBox1.Controls.Add(Me.rText1)
    Me.GroupBox1.Controls.Add(Me.LbDate)
    Me.GroupBox1.Location = New System.Drawing.Point(497, 30)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(405, 136)
    Me.GroupBox1.TabIndex = 0
    Me.GroupBox1.TabStop = False
    '
    'lbDate2
    '
    Me.lbDate2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbDate2.AutoSize = True
    Me.lbDate2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbDate2.Location = New System.Drawing.Point(20, 66)
    Me.lbDate2.Name = "lbDate2"
    Me.lbDate2.Size = New System.Drawing.Size(97, 17)
    Me.lbDate2.TabIndex = 4
    Me.lbDate2.Text = "&O&riginal Date:"
    '
    'lbGPSLocation
    '
    Me.lbGPSLocation.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbGPSLocation.AutoSize = True
    Me.lbGPSLocation.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbGPSLocation.Location = New System.Drawing.Point(20, 99)
    Me.lbGPSLocation.Name = "lbGPSLocation"
    Me.lbGPSLocation.Size = New System.Drawing.Size(102, 17)
    Me.lbGPSLocation.TabIndex = 6
    Me.lbGPSLocation.Text = "&GPS Location:"
    '
    'pView
    '
    Me.pView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pView.BackColor = System.Drawing.SystemColors.ControlDarkDark
    Me.pView.BitmapPath = Nothing
    Me.pView.CurrentPage = -1
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
    Me.pView.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High
    Me.pView.Location = New System.Drawing.Point(497, 182)
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
    Me.pView.Size = New System.Drawing.Size(471, 326)
    Me.pView.TabIndex = 0
    Me.pView.TabStop = False
    Me.pView.ZoomFactor = 1.0R
    '
    'frmComment
    '
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(980, 559)
    Me.Controls.Add(Me.pView)
    Me.Controls.Add(Me.rText0)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.lbPicPath)
    Me.Controls.Add(Me.Label0)
    Me.Controls.Add(Me.cmdNext)
    Me.Controls.Add(Me.cmdBack)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.Label2w)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(4, 39)
    Me.Name = "frmComment"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Image Comments"
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    CType(Me.pView, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

End Sub
 Friend WithEvents lbPicPath As System.Windows.Forms.Label
 Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
 Public WithEvents rText3 As System.Windows.Forms.RichTextBox
 Public WithEvents lbGPSLocation As System.Windows.Forms.Label
 Public WithEvents rText2 As System.Windows.Forms.RichTextBox
 Public WithEvents lbDate2 As System.Windows.Forms.Label
 Friend WithEvents pView As PhotoMud.pViewer
#End Region 
End Class