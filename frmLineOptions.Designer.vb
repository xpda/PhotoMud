<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmLineOptions
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
  Public WithEvents chkDefault As CheckBox
  Public WithEvents Combo1 As ComboBox
  Public WithEvents cmdCancel As Button
  Public WithEvents cmdOK As Button
  Public WithEvents Label2 As Label
  Public WithEvents lbThickness As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLineOptions))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.chkDefault = New System.Windows.Forms.CheckBox()
    Me.Combo1 = New System.Windows.Forms.ComboBox()
    Me.nmThickness = New System.Windows.Forms.NumericUpDown()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.cmdOK = New System.Windows.Forms.Button()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.lbThickness = New System.Windows.Forms.Label()
    Me.pView1 = New PhotoMud.pViewer()
    CType(Me.nmThickness, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pView1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'cmdHelp
    '
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(31, 267)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 41)
    Me.cmdHelp.TabIndex = 5
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'chkDefault
    '
    Me.chkDefault.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkDefault.Location = New System.Drawing.Point(15, 174)
    Me.chkDefault.Name = "chkDefault"
    Me.chkDefault.Size = New System.Drawing.Size(216, 36)
    Me.chkDefault.TabIndex = 4
    Me.chkDefault.Text = "&Save as Default Settings"
    Me.ToolTip1.SetToolTip(Me.chkDefault, "Save these values as the default for drawing lines")
    Me.chkDefault.UseVisualStyleBackColor = False
    '
    'Combo1
    '
    Me.Combo1.BackColor = System.Drawing.SystemColors.Window
    Me.Combo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.Combo1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Combo1.Location = New System.Drawing.Point(15, 45)
    Me.Combo1.Name = "Combo1"
    Me.Combo1.Size = New System.Drawing.Size(211, 25)
    Me.Combo1.TabIndex = 1
    Me.ToolTip1.SetToolTip(Me.Combo1, "Pattern style of the line")
    '
    'nmThickness
    '
    Me.nmThickness.Location = New System.Drawing.Point(174, 102)
    Me.nmThickness.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
    Me.nmThickness.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nmThickness.Name = "nmThickness"
    Me.nmThickness.Size = New System.Drawing.Size(52, 25)
    Me.nmThickness.TabIndex = 3
    Me.ToolTip1.SetToolTip(Me.nmThickness, "Line thickness, in pixels")
    Me.nmThickness.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'cmdCancel
    '
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdCancel.Location = New System.Drawing.Point(116, 297)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(91, 31)
    Me.cmdCancel.TabIndex = 7
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'cmdOK
    '
    Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdOK.Location = New System.Drawing.Point(116, 252)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(91, 31)
    Me.cmdOK.TabIndex = 6
    Me.cmdOK.Text = "&OK"
    Me.cmdOK.UseVisualStyleBackColor = False
    '
    'Label2
    '
    Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label2.Location = New System.Drawing.Point(15, 20)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(111, 21)
    Me.Label2.TabIndex = 0
    Me.Label2.Text = "Line &Style:"
    '
    'lbThickness
    '
    Me.lbThickness.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbThickness.Location = New System.Drawing.Point(15, 104)
    Me.lbThickness.Name = "lbThickness"
    Me.lbThickness.Size = New System.Drawing.Size(156, 21)
    Me.lbThickness.TabIndex = 2
    Me.lbThickness.Text = "Line &Thickness (1-50):"
    '
    'pView1
    '
    Me.pView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pView1.BackColor = System.Drawing.SystemColors.ControlDarkDark
    Me.pView1.BitmapPath = Nothing
    Me.pView1.CurrentPage = 0
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
    Me.pView1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High
    Me.pView1.Location = New System.Drawing.Point(253, 20)
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
    Me.pView1.Size = New System.Drawing.Size(380, 308)
    Me.pView1.TabIndex = 8
    Me.pView1.TabStop = False
    Me.pView1.ZoomFactor = 1.0R
    '
    'frmLineOptions
    '
    Me.AcceptButton = Me.cmdOK
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(654, 350)
    Me.Controls.Add(Me.pView1)
    Me.Controls.Add(Me.nmThickness)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.chkDefault)
    Me.Controls.Add(Me.Combo1)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.lbThickness)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 23)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmLineOptions"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Line Options"
    CType(Me.nmThickness, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pView1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

End Sub
 Friend WithEvents nmThickness As System.Windows.Forms.NumericUpDown
 Friend WithEvents pView1 As PhotoMud.pViewer
#End Region
End Class