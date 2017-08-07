<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmFullscreen
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
  Public WithEvents Timer1 As Timer
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
Me.components = New System.ComponentModel.Container
Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFullscreen))
Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
Me.mnx = New System.Windows.Forms.ContextMenuStrip(Me.components)
Me.mnxNext = New System.Windows.Forms.ToolStripMenuItem
Me.mnxPrevious = New System.Windows.Forms.ToolStripMenuItem
Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
Me.mnxInfo = New System.Windows.Forms.ToolStripMenuItem
Me.mnxComment = New System.Windows.Forms.ToolStripMenuItem
Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
Me.mnxTag = New System.Windows.Forms.ToolStripMenuItem
Me.mnxDelete = New System.Windows.Forms.ToolStripMenuItem
Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
Me.mnxClose = New System.Windows.Forms.ToolStripMenuItem
Me.picTagCheck = New System.Windows.Forms.PictureBox
Me.mnx.SuspendLayout()
CType(Me.picTagCheck, System.ComponentModel.ISupportInitialize).BeginInit()
Me.SuspendLayout()
'
'Timer1
'
Me.Timer1.Interval = 1
'
'mnx
'
Me.mnx.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnxNext, Me.mnxPrevious, Me.ToolStripSeparator1, Me.mnxInfo, Me.mnxComment, Me.ToolStripSeparator3, Me.mnxTag, Me.mnxDelete, Me.ToolStripSeparator2, Me.mnxClose})
Me.mnx.Name = "mnx"
Me.mnx.Size = New System.Drawing.Size(250, 176)
'
'mnxNext
'
Me.mnxNext.Name = "mnxNext"
Me.mnxNext.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Right), System.Windows.Forms.Keys)
Me.mnxNext.Size = New System.Drawing.Size(249, 22)
Me.mnxNext.Text = "&Next"
'
'mnxPrevious
'
Me.mnxPrevious.Name = "mnxPrevious"
Me.mnxPrevious.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Left), System.Windows.Forms.Keys)
Me.mnxPrevious.Size = New System.Drawing.Size(249, 22)
Me.mnxPrevious.Text = "&Previous"
'
'ToolStripSeparator1
'
Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
Me.ToolStripSeparator1.Size = New System.Drawing.Size(246, 6)
'
'mnxInfo
'
Me.mnxInfo.Name = "mnxInfo"
Me.mnxInfo.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
Me.mnxInfo.Size = New System.Drawing.Size(249, 22)
Me.mnxInfo.Text = "&Info..."
'
'mnxComment
'
Me.mnxComment.Name = "mnxComment"
Me.mnxComment.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
Me.mnxComment.Size = New System.Drawing.Size(249, 22)
Me.mnxComment.Text = "&Add Comment..."
'
'ToolStripSeparator3
'
Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
Me.ToolStripSeparator3.Size = New System.Drawing.Size(246, 6)
'
'mnxTag
'
Me.mnxTag.Name = "mnxTag"
Me.mnxTag.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T), System.Windows.Forms.Keys)
Me.mnxTag.Size = New System.Drawing.Size(249, 22)
Me.mnxTag.Text = "&Tag"
'
'mnxDelete
'
Me.mnxDelete.Name = "mnxDelete"
Me.mnxDelete.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
Me.mnxDelete.Size = New System.Drawing.Size(249, 22)
Me.mnxDelete.Text = "&Delete"
'
'ToolStripSeparator2
'
Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
Me.ToolStripSeparator2.Size = New System.Drawing.Size(246, 6)
'
'mnxClose
'
Me.mnxClose.Name = "mnxClose"
Me.mnxClose.ShortcutKeyDisplayString = "Esc"
Me.mnxClose.Size = New System.Drawing.Size(249, 22)
Me.mnxClose.Text = "&Close"
'
'picTagCheck
'
Me.picTagCheck.Location = New System.Drawing.Point(460, 351)
Me.picTagCheck.Name = "picTagCheck"
Me.picTagCheck.Size = New System.Drawing.Size(56, 42)
Me.picTagCheck.TabIndex = 4
Me.picTagCheck.TabStop = False
'
'frmFullscreen
'
Me.BackColor = System.Drawing.SystemColors.WindowFrame
Me.ClientSize = New System.Drawing.Size(550, 428)
Me.ContextMenuStrip = Me.mnx
Me.ControlBox = False
Me.Controls.Add(Me.picTagCheck)
Me.Cursor = System.Windows.Forms.Cursors.Default
Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
Me.Name = "frmFullscreen"
Me.ShowInTaskbar = False
Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
Me.mnx.ResumeLayout(False)
CType(Me.picTagCheck, System.ComponentModel.ISupportInitialize).EndInit()
Me.ResumeLayout(False)

End Sub
 Friend WithEvents mnx As System.Windows.Forms.ContextMenuStrip
 Friend WithEvents mnxNext As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnxPrevious As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
 Friend WithEvents mnxTag As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnxDelete As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
 Friend WithEvents mnxClose As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnxInfo As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnxComment As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
 Friend WithEvents picTagCheck As System.Windows.Forms.PictureBox
#End Region
End Class