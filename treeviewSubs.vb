Imports System.IO
Imports vb = Microsoft.VisualBasic

Module treeviewSubs

  Sub TreeViewInit(ByRef treeView As TreeView, ByVal mPath As String, Optional ByRef dirWatch As FileSystemWatcher = Nothing)
    ' load the tree, mPath is the active path
    ' in treeview, .text has the short folder name, .tag as the complete path

    Dim tNode As TreeNode = Nothing
    Dim d As DriveInfo
    Dim s As String = ""
    Dim nd As TreeNode
    Dim Success As Boolean

    treeView.Nodes.Clear()

    Try
      s = My.Computer.FileSystem.SpecialDirectories.Desktop
    Catch
      s = ""
    End Try

    If Len(s) > 0 Then
      tNode = treeView.Nodes.Add("Desktop")
      tNode.Tag = Path.GetFullPath(s)
      tNode.ImageIndex = 0
      tNode.SelectedImageIndex = 0
    End If

    Try
      s = My.Computer.FileSystem.SpecialDirectories.MyPictures
    Catch
      s = ""
    End Try

    If Len(s) > 0 Then
      tNode = treeView.Nodes.Add("My Pictures")
      tNode.Tag = Path.GetFullPath(s)
      tNode.ImageIndex = 0
      tNode.SelectedImageIndex = 0
    End If

    ' load drives into treeview
    For Each d In DriveInfo.GetDrives
      If String.Compare(d.Name, "a:\", True) <> 0 OrElse d.DriveType <> DriveType.Removable Then
        If d.IsReady Then
          s = d.VolumeLabel & " (" & d.Name
          If vb.Right(s, 1) = "\" Then Mid(s, Len(s), 1) = ")" Else s = s & ")"
          tNode = treeView.Nodes.Add(s)
          tNode.Tag = Path.GetFullPath(d.Name)
          tNode.ImageIndex = 0
          tNode.SelectedImageIndex = 0
        End If
      End If
    Next d

    If Left(mPath, 2) = "\\" Then ' network path -- add it.
      s = Path.GetPathRoot(mPath)
      tNode = treeView.Nodes.Add(s)
      tNode.Tag = Path.GetFullPath(s)
      tNode.ImageIndex = 0
      tNode.SelectedImageIndex = 0
    End If

    Success = TreeViewSelectPath(treeView, mPath, dirWatch)

    Try
      s = Path.GetPathRoot(mPath)
    Catch ex As Exception
      s = ""
    End Try

    For Each nd In treeView.Nodes
      If eqstr(s, nd.Tag) Then ' found the drive
        Success = TreeViewSelectPath(treeView, mPath, dirWatch, nd)
        Exit For
      End If
    Next nd

  End Sub

  Function getDriveNode(ByRef treeView As TreeView, ByVal fPath As String) As TreeNode

    Dim s As String
    Dim tNode As TreeNode

    getDriveNode = Nothing

    If Trim(fPath) = "" Then Exit Function
    If Not Directory.Exists(fPath) Then Exit Function

    Try
      s = Path.GetPathRoot(fPath)

      For Each tNode In treeView.Nodes
        If eqstr(s, tNode.Tag) Then ' found the drive
          getDriveNode = tNode
          Exit For
        End If
      Next tNode
    Catch ex As System.IO.IOException
    End Try

    If getDriveNode Is Nothing And vb.Left(fPath, 2) = "\\" Then ' add network drive
      s = Path.GetPathRoot(fPath)
      tNode = treeView.Nodes.Add(s)
      tNode.Tag = Path.GetFullPath(s)
      tNode.ImageIndex = 0
      tNode.SelectedImageIndex = 0
      getDriveNode = tNode
    End If

  End Function

  Function TreeViewSelectPath(ByRef treeView As TreeView, ByVal fPath As String, Optional ByRef dirWatch As FileSystemWatcher = Nothing, Optional ByVal srchNode As TreeNode = Nothing) As Boolean
    ' load treeview with a path, return success
    ' search from searchnode, or from the drive node if searchnode is empty
    ' returns true if it found the folder, false if not.

    Dim nd As TreeNode
    Dim s As String
    Dim success As Boolean

    If Trim(fPath) = "" Then Return False
    TreeViewSelectPath = True
    If (Len(fPath) > 1) AndAlso (Len(fPath) > 3 Or fPath.Chars(1) <> ":") Then fPath = fPath.TrimEnd("\")

    If Not Directory.Exists(fPath) Then
      TreeViewSelectPath = False
      Exit Function ' don't load if the path is invalid
    End If

    If dirWatch IsNot Nothing Then
      dirWatch.EnableRaisingEvents = False
      dirWatch.Path = fPath
    End If
    If srchNode Is Nothing Then srchNode = getDriveNode(treeView, fPath)
    If srchNode Is Nothing Then
      TreeViewSelectPath = False
      Exit Function
    End If

    If eqstr(srchNode.Tag, fPath) Then ' the drive is the node
      treeView.SelectedNode = srchNode


      If dirWatch IsNot Nothing Then
        If dirWatch.Path.StartsWith("\\") Then
          dirWatch.EnableRaisingEvents = False
        Else
          dirWatch.EnableRaisingEvents = True
        End If
      End If
      Exit Function
    End If

    ' first look inside the existing tree for fpath.
    For Each nd In srchNode.Nodes
      s = inPath(nd.Tag, fPath)
      If s <> "unequal" Then
        If s = "equal" Then ' found it
          treeView.SelectedNode = nd
        Else ' s = subfolder
          success = TreeViewSelectPath(treeView, fPath, dirWatch, nd) ' recursive
        End If
        If dirWatch IsNot Nothing Then
          Try
            If dirWatch.Path.StartsWith("\\") Then
              dirWatch.EnableRaisingEvents = False
            Else
              dirWatch.EnableRaisingEvents = True
            End If
          Catch ex As Exception
          End Try
        End If
        Exit Function
      End If
    Next nd

    ' The node is not in treeView. Look in the folder and load nodes onto treeview if it's found.
    loadNode(srchNode) ' load the nodes in srchnode with the folder contents

    For Each nd In srchNode.Nodes
      s = inPath(nd.Tag, fPath)
      If s <> "unequal" Then
        If s = "equal" Then ' found it
          treeView.SelectedNode = nd
        Else ' s = subfolder
          success = TreeViewSelectPath(treeView, fPath, dirWatch, nd) ' recursive
        End If
        If dirWatch IsNot Nothing Then
          Try
            If dirWatch.Path.StartsWith("\\") Then
              dirWatch.EnableRaisingEvents = False
            Else
              dirWatch.EnableRaisingEvents = True
            End If
          Catch ex As Exception
          End Try
        End If
        Exit Function
      End If
    Next nd

    If dirWatch IsNot Nothing Then
      If dirWatch.Path.StartsWith("\\") Then
        dirWatch.EnableRaisingEvents = False
      Else
        dirWatch.EnableRaisingEvents = True
      End If
    End If

    TreeViewSelectPath = False ' couldn't find the folder

  End Function

  Function inPath(ByVal nodePath As String, ByVal fullPath As String) As String
    ' returns "equal", "subfolder" (fullpath is under nodepath), or "unequal"

    Dim s As String

    If eqstr(nodePath, fullPath) Then ' matches
      inPath = "equal"
      Exit Function
    End If

    If Len(nodePath) = 0 Or Len(fullPath) = 0 Or Len(nodePath) >= Len(fullPath) Then
      inPath = "unequal"
      Exit Function
    End If

    s = Path.GetDirectoryName(fullPath)
    If inPath(nodePath, s) = "unequal" Then
      inPath = "unequal"
    Else
      inPath = "subfolder"
    End If

  End Function

  Sub loadNode(ByRef kNode As TreeNode)

    Dim sPath As String
    Dim tNode As TreeNode
    Dim s As String
    Static busy As Boolean = False

    If busy Then Exit Sub
    busy = True

    ' remove the nodes that shouldn't be there (in case of external folder delete/rename)
    For Each tNode In kNode.Nodes
      If tNode IsNot Nothing AndAlso Not Directory.Exists(tNode.Tag) Then kNode.Nodes.Remove(tNode)
    Next tNode

    Try
      If Directory.Exists(kNode.Tag) Then
        Directory.GetDirectories(kNode.Tag)

        ' add new nodes
        For Each s In Directory.GetDirectories(kNode.Tag)
          ' is it already there?
          sPath = s
          For Each tNode In kNode.Nodes
            If eqstr(tNode.Tag, sPath) Then
              sPath = ""
              Exit For
            End If
          Next tNode

          If sPath <> "" Then ' not already there
            tNode = kNode.Nodes.Add(Path.GetFileName(sPath))
            tNode.Tag = Path.GetFullPath(sPath)
            tNode.ImageIndex = 0
            tNode.SelectedImageIndex = 0
          End If
        Next s
      End If
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    busy = False
  End Sub

  Function TreeViewfind(ByVal treeView As TreeView, ByVal fPath As String, Optional ByVal srchNode As TreeNode = Nothing) As TreeNode
    ' returns a node in TreeView.
    ' search fpath, and use srchnode if it's available, drive nodes if not

    Dim nd As TreeNode

    TreeViewfind = Nothing
    If srchNode Is Nothing Then srchNode = getDriveNode(treeView, fPath)

    If srchNode IsNot Nothing Then
      For Each nd In srchNode.Nodes
        If eqstr(nd.Tag, vb.Left(fPath, Len(nd.Tag))) Then ' found the node
          If (Len(nd.Tag) < Len(fPath)) Then
            TreeViewfind = TreeViewfind(treeView, fPath, nd) ' recursive
          Else
            TreeViewfind = nd
          End If
          Exit For
        End If
      Next nd
    End If

  End Function

End Module
