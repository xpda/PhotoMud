<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSaveVerify
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
  Public WithEvents chkResize As CheckBox
  Public WithEvents txtXres As TextBox
  Public WithEvents txtYres As TextBox
  Public WithEvents lbXres As Label
  Public WithEvents lbYres As Label
  Public WithEvents Frame1 As GroupBox
  Public WithEvents txtQuality As TextBox
  Public WithEvents cmdNoSave As Button
  Public WithEvents cmdSave As Button
  Public WithEvents lbCompression1 As Label
  Public WithEvents lbCompression As Label
  Public WithEvents Label1 As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSaveVerify))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.chkResize = New System.Windows.Forms.CheckBox()
    Me.Frame1 = New System.Windows.Forms.GroupBox()
    Me.txtXres = New System.Windows.Forms.TextBox()
    Me.txtYres = New System.Windows.Forms.TextBox()
    Me.lbXres = New System.Windows.Forms.Label()
    Me.lbYres = New System.Windows.Forms.Label()
    Me.txtQuality = New System.Windows.Forms.TextBox()
    Me.cmdNoSave = New System.Windows.Forms.Button()
    Me.cmdSave = New System.Windows.Forms.Button()
    Me.lbCompression1 = New System.Windows.Forms.Label()
    Me.lbCompression = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Frame1.SuspendLayout()
    Me.SuspendLayout()
    '
    'chkResize
    '
    Me.chkResize.Checked = True
    Me.chkResize.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkResize.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkResize.Location = New System.Drawing.Point(25, 25)
    Me.chkResize.Name = "chkResize"
    Me.chkResize.Size = New System.Drawing.Size(161, 26)
    Me.chkResize.TabIndex = 1
    Me.chkResize.Text = "&Resize Photos"
    Me.ToolTip1.SetToolTip(Me.chkResize, "Convert the photos to a new size before saving")
    Me.chkResize.UseVisualStyleBackColor = False
    '
    'Frame1
    '
    Me.Frame1.Controls.Add(Me.chkResize)
    Me.Frame1.Controls.Add(Me.txtXres)
    Me.Frame1.Controls.Add(Me.txtYres)
    Me.Frame1.Controls.Add(Me.lbXres)
    Me.Frame1.Controls.Add(Me.lbYres)
    Me.Frame1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Frame1.Location = New System.Drawing.Point(25, 94)
    Me.Frame1.Name = "Frame1"
    Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
    Me.Frame1.Size = New System.Drawing.Size(312, 131)
    Me.Frame1.TabIndex = 11
    Me.Frame1.TabStop = False
    Me.Frame1.Text = "Resize"
    '
    'txtXres
    '
    Me.txtXres.AcceptsReturn = True
    Me.txtXres.BackColor = System.Drawing.SystemColors.Window
    Me.txtXres.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txtXres.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txtXres.Location = New System.Drawing.Point(243, 57)
    Me.txtXres.MaxLength = 0
    Me.txtXres.Name = "txtXres"
    Me.txtXres.Size = New System.Drawing.Size(57, 25)
    Me.txtXres.TabIndex = 3
    Me.txtXres.Text = "2000"
    '
    'txtYres
    '
    Me.txtYres.AcceptsReturn = True
    Me.txtYres.BackColor = System.Drawing.SystemColors.Window
    Me.txtYres.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txtYres.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txtYres.Location = New System.Drawing.Point(243, 86)
    Me.txtYres.MaxLength = 0
    Me.txtYres.Name = "txtYres"
    Me.txtYres.Size = New System.Drawing.Size(57, 25)
    Me.txtYres.TabIndex = 5
    Me.txtYres.Text = "2000"
    '
    'lbXres
    '
    Me.lbXres.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbXres.Location = New System.Drawing.Point(25, 60)
    Me.lbXres.Name = "lbXres"
    Me.lbXres.Size = New System.Drawing.Size(212, 22)
    Me.lbXres.TabIndex = 2
    Me.lbXres.Text = "Max. &Horizontal Resolution:"
    '
    'lbYres
    '
    Me.lbYres.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbYres.Location = New System.Drawing.Point(25, 90)
    Me.lbYres.Name = "lbYres"
    Me.lbYres.Size = New System.Drawing.Size(212, 21)
    Me.lbYres.TabIndex = 4
    Me.lbYres.Text = "Max. &Vertical Resolution:"
    '
    'txtQuality
    '
    Me.txtQuality.AcceptsReturn = True
    Me.txtQuality.BackColor = System.Drawing.SystemColors.Window
    Me.txtQuality.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txtQuality.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txtQuality.Location = New System.Drawing.Point(534, 114)
    Me.txtQuality.MaxLength = 0
    Me.txtQuality.Name = "txtQuality"
    Me.txtQuality.Size = New System.Drawing.Size(46, 25)
    Me.txtQuality.TabIndex = 7
    Me.txtQuality.Text = "15"
    '
    'cmdNoSave
    '
    Me.cmdNoSave.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdNoSave.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdNoSave.Location = New System.Drawing.Point(358, 262)
    Me.cmdNoSave.Name = "cmdNoSave"
    Me.cmdNoSave.Size = New System.Drawing.Size(117, 33)
    Me.cmdNoSave.TabIndex = 10
    Me.cmdNoSave.Text = "Do &Not Save"
    Me.cmdNoSave.UseVisualStyleBackColor = False
    '
    'cmdSave
    '
    Me.cmdSave.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdSave.Location = New System.Drawing.Point(208, 262)
    Me.cmdSave.Name = "cmdSave"
    Me.cmdSave.Size = New System.Drawing.Size(117, 33)
    Me.cmdSave.TabIndex = 9
    Me.cmdSave.Text = "&Save Photos"
    Me.cmdSave.UseVisualStyleBackColor = False
    '
    'lbCompression1
    '
    Me.lbCompression1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbCompression1.Location = New System.Drawing.Point(379, 119)
    Me.lbCompression1.Name = "lbCompression1"
    Me.lbCompression1.Size = New System.Drawing.Size(146, 21)
    Me.lbCompression1.TabIndex = 6
    Me.lbCompression1.Text = "&JPG Quality:"
    '
    'lbCompression
    '
    Me.lbCompression.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbCompression.Location = New System.Drawing.Point(376, 154)
    Me.lbCompression.Name = "lbCompression"
    Me.lbCompression.Size = New System.Drawing.Size(292, 108)
    Me.lbCompression.TabIndex = 8
    Me.lbCompression.Text = "(see form load for caption)"
    '
    'Label1
    '
    Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label1.Location = New System.Drawing.Point(22, 20)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(636, 81)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "The photos for the web site can be saved now, or you can copy them to the folder " & _
    "later. "
    '
    'frmSaveVerify
    '
    Me.AcceptButton = Me.cmdSave
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdNoSave
    Me.ClientSize = New System.Drawing.Size(680, 317)
    Me.Controls.Add(Me.Frame1)
    Me.Controls.Add(Me.txtQuality)
    Me.Controls.Add(Me.cmdNoSave)
    Me.Controls.Add(Me.cmdSave)
    Me.Controls.Add(Me.lbCompression1)
    Me.Controls.Add(Me.lbCompression)
    Me.Controls.Add(Me.Label1)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 28)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmSaveVerify"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Save Web Page Photos"
    Me.Frame1.ResumeLayout(False)
    Me.Frame1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
#End Region
End Class