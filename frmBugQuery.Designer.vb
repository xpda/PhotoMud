<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class chk
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(chk))
    Me.txRemarks = New System.Windows.Forms.RichTextBox()
    Me.txTaxon = New System.Windows.Forms.TextBox()
    Me.cmdTaxon = New System.Windows.Forms.Button()
    Me.tvTaxon = New System.Windows.Forms.TreeView()
    Me.txLocation = New System.Windows.Forms.TextBox()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.chkDescendants = New System.Windows.Forms.CheckBox()
    Me.cmdOK = New System.Windows.Forms.Button()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.txCommon = New System.Windows.Forms.TextBox()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.txCountry = New System.Windows.Forms.TextBox()
    Me.txState = New System.Windows.Forms.TextBox()
    Me.txCounty = New System.Windows.Forms.TextBox()
    Me.txFilename = New System.Windows.Forms.TextBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.txRankMin = New System.Windows.Forms.TextBox()
    Me.txRankMax = New System.Windows.Forms.TextBox()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.txModMax = New System.Windows.Forms.TextBox()
    Me.Label11 = New System.Windows.Forms.Label()
    Me.txModMin = New System.Windows.Forms.TextBox()
    Me.lbDate2 = New System.Windows.Forms.Label()
    Me.txElevationMax = New System.Windows.Forms.TextBox()
    Me.txConfidenceMin = New System.Windows.Forms.TextBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.txElevationMin = New System.Windows.Forms.TextBox()
    Me.lbConfidence = New System.Windows.Forms.Label()
    Me.txDateMax = New System.Windows.Forms.TextBox()
    Me.txDateMin = New System.Windows.Forms.TextBox()
    Me.txConfidenceMax = New System.Windows.Forms.TextBox()
    Me.txRatingMin = New System.Windows.Forms.TextBox()
    Me.txRatingMax = New System.Windows.Forms.TextBox()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.chkBugguide = New System.Windows.Forms.CheckBox()
    Me.chkInat = New System.Windows.Forms.CheckBox()
    Me.Panel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'txRemarks
    '
    Me.txRemarks.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txRemarks.Location = New System.Drawing.Point(155, 267)
    Me.txRemarks.Name = "txRemarks"
    Me.txRemarks.Size = New System.Drawing.Size(332, 26)
    Me.txRemarks.TabIndex = 13
    Me.txRemarks.Text = ""
    '
    'txTaxon
    '
    Me.txTaxon.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
    Me.txTaxon.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txTaxon.Location = New System.Drawing.Point(155, 51)
    Me.txTaxon.Name = "txTaxon"
    Me.txTaxon.Size = New System.Drawing.Size(296, 25)
    Me.txTaxon.TabIndex = 1
    '
    'cmdTaxon
    '
    Me.cmdTaxon.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdTaxon.Location = New System.Drawing.Point(459, 50)
    Me.cmdTaxon.Name = "cmdTaxon"
    Me.cmdTaxon.Size = New System.Drawing.Size(30, 27)
    Me.cmdTaxon.TabIndex = 2
    Me.cmdTaxon.Text = "..."
    Me.cmdTaxon.UseVisualStyleBackColor = True
    '
    'tvTaxon
    '
    Me.tvTaxon.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.tvTaxon.LabelEdit = True
    Me.tvTaxon.Location = New System.Drawing.Point(519, 49)
    Me.tvTaxon.Name = "tvTaxon"
    Me.tvTaxon.ShowNodeToolTips = True
    Me.tvTaxon.Size = New System.Drawing.Size(585, 669)
    Me.tvTaxon.TabIndex = 4
    '
    'txLocation
    '
    Me.txLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
    Me.txLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
    Me.txLocation.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txLocation.Location = New System.Drawing.Point(155, 160)
    Me.txLocation.Name = "txLocation"
    Me.txLocation.Size = New System.Drawing.Size(332, 25)
    Me.txLocation.TabIndex = 7
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label6.Location = New System.Drawing.Point(34, 270)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(72, 17)
    Me.Label6.TabIndex = 12
    Me.Label6.Text = "Re&marks:"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label3.Location = New System.Drawing.Point(34, 163)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(67, 17)
    Me.Label3.TabIndex = 6
    Me.Label3.Text = "&Location:"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label1.Location = New System.Drawing.Point(34, 53)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(114, 17)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "Scientific &Name:"
    '
    'chkDescendants
    '
    Me.chkDescendants.AutoSize = True
    Me.chkDescendants.Checked = True
    Me.chkDescendants.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkDescendants.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.chkDescendants.Location = New System.Drawing.Point(160, 83)
    Me.chkDescendants.Name = "chkDescendants"
    Me.chkDescendants.Size = New System.Drawing.Size(167, 21)
    Me.chkDescendants.TabIndex = 3
    Me.chkDescendants.Text = "Incl&ude Descendants"
    Me.chkDescendants.UseVisualStyleBackColor = True
    '
    'cmdOK
    '
    Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdOK.Location = New System.Drawing.Point(115, 686)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(103, 44)
    Me.cmdOK.TabIndex = 80
    Me.cmdOK.Text = "&OK"
    Me.cmdOK.UseVisualStyleBackColor = True
    '
    'cmdCancel
    '
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdCancel.Location = New System.Drawing.Point(281, 686)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(103, 44)
    Me.cmdCancel.TabIndex = 81
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = True
    '
    'txCommon
    '
    Me.txCommon.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
    Me.txCommon.Enabled = False
    Me.txCommon.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txCommon.Location = New System.Drawing.Point(155, 110)
    Me.txCommon.Name = "txCommon"
    Me.txCommon.Size = New System.Drawing.Size(332, 25)
    Me.txCommon.TabIndex = 5
    Me.txCommon.TabStop = False
    '
    'Label10
    '
    Me.Label10.AutoSize = True
    Me.Label10.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label10.Location = New System.Drawing.Point(34, 206)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(95, 17)
    Me.Label10.TabIndex = 8
    Me.Label10.Text = "Count&y, State"
    '
    'txCountry
    '
    Me.txCountry.Location = New System.Drawing.Point(379, 203)
    Me.txCountry.Name = "txCountry"
    Me.txCountry.Size = New System.Drawing.Size(108, 25)
    Me.txCountry.TabIndex = 11
    '
    'txState
    '
    Me.txState.Location = New System.Drawing.Point(321, 203)
    Me.txState.Name = "txState"
    Me.txState.Size = New System.Drawing.Size(39, 25)
    Me.txState.TabIndex = 10
    '
    'txCounty
    '
    Me.txCounty.Location = New System.Drawing.Point(155, 203)
    Me.txCounty.Name = "txCounty"
    Me.txCounty.Size = New System.Drawing.Size(148, 25)
    Me.txCounty.TabIndex = 9
    '
    'txFilename
    '
    Me.txFilename.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
    Me.txFilename.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
    Me.txFilename.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txFilename.Location = New System.Drawing.Point(155, 305)
    Me.txFilename.Name = "txFilename"
    Me.txFilename.Size = New System.Drawing.Size(332, 25)
    Me.txFilename.TabIndex = 15
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label2.Location = New System.Drawing.Point(34, 306)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(72, 17)
    Me.Label2.TabIndex = 14
    Me.Label2.Text = "&Filename:"
    '
    'Panel1
    '
    Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.Panel1.Controls.Add(Me.txRankMin)
    Me.Panel1.Controls.Add(Me.txRankMax)
    Me.Panel1.Controls.Add(Me.Label9)
    Me.Panel1.Controls.Add(Me.txModMax)
    Me.Panel1.Controls.Add(Me.Label11)
    Me.Panel1.Controls.Add(Me.txModMin)
    Me.Panel1.Controls.Add(Me.lbDate2)
    Me.Panel1.Controls.Add(Me.txElevationMax)
    Me.Panel1.Controls.Add(Me.txConfidenceMin)
    Me.Panel1.Controls.Add(Me.Label4)
    Me.Panel1.Controls.Add(Me.txElevationMin)
    Me.Panel1.Controls.Add(Me.lbConfidence)
    Me.Panel1.Controls.Add(Me.txDateMax)
    Me.Panel1.Controls.Add(Me.txDateMin)
    Me.Panel1.Controls.Add(Me.txConfidenceMax)
    Me.Panel1.Controls.Add(Me.txRatingMin)
    Me.Panel1.Controls.Add(Me.txRatingMax)
    Me.Panel1.Controls.Add(Me.Label5)
    Me.Panel1.Controls.Add(Me.Label8)
    Me.Panel1.Controls.Add(Me.Label7)
    Me.Panel1.Location = New System.Drawing.Point(30, 393)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(459, 274)
    Me.Panel1.TabIndex = 82
    '
    'txRankMin
    '
    Me.txRankMin.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txRankMin.Location = New System.Drawing.Point(138, 226)
    Me.txRankMin.Name = "txRankMin"
    Me.txRankMin.Size = New System.Drawing.Size(134, 25)
    Me.txRankMin.TabIndex = 121
    '
    'txRankMax
    '
    Me.txRankMax.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txRankMax.Location = New System.Drawing.Point(299, 226)
    Me.txRankMax.Name = "txRankMax"
    Me.txRankMax.Size = New System.Drawing.Size(142, 25)
    Me.txRankMax.TabIndex = 122
    '
    'Label9
    '
    Me.Label9.AutoSize = True
    Me.Label9.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label9.Location = New System.Drawing.Point(13, 76)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(96, 17)
    Me.Label9.TabIndex = 106
    Me.Label9.Text = "Modified Dat&e"
    '
    'txModMax
    '
    Me.txModMax.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txModMax.Location = New System.Drawing.Point(299, 76)
    Me.txModMax.Name = "txModMax"
    Me.txModMax.Size = New System.Drawing.Size(142, 25)
    Me.txModMax.TabIndex = 108
    '
    'Label11
    '
    Me.Label11.AutoSize = True
    Me.Label11.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label11.Location = New System.Drawing.Point(13, 229)
    Me.Label11.Name = "Label11"
    Me.Label11.Size = New System.Drawing.Size(46, 17)
    Me.Label11.TabIndex = 120
    Me.Label11.Text = "Ran&k:"
    '
    'txModMin
    '
    Me.txModMin.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txModMin.Location = New System.Drawing.Point(138, 76)
    Me.txModMin.Name = "txModMin"
    Me.txModMin.Size = New System.Drawing.Size(142, 25)
    Me.txModMin.TabIndex = 107
    '
    'lbDate2
    '
    Me.lbDate2.AutoSize = True
    Me.lbDate2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbDate2.Location = New System.Drawing.Point(13, 38)
    Me.lbDate2.Name = "lbDate2"
    Me.lbDate2.Size = New System.Drawing.Size(85, 17)
    Me.lbDate2.TabIndex = 103
    Me.lbDate2.Text = "Photo &Date:"
    '
    'txElevationMax
    '
    Me.txElevationMax.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txElevationMax.Location = New System.Drawing.Point(299, 115)
    Me.txElevationMax.Name = "txElevationMax"
    Me.txElevationMax.Size = New System.Drawing.Size(113, 25)
    Me.txElevationMax.TabIndex = 111
    '
    'txConfidenceMin
    '
    Me.txConfidenceMin.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txConfidenceMin.Location = New System.Drawing.Point(138, 151)
    Me.txConfidenceMin.Name = "txConfidenceMin"
    Me.txConfidenceMin.Size = New System.Drawing.Size(113, 25)
    Me.txConfidenceMin.TabIndex = 113
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label4.Location = New System.Drawing.Point(12, 118)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(67, 17)
    Me.Label4.TabIndex = 109
    Me.Label4.Text = "Ele&vation"
    '
    'txElevationMin
    '
    Me.txElevationMin.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txElevationMin.Location = New System.Drawing.Point(138, 115)
    Me.txElevationMin.Name = "txElevationMin"
    Me.txElevationMin.Size = New System.Drawing.Size(113, 25)
    Me.txElevationMin.TabIndex = 110
    '
    'lbConfidence
    '
    Me.lbConfidence.AutoSize = True
    Me.lbConfidence.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbConfidence.Location = New System.Drawing.Point(13, 154)
    Me.lbConfidence.Name = "lbConfidence"
    Me.lbConfidence.Size = New System.Drawing.Size(104, 17)
    Me.lbConfidence.TabIndex = 112
    Me.lbConfidence.Text = "&ID Confidence:"
    '
    'txDateMax
    '
    Me.txDateMax.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txDateMax.Location = New System.Drawing.Point(299, 38)
    Me.txDateMax.Name = "txDateMax"
    Me.txDateMax.Size = New System.Drawing.Size(142, 25)
    Me.txDateMax.TabIndex = 105
    '
    'txDateMin
    '
    Me.txDateMin.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txDateMin.Location = New System.Drawing.Point(138, 38)
    Me.txDateMin.Name = "txDateMin"
    Me.txDateMin.Size = New System.Drawing.Size(142, 25)
    Me.txDateMin.TabIndex = 104
    '
    'txConfidenceMax
    '
    Me.txConfidenceMax.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txConfidenceMax.Location = New System.Drawing.Point(299, 151)
    Me.txConfidenceMax.Name = "txConfidenceMax"
    Me.txConfidenceMax.Size = New System.Drawing.Size(113, 25)
    Me.txConfidenceMax.TabIndex = 114
    '
    'txRatingMin
    '
    Me.txRatingMin.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txRatingMin.Location = New System.Drawing.Point(138, 187)
    Me.txRatingMin.Name = "txRatingMin"
    Me.txRatingMin.Size = New System.Drawing.Size(113, 25)
    Me.txRatingMin.TabIndex = 116
    '
    'txRatingMax
    '
    Me.txRatingMax.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txRatingMax.Location = New System.Drawing.Point(299, 187)
    Me.txRatingMax.Name = "txRatingMax"
    Me.txRatingMax.Size = New System.Drawing.Size(113, 25)
    Me.txRatingMax.TabIndex = 117
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label5.Location = New System.Drawing.Point(13, 190)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(54, 17)
    Me.Label5.TabIndex = 115
    Me.Label5.Text = "&Rating:"
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(302, 14)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(73, 17)
    Me.Label8.TabIndex = 102
    Me.Label8.Text = "maximum"
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Location = New System.Drawing.Point(141, 14)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(69, 17)
    Me.Label7.TabIndex = 101
    Me.Label7.Text = "minimum"
    '
    'chkBugguide
    '
    Me.chkBugguide.AutoSize = True
    Me.chkBugguide.Checked = True
    Me.chkBugguide.CheckState = System.Windows.Forms.CheckState.Indeterminate
    Me.chkBugguide.Location = New System.Drawing.Point(155, 349)
    Me.chkBugguide.Name = "chkBugguide"
    Me.chkBugguide.Size = New System.Drawing.Size(91, 21)
    Me.chkBugguide.TabIndex = 20
    Me.chkBugguide.Text = "&Bugguide"
    Me.chkBugguide.ThreeState = True
    Me.chkBugguide.UseVisualStyleBackColor = True
    '
    'chkInat
    '
    Me.chkInat.AutoSize = True
    Me.chkInat.Checked = True
    Me.chkInat.CheckState = System.Windows.Forms.CheckState.Indeterminate
    Me.chkInat.Location = New System.Drawing.Point(393, 349)
    Me.chkInat.Name = "chkInat"
    Me.chkInat.Size = New System.Drawing.Size(94, 21)
    Me.chkInat.TabIndex = 24
    Me.chkInat.Text = "iNa&turalist"
    Me.chkInat.ThreeState = True
    Me.chkInat.UseVisualStyleBackColor = True
    '
    'chk
    '
    Me.AcceptButton = Me.cmdOK
    Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(1127, 750)
    Me.Controls.Add(Me.chkInat)
    Me.Controls.Add(Me.chkBugguide)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.txFilename)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label10)
    Me.Controls.Add(Me.txCountry)
    Me.Controls.Add(Me.txState)
    Me.Controls.Add(Me.txCounty)
    Me.Controls.Add(Me.txCommon)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.chkDescendants)
    Me.Controls.Add(Me.txRemarks)
    Me.Controls.Add(Me.txTaxon)
    Me.Controls.Add(Me.cmdTaxon)
    Me.Controls.Add(Me.tvTaxon)
    Me.Controls.Add(Me.txLocation)
    Me.Controls.Add(Me.Label6)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.Label1)
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "chk"
    Me.ShowInTaskbar = False
    Me.Text = "Query"
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txRemarks As System.Windows.Forms.RichTextBox
  Friend WithEvents txTaxon As System.Windows.Forms.TextBox
  Friend WithEvents cmdTaxon As System.Windows.Forms.Button
  Friend WithEvents tvTaxon As System.Windows.Forms.TreeView
  Friend WithEvents txLocation As System.Windows.Forms.TextBox
  Public WithEvents Label6 As System.Windows.Forms.Label
  Public WithEvents Label3 As System.Windows.Forms.Label
  Public WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents chkDescendants As System.Windows.Forms.CheckBox
  Friend WithEvents cmdOK As System.Windows.Forms.Button
  Friend WithEvents cmdCancel As System.Windows.Forms.Button
  Friend WithEvents txCommon As System.Windows.Forms.TextBox
  Public WithEvents Label10 As System.Windows.Forms.Label
  Friend WithEvents txCountry As System.Windows.Forms.TextBox
  Friend WithEvents txState As System.Windows.Forms.TextBox
  Friend WithEvents txCounty As System.Windows.Forms.TextBox
  Friend WithEvents txFilename As System.Windows.Forms.TextBox
  Public WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Public WithEvents lbDate2 As System.Windows.Forms.Label
  Friend WithEvents txElevationMax As System.Windows.Forms.TextBox
  Friend WithEvents txConfidenceMin As System.Windows.Forms.TextBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents txElevationMin As System.Windows.Forms.TextBox
  Friend WithEvents lbConfidence As System.Windows.Forms.Label
  Friend WithEvents txDateMax As System.Windows.Forms.TextBox
  Friend WithEvents txDateMin As System.Windows.Forms.TextBox
  Friend WithEvents txConfidenceMax As System.Windows.Forms.TextBox
  Friend WithEvents txRatingMin As System.Windows.Forms.TextBox
  Friend WithEvents txRatingMax As System.Windows.Forms.TextBox
  Public WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents Label7 As System.Windows.Forms.Label
  Public WithEvents Label9 As System.Windows.Forms.Label
  Friend WithEvents txModMax As System.Windows.Forms.TextBox
  Friend WithEvents txModMin As System.Windows.Forms.TextBox
  Public WithEvents Label11 As System.Windows.Forms.Label
  Friend WithEvents chkBugguide As System.Windows.Forms.CheckBox
  Friend WithEvents chkInat As System.Windows.Forms.CheckBox
  Friend WithEvents txRankMin As System.Windows.Forms.TextBox
  Friend WithEvents txRankMax As System.Windows.Forms.TextBox
End Class
