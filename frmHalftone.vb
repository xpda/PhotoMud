Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports System.Collections.Generic
Imports ImageMagick

Public Class frmHalftone
  Inherits Form

  Dim Loading As Boolean = True
  Dim Sliding As Boolean = False

  Dim gpath As GraphicsPath = Nothing

  Dim imageReduced As Boolean
  Dim xReduced As Double

  Dim halfMaps As New List(Of String)

  Dim WithEvents timer1 As New Timer

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

    drawHalftone(True) ' saves result to qimage

    Me.Cursor = Cursors.Default
    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Private Sub aview_Resize(sender As Object, e As EventArgs) Handles aview.Resize
    aview.zoom(1)
  End Sub

  Sub aview_zoomed() Handles aview.Zoomed, Me.Resize
    timer1.Stop() : timer1.Interval = 150 : timer1.Start() ' calls draw
  End Sub

  Private Sub Combo1_SelectedIndexChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles Combo1.SelectedIndexChanged
    If Loading Then Exit Sub

    Select Case Combo1.SelectedIndex
      Case 0 ' print
      Case 1 ' view
      Case 2 ' rect
      Case 3 ' circ
      Case 4 ' ellips
      Case 5 ' rand
      Case 6 ' linear
    End Select

    If Not Loading Then drawHalftone(False)

  End Sub

  Private Sub frmHalftone_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Loading = True

    Combo1.Items.Add("Halftone 4x4 (angled)")
    halfMaps.Add("h4x4a")
    Combo1.Items.Add("Halftone 6x6 (angled)")
    halfMaps.Add("h6x6a")
    Combo1.Items.Add("Halftone 8x8 (angled)")
    halfMaps.Add("h8x8a")
    Combo1.Items.Add("Halftone 4x4 (orthogonal)")
    halfMaps.Add("h4x4o")
    Combo1.Items.Add("Halftone 6x6 (orthogonal)")
    halfMaps.Add("h6x6o")
    Combo1.Items.Add("Halftone 8x8 (orthogonal)")
    halfMaps.Add("h8x8o")
    Combo1.Items.Add("Halftone 16x16 (orthogonal)")
    halfMaps.Add("h16x16o")
    Combo1.Items.Add("Ordered 2x2 (dispersed)")
    halfMaps.Add("o2x2")
    Combo1.Items.Add("Ordered 3x3 (dispersed)")
    halfMaps.Add("o3x3")
    Combo1.Items.Add("Ordered 4x4 (dispersed)")
    halfMaps.Add("o4x4")
    Combo1.Items.Add("Ordered 8x8 (dispersed)")
    halfMaps.Add("o8x8")
    Combo1.Items.Add("Circles 5x5 (black)")
    halfMaps.Add("c5x5b")
    Combo1.Items.Add("Circles 5x5 (white)")
    halfMaps.Add("c5x5w")
    Combo1.Items.Add("Circles 6x6 (black)")
    halfMaps.Add("c6x6b")
    Combo1.Items.Add("Circles 6x6 (white)")
    halfMaps.Add("c6x6w")
    Combo1.Items.Add("Circles 7x7 (black)")
    halfMaps.Add("c7x7b")
    Combo1.Items.Add("Circles 7x7 (white)")
    halfMaps.Add("c7x7w")
    Combo1.Items.Add("Checkerboard 2x1 (dither)")
    halfMaps.Add("checks")

    Combo1.SelectedIndex = 5

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
    Combo1_SelectedIndexChanged(Sender, e)

    Loading = False

  End Sub

  Sub drawHalftone(fullbitmap As Boolean)

    Static busy As Boolean = False
    Dim img As MagickImage
    Dim ditherMap As String

    If aview.pView0.Bitmap Is Nothing Then Exit Sub
    If busy Then Exit Sub
    busy = True

    Me.Cursor = Cursors.WaitCursor

    If fullbitmap Then
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

    ditherMap = halfMaps(Combo1.SelectedIndex)

    Try
      img.OrderedDither(ditherMap, Channels.All)
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    saveStuff(img, aview.pView1, gpath, fullbitmap)
    aview.Repaint()
    aview.zoomLabel()

    Me.Cursor = Cursors.Default
    busy = False

  End Sub

  Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles timer1.Tick
    ' redraw after some milliseconds
    timer1.Stop()
    drawHalftone(False)
  End Sub

  Function scaled(ByVal x As Double, ByVal nm As NumericUpDown) As Double
    scaled = x * xReduced
    scaled = Max(scaled, nm.Minimum)
    scaled = Min(scaled, nm.Maximum)
  End Function

  Private Sub _FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
    timer1.Stop()
    If gpath IsNot Nothing Then gpath.Dispose()
  End Sub

End Class