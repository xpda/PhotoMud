Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports ImageMagick
Imports System.Collections.Generic

Public Class frmArtEffects
  Inherits Form

  Dim loading As Boolean = True
  Dim Sliding As Boolean

  Dim effect(30) As String

  Dim gPath As GraphicsPath = Nothing

  Dim imageReduced As Boolean
  Dim xReduced As Double

  Dim WithEvents Timer1 As New Timer

  Private Sub cmdCancel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
    clearBitmap(qImage)
    Me.DialogResult = DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub cmdHelp_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdHelp.Click
    Try
      Help.ShowHelp(Me, helpFile, HelpNavigator.Topic, Me.Name & ".html")
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

  Private Sub cmdOK_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdOK.Click

    Me.Cursor = Cursors.WaitCursor

    drawEffect(True)

    iniArtEffect = cmbEffect.SelectedIndex
    Me.Cursor = Cursors.Default
    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Private Sub aview_Resize(sender As Object, e As EventArgs) Handles aView.Resize
    aView.zoom(1)
  End Sub

  Sub aview_zoomed() Handles aView.Zoomed
    Timer1.Stop() : Timer1.Interval = 150 : Timer1.Start() ' calls drawsharp
  End Sub

  Private Sub frmSpecialEffects_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Dim i As Integer

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    loading = True

    Me.Cursor = Cursors.WaitCursor

    i = cmbEffect.Items.Count
    cmbEffect.Items.Insert(i, "Test") : effect(i) = "test"
    i = cmbEffect.Items.Count
    cmbEffect.Items.Insert(i, "Bricks") : effect(i) = "bricks"
    i = cmbEffect.Items.Count
    cmbEffect.Items.Insert(i, "Fog") : effect(i) = "fog"
    i = cmbEffect.Items.Count
    cmbEffect.Items.Insert(i, "Charcoal") : effect(i) = "charcoal"
    i = cmbEffect.Items.Count
    cmbEffect.Items.Insert(i, "Pencil") : effect(i) = "pencil"
    i = cmbEffect.Items.Count
    cmbEffect.Items.Insert(i, "Colored Pencil") : effect(i) = "coloredpencil"
    i = cmbEffect.Items.Count
    cmbEffect.Items.Insert(i, "Glass Cells") : effect(i) = "glass"
    i = cmbEffect.Items.Count
    cmbEffect.Items.Insert(i, "Impressionist") : effect(i) = "impressionist"
    i = cmbEffect.Items.Count
    cmbEffect.Items.Insert(i, "Lens Flare") : effect(i) = "flare"
    i = cmbEffect.Items.Count
    cmbEffect.Items.Insert(i, "Pointillist") : effect(i) = "pointillist"
    i = cmbEffect.Items.Count
    cmbEffect.Items.Insert(i, "Polar Transformation") : effect(i) = "polar"
    i = cmbEffect.Items.Count
    cmbEffect.Items.Insert(i, "Puzzle Pieces") : effect(i) = "puzzle"
    i = cmbEffect.Items.Count
    cmbEffect.Items.Insert(i, "Rev Effect") : effect(i) = "reveffect"
    i = cmbEffect.Items.Count
    cmbEffect.Items.Insert(i, "Segment by Color") : effect(i) = "segment"
    i = cmbEffect.Items.Count
    cmbEffect.Items.Insert(i, "Shadow") : effect(i) = "shadow"
    i = cmbEffect.Items.Count
    cmbEffect.Items.Insert(i, "Swirl") : effect(i) = "swirl"
    i = cmbEffect.Items.Count
    cmbEffect.Items.Insert(i, "Warp") : effect(i) = "warp"
    i = cmbEffect.Items.Count
    cmbEffect.Items.Insert(i, "Water Reflections") : effect(i) = "ocean"
    i = cmbEffect.Items.Count
    cmbEffect.Items.Insert(i, "Wave Effect") : effect(i) = "wave"
    i = cmbEffect.Items.Count
    cmbEffect.Items.Insert(i, "Wind Effect") : effect(i) = "wind"
    i = cmbEffect.Items.Count
    cmbEffect.Items.Insert(i, "Zoom Wave") : effect(i) = "zoomwave"
    i = cmbEffect.Items.Count
    cmbEffect.Items.Insert(i, "Glass Lens") : effect(i) = "lens"
    i = cmbEffect.Items.Count
    cmbEffect.Items.Insert(i, "Psycho") : effect(i) = "psycho"

    'cmbEffect.SelectedIndex = iniArtEffect
    cmbEffect.SelectedIndex = -1
    setupSliders()

    If frmMain.mView.FloaterPath IsNot Nothing Then gpath = frmMain.mView.FloaterPath.Clone

    ' if it's too big, reduce the size and assign that.
    If qImage.Width * qImage.Height > bigMegapix Then
      imageReduced = True
      xReduced = getSmallerImage(qImage, aView.pView0)
      aView.pView1.setBitmap(aView.pView0.Bitmap)
    Else
      imageReduced = False
      aView.pView0.setBitmap(qImage)
      aView.pView1.setBitmap(qImage)
      xReduced = 1
    End If

    aView.ZoomViews(0.5)

    Me.Cursor = Cursors.Default

    aView.pCenter = New Point(aView.pView0.Bitmap.Width / 2, aView.pView0.Bitmap.Height / 2)

    loading = False
    Sliding = False

  End Sub

  Private Sub trkEffect_scroll(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles trkEffect0.Scroll, trkEffect1.Scroll, trkEffect2.Scroll, trkeffect3.Scroll, trkeffect4.Scroll, trkeffect5.Scroll

    Dim trk As TrackBar

    If Sliding Or aView.pView0.Bitmap Is Nothing Then Exit Sub
    Sliding = True
    trk = Sender

    If trk Is trkEffect0 Then
      nmEffect0.Value = trkEffect0.Value
    ElseIf trk Is trkEffect1 Then
      nmEffect1.Value = trkEffect1.Value
    ElseIf trk Is trkEffect2 Then
      nmEffect2.Value = trkEffect2.Value
    ElseIf trk Is trkeffect3 Then
      nmEffect3.Value = trkeffect3.Value
    ElseIf trk Is trkeffect4 Then
      nmEffect4.Value = trkeffect4.Value
    ElseIf trk Is trkeffect5 Then
      nmEffect5.Value = trkeffect5.Value
    End If

    Timer1.Interval = 150 : Timer1.Stop() : Timer1.Start() ' draweffect in 150 milliseconds
    Me.Cursor = Cursors.Default
    Sliding = False

  End Sub

  Private Sub nmEffect_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) _
    Handles nmEffect0.ValueChanged, nmEffect1.ValueChanged, nmEffect2.ValueChanged, _
      nmEffect3.ValueChanged, nmEffect4.ValueChanged, nmEffect5.ValueChanged

    If Sliding Then Exit Sub

    If sender Is nmEffect0 Then
      trkEffect0.Value = nmEffect0.Value
    ElseIf sender Is nmEffect1 Then
      trkEffect1.Value = nmEffect1.Value
    ElseIf sender Is nmEffect2 Then
      trkEffect2.Value = nmEffect2.Value
    ElseIf sender Is nmEffect3 Then
      trkeffect3.Value = nmEffect3.Value
    ElseIf sender Is nmEffect4 Then
      trkeffect4.Value = nmEffect4.Value
    ElseIf sender Is nmEffect5 Then
      trkeffect5.Value = nmEffect5.Value
    End If

    Timer1.Interval = 150 : Timer1.Stop() : Timer1.Start() ' draweffect in 150 milliseconds

  End Sub

  Sub setupSliders()

    If cmbEffect.SelectedIndex < 0 Then Exit Sub
    Sliding = True

    cmdColor0.Visible = False
    lbColor0.Visible = False

    nmEffect0.Visible = False
    trkEffect0.Visible = False
    trkEffect0.LargeChange = 5
    lbEffect0.Visible = False

    nmEffect1.Visible = False
    trkEffect1.Visible = False
    trkEffect1.LargeChange = 5
    lbEffect1.Visible = False

    nmEffect2.Visible = False
    trkEffect2.Visible = False
    trkEffect2.LargeChange = 5
    lbEffect2.Visible = False

    nmEffect3.Visible = False
    trkeffect3.Visible = False
    trkeffect3.LargeChange = 5
    lbEffect3.Visible = False

    nmEffect4.Visible = False
    trkeffect4.Visible = False
    trkeffect4.LargeChange = 5
    lbEffect4.Visible = False

    nmEffect5.Visible = False
    trkeffect5.Visible = False
    trkeffect5.LargeChange = 5
    lbEffect5.Visible = False

    chkEffect0.Visible = False
    chkEffect1.Visible = False

    optEffect0.Visible = False
    optEffect1.Visible = False
    optEffect2.Visible = False

    aView.lbPoint.Visible = False

    Select Case effect(cmbEffect.SelectedIndex)

      Case "bricks"
        nmEffect0.Visible = True
        nmEffect0.Minimum = 20
        nmEffect0.Maximum = 400
        nmEffect0.Value = 100
        lbEffect0.Text = "Brick Size"
        lbEffect0.Visible = True
        trkEffect0.LargeChange = 20

        chkEffect0.Text = "Colored Mortar"
        chkEffect0.Checked = True
        chkEffect0.Visible = True

      Case "fog"
        nmEffect0.Visible = True
        nmEffect0.Minimum = 1
        nmEffect0.Maximum = 100
        nmEffect0.Value = 10
        lbEffect0.Text = "Thickness"
        lbEffect0.Visible = True

        nmEffect1.Visible = True
        nmEffect1.Minimum = 1
        nmEffect1.Maximum = 100
        nmEffect1.Value = 50
        lbEffect1.Text = "Density"
        lbEffect1.Visible = True
        trkEffect1.LargeChange = 10

        nmEffect2.Visible = True
        nmEffect2.Minimum = 1
        nmEffect2.Maximum = 100
        nmEffect2.Value = 50
        lbEffect2.Text = "Darkness"
        lbEffect2.Visible = True
        trkEffect2.LargeChange = 10

        nmEffect3.Visible = True
        nmEffect3.Minimum = 1
        nmEffect3.Maximum = 100
        nmEffect3.Value = 50
        lbEffect3.Text = "Size"
        lbEffect3.Visible = True
        trkeffect3.LargeChange = 10

      Case "test"
        'partialDraw = True

        nmEffect0.Visible = True
        nmEffect0.Minimum = 0
        nmEffect0.Maximum = 100
        nmEffect0.Value = 50
        lbEffect0.Text = "value 1"
        lbEffect0.Visible = True

        nmEffect1.Visible = True
        nmEffect1.Minimum = 0
        nmEffect1.Maximum = 100
        nmEffect1.Value = 40
        lbEffect1.Text = "value 2"
        lbEffect1.Visible = True
        trkEffect1.LargeChange = 1

        nmEffect3.Visible = True
        nmEffect3.Minimum = 0
        nmEffect3.Maximum = 100
        nmEffect3.Value = 50
        lbEffect3.Text = "value 3"
        lbEffect3.Visible = True

        nmEffect4.Visible = True
        nmEffect4.Minimum = 0
        nmEffect4.Maximum = 100
        nmEffect4.Value = 20
        lbEffect4.Text = "value 4"
        lbEffect4.Visible = True

      Case "pencil"
        nmEffect0.Visible = True
        nmEffect0.Minimum = 1
        nmEffect0.Maximum = 100
        nmEffect0.Value = 50
        lbEffect0.Text = "Pencil Width"
        lbEffect0.Visible = True

        nmEffect1.Visible = True
        nmEffect1.Minimum = 1
        nmEffect1.Maximum = 100
        nmEffect1.Value = 50
        lbEffect1.Text = "Outline Width"
        lbEffect1.Visible = True

      Case "coloredpencil"
        nmEffect0.Visible = True
        nmEffect0.Minimum = 1
        nmEffect0.Maximum = 100
        nmEffect0.Value = 50
        lbEffect0.Text = "Pencil Width"
        lbEffect0.Visible = True

        nmEffect1.Visible = True
        nmEffect1.Minimum = 1
        nmEffect1.Maximum = 100
        nmEffect1.Value = 50
        lbEffect1.Text = "Outline Width"
        lbEffect1.Visible = True

      Case "charcoal"
        nmEffect0.Visible = True
        nmEffect0.Minimum = 1
        nmEffect0.Maximum = 100
        nmEffect0.Value = 5
        lbEffect0.Text = "Intensity"
        lbEffect0.Visible = True

      Case "glass"
        nmEffect0.Visible = True
        nmEffect0.Minimum = 1
        nmEffect0.Maximum = 400
        nmEffect0.Value = 100
        lbEffect0.Text = "Cell Size"
        lbEffect0.Visible = True
        trkEffect1.LargeChange = 20

      Case "flare"
        nmEffect0.Visible = True
        nmEffect0.Minimum = 0
        nmEffect0.Maximum = 100
        nmEffect0.Value = 40
        lbEffect0.Text = "Flare intensity"
        lbEffect0.Visible = True

        optEffect0.Text = "Normal Lens"
        optEffect0.Visible = True
        optEffect0.Checked = True
        optEffect1.Text = "Wide Angle Lens"
        optEffect1.Visible = True
        optEffect1.Checked = False
        optEffect2.Text = "Telephoto Lens"
        optEffect2.Visible = True
        optEffect2.Checked = False

        cmdColor0.Visible = True
        cmdColor0.BackColor = mainColor
        lbColor0.Visible = True
        lbColor0.Text = "Flare Color"

        aView.lbPoint.Text = "Click on the photo above to select the flare location."
        aView.lbPoint.Visible = True

      Case "impressionist"
        nmEffect0.Visible = True
        nmEffect0.Minimum = 1
        nmEffect0.Maximum = 50
        nmEffect0.Value = 10
        lbEffect0.Text = "Intensity"
        lbEffect0.Visible = True
        trkEffect0.LargeChange = 5


      Case "ocean"
        nmEffect0.Visible = True
        nmEffect0.Minimum = 0
        nmEffect0.Maximum = 100
        nmEffect0.Value = 60
        lbEffect0.Text = "Amplitude"
        lbEffect0.Visible = True

        nmEffect1.Visible = True
        nmEffect1.Minimum = 0
        nmEffect1.Maximum = 50
        nmEffect1.Value = 15
        lbEffect1.Text = "Frequency"
        lbEffect1.Visible = True

        chkEffect0.Text = "Transparency from the Bottom"
        chkEffect0.Checked = True
        chkEffect0.Visible = True

      Case "pointillist"
        nmEffect0.Visible = True
        nmEffect0.Minimum = 1
        nmEffect0.Maximum = 300
        nmEffect0.Value = 20
        lbEffect0.Text = "Cell Size"
        lbEffect0.Visible = True

      Case "polar"
        cmdColor0.Visible = True
        cmdColor0.BackColor = mBackColor
        lbColor0.Visible = True
        lbColor0.Text = "Background Color"

        optEffect0.Text = "Convert to Polar View"
        optEffect0.Visible = True
        optEffect0.Checked = True
        optEffect1.Text = "Convert from Polar View"
        optEffect1.Visible = True
        optEffect1.Checked = False

      Case "puzzle"
        nmEffect0.Visible = True
        nmEffect0.Minimum = 2
        nmEffect0.Maximum = 50
        nmEffect0.Value = 4
        trkEffect0.LargeChange = 1
        lbEffect0.Text = "Blocks Horizontally"
        lbEffect0.Visible = True

        nmEffect1.Visible = True
        nmEffect1.Minimum = 2
        nmEffect1.Maximum = 50
        nmEffect1.Value = 4
        trkEffect1.LargeChange = 1
        lbEffect1.Text = "Blocks Vertically"
        lbEffect1.Visible = True

      Case "reveffect"
        nmEffect0.Visible = True
        nmEffect0.Minimum = 1
        nmEffect0.Maximum = 100
        nmEffect0.Value = 40
        lbEffect0.Text = "Maximum Wave Height"
        lbEffect0.Visible = True

        nmEffect1.Visible = True
        nmEffect1.Minimum = 1
        nmEffect1.Maximum = 20
        nmEffect1.Value = 1
        trkEffect1.LargeChange = 1
        lbEffect1.Text = "Space Between Waves"
        lbEffect1.Visible = True

      Case "ocean"
        nmEffect0.Visible = True
        nmEffect0.Minimum = 1
        nmEffect0.Maximum = 100
        nmEffect0.Value = 25
        lbEffect0.Text = "Wave Height"
        lbEffect0.Visible = True

        nmEffect1.Visible = True
        nmEffect1.Minimum = 1
        nmEffect1.Maximum = 30
        nmEffect1.Value = 10
        lbEffect1.Text = "Wave Frequency"
        lbEffect1.Visible = True

        nmEffect3.Visible = True
        nmEffect3.Minimum = 1
        nmEffect3.Maximum = 100
        nmEffect3.Value = 100
        lbEffect3.Text = "Vertical Scale"
        lbEffect3.Visible = True

        nmEffect4.Visible = True
        nmEffect4.Minimum = 0
        nmEffect4.Maximum = 100
        nmEffect4.Value = 10
        lbEffect4.Text = "Dark Tint"
        lbEffect4.Visible = True

        cmdColor0.Visible = True
        cmdColor0.BackColor = mBackColor
        lbColor0.Visible = True
        lbColor0.Text = "Background Color"

        aView.lbPoint.Text = "Click on the photo above to select the center point."
        aView.lbPoint.Visible = True

      Case "segment"
        nmEffect0.Visible = True
        nmEffect0.Minimum = 15
        nmEffect0.Maximum = 100
        nmEffect0.Value = 40
        lbEffect0.Text = "Threshold"
        lbEffect0.Visible = True

      Case "shadow"
        nmEffect0.Visible = True
        nmEffect0.Minimum = 0
        nmEffect0.Maximum = 255
        nmEffect0.Value = 100
        lbEffect0.Text = "Threshold"
        lbEffect0.Visible = True

        nmEffect1.Visible = True
        nmEffect1.Minimum = -180
        nmEffect1.Maximum = 180
        nmEffect1.Value = 45
        trkEffect1.LargeChange = 45
        lbEffect1.Text = "Shadow Angle"
        lbEffect1.Visible = True

        chkEffect0.Text = "Colored Shadow"
        chkEffect0.Checked = True
        chkEffect0.Visible = True

      Case "sphere"
        nmEffect0.Visible = True
        nmEffect0.Minimum = 0
        nmEffect0.Maximum = 50
        nmEffect0.Value = 30
        lbEffect0.Text = "Amount of Stretching"
        lbEffect0.Visible = True

        aView.lbPoint.Text = "Click on the photo above to select the center point."
        aView.lbPoint.Visible = True

      Case "swirl"
        nmEffect0.Visible = True
        nmEffect0.Minimum = -1440
        nmEffect0.Maximum = 1440
        nmEffect0.Value = 180
        trkEffect0.LargeChange = 30
        lbEffect0.Text = "Angle to Rotate"
        lbEffect0.Visible = True

        aView.lbPoint.Text = "Click on the photo above to select the center point."
        aView.lbPoint.Visible = True

      Case "wave"
        nmEffect0.Visible = True
        nmEffect0.Minimum = 1
        nmEffect0.Maximum = 100
        nmEffect0.Value = 90
        lbEffect0.Text = "Wave Amplitude"
        lbEffect0.Visible = True

        nmEffect1.Visible = True
        nmEffect1.Minimum = 1
        nmEffect1.Maximum = 100
        nmEffect1.Value = 10
        lbEffect1.Text = "Wavelength"
        lbEffect1.Visible = True

        nmEffect2.Visible = True
        nmEffect2.Minimum = -180
        nmEffect2.Maximum = 180
        nmEffect2.Value = 0
        lbEffect2.Text = "Angle"
        lbEffect2.Visible = True

        nmEffect3.Visible = True
        nmEffect3.Minimum = -50
        nmEffect3.Maximum = 50
        nmEffect3.Value = 50
        lbEffect3.Text = "Horizontal <=> Vertical"
        lbEffect3.Visible = True

      Case "wind"
        nmEffect0.Visible = True
        nmEffect0.Minimum = 1
        nmEffect0.Maximum = 300
        nmEffect0.Value = 100
        lbEffect0.Text = "Wind Strength"
        lbEffect0.Visible = True

        nmEffect1.Visible = True
        nmEffect1.Minimum = -180
        nmEffect1.Maximum = 180
        nmEffect1.Value = -10
        lbEffect1.Text = "Angle"
        lbEffect1.Visible = True

        nmEffect2.Visible = True
        nmEffect2.Minimum = 1
        nmEffect2.Maximum = 100
        nmEffect2.Value = 25
        lbEffect2.Text = "Opacity"
        lbEffect2.Visible = True

      Case "zoomwave"
        nmEffect0.Visible = True
        nmEffect0.Minimum = 1
        nmEffect0.Maximum = 300
        nmEffect0.Value = 100
        lbEffect0.Text = "Wave Amplitude"
        lbEffect0.Visible = True

        nmEffect1.Visible = True
        nmEffect1.Minimum = 1
        nmEffect1.Maximum = 200
        nmEffect1.Value = 50
        lbEffect1.Text = "Number of Waves"
        lbEffect1.Visible = True

        nmEffect3.Visible = True
        nmEffect3.Minimum = -360
        nmEffect3.Maximum = 360
        nmEffect3.Value = -10
        lbEffect3.Text = "Radial Angle"
        lbEffect3.Visible = True

        nmEffect4.Visible = True
        nmEffect4.Minimum = 0
        nmEffect4.Maximum = 100
        nmEffect4.Value = 50
        lbEffect4.Text = "Zoom Factor"
        lbEffect4.Visible = True

        cmdColor0.Visible = True
        cmdColor0.BackColor = mBackColor
        lbColor0.Visible = True
        lbColor0.Text = "Background Color"

        aView.lbPoint.Text = "Click on the photo above to select the wave center."
        aView.lbPoint.Visible = True

      Case "warp"
        nmEffect0.Visible = True
        nmEffect0.Minimum = -100
        nmEffect0.Maximum = 100
        nmEffect0.Value = 60
        lbEffect0.Text = "Amount of Warping"
        lbEffect0.Visible = True
        trkEffect0.LargeChange = 10

        optEffect0.Text = "Spherical Warp"
        optEffect0.Visible = True
        optEffect0.Checked = True
        optEffect1.Text = "Planar Warp"
        optEffect1.Visible = True
        optEffect1.Checked = False
        optEffect2.Text = "Cylindrical Warp"
        optEffect2.Visible = True
        optEffect2.Checked = False

        aView.lbPoint.Text = "Click on the photo above to select the center point."
        aView.lbPoint.Visible = True

      Case "lens"
        nmEffect0.Visible = True
        nmEffect0.Minimum = 0
        nmEffect0.Maximum = qImage.Height \ 2
        nmEffect0.Value = qImage.Height \ 8
        lbEffect0.Text = "Radius"
        lbEffect0.Visible = True
        trkEffect0.LargeChange = 100

        nmEffect1.Visible = True
        nmEffect1.Minimum = 0
        nmEffect1.Maximum = 100
        nmEffect1.Value = 80
        lbEffect1.Text = "Strength"
        lbEffect1.Visible = True
        trkEffect1.LargeChange = 10

        aView.lbPoint.Text = "Click on the photo above to select the center point."
        aView.lbPoint.Visible = True

      Case "psycho"
        nmEffect0.Visible = True
        nmEffect0.Minimum = 1
        nmEffect0.Maximum = 100
        nmEffect0.Value = 40
        lbEffect0.Text = "Value"
        lbEffect0.Visible = True
        trkEffect0.LargeChange = 10

        nmEffect1.Visible = True
        nmEffect1.Minimum = 0
        nmEffect1.Maximum = 200
        nmEffect1.Value = 100
        lbEffect1.Text = "Value"
        lbEffect1.Visible = True
        trkEffect1.LargeChange = 20

        aView.lbPoint.Visible = False

      Case Else
        MsgBox("Missing effect (setupsliders): " & cmbEffect.SelectedIndex)

    End Select

    trkEffect0.Visible = nmEffect0.Visible
    trkEffect0.Minimum = nmEffect0.Minimum
    trkEffect0.Maximum = nmEffect0.Maximum
    trkEffect0.Value = nmEffect0.Value

    trkEffect1.Visible = nmEffect1.Visible
    trkEffect1.Minimum = nmEffect1.Minimum
    trkEffect1.Maximum = nmEffect1.Maximum
    trkEffect1.Value = nmEffect1.Value

    trkEffect2.Visible = nmEffect2.Visible
    trkEffect2.Minimum = nmEffect2.Minimum
    trkEffect2.Maximum = nmEffect2.Maximum
    trkEffect2.Value = nmEffect2.Value

    trkeffect3.Visible = nmEffect3.Visible
    trkeffect3.Minimum = nmEffect3.Minimum
    trkeffect3.Maximum = nmEffect3.Maximum
    trkeffect3.Value = nmEffect3.Value

    trkeffect4.Visible = nmEffect4.Visible
    trkeffect4.Minimum = nmEffect4.Minimum
    trkeffect4.Maximum = nmEffect4.Maximum
    trkeffect4.Value = nmEffect4.Value

    trkeffect5.Visible = nmEffect5.Visible
    trkeffect5.Minimum = nmEffect5.Minimum
    trkeffect5.Maximum = nmEffect5.Maximum
    trkeffect5.Value = nmEffect5.Value

    trkEffect0.Refresh()
    trkEffect1.Refresh()
    trkEffect2.Refresh()
    trkeffect3.Refresh()
    trkeffect4.Refresh()
    trkeffect5.Refresh()
    nmEffect0.Refresh()
    nmEffect1.Refresh()
    nmEffect2.Refresh()
    nmEffect3.Refresh()
    nmEffect4.Refresh()
    nmEffect5.Refresh()
    lbEffect0.Refresh()
    lbEffect1.Refresh()
    lbEffect2.Refresh()
    lbEffect3.Refresh()
    lbEffect4.Refresh()
    lbEffect5.Refresh()
    cmdColor0.Refresh()
    lbColor0.Refresh()
    chkEffect0.Refresh()
    chkEffect1.Refresh()
    optEffect0.Refresh()
    optEffect1.Refresh()
    optEffect2.Refresh()

    Sliding = False

  End Sub

  Sub drawEffect(ByVal fullBitmap As Boolean)

    Static Busy As Boolean = False
    Dim radius As Double
    Dim img As MagickImage
    Dim iStrength As Double
    Dim imgs As New MagickImageCollection
    Dim imgz As Collections.Generic.IEnumerable(Of MagickImage)
    Dim imgs2 As New MagickImageCollection
    Dim pCent As Point
    Dim bbSource(), bbTarget() As Byte

    Dim diameter, iOff, sourceOffset As Integer
    Dim ix1, iy1 As Integer
    Dim k As Integer

    If aView.pView0.Bitmap Is Nothing Then Exit Sub
    If Busy Then Exit Sub
    Busy = True

    Me.Cursor = Cursors.WaitCursor

    If fullBitmap Then
      pCent = aView.pCenter
      aView.pView1.ClearSelection()
      img = New MagickImage(qImage)

    Else
      ' only operate on the visible part of the bitmap
      ' use a floater image of the bitmap on the target
      ' this creates a region for clipping in setFloaterBitmap below
      aView.pView0.SetSelection(aView.pView0.ClientRectangle)
      aView.pView0.InitFloater()
      aView.pView0.FloaterVisible = False
      aView.pView0.FloaterOutline = False
      aView.pView1.SetSelection(aView.pView1.ClientRectangle)
      aView.pView1.InitFloater()
      aView.pView1.FloaterVisible = True
      aView.pView1.FloaterOutline = False
      img = bitmapToMagick(aView.pView0.FloaterBitmap)
      pCent.X = aView.pCenter.X - aView.pView0.FloaterPosition.X
      pCent.Y = aView.pCenter.Y - aView.pView0.FloaterPosition.Y
    End If

    '    If False And aView.lbPoint.Visible Then
    'r = aView.pView0.ControlToBitmap(aView.pView0.ClientRectangle)
    'If gPath IsNot Nothing Then
    ' pCent.X = Max(r.X, pCent.X)
    ' pCent.X = Min(r.X + r.Width - 1, pCent.X)
    ' pCent.Y = Max(r.Y, pCent.Y)
    ' pCent.Y = Min(r.Y + r.Height - 1, pCent.Y)
    ' Else
    ' pCent.X = Max(0, pCent.X)
    ' pCent.X = Min(aView.pView1.Bitmap.Width - 1, pCent.X)
    ' pCent.Y = Max(0, pCent.Y)
    ' pCent.Y = Min(aView.pView1.Bitmap.Height - 1, pCent.Y)
    ' End If
    ' End If

    img.HasAlpha = False

    If cmbEffect.SelectedIndex >= 0 Then
      Select Case effect(cmbEffect.SelectedIndex)
        Case "test"
          img.AddNoise(ImageMagick.NoiseType.Random)
          saveStuff(img, aView.pView1, gPath, fullBitmap)

        Case "charcoal"
          img.Charcoal(nmEffect0.Value / 4, 1)
          saveStuff(img, aView.pView1, gPath, fullBitmap)

        Case "psycho"
          Dim p As ImageMagick.Percentage
          p = nmEffect1.Value
          img.Modulate(100, 100, p)
          img.Evaluate(Channels.RGB, EvaluateOperator.Sine, nmEffect0.Value / 5)
          saveStuff(img, aView.pView1, gPath, fullBitmap)

        Case "pencil"
          Using img2 As New MagickImage(Color.Black, img.Width, img.Height)
            img.Grayscale(PixelIntensityMethod.Average)
            img2.Grayscale(PixelIntensityMethod.Average)
            img2.AddNoise(NoiseType.Impulse)
            img2.VirtualPixelMethod = VirtualPixelMethod.Tile
            img2.MotionBlur(0, 20, 125)
            img2.Charcoal(nmEffect0.Value / 20, 1)
            img2.Distort(DistortMethod.Arc, {90, 50, img.Width, 0})
            img2.Composite(img, CompositeOperator.Blend)

            img.Charcoal(nmEffect1.Value / 20, 1)
            img.Composite(img2, CompositeOperator.Multiply)
            saveStuff(img, aView.pView1, gPath, fullBitmap)
          End Using

        Case "coloredpencil"
          imgz = img.Separate(Channels.RGB)
          For i As Integer = 0 To imgz.Count - 1
            Using img2 As New MagickImage(Color.Black, img.Width, img.Height),
                imgc As MagickImage = imgz(i)
              img2.Grayscale(PixelIntensityMethod.Average)
              img2.AddNoise(NoiseType.Impulse)
              img2.VirtualPixelMethod = VirtualPixelMethod.Tile
              img2.MotionBlur(0, 20, 125)
              img2.Charcoal(nmEffect0.Value / 20, 1)
              img2.Distort(DistortMethod.Arc, {90, i * 42, imgc.Width, 0})
              img2.Composite(imgc, CompositeOperator.Blend)

              imgc.Charcoal(nmEffect1.Value / 20, 1)
              imgc.Composite(img2, CompositeOperator.Multiply)
              imgs.Add(imgc.Clone)
            End Using
          Next i

          img = imgs.Combine
          For Each imgc As MagickImage In imgs : clearBitmap(imgc) : Next imgc
          saveStuff(img, aView.pView1, gPath, fullBitmap)

        Case "flare"
          Dim flareSize As Integer = 80
          Dim flareCenter As Integer = 4
          Dim circ As New ImageMagick.DrawableCircle(flareSize, flareSize, flareSize + flareCenter, flareSize)
          Dim circLineColor As New ImageMagick.DrawableStrokeColor(Color.White)
          Dim circLineWidth As New ImageMagick.DrawableStrokeWidth(1)
          Dim circFillColor As New ImageMagick.DrawableFillColor(Color.White)

          Using img2 As New MagickImage("c:\tmp\tmp.jpg")
            img.Composite(img2, CompositeOperator.Screen)
          End Using
          saveStuff(img, aView.pView1, gPath, fullBitmap)

        Case "impressionist"
          Dim bmpq As Bitmap

          bmpq = img.ToBitmap
          Set32bppPArgb(bmpq)
          clearBitmap(img)

          bbSource = getBmpBytes(bmpq)
          bbTarget = bbSource.Clone

          diameter = nmEffect0.Value * 2
          iOff = 0
          For iy As Integer = 0 To bmpq.Height - 1
            For ix As Integer = 0 To bmpq.Width - 1
              ' k is the original position with random offset in x and y
              ix1 = ix + Ceiling(Rnd() * diameter) - diameter \ 2
              If ix1 < 0 Then ix1 = 0 Else If ix1 >= bmpq.Width Then ix1 = bmpq.Width - 1

              iy1 = iy + Ceiling(Rnd() * diameter) - diameter \ 2
              If iy1 < 0 Then iy1 = 0 Else If iy1 >= bmpq.Height Then iy1 = bmpq.Height - 1

              SourceOffset = iy1 * bmpq.Width * 4 + ix1 * 4

              bbTarget(iOff) = bbSource(sourceoffset)
              bbTarget(iOff + 1) = bbSource(sourceoffset + 1)
              bbTarget(iOff + 2) = bbSource(sourceoffset + 2)
              iOff = iOff + 4
            Next ix
          Next iy

          setBmpBytes(bmpq, bbTarget)
          saveStuff(bmpq, aView.pView1, gPath, fullBitmap)

        Case "ocean"
          Dim w, h As Double
          w = img.Width
          h = img.Height

          Dim imgw As New MagickImage("Gradient:", w, h)
          Dim img2 As MagickImage
          Dim imgHighlight As MagickImage
          Dim waveHeight, hz, Darkness As Integer
          Dim reflectScale As Double
          Dim iRed, iGreen, iBlue As Integer

          waveHeight = nmEffect0.Value / 5
          hz = nmEffect1.Value
          reflectScale = nmEffect3.Value / 100
          Darkness = -nmEffect4.Value

          img2 = New MagickImage("Gradient:black-gray", w, h)
          imgw.Evaluate(Channels.RGB, EvaluateOperator.Sine, hz)


          imgw.Composite(imgw, 0, 0, CompositeOperator.Displace, Str(waveHeight) & "x" & Str(waveHeight))
          imgw.Composite(img2, CompositeOperator.Multiply)
          imgw.Flip()
          imgw.Composite(img2, CompositeOperator.Plus)
          imgHighlight = New MagickImage(imgw.Clone)

          img2.Dispose()
          img2 = New MagickImage(img.Clone)
          img2.Composite(imgw, 0, 0, CompositeOperator.Displace, Str(h * waveHeight / 50) & "x" & Str(h * waveHeight / 50))
          imgw.Dispose()

          img2.Flip()
          img2.BrightnessContrast(Darkness, Darkness)

          Try
            img2.BackgroundColor = Color.Gray

            iRed = (cmdColor0.BackColor.R - 128) * 0.3 + 120
            iGreen = (cmdColor0.BackColor.G - 128) * 0.3 + 120
            iBlue = (cmdColor0.BackColor.B - 128) * 0.3 + 120
            img2.Tint(iRed & "," & iGreen & "," & iBlue)
          Catch ex As Exception
            MsgBox(ex.Message)
          End Try

          Using bmp As New Bitmap(w, h + h * reflectScale, PixelFormat.Format32bppPArgb),
              g As Graphics = Graphics.FromImage(bmp)
            g.DrawImage(img.ToBitmap, New Rectangle(0, 0, w, h))
            g.DrawImage(img2.ToBitmap, New Rectangle(0, h, w, h * reflectScale))
            saveStuff(bmp, aView.pView1, gPath, fullBitmap)
          End Using

          img2.Dispose()


        Case "wind"
          img.MotionBlur(0, img.Width / 50, 5)
          saveStuff(img, aView.pView1, gPath, fullBitmap)

        Case "lens"
          radius = trkEffect0.Value
          iStrength = trkEffect1.Value
          Lens(img, pCent, radius, iStrength)
          saveStuff(img, aView.pView1, gPath, fullBitmap)

        Case "fog"
          Dim cellx, celly As Integer
          Dim iSize, Density, Darkness, Thickness As Integer
          Dim ix, iy, iw, ih As Integer
          Dim pts As New List(Of Point)
          Dim r As Rectangle
          Dim fogColor As Color
          Dim bmpq, bmpz As Bitmap

          bmpz = New Bitmap(img.Width, img.Height, PixelFormat.Format32bppPArgb)

          Using g As Graphics = Graphics.FromImage(bmpz)

            Thickness = nmEffect0.Value
            Density = nmEffect1.Value * 255 / nmEffect1.Maximum
            Darkness = nmEffect2.Value * 127 / nmEffect2.Maximum
            iSize = nmEffect3.Value * 10

            g.Clear(Color.FromArgb(0, 0, 0, 0))

            cellx = qImage.Width / iSize
            celly = qImage.Height / iSize
            ' make fog cells, random shapes
            For i As Integer = 1 To cellx ^ 2 * Thickness
              ix = Rnd() * qImage.Width : iy = Rnd() * qImage.Height
              iw = Ceiling(iSize * Rnd() * 2) : ih = Ceiling(iSize * Rnd() * 2) ' width and height is up to 2x cell size
              r = New Rectangle(ix - iw \ 2, iy - ih \ 2, iw, ih)
              k = 128 - Rnd() * Darkness
              fogColor = Color.FromArgb(Density, k, k, k)
              pts = New List(Of Point)
              For j As Integer = 1 To Rnd() * 6 + 4
                pts.Add(New Point(r.X + (iw * Rnd() - cellx \ 2), r.Y + (ih) * Rnd() - celly \ 2))
              Next j
              g.FillPolygon(New SolidBrush(fogColor), pts.ToArray)
            Next i
          End Using

          bmpq = img.ToBitmap
          Set32bppPArgb(bmpq)

          clearBitmap(img)
          img = New MagickImage(bmpz)
          img.Blur(0, iSize / 5, Channels.All)
          bmpz = img.ToBitmap()

          Using g As Graphics = Graphics.FromImage(bmpq)
            g.DrawImage(bmpz, New Rectangle(0, 0, bmpq.Width, bmpq.Height))
          End Using

          saveStuff(bmpq, aView.pView1, gPath, fullBitmap)

          clearBitmap(bmpz)

        Case "glass"

          Using bmpz As Bitmap = img.ToBitmap,
            bmpq As Bitmap = New Bitmap(img.Width, img.Height, PixelFormat.Format32bppPArgb),
            g As Graphics = Graphics.FromImage(bmpq)

            Dim iSize As Integer

            iSize = nmEffect0.Value * 2 * aView.pView0.ZoomFactor
            If iSize <= 0 Then iSize = 2

            ' copy each cell twice, reduce size by half
            For iy As Integer = 0 To bmpq.Height - iSize + 1 Step iSize
              For ix As Integer = 0 To bmpq.Width - iSize + 1 Step iSize
                g.DrawImage(bmpz, New Rectangle(ix, iy, iSize, iSize), New Rectangle(ix, iy, iSize * 2, iSize * 2), GraphicsUnit.Pixel)
                g.DrawImage(bmpz, New Rectangle(ix + iSize \ 2, iy, iSize, iSize), New Rectangle(ix, iy, iSize * 2, iSize * 2), GraphicsUnit.Pixel)
              Next ix

              For ix As Integer = 0 To bmpq.Width - iSize + 1 Step iSize
                g.DrawImage(bmpz, New Rectangle(ix, iy + iSize \ 2, iSize, iSize), New Rectangle(ix, iy, iSize * 2, iSize * 2), GraphicsUnit.Pixel)
                g.DrawImage(bmpz, New Rectangle(ix + iSize \ 2, iy + iSize \ 2, iSize, iSize), New Rectangle(ix, iy, iSize * 2, iSize * 2), GraphicsUnit.Pixel)
              Next ix
            Next iy

            saveStuff(bmpq, aView.pView1, gPath, fullBitmap)
          End Using

        Case "polar"
          img.BackgroundColor = cmdColor0.BackColor
          img.VirtualPixelMethod = VirtualPixelMethod.Background
          If optEffect0.Checked Then
            img.Distort(DistortMethod.Polar, {0, 0, img.Width / 2, img.Height / 2, -180, 180})
          Else
            img.Distort(DistortMethod.DePolar, {0, 0, img.Width / 2, img.Height / 2, -180, 180})
          End If
          saveStuff(img, aView.pView1, gPath, fullBitmap)


        Case "puzzle"

          ' random cell placement
          Using bmpz As Bitmap = img.ToBitmap,
            bmpq As Bitmap = New Bitmap(img.Width, img.Height, PixelFormat.Format32bppPArgb),
            g As Graphics = Graphics.FromImage(bmpq)

            Dim w, h As Integer
            Dim rix As New List(Of Integer)
            Dim nxCell, nyCell, xSize, ySize As Integer
            Dim rCell As New List(Of Rectangle)

            nxCell = nmEffect0.Value * bmpq.Width / qImage.Width
            nyCell = nmEffect1.Value * bmpq.Height / qImage.Height
            If nxCell < 1 Then nxCell = 1 Else If nxCell > nmEffect0.Value Then nxCell = nmEffect0.Value
            If nyCell < 1 Then nyCell = 1 Else If nyCell > nmEffect1.Value Then nyCell = nmEffect1.Value
            xSize = bmpq.Width \ nxCell
            ySize = bmpq.Height \ nyCell

            ' get rects for puzzle pieces
            For iy As Integer = 0 To nyCell - 1
              For ix As Integer = 0 To nxCell - 1
                w = xSize : h = ySize
                'If ix = nxCell - 1 Then w = bmpq.Width - ix * xSize
                'If iy = nyCell - 1 Then h = bmpq.Height - iy * ySize
                rCell.Add(New Rectangle(ix * xSize, iy * ySize, w, h))
                rix.Add(rix.Count)
              Next ix
            Next iy

            shuffle(rix)

            ' copy each cell to random locatinos in bmpqtwice, reduce size by half
            For i As Integer = 0 To rCell.Count - 1
              g.DrawImage(bmpz, rCell(i), rCell(rix(i)), GraphicsUnit.Pixel)
            Next i

            saveStuff(bmpq, aView.pView1, gPath, fullBitmap)
          End Using


        Case "warp"


        Case "bricks"
        Case "segment"
        Case "swirl"
        Case "wave"
        Case "zoomwave"

        Case Else
          MsgBox("Missing effect: " & cmbEffect.SelectedIndex)

      End Select
    End If

    aView.Repaint()
    aView.zoomLabel()

    Me.Cursor = Cursors.Default
    Busy = False

  End Sub


  Sub Lens(ByRef img As MagickImage, pCenter As Point, r As Double, iStrength As Integer)
    ' strength should be 0 to 100, corresponds to 4 to .2 exponent
    ' t is radius, pCenter is the center of the lens

    Dim i, j, ix, iy As Integer
    Dim d, dx, dy, x As Double
    Dim Strength As Double
    Strength = 0.038 * -iStrength + 4 ' (.2 is the strongest, 4 the weakest)

    Using bmp As Bitmap = img.ToBitmap, bmp2 As Bitmap = bmp.Clone
      ' bmp2 is target, bmp is source
      For i = 0 To bmp.Width - 1
        For j = 0 To bmp.Height - 1
          dx = i - pCenter.X : dy = j - pCenter.Y
          d = Sqrt(dx * dx + dy * dy)
          If d <= r Then
            x = (1 - d / r) ^ Strength
            ix = i - x * dx : iy = j - x * dy
            If ix < 0 Then ix = 0 Else If ix >= bmp.Width Then ix = bmp.Width - 1
            If iy < 0 Then iy = 0 Else If iy >= bmp.Height Then iy = bmp.Height - 1
            bmp2.SetPixel(i, j, bmp.GetPixel(ix, iy))
          End If
        Next j
      Next i

      img.Dispose() : img = New MagickImage(bmp2)

    End Using


  End Sub


  Private Sub chkEffect0_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) _
    Handles chkEffect0.CheckedChanged, chkEffect1.CheckedChanged

    If Sliding Then Exit Sub
    drawEffect(False)

  End Sub

  Private Sub optEffect0_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) _
    Handles optEffect0.CheckedChanged, optEffect1.CheckedChanged, optEffect2.CheckedChanged

    If Sliding Then Exit Sub
    If sender.checked Then
      If effect(cmbEffect.SelectedIndex) = "warp" Then
        If sender Is optEffect2 Then aView.lbPoint.Visible = False Else aView.lbPoint.Visible = True ' no point for cylinder warp
      End If
      drawEffect(False)
    End If

  End Sub

  Private Sub cmdColor0_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmdColor0.Click
    cmdColor0.BackColor = getColor(cmdColor0.BackColor, colorDialog1)
    If Sliding Then Exit Sub
    drawEffect(False)
  End Sub

  Private Sub cmbEffect_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEffect.SelectedIndexChanged

    If Sliding Then Exit Sub
    setupSliders()
    drawEffect(False)
  End Sub

  Function scaled(ByVal x As Double, ByVal nm As NumericUpDown) As Double
    scaled = x * xReduced
    scaled = Max(scaled, nm.Minimum)
    scaled = Min(scaled, nm.Maximum)
  End Function

  Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
    ' redraw after some milliseconds
    Timer1.Stop()
    drawEffect(False)
  End Sub

  Private Sub frmBlur_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
    Timer1.Stop()
    If gPath IsNot Nothing Then gPath.Dispose()
  End Sub

End Class