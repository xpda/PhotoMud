Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports ImageMagick

Public Class frmBlur
  Inherits Form

  Dim Loading As Boolean = True
  Dim Sliding As Boolean = False

  Dim pcenter As PointF
  Dim gPath As GraphicsPath = Nothing

  Dim imageReduced As Boolean
  Dim xreduced As Double

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

    drawBlur(True) ' saves result to qimage

    Me.Cursor = Cursors.Default
    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Private Sub aview_Resize(sender As Object, e As EventArgs) Handles aView.Resize
    aView.zoom(1)
  End Sub

  Sub aview_zoomed() Handles aView.Zoomed, Me.Resize
    Timer1.Stop() : Timer1.Interval = 150 : Timer1.Start() ' calls drawsharp
  End Sub

  Private Sub frmBlur_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Loading = True

    Option0.Checked = True ' default to average Filter
    aView.lbPoint.Visible = False

    trkBlur.Minimum = 0
    trkBlur.Maximum = 100
    trkBlur.Value = 10
    nmBlur.Minimum = trkBlur.Minimum
    nmBlur.Maximum = trkBlur.Maximum
    nmBlur.Value = trkBlur.Value

    trkAngle.Minimum = 0
    trkAngle.Maximum = 359
    trkAngle.Value = 30
    nmAngle.Minimum = trkAngle.Minimum
    nmAngle.Maximum = trkAngle.Maximum
    nmAngle.Value = trkAngle.Value

    If frmMain.mView.FloaterPath IsNot Nothing Then gPath = frmMain.mView.FloaterPath.Clone

    ' if it's too big, reduce the size and assign that.
    If qImage.Width * qImage.Height > bigMegapix Then
      imageReduced = True
      xreduced = getSmallerImage(qImage, aView.pView0)
      aView.pView1.setBitmap(aView.pView0.Bitmap)
    Else
      imageReduced = False
      aView.pView0.setBitmap(qImage)
      aView.pView1.setBitmap(qImage)
      xreduced = 1
    End If

    aView.ZoomViews(0.5)

    pcenter = New PointF(CSng(aView.pView0.Bitmap.Width / 2), CSng(aView.pView0.Bitmap.Height / 2))

    Loading = False
    Sliding = False

  End Sub

  Private Sub Option_Changed(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles Option0.CheckedChanged, Option1.CheckedChanged, Option2.CheckedChanged, Option3.CheckedChanged, Option4.CheckedChanged

    Dim opt As RadioButton = Sender

    If opt.Checked Then
      Sliding = True

      If Option0.Checked Then
        trkBlur.Minimum = 0
        trkBlur.Maximum = 100
        trkBlur.Value = 5
        lbBlur.Text = "Blur Amount"

        trkAngle.Visible = False
        lbAngle.Visible = False
        aView.lbPoint.Visible = False

      ElseIf Option1.Checked Then
        trkBlur.Minimum = 0
        trkBlur.Maximum = 100
        trkBlur.Value = 5
        lbBlur.Text = "Blur Amount"

        trkAngle.Visible = False
        lbAngle.Visible = False
        aView.lbPoint.Visible = False

      ElseIf Option2.Checked Then  ' motion blur
        trkBlur.Minimum = 0
        trkBlur.Maximum = 100
        trkBlur.Value = 5
        lbBlur.Text = "Blur Amount"

        trkAngle.Visible = True
        trkAngle.Minimum = -180
        trkAngle.Maximum = 180
        trkAngle.Value = 0
        lbAngle.Text = "Motion Angle"
        lbAngle.Visible = True

        aView.lbPoint.Visible = False

      ElseIf Option3.Checked Then  ' radial blur
        trkBlur.Minimum = 0
        trkBlur.Maximum = 100
        trkBlur.Value = 5
        lbBlur.Text = "Blur Amount"

        trkAngle.Visible = True
        trkAngle.Minimum = -180
        trkAngle.Maximum = 180
        trkAngle.Value = 0
        lbAngle.Text = "Angle"
        lbAngle.Visible = True

        aView.lbPoint.Visible = True

      ElseIf Option4.Checked Then  ' zoom blur
        trkBlur.Minimum = 0
        trkBlur.Maximum = 100
        trkBlur.Value = 40
        lbBlur.Text = "Amount"

        aView.lbPoint.Visible = True

      End If

      nmBlur.Visible = trkBlur.Visible
      nmBlur.Minimum = trkBlur.Minimum
      nmBlur.Maximum = trkBlur.Maximum
      nmBlur.Value = trkBlur.Value

      nmAngle.Visible = trkAngle.Visible
      nmAngle.Minimum = trkAngle.Minimum
      nmAngle.Maximum = trkAngle.Maximum
      nmAngle.Value = trkAngle.Value

      Sliding = False

      If Not Loading Then drawBlur(False)
    End If

  End Sub

  Private Sub trkBlur_scroll(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles trkBlur.Scroll, trkAngle.Scroll

    Sliding = True
    If Sender Is trkBlur Then
      nmBlur.Value = trkBlur.Value
    Else
      nmAngle.Value = trkAngle.Value
    End If

    Timer1.Interval = 150 : Timer1.Stop() : Timer1.Start() ' draw after 150 milliseconds
    Sliding = False

  End Sub

  Private Sub nmBlur_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) _
    Handles nmBlur.ValueChanged, nmAngle.ValueChanged

    If Sliding Or Loading Then Exit Sub
    If sender Is nmAngle Then
      trkAngle.Value = nmAngle.Value
    Else
      trkBlur.Value = nmBlur.Value
    End If

    Timer1.Interval = 150 : Timer1.Stop() : Timer1.Start() ' draw after 150 milliseconds
  End Sub

  Sub drawBlur(ByRef fullBitmap As Boolean)
    ' fullbitmap tells whether to refresh the entire bitmap or just the visible part.
    ' gimage is the optional output image, for final drawing when a temporary reduced size image is used.

    Dim sigma, radius, angle As Double
    Dim r As Rectangle
    Static busy As Boolean = False

    Dim img As MagickImage

    If aView.pView0.Bitmap Is Nothing Then Exit Sub

    If busy Then Exit Sub
    busy = True

    Me.Cursor = Cursors.WaitCursor

    If fullBitmap Then
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
    End If

    If aView.lbPoint.Visible Then
      r = aView.pView0.ControlToBitmap(aView.pView0.ClientRectangle)
      If gPath IsNot Nothing AndAlso gPath.PointCount > 0 Then
        pcenter.X = Max(r.X, pcenter.X)
        pcenter.X = Min(r.X + r.Width - 1, pcenter.X)
        pcenter.Y = Max(r.Y, pcenter.Y)
        pcenter.Y = Min(r.Y + r.Height - 1, pcenter.Y)
      Else
        pcenter.X = Max(0, pcenter.X)
        pcenter.X = Min(aView.pView1.Bitmap.Width - 1, pcenter.X)
        pcenter.Y = Max(0, pcenter.Y)
        pcenter.Y = Min(aView.pView1.Bitmap.Height - 1, pcenter.Y)
      End If
    End If

    Try

      If Option0.Checked Then ' blur
        sigma = trkBlur.Value / 15
        radius = 0
        img.Blur(radius, sigma, Channels.All)

      ElseIf Option1.Checked Then  ' Gaussian
        sigma = trkBlur.Value / 15
        radius = 0
        img.GaussianBlur(radius, sigma, Channels.All)

      ElseIf Option2.Checked Then  ' Motion
        sigma = trkBlur.Value / 5
        angle = trkAngle.Value
        radius = 0
        img.MotionBlur(radius, sigma, angle)

      ElseIf Option3.Checked Then  ' Rotational
        sigma = trkBlur.Value / 15
        angle = trkAngle.Value
        radius = 0
        img.RotationalBlur(angle, Channels.All)

      ElseIf Option4.Checked Then  ' Adaptive
        sigma = trkBlur.Value / 5
        radius = 0
        img.AdaptiveBlur(radius, sigma)
      End If

    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    saveStuff(img, aView.pView1, gpath, fullBitmap)
    aView.Repaint()
    aView.zoomLabel()

    Me.Cursor = Cursors.Default
    busy = False

  End Sub

  Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
    ' redraw after some milliseconds
    Timer1.Stop()
    drawBlur(False)
  End Sub

  Function scaled(ByVal x As Double, ByVal nm As NumericUpDown) As Double
    scaled = x * xreduced
    scaled = Max(scaled, nm.Minimum)
    scaled = Min(scaled, nm.Maximum)
  End Function

  Private Sub _FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
    Timer1.Stop()
    If gPath IsNot Nothing Then gPath.Dispose()
  End Sub

End Class

