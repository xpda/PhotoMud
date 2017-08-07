<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmHalftone
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
  Public WithEvents Combo1 As ComboBox
  Public WithEvents cmdOK As Button
  Public WithEvents cmdCancel As Button
  Public WithEvents Label2 As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHalftone))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.Combo1 = New System.Windows.Forms.ComboBox()
    Me.cmdOK = New System.Windows.Forms.Button()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.aview = New PhotoMud.ctlViewCompare()
    Me.SuspendLayout()
    '
    'cmdHelp
    '
    Me.cmdHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(763, 483)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 39)
    Me.cmdHelp.TabIndex = 7
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'Combo1
    '
    Me.Combo1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Combo1.BackColor = System.Drawing.SystemColors.Window
    Me.Combo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.Combo1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Combo1.Location = New System.Drawing.Point(45, 437)
    Me.Combo1.Name = "Combo1"
    Me.Combo1.Size = New System.Drawing.Size(344, 25)
    Me.Combo1.TabIndex = 0
    Me.ToolTip1.SetToolTip(Me.Combo1, "Halftone pattern type")
    '
    'cmdOK
    '
    Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdOK.Location = New System.Drawing.Point(833, 441)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(91, 31)
    Me.cmdOK.TabIndex = 8
    Me.cmdOK.Text = "&OK"
    Me.cmdOK.UseVisualStyleBackColor = False
    '
    'cmdCancel
    '
    Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdCancel.Location = New System.Drawing.Point(833, 491)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(91, 31)
    Me.cmdCancel.TabIndex = 9
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'Label2
    '
    Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label2.Location = New System.Drawing.Point(42, 417)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(91, 16)
    Me.Label2.TabIndex = 0
    Me.Label2.Text = "&Method"
    '
    'aview
    '
    Me.aview.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.aview.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.aview.Location = New System.Drawing.Point(3, 3)
    Me.aview.Name = "aview"
    Me.aview.NextButtons = False
    Me.aview.pCenter = New System.Drawing.Point(0, 0)
    Me.aview.SingleView = False
    Me.aview.Size = New System.Drawing.Size(964, 406)
    Me.aview.TabIndex = 44
    Me.aview.zoomFactor = 0.0R
    '
    'frmHalftone
    '
    Me.AcceptButton = Me.cmdOK
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(970, 550)
    Me.Controls.Add(Me.aview)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.Combo1)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.Label2)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 30)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmHalftone"
    Me.ShowInTaskbar = False
    Me.Text = "Convert to Halftone"
    Me.ResumeLayout(False)

End Sub
 Public WithEvents cmdHelp As System.Windows.Forms.Button
 Friend WithEvents aview As PhotoMud.ctlViewCompare
#End Region
End Class