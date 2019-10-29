'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports System.IO
Imports ImageMagick

Public Class frmFilter
  Inherits Form

  Dim mx As ConvolveMatrix
  Dim multiplier As Double
  Dim maxFilterSize As Integer

  Dim Loading As Boolean = False

  Dim gpath As GraphicsPath = Nothing

  Dim imageReduced As Boolean
  Dim xreduced As Double

  Dim WithEvents Timer1 As New Timer

  Dim txFilt(48) As TextBox

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

    Me.Cursor = Cursors.WaitCursor

    drawFilter(True)

    Me.Cursor = Cursors.Default
    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Private Sub aview_Resize(sender As Object, e As EventArgs) Handles aView.Resize
    aView.zoom(1)
  End Sub

  Sub aview_zoomed() Handles aView.Zoomed, Me.Resize
    Timer1.Stop() : Timer1.Interval = 150 : Timer1.Start() ' calls draw
  End Sub

  Private Sub cmdLoadfilter_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdLoadFilter.Click

    Dim path As String
    Dim fName As String
    Dim i, j, k As Integer
    Dim ipos As Integer
    Dim jLine As String
    Dim result As DialogResult
    Dim ss() As String = Nothing
    Dim sNum() As String

    path = My.Application.Info.DirectoryPath
    If Not path.EndsWith("\") Then path = path & "\"

    openDialog1.Title = "Load filter"
    openDialog1.FileName = ""
    openDialog1.Filter = "filter|*.filter"
    openDialog1.DefaultExt = ".filter"
    openDialog1.InitialDirectory = dataPath
    openDialog1.CheckFileExists = True
    openDialog1.CheckPathExists = True
    openDialog1.ShowReadOnly = True

    Try
      result = openDialog1.ShowDialog()
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    If result <> DialogResult.OK Then
      Exit Sub
    End If

    fName = openDialog1.FileName

    cmbFilter.SelectedIndex = 0

    For i = 0 To maxFilterSize - 1
      For j = 0 To maxFilterSize - 1
        mx(i, j) = 0
      Next j
    Next i

    ss = File.ReadAllLines(fName)
    If IsNumeric(ss(0)) Then k = CInt(ss(0)) Else k = 0
    If k > 0 AndAlso k <= maxFilterSize Then
      mx = New ConvolveMatrix(k)
    Else
      mx = Nothing
      Exit Sub
    End If

    For i = 0 To mx.Order - 1
      If i > UBound(ss) + 1 Then Exit For
      jLine = ss(i + 1)
      jLine = Trim(jLine)
      jLine = jLine.Replace("  ", " ")
      sNum = jLine.Split(" ")

      ipos = 1
      For j = 0 To mx.Order - 1
        If j > UBound(sNum) Then Exit For
        mx(j, i) = CDbl(sNum(j))
      Next j
    Next i

    setTextBoxes()

    Select Case mx.Order
      Case 3
        optSize3x3.Checked = True
      Case 5
        optSize5x5.Checked = True
      Case 7
        optSize7x7.Checked = True
    End Select

  End Sub

  Private Sub cmdClearfilter_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdClearFilter.Click
    mx = New ConvolveMatrix(mx.Order)
    setTextBoxes()
  End Sub

  Private Sub cmdSavefilter_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdSaveFilter.Click

    Dim path As String
    Dim fName As String
    Dim i, j As Integer
    Dim result As DialogResult
    Dim sf As String

    path = My.Application.Info.DirectoryPath
    If Not path.EndsWith("\") Then path = path & "\"

    saveDialog1.Title = "Load filter"
    saveDialog1.FileName = ""
    saveDialog1.Filter = "filter|*.filter"
    saveDialog1.DefaultExt = ".filter"
    saveDialog1.InitialDirectory = dataPath
    saveDialog1.CheckPathExists = True
    saveDialog1.OverwritePrompt = True

    Try
      result = saveDialog1.ShowDialog()
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    If result <> DialogResult.OK Then Exit Sub

    getFormData()

    fName = saveDialog1.FileName

    sf = mx.Order & crlf
    For i = 0 To mx.Order - 1
      For j = 0 To mx.Order - 1
        sf = sf & " " & mx(j, i)
      Next j
      sf = sf & crlf
    Next i

    Try
      File.WriteAllText(fName, sf, System.Text.Encoding.GetEncoding("ISO-8859-1"))
    Catch ex As Exception
      MsgBox(ex.Message)
      Exit Sub
    End Try

  End Sub

  Private Sub cmdStart_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdStart.Click
    getFormData()
    drawFilter(False)
  End Sub

  Private Sub Combo1_SelectedIndexChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles cmbFilter.SelectedIndexChanged

    Dim i As Integer
    Dim j As Integer

    If Loading Then Exit Sub

    Select Case cmbFilter.SelectedIndex

      Case 0 ' custom

      Case 1 ' contour
        changeFilterSize(3)
        mx = New ConvolveMatrix(3)
        For i = 0 To mx.Order - 1
          For j = 0 To mx.Order - 1
            mx(i, j) = -1
          Next j
        Next i
        mx(1, 1) = 8

      Case 2 ' emboss
        changeFilterSize(3)
        mx = New ConvolveMatrix(3)
        For j = 0 To 2
          mx(0, j) = -1
          mx(2, j) = 1
        Next j
        mx(1, 0) = 0
        mx(1, 2) = 0
        mx(1, 1) = 1

      Case 3 ' sharpen big
        changeFilterSize(3)
        mx = New ConvolveMatrix(5)
        For i = 0 To mx.Order - 1
          For j = 0 To mx.Order - 1
            mx(i, j) = -1
          Next j
        Next i
        mx(2, 2) = 30

      Case 4 ' sharpen lots
        changeFilterSize(3)
        mx = New ConvolveMatrix(3)
        For i = 0 To mx.Order - 1
          For j = 0 To mx.Order - 1
            mx(i, j) = -1
          Next j
        Next i
        mx(1, 1) = 9

    End Select

    setTextBoxes()

  End Sub

  Private Sub frmfilter_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Dim i As Integer

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Loading = True

    maxFilterSize = 7

    txFilt(0) = txFilter0
    txFilt(1) = txFilter1
    txFilt(2) = txFilter2
    txFilt(3) = txFilter3
    txFilt(4) = txFilter4
    txFilt(5) = txFilter5
    txFilt(6) = txFilter6
    txFilt(7) = txFilter7
    txFilt(8) = txFilter8
    txFilt(9) = txFilter9
    txFilt(10) = txFilter10
    txFilt(11) = txFilter11
    txFilt(12) = txFilter12
    txFilt(13) = txFilter13
    txFilt(14) = txFilter14
    txFilt(15) = txFilter15
    txFilt(16) = txFilter16
    txFilt(17) = txFilter17
    txFilt(18) = txFilter18
    txFilt(19) = txFilter19
    txFilt(20) = txFilter20
    txFilt(21) = txFilter21
    txFilt(22) = txFilter22
    txFilt(23) = txFilter23
    txFilt(24) = txFilter24
    txFilt(25) = txFilter25
    txFilt(26) = txFilter26
    txFilt(27) = txFilter27
    txFilt(28) = txFilter28
    txFilt(29) = txFilter29
    txFilt(30) = txFilter30
    txFilt(31) = txFilter31
    txFilt(32) = txFilter32
    txFilt(33) = txFilter33
    txFilt(34) = txFilter34
    txFilt(35) = txFilter35
    txFilt(36) = txFilter36
    txFilt(37) = txFilter37
    txFilt(38) = txFilter38
    txFilt(39) = txFilter39
    txFilt(40) = txFilter40
    txFilt(41) = txFilter41
    txFilt(42) = txFilter42
    txFilt(43) = txFilter43
    txFilt(44) = txFilter44
    txFilt(45) = txFilter45
    txFilt(46) = txFilter46
    txFilt(47) = txFilter47
    txFilt(48) = txFilter48

    For i = 0 To 48
      AddHandler txFilt(i).KeyDown, AddressOf txFilt_KeyDown
      AddHandler txFilt(i).Enter, AddressOf txFilt_Enter
      AddHandler txFilt(i).TextChanged, AddressOf txFilt_TextChanged
    Next i

    cmbFilter.Items.Insert(0, "Custom")
    cmbFilter.Items.Insert(1, "Contour")
    cmbFilter.Items.Insert(2, "Emboss")
    cmbFilter.Items.Insert(3, "Sharpen Big")
    cmbFilter.Items.Insert(4, "Sharpen Lots")
    cmbFilter.SelectedIndex = 0

    cmbFilter.SelectedIndex = 0
    optSize3x3.Checked = True
    mx = New ConvolveMatrix(3)
    setTextBoxes()

    multiplier = 1

    If frmMain.mView.FloaterPath IsNot Nothing Then gpath = frmMain.mView.FloaterPath.Clone

    ' if it's too big, reduce the size and assign that.
    If qImage.Width * qImage.Height > bigMegapix Then
      imageReduced = True
      xreduced = getSmallerImage(qImage, aView.pView0)
      aView.pView1.setBitmap(aView.pView0.Bitmap)
    Else
      imageReduced = False
      aView.pView0.setBitmap(qImage)
      aView.pView1.setBitmap(qImage)
      xreduced = 1
    End If

    aView.ZoomViews(0.5)

    Loading = False

  End Sub
  Sub setTextBoxes()

    Dim filterMin As Integer

    filterMin = (maxFilterSize - mx.Order) \ 2

    For ix As Integer = 0 To maxFilterSize - 1
      For iy As Integer = 0 To maxFilterSize - 1
        If ix < filterMin Or ix >= mx.Order + filterMin Or iy < filterMin Or iy >= mx.Order + filterMin Then
          txFilt(iy * maxFilterSize + ix).Visible = False
        Else
          txFilt(iy * maxFilterSize + ix).Text = CStr(mx(ix - filterMin, iy - filterMin))
          txFilt(iy * maxFilterSize + ix).Visible = True
        End If
      Next iy
    Next ix

  End Sub

  Private Sub Option_CheckedChanged(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles optSize3x3.CheckedChanged, optSize5x5.CheckedChanged, optSize7x7.CheckedChanged

    Dim opt As RadioButton = Sender

    If Loading Then Exit Sub

    If opt.Checked Then
      If optSize3x3.Checked Then
        changeFilterSize(3)
      ElseIf optSize5x5.Checked Then
        changeFilterSize(5)
      ElseIf optSize7x7.Checked Then
        changeFilterSize(7)
      End If
      setTextBoxes()
    End If

  End Sub

  Sub changeFilterSize(ByRef newsize As Integer)

    Dim i As Integer
    Dim j As Integer
    Dim k As Integer
    Dim oldSize As Integer
    Dim tmpFilt(maxFilterSize - 1, maxFilterSize - 1) As Double

    If newsize = mx.Order Then Exit Sub

    oldSize = mx.Order

    getFormData()

    Select Case newsize
      Case 3
        optSize3x3.Checked = True
      Case 5
        optSize5x5.Checked = True
      Case 7
        optSize7x7.Checked = True
    End Select

    For i = 0 To mx.Order - 1
      For j = 0 To mx.Order - 1
        tmpFilt(i, j) = mx(i, j)
      Next j
    Next i

    k = (newsize - oldSize) \ 2
    mx = New ConvolveMatrix(newsize)

    For i = 0 To newsize - 1 - k
      For j = 0 To newsize - 1 - k
        If i < oldSize And j < oldSize And i + k >= 0 And j + k >= 0 Then mx(i + k, j + k) = tmpFilt(i, j)
      Next j
    Next i

    setTextBoxes()

  End Sub

  Private Sub txFilt_TextChanged(ByVal Sender As Object, ByVal e As EventArgs)
    If Loading Then Exit Sub
    If cmbFilter.SelectedIndex <> 0 Then cmbFilter.SelectedItem = 0
  End Sub

  Private Sub txFilt_Enter(ByVal Sender As Object, ByVal e As EventArgs)

    Dim index As Integer
    Dim t As TextBox = Sender

    index = findIndex(txFilt, t)

    txFilt(index).SelectionStart = 0
    txFilt(index).SelectionLength = Len(txFilt(index).Text)

  End Sub

  Private Sub txFilt_KeyDown(ByVal Sender As Object, ByVal e As KeyEventArgs)

    Dim index As Integer
    Dim filterMin As Integer

    index = findIndex(txFilt, Sender)
    filterMin = (maxFilterSize - mx.Order) \ 2

    Select Case e.KeyCode
      Case Keys.Left
        If index Mod maxFilterSize > filterMin Then txFilt(index - 1).Select()
      Case Keys.Up
        If index > (filterMin + 1) * maxFilterSize Then txFilt(index - maxFilterSize).Select()
      Case Keys.Right
        If index Mod maxFilterSize < mx.Order + filterMin - 1 Then txFilt(index + 1).Select()
      Case Keys.Down
        If index < (mx.Order + filterMin - 1) * maxFilterSize Then txFilt(index + maxFilterSize).Select()
    End Select

  End Sub

  Sub getFormData()
    Dim x, z As Double
    Dim filterMin As Integer
    Dim s As String

    filterMin = (maxFilterSize - mx.Order) \ 2

    mx = New ConvolveMatrix(mx.Order)

    z = 0

    For ix As Integer = 0 To mx.Order - 1
      For iy As Integer = 0 To mx.Order - 1
        s = txFilt((iy + filterMin) * maxFilterSize + ix + filterMin).Text
        If IsNumeric(s) Then x = CDbl(s)
        mx(ix, iy) = x
        z += x
      Next iy
    Next ix

  End Sub

  Private Sub drawFilter(fullBitmap As Boolean)

    ' fullbitmap tells whether to refresh the entire bitmap or just the visible part.
    ' gimage is the optional output image, for final drawing when a temporary reduced size image is used.

    Static busy As Boolean = False

    Dim img As MagickImage

    If aView.pView0.Bitmap Is Nothing Then Exit Sub
    If busy Then Exit Sub
    busy = True

    Me.Cursor = Cursors.WaitCursor

    getFormData()

    If fullBitmap Then
      aView.pView1.ClearSelection()
      img = New MagickImage(qImage)

    Else
      ' only operate on the visible part of the bitmap
      ' use a floater image of the bitmap on the target
      ' this creates a region for clipping in setFloaterBitmap below
      aView.pView0.SetSelection(aView.pView0.ClientRectangle)
      aView.pView0.InitFloater()
      aView.pView0.FloaterVisible = False
      aView.pView0.FloaterOutline = False
      aView.pView1.SetSelection(aView.pView1.ClientRectangle)
      aView.pView1.InitFloater()
      aView.pView1.FloaterVisible = True
      aView.pView1.FloaterOutline = False
      img = bitmapToMagick(aView.pView0.FloaterBitmap)
    End If

    img.HasAlpha = False ' doesn't work with alpha mode

    Try
      img.Convolve(mx)
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
    saveStuff(img, aView.pView1, gpath, fullBitmap)
    aView.Repaint()
    aView.zoomLabel()

    Me.Cursor = Cursors.Default
    busy = False

  End Sub

  Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
    ' redraw after some milliseconds
    Timer1.Stop()
    drawFilter(False)
  End Sub

  Function scaled(ByVal x As Double, ByVal nm As NumericUpDown) As Double
    scaled = x * xreduced
    scaled = Max(scaled, nm.Minimum)
    scaled = Min(scaled, nm.Maximum)
  End Function

  Private Sub _FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
    Timer1.Stop()
    If gpath IsNot Nothing Then gpath.Dispose()
  End Sub

End Class