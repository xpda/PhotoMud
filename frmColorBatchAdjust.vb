'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports vb = Microsoft.VisualBasic
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports System.IO
Imports System

Public Class frmColorBatchAdjust
  Inherits Form

  Dim Loading As Boolean = True
  Dim Sliding As Boolean
  Dim downx As Double
  Dim downy As Double
  Dim iPicnum As Integer

  Dim WithEvents Timer1 As New Timer

  Private Sub cmdCancel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click

    colorAdjust = False
    Me.DialogResult = DialogResult.Cancel
    Me.Close()

  End Sub

  Private Sub aview_MoveNext(i As Integer) Handles aview.MoveNext

    If i = 1 Then
      iPicnum = iPicnum + 1
      If iPicnum > tagPath.Count Then iPicnum = 1
    ElseIf i = -1 Then ' previous
      iPicnum = iPicnum - 1
      If iPicnum < 1 Then iPicnum = tagPath.Count
    End If

    loadPic() ' loads tagpath(ipicnum)
    drawPic()

  End Sub

  Private Sub cmdOK_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdOK.Click

    Me.Cursor = Cursors.WaitCursor

    ' parameters for fileconvert
    clrValue(0) = nmBright.Value
    clrValue(1) = nmContrast.Value
    clrValue(2) = nmSaturation.Value
    clrValue(3) = nmRed.Value
    clrValue(4) = nmGreen.Value
    clrValue(5) = nmBlue.Value
    If chkAutoAdjust.Checked Then clrValue(6) = 1 Else clrValue(6) = 0
    If chkPreserveIntensity.Checked Then clrValue(7) = 1 Else clrValue(7) = 0

    Me.Cursor = Cursors.Default

    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Private Sub frmColorBatchAdjust_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Timer1.Enabled = False

    Loading = True

    trkRed.Value = 0
    trkGreen.Value = 0
    trkBlue.Value = 0
    trkBright.Value = 0
    trkContrast.Value = 0
    trkSaturation.Value = 0

    nmRed.Value = trkRed.Value
    nmGreen.Value = trkGreen.Value
    nmBlue.Value = trkBlue.Value
    nmBright.Value = trkBright.Value
    nmContrast.Value = trkContrast.Value
    nmSaturation.Value = trkSaturation.Value

    chkAutoAdjust.Checked = False

    iPicnum = 1
    loadPic() ' loads tagpath(ipicnum)

    Loading = False
    Sliding = False
    colorAdjust = False

  End Sub

  Private Sub TrackBar_scroll(ByVal sender As Object, ByVal e As EventArgs) _
    Handles trkBlue.Scroll, trkGreen.Scroll, trkRed.Scroll, trkBright.Scroll, trkContrast.Scroll, trkSaturation.Scroll

    Dim trk As TrackBar
    trk = sender

    If Sliding Or (aview.pView0.Bitmap Is Nothing) Then Exit Sub
    Sliding = True
    colorAdjust = True

    If sender Is trkRed Then
      nmRed.Value = trk.Value
    ElseIf sender Is trkGreen Then
      nmGreen.Value = trk.Value
    ElseIf sender Is trkBlue Then
      nmBlue.Value = trk.Value
    ElseIf sender Is trkBright Then
      nmBright.Value = trk.Value
    ElseIf sender Is trkContrast Then
      nmContrast.Value = trk.Value
    ElseIf sender Is trkSaturation Then
      nmSaturation.Value = trk.Value
    End If

    Timer1.Interval = 150 : Timer1.Stop() : Timer1.Start() ' draw after 150 milliseconds

    Sliding = False

  End Sub

  Private Sub nm_valuechanged(ByVal sender As Object, ByVal e As EventArgs) _
    Handles nmBlue.ValueChanged, nmGreen.ValueChanged, nmRed.ValueChanged, _
    nmBright.ValueChanged, nmContrast.ValueChanged, nmSaturation.ValueChanged

    Dim nm As NumericUpDown
    nm = sender

    If Sliding Or Loading Or (aview.pView0.Bitmap Is Nothing) Then Exit Sub
    colorAdjust = True

    If sender Is nmRed Then
      trkRed.Value = nm.Value
    ElseIf sender Is nmGreen Then
      trkGreen.Value = nm.Value
    ElseIf sender Is nmBlue Then
      trkBlue.Value = nm.Value
    ElseIf sender Is nmBright Then
      trkBright.Value = nm.Value
    ElseIf sender Is nmContrast Then
      trkContrast.Value = nm.Value
    ElseIf sender Is nmSaturation Then
      trkSaturation.Value = nm.Value
    End If

    Timer1.Interval = 150 : Timer1.Stop() : Timer1.Start() ' draw after 150 milliseconds
    Sliding = False

  End Sub

  Sub drawPic()

    Dim bmp As Bitmap

    aview.pView1.setBitmap(aview.pView0.Bitmap)

    clrValue(0) = nmBright.Value
    clrValue(1) = nmContrast.Value
    clrValue(2) = nmSaturation.Value
    clrValue(3) = nmRed.Value
    clrValue(4) = nmGreen.Value
    clrValue(5) = nmBlue.Value
    If chkAutoAdjust.Checked Then clrValue(6) = 1 Else clrValue(6) = 0

    bmp = aview.pView0.Bitmap.Clone
    batchAdjust(bmp, aview.pView1)
    aview.pView1.setBitmap(bmp)
    clearBitmap(bmp)
    aview.pView1.Zoom()
    aview.Repaint()

  End Sub

  Sub loadPic()

    Dim msg As String = ""

    Using bmp As Bitmap = readBitmap(tagPath(iPicnum), msg)
      aview.pView0.setBitmap(bmp)
    End Using

    If aview.pView0.Bitmap IsNot Nothing Then
      aview.pView1.setBitmap(aview.pView0.Bitmap)
      aview.ZoomViews(0)
    End If

  End Sub

  Private Sub chkAutoAdjust_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkAutoAdjust.CheckedChanged

    If chkAutoAdjust.Checked Then
      trkRed.Enabled = False
      trkGreen.Enabled = False
      trkBlue.Enabled = False
      trkBright.Enabled = False
      trkContrast.Enabled = False
      trkSaturation.Enabled = False

      nmRed.Enabled = False
      nmGreen.Enabled = False
      nmBlue.Enabled = False
      nmBright.Enabled = False
      nmContrast.Enabled = False
      nmSaturation.Enabled = False
    Else
      trkRed.Enabled = True
      trkGreen.Enabled = True
      trkBlue.Enabled = True
      trkBright.Enabled = True
      trkContrast.Enabled = True
      trkSaturation.Enabled = True

      nmRed.Enabled = True
      nmGreen.Enabled = True
      nmBlue.Enabled = True
      nmBright.Enabled = True
      nmContrast.Enabled = True
      nmSaturation.Enabled = True
    End If

    drawPic()

  End Sub

  Private Sub cmdReset_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmdReset.Click

    trkRed.Value = 0
    trkGreen.Value = 0
    trkBlue.Value = 0
    trkBright.Value = 0
    trkContrast.Value = 0
    trkSaturation.Value = 0

    nmRed.Value = trkRed.Value
    nmGreen.Value = trkGreen.Value
    nmBlue.Value = trkBlue.Value
    nmBright.Value = trkBright.Value
    nmContrast.Value = trkContrast.Value
    nmSaturation.Value = trkSaturation.Value

    chkAutoAdjust.Checked = False

    Timer1.Interval = 150 : Timer1.Stop() : Timer1.Start() ' draw after 150 milliseconds

    aview.ZoomViews(0)
    drawPic()

  End Sub

  Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    Timer1.Stop()
    drawPic()
  End Sub

  Sub aview_zoomed() Handles aview.Zoomed, Me.Resize
    Timer1.Stop() : Timer1.Interval = 150 : Timer1.Start() ' calls drawsharp
  End Sub

End Class