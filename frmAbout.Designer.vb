<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmAbout
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
  Public WithEvents lbTitle As Label
  Public WithEvents lbUrl As Label
  Public WithEvents lbVersion As Label
  Public WithEvents Frame1 As GroupBox
  Public WithEvents about As Button
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
Me.components = New System.ComponentModel.Container
Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
Me.Frame1 = New System.Windows.Forms.GroupBox
Me.lbTitle = New System.Windows.Forms.Label
Me.lbCompany = New System.Windows.Forms.Label
Me.lbUrl = New System.Windows.Forms.Label
Me.lbVersion = New System.Windows.Forms.Label
Me.about = New System.Windows.Forms.Button
Me.Frame1.SuspendLayout()
Me.SuspendLayout()
'
'Frame1
'
Me.Frame1.Controls.Add(Me.lbTitle)
Me.Frame1.Controls.Add(Me.lbCompany)
Me.Frame1.Controls.Add(Me.lbUrl)
Me.Frame1.Controls.Add(Me.lbVersion)
Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.Frame1.Location = New System.Drawing.Point(10, 15)
Me.Frame1.Name = "Frame1"
Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
Me.Frame1.Size = New System.Drawing.Size(326, 206)
Me.Frame1.TabIndex = 1
Me.Frame1.TabStop = False
'
'lbTitle
'
Me.lbTitle.AutoSize = True
Me.lbTitle.Font = New System.Drawing.Font("Arial", 13.8!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.lbTitle.Location = New System.Drawing.Point(82, 25)
Me.lbTitle.Name = "lbTitle"
Me.lbTitle.Size = New System.Drawing.Size(135, 28)
Me.lbTitle.TabIndex = 7
Me.lbTitle.Text = "Photo Mud"
Me.lbTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'lbCompany
'
Me.lbCompany.AutoSize = True
Me.lbCompany.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.lbCompany.Location = New System.Drawing.Point(53, 165)
Me.lbCompany.Name = "lbCompany"
Me.lbCompany.Size = New System.Drawing.Size(31, 17)
Me.lbCompany.TabIndex = 5
Me.lbCompany.Text = " Co"
Me.lbCompany.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'lbUrl
'
Me.lbUrl.AutoSize = True
Me.lbUrl.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.lbUrl.ForeColor = System.Drawing.SystemColors.Highlight
Me.lbUrl.Location = New System.Drawing.Point(32, 110)
Me.lbUrl.Name = "lbUrl"
Me.lbUrl.Size = New System.Drawing.Size(37, 17)
Me.lbUrl.TabIndex = 4
Me.lbUrl.Text = "URL"
Me.lbUrl.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'lbVersion
'
Me.lbVersion.AutoSize = True
Me.lbVersion.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.lbVersion.Location = New System.Drawing.Point(116, 55)
Me.lbVersion.Name = "lbVersion"
Me.lbVersion.Size = New System.Drawing.Size(80, 17)
Me.lbVersion.TabIndex = 3
Me.lbVersion.Text = "Version 1.0"
Me.lbVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'about
'
Me.about.DialogResult = System.Windows.Forms.DialogResult.Cancel
Me.about.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.about.Location = New System.Drawing.Point(355, 175)
Me.about.Name = "about"
Me.about.Size = New System.Drawing.Size(91, 31)
Me.about.TabIndex = 0
Me.about.Text = "OK"
Me.about.UseVisualStyleBackColor = False
'
'frmAbout
'
Me.AcceptButton = Me.about
Me.CancelButton = Me.about
Me.ClientSize = New System.Drawing.Size(458, 241)
Me.Controls.Add(Me.Frame1)
Me.Controls.Add(Me.about)
Me.Cursor = System.Windows.Forms.Cursors.Default
Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
Me.Location = New System.Drawing.Point(3, 22)
Me.MaximizeBox = False
Me.MinimizeBox = False
Me.Name = "frmAbout"
Me.ShowInTaskbar = False
Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
Me.Text = "About"
Me.Frame1.ResumeLayout(False)
Me.Frame1.PerformLayout()
Me.ResumeLayout(False)

End Sub
  Public WithEvents lbCompany As System.Windows.Forms.Label
#End Region
End Class