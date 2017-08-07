<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmAskAssociate
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
  Public WithEvents cmdYes As Button
  Public WithEvents cmdNo As Button
  Public WithEvents chkAskAgain As CheckBox
  Public WithEvents Label1 As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
Me.components = New System.ComponentModel.Container
Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAskAssociate))
Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
Me.cmdYes = New System.Windows.Forms.Button
Me.cmdNo = New System.Windows.Forms.Button
Me.chkAskAgain = New System.Windows.Forms.CheckBox
Me.Label1 = New System.Windows.Forms.Label
Me.cmdHelp = New System.Windows.Forms.Button
Me.SuspendLayout()
'
'cmdYes
'
Me.cmdYes.Font = New System.Drawing.Font("Arial", 9.0!)
Me.cmdYes.Location = New System.Drawing.Point(378, 126)
Me.cmdYes.Name = "cmdYes"
Me.cmdYes.Size = New System.Drawing.Size(91, 31)
Me.cmdYes.TabIndex = 1
Me.cmdYes.Text = "&Yes"
Me.cmdYes.UseVisualStyleBackColor = False
'
'cmdNo
'
Me.cmdNo.DialogResult = System.Windows.Forms.DialogResult.Cancel
Me.cmdNo.Font = New System.Drawing.Font("Arial", 9.0!)
Me.cmdNo.Location = New System.Drawing.Point(378, 171)
Me.cmdNo.Name = "cmdNo"
Me.cmdNo.Size = New System.Drawing.Size(91, 31)
Me.cmdNo.TabIndex = 2
Me.cmdNo.Text = "&No"
Me.cmdNo.UseVisualStyleBackColor = False
'
'chkAskAgain
'
Me.chkAskAgain.Font = New System.Drawing.Font("Arial", 9.0!)
Me.chkAskAgain.Location = New System.Drawing.Point(48, 177)
Me.chkAskAgain.Name = "chkAskAgain"
Me.chkAskAgain.Size = New System.Drawing.Size(256, 21)
Me.chkAskAgain.TabIndex = 0
Me.chkAskAgain.Text = "&Don't ask again"
Me.chkAskAgain.UseVisualStyleBackColor = False
'
'Label1
'
Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!)
Me.Label1.Location = New System.Drawing.Point(45, 36)
Me.Label1.Name = "Label1"
Me.Label1.Size = New System.Drawing.Size(281, 138)
Me.Label1.TabIndex = 3
Me.Label1.Text = "No image files are currently associated with Photo Mud. Do you want to select the" & _
    " image file types for Windows to associate with Photo Mud?"
'
'cmdHelp
'
Me.cmdHelp.Anchor = System.Windows.Forms.AnchorStyles.Bottom
Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
Me.cmdHelp.Location = New System.Drawing.Point(404, 70)
Me.cmdHelp.Name = "cmdHelp"
Me.cmdHelp.Size = New System.Drawing.Size(41, 39)
Me.cmdHelp.TabIndex = 4
Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
Me.cmdHelp.UseVisualStyleBackColor = False
'
'frmAskAssociate
'
Me.AcceptButton = Me.cmdYes
Me.CancelButton = Me.cmdNo
Me.ClientSize = New System.Drawing.Size(504, 235)
Me.Controls.Add(Me.cmdHelp)
Me.Controls.Add(Me.cmdYes)
Me.Controls.Add(Me.cmdNo)
Me.Controls.Add(Me.chkAskAgain)
Me.Controls.Add(Me.Label1)
Me.Cursor = System.Windows.Forms.Cursors.Default
Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
Me.Location = New System.Drawing.Point(3, 30)
Me.MaximizeBox = False
Me.MinimizeBox = False
Me.Name = "frmAskAssociate"
Me.ShowInTaskbar = False
Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
Me.Text = "Associate Files?"
Me.ResumeLayout(False)

End Sub
  Public WithEvents cmdHelp As System.Windows.Forms.Button
#End Region
End Class