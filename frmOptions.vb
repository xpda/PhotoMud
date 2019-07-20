Imports vb = Microsoft.VisualBasic
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports System.IO

Public Class frmOptions
  Inherits Form

  Dim FontName As String
  Dim FontSize As Double
  Dim FontBold As Boolean
  Dim FontItalic As Boolean
  Dim FontUnderline As Boolean
  Dim helpTopic As String
  Dim Loading As Boolean = True

  Private Sub cmdCancel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
    Me.DialogResult = DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub cmdHelp_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdHelp.Click

    Try
      Help.ShowHelp(Me, helpFile, HelpNavigator.Topic, helpTopic & ".html")
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

  End Sub

  Private Sub cmdReset_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdReset.Click
    setDefaults()
    InitForm()
  End Sub

  Private Sub cmdOK_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdOK.Click
    assignValues()
    Me.DialogResult = DialogResult.OK
    Me.Close()
  End Sub

  Sub assignValues()
    ' save everything to ini variables

    ' these are saved in the separate forms:
    '   cmdEditKeywords
    '   cmdFileAssoc
    '   cmdToolbar

    iniDateinCommentCommand = chkDateInCommentCommand.Checked
    iniMultiTagPath = chkMultiTagPath.Checked
    iniMultiUndo = chkMultiUndo.Checked
    iniDisableUndo = chkDisableUndo.Checked
    iniPngIndexed = chkPNGIndexed.Checked
    iniShowTips = chkShowTips.Checked
    iniSlideOrder = "0"
    If optSlideOrderRandom.Checked Then iniSlideOrder = "random"
    If optSlideOrderFilename.Checked Then iniSlideOrder = "filename"
    If optSlideOrderPhotoDate.Checked Then iniSlideOrder = "photodate"
    If optSlideOrderFileDate.Checked Then iniSlideOrder = "filedate"
    If optSlideOrderNone.Checked Then iniSlideOrder = "none"
    iniSlideShowName = chkSlideShowName.Checked
    iniSlideShowDescription = chkSlideShowDescription.Checked
    iniSlideShowPhotoDate = chkSlideshowPhotoDate.Checked
    iniViewToolbar = chkViewToolbar.Checked
    iniWebConvertUTCtoLocal = CInt(chkWebConvertUTCtoLocal.Checked)
    iniZoomOne = chkZoomOne.Checked
    iniDelRawFiles = chkDelRawFiles.Checked
    iniAdjustIndColor = chkAdjustIndColor.Checked
    iniAdjustIndintensity = chkAdjustIndIntensity.Checked
    iniAdjustPreserveIntensity = chkAdjustPreserveIntensity.Checked

    iniButtonSize = cmbButtonSize.SelectedIndex
    iniViewStyle = cmbViewStyle.SelectedIndex

    iniWebBackColor = cmdWebBackColor.BackColor
    iniWebForeColor = cmdWebForeColor.BackColor

    iniJpgQuality = nmJpgQuality.Value
    iniColorTolerance = nmColorTolerance.Value
    iniSendJPGQuality = nmSendJPGQuality.Value
    iniSlideRate = nmSlideRate.Value
    iniSlideFadeTime = nmSlideFadeTime.Value
    iniThumbX = nmThumbX.Value
    iniThumbY = nmThumbY.Value

    If chkNormalCaptionSize.Checked Then iniWebCaptionSize = 0 Else iniWebCaptionSize = nmWebCaptionSize.Value ' 0 = normal
    iniWebCaptionAlign = cmbWebCaptionAlign.Text
    iniWebCellPadding = nmWebCellPadding.Value
    'iniWebCellSpacing = nmWebCellSpacing.Value
    iniWebFont = txWebFont.Text
    iniWebGoogleAnalytics = txWebGoogleAnalytics.Text
    iniWebGoogleEvents = chkWebGoogleEvents.Checked
    'iniWebnColumns = nmWebnColumns.Value
    If chkWebTarget.Checked Then iniWebTarget = 1 Else iniWebTarget = 0
    iniWebTableBorder = nmWebTableBorder.Value
    iniWebThumbX = nmWebThumbX.Value
    iniWebThumbY = nmWebThumbY.Value
    iniWebShadowSize = nmWebShadowSize.Value
    iniWebTitleSize = nmWebTitleSize.Value
    If chkNormalTitleSize.Checked Then iniWebTitleSize = 0 Else iniWebTitleSize = nmWebTitleSize.Value ' 0 = normal

    iniFontName = FontName
    iniFontSize = FontSize
    iniFontBold = FontBold
    iniFontitalic = FontItalic
    iniFontUnderline = FontUnderline

    iniDBhost = txDBhost.Text
    iniDBdatabase = txDBdatabase.Text
    iniDBuser = txDBuser.Text
    iniDBpassword = txDBpassword.Text
    iniDBConnStr = getConnStr(iniDBhost, iniDBdatabase, iniDBuser, iniDBpassword)
    iniBugPath = txBugPath.Text
    If iniBugPath.EndsWith("\") Then iniBugPath = Mid(iniBugPath, 1, Len(iniBugPath) - 1)

    iniEmailAccount = txEmailAccount.Text
    iniEmailPassword = txEmailPassword.Text
    iniEmailHost = txEmailhost.Text
    iniEmailPort = txEmailPort.Text


  End Sub


  Private Sub frmOptions_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Loading = True

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    nmWebThumbX.Accelerations.Add(New NumericUpDownAcceleration(2, 7))
    nmWebThumbY.Accelerations.Add(New NumericUpDownAcceleration(2, 7))

    cmbButtonSize.Items.Add("Medium")
    cmbButtonSize.Items.Add("Large")
    If iniButtonSize >= 0 Or iniButtonSize <= cmbButtonSize.Items.Count Then cmbButtonSize.SelectedIndex = iniButtonSize

    cmbViewStyle.Items.Add("Thumbnails")
    cmbViewStyle.Items.Add("Details")
    If iniViewStyle >= 0 Or iniViewStyle <= cmbViewStyle.Items.Count Then cmbViewStyle.SelectedIndex = iniViewStyle

    InitForm()
    If OptionTab IsNot Nothing Then
      Try
        tab1.SelectTab(OptionTab)
      Catch
      End Try
    End If

    Loading = False

  End Sub

  Sub InitForm()
    ' load everything from the ini variables to the form

    chkDateInCommentCommand.Checked = iniDateinCommentCommand
    chkMultiTagPath.Checked = iniMultiTagPath
    chkMultiUndo.Checked = iniMultiUndo
    chkDisableUndo.Checked = iniDisableUndo
    chkPNGIndexed.Checked = iniPngIndexed
    chkShowTips.Checked = iniShowTips

    optSlideOrderNone.Checked = False
    optSlideOrderFilename.Checked = False
    optSlideOrderPhotoDate.Checked = False
    optSlideOrderFileDate.Checked = False
    optSlideOrderRandom.Checked = False
    Select Case iniSlideOrder
      Case "none" : optSlideOrderNone.Checked = True
      Case "filename" : optSlideOrderFilename.Checked = True
      Case "photodate" : optSlideOrderPhotoDate.Checked = True
      Case "filedate" : optSlideOrderFileDate.Checked = True
      Case "random" : optSlideOrderRandom.Checked = True
    End Select
    chkSlideShowName.Checked = iniSlideShowName
    chkSlideShowDescription.Checked = iniSlideShowDescription
    chkSlideshowPhotoDate.Checked = iniSlideShowPhotoDate
    chkViewToolbar.Checked = iniViewToolbar
    chkWebConvertUTCtoLocal.Checked = CBool(iniWebConvertUTCtoLocal)
    chkZoomOne.Checked = iniZoomOne
    chkDelRawFiles.Checked = iniDelRawFiles
    chkAdjustIndColor.Checked = iniAdjustIndColor
    chkAdjustIndIntensity.Checked = iniAdjustIndintensity
    chkAdjustPreserveIntensity.Checked = iniAdjustPreserveIntensity

    If iniButtonSize >= 0 And iniButtonSize <= cmbButtonSize.Items.Count - 1 Then
      cmbButtonSize.SelectedIndex = iniButtonSize
    Else
      cmbButtonSize.SelectedIndex = 0 ' default medium
    End If

    If iniViewStyle >= 0 And iniViewStyle <= cmbViewStyle.Items.Count - 1 Then
      cmbViewStyle.SelectedIndex = iniViewStyle
    Else
      cmbViewStyle.SelectedIndex = 0 ' default  thumbnails
    End If

    cmdWebBackColor.BackColor = iniWebBackColor
    txWebBackcolor.Text = ColorTranslator.ToHtml(cmdWebBackColor.BackColor)
    cmdWebForeColor.BackColor = iniWebForeColor
    txWebForecolor.Text = ColorTranslator.ToHtml(cmdWebForeColor.BackColor)

    If iniJpgQuality >= nmJpgQuality.Minimum And iniJpgQuality <= nmJpgQuality.Maximum Then nmJpgQuality.Value = iniJpgQuality
    If iniColorTolerance >= nmColorTolerance.Minimum And iniColorTolerance <= nmColorTolerance.Maximum Then nmColorTolerance.Value = iniColorTolerance
    If iniSendJPGQuality >= nmSendJPGQuality.Minimum And iniSendJPGQuality <= nmSendJPGQuality.Maximum Then nmSendJPGQuality.Value = iniSendJPGQuality
    If iniSlideRate >= nmSlideRate.Minimum And iniSlideRate <= nmSlideRate.Maximum Then nmSlideRate.Value = iniSlideRate
    If iniSlideFadeTime >= nmSlideFadeTime.Minimum And iniSlideFadeTime <= nmSlideFadeTime.Maximum Then nmSlideFadeTime.Value = iniSlideFadeTime
    If iniThumbX >= nmThumbX.Minimum And iniThumbX <= nmThumbX.Maximum Then nmThumbX.Value = iniThumbX
    If iniThumbY >= nmThumbY.Minimum And iniThumbY <= nmThumbY.Maximum Then nmThumbY.Value = iniThumbY
    If iniWebCaptionSize > 0 Then
      If iniWebCaptionSize >= nmWebCaptionSize.Minimum And iniWebCaptionSize <= nmWebCaptionSize.Maximum Then nmWebCaptionSize.Value = iniWebCaptionSize
      chkNormalCaptionSize.Checked = False
    Else
      chkNormalCaptionSize.Checked = True
      nmWebCaptionSize.Enabled = False
    End If
    If iniWebCaptionAlign <> "" Then
      cmbWebCaptionAlign.Text = iniWebCaptionAlign
    Else
      cmbWebCaptionAlign.SelectedIndex = 0
    End If
    If iniWebCellPadding >= nmWebCellPadding.Minimum And iniWebCellPadding <= nmWebCellPadding.Maximum Then nmWebCellPadding.Value = iniWebCellPadding
    'If iniWebCellSpacing >= nmWebCellSpacing.Minimum And iniWebCellSpacing <= nmWebCellSpacing.Maximum Then nmWebCellSpacing.Value = iniWebCellSpacing
    'If iniWebnColumns >= nmWebnColumns.Minimum And iniWebnColumns <= nmWebnColumns.Maximum Then nmWebnColumns.Value = iniWebnColumns
    If iniWebTarget = 0 Then chkWebTarget.Checked = False Else chkWebTarget.Checked = True
    If iniWebTableBorder >= nmWebTableBorder.Minimum And iniWebTableBorder <= nmWebTableBorder.Maximum Then nmWebTableBorder.Value = iniWebTableBorder
    If iniWebThumbX >= nmWebThumbX.Minimum And iniWebThumbX <= nmWebThumbX.Maximum Then nmWebThumbX.Value = iniWebThumbX
    If iniWebThumbY >= nmWebThumbY.Minimum And iniWebThumbY <= nmWebThumbY.Maximum Then nmWebThumbY.Value = iniWebThumbY
    If iniWebShadowSize >= nmWebShadowSize.Minimum And iniWebShadowSize <= nmWebShadowSize.Maximum Then nmWebShadowSize.Value = iniWebShadowSize
    If iniWebTitleSize > 0 Then
      If iniWebTitleSize >= nmWebTitleSize.Minimum And iniWebTitleSize <= nmWebTitleSize.Maximum Then nmWebTitleSize.Value = iniWebTitleSize
      chkNormalTitleSize.Checked = False
    Else
      chkNormalTitleSize.Checked = True
      nmWebTitleSize.Enabled = False
    End If

    txWebFont.Text = iniWebFont
    txWebGoogleAnalytics.Text = iniWebGoogleAnalytics
    chkWebGoogleEvents.Checked = iniWebGoogleEvents

    FontName = iniFontName
    FontSize = iniFontSize
    FontBold = iniFontBold
    FontItalic = iniFontitalic
    FontUnderline = iniFontUnderline

    txDBhost.Text = iniDBhost
    txDBdatabase.Text = iniDBdatabase
    txDBuser.Text = iniDBuser
    txDBpassword.Text = iniDBpassword
    txBugPath.Text = iniBugPath

    txEmailAccount.Text = iniEmailAccount
    txEmailPassword.Text = iniEmailPassword
    txEmailHost.Text = iniEmailHost
    txEmailPort.Text = iniEmailPort

  End Sub

  Private Sub cmdToolbar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmdToolbar.Click
    Using frm As New frmToolbar
      frm.ShowDialog()
    End Using
  End Sub

  Private Sub cmdFileAssoc_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmdFileAssoc.Click
    Using frm As New frmFileAssoc
      frm.ShowDialog()
    End Using
  End Sub

  Private Sub chkPNGIndexed_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkPNGIndexed.CheckedChanged

    If Loading Then Exit Sub

  End Sub

  Private Sub cmdWebForeColor_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmdWebForeColor.Click
    cmdWebForeColor.BackColor = getColor(cmdWebForeColor.BackColor, colorDialog1)
    txWebForecolor.Text = ColorTranslator.ToHtml(cmdWebForeColor.BackColor)
  End Sub

  Private Sub cmdWebBackColor_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmdWebBackColor.Click
    cmdWebBackColor.BackColor = getColor(cmdWebBackColor.BackColor, colorDialog1)
    txWebBackcolor.Text = ColorTranslator.ToHtml(cmdWebBackColor.BackColor)
  End Sub

  Private Sub chkNormalCaptionSize_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkNormalCaptionSize.CheckedChanged

    If Loading Then Exit Sub

    If chkNormalCaptionSize.Checked Then
      nmWebCaptionSize.Enabled = False
    Else
      nmWebCaptionSize.Enabled = True
    End If
  End Sub

  Private Sub chkNormalTitleSize_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkNormalTitleSize.CheckedChanged

    If Loading Then Exit Sub

    If chkNormalTitleSize.Checked Then
      nmWebTitleSize.Enabled = False
    Else
      nmWebTitleSize.Enabled = True
    End If
  End Sub

  Private Sub tab1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tab1.SelectedIndexChanged

    If tab1.SelectedTab Is tabGeneral Then
      helpTopic = "generaloptions"
    ElseIf tab1.SelectedTab Is tabEditing Then
      helpTopic = "editingoptions"
    ElseIf tab1.SelectedTab Is tabSlideShow Then
      helpTopic = "slideshowoptions"
    ElseIf tab1.SelectedTab Is tabFile Then
      helpTopic = "fileandcompressionoptions"
    ElseIf tab1.SelectedTab Is tabExplore Then
      helpTopic = "exploreroptions"
    ElseIf tab1.SelectedTab Is tabWebPage Then
      helpTopic = "webpageoptions"
    End If

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, helpTopic & ".html")

  End Sub

  Private Sub txWebForecolor_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txWebForecolor.Leave
    Try
      cmdWebForeColor.BackColor = ColorTranslator.FromHtml(txWebForecolor.Text)
    Catch ex As Exception
      txWebForecolor.Text = ColorTranslator.ToHtml(cmdWebForeColor.BackColor)
    End Try
  End Sub

  Private Sub txWebbackcolor_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txWebBackcolor.Leave
    Try
      cmdWebBackColor.BackColor = ColorTranslator.FromHtml(txWebBackcolor.Text)
    Catch ex As Exception
      txWebBackcolor.Text = ColorTranslator.ToHtml(cmdWebBackColor.BackColor)
    End Try
  End Sub

  Private Sub cmdTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTest.Click

    Dim connStr As String

    connStr = getConnStr(txDBhost.Text, txDBdatabase.Text, txDBuser.Text, txDBpassword.Text)

    If checkDB(connStr) Then
      MsgBox("Database connection was successful")
    Else
      MsgBox("Database connection was unsuccessful")
    End If

  End Sub

  Private Sub cmdEmailTest_Click(sender As Object, e As EventArgs) Handles cmdEmailTest.Click

    Dim msg As String
    msg = sendMail(txEmailAccount.Text, txEmailAccount.Text, txEmailHost.Text, txEmailPort.Text,
                   txEmailAccount.Text, txEmailPassword.Text,
                   "This is a test email.", "Photo Mud Email Test", "Photo Mud", Nothing)
    If msg = "" Then
      MsgBox("Email connection was successful")
    Else
      MsgBox("Email connection was unsuccessful. " & vbCrLf & msg)
    End If

  End Sub
End Class