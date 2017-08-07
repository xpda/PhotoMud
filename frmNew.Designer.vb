<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmNew
#Region "Windows Form Designer generated code "
  <System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
    MyBase.New()
    'This call is required by the Windows Form Designer.
    InitializeComponent()
  End Sub
  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
    If Disposing Then
''			If Not components Is Nothing Then
''        components.Dispose()
''      End If
    End If
''    MyBase.Dispose(Disposing)
  End Sub
  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer
  Public ToolTip1 As ToolTip
  Public WithEvents cmdHelp As Button
  Public WithEvents cmdCancel As Button
  Public WithEvents cmdOK As Button
  Public WithEvents Label3 As Label
  Public WithEvents Label2 As Label
  Public WithEvents Label1 As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
Me.components = New System.ComponentModel.Container
Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNew))
Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
Me.cmdHelp = New System.Windows.Forms.Button
Me.cmdCancel = New System.Windows.Forms.Button
Me.cmdOK = New System.Windows.Forms.Button
Me.Label3 = New System.Windows.Forms.Label
Me.Label2 = New System.Windows.Forms.Label
Me.Label1 = New System.Windows.Forms.Label
Me.nmYres = New System.Windows.Forms.NumericUpDown
Me.nmXres = New System.Windows.Forms.NumericUpDown
CType(Me.nmYres, System.ComponentModel.ISupportInitialize).BeginInit()
CType(Me.nmXres, System.ComponentModel.ISupportInitialize).BeginInit()
Me.SuspendLayout()
'
'cmdHelp
'
Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!)

Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
Me.cmdHelp.Location = New System.Drawing.Point(275, 70)
Me.cmdHelp.Name = "cmdHelp"
Me.cmdHelp.Size = New System.Drawing.Size(46, 46)
Me.cmdHelp.TabIndex = 4
Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
Me.cmdHelp.UseVisualStyleBackColor = False
'
'cmdCancel
'
Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)

Me.cmdCancel.Location = New System.Drawing.Point(250, 185)
Me.cmdCancel.Name = "cmdCancel"
Me.cmdCancel.Size = New System.Drawing.Size(91, 31)
Me.cmdCancel.TabIndex = 6
Me.cmdCancel.Text = "Cancel"
Me.cmdCancel.UseVisualStyleBackColor = False
'
'cmdOK
'
Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!)

Me.cmdOK.Location = New System.Drawing.Point(250, 135)
Me.cmdOK.Name = "cmdOK"
Me.cmdOK.Size = New System.Drawing.Size(91, 31)
Me.cmdOK.TabIndex = 5
Me.cmdOK.Text = "&OK"
Me.cmdOK.UseVisualStyleBackColor = False
'
'Label3
'
Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!)

Me.Label3.Location = New System.Drawing.Point(30, 25)
Me.Label3.Name = "Label3"
Me.Label3.Size = New System.Drawing.Size(331, 23)
Me.Label3.TabIndex = 6
Me.Label3.Text = "Enter the size for the new image, in pixels."
'
'Label2
'
Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!)

Me.Label2.Location = New System.Drawing.Point(44, 124)
Me.Label2.Name = "Label2"
Me.Label2.Size = New System.Drawing.Size(79, 18)
Me.Label2.TabIndex = 2
Me.Label2.Text = "&Height:"
'
'Label1
'
Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!)

Me.Label1.Location = New System.Drawing.Point(44, 94)
Me.Label1.Name = "Label1"
Me.Label1.Size = New System.Drawing.Size(79, 18)
Me.Label1.TabIndex = 0
Me.Label1.Text = "&Width:"
'
'nmYres
'
Me.nmYres.Location = New System.Drawing.Point(123, 122)
Me.nmYres.Maximum = New Decimal(New Integer() {20000, 0, 0, 0})
Me.nmYres.Minimum = New Decimal(New Integer() {20, 0, 0, 0})
Me.nmYres.Name = "nmYres"
Me.nmYres.Size = New System.Drawing.Size(84, 25)
Me.nmYres.TabIndex = 3
Me.nmYres.Value = New Decimal(New Integer() {20000, 0, 0, 0})
'
'nmXres
'
Me.nmXres.Location = New System.Drawing.Point(123, 92)
Me.nmXres.Maximum = New Decimal(New Integer() {20000, 0, 0, 0})
Me.nmXres.Minimum = New Decimal(New Integer() {20, 0, 0, 0})
Me.nmXres.Name = "nmXres"
Me.nmXres.Size = New System.Drawing.Size(84, 25)
Me.nmXres.TabIndex = 1
Me.nmXres.Value = New Decimal(New Integer() {20000, 0, 0, 0})
'
'frmNew
'
Me.AcceptButton = Me.cmdOK
Me.BackColor = System.Drawing.SystemColors.Control
Me.CancelButton = Me.cmdCancel
Me.ClientSize = New System.Drawing.Size(368, 241)
Me.Controls.Add(Me.nmYres)
Me.Controls.Add(Me.nmXres)
Me.Controls.Add(Me.cmdHelp)
Me.Controls.Add(Me.cmdCancel)
Me.Controls.Add(Me.cmdOK)
Me.Controls.Add(Me.Label3)
Me.Controls.Add(Me.Label2)
Me.Controls.Add(Me.Label1)
Me.Cursor = System.Windows.Forms.Cursors.Default
Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
Me.Location = New System.Drawing.Point(3, 23)
Me.MaximizeBox = False
Me.MinimizeBox = False
Me.Name = "frmNew"
Me.ShowInTaskbar = False
Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
Me.Text = "New Image"
CType(Me.nmYres, System.ComponentModel.ISupportInitialize).EndInit()
CType(Me.nmXres, System.ComponentModel.ISupportInitialize).EndInit()
Me.ResumeLayout(False)

End Sub
  Friend WithEvents nmYres As System.Windows.Forms.NumericUpDown
  Friend WithEvents nmXres As System.Windows.Forms.NumericUpDown
#End Region
End Class