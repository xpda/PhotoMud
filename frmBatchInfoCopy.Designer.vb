<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBatchInfoCopy
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBatchInfoCopy))
    Me.TreeViewDest = New System.Windows.Forms.TreeView()
    Me.imgTreeView = New System.Windows.Forms.ImageList(Me.components)
    Me.txDestPath = New System.Windows.Forms.TextBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.TreeViewSource = New System.Windows.Forms.TreeView()
    Me.txSourcePath = New System.Windows.Forms.TextBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
    Me.ProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdStart = New System.Windows.Forms.Button()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.chkCommentOnly = New System.Windows.Forms.CheckBox()
    Me.bkgSave = New System.ComponentModel.BackgroundWorker()
    Me.StatusStrip1.SuspendLayout()
    Me.SuspendLayout()
    '
    'TreeViewDest
    '
    Me.TreeViewDest.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TreeViewDest.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.TreeViewDest.ImageIndex = 0
    Me.TreeViewDest.ImageList = Me.imgTreeView
    Me.TreeViewDest.Location = New System.Drawing.Point(448, 63)
    Me.TreeViewDest.Name = "TreeViewDest"
    Me.TreeViewDest.SelectedImageIndex = 0
    Me.TreeViewDest.Size = New System.Drawing.Size(417, 477)
    Me.TreeViewDest.TabIndex = 30
    Me.TreeViewDest.TabStop = False
    '
    'imgTreeView
    '
    Me.imgTreeView.ImageStream = CType(resources.GetObject("imgTreeView.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.imgTreeView.TransparentColor = System.Drawing.Color.Transparent
    Me.imgTreeView.Images.SetKeyName(0, "ClosedFolder")
    Me.imgTreeView.Images.SetKeyName(1, "OpenFolder")
    '
    'txDestPath
    '
    Me.txDestPath.AcceptsReturn = True
    Me.txDestPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txDestPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
    Me.txDestPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories
    Me.txDestPath.BackColor = System.Drawing.SystemColors.Window
    Me.txDestPath.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txDestPath.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txDestPath.Location = New System.Drawing.Point(448, 35)
    Me.txDestPath.MaxLength = 0
    Me.txDestPath.Name = "txDestPath"
    Me.txDestPath.Size = New System.Drawing.Size(417, 25)
    Me.txDestPath.TabIndex = 3
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label4.Location = New System.Drawing.Point(445, 10)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(130, 17)
    Me.Label4.TabIndex = 2
    Me.Label4.Text = "&Destination Folder:"
    '
    'TreeViewSource
    '
    Me.TreeViewSource.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TreeViewSource.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.TreeViewSource.ImageIndex = 0
    Me.TreeViewSource.ImageList = Me.imgTreeView
    Me.TreeViewSource.Location = New System.Drawing.Point(11, 63)
    Me.TreeViewSource.Name = "TreeViewSource"
    Me.TreeViewSource.SelectedImageIndex = 0
    Me.TreeViewSource.Size = New System.Drawing.Size(417, 477)
    Me.TreeViewSource.TabIndex = 33
    Me.TreeViewSource.TabStop = False
    '
    'txSourcePath
    '
    Me.txSourcePath.AcceptsReturn = True
    Me.txSourcePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txSourcePath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
    Me.txSourcePath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories
    Me.txSourcePath.BackColor = System.Drawing.SystemColors.Window
    Me.txSourcePath.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txSourcePath.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txSourcePath.Location = New System.Drawing.Point(11, 35)
    Me.txSourcePath.MaxLength = 0
    Me.txSourcePath.Name = "txSourcePath"
    Me.txSourcePath.Size = New System.Drawing.Size(417, 25)
    Me.txSourcePath.TabIndex = 1
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label1.Location = New System.Drawing.Point(8, 10)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(104, 17)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "&Source Folder:"
    '
    'StatusStrip1
    '
    Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
    Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProgressBar1})
    Me.StatusStrip1.Location = New System.Drawing.Point(0, 655)
    Me.StatusStrip1.Name = "StatusStrip1"
    Me.StatusStrip1.Size = New System.Drawing.Size(879, 23)
    Me.StatusStrip1.TabIndex = 34
    Me.StatusStrip1.Text = "StatusStrip1"
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(200, 17)
    '
    'cmdStart
    '
    Me.cmdStart.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdStart.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdStart.Location = New System.Drawing.Point(545, 587)
    Me.cmdStart.Name = "cmdStart"
    Me.cmdStart.Size = New System.Drawing.Size(105, 51)
    Me.cmdStart.TabIndex = 5
    Me.cmdStart.Text = "Copy &Photo Information"
    Me.ToolTip1.SetToolTip(Me.cmdStart, "Copy the Exif and other photo information from the source folder to photos with t" & _
        "he same name in the destination folder.")
    Me.cmdStart.UseVisualStyleBackColor = False
    '
    'cmdCancel
    '
    Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdCancel.Location = New System.Drawing.Point(674, 587)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(105, 51)
    Me.cmdCancel.TabIndex = 6
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'Label2
    '
    Me.Label2.Location = New System.Drawing.Point(8, 549)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(453, 65)
    Me.Label2.TabIndex = 37
    Me.Label2.Text = "This function copies the photo information, "
    '
    'chkCommentOnly
    '
    Me.chkCommentOnly.AutoSize = True
    Me.chkCommentOnly.Location = New System.Drawing.Point(61, 625)
    Me.chkCommentOnly.Name = "chkCommentOnly"
    Me.chkCommentOnly.Size = New System.Drawing.Size(214, 21)
    Me.chkCommentOnly.TabIndex = 4
    Me.chkCommentOnly.Text = "&Copy only the comment field"
    Me.chkCommentOnly.UseVisualStyleBackColor = True
    '
    'frmBatchInfoCopy
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(879, 678)
    Me.Controls.Add(Me.chkCommentOnly)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.cmdStart)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.StatusStrip1)
    Me.Controls.Add(Me.TreeViewSource)
    Me.Controls.Add(Me.txSourcePath)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.TreeViewDest)
    Me.Controls.Add(Me.txDestPath)
    Me.Controls.Add(Me.Label4)
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "frmBatchInfoCopy"
    Me.Text = "Photo Information Batch Copy"
    Me.StatusStrip1.ResumeLayout(False)
    Me.StatusStrip1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TreeViewDest As System.Windows.Forms.TreeView
  Public WithEvents txDestPath As System.Windows.Forms.TextBox
  Public WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents TreeViewSource As System.Windows.Forms.TreeView
  Public WithEvents txSourcePath As System.Windows.Forms.TextBox
  Public WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents imgTreeView As System.Windows.Forms.ImageList
  Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ToolStripProgressBar
  Public WithEvents ToolTip1 As System.Windows.Forms.ToolTip
  Public WithEvents cmdStart As System.Windows.Forms.Button
  Public WithEvents cmdCancel As System.Windows.Forms.Button
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents chkCommentOnly As System.Windows.Forms.CheckBox
  Friend WithEvents bkgSave As System.ComponentModel.BackgroundWorker
End Class
