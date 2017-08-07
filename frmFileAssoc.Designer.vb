<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmFileAssoc
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
  Public WithEvents cmdSelAll As Button
  Public WithEvents cmdSelUnassoc As Button
  Public WithEvents cmdClear As Button
  Public WithEvents cmdOK As Button
  Public WithEvents cmdCancel As Button
  Public WithEvents Label1 As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFileAssoc))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.cmdSelAll = New System.Windows.Forms.Button()
    Me.cmdSelUnassoc = New System.Windows.Forms.Button()
    Me.cmdClear = New System.Windows.Forms.Button()
    Me.cmdOK = New System.Windows.Forms.Button()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.SuspendLayout()
    '
    'cmdHelp
    '
    Me.cmdHelp.AutoSize = True
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(69, 97)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 41)
    Me.cmdHelp.TabIndex = 15
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'cmdSelAll
    '
    Me.cmdSelAll.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdSelAll.Location = New System.Drawing.Point(31, 162)
    Me.cmdSelAll.Name = "cmdSelAll"
    Me.cmdSelAll.Size = New System.Drawing.Size(116, 35)
    Me.cmdSelAll.TabIndex = 16
    Me.cmdSelAll.Text = "Select &All"
    Me.ToolTip1.SetToolTip(Me.cmdSelAll, "Select all formats")
    Me.cmdSelAll.UseVisualStyleBackColor = False
    '
    'cmdSelUnassoc
    '
    Me.cmdSelUnassoc.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdSelUnassoc.Location = New System.Drawing.Point(31, 212)
    Me.cmdSelUnassoc.Name = "cmdSelUnassoc"
    Me.cmdSelUnassoc.Size = New System.Drawing.Size(116, 50)
    Me.cmdSelUnassoc.TabIndex = 17
    Me.cmdSelUnassoc.Text = "Select &Unassociated"
    Me.ToolTip1.SetToolTip(Me.cmdSelUnassoc, "Select all formats without a program associated with them")
    Me.cmdSelUnassoc.UseVisualStyleBackColor = False
    '
    'cmdClear
    '
    Me.cmdClear.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdClear.Location = New System.Drawing.Point(31, 277)
    Me.cmdClear.Name = "cmdClear"
    Me.cmdClear.Size = New System.Drawing.Size(116, 35)
    Me.cmdClear.TabIndex = 18
    Me.cmdClear.Text = "&Clear All"
    Me.ToolTip1.SetToolTip(Me.cmdClear, "Clear all selections")
    Me.cmdClear.UseVisualStyleBackColor = False
    '
    'cmdOK
    '
    Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdOK.Location = New System.Drawing.Point(31, 352)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(116, 35)
    Me.cmdOK.TabIndex = 19
    Me.cmdOK.Text = "&OK"
    Me.ToolTip1.SetToolTip(Me.cmdOK, "Save and return")
    Me.cmdOK.UseVisualStyleBackColor = False
    '
    'cmdCancel
    '
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdCancel.Location = New System.Drawing.Point(31, 402)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(116, 35)
    Me.cmdCancel.TabIndex = 20
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'Label1
    '
    Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label1.Location = New System.Drawing.Point(28, 15)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(578, 57)
    Me.Label1.TabIndex = 18
    Me.Label1.Text = "Select the image types you would like to have associated with this application. T" & _
    "hat will cause Photo Mud to display the image when you open it in Windows Explor" & _
    "er."
    '
    'frmFileAssoc
    '
    Me.AcceptButton = Me.cmdOK
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(653, 543)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.cmdSelAll)
    Me.Controls.Add(Me.cmdSelUnassoc)
    Me.Controls.Add(Me.cmdClear)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.Label1)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 23)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmFileAssoc"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Image File Associations"
    Me.ResumeLayout(False)
    Me.PerformLayout()

End Sub
#End Region
End Class