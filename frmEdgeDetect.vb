Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports ImageMagick

Public Class frmEdgeDetect
  Inherits Form

  Dim Loading As Boolean = True
  Dim Sliding As Boolean = False

  Dim gpath As GraphicsPath = Nothing

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

    drawEdgeDetect(True)

    Me.Cursor = Cursors.Default
    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Private Sub aview_Resize(sender As Object, e As EventArgs) Handles aView.Resize
    aView.zoom(1)
  End Sub

  Sub aview_zoomed() Handles aView.Zoomed
    timer1.Stop() : timer1.Interval = 150 : timer1.Start() ' calls drawsharp
  End Sub

  Private Sub frmEdgeDetect_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Loading = True

    trkIntensity.Value = 10 ' = 1
    nmIntensity.Value = trkIntensity.Value
    trkMinThreshold.Value = 10
    nmMinThreshold.Value = trkMinThreshold.Value
    trkMaxThreshold.Value = 30
    nmMaxThreshold.Value = trkMaxThreshold.Value

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

    Loading = False
    Sliding = False

  End Sub

  Private Sub cmbMethod_SelectedIndexChanged(sender As Object, e As EventArgs)
    Timer1.Interval = 150 : Timer1.Stop() : Timer1.Start() ' draw after 150 milliseconds
  End Sub

  Private Sub trkIntensity_scroll(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles trkIntensity.Scroll, trkMinThreshold.Scroll, trkMaxThreshold.Scroll

    Sliding = True

    Select Case Sender.name
      Case trkIntensity.Name
        nmIntensity.Value = trkIntensity.Value
      Case trkMinThreshold.Name
        nmMinThreshold.Value = trkMinThreshold.Value
      Case trkMaxThreshold.Name
        nmMaxThreshold.Value = trkMaxThreshold.Value
    End Select

    timer1.Interval = 150 : timer1.Stop() : timer1.Start() ' draw after 150 milliseconds
    Sliding = False
  End Sub

  Private Sub nmradius_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) _
    Handles nmIntensity.ValueChanged, nmMinThreshold.ValueChanged, nmMaxThreshold.ValueChanged

    If Sliding Or Loading Then Exit Sub

    Select Case sender.name
      Case nmIntensity.Name
        trkIntensity.Value = nmIntensity.Value
      Case nmMinThreshold.Name
        trkMinThreshold.Value = nmMinThreshold.Value
      Case nmMaxThreshold.Name
        trkMaxThreshold.Value = nmMaxThreshold.Value
    End Select

    timer1.Interval = 150 : timer1.Stop() : timer1.Start() ' draw after 150 milliseconds
  End Sub

  Sub drawEdgeDetect(fullBitmap As Boolean)

    Dim Radius As Double
    Dim Sigma As Double
    Dim minThreshold As ImageMagick.Percentage
    Dim maxThreshold As ImageMagick.Percentage
    Dim img As MagickImage

    Static busy As Boolean = False

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

    img.HasAlpha = False ' doesn't work with alpha mode

    Try
      Radius = 0
      Sigma = trkIntensity.Value / 10
      minThreshold = trkMinThreshold.Value
      maxThreshold = trkMaxThreshold.Value
      img.CannyEdge(0, Sigma, minThreshold, maxThreshold)

    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    saveStuff(img, aView.pView1, gpath, fullBitmap) ' save img to pview0 or qimage, depending on fullbitmap
    aView.Repaint()
    aView.zoomLabel()

    Me.Cursor = Cursors.Default
    busy = False

  End Sub

  Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles timer1.Tick
    ' redraw after some milliseconds
    timer1.Stop()
    drawEdgeDetect(False)
  End Sub

  Private Sub _FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
    timer1.Stop()
    If gpath IsNot Nothing Then gpath.Dispose()
  End Sub

End Class
