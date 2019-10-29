'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports ImageMagick

Public Class frmContrastStretch

  Inherits Form

  Dim clock As New Stopwatch

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

    drawStretch(True) ' saves result to qimage

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

  Private Sub form_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Loading = True

    aView.lbPoint.Visible = False

    trkWhite.Minimum = -100
    trkWhite.Maximum = 100
    trkWhite.Value = 0
    trkWhite.LargeChange = 10
    trkWhite.TickFrequency = 100
    trkWhite.TickStyle = TickStyle.BottomRight
    nmWhite.Minimum = trkWhite.Minimum
    nmWhite.Maximum = trkWhite.Maximum
    nmWhite.Value = trkWhite.Value

    trkBlack.Minimum = -100
    trkBlack.Maximum = 100
    trkBlack.Value = 0
    trkBlack.LargeChange = 10
    trkBlack.TickFrequency = 100
    trkBlack.TickStyle = TickStyle.BottomRight
    nmBlack.Minimum = trkBlack.Minimum
    nmBlack.Maximum = trkBlack.Maximum
    nmBlack.Value = trkBlack.Value

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


  Private Sub trkMax_scroll(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles trkWhite.Scroll, trkBlack.Scroll

    Sliding = True
    If Sender Is trkWhite Then
      nmWhite.Value = trkWhite.Value
    Else
      nmBlack.Value = trkBlack.Value
    End If

    Timer1.Interval = 150 : Timer1.Stop() : Timer1.Start() ' draw after 150 milliseconds
    Sliding = False

  End Sub

  Private Sub nmStretch_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) _
    Handles nmWhite.ValueChanged, nmBlack.ValueChanged

    If Sliding Or Loading Then Exit Sub
    If sender Is nmBlack Then
      trkBlack.Value = nmBlack.Value
    Else
      trkWhite.Value = nmWhite.Value
    End If

    Timer1.Interval = 150 : Timer1.Stop() : Timer1.Start() ' draw after 150 milliseconds
  End Sub

  Sub drawStretch(ByRef fullBitmap As Boolean)
    ' fullbitmap tells whether to refresh the entire bitmap or just the visible part.
    ' gimage is the optional output image, for final drawing when a temporary reduced size image is used.

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
      ''`img.ContrastStretch(trkBlack.Value, trkWhite.Value)
      ''`img.SigmoidalContrast(chkSharpen.Checked, trkBlack.Value / 10, trkWhite.Value * 655.35)
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    saveStuff(img, aView.pView1, gpath, fullBitmap)

    Dim bb() As Byte
    bb = getBmpBytes(aView.pView1.Bitmap) ' values are b, g, r, A
    For i As Integer = 2 To UBound(bb) Step 4
      bb(i) = CByte(Min(CInt(bb(i)) * 3, 255))
    Next i
    setBmpBytes(aView.pView1.Bitmap, bb)


    aView.Repaint()
    aView.zoomLabel()

    Me.Cursor = Cursors.Default
    busy = False

  End Sub

  Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
    ' redraw after some milliseconds
    Timer1.Stop()
    drawStretch(False)
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
