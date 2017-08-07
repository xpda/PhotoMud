<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCalendarEvents
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
Me.components = New System.ComponentModel.Container
Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCalendarEvents))
Me.Grid1 = New System.Windows.Forms.DataGridView
Me.Description = New System.Windows.Forms.DataGridViewTextBoxColumn
Me.colDate = New System.Windows.Forms.DataGridViewTextBoxColumn
Me.mnxGrid = New System.Windows.Forms.ContextMenuStrip(Me.components)
Me.mnxGridInsert = New System.Windows.Forms.ToolStripMenuItem
Me.mnxGridDelete = New System.Windows.Forms.ToolStripMenuItem
Me.mnxGridUndo = New System.Windows.Forms.ToolStripMenuItem
Me.cmdHelp = New System.Windows.Forms.Button
Me.cmdSave = New System.Windows.Forms.Button
Me.cmdCancel = New System.Windows.Forms.Button
Me.Label1 = New System.Windows.Forms.Label
Me.cmbCat = New System.Windows.Forms.ComboBox
Me.Label2 = New System.Windows.Forms.Label
Me.cmdDelCat = New System.Windows.Forms.Button
Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
CType(Me.Grid1, System.ComponentModel.ISupportInitialize).BeginInit()
Me.mnxGrid.SuspendLayout()
Me.SuspendLayout()
'
'Grid1
'
Me.Grid1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
Me.Grid1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
Me.Grid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
Me.Grid1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Description, Me.colDate})
Me.Grid1.ContextMenuStrip = Me.mnxGrid
Me.Grid1.Location = New System.Drawing.Point(12, 66)
Me.Grid1.MultiSelect = False
Me.Grid1.Name = "Grid1"
Me.Grid1.RowTemplate.Height = 24
Me.Grid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
Me.Grid1.Size = New System.Drawing.Size(784, 432)
Me.Grid1.TabIndex = 3
'
'Description
'
Me.Description.HeaderText = "Description"
Me.Description.Name = "Description"
'
'colDate
'
Me.colDate.HeaderText = "Date"
Me.colDate.Name = "colDate"
'
'mnxGrid
'
Me.mnxGrid.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnxGridInsert, Me.mnxGridDelete, Me.mnxGridUndo})
Me.mnxGrid.Name = "mnxGrid"
Me.mnxGrid.Size = New System.Drawing.Size(147, 82)
'
'mnxGridInsert
'
Me.mnxGridInsert.Name = "mnxGridInsert"
Me.mnxGridInsert.Size = New System.Drawing.Size(146, 26)
Me.mnxGridInsert.Text = "&Insert"
'
'mnxGridDelete
'
Me.mnxGridDelete.Name = "mnxGridDelete"
Me.mnxGridDelete.Size = New System.Drawing.Size(146, 26)
Me.mnxGridDelete.Text = "&Delete"
'
'mnxGridUndo
'
Me.mnxGridUndo.Name = "mnxGridUndo"
Me.mnxGridUndo.Size = New System.Drawing.Size(146, 26)
Me.mnxGridUndo.Text = "&Undo"
'
'cmdHelp
'
Me.cmdHelp.Anchor = System.Windows.Forms.AnchorStyles.Bottom
Me.cmdHelp.Font = New System.Drawing.Font("Arial", 9.0!)
Me.cmdHelp.Image = CType(resources.GetObject("cmdHelp.Image"), System.Drawing.Image)
Me.cmdHelp.Location = New System.Drawing.Point(468, 516)
Me.cmdHelp.Name = "cmdHelp"
Me.cmdHelp.Size = New System.Drawing.Size(41, 44)
Me.cmdHelp.TabIndex = 4
Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
Me.ToolTip1.SetToolTip(Me.cmdHelp, "Help")
Me.cmdHelp.UseVisualStyleBackColor = False
'
'cmdSave
'
Me.cmdSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom
Me.cmdSave.Font = New System.Drawing.Font("Arial", 9.0!)
Me.cmdSave.Location = New System.Drawing.Point(650, 526)
Me.cmdSave.Name = "cmdSave"
Me.cmdSave.Size = New System.Drawing.Size(92, 34)
Me.cmdSave.TabIndex = 6
Me.cmdSave.Text = "&Save"
Me.ToolTip1.SetToolTip(Me.cmdSave, "Save changes and return")
Me.cmdSave.UseVisualStyleBackColor = False
'
'cmdCancel
'
Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.0!)
Me.cmdCancel.Location = New System.Drawing.Point(533, 526)
Me.cmdCancel.Name = "cmdCancel"
Me.cmdCancel.Size = New System.Drawing.Size(92, 34)
Me.cmdCancel.TabIndex = 5
Me.cmdCancel.Text = "Cancel"
Me.cmdCancel.UseVisualStyleBackColor = False
'
'Label1
'
Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!)
Me.Label1.Location = New System.Drawing.Point(12, 516)
Me.Label1.Name = "Label1"
Me.Label1.Size = New System.Drawing.Size(363, 59)
Me.Label1.TabIndex = 50
Me.Label1.Text = "Enter birthdays, anniversaries, and other dates you would like to add to your cal" & _
    "endar."
'
'cmbCat
'
Me.cmbCat.FormattingEnabled = True
Me.cmbCat.Items.AddRange(New Object() {"Custom Events", ""})
Me.cmbCat.Location = New System.Drawing.Point(99, 16)
Me.cmbCat.Name = "cmbCat"
Me.cmbCat.Size = New System.Drawing.Size(222, 25)
Me.cmbCat.TabIndex = 1
Me.ToolTip1.SetToolTip(Me.cmbCat, "Category of dates")
'
'Label2
'
Me.Label2.AutoSize = True
Me.Label2.Location = New System.Drawing.Point(12, 19)
Me.Label2.Name = "Label2"
Me.Label2.Size = New System.Drawing.Size(71, 17)
Me.Label2.TabIndex = 0
Me.Label2.Text = "&Category:"
'
'cmdDelCat
'
Me.cmdDelCat.Location = New System.Drawing.Point(327, 12)
Me.cmdDelCat.Name = "cmdDelCat"
Me.cmdDelCat.Size = New System.Drawing.Size(145, 30)
Me.cmdDelCat.TabIndex = 2
Me.cmdDelCat.Text = "&Delete Category"
Me.cmdDelCat.UseVisualStyleBackColor = True
'
'frmCalendarEvents
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.CancelButton = Me.cmdCancel
Me.ClientSize = New System.Drawing.Size(808, 582)
Me.Controls.Add(Me.Label2)
Me.Controls.Add(Me.cmdDelCat)
Me.Controls.Add(Me.cmbCat)
Me.Controls.Add(Me.Label1)
Me.Controls.Add(Me.cmdHelp)
Me.Controls.Add(Me.cmdSave)
Me.Controls.Add(Me.cmdCancel)
Me.Controls.Add(Me.Grid1)
Me.Font = New System.Drawing.Font("Arial", 9.0!)
Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
Me.Name = "frmCalendarEvents"
Me.ShowInTaskbar = False
Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
Me.Text = "Custom Calendar Events"
CType(Me.Grid1, System.ComponentModel.ISupportInitialize).EndInit()
Me.mnxGrid.ResumeLayout(False)
Me.ResumeLayout(False)
Me.PerformLayout()

End Sub
    Friend WithEvents Grid1 As System.Windows.Forms.DataGridView
    Public WithEvents cmdHelp As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents mnxGrid As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnxGridInsert As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnxGridDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnxGridUndo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmbCat As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdDelCat As System.Windows.Forms.Button
    Friend WithEvents Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
