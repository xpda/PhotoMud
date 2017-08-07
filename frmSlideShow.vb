
Imports System.IO
Imports System.ComponentModel
Imports System.Text
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Collections.Generic

Public Class frmSlideShow
  Inherits Form
  Dim iPic As Integer = 0
  Dim Loading As Boolean = True
  Dim Playing As Boolean
  Dim ScreenSaverSetting(3) As Boolean
  Dim ix As New List(Of Integer)
  Dim pComments As List(Of PropertyItem)
  Dim slideRate As Double
  Dim lastMouseLocation As Point
  Dim WithEvents timer1 As New Timer ' timer for slides
  Dim WithEvents timer2 As New Timer ' timer to hide controls
  Dim v(0) As Object
  Dim slideKey As New List(Of DateTime)

  Dim nextFname As String = ""
  Dim showTime As Boolean

  Dim gBitmap As Bitmap

  Private Sub cmdExit_Click(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles cmdExit.Click, mnuStop.Click
    Me.DialogResult = DialogResult.OK
    Me.Close()
  End Sub

  Private Sub cmdHelp_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdHelp.Click
    Try
      Help.ShowHelp(Me, helpFile, HelpNavigator.Topic, Me.Name & ".html")
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

  Private Sub cmdNext_Click(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles cmdNext.Click, cmdPrevious.Click, mnuNext.Click, mnuPrevious.Click
    If Sender Is cmdNext Or Sender Is mnuNext Then nextPic(1) Else nextPic(-1)
    If Playing Then
      timer1.Stop() : timer1.Start() ' reset the count
    End If
  End Sub

  Private Sub cmdPlay_Click(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles cmdPlay.Click, mnuPlay.Click, mnuPause.Click

    Playing = Not Playing

    If Playing Or Sender Is mnuPause Then
      cmdPlay.BackgroundImage = ImageList1.Images.Item("pause")
      cmdPlay.BackgroundImageLayout = ImageLayout.Center
      nextPic(1)
      timer1.Start()
      timer2.Stop() : timer2.Start()
    ElseIf Not Playing Or Sender Is mnuPlay Then
      cmdPlay.BackgroundImage = ImageList1.Images.Item("play")
      cmdPlay.BackgroundImageLayout = ImageLayout.Center
      timer1.Stop()
    End If

  End Sub

  Private Sub frmSlideShow_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
    If Panel1.Visible Then cmdPlay.select() Else Picture1.select()
  End Sub

  Private Sub frmSlideShow_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Loading = True

    'Picture1.Image = New Bitmap(Picture1.ClientSize.Width, Picture1.ClientSize.Height, PixelFormat.Format32bppPArgb)

    Me.WindowState = FormWindowState.Maximized
    slideRate = iniSlideRate
    If slideRate < nmSlideRate.Minimum Or slideRate > nmSlideRate.Maximum Then slideRate = nmSlideRate.Minimum
    nmSlideRate.Value = slideRate
    timer1.Interval = slideRate * 1000

    cmdPlay.BackgroundImage = ImageList1.Images.Item("pause")
    cmdPlay.BackgroundImageLayout = ImageLayout.Center

    getSettings()
    orderSlides()
    GetScreenSaverSettings(ScreenSaverSetting(0), ScreenSaverSetting(1), ScreenSaverSetting(2))
    SetScreenSaver(False, False, False, ScreenSaverSetting) ' turn off screen saver

  End Sub

  Private Sub frmSlideShow_FormClosing(ByVal Sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
    timer1.Stop()
    timer2.Stop()
    cancelBkgLoad()
    Cursor.Show()
    SetScreenSaver(True, True, True, ScreenSaverSetting) ' restore screen saver settings

    clearBitmap(gBitmap)
    timer1.Stop()
    timer2.Stop()

  End Sub

  Private Sub globalkey(ByVal e As KeyEventArgs)

    e.Handled = True

    If e.KeyCode <> 27 Then ShowControls(True)

    Select Case e.KeyCode
      Case 27
        Me.Close()
      Case 33 ' pg up
        cmdNext_Click(cmdPrevious, New EventArgs())
      Case 34 ' pg dn
        cmdNext_Click(cmdNext, New EventArgs())
      Case 37, 39 ' alt left arrow, alt right arrow
        If Not e.Shift And Not e.Control And Not e.Alt Then
          nmSlideRate.select()
        ElseIf e.Alt Then
          If e.KeyCode = 37 Then
            cmdNext_Click(cmdPrevious, New EventArgs())
          Else
            cmdNext_Click(cmdNext, New EventArgs())
          End If
        End If
      Case 72 ' alt-h
        If e.Alt Then ShowControls(True)
      Case Else
        e.Handled = False
    End Select

  End Sub

  Private Sub picture1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Picture1.KeyDown, Me.KeyDown

    If e.KeyCode = 32 Then
      cmdPlay_Click(cmdPlay, e)
    Else
      globalkey(e)
    End If

  End Sub

  Private Sub picture1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Picture1.MouseDown, Me.MouseDown

    ShowControls(True)

    If Not Playing Then
      If e.Button = MouseButtons.Left Then
        nextPic(1)
        'ElseIf e.Button = MouseButtons.Right Then
        '  nextPic(-1)
      End If
    End If

  End Sub

  Private Sub nmSlideRate_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles nmSlideRate.ValueChanged
    If Loading Then Exit Sub
    slideRate = nmSlideRate.Value
    timer1.Stop()
    timer1.Interval = slideRate * 1000
    timer1.Start()
    ShowControls(True)
  End Sub

  Sub nextPic(ByVal inc As Integer)
    ' show the image in memory and begin loading the next one.
    ' if an image is being loaded, set flags for it to be shown and the next load to start when bkgload is done.

    Dim i As Integer

    If Len(nextFname) = 0 Or Not bkgLoad.IsBusy Then
      iPic = iPic + inc
      If iPic >= tagPath.Count Then iPic = 0
      If iPic < 0 Then iPic = tagPath.Count - 1

      i = iPic + 1
      If i >= tagPath.Count Then i = 0
      If i < 0 Then iPic = tagPath.Count - 1
    End If

    If bkgLoad.IsBusy Then ' save the filename for loading in RunWorkerCompleted, and save showtime to show image
      If nextFname = "" Then nextFname = tagPath(ix(i))
      showTime = True

    Else
      drawBitmap()
      ' prefetch in the background
      nextFname = ""
      showTime = False
      v(0) = tagPath(ix(i))
      bkgLoad.RunWorkerAsync(v)
    End If

  End Sub

  Sub drawBitmap()

    Dim r As Rectangle

    If Playing Then timer1.Stop()

    If gBitmap IsNot Nothing And Picture1.ClientSize.Width > 0 And Picture1.ClientSize.Height > 0 Then
      lbFilename.Text = Path.GetFileName(tagPath(ix(iPic)))
      lbDescription.Text = getBmpComment(uExif.TagID.Description, pComments)
      lbPhotoDate.Text = Format(getCommentsDate(pComments), "MMM dd, yyyy")

      r = New Rectangle(0, 0, Picture1.ClientSize.Width, Picture1.ClientSize.Height)

      Try
        Using g As Graphics = Picture1.CreateGraphics
          g.DrawImage(gBitmap, r)
        End Using
      Catch ex As Exception
        MsgBox(ex.Message, MsgBoxStyle.AbortRetryIgnore)
      End Try
    End If

    If Playing Then timer1.Start()

  End Sub

  Function getCommentsDate(pComments As List(Of PropertyItem)) As DateTime

    Dim s As String
    Dim dt As DateTime

    If pComments Is Nothing Then Return Nothing
    s = getBmpComment(propID.DateTime, pComments)
    s = Replace(s, ":", "/", 1, 2)
    If s.Trim <> "" Then
      Try
        dt = CDate(s)
        Return dt
      Catch ex As Exception
        MsgBox(ex.Message, MsgBoxStyle.AbortRetryIgnore)
        Return Nothing
      End Try
    End If

  End Function

  Private Sub bkgLoad_dowork(ByVal Sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) _
    Handles bkgLoad.DoWork

    Dim r As Rectangle
    Dim picInfo As pictureInfo
    Dim msg As String = ""
    Dim fName As String

    fName = e.Argument(0)

    picInfo = getPicinfo(fName, False)

    If picInfo.isNull Then
      'MsgBox("Error reading " & fname & crlf & ex.Message, MsgBoxStyle.OkOnly)
      Exit Sub
    End If

    ' set r to the image destination rectangle
    If 1 / Picture1.ClientSize.Width > picInfo.Aspect / Picture1.ClientSize.Height Then ' wide
      r.X = 0
      r.Width = Picture1.ClientSize.Width
      r.Height = r.Width * picInfo.Aspect
      r.Y = (Picture1.ClientSize.Height - r.Height) / 2
    Else
      r.Y = 0
      r.Height = Picture1.ClientSize.Height
      r.Width = r.Height / picInfo.Aspect
      r.X = (Picture1.ClientSize.Width - r.Width) / 2
    End If

    clearBitmap(gBitmap)

    Try
      Using bmp As Bitmap = readBitmap(fName, msg)
        If bmp IsNot Nothing Then
          gBitmap = New Bitmap(r.Width, r.Height, PixelFormat.Format32bppPArgb)
          Using g As Graphics = Graphics.FromImage(gBitmap)
            g.InterpolationMode = InterpolationMode.High
            g.DrawImage(bmp, r)
          End Using
        End If
      End Using
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

  End Sub

  Sub orderSlides()

    Dim j, i, k As Integer
    Dim ix1 As New List(Of Integer)

    ix = New List(Of Integer)
    ix1 = New List(Of Integer)
    slideKey = New List(Of Date)

    For i = 0 To tagPath.Count - 1
      slideKey.Add(Nothing)
      If iniSlideOrder = "filedate" Then
        Try
          slideKey(i) = File.GetLastWriteTime(tagPath(i))
        Catch ex As Exception
          MsgBox(ex.Message, MsgBoxStyle.AbortRetryIgnore)
          slideKey(i) = CDate(Date.MinValue)
        End Try
      ElseIf iniSlideOrder = "photodate" Then
        Try
          pComments = readPropertyItems(tagPath(i))
          slideKey(i) = getCommentsDate(pComments)
        Catch ex As Exception
          MsgBox(ex.Message, MsgBoxStyle.AbortRetryIgnore)
          slideKey(i) = Date.MinValue
        End Try

      End If
      ix.Add(i)
      ix1.Add(0)
    Next i

    Select Case iniSlideOrder

      Case "random"
        Randomize()
        For i = 0 To tagPath.Count - 1
          k = Int(Rnd(1) * (tagPath.Count))
          If ix1(k) = 0 Then
            ix1(k) = 1 ' mark this one as done
            ix(i) = k
          Else ' hunt the next free one
            For j = 0 To tagPath.Count - 1
              k = k + 1
              If k > tagPath.Count - 1 Then k = 0
              If ix1(k) = 0 Then
                ix1(k) = 1
                ix(i) = k
                Exit For
              End If
            Next j
          End If
        Next i

      Case "filename"
        MergeSort(tagPath, ix, 0, tagPath.Count - 1)

      Case "filedate"
        MergeSort(slideKey, ix, 0, tagPath.Count - 1)

      Case "photo date"
        MergeSort(slideKey, ix, 0, tagPath.Count - 1)

      Case Else ' none

    End Select


  End Sub

  Private Sub cmdOptions_Click(ByVal sender As System.Object, ByVal e As EventArgs) _
    Handles cmdOptions.Click, mnuOptions.Click
    Using frm As New frmOptions
      OptionTab = frm.tabSlideShow
      ShowControls(True)
      timer2.Stop()
      frm.ShowDialog()
    End Using
    getSettings()
    timer2.Start()
  End Sub

  Sub getSettings()

    slideRate = iniSlideRate
    StatusVisible()
    lbDescription.Visible = iniSlideShowDescription
    lbFilename.Visible = iniSlideShowName
    lbPhotoDate.Visible = iniSlideShowPhotoDate

  End Sub

  Private Sub contextMnu_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles contextMnu.Opening
    If Playing Then
      mnuPlay.Visible = False
      mnuPause.Visible = True
    Else
      mnuPlay.Visible = True
      mnuPause.Visible = False
    End If
    mnuRandom.Checked = False
    mnuOrderFilename.Checked = False
    mnuOrderPhotoDate.Checked = False
    mnuOrderFiledate.Checked = False
    Select Case iniSlideOrder
      Case "random" : mnuRandom.Checked = True
      Case "filename" : mnuOrderFilename.Checked = True
      Case "photodate" : mnuOrderPhotoDate.Checked = True
      Case "filedate" : mnuOrderFiledate.Checked = True
    End Select

    mnuHideDescription.Checked = Not iniSlideShowDescription
    mnuHideFilename.Checked = Not iniSlideShowName
    mnuHidePhotoDate.Checked = Not iniSlideShowPhotoDate
  End Sub

  Private Sub mnuOrder_Click(ByVal sender As System.Object, ByVal e As EventArgs) _
    Handles mnuRandom.Click, mnuOrderFilename.Click, mnuOrderPhotoDate.Click, mnuOrderFiledate.Click

    If sender Is mnuRandom Then iniSlideOrder = "random"
    If sender Is mnuOrderFilename Then iniSlideOrder = "filename"
    If sender Is mnuOrderPhotoDate Then iniSlideOrder = "photodate"
    If sender Is mnuOrderFiledate Then iniSlideOrder = "filedate"

    orderSlides()
  End Sub

  Private Sub mnuHideDescription_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnuHideDescription.Click
    iniSlideShowDescription = Not iniSlideShowDescription
    StatusVisible()
  End Sub

  Private Sub mnuHideFilename_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnuHideFilename.Click
    iniSlideShowName = Not iniSlideShowName
    StatusVisible()
  End Sub

  Private Sub mnuHidePhotodate_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnuHidePhotoDate.Click
    iniSlideShowPhotoDate = Not iniSlideShowPhotoDate
    StatusVisible()
  End Sub

  Sub StatusVisible()
    If iniSlideShowDescription Or iniSlideShowName Then StatusStrip1.Visible = True Else StatusStrip1.Visible = False
    lbDescription.Visible = iniSlideShowDescription
    lbFilename.Visible = iniSlideShowName
    lbPhotoDate.Visible = iniSlideShowPhotoDate
  End Sub

  Private Sub mnuHelp_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnuHelp.Click
    Try
      Help.ShowHelp(Me, helpFile, HelpNavigator.Topic, Me.Name & ".html")
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

  Private Sub Timer2_Tick(ByVal Sender As Object, ByVal e As EventArgs) Handles timer2.Tick
    If Playing Then ShowControls(False)
  End Sub

  Private Sub Timer1_Tick(ByVal Sender As Object, ByVal e As EventArgs) Handles timer1.Tick
    nextPic(1)
  End Sub

  Sub cancelBkgLoad()

    If bkgLoad.IsBusy Then
      bkgLoad.CancelAsync()
      Do While bkgLoad.IsBusy
        Application.DoEvents()
      Loop
    End If

  End Sub

  Sub ShowControls(ByVal shoe As Boolean)

    If shoe Then
      timer2.Stop()
      timer2.Interval = 3500
      timer2.Start()
      Cursor.Show()
      Panel1.Visible = True
    Else
      timer2.Stop()
      Panel1.Visible = False
      Picture1.Refresh()
      Cursor.Hide()
    End If

  End Sub

  Private Sub Panel1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
    Handles Panel1.MouseDown, cmdPrevious.MouseDown, cmdNext.MouseDown, cmdPlay.MouseDown, _
      cmdOptions.MouseDown, cmdExit.MouseDown, Label1.MouseDown, nmSlideRate.MouseDown, cmdHelp.MouseDown, Me.MouseDown
    ShowControls(True)
  End Sub

  Private Sub Picture1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
    Handles Picture1.MouseMove, Panel1.MouseMove, cmdPrevious.MouseMove, cmdNext.MouseMove, cmdPlay.MouseMove, StatusStrip1.MouseMove, _
      cmdOptions.MouseMove, cmdExit.MouseMove, Label1.MouseMove, nmSlideRate.MouseMove, cmdHelp.MouseMove, Me.MouseMove
    If Equals(Cursor.Position, lastMouseLocation) Then Exit Sub
    ShowControls(True)
    lastMouseLocation = Cursor.Position
  End Sub

  Private Sub frmSlideShow_keydown(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles Panel1.KeyDown, cmdPrevious.KeyDown, cmdNext.KeyDown, cmdPlay.KeyDown, StatusStrip1.KeyDown, _
      cmdOptions.KeyDown, cmdExit.KeyDown, Label1.KeyDown, nmSlideRate.KeyDown, cmdHelp.KeyDown
    globalkey(e)
  End Sub

  Private Sub frmSlideShow_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize

    If Panel1.Width < Me.ClientSize.Width Then Panel1.Left = (Me.ClientSize.Width - Panel1.Width) / 2
    If Panel1.Height < Me.ClientSize.Height Then Panel1.Top = Me.ClientSize.Height - Panel1.Height + 1

  End Sub

  Private Sub bkgLoad_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bkgLoad.RunWorkerCompleted

    If showTime Then
      showTime = False
      drawBitmap()
    End If

    If nextFname <> "" Then
      ' there is a file waiting -- start it.
      v(0) = nextFname
      nextFname = ""
      bkgLoad.RunWorkerAsync(v)
    End If

  End Sub

  Private Sub frmSlideShow_Shown(sender As Object, e As EventArgs) Handles Me.Shown
    v(0) = tagPath(ix(0))
    bkgLoad.RunWorkerAsync(v)
    nextPic(0)
    Playing = True
    timer1.Start()
  End Sub
End Class