'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports System.Collections.Generic

Public Class frmFileTypes
  Inherits Form

  Dim oldFiletype As New List(Of String)
  Dim chkFmt As New List(Of CheckBox)

  Private Sub cmdCancel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click

    iniFileType = New List(Of String)
    For Each s As String In oldFiletype
      iniFileType.Add(s)
    Next s

    Me.DialogResult = DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub cmdOK_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdOK.Click
    setFilter()
    Me.DialogResult = DialogResult.OK
    Me.Close()
  End Sub

  Private Sub cmdSelectAll_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdSelectAll.Click

    For Each chk As CheckBox In chkFmt
      chk.Checked = True
    Next chk

  End Sub

  Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click

    For Each chk As CheckBox In chkFmt
      chk.Checked = False
    Next chk

  End Sub

  Private Sub fmFileTypes_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Dim j As Integer
    Dim i As Integer
    Dim chk As CheckBox

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, "filetypes.html")

    Label1.Text = "Check the file types to view with the File Open dialog:"

    For i = 0 To fmtCommon.Count - 1
      chk = New CheckBox
      chk.Left = 200
      chk.Top = 70 + i * chk.Height * 1.3
      chk.Font = Me.Font
      chk.Name = "chkFmt" & i
      chk.AutoSize = True
      chk.TabIndex = 10 + i
      chk.Visible = True
      chk.Text = fmtCommon(i).Description & " (" & fmtCommon(i).Ext & ")"
      Me.Controls.Add(chk)
      chkFmt.Add(chk)

    Next i

    i = chkFmt(0).Top + (chkFmt.Count + 1.5) * chkFmt(0).Height * 1.3
    i -= Me.ClientSize.Height
    If i > 0 Then Me.Height += i

    oldFiletype = New List(Of String)
    For Each s As String In iniFileType
      oldFiletype.Add(s)
    Next s

    For Each chk In chkFmt ' clear checkboxes
      chk.Checked = False
    Next chk

    For i = 0 To fmtCommon.Count - 1
      chkFmt(i).Text = fmtCommon(i).Description & " (" & fmtCommon(i).Ext & ")"
    Next i

    ' get match up extensions with chkFmt items, and check them.
    For i = 0 To iniFileType.Count - 1
      For j = 0 To fmtCommon.Count - 1
        If eqstr(iniFileType(i), fmtCommon(j).ID) Then
          chkFmt(j).Checked = True
          Exit For
        End If
      Next j
    Next i

  End Sub

  Sub setFilter()

    Dim i As Integer

    iniFileType = New List(Of String)

    For i = 0 To chkFmt.Count - 1 ' these have to be in order
      If chkFmt(i).Checked And Len(chkFmt(i).Text) <> 0 Then
        For Each s As String In fmtCommon(i).Ext.Split(";")
          iniFileType.Add(s)
        Next s
      End If
    Next i

    If iniFileType.Count = 0 Then
      iniFileType.Add(".jpg")
      iniFileType.Add(".jpeg")
    End If

  End Sub

End Class