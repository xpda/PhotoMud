'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports System.IO
Imports Microsoft.Win32
Imports System.Collections.Generic

Public Class frmFileAssoc
  Inherits Form

  Dim chkFmt As New List(Of CheckBox)

  Private Sub cmdCancel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
    Me.DialogResult = DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub cmdClear_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdClear.Click

    For Each chk As CheckBox In chkFmt
      chk.Checked = False
    Next chk

  End Sub

  Private Sub cmdHelp_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdHelp.Click
    Try
      Help.ShowHelp(Me, helpFile, HelpNavigator.Topic, Me.Name & ".html")
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

  Private Sub cmdOK_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdOK.Click

    AttachFiles()
    Me.DialogResult = DialogResult.OK
    Me.Close()
    iniAskAssociate = True

  End Sub

  Private Sub cmdSelAll_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdSelAll.Click

    For Each chk As CheckBox In chkFmt
      chk.Checked = True
    Next chk

  End Sub

  Private Sub cmdSelUnassoc_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdSelUnassoc.Click

    Dim i As Integer
    Dim assoc As String = ""

    For i = 0 To chkFmt.Count - 1
      Try
        assoc = Registry.GetValue("HKEY_CURRENT_USER\software\classes", fmtCommon(i).ID, "error")
      Catch ex As Exception
        MsgBox(ex.Message)
      End Try
      If assoc = "" Then chkFmt(i).Checked = True
    Next i

  End Sub

  Private Sub frmFileAssoc_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Dim assoc As String
    Dim i As Integer
    Dim chk As CheckBox

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Label1.Text = "Select the file types you would like to have associated with this application. That will cause " & AppName & " to open the file whenever you click on it in Windows Explorer."

    For i = 0 To fmtCommon.Count - 1
      chk = New CheckBox
      chk.Left = 200
      chk.Top = 100 + i * chk.Height * 1.3
      chk.Font = Me.Font
      chk.Name = "chkFmt" & i
      chk.AutoSize = True
      chk.TabIndex = 10 + i
      chk.Visible = True
      chk.Text = fmtCommon(i).Description & " (" & fmtCommon(i).Ext & ")"
      Me.Controls.Add(chk)
      chkFmt.Add(chk)

      Try
        assoc = Registry.GetValue("HKEY_CURRENT_USER\software\classes" & "\" & fmtCommon(i).ID, "", "error")
      Catch ex As Exception
        MsgBox(ex.Message)
        assoc = "error"
      End Try

      Select Case assoc
        Case appTag
          chkFmt(i).Checked = True
        Case ""
          chkFmt(i).Checked = False
      End Select
    Next i

    i = chkFmt(0).Top + (chkFmt.Count + 1.5) * chkFmt(0).Height * 1.3
    i -= Me.ClientSize.Height
    If i > 0 Then Me.Height += i

  End Sub

  Sub AttachFiles()

    Dim i, j As Integer
    Dim assoc As String
    Dim ss() As String
    Dim separator() As Char = {" "}

    For i = 0 To chkFmt.Count - 1
      If chkFmt(i).Checked Then
        ss = fmtCommon(i).Ext.Split(";")
        For j = 0 To UBound(ss)
          MakeAssociation(ss(j))
        Next j

      ElseIf Not chkFmt(i).Checked Then
        ss = fmtCommon(i).Ext.Split(";")
        For j = 0 To UBound(ss)
          Try
            assoc = Registry.GetValue("HKEY_CURRENT_USER\software\classes" & "\" & ss(j), "", "error")
          Catch ex As Exception
            MsgBox(ex.Message)
            assoc = ""
          End Try
          If assoc = appTag Then RemoveAssociation(ss(j))
        Next j
      End If
    Next i

  End Sub

  Public Sub MakeAssociation(ByRef FileType As String)
    ' associate the filetype with the program

    Dim appKey As String
    Dim Loader As String
    Dim q As RegistryKey

    Try
      Registry.SetValue("HKEY_CURRENT_USER\software\classes\" & FileType, "", appTag) ' set new value, overwrite any other, creates key if not there.
      ' if the explorer ProgID tag is set, then overwrite it.
      q = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\" & FileType, True)
      If q IsNot Nothing AndAlso q.GetValue("ProgID", "notfound") <> "notfound" Then
        q.SetValue("ProgID", appTag) ' for the local user, overrides hkcr
      End If
      ' it won't hurt to overwrite this.
      appKey = "HKEY_CURRENT_USER\software\classes\" & appTag
      Registry.SetValue(appKey, "", "image")
      Registry.SetValue(appKey & "\shell", "", "open")
      Registry.SetValue(appKey & "\shell\open", "", "")

      Loader = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) & "\" & AppName.Replace(" ", "") & "loader.exe" ' & AppName & ".exe" ' 
      Registry.SetValue(appKey & "\shell\open\command", "", """" & Loader & """ ""%1""")
      Registry.SetValue(appKey, "", "image")
      appKey = "HKEY_CURRENT_USER\software\classes\CLSID\" & Guid
      Registry.SetValue(appKey, "", appLongTitle)
      Registry.SetValue(appKey & "\LocalServer32", "", """" & Loader & """")
      Registry.SetValue(appKey & "\ProgID", "", appTag)
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

  End Sub

  Private Sub RemoveAssociation(ByRef FileType As String)

    Dim q As RegistryKey

    ' deletes the association for FileType
    Try
      Registry.SetValue("HKEY_CURRENT_USER\software\classes\" & FileType, "", "") ' set new value, overwrite any other, creates key if not there.
      q = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\" & FileType, True)
    Catch ex As Exception
      MsgBox(ex.Message)
      q = Nothing
    End Try
    If q IsNot Nothing AndAlso q.GetValue("ProgID", "notfound") <> "notfound" Then q.DeleteValue("ProgID")

  End Sub

End Class