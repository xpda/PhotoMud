<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlFeatherControl
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
    Me.nmRange = New System.Windows.Forms.NumericUpDown()
    Me.trkRange = New System.Windows.Forms.TrackBar()
    Me.Label4 = New System.Windows.Forms.Label()
    CType(Me.nmRange, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.trkRange, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'nmRange
    '
    Me.nmRange.Location = New System.Drawing.Point(289, 12)
    Me.nmRange.Maximum = New Decimal(New Integer() {200, 0, 0, 0})
    Me.nmRange.Name = "nmRange"
    Me.nmRange.Size = New System.Drawing.Size(61, 25)
    Me.nmRange.TabIndex = 10
    '
    'trkRange
    '
    Me.trkRange.AutoSize = False
    Me.trkRange.Location = New System.Drawing.Point(152, 14)
    Me.trkRange.Maximum = 200
    Me.trkRange.Name = "trkRange"
    Me.trkRange.Size = New System.Drawing.Size(133, 27)
    Me.trkRange.TabIndex = 9
    Me.trkRange.TickStyle = System.Windows.Forms.TickStyle.None
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!)
    Me.Label4.Location = New System.Drawing.Point(16, 14)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(128, 17)
    Me.Label4.TabIndex = 8
    Me.Label4.Text = "Feathering &Range:"
    '
    'ctlFeatherControl
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Controls.Add(Me.nmRange)
    Me.Controls.Add(Me.trkRange)
    Me.Controls.Add(Me.Label4)
    Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Name = "ctlFeatherControl"
    Me.Size = New System.Drawing.Size(370, 50)
    CType(Me.nmRange, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.trkRange, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

End Sub
  Friend WithEvents nmRange As System.Windows.Forms.NumericUpDown
    Friend WithEvents trkRange As System.Windows.Forms.TrackBar
    Friend WithEvents Label4 As System.Windows.Forms.Label

End Class
