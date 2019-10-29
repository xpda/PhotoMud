'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports vb = Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports System.IO

Public Class frmRotate
  Inherits Form

  Dim Processing As Boolean = True
  Dim img As Bitmap
  Dim rollAngle As Double

  Dim WithEvents Timer1 As New Timer

  Private Sub cmdHelp_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdHelp.Click
    Try
      Help.ShowHelp(Me, helpFile, HelpNavigator.Topic, Me.Name & ".html")
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

  Private Sub cmdOK_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdOK.Click

    Dim img2 As Bitmap = Nothing

    txAngle_Leave(Sender, e)

    Me.Cursor = Cursors.WaitCursor

    If trkAngle.Value <> 0 Then
      rotateGraphics(0, 0, img, img2)  ' img2 is target
      If img2 IsNot Nothing Then
        clearBitmap(qImage)
        qImage = img2.Clone
        clearBitmap(img2)
      End If
    End If

    Me.Cursor = Cursors.Default
    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Private Sub cmdCancel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
    clearBitmap(qImage)
    Me.DialogResult = DialogResult.Cancel
    Me.Close()
  End Sub


  Private Sub frmRotate_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
    clearBitmap(img)
    rotateTrim = chkTrim.Checked
  End Sub

  Private Sub frmRotate_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Dim pComments As List(Of PropertyItem)
    Dim uComments As uExif
    Dim picInfo As pictureInfo = Nothing
    Dim s As String = ""
    Dim s1 As String
    Dim i, i1 As Integer

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Processing = True
    Timer1.Enabled = False
    clearBitmap(qImage)

    trkAngle.Value = 0
    txAngle.Text = 0

    chkTrim.Checked = rotateTrim

    Me.Cursor = Cursors.WaitCursor

    img = frmMain.mView.Bitmap.Clone
 
    Me.Cursor = Cursors.Default
    trkAngle_ValueChanged(Sender, e)

    ' get the camera roll angle for auto align
    picInfo = getPicinfo(currentpicPath, True)
    If picInfo.ErrMessage <> "" Then
      uComments = readComments(currentpicPath, True, True)
      pComments = readPropertyItems(currentpicPath)
      formatExifComments(True, False, False, False, s, uComments, picInfo, pComments) ' s has the answer
      i = InStr(s, "Camera Roll Angle:")
      If i > 0 Then
        i = i + 19
        i1 = InStr(i, s, "°")
        s1 = Mid(s, i, i1 - i)
        If IsNumeric(s1) Then
          cmdAuto.Enabled = True
          rollAngle = s1
        Else
          cmdAuto.Enabled = False
          rollAngle = 0
        End If
      End If
    End If

    Timer1.Enabled = True
    Processing = False

  End Sub

  Private Sub Option_CheckedChanged(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles OptReset.CheckedChanged, Opt90.CheckedChanged, Opt180.CheckedChanged, Opt270.CheckedChanged

    Static busy As Boolean = False
    Dim opt As RadioButton

    If busy Then Exit Sub
    busy = True

    opt = Sender

    If opt.Checked Then ' skip the uncheck event
      If opt Is OptReset Then
        trkAngle.Value = 0
        txAngle.Text = "0"
      ElseIf Sender Is Opt90 Then
        trkAngle.Value = 900
        txAngle.Text = "90"
      ElseIf Sender Is Opt270 Then
        trkAngle.Value = -900
        txAngle.Text = "270"
      ElseIf Sender Is Opt180 Then
        trkAngle.Value = 1800
        txAngle.Text = "180"
      End If
    End If

    busy = False
  End Sub

  Private Sub txAngle_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles txAngle.Leave

    Dim rAngle As Double

    If Not IsNumeric(txAngle.Text) Then
      MsgBox("Enter a number between -360 and 360.")
      txAngle.select()
    Else
      rAngle = CDbl(txAngle.Text)
      rAngle = rAngle Mod 360
      If rAngle < 180 Then rAngle = rAngle + 360
      If rAngle > 180 Then rAngle = rAngle - 360
      rAngle = rAngle * 10
      trkAngle.Value = rAngle
    End If

  End Sub

  Private Sub txAngle_TextChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles txAngle.TextChanged

    If Processing Then Exit Sub

    If Opt90.Checked Or Opt270.Checked Or Opt180.Checked Then
      If IsNumeric(txAngle.Text) Then
        If CDbl(txAngle.Text) <> 90 And Opt90.Checked Then Opt90.Checked = False
        If CDbl(txAngle.Text) <> -90 And Opt270.Checked Then Opt270.Checked = False
        If CDbl(txAngle.Text) <> 180 And Opt180.Checked Then Opt180.Checked = False
      End If
    End If

  End Sub

  Private Sub txAngle_Enter(ByVal Sender As Object, ByVal e As EventArgs) Handles txAngle.Enter

    txAngle.SelectionStart = 0
    txAngle.SelectionLength = Len(txAngle.Text)

  End Sub

  Private Sub trkAngle_Scroll(ByVal sender As System.Object, ByVal e As EventArgs) Handles trkAngle.Scroll

    Dim rAngle As Double

    rAngle = trkAngle.Value / 10
    txAngle.Text = Format(rAngle, "#0.#")

  End Sub

  Private Sub trkAngle_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles trkAngle.ValueChanged

    If Processing Then Exit Sub
    rotateGraphics(pView.ClientSize.Width, pView.ClientSize.Height, img)

  End Sub

  Sub rotateGraphics(ByVal dstWidth As Integer, ByVal dstHeight As Integer, _
    ByVal img As Bitmap, Optional ByRef img2 As Bitmap = Nothing)
    ' rotates img and puts it into g
    ' if dstwidth is zero, then it assigns g to img2 and scales img2 to full size, for the final save.
    ' otherwise, it's used with picturebox or something interactive.

    Dim z As Double
    Dim r As RectangleF
    Dim rx(3), ry(3) As Double
    Dim sine, cosine As Double
    Dim cX, cY As Double
    Dim i As Integer
    Dim xi, yi As Double
    Dim p(4) As PointF
    Dim pType(4) As Byte
    Dim xoff, yoff As Double
    Dim qAngle As Double
    Dim Grid As Boolean
    Dim Xi1, Yi1, Xi2, Yi2, Xi3, Yi3, Xi4, Yi4 As Double
    Dim Xmin, Ymin, Xmax, Ymax As Double
    Dim g As Graphics
    Dim rAngle As Double

    rAngle = trkAngle.Value / 10

    If img Is Nothing Then
      cmdCancel_Click(Nothing, Nothing)
      Exit Sub
    End If

    ' set rAngle between 0 and 360
    rAngle = rAngle Mod 360
    If rAngle < 0 Then rAngle = rAngle + 360

    ' adjust the angle so we only have to consider one intersection case.
    If 0 <= rAngle And rAngle < 90 Then
      qAngle = -rAngle
    ElseIf 90 <= rAngle And rAngle < 180 Then
      qAngle = rAngle + 180
    ElseIf 180 <= rAngle And rAngle < 270 Then
      qAngle = -(rAngle + 180)
    ElseIf 270 <= rAngle And rAngle <= 360 Then
      qAngle = rAngle
    End If

    ' rotate the image corners
    rx(0) = 0 : ry(0) = 0
    rx(1) = img.Width : ry(1) = 0
    rx(2) = rx(1) : ry(2) = img.Height
    rx(3) = 0 : ry(3) = ry(2)
    cX = img.Width / 2 : cY = img.Height / 2
    sine = Sin(qAngle * pi / 180)
    cosine = Cos(qAngle * pi / 180)
    For i = 0 To 3
      rotateXY(rx(i), ry(i), cX, cY, sine, cosine)
    Next i
    Xmin = rx(0) : Ymin = ry(0)
    Xmax = rx(0) : Ymax = ry(0)
    For i = 1 To 3
      Xmin = Min(Xmin, rx(i)) : Ymin = Min(Ymin, ry(i))
      Xmax = Max(Xmax, rx(i)) : Ymax = Max(Ymax, ry(i))
    Next i

    ' intersect works on the long edge, since only two of 4 corners touch.
    If img.Width > img.Height Then
      Xi1 = rx(0) : Yi1 = ry(0)
      Xi2 = rx(1) : Yi2 = ry(1)
      Xi3 = 0 : Yi3 = 0
      Xi4 = img.Width : Yi4 = img.Height
    Else
      Xi1 = rx(0) : Yi1 = ry(0)
      Xi2 = rx(3) : Yi2 = ry(3)
      Xi3 = 0 : Yi3 = img.Height
      Xi4 = img.Width : Yi4 = 0
    End If

    If (Xmax - Xmin > Ymax - Ymin And img.Width < img.Height) Or (Xmax - Xmin < Ymax - Ymin And img.Width > img.Height) Then
      ' image has gone from wider to taller or taller to wider - rotate aspect ratio 90 degrees.
      Xi3 = (Xi4 - Yi4) / 2
      Yi3 = -Xi3
      z = Xi4
      Xi4 = Yi4
      Yi4 = z
    End If

    If chkTrim.Checked Then
      'i = lineIntersect(rx(0), ry(0), rx(1), ry(1), 0, 0, img.Width, img.Height, xi, yi) ' this is good for flat images rotating to same aspect.
      i = lineIntersect(Xi1, Yi1, Xi2, Yi2, Xi3, Yi3, Xi4, Yi4, xi, yi)
      If xi > cX Then xi = cX - (xi - cX)
      If yi > cY Then yi = cY - (yi - cY)

      If i <> 1 Then ' weird shape - don't trim
        xi = rx(0) : yi = ry(1)
      End If
    Else ' don't trim -- show the entire original image
      xi = rx(0) : yi = ry(1)
    End If
    ' good rectangle after transform reset
    r.X = xi
    r.Y = yi
    r.Width = (cX - xi) * 2
    r.Height = (cY - yi) * 2

    ' p is the clipping path, but it needs to be rotated back
    p(0).X = xi : p(0).Y = yi
    p(1).X = xi + Round((cX - xi) * 2) : p(1).Y = p(0).Y
    p(2).X = p(1).X : p(2).Y = yi + Round((cY - yi) * 2)
    p(3).X = p(0).X : p(3).Y = p(2).Y
    p(4).X = p(0).X : p(4).Y = p(0).Y

    ' these are for g.clip below
    pType(0) = PathPointType.Start
    pType(1) = PathPointType.Line
    pType(2) = PathPointType.Line
    pType(3) = PathPointType.Line
    pType(4) = PathPointType.Line

    ' rotate the clipping path backwards, because the transforms are still in effect
    sine = Sin(-rAngle * pi / 180)
    cosine = Cos(-rAngle * pi / 180)
    For i = 0 To 4
      rotateXY(p(i).X, p(i).Y, cX, cY, sine, cosine)
    Next i

    If dstWidth <= 0 Then  ' adjust destination bitmap on img2 and don't zoom. Assigns g to img2.
      ' this is probably slow -- it's only for the final rotation onto a bitmap
      xoff = 0
      yoff = 0
      z = 1
      img2 = New Bitmap(r.Width, r.Height, PixelFormat.Format24bppRgb)
      g = Graphics.FromImage(img2)
      g.InterpolationMode = InterpolationMode.High
      Grid = False

    Else ' scale to fit dstWidth and dstHeight
      g = Graphics.FromImage(pView.Bitmap)
      z = dstWidth / r.Width
      xoff = 0
      yoff = (dstHeight - r.Height * z) / 2
      If z > dstHeight / r.Height Then
        z = dstHeight / r.Height
        xoff = (dstWidth - r.Width * z) / 2
        yoff = 0
      End If
      g.InterpolationMode = InterpolationMode.Default
      Grid = chkGrid.Checked
    End If

    g.ResetTransform()
    g.Clear(Color.DarkGray)

    ' translate to the center of the drawing and rotate
    g.TranslateTransform(-img.Width / 2, -img.Height / 2, MatrixOrder.Append)
    g.RotateTransform(rAngle, MatrixOrder.Append)

    g.TranslateTransform(cX - xi, cY - yi, MatrixOrder.Append)  ' move to upper left
    g.ScaleTransform(z, z, MatrixOrder.Append)  ' scale to output size
    g.TranslateTransform(xoff, yoff, MatrixOrder.Append) ' move to center image

    g.Clip = New Region(New GraphicsPath(p, pType))
    Try
      g.DrawImage(img, 0, 0, img.Width, img.Height)
    Catch ex As Exception
      g.DrawRectangle(Pens.DarkViolet, 0, 0, img.Width, img.Height)
    End Try
    'g.DrawLines(gPen, p) ' clipping rectangle
    g.ResetTransform()
    'g.DrawRectangle(gPen, Rectangle.Round(r))  ' clipping rectangle, after reset transform
    If Grid Then drawgrid(g)

    pView.Zoom(0)
    If g IsNot Nothing Then g.Dispose() : g = Nothing

  End Sub

  Sub drawgrid(ByRef g As Graphics)

    Dim nGrid As Point = New Point(8, 6)
    Dim dx, dy As Integer
    Dim i As Integer

    Using gPen As New Pen(Color.Gray, 1)

      dx = pView.ClientSize.Width / nGrid.X
      dy = pView.ClientSize.Height / nGrid.Y
      For i = 1 To nGrid.X
        g.DrawLine(gPen, dx * i, pView.ClientRectangle.Y, dx * i, pView.ClientRectangle.X + pView.ClientSize.Height)
      Next i
      For i = 1 To nGrid.Y
        g.DrawLine(gPen, pView.ClientRectangle.X, dy * i, pView.ClientRectangle.X + pView.ClientSize.Width, dy * i)
      Next i
    End Using

  End Sub

  Private Sub chkTrim_checkedchanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTrim.CheckedChanged
    trkAngle_ValueChanged(trkAngle, e)
  End Sub

  Private Sub chkGrid_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkGrid.CheckedChanged
    trkAngle_ValueChanged(trkAngle, e)
  End Sub

  Private Sub frmRotate_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize

    If Me.WindowState = FormWindowState.Minimized Or pView.ClientSize.Width = 0 Then Exit Sub
    pView.newBitmap(pView.ClientSize.Width, pView.ClientSize.Height, mBackColor)
    trkAngle_ValueChanged(sender, e)

  End Sub

  Private Sub aview_Resize(sender As Object, e As EventArgs) Handles pView.Resize
    pView.Zoom(1)
  End Sub

  Sub pview_zoomed() Handles pView.Zoomed, Me.Resize
    Timer1.Stop() : Timer1.Interval = 50 : Timer1.Start() ' calls drawsharp
  End Sub

  Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
    ' redraw after some milliseconds
    Timer1.Stop()
    rotateGraphics(pView.ClientSize.Width, pView.ClientSize.Height, img)
  End Sub

  Private Sub _FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
    Timer1.Stop()
  End Sub

  Private Sub cmdAuto_Click(sender As Object, e As EventArgs) Handles cmdAuto.Click
    txAngle.Text = rollAngle
    txAngle_Leave(sender, e)
  End Sub

  Private Sub frmRotate_Shown(sender As Object, e As EventArgs) Handles Me.Shown
    trkAngle.Focus()
  End Sub
End Class