'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports System.IO
Imports System

Public Class frmCombine

  Inherits Form

  Dim strMethod(11) As String
  Dim Operation(11) As mergeOp
  Dim Loading As Boolean = True
  Dim Sliding As Boolean = False
  Dim xoff As Double
  Dim yoff As Double
  Dim dragx As Double
  Dim dragy As Double
  Dim gPath2 As GraphicsPath
  Dim gPath3 As GraphicsPath

  Private Sub chkAspect_CheckedChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles chkAspect.CheckedChanged

    If Loading Then Exit Sub

    ScaleBitmap()
    CombinePreview()

  End Sub

  Private Sub chkFit_CheckedChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles chkFit.CheckedChanged

    If Loading Then Exit Sub

    If chkFit.Checked Then
      chkAspect.Enabled = True
      ScaleBitmap()
    Else
      chkAspect.Enabled = False
      updateImage(pView3, chkGray3)
    End If

    xoff = (pView2.Bitmap.Width - pView3.Bitmap.Width) / 2
    yoff = (pView2.Bitmap.Height - pView3.Bitmap.Height) / 2
    CombinePreview()

  End Sub

  Sub ScaleBitmap()

    Dim bmpSize As Size
    Dim widthallowed As Integer
    Dim heightallowed As Integer

    Me.Cursor = Cursors.WaitCursor

    widthallowed = pView2.Bitmap.Width
    heightallowed = pView2.Bitmap.Height

    If chkAspect.Checked Then
      updateImage(pView3, chkGray3)
      If ((widthallowed * pView3.Bitmap.Height) / pView3.Bitmap.Width) < heightallowed Then
        bmpSize.width = widthallowed
        bmpSize.Height = CInt((bmpSize.Width * pView3.Bitmap.Height) / pView3.Bitmap.Width)
      Else
        bmpSize.height = heightallowed
        bmpSize.Width = CInt((bmpSize.Height * pView3.Bitmap.Width) / pView3.Bitmap.Height)
      End If
    Else
      bmpSize.width = widthallowed
      bmpSize.height = heightallowed
    End If

    pView3.ResizeBitmap(bmpSize)

    xoff = (pView2.Bitmap.Width - pView3.Bitmap.Width) / 2
    yoff = (pView2.Bitmap.Height - pView3.Bitmap.Height) / 2

    Me.Cursor = Cursors.Default

  End Sub

  Private Sub chkGray2_CheckedChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles chkGray2.CheckedChanged

    If Loading Then Exit Sub
    updateImage(pView2, chkGray2)
    CombinePreview()

  End Sub

  Private Sub chkGray3_CheckedChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles chkGray3.CheckedChanged

    If Loading Then Exit Sub

    If chkFit.Checked Then
      ScaleBitmap()
    Else
      updateImage(pView3, chkGray3)
    End If

    CombinePreview()

  End Sub

  Private Sub cmdCancel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
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

    clearBitmap(qImage)
    If pView1.Bitmap IsNot Nothing Then
      qImage = pView1.Bitmap.Clone
    Else
      pView1.setBitmap(Nothing)
    End If

    Me.Cursor = Cursors.Default
    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Private Sub Combo1_SelectedIndexChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles Combo1.SelectedIndexChanged

    If Loading Then Exit Sub
    CombinePreview()

  End Sub

  Private Sub frmCombine_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Dim i As Integer
    Dim mx As Matrix

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Loading = True
    clearBitmap(qImage)

    pView1.InterpolationMode = InterpolationMode.High

    ''pview1.ShowSelection = True
    ''pview2.ShowSelection = True
    ''pview3.ShowSelection = True

    trkIntensity2.Value = 0
    trkIntensity3.Value = 0
    nmIntensity2.Value = 0
    nmIntensity3.Value = 0

    pView2.setBitmap(frmMain.mView.Bitmap)

    If frmMain.mView.FloaterBitmap IsNot Nothing Then ' copy source floater to pview2 region

      pView2.addPath(frmMain.mView.FloaterPath)
      gPath2 = frmMain.mView.FloaterPath.Clone
      mx = New Matrix
      mx.Translate(frmMain.mView.FloaterPosition.Y, frmMain.mView.FloaterPosition.X)
      gPath2.Transform(mx)
    End If

    pView3.setBitmap(frmMain.combineRview.Bitmap)
    If frmMain.combineRview.FloaterBitmap IsNot Nothing Then ' copy source floater to pview3 region
      pView3.addPath(frmMain.mView.FloaterPath)
      gPath3 = frmMain.mView.FloaterPath.Clone
      mx = New Matrix
      mx.Translate(frmMain.combineRview.FloaterPosition.Y, frmMain.combineRview.FloaterPosition.X)
      gPath3.Transform(mx)
    End If

    If pView2.BitmapPath IsNot Nothing Or pView3.BitmapPath IsNot Nothing Then
      chkFit.Checked = False
      chkAspect.Checked = False
      chkFit.Enabled = False
    Else
      chkFit.Checked = False ' changed 2/2/2008
      chkAspect.Checked = True
      chkFit.Enabled = True
    End If

    If chkFit.Checked Then
      chkAspect.Enabled = True
      ScaleBitmap()
    Else
      chkAspect.Enabled = False
      updateImage(pView3, chkGray3)
    End If

    strMethod(0) = "Multiply"
    strMethod(1) = "Average"
    strMethod(2) = "Add"
    strMethod(3) = "Minimum"
    strMethod(4) = "Maximum"
    strMethod(5) = "Or"
    strMethod(6) = "Exclusive Or"
    strMethod(7) = "Subtract from Image 1"
    strMethod(8) = "Subtract from Image 2"
    strMethod(9) = "Divide Image 1"
    strMethod(10) = "Divide Image 2"
    strMethod(11) = "Replace"

    Operation(0) = mergeOp.opMultiply
    Operation(1) = mergeOp.opAverage
    Operation(2) = mergeOp.opAdd
    Operation(3) = mergeOp.opMinimum
    Operation(4) = mergeOp.opMaximum
    Operation(5) = mergeOp.opOr
    Operation(6) = mergeOp.opXor
    Operation(7) = mergeOp.opSubtractFromSource
    Operation(8) = mergeOp.opSubtractFromTarget
    Operation(9) = mergeOp.opDivideSource
    Operation(10) = mergeOp.opDivideTarget
    Operation(11) = mergeOp.opReplace

    Combo1.Items.Clear()
    For i = 0 To 10
      Combo1.Items.Add(strMethod(i))
    Next i
    Combo1.SelectedIndex = 0 ' multiply
    If pView2.BitmapPath IsNot Nothing Or pView3.BitmapPath IsNot Nothing Then Combo1.SelectedIndex = 11 ' replace

    xoff = (pView2.Bitmap.Width - pView3.Bitmap.Width) / 2
    yoff = (pView2.Bitmap.Height - pView3.Bitmap.Height) / 2

    pView2.Zoom(0)
    pView3.Zoom(0)
    CombinePreview()

    Loading = False

  End Sub

  Sub CombinePreview()

    Dim destRect As Rectangle

    Me.Cursor = Cursors.WaitCursor
    pView1.setBitmap(pView2.Bitmap)

    destRect = New Rectangle(CInt(xoff), CInt(yoff), pView1.Bitmap.Width, pView1.Bitmap.Height)
    bmpMerge(pView3.Bitmap, pView1.Bitmap, Operation(Combo1.SelectedIndex), destRect)

    pView1.Zoom(0)
    Me.Cursor = Cursors.Default

  End Sub

  Private Sub pview1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pView1.MouseDown
    pView1.Cursor = Cursors.Hand
    dragx = e.X
    dragy = e.Y
  End Sub

  Private Sub pview1_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pView1.MouseUp
    pView1.Cursor = Cursors.WaitCursor
    xoff += (e.X - dragx) / pView1.ZoomFactor
    yoff += (e.Y - dragy) / pView1.ZoomFactor
    CombinePreview()
    pView1.Cursor = Cursors.Default

  End Sub

  Private Sub trkIntensity_Scroll(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles trkIntensity2.Scroll, trkIntensity3.Scroll

    If Loading Then Exit Sub

    Sliding = True
    If Sender Is trkIntensity2 Then
      updateImage(pView2, chkGray2)
      nmIntensity2.Value = CInt(trkIntensity2.Value / 10)
      pView2.Zoom(0)
    Else
      If chkFit.Checked Then
        ScaleBitmap()
      Else
        updateImage(pView3, chkGray3)
        nmIntensity3.Value = CInt(trkIntensity3.Value / 10)
        pView3.Zoom(0)
      End If
    End If

    CombinePreview()
    Sliding = False

  End Sub
  Private Sub pview1_MouseWheel(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pView1.MouseWheel
    mouseWheelZoom(pView1, e, Nothing, 1.2)
  End Sub

  Sub updateImage(ByRef rview As pViewer, ByVal chkGray As CheckBox)
    ' restore the image, then add the region, grayscale, and brightness

    If rview Is pView3 Then
      rview.setBitmap(frmMain.combineRview.Bitmap)
    Else
      rview.setBitmap(frmMain.mView.Bitmap)
    End If

    If chkGray.Checked Then rview.MakeGrayscale()

    ''rview.bitmap.AddDataToRegion(Nothing, rgn, 0, RasterRegionCombineMode.Set) ' assign the region data from _load
    'changeIntensityCmd.Brightness = trkIntensity.Value
    Try
      ''changeIntensityCmd.Run(rview.bitmap)
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

  Private Sub nmIntensity2_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles nmIntensity2.ValueChanged, nmIntensity3.ValueChanged

    If Sliding Then Exit Sub

    If sender Is nmIntensity2 Then
      trkIntensity2.Value = nmIntensity2.Value * 10
      trkIntensity_Scroll(trkIntensity2, e)
    Else
      trkIntensity3.Value = nmIntensity3.Value * 10
      trkIntensity_Scroll(trkIntensity3, e)
    End If
  End Sub

  Private Sub frmCombine_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
    If gPath2 IsNot Nothing Then gPath2.Dispose()
    If gPath3 IsNot Nothing Then gPath3.Dispose()
  End Sub

End Class