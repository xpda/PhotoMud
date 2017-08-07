Imports System.Drawing.Drawing2D


Public Class ctlViewCompare
  Implements IDisposable

  Dim uZoom As Double
  Dim uNextButtons As Boolean = False
  Dim uSingleView As Boolean = False
  Property pCenter As Point = Point.Empty

  Event MoveNext(ByVal Inc As Integer)
  Event Zoomed()

  Dim downX As Integer
  Dim downY As Integer
  Dim timer1 As New Timer

  Public Sub New()

    InitializeComponent() ' This is required by the designer.

    pView0.Visible = True
    pView1.Visible = True
    pView0.BorderStyle = BorderStyle.Fixed3D
    pView1.BorderStyle = BorderStyle.Fixed3D
    pView0.InterpolationMode = InterpolationMode.NearestNeighbor
    pView1.InterpolationMode = InterpolationMode.NearestNeighbor
    pView0.Dock = DockStyle.None
    pView1.Dock = DockStyle.None
    pView0.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Right
    pView1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Right
    pView0.ZoomFactor = 0.5
    pView1.ZoomFactor = 0.5

    lbPic1.Text = "Original"
    lbPic2.Text = "Modified"
    lbPic1.TextAlign = ContentAlignment.TopLeft
    lbPic2.TextAlign = ContentAlignment.TopRight

    lbPoint.Text = ""

  End Sub

  Property SingleView As Boolean
    Get
      Return uSingleView
    End Get
    Set(ByVal value As Boolean)
      If value <> uSingleView Then
        uSingleView = value
        If uSingleView Then
          pView0.Visible = False
          lbPic1.Visible = False
          lbPic2.Visible = False
        Else
          pView0.Visible = True
          lbPic1.Visible = True
          lbPic2.Visible = True
        End If
        ctlViewCompare_Resize(Nothing, Nothing)
      End If
    End Set
  End Property

  Property NextButtons As Boolean
    Get
      Return uNextButtons
    End Get
    Set(ByVal x As Boolean)
      uNextButtons = x
      ctlViewCompare_Resize(Nothing, Nothing)
    End Set
  End Property

  Property zoomFactor As Double
    Get
      Return uZoom
    End Get
    Set(ByVal x As Double)
      uZoom = x
    End Set
  End Property

  Private Sub ctlViewCompare_Resize(sender As Object, e As EventArgs) Handles Me.Resize

    pView0.Top = 4
    pView0.Left = 4
    pView0.Width = (Me.Size.Width) \ 2 - (pView0.Left * 3)
    pView0.Height = Me.Size.Height - pView0.Top - cmdZoomin.Height * 1.7

    pView1.Top = pView0.Top
    pView1.Left = pView0.Left + pView0.Width + pView0.Left
    pView1.Width = pView0.Width
    pView1.Height = pView0.Height

    If SingleView Then ' only one pview is active
      pView1.Top = pView0.Top
      pView1.Left = pView0.Left
      pView1.Width = Me.Size.Width - pView0.Left
      pView1.Height = pView0.Height
    Else
      lbPic1.Left = pView0.Left * 2
      lbPic1.Top = pView0.Top + pView0.Height + lbPic1.Height \ 4
      lbPic2.Left = pView1.Left + pView1.Width - lbPic2.Width - pView0.Left
      lbPic2.Top = lbPic1.Top
    End If

    Me.Controls.Add(pView0)
    Me.Controls.Add(pView1)

    cmdZoomin.Top = pView0.Top + pView0.Height + cmdZoomin.Height \ 5
    cmdZoomin.Left = pView0.Left + pView0.Width + pView0.Left - pView0.Left * 2 - cmdZoomin.Width
    cmdZoomout.Top = cmdZoomin.Top
    cmdZoomout.Left = cmdZoomin.Left + cmdZoomout.Width + pView0.Left * 4

    lbZoom.Top = lbPic1.Top
    lbZoom.Left = (lbPic2.Left + cmdZoomout.Left) \ 2 - lbZoom.Width \ 2

    lbPoint.Top = lbPic1.Top
    lbPoint.Left = lbPic1.Left + lbZoom.Width * 2

    If uNextButtons Then
      cmdNext.Visible = True
      cmdPrevious.Visible = True
      cmdZoomin.Left = pView0.Left + pView0.Width + pView0.Left
      cmdNext.Left = cmdPrevious.Left + cmdNext.Width + pView0.Left * 4
      cmdZoomout.Left = cmdZoomin.Left + cmdZoomout.Width + pView0.Left * 4
      lbPoint.Visible = False

    Else
      cmdNext.Visible = False
      cmdPrevious.Visible = False
    End If

  End Sub

  Public Sub zoom(relZoom As Double)
    If Not SingleView Then pView0.Zoom(relZoom * pView0.ZoomFactor)
    pView1.Zoom(relZoom * pView1.ZoomFactor)
    zoomLabel()
  End Sub

  Private Sub pview_MouseWheel(ByVal sender As Object, ByVal e As MouseEventArgs) _
    Handles pView0.MouseWheel, pView1.MouseWheel

    If Not SingleView Then main.mouseWheelZoom(pView0, e, Nothing, 1.2)
    main.mouseWheelZoom(pView1, e, Nothing, 1.2)
    RaiseEvent Zoomed()
    zoomLabel()
  End Sub

  Private Sub pview_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) _
    Handles pView0.MouseUp, pView1.MouseUp
    RaiseEvent Zoomed()
  End Sub

  Private Sub pview_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) _
    Handles pView0.MouseMove, pView1.MouseMove

    Dim dx As Integer
    Dim dy As Integer
    Dim rv As pViewer
    rv = sender

    rv.Cursor = Cursors.Hand
    If e.Button = MouseButtons.Left Then
      dx = (e.X - downX)
      dy = (e.Y - downY)
      If Not SingleView Then pView0.Pan(dx, dy)
      pView1.Pan(dx, dy)
      downX = e.X
      downY = e.Y
    End If

  End Sub

  Private Sub pview_MouseDown(sender As Object, e As MouseEventArgs) Handles pView0.MouseDown, pView1.MouseDown

    Dim pview As pViewer

    pview = sender

    downX = e.X
    downY = e.Y
    If sender Is pView0 And lbPoint.Visible Then  ' record center point
      pCenter = pview.ControlToBitmap(New Point(downX, downY))
    End If

  End Sub

  Public Sub ZoomViews(absZoom As Double)
    If Not SingleView Then pView0.Zoom(absZoom) ' changed from zero, 5/13/15
    pView1.Zoom(absZoom)
    RaiseEvent Zoomed()
    zoomLabel()
  End Sub

  Public Function imageCenter(p As PointF) As PointF
    If pView1.Bitmap IsNot Nothing Then
      Return New PointF(pView1.Bitmap.Width / 2, pView1.Bitmap.Height / 2)
    End If
  End Function

  Public Sub Clear()
    clearBitmap(pView0.Bitmap)
    clearBitmap(pView1.Bitmap)
  End Sub

  Public Sub zoomLabel()
    lbZoom.Text = "Zoom: " & Format(pView1.ZoomFactor, "#,##0.#") & "x"
  End Sub

  Private Sub cmdZoomin_Click(sender As Object, e As EventArgs) Handles cmdZoomin.Click
    zoom(2)
    RaiseEvent Zoomed()
  End Sub

  Private Sub cmdZoomout_Click(sender As Object, e As EventArgs) Handles cmdZoomout.Click
    zoom(0.5)
    RaiseEvent Zoomed()
  End Sub

  Public Sub Repaint()
    If Not SingleView Then pView0.Refresh()
    pView1.Refresh()
  End Sub

  Private Sub cmdNext_Click(sender As Object, e As EventArgs) Handles cmdNext.Click, cmdPrevious.Click

    If sender Is cmdNext Then
      RaiseEvent MoveNext(1)
    Else
      RaiseEvent MoveNext(-1)
    End If
  End Sub

  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    'UserControl overrides dispose to clean up the component list.
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

End Class
