<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmContrastStretch
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmContrastStretch))
    Me.nmWhite = New System.Windows.Forms.NumericUpDown()
    Me.nmBlack = New System.Windows.Forms.NumericUpDown()
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.trkWhite = New System.Windows.Forms.TrackBar()
    Me.trkBlack = New System.Windows.Forms.TrackBar()
    Me.cmdOK = New System.Windows.Forms.Button()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.lbBlack = New System.Windows.Forms.Label()
    Me.lbWhite = New System.Windows.Forms.Label()
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.aView = New PhotoMud.ctlViewCompare()
    CType(Me.nmWhite, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmBlack, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkWhite, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkBlack, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'nmWhite
    '
    Me.nmWhite.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.nmWhite.DecimalPlaces = 2
    Me.nmWhite.Increment = New Decimal(New Integer() {5, 0, 0, 131072})
    Me.nmWhite.Location = New System.Drawing.Point(561, 519)
    Me.nmWhite.Name = "nmWhite"
    Me.nmWhite.Size = New System.Drawing.Size(69, 25)
    Me.nmWhite.TabIndex = 43
    Me.nmWhite.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'nmBlack
    '
    Me.nmBlack.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.nmBlack.DecimalPlaces = 2
    Me.nmBlack.Location = New System.Drawing.Point(561, 451)
    Me.nmBlack.Name = "nmBlack"
    Me.nmBlack.Size = New System.Drawing.Size(69, 25)
    Me.nmBlack.TabIndex = 40
    Me.nmBlack.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'cmdHelp
    '
    Me.cmdHelp.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(814, 421)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 39)
    Me.cmdHelp.TabIndex = 44
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'trkWhite
    '
    Me.trkWhite.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.trkWhite.AutoSize = False
    Me.trkWhite.Location = New System.Drawing.Point(363, 523)
    Me.trkWhite.Maximum = 100
    Me.trkWhite.Name = "trkWhite"
    Me.trkWhite.Size = New System.Drawing.Size(195, 22)
    Me.trkWhite.TabIndex = 42
    Me.trkWhite.TickFrequency = 5
    Me.trkWhite.TickStyle = System.Windows.Forms.TickStyle.None
    Me.trkWhite.Value = 95
    '
    'trkBlack
    '
    Me.trkBlack.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.trkBlack.AutoSize = False
    Me.trkBlack.Location = New System.Drawing.Point(363, 454)
    Me.trkBlack.Maximum = 100
    Me.trkBlack.Minimum = -100
    Me.trkBlack.Name = "trkBlack"
    Me.trkBlack.Size = New System.Drawing.Size(195, 22)
    Me.trkBlack.TabIndex = 39
    Me.trkBlack.TickFrequency = 100
    '
    'cmdOK
    '
    Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdOK.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdOK.Location = New System.Drawing.Point(792, 481)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(91, 31)
    Me.cmdOK.TabIndex = 45
    Me.cmdOK.Text = "&OK"
    Me.cmdOK.UseVisualStyleBackColor = False
    '
    'cmdCancel
    '
    Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdCancel.Location = New System.Drawing.Point(792, 531)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(91, 31)
    Me.cmdCancel.TabIndex = 46
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'lbBlack
    '
    Me.lbBlack.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.lbBlack.AutoSize = True
    Me.lbBlack.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbBlack.Location = New System.Drawing.Point(360, 432)
    Me.lbBlack.Name = "lbBlack"
    Me.lbBlack.Size = New System.Drawing.Size(99, 17)
    Me.lbBlack.TabIndex = 38
    Me.lbBlack.Text = "&Black Change"
    Me.lbBlack.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lbWhite
    '
    Me.lbWhite.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.lbWhite.AutoSize = True
    Me.lbWhite.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbWhite.Location = New System.Drawing.Point(359, 498)
    Me.lbWhite.Name = "lbWhite"
    Me.lbWhite.Size = New System.Drawing.Size(101, 17)
    Me.lbWhite.TabIndex = 41
    Me.lbWhite.Text = "&White Change"
    Me.lbWhite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'aView
    '
    Me.aView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.aView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.aView.Location = New System.Drawing.Point(3, 5)
    Me.aView.Name = "aView"
    Me.aView.NextButtons = False
    Me.aView.pCenter = New System.Drawing.Point(0, 0)
    Me.aView.SingleView = False
    Me.aView.Size = New System.Drawing.Size(962, 412)
    Me.aView.TabIndex = 48
    Me.aView.zoomFactor = 0.0R
    '
    'frmContrastStretch
    '
    Me.AcceptButton = Me.cmdOK
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(968, 599)
    Me.Controls.Add(Me.aView)
    Me.Controls.Add(Me.nmWhite)
    Me.Controls.Add(Me.nmBlack)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.trkWhite)
    Me.Controls.Add(Me.trkBlack)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.lbBlack)
    Me.Controls.Add(Me.lbWhite)
    Me.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 30)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmContrastStretch"
    Me.ShowInTaskbar = False
    Me.Text = "Contrast Stretch"
    CType(Me.nmWhite, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmBlack, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkWhite, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkBlack, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents aView As PhotoMud.ctlViewCompare
  Friend WithEvents nmWhite As System.Windows.Forms.NumericUpDown
  Friend WithEvents nmBlack As System.Windows.Forms.NumericUpDown
  Public WithEvents cmdHelp As System.Windows.Forms.Button
  Friend WithEvents trkWhite As System.Windows.Forms.TrackBar
  Friend WithEvents trkBlack As System.Windows.Forms.TrackBar
  Public WithEvents cmdOK As System.Windows.Forms.Button
  Public WithEvents cmdCancel As System.Windows.Forms.Button
  Public WithEvents lbBlack As System.Windows.Forms.Label
  Public WithEvents lbWhite As System.Windows.Forms.Label
  Public WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
