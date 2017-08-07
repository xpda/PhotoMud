<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmExpand
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
  Public WithEvents optDown As RadioButton
  Public WithEvents optUp As RadioButton
  Public WithEvents Frame2 As GroupBox
  Public WithEvents optLeft As RadioButton
  Public WithEvents optRight As RadioButton
  Public WithEvents Frame1 As GroupBox
  Public WithEvents cmdHelp As Button
  Public WithEvents cmdCancel As Button
  Public WithEvents cmdOK As Button
  Public WithEvents Label2 As Label
  Public WithEvents lbYres As Label
  Public WithEvents lbXres As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
Me.components = New System.ComponentModel.Container
Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExpand))
Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
Me.cmdHelp = New System.Windows.Forms.Button
Me.optDown = New System.Windows.Forms.RadioButton
Me.optUp = New System.Windows.Forms.RadioButton
Me.optLeft = New System.Windows.Forms.RadioButton
Me.optRight = New System.Windows.Forms.RadioButton
Me.nmYres = New System.Windows.Forms.NumericUpDown
Me.nmXres = New System.Windows.Forms.NumericUpDown
Me.Frame2 = New System.Windows.Forms.GroupBox
Me.Frame1 = New System.Windows.Forms.GroupBox
Me.cmdCancel = New System.Windows.Forms.Button
Me.cmdOK = New System.Windows.Forms.Button
Me.Label2 = New System.Windows.Forms.Label
Me.lbYres = New System.Windows.Forms.Label
Me.lbXres = New System.Windows.Forms.Label
CType(Me.nmYres, System.ComponentModel.ISupportInitialize).BeginInit()
CType(Me.nmXres, System.ComponentModel.ISupportInitialize).BeginInit()
Me.Frame2.SuspendLayout()
Me.Frame1.SuspendLayout()
Me.SuspendLayout()
'
'cmdHelp
'
Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
Me.cmdHelp.Location = New System.Drawing.Point(575, 175)
Me.cmdHelp.Name = "cmdHelp"
Me.cmdHelp.Size = New System.Drawing.Size(41, 41)
Me.cmdHelp.TabIndex = 10
Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
Me.cmdHelp.UseVisualStyleBackColor = False
'
'optDown
'
Me.optDown.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

Me.optDown.Location = New System.Drawing.Point(125, 10)
Me.optDown.Name = "optDown"
Me.optDown.Size = New System.Drawing.Size(101, 26)
Me.optDown.TabIndex = 7
Me.optDown.TabStop = True
Me.optDown.Text = "&Down"
Me.ToolTip1.SetToolTip(Me.optDown, "Expand downward")
Me.optDown.UseVisualStyleBackColor = False
'
'optUp
'
Me.optUp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

Me.optUp.Location = New System.Drawing.Point(20, 10)
Me.optUp.Name = "optUp"
Me.optUp.Size = New System.Drawing.Size(101, 26)
Me.optUp.TabIndex = 6
Me.optUp.TabStop = True
Me.optUp.Text = "&Up"
Me.ToolTip1.SetToolTip(Me.optUp, "Expand upward")
Me.optUp.UseVisualStyleBackColor = False
'
'optLeft
'
Me.optLeft.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

Me.optLeft.Location = New System.Drawing.Point(20, 10)
Me.optLeft.Name = "optLeft"
Me.optLeft.Size = New System.Drawing.Size(101, 26)
Me.optLeft.TabIndex = 4
Me.optLeft.TabStop = True
Me.optLeft.Text = "&Left"
Me.ToolTip1.SetToolTip(Me.optLeft, "Expand to the left")
Me.optLeft.UseVisualStyleBackColor = False
'
'optRight
'
Me.optRight.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

Me.optRight.Location = New System.Drawing.Point(125, 10)
Me.optRight.Name = "optRight"
Me.optRight.Size = New System.Drawing.Size(101, 26)
Me.optRight.TabIndex = 5
Me.optRight.TabStop = True
Me.optRight.Text = "&Right"
Me.ToolTip1.SetToolTip(Me.optRight, "Expand to the right")
Me.optRight.UseVisualStyleBackColor = False
'
'nmYres
'
Me.nmYres.Location = New System.Drawing.Point(293, 103)
Me.nmYres.Maximum = New Decimal(New Integer() {20000, 0, 0, 0})
Me.nmYres.Minimum = New Decimal(New Integer() {20, 0, 0, 0})
Me.nmYres.Name = "nmYres"
Me.nmYres.Size = New System.Drawing.Size(85, 25)
Me.nmYres.TabIndex = 3
Me.ToolTip1.SetToolTip(Me.nmYres, "New vertical resolution, in pixels")
Me.nmYres.Value = New Decimal(New Integer() {20000, 0, 0, 0})
'
'nmXres
'
Me.nmXres.Location = New System.Drawing.Point(293, 68)
Me.nmXres.Maximum = New Decimal(New Integer() {20000, 0, 0, 0})
Me.nmXres.Minimum = New Decimal(New Integer() {20, 0, 0, 0})
Me.nmXres.Name = "nmXres"
Me.nmXres.Size = New System.Drawing.Size(85, 25)
Me.nmXres.TabIndex = 1
Me.ToolTip1.SetToolTip(Me.nmXres, "New horizontal resolution, in pixels")
Me.nmXres.Value = New Decimal(New Integer() {20000, 0, 0, 0})
'
'Frame2
'
Me.Frame2.Controls.Add(Me.optDown)
Me.Frame2.Controls.Add(Me.optUp)
Me.Frame2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

