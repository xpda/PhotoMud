Imports Leadtools.WinForms

Public Class rViewer
Inherits RasterImageViewer

Protected NotOverridable Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keydata As Keys) As Boolean
If keydata = Keys.Right Or keydata = Keys.Left Or keydata = Keys.Up Or keydata = Keys.Down Then
  OnKeyDown(New KeyEventArgs(keydata))
  ProcessCmdKey = True
Else
  ProcessCmdKey = MyBase.ProcessCmdKey(msg, keydata)
  End If
End Function

Public Sub New()

MyBase.AutoResetScaleFactor = False
MyBase.AutoResetScrollPosition = False
MyBase.AutoScroll = True
MyBase.Dock = DockStyle.Fill
MyBase.HorizontalAlignMode = Leadtools.RasterPaintAlignMode.Center
MyBase.VerticalAlignMode = Leadtools.RasterPaintAlignMode.Center
MyBase.Location = New Point(0, 0)
MyBase.TabStop = False
MyBase.EnableScrollingInterface = True
MyBase.BackColor = SystemColors.ControlDarkDark

End Sub

End Class

