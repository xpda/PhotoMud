Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports System.IO
Imports System
Imports System.Threading.Tasks
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Threading
Imports ImageMagick

Public Class frmConvert
  Inherits Form

  Dim saveExif As Boolean = True

  Dim abort As Boolean
  Dim processing As Boolean
  Dim Loading As Boolean = True
  Dim pathAssigned As Boolean ' flag not to change path text
  Dim icount As Integer ' number converted

  Dim watermarkFile As String

  Dim ixFmt(fmtCommon.Count - 1) As Integer
  Dim nix As Integer

  Private Sub chkResize_CheckedChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles chkResize.CheckedChanged

    If Loading Then Exit Sub

    If chkResize.Checked Then
      lbXres.Enabled = True
      lbYres.Enabled = True
      nmXres.Enabled = True
      nmYres.Enabled = True
      chkExpandImages.Enabled = True
    Else
      lbXres.Enabled = False
      lbYres.Enabled = False
      nmXres.Enabled = False
      nmYres.Enabled = False
      chkExpandImages.Enabled = False
    End If

  End Sub

  Private Sub cmbFiletype_SelectedIndexChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles cmbFiletype.SelectedIndexChanged

    If Loading Then Exit Sub

    EnableOptions()

  End Sub

  Sub EnableOptions()

    If cmbFiletype.SelectedIndex < 0 Then Exit Sub

    Select Case fmtCommon(ixFmt(cmbFiletype.SelectedIndex)).ID

      Case ".jpg"
        fmJPG.Visible = True
        fmPng.Visible = False

      Case ".png"
        fmJPG.Visible = False
        fmPng.Visible = True

      Case Else
        fmJPG.Visible = False
        fmPng.Visible = False

    End Select

  End Sub

  Private Sub cmdCancel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
    abort = True
    Me.DialogResult = DialogResult.Cancel
    If bkgSave.IsBusy Then
      bkgSave.CancelAsync()
    Else
      Me.Close()
    End If
  End Sub

  Private Sub cmdColor_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdColor.Click

    colorAdjust = False
    clrValue(0) = 0
    clrValue(1) = 0
    clrValue(2) = 0

    Using frm As New frmColorBatchAdjust
      frm.ShowDialog()
    End Using
    lbCount.Text = tagPath.Count & " photos are tagged to be converted."
    If colorAdjust Then lbCount.Text = lbCount.Text & crlf & "Color adjustments will be performed."
    If watermarkFile <> "" Then lbCount.Text = lbCount.Text & crlf & "Watermark will be applied: " & watermarkFile

  End Sub

  Private Sub cmdStart_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdStart.Click

    Dim n, j, i, k As Integer
    Dim targetPath As String
    Dim targetFName As String
    Dim targetExt As String
    Dim namePrefix As String
    Dim overWriteList As List(Of String)

    Dim s As String = ""
    Dim commonPath As String = ""
    Dim success As Boolean

    Dim msg As String = ""
    Dim overwriteFlag As String = ""

    Dim pView As New pViewer ' for convertfile
    Dim v(5) As Object

    If processing Then Exit Sub
    processing = True

    If cmbFiletype.SelectedIndex >= 0 Then
      targetExt = fmtCommon(ixFmt(cmbFiletype.SelectedIndex)).ID
    Else
      targetExt = cmbFiletype.Text
    End If

    namePrefix = txPrefix.Text.Trim

    If getFormat(targetExt) = MagickFormat.Unknown Then
      MsgBox("Unsupported file format.")
      cmbFiletype.Select()
      Exit Sub
    End If

    If TreeView.SelectedNode Is Nothing OrElse txCurrentPath.Text <> TreeView.SelectedNode.Tag Then
      s = txCurrentPath.Text
      i = CheckFolder(s, True)

      If i < 0 Then
        txCurrentPath.Text = TreeView.SelectedNode.Tag
        txCurrentPath.Select()
        processing = False
        Exit Sub
      End If
      pathAssigned = True
      success = TreeViewSelectPath(TreeView, txCurrentPath.Text)
    End If

    targetPath = TreeView.SelectedNode.Tag

    Me.Cursor = Cursors.WaitCursor
    ProgressBar1.Visible = True
    ProgressBar1.Minimum = 0
    ProgressBar1.Maximum = 100
    'If tagPath.Count >= 0 Then ProgressBar1.Maximum = tagPath.Count + 1 Else ProgressBar1.Maximum = 1
    ProgressBar1.Value = 0

    abort = False
    icount = 0

    If chkSubfolders.Checked Then ' get the common subfolder name
      commonPath = LCase(Path.GetDirectoryName(tagPath(1)))
      For i = 0 To tagPath.Count - 1
        s = LCase(tagPath(i))
        k = Len(commonPath)
        If Len(s) < k Then k = Len(s)
        n = 0
        For j = 0 To k - 1
          If s.Chars(j) <> commonPath.Chars(j) Then Exit For
          If s.Chars(j) = "\" Then n = j
        Next j
        commonPath = commonPath.Substring(0, n)
      Next i

      If Not commonPath.EndsWith("\") Then commonPath = ""
    End If

    ' get a list of overwritable files, overridden by overwriteflag.
    overwriteFlag = "" ' ask
    overWriteList = New List(Of String)
    For Each sourceFile As String In tagPath
      targetFName = getTargetFilename(sourceFile, targetPath, commonPath, targetExt, namePrefix)
      If File.Exists(targetFName) Then
        i = askOverwrite(targetFName, True, overwriteFlag)
        If i < 0 Then ' cancel
          cmdCancel_Click(Sender, e)
          Exit Sub
        End If
        If overwriteFlag = "all" Or overwriteFlag = "none" Then Exit For
        If i = 1 Then overWriteList.Add(targetFName) ' OK to write the file
      End If
    Next sourceFile

    v(0) = targetPath
    v(1) = commonPath
    v(2) = targetExt
    v(3) = namePrefix
    v(4) = overwriteFlag
    v(5) = overWriteList
    bkgSave.RunWorkerAsync(v)

  End Sub

  Sub Finish(cancelled As Boolean)

    Me.Cursor = Cursors.Default
    If icount = 1 Then
      MsgBox(icount & " photo was processed.", MsgBoxStyle.OkOnly, "File Convert")
    Else
      MsgBox(icount & " photos were processed.", MsgBoxStyle.OkOnly, "File Convert")
    End If

    If Not cancelled Then
      iniSavePath = TreeView.SelectedNode.Tag
      iniJpgQuality = nmQuality.Value
      If cmbFiletype.SelectedIndex >= 0 Then iniSaveFiletype = fmtCommon(ixFmt(cmbFiletype.SelectedIndex)).ID

      iniSaveResize = chkResize.Checked
      If iniSaveResize Then
        iniSaveXSize = 0 : iniSaveYSize = 0
        If chkResize.Checked Then
          iniSaveXSize = nmXres.Value
          iniSaveYSize = nmYres.Value
        End If
      End If
      Me.DialogResult = DialogResult.OK
    Else
      Me.DialogResult = DialogResult.Cancel
    End If

    Me.Close()

  End Sub

  Private Sub bkgSave_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) _
    Handles bkgSave.ProgressChanged
    If Not ProgressBar1.Visible Then Exit Sub
    If ProgressBar1.Value <> e.ProgressPercentage Then ProgressBar1.Value = e.ProgressPercentage
    icount += 1
  End Sub

  Private Sub bkgSave_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bkgSave.RunWorkerCompleted
    Finish(e.Cancelled)
  End Sub

  Sub saveAll(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bkgSave.DoWork

    Dim targetPath As String
    Dim commonPath As String
    Dim targetExt As String
    Dim namePrefix As String
    Dim overwriteFlag As String
    Dim overwriteList As List(Of String)
    Dim bkg As BackgroundWorker
    Dim n As Integer = 0

    bkg = sender

    targetPath = e.Argument(0)
    commonPath = e.Argument(1)
    targetExt = e.Argument(2)
    namePrefix = e.Argument(3)
    overwriteFlag = e.Argument(4)
    overwriteList = e.Argument(5)

    For Each tfile As String In tagPath

      Dim targetFilename As String
      Dim mResult As MsgBoxResult

      targetFilename = getTargetFilename(tfile, targetPath, commonPath, targetExt, namePrefix)
      If overwriteFlag = "all" OrElse Not File.Exists(targetFilename) OrElse _
        overwriteList.IndexOf(targetFilename) >= 0 Then
        mResult = saveit(tfile, targetFilename)
        If mResult = MsgBoxResult.Cancel Then Exit Sub

        n += 1
        bkg.ReportProgress(n * 100 \ tagPath.Count)
      End If

    Next tfile

  End Sub

  Function saveit(sourceFile As String, targetFilename As String) As MsgBoxResult

    ' save the file "sourceFile"

    Dim saver As ImageSave
    Dim mResult As MsgBoxResult
    Dim pView As New pViewer ' for convertfile, text
    Dim Xres, Yres As Integer
    Dim msg As String = ""
    Dim picinfo As pictureInfo

    Dim tSize As SizeF
    Dim textFmt As StringFormat
    Dim isize As Integer = 45
    Dim textP As Point

    Dim gBitMap As Bitmap = Nothing
    Dim bmpFrames As New List(Of Bitmap)

    saver = New ImageSave
    saver.Quality = nmQuality.Value
    saver.saveExif = chkExif.Checked
    saver.compressionAmount = nmPngCompression.Value
    saver.PngIndexed = chkPngIndexed.Checked

    Xres = 0 : Yres = 0
    If chkResize.Checked Then
      Xres = nmXres.Value
      Yres = nmYres.Value
    End If

    picinfo = getPicinfo(sourceFile, True)
    If picinfo.hasPages Then
      bmpFrames = readMultiframeBitmap(sourceFile, msg)
      If bmpFrames.Count > 0 Then gBitMap = bmpFrames(0) Else gBitMap = Nothing
    Else ' single image
      gBitMap = readBitmap(sourceFile, msg)
    End If

    If chkExif.Checked Then saver.pComments = readPropertyItems(sourceFile)

    If gBitMap Is Nothing Then
      mResult = MsgBox(sourceFile & " could not be loaded. Press OK to continue, Cancel to stop." & crlf & msg, MsgBoxStyle.OkCancel)
      If mResult = MsgBoxResult.Cancel Then
        clearBitmap(gBitMap)
        For Each bmp As Bitmap In bmpFrames : clearBitmap(bmp) : Next bmp
        Return mResult
      End If
    Else
      If Xres > gBitMap.Width And Yres > gBitMap.Height And (Not chkExpandImages.Checked) Then
        saver.saveSize = Size.Empty
      Else
        saver.saveSize = New Size(Xres, Yres)
      End If

      If colorAdjust Then
        pView = New pViewer
        If Not picinfo.hasPages Then
          batchAdjust(gBitMap, pView) ' uses crlvalue() from frmColorBatchAdjust to change color stuff
        Else
          For Each bmp As Bitmap In bmpFrames : batchAdjust(bmp, pView) : Next bmp
        End If
        pView.Dispose()
      End If

      If watermarkFile <> "" Then

        Using g As Graphics = Graphics.FromImage(gBitMap)
          ' watermarkfile is text string, temporarily
          isize = 90
          pView.Font = getFont(1, "Lucida Console", isize, False, False, False)
          textFmt = StringFormat.GenericTypographic
          textFmt.Alignment = StringAlignment.Center
          textFmt.LineAlignment = StringAlignment.Center
          textFmt.Trimming = StringTrimming.None
          textFmt.FormatFlags = StringFormatFlags.MeasureTrailingSpaces Or StringFormatFlags.NoClip Or StringFormatFlags.FitBlackBox
          tSize = Size.Round(g.MeasureString(watermarkFile, pView.Font, New Point(isize \ 2, isize \ 2), textFmt))
          textP = New Point(tSize.Height * 2 + tSize.Width \ 2, g.VisibleClipBounds.Height - tSize.Height * 1.5)

          pView.DrawText(g, watermarkFile, textP,
                         pView.Font, Color.Beige, Color.Black, False, 0, textFmt)


        End Using
        '        Using bmpWatermark As Bitmap = readBitmap(watermarkFile, msg)
        ' Dim r As Rectangle
        ' r.X = 0
        ' r.Y = gBitMap.Height - bmpWatermark.Height ' lower left
        ' r.Width = bmpWatermark.Width
        ' r.Height = bmpWatermark.Height
        ' bmpMerge(bmpWatermark, gBitMap, mergeOp.opReplaceNonzero, r)
        ' End Using
      End If

      saver.sourceFilename = sourceFile
      If Not picinfo.hasPages Then
        msg = saver.write(gBitMap, targetFilename, True)
      Else
        msg = saver.write(gBitMap, targetFilename, True, bmpFrames)
      End If

      clearBitmap(gBitMap)
      For Each bmp As Bitmap In bmpFrames : clearBitmap(bmp) : Next bmp

      If msg <> "" Then
        MsgBox(msg)
        Return MsgBoxResult.Cancel
      End If
    End If

    Return mResult

  End Function

  Function getTargetFilename(sourceFile As String, targetPath As String, commonPath As String, _
      targetExt As String, namePrefix As String) As String

    Dim targetName As String
    Dim subpath As String = ""
    Dim s As String

    targetName = Path.GetFileName(sourceFile)
    s = Path.GetDirectoryName(sourceFile)
    If s.Chars(Len(s) - 1) <> "\" Then s = s & "\"

    targetName = Path.ChangeExtension(targetName, targetExt)
    If eqstr(Path.GetExtension(sourceFile), ".jpeg") And eqstr(Path.GetExtension(targetName), ".jpg") Then
      targetName = targetName.Substring(0, targetName.Length - 3) & "jpeg"
    End If

    If namePrefix <> "" Then targetName = Trim(namePrefix) & targetName

    If chkSubfolders.Checked Then
      subpath = Mid(s, Len(commonPath))
      If CheckFolder(targetPath & subpath, False) < 0 Then subpath = ""
    End If

    Return targetPath & subpath & "\" & targetName

  End Function

  Private Sub fmConvert_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Dim i, k As Integer
    Dim node As TreeNode

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Loading = True
    processing = False

    bkgSave.WorkerReportsProgress = True
    bkgSave.WorkerSupportsCancellation = True

    'Me.Height = Me.Height - ((fmPng.Location.Y + fmPng.Size.Height) - (fmResize.Location.Y + fmResize.Size.Height))
    Me.Height = Me.Height - ((fmPng.Location.Y + fmPng.Size.Height) - (chkSubfolders.Location.Y + chkSubfolders.Size.Height)) + 20
    If Me.Height < 650 Then Me.Height = 650
    fmPng.Left = fmJPG.Left : fmPng.Top = fmJPG.Top

    nix = -1
    For i = 0 To fmtCommon.Count - 1
      If fmtCommon(i).isWritable Then
        nix = nix + 1
        ixFmt(nix) = i
        cmbFiletype.Items.Insert(nix, fmtCommon(i).Description & " (" & fmtCommon(i).Ext & ")")
        If cmbFiletype.SelectedIndex < 0 Then
          If eqstr(fmtCommon(i).ID, iniSaveFiletype) Then cmbFiletype.SelectedIndex = nix '' multiple ext
          If eqstr(fmtCommon(i).ID, ".jpg") Then k = nix ' default to .jpg
        End If
      End If
    Next i

    If cmbFiletype.SelectedIndex < 0 Then cmbFiletype.SelectedIndex = k

    EnableOptions()

    chkPngIndexed.Checked = iniPngIndexed
    nmPngCompression.Value = iniPngCompression

    nmQuality.Value = iniJpgQuality
    lbCompression.Text = "0 is highest compression and lowest quality." & crlf & _
      "100 is highest quality and lowest compression." & crlf & "95 is a good value for most applications."

    For Each node In frmExplore.TreeView.Nodes
      TreeView.Nodes.Add(node.Clone) ' copies subtree, also
    Next node

    txPrefix.Text = ""

    ProgressBar1.Visible = False
    ProgressBar1.Value = ProgressBar1.Minimum

    lbCount.Text = tagPath.Count & " photos are tagged to be converted."

    chkResize.Checked = iniSaveResize
    nmXres.Value = iniSaveXSize
    nmYres.Value = iniSaveYSize

    If chkResize.Checked Then
      lbXres.Enabled = True
      lbYres.Enabled = True
      nmXres.Enabled = True
      nmYres.Enabled = True
      chkExpandImages.Enabled = True
    Else
      lbXres.Enabled = False
      lbYres.Enabled = False
      nmXres.Enabled = False
      nmYres.Enabled = False
      chkExpandImages.Enabled = False
    End If

    colorAdjust = False
    watermarkFile = ""

    If iniSavePath = "" Then
      txCurrentPath.Text = iniExplorePath
    Else
      txCurrentPath.Text = iniSavePath
    End If
    If txCurrentPath.Text.EndsWith(":") Then txCurrentPath.Text = txCurrentPath.Text & "\"

    TreeViewInit(TreeView, txCurrentPath.Text) ' initialize treeview with drives, etc.
    Loading = False

  End Sub

  Private Sub txtCurrentPath_Enter(ByVal Sender As Object, ByVal e As EventArgs) Handles txCurrentPath.Enter

    txCurrentPath.SelectionStart = 0
    txCurrentPath.SelectionLength = Len(txCurrentPath.Text)

  End Sub

  Private Sub txtCurrentPath_Leave(ByVal Sender As Object, ByVal e As EventArgs) Handles txCurrentPath.Leave

    Dim i As Integer
    Dim s As String
    Dim success As Boolean

    If processing Then Exit Sub

    If txCurrentPath.Text.EndsWith(":") Then txCurrentPath.Text = txCurrentPath.Text & "\"

    s = txCurrentPath.Text
    i = CheckFolder(s, True)

    If i < 0 Then
      txCurrentPath.Text = TreeView.SelectedNode.Tag
      txCurrentPath.Select()
    Else
      If i = 1 Then ' directory created -- add to treeview
        processing = True
        TreeViewInit(TreeView, txCurrentPath.Text) ' initialize treeview with drives, etc.
        processing = False
      Else
        pathAssigned = True
        success = TreeViewSelectPath(TreeView, txCurrentPath.Text)
      End If
    End If

  End Sub

  Private Sub TreeView_AfterSelect(ByVal Sender As Object, ByVal e As TreeViewEventArgs) Handles TreeView.AfterSelect

    If (Not (e.Node Is Nothing)) And (Not processing) Then
      Me.Cursor = Cursors.WaitCursor

      If Not pathAssigned Then
        txCurrentPath.Text = e.Node.Tag
      Else
        pathAssigned = False
      End If

      loadNode(e.Node)
      e.Node.ImageIndex = 1
      e.Node.SelectedImageIndex = 1
      txCurrentPath.Text = e.Node.Tag
      Me.Cursor = Cursors.Default

    End If

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

  Private Sub chkPNGIndexed_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkPngIndexed.CheckedChanged
    If Loading Then Exit Sub
    iniPngIndexed = chkPngIndexed.Checked
  End Sub

  Private Sub cmdWatermark_Click(sender As Object, e As EventArgs) Handles cmdWatermark.Click

    'fnames = InputFilename(False)
    'If fnames.Count = 1 Then watermarkFile = fnames(0) Else watermarkFile = ""

    watermarkFile = "Watermark"

    lbCount.Text = tagPath.Count & " photos are tagged to be converted."
    If colorAdjust Then lbCount.Text = lbCount.Text & crlf & "Color adjustments will be performed."
    If watermarkFile <> "" Then lbCount.Text = lbCount.Text & crlf & "Watermark will be applied: " & watermarkFile

  End Sub

End Class