'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Math

Public Enum shape
  Line = 0
  Arrow
  Box
  Circle
  ClosedCurve
  Curve
  Ellipse
  Measure
  Path
  Polygon
  Text
End Enum

Public Class pViewer
  Inherits PictureBox

  Event Zoomed()

  ' =========== dispose these that need it in me.disposed ================
  Private uBitmap As Bitmap = Nothing
  Property BitmapPath As GraphicsPath ' selection path

  Dim uPage As Integer = 0
  Property pageBmp As New List(Of Bitmap)

  Private uFloaterBitmap As Bitmap = Nothing ' screen bitmap for dragging a selection
  Property FloaterPath As GraphicsPath
  Property FloaterPosition As Point ' upper left (bitmap coordinates)
  Property FloaterOutline As Boolean

  Private uZoomFactor As Double = 1
  Private uCenterPoint As Point = Point.Empty

  Dim uGraphics As Graphics = Nothing

  Property SelectionVisible As Boolean = True
  Property FloaterVisible As Boolean = True

  Dim uRubberPath As GraphicsPath
  Dim uRubberFont As Font
  Property RubberPoints() As New List(Of Point)
  Property RubberBox As Rectangle = Rectangle.Empty
  Property RubberDashed As Boolean = False
  Property RubberColor As Color = Color.Navy
  Property rubberBackColor As Color = Color.White
  Property RubberLineWidth As Double = 1
  Property RubberEnabled As Boolean = False
  Property RubberShape As shape = shape.Curve
  Property RubberString As String = ""
  Property RubberAngle As Double = 0 ' angle for text, etc.
  Property RubberTextFmt As StringFormat = Nothing ' text
  Property RubberFilled As Boolean = False
  Property RubberBoxCrop As Boolean = False ' whether to dim area outside the box

  Dim uDrawPath As GraphicsPath
  Dim uDrawfont As Font
  Property DrawPoints() As New List(Of Point)
  Property DrawBox As Rectangle = Rectangle.Empty
  Property DrawDashed As Boolean = False
  Property DrawForeColor As Color = Color.Navy
  Property DrawBackColor As Color = Color.White
  Property DrawLineWidth As Double = 1
  Property DrawShape As shape = shape.Line
  Property DrawString As String = ""
  Property DrawAngle As Double = 0 ' angle for text, etc.
  Property DrawTextFmt As StringFormat = Nothing ' text
  Property DrawFilled As Boolean = False

  Property InterpolationMode As InterpolationMode

  Property RubberPath As GraphicsPath
    Get
      Return uRubberPath
    End Get
    Set(gPath As GraphicsPath)
      If uRubberPath IsNot Nothing Then uRubberPath.Dispose() : uRubberPath = Nothing
      uRubberPath = gPath
    End Set
  End Property

  Property DrawPath As GraphicsPath
    Get
      Return uDrawPath
    End Get
    Set(gPath As GraphicsPath)
      If uDrawPath IsNot Nothing Then uDrawPath.Dispose() : uDrawPath = Nothing
      uDrawPath = gPath
    End Set
  End Property

  Property RubberFont As Font
    Get
      Return uRubberFont
    End Get
    Set(gfont As Font)
      If uRubberFont IsNot Nothing Then uRubberFont.Dispose() : uRubberFont = Nothing
      uRubberFont = gfont
    End Set
  End Property

  Property DrawFont As Font
    Get
      Return uDrawfont
    End Get
    Set(gfont As Font)
      If uDrawfont IsNot Nothing Then uDrawfont.Dispose() : uDrawfont = Nothing
      uDrawfont = gfont
    End Set
  End Property

  Property CurrentPage As Integer
    Get
      Return uPage
    End Get
    Set(ByVal iPage As Integer)
      If pageBmp.Count <= 1 Then Exit Property
      If iPage >= pageBmp.Count Then iPage = 0
      If iPage < 0 Then iPage = pageBmp.Count - 1
      clearBitmap(uBitmap)
      uBitmap = pageBmp(iPage).Clone
      uPage = iPage
    End Set
  End Property

  Public Sub RemovePage(PageNumber As Integer)
    If PageNumber < 0 Or PageNumber >= pageBmp.Count Then Exit Sub
    clearBitmap(pageBmp(PageNumber))
    pageBmp.RemoveAt(PageNumber)

    If pageBmp.Count = 1 Then
      clearBitmap(uBitmap)
      uBitmap = pageBmp(0).Clone
      clearBitmap(pageBmp(0))
      pageBmp = New List(Of Bitmap)
      uPage = 0
    End If

  End Sub

  Public Sub InsertPage(PageNumber As Integer, bmp As Bitmap)

    If PageNumber < 0 Or PageNumber > pageBmp.Count Then Exit Sub

    If pageBmp.Count = 0 Then pageBmp.Add(uBitmap.Clone) ' single page doesn't use pagebmp

    If PageNumber = pageBmp.Count Then
      pageBmp.Add(bmp.Clone)
    Else
      pageBmp.Add(pageBmp(pageBmp.Count - 1))

      For i As Integer = pageBmp.Count - 2 To PageNumber + 1 Step -1 ' add to end if necessary
        pageBmp(i) = pageBmp(i - 1)
      Next i
      pageBmp(PageNumber) = bmp.Clone
    End If

    clearBitmap(uBitmap)
    uBitmap = pageBmp(PageNumber).Clone
    uPage = PageNumber

  End Sub

  Property PageCount As Integer
    Get
      Return pageBmp.Count
    End Get

    Set(nPages As Integer)
      For i As Integer = pageBmp.Count To nPages ' add to end if necessary
        pageBmp.Add(New Bitmap(1, 1, PixelFormat.Format32bppPArgb))
      Next i

      For i As Integer = nPages To pageBmp.Count - 1 ' delete at end if necessary
        clearBitmap(pageBmp(i))
        pageBmp.RemoveAt(i)
      Next i

      If uPage > pageBmp.Count - 1 Then uPage = pageBmp.Count - 1

    End Set
  End Property

  Property ZoomFactor As Double
    Get
      Return uZoomFactor
    End Get
    Set(ByVal zoomFac As Double)
      Zoom(zoomFac)
    End Set
  End Property

  ReadOnly Property CenterPoint As Point
    ' point is in bitmap units
    Get
      Return uCenterPoint
    End Get
  End Property

  ReadOnly Property Bitmap As Bitmap
    Get
      Return uBitmap
    End Get
  End Property

  Public Sub newBitmap(width As Integer, height As Integer, color As Color)
    clearBitmap(uBitmap)
    clearframes()
    uBitmap = New Bitmap(width, height, PixelFormat.Format32bppPArgb)
    Using g As Graphics = Graphics.FromImage(uBitmap)
      g.Clear(color)
    End Using
  End Sub

  Public Sub setBitmap(bmpSource As Bitmap)
    ' assigns a clone of bmpSource to uBitmap

    Dim rSource, rTarget As Rectangle

    If bmpSource Is Nothing OrElse bmpSource.PixelFormat = 0 Then
      uBitmap = Nothing
    Else
      If BitmapPath Is Nothing Then
        If uBitmap IsNot Nothing Then
          uBitmap.Dispose()
        Else ' center if new bitmap
          uCenterPoint = New Point(bmpSource.Width \ 2, bmpSource.Height \ 2) ' center a new bitmap by default
        End If

        If bmpSource.PixelFormat = PixelFormat.Format32bppPArgb Then
          uBitmap = bmpSource.Clone
        Else ' use drawImage to get it to 32bppPArgb format.
          uBitmap = New Bitmap(bmpSource.Width, bmpSource.Height, PixelFormat.Format32bppPArgb)
          uBitmap.SetResolution(bmpSource.HorizontalResolution, bmpSource.VerticalResolution)
          Using g As Graphics = Graphics.FromImage(uBitmap)
            g.InterpolationMode = InterpolationMode.NearestNeighbor
            g.DrawImage(bmpSource, New Rectangle(0, 0, bmpSource.Width, bmpSource.Height))
          End Using
        End If

      Else ' copy into the bitmappath
        Using g As Graphics = Graphics.FromImage(uBitmap)
          g.SetClip(BitmapPath)
          rSource = New Rectangle(0, 0, bmpSource.Width, bmpSource.Height)
          rTarget = New Rectangle(0, 0, uBitmap.Width, uBitmap.Height)
          g.DrawImage(bmpSource, rSource, rTarget, GraphicsUnit.Pixel)
        End Using
      End If
    End If

    If pageBmp.Count > 1 Then ' assign to pages
      clearBitmap(pageBmp(uPage))
      pageBmp(uPage) = uBitmap.Clone
    End If

  End Sub

  ReadOnly Property FloaterBitmap As Bitmap
    Get
      Return uFloaterBitmap
    End Get
  End Property

  Public Sub setFloaterBitmap(ByRef SourceBitmap As Bitmap)
    ' assigns a clone of SourceBitmap to uFloaterBitmap
    If uFloaterBitmap IsNot Nothing Then uFloaterBitmap.Dispose()
    If SourceBitmap Is Nothing Then
      uFloaterBitmap = Nothing
    Else
      Set32bppPArgb(SourceBitmap)
      uFloaterBitmap = SourceBitmap.Clone
    End If
  End Sub

  Public Sub New()
    MyBase.New()

    MyBase.Dock = DockStyle.None
    MyBase.Location = New Point(0, 0)
    MyBase.TabStop = False
    MyBase.BackColor = SystemColors.ControlDarkDark
    MyBase.SizeMode = PictureBoxSizeMode.Normal

    InterpolationMode = InterpolationMode.High

  End Sub

  Public Sub setCenterPoint(pCenter As Point, reDraw As Boolean)
    ' pcenter is in bitmap units. 
    uCenterPoint = pCenter
    If reDraw Then Zoom()
  End Sub

  Public Sub CenterBitmap()
    ' This just changes uCenterpoint. It doesn't redraw. Good for calling before zoom with a new bitmap.
    If uBitmap IsNot Nothing Then
      uCenterPoint = New Point(uBitmap.Width \ 2, uBitmap.Height \ 2)
    End If
  End Sub

  Public Sub ZoomWindow(rControl As Rectangle)
    ' rControls is in control coordinates
    Dim z As Double
    Dim r As Rectangle

    r = ControlToBitmap(rControl)
    uCenterPoint = New Point(r.X + r.Width * 0.5, r.Y + r.Height * 0.5) ' centerpoint in bitmap
    z = Min(Me.ClientSize.Width / r.Width, Me.ClientSize.Height / r.Height)
    RubberClear()
    Zoom(z)

  End Sub

  Public Sub Zoom()
    ' redraw if no parameters
    Dim z As Double = uZoomFactor
    Zoom(z)
  End Sub

  Public Sub Zoom(ZoomFactor As Double)
    ' zoom into screen center if only one parameter
    Dim p As Point
    p = uCenterPoint
    Zoom(ZoomFactor, p)
  End Sub

  Public Sub Zoom(ZoomFactor As Double, ZoomInto As Point)
    ' zoom into CenterPoint at absolute ZoomFactor, in bitmap coordinates
    ' if Zoomfactor = 0, fit to window, but don't scale up to fit. 
    ' If Zoomfactor < 0, fit to window, and zoom in if necessary.

    Dim vw, vh As Integer
    Dim x, y As Integer
    Dim dz As Double

    If uBitmap Is Nothing Then Exit Sub
    fitPicImage()

    If ZoomFactor > 0 Then ' set centerpoint for zoominto point
      dz = uZoomFactor / ZoomFactor
      uCenterPoint.X = ZoomInto.X + (uCenterPoint.X - ZoomInto.X) * dz
      uCenterPoint.Y = ZoomInto.Y + (uCenterPoint.Y - ZoomInto.Y) * dz
    End If

    If ZoomFactor <= 0 Then ' zoom fit
      ' if 0, don't scale up to fit. If < 0, then zoom in so it fits the window.
      uZoomFactor = Math.Min(MyBase.ClientSize.Width / uBitmap.Width, MyBase.ClientSize.Height / uBitmap.Height)
      If ZoomFactor = 0 And uZoomFactor > 1 Then uZoomFactor = 1
      uCenterPoint = New Point(uBitmap.Width \ 2, uBitmap.Height \ 2)
    Else
      uZoomFactor = ZoomFactor
    End If

    vw = MyBase.ClientSize.Width / uZoomFactor
    vh = MyBase.ClientSize.Height / uZoomFactor
    x = uCenterPoint.X - vw * 0.5 ' x and y are the upper left corner location, in bitmap units
    y = uCenterPoint.Y - vh * 0.5

    If MyBase.Image IsNot Nothing Then
      Using g As Graphics = Graphics.FromImage(MyBase.Image)
        If x < 0 Or y < 0 Or vw > uBitmap.Width Or vh < uBitmap.Height Then g.Clear(MyBase.BackColor)
        If uZoomFactor < 2.5 Then
          g.InterpolationMode = InterpolationMode
        Else
          g.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor ' big pixels
        End If
        g.DrawImage(uBitmap, MyBase.ClientRectangle, x, y, vw, vh, GraphicsUnit.Pixel) ' x, y, vw, vh, GraphicsUnit.Pixel)
        MyBase.Refresh()
      End Using
    End If
    RaiseEvent Zoomed()

  End Sub

  Public Sub Pan(dx As Integer, dy As Integer)
    ' dx and dy are distance in control pixels, not bitmap pixels.

    Dim vw, vh As Integer
    Dim x, y As Integer

    If uBitmap Is Nothing Then Exit Sub
    fitPicImage()

    uCenterPoint.X -= dx / ZoomFactor
    uCenterPoint.Y -= dy / ZoomFactor

    vw = MyBase.ClientSize.Width / uZoomFactor
    vh = MyBase.ClientSize.Height / uZoomFactor
    x = uCenterPoint.X - vw * 0.5
    y = uCenterPoint.Y - vh * 0.5

    If MyBase.Image IsNot Nothing Then
      Using g As Graphics = Graphics.FromImage(MyBase.Image)
        If x < 0 Or y < 0 Or vw + x > uBitmap.Width Or vh + y > uBitmap.Height Then g.Clear(MyBase.BackColor)
        If uZoomFactor < 2.5 Then
          g.InterpolationMode = InterpolationMode
        Else ' big pixels
          g.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
        End If
        g.DrawImage(uBitmap, MyBase.ClientRectangle, x, y, vw, vh, GraphicsUnit.Pixel)
        MyBase.Refresh()
      End Using
    End If

    RaiseEvent Zoomed()

  End Sub

  Sub fitPicImage()
    ' if the picturebox image is not the size of the picturebox, reset it.
    Dim bmp As Bitmap

    If MyBase.Image Is Nothing Then ' should never happen
      MyBase.Image = New Bitmap(MyBase.ClientSize.Width, MyBase.ClientSize.Height, PixelFormat.Format32bppPArgb)
    End If

    If MyBase.Image.Width <> MyBase.ClientSize.Width Or MyBase.Image.Height <> MyBase.ClientSize.Height Then
      ' there was a resize, or new window
      If MyBase.ClientSize.Width > 0 And MyBase.ClientSize.Height > 0 Then
        bmp = MyBase.Image
        MyBase.Image = New Bitmap(MyBase.ClientSize.Width, MyBase.ClientSize.Height, PixelFormat.Format32bppPArgb)
        If bmp IsNot Nothing Then bmp.Dispose()
      End If
    End If

  End Sub

  Public Function ControlToBitmap(p As Point) As Point

    Dim dx, dy As Double
    Dim pWork As Point

    dx = p.X - MyBase.ClientSize.Width / 2
    dy = p.Y - MyBase.ClientSize.Height / 2

    pWork.X = uCenterPoint.X + dx / uZoomFactor
    pWork.Y = uCenterPoint.Y + dy / uZoomFactor

    Return pWork

  End Function

  Public Function BitmapToControl(pf As Point) As Point

    Dim dx, dy As Double
    Dim pControl As Point

    dx = (pf.X - uCenterPoint.X) * ZoomFactor
    dy = (pf.Y - uCenterPoint.Y) * ZoomFactor

    pControl.X = dx + MyBase.ClientSize.Width / 2
    pControl.Y = dy + MyBase.ClientSize.Height / 2

    Return pControl

  End Function

  Public Function ControlToBitmap(r As Rectangle) As Rectangle

    Dim pf As Point
    Dim rb As Rectangle

    pf = ControlToBitmap(New Point(r.X, r.Y))
    rb.X = pf.X
    rb.Y = pf.Y
    rb.Width = r.Width / ZoomFactor
    rb.Height = r.Height / ZoomFactor
    Return rb

  End Function

  Public Function BitmapToControl(rf As Rectangle) As Rectangle

    Dim pf As Point
    Dim rc As Rectangle

    pf = New Point(rf.X, rf.Y)
    pf = BitmapToControl(pf)
    rc.X = pf.X
    rc.Y = pf.Y
    rc.Width = rf.Width * ZoomFactor
    rc.Height = rf.Height * ZoomFactor
    Return rc

  End Function

  Public Sub ControlToBitmap(ByRef gPath As GraphicsPath)

    Dim mx As New Matrix
    Dim pf As Point

    pf = BitmapToControl(New Point(0, 0))
    mx.Translate(-pf.X, -pf.Y, MatrixOrder.Append)
    mx.Scale(1 / ZoomFactor, 1 / ZoomFactor, MatrixOrder.Append)
    'mx.Translate(-FloaterPosition.X, -FloaterPosition.Y, MatrixOrder.Append)
    gPath.Transform(mx)

  End Sub

  Public Sub BitmapToControl(ByRef gPath As GraphicsPath)

    Dim mx As New Matrix
    Dim pf As Point

    pf = BitmapToControl(FloaterPosition)
    mx.Scale(ZoomFactor, ZoomFactor, MatrixOrder.Append)
    mx.Translate(pf.X, pf.Y, MatrixOrder.Append)
    gPath.Transform(mx)

  End Sub

  Public Sub ApplyColorMatrix(ColorMatrix As ColorMatrix)
    ApplyColorMatrix(ColorMatrix, uBitmap, uBitmap, Nothing) ' default source and target to mview.bitmap
  End Sub

  Public Sub ApplyColorMatrix(ColorMatrix As ColorMatrix, SourceBitmap As Bitmap)
    ApplyColorMatrix(ColorMatrix, SourceBitmap, uBitmap, Nothing) ' default target to mview.bitmap
  End Sub

  Public Sub ApplyColorMatrix(ColorMatrix As ColorMatrix, SourceBitmap As Bitmap, ByRef TargetBitmap As Bitmap,
                              gPath As GraphicsPath)

    ' apply a ColorMatrix to the bitmap
    Dim disposeSource As Boolean = False
    Dim attr As ImageAttributes = New ImageAttributes()

    If SourceBitmap Is TargetBitmap Then
      SourceBitmap = SourceBitmap.Clone
      disposeSource = True
    End If

    clearBitmap(TargetBitmap)
    TargetBitmap = New Bitmap(SourceBitmap.Width, SourceBitmap.Height, SourceBitmap.PixelFormat)
    'TargetBitmap.SetResolution(SourceBitmap.HorizontalResolution, SourceBitmap.HorizontalResolution)

    attr.SetColorMatrix(ColorMatrix)

    Using g As Graphics = Graphics.FromImage(TargetBitmap)
      If gPath IsNot Nothing AndAlso gPath.PointCount > 0 Then g.SetClip(gPath)
      g.DrawImage(SourceBitmap, New Rectangle(0, 0, SourceBitmap.Width, SourceBitmap.Height),
       0, 0, SourceBitmap.Width, SourceBitmap.Height, GraphicsUnit.Pixel, attr)
    End Using

    If disposeSource Then SourceBitmap.Dispose()

  End Sub

  Public Sub MakeSepia()
    Dim mx As ColorMatrix = New ColorMatrix(
       {New Single() {0.393, 0.349, 0.272, 0, 0},
        New Single() {0.769, 0.686, 0.534, 0, 0},
        New Single() {0.189, 0.168, 0.131, 0, 0},
        New Single() {0, 0, 0, 1, 0},
        New Single() {0, 0, 0, 0, 1}})
    ApplyColorMatrix(mx)

  End Sub

  Public Sub HueRotate(Degrees As Double)

    Dim a As Double
    Dim sine, cosine As Double

    Dim lumR As Double = 0.213
    Dim lumG As Double = 0.715
    Dim lumB As Double = 0.072

    a = Degrees * Math.PI / 180
    cosine = Cos(a)
    sine = Sin(a)

    Dim mx As ColorMatrix = New ColorMatrix(
       {New Single() {lumR + cosine * (1 - lumR) + sine * (-lumR),
                      lumG + cosine * (-lumG) + sine * (-lumG),
                      lumB + cosine * (-lumB) + sine * (1 - lumB), 0, 0},
        New Single() {lumR + cosine * (-lumR) + sine * (0.143),
                      lumG + cosine * (1 - lumG) + sine * (0.14),
                      lumB + cosine * (-lumB) + sine * (-0.283), 0, 0},
        New Single() {lumR + cosine * (-lumR) + sine * (-(1 - lumR)),
                      lumG + cosine * (-lumG) + sine * (lumG),
                      lumB + cosine * (1 - lumB) + sine * (lumB), 0, 0},
        New Single() {0, 0, 0, 1, 0},
        New Single() {0, 0, 0, 0, 1}})

    ApplyColorMatrix(mx)

  End Sub

  Public Sub MakeBlackAndWhite(Threshold As Integer)

    ' threshold goes from -50 to 50, mapped to x at .5 to 10, zero for normal
    Dim x As Double

    x = 0.00001228 * Threshold ^ 3 + 0.0015 * Threshold ^ 2 + 0.0643 * Threshold + 1.5

    Dim mx As ColorMatrix = New ColorMatrix(
       {New Single() {x, x, x, 0, 0},
        New Single() {x, x, x, 0, 0},
        New Single() {x, x, x, 0, 0},
        New Single() {0, 0, 0, 1, 0},
        New Single() {-1, -1, -1, 0, 1}})

    ApplyColorMatrix(mx)

  End Sub

  Public Sub MakeGrayscale()

    ' grayscale ColorMatrix
    Dim mxColor As ColorMatrix = New ColorMatrix(
      {New Single() {0.3F, 0.3F, 0.3F, 0, 0},
        New Single() {0.59F, 0.59F, 0.59F, 0, 0},
        New Single() {0.11F, 0.11F, 0.11F, 0, 0},
        New Single() {0, 0, 0, 1, 0},
        New Single() {0, 0, 0, 0, 1}})

    ApplyColorMatrix(mxColor)

  End Sub

  Public Sub Invert()

    ' inversion matrix
    Dim mxColor As ColorMatrix = New ColorMatrix(
       {New Single() {-1, 0, 0, 0, 0},
        New Single() {0, -1, 0, 0, 0},
        New Single() {0, 0, -1, 0, 0},
        New Single() {0, 0, 0, 1, 0},
        New Single() {1, 1, 1, 0, 1}})

    ApplyColorMatrix(mxColor)

  End Sub

  Public Sub Crop(r As Rectangle)
    Crop(r, uBitmap, uBitmap) ' default source and target to mview.bitmap
  End Sub

  Public Sub Crop(r As Rectangle, ByVal SourceBitmap As Bitmap, ByRef TargetBitmap As Bitmap)

    Dim disposeSource As Boolean = False

    ' check rectangle range
    If r.X < 0 Then
      r.Width += r.X
      r.X = 0
    End If
    If r.Y < 0 Then
      r.Height += r.Y
      r.Y = 0
    End If
    If r.X + r.Width > SourceBitmap.Width Then r.Width = SourceBitmap.Width - r.X
    If r.Y + r.Height > SourceBitmap.Height Then r.Height = SourceBitmap.Height - r.Y

    If r.Width <= 0 Or r.Height <= 0 Then Exit Sub

    If SourceBitmap Is TargetBitmap Then
      SourceBitmap = SourceBitmap.Clone
      disposeSource = True
    End If

    clearBitmap(TargetBitmap)
    TargetBitmap = New Bitmap(r.Width, r.Height, SourceBitmap.PixelFormat)
    TargetBitmap.SetResolution(SourceBitmap.HorizontalResolution, SourceBitmap.HorizontalResolution) ' necessary for drawimage

    Using g As Graphics = Graphics.FromImage(TargetBitmap)
      g.InterpolationMode = InterpolationMode
      g.DrawImage(SourceBitmap, 0, 0, r, GraphicsUnit.Pixel)
    End Using

    If disposeSource Then SourceBitmap.Dispose()

  End Sub

  Public Sub ResizeBitmap(NewSize As Size)
    ResizeBitmap(NewSize, uBitmap, uBitmap)
  End Sub

  Public Sub ResizeBitmap(maxWidth As Integer, maxHeight As Integer, SourceBitmap As Bitmap, ByRef TargetBitmap As Bitmap)
    ' use maxWidth and maxHeight as dimension limits, with no change in aspect ratio.
    Dim xsize, ysize As Integer

    If maxHeight / maxWidth > SourceBitmap.Height / SourceBitmap.Width Then
      ysize = Round(SourceBitmap.Height * maxWidth / SourceBitmap.Width)
      xsize = maxWidth
    Else
      xsize = Round(SourceBitmap.Width * maxHeight / SourceBitmap.Height)
      ysize = maxHeight
    End If

    ResizeBitmap(New Size(xsize, ysize), SourceBitmap, TargetBitmap)

  End Sub

  Public Sub ResizeBitmap(NewSize As Size, SourceBitmap As Bitmap, ByRef TargetBitmap As Bitmap)

    Dim disposeSource As Boolean = False

    If SourceBitmap Is TargetBitmap Then
      SourceBitmap = SourceBitmap.Clone
      disposeSource = True
    End If

    If NewSize.Width <= 0 Or NewSize.Height <= 0 Then ' should never happen
      NewSize.Width = SourceBitmap.Width
      NewSize.Height = SourceBitmap.Height
    End If

    If TargetBitmap IsNot Nothing Then TargetBitmap.Dispose()
    TargetBitmap = New Bitmap(NewSize.Width, NewSize.Height, PixelFormat.Format32bppPArgb)

    Using g As Graphics = Graphics.FromImage(TargetBitmap)
      g.InterpolationMode = InterpolationMode
      g.DrawImage(SourceBitmap, New Rectangle(0, 0, NewSize.Width, NewSize.Height))
    End Using

    If disposeSource Then SourceBitmap.Dispose()

  End Sub

  Public Sub Fill(FillColor As Color)
    Fill(FillColor, uBitmap)
  End Sub

  Public Sub Fill(FillColor As Color, TargetBitmap As Bitmap, Optional gPath As GraphicsPath = Nothing)

    If TargetBitmap Is Nothing Then Exit Sub


    Using g As Graphics = Graphics.FromImage(TargetBitmap)
      If gPath IsNot Nothing AndAlso gPath.PointCount > 0 Then g.SetClip(gPath)
      g.InterpolationMode = InterpolationMode
      g.Clear(FillColor)
    End Using

  End Sub


  Sub rotate(Degrees As Double)

    Dim x, y, x2, y2 As Integer
    Dim dx, dy As Integer
    Dim angle, sine, cosine As Double

    Select Case Degrees
      Case 0, 360
        Exit Sub
      Case 90
        uBitmap.RotateFlip(RotateFlipType.Rotate90FlipNone)
        Exit Sub
      Case 180
        uBitmap.RotateFlip(RotateFlipType.Rotate180FlipNone)
        Exit Sub
      Case 270
        uBitmap.RotateFlip(RotateFlipType.Rotate270FlipNone)
        Exit Sub
    End Select

    angle = -Degrees * pi / 180
    cosine = Cos(angle) : sine = Sin(angle)
    x = uBitmap.Width \ 2
    y = uBitmap.Height \ 2
    x2 = Abs(x * Cos(angle)) + Abs(y * Sin(angle))
    y2 = Abs(x * Sin(angle)) + Abs(y * Cos(angle))

    ' rotate clockwise
    Using bmp As Bitmap = uBitmap.Clone
      uBitmap = New Bitmap(x2 * 2, y2 * 2)
      Using g As Graphics = Graphics.FromImage(uBitmap)
        g.Clear(Color.Navy)
        g.ResetTransform()
        g.InterpolationMode = InterpolationMode
        g.TranslateTransform(-uBitmap.Width \ 2, -uBitmap.Height \ 2)
        g.RotateTransform(Degrees, MatrixOrder.Append)
        g.TranslateTransform(x2, y2, MatrixOrder.Append)

        dx = x2 * 2 - bmp.Width
        dy = y2 * 2 - bmp.Height
        g.DrawImage(bmp, New Rectangle(dx \ 2, dy \ 2, uBitmap.Width - dx, uBitmap.Height - dy))
      End Using
    End Using

  End Sub

  '  Public Sub BrightConSat(Brightness As Integer, Contrast As Integer, Saturation As Integer)
  '   BrightConSat(Brightness, Contrast, Saturation, uBitmap, uBitmap, Nothing) ' default source and target to mview.bitmap
  'End Sub

  'Public Sub BrightConSat(Brightness As Integer, Contrast As Integer, Saturation As Integer, SourceBitmap As Bitmap)
  '  BrightConSat(Brightness, Contrast, Saturation, SourceBitmap, uBitmap, Nothing) ' default target to mview.bitmap
  'End Sub

  Public Sub BrightConSat(Brightness As Integer, Contrast As Integer, Saturation As Integer, _
    ByRef SourceBitmap As Bitmap, ByRef TargetBitmap As Bitmap, gPath As GraphicsPath)

    ' Brightness, Contrast, and Saturation should be -100 to +100, 0=no change

    Dim mx As New ColorMatrix
    Dim b, c, t, sr, sg, sb, sat As Double

    b = Brightness / 100
    c = Contrast / 100 + 1
    sat = Saturation / 100 + 1
    t = (1 - c) / 2
    sr = (1 - sat) * 0.3086 ' or .2125
    sg = (1 - sat) * 0.6094 ' or .7154
    sb = (1 - sat) * 0.082 ' or .0721

    mx(0, 0) = c * (sr + sat)
    mx(0, 1) = c * sr
    mx(0, 2) = mx(0, 1)

    mx(1, 0) = c * sg
    mx(1, 1) = c * (sg + sat)
    mx(1, 2) = mx(1, 0)

    mx(2, 0) = c * sb
    mx(2, 1) = mx(2, 0)
    mx(2, 2) = c * (sb + sat)

    mx(4, 0) = t + b
    mx(4, 1) = t + b
    mx(4, 2) = t + b

    ApplyColorMatrix(mx, SourceBitmap, TargetBitmap, gPath)

  End Sub

  Function addPath(qPath As GraphicsPath) As String
    ' uses bitmap coordinates

    Try
      If BitmapPath Is Nothing Then
        BitmapPath = qPath.Clone
      Else
        BitmapPath.AddPath(qPath, False)
      End If
    Catch ex As Exception
      ClearSelection()
      Return (ex.Message)
    End Try

    Return ""

  End Function

  Function SetSelection(ControlPath As GraphicsPath) As String
    ' uses control coordinates, not bitmap coordinates

    If ControlPath.PointCount <= 1 Then Return "Invalid Path"
    If BitmapPath IsNot Nothing Then BitmapPath.Dispose() : BitmapPath = Nothing

    BitmapPath = ControlPath.Clone
    ControlToBitmap(BitmapPath)
    Return ""

  End Function

  Function SetSelection(SelectionRectangle As Rectangle) As String
    ' uses control coordinates, not bitmap coordinates

    If SelectionRectangle.Width = 0 Or SelectionRectangle.Height = 0 Then
      ClearSelection()
      Return "Invalid Rectangle"
    End If

    If BitmapPath IsNot Nothing Then BitmapPath.Dispose()
    BitmapPath = New GraphicsPath
    BitmapPath.AddRectangle(ControlToBitmap(SelectionRectangle))

    Return ""

  End Function

  Sub RubberClear()
    RubberBoxCrop = False ' whether to dim area outside the box
    RubberBox = Rectangle.Empty
    RubberDashed = False
    RubberColor = Color.Navy
    rubberBackColor = Color.White
    RubberLineWidth = 1
    RubberEnabled = False
    RubberShape = shape.Line
    RubberString = "" ' text
    RubberAngle = 0 ' angle for text, etc.
    RubberTextFmt = Nothing ' text
    RubberFilled = False
    RubberPoints = New List(Of Point)
    If uRubberFont IsNot Nothing Then uRubberFont.Dispose() : uRubberFont = Nothing
    If uRubberPath IsNot Nothing Then uRubberPath.Dispose() : uRubberPath = Nothing
  End Sub

  Sub ClearSelection()
    If BitmapPath IsNot Nothing Then BitmapPath.Dispose() : BitmapPath = Nothing
    SelectionVisible = False
  End Sub

  Sub ClearFloater()
    If uFloaterBitmap IsNot Nothing Then uFloaterBitmap.Dispose() : uFloaterBitmap = Nothing
    If FloaterPath IsNot Nothing Then FloaterPath.Dispose() : FloaterPath = Nothing
    FloaterPosition = Point.Empty
    FloaterVisible = False
  End Sub

  Sub InitFloater()
    ' copy bitmap at the current selection to the floater
    ' the current selection is bitmappath, assigned to floaterpath and floaterbitmap
    Dim r As Rectangle
    Dim mx As New Matrix

    If BitmapPath Is Nothing Then Exit Sub
    clearBitmap(uFloaterBitmap)
    If FloaterPath IsNot Nothing Then FloaterPath.Dispose() : FloaterPath = Nothing

    Try
      r = Rectangle.Round(BitmapPath.GetBounds)
      uFloaterBitmap = New Bitmap(r.Width, r.Height, PixelFormat.Format32bppPArgb)
      uFloaterBitmap.SetResolution(uBitmap.HorizontalResolution, uBitmap.HorizontalResolution) ' necessary for DrawImage

      FloaterPath = BitmapPath.Clone ' translate floater path to 0,0
      mx.Translate(-r.X, -r.Y)
      FloaterPath.Transform(mx)

      Using g As Graphics = Graphics.FromImage(uFloaterBitmap)
        g.DrawImage(uBitmap, 0, 0, r, GraphicsUnit.Pixel)
      End Using
      FloaterPosition = New Point(r.X, r.Y) ' bitmap coordinates

    Catch ex As Exception
      MsgBox(ex.Message)
      ClearFloater()
      Exit Sub
    End Try

    FloaterOutline = True

  End Sub

  Private Sub uDrawFloater(g As Graphics)

    Dim r As Rectangle
    Dim gPen As New Pen(Color.White, 1)

    If uZoomFactor < 2.5 Then
      g.InterpolationMode = InterpolationMode
    Else
      g.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor ' big pixels
    End If

    r = New Rectangle(FloaterPosition.X, FloaterPosition.Y, uFloaterBitmap.Width, uFloaterBitmap.Height)
    r = BitmapToControl(r)

    Try
      Using gPath As GraphicsPath = FloaterPath.Clone
        BitmapToControl(gPath)
        g.SetClip(gPath)
        g.DrawImage(uFloaterBitmap, r.X, r.Y, r.Width, r.Height)

        g.SetClip(r)
        If FloaterOutline Then uDrawDashedPath({Color.White, Color.Black}, 3, gPath, g)
      End Using

    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

  End Sub

  Private Sub uDrawDashedPath(colors() As Color, dashLength As Double, gPath As GraphicsPath, gf As Graphics, _
    Optional usePolygon As Boolean = False)
    ' draws a dashed line with all the colors filled in

    Dim i As Integer
    Dim dashLengths(1) As Single

    dashLengths(0) = dashLength
    dashLengths(1) = dashLength * UBound(colors)
    If dashLengths(1) = 0 Then dashLengths(1) = dashLengths(0)

    Try
      Using gPen As Pen = New Pen(colors(0), 1)
        For i = 0 To UBound(colors)
          gPen.DashStyle = DashStyle.Custom
          gPen.DashPattern = dashLengths
          gPen.DashOffset = i * dashLengths(0)
          gPen.Color = colors(i)
          If usePolygon Then
            gf.DrawPolygon(gPen, gPath.PathPoints)
          Else
            gf.DrawPath(gPen, gPath)
          End If
        Next i
      End Using

    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

  End Sub

  Sub assignFloater()
    ' draws uFLoaterBitmap onto uBitmap

    Dim mx As New Matrix

    If FloaterBitmap Is Nothing Or FloaterPath Is Nothing Then Exit Sub

    Try
      Using g As Graphics = Graphics.FromImage(uBitmap),
            gPath As GraphicsPath = FloaterPath.Clone
        mx.Translate(FloaterPosition.X, FloaterPosition.Y)
        gPath.Transform(mx)
        g.SetClip(gPath)
        g.DrawImage(uFloaterBitmap, FloaterPosition.X, FloaterPosition.Y, uFloaterBitmap.Width, uFloaterBitmap.Height)
      End Using
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    ClearFloater()
    Zoom()

  End Sub

  Sub DrawLine(pp As List(Of Point), LineColor As Color,
    Optional LineWidth As Double = 1, Optional Quality As SmoothingMode = Drawing2D.SmoothingMode.HighQuality)
    DrawLine(pp.ToArray, LineColor, LineWidth, Quality)
  End Sub

  Sub DrawLine(pp() As Point, LineColor As Color,
    Optional LineWidth As Double = 1, Optional Quality As SmoothingMode = Drawing2D.SmoothingMode.HighQuality)
    ' Public sub to draw line to bitmap
    Using _
        g As Graphics = Graphics.FromImage(uBitmap),
        gpen As Pen = New Pen(Color.White)
      gpen.Color = LineColor
      gpen.Width = LineWidth
      g.SmoothingMode = Quality
      g.DrawLines(gpen, pp)
    End Using
  End Sub

  Sub uDrawLine(g As Graphics, pp() As Point, LineColor As Color, _
    Optional LineWidth As Double = 1, Optional Quality As SmoothingMode = Drawing2D.SmoothingMode.HighQuality)
    ' private, draw a line on g
    Using gpen As Pen = New Pen(Color.White)
      gpen.Color = LineColor
      gpen.Width = LineWidth
      g.SmoothingMode = Quality
      g.DrawLines(gpen, pp)
    End Using

  End Sub

  Sub DrawText(g As Graphics, TextString As String, TextCenter As Point, _
               TextFont As Font, ForeColor As Color, BackColor As Color, BackFilled As Boolean, _
               TextAngle As Double, TextFmt As StringFormat)
    ' uses globals gfont, textangle

    Dim tSize As SizeF
    Dim mx As New Matrix
    Dim r As Rectangle

    If TextString = "" Then Exit Sub

    tSize = g.MeasureString(TextString, TextFont, New Point(0, 0), TextFmt)

    If TextAngle <> 0 Then
      ' rotate matrix for gpath
      g.TranslateTransform(TextCenter.X, TextCenter.Y)
      g.RotateTransform(-TextAngle)
      g.TranslateTransform(-TextCenter.X, -TextCenter.Y)
    End If

    If BackFilled Then
      r = New Rectangle(TextCenter.X - tSize.Width \ 2 - tSize.Height * 0.3, TextCenter.Y - tSize.Height \ 2, _
                        tSize.Width + tSize.Height * 0.6, tSize.Height)
      Using gbrush As New SolidBrush(BackColor)
        g.FillRectangle(gbrush, r)
      End Using
    End If

    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit

    Using gbrush As New SolidBrush(ForeColor)
      g.DrawString(TextString, TextFont, New SolidBrush(ForeColor), TextCenter, TextFmt)
    End Using

  End Sub

  Private Sub rubberDraw(g As Graphics)
    ' copies the rubber parameters to draw parameters, then calls Draw to draw the rubber band lines.

    If RubberEnabled Then
      g.SmoothingMode = SmoothingMode.HighQuality
      g.PixelOffsetMode = PixelOffsetMode.HighQuality
      g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit

      DrawShape = RubberShape
      DrawDashed = RubberDashed
      DrawForeColor = RubberColor
      DrawBackColor = rubberBackColor
      DrawLineWidth = RubberLineWidth
      DrawBox = RubberBox
      DrawString = RubberString
      DrawAngle = RubberAngle
      DrawTextFmt = RubberTextFmt
      DrawFilled = RubberFilled
      DrawPoints = New List(Of Point)
      DrawPoints.AddRange(RubberPoints)
      If uDrawPath IsNot Nothing Then uDrawPath.Dispose() : uDrawPath = Nothing
      If uRubberPath IsNot Nothing Then uDrawPath = uRubberPath.Clone
      If uDrawfont IsNot Nothing Then uDrawfont.Dispose() : uDrawfont = Nothing
      If uRubberFont IsNot Nothing Then uDrawfont = uRubberFont.Clone

      Draw(RubberShape, g)
    End If

  End Sub

  Public Sub Draw(drawingShape As shape, g As Graphics)

    ' draws lines for both rubberbands (pviewer paint event) and bitmaps

    g.SmoothingMode = SmoothingMode.HighQuality
    g.PixelOffsetMode = PixelOffsetMode.HighQuality

    Try

      Select Case drawingShape
        Case shape.Box
          If (DrawBox.Width >= 2 And DrawBox.Height >= 2) Then
            If DrawDashed Then
              Using gPath As GraphicsPath = New GraphicsPath
                gPath.AddRectangle(DrawBox)
                uDrawDashedPath({DrawForeColor, DrawBackColor}, 3, gPath, g)
              End Using
            Else
              g.DrawRectangle(New Pen(DrawForeColor, DrawLineWidth), DrawBox)
            End If
          End If

        Case shape.Arrow

        Case shape.Text
          If DrawPoints.Count >= 1 Then
            DrawText(g, DrawString, DrawPoints(0), DrawFont, DrawForeColor, DrawBackColor, _
                    DrawFilled, DrawAngle, DrawTextFmt)
          End If

        Case shape.Path
          If DrawDashed Then
            ''uDrawLine(g, udrawPath.PathPoints, Color.Red)
            uDrawDashedPath({DrawForeColor, DrawBackColor}, 3, uDrawPath, g)

          Else
            If DrawFilled Then ' for text, points are for filled background rectangle
              If DrawPoints.Count > 0 Then g.FillPolygon(New SolidBrush(DrawBackColor), DrawPoints.ToArray)
              g.FillPath(New SolidBrush(DrawForeColor), uDrawPath)
            Else ' for other stuff. Points are for drawing points.
              g.DrawPath(New Pen(DrawForeColor, DrawLineWidth), uDrawPath)
              For Each p As Point In DrawPoints
                g.FillRectangle(New SolidBrush(DrawBackColor),
                  New Rectangle(p.X, p.Y, DrawLineWidth * 2, DrawLineWidth * 2))
              Next p
            End If
          End If

        Case shape.Ellipse, shape.Circle
          If (DrawBox.Width >= 2 And DrawBox.Height >= 2) Then
            If DrawDashed Then
              Using gPath As GraphicsPath = New GraphicsPath
                gPath.AddEllipse(DrawBox)
                uDrawDashedPath({DrawForeColor, DrawBackColor}, 3, gPath, g)
              End Using
            Else
              g.DrawEllipse(New Pen(DrawForeColor, DrawLineWidth), DrawBox)
            End If
          End If

        Case shape.Line, shape.Polygon
          If DrawPoints.Count > 1 Then
            If DrawDashed AndAlso DrawPoints.Count > 2 Then
              Using gPath As GraphicsPath = New GraphicsPath
                gPath.AddLines(DrawPoints.ToArray)
                If DrawShape = shape.Polygon Then
                  uDrawDashedPath({DrawForeColor, DrawBackColor}, 3, gPath, g, True) ' uses drawpolygon
                Else ' don't bother to close the polygon
                  uDrawDashedPath({DrawForeColor, DrawBackColor}, 3, gPath, g, False) ' uses drawpath
                End If
              End Using
            Else
              If DrawShape = shape.Polygon Then
                g.DrawPolygon(New Pen(DrawForeColor, DrawLineWidth), DrawPoints.ToArray)
              Else
                g.DrawLines(New Pen(DrawForeColor, DrawLineWidth), DrawPoints.ToArray)
              End If
            End If
          End If

        Case shape.Measure
          If DrawPoints.Count > 0 Then
            Dim pin As New Pen(DrawForeColor, DrawLineWidth)
            pin.EndCap = LineCap.ArrowAnchor
            pin.StartCap = LineCap.ArrowAnchor
            g.DrawLines(pin, DrawPoints.ToArray)
          End If
      End Select

    Catch ex As Exception
      MsgBox("Error drawing: " & ex.Message)
    End Try

  End Sub

  Protected Overrides Sub OnPaint(e As PaintEventArgs)
    MyBase.OnPaint(e)
    If FloaterVisible And (uFloaterBitmap IsNot Nothing) Then uDrawFloater(e.Graphics) ' draw floater bitmap
    rubberDraw(e.Graphics) ' draw rubberband box, line, etc.
  End Sub

  Protected NotOverridable Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keydata As Keys) As Boolean
    If keydata = Keys.Right Or keydata = Keys.Left Or keydata = Keys.Up Or keydata = Keys.Down Then
      OnKeyDown(New KeyEventArgs(keydata))
      ProcessCmdKey = True
    Else
      ProcessCmdKey = MyBase.ProcessCmdKey(msg, keydata)
    End If
  End Function

  Private Sub pViewer_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseWheel, Me.MouseClick
    '' changed 7/24/17 instead of me.enter, handles me.mousewheel and me.click. 
    '' test frmComment with a bad date and mousewheel zoom in frmmainf
    MyBase.Select()
  End Sub

  Sub clearframes()

    If pageBmp IsNot Nothing Then
      For Each bmp As Bitmap In pageBmp
        If bmp IsNot Nothing Then bmp.Dispose()
      Next bmp
    End If
    pageBmp = New List(Of Bitmap)

  End Sub


  Private Sub pViewer_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    clearframes()
    If uBitmap IsNot Nothing Then uBitmap.Dispose()
    If BitmapPath IsNot Nothing Then BitmapPath.Dispose()
    If uFloaterBitmap IsNot Nothing Then uFloaterBitmap.Dispose()
    If FloaterPath IsNot Nothing Then FloaterPath.Dispose()
    If uGraphics IsNot Nothing Then uGraphics.Dispose()
    If uRubberPath IsNot Nothing Then uRubberPath.Dispose()
    If uRubberFont IsNot Nothing Then uRubberFont.Dispose()
    If uDrawPath IsNot Nothing Then uDrawPath.Dispose()
    If uDrawfont IsNot Nothing Then uDrawfont.Dispose()
  End Sub

End Class
