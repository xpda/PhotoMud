'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

' for picload service - load a file from Windows Explorer double-click
Imports System.IO
Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports vb = Microsoft.VisualBasic

Public Class PicLoadService : Implements IPhotoMudPicLoad

Public Function LoadPic(ByVal fName As String) As String Implements IPhotoMudPicLoad.LoadPic

Dim rview As mudViewer
Dim i As Integer
Dim sdb As New ServiceDebugBehavior

sdb.IncludeExceptionDetailInFaults = True

If frmMain.Loading Then
  For i = 1 To 50
    Threading.Thread.Sleep(100)
    If Not frmMain.Loading Then Exit For
    Next i
  End If

rview = FileIsOpen(fName)
If rview Is Nothing Then
  rview = loadNew(fName)  ' open file from double click
  End If

If rview Is Nothing Then
  LoadPic = ""
Else
  LoadPic = fName
  rview.Activate(Nothing, Nothing)
  End If

End Function

End Class

<ServiceContract()> Public Interface IPhotoMudPicLoad
<OperationContract()> Function LoadPic(ByVal fName As String) As String

End Interface
