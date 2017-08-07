Imports System.Drawing.Text

Public Class ctlTextControl

  Dim fName As String = False
  Dim fBold As Boolean = False
  Dim fItalic As Boolean = False
  Dim fUnderline As Boolean = False
  Dim fSize As Double = 150
  Dim tAngle As Double = 0
  Dim tMultiline As Boolean = False
  Dim tAlign As String = "left"
  Dim tBackfill As Boolean = False
  Dim tforeColor As Color = Color.Black
  Dim tbackColor As Color = Color.White

  Dim sDateTime As String = ""
  Dim sDescription As String = ""
  Dim tString As String = ""
  Dim loading As Boolean = True
  Dim processing As Boolean = False

  Dim clickPoint As Point

  Event Changed(sender As Object, e As EventArgs)
  Event OK(sender As Object, e As EventArgs)
  Event Cancel(sender As Object, e As EventArgs)

  WriteOnly Property DateTime() As String

    Set(ByVal value As String)
      sDateTime = value
      If Trim(sDateTime) = "" Then cmdDate.Enabled = False Else cmdDate.Enabled = True
    End Set

  End Property

  WriteOnly Property Description() As String

    Set(ByVal value As String)
      sDescription = value
      If Trim(sDescription) = "" Then cmdDescription.Enabled = False Else cmdDescription.Enabled = True
    End Set

  End Property

  Property textAngle() As Double

    Get
      textAngle = tAngle
    End Get

    Set(ByVal value As Double)
      processing = True
      If value >= nmTextAngle.Minimum And value <= nmTextAngle.Maximum Then
        tAngle = value
        nmTextAngle.Value = tAngle
      End If
      processing = False
    End Set

  End Property
  Property fontSize() As Integer

    Get
      fontSize = fSize
    End Get

    Set(ByVal value As Integer)
      processing = True
      If value >= nmFontSize.Minimum And value <= nmFontSize.Maximum Then
        fSize = value
        nmFontSize.Value = fSize
      End If
      processing = False
    End Set

  End Property

  Property textColor() As Color

    Get
      textColor = tforeColor
    End Get

    Set(ByVal value As Color)
      tforeColor = value
      cmdTextColor.BackColor = tforeColor
    End Set

  End Property

  Property textBackgroundColor() As Color

    Get
      textBackgroundColor = tbackColor
    End Get

    Set(ByVal value As Color)
      tbackColor = value
      cmdTextBackcolor.BackColor = tbackColor
    End Set

  End Property

  Property BackFill() As Boolean

    Get
      BackFill = tBackfill
    End Get

    Set(ByVal value As Boolean)
      processing = True
      tBackfill = value
      chkTextBackfill.Checked = tBackfill
      processing = False
    End Set

  End Property

  Property Multiline() As Boolean

    Get
      Multiline = tMultiline
    End Get

    Set(ByVal value As Boolean)
      processing = True
      tMultiline = value
      chkTextMultiline.Checked = tMultiline
      txText.Multiline = value
      cmdAlignLeft.Visible = tMultiline
      cmdAlignCenter.Visible = tMultiline
      cmdAlignRight.Visible = tMultiline
      setAlign()
      processing = False
    End Set

  End Property

  Property Alignment() As Boolean

    Get
      Alignment = tAlign
    End Get

    Set(ByVal value As Boolean)
      processing = True
      tAlign = value
      setAlign()
      processing = False
    End Set

  End Property

  Property Bold() As Boolean

    Get
      Bold = fBold
    End Get

    Set(ByVal value As Boolean)
      fBold = value
      If fBold Then ' toggle button appearance
        cmdBold.BackColor = Color.Khaki
      Else
        cmdBold.BackColor = SystemColors.ButtonFace
      End If
    End Set

  End Property

  Property Italic() As Boolean

    Get
      Italic = fItalic
    End Get

    Set(ByVal value As Boolean)
      fItalic = value
      If fItalic Then ' toggle button appearance
        cmdItalic.BackColor = color.Khaki
      Else
        cmdItalic.BackColor = SystemColors.ButtonFace
      End If
    End Set

  End Property

  Property Underline() As Boolean

    Get
      Underline = fUnderline
    End Get

    Set(ByVal value As Boolean)
      fUnderline = value
      If fUnderline Then ' toggle button appearance
        cmdUnderline.BackColor = color.Khaki
      Else
        cmdUnderline.BackColor = SystemColors.ButtonFace
      End If
    End Set

  End Property

  Property fontName() As String

    Get
      fontName = fName
    End Get

    Set(ByVal value As String)
      processing = True
      fName = value
      cmbFonts.SelectedText = fName
      processing = False
    End Set

  End Property

  Property textString() As String

    Get
      textString = tString
    End Get

    Set(ByVal value As String)
      processing = True
      tString = value
      txText.Text = tString
      processing = False
    End Set

  End Property

  Private Sub cmdBold_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdBold.Click
    fBold = Not fBold
    If fBold Then ' toggle button appearance
      cmdBold.BackColor = Color.Khaki
    Else
      cmdBold.BackColor = SystemColors.ButtonFace
    End If
    RaiseEvent Changed(Sender, e)
  End Sub

  Private Sub cmdItalic_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdItalic.Click
    fItalic = Not fItalic
    If fItalic Then ' toggle button appearance
      cmdItalic.BackColor = color.Khaki
    Else
      cmdItalic.BackColor = SystemColors.ButtonFace
    End If
    RaiseEvent Changed(Sender, e)
  End Sub

  Private Sub cmdUnderline_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdUnderline.Click
    fUnderline = Not fUnderline
    If fUnderline Then ' toggle button appearance
      cmdUnderline.BackColor = color.Khaki
    Else
      cmdUnderline.BackColor = SystemColors.ButtonFace
    End If
    RaiseEvent Changed(Sender, e)
  End Sub

  Private Sub cmdTextColor_Click(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles cmdTextColor.Click, cmdTextBackcolor.Click

    Dim cmd As Button
    Dim result As DialogResult

    cmd = Sender

    colorDialog1.Color = cmd.BackColor
    Try
      result = colorDialog1.ShowDialog()
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    If result = DialogResult.OK Then
      cmd.BackColor = colorDialog1.Color
    End If

    If Sender Is cmdTextColor Then
      tforeColor = cmd.BackColor
    ElseIf Sender Is cmdTextBackcolor Then
      tbackColor = cmd.BackColor
    End If

    RaiseEvent Changed(Sender, e)

  End Sub

  Private Sub cmdDate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdDate.Click

    Dim s As String = ""
    Dim d As Date

    s = sDateTime
    If Len(s) = 19 Then
      Mid(s, 5, 1) = "/"
      Mid(s, 8, 1) = "/"
      Try
        d = CDate(s)
        s = Format(d, "short date") & " " & Format(d, "long time")
      Catch
        s = ""
      End Try
    End If

    If Len(s) > 0 Then
      If txText.Text <> "" Then
        If Len(s) > 0 Then txText.Text = txText.Text & " " & s
      Else
        txText.Text = s
      End If
    End If

    RaiseEvent Changed(sender, e)

  End Sub

  Private Sub cmdDescription_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdDescription.Click

    Dim s As String

    s = sDescription
    If Len(s) > 0 Then
      If txText.Text <> "" Then
        If Len(s) > 0 Then txText.Text = txText.Text & " " & s
      Else
        txText.Text = s
      End If
    End If

    RaiseEvent Changed(Sender, e)

  End Sub

  Private Sub txText_TextChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles txText.TextChanged
    If processing Or loading Then Exit Sub
    tString = txText.Text
    RaiseEvent Changed(Sender, e)

  End Sub

  Private Sub nmfontSize_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles nmFontSize.ValueChanged
    If loading Or processing Then Exit Sub
    fSize = nmFontSize.Value
    RaiseEvent Changed(sender, e)
  End Sub


  Private Sub nmtextangle_valuechanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles nmTextAngle.ValueChanged
    If loading Or processing Then Exit Sub
    tAngle = nmTextAngle.Value
    RaiseEvent Changed(sender, e)
  End Sub

  Private Sub Combo1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmbFonts.SelectedIndexChanged
    If processing Or loading Then Exit Sub
    fName = cmbFonts.SelectedItem
    RaiseEvent Changed(sender, e)
  End Sub

  Private Sub chkbackfill_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTextBackfill.CheckedChanged
    If loading Or processing Then Exit Sub
    tBackfill = chkTextBackfill.Checked
    RaiseEvent Changed(sender, e)
  End Sub

  Private Sub chkmultiline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTextMultiline.CheckedChanged
    If loading Or processing Then Exit Sub
    tMultiline = chkTextMultiline.Checked
    txText.Multiline = tMultiline
    cmdAlignLeft.Visible = tMultiline
    cmdAlignCenter.Visible = tMultiline
    cmdAlignRight.Visible = tMultiline
    RaiseEvent Changed(sender, e)
  End Sub

  'Private Sub chkTextFixedSize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTextFixedSize.CheckedChanged
  'If Loading Or Processing Then Exit Sub
  'textFixedSize = chkTextFixedSize.Checked
  'Call SetText()
  'End Sub

  Private Sub ctlText_OK(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmdOK.Click
    RaiseEvent OK(sender, e)
  End Sub

  Private Sub ctlText_Cancel(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmdCancel.Click
    RaiseEvent Cancel(sender, e)
  End Sub

  Private Sub ctlTextControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    Dim installedFonts As New InstalledFontCollection
    Dim fam As FontFamily

    For Each fam In installedFonts.Families
      cmbFonts.Items.Add(fam.Name)
      If eqstr(fam.Name, iniFontName) Then cmbFonts.SelectedIndex = cmbFonts.Items.Count - 1
    Next fam

    loading = False
  End Sub

  Private Sub ctlText_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
    If e.Button = MouseButtons.Left Then
      clickPoint = e.Location
      Me.Cursor = Cursors.SizeAll
    End If
  End Sub
  Private Sub ctlText_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
    If e.Button = System.Windows.Forms.MouseButtons.Left Then
      Me.Location = Me.Location + e.Location - clickPoint
    End If
  End Sub
  Private Sub ctlText_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
    If e.Button = System.Windows.Forms.MouseButtons.Left Then
      Me.Location = Me.Location + e.Location - clickPoint
    End If
    Me.Cursor = Cursors.Default
  End Sub

  Public Sub New()
    ' This is required by the Windows Form Designer.
    InitializeComponent()
    txText.Select()
  End Sub

  Private Sub ctlTextControl_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
    If Me.Visible Then txText.Select()
  End Sub

  Sub setAlign()

    Select Case tAlign
      Case "left"
        cmdAlignLeft.BackColor = color.Khaki
        cmdAlignCenter.BackColor = SystemColors.ButtonFace
        cmdAlignRight.BackColor = SystemColors.ButtonFace
        txText.TextAlign = HorizontalAlignment.Left
      Case "center"
        cmdAlignLeft.BackColor = SystemColors.ButtonFace
        cmdAlignCenter.BackColor = color.Khaki
        cmdAlignRight.BackColor = SystemColors.ButtonFace
        txText.TextAlign = HorizontalAlignment.Center
      Case "right"
        cmdAlignLeft.BackColor = SystemColors.ButtonFace
        cmdAlignCenter.BackColor = SystemColors.ButtonFace
        cmdAlignRight.BackColor = color.Khaki
        txText.TextAlign = HorizontalAlignment.Right
    End Select

  End Sub

  Private Sub cmdAlign_Click(sender As Object, e As EventArgs) _
    Handles cmdAlignLeft.Click, cmdAlignCenter.Click, cmdAlignRight.Click

    If sender Is cmdAlignLeft Then
      tAlign = "left"
    ElseIf sender Is cmdAlignCenter Then
      tAlign = "center"
    ElseIf sender Is cmdAlignRight Then
      tAlign = "right"
    End If

    setAlign()
  End Sub
End Class
