Imports Microsoft.Win32
Imports System
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Threading
Imports System.Threading.Tasks
Imports System.IO
Imports System.Collections.Generic
Imports System.Math
Imports ImageMagick

Public Class frmExploref
  Inherits Form

  ' these are for access to the imagelist and listview from the background thread.
  Delegate Sub imagelistAddCallback(ByRef img As Bitmap, ByVal imageKey As String)
  Delegate Sub tooltipAddCallback(ByVal fname As String, ByVal Key As String, ByVal tip As String)
  Delegate Sub itemDrawCallback(ByVal imageKey As String)
  Delegate Sub itemAssignCallback(ByVal fName As String, ByVal sDate As String)
  Delegate Function getListviewTopCallback() As Integer
  Dim thumbPause As New ManualResetEvent(True)
  Dim photoDatesPause As New ManualResetEvent(True)

  'Public picName As String ' current pic file name only
  'Public picpath As String ' current pic file path
  Public iPic As Integer ' current pic in listview1

  Dim txFolderChanged As Boolean
  Dim pathList As New List(Of String)
  Dim pathPos As Integer ' current location in pathlist
  Dim pathTop As Integer ' top location in pathlist - limit of alt-right
  Dim pathPrevious As Boolean
  Dim pathNext As Boolean

  Dim locTagPaths As Integer

  Public Loading As Boolean = True
  Dim processing As Boolean = True
  Dim labelEditing As String
  Dim itemDoubleClicked As Boolean

  Dim currentTool As String

  Public pComments As List(Of PropertyItem)

  Dim loopFrom, loopTo As Integer ' filling listview thumbnails in the background
  Dim thumbLimit As Integer = 3000
  Dim lastTop As Integer
  Dim iconStatus As New List(Of Integer)

  Dim contextMenuNode As TreeNode

  Dim imgLviewState As New ImageList
  Dim WithEvents timerListView As New System.Windows.Forms.Timer
  Dim WithEvents timerTreeView As New System.Windows.Forms.Timer

  Public WithEvents rview As New pViewer

  Dim gImage As Bitmap

  Dim dResult As DialogResult

  Private Sub mnuFileNew_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuFileNew.Click

    Dim i As Integer

    i = mViews.Count
    Using frm As New frmNew
      dResult = frm.ShowDialog()
    End Using
    If mViews.Count > i And dResult <> DialogResult.Cancel Then changeForm(Me, frmMain)
  End Sub

  Private Sub ListView1_AfterLabelEdit(ByVal sender As Object, ByVal e As LabelEditEventArgs) Handles ListView1.AfterLabelEdit

    Dim item As ListViewItem
    Dim fPath As String

    item = ListView1.Items(e.Item) ' item edited
    fPath = Path.GetDirectoryName(item.Tag)

    dirWatch.EnableRaisingEvents = False
    Try
      My.Computer.FileSystem.RenameFile(item.Tag, e.Label)
      item.Tag = fPath & "\" & e.Label ' replace the full path in .tag
    Catch ex As Exception
      e.CancelEdit = True
    End Try
    If dirWatch IsNot Nothing AndAlso Not dirWatch.Path.StartsWith("\\") Then dirWatch.EnableRaisingEvents = True
    labelEditing = ""

  End Sub

  Private Sub ListView1_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles ListView1.MouseDoubleClick
    openSelected()
  End Sub

  Private Sub rview1_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles rview.DoubleClick
    If (rview.Bitmap IsNot Nothing) Then
      openSelected()
    End If
  End Sub

  Private Sub mnxListView_Opening(sender As Object, e As ComponentModel.CancelEventArgs) Handles mnxListView.Opening

    Dim fName As String
    Dim xx() As Double

    mnxFMap.Enabled = False
    If ListView1.SelectedItems.Count = 1 Then
      fName = ListView1.SelectedItems(0).Tag
      pComments = readPropertyItems(fName)
      xx = getBmpComment(propID.GpsLatitude, pComments)
      If xx IsNot Nothing Then mnxFMap.Enabled = True
    End If

  End Sub

  Private Sub mnxFProperties_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnxFProperties.Click
    mnuToolsInfo_Click(Sender, e)
  End Sub

  Private Sub mnuEditPasteNew_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuEditPasteNew.Click
    EditPasteNew()
  End Sub

  Private Sub mnxFMap_Click(sender As Object, e As EventArgs) Handles mnxFMap.Click

    Dim Location As String = ""
    Dim Altitude As String = ""
    Dim xLat, xLon As Double
    Dim iAltitude As Integer
    Dim url As String

    getGPSLocation(pComments, Location, Altitude, Xlat, Xlon, ialtitude)

    If Location <> "" Then
      Location = Location.Replace("°", " ")
      Location = Location.Replace("'", "")
      'If Location.Contains("""") Then Location = Location.Replace("'", " ") Else s = Location.Replace("'", "")
      'Location = Location.Replace("""", "")

      url = "https://www.google.com/maps/place/" & Location & "/@" &
                Format(xLat, "0.0#####") & "," & Format(xLon, "0.0#####") & ",14z/" &
                "data=!4m2!3m1!1s0x0:0x0!5m1!1e4"

      Try
        System.Diagnostics.Process.Start(url) ' show the url in a browser
      Catch ex As Exception
        MsgBox("Couldn't launch the browser." & crlf & ex.Message)
      End Try

    End If

  End Sub

  Private Sub mnuEditTagPrevious_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuEditTagPrevious.Click

    Dim item As ListViewItem

    processing = True

    For Each item In ListView1.Items
      If item.Checked Then item.Checked = False
      item.Checked = False
    Next item

    tagPath = oldTagPath
    locTagPaths = 0
    setTags() ' tag or select, no tags in thumbnail view

    processing = False

  End Sub

  Private Sub mnuFileAcquire_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuFileAcquire.Click

    Dim dResult As DialogResult

    Me.Cursor = Cursors.WaitCursor
    dResult = Acquire()
    Me.Cursor = Cursors.Default
    If mViews.Count > 0 And dResult <> DialogResult.Cancel Then changeForm(Me, frmMain)


  End Sub

  Private Sub mnuHelpTips_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuHelpTips.Click
    Using frm As New frmTips
      dResult = frm.ShowDialog()
    End Using
  End Sub

  Private Sub mnuTools_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuTools.DropDownClosed

    mnuToolsWallpaper.Enabled = True
    mnuToolsComment.Enabled = True
    mnuToolsBugPhotos.Enabled = True
    mnuToolsLinkBugPhotos.Enabled = True
    mnuToolsInfo.Enabled = True
    mnuToolsSlideshow.Enabled = True
    mnuToolsMergeColor.Enabled = True
    mnuToolsWebpage.Enabled = True
    mnuToolsCalendar.Enabled = True

  End Sub

  Private Sub mnuTools_DropDownOpening(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuTools.DropDownOpening

    Dim s As String

    If ListView1.SelectedItems.Count = 0 Then
      mnuToolsWallpaper.Enabled = False
      mnuToolsComment.Enabled = False
      mnuToolsBugPhotos.Enabled = False
      mnuToolsInfo.Enabled = False
    Else
      mnuToolsWallpaper.Enabled = True
      mnuToolsComment.Enabled = True
      mnuToolsBugPhotos.Enabled = True
      mnuToolsInfo.Enabled = True
    End If

    If tagPath.Count <= 0 Then
      mnuToolsLinkBugPhotos.Enabled = False
      mnuToolsSlideshow.Enabled = False
      mnuToolsMergeColor.Enabled = False
      mnuToolsWebpage.Enabled = False
      mnuToolsCalendar.Enabled = False
    Else
      mnuToolsLinkBugPhotos.Enabled = True
      mnuToolsSlideshow.Enabled = True
      mnuToolsMergeColor.Enabled = True
      mnuToolsWebpage.Enabled = True
      mnuToolsCalendar.Enabled = True
    End If

    If (ListView1.SelectedItems.Count = 0) Then
      mnuToolsComment.Enabled = False
    Else
      s = LCase(Path.GetExtension(ListView1.SelectedItems(0).Tag))
      If s = ".jpg" Or s = ".jpeg" Or s = ".tif" Or s = ".tiff" Or s = ".gif" Or s = ".png" Then
        mnuToolsComment.Enabled = True
      Else
        mnuToolsComment.Enabled = False
      End If
    End If

  End Sub

  Private Sub mnuToolsComment_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuToolsComment.Click, mnuToolsBugPhotos.Click

    Dim lastPath As String
    Dim item As ListViewItem

    currentpicPath = ListView1.SelectedItems(0).Tag
    lastPath = currentpicPath
    pComments = readPropertyItems(currentpicPath)
    callingForm = Me
    If bkgThumb.IsBusy Then thumbPause.Reset()
    If bkgPhotoDates.IsBusy Then photoDatesPause.Reset()
    If Sender Is mnuToolsComment Then
      Using frm As New frmComment
        frm.ShowDialog()
      End Using
    ElseIf Sender Is mnuToolsBugPhotos Then
      Using frm As New frmBugPhotos
        frm.ShowDialog()
      End Using
    End If

    If currentpicPath <> lastPath Then
      ListView1.SelectedItems.Clear()
      ' select the last file viewed
      For Each item In ListView1.Items
        If eqstr(item.Tag, currentpicPath) Then
          item.Selected = True
          item.Focused = True
          Exit For
        End If
      Next item
    End If

  End Sub

  Private Sub mnuToolsMergeColor_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuToolsMergeColor.Click

    If tagPath.Count <= 0 Then Exit Sub

    Me.Cursor = Cursors.WaitCursor

    clearBitmap(qImage)
    Using frm As New frmColorMerge
      dResult = frm.ShowDialog()
    End Using
    Me.Cursor = Cursors.Default

    If dResult <> DialogResult.Cancel Then
      If (qImage IsNot Nothing) Then
        rview = newWindow(qImage)
        changeForm(Me, frmMain)
      End If
      cleartags()
    End If

  End Sub

  Private Sub mnuToolsPicSearch_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuToolsPicSearch.Click
    callingForm = Me
    Using frm As New frmDupSearch
      dResult = frm.ShowDialog()
    End Using
    mnuViewRefresh_Click(Sender, e) ' reset in case of delete

  End Sub

  Private Sub mnuToolsSearch_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuToolsSearch.Click

    Using frm As New frmSearch
      dResult = frm.ShowDialog()
    End Using
    If dResult <> DialogResult.Cancel Then
      setTags()
      setStatusLine()
    End If

  End Sub

  Private Sub mnuToolsSlideshow_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuToolsSlideshow.Click
    If tagPath.Count <= 0 Then Exit Sub
    Using frm As New frmSlideShow
      dResult = frm.ShowDialog()
    End Using
    If dResult <> DialogResult.Cancel Then cleartags()
  End Sub

  Private Sub mnuToolsWallpaper_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuToolsWallpaper.Click

    Dim fName As String = ""
    Dim fPath As String = ""
    Dim msg As String = ""

    If (ListView1.SelectedItems.Count = 1) Then

      fName = Path.GetFileName(ListView1.SelectedItems(0).Tag)
      fPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows)
      If Not fPath.EndsWith("\") Then fPath &= "\"
      fName = fPath & fName
      File.Copy(ListView1.SelectedItems(0).Tag, fName)
      msg = SetWallPaper(fName)

      If msg = "" Then
        MsgBox("The wallpaper was created.")
      Else
        MsgBox("Oops!  Couldn't save the wallpaper. Make sure it's a .jpg or .bmp file." & crlf & msg, MsgBoxStyle.OkOnly)
      End If

    End If

  End Sub

  Private Sub mnuViewPrevious_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuViewPrevious.Click

    Dim Success As Boolean

    If pathPos >= 1 Then
      pathPrevious = True
      pathPos = pathPos - 1
      Success = TreeViewSelectPath(TreeView, pathList(pathPos), dirWatch)
    End If

  End Sub

  Private Sub mnuViewNext_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuViewNext.Click

    Dim Success As Boolean

    If pathPos < pathTop Then
      pathNext = True
      pathPos = pathPos + 1
      Success = TreeViewSelectPath(TreeView, pathList(pathPos), dirWatch)
    End If

  End Sub

  Private Sub mnxFComment_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnxFComment.Click
    mnuToolsComment_Click(Sender, e)
  End Sub

  Private Sub mnuFileOpenCurrent_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuFileOpenCurrent.Click
    openSelected()
  End Sub

  Private Sub mnuToolsAssoc_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuToolsAssoc.Click
    Using frm As New frmFileAssoc
      dResult = frm.ShowDialog()
    End Using
  End Sub

  Private Sub mnuToolsOptions_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuToolsOptions.Click

    Using frm As New frmOptions
      dResult = frm.ShowDialog()
    End Using

    If dResult <> DialogResult.Cancel Then
      assignAllToolbars()
      mnuViewRefresh_Click(Sender, e)
    End If

  End Sub

  Private Sub mnuToolsToolbar_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuToolsToolbar.Click

    Dim oldButtonSize As Integer

    oldButtonSize = iniButtonSize
    callingForm = Me

    Using frm As New frmToolbar
      dResult = frm.ShowDialog()
    End Using

    If dResult <> DialogResult.Cancel Then

      If oldButtonSize = iniButtonSize Then ' only update this form
        assignVToolbar()
      Else
        assignAllToolbars()
      End If

      enableButtons()
    End If

  End Sub

  Private Sub ListView1_ItemSelectionChanged(ByVal sender As Object, ByVal e As ListViewItemSelectionChangedEventArgs) Handles ListView1.ItemSelectionChanged
    'Private Sub ListView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListView1.SelectedIndexChanged

    Static busy As Boolean = False
    Dim i As Integer

    If processing Or Loading Then Exit Sub
    If busy Then Exit Sub
    busy = True

    If ListView1.Items.Count >= thumbLimit AndAlso (Not bkgThumb.IsBusy) Then
      i = getListviewTop()
      If i <> lastTop And i >= 0 And i < ListView1.Items.Count Then ' listview moved - reset loop variables
        ThumbnailLoad(iniExplorePath, False) ' it only lost 60 items in this case 
      End If
    End If

    Me.Cursor = Cursors.WaitCursor
    If ListView1.SelectedItems.Count = 1 Then ' 
      currentpicPath = ListView1.SelectedItems(0).Tag
      If ListView1.View <> View.LargeIcon Then
        timerListView.Stop()
        timerListView.Interval = 250
        timerListView.Start()
      End If
      'If ListView1.View <> View.LargeIcon Then showPicture(currentPicPath, rview, False, Nothing)
      'SeparatePath(currentPicPath, picpath, picName)
    Else
      If ListView1.View <> View.LargeIcon Then rview.setBitmap(Nothing)
    End If

    enableButtons()
    setStatusLine()
    Me.Cursor = Cursors.Default
    busy = False

  End Sub

  Private Sub timerListView_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timerListView.Tick

    Dim msg As String = ""

    timerListView.Stop()
    Me.Cursor = Cursors.WaitCursor

    If rview IsNot Nothing AndAlso rview.ClientSize.Height > 0 Then
      Using bmp As Bitmap = readBitmap(currentpicPath, msg)
        rview.setBitmap(bmp)
      End Using
      rview.Zoom(0) ' fit to window
    End If

    Me.Cursor = Cursors.Default
  End Sub

  Private Sub ListView1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles ListView1.KeyDown

    Select Case e.KeyCode

      Case 13 ' Enter
        openSelected()

      Case 46 ' delete
        If labelEditing = "" Then mnuEditDelete_Click(sender, e)

      Case Else
        globalKeydown(e)

    End Select

  End Sub

  Private Sub TreeView_BeforeLabelEdit(ByVal sender As Object, ByVal e As NodeLabelEditEventArgs) Handles TreeView.BeforeLabelEdit
    labelEditing = "treeview"
  End Sub

  Private Sub frmExplore_Activated(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Activated
    If Loading Then Loading = False ' just in case
    setMnuWindows()
    thumbPause.Set() ' resume thumbnails, if paused
    photoDatesPause.Set() ' resume thumbnails, if paused
  End Sub

  Sub setMnuWindows()

    Dim mv As mudViewer
    Dim q As ToolStripMenuItem
    Dim i As Integer

    mnuWindow.DropDownItems.Clear()

    i = 0
    If mViews.Count <= 0 Then
      mnuWindow.Enabled = False
    Else
      mnuWindow.Enabled = True
      For Each mv In mViews
        q = New ToolStripMenuItem
        i = i + 1
        q.Text = "&" & i & " " & mv.Caption
        RemoveHandler q.Click, AddressOf mdiWindow_click
        AddHandler q.Click, AddressOf mdiWindow_click
        mnuWindow.DropDownItems.Add(q)
      Next mv
    End If

  End Sub

  Private Sub mdiWindow_click(ByVal sender As Object, ByVal e As EventArgs)
    ' handles the mnuWindow items
    Dim q As ToolStripMenuItem
    Dim s As String
    Dim i As Integer
    Dim mv As mudViewer
    q = sender

    s = q.Text
    i = InStr(s, " ")
    If i > 0 Then s = Mid(s, i + 1)

    For Each mv In mViews
      If s = mv.Caption Then
        mv.Activate(sender, e)
        Exit For
      End If
    Next mv

  End Sub

  Private Sub frmExplore_KeyDown(ByVal Sender As Object, ByVal e As KeyEventArgs) _
    Handles Me.KeyDown, rview.KeyDown, TreeView.KeyDown
    globalKeydown(e)
  End Sub

  Private Sub globalKeydown(ByVal e As KeyEventArgs)

    Dim Item As ListViewItem

    If labelEditing <> "" Then Exit Sub

    Select Case e.KeyCode

      Case Keys.Space ' space
        If ListView1.FocusedItem IsNot Nothing And ListView1.View = View.LargeIcon Then
          ListView1.FocusedItem.Checked = Not ListView1.FocusedItem.Checked
          'ListView1_ItemChecked(Nothing, Nothing)
          e.Handled = True
        End If

      Case Keys.Escape ' esc
        rview.setBitmap(Nothing)

        processing = True
        For Each Item In ListView1.SelectedItems
          Item.Selected = False
        Next Item
        For Each Item In ListView1.Items ' checked items
          If Item.Checked Then Item.Checked = False
        Next Item
        processing = False

        If tagPath.Count > 0 Then
          oldTagPath = tagPath
          locTagPaths = 0
          tagPath = New List(Of String)
          setStatusLine()
        End If
        enableButtons()
        abort = True
        e.Handled = True

      Case Keys.Left ' left arrow
        If e.Alt Then
          mnuViewPrevious_Click(mnuViewPrevious, e)
          e.Handled = True
        End If

      Case Keys.Up ' up arrow
        If e.Alt Then
          mnuUpOneLevel_Click(mnuUpOneLevel, e)
          e.Handled = True
        End If

      Case Keys.Right ' right arrow
        If e.Alt Then
          mnuViewNext_Click(mnuViewNext, e)
          e.Handled = True
        End If

      Case Keys.W
        If e.Control Then
          mnuToolsMergeColor_Click(mnuToolsMergeColor, e)
          e.Handled = True
        End If

      Case Keys.D
        If e.Alt Then
          txFolder.Select()
          e.Handled = True
        End If

      Case Keys.F6
        If Not e.Shift And Not e.Control Then
          mnufileedit_Click(mnuToolsMergeColor, e)
          e.Handled = True
        End If

    End Select

  End Sub

  Private Sub frmExplore_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing

    processing = True

    'frmMain.Close()
    'If abortClosing Then
    '  abortClosing = False
    '  e.Cancel = True
    '  processing = False
    '  Exit Sub
    '  End If

    If bkgThumb.IsBusy Then
      ' stop thumbnail loading if necessary
      bkgThumb.CancelAsync()
      Do While bkgThumb.IsBusy
        Thread.Sleep(20)
        Application.DoEvents()
      Loop
    End If

    If bkgPhotoDates.IsBusy Then
      ' stop thumbnail loading if necessary
      bkgPhotoDates.CancelAsync()
      Do While bkgPhotoDates.IsBusy
        Thread.Sleep(20)
        Application.DoEvents()
      Loop
    End If

    clearBitmap(gImage)
    If thumbPause IsNot Nothing Then thumbPause.Dispose()
    If photoDatesPause IsNot Nothing Then photoDatesPause.Dispose()
    processing = False

  End Sub

  Private Sub frmExplore_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Dim i As Integer
    Dim s As String

    Me.Text = AppName & " Explorer"
    Me.Cursor = Cursors.WaitCursor

    bkgThumb.WorkerSupportsCancellation = True
    bkgPhotoDates.WorkerSupportsCancellation = True

    rview.SizeMode = PictureBoxSizeMode.Zoom
    rview.Dock = DockStyle.Fill
    rview.Location = New Point(0, 0)
    rview.TabStop = False
    rview.BackColor = SystemColors.ControlDarkDark

    SplitContainer2.Panel1.Controls.Add(rview)

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, "explorerandeditor.html")

    Select Case iniButtonSize
      Case 0
        Toolstrip1.ImageScalingSize = New Size(24, 24)
      Case Else ' 1
        Toolstrip1.ImageScalingSize = New Size(32, 32)
    End Select

    Loading = True

    If bugDBEnabled Then
      mnuToolsBugseparator.Visible = True
      mnuToolsBugPhotos.Visible = True
      mnuToolsLinkBugPhotos.Visible = True
    Else
      mnuToolsBugseparator.Visible = False
      mnuToolsBugPhotos.Visible = False
      mnuToolsLinkBugPhotos.Visible = False
    End If

    mnu.Visible = True
    Me.Cursor = Cursors.WaitCursor

    ' ListView1.ListViewItemSorter = New lvSort(0)

    If iniFileType.Count = 0 Then
      iniFileType.Add(".jpg")
      iniFileType.Add(".jpeg")
    End If

    mnuHelpRegister.Text = "&Register " & AppName & " Online"
    mnuHelpAbout.Text = "&About " & AppName

    s = "*." & iniFileType(0)
    For i = 1 To iniFileType.Count - 1
      s = s & ";*." & iniFileType(i)
    Next i

    ListView1.ShowItemToolTips = True
    ' ListView1.View = View.Details
    ListView1.Columns.Add("Name ", CInt(ListView1.Width * 0.3), HorizontalAlignment.Left)
    ListView1.Columns.Add("Size", CInt(ListView1.Width * 0.1), HorizontalAlignment.Right)
    ListView1.Columns.Add("Date Modified", CInt(ListView1.Width * 0.15), HorizontalAlignment.Left)
    ListView1.Columns.Add("Date Picture Taken", CInt(ListView1.Width * 0.15), HorizontalAlignment.Left)
    ListView1.StateImageList = imgLviewState

    Toolstrip1.Visible = iniViewToolbar
    assignVToolbar()
    setViewStyle()
    Loading = False
    processing = False

    If iniExplorePath.EndsWith(":") Then iniExplorePath &= "\"

    Try
      If Not Directory.Exists(iniExplorePath) Then
        iniExplorePath = My.Computer.FileSystem.SpecialDirectories.MyPictures
        If iniExplorePath = "" Then iniExplorePath = My.Computer.FileSystem.SpecialDirectories.MyDocuments
      End If
      If Not Directory.Exists(iniExplorePath) Then iniExplorePath = "c:\"
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    TreeViewInit(TreeView, iniExplorePath, dirWatch) ' initialize treeview with drives, etc.
    txFolder.Text = iniExplorePath
    txFolderChanged = False

    pathList = New List(Of String)
    pathList.Add(TreeView.SelectedNode.Tag)
    pathPos = 0
    pathTop = pathPos
    pathPrevious = False
    pathNext = False

    'If (ListView1.SelectedItems.Count = 0) And (ListView1.Items.Count >= 1) Then
    '  ListView1.Items(0).Selected = True
    '  ListView1.Items(0).EnsureVisible()
    '  ListView1.Items(0).Focused = True
    '  currentpicPath = ListView1.Items(0).Tag
    '  End If

    dirWatch.Filter = "*.*"
    Try
      dirWatch.Path = TreeView.SelectedNode.Tag
      If dirWatch.Path.StartsWith("\\") Then
        dirWatch.EnableRaisingEvents = False
      Else
        dirWatch.NotifyFilter = NotifyFilters.DirectoryName Or NotifyFilters.FileName
        dirWatch.IncludeSubdirectories = False
        dirWatch.EnableRaisingEvents = True
      End If
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    Me.Cursor = Cursors.Default

  End Sub

  Private Sub mnuEditSelecttagged_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuEditSelecttagged.Click

    Dim Item As ListViewItem

    processing = True

    For Each Item In ListView1.SelectedItems
      Item.Selected = False
    Next Item

    For Each Item In ListView1.Items ' CheckedItems
      If Item.Checked Then Item.Selected = True
    Next Item

    processing = False

    setStatusLine()
    enableButtons()

  End Sub

  Private Sub mnxFCopyFile_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnxFCopyFile.Click
    mnuEditCopyFile_Click(Sender, e)
  End Sub

  Private Sub mnxFCopyImage_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnxFCopyImage.Click
    mnuEditCopyImage_Click(Sender, e)
  End Sub

  Private Sub mnxFCopyFilename_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnxFCopyFilename.Click
    mnuEditCopyfilename_Click(Sender, e)
  End Sub

  Private Sub mnxFPasteFile_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnxFPasteFile.Click
    mnuEditPaste_Click(Sender, e)
  End Sub

  Private Sub mnxFDelete_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnxFDelete.Click
    mnuEditDelete_Click(Sender, e)
  End Sub

  Private Sub mnxFOpen_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnxFOpen.Click
    openSelected()
  End Sub

  Private Sub mnxFPrint_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnxFPrint.Click
    mnuFilePrint_Click(Sender, e)
  End Sub

  Private Sub mnxFRename_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnxFRename.Click
    mnuEditRename_Click(Sender, e)
  End Sub

  Private Sub mnxFSaveas_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnxFSaveAs.Click
    mnuFileSaveAs_Click(Sender, e)
  End Sub

  Private Sub mnxFemail_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnxFemail.Click
    mnuFileSend_Click(Sender, e)
  End Sub

  Private Sub mnuEditCleartags_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuEditCleartags.Click
    cleartags()
  End Sub

  Sub cleartags()
    Dim item As ListViewItem

    oldTagPath = tagPath
    processing = True
    For Each item In ListView1.Items ' CheckedItems
      If item.Checked Then item.Checked = False
    Next item
    processing = False
    locTagPaths = 0
    tagPath = New List(Of String)
    enableButtons()
    setStatusLine()

  End Sub

  Private Sub mnuEditCopyfilename_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuEditCopyfilename.Click

    Me.Cursor = Cursors.WaitCursor
    If ListView1.SelectedItems.Count = 1 Then
      My.Computer.Clipboard.Clear()
      My.Computer.Clipboard.SetText(ListView1.SelectedItems(0).Tag, TextDataFormat.Text)
    End If
    Me.Cursor = Cursors.Default

  End Sub

  Private Sub mnuEditCopyImage_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuEditCopyImage.Click

    Dim s As String
    Dim msg As String = ""

    Me.Cursor = Cursors.WaitCursor

    ' if editing text, copy the text instead of the image
    s = ""
    If labelEditing = "listview1" Then
      s = ListView1.FocusedItem.Text
    ElseIf labelEditing = "treeview" Then
      s = TreeView.SelectedNode.Text
    ElseIf txFolder.Focused Then
      s = txFolder.SelectedText
      If s = "" Then s = txFolder.Text
    End If
    If Len(s) > 0 Then
      Clipboard.Clear()
      Clipboard.SetText(s, TextDataFormat.Text)
      Me.Cursor = Cursors.Default
      Exit Sub
    End If

    If rview.Bitmap Is Nothing Then ' thumbnail view probably
      ' load the image from disk and copy to the clipboard
      If ListView1.SelectedItems.Count = 1 Then
        gImage = readBitmap(ListView1.SelectedItems(0).Tag, msg)

        If gImage IsNot Nothing Then ' copy to clipboard
          Clipboard.SetImage(gImage)
          clearBitmap(gImage)
        End If
      End If
    Else
      Clipboard.SetImage(rview.Bitmap)
    End If

    Me.Cursor = Cursors.Default

  End Sub

  Private Sub mnuEditCopyFile_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuEditCopyFile.Click

    Dim ss As New System.Collections.Specialized.StringCollection
    Dim s As String
    Dim item As ListViewItem

    Me.Cursor = Cursors.WaitCursor

    If labelEditing = "listview1" Then
      s = ListView1.FocusedItem.Text
      Clipboard.Clear()
      Clipboard.SetText(s, TextDataFormat.Text)
    ElseIf labelEditing = "treeview" Then
      s = TreeView.SelectedNode.Text
      Clipboard.Clear()
      Clipboard.SetText(s, TextDataFormat.Text)
    ElseIf txFolder.Focused Then
      Clipboard.Clear()
      Clipboard.SetText(txFolder.Text, TextDataFormat.Text)
    ElseIf ListView1.Focused Then
      For Each item In ListView1.SelectedItems
        ss.Add(item.Tag)
      Next item

      If ss.Count > 0 Then
        My.Computer.Clipboard.Clear()
        My.Computer.Clipboard.SetFileDropList(ss)
      End If
    End If

    Me.Cursor = Cursors.Default

  End Sub

  Private Sub mnuEditPaste_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuEditPaste.Click

    Dim ss As New System.Collections.Specialized.StringCollection
    Dim fName As String
    Dim destDir As String
    Dim destPath As String
    Dim mResult As MsgBoxResult

    If txFolder.Focused AndAlso My.Computer.Clipboard.ContainsText() Then
      ' paste text into the address line
      txFolder.Text = My.Computer.Clipboard.GetText
      Exit Sub
    End If

    Me.Cursor = Cursors.WaitCursor
    ss = My.Computer.Clipboard.GetFileDropList()

    destDir = TreeView.SelectedNode.Tag
    If destDir.EndsWith("\") And (Len(destDir) > 3 Or destDir.Chars(1) <> ":") Then destDir = destDir.Remove(Len(destDir) - 1, 1)
    If Not Directory.Exists(destDir) Then
      Try
        Directory.CreateDirectory(destDir)
      Catch ex As Exception
        MsgBox(ex.Message)
        Me.Cursor = Cursors.Default
        Exit Sub
      End Try
    End If

    If destDir.EndsWith("\") And (Len(destDir) > 3 Or destDir.Chars(1) <> ":") Then destDir = destDir.Remove(Len(destDir) - 1, 1)
    For Each fName In ss
      If Not destDir.EndsWith("\") Then
        destPath = destDir & "\" & Path.GetFileName(fName)
      Else
        destPath = destDir & Path.GetFileName(fName) ' c:\
      End If
      If File.Exists(destPath) Then
        mResult = MsgBox(destPath & " exists. Do you want to overwrite it?", MsgBoxStyle.YesNoCancel)
        If mResult = MsgBoxResult.Cancel Then Exit For
        If mResult = MsgBoxResult.Yes Then
          Try
            File.Replace(fName, destPath, UndoPath & "~" & Path.GetFileName(fName) & ".~tmp")
          Catch ex As System.IO.IOException
            mResult = MsgBox(fName & " could not be copied.", MsgBoxStyle.OkCancel)
            If mResult = MsgBoxResult.Cancel Then Exit For
          End Try
        End If

      Else ' file does not exist -- go ahead and copy
        Try
          File.Copy(fName, destPath)
        Catch ex As System.IO.IOException
          mResult = MsgBox(fName & " could not be copied.", MsgBoxStyle.OkCancel)
          If mResult = MsgBoxResult.Cancel Then Exit For
        End Try
      End If
    Next fName

    mnuViewRefresh_Click(Sender, e)
    Me.Cursor = Cursors.Default

  End Sub

  Private Sub mnuEdit_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEdit.DropDownClosed
    ' enable shortcut keys
    mnuEditPasteNew.Enabled = True
    mnuEditCopyfilename.Enabled = True
    mnuEditCopyImage.Enabled = True
    mnuEditRotateLeft.Enabled = True
    mnuEditRotateRight.Enabled = True
    mnuEditRename.Enabled = True
    mnuEditCopyFile.Enabled = True
    mnuEditDelete.Enabled = True
    mnuEditTagselectedfiles.Enabled = True
    mnuEditSelecttagged.Enabled = True
    mnuEditCleartags.Enabled = True
    mnuEditCleartags.Visible = True
    mnuEditTagselectedfiles.Visible = True
    mnuEditSelecttagged.Visible = True
    mnuEditTagPrevious.Visible = True
    mnuEditLine1.Visible = True
    mnuEditTagPrevious.Enabled = True

  End Sub

  Private Sub mnuEdit_DropDownOpening(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuEdit.DropDownOpening

    If Clipboard.ContainsImage() Then
      mnuEditPasteNew.Enabled = True
    Else
      mnuEditPasteNew.Enabled = False
    End If

    If ListView1.SelectedItems.Count <> 1 Then
      mnuEditCopyfilename.Enabled = False
      mnuEditCopyImage.Enabled = False
      mnuEditRotateLeft.Enabled = False
      mnuEditRotateRight.Enabled = False
      mnuEditRename.Enabled = False
    Else
      mnuEditCopyfilename.Enabled = True
      mnuEditCopyImage.Enabled = True
      mnuEditRotateLeft.Enabled = True
      mnuEditRotateRight.Enabled = True
      mnuEditRename.Enabled = True
    End If

    If ListView1.SelectedItems.Count <= 0 Then
      mnuEditCopyFile.Enabled = False
      mnuEditDelete.Enabled = False
      mnuEditTagselectedfiles.Enabled = False
    Else
      mnuEditCopyFile.Enabled = True
      mnuEditDelete.Enabled = True
      mnuEditTagselectedfiles.Enabled = True
    End If

    If tagPath.Count = 0 Then
      mnuEditSelecttagged.Enabled = False
      mnuEditCleartags.Enabled = False
    Else
      mnuEditSelecttagged.Enabled = True
      mnuEditCleartags.Enabled = True
    End If

    mnuEditCleartags.Visible = True
    mnuEditTagselectedfiles.Visible = True
    mnuEditSelecttagged.Visible = True
    mnuEditTagPrevious.Visible = True
    mnuEditLine1.Visible = True

    If oldTagPath.Count > 0 Then
      mnuEditTagPrevious.Enabled = True
    Else
      mnuEditTagPrevious.Enabled = False
    End If

  End Sub

  Private Sub mnuEditDelete_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuEditDelete.Click

    Dim mResult As MsgBoxResult
    Dim item As ListViewItem

    If ListView1.SelectedItems.Count = 1 Then
      mResult = MsgBox("Do you really want to delete """ & ListView1.SelectedItems(0).Tag & """?", MsgBoxStyle.YesNoCancel)
    ElseIf ListView1.SelectedItems.Count > 1 Then
      mResult = MsgBox("Do you really want to delete these " & ListView1.SelectedItems.Count & " files?", MsgBoxStyle.YesNoCancel)
    Else
      Exit Sub
    End If

    If mResult = MsgBoxResult.Yes Then
      If dirWatch IsNot Nothing Then dirWatch.EnableRaisingEvents = False
      ' delete the files and remove from listview
      For Each item In ListView1.SelectedItems
        Try
          File.Delete(item.Tag)
          If useQuery Then
            bugDelete(Path.GetFileName(item.Tag)) ' delete from database
            queryNames.Remove(item.Tag)
          End If

          If item.Checked Then unTag(item.Tag)
          item.Remove()
          If ListView1.FocusedItem IsNot Nothing Then ListView1.FocusedItem.Selected = True
          If iniDelRawFiles Then delMatchingRawFile(item.Tag)
        Catch ex As Exception
          MsgBox(ex.Message)
        End Try
      Next item
      If dirWatch IsNot Nothing AndAlso Not dirWatch.Path.StartsWith("\\") Then dirWatch.EnableRaisingEvents = True
    End If

  End Sub

  Private Sub mnuEditRename_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuEditRename.Click

    If ListView1.Focused Then
      If ListView1.SelectedItems.Count > 0 Then
        ListView1.SelectedItems(0).BeginEdit()
      End If
    ElseIf TreeView.Focused Then
      If TreeView.SelectedNode IsNot Nothing Then
        TreeView.SelectedNode.BeginEdit()
      End If
    End If

  End Sub

  Private Sub mnuEditSelectall_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuEditSelectall.Click

    Dim item As ListViewItem

    Me.Cursor = Cursors.WaitCursor

    processing = True
    For Each item In ListView1.Items
      item.Selected = True
    Next item
    processing = False

    setStatusLine()
    rview.setBitmap(Nothing)
    Me.Cursor = Cursors.Default
    enableButtons()

  End Sub

  Private Sub mnuEditTagselectedfiles_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuEditTagselectedfiles.Click

    Dim Item As ListViewItem

    processing = True

    For Each Item In ListView1.SelectedItems
      If Not (Item.Checked) Then
        Item.Checked = True
        locTagPaths = locTagPaths + 1
        tagPath.Add(Item.Tag)
      End If
    Next Item

    processing = False
    setStatusLine()
    enableButtons()

  End Sub

  Private Sub mnuFile_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFile.DropDownClosed
    ' enable shortcuts
    mnuFileOpenCurrent.Enabled = True
    mnuFilePrint.Enabled = True
    mnuFileSaveAs.Enabled = True
    mnuFileSend.Enabled = True
    mnuFileCopy.Enabled = True
    mnuFilePrintTagged.Enabled = True
    mnuFileSendTagged.Enabled = True
    mnuFileConvert.Enabled = True
    mnuFilePrint.Enabled = True
    mnuFileSend.Enabled = True
    mnuFileConvert.Enabled = True

  End Sub

  Private Sub mnuFile_DropDownOpening(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuFile.DropDownOpening

    loadMru(mnuFile)

    If ListView1.SelectedItems.Count = 0 Then
      mnuFileOpenCurrent.Enabled = False
    Else
      mnuFileOpenCurrent.Enabled = True
    End If

    If ListView1.SelectedItems.Count <> 1 Then
      mnuFilePrint.Enabled = False
      mnuFileSaveAs.Enabled = False
      mnuFileSend.Enabled = False
      mnuFileCopy.Enabled = False
    Else
      mnuFilePrint.Enabled = True
      mnuFileSaveAs.Enabled = True
      mnuFileSend.Enabled = True
      mnuFileCopy.Enabled = True
    End If

    If ListView1.View <> View.LargeIcon Then ' use tags unless thumbnail view, then use selection
      mnuFilePrint.Text = "&Print..."
      mnuFileSend.Text = "E&mail Photo..."

    Else ' thumbnail view
      If ListView1.SelectedItems.Count = 1 Then
        mnuFilePrint.Text = "&Print Selected Photo..."
        mnuFileSend.Text = "E&mail Selected Photo..."
      End If

    End If

    If tagPath.Count > 0 Then
      mnuFilePrintTagged.Enabled = True
      mnuFileSendTagged.Enabled = True
      mnuFileConvert.Enabled = True
    Else
      mnuFilePrintTagged.Enabled = False
      mnuFileSendTagged.Enabled = False
      mnuFileConvert.Enabled = False
    End If

    If mViews.Count > 0 Then
      mnuFileCloseAll.Visible = True
    Else
      mnuFileCloseAll.Visible = False
    End If

    If ListView1.SelectedItems.Count = 1 Or mViews.Count >= 1 Then
      mnufileedit.Enabled = True
    Else
      mnufileedit.Enabled = False
    End If

  End Sub

  Private Sub mnuFileCopy_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuFileCopy.Click

    Dim i As Integer
    Dim targetFile As String
    Dim sourceFile As String
    Dim sFilter As String
    Dim ext As String

    If ListView1.SelectedItems.Count <> 1 Then Exit Sub

    sourceFile = ListView1.SelectedItems(0).Tag
    ext = Path.GetExtension(sourceFile)

    sFilter = "All Files|*.*"  ' if no extension on current file
    For i = 0 To fmtCommon.Count - 1
      sFilter = sFilter & "|" & fmtCommon(i).Description & "|*" & fmtCommon(i).Ext
      If (ext = ".jpg" Or ext = ".jpeg") And fmtCommon(i).ID = ".jpg" Or _
         (ext = ".tif" Or ext = ".tiff") And fmtCommon(i).ID = ".tif" Then
        saveDialog1.FilterIndex = i + 2
      End If
    Next i

    saveDialog1.AddExtension = True
    saveDialog1.FileName = Path.GetFileName(sourceFile)
    saveDialog1.Filter = sFilter
    saveDialog1.DefaultExt = ext
    saveDialog1.InitialDirectory = iniSavePath
    saveDialog1.CheckPathExists = True
    saveDialog1.OverwritePrompt = True

    Try
      dResult = saveDialog1.ShowDialog()
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    If dResult = DialogResult.OK Then
      targetFile = saveDialog1.FileName

      If targetFile <> "" Then
        FileCopy(sourceFile, targetFile)
        iniSavePath = Path.GetDirectoryName(targetFile)
      End If
    End If

  End Sub

  Private Sub mnufileedit_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnufileedit.Click

    ' nothing open -- edit current file, if there is one
    If mViews.Count <= 0 Then
      If ListView1.SelectedItems.Count = 1 Then openSelected()
    Else
      changeForm(Me, frmMain)
    End If

  End Sub

  Private Sub mnuFileExit_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuFileExit.Click
    abortClosing = False
    frmMain.Close()
  End Sub

  Private Sub mnuViewStyleThumbnails_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuViewStyleThumbnails.Click

    mnuViewStyleThumbnails.Checked = True ' thumbnails
    mnuViewStyleDetails.Checked = False ' details
    iniViewStyle = 0
    setViewStyle()

  End Sub

  Private Sub mnuViewStyleDetails_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuViewStyleDetails.Click
    mnuViewStyleThumbnails.Checked = False ' thumbnails
    mnuViewStyleDetails.Checked = True ' details
    iniViewStyle = 1
    setViewStyle()

  End Sub

  Sub setViewStyle()

    Dim item As ListViewItem = Nothing
    Dim s As String
    Dim selectedPath As String
    Dim Ascend As String = "  " & ChrW(&H25B2)
    Dim Descend As String = "  " & ChrW(&H25BC)
    Dim col As ColumnHeader

    rview.setBitmap(Nothing)

    If iniFolderWidth <= 0 Or iniFolderWidth >= 1 Then iniFolderWidth = 0.2
    SplitContainer1.SplitterDistance = iniFolderWidth * SplitContainer1.Width
    If iniListTop < 0 Or iniListTop >= 1 Then iniListTop = 0.75

    If ListView1.SelectedItems.Count = 1 Then
      selectedPath = ListView1.SelectedItems(0).Tag
    Else
      selectedPath = ""
    End If

    ListView1.Items.Clear()

    Select Case iniViewStyle
      Case 0 ' thumbnails
        ListView1.View = View.LargeIcon ' thumbnails
        SplitContainer2.SplitterDistance = 0 ' no big picture above thumbnails
        SetControlColor(ListView1, SystemColors.ControlDark, SystemColors.InactiveCaptionText)
        'ListView1.BackColor = Color.FromArgb(255, 230, 230, 230)
        'ListView1.ForeColor = Color.FromArgb(255, 90, 90, 90)
        ListView1.Font = New Font("arial", 7)
        ListView1.CheckBoxes = True
        imgLviewState.ImageSize = New Size(17, 17) ' checkboxes disappear without this. MS bug? (was 21)

      Case 1 ' details
        ListView1.View = View.Details
        SplitContainer2.SplitterDistance = iniListTop * SplitContainer2.Height
        ListView1.ForeColor = New System.Drawing.Color()
        ListView1.BackColor = New System.Drawing.Color()
        ' Loading = True
        ListView1.Font = New Font("arial", 9)
        ListView1.CheckBoxes = True
        For Each col In ListView1.Columns
          ' get rid of sort arrow, if any
          s = col.Text
          s = Replace(s, Ascend, "")
          s = Replace(s, Descend, "")
          col.Text = s
        Next col
        ListView1.Columns(0).Text = ListView1.Columns(0).Text & Ascend ' ascending sort, filename

        If ListView1.Columns.Count >= 3 Then
          ListView1.Columns(0).Width = ListView1.Width * 0.3
          ListView1.Columns(1).Width = ListView1.Width * 0.12
          ListView1.Columns(2).Width = ListView1.Width * 0.17
          ListView1.Columns(3).Width = ListView1.Width * 0.17
        End If

      Case 2 ' list
        ListView1.View = View.List
        SplitContainer2.SplitterDistance = iniListTop * SplitContainer2.Height
        ListView1.ForeColor = New System.Drawing.Color
        ListView1.BackColor = New System.Drawing.Color
        ListView1.Font = New Font("arial", 9)
        ListView1.CheckBoxes = True
    End Select

    ' ascending sort, filename
    ListView1.ListViewItemSorter = New lvSort(0)
    If Not Loading Then ListViewLoad(iniExplorePath, selectedPath)

    'If item IsNot Nothing Then  ' put the cursor back where it was
    '  item.EnsureVisible()
    '  item.Selected = True
    '  item.Focused = True
    '  End If

  End Sub

  Private Sub mnuFileOpen_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuFileOpen.Click
    openPicFile()
  End Sub

  Private Sub mnuFileSaveAs_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuFileSaveAs.Click

    If ListView1.SelectedItems.Count <> 1 Then Exit Sub

    clearBitmap(qImage)
    'If rview.Bitmap IsNot Nothing Then
    '  qImage = New Bitmap(rview.Bitmap) ' commented out 5/16. always read, for multipage
    'End If

    callingForm = Me
    currentpicPath = ListView1.SelectedItems(0).Tag
    Using frm As New frmSaveAs
      dResult = frm.ShowDialog()
    End Using

    clearBitmap(qImage)

  End Sub

  Private Sub mnuFileSend_Click(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles mnuFileSend.Click, mnuFileSendTagged.Click

    If Sender Is mnuFileSend Then ' Send rview.bitmap
      MultiFile = False
      currentpicPath = ListView1.SelectedItems(0).Tag

    Else ' tagged file send. Load files
      If tagPath.Count <= 0 Then
        MsgBox("No photos are tagged to be sent.", MsgBoxStyle.OkOnly)
        Exit Sub
      End If
      MultiFile = True ' flag to load files -- works for one selected file, too.
    End If

    forceConverted = False
    callingForm = Me
    Using frm As New frmSend
      dResult = frm.ShowDialog()
    End Using

    If dResult <> DialogResult.Cancel Then cleartags()
    Me.Refresh()

  End Sub

  Private Sub mnuFilePrint_Click(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles mnuFilePrintTagged.Click, mnuFilePrint.Click

    Dim msg As String = ""

    If Sender Is mnuFilePrint Then
      If ListView1.View <> View.LargeIcon AndAlso rview.Bitmap IsNot Nothing Then ' Print rview.bitmap
        currentpicPath = ListView1.SelectedItems(0).Tag
        qImage = New Bitmap(rview.Bitmap)

      ElseIf ListView1.View = View.LargeIcon AndAlso ListView1.SelectedItems.Count = 1 Then ' print selected item
        currentpicPath = ListView1.SelectedItems(0).Tag
        qImage = readBitmap(ListView1.SelectedItems(0).Tag, msg)

        If qImage Is Nothing Then
          MsgBox(msg & crlf & "Oops!  Couldn't read the file " & ListView1.SelectedItems(0).Tag)
          tagPath = New List(Of String)
        End If
      End If

    Else ' tagged file send.
      If tagPath.Count <= 0 Then
        MsgBox("No photos are tagged to be printed.", MsgBoxStyle.OkOnly)
        Exit Sub
      End If
      clearBitmap(qImage) ' if qImage is nothing, then it uses the files in tagpath.count
    End If

      If qImage IsNot Nothing Or tagPath.Count > 0 Then
        callingForm = Me
        Using frm As New frmPrint
          dResult = frm.ShowDialog() ' print qImage or tagfiles
        End Using
        clearBitmap(qImage)
      End If

      If dResult <> DialogResult.Cancel Then cleartags()
      Me.Refresh()

  End Sub

  Private Sub mnuToolsConvert_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuFileConvert.Click

    If tagPath.Count <= 0 Then
      MsgBox("No files are tagged to be converted.", MsgBoxStyle.OkOnly)
      Exit Sub
    End If

    If bkgThumb.IsBusy Then thumbPause.Reset()
    If bkgPhotoDates.IsBusy Then photoDatesPause.Reset()
    Using frm As New frmConvert
      dResult = frm.ShowDialog()
    End Using

    If dResult <> DialogResult.Cancel Then cleartags()
    Me.Refresh()

  End Sub

  Private Sub mnuToolsWebpage_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuToolsWebpage.Click

    If tagPath.Count <= 0 Then Exit Sub

    dirWatch.EnableRaisingEvents = False

    Using frm As New frmWebPage
      dResult = frm.ShowDialog()
    End Using
    If dResult <> DialogResult.Cancel Then cleartags()

    If dirWatch IsNot Nothing AndAlso Not dirWatch.Path.StartsWith("\\") Then
      Try
        dirWatch.EnableRaisingEvents = True
        Me.Refresh()
      Catch ex As Exception
        MsgBox(ex.Message)
      End Try
    End If
  End Sub

  Private Sub mnuUpOneLevel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuUpOneLevel.Click

    If TreeView.SelectedNode IsNot Nothing Then TreeView.SelectedNode = TreeView.SelectedNode.Parent

  End Sub

  Private Sub mnuView_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuView.DropDownClosed

    mnuEditRotateLeft.Enabled = True
    mnuEditRotateRight.Enabled = True
    mnuViewFullscreen.Enabled = True
    mnuViewStyleDetails.Checked = True
    mnuViewStyleThumbnails.Checked = True

  End Sub

  Private Sub mnuView_DropDownOpening(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuView.DropDownOpening

    If ListView1.SelectedItems.Count <> 1 Then
      mnuEditRotateLeft.Enabled = False
      mnuEditRotateRight.Enabled = False
      mnuViewFullscreen.Enabled = False
    Else
      mnuEditRotateLeft.Enabled = True
      mnuEditRotateRight.Enabled = True
      mnuViewFullscreen.Enabled = True
    End If

    mnuViewToolbar.Checked = Toolstrip1.Visible

    mnuViewStyleDetails.Checked = False
    mnuViewStyleThumbnails.Checked = False
    Select Case ListView1.View
      Case View.Details
        mnuViewStyleDetails.Checked = True
      Case View.LargeIcon
        mnuViewStyleThumbnails.Checked = True
    End Select

    If pathPos > 0 Then mnuViewPrevious.Enabled = True Else mnuViewPrevious.Enabled = False
    If pathPos < pathTop Then mnuViewNext.Enabled = True Else mnuViewNext.Enabled = False

  End Sub

  Private Sub mnuToolsFileFilter_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuToolsFileFilter.Click

    Using frm As New frmFileTypes
      dResult = frm.ShowDialog()
    End Using

    If dResult <> DialogResult.Cancel Then
      Me.Cursor = Cursors.WaitCursor
      ListViewLoad(TreeView.SelectedNode.Tag)
      Me.Cursor = Cursors.Default
    End If

  End Sub

  Private Sub mnuViewFullscreen_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuViewFullscreen.Click

    Dim msg As String = ""
    Dim lastPath As String
    Dim item As ListViewItem

    If ListView1.SelectedItems.Count <> 1 Then Exit Sub
    Me.Cursor = Cursors.WaitCursor

    callingForm = Me
    currentpicPath = ListView1.SelectedItems(0).Tag
    lastPath = currentpicPath
    iPic = ListView1.SelectedItems(0).Index ' parameter to frmFullScreen
    clearBitmap(qImage)


    If rview.Bitmap IsNot Nothing Then
      clearBitmap(qImage)
      qImage = rview.Bitmap.Clone
      Using frm As New frmFullscreen
        dResult = frm.ShowDialog()
      End Using
      clearBitmap(qImage)
    Else
      qImage = readBitmap(currentpicPath, msg)

      If qImage IsNot Nothing Then
        Using frm As New frmFullscreen
          dResult = frm.ShowDialog()
        End Using
        clearBitmap(qImage)
      End If
    End If

    If currentpicPath <> lastPath Then
      ' select the last file viewed
      For Each item In ListView1.Items
        If eqstr(item.Tag, currentpicPath) Then
          item.Selected = True
          item.Focused = True
          Exit For
        End If
      Next item
    End If

    setTags() ' in case something was tagged
    If ListView1.SelectedItems.Count = 1 Then ListView1.SelectedItems(0).EnsureVisible()
    Me.Cursor = Cursors.Default

  End Sub

  Private Sub mnuToolsInfo_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuToolsInfo.Click

    If ListView1.SelectedItems.Count = 1 Then
      Me.Cursor = Cursors.WaitCursor
      currentpicPath = ListView1.SelectedItems(0).Tag ' currentpicpath is global used for browse by frmInfo

      clearBitmap(qImage)
      If rview.Bitmap IsNot Nothing Then ' else frmInfo will read from currentpicpath
        qImage = rview.Bitmap.Clone
      End If

      iPic = ListView1.SelectedItems(0).Index
      callingForm = Me
      Using frm As New frmInfo
        dResult = frm.ShowDialog()
      End Using

      clearBitmap(qImage)

      Me.Cursor = Cursors.Default
    End If

  End Sub

  Private Sub mnuViewRefresh_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuViewRefresh.Click
    Dim Success As Boolean
    useQuery = False
    If dirWatch IsNot Nothing AndAlso Not dirWatch.Path.StartsWith("\\") Then dirWatch.EnableRaisingEvents = True
    TreeViewInit(TreeView, iniExplorePath, dirWatch) ' initialize treeview with drives, etc.
    Success = TreeViewSelectPath(TreeView, iniExplorePath, dirWatch)
  End Sub

  Private Sub mnuEditRotate_Click(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles mnuEditRotateLeft.Click, mnuEditRotateRight.Click

    Dim i As Integer
    Dim kError As String = ""
    Dim msg As String = ""
    Dim picinfo As pictureInfo
    Dim saver As ImageSave

    If ListView1.SelectedItems.Count <> 1 Then Exit Sub
    dirWatch.EnableRaisingEvents = False
    Me.Cursor = Cursors.WaitCursor

    picinfo = getPicinfo(currentpicPath, False)

    If picinfo.FormatID = MagickFormat.Jpg Or picinfo.FormatID = MagickFormat.Jpeg Then
      ' rotate jpg files without loss on disk, other formats in memory only.

      Try
        Using bmp As New Bitmap(currentpicPath)
          jpgEncoderParameters = New EncoderParameters(1)
          If Sender Is mnuEditRotateRight Then
            jpgEncoderParameter = New EncoderParameter(Imaging.Encoder.Transformation,
              Fix(EncoderValue.TransformRotate90))
          Else
            jpgEncoderParameter = New EncoderParameter(Imaging.Encoder.Transformation,
              Fix(EncoderValue.TransformRotate270))
          End If
          jpgEncoderParameters.Param(0) = jpgEncoderParameter
          bmp.Save(currentpicPath & ".~", jpgImageCodecInfo, jpgEncoderParameters) ' save to temp file
        End Using
        If File.Exists(currentpicPath & ".~") Then ' move to original
          File.Delete(currentpicPath)
          File.Move(currentpicPath & ".~", currentpicPath)
        End If
      Catch ex As Exception
        kError = ex.Message
      End Try

    End If ' lossless jpg rotation


    If (picinfo.FormatID <> MagickFormat.Jpg And picinfo.FormatID <> MagickFormat.Jpeg) Or Len(kError) <> 0 Then
      If rview.Bitmap IsNot Nothing Then
        If Sender Is mnuEditRotateRight Then rview.rotate(90) Else rview.rotate(270)
        saver = New ImageSave
        kError = saver.write(rview.Bitmap, currentpicPath, True)

      Else
        Try
          Using bmp As Bitmap = readBitmap(currentpicPath, msg)
            If Sender Is mnuEditRotateRight Then
              bmp.RotateFlip(RotateFlipType.Rotate90FlipNone)
            Else
              bmp.RotateFlip(RotateFlipType.Rotate270FlipNone)
            End If
            saver = New ImageSave
            msg = saver.write(bmp, currentpicPath & ".~", True)
            If ListView1.View <> View.LargeIcon Then rview.setBitmap(bmp)
          End Using
          If File.Exists(currentpicPath & ".~") Then ' move to original
            File.Delete(currentpicPath)
            File.Move(currentpicPath & ".~", currentpicPath)
          End If
          kError = ""
        Catch ex As Exception
          kError = ex.Message
        End Try
      End If
    End If

    ' reload
    If kError = "" Then
      If ListView1.View = View.LargeIcon Then
        updateThumbnail(currentpicPath)
        i = ListView1.SelectedItems(0).Index
        ListView1.RedrawItems(i, i, False)
      Else
        showPicture(currentpicPath, rview, False, Nothing)
      End If
    End If

    setStatusLine()
    dirWatch.EnableRaisingEvents = True
    Me.Cursor = Cursors.Default

  End Sub

  Private Sub mnuViewtoolbar_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuViewToolbar.Click

    If mnuViewToolbar.Checked Then
      mnuViewToolbar.Checked = False
    Else
      mnuViewToolbar.Checked = True
    End If

    iniViewToolbar = mnuViewToolbar.Checked
    Toolstrip1.Visible = iniViewToolbar
    assignAllToolbars()

  End Sub

  Private Sub txfolder_TextChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles txFolder.TextChanged
    txFolderChanged = True
  End Sub

  Private Sub txfolder_Enter(ByVal Sender As Object, ByVal e As EventArgs) Handles txFolder.Enter

    txFolder.SelectionStart = 0
    txFolder.SelectionLength = Len(txFolder.Text)

  End Sub

  Private Sub txfolder_KeyDown(ByVal Sender As Object, ByVal e As KeyEventArgs) Handles txFolder.KeyDown

    If e.KeyCode = 37 Or e.KeyCode = 39 Then
      globalKeydown(e)

    ElseIf e.KeyCode = 13 Then  ' Enter
      Try
        TreeView.Select()
      Catch
      End Try
      e.Handled = True

    ElseIf e.KeyCode = 27 Then  ' esc
      If TreeView.SelectedNode IsNot Nothing Then txFolder.Text = TreeView.SelectedNode.Text
      e.Handled = True
    End If

  End Sub

  Sub openSelected()

    Dim Item As ListViewItem

    Me.Cursor = Cursors.WaitCursor
    abort = False

    If bkgThumb.IsBusy Then thumbPause.Reset()
    If bkgPhotoDates.IsBusy Then photoDatesPause.Reset()

    For Each Item In ListView1.SelectedItems
      OpenDoc(Item.Tag)
      If abort Then
        abort = False
        Exit Sub
      End If
    Next Item

    Me.Cursor = Cursors.Default

  End Sub

  Private Sub mnxTCustomize_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnxTCustomize.Click
    mnuToolsToolbar_Click(Sender, e)
  End Sub

  Private Sub mnxTHideToolbar_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnxTHideToolbar.Click

    mnuViewToolbar.Checked = False
    Me.Toolstrip1.Visible = False
    iniViewToolbar = False
    assignAllToolbars()

  End Sub

  Private Sub mnxTIconSize_Click(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles mnxTSmallIcons.Click, mnxTLargeIcons.Click

    Dim b As ToolStripMenuItem
    b = Sender

    mnxTSmallIcons.Checked = False
    mnxTLargeIcons.Checked = False
    b.Checked = True

    If Sender Is mnxTSmallIcons Then
      If iniButtonSize <> 0 Then
        iniButtonSize = 0
        assignAllToolbars()
      End If
    ElseIf Sender Is mnxTLargeIcons Then
      If iniButtonSize <> 1 Then
        iniButtonSize = 1
        assignAllToolbars()
      End If
    End If

  End Sub

  Private Sub mnxTHelp_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnxTHelp.Click

    Dim topic As String

    ' I would have to do this if I put aliases in the help file
    Select Case currentTool

      Case "mnufileopen"
        topic = "fileopen"

      Case "mnufilesave"
        topic = "frmsaveas"

      Case "mnufileprint"
        topic = "frmprint"

      Case "mnufilesend"
        topic = "frmsend"

      Case "mnueditcopy"
        topic = "copyandpaste"

      Case "mnueditdelete"
        topic = "filedelete"

      Case "mnuwebpage"
        topic = "frmwebpage"

      Case "mnuslideshow"
        topic = "frmslideshow"

      Case "mnuviewprevious", "mnuviewnext", "mnuuponelevel"
        topic = "usingthetoolbar"

      Case "mnuimagerotateleft", "mnuimagerotateright"
        topic = "frmrotate"

      Case "mnuviewfullscreen"
        topic = "frmfullscreen"

      Case "mnuviewthumbnails", "mnuviewdetails"
        topic = "viewstyle"

      Case "mnuhelphelp"
        topic = ""

      Case Else
        topic = ""

    End Select

    Try
      If topic <> "" Then
        Help.ShowHelp(Me, helpFile, HelpNavigator.Topic, topic & ".html")
      Else
        Help.ShowHelp(Me, helpFile, HelpNavigator.Index)
      End If
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

  End Sub

  Private Sub mnuHelpAbout_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuHelpAbout.Click
    HelpAbout()
    Me.Refresh()
  End Sub

  Private Sub mnuHelpHelpIndex_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuHelpHelpIndex.Click
    Try
      Help.ShowHelp(Me, helpFile, HelpNavigator.Index)
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub
  Private Sub mnuHelpHelpTopics_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuHelpHelpTopics.Click
    Try
      Help.ShowHelp(Me, helpFile, HelpNavigator.TableOfContents, "")
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

  Private Sub mnuHelpRegister_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuHelpRegister.Click
    HelpBrowse(urlRegister)
  End Sub

  Private Sub mnuHelpUpdate_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuHelpUpdate.Click
    HelpBrowse(urlUpdate)
  End Sub

  Private Sub setTags()

    Dim i As Integer
    Dim ixTag As New List(Of Integer)
    Dim ixItem As New List(Of Integer)
    Dim sItem As New List(Of String)
    Dim kTag As Integer
    Dim Tagged As Boolean

    processing = True

    For i = 0 To ListView1.Items.Count - 1
      ixItem.Add(i)
      sItem.Add(ListView1.Items(i).Tag)
    Next i
    MergeSort(sItem, ixItem, 0, sItem.Count - 1)

    For i = 0 To tagPath.Count - 1
      ixTag.Add(i)
    Next i
    MergeSort(tagPath, ixTag, 0, tagPath.Count - 1)

    ' merge two sorted lists because .find is too slow.
    ' sitem is 0 based, tagpaths is 1 based
    locTagPaths = 0
    kTag = 0
    For i = 0 To sItem.Count - 1
      Tagged = False
      Do While kTag < tagPath.Count

        Select Case String.Compare(sItem(ixItem(i)), tagPath(ixTag(kTag)), True)
          Case 0 ' found a match
            ListView1.Items(ixItem(i)).Checked = True
            Tagged = True
            locTagPaths = locTagPaths + 1
            kTag = kTag + 1
            Exit Do
          Case -1 ' item < tag -- go to next item
            Exit Do
          Case 1 ' item > tag -- go to next tag
            kTag = kTag + 1
        End Select

      Loop

      If Not Tagged AndAlso ListView1.Items(ixItem(i)).Checked Then ListView1.Items(ixItem(i)).Checked = False
    Next i

    processing = False
    setStatusLine()
    enableButtons()

  End Sub

  Private Sub setStatusLine()

    Dim s As String
    Dim picinfo As New pictureInfo

    If Loading Then Exit Sub

    s = ""

    If ListView1.SelectedItems.Count > 1 Then
      rview.setBitmap(Nothing)
      s = ListView1.SelectedItems.Count & " photos selected.    "
    ElseIf ListView1.SelectedItems.Count = 1 Then
      If (rview.Bitmap IsNot Nothing) Then
        s = "File: " & Path.GetFileName(currentpicPath) & ",  " & rview.Bitmap.Width & " x " & rview.Bitmap.Height & ".     "
      Else
        If currentpicPath <> "" Then picinfo = getPicinfo(currentpicPath, True)
        If Not picinfo.isNull AndAlso (picinfo.Width > 0 And picinfo.Height > 0) Then
          s = "File: " & Path.GetFileName(currentpicPath) & ",  " & picinfo.Width & " x " & picinfo.Height & ".     "
        End If
      End If
    End If

    If tagPath.Count > 0 Then ' And ListView1.View <> View.LargeIcon Then
      s = s & tagPath.Count & " photos tagged"
      If tagPath.Count <> locTagPaths Then s = s & ", " & locTagPaths & " here"
      s = s & ".     "
    End If

    If ListView1.Items.Count = 1 Then
      s = s & "1 photo in folder.     "
    Else
      s = s & ListView1.Items.Count & " photos in folder.     "
    End If

    If lbStatus.Visible Then lbStatus.Text = s

  End Sub

  Sub enableButtons()

    Dim b As ToolStripItem

    If Loading Then Exit Sub

    For Each b In Toolstrip1.Items
      Select Case b.Tag
        Case "mnufileprint", "mnufilesend"
          If ListView1.SelectedItems.Count = 1 Then b.Enabled = True Else b.Enabled = False

        Case "mnuwebpage", "mnuslideshow"
          If tagPath.Count > 0 Then b.Enabled = True Else b.Enabled = False

        Case "mnufileopen", "mnueditdelete"
          If ListView1.SelectedItems.Count >= 1 Then b.Enabled = True Else b.Enabled = False

        Case "mnueditcopy", "mnufilesave", "mnuimagerotateleft", "mnuimagerotateright", "mnuviewfullscreen"
          If ListView1.SelectedItems.Count = 1 Then b.Enabled = True Else b.Enabled = False

        Case "mnuviewprevious"
          If pathPos < 1 Then b.Enabled = False Else b.Enabled = True
        Case "mnuviewnext"
          If pathPos >= pathTop Then b.Enabled = False Else b.Enabled = True
      End Select
    Next b

  End Sub

  Public Function getNextPath(ByRef i As Integer) As String
    ' for fullscreen, etc
    getNextPath = ""
    If ListView1.Items.Count = 0 Then Exit Function
    If i < 0 Then i = ListView1.Items.Count - 1
    If i > ListView1.Items.Count - 1 Then i = 0
    If Not File.Exists(ListView1.Items(i).Tag) Then ' file was deleted - remove listview item
      If ListView1.Items(i).Checked Then unTag(ListView1.Items(i).Tag)
      ListView1.Items(i).Remove()
      If i > ListView1.Items.Count - 1 Then i = 0
    End If
    getNextPath = ListView1.Items(i).Tag
    ListView1.SelectedItems.Clear() ' select one item only
    ListView1.Items(i).Focused = True
    ListView1.Items(i).Selected = True
  End Function

  Private Sub unTag(ByRef fPath As String)

    If tagPath.IndexOf(fPath) >= 0 Then
      tagPath.Remove(fPath)
      locTagPaths = locTagPaths - 1
    End If

  End Sub

  Private Sub TreeView_AfterLabelEdit(ByVal sender As Object, ByVal e As NodeLabelEditEventArgs) Handles TreeView.AfterLabelEdit
    ' rename the folder if possible

    dirWatch.EnableRaisingEvents = False
    Try
      My.Computer.FileSystem.RenameDirectory(e.Node.Tag, e.Label)
      e.Node.Tag = Path.GetDirectoryName(e.Node.Tag) & "\" & e.Label ' replace the full path in .tag
      dirWatch.Path = e.Node.Tag
    Catch
      e.CancelEdit = True
    End Try

    Try
      If dirWatch IsNot Nothing AndAlso Not dirWatch.Path.StartsWith("\\") Then dirWatch.EnableRaisingEvents = True
    Catch ex As Exception
    End Try

    labelEditing = ""

  End Sub

  Private Sub TreeView_AfterSelect(ByVal Sender As Object, ByVal e As TreeViewEventArgs) Handles TreeView.AfterSelect

    Static busy As Boolean = False

    If processing Or Loading Then Exit Sub
    If busy Then Exit Sub
    busy = True

    Me.Cursor = Cursors.WaitCursor
    loadNode(e.Node)

    e.Node.ImageIndex = 1
    e.Node.SelectedImageIndex = 1
    txFolder.Text = e.Node.Tag

    If (Not pathPrevious) And (Not pathNext) Then
      pathPos = pathPos + 1
      pathList.Add(e.Node.Tag)
      pathTop = pathPos
    End If

    iniExplorePath = e.Node.Tag
    loadPath = iniExplorePath

    pathPrevious = False
    pathNext = False

    timerTreeView.Stop()
    timerTreeView.Interval = 250 ' wait .25 seconds before loading, for keyrepeat.
    timerTreeView.Start()

    Me.Cursor = Cursors.Default

    busy = False

  End Sub

  Private Sub timertreeView_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timerTreeView.Tick
    timerTreeView.Stop()
    Me.Cursor = Cursors.WaitCursor
    If TreeView.SelectedNode IsNot Nothing Then ListViewLoad(TreeView.SelectedNode.Tag)
    Me.Cursor = Cursors.Default
  End Sub

  Private Sub TreeView_BeforeSelect(ByVal sender As Object, ByVal e As TreeViewCancelEventArgs) Handles TreeView.BeforeSelect

    If TreeView.SelectedNode IsNot Nothing Then
      TreeView.SelectedNode.ImageIndex = 0
      TreeView.SelectedNode.SelectedImageIndex = 0
    End If

  End Sub

  Private Sub TreeView_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As TreeNodeMouseClickEventArgs) Handles TreeView.NodeMouseDoubleClick
    e.Node.Expand()
  End Sub

  Private Sub txfolder_Leave(ByVal Sender As Object, ByVal e As EventArgs) Handles txFolder.Leave

    Dim Success As Boolean

    If Not txFolderChanged Then Exit Sub
    If processing Then Exit Sub

    Success = False
    Success = TreeViewSelectPath(TreeView, txFolder.Text, dirWatch)

    If Not Success And TreeView.SelectedNode IsNot Nothing Then
      txFolder.Text = TreeView.SelectedNode.Tag
    End If

    txFolderChanged = False

  End Sub

  Sub ListViewLoad(ByVal fPath As String, Optional ByVal selectedPath As String = "")

    ' loads the listview with photos from folder fPath.
    Dim fileNames As New List(Of String)
    Dim ix As New List(Of Integer)
    Dim i As Integer
    ' Dim v As lvSort  11/6/2012
    Static busy As Boolean
    Dim v As lvSort

    If useQuery Then
      dirWatch.EnableRaisingEvents = False
      TreeView.Enabled = False
      txFolder.Enabled = False
    Else
      Try
        If dirWatch IsNot Nothing AndAlso Not dirWatch.Path.StartsWith("\\") Then dirWatch.EnableRaisingEvents = True
      Catch ex As Exception
        MsgBox(ex.Message)
      End Try
      TreeView.Enabled = True
      txFolder.Enabled = True
    End If

    If busy Then Exit Sub ' no re-entry
    busy = True

    If bkgThumb.IsBusy Then
      ' stop thumbnail loading if necessary
      bkgThumb.CancelAsync()
      Do While bkgThumb.IsBusy
        Thread.Sleep(20)
        Application.DoEvents()
      Loop
    End If

    If bkgPhotoDates.IsBusy Then
      ' stop thumbnail loading if necessary
      bkgPhotoDates.CancelAsync()
      Do While bkgPhotoDates.IsBusy
        Thread.Sleep(20)
        Application.DoEvents()
      Loop
    End If

    If ListView1.View = View.LargeIcon Then
      ThumbnailLoad(fPath) ' loads thumbnails in background
      afterListviewFill(selectedPath)
      busy = False
      Exit Sub
    End If

    ' ====== not thumbnails ===========
    rview.setBitmap(Nothing)
    rview.Refresh()
    processing = True
    ListView1.SmallImageList = imgSmallIcons
    ListView1.BeginUpdate()
    v = ListView1.ListViewItemSorter  ' commented out 11/6/2012, uncommented 10/1/17. Too slow?
    ListView1.ListViewItemSorter = Nothing
    ListView1.Items.Clear()

    If useQuery Then
      getQueryPaths(fileNames, True)
      If fileNames.Count <= 0 Then
        useQuery = False
        If dirWatch.Path.StartsWith("\\") Then
          dirWatch.EnableRaisingEvents = False
        Else
          dirWatch.EnableRaisingEvents = True
        End If
      End If
    End If
    If Not useQuery Then
      i = getFilePaths(fPath, fileNames, False)
    End If

    For i = 0 To fileNames.Count - 1 : ix.Add(i) : Next
    If Not useQuery Then MergeSort(fileNames, ix, 0, fileNames.Count - 1)

    imgSmallIcons.ImageSize = New Size(ListView1.Font.Height + 2, ListView1.Font.Height + 2)

    For i = 0 To fileNames.Count - 1

      addListviewItem(fileNames(ix(i)))

      If i = 200 Then  ' update the list for a large folder
        ListView1.EndUpdate()
        Application.DoEvents()
        ListView1.BeginUpdate()
      End If
    Next i

    ListView1.ListViewItemSorter = v  ' commented out 11/6/2012, uncommented 10/1/17
    'ListView1.ListViewItemSorter = New lvSort(0)   ' added 11/6/2012, commented out 10/1/17
    ListView1.EndUpdate()
    processing = False

    afterListviewFill(selectedPath)
    ListView1.Refresh()
    If ListView1.View = View.Details Then getPhotoDates()

    busy = False

  End Sub

  Sub addListviewItem(fname As String)

    Dim item As ListViewItem
    Dim fInfo As FileInfo
    Dim fIcon As Icon
    Dim k As Integer

    If Not (imgSmallIcons.Images.ContainsKey(Path.GetExtension(fname))) Then
      fIcon = System.Drawing.Icon.ExtractAssociatedIcon(fname)
      If fIcon Is Nothing Then fIcon = SystemIcons.WinLogo
      imgSmallIcons.Images.Add(Path.GetExtension(fname), fIcon)
    End If

    item = New ListViewItem
    item.ImageKey = Path.GetExtension(fname)
    item.Text = Path.GetFileName(fname)
    item.Name = item.Text ' for .find
    item.Tag = fname
    fInfo = My.Computer.FileSystem.GetFileInfo(fname)
    k = Round(fInfo.Length / 1000)
    If k = 0 Then k = 1
    item.SubItems.Add(Format(k, "#,##0 KB"))
    item.SubItems.Add(Format(fInfo.LastWriteTime, "short date") & " " & Format(fInfo.LastWriteTime, "short time"))
    item.SubItems.Add("")
    ListView1.Items.Add(item)

  End Sub


  Sub afterListviewFill(ByVal selectedPath As String)

    Dim item As ListViewItem

    'If ListView1.View <> View.LargeIcon Then
    locTagPaths = 0
    If Not iniMultiTagPath Then
      oldTagPath = tagPath
      tagPath = New List(Of String)
    Else
      setTags()
    End If
    '  End If

    If selectedPath <> "" Then
      For Each item In ListView1.Items
        If item.Tag = selectedPath Then
          item.EnsureVisible()
          item.Selected = True
          item.Focused = True
          Exit For
        End If
      Next item
    End If

    If ListView1.SelectedItems.Count = 0 And ListView1.Items.Count > 0 Then
      ListView1.Items(0).EnsureVisible()
      ListView1.Items(0).Focused = True
      ListView1.Items(0).Selected = True
    End If

    getStateImages(imgLviewState, ListView1.BackColor, imgSmallIcons.ImageSize.Width - 4, imgSmallIcons.ImageSize.Height - 4)
    ListView1.StateImageList = imgLviewState

  End Sub

  Sub ThumbnailLoad(ByVal fPath As String, Optional ByRef initialize As Boolean = True)

    Dim fileNames As New List(Of String)
    Dim ix As New List(Of Integer)
    Dim i As Integer
    Dim items As New List(Of ListViewItem)
    Dim iconSize As Point
    Dim v(2) As Object

    If useQuery Then
      getQueryPaths(fileNames, initialize)
      'fileNames = New List(Of String)
      'For Each s1 As String In queryNames
      ' fileNames.Add(s1)
      'Next s1

      'If nFiles <= 0 Then
      '  useQuery = False
      '  dirWatch.EnableRaisingEvents = True
      '  End If
    End If
    If Not useQuery Then
      i = getFilePaths(fPath, fileNames, False)
    End If

    For i = 0 To fileNames.Count - 1 : ix.Add(i) : Next
    If Not useQuery Then MergeSort(fileNames, ix, 0, fileNames.Count - 1)
    ListView1.ListViewItemSorter = Nothing ' \\\ added 11/6/2012

    'ListView1.CheckBoxes = False
    If initialize Then
      If iniThumbX <= 0 Or iniThumbY <= 0 Or iniThumbX >= 248 Or iniThumbY >= 248 Then
        iniThumbX = 160 : iniThumbY = 120
      End If
      iconSize = New Point(iniThumbX, iniThumbY)
      ListView1.View = View.LargeIcon

      ListView1.Items.Clear()
      imgThumbnails.Images.Clear()
      imgThumbnails.ImageSize = New Point(iconSize.X + iniShadowSize, iconSize.Y + iniShadowSize)
      iconStatus = New List(Of Integer)

      For i = 0 To fileNames.Count - 1
        items.Add(New ListViewItem)
        items(i).Text = Path.GetFileName(fileNames(ix(i)))
        items(i).Name = items(i).Text ' for .find
        items(i).Tag = fileNames(ix(i))
        items(i).ImageKey = items(i).Text
        'items(i).Checked = True  ' doesn't draw the unchecked box if you don't do this. (commented with vs2013)
        'items(i).Checked = False
        iconStatus.Add(0)
      Next i

      ListView1.Items.AddRange(items.ToArray)

    End If

    v(0) = fileNames
    v(1) = ix
    v(2) = iconSize

    thumbPause.Set()
    bkgThumb.RunWorkerAsync(v)

  End Sub

  Sub updateThumbnail(ByVal fName As String)
    ' replace the image in imgThumbnails

    Dim gimage As Bitmap = Nothing
    Dim msg As String = ""

    Dim img As Bitmap
    Dim imageKey As String

    If iniThumbX < 200 Then
      gimage = readThumbnail(iniThumbX, iniThumbY, fName, msg, True)
    End If

    If gimage Is Nothing Or Len(msg) <> 0 Then
      ' load the non-stamp thumbnail (resizes image)
      gimage = readThumbnail(iniThumbX, iniThumbY, fName, msg, False, Drawing2D.InterpolationMode.High)
    End If

    imageKey = Path.GetFileName(fName)

    If gimage IsNot Nothing Then
      img = getShadow(gimage, iniShadowSize, ListView1.BackColor, iniThumbX, iniThumbY) ' add shadow
      imageListAdd(img, imageKey)
      Try
        imgThumbnails.Images.RemoveByKey(imageKey)
        imgThumbnails.Images.Add(imageKey, img)
      Catch ex As Exception
      End Try
      gimage.Dispose() : gimage = Nothing
    End If

  End Sub

  Private Sub bkgThumb_dowork(ByVal Sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) _
    Handles bkgThumb.DoWork

    ' this is for to access to the imagelist and listview from the background thread.
    Dim imagelistDelegate As New imagelistAddCallback(AddressOf imageListAdd)
    Dim tooltipDelegate As New tooltipAddCallback(AddressOf tooltipAdd)
    Dim itemdrawDelegate As New itemDrawCallback(AddressOf itemDraw)
    Dim tip As String = ""

    Dim fileNames As New List(Of String)
    Dim ix As New List(Of Integer)
    Dim iconsize As Point

    Dim picInfo As pictureInfo

    Dim i As Integer
    Dim s As String
    Dim imageKey As String
    Dim gImage As Bitmap = Nothing
    Dim img As Bitmap = Nothing
    Dim msg As String = ""

    ' object v has 
    fileNames = e.Argument(0)
    ix = e.Argument(1)
    iconsize = e.Argument(2)

    If fileNames.Count = 0 Then Exit Sub

    ' get thumbnail stamps and tooltips from the image file
    lastTop = -1
    setLoopStuff(loopFrom, loopTo, lastTop, fileNames.Count)
    Do While True
      i = loopFrom
      If bkgThumb.CancellationPending Then
        e.Cancel = True
        Exit Do
      End If
      thumbPause.WaitOne() ' see if it's paused for editing

      If iconStatus(ix(i)) = 0 Then ' skip if already done

        imageKey = Path.GetFileName(fileNames(ix(i)))
        ' EXIF, CMP, JFIF and FlashPix can have thumbnail stamps
        s = LCase(Path.GetExtension(imageKey))
        If Not (imgThumbnails.Images.ContainsKey(imageKey)) And _
          (s = ".jpg" Or s = ".jpeg" Or s = ".tiff" Or s = ".tif" Or s = ".cmp") Then
          gImage = readThumbnail(iniThumbX, iniThumbY, fileNames(ix(i)), msg, True)

          If gImage IsNot Nothing And msg = "" Then
            picInfo = getPicinfo(fileNames(ix(i)), True, 1)
            tip = getImageTip(fileNames(ix(i)), picInfo)
            Me.Invoke(tooltipDelegate, fileNames(ix(i)), imageKey, tip) ' Sub tooltipAdd
            img = getShadow(gImage, iniShadowSize, ListView1.BackColor, iniThumbX, iniThumbY) ' add shadow
            Try
              Me.Invoke(imagelistDelegate, img, imageKey)  ' imagelistAdd
              iconStatus(ix(i)) = 1
            Catch
              Exit Do
            End Try
          End If

        End If
      End If
      If loopFrom = loopTo Then Exit Do
      loopFrom = loopFrom + 1
      If loopFrom > fileNames.Count - 1 Then loopFrom = 0

      setLoopStuff(loopFrom, loopTo, lastTop, fileNames.Count)
    Loop

    ' if necessary, get non-stamp thumbnails from images
    lastTop = -1
    setLoopStuff(loopFrom, loopTo, lastTop, fileNames.Count)
    Do While True
      i = loopFrom
      If bkgThumb.CancellationPending Then
        e.Cancel = True
        Exit Do
      End If
      thumbPause.WaitOne() ' see if it's paused for editing

      imageKey = Path.GetFileName(fileNames(ix(i)))

      If (Not (imgThumbnails.Images.ContainsKey(imageKey)) Or iconsize.X > 200) And iconStatus(ix(i)) < 2 Then ' Or iconsize.X > 160 Then
        gImage = readThumbnail(iniThumbX, iniThumbY, fileNames(ix(i)), msg, False, InterpolationMode.High)

        If gImage IsNot Nothing Or Len(msg) <> 0 Then
          picInfo = getPicinfo(fileNames(ix(i)), True, 1)
          If Not picInfo.isNull Then tip = getImageTip(fileNames(ix(i)), picInfo) Else tip = ""
          img = getShadow(gImage, iniShadowSize, ListView1.BackColor, iniThumbX, iniThumbY) ' add shadow
          Try
            Me.Invoke(tooltipDelegate, fileNames(ix(i)), imageKey, tip) ' Sub tooltipAdd
            ' this is for access to the imagelist and listview from the background thread.
            Me.Invoke(imagelistDelegate, img, imageKey) ' imagelistAdd
            iconStatus(ix(i)) = 2
          Catch ex As Exception
            e.Cancel = True
            Exit Do
          End Try
        End If
      End If

      If loopFrom = loopTo Then Exit Do
      loopFrom = loopFrom + 1
      If loopFrom > fileNames.Count - 1 Then loopFrom = 0
      setLoopStuff(loopFrom, loopTo, lastTop, fileNames.Count)
    Loop

    clearBitmap(gImage)
    clearBitmap(img)

  End Sub

  Sub setLoopStuff(ByRef loopFrom As Integer, ByRef loopTo As Integer, ByRef lastTopp As Integer, ByVal nFiles As Integer)
    Dim getListviewTopDelegate As New getListviewTopCallback(AddressOf getListviewTop)
    Dim i As Integer

    Try
      i = Me.Invoke(getListviewTopDelegate)
    Catch ex As Exception
      MsgBox("Error in Invoke: " & ex.Message)
      Exit Sub
    End Try
    If i <> lastTopp And i >= 0 And i < nFiles Then ' listview moved - reset loop variables
      loopFrom = i
      If nFiles < thumbLimit Then
        loopTo = loopFrom - 1
        If loopTo < 0 Then loopTo = nFiles - 1
      Else
        loopTo = loopFrom + 60
        If loopTo >= nFiles Then loopTo = loopFrom - 1
        If loopTo < 0 Then loopTo = nFiles - 1
      End If
      lastTopp = i
    End If

  End Sub

  Sub trimThumb(ByRef gimage As Bitmap, ByRef picInfo As pictureInfo)
    ' crop the thumbnail from tAspect to picinfo.Aspect, because leadtools always reads a 160x120 jpg thumbnail

    Dim ix1, iy1 As Integer
    Dim ih, iw As Integer
    Dim tAspect As Double

    If picInfo.Aspect > 0 And gimage.Width > 0 Then
      tAspect = gimage.Height / gimage.Width

      If Abs(tAspect - picInfo.Aspect) * gimage.Width > 2 Then ' trim it
        ix1 = 0 : iy1 = 0 : ih = gimage.Height : iw = gimage.Width
        If tAspect > picInfo.Aspect Then ' thumbnail is too tall -- trim the height
          ih = gimage.Width * picInfo.Aspect
          iy1 = (gimage.Height - ih) / 2
        Else ' trim sides
          iw = gimage.Height / picInfo.Aspect
          ix1 = (gimage.Width - iw) / 2
        End If

      End If
    End If

  End Sub

  Function getListviewTop() As Integer
    ' get the top item in listview (thumbnail mode only)
    getListviewTop = 0
    If ListView1.View <> View.LargeIcon Then Exit Function

    Try
      getListviewTop = ListView1.FindNearestItem(SearchDirectionHint.Right, New Point(20, 20)).Index
      If getListviewTop > 0 Then getListviewTop = getListviewTop - 1
    Catch ex As Exception
    End Try

  End Function

  Function getImageTip(ByVal fName As String, ByRef picinfo As pictureInfo) As String

    Dim s As String
    Dim k As Integer

    If picinfo Is Nothing Then Return ""

    s = "File name: " & Path.GetFileName(fName)
    If picinfo.Width > 0 And picinfo.Height > 0 Then
      s = s & crlf & "Resolution: " & picinfo.Width & " x " & picinfo.Height
    End If
    If picinfo.fileSize > 0 Then
      k = Round(picinfo.fileSize) / 1000
      If k = 0 Then k = 1
      s = s & crlf & "File size: " & Format(k, "#,##0") & " KB"
    End If
    If picinfo.colorDepth > 0 Then s = s & crlf & "Color depth: " & picinfo.colorDepth
    If picinfo.FormatDescription.Trim <> "" Then s = s & crlf & "Format: " & picinfo.FormatDescription
    s = s & crlf & "Multipage: " & picinfo.hasPages

    s = readPhotoDate(fName)
    If s <> Nothing Then s = s & crlf & "Photo date: " & s

    Return s

  End Function

  Sub imageListAdd(ByRef bmp As Bitmap, ByVal itemKey As String)

    Dim item As ListViewItem
    Dim imageKey As String
    Dim i As Integer
    'Dim Result As MsgBoxResult

    item = ListView1.Items(itemKey)
    If item IsNot Nothing Then ' could be nothing from previous listviewload
      If imgThumbnails.Images(item.ImageKey) IsNot Nothing Then
        imageKey = item.ImageKey
        i = imgThumbnails.Images.IndexOfKey(item.ImageKey)
        imgThumbnails.Images(i) = bmp
        item.ImageKey = "" ' redraw item
        item.ImageKey = imageKey
      Else
        Try
          imgThumbnails.Images.Add(itemKey, bmp)
        Catch ex As Exception
          ' Result = MsgBox("Imagelist Error: " & ex.Message & vbCrLf, MsgBoxStyle.OkCancel) ' don't interrupt for bad image files
          ' If Result = MsgBoxResult.Cancel Then bkgThumb.CancelAsync()
        End Try
      End If
    End If
  End Sub

  Sub tooltipAdd(ByVal fname As String, ByVal Key As String, ByVal tip As String)
    Dim item As ListViewItem

    item = ListView1.FindItemWithText(Key)
    If item IsNot Nothing AndAlso item.Text = Key Then
      item.ToolTipText = tip
    End If

  End Sub

  Sub itemDraw(ByVal imageKey As String)

    Dim item As ListViewItem

    item = ListView1.FindItemWithText(imageKey)
    ListView1.RedrawItems(item.Index, item.Index, False)
  End Sub

  Sub getPhotoDates()

    ' this is for to access to the imagelist and listview from the background thread.
    Dim itemAssignDelegate As New itemAssignCallback(AddressOf itemAssign)

    Dim item As ListViewItem
    Dim fileNames As New List(Of String)
    Dim v As Object

    For Each item In ListView1.Items
      fileNames.Add(item.Tag)
    Next item

    v = fileNames
    photoDatesPause.Set()
    bkgPhotoDates.RunWorkerAsync(v)

  End Sub

  Private Sub bkgPhotoDates_DoWork1(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) _
    Handles bkgPhotoDates.DoWork

    ' this is for to access to the imagelist and listview from the background thread.
    Dim itemAssignDelegate As New itemAssignCallback(AddressOf itemAssign)
    Dim itemdrawDelegate As New itemDrawCallback(AddressOf itemDraw)

    Dim s As String
    Dim fileNames As New List(Of String)

    fileNames = e.Argument

    For Each fName As String In fileNames ' already sorted in listview
      photoDatesPause.WaitOne() ' see if it's paused

      If bkgPhotoDates.CancellationPending Then
        e.Cancel = True
        Exit For
      End If
      s = readPhotoDate(fName)
      Try
        Me.Invoke(itemAssignDelegate, fName, s)   ' item.subitems(3) = s
      Catch ex As Exception
        e.Cancel = True
        Exit For
      End Try
    Next fName

  End Sub

  Sub itemAssign(ByVal fName As String, ByVal sDate As String)

    Dim item As ListViewItem
    Dim s As String

    s = Path.GetFileName(fName)
    item = ListView1.FindItemWithText(s)
    If item IsNot Nothing Then
      item.SubItems(3).Text = sDate
      ListView1.RedrawItems(item.Index, item.Index, False)
    End If

  End Sub

  Private Sub Toolstrip1_mousemove(ByVal Sender As Object, ByVal e As EventArgs)
    ' this is the handler for all tool buttons' click, assigned in assignVToolbar
    currentTool = Sender.tag
  End Sub

  Private Sub Toolstrip1_Click(ByVal Sender As Object, ByVal e As EventArgs)
    ' this is the handler for all tool buttons' click, assigned in assignVToolbar

    Select Case Sender.Tag

      Case "mnufileopen"
        mnuFileOpenCurrent_Click(Sender, e)

      Case "mnufilesave"
        mnuFileSaveAs_Click(Sender, e)

      Case "mnufileprint"
        mnuFilePrint_Click(Sender, e)

      Case "mnufilesend"
        mnuFileSend_Click(Sender, e)

      Case "mnueditcopy"
        mnuEditCopyImage_Click(Sender, e)

      Case "mnueditdelete"
        mnuEditDelete_Click(Sender, e)

      Case "mnuwebpage"
        mnuToolsWebpage_Click(Sender, e)

      Case "mnuslideshow"
        mnuToolsSlideshow_Click(Sender, e)

      Case "mnuviewprevious"
        mnuViewPrevious_Click(Sender, e)

      Case "mnuviewnext"
        mnuViewNext_Click(Sender, e)

      Case "mnuuponelevel"
        mnuUpOneLevel_Click(Sender, e)

      Case "mnuimagerotateleft"
        mnuEditRotate_Click(mnuEditRotateLeft, e)

      Case "mnuimagerotateright"
        mnuEditRotate_Click(mnuEditRotateRight, e)

      Case "mnuviewfullscreen"
        mnuViewFullscreen_Click(Sender, e)

      Case "mnuviewthumbnails"
        mnuViewStyleThumbnails_Click(Sender, e)
      Case "mnuviewdetails"
        mnuViewStyleDetails_Click(Sender, e)

      Case "mnuhelphelp"
        mnuHelpHelpTopics_Click(Sender, e)
    End Select

  End Sub

  Private Sub SplitContainer2_SplitterMoved(ByVal sender As Object, ByVal e As SplitterEventArgs) Handles SplitContainer2.SplitterMoved
    If (e.SplitY >= 0) And (ListView1.View <> View.LargeIcon) Then
      iniListTop = e.SplitY / SplitContainer2.Height
      If iniListTop > 0 Then rview.Zoom(0)
    End If
  End Sub

  Private Sub SplitContainer1_SplitterMoved(ByVal sender As Object, ByVal e As SplitterEventArgs) Handles SplitContainer1.SplitterMoved
    If e.SplitX > 0 Then
      iniFolderWidth = e.SplitX / SplitContainer1.Width
      If ListView1.View <> View.LargeIcon Then rview.Zoom(0)
    End If
  End Sub

  Private Sub dirWatch_created(ByVal sender As Object, ByVal e As FileSystemEventArgs) _
    Handles dirWatch.Created, dirWatch.Deleted
    dirwatchEvent(sender, "", e.FullPath, e.ChangeType)
  End Sub

  Private Sub dirWatch_renamed(ByVal sender As Object, ByVal e As RenamedEventArgs) _
    Handles dirWatch.Renamed
    dirwatchEvent(sender, e.OldFullPath, e.FullPath, e.ChangeType)
  End Sub

  Sub dirwatchEvent(sender As Object, oldFullPath As String, fullPath As String, changetype As WatcherChangeTypes)
    ' reloads treeview or listview if a folder or file is changed. 
    ' if a folder or file is deleted out of iniExplorePath, then they both are updated.

    Dim fPath As String
    Dim item As ListViewItem
    Dim i, k As Integer
    Dim nd As TreeNode
    Dim s, ext As String

    fPath = Path.GetDirectoryName(FullPath)
    ext = Path.GetExtension(FullPath)
    k = -1
    For i = 0 To iniFileType.Count - 1
      s = iniFileType(i)
      If eqstr(s, ext) Then
        k = i
        Exit For
      End If
    Next i

    If k < 0 Then ' is it a directory?
      Try
        If Directory.Exists(fullPath) Then k = 1
      Catch
      End Try
    End If

    If k >= 0 Then ' image extension
      If eqstr(fPath, iniExplorePath) And (Not Directory.Exists(fullPath)) Then

        Select Case changetype

          Case WatcherChangeTypes.Created
            addListviewItem(fullPath)

          Case WatcherChangeTypes.Renamed
            item = ListView1.FindItemWithText(Path.GetFileName(oldFullPath))

            If item IsNot Nothing Then
              If item.Checked Then unTag(item.Tag)
              item.Remove()
              addListviewItem(fullPath)
            End If

          Case WatcherChangeTypes.Deleted

            item = ListView1.FindItemWithText(Path.GetFileName(fullPath))

            If item IsNot Nothing Then
              If item.Checked Then unTag(item.Tag)
              item.Remove()
            End If

        End Select

        ' If ListView1.FocusedItem IsNot Nothing Then ListView1.FocusedItem.Selected = True ``

      End If
    End If

    If Not File.Exists(fullPath) Then ' do the folder stuff
      nd = TreeViewfind(TreeView, fPath) ' search for the path in treeview. Ignore if it's not in ther

      If nd IsNot Nothing Then
        loadNode(nd) ' load the changed folder in treeview
        nd.Expand()
        If Not Directory.Exists(iniExplorePath) Then
          iniExplorePath = nd.Tag
          loadPath = iniExplorePath
        End If
      End If
    End If

  End Sub

  Private Sub mnuFileMru_Click(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles mnuFileMru1.Click, mnuFileMru2.Click, mnuFileMru3.Click, mnuFileMru4.Click, mnuFileMru5.Click, _
    mnuFileMru6.Click, mnuFileMru7.Click, mnuFileMru8.Click, mnuFileMru9.Click

    Dim index As Integer

    If bkgThumb.IsBusy Then thumbPause.Reset()
    If bkgPhotoDates.IsBusy Then photoDatesPause.Reset()

    index = Val(Sender.name.chars(Sender.name.length - 1)) ' get last character, 1-9
    LoadMruFile(index) ' index is 1-9 -- which file

  End Sub

  Sub assignVToolbar()

    Dim i As Integer
    Dim b As ToolStripItem
    Dim bd As ToolStripDropDownItem
    Dim ts As ToolStripSeparator

    Select Case iniButtonSize
      Case 0
        Toolstrip1.ImageScalingSize = New Size(24, 24)
      Case Else ' 1
        Toolstrip1.ImageScalingSize = New Size(32, 32)
    End Select

    readToolButtons(iniButtonSize)

    Toolstrip1.Items.Clear()

    For i = 1 To nVToolButtons
      If iniVToolButton(i) = "---" Then ' make a new separator item 
        ts = New ToolStripSeparator
        ts.Tag = "---" & i
        ts.Name = "tsvSeparator" & i
        ts.Size = New Size(32, 32)
        Toolstrip1.Items.Add(ts)
      ElseIf iniVToolButton(i) = "mnuviewstyle" Then ' dropdown button
        bd = iniVButton(iniVToolButton(i))
        bd.Margin = New Padding(2)
        Try
          bd.Image = iniToolbarPic(bd.Tag)
        Catch
        End Try
        bd.DropDown = mnxViewStyle
        Toolstrip1.Items.Add(bd)
        RemoveHandler bd.Click, AddressOf Toolstrip1_Click  ' prevents double to handler
        AddHandler bd.Click, AddressOf Toolstrip1_Click
        RemoveHandler bd.MouseMove, AddressOf Toolstrip1_mousemove  ' prevents double to handler
        AddHandler bd.MouseMove, AddressOf Toolstrip1_mousemove
      Else
        Try
          b = iniVButton(iniVToolButton(i))
          b.Margin = New Padding(2)
          Try
            b.Image = iniToolbarPic(b.Tag)
          Catch
          End Try
          Toolstrip1.Items.Add(b)
          RemoveHandler b.Click, AddressOf Toolstrip1_Click  ' prevents double to handler
          AddHandler b.Click, AddressOf Toolstrip1_Click
          RemoveHandler b.MouseMove, AddressOf Toolstrip1_mousemove  ' prevents double to handler
          AddHandler b.MouseMove, AddressOf Toolstrip1_mousemove
        Catch
          MsgBox("Menu button " & iniVToolButton(i) & " is missing.")
        End Try
      End If
    Next i

    For Each b In Toolstrip1.Items
      If iniToolbarText Then
        b.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
      Else
        b.DisplayStyle = ToolStripItemDisplayStyle.Image
      End If
    Next b

  End Sub

  Private Sub mnxVThumbnails_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnxVThumbnails.Click
    mnuViewStyleThumbnails_Click(sender, e)
  End Sub

  Private Sub mnxVDetails_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnxVDetails.Click
    mnuViewStyleDetails_Click(sender, e)
  End Sub

  Private Sub ListView1_BeforeLabelEdit(ByVal sender As Object, ByVal e As LabelEditEventArgs) Handles ListView1.BeforeLabelEdit
    labelEditing = "listview1"
  End Sub

  Private Sub mnuToolsCalendar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuToolsCalendar.Click

    If tagPath.Count <= 0 Then Exit Sub
    Me.Cursor = Cursors.WaitCursor

    Using frm As New frmCalendar
      dResult = frm.ShowDialog()
    End Using
    If dResult <> DialogResult.Cancel Then cleartags()

    Me.Cursor = Cursors.Default
    Me.Refresh()

  End Sub

  Private Sub mnxTreeviewNewFolder_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnxTreeviewNewFolder.Click
    Dim nd As TreeNode
    Dim i As Integer
    Dim dPath As String
    Dim s As String

    dirWatch.EnableRaisingEvents = False

    dPath = Path.GetDirectoryName(TreeView.SelectedNode.Tag) & "\New Folder"
    dPath = dPath.Replace(":\\", ":\")

    If Directory.Exists(dPath) Then
      For i = 1 To 50
        s = dPath & " (" & i & ")"
        If Not Directory.Exists(s) Then
          dPath = s
          Exit For
        End If
      Next i
    End If

    If Directory.Exists(dPath) Then Exit Sub

    Try
      Directory.CreateDirectory(dPath)
      i = 0
    Catch
      i = Err.Number
    End Try

    If i = 0 Then
      nd = TreeView.SelectedNode.Parent.Nodes.Add(Path.GetFileName(dPath))
      nd.Tag = dPath
      nd.ImageIndex = 0
      nd.SelectedImageIndex = 0
      nd.EnsureVisible()
      nd.BeginEdit()
    End If

    If Not dirWatch.Path.StartsWith("\\") Then dirWatch.EnableRaisingEvents = True

  End Sub

  Private Sub mnxTreeviewRenameFolder_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnxTreeviewRenameFolder.Click
    contextMenuNode.BeginEdit()
  End Sub

  Private Sub mnxTreeviewDeleteFolder_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnxTreeviewDeleteFolder.Click
    Dim nFiles, nDir As Integer
    Dim mResult As MsgBoxResult
    Dim s As String = ""

    Try
      nDir = Directory.GetDirectories(contextMenuNode.Tag).Count
      nFiles = Directory.GetFiles(contextMenuNode.Tag).Count
    Catch ex As Exception
      MsgBox(ex.Message)
      Exit Sub
    End Try

    If nDir > 0 Or nFiles > 0 Then
      If nFiles > 1 Then s = "There are " & nFiles & " files "
      If nFiles = 1 Then s = "There is 1 file "
      If nFiles = 0 Then
        If nDir > 1 Then s = "There are " & nDir & " folders "
        If nDir = 1 Then s = "There is 1 folder "
      Else
        If nDir > 1 Then s = s & "and " & nDir & " folders "
        If nDir = 1 Then s = s & "and 1 folder "
      End If
      s = s & "in " & contextMenuNode.Tag & ". Photo Mud will only delete an empty folder."
      mResult = MsgBox(s, MsgBoxStyle.OkOnly)

    Else
      s = "Are you sure you want to delete " & contextMenuNode.Tag & "?"
      mResult = MsgBox(s, MsgBoxStyle.YesNoCancel)
      If mResult = MsgBoxResult.Yes Then
        dirWatch.EnableRaisingEvents = False
        If eqstr(dirWatch.Path, contextMenuNode.Tag) Then dirWatch.Path = Path.GetDirectoryName(contextMenuNode.Tag)
        Try
          Directory.Delete(contextMenuNode.Tag)
        Catch ex As Exception
          MsgBox(ex.Message & crlf & "The folder could not be deleted.")
          dirWatch.Path = contextMenuNode.Tag
          Exit Sub
        End Try
        contextMenuNode.Remove()
        If Not dirWatch.Path.StartsWith("\\") Then dirWatch.EnableRaisingEvents = True
      End If
    End If

  End Sub

  Private Sub TreeView_NodeMouseClick(ByVal sender As Object, ByVal e As TreeNodeMouseClickEventArgs) Handles TreeView.NodeMouseClick
    If e.Button = MouseButtons.Right Then contextMenuNode = e.Node
  End Sub

  Private Sub mnxTreeviewRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnxTreeviewRefresh.Click
    mnuViewRefresh_Click(sender, e)
  End Sub

  Private Sub frmExplore_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed
    frmMain.Close()
  End Sub

  Private Sub ListView1_ColumnClick(ByVal Sender As Object, ByVal e As ColumnClickEventArgs) Handles ListView1.ColumnClick

    Dim v As lvSort
    Dim s As String
    Dim Ascend As String = "  " & ChrW(&H25B2)
    Dim Descend As String = "  " & ChrW(&H25BC)

    v = ListView1.ListViewItemSorter ' this is in main.vb
    If v Is Nothing Then Exit Sub

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
      Case 1 ' size
        v.dataType = "number"
      Case 2, 3 ' date
        v.dataType = "date"
      Case Else ' includes 0 - name
        v.dataType = "" ' string
    End Select

    ListView1.Sort()
    If ListView1.SelectedItems.Count = 1 Then ListView1.SelectedItems(0).EnsureVisible()

  End Sub

  Private Sub mnxTreeviewTagFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles mnxTreeviewTagFolder.Click, mnxTreeviewTagFolderSub.Click

    Me.Cursor = Cursors.WaitCursor
    cleartags()
    If sender Is mnxTreeviewTagFolderSub Then
      tagAll(contextMenuNode.Tag, True)
    Else
      tagAll(contextMenuNode.Tag, False)
    End If
    setStatusLine()
    Me.Cursor = Cursors.Default

  End Sub

  Sub tagAll(ByVal fPath As String, ByVal subFolders As Boolean)

    Dim ss() As String
    Dim s, ext As String
    Dim i As Integer
    Dim item As ListViewItem

    Try
      If Not Directory.Exists(fPath) Then Exit Sub
      ss = Directory.GetFiles(fPath)
    Catch ex As Exception
      MsgBox(ex.Message)
      Exit Sub
    End Try

    If eqstr(fPath, TreeView.SelectedNode.Tag) Then ' tag all local nodes
      processing = True
      For Each item In ListView1.Items
        item.Checked = True
      Next item
      processing = False
      locTagPaths = ListView1.Items.Count
    End If

    For Each s In ss
      ext = Path.GetExtension(s)
      For i = 0 To iniFileType.Count - 1
        If eqstr(iniFileType(i), ext) Then ' tag it
          tagPath.Add(s)
          Exit For
        End If
      Next i
    Next s

    If subFolders Then
      Try
        ss = Directory.GetDirectories(fPath)
      Catch ex As Exception
        MsgBox(ex.Message)
        Exit Sub
      End Try

      For Each s In ss
        tagAll(s, subFolders)
      Next s
    End If

  End Sub

  Private Sub mnxTreeview_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles mnxTreeview.Opening

    'If ListView1.View = View.LargeIcon Then
    '  mnxTreeviewTagFolder.Enabled = False
    '  mnxTreeviewTagFolderSub.Enabled = False
    'Else
    mnxTreeviewTagFolder.Enabled = True
    mnxTreeviewTagFolderSub.Enabled = True
    '  End If

  End Sub

  Private Sub mnxToolStrip_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles mnxToolStrip.Opening

    If iniButtonSize = 0 Then
      mnxTSmallIcons.Checked = True
      mnxTLargeIcons.Checked = False
    Else
      mnxTSmallIcons.Checked = False
      mnxTLargeIcons.Checked = True
    End If

  End Sub

  Private Sub mnuFileCloseAll_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles mnuFileCloseAll.Click
    closeAll()
    setMnuWindows()
  End Sub

  Private Sub ListView1_ItemCheck(ByVal sender As Object, ByVal e As ItemCheckEventArgs) Handles ListView1.ItemCheck

    Dim i, k As Integer
    Dim item As ListViewItem
    Static busy As Boolean

    If itemDoubleClicked Then
      itemDoubleClicked = False
      e.NewValue = e.CurrentValue
      Exit Sub
    End If

    If processing Or Loading Then Exit Sub
    If busy Then Exit Sub
    busy = True

    item = ListView1.Items(e.Index)

    If e.NewValue = CheckState.Checked Then ' add to list
      k = 0
      For i = 0 To tagPath.Count - 1
        If tagPath(i) = item.Tag Then
          k = i
          Exit For
        End If
      Next i
      If k = 0 Then
        locTagPaths = locTagPaths + 1
        tagPath.Add(item.Tag)
      End If

    ElseIf e.NewValue = CheckState.Unchecked Then  ' remove from list
      unTag(item.Tag)
    End If

    setStatusLine()
    enableButtons()

    busy = False

  End Sub

  Private Sub ListView1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles ListView1.MouseDown

    If e.Button = MouseButtons.Left AndAlso e.Clicks >= 2 Then
      itemDoubleClicked = True
    End If

  End Sub

  Private Sub frmExplore_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

    'If frmSplash.Visible Then frmSplash.Close()
    Me.Refresh()
    Me.Cursor = Cursors.Default
    enableButtons()

    If ListView1.Columns.Count >= 3 Then
      ListView1.Columns(0).Width = ListView1.Width * 0.3
      ListView1.Columns(1).Width = ListView1.Width * 0.12
      ListView1.Columns(2).Width = ListView1.Width * 0.17
      ListView1.Columns(3).Width = ListView1.Width * 0.17
    End If

  End Sub

  Private Sub mnuToolsBatchInfoCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuToolsBatchInfoCopy.Click
    If bkgThumb.IsBusy Then thumbPause.Reset()
    If bkgPhotoDates.IsBusy Then photoDatesPause.Reset()
    Using frm As New frmBatchInfoCopy
      dResult = frm.ShowDialog()
    End Using
  End Sub

  Private Sub frmExplore_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles Me.ResizeEnd, Me.Resize
    ' resize catches maximize and normal windowstates, and resizeend catches movement.

    Static busy As Boolean

    If busy Or Form.ActiveForm IsNot Me Then Exit Sub
    If Not Loading Then
      busy = True
      setiniWindowsize(Me)
      busy = False
    End If

  End Sub

  Private Sub mnuEditTagDirMatches_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEditTagDirMatches.Click

    Using frm As New frmTagMatches
      dResult = frm.ShowDialog()
    End Using
    If dResult <> DialogResult.Cancel Then setTags()

  End Sub


  Private Sub mnuToolsBugLinkPhotos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuToolsLinkBugPhotos.Click

    ' link images in the database -- changes database only

    If tagPath.Count <= 1 Then Exit Sub

    linkBugPhotos()
    cleartags()
    Me.Refresh()

  End Sub

  Private Sub mnuToolsBugQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuToolsBugQuery.Click

    useQuery = True
    dirWatch.EnableRaisingEvents = False
    Me.Cursor = Cursors.WaitCursor
    ListViewLoad(TreeView.SelectedNode.Tag)
    Me.Cursor = Cursors.Default

  End Sub

  Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean

    ' alt-left and alt-right should not be intercepted by treeview (or anything else)
    If keyData = (Keys.Alt Or Keys.Left) Or keyData = (Keys.Alt Or Keys.Right) Then
      globalKeydown(New KeyEventArgs(keyData))
      Return True
    End If

    ' alt-d focuses on the File menu regardless of keydown handling without this.
    If keyData = (Keys.Alt Or Keys.D) Then
      globalKeydown(New KeyEventArgs(Keys.Alt Or Keys.D))
      Return True
    Else
      Return MyBase.ProcessDialogKey(keyData)
    End If

  End Function


End Class

