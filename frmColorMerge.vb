'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports System.IO
Imports System.Collections.Generic
Imports ImageMagick

Public Class frmColorMerge
  Inherits Form

  Dim red(7) As Integer
  Dim green(7) As Integer
  Dim blue(7) As Integer
  Dim Processing As Boolean = True
  Dim iRow, icol As Integer
  Dim highLight As Color
  Dim normalColor As Color
  Dim Changed As Boolean
  Dim nFiles As Integer
  Dim ix As New List(Of Integer)
  Dim cZoom As Double
  Dim fExposure(7) As Double
  Dim useExposure As Boolean
  Dim isMars As Boolean

  Dim downx As Double
  Dim downy As Double

  Dim gBitmap As Bitmap
  Dim pComments As List(Of PropertyItem)

  Dim txPath(9) As TextBox
  Dim txRed(9) As TextBox
  Dim txGreen(9) As TextBox
  Dim txBlue(9) As TextBox
  Dim txWeight(9) As TextBox

  Dim optColor(7) As RadioButton

  Private Sub chkWeight_CheckedChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles chkWeight.CheckedChanged
    If Processing Then Exit Sub
    Changed = True
    mergeColors()
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
    qImage = pView.Bitmap.Clone
    Me.DialogResult = DialogResult.OK
    Me.Close()
  End Sub

  Private Sub cmdZoomin_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdZoomin.Click
    pView.Zoom(2)
    cZoom = cZoom * 2
  End Sub

  Private Sub cmdZoomout_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdZoomout.Click
    pView.Zoom(0.5)
    cZoom = cZoom * 0.5
  End Sub

  Private Sub pview_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
    downx = e.X
    downy = e.Y
  End Sub

  Private Sub pview_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)

    Dim dx As Integer
    Dim dy As Integer
    Dim rv As pViewer
    rv = sender
    rv.Cursor = Cursors.Hand

    If e.Button = MouseButtons.Left Then

      dx = (e.X - downx)
      dy = (e.Y - downy)
      pView.Pan(dx, dy)
      downx = e.X
      downy = e.Y
    End If

  End Sub

  Private Sub frmColorMerge_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Dim i As Integer
    Dim s As String = ""
    Dim mResult As MsgBoxResult

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Processing = True
    clearBitmap(qImage)

    txPath(0) = txPath0
    txPath(1) = txPath1
    txPath(2) = txPath2
    txPath(3) = txPath3
    txPath(4) = txPath4
    txPath(5) = txPath5
    txPath(6) = txPath6
    txPath(7) = txPath7
    txPath(8) = txPath8
    txPath(9) = txPath9

    txWeight(0) = txWeight0
    txWeight(1) = txWeight1
    txWeight(2) = txWeight2
    txWeight(3) = txWeight3
    txWeight(4) = txWeight4
    txWeight(5) = txWeight5
    txWeight(6) = txWeight6
    txWeight(7) = txWeight7
    txWeight(8) = txWeight8
    txWeight(9) = txWeight9

    txRed(0) = txR0
    txRed(1) = txR1
    txRed(2) = txR2
    txRed(3) = txR3
    txRed(4) = txR4
    txRed(5) = txR5
    txRed(6) = txR6
    txRed(7) = txR7
    txRed(8) = txR8
    txRed(9) = txR9

    txGreen(0) = txG0
    txGreen(1) = txG1
    txGreen(2) = txG2
    txGreen(3) = txG3
    txGreen(4) = txG4
    txGreen(5) = txG5
    txGreen(6) = txG6
    txGreen(7) = txG7
    txGreen(8) = txG8
    txGreen(9) = txG9

    txBlue(0) = txB0
    txBlue(1) = txB1
    txBlue(2) = txB2
    txBlue(3) = txB3
    txBlue(4) = txB4
    txBlue(5) = txB5
    txBlue(6) = txB6
    txBlue(7) = txB7
    txBlue(8) = txB8
    txBlue(9) = txB9

    For i = 0 To 9
      AddHandler txRed(i).TextChanged, AddressOf txred_TextChanged
      AddHandler txRed(i).Enter, AddressOf txRed_Enter
      AddHandler txRed(i).Leave, AddressOf txRed_Leave
      AddHandler txRed(i).KeyDown, AddressOf txBox_KeyDown

      AddHandler txGreen(i).TextChanged, AddressOf txGreen_TextChanged
      AddHandler txGreen(i).Enter, AddressOf txGreen_Enter
      AddHandler txGreen(i).Leave, AddressOf txGreen_Leave
      AddHandler txGreen(i).KeyDown, AddressOf txBox_KeyDown

      AddHandler txBlue(i).TextChanged, AddressOf txBlue_TextChanged
      AddHandler txBlue(i).Enter, AddressOf txBlue_Enter
      AddHandler txBlue(i).Leave, AddressOf txBlue_Leave
      AddHandler txBlue(i).KeyDown, AddressOf txBox_KeyDown

      AddHandler txWeight(i).TextChanged, AddressOf txWeight_TextChanged
      AddHandler txWeight(i).Enter, AddressOf txWeight_Enter
      AddHandler txWeight(i).Leave, AddressOf txWeight_Leave
      AddHandler txWeight(i).KeyDown, AddressOf txBox_KeyDown
    Next i

    isMars = False
    useExposure = True

    highLight = Color.FromArgb(78, 78, 100)
    normalColor = SystemColors.Window

    For i = 0 To tagPath.Count - 1 : ix.Add(i) : Next i
    MergeSort(tagPath, ix, 0, tagPath.Count - 1) ' sort image names

    If tagPath.Count <= 0 Then Exit Sub
    If tagPath.Count > txPath.Count Then
      mResult = MsgBox("Warning:  Only " & txPath.Count & " files can be used with this function. The first " & txPath.Count & " files will be used.", _
        MsgBoxStyle.OkCancel)
      If mResult = MsgBoxResult.Cancel Then
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
        Exit Sub
      End If
      nFiles = txPath.Count
    Else
      nFiles = tagPath.Count
    End If

    For i = 1 To nFiles
      'SeparatePath(tagPath(ix(i)), s1, s)
      s = Path.GetFileName(tagPath(ix(i)))
      txPath(i - 1).Text = s
      txWeight(i - 1).Text = "1.00"
      txRed(i - 1).Text = "0"
      txGreen(i - 1).Text = "0"
      txBlue(i - 1).Text = "0"
      txPath(i - 1).Visible = True
      txRed(i - 1).Visible = True
      txGreen(i - 1).Visible = True
      txBlue(i - 1).Visible = True
      txWeight(i - 1).Visible = True
    Next i

    For i = nFiles + 1 To txPath.Count
      txPath(i - 1).Visible = False
      txRed(i - 1).Visible = False
      txGreen(i - 1).Visible = False
      txBlue(i - 1).Visible = False
      txWeight(i - 1).Visible = False
    Next i

    red(0) = 100 : green(0) = 0 : blue(0) = 0
    red(1) = 0 : green(1) = 100 : blue(1) = 0
    red(2) = 0 : green(2) = 0 : blue(2) = 100
    red(3) = 0 : green(3) = 100 : blue(3) = 100
    red(4) = 100 : green(4) = 0 : blue(4) = 100
    red(5) = 100 : green(5) = 100 : blue(5) = 0
    red(6) = 31 : green(6) = 18 : blue(6) = 31
    red(7) = 100 : green(7) = 100 : blue(7) = 100

    optColor(0) = Opt0
    optColor(1) = Opt1
    optColor(2) = Opt2
    optColor(3) = Opt3
    optColor(4) = Opt4
    optColor(5) = Opt5
    optColor(6) = Opt6
    optColor(7) = Opt7
    For i = 0 To 7
      optColor(i).Checked = False
    Next i

    pView.Top = ((txPath(nFiles - 1).Top) + (txPath(nFiles - 1).Height) + 120)
    pView.Height = ((Me.ClientSize.Height) - (pView.Top) - 120)
    cZoom = 1

    chkWeight.Checked = True

    Processing = False

    'getExposureFactors()
    mergeColors()

  End Sub

  Private Sub Opt_CheckedChanged(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles Opt0.CheckedChanged, Opt1.CheckedChanged, Opt2.CheckedChanged, Opt3.CheckedChanged, _
    Opt4.CheckedChanged, Opt5.CheckedChanged, Opt6.CheckedChanged, Opt7.CheckedChanged

    Dim index As Short

    If Processing Then Exit Sub

    If Sender.Checked Then
      If Sender Is Opt0 Then index = 0
      If Sender Is Opt1 Then index = 1
      If Sender Is Opt2 Then index = 2
      If Sender Is Opt3 Then index = 3
      If Sender Is Opt4 Then index = 4
      If Sender Is Opt5 Then index = 5
      If Sender Is Opt6 Then index = 6
      If Sender Is Opt7 Then index = 7

      If CDbl(txRed(iRow).Text) <> red(index) Then
        Changed = True
        txRed(iRow).Text = red(index)
      End If
      If CDbl(txGreen(iRow).Text) <> green(index) Then
        Changed = True
        txGreen(iRow).Text = green(index)
      End If
      If CDbl(txBlue(iRow).Text) <> blue(index) Then
        Changed = True
        txBlue(iRow).Text = blue(index)
      End If
      mergeColors()

    End If

  End Sub

  Sub mergeColors()

    Dim i As Integer
    Dim wScale As Double
    Dim greenTotal, redTotal, blueTotal As Integer
    Dim g, r, b As Double

    Dim img2 As MagickImage = Nothing

    Dim msg As String = ""

    If Processing Or Not Changed Then Exit Sub
    Me.Cursor = Cursors.WaitCursor

    redTotal = 0 : greenTotal = 0 : blueTotal = 0

    ' set the weights for non-independent weights
    For i = 0 To nFiles - 1
      redTotal = redTotal + CDbl(txRed(i).Text) * CDbl(txWeight(i).Text)
      greenTotal = greenTotal + CDbl(txGreen(i).Text) * CDbl(txWeight(i).Text)
      blueTotal = blueTotal + CDbl(txBlue(i).Text) * CDbl(txWeight(i).Text)
    Next i

    If chkWeight.Checked Then
      wScale = redTotal
      If wScale < greenTotal Then wScale = greenTotal
      If wScale < blueTotal Then wScale = blueTotal
      If wScale = 0 Then wScale = 1 Else wScale = 100 / wScale
    Else
      wScale = 1
    End If

    showPicture(tagPath(ix(1)), pView, False, pComments)
    If (pView.Bitmap IsNot Nothing) Then
      Using gf As Graphics = Graphics.FromImage(pView.Bitmap)
        gf.Clear(Color.Black)
      End Using
    End If

    Using imgz As MagickImageCollection = New MagickImageCollection

      For i = 1 To nFiles
        r = CDbl(txRed(i - 1).Text) * CDbl(txWeight(i - 1).Text)
        g = CDbl(txGreen(i - 1).Text) * CDbl(txWeight(i - 1).Text)
        b = CDbl(txBlue(i - 1).Text) * CDbl(txWeight(i - 1).Text)
        If r > 0 Or g > 0 Or b > 0 Then
          gBitmap = readBitmap(tagPath(ix(i)), msg)

          If gBitmap IsNot Nothing Then  ' file ok

            'first, do a color merge on each file. Then average all the files.
            Try
              Using _
                  img As MagickImage = New MagickImage(gBitmap), _
                  imgs As New MagickImageCollection

                img2 = img.Clone
                img2.BrightnessContrast(wScale * r - 100, 0)
                imgs.Add(img2.Clone)
                img2 = img.Clone
                img2.BrightnessContrast(wScale * g - 100, 0)
                imgs.Add(img2.Clone)
                img2 = img.Clone
                img2.BrightnessContrast(wScale * b - 100, 0)
                imgs.Add(img2.Clone)

                img2 = imgs.Combine
                imgz.Add(img2.Clone)
              End Using
            Catch ex As Exception
              MsgBox(ex.Message)
              Exit For
            End Try
          End If
        End If
      Next i

      clearBitmap(img2)

      Try
        If imgz.Count > 0 Then
          Using imgx As MagickImage = imgz.Evaluate(EvaluateOperator.Mean) ' average all the files
            Using bmp As Bitmap = imgx.ToBitmap
              pView.setBitmap(bmp)
            End Using
          End Using
          If isMars AndAlso pView.Bitmap IsNot Nothing Then
            pView.BrightConSat(30, 20, -40, pView.Bitmap, pView.Bitmap, Nothing) ' untested -- may be different results
          End If
        End If

      Catch ex As Exception
        MsgBox(ex.Message)
      End Try

    End Using

    pView.Zoom(cZoom)
    Changed = False
    Me.Cursor = Cursors.Default

  End Sub

  Sub mergeColorsBatch(ByRef fName() As String)

    Dim i As Integer
    Dim wScale As Double
    Dim greenTotal, redTotal, blueTotal As Integer
    Dim g, r, b As Double
    Dim msg As String = ""

    Dim imgz As New MagickImageCollection

    If Processing Or Not Changed Then Exit Sub

    redTotal = 0 : greenTotal = 0 : blueTotal = 0

    ' set the weights for non-independent weights
    For i = 0 To nFiles - 1
      redTotal = redTotal + CDbl(txRed(i).Text) * CDbl(txWeight(i).Text)
      greenTotal = greenTotal + CDbl(txGreen(i).Text) * CDbl(txWeight(i).Text)
      blueTotal = blueTotal + CDbl(txBlue(i).Text) * CDbl(txWeight(i).Text)
    Next i

    If chkWeight.Checked Then
      wScale = redTotal
      If wScale < greenTotal Then wScale = greenTotal
      If wScale < blueTotal Then wScale = blueTotal
      If wScale = 0 Then wScale = 1 Else wScale = 100 / wScale
    Else
      wScale = 1
    End If

    showPicture(fName(1), pView, False, pcomments)
    If (pView.Bitmap IsNot Nothing) Then
      Using gf As Graphics = Graphics.FromImage(pView.Bitmap)
        gf.Clear(Color.Black)
      End Using
    End If

    For i = 1 To nFiles
      r = CDbl(txRed(i - 1).Text) * CDbl(txWeight(i - 1).Text)
      g = CDbl(txGreen(i - 1).Text) * CDbl(txWeight(i - 1).Text)
      b = CDbl(txBlue(i - 1).Text) * CDbl(txWeight(i - 1).Text)
      If r > 0 Or g > 0 Or b > 0 Then

        Using img As MagickImage = New MagickImage(readBitmap(tagPath(ix(i)), msg))
          If img IsNot Nothing Then  ' file ok
            Try
              img.BackgroundColor = Color.White
              img.Tint(wScale * r & "," & wScale * g & "," & wScale * b)
              imgz.Add(img.Clone)
            Catch ex As Exception
            End Try
          End If
        End Using
      End If
    Next i

    Using img As MagickImage = imgz.Combine, saver As New ImageSave, bmp As Bitmap = img.ToBitmap
      attachPropertyItems(bmp, pComments)
      saver.write(bmp, tagPath(ix(i)), True)
    End Using

    pView.Refresh()
    Changed = False

  End Sub

  Sub globalkey(ByVal e As KeyEventArgs)

    Select Case e.KeyCode
      Case 38, 40 ' up
        If e.KeyCode = 38 Then SetRow(iRow - 1)
        If e.KeyCode = 40 Then SetRow(iRow + 1)
        Select Case icol
          Case 1
            txRed(iRow).Select()
          Case 2
            txGreen(iRow).Select()
          Case 3
            txBlue(iRow).Select()
          Case 4
            txWeight(iRow).Select()
        End Select
      Case 69 ' ctrl-e - toggle exposure
        If e.Control Then
          useExposure = Not useExposure
          Mars(0)
          pView.Refresh()
        End If

      Case 77 ' ctrl-m
        If e.Control And Not e.Shift Then
          isMars = True
          Mars(0)
          ' lead1.Saturation -280
          pView.Refresh()
        ElseIf e.Control And e.Shift Then
          Mars(1)
        End If

      Case 51, 52, 53, 54, 55 ' 3, 4, 5, 6, 7
        If e.Control Then
          isMars = True
          setMars(e.KeyCode - 48)
          mergeColors()
        End If

      Case 81 ' q
        If e.Control Then
          isMars = True
          marsBatch()
        End If

    End Select

  End Sub

  Private Sub txred_TextChanged(ByVal Sender As Object, ByVal e As EventArgs)
    Changed = True
  End Sub
  Private Sub txGreen_TextChanged(ByVal Sender As Object, ByVal e As EventArgs)
    Changed = True
  End Sub
  Private Sub txBlue_TextChanged(ByVal Sender As Object, ByVal e As EventArgs)
    Changed = True
  End Sub
  Private Sub txWeight_TextChanged(ByVal Sender As Object, ByVal e As EventArgs)
    Changed = True
  End Sub

  Private Sub txBox_KeyDown(ByVal Sender As Object, ByVal e As KeyEventArgs)
    globalkey(e)
  End Sub

  Private Sub txRed_Enter(ByVal Sender As Object, ByVal e As EventArgs)

    Dim index As Integer
    index = findIndex(txRed, Sender)

    SetRow(index)
    icol = 1
    txRed(index).SelectionStart = 0
    txRed(index).SelectionLength = Len(txRed(index).Text)
  End Sub
  Private Sub txGreen_Enter(ByVal Sender As Object, ByVal e As EventArgs)

    Dim index As Integer
    index = findIndex(txGreen, Sender)

    SetRow(index)
    icol = 2
    txGreen(index).SelectionStart = 0
    txGreen(index).SelectionLength = Len(txGreen(index).Text)
  End Sub
  Private Sub txBlue_Enter(ByVal Sender As Object, ByVal e As EventArgs)

    Dim index As Integer
    index = findIndex(txBlue, Sender)

    SetRow(index)
    icol = 3
    txBlue(index).SelectionStart = 0
    txBlue(index).SelectionLength = Len(txBlue(index).Text)
  End Sub
  Private Sub txWeight_Enter(ByVal Sender As Object, ByVal e As EventArgs)

    Dim index As Integer
    index = findIndex(txWeight, Sender)

    SetRow(index)
    icol = 4
    txWeight(index).SelectionStart = 0
    txWeight(index).SelectionLength = Len(txWeight(index).Text)
  End Sub

  Private Sub txWeight_Leave(ByVal Sender As Object, ByVal e As EventArgs)

    Dim x As Double
    Dim index As Integer

    If Me.ActiveControl Is cmdCancel Then Exit Sub
    index = findIndex(txWeight, Sender)

    If Not checknumber(txWeight(index).Text, 0, 3, x) Then
      txWeight(index).Select()
      MsgBox("Enter a number between 0 and 3")
      Exit Sub
    Else
      txWeight(index).Text = Format(x, "0.000")
    End If

    mergeColors()

  End Sub
  Private Sub txRed_Leave(ByVal Sender As Object, ByVal e As EventArgs)

    Dim x As Double
    Dim index As Integer

    If Me.ActiveControl Is cmdCancel Then Exit Sub
    index = findIndex(txRed, Sender)

    If Not checknumber(txRed(index).Text, 0, 100, x) Then
      txRed(index).Select()
      MsgBox("Enter a number between 0 and 100")
      Exit Sub
    Else
      txRed(index).Text = CStr(Round(x))
    End If

    mergeColors()

  End Sub
  Private Sub txGreen_Leave(ByVal Sender As Object, ByVal e As EventArgs)

    Dim x As Double
    Dim index As Integer

    If Me.ActiveControl Is cmdCancel Then Exit Sub
    index = findIndex(txGreen, Sender)

    If Not checknumber(txGreen(index).Text, 0, 100, x) Then
      txGreen(index).select()
      MsgBox("Enter a number between 0 and 100")
      Exit Sub
    Else
      txGreen(index).Text = CStr(Round(x))
    End If

    mergeColors()

  End Sub
  Private Sub txBlue_Leave(ByVal Sender As Object, ByVal e As EventArgs)

    Dim x As Double
    Dim index As Integer

    If Me.ActiveControl Is cmdCancel Then Exit Sub
    index = findIndex(txBlue, Sender)

    If Not checknumber(txBlue(index).Text, 0, 100, x) Then
      txBlue(index).select()
      MsgBox("Enter a number between 0 and 100")
      Exit Sub
    Else
      txBlue(index).Text = CStr(Round(x))
    End If

    mergeColors()

  End Sub

  Sub SetRow(ByVal newRow As Integer)

    Dim i As Integer

    If newRow < 0 Then newRow = nFiles
    If newRow >= nFiles Then newRow = 0

    txPath(iRow).BackColor = normalColor
    txRed(iRow).BackColor = normalColor
    txGreen(iRow).BackColor = normalColor
    txBlue(iRow).BackColor = normalColor
    txWeight(iRow).BackColor = normalColor

    iRow = newRow
    txPath(iRow).BackColor = highLight
    txRed(iRow).BackColor = highLight
    txGreen(iRow).BackColor = highLight
    txBlue(iRow).BackColor = highLight
    txWeight(iRow).BackColor = highLight

    For i = 0 To optColor.Count - 1
      If IsNumeric(txRed(iRow).Text) And IsNumeric(txGreen(iRow).Text) And IsNumeric(txBlue(iRow).Text) Then
        If CDbl(txRed(iRow).Text) = red(i) And CDbl(txGreen(iRow).Text) = green(i) And CDbl(txBlue(iRow).Text) = blue(i) Then
          optColor(i).Checked = True
        Else
          optColor(i).Checked = False
        End If
      End If
    Next i

  End Sub

  Sub Mars(ByRef index As Short)

    Dim i As Integer
    Dim k As Integer

    Processing = True

    If index = 0 Then

      SetRow(0)
      txWeight(0).Text = "0.00"

      For i = 1 To nFiles
        k = CInt(Mid(txPath(i - 1).Text, InStr(txPath(i - 1).Text, ".") - 3, 1))
        SetRow(i - 1)
        setMars(k)
      Next i

      'SetRow(1)
      'setMars(3)
      'SetRow(2)
      'setMars(4)
      'SetRow(3)
      'setMars(5)
      'SetRow(4)
      'setMars(6)
      'SetRow(5)
      'setMars(7)

      SetRow(0)

    ElseIf index = 1 Then
      SetRow(0)
      txWeight(0).Text = CStr(0)

      SetRow(1)
      setMarsx(3)
      SetRow(2)
      setMarsx(4)
      SetRow(3)
      setMarsx(5)
      SetRow(4)
      setMarsx(6)
      SetRow(5)
      setMarsx(7)

      SetRow(0)
    End If

    Processing = False

    mergeColors()

  End Sub

  Sub setMars(ByVal index As Short)

    Dim i As Integer

    Dim x(7) As Double
    Dim y(7) As Double
    Dim z(7) As Double

    ' weights for non-exposure stuff
    x(3) = 0.6
    x(4) = 0.5
    x(5) = 0.96
    x(6) = 1
    x(7) = 1

    ' target exposures - make these bigger for more weight
    y(3) = 0.4
    y(4) = 0.5
    y(5) = 1.52
    y(6) = 1.74
    y(7) = 5.98

    For i = 3 To 7
      z(i) = x(i) * y(i)
    Next i

    If fExposure(index) = 0 Or (Not useExposure) Then
      txWeight(iRow).Text = CStr(x(index))
    Else
      txWeight(iRow).Text = CStr(z(index) / fExposure(index))
    End If

    Select Case index

      Case 3
        txRed(iRow).Text = CStr(210 \ 2.55)
        txGreen(iRow).Text = CStr(0)
        txBlue(iRow).Text = CStr(0)

      Case 4
        txRed(iRow).Text = CStr(255 \ 2.55)
        txGreen(iRow).Text = CStr(149 \ 2.55)
        txBlue(iRow).Text = CStr(0)

      Case 5
        txRed(iRow).Text = CStr(106 \ 2.55)
        txGreen(iRow).Text = CStr(255 \ 2.55)
        txBlue(iRow).Text = CStr(0)

      Case 6
        txRed(iRow).Text = CStr(0)
        txGreen(iRow).Text = CStr(50 \ 2.55)
        txBlue(iRow).Text = CStr(204 \ 2.55)

      Case 7
        txRed(iRow).Text = CStr(87 \ 2.55)
        txGreen(iRow).Text = CStr(30 \ 2.55)
        txBlue(iRow).Text = CStr(142 \ 2.55)

      Case Else
        txWeight(iRow).Text = CStr(0)

    End Select

  End Sub
  Sub setMarsNoBand(ByVal index As Short)

    Select Case index

      Case 3
        txWeight(iRow).Text = CStr(1)
        txRed(iRow).Text = CStr(255 \ 2.55)
        txGreen(iRow).Text = CStr(0)
        txBlue(iRow).Text = CStr(0)

      Case 4
        txWeight(iRow).Text = CStr(1)
        txRed(iRow).Text = CStr(255 \ 2.55)
        txGreen(iRow).Text = CStr(255 \ 2.55)
        txBlue(iRow).Text = CStr(0)

      Case 5
        txWeight(iRow).Text = CStr(1)
        txRed(iRow).Text = CStr(0)
        txGreen(iRow).Text = CStr(255 \ 2.55)
        txBlue(iRow).Text = CStr(255 \ 2.55)

      Case 6, 7
        txWeight(iRow).Text = CStr(1)
        txRed(iRow).Text = CStr(0)
        txGreen(iRow).Text = CStr(0)
        txBlue(iRow).Text = CStr(255 \ 2.55)

      Case Else
        txWeight(iRow).Text = CStr(0)
        txRed(iRow).Text = CStr(0)
        txGreen(iRow).Text = CStr(0)
        txBlue(iRow).Text = CStr(0)

    End Select

  End Sub
  Sub setMarsx(ByVal index As Short)

    Select Case index

      Case 3
        txWeight(iRow).Text = "1.00"
        txRed(iRow).Text = CStr(100)
        txGreen(iRow).Text = CStr(0)
        txBlue(iRow).Text = CStr(0)

      Case 4
        txWeight(iRow).Text = "0.50"
        txRed(iRow).Text = CStr(255)
        txGreen(iRow).Text = CStr(255)
        txBlue(iRow).Text = CStr(0)

      Case 5
        txWeight(iRow).Text = "1.00"
        txRed(iRow).Text = CStr(0)
        txGreen(iRow).Text = CStr(255)
        txBlue(iRow).Text = CStr(0)

      Case 6
        txWeight(iRow).Text = "1.00"
        txRed(iRow).Text = CStr(0)
        txGreen(iRow).Text = CStr(0)
        txBlue(iRow).Text = CStr(255)

      Case 7
        txWeight(iRow).Text = "1.00"
        txRed(iRow).Text = CStr(79)
        txGreen(iRow).Text = CStr(47)
        txBlue(iRow).Text = CStr(79)

      Case Else
        txWeight(iRow).Text = CStr(0)
        txRed(iRow).Text = CStr(0)
        txGreen(iRow).Text = CStr(0)
        txBlue(iRow).Text = CStr(0)

    End Select

  End Sub

  Sub marsBatch()
    ' they have color cameras on mars now.

  End Sub

  Sub processGroup(ByRef fName() As String, ByRef nFiles As Integer)

    Dim outName As String
    Dim cName As String ' old color name, get comments from this.
    Dim j, i, k As Integer
    Dim NoBand(7) As Boolean
    Dim s As String
    Dim m As String
    Dim saver As New ImageSave

    outName = fName(nFiles)
    i = InStr(outName, ".") - 3
    Mid(outName, i, 1) = "-"
    cName = outName

    'outName = "c:\mars\" & outName

    For i = 1 To 7 : NoBand(i) = True : Next i
    For i = 1 To nFiles
      j = CInt(Mid(fName(i), InStr(fName(i), ".") - 3, 1))
      NoBand(j) = False
    Next i

    If NoBand(3) Or NoBand(4) Or NoBand(5) Or (NoBand(6) Or NoBand(7)) Then
      i = InStr(outName, ".") - 3
    End If

    For i = 1 To nFiles
      iRow = i - 1
      k = CInt(Mid(fName(i), InStr(fName(i), ".") - 3, 1))
      If NoBand(7) Then
        setMarsNoBand(k) ' only 3,4,5,6
      ElseIf NoBand(6) Then
        setMarsNoBand(k) ' only 3,4,5,7
      Else
        setMars(k)
      End If
    Next i

    Changed = True

    mergeColorsBatch(fName)

    If cName <> "" Then
      pComments = readPropertyItems(cName)
    Else
      pComments = New List(Of PropertyItem)
    End If

    If pComments.Count > 0 Then ' save comment only if exif already
      s = getBmpComment(propID.ImageDescription, pComments)
      For i = 3 To 7
        m = "(Color filter " & i & " is missing.)"
        If Len(s) = 0 And NoBand(i) Then s = m
        If Len(s) >= Len(m) Then If Not s.EndsWith(m) Then If NoBand(i) Then s = m & crlf & s
      Next i
      s = s.Trim
      setBmpComment(propID.ImageDescription, pComments, s, exifType.typeAscii)
    End If

    saver.write(pView.Bitmap, outName, True)

  End Sub

  Sub getExposureFactors()

    Dim i As Integer
    Dim k As Integer
    Dim expo(10) As Double

    Dim v As Object
    Dim x As Double

    For i = 1 To nFiles
      Try
        v = getBmpComment(propID.ExposureTime, pComments)
        x = v(0)
      Catch ex As Exception
        MsgBox(ex.Message)
        x = 0
      End Try
      expo(i) = x
    Next i

    ' use the filter number for the subscript
    For i = 1 To nFiles
      If IsNumeric(Mid(tagPath(ix(i)), InStr(tagPath(ix(i)), ".") - 3, 1)) Then
        k = CInt(Mid(tagPath(ix(i)), InStr(tagPath(ix(i)), ".") - 3, 1))
        If k > UBound(fExposure) Then k = 0
      Else
        Exit For
      End If
      fExposure(k) = expo(i)
    Next i

  End Sub

  Private Sub frmColorMerge_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
    clearBitmap(gBitmap)
  End Sub

End Class