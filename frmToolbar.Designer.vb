<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmToolbar
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
  Public WithEvents cmdReset As Button
  Public WithEvents List2 As ListBox
  Public WithEvents List1 As ListBox
  Public WithEvents OptionLarge As RadioButton
  Public WithEvents OptionSmall As RadioButton
  Public WithEvents cmdMoveDown As Button
  Public WithEvents cmdMoveUp As Button
  Public WithEvents cmdRemove As Button
  Public WithEvents cmdAdd As Button
  Public WithEvents cmdOK As Button
  Public WithEvents cmdCancel As Button
  Public WithEvents cmdHelp As Button
  Public WithEvents Label2 As Label
  Public WithEvents Label1 As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
Me.components = New System.ComponentModel.Container
Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmToolbar))
Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
Me.cmdHelp = New System.Windows.Forms.Button
Me.cmdReset = New System.Windows.Forms.Button
Me.OptionLarge = New System.Windows.Forms.RadioButton
Me.OptionSmall = New System.Windows.Forms.RadioButton
Me.cmdMoveDown = New System.Windows.Forms.Button
Me.cmdMoveUp = New System.Windows.Forms.Button
Me.cmdOK = New System.Windows.Forms.Button
Me.chkToolbarText = New System.Windows.Forms.CheckBox
Me.List2 = New System.Windows.Forms.ListBox
Me.List1 = New System.Windows.Forms.ListBox
Me.cmdRemove = New System.Windows.Forms.Button
Me.cmdAdd = New System.Windows.Forms.Button
Me.cmdCancel = New System.Windows.Forms.Button
Me.Label2 = New System.Windows.Forms.Label
Me.Label1 = New System.Windows.Forms.Label
Me.SuspendLayout()
'
'cmdHelp
'
Me.cmdHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!)

Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
Me.cmdHelp.Location = New System.Drawing.Point(668, 50)
Me.cmdHelp.Name = "cmdHelp"
Me.cmdHelp.Size = New System.Drawing.Size(41, 41)
Me.cmdHelp.TabIndex = 7
Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
Me.cmdHelp.UseVisualStyleBackColor = False
'
'cmdReset
'
Me.cmdReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
Me.cmdReset.Font = New System.Drawing.Font("Arial", 9.0!)

Me.cmdReset.Location = New System.Drawing.Point(648, 370)
Me.cmdReset.Name = "cmdReset"
Me.cmdReset.Size = New System.Drawing.Size(91, 31)
Me.cmdReset.TabIndex = 12
Me.cmdReset.Text = "Rese&t"
Me.ToolTip1.SetToolTip(Me.cmdReset, "Reset toolbars to program defaults")
Me.cmdReset.UseVisualStyleBackColor = False
'
'OptionLarge
'
Me.OptionLarge.Anchor = System.Windows.Forms.AnchorStyles.Bottom
Me.OptionLarge.Font = New System.Drawing.Font("Arial", 9.0!)

Me.OptionLarge.Location = New System.Drawing.Point(404, 404)
Me.OptionLarge.Name = "OptionLarge"
Me.OptionLarge.Size = New System.Drawing.Size(176, 22)
Me.OptionLarge.TabIndex = 6
Me.OptionLarge.TabStop = True
Me.OptionLarge.Text = "&Large Icons"
Me.ToolTip1.SetToolTip(Me.OptionLarge, "Use large toolbar icons")
Me.OptionLarge.UseVisualStyleBackColor = False
'
'OptionSmall
'
Me.OptionSmall.Anchor = System.Windows.Forms.AnchorStyles.Bottom
Me.OptionSmall.Font = New System.Drawing.Font("Arial", 9.0!)

Me.OptionSmall.Location = New System.Drawing.Point(404, 379)
Me.OptionSmall.Name = "OptionSmall"
Me.OptionSmall.Size = New System.Drawing.Size(176, 22)
Me.OptionSmall.TabIndex = 5
Me.OptionSmall.TabStop = True
Me.OptionSmall.Text = "&Small Icons"
Me.ToolTip1.SetToolTip(Me.OptionSmall, "Use small toolbar icons")
Me.OptionSmall.UseVisualStyleBackColor = False
'
'cmdMoveDown
'
Me.cmdMoveDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
Me.cmdMoveDown.Font = New System.Drawing.Font("Arial", 9.0!)

Me.cmdMoveDown.Location = New System.Drawing.Point(643, 175)
Me.cmdMoveDown.Name = "cmdMoveDown"
Me.cmdMoveDown.Size = New System.Drawing.Size(98, 32)
Me.cmdMoveDown.TabIndex = 9
Me.cmdMoveDown.Text = "Move &Down"
Me.ToolTip1.SetToolTip(Me.cmdMoveDown, "Move the selected button down")
Me.cmdMoveDown.UseVisualStyleBackColor = False
'
'cmdMoveUp
'
Me.cmdMoveUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
Me.cmdMoveUp.Font = New System.Drawing.Font("Arial", 9.0!)

Me.cmdMoveUp.Location = New System.Drawing.Point(643, 125)
Me.cmdMoveUp.Name = "cmdMoveUp"
Me.cmdMoveUp.Size = New System.Drawing.Size(98, 32)
Me.cmdMoveUp.TabIndex = 8
Me.cmdMoveUp.Text = "Move &Up"
Me.ToolTip1.SetToolTip(Me.cmdMoveUp, "Move the selected button up")
Me.cmdMoveUp.UseVisualStyleBackColor = False
'
'cmdOK
'
Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!)

