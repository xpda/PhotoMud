<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmCombine
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
  ' Public WithEvents lead2 As AxLEADArray
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCombine))
    Me.Frame2 = New System.Windows.Forms.GroupBox()
    Me.nmIntensity2 = New System.Windows.Forms.NumericUpDown()
    Me.trkIntensity2 = New System.Windows.Forms.TrackBar()
    Me.chkGray2 = New System.Windows.Forms.CheckBox()
    Me.Label2_0 = New System.Windows.Forms.Label()
    Me.Frame3 = New System.Windows.Forms.GroupBox()
    Me.nmIntensity3 = New System.Windows.Forms.NumericUpDown()
    Me.trkIntensity3 = New System.Windows.Forms.TrackBar()
    Me.chkGray3 = New System.Windows.Forms.CheckBox()
    Me.Label2_1 = New System.Windows.Forms.Label()
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.chkAspect = New System.Windows.Forms.CheckBox()
    Me.Combo1 = New System.Windows.Forms.ComboBox()
    Me.cmdOK = New System.Windows.Forms.Button()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.chkFit = New System.Windows.Forms.CheckBox()
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.pView1 = New PhotoMud.pViewer()
    Me.pView2 = New PhotoMud.pViewer()
    Me.pView3 = New PhotoMud.pViewer()
    Me.Frame2.SuspendLayout()
    CType(Me.nmIntensity2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkIntensity2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Frame3.SuspendLayout()
    CType(Me.nmIntensity3, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkIntensity3, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pView1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pView2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pView3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'Frame2
    '
    Me.Frame2.Controls.Add(Me.pView2)
    Me.Frame2.Controls.Add(Me.nmIntensity2)
    Me.Frame2.Controls.Add(Me.trkIntensity2)
    Me.Frame2.Controls.Add(Me.chkGray2)
    Me.Frame2.Controls.Add(Me.Label2_0)
    Me.Frame2.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Frame2.Location = New System.Drawing.Point(12, 236)
    Me.Frame2.Name = "Frame2"
    Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
    Me.Frame2.Size = New System.Drawing.Size(246, 233)
    Me.Frame2.TabIndex = 25
    Me.Frame2.TabStop = False
    '
    'nmIntensity2
    '
    Me.nmIntensity2.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.nmIntensity2.Location = New System.Drawing.Point(181, 155)
    Me.nmIntensity2.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
    Me.nmIntensity2.Name = "nmIntensity2"
    Me.nmIntensity2.Size = New System.Drawing.Size(53, 23)
    Me.nmIntensity2.TabIndex = 4
    Me.ToolTip1.SetToolTip(Me.nmIntensity2, "Adjust the intensity (brightness) before combining")
    Me.nmIntensity2.Value = New Decimal(New Integer() {100, 0, 0, -2147483648})
    '
    'trkIntensity2
    '
    Me.trkIntensity2.AutoSize = False
    Me.trkIntensity2.LargeChange = 100
    Me.trkIntensity2.Location = New System.Drawing.Point(11, 156)
    Me.trkIntensity2.Maximum = 1000
    Me.trkIntensity2.Minimum = -1000
    Me.trkIntensity2.Name = "trkIntensity2"
    Me.trkIntensity2.Size = New System.Drawing.Size(173, 21)
    Me.trkIntensity2.SmallChange = 10
    Me.trkIntensity2.TabIndex = 3
    Me.trkIntensity2.TickStyle = System.Windows.Forms.TickStyle.None
    Me.ToolTip1.SetToolTip(Me.trkIntensity2, "Adjust the intensity (brightness) before combining")
    '
    'chkGray2
    '
    Me.chkGray2.AutoSize = True
    Me.chkGray2.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkGray2.Location = New System.Drawing.Point(20, 200)
    Me.chkGray2.Name = "chkGray2"
    Me.chkGray2.Size = New System.Drawing.Size(167, 21)
    Me.chkGray2.TabIndex = 5
    Me.chkGray2.Text = "Convert to Gra&yscale"
    Me.ToolTip1.SetToolTip(Me.chkGray2, "Convert to grayscale before combining")
    Me.chkGray2.UseVisualStyleBackColor = False
    '
    'Label2_0
    '
    Me.Label2_0.AutoSize = True
    Me.Label2_0.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label2_0.Location = New System.Drawing.Point(70, 175)
    Me.Label2_0.Name = "Label2_0"
    Me.Label2_0.Size = New System.Drawing.Size(61, 17)
    Me.Label2_0.TabIndex = 11
    Me.Label2_0.Text = "Intensity"
    '
    'Frame3
    '
    Me.Frame3.Controls.Add(Me.pView3)
    Me.Frame3.Controls.Add(Me.nmIntensity3)
    Me.Frame3.Controls.Add(Me.trkIntensity3)
    Me.Frame3.Controls.Add(Me.chkGray3)
    Me.Frame3.Controls.Add(Me.Label2_1)
    Me.Frame3.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Frame3.Location = New System.Drawing.Point(12, 0)
    Me.Frame3.Name = "Frame3"
    Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
    Me.Frame3.Size = New System.Drawing.Size(246, 230)
    Me.Frame3.TabIndex = 26
    Me.Frame3.TabStop = False
    '
    'nmIntensity3
    '
    Me.nmIntensity3.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.nmIntensity3.Location = New System.Drawing.Point(183, 153)
    Me.nmIntensity3.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
    Me.nmIntensity3.Name = "nmIntensity3"
    Me.nmIntensity3.Size = New System.Drawing.Size(53, 23)
    Me.nmIntensity3.TabIndex = 1
    Me.ToolTip1.SetToolTip(Me.nmIntensity3, "Adjust the intensity (brightness) before combining")
    Me.nmIntensity3.Value = New Decimal(New Integer() {100, 0, 0, -2147483648})
    '
    'trkIntensity3
    '
    Me.trkIntensity3.AutoSize = False
    Me.trkIntensity3.LargeChange = 100
    Me.trkIntensity3.Location = New System.Drawing.Point(12, 156)
    Me.trkIntensity3.Maximum = 1000
    Me.trkIntensity3.Minimum = -1000
    Me.trkIntensity3.Name = "trkIntensity3"
    Me.trkIntensity3.Size = New System.Drawing.Size(172, 21)
    Me.trkIntensity3.SmallChange = 10
    Me.trkIntensity3.TabIndex = 0
    Me.trkIntensity3.TickStyle = System.Windows.Forms.TickStyle.None
    Me.ToolTip1.SetToolTip(Me.trkIntensity3, "Adjust the intensity (brightness) before combining")
    '
    'chkGray3
    '
    Me.chkGray3.AutoSize = True
    Me.chkGray3.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkGray3.Location = New System.Drawing.Point(20, 200)
    Me.chkGray3.Name = "chkGray3"
    Me.chkGray3.Size = New System.Drawing.Size(167, 21)
    Me.chkGray3.TabIndex = 2
    Me.chkGray3.Text = "Convert to &Grayscale"
    Me.ToolTip1.SetToolTip(Me.chkGray3, "Convert to grayscale before combining")
    Me.chkGray3.UseVisualStyleBackColor = False
    '
    'Label2_1
    '
    Me.Label2_1.AutoSize = True
    Me.Label2_1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label2_1.Location = New System.Drawing.Point(71, 176)
    Me.Label2_1.Name = "Label2_1"
    Me.Label2_1.Size = New System.Drawing.Size(61, 17)
    Me.Label2_1.TabIndex = 13
    Me.Label2_1.Text = "Intensity"
    '
    'cmdHelp
    '
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(217, 616)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 41)
    Me.cmdHelp.TabIndex = 12
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label1.Location = New System.Drawing.Point(12, 549)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(145, 17)
    Me.Label1.TabIndex = 8
    Me.Label1.Text = "&Combination Method:"
    '
    'chkAspect
    '
    Me.chkAspect.AutoSize = True
    Me.chkAspect.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkAspect.Location = New System.Drawing.Point(32, 516)
    Me.chkAspect.Name = "chkAspect"
    Me.chkAspect.Size = New System.Drawing.Size(169, 21)
    Me.chkAspect.TabIndex = 7
    Me.chkAspect.Text = "&Maintain Aspect Ratio"
    Me.ToolTip1.SetToolTip(Me.chkAspect, "Maintain the aspect ratio during scaling")
    Me.chkAspect.UseVisualStyleBackColor = False
    '
    'Combo1
    '
    Me.Combo1.BackColor = System.Drawing.SystemColors.Window
    Me.Combo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.Combo1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Combo1.Location = New System.Drawing.Point(12, 571)
    Me.Combo1.Name = "Combo1"
    Me.Combo1.Size = New System.Drawing.Size(244, 25)
    Me.Combo1.TabIndex = 9
    Me.ToolTip1.SetToolTip(Me.Combo1, "Method of combining." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Use Multiply to add one image to a white area in another." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & _
        "Use Add to add the colors of two photos.")
    '
    'cmdOK
    '
    Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdOK.Location = New System.Drawing.Point(12, 622)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(96, 35)
    Me.cmdOK.TabIndex = 10
    Me.cmdOK.Text = "&OK"
    Me.cmdOK.UseVisualStyleBackColor = False
    '
    'cmdCancel
    '
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdCancel.Location = New System.Drawing.Point(114, 622)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(96, 35)
    Me.cmdCancel.TabIndex = 11
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'chkFit
    '
    Me.chkFit.AutoSize = True
    Me.chkFit.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkFit.Location = New System.Drawing.Point(32, 489)
    Me.chkFit.Name = "chkFit"
    Me.chkFit.Size = New System.Drawing.Size(103, 21)
    Me.chkFit.TabIndex = 6
    Me.chkFit.Text = "&Scale to Fit"
    Me.ToolTip1.SetToolTip(Me.chkFit, "Scale the top photo to fit the bottom photo")
    Me.chkFit.UseVisualStyleBackColor = False
    '
    'pView1
    '
    Me.pView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pView1.BackColor = System.Drawing.SystemColors.ControlDarkDark
    Me.pView1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low
    Me.pView1.Location = New System.Drawing.Point(275, 0)
    Me.pView1.Name = "pView1"
    Me.pView1.Size = New System.Drawing.Size(719, 698)
    Me.pView1.TabIndex = 27
    Me.pView1.TabStop = False
    Me.pView1.ZoomFactor = 1.0R
    '
    'pView2
    '
    Me.pView2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pView2.BackColor = System.Drawing.SystemColors.ControlDarkDark
    Me.pView2.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low
    Me.pView2.Location = New System.Drawing.Point(11, 19)
    Me.pView2.Name = "pView2"
    Me.pView2.Size = New System.Drawing.Size(224, 132)
    Me.pView2.TabIndex = 12
    Me.pView2.TabStop = False
    Me.pView2.ZoomFactor = 1.0R
    '
    'pView3
    '
    Me.pView3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pView3.BackColor = System.Drawing.SystemColors.ControlDarkDark
    Me.pView3.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low
    Me.pView3.Location = New System.Drawing.Point(11, 19)
    Me.pView3.Name = "pView3"
    Me.pView3.Size = New System.Drawing.Size(224, 132)
    Me.pView3.TabIndex = 14
    Me.pView3.TabStop = False
    Me.pView3.ZoomFactor = 1.0R
    '
    'frmCombine
    '
    Me.AcceptButton = Me.cmdOK
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(994, 698)
    Me.Controls.Add(Me.pView1)
    Me.Controls.Add(Me.Frame3)
    Me.Controls.Add(Me.Frame2)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.chkAspect)
    Me.Controls.Add(Me.Combo1)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.chkFit)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 23)
    Me.MinimizeBox = False
    Me.Name = "frmCombine"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Combine Photos"
    Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
    Me.Frame2.ResumeLayout(False)
    Me.Frame2.PerformLayout()
    CType(Me.nmIntensity2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkIntensity2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Frame3.ResumeLayout(False)
    Me.Frame3.PerformLayout()
    CType(Me.nmIntensity3, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkIntensity3, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pView1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pView2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pView3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

End Sub
  Public WithEvents Frame2 As System.Windows.Forms.GroupBox
  Friend WithEvents trkIntensity2 As System.Windows.Forms.TrackBar
  Public WithEvents chkGray2 As System.Windows.Forms.CheckBox
  Public WithEvents Label2_0 As System.Windows.Forms.Label
  Public WithEvents Frame3 As System.Windows.Forms.GroupBox
  Friend WithEvents trkIntensity3 As System.Windows.Forms.TrackBar
  Public WithEvents chkGray3 As System.Windows.Forms.CheckBox
  Public WithEvents Label2_1 As System.Windows.Forms.Label
  Public WithEvents cmdHelp As System.Windows.Forms.Button
  Public WithEvents Label1 As System.Windows.Forms.Label
  Public WithEvents chkAspect As System.Windows.Forms.CheckBox
  Public WithEvents Combo1 As System.Windows.Forms.ComboBox
  Public WithEvents cmdOK As System.Windows.Forms.Button
  Public WithEvents cmdCancel As System.Windows.Forms.Button
  Public WithEvents chkFit As System.Windows.Forms.CheckBox
  Friend WithEvents nmIntensity2 As System.Windows.Forms.NumericUpDown
  Friend WithEvents nmIntensity3 As System.Windows.Forms.NumericUpDown
  Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
  Friend WithEvents pView2 As PhotoMud.pViewer
  Friend WithEvents pView3 As PhotoMud.pViewer
  Friend WithEvents pView1 As PhotoMud.pViewer
#End Region
End Class