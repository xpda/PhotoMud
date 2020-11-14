'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports System.IO
Imports System

Public Class frmConcatenate

  Dim Processing As Boolean = False
  Dim Loading As Boolean = True

  Private Sub frmConcatenate_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load

    op2topLeft.Checked = True
    op3topRight.Checked = True

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Loading = True
    clearBitmap(qImage)

    pView2.setBitmap(frmMain.mView.Bitmap)

    pView3.setBitmap(frmMain.combineRview.Bitmap)

    pView2.InterpolationMode = InterpolationMode.Default
    pView2.Zoom(0) ' , RasterPaintDisplayModeFlags.None)
    pView3.InterpolationMode = InterpolationMode.Default
    pView3.Zoom(0) ', RasterPaintDisplayModeFlags.None)
    ConcatPreview()

    Loading = False

  End Sub

  Sub ConcatPreview()

    Dim w, h As Integer
    Dim x2, y2, x3, y3 As Integer

    Me.Cursor = Cursors.WaitCursor

    If op3topLeft.Checked And op2bottomLeft.Checked Or
      op3topRight.Checked And op2bottomRight.Checked Or
      op3bottomRight.Checked And op2topRight.Checked Or
      op3bottomLeft.Checked And op2topLeft.Checked Then
      ' stacked vertically
      w = Max(pView2.Bitmap.Width, pView3.Bitmap.Width)
      h = pView2.Bitmap.Height + pView3.Bitmap.Height

      If op3topLeft.Checked Or op3bottomLeft.Checked Then ' left corners
        x2 = 0 : x3 = 0
      Else ' right corners
        If pView3.Bitmap.Width >= pView2.Bitmap.Width Then
          x2 = pView3.Bitmap.Width - pView2.Bitmap.Width
          x3 = 0
        Else
          x2 = 0
          x3 = pView2.Bitmap.Width - pView3.Bitmap.Width
        End If
      End If
      If op3topLeft.Checked Or op3topRight.Checked Then ' pview2 on top
        y2 = 0
        y3 = pView2.Bitmap.Height
      Else
        y2 = pView3.Bitmap.Height
        y3 = 0
      End If

    Else ' concat horizontally
      w = pView2.Bitmap.Width + pView3.Bitmap.Width
      h = Max(pView2.Bitmap.Height, pView3.Bitmap.Height)

      If op3topLeft.Checked Or op3topRight.Checked Then ' top corners
        y2 = 0 : y3 = 0
      Else ' bottom corners
        If pView3.Bitmap.Height >= pView2.Bitmap.Height Then
          y2 = pView3.Bitmap.Height - pView2.Bitmap.Height
          y3 = 0
        Else
          y2 = 0
          y3 = pView2.Bitmap.Height - pView3.Bitmap.Height
        End If
      End If

      If op3topLeft.Checked Or op3bottomLeft.Checked Then ' pview2 on left
        x2 = 0
        x3 = pView2.Bitmap.Width
      Else
        x2 = pView3.Bitmap.Width
        x3 = 0
      End If

    End If

    pView1.newBitmap(w, h, mBackColor)

    Try
      Using g As Graphics = Graphics.FromImage(pView1.Bitmap)
        g.DrawImage(pView3.Bitmap, New Rectangle(x3, y3, pView3.Bitmap.Width, pView3.Bitmap.Height))
        g.DrawImage(pView2.Bitmap, New Rectangle(x2, y2, pView2.Bitmap.Width, pView2.Bitmap.Height))
      End Using
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    pView1.Zoom(0)

    Me.Cursor = Cursors.Default

  End Sub


  Private Sub frmConcatenate_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

    Dim iPad, iPad2 As Integer

    iPad = 0.0 * op2topLeft.Height
    iPad2 = op2topLeft.Height
    op2topLeft.Left = pView2.Left - iPad2 ' top left
    op2topLeft.Top = pView2.Top - iPad2 ' top left
    op3topLeft.Left = pView3.Left - iPad2 ' top left
    op3topLeft.Top = pView3.Top - iPad2 ' top left
    op2topRight.Left = pView2.Right + iPad ' top right
    op2topRight.Top = pView2.Top - iPad2 ' top right
    op3topRight.Left = pView3.Right + iPad ' top right
    op3topRight.Top = pView3.Top - iPad2 ' top right
    op2bottomRight.Left = pView2.Right + iPad ' bottom  right
    op2bottomRight.Top = pView2.Bottom + iPad ' bottom  right
    op3bottomRight.Left = pView3.Right + iPad ' bottom  right
    op3bottomRight.Top = pView3.Bottom + iPad ' bottom  right
    op2bottomLeft.Left = pView2.Left - iPad2 ' bottom  left
    op2bottomLeft.Top = pView2.Bottom + iPad ' bottom  left
    op3bottomLeft.Left = pView3.Left - iPad2 ' bottom  left
    op3bottomLeft.Top = pView3.Bottom + iPad ' bottom  left

  End Sub

  Private Sub op3topLeft_ch(ByVal sender As Object, ByVal e As EventArgs) Handles op3topLeft.CheckedChanged
    If Loading Or Processing Or Not sender.checked Then Exit Sub
    If Not op2bottomLeft.Checked And Not op2topRight.Checked Then
      Processing = True
      op2topRight.Checked = True
      Processing = False
    End If
    ConcatPreview()
  End Sub

  Private Sub op3topRight_ch(ByVal sender As Object, ByVal e As EventArgs) Handles op3topRight.CheckedChanged
    If Loading Or Processing Or Not sender.checked Then Exit Sub
    If Not op2bottomRight.Checked And Not op2topLeft.Checked Then
      Processing = True
      op2topLeft.Checked = True
      Processing = False
    End If
    ConcatPreview()
  End Sub

  Private Sub op3bottomRight_ch(ByVal sender As Object, ByVal e As EventArgs) Handles op3bottomRight.CheckedChanged
    If Loading Or Processing Or Not sender.checked Then Exit Sub
    If Not op2bottomLeft.Checked And Not op2topRight.Checked Then
      Processing = True
      op2topRight.Checked = True
      Processing = False
    End If
    ConcatPreview()
  End Sub

  Private Sub op3bottomLeft_ch(ByVal sender As Object, ByVal e As EventArgs) Handles op3bottomLeft.CheckedChanged
    If Loading Or Processing Or Not sender.checked Then Exit Sub
    If Not op2bottomRight.Checked And Not op2topLeft.Checked Then
      Processing = True
      op2bottomRight.Checked = True
      Processing = False
    End If
    ConcatPreview()
  End Sub

  Private Sub op2topLeft_ch(ByVal sender As Object, ByVal e As EventArgs) Handles op2topLeft.CheckedChanged
    If Loading Or Processing Or Not sender.checked Then Exit Sub
    If Not op3bottomLeft.Checked And Not op3topRight.Checked Then
      Processing = True
      op3topRight.Checked = True
      Processing = False
    End If
    ConcatPreview()
  End Sub

  Private Sub op2topRight_ch(ByVal sender As Object, ByVal e As EventArgs) Handles op2topRight.CheckedChanged
    If Loading Or Processing Or Not sender.checked Then Exit Sub
    If Not op3bottomRight.Checked And Not op3topLeft.Checked Then
      Processing = True
      op3topLeft.Checked = True
      Processing = False
    End If
    ConcatPreview()
  End Sub

  Private Sub op2bottomRight_ch(ByVal sender As Object, ByVal e As EventArgs) Handles op2bottomRight.CheckedChanged
    If Loading Or Processing Or Not sender.checked Then Exit Sub
    If Not op3bottomLeft.Checked And Not op3topRight.Checked Then
      Processing = True
      op3topRight.Checked = True
      Processing = False
    End If
    ConcatPreview()
  End Sub

  Private Sub op2bottomLeft_ch(ByVal sender As Object, ByVal e As EventArgs) Handles op2bottomLeft.CheckedChanged
    If Loading Or Processing Or Not sender.checked Then Exit Sub
    If Not op3bottomRight.Checked And Not op3topLeft.Checked Then
      Processing = True
      op3bottomRight.Checked = True
      Processing = False
    End If
    ConcatPreview()
  End Sub

  Private Sub cmdOK_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdOK.Click

    Me.Cursor = Cursors.WaitCursor

    clearBitmap(qImage)
    If pView1.Bitmap IsNot Nothing Then qImage = pView1.Bitmap.Clone

    Me.Cursor = Cursors.Default
    Me.DialogResult = DialogResult.OK
    Me.Close()

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

End Class