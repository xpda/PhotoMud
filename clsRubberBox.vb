Public Class clsrubberBox

Dim rX, rY, rWidth, rHeight As Double
Dim rVisible As Boolean
Dim uDraw As New uDrawClass

Dim g As Graphics
Protected hdc As Integer
Protected boxSet As Boolean

ReadOnly Property Rectangle() As Rectangle
  Get
    Rectangle = New Rectangle(rX, rY, rWidth, rHeight)
  End Get
End Property

Public Sub setBox(ByVal box As RectangleF)
If boxSet Then Call refresh() ' erase old one
rX = box.X
rY = box.Y
rWidth = box.Width
rHeight = box.Height
boxSet = True
Call refresh()
End Sub

Property Visible() As Boolean

Get
  Visible = rVisible
End Get

Set(ByVal makeVisible As Boolean)

  If Visible And Not makeVisible Then ' free all the extra stuff
    If boxSet Then Call refresh() ' erase last one
    boxSet = False
    If g IsNot Nothing Then
      Try
        g.ReleaseHdc()
        g.Dispose()
        Catch
        End Try
      End If
    g = Nothing

  ElseIf Not Visible And makeVisible Then
    uDraw.penColor = RGB(0, 0, 80)
    uDraw.PenMode = R2_NOTXORPEN
    uDraw.PenWidth = 2
    Try
      g = frmMain.mView.CreateGraphics
      hdc = g.GetHdc
      Catch
      End Try
    If boxSet Then Call refresh()
    End If
  rVisible = makeVisible
End Set

End Property

Public Sub New()
rVisible = False
boxSet = False
End Sub

Public Sub refresh()
' draw the box
If Not boxSet Then Exit Sub
uDraw.DrawBox(hdc, rX, rY, rX + rWidth, rY + rHeight)
End Sub

Protected Sub drawClothesLine(ByRef rx() As Double, ByRef ry() As Double, ByVal np As Integer, ByVal closed As Boolean)
' draw a line, 1-based. udraw cannot be exposed.
uDraw.DrawPolyline(hdc, rx, ry, np, closed)
End Sub

Protected Sub drawBox(ByVal x As Double, ByVal y As Double, ByVal Width As Double, ByVal Height As Double)
' draw a box. udraw cannot be exposed.
uDraw.DrawBox(hdc, x, y, Width, Height)
End Sub

Protected Overrides Sub Finalize()
MyBase.Finalize()

If g IsNot Nothing Then
  Try
    g.ReleaseHdc()
    g.Dispose()
    Catch
    End Try
  g = Nothing
  End If

End Sub

End Class
