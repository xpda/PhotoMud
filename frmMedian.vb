Imports vb = Microsoft.VisualBasic
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports ImageMagick

Public Class frmMedian
  Inherits Form

  Dim Loading As Boolean = True
  Dim sliding As Boolean = False

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

    drawMedian(True) ' saves result to qimage

    Me.Cursor = Cursors.Default
    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Private Sub aview_Resize(sender As Object, e As EventArgs) Handles aview.Resize
    aview.zoom(1)
  End Sub

  Sub aview_zoomed() Handles aview.Zoomed, Me.Resize
    Timer1.Stop() : Timer1.Interval = 150 : Timer1.Start() ' calls drawsharp
  End Sub

  Private Sub frmMedian_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Loading = True

    trkSample.Value = 3
    nmSample.Value = 3

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

    Loading = False
    sliding = False

  End Sub

  Private Sub trkSample_scroll(ByVal Sender As Object, ByVal e As EventArgs) Handles trkSample.Scroll
    sliding = True
    nmSample.Value = trkSample.Value
    Timer1.Interval = 150 : Timer1.Stop() : Timer1.Start() ' draw after 150 milliseconds
    sliding = False
  End Sub

  Private Sub nmSample_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles nmSample.ValueChanged
    If Not sliding Then
      trkSample.Value = nmSample.Value
      Timer1.Interval = 150 : Timer1.Stop() : Timer1.Start() ' draw after 150 milliseconds
    End If
  End Sub

  Sub drawMedian(ByRef fullBitmap As Boolean)

    Static busy As Boolean = False
    Dim i As Integer
    Dim img As MagickImage

    If aview.pView0.Bitmap Is Nothing Then Exit Sub
    If busy Then Exit Sub
    busy = True

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

    i = trkSample.Value * xReduced
    If i > 0 Then
      Try
        img.ReduceNoise(i)
      Catch ex As Exception
        MsgBox(ex.Message)
      End Try
    End If

    saveStuff(img, aview.pView1, gpath, fullBitmap)
    aview.Repaint()
    aview.zoomLabel()

    Me.Cursor = Cursors.Default
    busy = False

  End Sub

  Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
    ' redraw after some milliseconds
    Timer1.Stop()
    drawMedian(False)
  End Sub

  Private Sub _FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
    Timer1.Stop()
    If gpath IsNot Nothing Then gpath.Dispose() : gpath = Nothing
  End Sub

End Class