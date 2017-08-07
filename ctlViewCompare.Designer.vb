<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlViewCompare
    Inherits System.Windows.Forms.UserControl

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctlViewCompare))
    Me.lbPoint = New System.Windows.Forms.Label()
    Me.lbPic2 = New System.Windows.Forms.Label()
    Me.lbPic1 = New System.Windows.Forms.Label()
    Me.lbZoom = New System.Windows.Forms.Label()
    Me.cmdZoomout = New System.Windows.Forms.Button()
    Me.cmdZoomin = New System.Windows.Forms.Button()
    Me.cmdNext = New System.Windows.Forms.Button()
    Me.cmdPrevious = New System.Windows.Forms.Button()
    Me.pView1 = New PhotoMud.pViewer()
    Me.pView0 = New PhotoMud.pViewer()
    CType(Me.pView1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pView0, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'lbPoint
    '
    Me.lbPoint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbPoint.AutoSize = True
    Me.lbPoint.Location = New System.Drawing.Point(122, 432)
    Me.lbPoint.Name = "lbPoint"
    Me.lbPoint.Size = New System.Drawing.Size(51, 17)
    Me.lbPoint.TabIndex = 101
    Me.lbPoint.Text = "lbPoint"
    '
    'lbPic2
    '
    Me.lbPic2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbPic2.AutoSize = True
    Me.lbPic2.Cursor = System.Windows.Forms.Cursors.Default
    Me.lbPic2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbPic2.Location = New System.Drawing.Point(924, 429)
    Me.lbPic2.Name = "lbPic2"
    Me.lbPic2.Size = New System.Drawing.Size(61, 17)
    Me.lbPic2.TabIndex = 99
    Me.lbPic2.Text = "Modified"
    Me.lbPic2.TextAlign = System.Drawing.ContentAlignment.TopRight
    '
    'lbPic1
    '
    Me.lbPic1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbPic1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbPic1.Location = New System.Drawing.Point(8, 429)
    Me.lbPic1.Name = "lbPic1"
    Me.lbPic1.Size = New System.Drawing.Size(125, 26)
    Me.lbPic1.TabIndex = 98
    Me.lbPic1.Text = "Original"
    '
    'lbZoom
    '
    Me.lbZoom.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbZoom.AutoSize = True
    Me.lbZoom.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbZoom.Location = New System.Drawing.Point(676, 429)
    Me.lbZoom.Name = "lbZoom"
    Me.lbZoom.Size = New System.Drawing.Size(68, 17)
    Me.lbZoom.TabIndex = 97
    Me.lbZoom.Text = "Zoom: 1x"
    '
    'cmdZoomout
    '
    Me.cmdZoomout.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdZoomout.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdZoomout.Image = CType(resources.GetObject("cmdZoomout.Image"), System.Drawing.Image)
    Me.cmdZoomout.Location = New System.Drawing.Point(478, 429)
    Me.cmdZoomout.Name = "cmdZoomout"
    Me.cmdZoomout.Size = New System.Drawing.Size(46, 48)
    Me.cmdZoomout.TabIndex = 95
    Me.cmdZoomout.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.cmdZoomout.UseVisualStyleBackColor = False
    '
    'cmdZoomin
    '
    Me.cmdZoomin.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdZoomin.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdZoomin.Image = CType(resources.GetObject("cmdZoomin.Image"), System.Drawing.Image)
    Me.cmdZoomin.Location = New System.Drawing.Point(531, 429)
    Me.cmdZoomin.Name = "cmdZoomin"
    Me.cmdZoomin.Size = New System.Drawing.Size(46, 48)
    Me.cmdZoomin.TabIndex = 96
    Me.cmdZoomin.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.cmdZoomin.UseVisualStyleBackColor = False
    '
    'cmdNext
    '
    Me.cmdNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdNext.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdNext.Image = CType(resources.GetObject("cmdNext.Image"), System.Drawing.Image)
    Me.cmdNext.ImageAlign = System.Drawing.ContentAlignment.TopCenter
    Me.cmdNext.Location = New System.Drawing.Point(797, 429)
    Me.cmdNext.Name = "cmdNext"
    Me.cmdNext.Size = New System.Drawing.Size(41, 41)
    Me.cmdNext.TabIndex = 103
    Me.cmdNext.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.cmdNext.UseVisualStyleBackColor = False
    '
    'cmdPrevious
    '
    Me.cmdPrevious.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPrevious.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdPrevious.Image = CType(resources.GetObject("cmdPrevious.Image"), System.Drawing.Image)
    Me.cmdPrevious.ImageAlign = System.Drawing.ContentAlignment.TopCenter
    Me.cmdPrevious.Location = New System.Drawing.Point(750, 429)
    Me.cmdPrevious.Name = "cmdPrevious"
    Me.cmdPrevious.Size = New System.Drawing.Size(41, 41)
    Me.cmdPrevious.TabIndex = 102
    Me.cmdPrevious.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.cmdPrevious.UseVisualStyleBackColor = False
    '
    'pView1
    '
    Me.pView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '
    'pView0
    '
    Me.pView0.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '
    'ctlViewCompare
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.Controls.Add(Me.pView1)
    Me.Controls.Add(Me.pView0)
    Me.Controls.Add(Me.cmdNext)
    Me.Controls.Add(Me.cmdPrevious)
    Me.Controls.Add(Me.lbPoint)
    Me.Controls.Add(Me.lbPic2)
    Me.Controls.Add(Me.lbPic1)
    Me.Controls.Add(Me.lbZoom)
    Me.Controls.Add(Me.cmdZoomout)
    Me.Controls.Add(Me.cmdZoomin)
    Me.Name = "ctlViewCompare"
    Me.Size = New System.Drawing.Size(1086, 504)
    CType(Me.pView1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pView0, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

End Sub
    Friend WithEvents lbPoint As System.Windows.Forms.Label
    Public WithEvents lbPic2 As System.Windows.Forms.Label
    Public WithEvents lbPic1 As System.Windows.Forms.Label
    Friend WithEvents lbZoom As System.Windows.Forms.Label
    Public WithEvents cmdZoomout As System.Windows.Forms.Button
    Public WithEvents cmdZoomin As System.Windows.Forms.Button
    Public WithEvents cmdNext As System.Windows.Forms.Button
    Public WithEvents cmdPrevious As System.Windows.Forms.Button
    Friend WithEvents pView0 As PhotoMud.pViewer
    Friend WithEvents pView1 As PhotoMud.pViewer

End Class
