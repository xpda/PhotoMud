Public Class ctlColorTolerance

  Dim busy As Boolean = False
  Dim loading As Boolean = True
  Dim clickPoint As Point

  Event ToleranceChanged(sender As Object, e As EventArgs)
  Event OK(sender As Object, e As EventArgs)
  Event Cancel(sender As Object, e As EventArgs)

  Property Tolerance() As Integer

    Get
      Tolerance = nmTolerance.Value
    End Get

    Set(ByVal value As Integer)
      If value >= nmTolerance.Minimum And value <= nmTolerance.Maximum Then
        nmTolerance.Value = value
        trkTolerance.Value = value
      End If
    End Set

  End Property

  Property saveDefault() As Boolean

    Get
      saveDefault = chkDefault.Checked
    End Get

    Set(ByVal value As Boolean)
      chkDefault.Checked = value
    End Set

  End Property

  Private Sub nmUpperTolerance_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles nmTolerance.ValueChanged
    If busy Or loading Then Exit Sub
    trkTolerance.Value = nmTolerance.Value
    RaiseEvent ToleranceChanged(sender, e)
  End Sub

  Private Sub trkUpperTolerance_Scroll(ByVal sender As System.Object, ByVal e As EventArgs) Handles trkTolerance.Scroll
    busy = True
    nmTolerance.Value = trkTolerance.Value
    RaiseEvent ToleranceChanged(sender, e)
    busy = False
  End Sub

  Private Sub ctlColorTolerance_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    loading = False
  End Sub

  Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    RaiseEvent OK(sender, e)
  End Sub

  Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
    RaiseEvent Cancel(sender, e)
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
