<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPicturize2
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
  Public WithEvents cmdNext As Button
  Public WithEvents cmdPrevious As Button
  Public WithEvents cmdCancel As Button
  Public WithEvents txnPicV As TextBox
  Public WithEvents txnPicH As TextBox
  Public WithEvents txCellsizeV As TextBox
  Public WithEvents txCellsizeH As TextBox
  Public WithEvents Label6 As Label
  Public WithEvents Label5 As Label
  Public WithEvents Label4 As Label
  Public WithEvents Label3 As Label
  Public WithEvents Label2 As Label
  Public WithEvents Label1 As Label
  Public WithEvents lbMain As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPicturize2))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.txnPicV = New System.Windows.Forms.TextBox()
    Me.txnPicH = New System.Windows.Forms.TextBox()
    Me.txCellsizeV = New System.Windows.Forms.TextBox()
    Me.txCellsizeH = New System.Windows.Forms.TextBox()
    Me.cmdNext = New System.Windows.Forms.Button()
    Me.cmdPrevious = New System.Windows.Forms.Button()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.lbMain = New System.Windows.Forms.Label()
    Me.chkUsePicsOnce = New System.Windows.Forms.CheckBox()
    Me.chkColorAdjust = New System.Windows.Forms.CheckBox()
    Me.SuspendLayout()
    '
    'cmdHelp
    '
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(50, 387)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 41)
    Me.cmdHelp.TabIndex = 12
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'txnPicV
    '
    Me.txnPicV.AcceptsReturn = True
    Me.txnPicV.BackColor = System.Drawing.SystemColors.Window
    Me.txnPicV.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txnPicV.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txnPicV.Location = New System.Drawing.Point(232, 187)
    Me.txnPicV.MaxLength = 0
    Me.txnPicV.Name = "txnPicV"
    Me.txnPicV.Size = New System.Drawing.Size(71, 25)
    Me.txnPicV.TabIndex = 7
    Me.ToolTip1.SetToolTip(Me.txnPicV, "Enter the number of cell images making up the final mosaic image")
    '
    'txnPicH
    '
    Me.txnPicH.AcceptsReturn = True
    Me.txnPicH.BackColor = System.Drawing.SystemColors.Window
    Me.txnPicH.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txnPicH.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txnPicH.Location = New System.Drawing.Point(232, 162)
    Me.txnPicH.MaxLength = 0
    Me.txnPicH.Name = "txnPicH"
    Me.txnPicH.Size = New System.Drawing.Size(71, 25)
    Me.txnPicH.TabIndex = 5
    Me.ToolTip1.SetToolTip(Me.txnPicH, "Enter the number of cell images making up the final mosaic image")
    '
    'txCellsizeV
    '
    Me.txCellsizeV.AcceptsReturn = True
    Me.txCellsizeV.BackColor = System.Drawing.SystemColors.Window
    Me.txCellsizeV.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txCellsizeV.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txCellsizeV.Location = New System.Drawing.Point(232, 92)
    Me.txCellsizeV.MaxLength = 0
    Me.txCellsizeV.Name = "txCellsizeV"
    Me.txCellsizeV.Size = New System.Drawing.Size(71, 25)
    Me.txCellsizeV.TabIndex = 3
    Me.ToolTip1.SetToolTip(Me.txCellsizeV, "The cell image size is the size of each" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "of the small images making up the mosaic" & _
        ".")
    '
    'txCellsizeH
    '
    Me.txCellsizeH.AcceptsReturn = True
    Me.txCellsizeH.BackColor = System.Drawing.SystemColors.Window
    Me.txCellsizeH.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txCellsizeH.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txCellsizeH.Location = New System.Drawing.Point(232, 67)
    Me.txCellsizeH.MaxLength = 0
    Me.txCellsizeH.Name = "txCellsizeH"
    Me.txCellsizeH.Size = New System.Drawing.Size(71, 25)
    Me.txCellsizeH.TabIndex = 1
    Me.ToolTip1.SetToolTip(Me.txCellsizeH, "The cell image size is the size of each" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "of the small images making up the mosaic" & _
        ".")
    '
    'cmdNext
    '
    Me.cmdNext.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdNext.Location = New System.Drawing.Point(250, 392)
    Me.cmdNext.Name = "cmdNext"
    Me.cmdNext.Size = New System.Drawing.Size(91, 31)
    Me.cmdNext.TabIndex = 14
    Me.cmdNext.Text = "&Next"
    Me.cmdNext.UseVisualStyleBackColor = False
    '
    'cmdPrevious
    '
    Me.cmdPrevious.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdPrevious.Location = New System.Drawing.Point(132, 392)
    Me.cmdPrevious.Name = "cmdPrevious"
    Me.cmdPrevious.Size = New System.Drawing.Size(91, 31)
    Me.cmdPrevious.TabIndex = 13
    Me.cmdPrevious.Text = "&Previous"
    Me.cmdPrevious.UseVisualStyleBackColor = False
    '
    'cmdCancel
    '
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdCancel.Location = New System.Drawing.Point(369, 392)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(91, 31)
    Me.cmdCancel.TabIndex = 15
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'Label6
    '
    Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label6.Location = New System.Drawing.Point(137, 192)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(86, 16)
    Me.Label6.TabIndex = 6
    Me.Label6.Text = "Ver&tical:"
    '
    'Label5
    '
    Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label5.Location = New System.Drawing.Point(137, 97)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(86, 16)
    Me.Label5.TabIndex = 2
    Me.Label5.Text = "&Vertical:"
    '
    'Label4
    '
    Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label4.Location = New System.Drawing.Point(137, 167)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(86, 16)
    Me.Label4.TabIndex = 4
    Me.Label4.Text = "Hori&zontal:"
    '
    'Label3
    '
    Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label3.Location = New System.Drawing.Point(137, 72)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(86, 16)
    Me.Label3.TabIndex = 0
    Me.Label3.Text = "&Horizontal:"
    '
    'Label2
    '
    Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label2.Location = New System.Drawing.Point(122, 137)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(212, 27)
    Me.Label2.TabIndex = 17
    Me.Label2.Text = "Cell Images in Main Image:"
    '
    'Label1
    '
    Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label1.Location = New System.Drawing.Point(122, 42)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(181, 21)
    Me.Label1.TabIndex = 16
    Me.Label1.Text = "Cell Image Size (Pixels):"
    '
    'lbMain
    '
    Me.lbMain.AutoSize = True
    Me.lbMain.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbMain.Location = New System.Drawing.Point(122, 326)
    Me.lbMain.Name = "lbMain"
    Me.lbMain.Size = New System.Drawing.Size(203, 17)
    Me.lbMain.TabIndex = 15
    Me.lbMain.Text = "Resulting Image Size (Pixels):"
    '
    'chkUsePicsOnce
    '
    Me.chkUsePicsOnce.AutoSize = True
    Me.chkUsePicsOnce.Location = New System.Drawing.Point(125, 281)
    Me.chkUsePicsOnce.Name = "chkUsePicsOnce"
    Me.chkUsePicsOnce.Size = New System.Drawing.Size(232, 21)
    Me.chkUsePicsOnce.TabIndex = 24
    Me.chkUsePicsOnce.Text = "Use each cell image only once."
    Me.chkUsePicsOnce.UseVisualStyleBackColor = True
    '
    'chkColorAdjust
    '
    Me.chkColorAdjust.AutoSize = True
    Me.chkColorAdjust.Location = New System.Drawing.Point(125, 242)
    Me.chkColorAdjust.Name = "chkColorAdjust"
    Me.chkColorAdjust.Size = New System.Drawing.Size(268, 21)
    Me.chkColorAdjust.TabIndex = 25
    Me.chkColorAdjust.Text = "Tint each cell image to match colors."
    Me.chkColorAdjust.UseVisualStyleBackColor = True
    '
    'frmPicturize2
    '
    Me.AcceptButton = Me.cmdNext
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(541, 476)
    Me.Controls.Add(Me.chkColorAdjust)
    Me.Controls.Add(Me.chkUsePicsOnce)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.cmdNext)
    Me.Controls.Add(Me.cmdPrevious)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.txnPicV)
    Me.Controls.Add(Me.txnPicH)
    Me.Controls.Add(Me.txCellsizeV)
    Me.Controls.Add(Me.txCellsizeH)
    Me.Controls.Add(Me.Label6)
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.lbMain)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 23)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmPicturize2"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Photo Mosaic"
    Me.ResumeLayout(False)
    Me.PerformLayout()

End Sub
  Friend WithEvents chkUsePicsOnce As System.Windows.Forms.CheckBox
  Friend WithEvents chkColorAdjust As System.Windows.Forms.CheckBox
#End Region
End Class