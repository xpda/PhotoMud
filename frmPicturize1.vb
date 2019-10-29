'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports vb = Microsoft.VisualBasic
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports System.IO
Imports System.Collections.Generic

Public Class frmPicturize1
  Inherits Form

  ' pcz... variables are global

  Private Sub cmdBrowse_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdBrowse.Click
    'Opens a folder browser

    Dim folderBrowser1 As New FolderBrowserDialog
    Dim result As DialogResult
    Dim ss As New List(Of String)

    folderBrowser1.SelectedPath = txtCellFolder.Text
    result = folderBrowser1.ShowDialog()

    pczCellFolder = ""
    If (result = DialogResult.OK) Then
      pczCellFolder = folderBrowser1.SelectedPath
    End If

    If pczCellFolder <> "" Then
      If vb.Right(pczCellFolder, 1) <> "\" Then pczCellFolder = pczCellFolder & "\"
    End If

    txtCellFolder.Text = pczCellFolder

    ss.Add(".jpg")
    ss.Add(".jpeg")
    pcznCellPics = countfiles(pczCellFolder, ss)
    lbNPics.Text = "Contains " & pcznCellPics & " image files."

  End Sub

  Private Sub cmdHelp_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdHelp.Click
    Try
      Help.ShowHelp(Me, helpFile, HelpNavigator.Topic, "photomosaic" & ".html") ' same help for three dialogs
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

  Private Sub cmdNav_Click(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles cmdPrevious.Click, cmdNext.Click, cmdCancel.Click

    If Sender Is cmdPrevious Then
      pczRetc = 0
    ElseIf Sender Is cmdNext Then
      pczRetc = 1
    ElseIf Sender Is cmdCancel Then
      pczRetc = 2
    End If

    Select Case pczRetc

      Case 0 ' previous
        Me.DialogResult = DialogResult.OK
        Me.Close()

      Case 1, 3 ' next, finish
        txtCellFolder_Leave(txtCellFolder, New EventArgs())
        If pcznCellPics <= 0 Then
          MsgBox("No image files are in " & pczCellFolder)
          txtCellFolder.select()
          Exit Sub
        End If
        Me.DialogResult = DialogResult.OK
        Me.Close()

      Case 2 ' cancel
        Me.DialogResult = DialogResult.Cancel
        Me.Close()

    End Select

  End Sub

  Private Sub frmPicturize1_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Dim ss As New List(Of String)

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, "photomosaic.html")

    Label1.Text = "The Photo Mosaic tool makes a mosaic of the current photo, using small copies of photos from the selected directory for the pixels or cells." & crlf & crlf
    Label2.Text = Label2.Text & "Notes:" & crlf & crlf
    Label2.Text = "1.  For best results, there should be a larger number of ""cell"" photos in the selected folder." & crlf & crlf
    Label2.Text = Label2.Text & "2.  The cell photos must be .jpg files."
    Label3.Text = "Enter the folder containing the cell photos, then press ""Next"""

    If pczCellFolder = "" Then pczCellFolder = iniExplorePath
    txtCellFolder.Text = pczCellFolder

    ss.Add(".jpg")
    ss.Add(".jpeg")

    If pczCellFolder <> "" Then
      pcznCellPics = countfiles(pczCellFolder, ss)
      lbNPics.Text = "Contains " & pcznCellPics & " image files."
    Else
      pcznCellPics = 0
    End If

    cmdPrevious.Enabled = False

  End Sub

  Private Sub txtCellFolder_Leave(ByVal Sender As Object, ByVal e As EventArgs) Handles txtCellFolder.Leave

    Dim fNames As New List(Of String)
    Dim picInfo As pictureInfo
    Dim ss As New List(Of String)

    pczCellFolder = txtCellFolder.Text
    If vb.Right(pczCellFolder, 1) <> "\" Then pczCellFolder = pczCellFolder & "\"

    ss.Add(".jpg")
    ss.Add(".jpeg")
    fNames = dirGetfiles(pczCellFolder, ss)
    pczCellAspect = 0.75

    pcznCellPics = fNames.Count
    lbNPics.Text = "Contains " & pcznCellPics & " image files."

    If fNames IsNot Nothing AndAlso fNames.Count > 0 Then ' get the aspect ratio of the first .jpg file.
      picInfo = getPicinfo(fNames(0), True)
      If Not picInfo.isNull Then pczCellAspect = picInfo.Aspect
    End If

  End Sub

  Function countfiles(ByRef folder As String, ByRef ext As List(Of String)) As Integer

    Dim fNames As New List(Of String)

    fNames = dirGetfiles(folder, ext)

    If fNames IsNot Nothing Then
      Return fNames.Count
    Else
      Return 0
    End If

  End Function

End Class