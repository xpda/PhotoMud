<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmFileTypes
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
  Public WithEvents cmdSelectAll As Button
  Public WithEvents cmdCancel As Button
  Public WithEvents cmdOK As Button
  Public WithEvents Label1 As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFileTypes))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdSelectAll = New System.Windows.Forms.Button()
    Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cmdSelectAll
        '
        Me.cmdSelectAll.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.cmdSelectAll.Location = New System.Drawing.Point(33, 86)
        Me.cmdSelectAll.Name = "cmdSelectAll"
        Me.cmdSelectAll.Size = New System.Drawing.Size(104, 34)
        Me.cmdSelectAll.TabIndex = 15
        Me.cmdSelectAll.Text = "Select &All"
        Me.ToolTip1.SetToolTip(Me.cmdSelectAll, "View all photo file types")
        Me.cmdSelectAll.UseVisualStyleBackColor = False
        '
        'cmdOK
        '
        Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.cmdOK.Location = New System.Drawing.Point(33, 126)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(104, 34)
        Me.cmdOK.TabIndex = 16
        Me.cmdOK.Text = "&OK"
        Me.ToolTip1.SetToolTip(Me.cmdOK, "Save and return")
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'cmdClear
        '
        Me.cmdClear.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.cmdClear.Location = New System.Drawing.Point(33, 206)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(104, 35)
        Me.cmdClear.TabIndex = 19
        Me.cmdClear.Text = "&Clear All"
        Me.ToolTip1.SetToolTip(Me.cmdClear, "Clear all selections")
        Me.cmdClear.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.cmdCancel.Location = New System.Drawing.Point(33, 166)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(104, 34)
        Me.cmdCancel.TabIndex = 17
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(30, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(188, 17)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Check the file types to view:"
        '
        'frmFileTypes
        '
        Me.AcceptButton = Me.cmdOK
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(782, 277)
        Me.Controls.Add(Me.cmdClear)
        Me.Controls.Add(Me.cmdSelectAll)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 27)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFileTypes"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Select File Types"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents cmdClear As System.Windows.Forms.Button
#End Region
End Class