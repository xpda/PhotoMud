'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports ImageMagick

Public Class frmBorder
  Inherits Form

  Dim Loading As Boolean = True
  Dim Sliding As Boolean = False

  Dim gPath As GraphicsPath

  Dim imageReduced As Boolean
  Dim xreduced As Double
  Dim qImage0 As Bitmap ' this is the source shown in pview1

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

    drawBorder(True) ' saves result to qimage

    iniBorderColor = cmdBorderColor.BackColor
    iniBorderOuterThickness = nmOuter.Value
    iniBorderInnerThickness = nmInner.Value

    If opt2D.Checked Then
      iniBorderStyle = 0
    ElseIf opt3D.Checked Then
      iniBorderStyle = 1
    ElseIf optRaise.Checked Then
      iniBorderStyle = 2
    ElseIf optButton.Checked Then
      iniBorderStyle = 3
    End If

    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Sub aview_zoomed() Handles aView.Zoomed, Me.Resize
    Timer1.Stop() : Timer1.Interval = 150 : Timer1.Start() ' calls draw
  End Sub

  Private Sub frmBorder_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Dim i As Integer
    Dim z As Double

    Timer1.Enabled = False

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    aView.SingleView = True

    cmdBorderColor.BackColor = iniBorderColor

    trkOuter.Value = Min(iniBorderOuterThickness, trkOuter.Maximum)
    trkInner.Value = Min(iniBorderInnerThickness, trkInner.Maximum)
    nmOuter.Value = trkOuter.Value
    nmInner.Value = trkInner.Value

    If qImage IsNot Nothing Then
      i = qImage.Width \ 5
      trkInner.Maximum = i
      trkOuter.Maximum = i
      nmInner.Maximum = i
      nmOuter.Maximum = i
    End If

    Select Case iniBorderStyle
      Case 0
        opt2D.Checked = True
      Case 1
        opt3D.Checked = True
      Case 2
        optRaise.Checked = True
      Case 3
        optButton.Checked = True
    End Select

    If frmMain.mView.FloaterPath IsNot Nothing Then gPath = frmMain.mView.FloaterPath.Clone

    ' if it's too big, reduce the size and assign that.
    If qImage.Width * qImage.Height > bigMegapix / 4 Then
      imageReduced = True
      xreduced = getSmallerImage(qImage, aView.pView1, True)
      qImage0 = aView.pView1.Bitmap.Clone
    Else
      imageReduced = False
      aView.pView1.setBitmap(qImage)
      xreduced = 1
      qImage0 = qImage.Clone
    End If

    z = Min(aView.pView1.ClientSize.Width / qImage0.Width, aView.pView1.ClientSize.Height / qImage0.Height)
    z = z * 0.8
    aView.ZoomViews(z)

    Sliding = False
    Loading = False

  End Sub

  Private Sub opt_CheckedChanged(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles opt2D.CheckedChanged, opt3D.CheckedChanged, optRaise.CheckedChanged, optButton.CheckedChanged

    Dim opt As RadioButton = Sender

    If opt.Checked Then

      If opt3D.Checked Then
        trkInner.Visible = True
        nmInner.Visible = True
        lbInner.Visible = True
        lbOuter.Text = "Outer thickness (pixels)"
      Else
        trkInner.Visible = False
        nmInner.Visible = False
        lbInner.Visible = False
        lbOuter.Text = "Border thickness (pixels)"
      End If

    End If

    If Not Loading Then drawBorder(False)

  End Sub

  Private Sub cmdBorderColor_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdBorderColor.Click

    Dim result As DialogResult

    colorDialog1.Color = cmdBorderColor.BackColor

    Try
      result = colorDialog1.ShowDialog()
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    If result = DialogResult.OK Then
      cmdBorderColor.BackColor = colorDialog1.Color
    End If

    drawBorder(False)

  End Sub

  Private Sub trkInner_scroll(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles trkInner.Scroll, trkOuter.Scroll

    If Loading Then Exit Sub

    Sliding = True
    If Sender Is trkInner Then
      nmInner.Value = trkInner.Value
    Else
      nmOuter.Value = trkOuter.Value
    End If

    Timer1.Interval = 150 : Timer1.Stop() : Timer1.Start() ' draw after 150 milliseconds
    Sliding = False

  End Sub

  Private Sub nmOuter_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) _
    Handles nmOuter.ValueChanged, nmInner.ValueChanged

    If Sliding Or Loading Then Exit Sub
    If sender Is nmInner Then
      trkInner.Value = nmInner.Value
    Else
      trkOuter.Value = nmOuter.Value
    End If

    Timer1.Interval = 150 : Timer1.Stop() : Timer1.Start() ' draw after 150 milliseconds

  End Sub

  Sub drawBorder(fullBitmap As Boolean)

    Dim OuterWidth As Integer
    Dim InnerWidth As Integer
    Dim i As Integer
    Dim p As Point

    Static busy As Boolean = False
    Dim img As MagickImage

    If aView.pView1.Bitmap Is Nothing Then Exit Sub
    If busy Then Exit Sub
    busy = True

    Me.Cursor = Cursors.WaitCursor

    OuterWidth = trkOuter.Value
    InnerWidth = trkInner.Value

    If fullBitmap Then
      img = New MagickImage(qImage)

    Else
      img = New MagickImage(qImage0) ' reduced size source
      InnerWidth = CInt(InnerWidth * xreduced)
      OuterWidth = CInt(OuterWidth * xreduced)
    End If

    If opt2D.Checked Then
      ' border
      img.BorderColor = cmdBorderColor.BackColor
      img.Border(OuterWidth)

    ElseIf opt3D.Checked Then
      '3-d frame
      i = OuterWidth * 2 + InnerWidth
      Dim geo As New MagickGeometry(i & "x" & i & "+" & OuterWidth & "+" & OuterWidth)
      img.Frame(geo)

    ElseIf optRaise.Checked Then
      'bevel
      img.Raise(OuterWidth)

    ElseIf optButton.Checked Then
      ' buttonize
      Using img2 As MagickImage = New MagickImage(Color.Gray, img.Width, img.Height)
        img2.Colorize(Color.Gray, New Percentage(OuterWidth))
        img2.Raise(OuterWidth)
        img2.Normalize()
        img2.Blur(0, OuterWidth, Channels.RGB)
        img.Composite(img2, CompositeOperator.HardLight)
      End Using

    End If

    If fullBitmap Then
      If qImage IsNot Nothing Then qImage.Dispose()
      qImage = img.ToBitmap
      aView.zoom(1)
    Else
      p = aView.pView1.CenterPoint
      p = New Point(p.X + (img.Width - aView.pView1.Bitmap.Width) \ 2, p.Y + (img.Height - aView.pView1.Bitmap.Height) \ 2)
      Using bmp As Bitmap = img.ToBitmap
        aView.pView1.setBitmap(bmp)
      End Using
      aView.pView1.setCenterPoint(p, True)
    End If

    clearBitmap(img)

    aView.zoomLabel()

    Me.Cursor = Cursors.Default
    busy = False

  End Sub

  Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
    ' redraw after some milliseconds
    Timer1.Stop()
    drawBorder(False)
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