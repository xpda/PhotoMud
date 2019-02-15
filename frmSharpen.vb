Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports ImageMagick

Public Class frmSharpen
  Inherits Form

  Dim Loading As Boolean = True
  Dim Sliding As Boolean

  Dim downx As Double
  Dim downy As Double

  Dim gpath As GraphicsPath = Nothing

  Dim imageReduced As Boolean
  Dim xreduced As Double

  Dim pCenter As PointF ' not used yet

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

    nmSharpen0_valuechanged(Sender, e) ' for pressing Enter
    Timer1.Interval = 30
    iniSharpen(0) = trkSharpen0.Value
    iniSharpen(1) = trkSharpen1.Value
    iniSharpen(2) = trkSharpen2.Value
    If optSharpen.Checked Then
      iniSharpen(3) = 0
    ElseIf optUnsharp.Checked Then
      iniSharpen(3) = 1
    End If

    Me.Cursor = Cursors.WaitCursor

    drawSharp(True)

    Me.Cursor = Cursors.Default
    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Private Sub frmSharpen_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Loading = True

    ' amount
    trkSharpen0.Minimum = 0
    trkSharpen0.Maximum = 100
    nmSharpen0.Minimum = trkSharpen0.Minimum
    nmSharpen0.Maximum = trkSharpen0.Maximum

    ' radius
    trkSharpen1.Minimum = 1
    trkSharpen1.Maximum = 10
    nmSharpen1.Minimum = trkSharpen1.Minimum
    nmSharpen1.Maximum = trkSharpen1.Maximum

    ' threshold
    trkSharpen2.Minimum = 0
    trkSharpen2.Maximum = 100
    nmSharpen2.Minimum = trkSharpen2.Minimum
    nmSharpen2.Maximum = trkSharpen2.Maximum

    If iniSharpen(0) >= trkSharpen0.Minimum And iniSharpen(0) <= trkSharpen0.Maximum Then trkSharpen0.Value = iniSharpen(0) Else trkSharpen0.Value = trkSharpen0.Minimum
    If iniSharpen(1) >= trkSharpen1.Minimum And iniSharpen(1) <= trkSharpen1.Maximum Then trkSharpen1.Value = iniSharpen(1) Else trkSharpen1.Value = trkSharpen1.Minimum
    If iniSharpen(2) >= trkSharpen2.Minimum And iniSharpen(2) <= trkSharpen2.Maximum Then trkSharpen2.Value = iniSharpen(2) Else trkSharpen2.Value = trkSharpen2.Minimum
    nmSharpen0.Value = trkSharpen0.Value
    nmSharpen1.Value = trkSharpen1.Value
    nmSharpen2.Value = trkSharpen2.Value

    If iniSharpen(3) = 0 Then
      optSharpen.Checked = True
    ElseIf iniSharpen(3) = 1 Then
      optUnsharp.Checked = True
    End If

    If optSharpen.Checked Then
      nmSharpen2.Visible = False
      trkSharpen2.Visible = False
      lbUnsharp2.Visible = False
    Else
      nmSharpen1.Visible = True
      nmSharpen2.Visible = True
      trkSharpen1.Visible = True
      trkSharpen2.Visible = True
      lbUnsharp1.Visible = True
      lbUnsharp2.Visible = True
    End If

    If frmMain.mView.FloaterPath IsNot Nothing Then gpath = frmMain.mView.FloaterPath.Clone

    ' if it's too big, reduce the size and assign that.
    If qImage.Width * qImage.Height > bigMegapix Then
      imageReduced = True
      xreduced = getSmallerImage(qImage, aview.pView0)
      aview.pView1.setBitmap(aview.pView0.Bitmap)
    Else
      imageReduced = False
      aview.pView0.setBitmap(qImage)
      aview.pView1.setBitmap(qImage)
      xreduced = 1
    End If

    aview.ZoomViews(0.5)

    pCenter = New PointF(CSng(aview.pView0.Bitmap.Width / 2), CSng(aview.pView0.Bitmap.Height / 2))

    Loading = False
    Sliding = False

  End Sub

  Private Sub trkSharpen0_scroll(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles trkSharpen0.Scroll, trkSharpen1.Scroll, trkSharpen2.Scroll
    If Loading Then Exit Sub
    Sliding = True
    If Sender Is trkSharpen0 Then nmSharpen0.Value = trkSharpen0.Value
    If Sender Is trkSharpen1 Then nmSharpen1.Value = trkSharpen1.Value
    If Sender Is trkSharpen2 Then nmSharpen2.Value = trkSharpen2.Value
    Timer1.Interval = 150 : Timer1.Stop() : Timer1.Start() ' draw after 150 milliseconds
    Sliding = False
  End Sub

  Private Sub nmSharpen0_valuechanged(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles nmSharpen0.ValueChanged, nmSharpen1.ValueChanged, nmSharpen2.ValueChanged
    If Loading Or Sliding Then Exit Sub
    If Sender Is nmSharpen0 Then trkSharpen0.Value = nmSharpen0.Value
    If Sender Is nmSharpen1 Then trkSharpen1.Value = nmSharpen1.Value
    If Sender Is nmSharpen2 Then trkSharpen2.Value = nmSharpen2.Value
    Timer1.Interval = 150 : Timer1.Stop() : Timer1.Start() ' draw after 150 milliseconds
  End Sub

  Private Sub aview_Resize(sender As Object, e As EventArgs) Handles aview.Resize
    aview.zoom(1)
  End Sub

  Sub aview_zoomed() Handles aview.Zoomed, Me.Resize
    Timer1.Stop() : Timer1.Interval = 150 : Timer1.Start() ' calls drawsharp
  End Sub

  Sub drawSharp(ByRef fullBitmap As Boolean)

    Dim sigma, threshold As Double
    Dim radius As Integer
    Dim amount As Double
    Static busy As Boolean = False

    Dim img As MagickImage

    If aview.pView0.Bitmap Is Nothing Then Exit Sub
    If busy Then Exit Sub
    busy = True

    Me.Cursor = Cursors.WaitCursor

    If fullBitmap Then
      aview.pView1.ClearSelection()
      img = New MagickImage(aview.pView0.Bitmap)

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

    Try
      If optSharpen.Checked Then
        sigma = trkSharpen0.Value / 20
        radius = trkSharpen1.Value
        img.Sharpen(radius, sigma, ImageMagick.Channels.RGB)

      ElseIf optUnsharp.Checked Then
        sigma = trkSharpen0.Value / 100
        radius = trkSharpen1.Value
        amount = trkSharpen2.Value / 20
        threshold = 0.02
        img.Unsharpmask(radius, sigma, amount, threshold, ImageMagick.Channels.RGB)
      End If
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    saveStuff(img, aview.pView1, gpath, fullBitmap) ' save img to pview0 or qimage, depending on fullbitmap
    aview.Repaint()
    aview.zoomLabel()

    Me.Cursor = Cursors.Default
    busy = False

  End Sub

  Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
    ' redraw after some milliseconds
    Timer1.Stop()
    drawSharp(False)
  End Sub

  Private Sub optSharpen_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) _
    Handles optSharpen.CheckedChanged, optUnsharp.CheckedChanged

    Dim opt As RadioButton = sender

    If Loading Then Exit Sub

    If opt.Checked Then
      If optSharpen.Checked Then
        nmSharpen2.Visible = False
        trkSharpen2.Visible = False
        lbUnsharp2.Visible = False
      ElseIf optUnsharp.Checked Then
        nmSharpen1.Visible = True
        nmSharpen2.Visible = True
        trkSharpen1.Visible = True
        trkSharpen2.Visible = True
        lbUnsharp1.Visible = True
        lbUnsharp2.Visible = True
        nmSharpen1.Refresh()
        nmSharpen2.Refresh()
        trkSharpen1.Refresh()
        trkSharpen2.Refresh()
        lbUnsharp1.Refresh()
        lbUnsharp2.Refresh()
      End If

      drawSharp(False)
    End If

  End Sub

  Private Sub form_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
    Timer1.Stop()
    If gpath IsNot Nothing Then gpath.Dispose() : gpath = Nothing
  End Sub

End Class