<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmResize
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
  Public WithEvents txHres As TextBox
  Public WithEvents txVres As TextBox
  Public WithEvents cmdHelp As Button
  Public WithEvents cmdCancel As Button
  Public WithEvents cmdOK As Button
  Public WithEvents chkAspect As CheckBox
  Public WithEvents txPctH As TextBox
  Public WithEvents txPctW As TextBox
  Public WithEvents txSizeH As TextBox
  Public WithEvents txSizeW As TextBox
  Public WithEvents Label7 As Label
  Public WithEvents Label6 As Label
  Public WithEvents Label5 As Label
  Public WithEvents Label4 As Label
  Public WithEvents Label3 As Label
  Public WithEvents Label2 As Label
  Public WithEvents Label1 As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
Me.components = New System.ComponentModel.Container
Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmResize))
Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
Me.cmdHelp = New System.Windows.Forms.Button
Me.txHres = New System.Windows.Forms.TextBox
Me.txVres = New System.Windows.Forms.TextBox
Me.chkAspect = New System.Windows.Forms.CheckBox
Me.txPctH = New System.Windows.Forms.TextBox
Me.txPctW = New System.Windows.Forms.TextBox
Me.txSizeH = New System.Windows.Forms.TextBox
Me.txSizeW = New System.Windows.Forms.TextBox
Me.cmdCancel = New System.Windows.Forms.Button
Me.cmdOK = New System.Windows.Forms.Button
Me.Label7 = New System.Windows.Forms.Label
Me.Label6 = New System.Windows.Forms.Label
Me.Label5 = New System.Windows.Forms.Label
Me.Label4 = New System.Windows.Forms.Label
Me.Label3 = New System.Windows.Forms.Label
Me.Label2 = New System.Windows.Forms.Label
Me.Label1 = New System.Windows.Forms.Label
Me.SuspendLayout()
'
'cmdHelp
'
Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
Me.cmdHelp.Location = New System.Drawing.Point(445, 160)
Me.cmdHelp.Name = "cmdHelp"
Me.cmdHelp.Size = New System.Drawing.Size(41, 41)
Me.cmdHelp.TabIndex = 15
Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
Me.cmdHelp.UseVisualStyleBackColor = False
'
'txHres
'
Me.txHres.AcceptsReturn = True
Me.txHres.BackColor = System.Drawing.SystemColors.Window
Me.txHres.Cursor = System.Windows.Forms.Cursors.IBeam
Me.txHres.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.txHres.Location = New System.Drawing.Point(215, 220)
Me.txHres.MaxLength = 0
Me.txHres.Name = "txHres"
Me.txHres.Size = New System.Drawing.Size(96, 25)
Me.txHres.TabIndex = 10
Me.ToolTip1.SetToolTip(Me.txHres, "Dots per inch can be used in printing and desktop publishing.")
'
'txVres
'
Me.txVres.AcceptsReturn = True
Me.txVres.BackColor = System.Drawing.SystemColors.Window
Me.txVres.Cursor = System.Windows.Forms.Cursors.IBeam
Me.txVres.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.txVres.Location = New System.Drawing.Point(215, 250)
Me.txVres.MaxLength = 0
Me.txVres.Name = "txVres"
Me.txVres.Size = New System.Drawing.Size(96, 25)
Me.txVres.TabIndex = 12
Me.ToolTip1.SetToolTip(Me.txVres, "Dots per inch can be used in printing and desktop publishing.")
'
'chkAspect
'
Me.chkAspect.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.chkAspect.Location = New System.Drawing.Point(35, 145)
Me.chkAspect.Name = "chkAspect"
Me.chkAspect.Size = New System.Drawing.Size(197, 26)
Me.chkAspect.TabIndex = 8
Me.chkAspect.Text = "Maintain &Aspect Ratio"
Me.ToolTip1.SetToolTip(Me.chkAspect, "Check to keep the aspect ratio (width to height ratio) the same.")
Me.chkAspect.UseVisualStyleBackColor = False
'
'txPctH
'
Me.txPctH.AcceptsReturn = True
Me.txPctH.BackColor = System.Drawing.SystemColors.Window
Me.txPctH.Cursor = System.Windows.Forms.Cursors.IBeam
Me.txPctH.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.txPctH.Location = New System.Drawing.Point(390, 105)
Me.txPctH.MaxLength = 0
Me.txPctH.Name = "txPctH"
Me.txPctH.Size = New System.Drawing.Size(96, 25)
Me.txPctH.TabIndex = 7
Me.ToolTip1.SetToolTip(Me.txPctH, "Percentage change in height")
'
'txPctW
'
Me.txPctW.AcceptsReturn = True
Me.txPctW.BackColor = System.Drawing.SystemColors.Window
Me.txPctW.Cursor = System.Windows.Forms.Cursors.IBeam
Me.txPctW.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.txPctW.Location = New System.Drawing.Point(390, 75)
Me.txPctW.MaxLength = 0
Me.txPctW.Name = "txPctW"
Me.txPctW.Size = New System.Drawing.Size(96, 25)
Me.txPctW.TabIndex = 3
Me.ToolTip1.SetToolTip(Me.txPctW, "Percentage change in width")
'
'txSizeH
'
Me.txSizeH.AcceptsReturn = True
Me.txSizeH.BackColor = System.Drawing.SystemColors.Window
Me.txSizeH.Cursor = System.Windows.Forms.Cursors.IBeam
Me.txSizeH.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.txSizeH.Location = New System.Drawing.Point(132, 105)
Me.txSizeH.MaxLength = 0
Me.txSizeH.Name = "txSizeH"
Me.txSizeH.Size = New System.Drawing.Size(94, 25)
Me.txSizeH.TabIndex = 5
Me.ToolTip1.SetToolTip(Me.txSizeH, "New height, in pixels")
'
'txSizeW
'
Me.txSizeW.AcceptsReturn = True
Me.txSizeW.BackColor = System.Drawing.SystemColors.Window
Me.txSizeW.Cursor = System.Windows.Forms.Cursors.IBeam
Me.txSizeW.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.txSizeW.Location = New System.Drawing.Point(132, 75)
Me.txSizeW.MaxLength = 0
Me.txSizeW.Name = "txSizeW"
Me.txSizeW.Size = New System.Drawing.Size(94, 25)
Me.txSizeW.TabIndex = 1
Me.ToolTip1.SetToolTip(Me.txSizeW, "New width, in pixels")
'
'cmdCancel
'
Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.cmdCancel.Location = New System.Drawing.Point(425, 270)
Me.cmdCancel.Name = "cmdCancel"
Me.cmdCancel.Size = New System.Drawing.Size(91, 31)
Me.cmdCancel.TabIndex = 14
Me.cmdCancel.Text = "Cancel"
Me.cmdCancel.UseVisualStyleBackColor = False
'
'cmdOK
'
Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.cmdOK.Location = New System.Drawing.Point(425, 220)
Me.cmdOK.Name = "cmdOK"
Me.cmdOK.Size = New System.Drawing.Size(91, 31)
Me.cmdOK.TabIndex = 13
Me.cmdOK.Text = "&OK"
Me.cmdOK.UseVisualStyleBackColor = False
'
'Label7
'
Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.Label7.Location = New System.Drawing.Point(25, 225)
Me.Label7.Name = "Label7"
Me.Label7.Size = New System.Drawing.Size(186, 21)
Me.Label7.TabIndex = 9
Me.Label7.Text = "Hori&zontal Dots per Inch:"
'
'Label6
'
Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.Label6.Location = New System.Drawing.Point(25, 255)
Me.Label6.Name = "Label6"
Me.Label6.Size = New System.Drawing.Size(186, 21)
Me.Label6.TabIndex = 11
Me.Label6.Text = "Vertical &Dots per Inch:"
'
'Label5
'
Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.Label5.Location = New System.Drawing.Point(33, 35)
Me.Label5.Name = "Label5"
Me.Label5.Size = New System.Drawing.Size(267, 22)
Me.Label5.TabIndex = 16
Me.Label5.Text = "Enter the new size for the photo."
'
'Label4
'
Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.Label4.Location = New System.Drawing.Point(265, 110)
Me.Label4.Name = "Label4"
Me.Label4.Size = New System.Drawing.Size(122, 22)
Me.Label4.TabIndex = 6
Me.Label4.Text = "Percent &Change:"
'
'Label3
'
Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.Label3.Location = New System.Drawing.Point(265, 80)
Me.Label3.Name = "Label3"
Me.Label3.Size = New System.Drawing.Size(122, 27)
Me.Label3.TabIndex = 2
Me.Label3.Text = "&Percent Change:"
'
'Label2
'
Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.Label2.Location = New System.Drawing.Point(35, 110)
Me.Label2.Name = "Label2"
Me.Label2.Size = New System.Drawing.Size(86, 22)
Me.Label2.TabIndex = 4
Me.Label2.Text = "&Height:"
'
'Label1
'
Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.Label1.Location = New System.Drawing.Point(35, 80)
Me.Label1.Name = "Label1"
Me.Label1.Size = New System.Drawing.Size(86, 22)
Me.Label1.TabIndex = 0
Me.Label1.Text = "&Width:"
'
'frmResize
'
Me.AcceptButton = Me.cmdOK
Me.BackColor = System.Drawing.SystemColors.Control
Me.CancelButton = Me.cmdCancel
Me.ClientSize = New System.Drawing.Size(555, 333)
Me.Controls.Add(Me.txHres)
Me.Controls.Add(Me.txVres)
Me.Controls.Add(Me.cmdHelp)
Me.Controls.Add(Me.cmdCancel)
Me.Controls.Add(Me.cmdOK)
Me.Controls.Add(Me.chkAspect)
Me.Controls.Add(Me.txPctH)
Me.Controls.Add(Me.txPctW)
Me.Controls.Add(Me.txSizeH)
Me.Controls.Add(Me.txSizeW)
Me.Controls.Add(Me.Label7)
Me.Controls.Add(Me.Label6)
Me.Controls.Add(Me.Label5)
Me.Controls.Add(Me.Label4)
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
Me.Name = "frmResize"
Me.ShowInTaskbar = False
Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
Me.Text = "Resize"
Me.ResumeLayout(False)
Me.PerformLayout()

End Sub
#End Region
End Class