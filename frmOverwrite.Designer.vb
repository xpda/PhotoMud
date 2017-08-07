<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmOverwrite
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
  Public WithEvents chkAll As CheckBox
  Public WithEvents cmdCancel As Button
  Public WithEvents cmdNo As Button
  Public WithEvents cmdYes As Button
  Public WithEvents Label1 As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
Me.components = New System.ComponentModel.Container
Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOverwrite))
Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
Me.chkAll = New System.Windows.Forms.CheckBox
Me.cmdCancel = New System.Windows.Forms.Button
Me.cmdNo = New System.Windows.Forms.Button
Me.cmdYes = New System.Windows.Forms.Button
Me.Label1 = New System.Windows.Forms.Label
Me.SuspendLayout()
'
'chkAll
'
Me.chkAll.Font = New System.Drawing.Font("Arial", 9.0!)
Me.chkAll.Location = New System.Drawing.Point(421, 101)
Me.chkAll.Name = "chkAll"
Me.chkAll.Size = New System.Drawing.Size(101, 22)
Me.chkAll.TabIndex = 3
Me.chkAll.Text = "&All Files"
Me.chkAll.UseVisualStyleBackColor = False
'
'cmdCancel
'
Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)
Me.cmdCancel.Location = New System.Drawing.Point(317, 95)
Me.cmdCancel.Name = "cmdCancel"
Me.cmdCancel.Size = New System.Drawing.Size(77, 33)
Me.cmdCancel.TabIndex = 2
Me.cmdCancel.Text = "&Cancel"
Me.cmdCancel.UseVisualStyleBackColor = False
'
'cmdNo
'
Me.cmdNo.Font = New System.Drawing.Font("Arial", 9.0!)
Me.cmdNo.Location = New System.Drawing.Point(221, 95)
Me.cmdNo.Name = "cmdNo"
Me.cmdNo.Size = New System.Drawing.Size(77, 33)
Me.cmdNo.TabIndex = 1
Me.cmdNo.Text = "&No"
Me.cmdNo.UseVisualStyleBackColor = False
'
'cmdYes
'
Me.cmdYes.Font = New System.Drawing.Font("Arial", 9.0!)
Me.cmdYes.Location = New System.Drawing.Point(126, 95)
Me.cmdYes.Name = "cmdYes"
Me.cmdYes.Size = New System.Drawing.Size(77, 33)
Me.cmdYes.TabIndex = 0
Me.cmdYes.Text = "&Yes"
Me.cmdYes.UseVisualStyleBackColor = False
'
'Label1
'
Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!)
Me.Label1.Location = New System.Drawing.Point(12, 31)
Me.Label1.Name = "Label1"
Me.Label1.Size = New System.Drawing.Size(624, 61)
Me.Label1.TabIndex = 0
Me.Label1.Text = "Label1"
'
'frmOverwrite
'
Me.AcceptButton = Me.cmdNo
Me.BackColor = System.Drawing.SystemColors.Control
Me.CancelButton = Me.cmdCancel
Me.ClientSize = New System.Drawing.Size(648, 159)
Me.Controls.Add(Me.chkAll)
Me.Controls.Add(Me.cmdCancel)
Me.Controls.Add(Me.cmdNo)
Me.Controls.Add(Me.cmdYes)
Me.Controls.Add(Me.Label1)
Me.Cursor = System.Windows.Forms.Cursors.Default
Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
Me.Location = New System.Drawing.Point(3, 27)
Me.MaximizeBox = False
Me.MinimizeBox = False
Me.Name = "frmOverwrite"
Me.ShowInTaskbar = False
Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
Me.Text = "Overwrite File?"
Me.ResumeLayout(False)

End Sub
#End Region
End Class