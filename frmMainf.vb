'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports Microsoft.Win32
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Text
Imports System.Drawing
Imports System.Math
Imports System.IO
Imports System.Collections.Generic

Imports ImageMagick

Public Class frmMainf
  Inherits Form

  Enum rbMode ' rBoxMode
    none = 0
    readyMouseDown
    readyMouseUp
    readyBoxDrag
    dragLeftRight
    dragTopBottom
    moveCropBox
  End Enum

  Dim Command As cmd

  Dim imageTmp As Bitmap

  Dim ShiftDown As Boolean = False
  Dim controlDown As Boolean = False

  Public combineRview As mudViewer
  Public iPic As Integer ' for fullscreen next and previous
  Dim localPics As New List(Of String) ' for getnextpath

  Public DrawPenWidth As Integer
  Public DrawPenStyle As Integer

  Dim LastButton As String
  Dim currentTool As String = ""

  Dim zX(300) As Double
  Dim zY(300) As Double ' for curves
  Dim nzp As Integer
  Dim zP As New List(Of Point)

  Dim zoomStep As Double = 1.25
  Dim insideSetPage As Boolean
  Dim mnxCancel As Boolean = False ' flag to cancel context menu

  ' rubber box stuff
  Public rBoxMode As rbMode '   0 = none,
  Dim rBoxX, rBoxY As Integer ' drag start location
  Dim rboxleft As Integer ' rubber band box
  Dim rBoxTop As Integer
  Dim rBoxWidth As Integer
  Dim rBoxHeight As Integer
  Dim rbX As Integer ' temporary rubber band box
  Dim rbY As Integer
  Dim rbWidth As Integer
  Dim rbHeight As Integer
  Dim edge As Short ' right 4 bits in edge = edge matches
  Dim rb As Rectangle  ' temporary saves
  Dim rbq As Rectangle
  Dim rbox As Rectangle

  Dim textPoint As Point ' text center, control coordinates
  Dim dragPoint As Point ' point where mousedown occurs to start dragging
  Dim Dragging As Boolean = False

  Dim ImageAspect As Double

  Dim DrawingLine As Integer
  Dim Sketching As Integer
  Dim LineP As Point ' client point
  Dim sketchP As New List(Of Point)

  Dim DrawingStretch As Integer
  Dim rX(3), rY(3) As Double
  Dim npts As Integer
  Dim stretchAspect As Double

  Dim drawingMode As Integer

  ' for text
  Dim textMultiline As Boolean
  Dim textBackFill As Boolean
  Dim textAngle As Double
  Dim textColor As Color
  Dim textBackColor As Color
  Dim fontSize As Integer
  Dim fontName As String

  Dim textFmt As StringFormat
  Dim textString As String = "Text to be added to the photo"
  Dim fBold As Boolean
  Dim fItalic As Boolean
  Dim fUnderline As Boolean

  Dim colorTolerance As Integer
  Dim featherAmount As Integer
  Dim clickPoint As Point

  Dim repainted As Boolean

  Dim firstP As Point ' mouse location at MouseDown, for mousemove
  Dim lastP As Point ' previous mouse location, for mousemove
  Dim lastD As Point ' movement since lastP

  Dim Processing As Boolean = False
  Public Loading As Boolean = True
  Dim loadFile As String = ""

  Dim floatX, floatY As Integer ' point for initial floater position, for expand floater

  ' variables for the last command for F3 (repeat command)
  Delegate Sub lastCmdDelegate(sender As Object, e As KeyEventArgs)
  Dim lastCmdSub As lastCmdDelegate
  Dim dResult As DialogResult

  Dim WithEvents timerRedraw As Timer
  Dim WithEvents timerAnimate As Timer
  Dim WithEvents timerBright As Timer
  Dim WithEvents timerFeather As Timer
  Dim cmdRunning As Boolean

  Public WithEvents mView As mudViewer
  Public WithEvents mainTabs As ctlTabs

  Private Sub frmMain_Activated(sender As Object, e As EventArgs) Handles Me.Activated

    ' this event occurs before load sometimes
    If Loading Then Exit Sub

    If mViews.Count > 1 Then mnuViewNext.Enabled = True Else mnuViewNext.Enabled = False

  End Sub

  Private Sub frmMain_KeyDown(sender As Object, e As KeyEventArgs) _
  Handles Me.KeyDown, mView.KeyDown, cmdAnimate.KeyDown, panelPage.KeyDown, mView.subKeyDown, _
  mainTabs.KeyDown

    If (TypeOf Me.ActiveControl Is ctlBrightness) OrElse (TypeOf Me.ActiveControl Is ctlColorTolerance) OrElse _
      (TypeOf Me.ActiveControl Is ctlTextControl) Then
      Exit Sub
    End If

    globalKeydown(e)
  End Sub

  Sub MainFormInit()

    ' this does initialization in the startup form. Move it to frmExplore if that becomes the startup form.

    Dim i, k As Integer
    Dim s As String
    Dim mView As mudViewer
    Dim msg As String

    loadFile = ""
    For Each s In My.Application.CommandLineArgs
      If File.Exists(s) Then
        loadFile = s
        Exit For
      End If
    Next s

    If loadFile = "" Then
      k = 0
      For i = 0 To fmtCommon.Count - 1
        Try
          s = Registry.GetValue("HKEY_CURRENT_USER\software\classes\" & fmtCommon(i).ID, "", "error")
        Catch ex As Exception
          msg = ex.Message
          s = "error"
        End Try
        If s = appTag Then k = k + 1
      Next i

      If k = 0 And iniAskAssociate Then
        Using frm As New frmAskAssociate
          dResult = frm.ShowDialog()
        End Using

      End If

    Else ' If loadFile <> "" Then
      s = Path.GetDirectoryName(loadFile)
      If Not s.EndsWith("\") Then s &= "\"
      If String.Compare(s, Path.GetTempPath, True) <> 0 Then
        iniExplorePath = s
      End If
      mView = loadNew(loadFile) ' open file from double click (sets mView)
    End If

  End Sub

  Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    Me.Text = AppName
    Me.Cursor = Cursors.WaitCursor

    mainTabs = New ctlTabs
    Me.Controls.Add(mainTabs)
    mainTabs.BringToFront()
    mainTabs.Visible = True
    mainTabs.Dock = DockStyle.Fill

    timerRedraw = New Timer
    timerAnimate = New Timer
    timerBright = New Timer
    timerFeather = New Timer

    MainFormInit()

    mainColor = iniForeColor
    mBackColor = iniBackColor

    'helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    'helpProvider1.SetHelpKeyword(Me, "explorerandeditor.html")

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.TableOfContents)
    helpProvider1.SetHelpKeyword(Me, "")

    Select Case iniButtonSize
      Case 0
        ToolStrip1.ImageScalingSize = New Size(24, 24)
      Case Else ' 1
        ToolStrip1.ImageScalingSize = New Size(32, 32)
    End Select

    mnu.Visible = True
    Me.CancelButton = Nothing
    Me.AcceptButton = Nothing

    colorTolerance = iniColorTolerance
    featherAmount = 0

    ' line defaults
    DrawPenStyle = iniDrawPenStyle
    DrawPenWidth = iniDrawPenWidth

    Command = cmd.None

    mnuHelpTips.Text = "&Show " & AppName & " Tips"
    mnuHelpRegister.Text = "&Register " & AppName & " Online"
    mnuHelpAbout.Text = "&About " & AppName

    ToolStrip1.Visible = iniViewToolbar
    assignToolbar()

    setupPanelPage(mView)

  End Sub

  Private Function closeTab() As Boolean
    ' close the selected tab (photo), returns true to abort closing.

    Dim mResult As MsgBoxResult
    Dim gMsg, msg As String
    Dim saver As ImageSave

    Processing = True

    If mView IsNot Nothing Then
      If mView.ctlText.Visible Then closePanelText()
      If mView.picModified Then
        If mView.picName = "" Then
          gMsg = "This photo has been changed. Do you want to save it?"
        Else
          gMsg = mView.picName & " has been changed. Do you want to save it?"
        End If
        mResult = MsgBox(gMsg, MsgBoxStyle.YesNoCancel)
        closeTab = mResult

        If mResult = MsgBoxResult.Cancel Then
          Me.Select()
          Processing = False
          Return True ' abort closing
        ElseIf mResult = MsgBoxResult.Yes Then
          If mView.picName = "" OrElse mView.picPage <> "" Then
            FileSaveAs(0) ' save image
          Else
            saver = New ImageSave
            If mView.pComments IsNot Nothing Then ' added 7/30/17
              saver.pComments = New List(Of PropertyItem)
              saver.pComments.AddRange(mView.pComments)
            End If
            saver.Quality = iniJpgQuality
            saver.sourceFilename = mView.picName
            saver.saveExif = True
            saver.copyProfiles = True
            msg = saver.write(mView.Bitmap, mView.picName, True)
            mView.Cursor = Cursors.Default
            If msg <> "" Then
              mResult = MsgBox(mView.picName & " could not be saved." & crlf & msg, MsgBoxStyle.OkOnly)
              Return True ' abort closing
            Else
              mView.picModified = False
              mView.originalFileName = mView.picName
            End If
          End If
        End If
      End If
      If mView.picModified Then ClearUndo()
      mView.Close() ' close the tab
    End If

    Return False ' do not abort

  End Function

  Private Sub mView_DoubleClick(sender As Object, e As EventArgs) Handles mView.DoubleClick
    If DrawingStretch > 0 And DrawingStretch < 10 And npts > 1 Then npts = npts - 1 ' point added by second click
    If DrawingLine = 2 And zP.Count > 0 Then zP.RemoveAt(zP.Count - 1) ' point added by second click
    CompleteCommand()
  End Sub

  Private Sub globalKeydown(e As KeyEventArgs)

    Dim p As Point
    Dim i As Integer
    Dim inc As Integer

    'If e.KeyCode = Keys.Q Then
    '  test()
    '  e.Handled = True
    '  Exit Sub
    '  End If

    e.Handled = False
    If mView Is Nothing Then Exit Sub

    If Not e.Shift Then
      inc = mView.ClientSize.Width / 6
    Else
      inc = mView.ClientSize.Width / 60
    End If

    Select Case e.KeyCode

      Case Keys.Delete
        If mView.FloaterBitmap IsNot Nothing Then mnuEditDeleteSelection_click(Nothing, Nothing)

      Case Keys.Back ' backspace
        If e.Alt Then mnuEditUndo_click(Nothing, Nothing)
        e.Handled = True

      Case Keys.Enter
        CompleteCommand()
        e.Handled = True

      Case Keys.Escape ' Esc
        If DrawingStretch > 0 And npts > 0 Then
          npts = npts - 1
          If npts = 0 Then
            ClearProcesses(True)
            mnuEditPasteV_click(Nothing, Nothing)
          End If
        Else ' all other commands -- abort
          If DrawingLine > 0 OrElse Sketching > 0 OrElse Command <> cmd.None OrElse drawingMode <> 0 Then
            ClearProcesses(True)
          Else ' close the window
            mnuFileClose_click(Me, New EventArgs)
          End If
        End If
        If mView IsNot Nothing Then mView.Invalidate()
        e.Handled = True

      Case Keys.Left ' left arrow
        If drawingMode > 0 Then ' move cursor left
          p = Cursor.Position
          If e.Shift Then p.X = p.X - 1 Else p.X = p.X - 10
          Cursor.Position = p
          e.Handled = True
        ElseIf e.Alt Then
          mnuEditUndo_click(Nothing, Nothing)
          e.Handled = True
        ElseIf e.Control Then
          ' move the viewpoint all the way to the left
          If mView.Bitmap.Width * mView.ZoomFactor > mView.ClientSize.Width Then
            i = (mView.ClientSize.Width \ 2) / mView.ZoomFactor
            mView.setCenterPoint(New Point(i, mView.CenterPoint.Y), True)
            e.Handled = True
          End If
        ElseIf e.Shift Then
          If Command <> cmd.DrawText Then
            mnuViewZoomfit_click(mView, e)
            e.Handled = True
          End If
        Else
          ' pan left
          mView.Pan(inc, 0)
          e.Handled = True
        End If

      Case Keys.Right ' right arrow
        If drawingMode > 0 Then ' move cursor right
          p = Cursor.Position
          If e.Shift Then p.X = p.X + 1 Else p.X = p.X + 10
          Cursor.Position = p
        ElseIf e.Alt Then
          mnuEditRedo_click(Nothing, Nothing)
        ElseIf e.Control Then
          If mView.Bitmap.Width * mView.ZoomFactor > mView.ClientSize.Width Then
            i = (mView.ClientSize.Width \ 2) / mView.ZoomFactor
            mView.setCenterPoint(New Point(mView.Bitmap.Width - i, mView.CenterPoint.Y), True)
            e.Handled = True
          End If
        ElseIf e.Shift Then
          If Command <> cmd.DrawText Then
            mnuViewZoom100_click(mView, e)
            e.Handled = True
          End If
        Else
          mView.Pan(-inc, 0)
          e.Handled = True
        End If

      Case Keys.Up ' up arrow
        If drawingMode > 0 Then ' move cursor left
          p = Cursor.Position
          If e.Shift Then p.Y = p.Y - 1 Else p.Y = p.Y - 10
          Cursor.Position = p
          e.Handled = True
        ElseIf e.Control Then
          ' move the view all the way up
          If mView.Bitmap.Height * mView.ZoomFactor > mView.ClientSize.Height Then
            i = (mView.ClientSize.Height \ 2) / mView.ZoomFactor
            mView.setCenterPoint(New Point(mView.CenterPoint.X, i), True)
            e.Handled = True
          End If
        Else
          ' move view up
          mView.Pan(0, inc)
          e.Handled = True
        End If

      Case Keys.Down ' down arrow
        If drawingMode > 0 Then ' move cursor left
          p = Cursor.Position
          If e.Shift Then p.Y = p.Y + 1 Else p.Y = p.Y + 10
          Cursor.Position = p
          e.Handled = True
        ElseIf e.Control Then
          ' move the viewpoint all the way down
          If mView.Bitmap.Height * mView.ZoomFactor > mView.ClientSize.Height Then
            i = (mView.ClientSize.Height \ 2) / mView.ZoomFactor
            mView.setCenterPoint(New Point(mView.CenterPoint.X, mView.Bitmap.Height - i), True)
            e.Handled = True
          End If
        Else
          ' move view down
          mView.Pan(0, -inc)
          e.Handled = True
        End If

      Case Keys.PageUp ' pgUp
        If drawingMode > 0 Then ' move cursor up right
          p = Cursor.Position
          If e.Shift Then
            p.X = p.X + 1 : p.Y = p.Y + 1
          Else
            p.X = p.X - 10 : p.Y = p.Y - 10
          End If
          Cursor.Position = p

        ElseIf panelPage.Visible Then
          If nmPage.Value > nmPage.Minimum Then nmPage.Value = nmPage.Value - 1
          e.Handled = True
        End If

      Case Keys.PageDown ' pgDn
        If drawingMode > 0 Then ' move cursor down right
          p = Cursor.Position
          If e.Shift Then
            p.X = p.X + 1 : p.Y = p.Y + 1
          Else
            p.X = p.X + 10 : p.Y = p.Y + 10
          End If
          Cursor.Position = p
          e.Handled = True

        ElseIf panelPage.Visible Then
          If nmPage.Value < nmPage.Maximum Then nmPage.Value = nmPage.Value + 1
          e.Handled = True
        End If

      Case Keys.End ' End
        If drawingMode > 0 Then ' move cursor down left
          p = Cursor.Position
          If e.Shift Then
            p.X = p.X - 1 : p.Y = p.Y + 1
          Else
            p.X = p.X - 10 : p.Y = p.Y + 10
          End If
          Cursor.Position = p
          e.Handled = True
        End If

      Case Keys.Home ' Home
        If drawingMode > 0 Then ' move cursor up left
          p = Cursor.Position
          If e.Shift Then
            p.X = p.X - 1 : p.Y = p.Y - 1
          Else
            p.X = p.X - 10 : p.Y = p.Y - 10
          End If
          Cursor.Position = p
          e.Handled = True
        End If

      Case Keys.Insert ' insert
        If DrawingStretch > 0 Or DrawingLine > 0 And Not e.Shift Then
          mView_MouseDown(mView, New MouseEventArgs(New MouseButtons, 1, lastP.X, lastP.Y, 0))
          e.Handled = True
        End If

      Case Keys.Z
        If Command <> cmd.DrawText Then
          mnuViewZoomWindow_click(mView, e)
          e.Handled = True
        End If

      Case Keys.Add, Keys.Oemplus '  107, 187,  + or =
        If e.KeyCode = 187 Or e.KeyCode = 107 Then
          ViewZoom(0)
          e.Handled = True
        End If

      Case Keys.Subtract, Keys.OemMinus ' 109, 189 ' -
        If (Not e.Shift And e.KeyCode = 189) Or e.KeyCode = 109 Then
          ViewZoom(1)
          e.Handled = True
        End If

      Case Keys.F6 ' 117 ' F6
        If Not e.Shift And Not e.Control Then
          showExplore()
          e.Handled = True
        End If

    End Select

    If Command = cmd.DrawText And (e.Control And Not e.Shift And Not e.Alt) Then ' Ctrl only
      Select Case e.KeyCode
        Case Keys.B ' 66 ' b
          mView.ctlText.Bold = Not mView.ctlText.Bold
          e.Handled = True
        Case Keys.I ' 73 ' i
          mView.ctlText.Italic = Not mView.ctlText.Italic
          e.Handled = True
        Case Keys.U ' 85 ' u
          mView.ctlText.Underline = Not mView.ctlText.Underline
          e.Handled = True
        Case Keys.OemOpenBrackets  ' 219 ' [
          i = mView.ctlText.fontSize
          If i >= 2 Then mView.ctlText.fontSize = i - 1
          ' SetText()
          e.Handled = True
        Case Keys.OemCloseBrackets ' 221 ' ]
          i = mView.ctlText.fontSize
          mView.ctlText.fontSize = i + 1
          ' SetText()
          e.Handled = True

      End Select
    End If ' text mode, control only

  End Sub

  Public Sub mView_DragEnter(sender As Object, ByVal e As DragEventArgs) _
    Handles mView.DragEnter, Me.DragEnter
    If e.Data.GetDataPresent("FileNameW") Then e.Effect = DragDropEffects.Copy
  End Sub

  Public Sub mView_DragDrop(sender As Object, ByVal e As DragEventArgs) _
    Handles mView.DragDrop, Me.DragDrop
    ' this only passes one file, but it will look for more.

    Dim strFileName As String
    Dim i As Integer
    Dim fName As String
    Dim ofn As Object
    Dim tab As TabPage = Nothing

    strFileName = ""

    If e.Data.GetDataPresent("FileNameW") Then
      ofn = e.Data.GetData("FileNameW")
      If IsArray(ofn) Then
        strFileName = ofn(0)
      End If
    End If

    ' open files from the command line.
    ' this is also done from linkexecute, but only one copy of each file should open.
    ' this has to be done here because drag and drop on a new instance doesn't activate dde.
    If strFileName <> "" Then
      i = InStr(strFileName, " ")
      Do While i > 0
        fName = strFileName.Substring(strFileName.Length - (i - 1))
        strFileName = Mid(strFileName, i + 1)
        If eqstr(Path.GetExtension(fName), ".lnk") Then fName = getLinkTarget(fName)
        mView = loadNew(fName)
        i = InStr(strFileName, " ")
      Loop
      If eqstr(Path.GetExtension(strFileName), ".lnk") Then strFileName = getLinkTarget(strFileName)
      mView = loadNew(strFileName)
    End If

  End Sub

  Private Sub mnuEdit_DropDownClosed(sender As Object, ByVal e As System.EventArgs) Handles mnuEdit.DropDownClosed

    mnuEdit.Enabled = True
    mnuEditPaste.Enabled = True
    mnuEditPasteV.Enabled = True
    mnuEditPasteNew.Enabled = True
    mnuEditDeleteSelection.Enabled = True
    mnuEditDeleteFile.Enabled = True
    mnuEditRevert.Enabled = True
    mnuEditDeletePage.Enabled = True

  End Sub

  Private Sub mnuEdit_DropDownOpening(sender As Object, e As EventArgs) Handles mnuEdit.DropDownOpening

    Dim i As Integer

    mnuEditSelectSimilar.Visible = False ' needs to be added eventually

    If mView.Bitmap Is Nothing Then
      mnuEdit.Enabled = False
      Exit Sub
    Else
      mnuEdit.Enabled = True
    End If

    If mViews.Count <= 0 Then Exit Sub

    If Clipboard.ContainsImage Then
      mnuEditPaste.Enabled = True
      mnuEditPasteV.Enabled = True
      mnuEditPasteNew.Enabled = True
    Else
      mnuEditPaste.Enabled = False
      mnuEditPasteV.Enabled = False
      mnuEditPasteNew.Enabled = False
    End If

    If mView.Bitmap IsNot Nothing AndAlso mView.FloaterBitmap IsNot Nothing Then
      mnuEditDeleteSelection.Enabled = True
    Else
      mnuEditDeleteSelection.Enabled = False
    End If

    If mView.picName <> "" And (mView.Bitmap IsNot Nothing) Then
      mnuEditDeleteFile.Enabled = True
      If mView.nUndo >= 1 Then mnuEditRevert.Enabled = True Else mnuEditRevert.Enabled = False
      For i = 0 To ToolStrip1.Items.Count - 1
        If ToolStrip1.Items.Item(i).Name = "mnueditdelete" Then
          ToolStrip1.Items.Item(i).Enabled = True
          Exit For
        End If
      Next i
    Else
      mnuEditDeleteFile.Enabled = False
      mnuEditRevert.Enabled = False
      For i = 0 To ToolStrip1.Items.Count - 1
        If ToolStrip1.Items.Item(i).Name = "mnueditdelete" Then
          ToolStrip1.Items.Item(i).Enabled = False
          Exit For
        End If
      Next i
    End If

    If mView.nUndo >= 1 Then mnuEditUndo.Enabled = True Else mnuEditUndo.Enabled = False
    If mView.nRedo > mView.nUndo Then mnuEditRedo.Enabled = True Else mnuEditRedo.Enabled = False

    If mView.PageCount > 1 Then
      mnuEditDeletePage.Enabled = True
    Else
      mnuEditDeletePage.Enabled = False
    End If

    If lastCmdSub IsNot Nothing Then
      mnuEditRepeatCommand.Enabled = True
    Else
      mnuEditRepeatCommand.Enabled = False
    End If

    If mView.FloaterBitmap IsNot Nothing Then
      mnuEditCut.Enabled = True
      mnuEditInvertSelection.Enabled = True
    Else
      mnuEditCut.Enabled = False
      mnuEditInvertSelection.Enabled = False
    End If

  End Sub

  Private Sub mnuDraw_DropDownOpening(sender As Object, e As EventArgs) Handles mnuDraw.DropDownOpening

    mnuDrawFillSelection.Enabled = False
    If (mView.Bitmap IsNot Nothing) Then
      If Command = cmd.Floater Then mnuDrawFillSelection.Enabled = True
    End If

  End Sub

  Private Sub mnuImage_DropDownOpening(sender As Object, e As EventArgs) Handles mnuImage.DropDownOpening

    If rBoxMode = rbMode.readyBoxDrag And Command = cmd.Crop Then
      mnuImageCrop.Enabled = True
    Else
      mnuImageCrop.Enabled = False
    End If

  End Sub

  Private Sub mnuColorBrightness_click(sender As Object, e As EventArgs) Handles mnuColorBrightness.Click
    CommandPrep(AddressOf mnuColorBrightness_click, False)
    Command = cmd.Brightness
    setPanelBright()
  End Sub

  Private Sub mnuColorGrayscale_click(sender As Object, e As EventArgs) Handles mnuColorGrayscale.Click
    Me.Cursor = Cursors.WaitCursor
    CommandPrep(AddressOf mnuColorGrayscale_click)
    SaveUndo()
    mView.MakeGrayscale()
    mView.Zoom()
    Me.Cursor = Cursors.Default
  End Sub

  Private Sub mnuColorNegative_click(sender As Object, e As EventArgs) Handles mnuColorNegative.Click
    CommandPrep(AddressOf mnuColorNegative_click)
    Me.Cursor = Cursors.WaitCursor
    SaveUndo()
    mView.Invert()
    mView.Zoom()
    Me.Cursor = Cursors.Default
  End Sub

  Private Sub mnuColorHalftone_click(sender As Object, e As EventArgs) Handles mnuColorHalftone.Click
    CommandPrep(AddressOf mnuColorHalftone_click, False)
    showEffectForm(frmHalftone)
  End Sub

  Private Sub mnuColorSample_click(sender As Object, e As EventArgs) Handles mnuColorSample.Click
    Command = cmd.ColorSample
    mView.Cursor = cursorDropper
  End Sub

  Private Sub mnuDrawFill_click(sender As Object, e As EventArgs) Handles mnuDrawFill.Click

    'If mView.FloaterBitmap IsNot Nothing Then mView.assignFloater()
    CommandPrep(AddressOf mnuDrawFill_click, False)

    mView.Cursor = cursorFill
    Command = cmd.DrawFill
    clickPoint = Point.Empty
    setPanelTolerance()
  End Sub
  Private Sub mnuDrawFillSelection_click(sender As Object, e As EventArgs) Handles mnuDrawFillSelection.Click
    CommandPrep(AddressOf mnuDrawFillSelection_click, False)
    SaveUndo()
    fillFloater(mainColor)
  End Sub

  Private Sub mnuDrawLine_Click(sender As Object, e As EventArgs) Handles mnuDrawLine.Click
    CommandPrep(AddressOf mnuDrawLine_Click)
    mView.Cursor = cursorDrawLine
    Command = cmd.DrawLine
    startLineDraw()
  End Sub
  Private Sub mnuDrawCurve_Click(sender As Object, e As EventArgs) Handles mnuDrawCurve.Click
    CommandPrep(AddressOf mnuDrawCurve_Click)
    Command = cmd.DrawCurve
    mView.Cursor = cursorDrawCurve
    startLineDraw()
  End Sub
  Private Sub mnuDrawArrow_Click(sender As Object, e As EventArgs) Handles mnuDrawArrow.Click
    CommandPrep(AddressOf mnuDrawArrow_Click)
    Command = cmd.DrawArrow
    mView.Cursor = cursorDrawArrow
    startLineDraw()
  End Sub
  Private Sub mnuDrawCircle_Click(sender As Object, e As EventArgs) Handles mnuDrawCircle.Click
    CommandPrep(AddressOf mnuDrawCircle_Click)
    mView.Cursor = cursorDrawCircle
    Command = cmd.DrawCircle
    startLineDraw()
  End Sub
  Private Sub mnuDrawEllipse_Click(sender As Object, e As EventArgs) Handles mnuDrawEllipse.Click
    CommandPrep(AddressOf mnuDrawEllipse_Click)
    mView.Cursor = cursorDrawCircle
    Command = cmd.DrawEllipse
    startLineDraw()
  End Sub
  Private Sub mnuDrawBox_Click(sender As Object, e As EventArgs) Handles mnuDrawBox.Click
    CommandPrep(AddressOf mnuDrawBox_Click)
    Command = cmd.DrawBox
    mView.Cursor = cursorDrawBox
    startLineDraw()
  End Sub

  Private Sub startLineDraw()

    If Command = cmd.Floater Then mView.assignFloater()
    DrawingLine = 1
    drawingMode = 1 ' enable cursor keys

  End Sub

  Private Sub mnuDrawLineOptions_click(sender As Object, e As EventArgs) Handles mnuDrawLineOptions.Click
    Using frm As New frmLineOptions
      dResult = frm.ShowDialog()
    End Using
  End Sub

  Private Sub mnuDrawSketch_click(sender As Object, e As EventArgs) Handles mnuDrawSketch.Click
    CommandPrep(AddressOf mnuDrawSketch_click)
    Command = cmd.DrawSketch
    mView.Cursor = cursorSketch
    startLineDraw()
  End Sub

  Private Sub mnuDrawText_click(sender As Object, e As EventArgs) Handles mnuDrawText.Click
    CommandPrep(AddressOf mnuDrawText_click)
    setPanelText()
  End Sub

  '  Sub drawMviewText(textString As String, p As Point)
  ' ' drawMviewText is not used -- saved for gpath example
  '
  'Dim tSize As SizeF
  'Dim mx As New Matrix
  'Dim pp As New List(Of Point)
  'Dim sine, cosine As Double
  '
  '    if textString = "" Then Exit Sub
  '
  '    Using gPath As New GraphicsPath,
  '          xFont As Font = getFont(mView.ZoomFactor),
  '          g As Graphics = mView.CreateGraphics
  '
  '      tSize = g.MeasureString(textString, xFont, p, textFmt)
  '
  '  'r = New Rectangle(p.X - tSize.Width \ 2, p.Y - tSize.Height \ 2, tSize.Width, tSize.Height)
  '      pp.Add(New Point(p.X - tSize.Width \ 2 - xFont.Size * 0.3, p.Y - tSize.Height \ 2))
  '      pp.Add(New Point(pp(0).X + tSize.Width + xFont.Size * 0.6, pp(0).Y))
  '      pp.Add(New Point(pp(1).X, p.Y + tSize.Height \ 2))
  '      pp.Add(New Point(pp(0).X, pp(2).Y))
  '
  '      If textAngle <> 0 Then
  '  ' rotate the rectangle
  '        sine = Sin(-textAngle * piOver180) : cosine = Cos(-textAngle * piOver180)
  '        For i As Integer = 0 To pp.Count - 1
  '          rotatePoint(pp(i), p, sine, cosine)
  '        Next i
  '  ' rotate matrix for gpathath
  '        mx.Translate(p.X, p.Y)
  '        mx.Rotate(-textAngle)
  '        mx.Translate(-p.X, -p.Y)
  '      End If
  '
  '      If textBackFill Then
  '        mView.RubberPoints = pp
  '        mView.rubberBackColor = textBackColor
  '      Else
  '        mView.RubberPoints = New List(Of Point)
  '      End If
  '
  ' use rubberband mode
  '      gPath.AddString(textString, xFont.FontFamily, xFont.Style, xFont.Size, p, textFmt) ' center text about p
  '      gPath.Transform(mx)
  '      mView.RubberColor = textColor
  '      mView.RubberPath = gPath.Clone
  '      mView.Invalidate()
  '
  '    End Using
  '
  '  End Sub

  Private Sub mnuDrawColor_Click(sender As Object, e As EventArgs) Handles mnuDrawColor.Click

    colorDialog1.Color = mainColor
    colorDialog1.FullOpen = True
    Try
      dResult = colorDialog1.ShowDialog()
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    If dResult = DialogResult.OK Then
      mainColor = colorDialog1.Color
      setToolColor("mnudrawforecolor", mainColor)
    End If

  End Sub

  Private Sub mnuDrawBackColor_Click(sender As Object, e As EventArgs) Handles mnuDrawBackColor.Click

    colorDialog1.Color = mBackColor
    colorDialog1.FullOpen = True
    Try
      dResult = colorDialog1.ShowDialog()
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    If dResult = DialogResult.OK Then
      mBackColor = colorDialog1.Color
      setToolColor("mnudrawbackcolor", mBackColor)
    End If

  End Sub

  Private Sub mnuEditCopy_click(sender As Object, e As EventArgs) _
    Handles mnuEditCopy.Click, mnuEditCut.Click

    Dim clipData As New DataObject
    Dim bmp As Bitmap

    CommandPrep(AddressOf mnuEditCopy_click, False)
    If Command <> cmd.None Then CompleteCommand()

    mView.Cursor = Cursors.WaitCursor

    If mView.FloaterBitmap Is Nothing Or mView.FloaterPath Is Nothing Then ' copy the whole image
      Clipboard.SetImage(mView.Bitmap)
    Else ' copy the floater and path
      ' use clipData to get two objects into the clipboard.
      clipData = New DataObject()
      clipData.SetData("gpathPoints", mView.FloaterPath.PathPoints)
      clipData.SetData("gpathTypes", mView.FloaterPath.PathTypes)
      bmp = New Bitmap(mView.FloaterBitmap.Width, mView.FloaterBitmap.Height, PixelFormat.Format32bppPArgb)
      Using g As Graphics = Graphics.FromImage(bmp)
        g.Clear(Color.Transparent) ' set transparent background for other programs (Windows 8+)
        g.SetClip(mView.FloaterPath)
        g.DrawImage(mView.FloaterBitmap, New Rectangle(0, 0, mView.FloaterBitmap.Width, mView.FloaterBitmap.Height))
      End Using
      clipData.SetImage(bmp) ' do not dispose bmp
      Clipboard.SetDataObject(clipData)
    End If

    If sender Is mnuEditCut Then ' cut - erase (fill) selection
      SaveUndo()
      mView.Fill(mBackColor, mView.FloaterBitmap, mView.FloaterPath)
    End If

    Command = cmd.None
    mView.Cursor = Cursors.Default

  End Sub

  Private Sub mnuEditDeleteFile_click(sender As Object, e As EventArgs) Handles mnuEditDeleteFile.Click

    Dim mResult As MsgBoxResult
    Dim iattr As Integer
    Dim filePath As String = ""

    If mView.picName = "" Then Exit Sub
    CommandPrep(AddressOf mnuEditDeleteFile_click, False)

    mResult = MsgBox("Do you really want to delete """ & mView.picName & """?", MsgBoxStyle.YesNoCancel)

    If mResult = MsgBoxResult.No Or mResult = MsgBoxResult.Cancel Then Exit Sub

    iattr = GetAttr(mView.picName)
    mResult = MsgBoxResult.Ignore ' not yes
    If (iattr And 1) <> 0 Then ' readonly
      mResult = MsgBox(filePath & " is a read-only file. Do you want to erase it anyway?", MsgBoxStyle.YesNoCancel)
      If mResult = MsgBoxResult.Cancel Then Exit Sub
      If mResult = MsgBoxResult.Yes Then
        Try
          SetAttr(filePath, FileAttribute.Normal)
        Catch ex As Exception
          MsgBox(ex.Message)
          Exit Sub
        End Try
      End If
    End If

    If (iattr And 31) = 0 Or mResult = MsgBoxResult.Yes Then
      Try
        File.Delete(mView.picName)
        If iniDelRawFiles Then delMatchingRawFile(mView.picName)
      Catch ex As Exception
        MsgBox(ex.Message)
        Exit Sub
      End Try
    End If

    mnuFileClose_click(sender, e)

  End Sub

  Private Sub mnuEditDeleteSelection_click(sender As Object, e As EventArgs) Handles mnuEditDeleteSelection.Click
    ' delete selection -- color the floater and assign it to the image

    If (mView.Bitmap Is Nothing) Or (mView.BitmapPath Is Nothing) Or mView.ctlText.Visible Then Exit Sub

    Me.Cursor = Cursors.WaitCursor
    CommandPrep(AddressOf mnuEditDeleteSelection_click, False)
    SaveUndo()

    Try
      If mView.FloaterBitmap IsNot Nothing Then
        Using g As Graphics = Graphics.FromImage(mView.FloaterBitmap)
          g.SetClip(mView.FloaterPath)
          g.Clear(mBackColor)
        End Using
      End If
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    mView.assignFloater()
    CompleteCommand()

  End Sub

  Private Sub mnuEditPaste_click(sender As Object, e As EventArgs) Handles mnuEditPaste.Click

    Dim clipData As DataObject
    Dim gPoints() As PointF
    Dim gTypes() As Byte

    CommandPrep(AddressOf mnuEditPaste_click, False)

    If Clipboard.ContainsData("dataobject") Then
      clipData = Clipboard.GetDataObject()
      gPoints = clipData.GetData("gpathPoints")
      gTypes = clipData.GetData("gpathTypes")
      If gPoints IsNot Nothing And gTypes IsNot Nothing And clipData.GetDataPresent(GetType(Bitmap)) Then
        Using gPath As New GraphicsPath(gPoints, gTypes),
              bmp As Bitmap = clipData.GetImage()
          If gPath IsNot Nothing AndAlso gPath.PointCount > 0 AndAlso bmp IsNot Nothing Then ' save floater

            'mView.BitmapToControl(gPath)
            mView.setFloaterBitmap(bmp)
            'mView.SetSelection(gPath)
            If mView.FloaterPath IsNot Nothing Then mView.FloaterPath.Dispose() : mView.FloaterPath = Nothing
            mView.FloaterPath = gPath.Clone
            mView.FloaterPosition = New Point(mView.Bitmap.Width \ 2, mView.Bitmap.Height \ 2)
            startFloater()
            mView.Zoom()
          End If
        End Using
      End If
    Else
      Using bmp As Bitmap = Clipboard.GetImage
        If bmp IsNot Nothing Then mView.setBitmap(bmp)
      End Using
    End If

  End Sub

  Private Sub mnuEditPasteV_click(sender As Object, e As EventArgs) Handles mnuEditPasteV.Click
    ' assign qImage for the stretch

    CommandPrep(AddressOf mnuEditPasteV_click)

    If Not Clipboard.ContainsImage Then
      ClearProcesses(True)
      Exit Sub
    End If

    ' Copy the bitmap from the clipboard
    qImage = Clipboard.GetImage
    If qImage IsNot Nothing AndAlso qImage.Width > 0 Then
      stretchAspect = qImage.Height / qImage.Width
      rX(0) = 0
      rY(0) = (qImage.Height - 1) * mView.ZoomFactor
      rX(1) = (qImage.Width - 1) * mView.ZoomFactor
      rY(1) = (qImage.Height - 1) * mView.ZoomFactor
      rX(2) = 0
      rY(2) = 0
      rX(3) = (qImage.Width - 1) * mView.ZoomFactor
      rY(3) = 0

      DrawingStretch = 1
      Command = cmd.PasteV
      drawingMode = 1 ' enable cursor keys
      mView.RubberEnabled = True
      mView.RubberShape = shape.Polygon
      mView.RubberBoxCrop = True
      mView.RubberDashed = True
      mView.RubberColor = Color.Black
      mView.rubberBackColor = Color.White
      mView.Cursor = cursorStretchPaste
      npts = 0

    Else '  qimage = Nothing or width = 0
      ClearProcesses(True)
    End If

  End Sub

  Private Sub mnuImageCrop_click(sender As Object, e As EventArgs) Handles mnuImageCrop.Click
    If rBoxMode = rbMode.readyBoxDrag Then rBoxDone()
  End Sub

  Private Sub mnuImageFlip_click(sender As Object, e As EventArgs) _
    Handles mnuImageFlipHoriz.Click, mnuImageFlipVert.Click

    CommandPrep(AddressOf mnuImageFlip_click, False)
    SaveUndo()

    If sender Is mnuImageFlipHoriz Then
      mView.Bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX)
    Else
      mView.Bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY)
    End If

    mView.Zoom()

  End Sub

  Private Sub mnuImageAutoCrop_click(sender As Object, e As EventArgs) Handles mnuImageAutoCrop.Click

    CommandPrep(AddressOf mnuImageAutoCrop_click, False)
    SaveUndo()

    autoCrop(mView)

    mView.Zoom()

  End Sub

  Private Sub mnuEditRedo_click(sender As Object, e As EventArgs) Handles mnuEditRedo.Click

    ClearProcesses(True)
    LoadRedo()

  End Sub

  Sub mnuEditInvertSelection_click(sender As Object, e As EventArgs) Handles mnuEditInvertSelection.Click

    If mView.BitmapPath Is Nothing Then Exit Sub

    mView.BitmapPath.AddRectangle(New Rectangle(0, 0, mView.Bitmap.Width, mView.Bitmap.Height))

    Command = cmd.Floater
    mView.RubberDashed = True
    setPanelFeather()

  End Sub

  Private Sub mnuImageResize_click(sender As Object, e As EventArgs) Handles mnuImageResize.Click
    CommandPrep(AddressOf mnuImageResize_click)
    Using frm As New frmResize
      dResult = frm.ShowDialog()
    End Using
  End Sub

  Private Sub mnuImageRotate_click(sender As Object, e As EventArgs) Handles mnuImageRotate.Click

    CommandPrep(AddressOf mnuImageRotate_click, False)
    Me.Cursor = Cursors.WaitCursor

    Using frm As New frmRotate
      dResult = frm.ShowDialog()
    End Using

    If dResult = System.Windows.Forms.DialogResult.OK Then
      KeepNewBitmap(qImage)
      redraw(mView, 3)
    End If
    Me.Cursor = Cursors.Default

  End Sub

  Sub rotateLeftRight(ByVal index As Integer)

    Dim angle As Integer
    Me.Cursor = Cursors.WaitCursor
    SaveUndo()

    If index = 1 Then angle = 270 Else angle = 90

    mView.rotate(angle)

    Me.Cursor = Cursors.Default

  End Sub

  Private Sub mnuEditSelectRectangle_click(sender As Object, e As EventArgs) Handles mnuEditSelectRectangle.Click
    editSelectInteractive(0)
  End Sub
  Private Sub mnuEditSelectEllipse_click(sender As Object, e As EventArgs) Handles mnuEditSelectEllipse.Click
    editSelectInteractive(2)
  End Sub
  Private Sub mnuEditSelectFreehand_click(sender As Object, e As EventArgs) Handles mnuEditSelectFreehand.Click
    editSelectInteractive(3)
  End Sub

  Sub editSelectInteractive(ByVal index As Integer)

    ClearProcesses(True)

    mView.RubberEnabled = True
    mView.RubberBoxCrop = False
    mView.RubberDashed = True
    mView.RubberColor = Color.Black
    mView.rubberBackColor = Color.White

    Select Case index
      Case 0
        mView.Cursor = cursorSelRectangle
        Command = cmd.SelectRectangle
        mView.RubberShape = shape.Box

      Case 2
        mView.Cursor = cursorSelEllipse
        Command = cmd.SelectEllipse
        mView.RubberShape = shape.Ellipse

      Case 3
        mView.Cursor = cursorSketch
        Command = cmd.SelectSketch
        zP = New List(Of Point)
        mView.RubberShape = shape.Polygon

      Case Else
        Command = cmd.None
    End Select

  End Sub

  Private Sub mnuEditUndo_click(sender As Object, e As EventArgs) Handles mnuEditUndo.Click

    ClearProcesses(True)
    LoadUndo() ' loads into mview.bitmap

  End Sub

  Private Sub mnuEditRevert_click(sender As Object, e As EventArgs) Handles mnuEditRevert.Click
    If mView.picName <> "" Then
      showPicture(mView.picName, mView, False, mView.pComments)
      redraw(mView, 3)
      mView.picModified = False
    End If
  End Sub

  Private Sub mnuImageLightAmp_click(sender As Object, e As EventArgs) Handles mnuImageLightAmp.Click
    CommandPrep(AddressOf mnuImageLightAmp_click, False)
    showEffectForm(frmAmp)
  End Sub

  Private Sub mnuImageBlur_click(sender As Object, e As EventArgs) Handles mnuImageBlur.Click
    CommandPrep(AddressOf mnuImageBlur_click, False)
    showEffectForm(frmBlur)
  End Sub

  Private Sub mnuImageContrastStretch_Click(sender As Object, e As EventArgs) Handles mnuImageContrastStretch.Click
    CommandPrep(AddressOf mnuImageContrastStretch_Click, False)
    showEffectForm(frmContrastStretch)
  End Sub

  Private Sub mnuColorAdjust_click(sender As Object, e As EventArgs) Handles mnuColorAdjust.Click
    CommandPrep(AddressOf mnuColorAdjust_click, False)
    showEffectForm(frmColorAdjust)
  End Sub

  Private Sub mnuImageMedian_click(sender As Object, e As EventArgs) Handles mnuImageMedian.Click
    CommandPrep(AddressOf mnuImageMedian_click, False)
    showEffectForm(frmMedian)
  End Sub

  Private Sub mnuImageEffects_click(sender As Object, e As EventArgs) Handles mnuImageEffects.Click
    CommandPrep(AddressOf mnuImageEffects_click, False)
    showEffectForm(frmSpecialEffects)
  End Sub

  Private Sub mnuImageArtEffects_Click(sender As Object, e As EventArgs) Handles mnuImageArtEffects.Click
    CommandPrep(AddressOf mnuImageEffects_click, False)
    showEffectForm(frmArtEffects)
  End Sub

  Private Sub mnuImageExpand_click(sender As Object, e As EventArgs) Handles mnuImageExpand.Click

    CommandPrep(AddressOf mnuImageExpand_click)
    Me.Cursor = Cursors.WaitCursor
    Using frm As New frmExpand
      dResult = frm.ShowDialog()
    End Using
    If dResult = DialogResult.OK Then
      KeepNewBitmap(qImage)
      redraw(mView, 3)
    End If
    Me.Cursor = Cursors.Default

  End Sub

  Private Sub mnuFileClose_click(sender As Object, e As EventArgs) Handles mnuFileClose.Click
    Dim Success As Boolean
    Success = closeTab() ' ignore return
  End Sub

  Private Sub mnuToolsMeasure_Click(sender As Object, e As EventArgs) Handles mnuToolsMeasure.Click
    CommandPrep(AddressOf mnuToolsMeasure_Click)
    mView.Cursor = cursorDrawLine
    Command = cmd.DrawMeasure
    startLineDraw()
  End Sub

  Private Sub mnuToolsConcatenate_Click(sender As System.Object, ByVal e As System.EventArgs) Handles mnuToolsConcatenate.Click

    Dim n As Integer
    Dim mv As mudViewer

    CommandPrep(AddressOf mnuToolsCombine_click)
    n = 0

    If combineRview IsNot Nothing Then combineRview.Dispose() : combineRview = Nothing
    For Each mv In mViews
      ' look for other open photos
      If mv IsNot mView Then
        combineRview = mv
        n = n + 1
      End If
    Next mv

    If n >= 2 Then
      Using frm As New frmCombineSelection
        dResult = frm.ShowDialog()
      End Using
      If dResult = DialogResult.Cancel Or combineRview Is Nothing Then Exit Sub
    ElseIf n <= 0 Then
      MsgBox("First, open the photo you want to concatenate tothis one. Both photos need to be opened at the same time before you concatenate them.")
      Exit Sub
    End If

    ClearProcesses(True)

    Me.Cursor = Cursors.WaitCursor
    Using frm As New frmConcatenate
      dResult = frm.ShowDialog()
    End Using

    If dResult = DialogResult.OK And qImage IsNot Nothing Then
      mView.ClearFloater()
      KeepNewBitmap(qImage)
    End If
    combineRview = Nothing

    Me.Cursor = Cursors.Default

  End Sub

  Private Sub mnuToolsCombine_click(sender As Object, e As EventArgs) Handles mnuToolsCombine.Click

    Dim n As Integer
    Dim mv As mudViewer

    CommandPrep(AddressOf mnuToolsCombine_click)
    n = 0

    If combineRview IsNot Nothing Then combineRview.Dispose() : combineRview = Nothing
    For Each mv In mViews
      ' look for other open photos
      If mv IsNot mView Then
        combineRview = mv
        n = n + 1
      End If
    Next mv

    If n > 1 Then
      Using frm As New frmCombineSelection
        dResult = frm.ShowDialog()
      End Using
      If dResult = DialogResult.Cancel Or combineRview Is Nothing Then Exit Sub
    ElseIf n <= 0 Then
      MsgBox("First, open the photo you want to combine with this one. Both photos need to be opened at the same time before you combine them.")
      Exit Sub
    End If

    Me.Cursor = Cursors.WaitCursor
    Using frm As New frmCombine
      dResult = frm.ShowDialog()
    End Using
    If dResult = DialogResult.OK And qImage IsNot Nothing Then
      mView.ClearFloater()
      KeepNewBitmap(qImage)
    End If

    combineRview = Nothing
    ClearProcesses(False)
    Me.Cursor = Cursors.Default

  End Sub

  Private Sub mnuFilePrint_click(sender As Object, e As EventArgs) Handles mnuFilePrint.Click

    CommandPrep(AddressOf mnuFilePrint_click)
    If mView.Bitmap Is Nothing Then Exit Sub
    MultiFile = False
    callingForm = Me
    Me.Cursor = Cursors.WaitCursor
    qImage = mView.Bitmap.Clone
    Using frm As New frmPrint
      dResult = frm.ShowDialog()
    End Using
    clearBitmap(qImage)
    Me.Cursor = Cursors.Default

  End Sub

  Private Sub mnuFileSave_click(sender As Object, e As EventArgs) Handles mnuFileSave.Click

    Dim picFormat As MagickFormat
    Dim saver As ImageSave
    Dim msg As String

    If mView.FloaterBitmap IsNot Nothing Then mView.assignFloater()
    Me.Refresh()

    picFormat = getFormat(mView.picName)
    If picFormat >= 0 AndAlso MagickNET.SupportedFormats(picFormat).IsWritable Then
      saver = New ImageSave
      If mView.pComments IsNot Nothing Then ' added 7/30/17
        saver.pComments = New List(Of PropertyItem)
        saver.pComments.AddRange(mView.pComments)
      End If
      saver.Quality = iniJpgQuality
      saver.sourceFilename = mView.picName
      saver.saveExif = True
      saver.copyProfiles = True
      msg = saver.write(mView.Bitmap, mView.picName, True)
      If msg <> "" Then
        MsgBox(mView.picName & " could not be saved." & crlf & msg, MsgBoxStyle.OkOnly)
      Else
        mView.picModified = False
        mView.originalFileName = mView.picName
        AddMruFile(mView.picName)
      End If

    Else
      FileSaveAs(0)
    End If

  End Sub

  Private Sub mnuFileSaveAs_click(sender As Object, e As EventArgs) Handles mnuFileSaveAs.Click
    CommandPrep(AddressOf mnuFileSaveAs_click)
    FileSaveAs(0) ' file saveAs
  End Sub

  Private Sub mnuFileSaveSelection_Click(sender As Object, e As EventArgs) Handles mnuFileSaveSelection.Click
    CommandPrep(AddressOf mnuFileSaveSelection_Click, False)
    FileSaveAs(1) ' file saveSelection
  End Sub

  Sub FileSaveAs(ByVal index As Integer)
    ' index 1 = save floater, index 0 = save image

    If index = 0 And mView.FloaterBitmap IsNot Nothing Then mView.assignFloater()
    If index = 1 And mView.FloaterBitmap Is Nothing Then Exit Sub

    callingForm = Me
    Using frm As New frmSaveAs
      dResult = frm.ShowDialog()
    End Using

    If index = 1 Then
      mView.ClearFloater()
      mView.ClearSelection()
      mView.ctlFeather.Visible = False
      mView.Refresh()
    End If

    StatusLine()

  End Sub

  Private Sub mnuFileSend_click(sender As Object, e As EventArgs) Handles mnuFileSend.Click

    If mView.Bitmap Is Nothing Then Exit Sub
    CommandPrep(AddressOf mnuFileSend_click)

    If mView.picModified Or Len(mView.picName) = 0 OrElse Len(mView.picPage) <> 0 Then forceConverted = True
    MultiFile = False
    currentpicPath = mView.picName
    callingForm = Me
    Me.Cursor = Cursors.WaitCursor
    Using frm As New frmSend
      dResult = frm.ShowDialog()
    End Using
    Me.Cursor = Cursors.Default

  End Sub

  Private Sub mnuImageSetCrop_click(sender As Object, e As EventArgs) Handles mnuImageSetCrop.Click

    CommandPrep(AddressOf mnuImageSetCrop_click)
    Command = cmd.Crop
    rBoxMode = rbMode.readyMouseDown
    mView.Cursor = cursorCrop
    mView.RubberEnabled = True
    mView.RubberShape = shape.Box
    mView.RubberBoxCrop = True
    mView.RubberDashed = True
    mView.RubberColor = Color.Black
    mView.rubberBackColor = Color.White
    imageTmp = mView.Bitmap.Clone

  End Sub

  Private Sub mnuViewZoomWindow_click(sender As Object, e As EventArgs) Handles mnuViewZoomWindow.Click
    ' CommandPrep(AddressOf mnuViewZoomWindow_click)
    mView.Cursor = cursorZoom
    Command = cmd.ZoomWindow
    mView.RubberEnabled = True
    mView.RubberShape = shape.Box
    mView.RubberBoxCrop = False
    mView.RubberDashed = True
    mView.RubberColor = Color.Black
    mView.rubberBackColor = Color.White
  End Sub

  Private Sub mnuImageSharpen_click(sender As Object, e As EventArgs) Handles mnuImageSharpen.Click
    CommandPrep(AddressOf mnuImageSharpen_click, False)
    showEffectForm(frmSharpen)
  End Sub

  Private Sub mnuImageStretch_click(sender As Object, e As EventArgs) Handles mnuImageStretch.Click, mnuImageAlign.Click
    ' use stretchpaste

    CommandPrep(AddressOf mnuImageStretch_click, False)
    Command = cmd.None

    If mView.FloaterBitmap Is Nothing Then
      qImage = mView.Bitmap.Clone ' returns a clone
    Else
      qImage = mView.FloaterBitmap.Clone
    End If

    If qImage IsNot Nothing AndAlso qImage.Width > 0 Then
      stretchAspect = qImage.Height / qImage.Width
      rX(0) = 0
      rY(0) = (qImage.Height - 1) * 0.75 * mView.ZoomFactor
      rX(1) = (qImage.Width - 1) * 0.75 * mView.ZoomFactor
      rY(1) = (qImage.Height - 1) * 0.75 * mView.ZoomFactor
      rX(2) = 0
      rY(2) = 0
      rX(3) = (qImage.Width - 1) * 0.75 * mView.ZoomFactor
      rY(3) = 0

      DrawingStretch = 1
      If sender Is mnuImageStretch Then
        Command = cmd.ImageStretch
      Else
        Command = cmd.ImageAlign
      End If
      drawingMode = 1 ' enable cursor keys
      mView.RubberEnabled = True
      mView.RubberShape = shape.Polygon
      mView.RubberBoxCrop = True
      mView.RubberDashed = True
      mView.RubberColor = Color.Black
      mView.rubberBackColor = Color.White
      mView.Cursor = cursorStretchPaste
      npts = 0

    Else
      ClearProcesses(True)
    End If

  End Sub

  Private Sub mnuColorHisto_click(sender As Object, e As EventArgs) Handles mnuColorHisto.Click
    CommandPrep(AddressOf mnuColorHisto_click, False)
    showEffectForm(frmHisto)
  End Sub

  Private Sub mnuColorEnhance_click(sender As Object, e As EventArgs) Handles mnuColorEnhance.Click

    mView.Cursor = Cursors.WaitCursor
    SaveUndo()

    Using img As MagickImage = New MagickImage(mView.Bitmap)
      img.Normalize()
      Using bmp As Bitmap = img.ToBitmap
        mView.setBitmap(bmp)
      End Using
    End Using

    mView.Zoom()
    mView.Cursor = Cursors.Default

  End Sub

  Sub mnuImageEdgeDetect_click(sender As Object, e As EventArgs) Handles mnuImageEdgeDetect.Click
    CommandPrep(AddressOf mnuImageEdgeDetect_click, False)
    showEffectForm(frmEdgeDetect)
  End Sub

  Sub mnuImageBorder_click(sender As Object, e As EventArgs) Handles mnuImageBorder.Click
    CommandPrep(AddressOf mnuImageBorder_click)
    showEffectForm(frmBorder)
  End Sub

  Sub mnuToolsInfo_click(sender As Object, e As EventArgs) Handles mnuToolsInfo.Click

    CommandPrep(AddressOf mnuToolsInfo_click, False)
    Me.Cursor = Cursors.WaitCursor

    currentpicPath = mView.picName ' currentPicPath is a global for use for browsing in frmInfo
    callingForm = Me
    Using frm As New frmInfo
      dResult = frm.ShowDialog()
    End Using
    Me.Select()
    Me.Cursor = Cursors.Default

  End Sub

  Sub mnuToolsComment_click(sender As Object, e As EventArgs) Handles mnuToolsComment.Click

    CommandPrep(AddressOf mnuToolsComment_click, False)

    currentpicPath = mView.picName
    mView.newComment = False
    callingForm = Me
    Using frm As New frmComment
      dResult = frm.ShowDialog()  ' does not save to the file (only in explorer or frmfullscreen)
    End Using
    If dResult = DialogResult.OK And mView.newComment Then mView.picModified = True

  End Sub

  Sub mnuToolsBugPhotos_click(sender As Object, e As EventArgs) Handles mnuToolsBugPhotos.Click

    CommandPrep(AddressOf mnuToolsBugPhotos_click, False)

    currentpicPath = mView.picName
    mView.newComment = False
    callingForm = Me
    Using frm As New frmBugPhotos
      dResult = frm.ShowDialog()
    End Using

  End Sub

  Sub mnuToolsPicSearch_click(sender As Object, e As EventArgs) Handles mnuToolsPicSearch.Click

    CommandPrep(AddressOf mnuToolsPicSearch_click)
    currentpicPath = mView.picName
    If iniExplorePath = "" Then
      'SeparatePath(mView.picName, iniExplorePath, Name)
      iniExplorePath = Path.GetDirectoryName(mView.picName)
    End If
    callingForm = Me
    Using frm As New frmDupSearch
      dResult = frm.ShowDialog()
    End Using

  End Sub

  Private Sub mnuEditSelectSimilar_click(sender As Object, e As EventArgs) Handles mnuEditSelectSimilar.Click
    ''` this does not work. It needs to be added eventually.
    ''If mView.FloaterBitmap IsNot Nothing Then setFloater()
    Exit Sub

    CommandPrep(AddressOf mnuEditSelectSimilar_click, False)
    mView.Cursor = cursorSelectSimilar
    Command = cmd.SelectSimilar
    clickPoint = Point.Empty
    setPanelTolerance()

  End Sub

  Private Sub mnuToolsAssoc_click(sender As Object, e As EventArgs) Handles mnuToolsAssoc.Click

    CommandPrep(AddressOf mnuToolsAssoc_click, False)
    Using frm As New frmFileAssoc
      dResult = frm.ShowDialog()
    End Using

  End Sub

  Private Sub mnuImageRedeye_click(sender As Object, e As EventArgs) Handles mnuImageRedeye.Click
    CommandPrep(AddressOf mnuImageBorder_click)
    showEffectForm(frmRedeye)
  End Sub

  Private Sub mnuToolsPicturize_click(sender As Object, e As EventArgs) Handles mnuToolsPicturize.Click

    Dim i As Integer

    CommandPrep(AddressOf mnuToolsPicturize_click)
    i = 1 ' the form number, 1 or 2
    ' pczretc is the return code from the form

    If pczCellFolder = "" Then pczCellFolder = iniExplorePath
    Using frm As New frmPicturize1
      dResult = frm.ShowDialog()
    End Using

    Do While pczRetc = 0 Or pczRetc = 1 ' next or previous
      Select Case i

        Case 1 ' returned from form 1
          If pczRetc = 1 Then ' next
            Using frm As New frmPicturize2
              dResult = frm.ShowDialog()
            End Using
            i = 2
          Else ' invalid
            Exit Do
          End If

        Case 2 ' returned from form 2
          If pczRetc = 0 Then ' prev
            Using frm As New frmPicturize1
              dResult = frm.ShowDialog()
            End Using
            i = 1
          ElseIf pczRetc = 1 Or pczRetc = 3 Then  ' next or finish
            Using frm As New frmPicturize3
              dResult = frm.ShowDialog()
            End Using
            i = 3
          Else ' invalid
            Exit Do
          End If

        Case 3 ' returned from form 3
          If pczRetc = 0 Then ' prev
            Using frm As New frmPicturize2
              dResult = frm.ShowDialog()
            End Using
            i = 2
          Else ' invalid
            Exit Do
          End If
      End Select

    Loop

    If pczRetc = 2 Then Exit Sub ' cancel

    If qImage IsNot Nothing AndAlso qImage.Width > 0 Then ' save in a new frmMain
      mView = newWindow(qImage.Width, qImage.Height)
      mView.setBitmap(qImage)
      mView.picModified = True
      redraw(mView, 3)
      clearBitmap(qImage)
    End If

  End Sub
  Sub mnuToolsFilter_click(sender As Object, e As EventArgs) Handles mnuToolsFilter.Click

    CommandPrep(AddressOf mnuToolsFilter_click, False)
    Me.Cursor = Cursors.WaitCursor
    If mView.FloaterBitmap IsNot Nothing Then
      qImage = mView.FloaterBitmap.Clone
    Else
      qImage = mView.Bitmap.Clone
    End If
    Using frm As New frmFilter
      dResult = frm.ShowDialog()
    End Using
    If dResult = DialogResult.OK Then KeepNewBitmap(qImage)
    Me.Cursor = Cursors.Default

  End Sub

  Private Sub mnuToolsFileFilter_Click(sender As Object, e As EventArgs) Handles mnuToolsFileFilter.Click
    Using frm As New frmFileTypes
      dResult = frm.ShowDialog() ' sets ini filetypes
    End Using
  End Sub

  Private Sub mnuToolsWallpaper_click(sender As Object, e As EventArgs) Handles mnuToolsWallpaper.Click

    Dim fName As String = ""
    Dim fPath As String = ""
    Dim msg As String = ""
    Dim saver As New ImageSave

    CommandPrep(AddressOf mnuToolsWallpaper_click)
    fName = Path.GetFileName(mView.picName)
    fPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows)

    If Not fPath.EndsWith("\") Then fPath &= "\"

    fName = Path.GetFileNameWithoutExtension(fName) & ".jpg"
    fName = fPath & fName

    msg = saver.write(mView.Bitmap, fName, True)

    If msg = "" Then
      msg = SetWallPaper(fName)
      If msg = "" Then
        MsgBox("The wallpaper was created.")
      Else
        MsgBox("Oops!  Couldn't save the wallpaper." & crlf & msg, MsgBoxStyle.OkOnly)
      End If
    End If

  End Sub

  Private Sub mnuViewFullscreen_click(sender As Object, e As EventArgs) Handles mnuViewFullscreen.Click

    Dim fPath As String
    Dim i As Integer

    CommandPrep(AddressOf mnuViewFullscreen_click)
    Me.Cursor = Cursors.WaitCursor
    callingForm = Me
    currentpicPath = mView.picName ' used by frmFullScreen
    fPath = Path.GetDirectoryName(mView.picName)

    i = getFilePaths(fPath, localPics, False)
    iPic = 0  ' starting point used by frmFullScreen
    For i = 0 To localPics.Count - 1
      If eqstr(currentpicPath, localPics(i)) Then
        iPic = i
        Exit For
      End If
    Next i

    qImage = mView.Bitmap.Clone
    Using frm As New frmFullscreen
      dResult = frm.ShowDialog()
    End Using
    clearBitmap(qImage)

    Me.Cursor = Cursors.Default
    If String.Compare(currentpicPath, mView.picName, True) <> 0 Then ' frmFullscreen changes currentPicPath
      OpenDoc(currentpicPath)
    End If

  End Sub

  Private Sub mnuViewRefresh_click(sender As Object, e As EventArgs) Handles mnuViewRefresh.Click
    redraw(mView, 0)
  End Sub

  Private Sub mnuViewZoomIn_click(sender As Object, e As EventArgs) Handles mnuViewZoomIn.Click
    ViewZoom(0)
  End Sub
  Private Sub mnuViewZoomout_click(sender As Object, e As EventArgs) Handles mnuViewZoomOut.Click
    ViewZoom(1)
  End Sub
  Private Sub mnuViewZoomfit_click(sender As Object, e As EventArgs) Handles mnuViewZoomFit.Click
    ViewZoom(7)
  End Sub
  Private Sub mnuViewZoom25_click(sender As Object, e As EventArgs) _
  Handles mnuViewZoom25.Click
    ViewZoom(2)
  End Sub
  Private Sub mnuViewZoom50_click(sender As Object, e As EventArgs) _
    Handles mnuViewZoom50.Click
    ViewZoom(3)
  End Sub
  Private Sub mnuViewZoom100_click(sender As Object, e As EventArgs) _
    Handles mnuViewZoom100.Click
    ViewZoom(4)
  End Sub
  Private Sub mnuViewZoom200_click(sender As Object, e As EventArgs) _
    Handles mnuViewZoom200.Click
    ViewZoom(5)
  End Sub

  Public Sub ViewZoom(ByVal index As Integer)

    Dim x As Double

    beforeZoom()

    Select Case index
      Case 0 ' zoom in
        x = mView.ZoomFactor * zoomStep
        If x <= Maxzoom And x >= 1 / Maxzoom Then mView.ZoomFactor = x
      Case 1 ' zoom out
        x = mView.ZoomFactor * (1 / zoomStep)
        If x <= Maxzoom And x >= 1 / Maxzoom Then mView.ZoomFactor = x
      Case 2
        mView.ZoomFactor = 0.25  ' 25%
      Case 3
        mView.ZoomFactor = 0.5 ' 50%
      Case 4
        mView.ZoomFactor = 1 ' 100%
      Case 5
        mView.ZoomFactor = 2 ' 200%
      Case 6
        mView.ZoomFactor = 4 ' 400%
      Case 7 ' fit to window
        mView.ZoomFactor = 0 ' fit to window
    End Select

    afterZoom()
    StatusLine()

  End Sub

  Private Sub mView_MouseDown(sender As Object, ByVal e As MouseEventArgs) Handles mView.MouseDown

    Dim i As Integer
    Dim p As PointF
    Dim r As Rectangle
    Dim inside As Boolean

    If mView.Bitmap Is Nothing Then Exit Sub

    If e.Button = MouseButtons.Middle Then ' middle button, finish command
      CompleteCommand()
      Exit Sub
    End If

    lastP = e.Location  ' last point
    firstP = lastP
    lastD = New Point(0, 0)  ' difference from previous point

    Select Case Command

      Case cmd.SelectSimilar, cmd.DrawFill
        clickPoint = e.Location
        SetTolerance()
        ' don't set Command to None -- can recur

      Case cmd.ColorSample
        ColorSample(e.Location, e.Button)
        CompleteCommand()
        Exit Sub

      Case cmd.Crop  ' rBoxMode should be either 0, 1, or 3 here.
        If rBoxMode = rbMode.readyBoxDrag Then ' 3 = ready for crop or adjust
          rBoxDrag(e.Location)
        ElseIf rBoxMode = rbMode.readyMouseDown Then ' was ready for mouse down
          rBoxX = e.X : rBoxY = e.Y
          rBoxHeight = 0 : rBoxWidth = 0
          rBoxMode = rbMode.readyMouseUp
        End If

      Case cmd.DrawText
        r = getTextRectangle(textPoint)
        If getBoxEdge(e.Location, r) = 0 Then ' clicked outside text - complete command
          mView_ctlTextOK()
          Exit Sub
        End If

        Dragging = True
        dragPoint = e.Location

      Case cmd.DrawLine, cmd.DrawCurve, cmd.DrawArrow, cmd.DrawCircle, cmd.DrawEllipse, _
           cmd.DrawBox, cmd.DrawMeasure, cmd.DrawSketch
        If DrawingLine = 1 Then ' start drawing
          SaveUndo()
          zP = New List(Of Point)
          zP.Add(lastP)

          DrawingLine = 2 ' first point set on line
          mView.RubberClear()
          mView.RubberEnabled = True
          mView.RubberLineWidth = DrawPenWidth * mView.ZoomFactor
          mView.RubberColor = mainColor
          mView.RubberDashed = False
          mView.RubberPoints = New List(Of Point)
          mView.RubberPoints.AddRange(zP)

          Select Case Command
            Case cmd.DrawLine
              mView.RubberShape = shape.Line
            Case cmd.DrawCurve
              mView.RubberPoints = getCurve(mView.RubberPoints, 8)
              mView.RubberShape = shape.Line
            Case cmd.DrawArrow
              mView.RubberPoints = getArrow(mView.RubberPoints, DrawPenWidth)
              mView.RubberShape = shape.Line
            Case cmd.DrawCircle
              mView.RubberBox = getCircle(firstP, lastP)
              mView.RubberShape = shape.Ellipse
            Case cmd.DrawEllipse
              mView.RubberShape = shape.Ellipse
            Case cmd.DrawBox
              mView.RubberShape = shape.Box
            Case cmd.DrawMeasure
              mView.RubberShape = shape.Measure
            Case cmd.DrawSketch
              Sketching = 2
              mView.RubberShape = shape.Line
          End Select

        ElseIf DrawingLine = 2 Then ' additional points
          zP.Add(lastP)
          If (Command = cmd.DrawCircle Or Command = cmd.DrawEllipse Or Command = cmd.DrawBox Or _
              Command = cmd.DrawMeasure) AndAlso zP.Count > 0 Then
            CompleteCommand() ' done with the 2-point items
          Else
            mView.Invalidate()
          End If
        End If

      Case cmd.PasteV, cmd.ImageStretch, cmd.ImageAlign
        If ShiftDown And controlDown Then
          npts = 4 ' got all 4 points, now drag mode
          DrawingStretch = 3
        Else
          If npts < 4 Then
            npts = npts + 1
            rX(npts - 1) = e.X
            rY(npts - 1) = e.Y
            If npts >= 4 Then DrawingStretch = 3 ' got all 4 points, now drag mode
          ElseIf npts >= 4 Then
            LineP = e.Location
            i = GetStretchEdge(LineP.X, LineP.Y)
            If i >= 1 And i <= 4 Then
              DrawingStretch = i + 10 ' point move
            ElseIf i = 16 Then
              DrawingStretch = 4 ' move
            End If
          End If
        End If

      Case cmd.Floater
        If mView.FloaterBitmap IsNot Nothing Then
          p = mView.ControlToBitmap(e.Location)
          p = New Point(p.X - mView.FloaterPosition.X, p.Y - mView.FloaterPosition.Y)
          inside = mView.FloaterPath.IsVisible(p)
          If (Not inside And e.Button = MouseButtons.Left) Or (inside And e.Button = MouseButtons.Right) Then
            ' left button outside or right inside assigns floater to bitmap
            If e.Button = MouseButtons.Right Then mnxCancel = True ' cancel context menu
            SaveUndo()
            mView.assignFloater() ' done dragging
            ClearProcesses(True)
          End If
        End If

      Case cmd.None, cmd.Pan  ' pan starts at mousedown
        ' start pan mode
        'If mView.Bitmap.Width * mView.ZoomFactor > mView.ClientSize.Width _
        ' Or mView.Bitmap.Height * mView.ZoomFactor > mView.ClientSize.Height Then
        mView.Cursor = Cursors.Hand
        Command = cmd.Pan
        mView.InterpolationMode = InterpolationMode.Low
        'End If

    End Select

  End Sub

  Private Sub mView_MouseMove(sender As Object, ByVal e As MouseEventArgs) Handles mView.MouseMove

    Dim i As Integer
    Dim rColor As Color
    Dim x1, y1 As Double
    Dim p As Point
    Dim pf As Point
    Dim rCurrent As Rectangle ' rectangle from first point to here
    Dim inside As Boolean
    Dim mouseDown As Boolean
    Dim r As Rectangle

    mouseDown = (e.Button And MouseButtons.Left)
    If mView.Bitmap Is Nothing Then Exit Sub
    lastD = e.Location - lastP ' movement since last
    lastP = e.Location ' save previous point
    If lastD = Point.Empty Then Exit Sub
    pf = mView.ControlToBitmap(e.Location)
    rCurrent = New Rectangle(Min(firstP.X, e.X), Min(firstP.Y, e.Y), Abs(firstP.X - e.X), Abs(firstP.Y - e.Y))

    ' update status line with pixel position
    lbPosition.Text = "position: (" & Format(pf.X, "#0") & ", " & Format(pf.Y, "#0") & ")"

    Select Case Command

      Case cmd.ColorSample
        If mouseDown Then
          If pf.X >= 0 And pf.X < mView.Bitmap.Width And pf.Y >= 0 And pf.Y < mView.Bitmap.Height Then
            rColor = mView.Bitmap.GetPixel(pf.X, pf.Y)
            lbPosition.Text = lbPosition.Text & "  RGB: " & rColor.R & ", " & rColor.G & ", " & rColor.B
          End If
        End If

      Case cmd.Floater
        If mView.FloaterBitmap IsNot Nothing And mView.FloaterPath IsNot Nothing Then
          p = New Point(pf.X - mView.FloaterPosition.X, pf.Y - mView.FloaterPosition.Y)
          inside = mView.FloaterPath.IsVisible(p)
          If e.Button = MouseButtons.None Then ' change the cursor if it's on the floater
            If inside Then
              mView.Cursor = Cursors.Hand
            Else
              mView.Cursor = Cursors.Default
            End If
          ElseIf inside And e.Button = MouseButtons.Left Then ' move the bitmap
            mView.FloaterPosition += New Point(lastD.X / mView.ZoomFactor, lastD.Y / mView.ZoomFactor)
            mView.Invalidate()
          End If
        End If

      Case cmd.Pan
        If mouseDown Then
          mView.Pan(lastD.X, lastD.Y)
          If timerRedraw IsNot Nothing Then timerRedraw.Interval = 350 : timerRedraw.Start() ' redraw high quality in 350 milliseconds
        End If

      Case cmd.DrawBox, cmd.DrawEllipse
        If zP.Count > 0 Then
          mView.RubberBox = rCurrent
          mView.Invalidate()
        End If

      Case cmd.DrawCircle
        If zP.Count > 0 Then
          mView.RubberPoints = New List(Of Point)
          mView.RubberPoints.AddRange(zP)
          mView.RubberPoints.Add(e.Location) ' current mouse position is a temporary point
          mView.RubberBox = getCircle(firstP, e.Location)
          mView.Invalidate()
        End If

      Case cmd.DrawLine, cmd.DrawCurve, cmd.DrawArrow, cmd.DrawMeasure
        If zP.Count > 0 Then
          mView.RubberPoints = New List(Of Point)
          mView.RubberPoints.AddRange(zP)
          mView.RubberPoints.Add(e.Location) ' current mouse position is a temporary point
          If Command = cmd.DrawCurve Then mView.RubberPoints = getCurve(mView.RubberPoints, 8)
          If Command = cmd.DrawArrow Then mView.RubberPoints = getArrow(mView.RubberPoints, DrawPenWidth)
          mView.Invalidate()
        End If

      Case cmd.SelectEllipse, cmd.SelectRectangle, cmd.ZoomWindow
        If mouseDown Then
          mView.RubberBox = rCurrent
          mView.Invalidate()
        End If

      Case cmd.SelectSketch, cmd.DrawSketch
        If mouseDown Then
          zP.Add(lastP)
          mView.RubberPoints = New List(Of Point)
          mView.RubberPoints.AddRange(zP)
          mView.Invalidate()
        End If

      Case cmd.DrawText
        If e.Button <> MouseButtons.Left Then ' set cursor if necessary
          r = getTextRectangle(textPoint)
          If getBoxEdge(e.Location, r) <> 0 Then
            mView.Cursor = Cursors.SizeAll
          Else
            mView.Cursor = Cursors.Default
          End If
        Else ' button is down -- drag the text
          p = textPoint + (e.Location - dragPoint)
          mView.RubberPoints = New List(Of Point)
          mView.RubberPoints.Add(p)
          mView.Invalidate()
        End If

      Case cmd.Crop
        If rBoxMode <> rbMode.readyMouseDown And e.Button = MouseButtons.Left And rBoxMode <> rbMode.none Then
          dragBox(e.Location, False)

        ElseIf rBoxMode = rbMode.readyBoxDrag And e.Button = MouseButtons.None Then
          ' check edge
          Select Case getBoxEdge(e.Location, New Rectangle(rboxleft, rBoxTop, rBoxWidth, rBoxHeight))
            Case 1, 2, 3 ' left-right
              mView.Cursor = Cursors.SizeWE
            Case 4, 8, 12 ' top-bottom
              mView.Cursor = Cursors.SizeNS
            Case 5, 7, 10, 13, 15 ' top left / lower right
              mView.Cursor = Cursors.SizeNWSE
            Case 6, 9, 11, 14 ' top right / lower left
              mView.Cursor = Cursors.SizeNESW
            Case 16 ' move
              mView.Cursor = Cursors.SizeAll
            Case Else
              mView.Cursor = Cursors.Hand
          End Select
        End If

      Case cmd.PasteV, cmd.ImageStretch, cmd.ImageAlign
        If DrawingStretch = 1 Then ' should never happen
          x1 = e.X : y1 = e.Y
          If ShiftDown And controlDown Then  ' ctrl-shift - scale, constant aspect
            If npts >= 1 Then
              rX(1) = x1
              rY(1) = rY(0)
              rX(2) = rX(0)
              rY(2) = rY(0) - stretchAspect * (rX(1) - rX(0))
              y1 = rY(2)
              npts = 3
            End If
          End If

          'drawStretchFrame(x1, y1)
          getStretchPoints(x1, y1, rX, rY, npts) ' gets 4 points into rx, ry: bl, br, tl, tr
          mView.RubberPoints.Clear()
          mView.RubberPoints.Add(New Point(rX(2), rY(2)))
          mView.RubberPoints.Add(New Point(rX(3), rY(3)))
          mView.RubberPoints.Add(New Point(rX(1), rY(1)))
          mView.RubberPoints.Add(New Point(rX(0), rY(0)))

          ' draw rubber quadralateral
          'If mview.rubberpoints.Count = 0 Then
          ' mview.rubberpoints.Add(lastP)
          'Else
          '  mview.rubberpoints(mview.rubberpoints.Count - 1) = lastP
          'End If
          mView.Invalidate() ' draw mview.rubber box

        ElseIf DrawingStretch > 1 Then
          If e.Button = MouseButtons.None Then
            ' check edge for cursor change
            Select Case GetStretchEdge(e.X, e.Y)
              Case 1, 2, 3, 4 ' on a corner point
                mView.Cursor = Cursors.Cross
              Case 16 ' move
                mView.Cursor = Cursors.SizeAll
              Case Else ' 0 = outside area
                mView.Cursor = Cursors.Hand
            End Select

          ElseIf e.Button = MouseButtons.Left Then
            ' move point or box
            If DrawingStretch > 10 Then ' move point
              rX(DrawingStretch - 10 - 1) = e.X
              rY(DrawingStretch - 10 - 1) = e.Y
            Else ' move whole box
              For i = 0 To npts - 1
                rX(i) = rX(i) + e.X - LineP.X
                rY(i) = rY(i) + e.Y - LineP.Y
              Next i
              LineP = e.Location
            End If

            mView.RubberPoints.Add(lastP)
            mView.Invalidate() ' draw mview.rubber box

          End If
        End If

    End Select

  End Sub

  Private Sub mView_MouseUp(sender As Object, ByVal e As MouseEventArgs) Handles mView.MouseUp

    Dim rCurrent As Rectangle
    Dim msg As String

    rCurrent = New Rectangle(Min(firstP.X, e.X), Min(firstP.Y, e.Y), Abs(firstP.X - e.X), Abs(firstP.Y - e.Y))

    Select Case Command
      Case cmd.Crop
        initRBoxDrag(e.Location)

      Case cmd.DrawText
        Dragging = False
        textPoint = textPoint + (e.Location - dragPoint)
        mView.RubberPoints = New List(Of Point)
        mView.RubberPoints.Add(textPoint)
        mView.Invalidate()

      Case cmd.SelectRectangle
        msg = mView.SetSelection(rCurrent)
        If msg = "" Then
          mView.InitFloater()
          startFloater() ' change from selectRectangle mode to Floater mode.
        End If
      Case cmd.SelectEllipse
        Using gpath As GraphicsPath = New GraphicsPath
          gpath.AddEllipse(rCurrent)
          mView.SetSelection(gpath)
        End Using
        mView.InitFloater()
        startFloater() ' change from selectRectangle mode to Floater mode.

      Case cmd.SelectSketch
        If zP.Count > 0 Then
          Using gpath As GraphicsPath = New GraphicsPath
            gpath.AddPolygon(zP.ToArray)
            mView.SetSelection(gpath)
          End Using
          mView.InitFloater()
          startFloater() ' change from selectRectangle mode to Floater mode.
        End If

      Case cmd.DrawSketch
        CompleteCommand()

      Case cmd.Pan
        mView.Cursor = Cursors.Default
        Command = cmd.None

      Case cmd.ZoomWindow
        mView.ZoomWindow(rCurrent)
        Command = cmd.None

    End Select

  End Sub

  Sub startFloater()
    ' starts selected floater mode
    ' SetSelection and InitFloater before calling here
    mView.RubberClear()
    mView.RubberPath = mView.FloaterPath.Clone
    mView.RubberShape = shape.Path
    If mView.Cursor = Cursors.Hand Then mView.Cursor = Cursors.Default
    Command = cmd.Floater
    mView.FloaterVisible = True
    mView.RubberDashed = True
    mView.FloaterOutline = True
    If Not mView.ctlFeather.Visible Then setPanelFeather()
    rBoxMode = rbMode.none
  End Sub

  Private Sub rBoxDone()

    Dim r As Rectangle

    mView.RubberEnabled = False
    mView.Cursor = Cursors.WaitCursor
    rBoxMode = rbMode.none
    mView.RubberClear()

    If rBoxWidth <= 1 Or rBoxHeight <= 1 Then
      ClearProcesses(True)
      Exit Sub
    End If

    Select Case Command
      ' this use to have drawshape -- anything to drag and shape a rectangle around
      Case cmd.SelectRectangle
        mView.SetSelection(New Rectangle(rboxleft, rBoxTop, rBoxWidth, rBoxHeight))
        mView.assignFloater()

      Case cmd.SelectEllipse
        Using gPath As GraphicsPath = New GraphicsPath
          gPath.AddEllipse(New Rectangle(rboxleft, rBoxTop, rBoxWidth, rBoxHeight))
          mView.SetSelection(gPath)
          mView.assignFloater()
        End Using

      Case cmd.Crop
        SaveUndo()
        r = mView.ControlToBitmap(New Rectangle(rboxleft, rBoxTop, rBoxWidth, rBoxHeight))
        mView.Crop(r)
        redraw(mView, 1)

    End Select

    If LastButton <> "" Then
      checkToolButton(ToolStrip1, LastButton, False)
      ToolStrip1.Refresh()
    End If

    mView.Cursor = Cursors.Default

  End Sub

  Sub SaveUndo()

    ' save the screen to the next undo file

    Dim i As Integer
    Dim fName As String
    Dim k As Integer
    Dim tmpCursor As Cursor


    If iniDisableUndo Then Exit Sub

    tmpCursor = mView.Cursor
    mView.Cursor = Cursors.WaitCursor

    mView.nUndo = mView.nUndo + 1
    mView.picModified = True

    If Not iniMultiUndo Then ' only copy to mview.bitmapundo
      mView.nUndo = 1
      mView.nRedo = 0
      clearBitmap(mView.bitmapUndo)
      If mView.Bitmap.Width * mView.Bitmap.Height <= bigMegapix Then ' normal undo
        mView.bitmapUndo = mView.Bitmap.Clone
      Else ' save to disk
        fName = UndoPath & "~" & AppName & mView.Tag & "~" & mView.nUndo & ".~tmp"
        saveUndoFile(fName, mView.Bitmap)
      End If
      mView.Cursor = tmpCursor
      Exit Sub
    End If

    ' ----------- multilevel undo starts here ------------------------------

    k = 0
    fName = UndoPath & "~" & AppName & mView.Tag & "~" & mView.nUndo & ".~tmp"
    If mView.nUndo <> mView.firstUndo Then ' don't save to disk the first time
      saveUndoFile(fName, mView.bitmapUndo)
    End If
    clearBitmap(mView.bitmapUndo)
    mView.bitmapUndo = mView.Bitmap.Clone

    If mView.nUndo = mView.firstUndo Then ' first time
      mView.nRedo = mView.nUndo
      mView.Cursor = tmpCursor
      Exit Sub
    End If


    If k <> 0 Then
      mView.nUndo = mView.nUndo - 1 ' unsuccessful
    Else
      ' erase redo files
      For i = mView.nUndo + 1 To mView.nRedo
        fName = UndoPath & "~" & AppName & mView.Tag & "~" & i & ".~tmp"
        Try
          File.Delete(fName)
        Catch ex As Exception
          MsgBox(ex.Message)
        End Try
      Next i
      mView.nRedo = mView.nUndo
      ' only allow 10 undos
      If mView.nUndo - mView.firstUndo + 2 > 11 Then
        fName = UndoPath & "~" & AppName & mView.Tag & "~" & mView.firstUndo + 1 & ".~tmp"
        Try
          File.Delete(fName)
        Catch ex As Exception
          MsgBox(ex.Message)
        End Try
        mView.firstUndo = mView.firstUndo + 1
      End If
    End If

    setUndoTools()
    mView.Cursor = tmpCursor

  End Sub

  Sub ClearUndo()

    ' erase all the undo files for this form

    Dim fNames(-1) As String
    Dim fName As String
    Dim i As Integer

    Try
      fNames = Directory.GetFiles(UndoPath, "~" & AppName & mView.Tag & "~*.~tmp")
      i = 0
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    If i = 0 Then
      For Each fName In fNames
        Try
          File.Delete(fName)
        Catch ex As Exception
          MsgBox(ex.Message)
        End Try
      Next fName
    End If

    mView.nUndo = 0
    mView.nRedo = 0
    mView.firstUndo = 1
    setUndoTools()

  End Sub
  Sub LoadUndo()

    ' load an undo file onto the screen, replace it with a redo file
    Dim fName As String
    Dim k As Integer
    Dim center As Integer
    Dim tmpCursor As Cursor

    If mView Is Nothing Then Exit Sub
    If mView.nUndo < mView.firstUndo And iniMultiUndo Then Exit Sub

    tmpCursor = Me.Cursor
    Me.Cursor = Cursors.WaitCursor

    If Not iniMultiUndo Then ' only copy bitmapundo
      If mView.nUndo >= 1 Then
        If mView.bitmapUndo IsNot Nothing Then
          mView.setBitmap(mView.bitmapUndo)
          clearBitmap(mView.bitmapUndo)
        Else
          fName = UndoPath & "~" & AppName & mView.Tag & "~" & mView.nUndo & ".~tmp"
          If File.Exists(fName) Then
            mView.bitmapUndo = readUndoFile(fName)
          End If
          If mView.bitmapUndo IsNot Nothing Then
            mView.setBitmap(mView.bitmapUndo)
            clearBitmap(mView.bitmapUndo)
          End If
        End If
        mView.nUndo = 0
        mView.nRedo = 0
        StatusLine()
      End If
      Me.Cursor = tmpCursor
      Exit Sub
    End If

    ' ------------- multilevel undo starts here --------------------
    If mView.Bitmap.Width = mView.bitmapUndo.Width And mView.Bitmap.Height = mView.bitmapUndo.Height Then
      center = 0
    Else
      center = 1 ' center new-sized image
    End If

    clearBitmap(imageTmp)
    imageTmp = mView.Bitmap.Clone
    mView.setBitmap(mView.bitmapUndo) ' this is the actual undo
    clearBitmap(mView.bitmapUndo)

    ' load the previous undo from disk into mview.bitmapundo
    fName = UndoPath & "~" & AppName & mView.Tag & "~" & mView.nUndo & ".~tmp"
    If mView.nUndo > mView.firstUndo Then mView.bitmapUndo = readUndoFile(fName)
    saveUndoFile(fName, imageTmp) ' save current bitmap for redo

    mView.nUndo = mView.nUndo - 1
    If k <> 0 Then mView.nRedo = mView.nUndo

    redraw(mView, center)
    Me.Cursor = tmpCursor
    setUndoTools()

  End Sub
  Sub LoadRedo()
    ' load a redo file onto the screen, replace it with an undo file

    Dim fName As String
    Dim k As Integer
    Dim center As Integer

    If Not iniMultiUndo OrElse mView.nRedo <= mView.nUndo Then Exit Sub

    mView.Cursor = Cursors.WaitCursor

    k = 0
    fName = UndoPath & "~" & AppName & mView.Tag & "~" & mView.nUndo + 1 & ".~tmp"
    clearBitmap(imageTmp)
    If mView.bitmapUndo IsNot Nothing Then imageTmp = mView.bitmapUndo.Clone
    clearBitmap(mView.bitmapUndo)
    mView.bitmapUndo = mView.Bitmap.Clone
    Using bmp As Bitmap = readUndoFile(fName)
      mView.setBitmap(bmp)
    End Using
    If imageTmp IsNot Nothing AndAlso mView.nUndo >= mView.firstUndo Then saveUndoFile(fName, imageTmp)
    clearBitmap(imageTmp)

    If mView.Bitmap IsNot Nothing And k = 0 Then
      If mView.Bitmap.Width = mView.bitmapUndo.Width And mView.Bitmap.Height = mView.bitmapUndo.Height Then
        center = 0
      Else
        center = 1
      End If
    End If

    If k = 0 Then mView.nUndo = mView.nUndo + 1

    redraw(mView, center)
    setUndoTools()
    mView.Cursor = Cursors.Default

  End Sub

  Sub saveUndoFile(fname As String, bmpq As Bitmap)

    Try
      Set32bppPArgb(bmpq)
      bmpq.Save(fname, Imaging.ImageFormat.Tiff) ' undoImageCodecInfo, undoEncoderParameters)
    Catch ex As Exception
      MsgBox(ex.Message & crlf & "Error saving Undo file. Try disabling the multi-level undo under Tools, Options, Editing.")
    End Try

  End Sub

  Function readUndoFile(fname As String) As Bitmap

    Try ' gdi leaves the file open, even after "end using"
      Using iStream As New FileStream(fname, FileMode.Open, FileAccess.Read)
        Return Bitmap.FromStream(iStream)
      End Using
    Catch ex As Exception
      MsgBox(ex.Message & crlf & "Error reading Undo file. Try disabling the multi-level undo under Tools, Options, Editing.")
    End Try

    Return Nothing
  End Function

  Sub KeepNewBitmap(ByRef qImage As Bitmap)
    ' saves newimage to mview.bitmap and handles undo, etc.
    ' disposes qimage

    If qImage Is Nothing Then
      If mView.FloaterBitmap IsNot Nothing Or mView.BitmapPath IsNot Nothing Then
        Command = cmd.Floater ' keep the floater floating if there's nothing to save
        mView.RubberDashed = True
      Else
        Command = cmd.None
      End If
      Exit Sub
    End If

    If mView.FloaterBitmap IsNot Nothing Or mView.BitmapPath IsNot Nothing Then
      mView.setFloaterBitmap(qImage)
      clearBitmap(qImage)
      Command = cmd.Floater
      mView.RubberDashed = True
      mView.Invalidate()
    Else
      SaveUndo()
      mView.setBitmap(qImage)
      clearBitmap(qImage)
      Command = cmd.None
      mView.Zoom()
    End If

  End Sub

  Sub initRBoxDrag(pCursor As Point)
    ' mouse is up in Crop command -- get edges and corners ready to drag (stretch)
    If rBoxMode = rbMode.readyMouseDown Or rBoxMode = rbMode.none Then Exit Sub ' should never happen

    If (pCursor.X - rBoxX) ^ 2 + (pCursor.Y - rBoxY) ^ 2 < 10 Then ' click, not drag
      rBoxDone()
      Exit Sub
    End If

    dragBox(pCursor, True)
    rBoxMode = rbMode.readyBoxDrag ' rBoxMode section done
    rboxleft = rbX
    rBoxTop = rbY
    rBoxWidth = rbWidth
    rBoxHeight = rbHeight

  End Sub

  Function getBoxEdge(pCursor As Point, r As Rectangle) As Short

    ' 0 = outside
    ' 1 = left
    ' 2 = right
    ' 4 = top
    ' 8 = bottom
    ' 16 = inside

    getBoxEdge = 0
    If Abs(pCursor.X - r.X) < 5 Then getBoxEdge = getBoxEdge + 1 ' mouse on left edge
    If Abs(pCursor.X - (r.X + r.Width - 1)) < 5 Then getBoxEdge = getBoxEdge + 2 ' right
    If Abs(pCursor.Y - r.Y) < 5 Then getBoxEdge = getBoxEdge + 4 ' top
    If Abs(pCursor.Y - (r.Y + r.Height - 1)) < 5 Then getBoxEdge = getBoxEdge + 8 ' bottom edge

    If getBoxEdge = 0 And pCursor.X >= r.X And pCursor.X < r.X + r.Width And _
      pCursor.Y >= r.Y And pCursor.Y < r.Y + r.Height Then
      ' move mode
      getBoxEdge = 16
    End If

  End Function

  Sub dragBox(pCursor As Point, ByVal mouseUp As Boolean)

    ' drag a box already on the screen for crop and other(?) commands

    Dim z As Double
    Dim q As Double
    Dim rControl As Rectangle
    Dim rBitmap As Rectangle
    Dim x, y As Integer

    x = pCursor.X : y = pCursor.Y

    ' clip to control
    If x < mView.ClientRectangle.Left Then x = mView.ClientRectangle.Left
    If y < mView.ClientRectangle.Top Then y = mView.ClientRectangle.Top
    If x > mView.ClientRectangle.Left + mView.ClientSize.Width - 1 Then x = mView.ClientRectangle.Left + mView.ClientSize.Width - 1
    If y > mView.ClientRectangle.Top + mView.ClientSize.Height - 1 Then y = mView.ClientRectangle.Top + mView.ClientSize.Height - 1

    Select Case rBoxMode

      Case rbMode.readyMouseUp ' corner drag, and intial drag
        If x > rBoxX Then
          rbX = rBoxX ' on the right
          rbWidth = x - rBoxX + 1
        Else
          rbX = x ' on the left
          rbWidth = rBoxX - x + 1
        End If

        If y > rBoxY Then
          rbY = rBoxY ' on bottom
          rbHeight = y - rBoxY + 1
        Else
          rbY = y ' on top
          rbHeight = rBoxY - y + 1
        End If

        ' maintain aspect ratio if control is pressed
        If controlDown And rbWidth >= 1 And rbHeight >= 1 Then
          z = (rbHeight / rbWidth) / ImageAspect
          If z > 1 Then
            ' too tall -- make it wider
            q = rbWidth * z
            If x < rBoxX Then rbX = rbX - (q - rbWidth)
            rbWidth = q
          Else
            q = rbHeight / z
            If y < rBoxY Then rbY = rbY - (q - rbHeight)
            rbHeight = q
          End If
        End If

      Case rbMode.dragLeftRight ' left-right edge drag
        If x > rBoxX Then
          rbX = rBoxX
          rbWidth = x - rBoxX + 1
        Else
          rbX = x
          rbWidth = rBoxX - x + 1
        End If
        rbY = rBoxY
        rbHeight = rBoxHeight

      Case rbMode.dragTopBottom ' top-bottom edge drag
        If y > rBoxY Then
          rbY = rBoxY
          rbHeight = y - rBoxY + 1
        Else
          rbY = y
          rbHeight = rBoxY - y + 1
        End If
        rbX = rBoxX
        rbWidth = rBoxWidth

      Case rbMode.moveCropBox ' move box
        rbX = rBoxX + x
        rbY = rBoxY + y

    End Select

    rControl = New Rectangle(rbX, rbY, rbWidth, rbHeight)
    rBitmap = mView.ControlToBitmap(rControl)

    If rbWidth > 10 And rbHeight > 10 Then
      ' drawshape used to go here.
      mView.RubberBox = rControl
      mView.Invalidate() ' draw mview.rubber box
    End If

    lbStatus.Text = "Size:  " & Round(rBitmap.Width) & " x " & Round(rBitmap.Height)

  End Sub

  Public Sub StatusLine()

    If mView Is Nothing Then Exit Sub

    If (mView.Bitmap IsNot Nothing) Then
      lbStatus.Text = "File name: " & mView.Caption & _
        "   Size: " & Format(mView.Bitmap.Width, "#,#") & " x " & Format(mView.Bitmap.Height, "#,#") & _
        "   Format: " & Replace(mView.Bitmap.PixelFormat.ToString, "Format", "") & _
        "   Zoom: " & Format(mView.ZoomFactor, "#0.##") & "x"
    End If

  End Sub

  Sub redraw(ByRef mView As mudViewer, ByRef iReset As Integer, Optional ByRef hiQual As Boolean = True)

    ' reset: 0 = no reset, 1 = center, 2 = zoom 1:1 + center, 3 = zoom to fit window +  center
    ' uses pCenter and Zoom for pan and zoom values

    Dim i As Integer
    Dim zoomFac As Double
    Dim pCenter As Point

    Me.Cursor = Cursors.WaitCursor
    Try
      If mView.Bitmap Is Nothing OrElse mView.Bitmap.Width = 0 Then Exit Sub
    Catch ex As Exception
      MsgBox(ex.Message)
      Exit Sub
    End Try

    pCenter = New Point(mView.Bitmap.Width \ 2, mView.Bitmap.Height \ 2)

    Select Case iReset
      Case 0 ' no reset
        mView.Zoom()
      Case 1 ' center
        mView.setCenterPoint(pCenter, True)
      Case 2 ' 1:1 + center
        mView.Zoom(zoomFac, pCenter)
      Case 3 ' zoom fit (unless iniZoomOne, then center)
        If iniZoomOne Then
          mView.Zoom(zoomFac, pCenter)
        Else
          mView.Zoom(0)
        End If
    End Select

    If mView.FloaterBitmap IsNot Nothing Then
      mView.FloaterVisible = True
    End If

    ImageAspect = mView.Bitmap.Height / mView.Bitmap.Width

    If DrawingStretch > 0 Then
      repainted = True
      If npts < 4 Then i = npts Else i = 3
    End If

    StatusLine()
    Me.Cursor = Cursors.Default

    If Not hiQual Then timerRedraw.Interval = 100 : timerRedraw.Start() ' hi-qual refresh

  End Sub

  Sub rBoxDrag(pCursor As Point)

    ' rbox is already there. check for side or corner to drag.
    edge = getBoxEdge(pCursor, New Rectangle(rboxleft, rBoxTop, rBoxWidth, rBoxHeight))

    Select Case edge
      Case 0 ' none - finish up
        rBoxDone()
        Exit Sub
      Case 1, 3 ' left
        rBoxMode = rbMode.dragLeftRight
        rBoxX = rboxleft + rBoxWidth - 1
        rBoxY = rBoxTop
      Case 2 ' right
        rBoxMode = rbMode.dragLeftRight
        rBoxX = rboxleft
        rBoxY = rBoxTop
      Case 4, 12 ' top
        rBoxMode = rbMode.dragTopBottom
        rBoxY = rBoxTop + rBoxHeight - 1
        rBoxX = rboxleft
      Case 8 ' bottom
        rBoxMode = rbMode.dragTopBottom
        rBoxY = rBoxTop
        rBoxX = rboxleft
      Case 5, 7, 13, 15 ' top left
        rBoxX = rboxleft + rBoxWidth - 1
        rBoxY = rBoxTop + rBoxHeight - 1
        rBoxMode = rbMode.readyMouseUp
      Case 6, 14 ' top right
        rBoxX = rboxleft
        rBoxY = rBoxTop + rBoxHeight - 1
        rBoxMode = rbMode.readyMouseUp
      Case 9, 11 ' bottom left
        rBoxX = rboxleft + rBoxWidth - 1
        rBoxY = rBoxTop
        rBoxMode = rbMode.readyMouseUp
      Case 10 ' top left
        rBoxX = rboxleft
        rBoxY = rBoxTop
        rBoxMode = rbMode.readyMouseUp
      Case 16 ' inside
        rBoxX = rboxleft - pCursor.X ' offset to corner
        rBoxY = rBoxTop - pCursor.Y
        rBoxMode = rbMode.moveCropBox ' move box
    End Select

    mView.RubberBox = New Rectangle(rBoxX, rBoxY, rBoxWidth, rBoxHeight)
    mView.Invalidate()

  End Sub

  Sub ClearProcesses(ByRef clearSelection As Boolean)

    Dim b As ToolStripButton
    Dim item As ToolStripItem
    ' clear out in-process stuff in case of Esc

    If mView Is Nothing OrElse mView.Bitmap Is Nothing Then Exit Sub
    If mView.RubberEnabled Then mView.RubberClear()

    If rBoxMode <> rbMode.none Then
      rBoxMode = rbMode.none
      If Command = cmd.Crop Then
        mView.setBitmap(imageTmp)
        clearBitmap(imageTmp)
      ElseIf Command = cmd.DrawText Then
        mView.ClearFloater()
      End If
      Command = cmd.None
    End If

    If clearSelection Then
      mView.ClearSelection()
      mView.ClearFloater()
      mView.ctlFeather.Visible = False
    End If

    Dragging = False
    DrawingLine = 0
    drawingMode = 0
    DrawingStretch = 0
    npts = 0

    Me.Cursor = Cursors.Default
    mView.Cursor = Cursors.Default

    For Each item In ToolStrip1.Items
      If TypeOf (item) Is ToolStripButton Then
        b = item
        If b.Checked Then b.Checked = False
      End If
    Next item
    ToolStrip1.Refresh()

    StatusLine()
    Command = cmd.None
    If mView.ctlTolerance.Visible Then closePanelTolerance()
    If mView.ctlBright.Visible Then closePanelBright() ' for brightness, contrast, saturation
    If mView.ctlText.Visible Then closePanelText()
    Me.CancelButton = Nothing
    Me.AcceptButton = Nothing
    EnableMenus(True)

    timerAnimate.Stop()  ' stop any animation
    cmdAnimate.Text = "Animate"

    If mView.FloaterBitmap Is Nothing Then
      Command = cmd.None
    Else
      Command = cmd.Floater
      mView.RubberDashed = True
      If Not mView.ctlFeather.Visible Then setPanelFeather()
    End If
    ShiftDown = False
    controlDown = False

  End Sub

  Sub CompleteCommand()

    mView.RubberClear()
    If rBoxMode = rbMode.readyBoxDrag Then rBoxDone()
    If DrawingLine = 2 Or Sketching = 2 Then DrawDone()
    If DrawingStretch >= 1 Then stretchPaste()

    If Command = cmd.Floater Then mView.assignFloater()

    DrawingLine = 0
    Sketching = 0
    Me.Cursor = Cursors.Default
    mView.Cursor = Cursors.Default

    StatusLine()
    If LastButton <> "" Then
      checkToolButton(ToolStrip1, LastButton, False)
      ToolStrip1.Refresh()
    End If

    drawingMode = 0 ' disable cursor keys
    Command = cmd.None
    If mView.ctlTolerance.Visible Then closePanelTolerance()
    If mView.ctlBright.Visible Then closePanelBright()
    mView.ctlFeather.Visible = False
    If mView.ctlText.Visible Then closePanelText()
    Me.CancelButton = Nothing
    Me.AcceptButton = Nothing

    ShiftDown = False
    controlDown = False

  End Sub

  Sub fillFloater(ByRef mainColor As Color)

    If mView.FloaterBitmap Is Nothing Then Exit Sub
    Me.Cursor = Cursors.WaitCursor

    mView.Fill(mainColor, mView.FloaterBitmap)
    Command = cmd.Floater
    mView.RubberDashed = True
    setPanelFeather()
    Me.Cursor = Cursors.Default

  End Sub
  Sub ColorSample(pc As Point, button As MouseButtons)

    Dim pf As PointF

    pf = mView.ControlToBitmap(pc)
    If pf.X >= 0 And pf.X < mView.Bitmap.Width And pf.Y >= 0 And pf.Y < mView.Bitmap.Height Then

      If button = MouseButtons.Right Then ' background color
        mBackColor = mView.Bitmap.GetPixel(pf.X, pf.Y)
        setToolColor("mnudrawbackcolor", mBackColor)
        mnxCancel = True
      Else ' foreground color
        mainColor = mView.Bitmap.GetPixel(pf.X, pf.Y)
        setToolColor("mnudrawforecolor", mainColor)
      End If
    End If

  End Sub

  Sub CommandPrep(ByVal lcmd As lastCmdDelegate, Optional ByVal assignFloat As Boolean = True)

    If Command = cmd.None Then CompleteCommand()
    If assignFloat Then mView.assignFloater()

    If mView IsNot Nothing Then
      ClearProcesses(False)
      Command = cmd.None
    End If

    ' save the last command info for the F3 Repeat
    lastCmdSub = lcmd

    ShiftDown = False
    controlDown = False

  End Sub

  Sub showEffectForm(ByRef frm As Form)
    ' called by frmBlur, Sharpen, edgdetect, etc.

    Me.Cursor = Cursors.WaitCursor
    If mView.FloaterBitmap IsNot Nothing Then
      qImage = mView.FloaterBitmap.Clone
    Else
      qImage = mView.Bitmap.Clone
    End If
    Using f As Form = frm
      dResult = f.ShowDialog()
    End Using
    If dResult = DialogResult.OK Then KeepNewBitmap(qImage)
    Me.Cursor = Cursors.Default

  End Sub

  Sub getStretchPoints(x As Double, y As Double, ByRef rx() As Double, ByRef ry() As Double, ByRef npts As Integer)
    ' adds the default points to rx, ry to get four points for the stretch box
    ' x, y are the latest cursor position, used for the "next" point
    ' stretchAspect is the aspect ratio of the current drawing
    ' npts is the number of points set by the user
    ' if x, y are set to rx(npts), ry(npts) then there is no movement, just a redraw

    Dim dx, dy As Double
    Dim i As Integer

    If npts = 0 Or (npts = 1 And x = rx(0) And y = ry(0)) Then
      dx = x - rx(0)
      dy = y - ry(0)
      For i = 0 To 3
        rx(i) = rx(i) + dx
        ry(i) = ry(i) + dy
      Next i

    ElseIf npts = 1 Then  ' draw box
      rx(1) = x
      ry(1) = y
      dx = rx(1) - rx(0)
      dy = ry(1) - ry(0)
      rx(2) = rx(0) + stretchAspect * dy
      ry(2) = ry(0) - stretchAspect * dx
      rx(3) = rx(2) + dx
      ry(3) = ry(2) + dy

    ElseIf npts = 2 Then  ' draw parallelogram
      If x = rx(1) And y = ry(1) Then
        rx(2) = rx(0) + stretchAspect * (ry(1) - ry(0))
        ry(2) = ry(0) - stretchAspect * (rx(1) - rx(0))
      Else
        rx(2) = x
        ry(2) = y
      End If
      rx(3) = rx(1) + (rx(2) - rx(0))
      ry(3) = ry(1) + (ry(2) - ry(0))

    ElseIf npts = 3 Then  ' draw quadralateral
      If x = rx(2) And y = ry(2) Then
        rx(3) = rx(1) + (rx(2) - rx(0))
        ry(3) = ry(1) + (ry(2) - ry(0))
      Else
        rx(3) = x
        ry(3) = y
      End If

      'If x = rx(2) And y = ry(2) Then
      ' rx(3) = rx(1) + (rx(2) - rx(0))
      ' ry(3) = ry(1) + (ry(2) - ry(0))
      'Else
      ' rx(3) = x
      ' ry(3) = y
      'End If


    End If

  End Sub

  Function GetStretchEdge(ByVal x As Double, ByVal y As Double) As Integer

    Dim i, k As Integer
    Dim d As Double
    Dim dmin As Double
    Dim kPoint As Integer
    Dim dx, dy As Double
    Dim y2 As Double

    ' first check points
    distance(x, y, rX(0), rY(0), dx, dy, dmin)
    kPoint = 1
    For i = 1 To npts - 1
      distance(x, y, rX(i), rY(i), dx, dy, d)
      If d < dmin Then
        kPoint = i
        dmin = d
      End If
    Next i

    If dmin < 5 Then
      GetStretchEdge = kPoint
      Exit Function
    End If

    ' is the point inside the box? If so, return 16 for move icon
    k = 0
    ' count the number of intersections above x
    i = Yintercept(x, rX(0), rY(0), rX(1), rY(1), y2)
    If i > 0 And y2 > y Then k = k + 1
    i = Yintercept(x, rX(1), rY(1), rX(3), rY(3), y2)
    If i > 0 And y2 > y Then k = k + 1
    i = Yintercept(x, rX(3), rY(3), rX(2), rY(2), y2)
    If i > 0 And y2 > y Then k = k + 1
    i = Yintercept(x, rX(2), rY(2), rX(0), rY(0), y2)
    If i > 0 And y2 > y Then k = k + 1

    If k Mod 2 <> 0 Then
      GetStretchEdge = 16
    Else
      GetStretchEdge = 0
    End If

  End Function

  Sub stretchPaste()
    ' this is used by mnuImageStretch, mnuImageAlign and mnuEditPasteV, 4-point stretch of qimage.

    Dim i As Integer
    Dim p As PointF

    mView.Cursor = Cursors.WaitCursor
    DrawingStretch = 0

    SaveUndo()
    If Command = cmd.ImageStretch Then ' image stretch instead of stretch paste
      mView.ClearSelection()
      If mView.FloaterBitmap IsNot Nothing Then
        mView.Fill(mBackColor, mView.FloaterBitmap)
      Else
        mView.Fill(mBackColor, mView.Bitmap)
      End If
    End If

    If qImage Is Nothing OrElse qImage.Width = 0 Then
      mView.Cursor = Cursors.Default
      Exit Sub
    End If

    Set32bppPArgb(qImage)

    If npts < 4 Then getStretchPoints(rX(npts - 1), rY(npts - 1), rX, rY, npts) ' fill in the extra points

    For i = 0 To 3
      p = mView.ControlToBitmap(New Point(rX(i), rY(i)))
      rX(i) = p.X : rY(i) = p.Y
    Next i

    If Command = cmd.ImageAlign Then
      alignDraw(mView, qImage, rX, rY, npts)
    Else
      stretchDraw(mView, qImage, rX, rY, npts)
    End If

    clearBitmap(qImage)
    npts = 0
    mView.RubberClear()
    mView.Zoom()
    mView.Cursor = Cursors.Default

  End Sub

  Function getCircle(p1 As Point, p2 As Point) As Rectangle
    ' convert center, outside to bounding rectangle

    Dim x1, x2, y1, y2, d As Double

    If p1 = p2 Then Exit Function

    x1 = p1.X : y1 = p1.Y
    x2 = p2.X : y2 = p2.Y
    d = Sqrt((x2 - x1) ^ 2 + (y2 - y1) ^ 2)

    Return New Rectangle(x1 - d, y1 - d, d * 2, d * 2)

  End Function


  Sub DrawDone()

    Dim rCurrent As Rectangle = Rectangle.Empty

    For i As Integer = 0 To zP.Count - 1  ' convert points to bitmap from client, for persistant drawing
      zP(i) = mView.ControlToBitmap(zP(i))
    Next i

    If zP.Count >= 2 Then rCurrent = New Rectangle(Min(zP(0).X, zP(1).X), Min(zP(0).Y, zP(1).Y), Abs(zP(0).X - zP(1).X), Abs(zP(0).Y - zP(1).Y))

    mView.DrawDashed = mView.RubberDashed

    mView.DrawPath = mView.RubberPath
    mView.DrawForeColor = mainColor
    mView.DrawBackColor = BackColor
    mView.DrawLineWidth = DrawPenWidth
    mView.DrawBox = mView.RubberBox

    Using g As Graphics = Graphics.FromImage(mView.Bitmap)

      g.SmoothingMode = SmoothingMode.HighQuality
      g.PixelOffsetMode = PixelOffsetMode.HighQuality

      Select Case Command
        Case cmd.DrawLine, cmd.DrawSketch
          mView.DrawPoints = New List(Of Point)
          mView.DrawPoints.AddRange(zP)
          mView.Draw(shape.Line, g)
        Case cmd.DrawCurve
          zP = getCurve(zP, 4)
          mView.DrawPoints = New List(Of Point)
          mView.DrawPoints.AddRange(zP)
          mView.Draw(shape.Line, g)
        Case cmd.DrawArrow
          zP = getArrow(zP, DrawPenWidth)
          mView.DrawPoints = New List(Of Point)
          mView.DrawPoints.AddRange(zP)
          mView.Draw(shape.Line, g)
        Case cmd.DrawEllipse
          mView.DrawBox = rCurrent
          mView.Draw(shape.Ellipse, g)
        Case cmd.DrawCircle
          mView.DrawBox = getCircle(zP(0), zP(1))
          mView.Draw(shape.Circle, g)
        Case cmd.DrawBox
          mView.DrawBox = rCurrent
          mView.Draw(shape.Box, g)
        Case cmd.DrawMeasure
        Case Else
      End Select

    End Using

    zP = New List(Of Point)
    mView.Zoom()

  End Sub

  Private Sub frmMain_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp, mView.KeyUp

    ShiftDown = e.Shift
    controlDown = e.Control

  End Sub

  Private Sub mnuEditRepeatCommand_Click(sender As Object, e As EventArgs) Handles mnuEditRepeatCommand.Click
    If lastCmdSub IsNot Nothing Then lastCmdSub(sender, Nothing)
  End Sub

  Private Sub mnuEditPasteNew_Click(sender As Object, e As EventArgs) Handles mnuEditPasteNew.Click
    CommandPrep(AddressOf mnuEditPasteNew_Click, False)
    EditPasteNew()
  End Sub

  Private Sub mnuViewToolbar_Click(sender As Object, e As EventArgs) Handles mnuViewToolbar.Click

    mnuViewToolbar.Checked = Not mnuViewToolbar.Checked

    iniViewToolbar = mnuViewToolbar.Checked
    ToolStrip1.Visible = iniViewToolbar
    assignAllToolbars()

  End Sub

  Private Sub mnuToolsToolbar_Click(sender As Object, e As EventArgs) Handles mnuToolsToolbar.Click
    mnxTCustomize_Click(sender, e)
  End Sub

  Private Sub mnuToolsOptions_Click(sender As Object, e As EventArgs) Handles mnuToolsOptions.Click

    CommandPrep(AddressOf mnuToolsOptions_Click, False)

    Using frm As New frmOptions
      dResult = frm.ShowDialog()
    End Using

    If dResult = DialogResult.OK Then
      ToolStrip1.Visible = iniViewToolbar
      mainColor = iniForeColor
      mBackColor = iniBackColor
      assignAllToolbars()
    End If

  End Sub

  Private Sub mnxTIconSize_Click(sender As Object, e As EventArgs) _
    Handles mnxTSmallIcons.Click, mnxTLargeIcons.Click

    Dim b As ToolStripMenuItem

    CommandPrep(AddressOf mnxTIconSize_Click, False)
    b = sender

    mnxTSmallIcons.Checked = False
    mnxTLargeIcons.Checked = False
    b.Checked = True

    If sender Is mnxTSmallIcons Then
      If iniButtonSize <> 0 Then
        iniButtonSize = 0
        assignAllToolbars()
      End If
    ElseIf sender Is mnxTLargeIcons Then
      If iniButtonSize <> 1 Then
        iniButtonSize = 1
        assignAllToolbars()
      End If
    End If

  End Sub

  Private Sub mnxTCustomize_Click(sender As Object, e As EventArgs) Handles mnxTCustomize.Click

    Dim oldButtonSize As Integer

    oldButtonSize = iniButtonSize
    CommandPrep(AddressOf mnxTCustomize_Click, False)
    callingForm = Me

    Using frm As New frmToolbar
      dResult = frm.ShowDialog()
    End Using

    If dResult = DialogResult.OK Then
      If oldButtonSize = iniButtonSize Then ' only update this form
        assignToolbar()
      Else
        assignAllToolbars()
      End If
    End If

  End Sub

  Private Sub mnxTHide_Click(sender As Object, e As EventArgs) Handles mnxTHide.Click

    CommandPrep(AddressOf mnxTHide_Click, False)
    Me.ToolStrip1.Visible = False
    iniViewToolbar = False

    assignAllToolbars()

  End Sub

  Private Sub mnxZoom100_Click(sender As Object, e As EventArgs) Handles mnxZoom100.Click
    mnuViewZoom100_click(sender, e)
  End Sub


  Private Sub mnxClose_Click(sender As Object, e As EventArgs) Handles mnxClose.Click
    mnuFileClose_click(sender, e)
  End Sub

  Private Sub mnxZoomIn_Click(sender As Object, e As EventArgs) Handles mnxZoomIn.Click
    mnuViewZoomIn_click(sender, e)
  End Sub

  Private Sub mnxZoomOut_Click(sender As Object, e As EventArgs) Handles mnxZoomOut.Click
    mnuViewZoomout_click(sender, e)
  End Sub

  Private Sub mnxViewFit_Click(sender As Object, e As EventArgs) Handles mnxViewFit.Click
    mnuViewZoomWindow_click(sender, e)
  End Sub

  Private Sub mnxSaveAs_Click(sender As Object, e As EventArgs) Handles mnxSaveAs.Click
    mnuFileSaveAs_click(sender, e)
  End Sub

  Private Sub mnxPrint_Click(sender As Object, e As EventArgs) Handles mnxPrint.Click
    mnuFilePrint_click(sender, e)
  End Sub

  Private Sub mnxSend_Click(sender As Object, e As EventArgs) Handles mnxSend.Click
    mnuFileSend_click(sender, e)
  End Sub

  Private Sub mnxCopy_Click(sender As Object, e As EventArgs) Handles mnxCopy.Click
    mnuEditCopy_click(sender, e)
  End Sub

  Private Sub mnuFileOpen_Click(sender As Object, e As EventArgs) Handles mnuFileOpen.Click
    CommandPrep(AddressOf mnuFileOpen_Click, False)
    openPicFile()
  End Sub


  Private Sub mnxTHelp_Click(sender As System.Object, ByVal e As System.EventArgs) Handles mnxTHelp.Click
    Dim topic As String

    Select Case currentTool

      Case "mnufileopen"
        topic = "fileopen"

      Case "mnufilesave"
        topic = "frmsaveas"

      Case "mnufileprint"
        topic = "frmprint"

      Case "mnufilesend"
        topic = "frmsend"

      Case "mnueditcopy", "mnueditcut", "mnueditpaste"
        topic = "copyandpaste"

      Case "mnueditdelete"
        topic = "filedelete"

      Case "mnufilenew"
        topic = "frmnew"

      Case "mnufileexplore"
        topic = "exploreandeditor"

      Case "mnueditundo", "mnueditredo"
        topic = "undoandredo"

      Case "mnueditselrectangle", "mnueditselellipse", "mnueditselfreehand", "mnueditselectsimilar"
        topic = "selections"

      Case "mnudrawtext"
        topic = "drawingtext"

      Case "mnuviewzoomin", "mnuviewzoomout", "mnuviewzoom"
        topic = "viewzoom"

      Case "mnuviewfullscreen"
        topic = "frmfullscreen"

      Case "mnuviewrefresh"
        topic = "usingthetoolbar"

      Case "mnuimagerotateleft", "mnuimagerotateright"
        topic = "frmrotate"

      Case "mnuimagesetcrop"
        topic = "crop"

      Case "mnudrawline", "mnudrawsketch"
        topic = "drawinglines"

      Case "mnudrawfill"
        topic = "drawfill"

      Case "mnudrawfillselection"
        topic = "drawfillselection"

      Case "mnucolorsample", "mnudrawforecolor", "mnudrawbackcolor"
        topic = "drawingcolors"

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

  Public Sub toolStrip1_MouseMove(sender As Object, e As EventArgs) ' handler for all buttons

    ' this is the handler for all tool buttons' click, assigned in assignVToolbar
    currentTool = sender.tag

  End Sub

  Public Sub toolStrip1_Click(sender As Object, e As EventArgs) ' handler for all buttons
    ' this is the handler for all tool buttons, assigned in assignToolbar

    Select Case CStr(sender.Tag)
      Case "mnufilenew"
        mnuFileNew_Click(sender, e)
      Case "mnufileopen"
        mnuFileOpen_Click(sender, e)
      Case "mnufilesave"
        mnuFileSaveAs_click(sender, e)
      Case "mnufileprint"
        mnuFilePrint_click(sender, e)
      Case "mnufilesend"
        mnuFileSend_click(sender, e)
      Case "mnufileexplore"
        mnuFileExplore_Click(sender, e)

      Case "mnueditcopy"
        mnuEditCopy_click(mnuEditCopy, e)
      Case "mnueditdelete"
        mnuEditDeleteFile_click(sender, e)
      Case "mnueditcut"
        mnuEditCopy_click(mnuEditCut, e)
      Case "mnueditpaste"
        mnuEditPaste_click(sender, e)
        If Not abort Then checkToolButton(ToolStrip1, sender.tag, True) ' for interactive commands
        LastButton = sender.tag ' last toolbar button pressed
      Case "mnueditundo"
        mnuEditUndo_click(sender, e)
      Case "mnueditredo"
        mnuEditRedo_click(sender, e)
      Case "mnueditselrectangle"
        mnuEditSelectRectangle_click(sender, e)
        If Not abort Then checkToolButton(ToolStrip1, sender.tag, True) ' for interactive commands
        LastButton = sender.tag ' last toolbar button pressed
      Case "mnueditselellipse"
        mnuEditSelectEllipse_click(sender, e)
        If Not abort Then checkToolButton(ToolStrip1, sender.tag, True) ' for interactive commands
        LastButton = sender.tag ' last toolbar button pressed
      Case "mnueditselfreehand"
        mnuEditSelectFreehand_click(sender, e)
        If Not abort Then checkToolButton(ToolStrip1, sender.tag, True) ' for interactive commands
        LastButton = sender.tag ' last toolbar button pressed
      Case "mnueditselectsimilar"
        mnuEditSelectSimilar_click(sender, e)
        If Not abort Then checkToolButton(ToolStrip1, sender.tag, True) ' for interactive commands
        LastButton = sender.tag ' last toolbar button pressed
      Case "mnudrawtext"
        mnuDrawText_click(sender, e)
        If Not abort Then checkToolButton(ToolStrip1, sender.tag, True) ' for interactive commands
        LastButton = sender.tag ' last toolbar button pressed

      Case "mnuviewzoomin"
        mnuViewZoomIn_click(sender, e)
      Case "mnuviewzoomout"
        mnuViewZoomout_click(sender, e)
      Case "mnuviewzoom"
        mnuViewZoomWindow_click(sender, e)
        If Not abort Then checkToolButton(ToolStrip1, sender.tag, True) ' for interactive commands
        LastButton = sender.tag ' last toolbar button pressed
      Case "mnuviewfullscreen"
        mnuViewFullscreen_click(sender, e)
      Case "mnuviewrefresh"
        mnuViewRefresh_click(sender, e)

      Case "mnuimagerotateleft"
        rotateLeftRight(1)
      Case "mnuimagerotateright"
        rotateLeftRight(2)
      Case "mnuimagesetcrop"
        mnuImageSetCrop_click(sender, e)
        If Not abort Then checkToolButton(ToolStrip1, sender.tag, True) ' for interactive commands
        LastButton = sender.tag ' last toolbar button pressed

      Case "mnudrawline"
        mnuDrawLine_Click(sender, e)
        If Not abort Then checkToolButton(ToolStrip1, sender.tag, True) ' for interactive commands
        LastButton = sender.tag ' last toolbar button pressed
      Case "mnudrawsketch"
        mnuDrawSketch_click(sender, e)
        If Not abort Then checkToolButton(ToolStrip1, sender.tag, True) ' for interactive commands
        LastButton = sender.tag ' last toolbar button pressed
      Case "mnudrawfill"
        mnuDrawFill_click(sender, e)
        If Not abort Then checkToolButton(ToolStrip1, sender.tag, True) ' for interactive commands
        LastButton = sender.tag ' last toolbar button pressed
      Case "mnudrawfillselection"
        mnuDrawFillSelection_click(sender, e)
      Case "mnucolorsample"
        mnuColorSample_click(sender, e)
        If Not abort Then checkToolButton(ToolStrip1, sender.tag, True) ' for interactive commands
        LastButton = sender.tag ' last toolbar button pressed
      Case "mnudrawforecolor"
        mnuDrawColor_Click(sender, e)
      Case "mnudrawbackcolor"
        mnuDrawBackColor_Click(sender, e)

      Case "mnuhelphelp"
        mnuHelpHelpIndex_Click(sender, e)

    End Select

  End Sub

  Public Sub mView_MouseWheel(sender As Object, ByVal e As MouseEventArgs) Handles mView.MouseWheel

    beforeZoom()
    mouseWheelZoom(mView, e, timerRedraw, zoomStep, InterpolationMode.Low)
    afterZoom()
    StatusLine()

  End Sub

  Sub beforeZoom()
    ' save in-process points to logical coordinates for a zoom
    Dim pf As Point

    If mView.RubberEnabled Or rBoxMode <> rbMode.none Then
      rb = mView.ControlToBitmap(mView.RubberBox)
      rbq = mView.ControlToBitmap(New Rectangle(rbX, rbY, rbWidth, rbHeight))
      rbox = mView.ControlToBitmap(New Rectangle(rboxleft, rBoxTop, rBoxWidth, rBoxHeight))
      pf = mView.ControlToBitmap(New Point(rBoxX, rBoxY))
      rBoxX = pf.X : rBoxY = pf.Y
    End If

  End Sub

  Sub afterZoom()
    ' restore to client coordinates
    Dim p As Point

    If mView.RubberEnabled Or rBoxMode <> rbMode.none Then
      rb = mView.BitmapToControl(rb)
      rbq = mView.BitmapToControl(rbq)
      rbox = mView.BitmapToControl(rbox)

      rbX = rbq.X : rbY = rbq.Y
      rbWidth = rbq.Width : rbHeight = rbq.Height

      rboxleft = rbox.X : rBoxTop = rbox.Y
      rBoxWidth = rbox.Width : rBoxHeight = rbox.Height

      p = mView.BitmapToControl(New Point(rBoxX, rBoxY))
      rBoxX = p.X : rBoxY = p.Y

    End If

  End Sub

  Private Sub mnxSave_Click(sender As Object, e As EventArgs) Handles mnxSave.Click
    mnuFileSave_click(sender, e)
  End Sub

  Private Sub mnuFileNew_Click(sender As Object, e As EventArgs) Handles mnuFileNew.Click
    CommandPrep(AddressOf mnuFileNew_Click, False)
    Using frm As New frmNew
      dResult = frm.ShowDialog()
    End Using
  End Sub

  Private Sub mnuFileCloseAll_Click(sender As Object, e As EventArgs) Handles mnuFileCloseAll.Click
    closeAll()
  End Sub

  Private Sub mnuFileExit_Click(sender As Object, e As EventArgs) Handles mnuFileExit.Click
    abortClosing = False
    Me.Close()
  End Sub

  Private Sub mnuFileMru_Click(sender As Object, e As EventArgs) _
    Handles mnuFileMru1.Click, mnuFileMru2.Click, mnuFileMru3.Click, mnuFileMru4.Click, mnuFileMru5.Click, _
    mnuFileMru6.Click, mnuFileMru7.Click, mnuFileMru8.Click, mnuFileMru9.Click

    Dim index As Integer
    Dim s As String

    s = sender.name
    index = CInt(s.Substring(s.Length - 1))
    LoadMruFile(index) ' index is 1-9 -- which file

  End Sub

  Private Sub mnuHelpHelpTopics_Click(sender As Object, e As EventArgs) Handles mnuHelpHelpTopics.Click
    Help.ShowHelp(Me, helpFile, HelpNavigator.TableOfContents, "")
  End Sub

  Private Sub mnuHelpHelpIndex_Click(sender As Object, e As EventArgs) Handles mnuHelpHelpIndex.Click
    Help.ShowHelpIndex(Me, helpFile)
  End Sub

  Private Sub mnuHelpTips_Click(sender As Object, e As EventArgs) Handles mnuHelpTips.Click
    Using frm As New frmTips
      dResult = frm.ShowDialog()
    End Using
  End Sub

  Private Sub mnuHelpRegister_Click(sender As Object, e As EventArgs) Handles mnuHelpRegister.Click
    HelpBrowse(urlRegister)
  End Sub

  Private Sub mnuHelpUpdate_Click(sender As Object, e As EventArgs) Handles mnuHelpUpdate.Click
    HelpBrowse(urlUpdate)
  End Sub

  Private Sub mnuHelpAbout_Click(sender As Object, e As EventArgs) Handles mnuHelpAbout.Click
    HelpAbout()
  End Sub

  Private Sub TimerRedraw_Tick(sender As Object, e As EventArgs) Handles timerRedraw.Tick
    timerRedraw.Stop()
    ' refresh at higher quality after some milliseconds
    If (mView IsNot Nothing) AndAlso mView.InterpolationMode <> InterpolationMode.High Then
      mView.InterpolationMode = InterpolationMode.High
      mView.Zoom()
    End If

  End Sub

  Private Sub setToolColor(ByVal tag As String, ByVal toolColor As Color)

    Dim item As ToolStripItem
    Dim b As ToolStripButton

    For Each item In ToolStrip1.Items
      If eqstr(item.Tag, tag) Then
        b = item
        b.BackColor = toolColor
        Exit For
      End If
    Next item

  End Sub

  Private Sub mnuFileExplore_Click(sender As System.Object, e As EventArgs) Handles mnuFileExplore.Click
    showExplore()
  End Sub

  Sub EnableMenus(ByVal enabled As Boolean)
    ' turn most menu and toolbar items on or off

    Dim item As ToolStripItem
    Dim b As ToolStripButton

    mnuFile.Enabled = enabled
    For Each item In mnuFile.DropDownItems : item.Enabled = enabled : Next item
    mnuEdit.Enabled = enabled
    For Each item In mnuEdit.DropDownItems : item.Enabled = enabled : Next item
    mnuImage.Enabled = enabled
    For Each item In mnuImage.DropDownItems : item.Enabled = enabled : Next item
    mnuColor.Enabled = enabled
    For Each item In mnuColor.DropDownItems : item.Enabled = enabled : Next item
    mnuDraw.Enabled = enabled
    For Each item In mnuDraw.DropDownItems : item.Enabled = enabled : Next item
    mnuTools.Enabled = enabled
    For Each item In mnuTools.DropDownItems : item.Enabled = enabled : Next item
    mnuViewFullscreen.Enabled = enabled
    mnuViewToolbar.Enabled = enabled

    For Each item In ToolStrip1.Items
      If TypeOf item Is ToolStripButton Then
        b = item
        Select Case CStr(b.Tag)
          Case "mnucolorsample"
            b.Enabled = enabled
          Case "mnudrawbackcolor"
            b.Enabled = enabled
          Case "mnudrawfill"
            b.Enabled = enabled
          Case "mnudrawfillselection"
            b.Enabled = enabled
          Case "mnudrawforecolor"
            b.Enabled = enabled
          Case "mnudrawline"
            b.Enabled = enabled
          Case "mnudrawsketch"
            b.Enabled = enabled
          Case "mnudrawtext"
            b.Enabled = enabled
          Case "mnueditcopy"
            b.Enabled = enabled
          Case "mnueditcut"
            b.Enabled = enabled
          Case "mnueditdelete"
            b.Enabled = enabled
          Case "mnueditpaste"
            b.Enabled = enabled
          Case "mnueditredo"
            If enabled Then
              If mView.nRedo > mView.nUndo Then b.Enabled = True Else b.Enabled = False
            Else
              b.Enabled = enabled
            End If
          Case "mnueditselectsimilar"
            b.Enabled = enabled
          Case "mnueditselellipse"
            b.Enabled = enabled
          Case "mnueditselfreehand"
            b.Enabled = enabled
          Case "mnueditselrectangle"
            b.Enabled = enabled
          Case "mnueditundo"
            If enabled Then
              If mView.nUndo >= 1 Then b.Enabled = True Else b.Enabled = False
            Else
              b.Enabled = enabled
            End If
          Case "mnufileexplore"
            b.Enabled = enabled
          Case "mnufilenew"
            b.Enabled = enabled
          Case "mnufileopen"
            b.Enabled = enabled
          Case "mnufileprint"
            b.Enabled = enabled
          Case "mnufilesave"
            b.Enabled = enabled
          Case "mnufilesend"
            b.Enabled = enabled
          Case "mnuimagerotateleft"
            b.Enabled = enabled
          Case "mnuimagerotateright"
            b.Enabled = enabled
          Case "mnuimagesetcrop"
            b.Enabled = enabled
        End Select
      End If
    Next item

  End Sub


  '===================================================================
  ' Brightness / Contrast / Saturation panel

  Sub ctlbright_changed() Handles mView.ctlBrightChanged
    timerBright.Interval = 250
    timerBright.Stop() : timerBright.Start()
  End Sub

  Sub drawBright()
    ' do the brightness commands and redraw, called by timer
    Dim tmpCursor As Cursor
    Dim bmp As Bitmap = Nothing ' cannot manipulate property floaterbitmap directly. vb bug?

    If qImage Is Nothing Or Command = cmd.None Or Loading > 0 Then Exit Sub
    tmpCursor = mView.Cursor
    mView.Cursor = Cursors.WaitCursor
    abort = False

    If mView.ctlBright.Brightness <> 0 Or mView.ctlBright.Contrast <> 0 Or mView.ctlBright.Saturation <> 0 Then
      If mView.FloaterPath Is Nothing Or mView.FloaterBitmap Is Nothing Then
        mView.BrightConSat(mView.ctlBright.Brightness, mView.ctlBright.Contrast, mView.ctlBright.Saturation,
                           qImage, bmp, Nothing)
        mView.setBitmap(bmp)
      Else
        mView.BrightConSat(mView.ctlBright.Brightness, mView.ctlBright.Contrast, mView.ctlBright.Saturation,
                           qImage, bmp, mView.FloaterPath)
        mView.setFloaterBitmap(bmp)
      End If
      clearBitmap(bmp)
      mView.Zoom()
    End If

    If abort Then mView_ctlbrightCancel()

    mView.Cursor = tmpCursor
  End Sub

  Sub setPanelBright()

    mView.ctlBright.BringToFront()
    If mView.ctlFeather.Visible Then mView.ctlFeather.Visible = False
    helpProvider1.SetHelpKeyword(Me, "brightness.html")

    mView.ctlBright.Left = mView.Left + 15
    mView.ctlBright.Top = mView.Top + 15
    ' Panel1.Top = mView.Top + mView.Height - Panel1.Height - 10 ' bottom
    mView.ctlBright.Visible = True

    mView.SelectionVisible = True

    If mView.FloaterBitmap IsNot Nothing Then
      qImage = mView.FloaterBitmap.Clone
    Else
      qImage = mView.Bitmap.Clone
    End If

    mView.ctlBright.setMode(Command)

    Me.CancelButton = mView.ctlBright.cmdCancel
    Me.AcceptButton = mView.ctlBright.cmdOK

    EnableMenus(False)

  End Sub

  Private Sub mView_ctlbrightOK() Handles mView.ctlBrightOK
    Dim gImage As Bitmap = Nothing

    mView.Cursor = Cursors.WaitCursor
    drawBright()

    If mView.FloaterBitmap IsNot Nothing Then ' no saveundo
      mView.SelectionVisible = False
      CompleteCommand()
      clearBitmap(qImage)
      Command = cmd.Floater
      mView.RubberDashed = True
      setPanelFeather()
    Else
      gImage = mView.Bitmap.Clone
      mView.setBitmap(qImage)  ' save the original in saveundo
      clearBitmap(qImage)
      SaveUndo()
      mView.setBitmap(gImage)
      clearBitmap(gImage)
      CompleteCommand()
    End If

    closePanelBright()

  End Sub

  Private Sub mView_ctlbrightCancel() Handles mView.ctlBrightCancel

    If cmdRunning Then
      abort = True

    Else
      If mView.FloaterBitmap Is Nothing Then
        mView.setBitmap(qImage)
      Else
        mView.setFloaterBitmap(qImage)
        clearBitmap(qImage)
      End If

      clearBitmap(qImage)
      If mView.ctlBright.Visible Then closePanelBright()
      ClearProcesses(False)
    End If

  End Sub

  ' end of Brightness, Contrast, Saturation panel
  '===================================================================
  ' panel Tolerance, for select similar, magic wand

  Sub setPanelTolerance()

    mView.ctlTolerance.BringToFront()

    If Command = cmd.SelectSimilar Then
      helpProvider1.SetHelpKeyword(Me, "editselectsimilarcolors.html")
    Else
      helpProvider1.SetHelpKeyword(Me, "drawfill.html")
    End If

    If mView.ctlFeather.Visible Then mView.ctlFeather.Visible = False

    mView.ctlTolerance.Left = mView.Left + 15
    mView.ctlTolerance.Top = mView.Top + 15
    mView.ctlTolerance.Visible = True

    mView.SelectionVisible = True

    Me.CancelButton = mView.ctlTolerance.cmdCancel
    Me.AcceptButton = mView.ctlTolerance.cmdOk

    mView.ctlTolerance.Tolerance = colorTolerance
    mView.ctlTolerance.saveDefault = False

    If mView.FloaterBitmap IsNot Nothing Then
      qImage = mView.FloaterBitmap.Clone
    Else
      qImage = mView.Bitmap.Clone
    End If

    EnableMenus(False)

  End Sub

  Private Sub mView_CtlToleranceChanged() Handles mView.ctlToleranceChanged
    colorTolerance = mView.ctlTolerance.Tolerance
    SetTolerance()
  End Sub

  Sub mView_ctlToleranceOK() Handles mView.ctlToleranceOK
    toleranceDone(True)
  End Sub

  Sub mView_ctlToleranceCancel() Handles mView.ctlToleranceCancel
    toleranceDone(False)
  End Sub

  Sub SetTolerance()
    ' clickpoint is a global -- the point clicked by the mouse

    Dim p As Point
    Dim tmpCursor As Cursor

    If clickPoint = Point.Empty Then Exit Sub

    tmpCursor = mView.Cursor
    mView.Cursor = Cursors.WaitCursor

    p = mView.ControlToBitmap(clickPoint)
    If mView.FloaterPath Is Nothing OrElse (mView.BitmapPath Is Nothing OrElse mView.BitmapPath.IsVisible(p)) Then

      If mView.FloaterBitmap IsNot Nothing Then
        p.X -= mView.FloaterPosition.X
        p.Y -= mView.FloaterPosition.Y
      End If

      If p.X < 0 Then p.X = 0
      If p.X > qImage.Width - 1 Then p.X = qImage.Width - 1
      If p.Y < 0 Then p.Y = 0
      If p.Y > qImage.Height - 1 Then p.Y = qImage.Height - 1

      Using img As New MagickImage(qImage)
        img.ColorFuzz = colorTolerance
        img.FloodFill(New MagickColor(mainColor), p.X, p.Y)
        Dim bmp As Bitmap = img.ToBitmap
        Set32bppPArgb(bmp)
        If mView.FloaterBitmap Is Nothing Then
          mView.setBitmap(bmp)
        Else
          mView.setFloaterBitmap(bmp)
        End If
        Using gPath As GraphicsPath = alphaToPath(bmp)
          If gPath IsNot Nothing AndAlso gPath.PointCount > 0 Then mView.FloaterPath = gPath.Clone Else mView.FloaterPath = Nothing
        End Using
        clearBitmap(bmp)
      End Using
      mView.Zoom()
    End If

    If Command = cmd.SelectSimilar Then ' select similar
    Else ' fill
    End If

    mView.Cursor = tmpCursor

  End Sub

  Sub toleranceDone(ByVal OK As Boolean)

    mView.Cursor = Cursors.WaitCursor

    If OK AndAlso mView.ctlTolerance.saveDefault Then iniColorTolerance = colorTolerance

    If Not OK Then
      If mView.FloaterBitmap Is Nothing Then ' restore the old region to floater
        mView.setBitmap(qImage)
      Else
        mView.setFloaterBitmap(qImage)
      End If
    End If

    If mView.FloaterBitmap IsNot Nothing Then
      mView.FloaterVisible = True
      Command = cmd.Floater
      mView.RubberDashed = True
      setPanelFeather()
    Else
      Command = cmd.None
    End If

    closePanelTolerance()

  End Sub

  ' end panelTolerance stuff
  '==========================================
  ' Feather

  Sub mView_ctlFeatherrangeChanged() Handles mView.ctlFeatherRangeChanged
    timerFeather.Interval = 250
    timerFeather.Stop() : timerFeather.Start()
  End Sub

  Sub SetFeather()

    ' feather the edges of the selection
    ' a. draw transparent alpha outside path
    ' b. blur the alpha layer using imagemagick
    ' c. save it to mview.floaterbitmap

    Dim tmp As Cursor
    Dim sigma, radius As Double

    If Processing Or Loading > 0 Then Exit Sub
    If mView.FloaterBitmap Is Nothing Or mView.ctlFeather.Range = 0 Then Exit Sub

    tmp = mView.Cursor
    mView.Cursor = Cursors.WaitCursor

    Using bmp As New Bitmap(mView.FloaterBitmap.Width, mView.FloaterBitmap.Height, PixelFormat.Format32bppPArgb),
          g As Graphics = Graphics.FromImage(bmp)
      g.Clear(Color.Transparent) ' set transparent background
      g.SetClip(mView.FloaterPath) ' draw selection into bmp
      g.DrawImage(mView.FloaterBitmap, New Rectangle(0, 0, mView.FloaterBitmap.Width, mView.FloaterBitmap.Height))

      Using img As New MagickImage(bmp)
        featherAmount = mView.ctlFeather.Range
        radius = featherAmount
        sigma = 255
        img.VirtualPixelMethod = VirtualPixelMethod.Transparent
        img.Blur(radius, sigma, Channels.Alpha)
        img.Level(New Percentage(50), New Percentage(100), Channels.Alpha)
        mView.setFloaterBitmap(img.ToBitmap)
        mView.Zoom()
      End Using
    End Using

    mView.Cursor = tmp

  End Sub

  Sub setPanelFeather()

    If mView.FloaterBitmap Is Nothing Then Exit Sub
    Processing = True ' flag for events
    mView.ctlFeather.Visible = True
    mView.ctlFeather.BringToFront()

    'If Not fixedMask Then ' leave panel invisible for paste if fixedmask is true
    mView.ctlFeather.Left = mView.Left + 15
    mView.ctlFeather.Top = mView.Top + 15
    mView.ctlFeather.Visible = True

    mView.ctlFeather.Range = featherAmount
    'End If

    mView.SelectionVisible = True
    Processing = False

  End Sub


  ' end of feather panel
  '===================================================================
  ' panel Text, for select similar, magic wand

  Sub setPanelText()

    helpProvider1.SetHelpKeyword(Me, "drawingtext.html")
    mView.ctlText.BringToFront()

    If mView.ctlFeather.Visible Then mView.ctlFeather.Visible = False

    mView.ctlText.Left = mView.Left + 15
    mView.ctlText.Top = mView.Top + 15
    mView.ctlText.Visible = True

    Processing = True

    textColor = iniTextColor
    mView.ctlText.textColor = iniTextColor
    textBackColor = iniTextBackColor
    mView.ctlText.textBackgroundColor = iniTextBackColor
    textAngle = iniTextAngle
    mView.ctlText.textAngle = iniTextAngle

    textMultiline = iniTextMultiline
    mView.ctlText.Multiline = iniTextMultiline
    textBackFill = iniTextBackFill
    mView.ctlText.BackFill = iniTextBackFill

    textFmt = StringFormat.GenericTypographic
    textFmt.Alignment = StringAlignment.Center
    textFmt.LineAlignment = StringAlignment.Center
    textFmt.Trimming = StringTrimming.None
    textFmt.FormatFlags = StringFormatFlags.MeasureTrailingSpaces Or StringFormatFlags.NoClip Or StringFormatFlags.FitBlackBox

    fontSize = iniFontSize
    mView.ctlText.fontSize = iniFontSize
    fontName = iniFontName
    mView.ctlText.fontName = iniFontName
    fBold = iniFontBold
    mView.ctlText.Bold = iniFontBold
    fItalic = iniFontitalic
    mView.ctlText.Italic = iniFontitalic
    fUnderline = iniFontUnderline
    mView.ctlText.Underline = iniFontUnderline
    mView.ctlText.textString = textString

    mView.ctlText.DateTime = getBmpComment(propID.DateTime, mView.pComments)
    mView.ctlText.Description = getBmpComment(propID.DateTime, mView.pComments)

    Processing = False

    Me.CancelButton = mView.ctlText.cmdCancel
    Me.AcceptButton = mView.ctlText.cmdOK

    EnableMenus(False)

    textPoint = New Point(mView.Width \ 2, mView.Height \ 2)
    mView_ctlTextChanged()

  End Sub

  Sub mView_ctlTextChanged() Handles mView.ctlTextChanged

    textString = mView.ctlText.textString
    textAngle = mView.ctlText.textAngle
    textMultiline = mView.ctlText.Multiline
    textBackFill = mView.ctlText.BackFill
    textColor = mView.ctlText.textColor
    textBackColor = mView.ctlText.textBackgroundColor
    fontSize = mView.ctlText.fontSize
    fontName = mView.ctlText.fontName
    fUnderline = mView.ctlText.Underline
    fBold = mView.ctlText.Bold
    fItalic = mView.ctlText.Italic

    Command = cmd.DrawText

    mView.RubberClear()
    mView.RubberPoints = New List(Of Point)
    mView.RubberPoints.Add(textPoint)
    mView.RubberShape = shape.Text
    mView.RubberEnabled = True
    mView.RubberLineWidth = DrawPenWidth * mView.ZoomFactor
    mView.RubberColor = textColor
    mView.rubberBackColor = textBackColor
    mView.RubberString = textString
    mView.RubberAngle = textAngle
    mView.RubberFont = getFont(mView.ZoomFactor, fontName, fontSize, fBold, fItalic, fUnderline)
    mView.RubberTextFmt = textFmt
    mView.RubberFilled = textBackFill

    mView.Invalidate()

  End Sub

  Function getTextRectangle(textPoint As Point) As Rectangle

    Dim tSize As Size
    Dim r As Rectangle
    Dim pp As New List(Of Point)
    Dim sine, cosine As Double
    Dim xMin, yMin, xMax, yMax As Double

    If textString = "" Then Return Rectangle.Empty

    Using g As Graphics = mView.CreateGraphics,
          gfont As Font = getFont(mView.ZoomFactor, fontName, fontSize, fBold, fItalic, fUnderline)
      tSize = Size.Round(g.MeasureString(textString, gfont, textPoint, textFmt))

      pp.Add(New Point(0, 0))
      pp.Add(New Point(pp(0).X + tSize.Width + tSize.Height * 0.6, pp(0).Y))
      pp.Add(New Point(pp(1).X, pp(0).Y + tSize.Height))
      pp.Add(New Point(pp(0).X, pp(2).Y))
    End Using

    If textAngle <> 0 Then
      sine = Sin(-textAngle * piOver180) : cosine = Cos(-textAngle * piOver180)
      For i As Integer = 0 To pp.Count - 1
        pp(i) = rotatePoint(pp(i), New Point(0, 0), sine, cosine)
      Next i
    End If

    ' get the width and height of the rotated box.
    xMin = pp(0).X : yMin = pp(0).Y
    xMax = pp(0).X : yMax = pp(0).Y
    For i As Integer = 1 To 3
      If pp(i).X < xMin Then xMin = pp(i).X
      If pp(i).Y < yMin Then yMin = pp(i).Y
      If pp(i).X > xMax Then xMax = pp(i).X
      If pp(i).Y > yMax Then yMax = pp(i).Y
    Next i

    r.Width = xMax - xMin + 1
    r.Height = yMax - yMin + 1
    r.X = textPoint.X - r.Width \ 2
    r.Y = textPoint.Y - r.Height \ 2

    Return r

  End Function

  Private Sub mView_ctlTextOK() Handles mView.ctlTextOK
    Dim p As Point

    SaveUndo()
    p = mView.ControlToBitmap(textPoint)
    Using g As Graphics = Graphics.FromImage(mView.Bitmap),
          gFont As Font = getFont(1, fontName, fontSize, fBold, fItalic, fUnderline)
      mView.DrawText(g, textString, p, gFont, textColor, textBackColor, textBackFill, textAngle, textFmt)
    End Using

    saveTextParameters()
    CompleteCommand()
  End Sub

  Private Sub mView_ctlTextcancel() Handles mView.ctlTextCancel
    ClearProcesses(True)
    If mView.ctlText.Visible Then closePanelText()
  End Sub

  Sub closePanelTolerance()
    mView.ctlTolerance.Visible = False
    panelCleanup()
  End Sub

  Sub closePanelBright()
    mView.ctlBright.Visible = False
    panelCleanup()
  End Sub

  Sub closePanelText()
    mView.ctlText.Visible = False
    panelCleanup()
  End Sub

  Sub panelCleanup()

    mView.Cursor = Cursors.WaitCursor
    Me.CancelButton = Nothing
    Me.AcceptButton = Nothing

    EnableMenus(True)

    mView.Cursor = Cursors.Default

    StatusLine()
    If LastButton <> "" Then
      checkToolButton(ToolStrip1, LastButton, False)
      ToolStrip1.Refresh()
    End If
    mView.Zoom()

    helpProvider1.SetHelpKeyword(Me, "explorerandeditor.html")
  End Sub

  Sub saveTextParameters()
    iniTextAngle = textAngle
    iniFontSize = fontSize
    iniTextColor = textColor
    iniTextBackColor = textBackColor
    iniFontName = fontName
    iniFontBold = fBold
    iniFontitalic = fItalic
    iniFontUnderline = fUnderline
    iniTextMultiline = textMultiline
    iniTextBackFill = textBackFill
  End Sub

  ' end panelText stuff
  ' ==========================================

  Private Sub mnuColorSepia_Click(sender As System.Object, e As EventArgs) Handles mnuColorSepia.Click

    CommandPrep(AddressOf mnuColorSepia_Click)
    Me.Cursor = Cursors.WaitCursor
    SaveUndo()
    mView.MakeSepia()
    mView.Zoom()
    Me.Cursor = Cursors.Default

  End Sub

  Private Sub mnuFile_DropDownOpening(sender As System.Object, e As EventArgs) Handles mnuFile.DropDownOpening
    loadMru(mnuFile)
  End Sub

  Public Sub assignToolbar()

    Dim i As Integer

    Dim b As ToolStripItem
    Dim b1 As ToolStripItem
    Dim ts As ToolStripSeparator

    Select Case iniButtonSize
      Case 0
        ToolStrip1.ImageScalingSize = New Size(24, 24)
      Case Else ' 1
        ToolStrip1.ImageScalingSize = New Size(32, 32)
    End Select

    readToolButtons(iniButtonSize)

    ToolStrip1.Items.Clear()
    For i = 1 To nToolButtons

      If iniToolButton(i) = "---" Then ' make a new separator item 
        ts = New ToolStripSeparator
        ts.Tag = "---" & i
        ts.Name = "tsSeparator" & i
        ts.Size = New Size(32, 32)
        ToolStrip1.Items.Add(ts)
      Else
        Try
          b1 = iniButton(iniToolButton(i))
          ' duplicate the button for multiple forms
          b = New ToolStripButton
          b.Text = b1.Text
          b.TextImageRelation = b1.TextImageRelation
          b.Tag = b1.Tag
          b.Name = b1.Name
          b.ToolTipText = b1.ToolTipText
          Try
            b.Image = iniToolbarPic(b1.Tag)
          Catch
          End Try
          b.Margin = New Padding(2)
          ToolStrip1.Items.Add(b)  ' add button to toolstrip
          AddHandler b.Click, AddressOf toolStrip1_Click
          AddHandler b.MouseMove, AddressOf toolStrip1_MouseMove
        Catch
          MsgBox("Menu button " & iniToolButton(i) & " is missing.")
        End Try
      End If
    Next i

    setToolColor("mnudrawbackcolor", mBackColor)
    setToolColor("mnudrawforecolor", mainColor)

    For Each b In ToolStrip1.Items
      If iniToolbarText Then
        b.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
      Else
        b.DisplayStyle = ToolStripItemDisplayStyle.Image
      End If
    Next b

  End Sub

  Sub showExplore()
    ' Shows frmExplore

    changeForm(Me, frmExplore)
    frmExplore.ListView1.Select()

  End Sub

  Sub setPage(ByVal i As Integer)

    If insideSetPage Or mView.PageCount <= 1 Then Exit Sub
    If i < 0 Or i >= mView.PageCount Then i = 0

    insideSetPage = True
    mView.CurrentPage = i
    nmPage.Value = i + 1
    insideSetPage = False

    mView.Zoom()

  End Sub

  Private Sub nmPage_ValueChanged(sender As System.Object, e As EventArgs) Handles nmPage.ValueChanged
    If Not nmPage.Visible Then Exit Sub
    setPage(nmPage.Value - 1)
  End Sub

  Private Sub cmdAnimate_Click(sender As System.Object, e As EventArgs) Handles cmdAnimate.Click

    If mView.PageCount > 1 Then
      If timerAnimate.Enabled Then
        timerAnimate.Stop()
        cmdAnimate.Text = "Animate"
        Command = cmd.None
      Else
        timerAnimate.Interval = 100 ' 10 per second
        timerAnimate.Start()
        cmdAnimate.Text = "Stop"
        Command = cmd.Animate
      End If
    End If

  End Sub

  Private Sub TimerAnimate_Tick(sender As System.Object, e As EventArgs) Handles timerAnimate.Tick
    Dim i As Integer
    timerAnimate.Enabled = False
    If mView IsNot Nothing AndAlso mView.Bitmap IsNot Nothing Then
      i = mView.CurrentPage + 1
      If i >= mView.PageCount Then i = 0
      setPage(i)
      mView.Zoom()
    Else
      setPage(0)
    End If
    Application.DoEvents()
    timerAnimate.Enabled = True
  End Sub
  Private Sub TimerBright_Tick(sender As System.Object, e As EventArgs) Handles timerBright.Tick
    timerBright.Stop()
    drawBright()
  End Sub

  Private Sub TimerFeather_Tick(sender As System.Object, e As EventArgs) Handles timerFeather.Tick
    timerFeather.Stop()
    SetFeather()
  End Sub

  Private Sub cmdDeletePage_Click(sender As System.Object, e As EventArgs) Handles cmdDeletePage.Click
    ' deletes the current page from mview.bitmap

    Dim i As Integer

    If mView.PageCount > 1 Then
      i = mView.CurrentPage
      mView.RemovePage(mView.CurrentPage)
      nmPage.Maximum = mView.PageCount
      lbPage.Text = "Page (of " & mView.PageCount & "): "
      If i >= mView.PageCount Then i = i - 1
      setPage(i)
    End If

    If mView.PageCount <= 1 And panelPage.Visible Then
      panelPage.Visible = False
      mView.Zoom()
    End If

  End Sub

  Private Sub cmdInsertPage_Click(sender As System.Object, e As EventArgs) Handles cmdInsertPage.Click
    ' insert a new page into mview.bitmap
    Dim i As Integer

    i = mView.CurrentPage
    Using bmp As New Bitmap(mView.Bitmap.Width, mView.Bitmap.Height, PixelFormat.Format32bppPArgb)
      mView.InsertPage(i, bmp)
    End Using
    nmPage.Maximum = mView.PageCount
    lbPage.Text = "Page (of " & mView.PageCount & "): "
    setPage(i)
    If mView.PageCount > 1 And Not panelPage.Visible Then panelPage.Visible = True

  End Sub

  Public Sub setupPanelPage(ByVal mView As mudViewer)

    If mView IsNot Nothing AndAlso mView.Bitmap IsNot Nothing AndAlso mView.PageCount > 1 Then
      nmPage.Minimum = 1
      nmPage.Maximum = mView.PageCount
      nmPage.Accelerations.Add(New NumericUpDownAcceleration(2, 100))
      lbPage.Text = "Page (of " & mView.PageCount & "): "
      setPage(0)
      panelPage.Visible = True
    Else
      panelPage.Visible = False
    End If

  End Sub

  Private Sub mnuEditInsertPage_Click(sender As System.Object, e As EventArgs) Handles mnuEditInsertPage.Click
    cmdInsertPage_Click(sender, e)
  End Sub

  Private Sub mnuEditDeletePage_Click(sender As System.Object, e As EventArgs) Handles mnuEditDeletePage.Click
    cmdDeletePage_Click(sender, e)
  End Sub

  Private Sub mnuview_DropDownOpening(sender As Object, e As EventArgs) Handles mnuview.DropDownOpening
    mnuViewToolbar.Checked = ToolStrip1.Visible
  End Sub

  Private Sub mnx_Opening(sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles mnx.Opening
    If mnxCancel Then
      mnxCancel = False
      e.Cancel = True
    End If

  End Sub

  Private Sub mnxT_Opening(sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles mnxT.Opening

    If iniButtonSize = 0 Then
      mnxTSmallIcons.Checked = True
      mnxTLargeIcons.Checked = False
    Else
      mnxTSmallIcons.Checked = False
      mnxTLargeIcons.Checked = True
    End If
  End Sub

  Public Sub mnuToolsSearch_Click(sender As Object, e As EventArgs) Handles mnuToolsSearch.Click
    Using frm As New frmSearch
      dResult = frm.ShowDialog()
    End Using
  End Sub

  Private Sub mnuViewNext_Click(sender As System.Object, ByVal e As System.EventArgs) Handles mnuViewNext.Click

    Dim i As Integer

    i = mainTabs.SelectedIndex
    i = i + 1
    If i > mainTabs.TabPages.Count - 1 Then i = 0
    mView = getMuddViewer(mainTabs.TabPages(i))
    mView.Activate(sender, e) ' sets mView

  End Sub

  Public Function getNextPath(ByRef i As Integer) As String
    ' for fullscreen, etc

    getNextPath = ""
    If localPics.Count = 0 Then Exit Function
    If i < 0 Then i = localPics.Count - 1
    If i >= localPics.Count Then i = 0
    getNextPath = localPics(i)
  End Function

  Private Sub mainTabs_SelectedIndexChanged(sender As Object, ByVal e As System.EventArgs) Handles mainTabs.SelectedIndexChanged
    If mainTabs.SelectedTab IsNot Nothing Then
      Try
        mView = getMuddViewer(mainTabs.SelectedTab)
        Me.Text = mView.Caption
      Catch ex As Exception
      End Try
    End If
    StatusLine()
  End Sub

  Private Sub mainTabs_Selecting(sender As Object, ByVal e As TabControlCancelEventArgs) Handles mainTabs.Selecting
    ClearProcesses(True)
  End Sub

  Private Sub mView_activated() Handles mView.Activated
    setUndoTools()
    If frmExplore.Visible Then frmExplore.Hide()
    Me.Show()
  End Sub

  Private Sub frmMain_Shown(sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

    Loading = False

    If frmExplore.WindowState <> Me.WindowState Then Me.WindowState = frmExplore.WindowState
    If Me.WindowState = FormWindowState.Normal Then
      If Me.Location <> frmExplore.Location Then Me.Location = frmExplore.Location
      If Me.Size <> frmExplore.Size Then Me.Size = frmExplore.Size
    End If

    If loadFile = "" AndAlso iniShowTips Then
      Using frm As New frmTips
        dResult = frm.ShowDialog()
      End Using
    End If

    Me.Cursor = Cursors.Default

    If mView IsNot Nothing Then
      frmExplore.Hide()
    Else
      frmExplore.Show()
      Me.Hide()
    End If

  End Sub

  Sub setUndoTools()

    Dim b As ToolStripItem
    Dim foundOne As Boolean

    For Each b In ToolStrip1.Items
      If b.Tag = "mnueditundo" Then
        If mView.nUndo >= 1 Then b.Enabled = True Else b.Enabled = False
        If foundOne Then Exit For
        foundOne = True
      ElseIf b.Tag = "mnueditredo" Then
        If mView.nRedo > mView.nUndo Then b.Enabled = True Else b.Enabled = False
        If foundOne Then Exit For
        foundOne = True
      End If
    Next b

  End Sub

  Private Sub frmMain_Resize(sender As Object, ByVal e As System.EventArgs) _
    Handles Me.Resize, Me.ResizeEnd
    ' resize catches maximize and normal windowstates, and resizeend catches movement.

    Static busy As Boolean

    If busy Or Form.ActiveForm IsNot Me Then Exit Sub
    If Not Loading Then
      busy = True
      setiniWindowsize(Me)
      busy = False
    End If

  End Sub

  Private Sub frmMain_FormClosing(sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
    ' the tabs should be closed, unless one cancelled.

    Static Dim busy As Boolean = False
    If busy Then Exit Sub
    busy = True

    Me.Cursor = Cursors.WaitCursor

    For i As Integer = 1 To mViews.Count ' "while mView isnot nothing" may loop forever?
      abortClosing = closeTab() ' close mView after asking to save modified photo
      If abortClosing Then
        e.Cancel = True
        Me.Cursor = Cursors.Default
        busy = False
        Exit Sub
      End If
    Next i


    frmExplore.Close()

    timerRedraw.Stop()
    timerAnimate.Stop()
    timerBright.Stop()
    timerFeather.Stop()

    ClearAllUndo()
    Me.Cursor = Cursors.Default

    If combineRview IsNot Nothing Then combineRview.Dispose() : combineRview = Nothing
    clearBitmap(qImage)

    saveini()

    busy = False

  End Sub

End Class
