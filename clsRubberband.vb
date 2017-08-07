Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Collections.Generic

Public Class clsRubberband
' draws shapes, called by paint for rubberband modes
' uses control coordinates, not bitmap

Property Box As Rectangle
Property BoxColor As Color = Color.Blue
Property BoxLineWidth As Single = 1
Property BoxEnabled As Boolean
Property BoxCrop As Boolean = False ' whether to dim area outside the box

Property Line() As New List(Of Point)
Property LineColor As System.Drawing.Color
Property LineLineWidth As Single
Property LineEnabled As Boolean

Public Sub clear() ' everything visible is false
BoxEnabled = False
BoxCrop = False
LineEnabled = False
End Sub

Public Sub Draw(g As Graphics)

If g Is Nothing Then Exit Sub

If BoxEnabled And (Box.Width >= 2 And Box.Height >= 2) Then
  Try

    If BoxCrop Then ' dim area outside of box
      Using rg As Region = New Region(Box)
        rg.Complement(g.ClipBounds)
        g.FillRegion(New HatchBrush(HatchStyle.SmallCheckerBoard, _
          Color.FromArgb(255, 0, 0, 0), Color.FromArgb(0, 0, 0, 0)), rg)
      End Using
      End If

    g.DrawRectangle(New Pen(BoxColor, BoxLineWidth), Box)
  Catch ex As Exception
    MsgBox(ex.Message)
    End Try
  End If

If LineEnabled AndAlso Line.Count > 1 Then
  Try
    g.DrawLines(New Pen(LineColor, LineLineWidth), Line.ToArray)
  Catch ex As Exception
    MsgBox(ex.Message)
    End Try
  End If

End Sub

End Class
