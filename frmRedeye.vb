Imports vb = Microsoft.VisualBasic
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math

Public Class frmRedeye
  Inherits Form

  Dim loading As Boolean = True
  Dim Sliding As Boolean

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

    drawRedeye(True) ' saves result to qimage

    Me.Cursor = Cursors.Default
    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Private Sub aview_Resize(sender As Object, e As EventArgs) Handles aview.Resize
    aView.zoom(1)
  End Sub

  Sub aview_zoomed() Handles aview.Zoomed, Me.Resize
    Timer1.Stop() : Timer1.Interval = 150 : Timer1.Start() ' calls drawsharp
  End Sub

  Private Sub frmRedeye_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    nmThreshold.Value = 80
    trkThreshold.Value = 80

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

  Private Sub Trackbar_scroll(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles trkThreshold.Scroll

    Sliding = True
    nmThreshold.Value = trkThreshold.Value

    Timer1.Stop() : Timer1.Interval = 150 : Timer1.Start() ' calls drawredeye
    Sliding = False

  End Sub

  Private Sub nmthreshold_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles nmThreshold.ValueChanged
    If Sliding Then Exit Sub
    trkThreshold.Value = nmThreshold.Value
    Timer1.Stop() : Timer1.Interval = 150 : Timer1.Start() ' calls drawredeye
  End Sub

  Sub drawRedeye(fullBitmap As Boolean)

    Dim i, j, k As Integer
    Dim r, g, b As Integer
    Dim bb() As Byte
    Dim bMark(,) As Byte
    Dim bmp As Bitmap
    Static busy As Boolean = False

    If loading Or aview.pView0.Bitmap Is Nothing Or aview.pView1.Bitmap Is Nothing Then Exit Sub

    If busy Then Exit Sub
    busy = True

    ' only operate on the visible part of the bitmap
    ' use a floater image of the bitmap on the target
    ' this creates a region for clipping in setFloaterBitmap below
    ' if "fullbitmap" then copy bmp to qimage afterward
    aview.pView0.SetSelection(aview.pView0.ClientRectangle)
    aview.pView0.InitFloater()
    aview.pView0.FloaterVisible = False
    aview.pView0.FloaterOutline = False
    aview.pView1.SetSelection(aview.pView1.ClientRectangle)
    aview.pView1.InitFloater()
    aview.pView1.FloaterVisible = True
    aview.pView1.FloaterOutline = False
    bmp = aview.pView0.FloaterBitmap

    bb = getBmpBytes(bmp)
    ReDim bMark(bmp.Width, bmp.Height)

    k = nmThreshold.Value
    i = 0
    For iy As Integer = 0 To bmp.Height - 1
      For ix As Integer = 0 To bmp.Width - 1
        b = bb(i)
        g = bb(i + 1)
        r = bb(i + 2)
        If r - g > k AndAlso r - b > k Then ' red
          bb(i + 2) = (g + b) / 2
          bMark(ix, iy) = 1 ' also do the next two neighbors later
        Else
          bMark(ix, iy) = 0
        End If
        i = i + 4
      Next ix
    Next iy

    'change two neighbors for each red pixel
    For iy As Integer = 0 To bmp.Height - 1
      For ix As Integer = 0 To bmp.Width - 1
        If bMark(ix, iy) = 1 AndAlso (ix >= 2 And iy >= 2 And ix <= bmp.Width - 3 And iy <= bmp.Height - 3) Then
          For j = iy - 2 To iy + 2
            For i = ix - 2 To ix + 2
              If bMark(i, j) = 0 Then
                k = (j * bmp.Width + i) * 4
                b = bb(k)
                g = bb(k + 1)
                r = bb(k + 2)
                bb(k + 2) = (g + b) / 2
                bMark(i, j) = 2
              End If
            Next i
          Next j
        End If
      Next ix
    Next iy


    setBmpBytes(bmp, bb)
    aview.pView1.setFloaterBitmap(bmp)

    If fullBitmap Then ' copy the floater to qimage
      aview.pView1.assignFloater()
      qImage = aview.pView1.Bitmap.Clone
    Else
      saveStuff(bmp, aview.pView1, gpath, fullBitmap)
    End If

    aview.Repaint()
    aview.zoomLabel()

    Me.Cursor = Cursors.Default
    busy = False

  End Sub

  Private Sub cmdRemoveRedeye_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveRedeye.Click
    drawRedeye(False)
  End Sub

  Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
    ' redraw after some milliseconds
    Timer1.Stop()
    drawRedeye(False)
  End Sub

  Private Sub _FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
    Timer1.Stop()
    If gpath IsNot Nothing Then gpath.Dispose() : gpath = Nothing
  End Sub

End Class
