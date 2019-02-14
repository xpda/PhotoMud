Imports System.IO
Imports ImageMagick
Imports System.Collections.Generic
Imports System.Drawing.Imaging

Public Class frmInfo
  Inherits Form

  Dim processing As Boolean
  Dim fpic As Integer
  Dim picPath As String

  Dim pcomments As New List(Of PropertyItem)
  Dim ucomments As uExif

  Private Sub chkInfo_CheckedChanged(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles chkMakernote.CheckedChanged, chkJpgTags.CheckedChanged, chkDump.CheckedChanged, _
    chkXmpInfo.CheckedChanged

    If processing Then Exit Sub
    processing = True

    If Sender Is chkDump Then
      If chkDump.Checked Then
        chkJpgTags.Enabled = False
        chkMakernote.Enabled = False
      Else
        chkJpgTags.Enabled = True
        chkMakernote.Enabled = True
      End If
    End If

    showInfo(picPath)
    processing = False

  End Sub

  Private Sub cmdHelp_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdHelp.Click
    Try
      Help.ShowHelp(Me, helpFile, HelpNavigator.Topic, Me.Name & ".html")
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

  Private Sub cmdNext_Click(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles cmdNext.Click, cmdPrevious.Click

    If processing Then Exit Sub
    processing = True

    If Sender Is cmdPrevious Then fpic = fpic - 1 Else fpic = fpic + 1

    If callingForm Is frmExplore Then
      picPath = frmExplore.getNextPath(fpic)
    End If

    rtext1.SelectionFont = New Font(rtext1.SelectionFont, FontStyle.Regular)
    rtext1.Rtf = ""
    rtext1.Refresh()
    ucomments = readComments(picPath, True, True)
    fitFile(picPath, pView)
    showInfo(picPath)
    Text1.Text = Path.GetFileName(picPath)

    processing = False

  End Sub

  Private Sub pView_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)

    If (pView.Bitmap IsNot Nothing) And (Not processing) Then
      pView.Cursor = Cursors.Hand
    Else
      pView.Cursor = Cursors.Default
    End If

  End Sub

  Private Sub pView1_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs)

    Dim lastCallingForm As Form
    Dim lastCurrentPicPath As String

    If callingForm.Name <> "frmFullscreen" AndAlso pView.Bitmap IsNot Nothing Then
      lastCallingForm = callingForm ' preserve callingform for the next, previous commands
      lastCurrentPicPath = currentpicPath
      callingForm = Me
      clearBitmap(qImage)
      qImage = pView.Bitmap.Clone
      Using frm As New frmFullscreen
        frm.ShowDialog()
      End Using

      clearBitmap(qImage)
      callingForm = lastCallingForm
      currentpicPath = lastCurrentPicPath
    End If
  End Sub

  Private Sub cmdOK_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdOK.Click

    iniInfoMaker = chkMakernote.Checked
    iniInfoJpeg = chkJpgTags.Checked
    iniInfoDump = chkDump.Checked
    iniInfoXmp = chkXmpInfo.Checked

    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Private Sub Command2_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdCopy.Click

    My.Computer.Clipboard.Clear()
    My.Computer.Clipboard.SetText(rtext1.Text, TextDataFormat.Rtf)

  End Sub

  Private Sub frmInfo_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
    If uComments IsNot Nothing Then uComments.Dispose() : uComments = Nothing
  End Sub

  Private Sub frmInfo_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    If (Screen.PrimaryScreen.Bounds.Height) > 1000 Then
      Me.Height = 950
    ElseIf (Screen.PrimaryScreen.Bounds.Height) > 790 Then
      Me.Height = 800
    Else
      Me.Height = 720
    End If
    If (Screen.PrimaryScreen.Bounds.Height) >= 1190 Then Me.Width = 1100
    Me.Top = (Screen.PrimaryScreen.Bounds.Height - Me.Height) / 2


    picPath = currentpicPath

    If callingForm Is frmExplore Then
      fpic = frmExplore.iPic
      cmdPrevious.Visible = True
      cmdNext.Visible = True
      If qImage IsNot Nothing Then
        ucomments = readComments(picPath, True, True)
        pView.setBitmap(qImage)
        pView.Zoom(0)
      Else
        fitFile(picPath, pView)
        ucomments = readComments(picPath, True, True)
      End If

    ElseIf callingForm.Name = "frmFullscreen" Then
      cmdPrevious.Visible = False
      cmdNext.Visible = False
      ucomments = readComments(picPath, True, True)
      pView.setBitmap(qImage)
      pView.Zoom(0)

    Else ' frmmain
      cmdPrevious.Visible = False
      cmdNext.Visible = False
      ucomments = readComments(frmMain.mView.picName, True, True)
      pView.setBitmap(frmMain.mView.Bitmap)
      pView.Zoom(0)
    End If

    processing = True
    If uComments Is Nothing Then
      chkJpgTags.Enabled = False
      chkDump.Enabled = False
      chkJpgTags.Checked = False
      chkDump.Checked = False
      chkXmpInfo.Enabled = False
    End If

    chkMakernote.Checked = iniInfoMaker
    chkJpgTags.Checked = iniInfoJpeg
    chkDump.Checked = iniInfoDump
    chkXmpInfo.Checked = iniInfoXmp

    Text1.Text = Path.GetFileName(picPath)
    showInfo(picPath)

    processing = False

    rtext1.select()

  End Sub

  Private Sub Command3_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdSave.Click
    ' save
    Dim Path As String = ""
    Dim fName As String
    Dim Result As DialogResult

    Try
      Path = My.Computer.FileSystem.SpecialDirectories.MyDocuments
    Catch
    End Try
    saveDialog1.Title = "Save Photo Information"
    saveDialog1.FileName = ""
    saveDialog1.Filter = "All Files (*.*)|*.*|text (*.txt)|*.txt"
    saveDialog1.FilterIndex = 2
    saveDialog1.DefaultExt = "txt"
    saveDialog1.InitialDirectory = Path
    saveDialog1.CheckPathExists = True
    saveDialog1.OverwritePrompt = True

    Try
      Result = saveDialog1.ShowDialog()
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    If Result = DialogResult.OK Then
      fName = saveDialog1.FileName
      Try
        File.WriteAllText(fName, rtext1.Text, System.Text.Encoding.GetEncoding("ISO-8859-1"))
      Catch ex As Exception
        MsgBox(ex.Message)
      End Try
    End If

  End Sub

  Private Sub showInfo(ByRef picpath As String)

    Dim s As String
    Dim iTabs(0) As Integer
    Dim picInfo As pictureInfo
    Dim pComments As New List(Of PropertyItem)

    s = ""

    iTabs(0) = 1
    rtext1.SelectionTabs = iTabs
    rtext1.SelectionTabs(0) = 25

    'Clock.Reset() : Clock.Start()
    picInfo = getPicinfo(picpath, True)
    ucomments = readComments(picpath, True, True)
    pComments = readPropertyItems(picpath) ' for thumbnail detection
    formatExifComments(chkMakernote.Checked, chkJpgTags.Checked, chkXmpInfo.Checked, _
      chkDump.Checked, s, ucomments, picInfo, pcomments) ' s has the answer
    'milli(0) = Clock.ElapsedMilliseconds

    If s = "" Then formatPicinfo(picInfo, s)

    s = s.Replace(crlf, "\par       ")
    s = s.Replace(ChrW(10), "\par       ")
    s = s & "\par "
    rtext1.Rtf = "{\rtf1\ansi{\pard\plain\f2\fs20\tx3500" & s & "}}"

    rtext1.SelectionStart = 1

  End Sub


End Class