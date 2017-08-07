<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmArtEffects
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmArtEffects))
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.nmEffect0 = New System.Windows.Forms.NumericUpDown()
    Me.nmEffect1 = New System.Windows.Forms.NumericUpDown()
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.trkEffect0 = New System.Windows.Forms.TrackBar()
    Me.trkEffect1 = New System.Windows.Forms.TrackBar()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.cmdOK = New System.Windows.Forms.Button()
    Me.lbEffect0 = New System.Windows.Forms.Label()
    Me.lbEffect1 = New System.Windows.Forms.Label()
    Me.chkEffect0 = New System.Windows.Forms.CheckBox()
    Me.chkEffect1 = New System.Windows.Forms.CheckBox()
    Me.optEffect0 = New System.Windows.Forms.RadioButton()
    Me.optEffect1 = New System.Windows.Forms.RadioButton()
    Me.optEffect2 = New System.Windows.Forms.RadioButton()
    Me.nmEffect2 = New System.Windows.Forms.NumericUpDown()
    Me.trkEffect2 = New System.Windows.Forms.TrackBar()
    Me.lbEffect2 = New System.Windows.Forms.Label()
    Me.lbColor0 = New System.Windows.Forms.Label()
    Me.cmdColor0 = New System.Windows.Forms.Button()
    Me.nmEffect5 = New System.Windows.Forms.NumericUpDown()
    Me.trkeffect5 = New System.Windows.Forms.TrackBar()
    Me.lbEffect5 = New System.Windows.Forms.Label()
    Me.nmEffect3 = New System.Windows.Forms.NumericUpDown()
    Me.nmEffect4 = New System.Windows.Forms.NumericUpDown()
    Me.trkeffect3 = New System.Windows.Forms.TrackBar()
    Me.trkeffect4 = New System.Windows.Forms.TrackBar()
    Me.lbEffect3 = New System.Windows.Forms.Label()
    Me.lbEffect4 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.cmbEffect = New System.Windows.Forms.ComboBox()
    Me.aView = New PhotoMud.ctlViewCompare()
    CType(Me.nmEffect0, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmEffect1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkEffect0, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkEffect1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmEffect2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkEffect2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmEffect5, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkeffect5, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmEffect3, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmEffect4, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkeffect3, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkeffect4, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ProgressBar1.Location = New System.Drawing.Point(636, 671)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(233, 16)
    Me.ProgressBar1.TabIndex = 63
    Me.ProgressBar1.Visible = False
    '
    'nmEffect0
    '
    Me.nmEffect0.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.nmEffect0.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
    Me.nmEffect0.Location = New System.Drawing.Point(198, 514)
    Me.nmEffect0.Name = "nmEffect0"
    Me.nmEffect0.Size = New System.Drawing.Size(64, 24)
    Me.nmEffect0.TabIndex = 52
    '
    'nmEffect1
    '
    Me.nmEffect1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.nmEffect1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
    Me.nmEffect1.Location = New System.Drawing.Point(198, 575)
    Me.nmEffect1.Name = "nmEffect1"
    Me.nmEffect1.Size = New System.Drawing.Size(64, 24)
    Me.nmEffect1.TabIndex = 49
    '
    'cmdHelp
    '
    Me.cmdHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(876, 484)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 42)
    Me.cmdHelp.TabIndex = 53
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.cmdHelp.UseVisualStyleBackColor = False
    '
    'trkEffect0
    '
    Me.trkEffect0.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.trkEffect0.AutoSize = False
    Me.trkEffect0.Location = New System.Drawing.Point(24, 513)
    Me.trkEffect0.Maximum = 100
    Me.trkEffect0.Minimum = 1
    Me.trkEffect0.Name = "trkEffect0"
    Me.trkEffect0.Size = New System.Drawing.Size(171, 25)
    Me.trkEffect0.TabIndex = 51
    Me.trkEffect0.TickStyle = System.Windows.Forms.TickStyle.None
    Me.trkEffect0.Value = 1
    '
    'trkEffect1
    '
    Me.trkEffect1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.trkEffect1.AutoSize = False
    Me.trkEffect1.Location = New System.Drawing.Point(24, 575)
    Me.trkEffect1.Maximum = 360
    Me.trkEffect1.Minimum = -360
    Me.trkEffect1.Name = "trkEffect1"
    Me.trkEffect1.Size = New System.Drawing.Size(171, 25)
    Me.trkEffect1.TabIndex = 48
    Me.trkEffect1.TickStyle = System.Windows.Forms.TickStyle.None
    '
    'cmdCancel
    '
    Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdCancel.Location = New System.Drawing.Point(849, 596)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(96, 33)
    Me.cmdCancel.TabIndex = 56
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'cmdOK
    '
    Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdOK.Location = New System.Drawing.Point(849, 543)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(96, 33)
    Me.cmdOK.TabIndex = 54
    Me.cmdOK.Text = "&OK"
    Me.cmdOK.UseVisualStyleBackColor = False
    '
    'lbEffect0
    '
    Me.lbEffect0.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbEffect0.AutoSize = True
    Me.lbEffect0.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbEffect0.Location = New System.Drawing.Point(34, 540)
    Me.lbEffect0.Name = "lbEffect0"
    Me.lbEffect0.Size = New System.Drawing.Size(54, 17)
    Me.lbEffect0.TabIndex = 50
    Me.lbEffect0.Text = "Effect0"
    '
    'lbEffect1
    '
    Me.lbEffect1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbEffect1.AutoSize = True
    Me.lbEffect1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbEffect1.Location = New System.Drawing.Point(34, 601)
    Me.lbEffect1.Name = "lbEffect1"
    Me.lbEffect1.Size = New System.Drawing.Size(54, 17)
    Me.lbEffect1.TabIndex = 47
    Me.lbEffect1.Text = "Effect1"
    '
    'chkEffect0
    '
    Me.chkEffect0.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.chkEffect0.AutoSize = True
    Me.chkEffect0.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkEffect0.Location = New System.Drawing.Point(585, 520)
    Me.chkEffect0.Name = "chkEffect0"
    Me.chkEffect0.Size = New System.Drawing.Size(105, 21)
    Me.chkEffect0.TabIndex = 65
    Me.chkEffect0.Text = "CheckBox1"
    Me.chkEffect0.UseVisualStyleBackColor = True
    '
    'chkEffect1
    '
    Me.chkEffect1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.chkEffect1.AutoSize = True
    Me.chkEffect1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkEffect1.Location = New System.Drawing.Point(585, 546)
    Me.chkEffect1.Name = "chkEffect1"
    Me.chkEffect1.Size = New System.Drawing.Size(105, 21)
    Me.chkEffect1.TabIndex = 66
    Me.chkEffect1.Text = "CheckBox1"
    Me.chkEffect1.UseVisualStyleBackColor = True
    '
    'optEffect0
    '
    Me.optEffect0.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.optEffect0.AutoSize = True
    Me.optEffect0.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.optEffect0.Location = New System.Drawing.Point(585, 562)
    Me.optEffect0.Name = "optEffect0"
    Me.optEffect0.Size = New System.Drawing.Size(117, 21)
    Me.optEffect0.TabIndex = 67
    Me.optEffect0.TabStop = True
    Me.optEffect0.Text = "RadioButton1"
    Me.optEffect0.UseVisualStyleBackColor = True
    '
    'optEffect1
    '
    Me.optEffect1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.optEffect1.AutoSize = True
    Me.optEffect1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.optEffect1.Location = New System.Drawing.Point(585, 588)
    Me.optEffect1.Name = "optEffect1"
    Me.optEffect1.Size = New System.Drawing.Size(117, 21)
    Me.optEffect1.TabIndex = 68
    Me.optEffect1.TabStop = True
    Me.optEffect1.Text = "RadioButton2"
    Me.optEffect1.UseVisualStyleBackColor = True
    '
    'optEffect2
    '
    Me.optEffect2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.optEffect2.AutoSize = True
    Me.optEffect2.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.optEffect2.Location = New System.Drawing.Point(585, 615)
    Me.optEffect2.Name = "optEffect2"
    Me.optEffect2.Size = New System.Drawing.Size(117, 21)
    Me.optEffect2.TabIndex = 69
    Me.optEffect2.TabStop = True
    Me.optEffect2.Text = "RadioButton2"
    Me.optEffect2.UseVisualStyleBackColor = True
    '
    'nmEffect2
    '
    Me.nmEffect2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.nmEffect2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
    Me.nmEffect2.Location = New System.Drawing.Point(198, 637)
    Me.nmEffect2.Name = "nmEffect2"
    Me.nmEffect2.Size = New System.Drawing.Size(64, 24)
    Me.nmEffect2.TabIndex = 72
    '
    'trkEffect2
    '
    Me.trkEffect2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.trkEffect2.AutoSize = False
    Me.trkEffect2.Location = New System.Drawing.Point(24, 637)
    Me.trkEffect2.Maximum = 360
    Me.trkEffect2.Minimum = -360
    Me.trkEffect2.Name = "trkEffect2"
    Me.trkEffect2.Size = New System.Drawing.Size(171, 25)
    Me.trkEffect2.TabIndex = 71
    Me.trkEffect2.TickStyle = System.Windows.Forms.TickStyle.None
    '
    'lbEffect2
    '
    Me.lbEffect2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbEffect2.AutoSize = True
    Me.lbEffect2.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbEffect2.Location = New System.Drawing.Point(34, 664)
    Me.lbEffect2.Name = "lbEffect2"
    Me.lbEffect2.Size = New System.Drawing.Size(54, 17)
    Me.lbEffect2.TabIndex = 70
    Me.lbEffect2.Text = "Effect2"
    '
    'lbColor0
    '
    Me.lbColor0.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbColor0.AutoSize = True
    Me.lbColor0.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbColor0.Location = New System.Drawing.Point(629, 486)
    Me.lbColor0.Name = "lbColor0"
    Me.lbColor0.Size = New System.Drawing.Size(43, 17)
    Me.lbColor0.TabIndex = 73
    Me.lbColor0.Text = "Color"
    '
    'cmdColor0
    '
    Me.cmdColor0.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdColor0.BackColor = System.Drawing.SystemColors.WindowText
    Me.cmdColor0.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdColor0.Location = New System.Drawing.Point(585, 484)
    Me.cmdColor0.Name = "cmdColor0"
    Me.cmdColor0.Size = New System.Drawing.Size(36, 21)
    Me.cmdColor0.TabIndex = 74
    Me.cmdColor0.UseVisualStyleBackColor = False
    '
    'nmEffect5
    '
    Me.nmEffect5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.nmEffect5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
    Me.nmEffect5.Location = New System.Drawing.Point(472, 637)
    Me.nmEffect5.Name = "nmEffect5"
    Me.nmEffect5.Size = New System.Drawing.Size(64, 24)
    Me.nmEffect5.TabIndex = 83
    '
    'trkeffect5
    '
    Me.trkeffect5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.trkeffect5.AutoSize = False
    Me.trkeffect5.Location = New System.Drawing.Point(298, 637)
    Me.trkeffect5.Maximum = 360
    Me.trkeffect5.Minimum = -360
    Me.trkeffect5.Name = "trkeffect5"
    Me.trkeffect5.Size = New System.Drawing.Size(171, 25)
    Me.trkeffect5.TabIndex = 82
    Me.trkeffect5.TickStyle = System.Windows.Forms.TickStyle.None
    '
    'lbEffect5
    '
    Me.lbEffect5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbEffect5.AutoSize = True
    Me.lbEffect5.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbEffect5.Location = New System.Drawing.Point(308, 663)
    Me.lbEffect5.Name = "lbEffect5"
    Me.lbEffect5.Size = New System.Drawing.Size(54, 17)
    Me.lbEffect5.TabIndex = 81
    Me.lbEffect5.Text = "Effect2"
    '
    'nmEffect3
    '
    Me.nmEffect3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.nmEffect3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
    Me.nmEffect3.Location = New System.Drawing.Point(472, 514)
    Me.nmEffect3.Name = "nmEffect3"
    Me.nmEffect3.Size = New System.Drawing.Size(64, 24)
    Me.nmEffect3.TabIndex = 80
    '
    'nmEffect4
    '
    Me.nmEffect4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.nmEffect4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
    Me.nmEffect4.Location = New System.Drawing.Point(472, 575)
    Me.nmEffect4.Name = "nmEffect4"
    Me.nmEffect4.Size = New System.Drawing.Size(64, 24)
    Me.nmEffect4.TabIndex = 77
    '
    'trkeffect3
    '
    Me.trkeffect3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.trkeffect3.AutoSize = False
    Me.trkeffect3.Location = New System.Drawing.Point(298, 513)
    Me.trkeffect3.Maximum = 100
    Me.trkeffect3.Minimum = 1
    Me.trkeffect3.Name = "trkeffect3"
    Me.trkeffect3.Size = New System.Drawing.Size(171, 25)
    Me.trkeffect3.TabIndex = 79
    Me.trkeffect3.TickStyle = System.Windows.Forms.TickStyle.None
    Me.trkeffect3.Value = 1
    '
    'trkeffect4
    '
    Me.trkeffect4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.trkeffect4.AutoSize = False
    Me.trkeffect4.Location = New System.Drawing.Point(298, 575)
    Me.trkeffect4.Maximum = 360
    Me.trkeffect4.Minimum = -360
    Me.trkeffect4.Name = "trkeffect4"
    Me.trkeffect4.Size = New System.Drawing.Size(171, 25)
    Me.trkeffect4.TabIndex = 76
    Me.trkeffect4.TickStyle = System.Windows.Forms.TickStyle.None
    '
    'lbEffect3
    '
    Me.lbEffect3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbEffect3.AutoSize = True
    Me.lbEffect3.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbEffect3.Location = New System.Drawing.Point(308, 540)
    Me.lbEffect3.Name = "lbEffect3"
    Me.lbEffect3.Size = New System.Drawing.Size(54, 17)
    Me.lbEffect3.TabIndex = 78
    Me.lbEffect3.Text = "Effect0"
    '
    'lbEffect4
    '
    Me.lbEffect4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbEffect4.AutoSize = True
    Me.lbEffect4.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbEffect4.Location = New System.Drawing.Point(308, 601)
    Me.lbEffect4.Name = "lbEffect4"
    Me.lbEffect4.Size = New System.Drawing.Size(54, 17)
    Me.lbEffect4.TabIndex = 75
    Me.lbEffect4.Text = "Effect1"
    '
    'Label2
    '
    Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(34, 437)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(98, 17)
    Me.Label2.TabIndex = 0
    Me.Label2.Text = "&Special Effect"
    '
    'cmbEffect
    '
    Me.cmbEffect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmbEffect.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
    Me.cmbEffect.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.cmbEffect.FormattingEnabled = True
    Me.cmbEffect.IntegralHeight = False
    Me.cmbEffect.Location = New System.Drawing.Point(27, 459)
    Me.cmbEffect.MaxDropDownItems = 30
    Me.cmbEffect.Name = "cmbEffect"
    Me.cmbEffect.Size = New System.Drawing.Size(296, 25)
    Me.cmbEffect.TabIndex = 1
    '
    'aView
    '
    Me.aView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.aView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.aView.Location = New System.Drawing.Point(1, 2)
    Me.aView.Name = "aView"
    Me.aView.NextButtons = False
    Me.aView.pCenter = New System.Drawing.Point(0, 0)
    Me.aView.SingleView = False
    Me.aView.Size = New System.Drawing.Size(974, 425)
    Me.aView.TabIndex = 85
    Me.aView.zoomFactor = 0.0R
    '
    'frmArtEffects
    '
    Me.AcceptButton = Me.cmdOK
    Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(977, 698)
    Me.Controls.Add(Me.aView)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.nmEffect5)
    Me.Controls.Add(Me.trkeffect5)
    Me.Controls.Add(Me.lbEffect5)
    Me.Controls.Add(Me.nmEffect3)
    Me.Controls.Add(Me.nmEffect4)
    Me.Controls.Add(Me.trkeffect3)
    Me.Controls.Add(Me.trkeffect4)
    Me.Controls.Add(Me.lbEffect3)
    Me.Controls.Add(Me.lbEffect4)
    Me.Controls.Add(Me.lbColor0)
    Me.Controls.Add(Me.cmdColor0)
    Me.Controls.Add(Me.nmEffect2)
    Me.Controls.Add(Me.trkEffect2)
    Me.Controls.Add(Me.lbEffect2)
    Me.Controls.Add(Me.optEffect2)
    Me.Controls.Add(Me.optEffect1)
    Me.Controls.Add(Me.optEffect0)
    Me.Controls.Add(Me.chkEffect1)
    Me.Controls.Add(Me.chkEffect0)
    Me.Controls.Add(Me.cmbEffect)
    Me.Controls.Add(Me.ProgressBar1)
    Me.Controls.Add(Me.nmEffect0)
    Me.Controls.Add(Me.nmEffect1)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.trkEffect0)
    Me.Controls.Add(Me.trkEffect1)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.lbEffect0)
    Me.Controls.Add(Me.lbEffect1)
    Me.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "frmArtEffects"
    Me.ShowInTaskbar = False
    Me.Text = "Artistic Effects"
    CType(Me.nmEffect0, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmEffect1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkEffect0, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkEffect1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmEffect2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkEffect2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmEffect5, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkeffect5, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmEffect3, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmEffect4, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkeffect3, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkeffect4, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

End Sub
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents nmEffect0 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmEffect1 As System.Windows.Forms.NumericUpDown
    Public WithEvents cmdHelp As System.Windows.Forms.Button
    Friend WithEvents trkEffect0 As System.Windows.Forms.TrackBar
    Friend WithEvents trkEffect1 As System.Windows.Forms.TrackBar
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents lbEffect0 As System.Windows.Forms.Label
    Public WithEvents lbEffect1 As System.Windows.Forms.Label
    Friend WithEvents chkEffect0 As System.Windows.Forms.CheckBox
    Friend WithEvents chkEffect1 As System.Windows.Forms.CheckBox
    Friend WithEvents optEffect0 As System.Windows.Forms.RadioButton
    Friend WithEvents optEffect1 As System.Windows.Forms.RadioButton
    Friend WithEvents optEffect2 As System.Windows.Forms.RadioButton
    Friend WithEvents nmEffect2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents trkEffect2 As System.Windows.Forms.TrackBar
    Public WithEvents lbEffect2 As System.Windows.Forms.Label
    Friend WithEvents lbColor0 As System.Windows.Forms.Label
    Friend WithEvents cmdColor0 As System.Windows.Forms.Button
    Friend WithEvents nmEffect5 As System.Windows.Forms.NumericUpDown
    Friend WithEvents trkeffect5 As System.Windows.Forms.TrackBar
    Public WithEvents lbEffect5 As System.Windows.Forms.Label
    Friend WithEvents nmEffect3 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmEffect4 As System.Windows.Forms.NumericUpDown
    Friend WithEvents trkeffect3 As System.Windows.Forms.TrackBar
    Friend WithEvents trkeffect4 As System.Windows.Forms.TrackBar
    Public WithEvents lbEffect3 As System.Windows.Forms.Label
    Public WithEvents lbEffect4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbEffect As System.Windows.Forms.ComboBox
    Friend WithEvents aView As PhotoMud.ctlViewCompare
End Class
