<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConcatenate
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConcatenate))
    Me.cmdHelp = New System.Windows.Forms.Button()
    Me.cmdOK = New System.Windows.Forms.Button()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.group3 = New System.Windows.Forms.GroupBox()
    Me.op3bottomLeft = New System.Windows.Forms.RadioButton()
    Me.op3bottomRight = New System.Windows.Forms.RadioButton()
    Me.op3topRight = New System.Windows.Forms.RadioButton()
    Me.op3topLeft = New System.Windows.Forms.RadioButton()
    Me.group2 = New System.Windows.Forms.GroupBox()
    Me.op2bottomRight = New System.Windows.Forms.RadioButton()
    Me.op2bottomLeft = New System.Windows.Forms.RadioButton()
    Me.op2topRight = New System.Windows.Forms.RadioButton()
    Me.op2topLeft = New System.Windows.Forms.RadioButton()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.pView1 = New PhotoMud.pViewer()
    Me.pView2 = New PhotoMud.pViewer()
    Me.pView3 = New PhotoMud.pViewer()
    Me.group3.SuspendLayout
    Me.group2.SuspendLayout
    CType(Me.pView1,System.ComponentModel.ISupportInitialize).BeginInit
    CType(Me.pView2,System.ComponentModel.ISupportInitialize).BeginInit
    CType(Me.pView3,System.ComponentModel.ISupportInitialize).BeginInit
    Me.SuspendLayout
    '
    'cmdHelp
    '
    Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9!)
    Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"),System.Drawing.Image)
    Me.cmdHelp.Location = New System.Drawing.Point(283, 645)
    Me.cmdHelp.Name = "cmdHelp"
    Me.cmdHelp.Size = New System.Drawing.Size(41, 41)
    Me.cmdHelp.TabIndex = 33
    Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
    Me.cmdHelp.UseVisualStyleBackColor = false
    '
    'cmdOK
    '
    Me.cmdOK.Font = New System.Drawing.Font("Arial", 9!)
    Me.cmdOK.Location = New System.Drawing.Point(78, 651)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(96, 35)
    Me.cmdOK.TabIndex = 31
    Me.cmdOK.Text = "&OK"
    Me.cmdOK.UseVisualStyleBackColor = false
    '
    'cmdCancel
    '
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9!)
    Me.cmdCancel.Location = New System.Drawing.Point(180, 651)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(96, 35)
    Me.cmdCancel.TabIndex = 32
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = false
    '
    'group3
    '
    Me.group3.Controls.Add(Me.pView3)
    Me.group3.Controls.Add(Me.op3bottomLeft)
    Me.group3.Controls.Add(Me.op3bottomRight)
    Me.group3.Controls.Add(Me.op3topRight)
    Me.group3.Controls.Add(Me.op3topLeft)
    Me.group3.Location = New System.Drawing.Point(33, 12)
    Me.group3.Name = "group3"
    Me.group3.Size = New System.Drawing.Size(356, 268)
    Me.group3.TabIndex = 47
    Me.group3.TabStop = false
    '
    'op3bottomLeft
    '
    Me.op3bottomLeft.AutoSize = true
    Me.op3bottomLeft.Location = New System.Drawing.Point(36, 228)
    Me.op3bottomLeft.Name = "op3bottomLeft"
    Me.op3bottomLeft.Size = New System.Drawing.Size(17, 16)
    Me.op3bottomLeft.TabIndex = 47
    Me.op3bottomLeft.TabStop = true
    Me.op3bottomLeft.UseVisualStyleBackColor = true
    '
    'op3bottomRight
    '
    Me.op3bottomRight.AutoSize = true
    Me.op3bottomRight.Location = New System.Drawing.Point(303, 228)
    Me.op3bottomRight.Name = "op3bottomRight"
    Me.op3bottomRight.Size = New System.Drawing.Size(17, 16)
    Me.op3bottomRight.TabIndex = 46
    Me.op3bottomRight.TabStop = true
    Me.op3bottomRight.UseVisualStyleBackColor = true
    '
    'op3topRight
    '
    Me.op3topRight.AutoSize = true
    Me.op3topRight.Location = New System.Drawing.Point(303, 35)
    Me.op3topRight.Name = "op3topRight"
    Me.op3topRight.Size = New System.Drawing.Size(17, 16)
    Me.op3topRight.TabIndex = 45
    Me.op3topRight.TabStop = true
    Me.op3topRight.UseVisualStyleBackColor = true
    '
    'op3topLeft
    '
    Me.op3topLeft.AutoSize = true
    Me.op3topLeft.Location = New System.Drawing.Point(36, 35)
    Me.op3topLeft.Name = "op3topLeft"
    Me.op3topLeft.Size = New System.Drawing.Size(17, 16)
    Me.op3topLeft.TabIndex = 44
    Me.op3topLeft.TabStop = true
    Me.op3topLeft.UseVisualStyleBackColor = true
    '
    'group2
    '
    Me.group2.Controls.Add(Me.pView2)
    Me.group2.Controls.Add(Me.op2bottomRight)
    Me.group2.Controls.Add(Me.op2bottomLeft)
    Me.group2.Controls.Add(Me.op2topRight)
    Me.group2.Controls.Add(Me.op2topLeft)
    Me.group2.Location = New System.Drawing.Point(33, 295)
    Me.group2.Name = "group2"
    Me.group2.Size = New System.Drawing.Size(356, 268)
    Me.group2.TabIndex = 48
    Me.group2.TabStop = false
    '
    'op2bottomRight
    '
    Me.op2bottomRight.AutoSize = true
    Me.op2bottomRight.Location = New System.Drawing.Point(303, 230)
    Me.op2bottomRight.Name = "op2bottomRight"
    Me.op2bottomRight.Size = New System.Drawing.Size(17, 16)
    Me.op2bottomRight.TabIndex = 51
    Me.op2bottomRight.TabStop = true
    Me.op2bottomRight.UseVisualStyleBackColor = true
    '
    'op2bottomLeft
    '
    Me.op2bottomLeft.AutoSize = true
    Me.op2bottomLeft.Location = New System.Drawing.Point(36, 230)
    Me.op2bottomLeft.Name = "op2bottomLeft"
    Me.op2bottomLeft.Size = New System.Drawing.Size(17, 16)
    Me.op2bottomLeft.TabIndex = 50
    Me.op2bottomLeft.TabStop = true
    Me.op2bottomLeft.UseVisualStyleBackColor = true
    '
    'op2topRight
    '
    Me.op2topRight.AutoSize = true
    Me.op2topRight.Location = New System.Drawing.Point(303, 37)
    Me.op2topRight.Name = "op2topRight"
    Me.op2topRight.Size = New System.Drawing.Size(17, 16)
    Me.op2topRight.TabIndex = 49
    Me.op2topRight.TabStop = true
    Me.op2topRight.UseVisualStyleBackColor = true
    '
    'op2topLeft
    '
    Me.op2topLeft.AutoSize = true
    Me.op2topLeft.Location = New System.Drawing.Point(36, 37)
    Me.op2topLeft.Name = "op2topLeft"
    Me.op2topLeft.Size = New System.Drawing.Size(17, 16)
    Me.op2topLeft.TabIndex = 48
    Me.op2topLeft.TabStop = true
    Me.op2topLeft.UseVisualStyleBackColor = true
    '
    'Label1
    '
    Me.Label1.Location = New System.Drawing.Point(30, 581)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(375, 61)
    Me.Label1.TabIndex = 49
    Me.Label1.Text = "Select a corner in each photo to be matched up. Keep in mind that not all corner "& _ 
    "combinations are valid."
    '
    'pView1
    '
    Me.pView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
    Me.pView1.BackColor = System.Drawing.SystemColors.ControlDarkDark
    Me.pView1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low
    Me.pView1.Location = New System.Drawing.Point(427, 0)
    Me.pView1.Name = "pView1"
    Me.pView1.Size = New System.Drawing.Size(561, 698)
    Me.pView1.TabIndex = 50
    Me.pView1.TabStop = false
    Me.pView1.ZoomFactor = 1R
    '
    'pView2
    '
    Me.pView2.BackColor = System.Drawing.SystemColors.ControlDarkDark
    Me.pView2.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low
    Me.pView2.Location = New System.Drawing.Point(36, 56)
    Me.pView2.Name = "pView2"
    Me.pView2.Size = New System.Drawing.Size(281, 168)
    Me.pView2.TabIndex = 52
    Me.pView2.TabStop = false
    Me.pView2.ZoomFactor = 1R
    '
    'pView3
    '
    Me.pView3.BackColor = System.Drawing.SystemColors.ControlDarkDark
    Me.pView3.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low
    Me.pView3.Location = New System.Drawing.Point(36, 54)
    Me.pView3.Name = "pView3"
    Me.pView3.Size = New System.Drawing.Size(281, 168)
    Me.pView3.TabIndex = 48
    Me.pView3.TabStop = false
    Me.pView3.ZoomFactor = 1R
    '
    'frmConcatenate
    '
    Me.AcceptButton = Me.cmdOK
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(994, 698)
    Me.Controls.Add(Me.pView1)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.group2)
    Me.Controls.Add(Me.group3)
    Me.Controls.Add(Me.cmdHelp)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.cmdCancel)
    Me.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
    Me.MinimizeBox = false
    Me.Name = "frmConcatenate"
    Me.ShowInTaskbar = false
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Concatenate Photos"
    Me.group3.ResumeLayout(false)
    Me.group3.PerformLayout
    Me.group2.ResumeLayout(false)
    Me.group2.PerformLayout
    CType(Me.pView1,System.ComponentModel.ISupportInitialize).EndInit
    CType(Me.pView2,System.ComponentModel.ISupportInitialize).EndInit
    CType(Me.pView3,System.ComponentModel.ISupportInitialize).EndInit
    Me.ResumeLayout(false)

End Sub
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents cmdHelp As System.Windows.Forms.Button
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents group3 As System.Windows.Forms.GroupBox
    Friend WithEvents op3bottomLeft As System.Windows.Forms.RadioButton
    Friend WithEvents op3bottomRight As System.Windows.Forms.RadioButton
    Friend WithEvents op3topRight As System.Windows.Forms.RadioButton
    Friend WithEvents op3topLeft As System.Windows.Forms.RadioButton
    Friend WithEvents group2 As System.Windows.Forms.GroupBox
    Friend WithEvents op2bottomRight As System.Windows.Forms.RadioButton
    Friend WithEvents op2bottomLeft As System.Windows.Forms.RadioButton
    Friend WithEvents op2topRight As System.Windows.Forms.RadioButton
    Friend WithEvents op2topLeft As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pView3 As PhotoMud.pViewer
    Friend WithEvents pView2 As PhotoMud.pViewer
    Friend WithEvents pView1 As PhotoMud.pViewer
End Class
