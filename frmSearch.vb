'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports System.Collections.Generic

Public Class frmSearch
  Inherits Form

  Dim ns As New List(Of String)
  Dim abort As Boolean
  Dim processing As Boolean
  Dim minDate, maxDate As Date
  Dim findWords As New List(Of String)
  Dim nFound As Integer

  Dim loading As Boolean = True
  Dim picPath As String

  Dim imgLviewState As New ImageList

  Private Sub cmdCancel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click

    abort = True
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

  Private Sub cmdStart_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdStart.Click

    Dim s As String

    If processing Then Exit Sub
    abort = False
    processing = True

    If Trim(txMinDate.Text) <> "" Then
      Try
        minDate = CDate(txMinDate.Text)
      Catch
        minDate = Nothing
      End Try
    End If

    If Trim(txMaxDate.Text) <> "" Then
      Try
        maxDate = CDate(txMaxDate.Text)
      Catch
        maxDate = Nothing
      End Try
    End If

    getWords(txFindText.Text)

    ProgressBar1.Value = ProgressBar1.Minimum
    ProgressBar1.Visible = True
    ListView1.Items.Clear()

    Me.Cursor = Cursors.WaitCursor
    s = txSearch.Text
    If Not s.EndsWith("\") Then s &= "\"
    Search(s, chkSubfolders.Checked)
    Me.Cursor = Cursors.Default

    ns = New List(Of String)

    ProgressBar1.Visible = False

    processing = False

  End Sub

  Private Sub cmdTag_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdTag.Click

    Dim item As ListViewItem

    If processing Then Exit Sub

    ' mark checked files as tagged for frmExplore
    tagPath = New List(Of String)
    For Each item In ListView1.CheckedItems
      tagPath.Add(item.Text)
    Next item

    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Private Sub frmSearch_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    ns = New List(Of String)
    findWords = New List(Of String)

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    loading = True
    ListView1.ListViewItemSorter = New lvSort(0)
    ListView1.Sorting = SortOrder.None

    txSearch.Text = iniExplorePath

    ListView1.View = View.Details
    ListView1.Columns.Add("Name")
    ListView1.Columns.Add("Size")
    ListView1.Columns.Add("Date Modified")
    ListView1.CheckBoxes = True

    ProgressBar1.Visible = False
    ProgressBar1.Minimum = 0
    ProgressBar1.Maximum = 100

    lbFilename.Text = ""

    chkSubfolders.Checked = True
    chkFilenames.Checked = False

    ListView1.Columns.Item(0).Width = CInt((ListView1.Width - 20) * 0.6)
    ListView1.Columns.Item(0).TextAlign = HorizontalAlignment.Left
    ListView1.Columns.Item(1).Width = CInt((ListView1.Width - 20) * 0.12)
    ListView1.Columns.Item(1).TextAlign = HorizontalAlignment.Right
    ListView1.Columns.Item(2).Width = CInt((ListView1.Width - 20) * 0.26)
    ListView1.Columns.Item(2).TextAlign = HorizontalAlignment.Left

    cmdTag.Enabled = False
    cmdOpen.Enabled = False

    ' iSize = New Size(ListView1.Font.Height + 2, ListView1.Font.Height + 2)
    getStateImages(imgLviewState, ListView1.BackColor, ListView1.Font.Height - 2, ListView1.Font.Height - 2)
    ListView1.StateImageList = imgLviewState

    optAllWords.Checked = True

    tagPath = New List(Of String)
    loading = False

  End Sub

  Private Sub form_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
    ListView1.Clear()
  End Sub

  Private Sub rview_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs)
    If rView.Bitmap Is Nothing Then Exit Sub
    callingForm = Me
    qImage = rView.Bitmap
    currentpicPath = picPath
    Using frm As New frmFullscreen
      frm.ShowDialog()
    End Using
    clearBitmap(qImage)
  End Sub

  Private Sub rview_MouseEnter(ByVal sender As Object, ByVal e As EventArgs)
    rView.Cursor = Cursors.Hand
  End Sub

  Private Sub ListView1_ItemChecked(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckedEventArgs) Handles ListView1.ItemChecked

    If ListView1.CheckedItems.Count > 0 Then
      cmdTag.Enabled = True
      cmdOpen.Enabled = True
    Else
      cmdTag.Enabled = False
      cmdOpen.Enabled = False
    End If

  End Sub

  Private Sub ListView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListView1.SelectedIndexChanged
    If loading Or processing Then Exit Sub
    If ListView1.SelectedItems.Count = 1 Then loadPicture(ListView1.SelectedIndices(0))
  End Sub

  Sub loadPicture(ByVal index As Integer)

    Dim fInfo As FileInfo
    Dim msg As String = ""

    Me.Cursor = Cursors.WaitCursor
    rView.setBitmap(Nothing)
    rView.Refresh()
    picPath = ListView1.Items(index).Text
    Using bmp As Bitmap = readBitmap(picPath, msg)
      rView.setBitmap(bmp)
    End Using

    If msg = "" Then
      fInfo = New FileInfo(ListView1.Items(index).Text)
      rView.Zoom(-1)
      lbFilename.Text = ListView1.Items(index).Text & ", " & rView.Bitmap.Width & "x" & rView.Bitmap.Height & ", " & Format(fInfo.Length / 1000, "#,#.0kb")
    End If

    Me.Cursor = Cursors.Default

  End Sub

  Private Sub txSearch_Enter(ByVal Sender As Object, ByVal e As EventArgs) Handles txSearch.Enter
    txSearch.SelectionStart = 0
    txSearch.SelectionLength = Len(txSearch.Text)
  End Sub

  Sub Search(ByRef fPath As String, ByRef subFolders As Boolean)

    Dim k, i As Integer
    Dim s, s2, s3 As String
    Dim iFile As Integer
    Dim vDate, fDate As DateTime
    Dim x As Double
    Dim fInfo As FileInfo
    Dim item As ListViewItem
    Dim pComments As List(Of PropertyItem)

    processing = True
    Me.Cursor = Cursors.WaitCursor

    i = getFilePaths(fPath, ns, subFolders)

    lbFound.Text = ""

    For iFile = 0 To ns.Count - 1
      If abort Then Exit For
      If ns.Count = 0 Then
        Me.Cursor = Cursors.Default
        Exit Sub
      End If
      ProgressBar1.Value = CInt((iFile + 1) / ns.Count * ProgressBar1.Maximum)

      Clock = New Stopwatch
      Clock.Reset() : Clock.Start()

      pComments = readPropertyItems(ns(iFile))
      milli(1) = Clock.ElapsedMilliseconds

      If pComments IsNot Nothing Then

        If minDate <> Nothing Or maxDate <> Nothing Then

          s = CStr(getBmpComment(propID.DateTime, pComments))
          If Len(s) = 19 Then
            s = s.Substring(0, 10) ' use date only, no time
            s = s.Replace(":", "/")
            Try
              vDate = CDate(s)
            Catch
              vDate = Nothing
            End Try
          End If
        End If

        If vDate = Nothing Or ((minDate = Nothing Or vDate >= minDate) And (maxDate = Nothing Or vDate <= maxDate)) Then
          ' date passes, check comments
          s = CStr(getBmpComment(propID.ImageDescription, pComments))
          s &= " " & CStr(getBmpComment(propID.DateTime, pComments))
          s &= " " & CStr(getBmpComment(propID.DateTimeOriginal, pComments))

          s3 = Path.GetFileNameWithoutExtension(ns(iFile)) ' for filename search
          If optAllWords.Checked Then ' and search
            k = 0
            For i = 0 To findWords.Count - 1
              s2 = findWords(i)
              If s2 IsNot Nothing AndAlso InStr(s, s2, CompareMethod.Text) = 0 Then
                k = 1
                Exit For
              End If
            Next i

            If k = 1 And chkFilenames.Checked Then ' check the file name
              k = 0
              For i = 1 To findWords.Count
                s2 = findWords(i)
                If s3 IsNot Nothing AndAlso InStr(s3, s2, CompareMethod.Text) = 0 Then
                  k = 1
                  Exit For
                End If
              Next i
            End If

          Else ' or search
            k = 1
            For i = 1 To findWords.Count
              If InStr(s, findWords(i), CompareMethod.Text) > 0 Then
                k = 0
                Exit For
              End If
              If chkFilenames.Checked AndAlso InStr(s3, findWords(i), CompareMethod.Text) > 0 Then
                k = 0
                Exit For
              End If
            Next i
          End If

          If k = 0 Then ' found it
            Try
              fInfo = New FileInfo(ns(iFile))
            Catch
              fInfo = Nothing
            End Try

            If fInfo IsNot Nothing Then
              x = fInfo.Length
              fDate = fInfo.LastWriteTime

             nFound +=1
              item = New ListViewItem
              item.Text = CStr(ns(iFile))
              item.SubItems.Add(Format(x / 1000, "#,##0 KB"))
              item.SubItems.Add(Format(fDate, "short date") & " " & Format(fDate, "medium time"))
              item.Checked = True
              ListView1.Items.Add(item)
            End If
          End If
        End If

      End If

      milli(2) = Clock.ElapsedMilliseconds
      Clock.Stop()
      Application.DoEvents()
    Next iFile

    'ListView1.Sorting = SortOrder.Ascending
    ListView1.Sort()
    lbFound.Text = nFound & " Matches"

    Me.Cursor = Cursors.Default
    processing = False

  End Sub

  Private Sub ListView1_ColumnClick(ByVal Sender As Object, ByVal e As ColumnClickEventArgs) Handles ListView1.ColumnClick
    Dim ColumnHeader As ColumnHeader = ListView1.Columns(e.Column)

    Dim i As Integer
    Dim v As lvSort

    If loading Or processing Then Exit Sub

    i = ColumnHeader.Index - 1
    v = ListView1.ListViewItemSorter ' this is in frmPicSearch.vb

    If i = v.sortkey Then
      If ListView1.Sorting = SortOrder.Ascending Then
        ListView1.Sorting = SortOrder.Descending
      Else
        ListView1.Sorting = SortOrder.Ascending
      End If
    Else
      ListView1.Sorting = SortOrder.Ascending
      v.sortkey = i
    End If

    ListView1.Sort()

  End Sub

  Private Sub cmdBrowse_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdBrowse.Click
    ' select a folder from a dialog

    Dim result As DialogResult

    If processing Then Exit Sub

    Try
      FolderDialog1.SelectedPath = txSearch.Text
    Catch
    End Try

    Try
      result = FolderDialog1.ShowDialog()
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    If result = DialogResult.OK Then txSearch.Text = FolderDialog1.SelectedPath

  End Sub

  Private Sub getWords(ByVal s As String)
    ' get the indivudual search strings and put them into findwords

    Dim iStart As Integer
    Dim i As Integer
    Dim c As Char
    Dim iQuote As Integer

    findWords = New List(Of String)
    iStart = 0
    iQuote = 0
    s &= ChrW(13)
    For i = 1 To Len(s)
      c = s.Chars(i - 1)

      If c = """" Then ' quote
        If iQuote <= 0 Then
          iQuote = i
        Else ' include words in quotes in a single word
          findWords.Add(LCase(Trim(Mid(s, iQuote + 1, i - iQuote - 1))))
          iStart = i
          iQuote = 0
        End If
      End If

      If c <= " " Then ' new word
        If iStart < i - 1 And iQuote = 0 Then
          findWords.Add(LCase(Trim(Mid(s, iStart + 1, i - iStart - 1))))
        End If
        iStart = i
      End If

    Next i

  End Sub

  Private Sub cmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen.Click

    Dim item As ListViewItem
    Dim mResult As MsgBoxResult

    If ListView1.CheckedItems.Count > 40 Then
      mResult = MsgBox("There are " & ListView1.CheckedItems.Count & " checked. That's too many to open at once.", MsgBoxStyle.OkOnly)
      Exit Sub
    End If

    If ListView1.CheckedItems.Count > 5 Then
      mResult = MsgBox("There are " & ListView1.CheckedItems.Count & " checked. Do you want to open them all?", MsgBoxStyle.YesNoCancel)
      If mResult <> MsgBoxResult.Yes Then Exit Sub
    End If

    For Each item In ListView1.CheckedItems
      OpenDoc(item.Text)
      Application.DoEvents()
      If abort Then
        abort = False
        Exit Sub
      End If
    Next item

    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Sub globalkey(ByVal e As KeyEventArgs)

    Dim item As ListViewItem

    e.Handled = False

    Select Case e.KeyCode
      Case Keys.A
        If e.Control Then ' select all
          For Each item In ListView1.Items
            item.Selected = True
          Next item
          e.Handled = True
        End If
      Case Keys.T
        If e.Control Then ' select all
          For Each item In ListView1.SelectedItems
            item.Checked = True
          Next item
          e.Handled = True
        End If
    End Select

  End Sub

  Private Sub ListView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) _
    Handles ListView1.KeyDown, Me.KeyDown
    globalkey(e)
  End Sub

End Class