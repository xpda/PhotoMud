Imports System.Threading.Tasks
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Collections.Generic
Imports System.Math

Public Class frmHisto
  Inherits Form

  Dim clock As New Stopwatch

  Dim Loading As Boolean = True
  Dim Sliding As Boolean = False
  Dim picMoving As Boolean

  Dim histXscale, histYscale As Double
  Dim newMap(255) As Integer
  Dim pDown As Single

  Dim gpath As GraphicsPath = Nothing

  Dim imageReduced As Boolean
  Dim xReduced As Double

  Dim Channel As String

  Dim histSource(2, 255) As Integer ' original histo, RGB
  Dim histDest(2, 255) As Integer ' modified histo

  ' freeform curve stuff
  Dim xn(52) As Double
  Dim fn(52) As Double
  Dim s(52) As Double
  Dim n As Integer
  Dim inode As Integer
  Dim dragging As Boolean

  Dim gImage As Bitmap

  Dim slideIndex As Integer = -1
  Dim WithEvents Timer1 As New Timer

  Dim abort As Boolean = False

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

    If slideIndex >= 0 Then Timer1_Tick(Sender, e)

    mapReDraw(True)

    Me.Cursor = Cursors.Default
    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Private Sub aview_Resize(sender As Object, e As EventArgs) Handles aview.Resize
    aview.zoom(1)
  End Sub

  Sub aview_zoomed() Handles aview.Zoomed
    Timer1.Stop() : Timer1.Interval = 150 : Timer1.Start() ' redraw after 150 ms
  End Sub

  Private Sub frmHisto_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Dim i As Integer

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Loading = True

    Sliding = False
    Me.Cursor = Cursors.WaitCursor

    picUp0.Cursor = Cursors.Hand
    picUp1.Cursor = Cursors.Hand
    picRight0.Cursor = Cursors.Hand
    picRight1.Cursor = Cursors.Hand

    picUp0.Left = ((pView2.Left) - (picUp0.Width) / 2)
    picUp1.Left = ((pView2.Left) + (pView2.Width) - (picUp0.Width) / 2)
    picRight1.Top = ((pView2.Top) - (picRight1.Height) / 2)
    picRight0.Top = ((pView2.Top) + (pView2.Height) - (picRight0.Height) / 2)

    optColorAll.Checked = True
    Channel = "master"
    resetSlides()

    For i = 0 To 255 : newMap(i) = i : Next i

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

    resetFreeform()

    Loading = False
    Sliding = False

    aview.ZoomViews(0.5)

    Me.Cursor = Cursors.Default

  End Sub


  Sub resetFreeform()
    xn(1) = 0 : fn(1) = pView2.ClientSize.Height
    xn(3) = pView2.ClientSize.Width : fn(3) = 0
    xn(2) = (xn(1) + xn(3)) / 2
    fn(2) = (fn(1) + fn(3)) / 2
    n = 3
  End Sub



  Private Sub cmdReset_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdReset.Click

    Dim i As Integer

    Me.Cursor = Cursors.WaitCursor

    resetSlides()
    aview.pView1.setBitmap(aview.pView1.Bitmap)
    aview.Repaint()

    For i = 0 To 255 : newMap(i) = i : Next i

    mapReDraw(False)

    Me.Cursor = Cursors.Default

  End Sub

  Private Sub chkFreeForm_CheckedChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles chkFreeForm.CheckedChanged

    Dim i As Integer

    If Loading Then Exit Sub

    If chkFreeForm.Checked Then
      controlVis(False)
      xn(1) = 0 : fn(1) = pView2.ClientSize.Height
      xn(3) = pView2.ClientSize.Width : fn(3) = 0
      xn(2) = (xn(1) + xn(3)) / 2
      fn(2) = (fn(1) + fn(3)) / 2
      n = 3
    Else
      dragging = False
      controlVis(True)
      resetSlides()
    End If

    For i = 0 To 255 : newMap(i) = i : Next
    resetFreeform()
    If Not Loading Then showHisto(pView2, histSource, histXscale, histYscale)

  End Sub

  Private Sub optColor_CheckedChanged(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles optColorAll.CheckedChanged, optColorRed.CheckedChanged, optColorGreen.CheckedChanged, optColorBlue.CheckedChanged

    If Loading Or Not Sender.checked Then Exit Sub

    If Sender Is optColorAll Then
      Channel = "master"
    ElseIf Sender Is optColorRed Then
      Channel = "red"
    ElseIf Sender Is optColorGreen Then
      Channel = "green"
    ElseIf Sender Is optColorBlue Then
      Channel = "blue"
    End If

    For i As Integer = 0 To 255 : newMap(i) = i : Next
    resetFreeform()
    mapReDraw(False)
    If Not Loading Then showHisto(pView2, histSource, histXscale, histYscale)

  End Sub

  Private Sub picUp_MouseDown(ByVal Sender As Object, ByVal e As MouseEventArgs) _
    Handles picUp0.MouseDown, picUp1.MouseDown

    pDown = e.X

  End Sub

  Private Sub picUp_MouseMove(ByVal Sender As Object, ByVal e As MouseEventArgs) _
    Handles picUp0.MouseMove, picUp1.MouseMove

    Dim i, index As Integer
    Dim pic As PictureBox
    Dim spinner As NumericUpDown

    If Sender Is picUp0 Then
      index = 0
      spinner = nmSlide2
    Else
      index = 1
      spinner = nmSlide3
    End If
    pic = Sender

    If e.Button = MouseButtons.Left Then
      If picMoving Then Exit Sub
      picMoving = True

      i = (pic.Left) + e.X - pDown
      If i < (pView2.Left) - (picUp0.Width) / 2 Then
        i = (pView2.Left) - (picUp0.Width) / 2
      ElseIf i > (pView2.Left) + (pView2.Width) - (picUp0.Width) / 2 Then
        i = (pView2.Left) + (pView2.Width) - (picUp0.Width) / 2
      End If

      If index = 0 And i > (picUp1.Left) Then i = (picUp1.Left)
      If index = 1 And i < (picUp0.Left) Then i = (picUp0.Left)

      pic.Left = (i)
      Sliding = True
      spinner.Value = ((i + (picUp0.Width) / 2) - (pView2.Left)) / (pView2.Width) * 255
      slideIndex = index + 2
      Timer1.Stop() : Timer1.Interval = 250 : Timer1.Start() ' slideChange(index + 2) after 250 milliseconds

      picMoving = False
      Sliding = False
    End If

  End Sub

  Private Sub picUp_MouseUp(ByVal Sender As Object, ByVal e As MouseEventArgs) _
    Handles picUp0.MouseUp, picUp1.MouseUp

    Timer1.Stop()
    picUp_MouseMove(Sender, e)

  End Sub

  Private Sub picRight_MouseDown(ByVal Sender As Object, ByVal e As MouseEventArgs) _
    Handles picRight0.MouseDown, picRight1.MouseDown

    pDown = e.X

  End Sub

  Private Sub picRight_MouseMove(ByVal Sender As Object, ByVal e As MouseEventArgs) _
    Handles picRight0.MouseMove, picRight1.MouseMove

    Dim i, index As Integer
    Dim pic As PictureBox
    Dim spinner As NumericUpDown

    If Sender Is picRight0 Then
      index = 0
      spinner = nmSlide0
    Else
      index = 1
      spinner = nmSlide1
    End If

    pic = Sender

    If e.Button = MouseButtons.Left Then
      If picMoving Then Exit Sub
      picMoving = True

      i = (pic.Top) + e.Y - pDown
      If i < (pView2.Top) - (picRight0.Height) / 2 Then
        i = (pView2.Top) - (picRight0.Height) / 2
      ElseIf i > (pView2.Top) + (pView2.Height) - (picRight0.Height) / 2 Then
        i = (pView2.Top) + (pView2.Height) - (picRight0.Height) / 2
      End If

      If index = 0 And i < (picRight1.Top) Then i = (picRight1.Top)
      If index = 1 And i > (picRight0.Top) Then i = (picRight0.Top)

      pic.Top = (i)
      Sliding = True
      spinner.Value = 255 - ((i + (picRight0.Height) / 2) - (pView2.Top)) / (pView2.Height) * 255

      slideIndex = index
      Timer1.Stop() : Timer1.Interval = 250 : Timer1.Start() ' slideChange(index) after 250 milliseconds
      picMoving = False
      Sliding = False
    End If

  End Sub

  Private Sub picRight_MouseUp(ByVal Sender As Object, ByVal e As MouseEventArgs) _
    Handles picRight0.MouseUp, picRight1.MouseUp

    Timer1.Stop()
    picRight_MouseMove(Sender, e)

  End Sub

  Private Sub slideChange(ByRef index As Short)

    Dim i As Integer
    Dim sum As Integer
    Dim sumPixels As Integer

    Me.Cursor = Cursors.WaitCursor
    sum = 0
    sumPixels = 0
    For i = 0 To 255 ' get sumpixels for percentage
      If Channel = "master" Or Channel = "red" Then sumPixels += histSource(0, i)
      If Channel = "master" Or Channel = "green" Then sumPixels += histSource(1, i)
      If Channel = "master" Or Channel = "blue" Then sumPixels += histSource(2, i)
    Next i
    If sumPixels = 0 Then sumPixels = 1

    Sliding = True
    Select Case index
      Case 0
        For i = 0 To nmSlide0.Value - 1
          If Channel = "master" Or Channel = "red" Then sum = sum + histSource(0, i)
          If Channel = "master" Or Channel = "green" Then sum = sum + histSource(1, i)
          If Channel = "master" Or Channel = "blue" Then sum = sum + histSource(2, i)
        Next i
        lbPct0.Text = Format(sum / sumPixels, "##0.0%")
        lbPct0.Refresh()
      Case 1
        For i = nmSlide1.Value + 1 To 255
          If Channel = "master" Or Channel = "red" Then sum = sum + histSource(0, i)
          If Channel = "master" Or Channel = "green" Then sum = sum + histSource(1, i)
          If Channel = "master" Or Channel = "blue" Then sum = sum + histSource(2, i)
        Next i
        lbPct1.Text = Format(sum / sumPixels, "##0.0%")
        lbPct1.Refresh()
      Case 2
        For i = 0 To nmSlide2.Value - 1
          If Channel = "master" Or Channel = "red" Then sum = sum + histSource(0, i)
          If Channel = "master" Or Channel = "green" Then sum = sum + histSource(1, i)
          If Channel = "master" Or Channel = "blue" Then sum = sum + histSource(2, i)
        Next i
        lbPct2.Text = Format(sum / sumPixels, "##0.0%")
        lbPct2.Refresh()
      Case 3
        For i = nmSlide3.Value + 1 To 255
          If Channel = "master" Or Channel = "red" Then sum = sum + histSource(0, i)
          If Channel = "master" Or Channel = "green" Then sum = sum + histSource(1, i)
          If Channel = "master" Or Channel = "blue" Then sum = sum + histSource(2, i)
        Next i
        lbPct3.Text = Format(sum / sumPixels, "##0.0%")
        lbPct3.Refresh()
    End Select

    moveArrows(index)
    reMap()

    Me.Cursor = Cursors.Default
    Sliding = False
  End Sub

  Sub moveArrows(ByVal index As Integer)

    Select Case index
      Case 0
        picRight0.Top = ((pView2.Top) + (pView2.Height) - ((picRight0.Height) / 2 + nmSlide0.Value / 255 * (pView2.Height)))
      Case 1
        picRight1.Top = ((pView2.Top) + (pView2.Height) - ((picRight0.Height) / 2 + nmSlide1.Value / 255 * (pView2.Height)))
      Case 2
        picUp0.Left = ((pView2.Left) - (picUp0.Width) / 2 + nmSlide2.Value / 255 * (pView2.Width))
      Case 3
        picUp1.Left = ((pView2.Left) - (picUp0.Width) / 2 + nmSlide3.Value / 255 * (pView2.Width))
    End Select

  End Sub

  Private Sub TrackBar1_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TrackBar1.ValueChanged
    If Sliding Or Loading Then Exit Sub
    Timer1.Stop() : Timer1.Interval = 250 : Timer1.Start()
    Sliding = True
    nmTrackbar.Value = TrackBar1.Value
    'Call reMap()
    Sliding = False

  End Sub

  Sub reMap()
    ' creates a color map called newmap, new values for 0-255 original values
    Static Dim busy As Boolean = False
    Dim k, i As Integer
    Dim x, t As Double
    Dim x3, x2, x1, x0, y0, y1, y2, y3 As Double
    Dim bx, ax, cx As Double
    Dim by, ay, cy As Double
    Dim xt, yt As Double
    Dim slope As Double
    Dim i2, i3 As Integer

    If Loading Or busy Or aview.pView1.Bitmap Is Nothing Then Exit Sub
    busy = True

    For i = 0 To 255 : newMap(i) = i : Next i

    i2 = nmSlide2.Value
    i3 = nmSlide3.Value

    ' linear spread
    x = 256 / (i3 - i2 + 1)
    For i = i2 To i3
      newMap(i) = (newMap(i) - i2) * x
    Next i
    For i = 0 To i2
      newMap(i) = 0
    Next i
    For i = i3 To 255
      newMap(i) = 255
    Next i

    ' spline spread
    slope = 256 / (i3 - i2 + 1)
    y0 = nmSlide0.Value / 255
    x0 = (i2 + y0 / slope) / 255
    y3 = nmSlide1.Value / 255
    x3 = (i3 - (255 - nmSlide1.Value) / slope) / 255


    'x0 = 0: y0 = 0
    'x3 = 1: y3 = 1

    If TrackBar1.Value <> 0 Then
      If TrackBar1.Value >= 0 Then
        x1 = TrackBar1.Value / 100 + x0 : y1 = y0
        x2 = 1 - TrackBar1.Value / 100 - (1 - x3) : y2 = y3
      Else
        x1 = x0 : y1 = -TrackBar1.Value / 100 + y0
        x2 = x3 : y2 = 1 + TrackBar1.Value / 100 - (1 - y3)
      End If

      cx = 3 * (x1 - x0)
      bx = 3 * (x2 - x1) - cx
      ax = x3 - x0 - cx - bx

      cy = 3 * (y1 - y0)
      by = 3 * (y2 - y1) - cy
      ay = y3 - y0 - cy - by

      k = x0 * 255
      For i = 0 To 500
        t = i / 500
        xt = ax * t ^ 3 + bx * t ^ 2 + cx * t + x0
        If xt * 255 >= k Then
          yt = ay * t ^ 3 + by * t ^ 2 + cy * t + y0
          newMap(k) = yt * 255
          k = k + 1
          If k > 255 Then Exit For
        End If
      Next i
    End If

    ' clip results
    For i = 0 To 255
      If newMap(i) < nmSlide0.Value Then
        newMap(i) = nmSlide0.Value
      ElseIf newMap(i) > nmSlide1.Value Then
        newMap(i) = nmSlide1.Value
      End If
    Next i

    mapReDraw(False)

    busy = False

  End Sub

  Sub mapReDraw(fullBitmap As Boolean)

    Static busy As Boolean = False
    Dim bmp As Bitmap
    Dim Progress As Double

    If Loading OrElse aview.pView0.Bitmap Is Nothing OrElse aview.pView0.Bitmap.PixelFormat = 0 Then Exit Sub

    If busy Then Exit Sub
    busy = True

    Me.Cursor = Cursors.WaitCursor

    If fullBitmap Then
      aview.pView1.ClearSelection()
      bmp = qImage.Clone
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
      bmp = aview.pView0.FloaterBitmap.Clone
      histSource = getHisto(bmp)
      If Not Loading Then showHisto(pView2, histSource, histXscale, histYscale)
    End If

    Progress = 0
    remapintensity(bmp, newMap, Channel)

    If Not fullBitmap Then
      histDest = getHisto(bmp)
      If Not Loading Then showHisto(pView3, histDest, histXscale, histYscale)
    End If

    saveStuff(bmp, aview.pView1, gpath, fullBitmap)
    aview.Repaint()
    aview.zoomLabel()

    Me.Cursor = Cursors.Default
    Timer1.Stop()
    busy = False

  End Sub

  Sub remapintensity(ByRef bmp As Bitmap, newmap() As Integer, Channel As String)

    Dim bb() As Byte
    bb = getBmpBytes(bmp)

    For i As Integer = 0 To UBound(bb) Step 4
      If Channel = "master" Or Channel = "blue" Then bb(i) = newmap(bb(i))
      If Channel = "master" Or Channel = "green" Then bb(i + 1) = newmap(bb(i + 1))
      If Channel = "master" Or Channel = "red" Then bb(i + 2) = newmap(bb(i + 2))
    Next i

    setBmpBytes(bmp, bb)
  End Sub

  Sub resetSlides()

    Sliding = True
    nmSlide0.Value = 0
    nmSlide1.Value = 255
    nmSlide2.Value = 0
    nmSlide3.Value = 255
    TrackBar1.Value = 0
    nmTrackbar.Value = 0
    Sliding = False
    lbPct0.Text = "0.0%"
    lbPct1.Text = "0.0%"
    lbPct2.Text = "0.0%"
    lbPct3.Text = "0.0%"

    moveArrows(0)
    moveArrows(1)
    moveArrows(2)
    moveArrows(3)

  End Sub

  Sub controlVis(ByRef vis As Boolean)

    nmSlide2.Visible = vis
    nmSlide1.Visible = vis
    nmSlide2.Visible = vis
    nmSlide3.Visible = vis

    lbPct0.Visible = vis
    lbPct1.Visible = vis
    lbPct2.Visible = vis
    lbPct3.Visible = vis

    Label6.Visible = vis
    Label7.Visible = vis
    Label9.Visible = vis

    picUp0.Visible = vis
    picUp1.Visible = vis
    picRight0.Visible = vis
    picRight1.Visible = vis

    TrackBar1.Visible = vis
    nmTrackbar.Visible = vis

  End Sub

  Sub drawMap(g As Graphics)

    Dim i As Integer
    Dim yscale As Double
    Dim p(255) As PointF

    yscale = pView2.ClientSize.Height / 255
    For i = 0 To 255
      p(i).X = i * histXscale
      p(i).Y = pView2.ClientSize.Height - newMap(i) * yscale
    Next i

    g.SmoothingMode = SmoothingMode.HighQuality
    g.PixelOffsetMode = PixelOffsetMode.HighQuality

    Using gPen As New Pen(Color.Green, 1)
      g.DrawLine(gPen, 0, pView2.ClientSize.Height, pView2.ClientSize.Width, 0)
      gPen.Color = Color.Red
      gPen.Width = 2
      g.DrawLines(gPen, p)
    End Using

  End Sub

  Private Sub pView2_MouseDown(ByVal Sender As Object, ByVal e As MouseEventArgs) Handles pView2.MouseDown

    Dim d As Double
    Dim i As Integer
    Dim dmin As Double
    Dim x, y As Double

    x = e.X
    y = e.Y

    If Not chkFreeForm.Checked Then Exit Sub

    dmin = (xn(2) - x) ^ 2 + (fn(2) - y) ^ 2
    inode = 2
    For i = 3 To n - 1
      d = (xn(i) - x) ^ 2 + (fn(i) - y) ^ 2
      If d < dmin Then
        dmin = d
        inode = i
      End If
    Next i

    dragging = False

    If e.Button = MouseButtons.Right And dmin < 40 Then ' delete node with right button
      For i = inode To n - 1
        xn(i) = xn(i + 1)
        fn(i) = fn(i + 1)
      Next i
      n = n - 1

    ElseIf e.Button = MouseButtons.Left Then
      splineDistance(xn, fn, n, x, y, d)
      If dmin > 40 And x <> xn(inode) And d < 40 And n < UBound(xn) - 2 Then ' add new point
        dragging = True
        If x > xn(inode) Then inode = inode + 1

        For i = n To inode Step -1
          xn(i + 1) = xn(i)
          fn(i + 1) = fn(i)
        Next i
        n = n + 1
        xn(inode) = x
        fn(inode) = y
      ElseIf dmin < 40 Then
        dragging = True
        xn(inode) = x
        fn(inode) = y
      End If

    End If

    pView2.Invalidate()

  End Sub

  Private Sub pview2_MouseMove(ByVal Sender As Object, ByVal e As MouseEventArgs) Handles pView2.MouseMove


    If Not chkFreeForm.Checked Then Exit Sub

    If e.Button = MouseButtons.Left And dragging Then
      If e.X > xn(inode - 1) And e.X < xn(inode + 1) Then
        xn(inode) = e.X
        fn(inode) = e.Y
      End If
      pView2.Invalidate()
    End If

  End Sub

  Private Sub pview2_MouseUp(ByVal Sender As Object, ByVal e As MouseEventArgs) Handles pView2.MouseUp


    If Not chkFreeForm.Checked Then Exit Sub

    If e.Button = MouseButtons.Left And dragging Then
      xn(inode) = e.X
      fn(inode) = e.Y
      FreeFormMap(True)
      pView2.Invalidate()
    End If

  End Sub
  Sub splineDistance(ByRef xn() As Double, ByRef fn() As Double, ByRef n As Integer, ByVal xm As Double, ByVal ym As Double, ByRef dmin As Double)

    Dim xs(n + 2) As Double
    Dim ys(n + 2) As Double
    Dim xp(pView2.ClientSize.Width + 10) As Double
    Dim yp(pView2.ClientSize.Width + 10) As Double
    Dim np As Integer
    Dim x As Double
    Dim d As Double
    Dim i As Integer
    Dim L As Integer

    ' add a point on each end to make the curve more natural
    For i = 1 To n
      xs(i + 1) = xn(i)
      ys(i + 1) = fn(i)
    Next i
    xs(1) = xs(2) - 1
    ys(1) = ys(2) - (ys(3) - ys(2)) / (xs(3) - xs(2))
    xs(n + 2) = xs(n + 1) + 1
    ys(n + 2) = ys(n + 1) + (ys(n + 1) - ys(n)) / (xs(n + 1) - xs(n))

    spcoeff(xs, ys, s, n + 2)

    xp(1) = xs(2)
    yp(1) = ys(2)
    np = 1
    L = 2
    For x = xs(2) + 1 To xs(n + 1)
      np = np + 1
      xp(np) = x
      If x > xs(L + 1) Then L = L + 1
      yp(np) = spline(x, xs, ys, s, L)
    Next x

    dmin = (xp(2) - xm) ^ 2 + (yp(2) - ym) ^ 2
    For i = 3 To np - 1
      d = (xp(i) - xm) ^ 2 + (yp(i) - ym) ^ 2
      If d < dmin Then
        dmin = d
      End If
    Next i

  End Sub

  Sub FreeFormMap(ByRef redraw As Boolean)
    Dim xs(n + 2) As Double
    Dim ys(n + 2) As Double
    Dim x As Double
    Dim i, k As Integer
    Dim L As Integer

    ' add a point on each end to make the curve more natural
    For i = 1 To n
      xs(i + 1) = xn(i)
      ys(i + 1) = fn(i)
    Next i
    xs(1) = xs(2) - 1
    ys(1) = ys(2) - (ys(3) - ys(2)) / (xs(3) - xs(2))
    xs(n + 2) = xs(n + 1) + 1
    ys(n + 2) = ys(n + 1) + (ys(n + 1) - ys(n)) / (xs(n + 1) - xs(n))

    spcoeff(xs, ys, s, n + 2)

    L = 2
    For i = 0 To 255
      x = i * pView2.ClientSize.Width / 256
      If x > xs(L + 1) Then L = L + 1
      k = (pView2.ClientSize.Height - spline(x, xs, ys, s, L)) * 256 / pView2.ClientSize.Height
      If k > 255 Then k = 255 Else If k < 0 Then k = 0
      newMap(i) = k
    Next i

    If redraw Then mapReDraw(False)

  End Sub

  Private Sub nmSlide0_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nmSlide0.ValueChanged
    If Sliding Or Loading Then Exit Sub
    Timer1.Stop() : Timer1.Interval = 250 : Timer1.Start() ' slidechange(0) after 250 milliseconds
    slideIndex = 0
  End Sub
  Private Sub nmSlide1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nmSlide1.ValueChanged
    If Sliding Or Loading Then Exit Sub
    Timer1.Stop() : Timer1.Interval = 250 : Timer1.Start() ' slidechange(1) after 250 milliseconds
    slideIndex = 1
  End Sub
  Private Sub nmSlide2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nmSlide2.ValueChanged
    If Sliding Or Loading Then Exit Sub
    Timer1.Stop() : Timer1.Interval = 250 : Timer1.Start() ' slidechange(2) after 250 milliseconds
    slideIndex = 2
  End Sub
  Private Sub nmSlide3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nmSlide3.ValueChanged
    If Sliding Or Loading Then Exit Sub
    Timer1.Stop() : Timer1.Interval = 250 : Timer1.Start() ' slidechange(3) after 250 milliseconds
    slideIndex = 3
  End Sub

  Private Sub nmTrackbar_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nmTrackbar.Validating
    If Sliding Or Loading Then Exit Sub
    slideIndex = 4
    Timer1.Stop() : Timer1.Interval = 250 : Timer1.Start() ' remap() after 250 milliseconds
    Sliding = True
    TrackBar1.Value = nmTrackbar.Value
    Sliding = False
  End Sub

  Private Sub frmHisto_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    '  If Not Timer1.Enabled Then
    '      showHisto(pView2, histSource)
    '      showHisto(pView3, histDest)
    '   End If
  End Sub

  Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    ' redraw after some milliseconds
    Timer1.Stop()
    If Loading Then Exit Sub
    If slideIndex >= 0 And slideIndex <= 3 Then
      slideChange(slideIndex)
      slideIndex = -1
    Else
      reMap()
    End If
  End Sub

  Private Sub pView2_Paint(sender As Object, e As PaintEventArgs) Handles pView2.Paint


    If Not chkFreeForm.Checked Then
      drawMap(e.Graphics)
    Else
      Dim zp As New List(Of Point)

      pView2.RubberClear()
      Using gPath As New GraphicsPath
        ' add the line
        For i As Integer = 1 To n
          zp.Add(New Point(xn(i), fn(i)))
        Next i
        zp = getCurve(zp, 4)
        gPath.AddLines(zp.ToArray)


        ' add the knots
        For i As Integer = 1 To n
          pView2.RubberPoints.Add(New Point(xn(i) - 2, fn(i) - 2))
        Next i

        pView2.RubberPath = gPath.Clone
        pView2.RubberEnabled = True
        pView2.RubberLineWidth = 2
        pView2.RubberColor = Color.White
        pView2.rubberBackColor = Color.FromArgb(0, 255, 0)
        pView2.RubberDashed = False

        pView2.RubberShape = shape.Path
        pView2.Invalidate()
      End Using
    End If
  End Sub

  Private Sub form_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
    If gImage IsNot Nothing Then gImage.Dispose() : gImage = Nothing
    If gpath IsNot Nothing Then gpath.Dispose() : gpath = Nothing
    Timer1.Enabled = False
  End Sub

End Class