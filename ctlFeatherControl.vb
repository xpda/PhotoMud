Public Class ctlFeatherControl
Dim busy As Boolean = False
Dim loading As Boolean = True

Dim clickPoint As Point

Event rangeChanged(sender As Object, e As EventArgs)

Property Range() As Integer

Get
Range = nmRange.Value
End Get

Set(ByVal value As Integer)
If value >= nmRange.Minimum And value <= nmRange.Maximum Then
  nmRange.Value = value
  trkRange.Value = value
  End If
End Set

End Property

  Private Sub nmRange_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles nmRange.ValueChanged
    If busy Or loading Then Exit Sub
    trkRange.Value = nmRange.Value
    RaiseEvent rangeChanged(sender, e)
  End Sub

Private Sub trkRange_Scroll(ByVal sender As System.Object, ByVal e As EventArgs) Handles trkRange.Scroll
busy = True
nmRange.Value = trkRange.Value
RaiseEvent rangeChanged(sender, e)
busy = False
End Sub

Private Sub ctlColor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
loading = False
End Sub

  Private Sub ctlBrightness_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
    If e.Button = MouseButtons.Left Then
      clickPoint = e.Location
      Me.Cursor = Cursors.SizeAll
    End If
  End Sub
Private Sub ctlBrightness_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
If e.Button = System.Windows.Forms.MouseButtons.Left Then
  Me.Location = Me.Location + e.Location - clickPoint
  End If
End Sub
Private Sub ctlBrightness_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
If e.Button = System.Windows.Forms.MouseButtons.Left Then
  Me.Location = Me.Location + e.Location - clickPoint
  End If
Me.Cursor = Cursors.Default
End Sub

Public Sub New()
    ' This is required by the Windows Form Designer.
InitializeComponent()
Me.Visible = False
End Sub

End Class
