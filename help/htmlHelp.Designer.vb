<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class Form1
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
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents Text1 As System.Windows.Forms.TextBox
	Public WithEvents Command1 As System.Windows.Forms.Button
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
Me.components = New System.ComponentModel.Container
Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
Me.Text1 = New System.Windows.Forms.TextBox
Me.Command1 = New System.Windows.Forms.Button
Me.SuspendLayout()
'
'Text1
'
Me.Text1.AcceptsReturn = True
Me.Text1.BackColor = System.Drawing.SystemColors.Window
Me.Text1.Cursor = System.Windows.Forms.Cursors.IBeam
Me.Text1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.Text1.ForeColor = System.Drawing.SystemColors.WindowText
Me.Text1.Location = New System.Drawing.Point(379, 65)
Me.Text1.MaxLength = 0
Me.Text1.Name = "Text1"
Me.Text1.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.Text1.Size = New System.Drawing.Size(226, 23)
Me.Text1.TabIndex = 1
Me.Text1.Text = "Photomud"
'
'Command1
'
Me.Command1.BackColor = System.Drawing.SystemColors.Control
Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
Me.Command1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
Me.Command1.Location = New System.Drawing.Point(70, 55)
Me.Command1.Name = "Command1"
Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.Command1.Size = New System.Drawing.Size(151, 61)
Me.Command1.TabIndex = 0
Me.Command1.Text = "rename files"
Me.Command1.UseVisualStyleBackColor = False
'
'Form1
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.BackColor = System.Drawing.SystemColors.Control
Me.ClientSize = New System.Drawing.Size(712, 172)
Me.Controls.Add(Me.Text1)
Me.Controls.Add(Me.Command1)
Me.Cursor = System.Windows.Forms.Cursors.Default
Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.Location = New System.Drawing.Point(4, 31)
Me.Name = "Form1"
Me.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.Text = "Form1"
Me.ResumeLayout(False)
Me.PerformLayout()

End Sub
#End Region 
End Class