<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmCombineSelection
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
  Public WithEvents ImageList1 As ImageList
  Public WithEvents cmdCancel As Button
  Public WithEvents cmdOK As Button
  Public WithEvents ListView1 As ListView
  Public WithEvents Label1 As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
Me.components = New System.ComponentModel.Container
Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCombineSelection))
Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
Me.cmdOK = New System.Windows.Forms.Button
Me.cmdCancel = New System.Windows.Forms.Button
Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
Me.ListView1 = New System.Windows.Forms.ListView
Me.Label1 = New System.Windows.Forms.Label
Me.SuspendLayout()
'
'cmdOK
'
Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!)

Me.cmdOK.Location = New System.Drawing.Point(405, 438)
Me.cmdOK.Name = "cmdOK"
Me.cmdOK.Size = New System.Drawing.Size(91, 31)
Me.cmdOK.TabIndex = 1
Me.cmdOK.Text = "&OK"
Me.ToolTip1.SetToolTip(Me.cmdOK, "Continue with selected photo")
Me.cmdOK.UseVisualStyleBackColor = False
'
'cmdCancel
'
Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)

Me.cmdCancel.Location = New System.Drawing.Point(405, 498)
Me.cmdCancel.Name = "cmdCancel"
Me.cmdCancel.Size = New System.Drawing.Size(91, 31)
Me.cmdCancel.TabIndex = 2
Me.cmdCancel.Text = "Cancel"
Me.cmdCancel.UseVisualStyleBackColor = False
'
'ImageList1
'
Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit
Me.ImageList1.ImageSize = New System.Drawing.Size(240, 160)
Me.ImageList1.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
'
'ListView1
'
Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
Me.ListView1.BackColor = System.Drawing.SystemColors.Window
Me.ListView1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.ListView1.LabelEdit = True
Me.ListView1.LargeImageList = Me.ImageList1
Me.ListView1.Location = New System.Drawing.Point(12, 60)
Me.ListView1.Name = "ListView1"
Me.ListView1.Size = New System.Drawing.Size(358, 469)
Me.ListView1.TabIndex = 0
Me.ListView1.UseCompatibleStateImageBehavior = False
'
'Label1
'
Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!)

Me.Label1.Location = New System.Drawing.Point(25, 15)
Me.Label1.Name = "Label1"
Me.Label1.Size = New System.Drawing.Size(306, 46)
Me.Label1.TabIndex = 3
Me.Label1.Text = "Which photo do you want to combine with the current one?"
'
'frmCombineSelection
'
Me.AcceptButton = Me.cmdOK
Me.BackColor = System.Drawing.SystemColors.Control
Me.CancelButton = Me.cmdCancel
Me.ClientSize = New System.Drawing.Size(526, 551)
Me.Controls.Add(Me.cmdCancel)
Me.Controls.Add(Me.cmdOK)
Me.Controls.Add(Me.ListView1)
Me.Controls.Add(Me.Label1)
Me.Cursor = System.Windows.Forms.Cursors.Default
Me.Font = New System.Drawing.Font("Arial", 9.0!)
Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
Me.Location = New System.Drawing.Point(3, 22)
Me.MaximizeBox = False
Me.MinimizeBox = False
Me.Name = "frmCombineSelection"
Me.ShowInTaskbar = False
Me.Text = "Combine Selection"
Me.ResumeLayout(False)

End Sub
#End Region
End Class