<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSaveAs
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
  Public WithEvents txFileName As TextBox
  Public WithEvents cmdBrowse As Button
  Public WithEvents txPct As TextBox
  Public WithEvents chkResize As CheckBox
  Public WithEvents txXres As TextBox
  Public WithEvents txYres As TextBox
  Public WithEvents lbPct As Label
  Public WithEvents lbXres As Label
  Public WithEvents lbYres As Label
  Public WithEvents fmResize As GroupBox
  Public WithEvents cmbFiletype As ComboBox
  Public WithEvents cmdSave As Button
  Public WithEvents cmdCancel As Button
  Public WithEvents Label1 As Label
  Public WithEvents label3 As Label
  Public WithEvents lbCompression1 As Label
  Public WithEvents lbCompression As Label
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSaveAs))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.chkResize = New System.Windows.Forms.CheckBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.nmPngCompression = New System.Windows.Forms.NumericUpDown()
    Me.chkExif = New System.Windows.Forms.CheckBox()
    Me.txFileName = New System.Windows.Forms.TextBox()
    Me.cmdBrowse = New System.Windows.Forms.Button()
    Me.fmResize = New System.Windows.Forms.GroupBox()
    Me.txPct = New System.Windows.Forms.TextBox()
    Me.txXres = New System.Windows.Forms.TextBox()
    Me.txYres = New System.Windows.Forms.TextBox()
    Me.lbPct = New System.Windows.Forms.Label()
    Me.lbXres = New System.Windows.Forms.Label()
    Me.lbYres = New System.Windows.Forms.Label()
    Me.cmbFiletype = New System.Windows.Forms.ComboBox()
    Me.cmdSave = New System.Windows.Forms.Button()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.label3 = New System.Windows.Forms.Label()
    Me.lbCompression1 = New System.Windows.Forms.Label()
    Me.lbCompression = New System.Windows.Forms.Label()
    Me.nmQuality = New System.Windows.Forms.NumericUpDown()
    Me.fmJpg = New System.Windows.Forms.GroupBox()
    Me.fmPng = New System.Windows.Forms.GroupBox()
    Me.chkPngIndexed = New System.Windows.Forms.CheckBox()
    CType(Me.nmPngCompression, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmResize.SuspendLayout()
    CType(Me.nmQuality, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmJpg.SuspendLayout()
    Me.fmPng.SuspendLayout()
    Me.SuspendLayout()
    '
    'chkResize
    '
    Me.chkResize.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkResize.Location = New System.Drawing.Point(25, 25)
    Me.chkResize.Name = "chkResize"
    Me.chkResize.Size = New System.Drawing.Size(161, 26)
    Me.chkResize.TabIndex = 0
    Me.chkResize.Text = "&Resize Photo"
    Me.ToolTip1.SetToolTip(Me.chkResize, "Convert the photo to a different size before saving")
    Me.chkResize.UseVisualStyleBackColor = False
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label2.Location = New System.Drawing.Point(36, 34)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(135, 17)
    Me.Label2.TabIndex = 6
    Me.Label2.Text = "&Compression (0-9):"
    Me.ToolTip1.SetToolTip(Me.Label2, "A low number is higher quality, large file size." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "A larger number is lower qualit" & _
        "y, smaller file size." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "15 is a good value in most cases.")
    '
    'nmPngCompression
    '
    Me.nmPngCompression.Location = New System.Drawing.Point(190, 32)
    Me.nmPngCompression.Maximum = New Decimal(New Integer() {9, 0, 0, 0})
    Me.nmPngCompression.Name = "nmPngCompression"
    Me.nmPngCompression.Size = New System.Drawing.Size(70, 25)
    Me.nmPngCompression.TabIndex = 7
    Me.ToolTip1.SetToolTip(Me.nmPngCompression, "A low number is higher quality, large file size." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "A larger number is lower qualit" & _
        "y, smaller file size." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "15 is a good value in most cases.")
    Me.nmPngCompression.Value = New Decimal(New Integer() {7, 0, 0, 0})
    '
    'chkExif
    '
    Me.chkExif.AutoSize = True
    Me.chkExif.Checked = True
    Me.chkExif.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkExif.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.chkExif.Location = New System.Drawing.Point(350, 26)
    Me.chkExif.Name = "chkExif"
    Me.chkExif.Size = New System.Drawing.Size(154, 21)
    Me.chkExif.TabIndex = 21
    Me.chkExif.Text = "Save E&xif Metadata"
    Me.ToolTip1.SetToolTip(Me.chkExif, "Change the size of the output files")
    Me.chkExif.UseVisualStyleBackColor = False
    '
    'txFileName
    '
    Me.txFileName.AcceptsReturn = True
    Me.txFileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txFileName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
    Me.txFileName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem
    Me.txFileName.BackColor = System.Drawing.SystemColors.Window
    Me.txFileName.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txFileName.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txFileName.Location = New System.Drawing.Point(165, 20)
    Me.txFileName.MaxLength = 0
    Me.txFileName.Name = "txFileName"
    Me.txFileName.Size = New System.Drawing.Size(403, 25)
    Me.txFileName.TabIndex = 1
    '
    'cmdBrowse
    '
    Me.cmdBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdBrowse.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdBrowse.Location = New System.Drawing.Point(574, 19)
    Me.cmdBrowse.Name = "cmdBrowse"
    Me.cmdBrowse.Size = New System.Drawing.Size(76, 29)
    Me.cmdBrowse.TabIndex = 2
    Me.cmdBrowse.Text = "&Browse..."
    Me.cmdBrowse.UseVisualStyleBackColor = False
    '
    'fmResize
    '
    Me.fmResize.Controls.Add(Me.txPct)
    Me.fmResize.Controls.Add(Me.chkResize)
    Me.fmResize.Controls.Add(Me.txXres)
    Me.fmResize.Controls.Add(Me.txYres)
    Me.fmResize.Controls.Add(Me.lbPct)
    Me.fmResize.Controls.Add(Me.lbXres)
    Me.fmResize.Controls.Add(Me.lbYres)
    Me.fmResize.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.fmResize.Location = New System.Drawing.Point(28, 253)
    Me.fmResize.Name = "fmResize"
    Me.fmResize.Padding = New System.Windows.Forms.Padding(0)
    Me.fmResize.Size = New System.Drawing.Size(298, 156)
    Me.fmResize.TabIndex = 6
    Me.fmResize.TabStop = False
    Me.fmResize.Text = "Resize"
    '
    'txPct
    '
    Me.txPct.AcceptsReturn = True
    Me.txPct.BackColor = System.Drawing.SystemColors.Window
    Me.txPct.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txPct.Enabled = False
    Me.txPct.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txPct.Location = New System.Drawing.Point(213, 57)
    Me.txPct.MaxLength = 0
    Me.txPct.Name = "txPct"
    Me.txPct.Size = New System.Drawing.Size(64, 25)
    Me.txPct.TabIndex = 2
    Me.txPct.Text = "100%"
    '
    'txXres
    '
    Me.txXres.AcceptsReturn = True
    Me.txXres.BackColor = System.Drawing.SystemColors.Window
    Me.txXres.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txXres.Enabled = False
    Me.txXres.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txXres.Location = New System.Drawing.Point(213, 87)
    Me.txXres.MaxLength = 0
    Me.txXres.Name = "txXres"
    Me.txXres.Size = New System.Drawing.Size(64, 25)
    Me.txXres.TabIndex = 4
    Me.txXres.Text = "1600"
    '
    'txYres
    '
    Me.txYres.AcceptsReturn = True
    Me.txYres.BackColor = System.Drawing.SystemColors.Window
    Me.txYres.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txYres.Enabled = False
    Me.txYres.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.txYres.Location = New System.Drawing.Point(213, 117)
    Me.txYres.MaxLength = 0
    Me.txYres.Name = "txYres"
    Me.txYres.Size = New System.Drawing.Size(64, 25)
    Me.txYres.TabIndex = 6
    Me.txYres.Text = "1200"
    '
    'lbPct
    '
    Me.lbPct.Enabled = False
    Me.lbPct.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbPct.Location = New System.Drawing.Point(25, 60)
    Me.lbPct.Name = "lbPct"
    Me.lbPct.Size = New System.Drawing.Size(182, 21)
    Me.lbPct.TabIndex = 1
    Me.lbPct.Text = "&Percent Change:"
    '
    'lbXres
    '
    Me.lbXres.Enabled = False
    Me.lbXres.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbXres.Location = New System.Drawing.Point(25, 90)
    Me.lbXres.Name = "lbXres"
    Me.lbXres.Size = New System.Drawing.Size(182, 21)
    Me.lbXres.TabIndex = 3
    Me.lbXres.Text = "&Horizontal Resolution:"
    '
    'lbYres
    '
    Me.lbYres.Enabled = False
    Me.lbYres.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbYres.Location = New System.Drawing.Point(25, 120)
    Me.lbYres.Name = "lbYres"
    Me.lbYres.Size = New System.Drawing.Size(182, 21)
    Me.lbYres.TabIndex = 5
    Me.lbYres.Text = "&Vertical Resolution:"
    '
    'cmbFiletype
    '
    Me.cmbFiletype.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmbFiletype.BackColor = System.Drawing.SystemColors.Window
    Me.cmbFiletype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cmbFiletype.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmbFiletype.Location = New System.Drawing.Point(165, 60)
    Me.cmbFiletype.Name = "cmbFiletype"
    Me.cmbFiletype.Size = New System.Drawing.Size(403, 25)
    Me.cmbFiletype.TabIndex = 4
    '
    'cmdSave
    '
    Me.cmdSave.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdSave.Location = New System.Drawing.Point(378, 263)
    Me.cmdSave.Name = "cmdSave"
    Me.cmdSave.Size = New System.Drawing.Size(91, 31)
    Me.cmdSave.TabIndex = 7
    Me.cmdSave.Text = "&Save"
    Me.cmdSave.UseVisualStyleBackColor = False
    '
    'cmdCancel
    '
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.cmdCancel.Location = New System.Drawing.Point(508, 263)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(91, 31)
    Me.cmdCancel.TabIndex = 8
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'Label1
    '
    Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label1.Location = New System.Drawing.Point(25, 25)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(136, 21)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "Output &File Name:"
    '
    'label3
    '
    Me.label3.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.label3.Location = New System.Drawing.Point(25, 65)
    Me.label3.Name = "label3"
    Me.label3.Size = New System.Drawing.Size(131, 21)
    Me.label3.TabIndex = 3
    Me.label3.Text = "Output File &Type:"
    '
    'lbCompression1
    '
    Me.lbCompression1.AutoSize = True
    Me.lbCompression1.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbCompression1.Location = New System.Drawing.Point(23, 26)
    Me.lbCompression1.Name = "lbCompression1"
    Me.lbCompression1.Size = New System.Drawing.Size(89, 17)
    Me.lbCompression1.TabIndex = 0
    Me.lbCompression1.Text = "&JPG Quality:"
    '
    'lbCompression
    '
    Me.lbCompression.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbCompression.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.lbCompression.Location = New System.Drawing.Point(23, 62)
    Me.lbCompression.Name = "lbCompression"
    Me.lbCompression.Size = New System.Drawing.Size(534, 64)
    Me.lbCompression.TabIndex = 17
    Me.lbCompression.Text = "(see form load for caption)"
    '
    'nmQuality
    '
    Me.nmQuality.Location = New System.Drawing.Point(170, 24)
    Me.nmQuality.Name = "nmQuality"
    Me.nmQuality.Size = New System.Drawing.Size(72, 25)
    Me.nmQuality.TabIndex = 1
    Me.nmQuality.Value = New Decimal(New Integer() {95, 0, 0, 0})
    '
    'fmJpg
    '
    Me.fmJpg.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.fmJpg.Controls.Add(Me.chkExif)
    Me.fmJpg.Controls.Add(Me.lbCompression1)
    Me.fmJpg.Controls.Add(Me.nmQuality)
    Me.fmJpg.Controls.Add(Me.lbCompression)
    Me.fmJpg.Location = New System.Drawing.Point(28, 108)
    Me.fmJpg.Name = "fmJpg"
    Me.fmJpg.Size = New System.Drawing.Size(571, 129)
    Me.fmJpg.TabIndex = 5
    Me.fmJpg.TabStop = False
    Me.fmJpg.Text = "JPG Options"
    '
    'fmPng
    '
    Me.fmPng.Controls.Add(Me.chkPngIndexed)
    Me.fmPng.Controls.Add(Me.Label2)
    Me.fmPng.Controls.Add(Me.nmPngCompression)
    Me.fmPng.Location = New System.Drawing.Point(28, 446)
    Me.fmPng.Name = "fmPng"
    Me.fmPng.Size = New System.Drawing.Size(571, 114)
    Me.fmPng.TabIndex = 31
    Me.fmPng.TabStop = False
    Me.fmPng.Text = "PNG Options"
    '
    'chkPngIndexed
    '
    Me.chkPngIndexed.AutoSize = True
    Me.chkPngIndexed.Location = New System.Drawing.Point(39, 68)
    Me.chkPngIndexed.Name = "chkPngIndexed"
    Me.chkPngIndexed.Size = New System.Drawing.Size(80, 21)
    Me.chkPngIndexed.TabIndex = 8
    Me.chkPngIndexed.Text = "Indexed"
    Me.chkPngIndexed.UseVisualStyleBackColor = True
    '
    'frmSaveAs
    '
    Me.AcceptButton = Me.cmdSave
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(680, 585)
    Me.Controls.Add(Me.fmPng)
    Me.Controls.Add(Me.fmJpg)
    Me.Controls.Add(Me.txFileName)
    Me.Controls.Add(Me.cmdBrowse)
    Me.Controls.Add(Me.fmResize)
    Me.Controls.Add(Me.cmbFiletype)
    Me.Controls.Add(Me.cmdSave)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.label3)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 28)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmSaveAs"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Save As"
    CType(Me.nmPngCompression, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmResize.ResumeLayout(False)
    Me.fmResize.PerformLayout()
    CType(Me.nmQuality, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmJpg.ResumeLayout(False)
    Me.fmJpg.PerformLayout()
    Me.fmPng.ResumeLayout(False)
    Me.fmPng.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents nmQuality As System.Windows.Forms.NumericUpDown
  Friend WithEvents fmJpg As System.Windows.Forms.GroupBox
  Friend WithEvents fmPng As System.Windows.Forms.GroupBox
  Friend WithEvents chkPngIndexed As System.Windows.Forms.CheckBox
  Public WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents nmPngCompression As System.Windows.Forms.NumericUpDown
  Public WithEvents chkExif As System.Windows.Forms.CheckBox
#End Region
End Class