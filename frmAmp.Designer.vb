<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmAmp
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
  Public WithEvents cmdCancel As Button
  Public WithEvents cmdOK As Button
  Public WithEvents Label2 As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAmp))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.nmBlack = New System.Windows.Forms.NumericUpDown()
    Me.nmWhite = New System.Windows.Forms.NumericUpDown()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.cmdOK = New System.Windows.Forms.Button()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.aView = New PhotoMud.ctlViewCompare()
    CType(Me.nmBlack, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmWhite, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'cmdHelp
    '
    Me.cmdHelp.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(453, 517)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 39)
    Me.cmdHelp.TabIndex = 3
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'nmBlack
    '
    Me.nmBlack.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.nmBlack.Location = New System.Drawing.Point(280, 534)
    Me.nmBlack.Name = "nmBlack"
    Me.nmBlack.Size = New System.Drawing.Size(58, 25)
    Me.nmBlack.TabIndex = 1
    Me.ToolTip1.SetToolTip(Me.nmBlack, "The amplification is the number of \n times the photo should be added to itself t" & _
        "o increase the brightness")
    Me.nmBlack.Value = New Decimal(New Integer() {2, 0, 0, 0})
    '
    'nmWhite
    '
    Me.nmWhite.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.nmWhite.Location = New System.Drawing.Point(280, 503)
    Me.nmWhite.Name = "nmWhite"
    Me.nmWhite.Size = New System.Drawing.Size(58, 25)
    Me.nmWhite.TabIndex = 8
    Me.ToolTip1.SetToolTip(Me.nmWhite, "The amplification is the number of \n times the photo should be added to itself t" & _
        "o increase the brightness")
    Me.nmWhite.Value = New Decimal(New Integer() {10, 0, 0, 0})
    '
    'cmdCancel
    '
    Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdCancel.Location = New System.Drawing.Point(532, 521)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(91, 31)
    Me.cmdCancel.TabIndex = 4
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'cmdOK
    '
    Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdOK.Location = New System.Drawing.Point(660, 521)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(91, 31)
    Me.cmdOK.TabIndex = 5
    Me.cmdOK.Text = "&OK"
    Me.cmdOK.UseVisualStyleBackColor = False
    '
    'Label2
    '
    Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label2.Location = New System.Drawing.Point(150, 536)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(117, 17)
    Me.Label2.TabIndex = 0
    Me.Label2.Text = "&Black Threshold:"
    '
    'Label1
    '
    Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label1.Location = New System.Drawing.Point(152, 507)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(114, 17)
    Me.Label1.TabIndex = 7
    Me.Label1.Text = "&White threshold:"
    '
    'aView
    '
    Me.aView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.aView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.aView.Location = New System.Drawing.Point(4, 4)
    Me.aView.Name = "aView"
    Me.aView.NextButtons = False
    Me.aView.pCenter = New System.Drawing.Point(0, 0)
    Me.aView.SingleView = False
    Me.aView.Size = New System.Drawing.Size(788, 478)
    Me.aView.TabIndex = 9
    Me.aView.zoomFactor = 0.0R
    '
    'frmAmp
    '
    Me.AcceptButton = Me.cmdOK
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(795, 579)
    Me.Controls.Add(Me.aView)
    Me.Controls.Add(Me.nmWhite)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.nmBlack)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.Label2)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 22)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmAmp"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Light Amplifier"
    CType(Me.nmBlack, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmWhite, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

End Sub
 Public WithEvents cmdHelp As System.Windows.Forms.Button
 Friend WithEvents nmBlack As System.Windows.Forms.NumericUpDown
 Private WithEvents ToolTip1 As System.Windows.Forms.ToolTip
 Friend WithEvents nmWhite As System.Windows.Forms.NumericUpDown
 Public WithEvents Label1 As System.Windows.Forms.Label
 Friend WithEvents aView As PhotoMud.ctlViewCompare
#End Region
End Class