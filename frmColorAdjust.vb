Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports ImageMagick

Public Class frmColorAdjust

  Dim Loading As Boolean = True
  Dim Sliding As Boolean

  Dim gpath As GraphicsPath = Nothing

  Dim imageReduced As Boolean
  Dim xreduced As Double

  Dim WithEvents Timer1 As New Timer

  Dim trks(2, 2) As TrackBar
  Dim nms(2, 2) As NumericUpDown

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

    drawColors(True) ' saves result to qimage

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

  Private Sub frmColors_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Loading = True
    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Timer1.Enabled = False

    nms(1, 0) = nmMidRed
    nms(1, 1) = nmMidGreen
    nms(1, 2) = nmMidBlue
    trks(1, 0) = trkMidRed
    trks(1, 1) = trkMidGreen
    trks(1, 2) = trkMidBlue

    chkPreserveIntensity.Checked = iniAdjustPreserveIntensity

    If frmMain.mView.FloaterPath IsNot Nothing Then gpath = frmMain.mView.FloaterPath.Clone

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

    Loading = False
    Sliding = False

  End Sub

  Private Sub chkPreserveIntensity_CheckedChanged(sender As Object, e As EventArgs) Handles chkPreserveIntensity.CheckedChanged
    drawColors(False)
  End Sub

  Sub drawColors(fullbitmap As Boolean)

    Dim gammaRed, gammaGreen, gammaBlue As Double
    Dim histo(2, 255) As Integer ' histogram data
    Dim histXscale, histYscale As Double
    Dim bmp As Bitmap
    Static busy As Boolean = False
    Dim img As MagickImage

    If aView.pView0.Bitmap Is Nothing Then Exit Sub

    If busy Then Exit Sub
    busy = True

    Me.Cursor = Cursors.WaitCursor

    If fullbitmap Then
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

    '    If chkPreserveIntensity.Enabled AndAlso chkPreserveIntensity.Checked Then
    ' If iRed + iGreen + iBlue > 0 Then
    ' x = 300 / (iRed + iGreen + iBlue)
    ' iRed *= x
    ' iGreen *= x
    ' iBlue *= x
    ' End If
    ' End If

    gammaRed = (nmMidRed.Value / 100 + 1) ^ 3 ' gamma value for img.level function
    gammaGreen = (nmMidGreen.Value / 100 + 1) ^ 3
    gammaBlue = (nmMidBlue.Value / 100 + 1) ^ 3

    Try
      img.BackgroundColor = Color.White
      img.Level(New Percentage(0), New Percentage(100), gammaRed, Channels.Red)
      img.Level(New Percentage(0), New Percentage(100), gammaGreen, Channels.Green)
      img.Level(New Percentage(0), New Percentage(100), gammaBlue, Channels.Blue)

    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    saveStuff(img, aView.pView1, gpath, fullbitmap)

    bmp = aView.pView1.FloaterBitmap.Clone
    histo = getHisto(bmp)
    showHisto(pviewHisto, histo, histXscale, histYscale)


    aView.Repaint()
    aView.zoomLabel()

    Me.Cursor = Cursors.Default
    busy = False

  End Sub

  Private Sub trk_Scroll(ByVal sender As Object, ByVal e As EventArgs) _
    Handles trkMidRed.Scroll, trkMidGreen.Scroll, trkMidBlue.Scroll

    Dim trk As TrackBar

    If Sliding Or Loading Then Exit Sub
    trk = sender

    If trk Is trkMidRed Then nmMidRed.Value = trk.Value
    If trk Is trkMidGreen Then nmMidGreen.Value = trk.Value
    If trk Is trkMidBlue Then nmMidBlue.Value = trk.Value

    Timer1.Interval = 150 : Timer1.Stop() : Timer1.Start() ' draw after 150 milliseconds

  End Sub

  Private Sub nmShadowRed_ValueChanged(ByVal sender As System.Object, ByVal e As EventArgs) _
    Handles nmMidRed.ValueChanged, nmMidGreen.ValueChanged, nmMidBlue.ValueChanged

    If Sliding Or Loading Then Exit Sub
    Sliding = True
    If sender Is nmMidRed Then trkMidRed.Value = nmMidRed.Value
    If sender Is nmMidGreen Then trkMidGreen.Value = nmMidGreen.Value
    If sender Is nmMidBlue Then trkMidBlue.Value = nmMidBlue.Value
    Timer1.Interval = 150 : Timer1.Stop() : Timer1.Start() ' draw after 150 milliseconds
    Sliding = False
  End Sub

  Private Sub cmdReset_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmdReset.Click

    aView.pView1.setBitmap(aView.pView0.Bitmap)
    nmMidRed.Value = 0
    nmMidGreen.Value = 0
    nmMidBlue.Value = 0

  End Sub

  Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    Timer1.Stop()
    drawColors(False)
  End Sub

  Private Sub _FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
    Timer1.Stop()
    If gpath IsNot Nothing Then gpath.Dispose()
  End Sub

End Class