Me.Frame2.Location = New System.Drawing.Point(445, 100)
Me.Frame2.Name = "Frame2"
Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
Me.Frame2.Size = New System.Drawing.Size(231, 41)
Me.Frame2.TabIndex = 13
Me.Frame2.TabStop = False
'
'Frame1
'
Me.Frame1.Controls.Add(Me.optLeft)
Me.Frame1.Controls.Add(Me.optRight)
Me.Frame1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

Me.Frame1.Location = New System.Drawing.Point(445, 55)
Me.Frame1.Name = "Frame1"
Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
Me.Frame1.Size = New System.Drawing.Size(231, 41)
Me.Frame1.TabIndex = 12
Me.Frame1.TabStop = False
'
'cmdCancel
'
Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

Me.cmdCancel.Location = New System.Drawing.Point(441, 175)
Me.cmdCancel.Name = "cmdCancel"
Me.cmdCancel.Size = New System.Drawing.Size(91, 35)
Me.cmdCancel.TabIndex = 9
Me.cmdCancel.Text = "Cancel"
Me.cmdCancel.UseVisualStyleBackColor = False
'
'cmdOK
'
Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

Me.cmdOK.Location = New System.Drawing.Point(311, 175)
Me.cmdOK.Name = "cmdOK"
Me.cmdOK.Size = New System.Drawing.Size(91, 35)
Me.cmdOK.TabIndex = 8
Me.cmdOK.Text = "&OK"
Me.cmdOK.UseVisualStyleBackColor = False
'
'Label2
'
Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

Me.Label2.Location = New System.Drawing.Point(448, 36)
Me.Label2.Name = "Label2"
Me.Label2.Size = New System.Drawing.Size(211, 26)
Me.Label2.TabIndex = 4
Me.Label2.Text = "Expansion Direction"
'
'lbYres
'
Me.lbYres.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

Me.lbYres.Location = New System.Drawing.Point(20, 105)
Me.lbYres.Name = "lbYres"
Me.lbYres.Size = New System.Drawing.Size(249, 21)
Me.lbYres.TabIndex = 2
Me.lbYres.Text = "&Vertical Size (currently 1024 pixels):"
'
'lbXres
'
Me.lbXres.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

Me.lbXres.Location = New System.Drawing.Point(20, 70)
Me.lbXres.Name = "lbXres"
Me.lbXres.Size = New System.Drawing.Size(281, 21)
Me.lbXres.TabIndex = 0
Me.lbXres.Text = "&Horizontal Size (currently 1024 pixels):"
'
'frmExpand
'
Me.AcceptButton = Me.cmdOK
Me.BackColor = System.Drawing.SystemColors.Control
Me.CancelButton = Me.cmdCancel
Me.ClientSize = New System.Drawing.Size(766, 243)
Me.Controls.Add(Me.nmYres)
Me.Controls.Add(Me.Frame2)
Me.Controls.Add(Me.nmXres)
Me.Controls.Add(Me.Frame1)
Me.Controls.Add(Me.cmdHelp)
Me.Controls.Add(Me.cmdCancel)
Me.Controls.Add(Me.cmdOK)
Me.Controls.Add(Me.Label2)
Me.Controls.Add(Me.lbYres)
Me.Controls.Add(Me.lbXres)
Me.Cursor = System.Windows.Forms.Cursors.Default
Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
Me.Location = New System.Drawing.Point(3, 22)
Me.MaximizeBox = False
Me.MinimizeBox = False
Me.Name = "frmExpand"
Me.ShowInTaskbar = False
Me.Text = "Image Expand"
Me.ToolTip1.SetToolTip(Me, "Help")
CType(Me.nmYres, System.ComponentModel.ISupportInitialize).EndInit()
CType(Me.nmXres, System.ComponentModel.ISupportInitialize).EndInit()
Me.Frame2.ResumeLayout(False)
Me.Frame1.ResumeLayout(False)
Me.ResumeLayout(False)

End Sub
  Friend WithEvents nmYres As System.Windows.Forms.NumericUpDown
  Friend WithEvents nmXres As System.Windows.Forms.NumericUpDown
#End Region
End Class