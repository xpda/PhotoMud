<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmRotate
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
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRotate))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.trkAngle = New System.Windows.Forms.TrackBar()
    Me.chkGrid = New System.Windows.Forms.CheckBox()
    Me.chkTrim = New System.Windows.Forms.CheckBox()
    Me.lbAngle = New System.Windows.Forms.Label()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.txAngle = New System.Windows.Forms.TextBox()
    Me.cmdOK = New System.Windows.Forms.Button()
    Me.Opt90 = New System.Windows.Forms.RadioButton()
    Me.Opt270 = New System.Windows.Forms.RadioButton()
    Me.Opt180 = New System.Windows.Forms.RadioButton()
    Me.OptReset = New System.Windows.Forms.RadioButton()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.pView = New PhotoMud.pViewer()
    Me.cmdAuto = New System.Windows.Forms.Button()
    CType(Me.trkAngle, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel2.SuspendLayout()
    CType(Me.pView, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'cmdHelp
    '
    Me.cmdHelp.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(566, 606)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 41)
    Me.cmdHelp.TabIndex = 11
    Me.cmdHelp.TabStop = False
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'trkAngle
    '
    Me.trkAngle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.trkAngle.AutoSize = False
    Me.trkAngle.LargeChange = 50
    Me.trkAngle.Location = New System.Drawing.Point(0, 501)
    Me.trkAngle.Maximum = 1800
    Me.trkAngle.Minimum = -1800
    Me.trkAngle.Name = "trkAngle"
    Me.trkAngle.Size = New System.Drawing.Size(847, 27)
    Me.trkAngle.TabIndex = 0
    Me.trkAngle.TickFrequency = 450
    Me.ToolTip1.SetToolTip(Me.trkAngle, "Slide to rotate left or right")
    '
    'chkGrid
    '
    Me.chkGrid.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.chkGrid.AutoSize = True
    Me.chkGrid.Checked = True
    Me.chkGrid.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkGrid.Location = New System.Drawing.Point(268, 622)
    Me.chkGrid.Name = "chkGrid"
    Me.chkGrid.Size = New System.Drawing.Size(137, 21)
    Me.chkGrid.TabIndex = 8
    Me.chkGrid.Text = "Show &Grid Lines"
    Me.ToolTip1.SetToolTip(Me.chkGrid, "Grid lines can be used to align the horizon or" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "other horizontal and vertical lin" & _
        "es in the photo.")
    Me.chkGrid.UseVisualStyleBackColor = True
    '
    'chkTrim
    '
    Me.chkTrim.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.chkTrim.AutoSize = True
    Me.chkTrim.Checked = True
    Me.chkTrim.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkTrim.Location = New System.Drawing.Point(268, 595)
    Me.chkTrim.Name = "chkTrim"
    Me.chkTrim.Size = New System.Drawing.Size(101, 21)
    Me.chkTrim.TabIndex = 7
    Me.chkTrim.Text = "&Trim Photo"
    Me.ToolTip1.SetToolTip(Me.chkTrim, "Trim the photo to remove the blank" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "triangles in the corners after rotation.")
    Me.chkTrim.UseVisualStyleBackColor = True
    '
    'lbAngle
    '
    Me.lbAngle.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.lbAngle.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAngle.Location = New System.Drawing.Point(265, 551)
    Me.lbAngle.Name = "lbAngle"
    Me.lbAngle.Size = New System.Drawing.Size(51, 21)
    Me.lbAngle.TabIndex = 5
    Me.lbAngle.Text = "&Angle:"
    '
    'cmdCancel
    '
    Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdCancel.Location = New System.Drawing.Point(700, 606)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(101, 31)
    Me.cmdCancel.TabIndex = 10
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'txAngle
    '
    Me.txAngle.AcceptsReturn = True
    Me.txAngle.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.txAngle.BackColor = System.Drawing.SystemColors.Window
    Me.txAngle.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txAngle.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txAngle.Location = New System.Drawing.Point(325, 546)
    Me.txAngle.MaxLength = 0
    Me.txAngle.Name = "txAngle"
    Me.txAngle.Size = New System.Drawing.Size(96, 25)
    Me.txAngle.TabIndex = 6
    '
    'cmdOK
    '
    Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdOK.Location = New System.Drawing.Point(700, 551)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(101, 31)
    Me.cmdOK.TabIndex = 9
    Me.cmdOK.Text = "&OK"
    Me.cmdOK.UseVisualStyleBackColor = False
    '
    'Opt90
    '
    Me.Opt90.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Opt90.Location = New System.Drawing.Point(7, 3)
    Me.Opt90.Name = "Opt90"
    Me.Opt90.Size = New System.Drawing.Size(186, 26)
    Me.Opt90.TabIndex = 1
    Me.Opt90.TabStop = True
    Me.Opt90.Text = "&Right 90°"
    Me.Opt90.UseVisualStyleBackColor = False
    '
    'Opt270
    '
    Me.Opt270.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Opt270.Location = New System.Drawing.Point(7, 29)
    Me.Opt270.Name = "Opt270"
    Me.Opt270.Size = New System.Drawing.Size(186, 26)
    Me.Opt270.TabIndex = 2
    Me.Opt270.TabStop = True
    Me.Opt270.Text = "&Left 90°"
    Me.Opt270.UseVisualStyleBackColor = False
    '
    'Opt180
    '
    Me.Opt180.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Opt180.Location = New System.Drawing.Point(7, 55)
    Me.Opt180.Name = "Opt180"
    Me.Opt180.Size = New System.Drawing.Size(186, 26)
    Me.Opt180.TabIndex = 3
    Me.Opt180.TabStop = True
    Me.Opt180.Text = "&180°"
    Me.Opt180.UseVisualStyleBackColor = False
    '
    'OptReset
    '
    Me.OptReset.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.OptReset.Location = New System.Drawing.Point(7, 81)
    Me.OptReset.Name = "OptReset"
    Me.OptReset.Size = New System.Drawing.Size(186, 26)
    Me.OptReset.TabIndex = 4
    Me.OptReset.TabStop = True
    Me.OptReset.Text = "0° (R&eset)"
    Me.OptReset.UseVisualStyleBackColor = False
    '
    'Panel2
    '
    Me.Panel2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.Panel2.Controls.Add(Me.OptReset)
    Me.Panel2.Controls.Add(Me.Opt180)
    Me.Panel2.Controls.Add(Me.Opt270)
    Me.Panel2.Controls.Add(Me.Opt90)
    Me.Panel2.Location = New System.Drawing.Point(23, 542)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(204, 115)
    Me.Panel2.TabIndex = 23
    '
    'pView
    '
    Me.pView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
    Me.pView.Size = New System.Drawing.Size(847, 501)
    Me.pView.TabIndex = 25
    Me.pView.TabStop = False
    Me.pView.ZoomFactor = 1.0R
    '
    'cmdAuto
    '
    Me.cmdAuto.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdAuto.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdAuto.Location = New System.Drawing.Point(532, 551)
    Me.cmdAuto.Name = "cmdAuto"
    Me.cmdAuto.Size = New System.Drawing.Size(101, 31)
    Me.cmdAuto.TabIndex = 26
    Me.cmdAuto.Text = "A&uto Align"
    Me.cmdAuto.UseVisualStyleBackColor = False
    '
    'frmRotate
    '
    Me.AcceptButton = Me.cmdOK
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(847, 667)
    Me.Controls.Add(Me.cmdAuto)
    Me.Controls.Add(Me.pView)
    Me.Controls.Add(Me.chkGrid)
    Me.Controls.Add(Me.chkTrim)
    Me.Controls.Add(Me.Panel2)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.lbAngle)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.txAngle)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.trkAngle)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 23)
    Me.MinimizeBox = False
    Me.Name = "frmRotate"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Rotate"
    CType(Me.trkAngle, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel2.ResumeLayout(False)
    CType(Me.pView, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

End Sub
 Friend WithEvents trkAngle As System.Windows.Forms.TrackBar
 Friend WithEvents chkGrid As System.Windows.Forms.CheckBox
 Friend WithEvents chkTrim As System.Windows.Forms.CheckBox
 Public WithEvents cmdHelp As System.Windows.Forms.Button
 Public WithEvents lbAngle As System.Windows.Forms.Label
 Public WithEvents cmdCancel As System.Windows.Forms.Button
 Public WithEvents txAngle As System.Windows.Forms.TextBox
 Public WithEvents cmdOK As System.Windows.Forms.Button
  Public WithEvents Opt90 As System.Windows.Forms.RadioButton
 Public WithEvents Opt270 As System.Windows.Forms.RadioButton
 Public WithEvents Opt180 As System.Windows.Forms.RadioButton
 Public WithEvents OptReset As System.Windows.Forms.RadioButton
 Friend WithEvents Panel2 As System.Windows.Forms.Panel
  Friend WithEvents pView As PhotoMud.pViewer
  Public WithEvents cmdAuto As System.Windows.Forms.Button
#End Region
End Class