Imports vb = Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Threading
Imports System.Drawing.Imaging
Imports System.IO
Imports ImageMagick

Public Class frmBatchInfoCopy

  Dim Processing As Boolean = False
  Dim Loading As Boolean = True

  Dim pathAssigned As Boolean

  Dim abort As Boolean = False

  Private Sub frmBatchInfoCopy_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    Dim node As TreeNode

    ProgressBar1.Visible = False
    ProgressBar1.Minimum = 0
    ProgressBar1.Maximum = 100

    bkgSave.WorkerReportsProgress = True
    bkgSave.WorkerSupportsCancellation = True

    Label2.Text = "This function copies the photo information, such as " & crlf & _
    "comments, exif info, etc., from the photos in one folder " & crlf & _
    "to the photos in another folder with identical names. "

    For Each node In frmExplore.TreeView.Nodes
      TreeViewDest.Nodes.Add(node.Clone) ' copies subtree, also
    Next node

    ' source path default
    txSourcePath.Text = iniExplorePath ' dest default = current folder 
    If vb.Right(txSourcePath.Text, 1) = ":" Then txSourcePath.Text = txDestPath.Text & "\"
    TreeViewInit(TreeViewSource, txSourcePath.Text) ' initialize treeview with drives, etc.

    ' dest default = current folder 
    txDestPath.Text = iniExplorePath ' dest default = current folder 
    If vb.Right(txDestPath.Text, 1) = ":" Then txDestPath.Text = txDestPath.Text & "\"
    TreeViewInit(TreeViewDest, txDestPath.Text) ' initialize treeview with drives, etc.

    Loading = False

  End Sub

  Private Sub TreeView_AfterSelect(ByVal Sender As Object, ByVal e As TreeViewEventArgs) _
    Handles TreeViewSource.AfterSelect, TreeViewDest.AfterSelect

    Dim tx As TextBox

    If Sender Is TreeViewSource Then tx = txSourcePath Else tx = txDestPath

    If (Not (e.Node Is Nothing)) And (Not Processing) Then
      Me.Cursor = Cursors.WaitCursor

      If Not pathAssigned Then
        tx.Text = e.Node.Tag
      Else
        pathAssigned = False
      End If

      loadNode(e.Node)
      e.Node.ImageIndex = 1
      e.Node.SelectedImageIndex = 1
      tx.Text = e.Node.Tag
      Me.Cursor = Cursors.Default

    End If

  End Sub

  Private Sub TreeView_BeforeSelect(ByVal sender As Object, ByVal e As TreeViewCancelEventArgs) _
    Handles TreeViewSource.BeforeSelect, TreeViewDest.BeforeSelect

    Dim tv As TreeView

    tv = sender

    If tv.SelectedNode IsNot Nothing Then
      tv.SelectedNode.ImageIndex = 0
      tv.SelectedNode.SelectedImageIndex = 0
    End If
  End Sub

  Private Sub TreeView_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As TreeNodeMouseClickEventArgs) _
    Handles TreeViewSource.NodeMouseDoubleClick, TreeViewDest.NodeMouseDoubleClick
    e.Node.Expand()
  End Sub

  Private Sub txDestPath_Leave(sender As Object, e As EventArgs) Handles txDestPath.Leave, txSourcePath.Leave

    Dim i As Integer
    Dim s As String
    Dim tx As TextBox
    Dim tv As TreeView
    Dim success As Boolean

    If Processing Or Loading Then Exit Sub

    tx = Sender
    If tx Is txSourcePath Then tv = TreeViewSource Else tv = TreeViewDest

    If vb.Right(tx.Text, 1) = ":" Then tx.Text = tx.Text & "\"

    s = tx.Text
    i = CheckFolder(s, True)

    If i < 0 Then
      tx.Text = tv.SelectedNode.Tag
      tx.select()
    Else
      If i = 1 Then ' directory created -- add to treeview
        Processing = True
        TreeViewInit(tv, tx.Text) ' initialize treeview with drives, etc.
        Processing = False
      Else
        pathAssigned = True
        success = TreeViewSelectPath(tv, tx.Text)
      End If
    End If

  End Sub

  Private Sub txtCurrentPath_Enter(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles txSourcePath.Enter, txDestPath.Enter

    Dim tx As TextBox
    If Loading Then Exit Sub
    tx = Sender
    tx.SelectionStart = 0
    tx.SelectionLength = Len(txDestPath.Text)

  End Sub

  Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click

    Dim v(1) As Object

    If Loading Then Exit Sub
    If Not cmdStart.Focused Then cmdStart.Select()

    Me.Cursor = Cursors.WaitCursor
    ProgressBar1.Value = 0
    ProgressBar1.Visible = True

    v(0) = txSourcePath.Text.Trim
    v(1) = txDestPath.Text.Trim
    bkgSave.RunWorkerAsync(v)

  End Sub

  Sub transfer(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bkgSave.DoWork

    Dim i, j, k, result As Integer
    Dim ixSource As New List(Of Integer)
    Dim ixDest As New List(Of Integer)
    Dim nMoved As Integer = 0
    Dim n As Integer = 0
    Dim sourceFiles As New List(Of String)
    Dim destFiles As New List(Of String)
    Dim sourcePath, destPath As String

    Dim bkg As BackgroundWorker

    bkg = sender
    sourcePath = e.Argument(0)
    destPath = e.Argument(1)

    ' get filenames
    i = getFilePaths(sourcePath, sourceFiles, False)
    If i < 0 Then Exit Sub
    i = getFilePaths(destPath, destFiles, False)
    If i < 0 Then Exit Sub

    ' sort results
    For i = 0 To sourceFiles.Count - 1
      ixSource.Add(i)
      sourceFiles(i) = Path.GetFileName(sourceFiles(i)) ' get rid of folder part
    Next i
    MergeSort(sourceFiles, ixSource, 0, sourceFiles.Count - 1)

    For i = 0 To destFiles.Count - 1
      ixDest.Add(i)
      destFiles(i) = Path.GetFileName(destFiles(i)) ' get rid of folder part
    Next i
    MergeSort(destFiles, ixDest, 0, destFiles.Count - 1)

    ' movecomments for each filename that is in both folders
    k = 0
    For i = 0 To sourceFiles.Count - 1
      For j = k To destFiles.Count - 1
        If abort Then
          Me.DialogResult = DialogResult.Cancel
          Exit Sub
        End If

        k = j
        Select Case String.Compare(sourceFiles(ixSource(i)), destFiles(ixDest(j)), True)
          Case 0 ' match
            result = moveComments(sourcePath & "\" & sourceFiles(ixSource(i)), _
                                  destPath & "\" & sourceFiles(ixSource(i)), nMoved)
            If result < 0 Then
              Me.Cursor = Cursors.Default
              Exit For
            End If
          Case -1 ' source < dest)
            Exit For
        End Select
      Next j
      n += 1
      bkg.ReportProgress(n * 100 \ sourceFiles.Count)
    Next i

    MsgBox("Information was moved to " & nMoved & " photos.")

  End Sub

  Function moveComments(sourceFile As String, destFile As String, ByRef nMoved As Integer) As Integer
    ' copies the comments from sourceFile to destFile

    Dim pComments As New List(Of PropertyItem)
    Dim saver As New ImageSave
    Dim msg As String = ""
    Dim mResult As MsgBoxResult
    Dim copied As Boolean
    Dim bmp As Bitmap = Nothing
    Dim v As Object
    Dim s As String

    pComments = readPropertyItems(sourceFile)
    If pComments.Count <= 2 Then Return 0
    copied = False

    Using iStream As New FileStream(destFile, FileMode.Open, FileAccess.Read)
      bmp = Image.FromStream(iStream, True, False)
      Set32bppPArgb(bmp) ' prevents write error?
    End Using
    If chkCommentOnly.Checked Then
      For Each p As PropertyItem In pComments

        If p.Id = propID.ImageDescription Then
          s = "" ' only copy non-blank comments in comment-only copy
          If p.Type = 2 Then ' string - should always be this
            v = getTagValue(p)
            If v IsNot Nothing AndAlso UBound(v) >= 0 Then s = v(0)
          End If
          If s <> "" Then attachPropertyItems(bmp, pComments)
          bmp.SetPropertyItem(p)
          copied = True
          Exit For
        End If
      Next p
    Else ' copy all comments
      attachPropertyItems(bmp, pComments)
      copied = True
    End If

    saver.Quality = 99 ' ignore ini
    If copied Then
      msg = saver.write(bmp, destFile, True)
      nMoved += 1
    End If
    clearBitmap(bmp)

    If msg <> "" Then
      mResult = MsgBox(destFile & " could not be saved." & crlf & msg, MsgBoxStyle.OkCancel)
      If mResult = MsgBoxResult.Cancel Then Return -1 Else Return 0
    End If

    Return 0

  End Function

  Sub finish(cancelled As Boolean)
    Me.Cursor = Cursors.Default
    If cancelled Then
      Me.DialogResult = DialogResult.Cancel
    Else
      Me.DialogResult = DialogResult.OK
    End If

    Me.Close()
  End Sub

  Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
    abort = True
    Me.DialogResult = DialogResult.Cancel
    If bkgSave.IsBusy Then
      bkgSave.CancelAsync()
    Else
      Me.Close()
    End If
  End Sub

  Private Sub bkgSave_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) _
    Handles bkgSave.ProgressChanged
    If Not ProgressBar1.Visible Then Exit Sub
    If ProgressBar1.Value <> e.ProgressPercentage Then ProgressBar1.Value = e.ProgressPercentage
  End Sub

  Private Sub bkgSave_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bkgSave.RunWorkerCompleted
    finish(e.Cancelled)
  End Sub

  Private Sub frmBatchInfoCopy_Shown(sender As Object, e As EventArgs) Handles Me.Shown
    txSourcePath.Select()
  End Sub

End Class