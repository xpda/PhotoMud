'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports VB = Microsoft.VisualBasic
Imports System.IO

Public Class frmTips
  Inherits Form
  Dim tips() As String

  Private Sub cmdClose_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdClose.Click
    If chkShowTips.Checked Then
      iniShowTips = True
    Else
      iniShowTips = False
    End If

    Me.DialogResult = DialogResult.OK
    Me.Close()
  End Sub

  Private Sub cmdNext_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdNext.Click
    showtip()
  End Sub

  Private Sub Command1_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles Command1.Click
    iniTipNumber = 1
    showtip()
  End Sub

  Private Sub frmTips_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.TableOfContents)
    helpProvider1.SetHelpKeyword(Me, "")

    If iniShowTips Then chkShowTips.Checked = True Else chkShowTips.Checked = False
    showtip()

  End Sub

  Sub showtip()

    Dim fName As String
    Dim i As Integer

    If tips Is Nothing OrElse UBound(tips) < 0 Then ' read the tips file once per application
      ' read all tips once per form show

      fName = exePath
      If VB.Right(fName, 1) <> "\" Then fName = fName & "\"
      fName = fName & "tips.dat"
      Try
        tips = File.ReadAllLines(fName)
      Catch
        ReDim tips(-1)
      End Try
    End If

    For i = 0 To UBound(tips) ' skip over blank lines
      If tips(iniTipNumber) <> "" Then
        lbtip.Text = tips(iniTipNumber)
        Exit For
      End If
      iniTipNumber = iniTipNumber + 1
      If iniTipNumber > UBound(tips) Then iniTipNumber = 1
    Next i

    iniTipNumber = iniTipNumber + 1
    If iniTipNumber > UBound(tips) Then iniTipNumber = 1

  End Sub

End Class