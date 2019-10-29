'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports System.Math

Public Class frmPicturize2
  Inherits Form

  Dim MainSize(2) As Integer

  Private Sub cmdHelp_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdHelp.Click
    Try
      Help.ShowHelp(Me, helpFile, HelpNavigator.Topic, "photomosaic" & ".html") ' same help for three dialogs
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

  Private Sub frmPicturize2_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, "photomosaic.html")

    chkColorAdjust.Checked = pczColorAdjust
    chkUsePicsOnce.Checked = pczUsePicsOnce

    If pczCellRes(0) <= 0 Then pczCellRes(0) = 48
    If pczCellRes(1) <= 0 Then pczCellRes(1) = 36
    txCellsizeH.Text = CStr(pczCellRes(0))
    txCellsizeV.Text = CStr(pczCellRes(1))

    MainSize(0) = frmMain.mView.Bitmap.Width
    MainSize(1) = frmMain.mView.Bitmap.Height
    If pczNPic(0) <= 0 Then pczNPic(0) = CInt(Round(2 * MainSize(0) / pczCellRes(0)))
    If pczNPic(1) <= 0 Then pczNPic(1) = CInt(Round(2 * MainSize(1) / pczCellRes(1)))
    txnPicH.Text = CStr(pczNPic(0))
    txnPicV.Text = CStr(pczNPic(1))

    lbMain.Text = "Resulting Image Size (Pixels):  " & CDbl(txCellsizeH.Text) * CDbl(txnPicH.Text) & " x " & CDbl(txCellsizeV.Text) * CDbl(txnPicV.Text)

  End Sub

  Private Sub txBox_Enter(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles txnPicH.Enter, txnPicV.Enter, txCellsizeH.Enter, txCellsizeV.Enter

    Dim txBox As TextBox = Sender

    txBox.SelectionStart = 0
    txBox.SelectionLength = Len(txBox.Text)
  End Sub

  Private Sub txtCellSize_Leave(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles txCellsizeH.Leave, txCellsizeV.Leave

    Dim txCellSize As TextBox
    Dim x As Double
    Dim max As Double

    If Me.ActiveControl Is cmdCancel Then Exit Sub
    txCellSize = Sender

    If Sender Is txCellsizeH Then max = MainSize(0) Else max = MainSize(1)

    If Not checknumber(txCellSize.Text, 1, max, x) Then
      MsgBox("Invalid size.")
      txCellSize.select()
      Exit Sub
    End If

    x = Round(x)

    txCellSize.Text = CStr(Round(x))
    If Sender Is txCellsizeH Then
      txCellsizeV.Text = CStr(Round(x * pczCellAspect))
    Else
      txCellsizeH.Text = CStr(Round(x / pczCellAspect))
    End If

    lbMain.Text = "Resulting Image Size (Pixels):  " & CDbl(txCellsizeH.Text) * CDbl(txnPicH.Text) & " x " & CDbl(txCellsizeV.Text) * CDbl(txnPicV.Text)

  End Sub
  Private Sub txtnPic_Leave(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles txnPicH.Leave, txnPicV.Leave

    Dim x As Double
    Dim max As Double
    Dim cellAspect As Double
    Dim txNpic As TextBox

    If Me.ActiveControl Is cmdCancel Then Exit Sub
    txNpic = Sender

    If Sender Is txnPicH Then max = MainSize(0) / 2 Else max = MainSize(1) / 2

    If Not checknumber(txNpic.Text, 1, max, x) Then
      MsgBox("Invalid size.")
      txNpic.select()
      Exit Sub
    End If

    x = Round(x)
    cellAspect = CDbl(txCellsizeV.Text) / CDbl(txCellsizeH.Text)
    If Sender Is txnPicH Then
      txnPicH.Text = CStr(Round(x))
      txnPicV.Text = CStr(Round(x * (MainSize(1) / MainSize(0)) / cellAspect))
    Else
      txnPicV.Text = CStr(Round(x))
      txnPicH.Text = CStr(Round(x * (MainSize(0) / MainSize(1)) * cellAspect))
    End If

    lbMain.Text = "Resulting Image Size (Pixels):  " & CDbl(txCellsizeH.Text) * CDbl(txnPicH.Text) & " x " & CDbl(txCellsizeV.Text) * CDbl(txnPicV.Text)

  End Sub

  Private Sub cmdNav_Click(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles cmdPrevious.Click, cmdNext.Click, cmdCancel.Click

    Dim dResult As DialogResult

    If Sender Is cmdPrevious Then
      pczRetc = 0
    ElseIf Sender Is cmdNext Then
      pczRetc = 1
    ElseIf Sender Is cmdCancel Then
      pczRetc = 2
    End If

    Select Case pczRetc

      Case 0 ' previous
        dresult = VerifyData()
        If dResult <> DialogResult.OK Then Exit Sub
        Me.DialogResult = DialogResult.OK
        Me.Close()

      Case 2 ' cancel
        Me.DialogResult = DialogResult.Cancel
        Me.Close()

      Case 1, 3 ' next, finish
        dResult = VerifyData()
        If dResult <> DialogResult.OK Then Exit Sub
        Me.DialogResult = DialogResult.OK
        Me.Close()

    End Select

  End Sub

  Function VerifyData() As DialogResult

    txtCellSize_Leave(txCellsizeH, New EventArgs())
    If Me.ActiveControl.ToString() = txCellsizeH.Text Then
      VerifyData = DialogResult.Cancel
      Exit Function
    End If
    txtCellSize_Leave(txCellsizeV, New EventArgs())
    If Me.ActiveControl.ToString() = txCellsizeV.Text Then
      VerifyData = DialogResult.Cancel
      Exit Function
    End If

    txtnPic_Leave(txnPicH, New EventArgs())
    If Me.ActiveControl.ToString() = txnPicH.Text Then
      VerifyData = DialogResult.Cancel
      Exit Function
    End If
    txtnPic_Leave(txnPicV, New EventArgs())

    If Me.ActiveControl.ToString() = txnPicV.Text Then
      VerifyData = DialogResult.Cancel
      Exit Function
    End If

    pczCellRes(0) = CInt(txCellsizeH.Text)
    pczCellRes(1) = CInt(txCellsizeV.Text)
    pczNPic(0) = CInt(txnPicH.Text)
    pczNPic(1) = CInt(txnPicV.Text)
    pczCellDiv(0) = 6 ' removed option 7/10/15
    pczCellDiv(1) = CInt(Round(pczCellDiv(0) * pczCellAspect))
    If pczCellDiv(1) <= 0 Then pczCellDiv(1) = 1
    pczColorAdjust = chkColorAdjust.Checked
    pczUsePicsOnce = chkUsePicsOnce.Checked

    VerifyData = DialogResult.OK

  End Function

  Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

  End Sub
End Class