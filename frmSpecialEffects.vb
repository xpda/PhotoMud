'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports System.Collections.Generic
Imports ImageMagick

Public Class frmSpecialEffects
  Inherits Form

  Dim loading As Boolean = True
  Dim Sliding As Boolean

  Dim gpath As GraphicsPath = Nothing

  Dim imageReduced As Boolean
  Dim xReduced As Double

  Dim WithEvents Timer1 As New Timer
  
  Private Sub cmdCancel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
    clearbitmap(qimage)
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

    Me.Cursor = Cursors.Default
    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Private Sub aview_Resize(sender As Object, e As EventArgs) Handles aview.Resize
    aView.zoom(1)
  End Sub

  Sub aview_zoomed() Handles aview.Zoomed
    timer1.Stop() : timer1.Interval = 150 : timer1.Start() ' calls drawsharp
  End Sub

  Private Sub frmSpecialEffects_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    loading = True

    Combo1.Left = trkEffect1.Left
    Combo1.Top = trkEffect1.Top

    setupSliders()

    If frmMain.mView.FloaterPath IsNot Nothing Then gpath = frmMain.mView.FloaterPath.Clone

    ' if it's too big, reduce the size and assign that.
    If qImage.Width * qImage.Height > bigMegapix Then
      imageReduced = True
      xReduced = getSmallerImage(qImage, aview.pView0)
      aview.pView1.setBitmap(aview.pView0.Bitmap)
    Else
      imageReduced = False
      aview.pView0.setBitmap(qImage)
      aview.pView1.setBitmap(qImage)
      xReduced = 1
    End If

    aview.ZoomViews(0.5)

    loading = False
    Sliding = False


  End Sub

  Private Sub Opt_CheckedChanged(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles optEmboss.CheckedChanged, optPosterize.CheckedChanged, optPixelate.CheckedChanged, optSolarize.CheckedChanged, _
    optIntensityDetect.CheckedChanged, optAddNoise.CheckedChanged

    If loading Then Exit Sub

    If Sender.Checked Then
      setupSliders()
      drawEffect(False)

      Sliding = False
    End If
  End Sub

  Sub setupSliders()

    Dim ss() As String

    Sliding = True

    trkEffect0.Visible = False
    lbEffect0.Visible = False
    trkEffect1.Visible = False
    lbEffect1.Visible = False
    Combo1.Visible = False

    If optEmboss.Checked Then
      lbEffect0.Text = "Angle"
      lbEffect0.Visible = True
      trkEffect0.Minimum = 0
      trkEffect0.Maximum = 360
      trkEffect0.Value = 30
      trkEffect0.LargeChange = 30
      trkEffect0.Visible = True

      lbEffect1.Text = "Depth"
      lbEffect1.Visible = True
      trkEffect1.Minimum = 1
      trkEffect1.Maximum = 10
      trkEffect1.Value = 4
      trkEffect1.LargeChange = 1
      trkEffect1.Visible = True

    ElseIf optPosterize.Checked Then
      lbEffect0.Text = "&Color Levels"
      lbEffect0.Visible = True
      trkEffect0.Minimum = 2
      trkEffect0.Maximum = 64
      trkEffect0.Value = 5
      trkEffect0.LargeChange = 8
      trkEffect0.Visible = True

    ElseIf optPixelate.Checked Then
      lbEffect0.Text = "&Element Size (pixels)"
      lbEffect0.Visible = True
      trkEffect0.Minimum = 2
      trkEffect0.Maximum = 250
      trkEffect0.Value = 20
      trkEffect0.LargeChange = 10
      trkEffect0.Visible = True

    ElseIf optSolarize.Checked Then
      lbEffect0.Text = "&Threshold (percent)"
      lbEffect0.Visible = True
      trkEffect0.Minimum = 0
      trkEffect0.Maximum = 100
      trkEffect0.Value = 90
      trkEffect0.LargeChange = 8
      trkEffect0.Visible = True

    ElseIf optIntensityDetect.Checked Then
      lbEffect0.Text = "&Upper Threshold"
      lbEffect0.Visible = True
      trkEffect0.Minimum = 0
      trkEffect0.Maximum = 100
      trkEffect0.Value = 80
      trkEffect0.LargeChange = 10
      trkEffect0.Visible = True

      lbEffect1.Text = "&Lower Threshold"
      lbEffect1.Visible = True
      trkEffect1.Minimum = 0
      trkEffect1.Maximum = 100
      trkEffect1.Value = 20
      trkEffect1.LargeChange = 10
      trkEffect1.Visible = True

    ElseIf optAddNoise.Checked Then
      lbEffect0.Text = "Amount of &Noise"
      lbEffect0.Visible = True
      trkEffect0.Minimum = 1
      trkEffect0.Maximum = 8
      trkEffect0.Value = 1
      trkEffect0.LargeChange = 1
      trkEffect0.Visible = True

      Combo1.Items.Clear()
      ss = System.Enum.GetNames(GetType(ImageMagick.NoiseType))
      For Each s As String In ss ' load all options except undefined and uniform
        If s.ToLower <> "undefined" And s.ToLower <> "uniform" Then Combo1.Items.Add(s)
      Next s
      Combo1.SelectedIndex = 0
      Combo1.Visible = True

    End If

    nmEffect0.Visible = trkEffect0.Visible
    nmEffect0.Minimum = trkEffect0.Minimum
    nmEffect0.Maximum = trkEffect0.Maximum
    nmEffect0.Value = trkEffect0.Value
    nmEffect1.Visible = trkEffect1.Visible
    nmEffect1.Minimum = trkEffect1.Minimum
    nmEffect1.Maximum = trkEffect1.Maximum
    nmEffect1.Value = trkEffect1.Value

  End Sub

  Private Sub trkEffect_scroll(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles trkEffect0.Scroll, trkEffect1.Scroll

    Dim trk As TrackBar

    If Sliding Or aview.pView0.Bitmap Is Nothing Then Exit Sub
    Sliding = True
    trk = Sender

    If optIntensityDetect.Checked Then ' intensity detect -- make sure min <= max
      If trk Is trkEffect0 Then
        If trkEffect1.Value > trkEffect0.Value Then trkEffect0.Value = trkEffect1.Value
      Else
        If trkEffect1.Value > trkEffect0.Value Then trkEffect1.Value = trkEffect0.Value
      End If
    End If

    If trk Is trkEffect0 Then
      nmEffect0.Value = trkEffect0.Value
    Else
      nmEffect1.Value = trkEffect1.Value
    End If

    Timer1.Interval = 150 : Timer1.Stop() : Timer1.Start() ' draweffect in 150 milliseconds
    Me.Cursor = Cursors.Default
    Sliding = False

  End Sub

  Private Sub nmEffect_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) _
    Handles nmEffect0.ValueChanged, nmEffect1.ValueChanged

    If Sliding Then Exit Sub

    If sender Is nmEffect0 Then
      trkEffect0.Value = nmEffect0.Value
    Else
      trkEffect1.Value = nmEffect1.Value
    End If

    Timer1.Interval = 150 : Timer1.Stop() : Timer1.Start() ' draweffect in 150 milliseconds

  End Sub

  Private Sub Combo1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Combo1.SelectedIndexChanged
    Timer1.Interval = 150 : Timer1.Stop() : Timer1.Start() ' draweffect in 150 milliseconds
  End Sub

  Sub drawEffect(ByVal fullBitmap As Boolean)

    Dim i As Integer
    Dim img As MagickImage
    Dim x As Double
    Dim dx, dy As Double
    Dim d As Percentage
    Dim Depth As Double
    Dim Angle As Double
    Dim ix1, iy1 As Integer
    Dim zz() As NoiseType

    Static Busy As Boolean = False

    If aview.pView0.Bitmap Is Nothing Then Exit Sub
    If Busy Then Exit Sub
    Busy = True

    Me.Cursor = Cursors.WaitCursor

    If fullBitmap Then
      aview.pView1.ClearSelection()
      img = New MagickImage(qImage)
    Else
      ' only operate on the visible part of the bitmap
      ' use a floater image of the bitmap on the target
      ' this creates a region for clipping in setFloaterBitmap below
      aview.pView0.SetSelection(aview.pView0.ClientRectangle)
      aview.pView0.InitFloater()
      aview.pView0.FloaterVisible = False
      aview.pView0.FloaterOutline = False
      aview.pView1.SetSelection(aview.pView1.ClientRectangle)
      aview.pView1.InitFloater()
      aview.pView1.FloaterVisible = True
      aview.pView1.FloaterOutline = False
      img = bitmapToMagick(aview.pView0.FloaterBitmap)
    End If

    img.HasAlpha = False ' doesn't work with alpha mode

    If optEmboss.Checked Then  ' emboss

      Depth = nmEffect1.Value
      Angle = nmEffect0.Value

      Using img2 As New MagickImage(img)
        img2.Negate()

        dy = Sin(Angle * pi / 180) * Depth * 0.5
        dx = Cos(Angle * pi / 180) * Depth * 0.5

        img.Distort(DistortMethod.ScaleRotateTranslate, {0, 0, 1, 0, dx, dy})
        img2.Distort(DistortMethod.ScaleRotateTranslate, {0, 0, 1, 0, -dx, -dy})
        img.Composite(img2, ImageMagick.Gravity.Center, CompositeOperator.Blend, "50%")
        img.AutoLevel()
      End Using

    ElseIf optPosterize.Checked Then  ' posterize
      i = nmEffect0.Value
      img.Posterize(i)

    ElseIf optPixelate.Checked Then  ' pixelate
      ix1 = img.Width
      iy1 = img.Height
      d = 100 / nmEffect0.Value
      img.Scale(d) ' percent
      img.Scale(ix1, iy1) ' original

    ElseIf optSolarize.Checked Then  ' solarize
      x = nmEffect0.Value * 655.35
      If x <= 0 Then x = 1
      img.Solarize(x)

    ElseIf optIntensityDetect.Checked Then  ' intensity detect

      Using img2 As New MagickImage(img)
        d = trkEffect0.Value
        img.Threshold(d)
        img.Negate()
        d = trkEffect1.Value
        img2.Threshold(d)
        img.Composite(img2, CompositeOperator.Multiply)
      End Using

    ElseIf optAddNoise.Checked Then  ' add noise
      zz = System.Enum.GetValues(GetType(ImageMagick.NoiseType))
      For Each z As NoiseType In zz
        If System.Enum.GetName(GetType(ImageMagick.NoiseType), z) = Combo1.Text Then ' found noise
          For i = 1 To trkEffect0.Value
            img.AddNoise(z)
          Next i
          Exit For
        End If
      Next z
    End If

    saveStuff(img, aview.pView1, gpath, fullBitmap) ' save img to pview0 or qimage, depending on fullbitmap
    aview.Repaint()
    aview.zoomLabel()

    Me.Cursor = Cursors.Default
    Busy = False

  End Sub

  Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
    ' redraw after some milliseconds
    Timer1.Stop()
    drawEffect(False)
  End Sub

  Private Sub form_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
    Timer1.Stop()
    If gpath IsNot Nothing Then gpath.Dispose() : gpath = Nothing
  End Sub

End Class