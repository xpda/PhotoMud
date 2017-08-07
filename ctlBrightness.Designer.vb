<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlBrightness
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
    Me.nmSaturation = New System.Windows.Forms.NumericUpDown()
    Me.nmContrast = New System.Windows.Forms.NumericUpDown()
    Me.nmBrightness = New System.Windows.Forms.NumericUpDown()
    Me.cmdResetBright = New System.Windows.Forms.Button()
    Me.lbSaturation = New System.Windows.Forms.Label()
    Me.trkSaturation = New System.Windows.Forms.TrackBar()
    Me.lbContrast = New System.Windows.Forms.Label()
    Me.trkContrast = New System.Windows.Forms.TrackBar()
    Me.lbBrightness = New System.Windows.Forms.Label()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.cmdOK = New System.Windows.Forms.Button()
    Me.trkBrightness = New System.Windows.Forms.TrackBar()
    CType(Me.nmSaturation, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmContrast, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmBrightness, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkSaturation, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkContrast, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkBrightness, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'nmSaturation
    '
    Me.nmSaturation.Location = New System.Drawing.Point(367, 80)
    Me.nmSaturation.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
    Me.nmSaturation.Minimum = New Decimal(New Integer() {50, 0, 0, -2147483648})
    Me.nmSaturation.Name = "nmSaturation"
    Me.nmSaturation.Size = New System.Drawing.Size(58, 25)
    Me.nmSaturation.TabIndex = 21
    '
    'nmContrast
    '
    Me.nmContrast.Location = New System.Drawing.Point(367, 49)
    Me.nmContrast.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
    Me.nmContrast.Minimum = New Decimal(New Integer() {50, 0, 0, -2147483648})
    Me.nmContrast.Name = "nmContrast"
    Me.nmContrast.Size = New System.Drawing.Size(58, 25)
    Me.nmContrast.TabIndex = 18
    '
    'nmBrightness
    '
    Me.nmBrightness.Location = New System.Drawing.Point(367, 18)
    Me.nmBrightness.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
    Me.nmBrightness.Minimum = New Decimal(New Integer() {50, 0, 0, -2147483648})
    Me.nmBrightness.Name = "nmBrightness"
    Me.nmBrightness.Size = New System.Drawing.Size(58, 25)
    Me.nmBrightness.TabIndex = 15
    '
    'cmdResetBright
    '
    Me.cmdResetBright.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdResetBright.Location = New System.Drawing.Point(82, 116)
    Me.cmdResetBright.Name = "cmdResetBright"
    Me.cmdResetBright.Size = New System.Drawing.Size(77, 30)
    Me.cmdResetBright.TabIndex = 22
    Me.cmdResetBright.Text = "&Reset"
    Me.cmdResetBright.UseVisualStyleBackColor = False
    '
    'lbSaturation
    '
    Me.lbSaturation.AutoSize = True
    Me.lbSaturation.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbSaturation.Location = New System.Drawing.Point(22, 87)
    Me.lbSaturation.Name = "lbSaturation"
    Me.lbSaturation.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.lbSaturation.Size = New System.Drawing.Size(78, 17)
    Me.lbSaturation.TabIndex = 19
    Me.lbSaturation.Text = "&Saturation:"
    Me.lbSaturation.TextAlign = System.Drawing.ContentAlignment.TopRight
    '
    'trkSaturation
    '
    Me.trkSaturation.AutoSize = False
    Me.trkSaturation.Location = New System.Drawing.Point(110, 80)
    Me.trkSaturation.Maximum = 100
    Me.trkSaturation.Minimum = -100
    Me.trkSaturation.Name = "trkSaturation"
    Me.trkSaturation.Size = New System.Drawing.Size(251, 26)
    Me.trkSaturation.TabIndex = 20
    Me.trkSaturation.TickStyle = System.Windows.Forms.TickStyle.None
    '
    'lbContrast
    '
    Me.lbContrast.AutoSize = True
    Me.lbContrast.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbContrast.Location = New System.Drawing.Point(22, 54)
    Me.lbContrast.Name = "lbContrast"
    Me.lbContrast.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.lbContrast.Size = New System.Drawing.Size(68, 17)
    Me.lbContrast.TabIndex = 16
    Me.lbContrast.Text = "Co&ntrast:"
    Me.lbContrast.TextAlign = System.Drawing.ContentAlignment.TopRight
    '
    'trkContrast
    '
    Me.trkContrast.AutoSize = False
    Me.trkContrast.Location = New System.Drawing.Point(110, 49)
    Me.trkContrast.Maximum = 100
    Me.trkContrast.Minimum = -100
    Me.trkContrast.Name = "trkContrast"
    Me.trkContrast.Size = New System.Drawing.Size(251, 26)
    Me.trkContrast.TabIndex = 17
    Me.trkContrast.TickStyle = System.Windows.Forms.TickStyle.None
    '
    'lbBrightness
    '
    Me.lbBrightness.AutoSize = True
    Me.lbBrightness.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbBrightness.Location = New System.Drawing.Point(22, 21)
    Me.lbBrightness.Name = "lbBrightness"
    Me.lbBrightness.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.lbBrightness.Size = New System.Drawing.Size(82, 17)
    Me.lbBrightness.TabIndex = 13
    Me.lbBrightness.Text = "&Brightness:"
    Me.lbBrightness.TextAlign = System.Drawing.ContentAlignment.TopRight
    '
    'cmdCancel
    '
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdCancel.Location = New System.Drawing.Point(278, 116)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(77, 30)
    Me.cmdCancel.TabIndex = 24
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'cmdOK
    '
    Me.cmdOK.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdOK.Location = New System.Drawing.Point(180, 116)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(77, 30)
    Me.cmdOK.TabIndex = 23
    Me.cmdOK.Text = "&OK"
    Me.cmdOK.UseVisualStyleBackColor = False
    '
    'trkBrightness
    '
    Me.trkBrightness.AutoSize = False
    Me.trkBrightness.Location = New System.Drawing.Point(110, 21)
    Me.trkBrightness.Maximum = 100
    Me.trkBrightness.Minimum = -100
    Me.trkBrightness.Name = "trkBrightness"
    Me.trkBrightness.Size = New System.Drawing.Size(251, 26)
    Me.trkBrightness.TabIndex = 14
    Me.trkBrightness.TickStyle = System.Windows.Forms.TickStyle.None
    '
    'ctlBrightness
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Controls.Add(Me.nmSaturation)
    Me.Controls.Add(Me.nmContrast)
    Me.Controls.Add(Me.nmBrightness)
    Me.Controls.Add(Me.cmdResetBright)
    Me.Controls.Add(Me.lbSaturation)
    Me.Controls.Add(Me.trkSaturation)
    Me.Controls.Add(Me.lbContrast)
    Me.Controls.Add(Me.trkContrast)
    Me.Controls.Add(Me.lbBrightness)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.trkBrightness)
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Name = "ctlBrightness"
    Me.Size = New System.Drawing.Size(444, 162)
    CType(Me.nmSaturation, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmContrast, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmBrightness, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkSaturation, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkContrast, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkBrightness, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

End Sub
    Friend WithEvents nmSaturation As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmContrast As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmBrightness As System.Windows.Forms.NumericUpDown
    Public WithEvents cmdResetBright As System.Windows.Forms.Button
    Friend WithEvents lbSaturation As System.Windows.Forms.Label
    Friend WithEvents trkSaturation As System.Windows.Forms.TrackBar
    Friend WithEvents lbContrast As System.Windows.Forms.Label
    Friend WithEvents trkContrast As System.Windows.Forms.TrackBar
    Friend WithEvents lbBrightness As System.Windows.Forms.Label
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents trkBrightness As System.Windows.Forms.TrackBar

End Class
