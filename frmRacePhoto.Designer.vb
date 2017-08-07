<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRacePhoto
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRacePhoto))
    Me.cmdBack = New System.Windows.Forms.Button()
    Me.Label2w = New System.Windows.Forms.Label()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.chkLink = New System.Windows.Forms.CheckBox()
    Me.lbPicPath = New System.Windows.Forms.Label()
    Me.cmdSaveAll = New System.Windows.Forms.Button()
    Me.cmdCrop = New System.Windows.Forms.Button()
    Me.cmdNext = New System.Windows.Forms.Button()
    Me.cmdImageUpdate = New System.Windows.Forms.Button()
    Me.txName = New System.Windows.Forms.TextBox()
    Me.txRaceNumber = New System.Windows.Forms.TextBox()
    Me.txDateAdded = New System.Windows.Forms.TextBox()
    Me.txOriginalPath = New System.Windows.Forms.TextBox()
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdDelete = New System.Windows.Forms.Button()
    Me.cmdSaveData = New System.Windows.Forms.Button()
    Me.txDate = New System.Windows.Forms.TextBox()
    Me.txFileName = New System.Windows.Forms.TextBox()
    Me.lbOriginalPath = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.lbDate2 = New System.Windows.Forms.Label()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.LbFname = New System.Windows.Forms.Label()
    Me.pView = New PhotoMud.pViewer()
    Me.GroupBox1.SuspendLayout()
    CType(Me.pView, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'cmdBack
    '
    Me.cmdBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdBack.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdBack.Image = CType(resources.GetObject("cmdBack.Image"), System.Drawing.Image)
    Me.cmdBack.ImageAlign = System.Drawing.ContentAlignment.TopCenter
    Me.cmdBack.Location = New System.Drawing.Point(32, 461)
    Me.cmdBack.Name = "cmdBack"
    Me.cmdBack.Size = New System.Drawing.Size(57, 46)
    Me.cmdBack.TabIndex = 98
    Me.cmdBack.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.cmdBack.UseVisualStyleBackColor = False
    '
    'Label2w
    '
    Me.Label2w.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label2w.AutoSize = True
    Me.Label2w.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label2w.Location = New System.Drawing.Point(538, 850)
    Me.Label2w.Name = "Label2w"
    Me.Label2w.Size = New System.Drawing.Size(276, 17)
    Me.Label2w.TabIndex = 93
    Me.Label2w.Text = "Press F2 to reset, F3 for previous values."
    Me.Label2w.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdClose.Location = New System.Drawing.Point(415, 521)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(92, 46)
    Me.cmdClose.TabIndex = 105
    Me.cmdClose.Text = "&Close"
    Me.cmdClose.UseVisualStyleBackColor = False
    '
    'chkLink
    '
    Me.chkLink.AutoSize = True
    Me.chkLink.Location = New System.Drawing.Point(21, 341)
    Me.chkLink.Name = "chkLink"
    Me.chkLink.Size = New System.Drawing.Size(177, 21)
    Me.chkLink.TabIndex = 48
    Me.chkLink.Text = "Lin&k to Previous Image"
    Me.chkLink.UseVisualStyleBackColor = True
    '
    'lbPicPath
    '
    Me.lbPicPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbPicPath.Location = New System.Drawing.Point(538, 810)
    Me.lbPicPath.Name = "lbPicPath"
    Me.lbPicPath.Size = New System.Drawing.Size(508, 32)
    Me.lbPicPath.TabIndex = 94
    Me.lbPicPath.Text = "Labelg"
    '
    'cmdSaveAll
    '
    Me.cmdSaveAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdSaveAll.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdSaveAll.Location = New System.Drawing.Point(315, 461)
    Me.cmdSaveAll.Name = "cmdSaveAll"
    Me.cmdSaveAll.Size = New System.Drawing.Size(92, 46)
    Me.cmdSaveAll.TabIndex = 101
    Me.cmdSaveAll.Text = "&Save"
    Me.cmdSaveAll.UseVisualStyleBackColor = False
    '
    'cmdCrop
    '
    Me.cmdCrop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdCrop.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdCrop.Location = New System.Drawing.Point(415, 461)
    Me.cmdCrop.Name = "cmdCrop"
    Me.cmdCrop.Size = New System.Drawing.Size(92, 46)
    Me.cmdCrop.TabIndex = 102
    Me.cmdCrop.Text = "&Q Crop"
    Me.cmdCrop.UseVisualStyleBackColor = False
    '
    'cmdNext
    '
    Me.cmdNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdNext.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdNext.Image = CType(resources.GetObject("cmdNext.Image"), System.Drawing.Image)
    Me.cmdNext.ImageAlign = System.Drawing.ContentAlignment.TopCenter
    Me.cmdNext.Location = New System.Drawing.Point(107, 461)
    Me.cmdNext.Name = "cmdNext"
    Me.cmdNext.Size = New System.Drawing.Size(57, 46)
    Me.cmdNext.TabIndex = 99
    Me.cmdNext.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.cmdNext.UseVisualStyleBackColor = False
    '
    'cmdImageUpdate
    '
    Me.cmdImageUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdImageUpdate.Location = New System.Drawing.Point(1052, 819)
    Me.cmdImageUpdate.Name = "cmdImageUpdate"
    Me.cmdImageUpdate.Size = New System.Drawing.Size(132, 49)
    Me.cmdImageUpdate.TabIndex = 106
    Me.cmdImageUpdate.Text = "update image counts"
    Me.cmdImageUpdate.UseVisualStyleBackColor = True
    '
    'txName
    '
    Me.txName.Enabled = False
    Me.txName.Location = New System.Drawing.Point(154, 63)
    Me.txName.Name = "txName"
    Me.txName.Size = New System.Drawing.Size(318, 25)
    Me.txName.TabIndex = 9
    '
    'txRaceNumber
    '
    Me.txRaceNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
    Me.txRaceNumber.Location = New System.Drawing.Point(154, 30)
    Me.txRaceNumber.Name = "txRaceNumber"
    Me.txRaceNumber.Size = New System.Drawing.Size(318, 25)
    Me.txRaceNumber.TabIndex = 5
    '
    'txDateAdded
    '
    Me.txDateAdded.Enabled = False
    Me.txDateAdded.Location = New System.Drawing.Point(324, 269)
    Me.txDateAdded.Name = "txDateAdded"
    Me.txDateAdded.Size = New System.Drawing.Size(148, 25)
    Me.txDateAdded.TabIndex = 44
    Me.txDateAdded.TabStop = False
    '
    'txOriginalPath
    '
    Me.txOriginalPath.Font = New System.Drawing.Font("Arial Narrow", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txOriginalPath.Location = New System.Drawing.Point(154, 235)
    Me.txOriginalPath.Name = "txOriginalPath"
    Me.txOriginalPath.Size = New System.Drawing.Size(318, 22)
    Me.txOriginalPath.TabIndex = 41
    '
    'cmdDelete
    '
    Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdDelete.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdDelete.Location = New System.Drawing.Point(215, 521)
    Me.cmdDelete.Name = "cmdDelete"
    Me.cmdDelete.Size = New System.Drawing.Size(92, 46)
    Me.cmdDelete.TabIndex = 103
    Me.cmdDelete.TabStop = False
    Me.cmdDelete.Text = "Delete"
    Me.cmdDelete.UseVisualStyleBackColor = False
    '
    'cmdSaveData
    '
    Me.cmdSaveData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdSaveData.Font = New System.Drawing.Font("Arial", 8.0!)
    Me.cmdSaveData.Location = New System.Drawing.Point(315, 521)
    Me.cmdSaveData.Name = "cmdSaveData"
    Me.cmdSaveData.Size = New System.Drawing.Size(92, 46)
    Me.cmdSaveData.TabIndex = 104
    Me.cmdSaveData.Text = "Save &Data"
    Me.cmdSaveData.UseVisualStyleBackColor = False
    '
    'txDate
    '
    Me.txDate.Enabled = False
    Me.txDate.Location = New System.Drawing.Point(154, 269)
    Me.txDate.Name = "txDate"
    Me.txDate.Size = New System.Drawing.Size(148, 25)
    Me.txDate.TabIndex = 43
    Me.txDate.TabStop = False
    '
    'txFileName
    '
    Me.txFileName.Location = New System.Drawing.Point(154, 205)
    Me.txFileName.Name = "txFileName"
    Me.txFileName.Size = New System.Drawing.Size(318, 25)
    Me.txFileName.TabIndex = 31
    '
    'lbOriginalPath
    '
    Me.lbOriginalPath.AutoSize = True
    Me.lbOriginalPath.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbOriginalPath.Location = New System.Drawing.Point(16, 239)
    Me.lbOriginalPath.Name = "lbOriginalPath"
    Me.lbOriginalPath.Size = New System.Drawing.Size(96, 17)
    Me.lbOriginalPath.TabIndex = 40
    Me.lbOriginalPath.Text = "Original Path:"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label2.Location = New System.Drawing.Point(16, 65)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(51, 17)
    Me.Label2.TabIndex = 8
    Me.Label2.Text = "Name:"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label1.Location = New System.Drawing.Point(16, 32)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(103, 17)
    Me.Label1.TabIndex = 4
    Me.Label1.Text = "Race Number:"
    '
    'lbDate2
    '
    Me.lbDate2.AutoSize = True
    Me.lbDate2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbDate2.Location = New System.Drawing.Point(16, 273)
    Me.lbDate2.Name = "lbDate2"
    Me.lbDate2.Size = New System.Drawing.Size(133, 17)
    Me.lbDate2.TabIndex = 42
    Me.lbDate2.Text = "Photo, Added Date:"
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.chkLink)
    Me.GroupBox1.Controls.Add(Me.txName)
    Me.GroupBox1.Controls.Add(Me.txRaceNumber)
    Me.GroupBox1.Controls.Add(Me.txDateAdded)
    Me.GroupBox1.Controls.Add(Me.txOriginalPath)
    Me.GroupBox1.Controls.Add(Me.txDate)
    Me.GroupBox1.Controls.Add(Me.txFileName)
    Me.GroupBox1.Controls.Add(Me.lbOriginalPath)
    Me.GroupBox1.Controls.Add(Me.Label2)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Controls.Add(Me.lbDate2)
    Me.GroupBox1.Controls.Add(Me.LbFname)
    Me.GroupBox1.Location = New System.Drawing.Point(18, 20)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(489, 393)
    Me.GroupBox1.TabIndex = 92
    Me.GroupBox1.TabStop = false
    '
    'LbFname
    '
    Me.LbFname.AutoSize = true
    Me.LbFname.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
    Me.LbFname.Location = New System.Drawing.Point(16, 207)
    Me.LbFname.Name = "LbFname"
    Me.LbFname.Size = New System.Drawing.Size(78, 17)
    Me.LbFname.TabIndex = 30
    Me.LbFname.Text = "File Name:"
    '
    'pView
    '
    Me.pView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
    Me.pView.BackColor = System.Drawing.SystemColors.ControlDarkDark
    Me.pView.BitmapPath = Nothing
    Me.pView.CurrentPage = -1
    Me.pView.DrawAngle = 0R
    Me.pView.DrawBackColor = System.Drawing.Color.White
    Me.pView.DrawBox = New System.Drawing.Rectangle(0, 0, 0, 0)
    Me.pView.DrawDashed = false
    Me.pView.DrawFilled = false
    Me.pView.DrawFont = Nothing
    Me.pView.DrawForeColor = System.Drawing.Color.Navy
    Me.pView.DrawLineWidth = 1!
    Me.pView.DrawPath = Nothing
    Me.pView.DrawPoints = CType(resources.GetObject("pView.DrawPoints"),System.Collections.Generic.List(Of System.Drawing.Point))
    Me.pView.DrawShape = PhotoMud.shape.Line
    Me.pView.DrawString = ""
    Me.pView.DrawTextFmt = Nothing
    Me.pView.FloaterOutline = false
    Me.pView.FloaterPath = Nothing
    Me.pView.FloaterPosition = New System.Drawing.Point(0, 0)
    Me.pView.FloaterVisible = true
    Me.pView.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High
    Me.pView.Location = New System.Drawing.Point(527, 12)
    Me.pView.Name = "pView"
    Me.pView.pageBmp = CType(resources.GetObject("pView.pageBmp"),System.Collections.Generic.List(Of System.Drawing.Bitmap))
    Me.pView.PageCount = 0
    Me.pView.RubberAngle = 0R
    Me.pView.rubberBackColor = System.Drawing.Color.White
    Me.pView.RubberBox = New System.Drawing.Rectangle(0, 0, 0, 0)
    Me.pView.RubberBoxCrop = false
    Me.pView.RubberColor = System.Drawing.Color.Navy
    Me.pView.RubberDashed = false
    Me.pView.RubberEnabled = false
    Me.pView.RubberFilled = false
    Me.pView.RubberFont = Nothing
    Me.pView.RubberLineWidth = 1R
    Me.pView.RubberPath = Nothing
    Me.pView.RubberPoints = CType(resources.GetObject("pView.RubberPoints"),System.Collections.Generic.List(Of System.Drawing.Point))
    Me.pView.RubberShape = PhotoMud.shape.Curve
    Me.pView.RubberString = ""
    Me.pView.RubberTextFmt = Nothing
    Me.pView.SelectionVisible = true
    Me.pView.Size = New System.Drawing.Size(784, 793)
    Me.pView.TabIndex = 109
    Me.pView.TabStop = false
    Me.pView.ZoomFactor = 1R
    '
    'frmRacePhoto
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(8!, 17!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.cmdClose
    Me.ClientSize = New System.Drawing.Size(1326, 880)
    Me.Controls.Add(Me.cmdBack)
    Me.Controls.Add(Me.Label2w)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.lbPicPath)
    Me.Controls.Add(Me.cmdSaveAll)
    Me.Controls.Add(Me.cmdCrop)
    Me.Controls.Add(Me.cmdNext)
    Me.Controls.Add(Me.cmdImageUpdate)
    Me.Controls.Add(Me.cmdDelete)
    Me.Controls.Add(Me.cmdSaveData)
    Me.Controls.Add(Me.pView)
    Me.Controls.Add(Me.GroupBox1)
    Me.Font = New System.Drawing.Font("Arial", 9!)
    Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
    Me.Name = "frmRacePhoto"
    Me.ShowIcon = false
    Me.ShowInTaskbar = false
    Me.Text = "Race Photos"
    Me.GroupBox1.ResumeLayout(false)
    Me.GroupBox1.PerformLayout
    CType(Me.pView,System.ComponentModel.ISupportInitialize).EndInit
    Me.ResumeLayout(false)
    Me.PerformLayout

End Sub
  Public WithEvents cmdBack As System.Windows.Forms.Button
  Public WithEvents Label2w As System.Windows.Forms.Label
  Public WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents chkLink As System.Windows.Forms.CheckBox
  Friend WithEvents lbPicPath As System.Windows.Forms.Label
  Public WithEvents cmdSaveAll As System.Windows.Forms.Button
  Public WithEvents cmdCrop As System.Windows.Forms.Button
  Public WithEvents cmdNext As System.Windows.Forms.Button
  Friend WithEvents cmdImageUpdate As System.Windows.Forms.Button
  Friend WithEvents txName As System.Windows.Forms.TextBox
  Friend WithEvents txRaceNumber As System.Windows.Forms.TextBox
  Friend WithEvents txDateAdded As System.Windows.Forms.TextBox
  Friend WithEvents txOriginalPath As System.Windows.Forms.TextBox
  Public WithEvents ToolTip1 As System.Windows.Forms.ToolTip
  Public WithEvents cmdDelete As System.Windows.Forms.Button
  Public WithEvents cmdSaveData As System.Windows.Forms.Button
  Friend WithEvents pView As PhotoMud.pViewer
  Friend WithEvents txDate As System.Windows.Forms.TextBox
  Friend WithEvents txFileName As System.Windows.Forms.TextBox
  Public WithEvents lbOriginalPath As System.Windows.Forms.Label
  Public WithEvents Label2 As System.Windows.Forms.Label
  Public WithEvents Label1 As System.Windows.Forms.Label
  Public WithEvents lbDate2 As System.Windows.Forms.Label
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Public WithEvents LbFname As System.Windows.Forms.Label
End Class
