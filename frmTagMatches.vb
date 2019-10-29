'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports vb = Microsoft.VisualBasic
Imports System.IO
Imports System.Collections.Generic

Public Class frmTagMatches

  Dim Processing As Boolean = False

  Dim pathAssigned As Boolean

  Dim nTagged As Integer

  Private Sub frmTagMatches_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    Dim node As TreeNode

    ProgressBar1.Visible = False

    Label2.Text = "This function tags each photo in the destination folder that " & crlf & _
    "has the same file name as a photo in the source folder. "

    For Each node In frmExplore.TreeView.Nodes
      TreeViewDest.Nodes.Add(node.Clone) ' copies subtree, also
    Next node

    txSourcePath.Text = iniExplorePath
    If vb.Right(txSourcePath.Text, 1) = ":" Then txSourcePath.Text = txSourcePath.Text & "\"
    TreeViewInit(TreeViewSource, txSourcePath.Text) ' initialize treeview with drives, etc.

    If Not useQuery Then
      If tagMatchPath = "" Then
        txDestPath.Text = iniExplorePath
      Else
        txDestPath.Text = tagMatchPath
      End If
      If vb.Right(txDestPath.Text, 1) = ":" Then txDestPath.Text = txDestPath.Text & "\"
      TreeViewInit(TreeViewDest, txDestPath.Text) ' initialize treeview with drives, etc.

    Else ' skip dest if useQuery
      lbDestPath.Visible = False
      txDestPath.Visible = False
      TreeViewDest.Visible = False
    End If

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

  Private Sub txCurrentPath_Leave(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles txDestPath.Leave, txSourcePath.Leave

    Dim i As Integer
    Dim s As String
    Dim tx As TextBox
    Dim tv As TreeView
    Dim success As Boolean

    If Processing Then Exit Sub

    tx = Sender
    If tx Is txSourcePath Then tv = TreeViewSource Else tv = TreeViewDest

    If vb.Right(tx.Text, 1) = ":" Then tx.Text = tx.Text & "\"

    s = tx.Text
    i = CheckFolder(s, True)

    If i < 0 Then
      tx.Text = tv.SelectedNode.Tag
      tx.Select()
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
    tx = Sender
    tx.SelectionStart = 0
    tx.SelectionLength = Len(txDestPath.Text)

  End Sub

  Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click

    Dim sourceFiles As New List(Of String)
    Dim destFiles As New List(Of String)
    Dim ixSource As New List(Of Integer)
    Dim ixDest As New List(Of Integer)
    Dim fileNames As New List(Of String)

    Dim i, j, k As Integer
    Dim fName As String
    Dim s As String
    Dim alreadyTagged As Boolean

    Me.Cursor = Cursors.WaitCursor
    nTagged = 0
    ProgressBar1.Value = 0
    ProgressBar1.Visible = True

    If txSourcePath.Focused Then
      txCurrentPath_Leave(txSourcePath, e)
    ElseIf txDestPath.Focused Then
      txCurrentPath_Leave(txDestPath, e)
    End If

    ' get filenames
    i = getFilePaths(txSourcePath.Text, sourceFiles, False)
    If i < 0 Then Exit Sub

    If useQuery Then ' for bugquery 
      For i = 0 To sourceFiles.Count - 1 ' remove folder name
        sourceFiles(i) = Path.GetFileName(sourceFiles(i))
      Next i

      getQueryPaths(fileNames, False)

      For Each fName In fileNames
        s = Path.GetFileName(fName)
        i = sourceFiles.IndexOf(s)
        If i >= 0 Then
          tagPath.Add(fName)
          nTagged = nTagged + 1
        End If
      Next fName


    Else ' normal way
      i = getFilePaths(txDestPath.Text, destFiles, False)
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

      ' tag each filename that is in both folders
      k = 0
      For i = 0 To sourceFiles.Count - 1
        ProgressBar1.Value = i * 100 / sourceFiles.Count
        For j = k To destFiles.Count - 1
          k = j
          Select Case String.Compare(sourceFiles(ixSource(i)), destFiles(ixDest(j)), True)
            Case 0 ' match
              alreadyTagged = False
              For Each s In tagPath
                If String.Compare(txDestPath.Text & "\" & destFiles(ixDest(j)), s, True) = 0 Then
                  alreadyTagged = True
                  Exit For
                End If
              Next s
              If Not alreadyTagged Then
                tagPath.Add(txDestPath.Text & "\" & destFiles(ixDest(j)))
                nTagged = nTagged + 1
              End If
            Case -1 ' source < dest)
              Exit For
          End Select
        Next j
      Next i
    End If

    MsgBox(nTagged & " photos were tagged.")
    Me.Cursor = Cursors.Default
    ProgressBar1.Visible = False
    tagMatchPath = txDestPath.Text

  End Sub

  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.DialogResult = DialogResult.OK
    Me.Close()
  End Sub
End Class
