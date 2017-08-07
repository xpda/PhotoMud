<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlColorTolerance
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
    Me.nmTolerance = New System.Windows.Forms.NumericUpDown()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.cmdOk = New System.Windows.Forms.Button()
    Me.trkTolerance = New System.Windows.Forms.TrackBar()
    Me.chkDefault = New System.Windows.Forms.CheckBox()
    Me.Label1 = New System.Windows.Forms.Label()
    CType(Me.nmTolerance,System.ComponentModel.ISupportInitialize).BeginInit
    CType(Me.trkTolerance,System.ComponentModel.ISupportInitialize).BeginInit
    Me.SuspendLayout
    '
    'nmTolerance
    '
    Me.nmTolerance.Location = New System.Drawing.Point(342, 16)
    Me.nmTolerance.Name = "nmTolerance"
    Me.nmTolerance.Size = New System.Drawing.Size(58, 25)
    Me.nmTolerance.TabIndex = 21
    '
    'cmdCancel
    '
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdCancel.Location = New System.Drawing.Point(286, 58)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(77, 30)
    Me.cmdCancel.TabIndex = 27
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'cmdOk
    '
    Me.cmdOk.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdOk.Location = New System.Drawing.Point(188, 58)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(77, 30)
    Me.cmdOk.TabIndex = 26
    Me.cmdOk.Text = "&OK"
    Me.cmdOk.UseVisualStyleBackColor = False
    '
    'trkTolerance
    '
    Me.trkTolerance.AutoSize = False
    Me.trkTolerance.Location = New System.Drawing.Point(183, 18)
    Me.trkTolerance.Maximum = 100
    Me.trkTolerance.Name = "trkTolerance"
    Me.trkTolerance.Size = New System.Drawing.Size(153, 27)
    Me.trkTolerance.TabIndex = 20
    Me.trkTolerance.TickStyle = System.Windows.Forms.TickStyle.None
    '
    'chkDefault
    '
    Me.chkDefault.AutoSize = true
    Me.chkDefault.Font = New System.Drawing.Font("Arial", 9!)
    Me.chkDefault.Location = New System.Drawing.Point(14, 60)
    Me.chkDefault.Name = "chkDefault"
    Me.chkDefault.Size = New System.Drawing.Size(133, 21)
    Me.chkDefault.TabIndex = 25
    Me.chkDefault.Text = "&Save as Default"
    Me.chkDefault.UseVisualStyleBackColor = true
    '
    'Label1
    '
    Me.Label1.AutoSize = true
    Me.Label1.Font = New System.Drawing.Font("Arial", 9!)
    Me.Label1.Location = New System.Drawing.Point(11, 18)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(165, 17)
    Me.Label1.TabIndex = 19
    Me.Label1.Text = "&Color Tolerance (0-100):"
    '
    'ctlColorTolerance
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(8!, 17!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Controls.Add(Me.nmTolerance)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.cmdOk)
    Me.Controls.Add(Me.trkTolerance)
    Me.Controls.Add(Me.chkDefault)
    Me.Controls.Add(Me.Label1)
    Me.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
    Me.Name = "ctlColorTolerance"
    Me.Size = New System.Drawing.Size(415, 100)
    CType(Me.nmTolerance,System.ComponentModel.ISupportInitialize).EndInit
    CType(Me.trkTolerance,System.ComponentModel.ISupportInitialize).EndInit
    Me.ResumeLayout(false)
    Me.PerformLayout

End Sub
  Friend WithEvents nmTolerance As System.Windows.Forms.NumericUpDown
  Public WithEvents cmdCancel As System.Windows.Forms.Button
  Friend WithEvents trkTolerance As System.Windows.Forms.TrackBar
    Friend WithEvents chkDefault As System.Windows.Forms.CheckBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents cmdOk As System.Windows.Forms.Button

End Class
