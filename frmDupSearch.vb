Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports System.IO
Imports System.Collections.Generic
Imports ImageMagick

Public Class frmDupSearch
  Inherits Form

  Dim v As New List(Of Object)
  Dim ns As New List(Of String)
  Dim nw As New List(Of Integer)
  Dim nh As New List(Of Integer)
  Dim Scores As New List(Of Integer)
  Dim ScoreFile1 As New List(Of String)
  Dim ScoreFile2 As New List(Of String)
  Dim ix As New List(Of Integer)
  Dim iFactor(255) As Integer
  Dim abort As Boolean
  Dim processing As Boolean
  Dim loading As Boolean = True
  Dim singleFile As Boolean

  Dim diffLimit As Integer

  Dim gImagex As New List(Of Bitmap)
  Dim gImage As Bitmap

  Private Sub cmdCancel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdClose.Click
    abort = True
    Me.DialogResult = DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub cmdDelete_Click(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles cmdDelete0.Click, cmdDelete1.Click

    Dim s As String
    Dim i, k As Integer
    Dim mResult As MsgBoxResult

    If ListView1.SelectedItems.Count < 1 Then Exit Sub

    If Sender Is cmdDelete0 Then
      s = ListView1.SelectedItems(0).SubItems(1).Text
    Else
      s = ListView1.SelectedItems(0).SubItems(2).Text
    End If

    k = ListView1.SelectedItems(0).Index

    mResult = MsgBox("Do you really want to erase " & s & "?", MsgBoxStyle.YesNo)
    If mResult = MsgBoxResult.Yes Then
      Try
        File.Delete(s)
      Catch ex As Exception
        MsgBox(s & " could not be deleted." & vbCrLf & ex.Message)
      End Try
      i = 0
      Do While i < ListView1.Items.Count
        If ListView1.Items.Item(i).SubItems(1).Text = s Or ListView1.Items.Item(i).SubItems(2).Text = s Then
          ListView1.Items.RemoveAt(i)
        Else
          i = i + 1
        End If
      Loop

      If k >= ListView1.Items.Count Then k = ListView1.Items.Count - 1
      If k >= 0 Then
        ListView1.Items.Item(k).Selected = True
        loadPictures(k)
      Else
        pView0.setBitmap(Nothing)
        pView0.Refresh()
        pView1.setBitmap(Nothing)
        pView1.Refresh()
        lbFilename0.Text = ""
        lbFilename1.Text = ""
      End If
    End If

  End Sub

  Private Sub cmdHelp_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdHelp.Click
    Try
      Help.ShowHelp(Me, helpFile, HelpNavigator.Topic, Me.Name & ".html")
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

  Private Sub cmdStart_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdStart.Click

    Dim s As String

    ' 1, 10, 50, 500, 1000, 2000,  5000, 10,000
    ' score is average of the pixel differences squared * 10

    ' check filename, if it's there
    txFilename.Text = txFilename.Text.Trim
    If txFilename.Text <> "" AndAlso (Not File.Exists(txFilename.Text)) Then
      MsgBox(txFilename.Text & "was not found.")
      txFilename.select()
      Exit Sub
    End If

    Select Case trkTolerance.Value
      Case 1
        diffLimit = 10
      Case 2
        diffLimit = 100
      Case 3
        diffLimit = 500
      Case 4
        diffLimit = 2500
      Case 5
        diffLimit = 5000
      Case 6
        diffLimit = 10000
      Case Else
        diffLimit = 2000
    End Select

    abort = False
    processing = True
    setView()

    ProgressBar1.Value = ProgressBar1.Minimum
    ProgressBar1.Visible = True
    pView1.setBitmap(Nothing)
    pView1.Refresh()
    lbFilename1.Text = ""
    cmdDelete0.Enabled = False
    cmdDelete1.Enabled = False
    ListView1.Items.Clear()

    Me.Cursor = Cursors.WaitCursor

    If txFilename.Text <> "" Then
      ns = New List(Of String)
      ns.Add(txFilename.Text) ' filenames
      singleFile = True
    End If

    s = txSearch.Text
    If Not s.EndsWith("\") Then s &= "\"
    scanPics(s, chkSubfolders.Checked)

    If abort Then
      Me.Cursor = Cursors.Default
      Exit Sub
    End If

    Me.Cursor = Cursors.WaitCursor
    getScores()
    Me.Cursor = Cursors.Default

    v = New List(Of Object)
    ns = New List(Of String)
    ScoreFile1 = New List(Of String)
    ScoreFile2 = New List(Of String)
    Scores = New List(Of Integer)
    ix = New List(Of Integer)

    ProgressBar1.Visible = False
  End Sub

  Private Sub frmPicSearch_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Dim i As Integer

    v = New List(Of Object)
    ns = New List(Of String)
    ScoreFile1 = New List(Of String)
    ScoreFile2 = New List(Of String)

    Scores = New List(Of Integer)
    ix = New List(Of Integer)

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    loading = True

    ListView1.ListViewItemSorter = New lvSort(0)

    txSearch.Text = iniExplorePath

    ListView1.View = View.Details
    ListView1.Columns.Add("score", CInt(ListView1.Width * 0.1), HorizontalAlignment.Right)
    ListView1.Columns.Add("file 1", CInt(ListView1.Width * 0.4), HorizontalAlignment.Left)
    ListView1.Columns.Add("file 2", CInt(ListView1.Width * 0.4), HorizontalAlignment.Left)

    ProgressBar1.Visible = False
    ProgressBar1.Minimum = 0
    ProgressBar1.Maximum = 100

    cmdDelete0.Enabled = False
    cmdDelete1.Enabled = False
    lbFilename0.Text = ""
    lbFilename1.Text = ""

    trkTolerance.Value = 5

    For i = 1 To 255 : iFactor(i) = i * i : Next i ' saves some multiplies in the loops
    'call paintqual(RasterPaintDisplayModeFlags.Bicubic, pView0)
    'call paintqual(RasterPaintDisplayModeFlags.Bicubic, pView1)

    loading = False

  End Sub

  Private Sub pview_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) _
    Handles pView0.MouseClick, pView1.MouseClick


    Dim msg As String = ""

    If processing Then Exit Sub

    If ListView1.SelectedItems.Count = 1 Then
      If sender Is pView0 Then
        currentpicPath = ListView1.SelectedItems(0).SubItems(1).Text
      Else
        currentpicPath = ListView1.SelectedItems(0).SubItems(2).Text
      End If

      qImage = readBitmap(currentpicPath, msg)

      If qImage IsNot Nothing Then
        callingForm = Me
        Using frm As New frmFullscreen
          frm.ShowDialog()
        End Using

        clearBitmap(qImage)
      End If
    End If

  End Sub

  Private Sub pView_MouseEnter(ByVal sender As Object, ByVal e As EventArgs) _
    Handles pView0.MouseEnter, pView1.MouseEnter

    Dim rv As pViewer
    rv = sender

    If (rv.Bitmap IsNot Nothing) And (Not processing) Then
      rv.Cursor = Cursors.Hand
    Else
      rv.Cursor = Cursors.Default
    End If
  End Sub

  Private Sub ListView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListView1.SelectedIndexChanged

    If loading Then Exit Sub
    If ListView1.SelectedItems.Count = 1 Then loadPictures(ListView1.SelectedIndices(0))

  End Sub

  Sub loadPictures(ByVal index As Integer)

    Dim size0, size1 As Long
    Dim width0, width1, height0, height1 As Integer
    Dim fName1, fName2 As String
    Dim picInfo As pictureInfo
    Dim msg As String = ""

    If processing Then Exit Sub

    Me.Cursor = Cursors.WaitCursor
    pView0.setBitmap(Nothing)
    pView1.setBitmap(Nothing)
    fName1 = ListView1.Items(index).SubItems(1).Text
    fName2 = ListView1.Items(index).SubItems(2).Text

    'showPicture(fname1, pView0, False, Nothing, 0)
    picInfo = getPicinfo(fName1, False)
    If picInfo.isNull Then
      MsgBox(picInfo.ErrMessage)
      Exit Sub
    End If
    width0 = picInfo.Width
    height0 = picInfo.Height

    picInfo = getPicinfo(fName2, False)
    If picInfo.isNull Then
      MsgBox(picInfo.ErrMessage)
      Me.Cursor = Cursors.Default
      Exit Sub
    End If
    width1 = picInfo.Width
    height1 = picInfo.Height

    fitFile(fName1, pView0)
    'showPicture(fname2, pView1, False, Nothing, 0)
    fitFile(fName2, pView1)

    size0 = 0 : size1 = 0
    Try
      size0 = New FileInfo(ListView1.Items.Item(index).SubItems(1).Text).Length
      size1 = New FileInfo(ListView1.Items.Item(index).SubItems(2).Text).Length
    Catch ex As Exception
      msg = ex.Message
    End Try

    lbFilename0.Text = ListView1.Items.Item(index).SubItems(1).Text & ", " & width0 & "x" & height0 & ", " & Format(size0 / 1000, "#,#.0kb")
    lbFilename1.Text = ListView1.Items.Item(index).SubItems(2).Text & ", " & width1 & "x" & height1 & ", " & Format(size1 / 1000, "#,#.0kb")

    Me.Cursor = Cursors.Default

  End Sub

  Private Sub txSearch_Enter(ByVal Sender As Object, ByVal e As EventArgs) Handles txSearch.Enter
    txSearch.SelectionStart = 0
    txSearch.SelectionLength = Len(txSearch.Text)
  End Sub

  Sub scanPics(ByRef fPath As String, ByRef subFolders As Boolean)

    Dim k, i As Integer
    Dim hSize As Single
    Dim rgbVal(2) As Integer
    Dim iFile As Integer
    Dim nPix As Integer
    Dim mResult As MsgBoxResult
    Dim histo(2, 255) As Integer
    Dim bmp As Bitmap
    Dim msg As String = ""

    processing = True

    If Not Directory.Exists(fPath) Then
      MsgBox("The folder " & fPath & " was not found.")
      abort = True
      Exit Sub
    End If

    i = getFilePaths(fPath, ns, subFolders)

    If ns.Count < 1 Then
      MsgBox("No files were found in " & fPath)
      abort = True
      Exit Sub
    End If

    If ns.Count > 500 Then
      If ns.Count < 3000 Then
        mResult = MsgBox(ns.Count & " photos will be compared with one another. This may take a while. Do you want to continue?", MsgBoxStyle.YesNo)
      Else
        mResult = MsgBox(ns.Count & " photos will be compared with one another. This will take a long time. Do you want to continue?", MsgBoxStyle.YesNo)
      End If
      If mResult = MsgBoxResult.No Then
        abort = True
        Exit Sub
      End If
    End If

    hSize = 100

    For iFile = 0 To ns.Count - 1
      ProgressBar1.Value = (iFile + 1) / ns.Count / 2 * ProgressBar1.Maximum

      bmp = readBitmap(ns(iFile), msg)
      If bmp IsNot Nothing Then
        pView0.InterpolationMode = InterpolationMode.Low
        pView0.ResizeBitmap(hSize, hSize, bmp, bmp) ' maintain aspect ratio
        pView0.setBitmap(bmp)
      End If
      clearBitmap(bmp)

      If pView0.Bitmap IsNot Nothing Then
        pView0.Zoom(-1)
        lbFilename0.Text = ns(iFile)

        Try
          gImagex.Add(pView0.Bitmap.Clone)
        Catch ex As Exception
          MsgBox("There is not enough memory to compare all the files. The first " & i + 1 & " will be compared with one another.")
          ns.RemoveRange(iFile, ns.Count - iFile)
          nw.RemoveRange(iFile, ns.Count - iFile)
          nh.RemoveRange(iFile, ns.Count - iFile)
          gImagex.RemoveRange(iFile, ns.Count - iFile)
          Exit For
        End Try

        nw.Add(pView0.Bitmap.Width)
        nh.Add(pView0.Bitmap.Height)

        'Create the histogram, collect total red, green, blue values in v
        rgbVal(0) = 0 : rgbVal(1) = 0 : rgbVal(2) = 0
        nPix = pView0.Bitmap.Width * pView0.Bitmap.Height

        histo = getHisto(pView0.Bitmap)

        For j As Integer = 0 To 2
          k = 0
          For i = 1 To 255
            k = k + histo(j, i) * i
          Next i
          rgbVal(j) = k / nPix
        Next j

        v.Add(rgbVal.Clone)

        Application.DoEvents()
        If abort Then Exit For

      Else
        gImagex.Add(New Bitmap(100, 100, PixelFormat.Format32bppPArgb))
        nw.Add(0) ' flag for invalid
        nh.Add(0)
        v.Add(0)
      End If
    Next iFile

  End Sub

  Sub getScores()

    Dim i As Integer
    Dim vs As lvSort
    Dim item As ListViewItem
    Dim subs(2) As String

    ListView1.Items.Clear()
    cmdClose.select()

    If singleFile Then
      getFileScore(0)
    Else
      For i = 0 To ns.Count - 2
        ProgressBar1.Value = i / (ns.Count - 1) * 0.5 * ProgressBar1.Maximum + ProgressBar1.Maximum * 0.5
        getFileScore(i)
        Application.DoEvents()
        If abort Then Exit For
      Next i
    End If

    If abort Then Exit Sub

    For i = 0 To ns.Count - 1
      clearBitmap(gImagex(i))
    Next i

    For i = 0 To Scores.Count - 1
      ix.Add(i)
    Next i

    MergeSort(Scores, ix, 0, Scores.Count - 1)

    ListView1.Sorting = SortOrder.None
    ListView1.ListViewItemSorter = Nothing
    ListView1.BeginUpdate()

    For i = 0 To Scores.Count - 1
      subs(0) = Format(Scores(ix(i)), "#,0")
      subs(1) = ScoreFile1(ix(i))
      subs(2) = ScoreFile2(ix(i))
      item = New ListViewItem(subs)
      ListView1.Items.Add(item)
    Next i

    processing = False
    ListView1.EndUpdate()
    ListView1.ListViewItemSorter = New lvSort(0)
    setView()

    If Scores.Count > 0 Then
      cmdDelete0.Enabled = True
      cmdDelete1.Enabled = True
      ListView1.HideSelection = False
      ListView1.Items.Item(0).Selected = True
      ListView1.Items.Item(0).Focused = True
      ' loadPictures(0)
      vs = ListView1.ListViewItemSorter
      vs.sortkey = 0
      vs.dataType = "number"
      vs.sortOrder = 1 ' ascending

      ListView1.Sort()
      ListView1.select()
    Else
      pView0.setBitmap(Nothing)
      pView0.Refresh()
      pView0.setBitmap(Nothing)
      pView1.Refresh()
      lbFilename0.Text = ""
      lbFilename1.Text = ""
      MsgBox("No matching files were found.")
    End If

  End Sub

  Sub getFileScore(ByVal i As Integer)

    ' add low scores of all files against i into scores

    Dim j, k As Integer
    Dim i1 As Integer
    Dim x As Integer
    Dim rgb1, rgb2 As Object
    Dim diffLimit3 As Integer
    Dim nPix As Integer
    Dim d1(2) As Integer
    Dim subs(2) As String
    Dim r As Rectangle
    Dim histo(2, 255) As Integer

    diffLimit3 = diffLimit * 0.06

    If nh(i) > 0 AndAlso nw(i) > 0 AndAlso gImagex(i) IsNot Nothing Then
      pView0.setBitmap(gImagex(i))
      nPix = nh(i) * nw(i) * 0.1
      pView0.Zoom(-1)
      lbFilename0.Text = ns(i)
      rgb1 = v(i)

      For j = i + 1 To ns.Count - 1
        If nh(j) > 0 And nw(j) > 0 Then
          rgb2 = v(j)
          For k = 0 To 2
            d1(k) = Abs(rgb1(k) - rgb2(k))
            d1(k) = d1(k) * d1(k)
          Next k
          If Abs(nh(i) - nh(j)) <= 3 And Abs(nw(i) - nw(j)) <= 3 And d1(0) <= diffLimit3 And d1(1) <= diffLimit3 And d1(2) <= diffLimit3 Then

            pView1.setBitmap(pView0.Bitmap)
            clearBitmap(gImage)
            gImage = pView0.Bitmap.Clone
            r = New Rectangle(0, 0, nw(i), nh(i))

            bmpMerge(gImagex(j), pView1.Bitmap, mergeOp.opSubtractFromTarget, r)
            bmpMerge(gImagex(j), gImage, mergeOp.opSubtractFromSource, r)
            bmpMerge(gImage, pView1.Bitmap, mergeOp.opAdd, r)

            histo = getHisto(pView1.Bitmap)

            k = 0
            For i1 = 1 To 255
              k = k + (histo(0, i1) + histo(1, i1) + histo(2, i1)) * iFactor(i1) ' ifactor is i1^2
            Next i1
            x = k / nPix / 3

            If x <= diffLimit Then
              Scores.Add(x)
              ScoreFile1.Add(ns(i))
              ScoreFile2.Add(ns(j))
            End If
          End If
        End If
        Application.DoEvents()
        If abort Then Exit For
      Next j
    End If


  End Sub

  Sub setView()

    lbFilename0.Top = lbFilename1.Top
    lbFilename0.Width = ((pView0.Width) - ((lbFilename0.Left) - (pView0.Left)))

    If processing Then ' buttons, lead2 invisible
      cmdDelete0.Visible = False
      cmdDelete1.Visible = False
      pView1.Visible = False
      lbFilename1.Visible = False
    Else
      cmdDelete0.Visible = True
      cmdDelete1.Visible = True
      pView1.Visible = True
      lbFilename1.Visible = True
    End If

  End Sub

  Private Sub ListView1_ColumnClick(ByVal Sender As Object, ByVal e As ColumnClickEventArgs) Handles ListView1.ColumnClick

    Dim v As lvSort
    Dim s As String
    Dim Ascend As String = "  " & ChrW(&H25B2)
    Dim Descend As String = "  " & ChrW(&H25BC)

    v = ListView1.ListViewItemSorter ' this is in frmPicSearch.vb

    s = ListView1.Columns(v.sortkey).Text
    s = Replace(s, Ascend, "")
    s = Replace(s, Descend, "")
    ListView1.Columns(v.sortkey).Text = s

    s = ListView1.Columns(e.Column).Text
    If e.Column = v.sortkey Then
      If v.sortOrder = 1 Then
        v.sortOrder = -1 ' descending
        ListView1.Columns(v.sortkey).Text = s & Descend
      Else
        v.sortOrder = 1 ' ascending
        ListView1.Columns(v.sortkey).Text = s & Ascend
      End If
    Else
      v.sortOrder = 1 ' ascending
      v.sortkey = e.Column
      ListView1.Columns(v.sortkey).Text = s & Ascend
    End If

    Select Case e.Column
      Case 0
        v.dataType = "number"
      Case Else ' includes 0 - name
        v.dataType = "" ' string
    End Select

    ListView1.Sort()
    If ListView1.SelectedItems.Count = 1 Then ListView1.SelectedItems(0).EnsureVisible()

  End Sub

  Private Sub cmdBrowse_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdBrowse.Click
    'Opens a folder browser

    Dim result As DialogResult

    Try
      folderDialog1.SelectedPath = txSearch.Text
    Catch
    End Try

    Try
      result = folderDialog1.ShowDialog()
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    If result = DialogResult.OK Then txSearch.Text = folderDialog1.SelectedPath

  End Sub

  Private Sub cmdOpenFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpenFile.Click

    Dim fileNames(-1) As String

    fileNames = InputFilename(False)
    If UBound(fileNames) = 0 Then txFilename.Text = fileNames(0)

  End Sub

  Private Sub form_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing

    Dim gm As Bitmap

    pView0.Dispose()
    pView1.Dispose()
    clearBitmap(gImage)

    For Each gm In gImagex
      clearBitmap(gm)
    Next gm

  End Sub

  Private Sub Panel1_Resize(sender As Object, e As EventArgs) Handles Panel1.Resize

    Dim margin As Integer = 3

    If Panel1.Width < 20 Or Panel1.Height < 50 Then Exit Sub

    pView0.Left = margin : pView0.Top = margin
    pView0.Left = margin : pView0.Top = margin
    pView0.Width = Panel1.Width \ 2 - margin * 4
    pView0.Height = Panel1.Height - (margin * 4 + cmdDelete0.Height)

    pView1.Width = pView0.Width
    pView1.Height = pView0.Height
    pView1.Left = pView0.Left + pView0.Width + margin * 2

    cmdDelete0.Left = pView0.Left + margin * 3
    cmdDelete0.Top = Panel1.Height - cmdDelete0.Height - margin
    lbFilename0.Top = cmdDelete0.Top
    lbFilename0.Left = cmdDelete0.Left + cmdDelete0.Width + margin * 3
    lbFilename0.Height = Panel1.Height - lbFilename0.Top - margin
    lbFilename0.Width = Panel1.Width - lbFilename0.Left - margin * 3

    cmdDelete1.Left = pView1.Left + margin * 3
    cmdDelete1.Top = cmdDelete0.Top
    lbFilename1.Top = cmdDelete1.Top
    lbFilename1.Left = cmdDelete1.Left + cmdDelete1.Width + margin * 3
    lbFilename1.Height = lbFilename0.Height
    lbFilename1.Width = lbFilename0.Width

    pView0.Zoom(-1)
    pView1.Zoom(-1)

  End Sub
End Class



