<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPicturize1
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
  Public WithEvents Label2 As Label
  Public WithEvents Frame1 As GroupBox
  Public WithEvents cmdCancel As Button
  Public WithEvents cmdPrevious As Button
  Public WithEvents cmdNext As Button
  Public WithEvents txtCellFolder As TextBox
  Public WithEvents cmdBrowse As Button
  Public WithEvents Label3 As Label
  Public WithEvents Label1 As Label
  Public WithEvents lbNPics As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPicturize1))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.cmdBrowse = New System.Windows.Forms.Button()
    Me.Frame1 = New System.Windows.Forms.GroupBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.cmdPrevious = New System.Windows.Forms.Button()
    Me.cmdNext = New System.Windows.Forms.Button()
    Me.txtCellFolder = New System.Windows.Forms.TextBox()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.lbNPics = New System.Windows.Forms.Label()
    Me.Frame1.SuspendLayout()
    Me.SuspendLayout()
    '
    'cmdHelp
    '
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(138, 377)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 41)
    Me.cmdHelp.TabIndex = 2
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'cmdBrowse
    '
    Me.cmdBrowse.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdBrowse.Location = New System.Drawing.Point(612, 284)
    Me.cmdBrowse.Name = "cmdBrowse"
    Me.cmdBrowse.Size = New System.Drawing.Size(81, 36)
    Me.cmdBrowse.TabIndex = 1
    Me.cmdBrowse.Text = "&Browse..."
    Me.ToolTip1.SetToolTip(Me.cmdBrowse, "Browse for the folder")
    Me.cmdBrowse.UseVisualStyleBackColor = False
    '
    'Frame1
    '
    Me.Frame1.Controls.Add(Me.Label2)
    Me.Frame1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Frame1.Location = New System.Drawing.Point(35, 96)
    Me.Frame1.Name = "Frame1"
    Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
    Me.Frame1.Size = New System.Drawing.Size(683, 127)
    Me.Frame1.TabIndex = 8
    Me.Frame1.TabStop = False
    '
    'Label2
    '
    Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label2.Location = New System.Drawing.Point(30, 30)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(650, 86)
    Me.Label2.TabIndex = 9
    Me.Label2.Text = "Label2"
    '
    'cmdCancel
    '
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdCancel.Location = New System.Drawing.Point(450, 382)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(91, 31)
    Me.cmdCancel.TabIndex = 5
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'cmdPrevious
    '
    Me.cmdPrevious.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdPrevious.Location = New System.Drawing.Point(214, 382)
    Me.cmdPrevious.Name = "cmdPrevious"
    Me.cmdPrevious.Size = New System.Drawing.Size(91, 31)
    Me.cmdPrevious.TabIndex = 3
    Me.cmdPrevious.Text = "&Previous"
    Me.cmdPrevious.UseVisualStyleBackColor = False
    '
    'cmdNext
    '
    Me.cmdNext.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdNext.Location = New System.Drawing.Point(332, 382)
    Me.cmdNext.Name = "cmdNext"
    Me.cmdNext.Size = New System.Drawing.Size(91, 31)
    Me.cmdNext.TabIndex = 4
    Me.cmdNext.Text = "&Next"
    Me.cmdNext.UseVisualStyleBackColor = False
    '
    'txtCellFolder
    '
    Me.txtCellFolder.AcceptsReturn = True
    Me.txtCellFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
    Me.txtCellFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories
    Me.txtCellFolder.BackColor = System.Drawing.SystemColors.Window
    Me.txtCellFolder.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txtCellFolder.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txtCellFolder.Location = New System.Drawing.Point(67, 291)
    Me.txtCellFolder.MaxLength = 0
    Me.txtCellFolder.Name = "txtCellFolder"
    Me.txtCellFolder.Size = New System.Drawing.Size(539, 25)
    Me.txtCellFolder.TabIndex = 0
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label3.Location = New System.Drawing.Point(65, 258)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(51, 17)
    Me.Label3.TabIndex = 7
    Me.Label3.Text = "Label3"
    '
    'Label1
    '
    Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label1.Location = New System.Drawing.Point(65, 15)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(573, 78)
    Me.Label1.TabIndex = 6
    Me.Label1.Text = "Label1"
    '
    'lbNPics
    '
    Me.lbNPics.AutoSize = True
    Me.lbNPics.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbNPics.Location = New System.Drawing.Point(85, 325)
    Me.lbNPics.Name = "lbNPics"
    Me.lbNPics.Size = New System.Drawing.Size(0, 17)
    Me.lbNPics.TabIndex = 2
    '
    'frmPicturize1
    '
    Me.AcceptButton = Me.cmdNext
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(782, 464)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.Frame1)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.cmdPrevious)
    Me.Controls.Add(Me.cmdNext)
    Me.Controls.Add(Me.txtCellFolder)
    Me.Controls.Add(Me.cmdBrowse)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.lbNPics)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 22)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmPicturize1"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Photo Mosaic"
    Me.Frame1.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

End Sub
#End Region
End Class