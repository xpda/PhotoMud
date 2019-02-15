Imports vb = Microsoft.VisualBasic
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math

Public Class frmResize
  Inherits Form
  Dim originalSize(1) As Double
  Dim gSize(1) As Double
  Dim gSizeChanged(1) As Boolean
  Dim Pct(1) As Double
  Dim pctChanged(1) As Boolean
  Dim Loading As Boolean = True

  Dim tMaxRes As Double ' in case maxres is smaller than the image

  Private Sub chkAspect_CheckedChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles chkAspect.CheckedChanged
    If Loading Then Exit Sub

    If chkAspect.Checked Then
      gSizeChanged(0) = True
      txSize_Leave(txSizeW, New EventArgs())
    End If

  End Sub

  Private Sub cmdCancel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click

    abort = True
    Me.DialogResult = DialogResult.Cancel
    Me.Close()

  End Sub

  Private Sub cmdHelp_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdHelp.Click
    Try
      Help.ShowHelp(Me, helpFile, HelpNavigator.Topic, Me.Name & ".html")
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

  Private Sub cmdOK_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdOK.Click

    Dim newSize As Size

    txSize_Leave(txSizeW, New EventArgs())
    txSize_Leave(txSizeH, New EventArgs())
    txPct_Leave(txPctW, New EventArgs())
    txPct_Leave(txPctH, New EventArgs())
    txRes_Leave(txHres, New EventArgs())
    txRes_Leave(txVres, New EventArgs())

    newSize.Width = CInt(txSizeW.Text)
    newSize.Height = CInt(txSizeH.Text)

    Me.Cursor = Cursors.WaitCursor
    ' scale borderwidth
    If frmMain.mView.Bitmap.Width <> newSize.Width Or frmMain.mView.Bitmap.Height <> newSize.Height Then
      frmMain.mView.ResizeBitmap(newSize)
      frmMain.mView.Zoom(0)
    End If

    If IsNumeric(txHres.Text) AndAlso IsNumeric(txHres.Text) Then
      frmMain.mView.Bitmap.SetResolution(CSng(txHres.Text), CSng(txVres.Text))
    End If

    Me.Cursor = Cursors.Default
    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Private Sub frmResize_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Loading = True

    originalSize(0) = frmMain.mView.Bitmap.Width
    originalSize(1) = frmMain.mView.Bitmap.Height

    txSizeW.Text = CStr(originalSize(0))
    txSizeH.Text = CStr(originalSize(1))
    txPctW.Text = "100"
    txPctH.Text = "100"
    chkAspect.Checked = True

    tMaxRes = Max(MaxRes, originalSize(0))
    tMaxRes = Max(tMaxRes, originalSize(1))

    txHres.Text = CStr(frmMain.mView.Bitmap.HorizontalResolution)
    txVres.Text = CStr(frmMain.mView.Bitmap.VerticalResolution)
    Loading = False

  End Sub

  Private Sub txPct_TextChanged(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles txPctW.TextChanged, txPctH.TextChanged
    If Loading Then Exit Sub

    If Sender Is txPctW Then pctChanged(0) = True Else pctChanged(1) = True

  End Sub

  Private Sub txPct_Enter(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles txPctW.Enter, txPctH.Enter, txSizeW.Enter, txSizeH.Enter, txHres.Enter, txVres.Enter

    Dim txBox As TextBox
    txBox = Sender

    txBox.SelectionStart = 0
    txBox.SelectionLength = Len(txBox.Text)

  End Sub

  Private Sub txSize_TextChanged(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles txSizeW.TextChanged, txSizeH.TextChanged
    If Loading Then Exit Sub

    If Sender Is txSizeW Then gSizeChanged(0) = True Else gSizeChanged(1) = True

  End Sub


  Private Sub txRes_Leave(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles txHres.Leave, txVres.Leave

    Dim x As Double
    Dim txBox As TextBox

    If Loading Then Exit Sub
    If Me.ActiveControl Is cmdCancel Then Exit Sub

    txBox = Sender
    If Not checknumber(txBox.Text, 1, tMaxRes, x) Then
      MsgBox("Please enter a number from 1 to " & tMaxRes & ".", MsgBoxStyle.OkCancel)
      txBox.select()
      Exit Sub
    End If

  End Sub
  Private Sub txSize_Leave(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles txSizeW.Leave, txSizeH.Leave

    Dim txBox As TextBox
    Dim x As Double
    Dim other As Short
    Dim index As Integer

    If Loading Then Exit Sub
    If Me.ActiveControl Is cmdCancel Then Exit Sub

    txBox = Sender
    If txBox Is txSizeW Then index = 0 Else index = 1

    If Not gSizeChanged(index) Then Exit Sub

    If Not checknumber(txBox.Text, 1, tMaxRes, x) Then
      MsgBox("Please enter a number from 1 to " & tMaxRes & ".")
      txBox.select()
      Exit Sub
    End If

    gSize(index) = x
    Pct(index) = 100 * x / originalSize(index)
    If index = 0 Then txPctW.Text = CStr(Round(Pct(index) * 10) / 10) Else txPctH.Text = CStr(Round(Pct(index) * 10) / 10)
    pctChanged(index) = False
    gSizeChanged(index) = False

    If chkAspect.Checked Then
      If index = 1 Then other = 0 Else other = 1
      gSize(other) = originalSize(other) * x / originalSize(index)
      Pct(other) = Pct(index)
      If other = 0 Then txSizeW.Text = CStr(Round(gSize(other))) Else txSizeH.Text = CStr(Round(gSize(other)))
      If other = 0 Then txPctW.Text = txPctH.Text Else txPctH.Text = txPctW.Text
      gSizeChanged(other) = False
      pctChanged(other) = False
    End If

  End Sub
  Private Sub txPct_Leave(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles txPctW.Leave, txPctH.Leave

    Dim Index As Integer
    Dim x As Double
    Dim xMax As Double
    Dim other As Integer
    Dim txBox As TextBox

    If Loading Then Exit Sub
    If Me.ActiveControl Is cmdCancel Then Exit Sub

    txBox = Sender
    If txBox Is txPctW Then Index = 0 Else Index = 1

    If Not pctChanged(Index) Then Exit Sub

    xMax = tMaxRes / Max(originalSize(0), originalSize(1)) * 100
    If Not checknumber(txBox.Text, 0.01, xMax, x) Then
      MsgBox("Please enter a number from .01 to " & Format(xMax, ",#0") & ".", MsgBoxStyle.OkCancel)
      txBox.select()
      Exit Sub
    End If

    Pct(Index) = x
    gSize(Index) = originalSize(Index) * x / 100
    If Index = 0 Then txSizeW.Text = CStr(Round(gSize(Index))) Else txSizeH.Text = CStr(Round(gSize(Index)))
    pctChanged(Index) = False
    gSizeChanged(Index) = False

    If chkAspect.Checked Then
      If Index = 1 Then other = 0 Else other = 1
      gSize(other) = originalSize(other) * gSize(Index) / originalSize(Index)
      Pct(other) = Pct(Index)
      If Index = 1 Then txSizeW.Text = CStr(Round(gSize(other))) Else txSizeH.Text = CStr(Round(gSize(other)))
      If Index = 1 Then txPctW.Text = txBox.Text Else txPctH.Text = txBox.Text
      gSizeChanged(other) = False
      pctChanged(other) = False
    End If

  End Sub

End Class