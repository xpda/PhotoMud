'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports System.IO
Imports System

Public Class frmCombineSelection
  Inherits Form

  Dim gImage As Bitmap

  Private Sub cmdCancel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
    frmMain.combineRview = Nothing
    Me.DialogResult = DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub cmdOK_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdOK.Click

    Dim i As Integer

    If ListView1.SelectedItems.Count > 0 Then
      i = CInt(ListView1.SelectedItems(0).ImageKey)
      frmMain.combineRview = mViews(i)
    End If
    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Private Sub frmCombineSelection_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    ListView1.Items.Clear()
    ImageList1.Images.Clear()
    clearBitmap(gImage)
  End Sub

  Private Sub frmCombineSelection_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Dim i As Double
    Dim mv As mudViewer
    Dim thumbXres, thumbYres As Integer
    'Dim xSize, ySize As Double
    Dim shadowSize As Integer = 6
    Dim img As Bitmap

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, "frmCombine.html")

    ListView1.Items.Clear()
    ImageList1.Images.Clear()

    i = 0
    thumbXres = ImageList1.ImageSize.Width - shadowSize
    thumbYres = ImageList1.ImageSize.Height - shadowSize

    For i = 1 To mViews.Count
      mv = mViews(i)
      If mv IsNot frmMain.mView Then
        mv.ResizeBitmap(thumbXres, thumbYres, mv.Bitmap, gImage) ' size limits, no aspect change 
        ' is this right? Replaced xSize, ySize with thumbXres and thumbYres 2/15/19

        ' add the shadow to gdi image and pad it to fit imagelist.
        img = getShadow(gImage, shadowSize, ListView1.BackColor, thumbXres, thumbYres)
        ImageList1.Images.Add(CStr(i), img)
        ListView1.Items.Add(CStr(i) & i, Path.GetFileName(mv.picName), CStr(i))
      End If
    Next i

    ListView1.Refresh()
    ListView1.MultiSelect = False
    clearBitmap(gImage)

  End Sub

  Private Sub ListView1_DoubleClick(ByVal Sender As Object, ByVal e As EventArgs) Handles ListView1.DoubleClick
    cmdOK_Click(cmdOK, New EventArgs())
  End Sub


End Class