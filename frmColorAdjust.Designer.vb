<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmColorAdjust
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmColorAdjust))
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.cmdOK = New System.Windows.Forms.Button()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.nmMidRed = New System.Windows.Forms.NumericUpDown()
    Me.trkMidRed = New System.Windows.Forms.TrackBar()
    Me.nmMidGreen = New System.Windows.Forms.NumericUpDown()
    Me.trkMidGreen = New System.Windows.Forms.TrackBar()
    Me.nmMidBlue = New System.Windows.Forms.NumericUpDown()
    Me.trkMidBlue = New System.Windows.Forms.TrackBar()
    Me.GroupBox2 = New System.Windows.Forms.GroupBox()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.cmdReset = New System.Windows.Forms.Button()
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.chkPreserveIntensity = New System.Windows.Forms.CheckBox()
    Me.aView = New PhotoMud.ctlViewCompare()
    Me.pviewHisto = New PhotoMud.pViewer()
    CType(Me.nmMidRed, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkMidRed, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmMidGreen, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkMidGreen, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmMidBlue, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkMidBlue, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBox2.SuspendLayout()
    CType(Me.pviewHisto, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'cmdHelp
    '
    Me.cmdHelp.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(518, 618)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 41)
    Me.cmdHelp.TabIndex = 18
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'cmdOK
    '
    Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdOK.Location = New System.Drawing.Point(713, 624)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(91, 35)
    Me.cmdOK.TabIndex = 20
    Me.cmdOK.Text = "&OK"
    Me.ToolTip1.SetToolTip(Me.cmdOK, "Save changes and return")
    Me.cmdOK.UseVisualStyleBackColor = False
    '
    'cmdCancel
    '
    Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdCancel.Location = New System.Drawing.Point(835, 624)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(91, 35)
    Me.cmdCancel.TabIndex = 21
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'nmMidRed
    '
    Me.nmMidRed.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.nmMidRed.Location = New System.Drawing.Point(269, 40)
    Me.nmMidRed.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
    Me.nmMidRed.Minimum = New Decimal(New Integer() {99, 0, 0, -2147483648})
    Me.nmMidRed.Name = "nmMidRed"
    Me.nmMidRed.Size = New System.Drawing.Size(55, 25)
    Me.nmMidRed.TabIndex = 7
    Me.ToolTip1.SetToolTip(Me.nmMidRed, "Adjust red in midtone areas")
    '
    'trkMidRed
    '
    Me.trkMidRed.AutoSize = False
    Me.trkMidRed.Location = New System.Drawing.Point(5, 40)
    Me.trkMidRed.Maximum = 99
    Me.trkMidRed.Minimum = -99
    Me.trkMidRed.Name = "trkMidRed"
    Me.trkMidRed.Size = New System.Drawing.Size(256, 25)
    Me.trkMidRed.TabIndex = 6
    Me.trkMidRed.TickFrequency = 5
    Me.trkMidRed.TickStyle = System.Windows.Forms.TickStyle.None
    Me.ToolTip1.SetToolTip(Me.trkMidRed, "Adjust red in midtone areas")
    '
    'nmMidGreen
    '
    Me.nmMidGreen.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.nmMidGreen.Location = New System.Drawing.Point(269, 86)
    Me.nmMidGreen.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
    Me.nmMidGreen.Minimum = New Decimal(New Integer() {99, 0, 0, -2147483648})
    Me.nmMidGreen.Name = "nmMidGreen"
    Me.nmMidGreen.Size = New System.Drawing.Size(55, 25)
    Me.nmMidGreen.TabIndex = 9
    Me.ToolTip1.SetToolTip(Me.nmMidGreen, "Adjust green in midtone areas")
    '
    'trkMidGreen
    '
    Me.trkMidGreen.AutoSize = False
    Me.trkMidGreen.Location = New System.Drawing.Point(5, 86)
    Me.trkMidGreen.Maximum = 99
    Me.trkMidGreen.Minimum = -99
    Me.trkMidGreen.Name = "trkMidGreen"
    Me.trkMidGreen.Size = New System.Drawing.Size(256, 25)
    Me.trkMidGreen.TabIndex = 8
    Me.trkMidGreen.TickFrequency = 5
    Me.trkMidGreen.TickStyle = System.Windows.Forms.TickStyle.None
    Me.ToolTip1.SetToolTip(Me.trkMidGreen, "Adjust green in midtone areas")
    '
    'nmMidBlue
    '
    Me.nmMidBlue.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.nmMidBlue.Location = New System.Drawing.Point(269, 127)
    Me.nmMidBlue.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
    Me.nmMidBlue.Minimum = New Decimal(New Integer() {99, 0, 0, -2147483648})
    Me.nmMidBlue.Name = "nmMidBlue"
    Me.nmMidBlue.Size = New System.Drawing.Size(55, 25)
    Me.nmMidBlue.TabIndex = 11
    Me.ToolTip1.SetToolTip(Me.nmMidBlue, "Adjust blue in midtone areas")
    '
    'trkMidBlue
    '
    Me.trkMidBlue.AutoSize = False
    Me.trkMidBlue.Location = New System.Drawing.Point(5, 127)
    Me.trkMidBlue.Maximum = 99
    Me.trkMidBlue.Minimum = -99
    Me.trkMidBlue.Name = "trkMidBlue"
    Me.trkMidBlue.Size = New System.Drawing.Size(256, 25)
    Me.trkMidBlue.TabIndex = 10
    Me.trkMidBlue.TickFrequency = 5
    Me.trkMidBlue.TickStyle = System.Windows.Forms.TickStyle.None
    Me.ToolTip1.SetToolTip(Me.trkMidBlue, "Adjust blue in midtone areas")
    '
    'GroupBox2
    '
    Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.GroupBox2.Controls.Add(Me.Label6)
    Me.GroupBox2.Controls.Add(Me.nmMidRed)
    Me.GroupBox2.Controls.Add(Me.Label7)
    Me.GroupBox2.Controls.Add(Me.trkMidRed)
    Me.GroupBox2.Controls.Add(Me.Label8)
    Me.GroupBox2.Controls.Add(Me.trkMidGreen)
    Me.GroupBox2.Controls.Add(Me.nmMidGreen)
    Me.GroupBox2.Controls.Add(Me.trkMidBlue)
    Me.GroupBox2.Controls.Add(Me.nmMidBlue)
    Me.GroupBox2.Location = New System.Drawing.Point(95, 412)
    Me.GroupBox2.Name = "GroupBox2"
    Me.GroupBox2.Size = New System.Drawing.Size(398, 170)
    Me.GroupBox2.TabIndex = 71
    Me.GroupBox2.TabStop = False
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Location = New System.Drawing.Point(330, 129)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(37, 17)
    Me.Label6.TabIndex = 11
    Me.Label6.Text = "&Blue"
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Location = New System.Drawing.Point(330, 86)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(48, 17)
    Me.Label7.TabIndex = 9
    Me.Label7.Text = "&Green"
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(330, 42)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(35, 17)
    Me.Label8.TabIndex = 7
    Me.Label8.Text = "&Red"
    '
    'cmdReset
    '
    Me.cmdReset.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.cmdReset.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdReset.Location = New System.Drawing.Point(589, 624)
    Me.cmdReset.Name = "cmdReset"
    Me.cmdReset.Size = New System.Drawing.Size(91, 35)
    Me.cmdReset.TabIndex = 19
    Me.cmdReset.Text = "Rese&t"
    Me.ToolTip1.SetToolTip(Me.cmdReset, "Reset to original colors")
    Me.cmdReset.UseVisualStyleBackColor = False
    '
    'chkPreserveIntensity
    '
    Me.chkPreserveIntensity.AutoSize = True
    Me.chkPreserveIntensity.Enabled = False
    Me.chkPreserveIntensity.Location = New System.Drawing.Point(100, 632)
    Me.chkPreserveIntensity.Name = "chkPreserveIntensity"
    Me.chkPreserveIntensity.Size = New System.Drawing.Size(196, 21)
    Me.chkPreserveIntensity.TabIndex = 78
    Me.chkPreserveIntensity.Text = "Preserve Overall Intensity"
    Me.chkPreserveIntensity.UseVisualStyleBackColor = True
    Me.chkPreserveIntensity.Visible = False
    '
    'aView
    '
    Me.aView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.aView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.aView.Location = New System.Drawing.Point(6, 7)
    Me.aView.Name = "aView"
    Me.aView.NextButtons = False
    Me.aView.pCenter = New System.Drawing.Point(0, 0)
    Me.aView.SingleView = False
    Me.aView.Size = New System.Drawing.Size(965, 396)
    Me.aView.TabIndex = 79
    Me.aView.zoomFactor = 0.0R
    '
    'pviewHisto
    '
    Me.pviewHisto.BackColor = System.Drawing.SystemColors.ActiveCaptionText
    Me.pviewHisto.BitmapPath = Nothing
    Me.pviewHisto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.pviewHisto.CurrentPage = 0
    Me.pviewHisto.DrawAngle = 0.0R
    Me.pviewHisto.DrawBackColor = System.Drawing.Color.White
    Me.pviewHisto.DrawBox = New System.Drawing.Rectangle(0, 0, 0, 0)
    Me.pviewHisto.DrawDashed = False
    Me.pviewHisto.DrawFilled = False
    Me.pviewHisto.DrawFont = Nothing
    Me.pviewHisto.DrawForeColor = System.Drawing.Color.Navy
    Me.pviewHisto.DrawLineWidth = 1.0!
    Me.pviewHisto.DrawPath = Nothing
    Me.pviewHisto.DrawPoints = CType(resources.GetObject("pviewHisto.DrawPoints"), System.Collections.Generic.List(Of System.Drawing.Point))
    Me.pviewHisto.DrawShape = PhotoMud.shape.Line
    Me.pviewHisto.DrawString = ""
    Me.pviewHisto.DrawTextFmt = Nothing
    Me.pviewHisto.FloaterOutline = False
    Me.pviewHisto.FloaterPath = Nothing
    Me.pviewHisto.FloaterPosition = New System.Drawing.Point(0, 0)
    Me.pviewHisto.FloaterVisible = True
    Me.pviewHisto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High
    Me.pviewHisto.Location = New System.Drawing.Point(569, 416)
    Me.pviewHisto.Name = "pviewHisto"
    Me.pviewHisto.pageBmp = CType(resources.GetObject("pviewHisto.pageBmp"), System.Collections.Generic.List(Of System.Drawing.Bitmap))
    Me.pviewHisto.PageCount = 0
    Me.pviewHisto.RubberAngle = 0.0R
    Me.pviewHisto.rubberBackColor = System.Drawing.Color.White
    Me.pviewHisto.RubberBox = New System.Drawing.Rectangle(0, 0, 0, 0)
    Me.pviewHisto.RubberBoxCrop = False
    Me.pviewHisto.RubberColor = System.Drawing.Color.Navy
    Me.pviewHisto.RubberDashed = False
    Me.pviewHisto.RubberEnabled = False
    Me.pviewHisto.RubberFilled = False
    Me.pviewHisto.RubberFont = Nothing
    Me.pviewHisto.RubberLineWidth = 1.0R
    Me.pviewHisto.RubberPath = Nothing
    Me.pviewHisto.RubberPoints = CType(resources.GetObject("pviewHisto.RubberPoints"), System.Collections.Generic.List(Of System.Drawing.Point))
    Me.pviewHisto.RubberShape = PhotoMud.shape.Curve
    Me.pviewHisto.RubberString = ""
    Me.pviewHisto.RubberTextFmt = Nothing
    Me.pviewHisto.SelectionVisible = True
    Me.pviewHisto.Size = New System.Drawing.Size(322, 166)
    Me.pviewHisto.TabIndex = 80
    Me.pviewHisto.TabStop = False
    Me.pviewHisto.ZoomFactor = 1.0R
    '
    'frmColorAdjust
    '
    Me.AcceptButton = Me.cmdOK
    Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(976, 685)
    Me.Controls.Add(Me.pviewHisto)
    Me.Controls.Add(Me.aView)
    Me.Controls.Add(Me.chkPreserveIntensity)
    Me.Controls.Add(Me.cmdReset)
    Me.Controls.Add(Me.GroupBox2)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.cmdCancel)
    Me.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "frmColorAdjust"
    Me.ShowInTaskbar = False
    Me.Text = "Color Adjustment"
    CType(Me.nmMidRed, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkMidRed, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmMidGreen, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkMidGreen, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmMidBlue, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkMidBlue, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBox2.ResumeLayout(False)
    Me.GroupBox2.PerformLayout()
    CType(Me.pviewHisto, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Public WithEvents cmdHelp As System.Windows.Forms.Button
  Public WithEvents cmdOK As System.Windows.Forms.Button
  Public WithEvents cmdCancel As System.Windows.Forms.Button
  Friend WithEvents nmMidRed As System.Windows.Forms.NumericUpDown
  Friend WithEvents trkMidRed As System.Windows.Forms.TrackBar
  Friend WithEvents nmMidGreen As System.Windows.Forms.NumericUpDown
  Friend WithEvents trkMidGreen As System.Windows.Forms.TrackBar
  Friend WithEvents nmMidBlue As System.Windows.Forms.NumericUpDown
  Friend WithEvents trkMidBlue As System.Windows.Forms.TrackBar
  Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
  Friend WithEvents Label6 As System.Windows.Forms.Label
  Friend WithEvents Label7 As System.Windows.Forms.Label
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Public WithEvents cmdReset As System.Windows.Forms.Button
  Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
  Friend WithEvents chkPreserveIntensity As System.Windows.Forms.CheckBox
  Friend WithEvents aView As PhotoMud.ctlViewCompare
  Friend WithEvents pviewHisto As PhotoMud.pViewer
End Class
