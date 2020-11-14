'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports System.IO
Imports System.Collections.Generic

Public Class frmPicturize3
  Inherits Form

  Dim picturizing As Integer
  Dim Escape As Integer

  Dim gImage1 As Bitmap
  Dim gImage2 As Bitmap
  Dim cellImages As New List(Of Bitmap)

  Private Sub cmdHelp_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdHelp.Click
    Try
      Help.ShowHelp(Me, helpFile, HelpNavigator.Topic, "photomosaic" & ".html") ' same help for three dialogs
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

  Private Sub cmdNav_Click(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles cmdPrevious.Click, cmdStart.Click, cmdCancel.Click, cmdFinish.Click

    If Sender Is cmdPrevious Then  ' previous
      pczRetc = 0
      If picturizing Then Exit Sub
      Me.DialogResult = DialogResult.OK
      Me.Close()

    ElseIf Sender Is cmdStart Then ' start
      pczRetc = 1
      If picturizing Then Exit Sub
      Picturize()

    ElseIf Sender Is cmdCancel Then ' cancel
      pczRetc = 2
      If picturizing Then
        Escape = True
      Else
        clearBitmap(qImage)
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
      End If

    ElseIf Sender Is cmdFinish Then ' done
      pczRetc = 3
      If picturizing Then Exit Sub

      Me.Cursor = Cursors.WaitCursor
      clearBitmap(qImage)
      If pView.Bitmap IsNot Nothing Then qImage = pView.Bitmap.Clone
      Me.Cursor = Cursors.Default
      Me.DialogResult = DialogResult.OK
      Me.Close()
    End If

  End Sub

  Private Sub Picturize()

    Dim pcolor(,,,) As Byte ' rgb quads for files.
    'Dim pcolor(1000, 5, 5, 2) As Byte ' rgb quads for files.
    Dim picUsed() As Integer

    Dim quad(5, 5, 2) As Integer
    Dim fPath As String
    Dim fName As String
    Dim fNames As List(Of String)
    Dim ext As New List(Of String)

    Dim cellXres As Integer
    Dim cellYres As Integer
    Dim i As Integer
    Dim j As Integer
    Dim i1 As Integer
    Dim j1 As Integer
    Dim k As Integer
    Dim k2 As Integer
    Dim MatchVal As Integer
    Dim MatchPos As Integer
    Dim CellDivX As Integer
    Dim CellDivY As Integer
    Dim npicX As Integer
    Dim npicY As Integer
    Dim mx As New ColorMatrix

    Dim nFiles As Integer
    Dim nMatches As Integer

    Dim userData(0) As Byte ' for New bitmap overload

    Dim msg As String = ""

    picturizing = True

    ext.Add(".jpg")
    ext.Add(".jpeg")

    fPath = pczCellFolder
    cellXres = pczCellRes(0)
    cellYres = pczCellRes(1)
    CellDivX = pczCellDiv(0)
    CellDivY = pczCellDiv(1)
    If CellDivY > 6 Then CellDivY = 6
    npicX = pczNPic(0)
    npicY = pczNPic(1)

    Escape = False

    fNames = dirGetfiles(fPath, ext)
    ProgressBar1.Maximum = fNames.Count

    Me.Cursor = Cursors.WaitCursor

    ' size original image so 1 pixel = 1 celldiv
    pView.InterpolationMode = InterpolationMode.High
    pView.ResizeBitmap(New Size(npicX * CellDivX, npicY * CellDivY), gImage1, gImage1)

    ' load files into memory (lead3) and get the color values
    fNames = dirGetfiles(fPath, ext)
    nFiles = -1
    ' lead3 has the final mosaic
    pView.newBitmap(cellXres * gImage1.Width / CellDivX, cellYres * gImage1.Height / CellDivY, Color.Black)
    pView.Zoom(0)

    ReDim pcolor(fNames.Count, 5, 5, 2)
    ReDim picUsed(fNames.Count)
    nMatches = 0

    pView.InterpolationMode = InterpolationMode.High
    For Each fName In fNames
      clearBitmap(gImage2)
      gImage2 = readBitmap(fName, msg)
      pView.ResizeBitmap(New Size(cellXres, cellYres), gImage2, gImage2)

      If gImage2 IsNot Nothing Then
        nFiles += 1
        ProgressBar1.Value = nFiles
        picUsed(nFiles) = 0

        cellImages.Add(gImage2.Clone)

        pView.InterpolationMode = InterpolationMode.Default
        pView.ResizeBitmap(New Size(CellDivX, CellDivY), gImage2, gImage2)

        For i = 0 To CellDivX - 1
          For j = 0 To CellDivY - 1
            pcolor(nFiles, i, j, 0) = gImage2.GetPixel(i, j).R  ' (row, column)
            pcolor(nFiles, i, j, 1) = gImage2.GetPixel(i, j).G
            pcolor(nFiles, i, j, 2) = gImage2.GetPixel(i, j).B
          Next j
        Next i
        pView.Refresh()
        Application.DoEvents()
      End If
    Next fName

    ProgressBar1.Maximum = 100
    ProgressBar1.Value = 0

    For i1 = 0 To (gImage1.Width - CellDivX) Step CellDivX
      pView.Select()
      ProgressBar1.Value = i1 / gImage1.Width * 100
      For j1 = 0 To (gImage1.Height - CellDivY) Step CellDivY
        If Escape Then
          cellImages = New List(Of Bitmap)
          clearBitmap(gImage1)
          clearBitmap(gImage2)
          Me.Cursor = Cursors.Default
          ProgressBar1.Value = 0
          frmPicturize3_Load(Me, New EventArgs())
          Exit Sub
        End If
        For i = 0 To CellDivX - 1
          For j = 0 To CellDivY - 1
            quad(i, j, 0) = gImage1.GetPixel(i1 + i, j1 + j).R
            quad(i, j, 1) = gImage1.GetPixel(i1 + i, j1 + j).G
            quad(i, j, 2) = gImage1.GetPixel(i1 + i, j1 + j).B
          Next j
        Next i

        ' see which quad fits best
        MatchVal = 1000000000
        MatchPos = 0
        For i2 As Integer = 1 To nFiles - 1
          If Not pczUsePicsOnce Or picUsed(i2) = 0 Then
            k = 0
            For i = 0 To CellDivX - 1
              For j = 0 To CellDivY - 1
                k += Abs(quad(i, j, 0) - pcolor(i2, i, j, 0)) ^ 2 + Abs(quad(i, j, 1) - pcolor(i2, i, j, 1)) ^ 2 +
                  Abs(quad(i, j, 2) - pcolor(i2, i, j, 2)) ^ 2
              Next j
            Next i
            If (k < MatchVal) Then
              MatchVal = k
              MatchPos = i2
            End If
          End If
        Next i2
        picUsed(MatchPos) += 1
        nMatches += 1
        If nMatches >= nFiles - 1 Then ' reset single-use flags -- too few files
          For i2 As Integer = 0 To nFiles - 1 : picUsed(i2) = 0 : Next i2
          nMatches = 0
        End If

        If pczColorAdjust Then ' adjust the cell color to match the image

          For i2 As Integer = 0 To 2
            k = 0
            k2 = 0
            For i = 0 To CellDivX - 1
              For j = 0 To CellDivY - 1
                k += pcolor(MatchPos, i, j, i2)
                k2 += quad(i, j, i2)
              Next j
            Next i

            mx(4, i2) = (k2 - k) / (CellDivX * CellDivY) / 255
          Next i2

          clearBitmap(gImage2)
          pView.ApplyColorMatrix(mx, cellImages(MatchPos), gImage2, Nothing)
          Using g As Graphics = Graphics.FromImage(pView.Bitmap)
            g.DrawImage(gImage2, New Rectangle((i1 * cellXres) \ CellDivX, (j1 * cellYres) \ CellDivY, cellXres, cellYres))
          End Using
          clearBitmap(gImage2)

        Else ' no color adjust
          Using g As Graphics = Graphics.FromImage(pView.Bitmap)
            g.DrawImage(cellImages(MatchPos), New Rectangle((i1 * cellXres) \ CellDivX, (j1 * cellYres) \ CellDivY, cellXres, cellYres))
          End Using
        End If

        pView.Refresh()
        Application.DoEvents()
      Next j1
    Next i1

    pView.Zoom(0)
    ProgressBar1.Value = 0
    cellImages = New List(Of Bitmap)
    clearBitmap(gImage1)
    clearBitmap(gImage2)
    cmdFinish.Enabled = True
    Me.AcceptButton = cmdFinish

    pcolor = Nothing
    picturizing = False

    Me.Cursor = Cursors.Default

  End Sub

  Private Sub frmPicturize3_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, "photomosaic.html")

    picturizing = False
    clearBitmap(qImage)

    cmdFinish.Enabled = False
    Me.AcceptButton = cmdFinish

    gImage1 = frmMain.mView.Bitmap.Clone
    pView.setBitmap(gImage1)
    pView.Zoom(0)

  End Sub

  Private Sub pview3_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
    If e.KeyCode = 27 Then Escape = True
  End Sub

  Private Sub form_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
    clearBitmap(gImage1)
    clearBitmap(gImage2)
  End Sub

End Class