Me.cmdOK.Location = New System.Drawing.Point(648, 270)
Me.cmdOK.Name = "cmdOK"
Me.cmdOK.Size = New System.Drawing.Size(91, 31)
Me.cmdOK.TabIndex = 10
Me.cmdOK.Text = "&OK"
Me.ToolTip1.SetToolTip(Me.cmdOK, "Save toolbar settings and return")
Me.cmdOK.UseVisualStyleBackColor = False
'
'chkToolbarText
'
Me.chkToolbarText.Anchor = System.Windows.Forms.AnchorStyles.Bottom
Me.chkToolbarText.AutoSize = True
Me.chkToolbarText.Location = New System.Drawing.Point(76, 385)
Me.chkToolbarText.Name = "chkToolbarText"
Me.chkToolbarText.Size = New System.Drawing.Size(149, 21)
Me.chkToolbarText.TabIndex = 4
Me.chkToolbarText.Text = "Show Toolbar Text"
Me.ToolTip1.SetToolTip(Me.chkToolbarText, "Display the tool button names under the icons")
Me.chkToolbarText.UseVisualStyleBackColor = True
'
'List2
'
Me.List2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
Me.List2.BackColor = System.Drawing.SystemColors.Window
Me.List2.Font = New System.Drawing.Font("Arial", 9.0!)
Me.List2.ItemHeight = 17
Me.List2.Location = New System.Drawing.Point(385, 46)
Me.List2.Name = "List2"
Me.List2.Size = New System.Drawing.Size(216, 293)
Me.List2.TabIndex = 3
'
'List1
'
Me.List1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
Me.List1.BackColor = System.Drawing.SystemColors.Window
Me.List1.Font = New System.Drawing.Font("Arial", 9.0!)
Me.List1.ItemHeight = 17
Me.List1.Location = New System.Drawing.Point(30, 46)
Me.List1.Name = "List1"
Me.List1.Size = New System.Drawing.Size(216, 293)
Me.List1.TabIndex = 0
'
'cmdRemove
'
Me.cmdRemove.Font = New System.Drawing.Font("Arial", 9.0!)

Me.cmdRemove.Location = New System.Drawing.Point(270, 175)
Me.cmdRemove.Name = "cmdRemove"
Me.cmdRemove.Size = New System.Drawing.Size(96, 31)
Me.cmdRemove.TabIndex = 2
Me.cmdRemove.Text = "<= &Remove"
Me.cmdRemove.UseVisualStyleBackColor = False
'
'cmdAdd
'
Me.cmdAdd.Font = New System.Drawing.Font("Arial", 9.0!)

Me.cmdAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
Me.cmdAdd.Location = New System.Drawing.Point(270, 125)
Me.cmdAdd.Name = "cmdAdd"
Me.cmdAdd.Size = New System.Drawing.Size(96, 31)
Me.cmdAdd.TabIndex = 1
Me.cmdAdd.Text = "&Add  =>"
Me.cmdAdd.UseVisualStyleBackColor = False
'
'cmdCancel
'
Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)

Me.cmdCancel.Location = New System.Drawing.Point(648, 320)
Me.cmdCancel.Name = "cmdCancel"
Me.cmdCancel.Size = New System.Drawing.Size(91, 31)
Me.cmdCancel.TabIndex = 11
Me.cmdCancel.Text = "Cancel"
Me.cmdCancel.UseVisualStyleBackColor = False
'
'Label2
'
Me.Label2.AutoSize = True
Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!)

Me.Label2.Location = New System.Drawing.Point(385, 22)
Me.Label2.Name = "Label2"
Me.Label2.Size = New System.Drawing.Size(168, 17)
Me.Label2.TabIndex = 1
Me.Label2.Text = "Current &Toolbar Buttons:"
'
'Label1
'
Me.Label1.AutoSize = True
Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!)

Me.Label1.Location = New System.Drawing.Point(30, 22)
Me.Label1.Name = "Label1"
Me.Label1.Size = New System.Drawing.Size(176, 17)
Me.Label1.TabIndex = 0
Me.Label1.Text = "A&vailable Toolbar Buttons:"
'
'frmToolbar
'
Me.AcceptButton = Me.cmdOK
Me.BackColor = System.Drawing.SystemColors.Control
Me.CancelButton = Me.cmdCancel
Me.ClientSize = New System.Drawing.Size(766, 447)
Me.Controls.Add(Me.chkToolbarText)
Me.Controls.Add(Me.cmdReset)
Me.Controls.Add(Me.List2)
Me.Controls.Add(Me.List1)
Me.Controls.Add(Me.OptionLarge)
Me.Controls.Add(Me.OptionSmall)
Me.Controls.Add(Me.cmdMoveDown)
Me.Controls.Add(Me.cmdMoveUp)
Me.Controls.Add(Me.cmdRemove)
Me.Controls.Add(Me.cmdAdd)
Me.Controls.Add(Me.cmdOK)
Me.Controls.Add(Me.cmdCancel)
Me.Controls.Add(Me.cmdHelp)
Me.Controls.Add(Me.Label2)
Me.Controls.Add(Me.Label1)
Me.Cursor = System.Windows.Forms.Cursors.Default
Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
Me.Location = New System.Drawing.Point(3, 30)
Me.MaximizeBox = False
Me.MinimizeBox = False
Me.Name = "frmToolbar"
Me.ShowInTaskbar = False
Me.Text = "Customize Toolbar"
Me.ResumeLayout(False)
Me.PerformLayout()

End Sub
  Friend WithEvents chkToolbarText As System.Windows.Forms.CheckBox
#End Region
End Class