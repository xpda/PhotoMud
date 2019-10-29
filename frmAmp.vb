'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports ImageMagick

Public Class frmAmp
  Inherits Form

  Dim Loading As Boolean = True
  Dim Sliding As Boolean = False

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

    drawAmp(True) ' saves result to qimage

    Me.Cursor = Cursors.Default
    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Sub aview_zoomed() Handles aView.Zoomed, Me.Resize
    Timer1.Stop() : Timer1.Interval = 150 : Timer1.Start() ' calls draw
  End Sub

  Private Sub frmAmp_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Loading = True

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    nmBlack.Minimum = 0
    nmBlack.Maximum = 100
    nmBlack.Value = 8
    nmBlack.Enabled = True

    nmWhite.Minimum = 0
    nmWhite.Maximum = 100
    nmWhite.Value = 30
    nmWhite.Enabled = True

    aView.SingleView = True

    If frmMain.mView.FloaterPath IsNot Nothing Then gPath = frmMain.mView.FloaterPath.Clone

    ' if it's too big, reduce the size and assign that.
    If qImage.Width * qImage.Height > bigMegapix Then
      imageReduced = True
      xreduced = getSmallerImage(qImage, aView.pView1)
    Else
      imageReduced = False
      aView.pView1.setBitmap(qImage)
      xreduced = 1
    End If

    aView.pView1.Zoom(0.5)

    Sliding = False
    Loading = False

  End Sub

  Private Sub nmAmp_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles nmBlack.ValueChanged, nmWhite.ValueChanged
    If Loading Then Exit Sub
    Timer1.Interval = 150 : Timer1.Stop() : Timer1.Start() ' draw after 150 milliseconds
  End Sub

  Sub drawAmp(fullbitmap As Boolean)
    Static busy As Boolean = False
    Dim img As MagickImage

    If aView.pView1.Bitmap Is Nothing Then Exit Sub
    If busy Then Exit Sub
    busy = True

    Me.Cursor = Cursors.WaitCursor

    If fullbitmap Then
      aView.pView1.ClearSelection()
      img = New MagickImage(qImage)

    Else
      ' only operate on the visible part of the bitmap
      ' use a floater image of the bitmap for the target
      aView.pView1.SetSelection(aView.pView1.ClientRectangle)
      aView.pView1.InitFloater()
      aView.pView1.FloaterVisible = True
      aView.pView1.FloaterOutline = False
      img = New MagickImage(aView.pView1.FloaterBitmap)
    End If

    img.HasAlpha = False

    Try
      img.WhiteThreshold(nmWhite.Value)
      img.BlackThreshold(nmBlack.Value)
      img.Level(CByte(nmBlack.Value), CByte(nmWhite.Value))
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    saveStuff(img, aView.pView1, gPath, fullbitmap)
    aView.Repaint()
    aView.zoomLabel()

    Me.Cursor = Cursors.Default
    busy = False

  End Sub

  Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
    ' redraw after some milliseconds
    Timer1.Stop()
    drawAmp(False)
  End Sub

  Function scaled(ByVal x As Double, ByVal nm As NumericUpDown) As Double
    scaled = x * xreduced
    scaled = Max(scaled, nm.Minimum)
    scaled = Min(scaled, nm.Maximum)
  End Function

  Private Sub _FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
    Timer1.Stop()
    If gPath IsNot Nothing Then gPath.Dispose() : gPath = Nothing
  End Sub

End Class