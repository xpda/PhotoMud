Public Class ctlBrightness

  Dim busy As Boolean = False
  Dim loading As Boolean = True
  Dim clickPoint As Point

  Event Changed(sender As Object, e As EventArgs)
  Event OK(sender As Object, e As EventArgs)
  Event Cancel(sender As Object, e As EventArgs)

  Property Brightness() As Integer

    Get
      Brightness = nmBrightness.Value
    End Get

    Set(ByVal value As Integer)
      If value >= nmBrightness.Minimum And value <= nmBrightness.Maximum Then
        nmBrightness.Value = value
        trkBrightness.Value = value
      End If
    End Set

  End Property

  Property Contrast() As Integer

    Get
      Contrast = nmContrast.Value
    End Get

    Set(ByVal value As Integer)
      If value >= nmContrast.Minimum And value <= nmContrast.Maximum Then
        nmContrast.Value = value
        trkContrast.Value = value
      End If
    End Set

  End Property

  Property Saturation() As Integer

    Get
      Saturation = nmSaturation.Value
    End Get

    Set(ByVal value As Integer)
      If value >= nmSaturation.Minimum And value <= nmSaturation.Maximum Then
        nmSaturation.Value = value
        trkSaturation.Value = value
      End If
    End Set

  End Property

  Private Sub nmBrightness_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles nmBrightness.ValueChanged
    If busy Or loading Then Exit Sub
    RaiseEvent Changed(sender, e)
    trkBrightness.Value = nmBrightness.Value
  End Sub

  Private Sub nmContrast_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles nmContrast.ValueChanged
    If busy Or loading Then Exit Sub
    RaiseEvent Changed(sender, e)
    trkContrast.Value = nmContrast.Value
  End Sub

  Private Sub nmSaturation_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles nmSaturation.ValueChanged
    If busy Or loading Then Exit Sub
    RaiseEvent Changed(sender, e)
    trkSaturation.Value = nmSaturation.Value
  End Sub

  Private Sub trkBrightness_Scroll(ByVal sender As System.Object, ByVal e As EventArgs) Handles trkBrightness.Scroll
    busy = True
    nmBrightness.Value = trkBrightness.Value
    RaiseEvent Changed(sender, e)
    busy = False
  End Sub

  Private Sub trkContrast_Scroll(ByVal sender As System.Object, ByVal e As EventArgs) Handles trkContrast.Scroll
    busy = True
    nmContrast.Value = trkContrast.Value
    RaiseEvent Changed(sender, e)
    busy = False
  End Sub

  Private Sub trkSaturation_Scroll(ByVal sender As System.Object, ByVal e As EventArgs) Handles trkSaturation.Scroll
    busy = True
    nmSaturation.Value = trkSaturation.Value
    RaiseEvent Changed(sender, e)
    busy = False
  End Sub

  Private Sub cmdResetBright_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmdResetBright.Click
    busy = True
    nmBrightness.Value = 0
    trkBrightness.Value = 0
    nmContrast.Value = 0
    trkContrast.Value = 0
    nmSaturation.Value = 0
    trkSaturation.Value = 0
    busy = False
    RaiseEvent Changed(sender, e)

  End Sub

  Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
    RaiseEvent OK(sender, e)
  End Sub

  Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
    RaiseEvent Cancel(sender, e)
  End Sub

  Public Sub setMode(ByVal inCommand As cmd)

    If inCommand = cmd.Brightness Then
      lbBrightness.Text = "&Brightness:"
      trkBrightness.Value = 0
      trkBrightness.Minimum = -100
      trkBrightness.Maximum = 100
      trkBrightness.SmallChange = 1
      trkBrightness.LargeChange = 5
      nmBrightness.Minimum = trkBrightness.Minimum
      nmBrightness.Maximum = trkBrightness.Maximum
      nmBrightness.Value = trkBrightness.Value

      lbContrast.Text = "Co&ntrast:"
      trkContrast.Value = 0
      trkContrast.Minimum = -100
      trkContrast.Maximum = 100
      trkContrast.SmallChange = 1
      trkContrast.LargeChange = 5
      nmContrast.Minimum = trkContrast.Minimum
      nmContrast.Maximum = trkContrast.Maximum
      nmContrast.Value = trkContrast.Value

      lbSaturation.Text = "&Saturation:"
      trkSaturation.Value = 0
      trkSaturation.Minimum = -100
      trkSaturation.Maximum = 100
      trkSaturation.SmallChange = 1
      trkSaturation.LargeChange = 10
      nmSaturation.Minimum = trkSaturation.Minimum
      nmSaturation.Maximum = trkSaturation.Maximum
      nmSaturation.Value = trkSaturation.Value

      '    ElseIf eqstr(inCommand, cmd.hsv) Then
      '      lbBrightness.Text = "H&ue (angle):"
      '      trkBrightness.Value = 0
      '      trkBrightness.Minimum = -360
      '      trkBrightness.Maximum = 360
      '      trkBrightness.SmallChange = 1
      '      trkBrightness.LargeChange = 20
      '      nmBrightness.Minimum = trkBrightness.Minimum
      '      nmBrightness.Maximum = trkBrightness.Maximum
      '      nmBrightness.Value = trkBrightness.Value
      '
      '      lbContrast.Text = "&Saturation:"
      '      trkContrast.Value = 0
      '      trkContrast.Minimum = -100
      '      trkContrast.Maximum = 100
      '      trkContrast.SmallChange = 1
      '      trkContrast.LargeChange = 10
      '      nmContrast.Minimum = trkContrast.Minimum
      '      nmContrast.Maximum = trkContrast.Maximum
      '      nmContrast.Value = trkContrast.Value
      '
      '      lbSaturation.Text = "I&ntensity:"
      '      trkSaturation.Value = 0
      '      trkSaturation.Minimum = -100
      '      trkSaturation.Maximum = 100
      '      trkSaturation.SmallChange = 1
      '      trkSaturation.LargeChange = 10
      '      nmSaturation.Minimum = trkSaturation.Minimum
      '      nmSaturation.Maximum = trkSaturation.Maximum
      '      nmSaturation.Value = trkSaturation.Value
    End If

    nmBrightness.select()
    nmBrightness.Select(0, Len(nmBrightness.Text))

  End Sub

  Private Sub ctlBrightness_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
      Me.Location = Me.Location + New Size(e.Location) - New Size(clickPoint)
    End If
  End Sub
  Private Sub ctlBrightness_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
    If e.Button = System.Windows.Forms.MouseButtons.Left Then
      Me.Location = Me.Location + New Size(e.Location) - New Size(clickPoint)
    End If
    Me.Cursor = Cursors.Default
  End Sub

  Public Sub New()
    ' This is required by the Windows Form Designer.
    InitializeComponent()
    Me.Visible = False
  End Sub

End Class


