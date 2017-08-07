<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlTextControl
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctlTextControl))
    Me.chkTextBackfill = New System.Windows.Forms.CheckBox()
    Me.cmdTextBackcolor = New System.Windows.Forms.Button()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.chkTextMultiline = New System.Windows.Forms.CheckBox()
    Me.nmTextAngle = New System.Windows.Forms.NumericUpDown()
    Me.nmFontSize = New System.Windows.Forms.NumericUpDown()
    Me.cmbFonts = New System.Windows.Forms.ComboBox()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.cmdDescription = New System.Windows.Forms.Button()
    Me.cmdDate = New System.Windows.Forms.Button()
    Me.cmdUnderline = New System.Windows.Forms.Button()
    Me.cmdItalic = New System.Windows.Forms.Button()
    Me.cmdBold = New System.Windows.Forms.Button()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.cmdOK = New System.Windows.Forms.Button()
    Me.cmdTextColor = New System.Windows.Forms.Button()
    Me.txText = New System.Windows.Forms.TextBox()
    Me.Label5 = New System.Windows.Forms.Label()
    Me._Label1_0 = New System.Windows.Forms.Label()
    Me._lbColor_0 = New System.Windows.Forms.Label()
    Me.cmdAlignLeft = New System.Windows.Forms.Button()
    Me.cmdAlignRight = New System.Windows.Forms.Button()
    Me.cmdAlignCenter = New System.Windows.Forms.Button()
    CType(Me.nmTextAngle, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nmFontSize, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'chkTextBackfill
    '
    Me.chkTextBackfill.AutoSize = True
    Me.chkTextBackfill.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.chkTextBackfill.Location = New System.Drawing.Point(26, 278)
    Me.chkTextBackfill.Name = "chkTextBackfill"
    Me.chkTextBackfill.Size = New System.Drawing.Size(130, 21)
    Me.chkTextBackfill.TabIndex = 35
    Me.chkTextBackfill.Text = "Fill Background"
    Me.chkTextBackfill.UseVisualStyleBackColor = False
    '
    'cmdTextBackcolor
    '
    Me.cmdTextBackcolor.BackColor = System.Drawing.SystemColors.Highlight
    Me.cmdTextBackcolor.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdTextBackcolor.Location = New System.Drawing.Point(387, 187)
    Me.cmdTextBackcolor.Name = "cmdTextBackcolor"
    Me.cmdTextBackcolor.Size = New System.Drawing.Size(31, 33)
    Me.cmdTextBackcolor.TabIndex = 30
    Me.cmdTextBackcolor.UseVisualStyleBackColor = False
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label6.Location = New System.Drawing.Point(424, 193)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(86, 17)
    Me.Label6.TabIndex = 29
    Me.Label6.Text = "Back&ground"
    '
    'chkTextMultiline
    '
    Me.chkTextMultiline.AutoSize = True
    Me.chkTextMultiline.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.chkTextMultiline.Location = New System.Drawing.Point(26, 307)
    Me.chkTextMultiline.Name = "chkTextMultiline"
    Me.chkTextMultiline.Size = New System.Drawing.Size(155, 21)
    Me.chkTextMultiline.TabIndex = 36
    Me.chkTextMultiline.Text = "Allow Multiple Lines"
    Me.chkTextMultiline.UseVisualStyleBackColor = True
    '
    'nmTextAngle
    '
    Me.nmTextAngle.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.nmTextAngle.Location = New System.Drawing.Point(170, 241)
    Me.nmTextAngle.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
    Me.nmTextAngle.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
    Me.nmTextAngle.Name = "nmTextAngle"
    Me.nmTextAngle.Size = New System.Drawing.Size(75, 25)
    Me.nmTextAngle.TabIndex = 34
    '
    'nmFontSize
    '
    Me.nmFontSize.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.nmFontSize.Location = New System.Drawing.Point(170, 204)
    Me.nmFontSize.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
    Me.nmFontSize.Minimum = New Decimal(New Integer() {4, 0, 0, 0})
    Me.nmFontSize.Name = "nmFontSize"
    Me.nmFontSize.Size = New System.Drawing.Size(75, 25)
    Me.nmFontSize.TabIndex = 32
    Me.nmFontSize.Value = New Decimal(New Integer() {4, 0, 0, 0})
    '
    'cmbFonts
    '
    Me.cmbFonts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cmbFonts.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmbFonts.FormattingEnabled = True
    Me.cmbFonts.Location = New System.Drawing.Point(19, 152)
    Me.cmbFonts.Name = "cmbFonts"
    Me.cmbFonts.Size = New System.Drawing.Size(223, 25)
    Me.cmbFonts.TabIndex = 23
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label3.Location = New System.Drawing.Point(18, 206)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(134, 17)
    Me.Label3.TabIndex = 31
    Me.Label3.Text = "Text Height (pixels):"
    '
    'cmdDescription
    '
    Me.cmdDescription.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdDescription.Location = New System.Drawing.Point(283, 254)
    Me.cmdDescription.Name = "cmdDescription"
    Me.cmdDescription.Size = New System.Drawing.Size(101, 33)
    Me.cmdDescription.TabIndex = 37
    Me.cmdDescription.Text = "&Description"
    Me.cmdDescription.UseVisualStyleBackColor = False
    '
    'cmdDate
    '
    Me.cmdDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdDate.Location = New System.Drawing.Point(283, 295)
    Me.cmdDate.Name = "cmdDate"
    Me.cmdDate.Size = New System.Drawing.Size(101, 33)
    Me.cmdDate.TabIndex = 38
    Me.cmdDate.Text = "&Date"
    Me.cmdDate.UseVisualStyleBackColor = False
    '
    'cmdUnderline
    '
    Me.cmdUnderline.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdUnderline.Location = New System.Drawing.Point(341, 149)
    Me.cmdUnderline.Name = "cmdUnderline"
    Me.cmdUnderline.Size = New System.Drawing.Size(31, 33)
    Me.cmdUnderline.TabIndex = 26
    Me.cmdUnderline.Text = "U"
    Me.cmdUnderline.UseVisualStyleBackColor = False
    '
    'cmdItalic
    '
    Me.cmdItalic.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdItalic.Location = New System.Drawing.Point(304, 149)
    Me.cmdItalic.Name = "cmdItalic"
    Me.cmdItalic.Size = New System.Drawing.Size(31, 33)
    Me.cmdItalic.TabIndex = 25
    Me.cmdItalic.Text = "i"
    Me.cmdItalic.UseVisualStyleBackColor = False
    '
    'cmdBold
    '
    Me.cmdBold.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdBold.Location = New System.Drawing.Point(267, 149)
    Me.cmdBold.Name = "cmdBold"
    Me.cmdBold.Size = New System.Drawing.Size(31, 33)
    Me.cmdBold.TabIndex = 24
    Me.cmdBold.Text = "B"
    Me.cmdBold.UseVisualStyleBackColor = False
    '
    'cmdCancel
    '
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdCancel.Location = New System.Drawing.Point(401, 295)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(82, 33)
    Me.cmdCancel.TabIndex = 40
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'cmdOK
    '
    Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdOK.Location = New System.Drawing.Point(401, 254)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(82, 33)
    Me.cmdOK.TabIndex = 39
    Me.cmdOK.Text = "&OK"
    Me.cmdOK.UseVisualStyleBackColor = False
    '
    'cmdTextColor
    '
    Me.cmdTextColor.BackColor = System.Drawing.SystemColors.Highlight
    Me.cmdTextColor.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdTextColor.Location = New System.Drawing.Point(387, 148)
    Me.cmdTextColor.Name = "cmdTextColor"
    Me.cmdTextColor.Size = New System.Drawing.Size(31, 33)
    Me.cmdTextColor.TabIndex = 28
    Me.cmdTextColor.UseVisualStyleBackColor = False
    '
    'txText
    '
    Me.txText.AcceptsReturn = True
    Me.txText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txText.BackColor = System.Drawing.SystemColors.Window
    Me.txText.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txText.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txText.Location = New System.Drawing.Point(19, 38)
    Me.txText.MaxLength = 0
    Me.txText.Multiline = True
    Me.txText.Name = "txText"
    Me.txText.Size = New System.Drawing.Size(463, 92)
    Me.txText.TabIndex = 22
    Me.txText.Text = "Text to be added to the photo"
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label5.Location = New System.Drawing.Point(18, 243)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(77, 17)
    Me.Label5.TabIndex = 33
    Me.Label5.Text = "Text &Angle:"
    '
    '_Label1_0
    '
    Me._Label1_0.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me._Label1_0.Location = New System.Drawing.Point(16, 13)
    Me._Label1_0.Name = "_Label1_0"
    Me._Label1_0.Size = New System.Drawing.Size(229, 22)
    Me._Label1_0.TabIndex = 21
    Me._Label1_0.Text = "&Text:"
    '
    '_lbColor_0
    '
    Me._lbColor_0.AutoSize = True
    Me._lbColor_0.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me._lbColor_0.Location = New System.Drawing.Point(424, 157)
    Me._lbColor_0.Name = "_lbColor_0"
    Me._lbColor_0.Size = New System.Drawing.Size(73, 17)
    Me._lbColor_0.TabIndex = 27
    Me._lbColor_0.Text = "Text &Color"
    '
    'cmdAlignLeft
    '
    Me.cmdAlignLeft.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdAlignLeft.Image = CType(resources.GetObject("cmdAlignLeft.Image"), System.Drawing.Image)
    Me.cmdAlignLeft.Location = New System.Drawing.Point(267, 193)
    Me.cmdAlignLeft.Name = "cmdAlignLeft"
    Me.cmdAlignLeft.Size = New System.Drawing.Size(31, 33)
    Me.cmdAlignLeft.TabIndex = 41
    Me.cmdAlignLeft.UseVisualStyleBackColor = False
    '
    'cmdAlignRight
    '
    Me.cmdAlignRight.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdAlignRight.Image = CType(resources.GetObject("cmdAlignRight.Image"), System.Drawing.Image)
    Me.cmdAlignRight.Location = New System.Drawing.Point(341, 193)
    Me.cmdAlignRight.Name = "cmdAlignRight"
    Me.cmdAlignRight.Size = New System.Drawing.Size(31, 33)
    Me.cmdAlignRight.TabIndex = 43
    Me.cmdAlignRight.UseVisualStyleBackColor = False
    '
    'cmdAlignCenter
    '
    Me.cmdAlignCenter.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdAlignCenter.Image = CType(resources.GetObject("cmdAlignCenter.Image"), System.Drawing.Image)
    Me.cmdAlignCenter.Location = New System.Drawing.Point(304, 193)
    Me.cmdAlignCenter.Name = "cmdAlignCenter"
    Me.cmdAlignCenter.Size = New System.Drawing.Size(31, 33)
    Me.cmdAlignCenter.TabIndex = 42
    Me.cmdAlignCenter.UseVisualStyleBackColor = False
    '
    'ctlTextControl
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Controls.Add(Me.cmdAlignRight)
    Me.Controls.Add(Me.cmdAlignCenter)
    Me.Controls.Add(Me.cmdAlignLeft)
    Me.Controls.Add(Me.chkTextBackfill)
    Me.Controls.Add(Me.cmdTextBackcolor)
    Me.Controls.Add(Me.Label6)
    Me.Controls.Add(Me.chkTextMultiline)
    Me.Controls.Add(Me.nmTextAngle)
    Me.Controls.Add(Me.nmFontSize)
    Me.Controls.Add(Me.cmbFonts)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.cmdDescription)
    Me.Controls.Add(Me.cmdDate)
    Me.Controls.Add(Me.cmdUnderline)
    Me.Controls.Add(Me.cmdItalic)
    Me.Controls.Add(Me.cmdBold)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.cmdTextColor)
    Me.Controls.Add(Me.txText)
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me._Label1_0)
    Me.Controls.Add(Me._lbColor_0)
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Name = "ctlTextControl"
    Me.Size = New System.Drawing.Size(532, 351)
    CType(Me.nmTextAngle, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nmFontSize, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents chkTextBackfill As System.Windows.Forms.CheckBox
  Public WithEvents cmdTextBackcolor As System.Windows.Forms.Button
  Public WithEvents Label6 As System.Windows.Forms.Label
  Friend WithEvents chkTextMultiline As System.Windows.Forms.CheckBox
  Friend WithEvents nmTextAngle As System.Windows.Forms.NumericUpDown
  Friend WithEvents nmFontSize As System.Windows.Forms.NumericUpDown
  Friend WithEvents cmbFonts As System.Windows.Forms.ComboBox
  Public WithEvents Label3 As System.Windows.Forms.Label
  Public WithEvents cmdDescription As System.Windows.Forms.Button
  Public WithEvents cmdDate As System.Windows.Forms.Button
  Public WithEvents cmdUnderline As System.Windows.Forms.Button
  Public WithEvents cmdItalic As System.Windows.Forms.Button
  Public WithEvents cmdBold As System.Windows.Forms.Button
  Public WithEvents cmdCancel As System.Windows.Forms.Button
  Public WithEvents cmdOK As System.Windows.Forms.Button
  Public WithEvents cmdTextColor As System.Windows.Forms.Button
  Private WithEvents txText As System.Windows.Forms.TextBox
  Public WithEvents Label5 As System.Windows.Forms.Label
  Public WithEvents _Label1_0 As System.Windows.Forms.Label
  Public WithEvents _lbColor_0 As System.Windows.Forms.Label
  Public WithEvents cmdAlignLeft As System.Windows.Forms.Button
  Public WithEvents cmdAlignRight As System.Windows.Forms.Button
  Public WithEvents cmdAlignCenter As System.Windows.Forms.Button

End Class
