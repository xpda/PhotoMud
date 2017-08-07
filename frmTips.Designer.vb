<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmTips
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
  Public WithEvents Command1 As Button
  Public WithEvents Label1 As Label
  Public WithEvents lbtip As Label
  Public WithEvents Picture1 As Panel
  Public WithEvents chkShowTips As CheckBox
  Public WithEvents cmdNext As Button
  Public WithEvents cmdClose As Button
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTips))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.Command1 = New System.Windows.Forms.Button()
    Me.Picture1 = New System.Windows.Forms.Panel()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.lbtip = New System.Windows.Forms.Label()
    Me.chkShowTips = New System.Windows.Forms.CheckBox()
    Me.cmdNext = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.Picture1.SuspendLayout()
    Me.SuspendLayout()
    '
    'Command1
    '
    Me.Command1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.Command1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Command1.Location = New System.Drawing.Point(385, 325)
    Me.Command1.Name = "Command1"
    Me.Command1.Size = New System.Drawing.Size(106, 31)
    Me.Command1.TabIndex = 2
    Me.Command1.Text = "&Restart Tips"
    Me.Command1.UseVisualStyleBackColor = False
    '
    'Picture1
    '
    Me.Picture1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Picture1.BackColor = System.Drawing.SystemColors.ActiveCaptionText
    Me.Picture1.BackgroundImage = CType(resources.GetObject("Picture1.BackgroundImage"), System.Drawing.Image)
    Me.Picture1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Picture1.Controls.Add(Me.Label1)
    Me.Picture1.Controls.Add(Me.lbtip)
    Me.Picture1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Picture1.Location = New System.Drawing.Point(15, 15)
    Me.Picture1.Name = "Picture1"
    Me.Picture1.Size = New System.Drawing.Size(611, 291)
    Me.Picture1.TabIndex = 3
    '
    'Label1
    '
    Me.Label1.BackColor = System.Drawing.Color.Transparent
    Me.Label1.Font = New System.Drawing.Font("Times New Roman", 24.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label1.Location = New System.Drawing.Point(10, 10)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(551, 46)
    Me.Label1.TabIndex = 6
    Me.Label1.Text = "Did you know..."
    '
    'lbtip
    '
    Me.lbtip.BackColor = System.Drawing.Color.Transparent
    Me.lbtip.Font = New System.Drawing.Font("Times New Roman", 11.0!)
    Me.lbtip.Location = New System.Drawing.Point(84, 98)
    Me.lbtip.Name = "lbtip"
    Me.lbtip.Size = New System.Drawing.Size(419, 163)
    Me.lbtip.TabIndex = 4
    '
    'chkShowTips
    '
    Me.chkShowTips.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.chkShowTips.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkShowTips.Location = New System.Drawing.Point(35, 325)
    Me.chkShowTips.Name = "chkShowTips"
    Me.chkShowTips.Size = New System.Drawing.Size(196, 26)
    Me.chkShowTips.TabIndex = 0
    Me.chkShowTips.Text = "&Show tips at startup"
    Me.chkShowTips.UseVisualStyleBackColor = False
    '
    'cmdNext
    '
    Me.cmdNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdNext.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdNext.Location = New System.Drawing.Point(260, 325)
    Me.cmdNext.Name = "cmdNext"
    Me.cmdNext.Size = New System.Drawing.Size(106, 31)
    Me.cmdNext.TabIndex = 1
    Me.cmdNext.Text = "&Next Tip"
    Me.cmdNext.UseVisualStyleBackColor = False
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdClose.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdClose.Location = New System.Drawing.Point(510, 325)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(106, 31)
    Me.cmdClose.TabIndex = 3
    Me.cmdClose.Text = "&Close"
    Me.cmdClose.UseVisualStyleBackColor = False
    '
    'frmTips
    '
    Me.AcceptButton = Me.cmdClose
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdClose
    Me.ClientSize = New System.Drawing.Size(641, 366)
    Me.Controls.Add(Me.Command1)
    Me.Controls.Add(Me.Picture1)
    Me.Controls.Add(Me.chkShowTips)
    Me.Controls.Add(Me.cmdNext)
    Me.Controls.Add(Me.cmdClose)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 30)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmTips"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Tip of the Day"
    Me.Picture1.ResumeLayout(False)
    Me.ResumeLayout(False)

End Sub
#End Region 
End Class