<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSend
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
  Public WithEvents OptOriginal As RadioButton
  Public WithEvents optResize As RadioButton
  Public WithEvents Frame2 As GroupBox
  Public WithEvents cmdHelp As Button
  Public WithEvents cmdCancel As Button
  Public WithEvents cmdSend As Button
  Public WithEvents cmbFiletype As ComboBox
  Public WithEvents txtYres As TextBox
  Public WithEvents txtXres As TextBox
  Public WithEvents chkResize As CheckBox
  Public WithEvents txtPct As TextBox
  Public WithEvents lbYres As Label
  Public WithEvents lbXres As Label
  Public WithEvents lbPct As Label
  Public WithEvents Frame1 As GroupBox
  Public WithEvents lbCompression As Label
  Public WithEvents lbCompression1 As Label
  Public WithEvents label3 As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSend))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.cmbFiletype = New System.Windows.Forms.ComboBox()
    Me.chkResize = New System.Windows.Forms.CheckBox()
    Me.Frame2 = New System.Windows.Forms.GroupBox()
    Me.OptOriginal = New System.Windows.Forms.RadioButton()
    Me.optResize = New System.Windows.Forms.RadioButton()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.cmdSend = New System.Windows.Forms.Button()
    Me.Frame1 = New System.Windows.Forms.GroupBox()
    Me.txtYres = New System.Windows.Forms.TextBox()
    Me.txtXres = New System.Windows.Forms.TextBox()
    Me.txtPct = New System.Windows.Forms.TextBox()
    Me.lbYres = New System.Windows.Forms.Label()
    Me.lbXres = New System.Windows.Forms.Label()
    Me.lbPct = New System.Windows.Forms.Label()
    Me.lbCompression = New System.Windows.Forms.Label()
    Me.lbCompression1 = New System.Windows.Forms.Label()
    Me.label3 = New System.Windows.Forms.Label()
    Me.nmQuality = New System.Windows.Forms.NumericUpDown()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.rText1 = New System.Windows.Forms.RichTextBox()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.txSubject = New System.Windows.Forms.TextBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.txToAddress = New System.Windows.Forms.TextBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.txFromName = New System.Windows.Forms.TextBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.txFromAddress = New System.Windows.Forms.TextBox()
    Me.Frame2.SuspendLayout()
    Me.Frame1.SuspendLayout()
    CType(Me.nmQuality, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'cmdHelp
    '
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(385, 280)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 41)
    Me.cmdHelp.TabIndex = 30
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'cmbFiletype
    '
    Me.cmbFiletype.BackColor = System.Drawing.SystemColors.Window
    Me.cmbFiletype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cmbFiletype.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmbFiletype.Location = New System.Drawing.Point(170, 95)
    Me.cmbFiletype.Name = "cmbFiletype"
    Me.cmbFiletype.Size = New System.Drawing.Size(251, 25)
    Me.cmbFiletype.TabIndex = 13
    Me.ToolTip1.SetToolTip(Me.cmbFiletype, "File Type to which the photo is to be converted")
    '
    'chkResize
    '
    Me.chkResize.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkResize.Location = New System.Drawing.Point(25, 25)
    Me.chkResize.Name = "chkResize"
    Me.chkResize.Size = New System.Drawing.Size(161, 26)
    Me.chkResize.TabIndex = 20
    Me.chkResize.Text = "&Resize Photo"
    Me.ToolTip1.SetToolTip(Me.chkResize, "Check to reduce the photo size before sending")
    Me.chkResize.UseVisualStyleBackColor = False
    '
    'Frame2
    '
    Me.Frame2.Controls.Add(Me.OptOriginal)
    Me.Frame2.Controls.Add(Me.optResize)
    Me.Frame2.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Frame2.Location = New System.Drawing.Point(25, 5)
    Me.Frame2.Name = "Frame2"
    Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
    Me.Frame2.Size = New System.Drawing.Size(376, 75)
    Me.Frame2.TabIndex = 18
    Me.Frame2.TabStop = False
    '
    'OptOriginal
    '
    Me.OptOriginal.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.OptOriginal.Location = New System.Drawing.Point(15, 18)
    Me.OptOriginal.Name = "OptOriginal"
    Me.OptOriginal.Size = New System.Drawing.Size(246, 21)
    Me.OptOriginal.TabIndex = 10
    Me.OptOriginal.TabStop = True
    Me.OptOriginal.Text = "Send the &Original Photo"
    Me.OptOriginal.UseVisualStyleBackColor = False
    '
    'optResize
    '
    Me.optResize.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.optResize.Location = New System.Drawing.Point(15, 45)
    Me.optResize.Name = "optResize"
    Me.optResize.Size = New System.Drawing.Size(246, 21)
    Me.optResize.TabIndex = 11
    Me.optResize.TabStop = True
    Me.optResize.Text = "&Convert or Resize the Photo"
    Me.optResize.UseVisualStyleBackColor = False
    '
    'cmdCancel
    '
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdCancel.Location = New System.Drawing.Point(355, 388)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(96, 33)
    Me.cmdCancel.TabIndex = 32
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'cmdSend
    '
    Me.cmdSend.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdSend.Location = New System.Drawing.Point(355, 350)
    Me.cmdSend.Name = "cmdSend"
    Me.cmdSend.Size = New System.Drawing.Size(96, 33)
    Me.cmdSend.TabIndex = 31
    Me.cmdSend.Text = "&Send"
    Me.cmdSend.UseVisualStyleBackColor = False
    '
    'Frame1
    '
    Me.Frame1.Controls.Add(Me.txtYres)
    Me.Frame1.Controls.Add(Me.txtXres)
    Me.Frame1.Controls.Add(Me.chkResize)
    Me.Frame1.Controls.Add(Me.txtPct)
    Me.Frame1.Controls.Add(Me.lbYres)
    Me.Frame1.Controls.Add(Me.lbXres)
    Me.Frame1.Controls.Add(Me.lbPct)
    Me.Frame1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Frame1.Location = New System.Drawing.Point(25, 265)
    Me.Frame1.Name = "Frame1"
    Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
    Me.Frame1.Size = New System.Drawing.Size(276, 156)
    Me.Frame1.TabIndex = 13
    Me.Frame1.TabStop = False
    Me.Frame1.Text = "Resize"
    '
    'txtYres
    '
    Me.txtYres.AcceptsReturn = True
    Me.txtYres.BackColor = System.Drawing.SystemColors.Window
    Me.txtYres.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txtYres.Enabled = False
    Me.txtYres.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txtYres.Location = New System.Drawing.Point(185, 115)
    Me.txtYres.MaxLength = 0
    Me.txtYres.Name = "txtYres"
    Me.txtYres.Size = New System.Drawing.Size(61, 25)
    Me.txtYres.TabIndex = 26
    Me.txtYres.Text = "2000"
    '
    'txtXres
    '
    Me.txtXres.AcceptsReturn = True
    Me.txtXres.BackColor = System.Drawing.SystemColors.Window
    Me.txtXres.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txtXres.Enabled = False
    Me.txtXres.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txtXres.Location = New System.Drawing.Point(185, 85)
    Me.txtXres.MaxLength = 0
    Me.txtXres.Name = "txtXres"
    Me.txtXres.Size = New System.Drawing.Size(61, 25)
    Me.txtXres.TabIndex = 24
    Me.txtXres.Text = "2000"
    '
    'txtPct
    '
    Me.txtPct.AcceptsReturn = True
    Me.txtPct.BackColor = System.Drawing.SystemColors.Window
    Me.txtPct.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txtPct.Enabled = False
    Me.txtPct.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txtPct.Location = New System.Drawing.Point(185, 55)
    Me.txtPct.MaxLength = 0
    Me.txtPct.Name = "txtPct"
    Me.txtPct.Size = New System.Drawing.Size(61, 25)
    Me.txtPct.TabIndex = 22
    Me.txtPct.Text = "100%"
    '
    'lbYres
    '
    Me.lbYres.Enabled = False
    Me.lbYres.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbYres.Location = New System.Drawing.Point(25, 120)
    Me.lbYres.Name = "lbYres"
    Me.lbYres.Size = New System.Drawing.Size(146, 21)
    Me.lbYres.TabIndex = 25
    Me.lbYres.Text = "&Vertical Resolution:"
    '
    'lbXres
    '
    Me.lbXres.Enabled = False
    Me.lbXres.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbXres.Location = New System.Drawing.Point(25, 90)
    Me.lbXres.Name = "lbXres"
    Me.lbXres.Size = New System.Drawing.Size(146, 21)
    Me.lbXres.TabIndex = 23
    Me.lbXres.Text = "&Horizontal Resolution:"
    '
    'lbPct
    '
    Me.lbPct.Enabled = False
    Me.lbPct.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbPct.Location = New System.Drawing.Point(25, 60)
    Me.lbPct.Name = "lbPct"
    Me.lbPct.Size = New System.Drawing.Size(146, 21)
    Me.lbPct.TabIndex = 21
    Me.lbPct.Text = "&Percent Change:"
    '
    'lbCompression
    '
    Me.lbCompression.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbCompression.Location = New System.Drawing.Point(30, 180)
    Me.lbCompression.Name = "lbCompression"
    Me.lbCompression.Size = New System.Drawing.Size(386, 66)
    Me.lbCompression.TabIndex = 14
    Me.lbCompression.Text = "(see form load for caption)"
    '
    'lbCompression1
    '
    Me.lbCompression1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbCompression1.Location = New System.Drawing.Point(30, 140)
    Me.lbCompression1.Name = "lbCompression1"
    Me.lbCompression1.Size = New System.Drawing.Size(136, 21)
    Me.lbCompression1.TabIndex = 14
    Me.lbCompression1.Text = "&JPG Quality:"
    '
    'label3
    '
    Me.label3.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.label3.Location = New System.Drawing.Point(30, 100)
    Me.label3.Name = "label3"
    Me.label3.Size = New System.Drawing.Size(131, 21)
    Me.label3.TabIndex = 12
    Me.label3.Text = "Output File &Type:"
    '
    'nmQuality
    '
    Me.nmQuality.Location = New System.Drawing.Point(170, 138)
    Me.nmQuality.Name = "nmQuality"
    Me.nmQuality.Size = New System.Drawing.Size(56, 25)
    Me.nmQuality.TabIndex = 15
    Me.nmQuality.Value = New Decimal(New Integer() {95, 0, 0, 0})
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Panel1.Controls.Add(Me.rText1)
    Me.Panel1.Controls.Add(Me.Label6)
    Me.Panel1.Controls.Add(Me.Label5)
    Me.Panel1.Controls.Add(Me.txSubject)
    Me.Panel1.Controls.Add(Me.Label4)
    Me.Panel1.Controls.Add(Me.txToAddress)
    Me.Panel1.Controls.Add(Me.Label2)
    Me.Panel1.Controls.Add(Me.txFromName)
    Me.Panel1.Controls.Add(Me.Label1)
    Me.Panel1.Controls.Add(Me.txFromAddress)
    Me.Panel1.Location = New System.Drawing.Point(543, 23)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(470, 421)
    Me.Panel1.TabIndex = 19
    '
    'rText1
    '
    Me.rText1.AcceptsTab = True
    Me.rText1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.rText1.DetectUrls = False
    Me.rText1.Location = New System.Drawing.Point(25, 188)
    Me.rText1.Name = "rText1"
    Me.rText1.Size = New System.Drawing.Size(437, 226)
    Me.rText1.TabIndex = 48
    Me.rText1.Text = ""
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Location = New System.Drawing.Point(22, 166)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(71, 17)
    Me.Label6.TabIndex = 8
    Me.Label6.Text = "&Message:"
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(22, 122)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(61, 17)
    Me.Label5.TabIndex = 46
    Me.Label5.Text = "S&ubject:"
    '
    'txSubject
    '
    Me.txSubject.Location = New System.Drawing.Point(181, 117)
    Me.txSubject.Name = "txSubject"
    Me.txSubject.Size = New System.Drawing.Size(282, 25)
    Me.txSubject.TabIndex = 47
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(22, 91)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(123, 17)
    Me.Label4.TabIndex = 44
    Me.Label4.Text = "To &email address:"
    '
    'txToAddress
    '
    Me.txToAddress.Location = New System.Drawing.Point(181, 86)
    Me.txToAddress.Name = "txToAddress"
    Me.txToAddress.Size = New System.Drawing.Size(282, 25)
    Me.txToAddress.TabIndex = 45
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(22, 60)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(88, 17)
    Me.Label2.TabIndex = 42
    Me.Label2.Text = "From &name:"
    '
    'txFromName
    '
    Me.txFromName.Location = New System.Drawing.Point(181, 55)
    Me.txFromName.Name = "txFromName"
    Me.txFromName.Size = New System.Drawing.Size(282, 25)
    Me.txFromName.TabIndex = 43
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(22, 29)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(143, 17)
    Me.Label1.TabIndex = 40
    Me.Label1.Text = "&From email address:"
    '
    'txFromAddress
    '
    Me.txFromAddress.Location = New System.Drawing.Point(181, 24)
    Me.txFromAddress.Name = "txFromAddress"
    Me.txFromAddress.Size = New System.Drawing.Size(282, 25)
    Me.txFromAddress.TabIndex = 41
    '
    'frmSend
    '
    Me.AcceptButton = Me.cmdSend
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(1056, 468)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.nmQuality)
    Me.Controls.Add(Me.Frame2)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.cmdSend)
    Me.Controls.Add(Me.cmbFiletype)
    Me.Controls.Add(Me.Frame1)
    Me.Controls.Add(Me.lbCompression)
    Me.Controls.Add(Me.lbCompression1)
    Me.Controls.Add(Me.label3)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 22)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmSend"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Send Photo"
    Me.Frame2.ResumeLayout(False)
    Me.Frame1.ResumeLayout(False)
    Me.Frame1.PerformLayout()
    CType(Me.nmQuality, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents nmQuality As System.Windows.Forms.NumericUpDown
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents txFromName As System.Windows.Forms.TextBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents txFromAddress As System.Windows.Forms.TextBox
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents txSubject As System.Windows.Forms.TextBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents txToAddress As System.Windows.Forms.TextBox
  Friend WithEvents rText1 As System.Windows.Forms.RichTextBox
  Friend WithEvents Label6 As System.Windows.Forms.Label
#End Region
End Class