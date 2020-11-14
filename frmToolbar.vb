'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports System.Collections.Generic

Public Class frmToolbar
  Inherits Form

  Dim Separator As String
  Dim maxButtons As Integer

  Private Sub cmdAdd_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdAdd.Click

    Dim i, k As Integer

    k = List1.SelectedIndex
    If k < 0 Or List2.Items.Count >= maxButtons Then Exit Sub

    i = List2.SelectedIndex
    If i < 0 Then i = 0
    List2.Items.Insert(i, List1.Items(k))
    List2.SelectedIndex = i

    If k > 0 Then
      List1.Items.RemoveAt(List1.SelectedIndex) ' keep separator
      If k >= List1.Items.Count Then k -= 1
      List1.SelectedIndex = k
    End If

  End Sub

  Private Sub cmdHelp_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdHelp.Click
    Try
      Help.ShowHelp(Me, helpFile, HelpNavigator.Topic, Me.Name & ".html")
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

  Private Sub cmdMoveUp_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdMoveUp.Click

    Dim k As Integer
    Dim s As String

    If List2.SelectedIndex <= 0 Then Exit Sub
    k = List2.SelectedIndex
    s = CStr(List2.Items(k - 1))
    List2.Items(k - 1) = List2.Items(k)
    List2.Items(k) = s
    List2.SelectedIndex = k - 1

  End Sub
  Private Sub cmdMoveDown_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdMoveDown.Click

    Dim k As Integer
    Dim s As String

    If List2.SelectedIndex >= List2.Items.Count - 1 Then Exit Sub
    k = List2.SelectedIndex
    s = CStr(List2.Items(k + 1))
    List2.Items(k + 1) = List2.Items(k)
    List2.Items(k) = s
    List2.SelectedIndex = k + 1

  End Sub

  Private Sub cmdOK_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdOK.Click

    If callingForm Is frmExplore Then
      assignButtons(iniVButton, iniVToolButton, nVToolButtons)
    Else
      assignButtons(iniButton, iniToolButton, nToolButtons)
    End If

  End Sub
  Private Sub assignButtons(ByRef Buttons As Collection, ByRef ToolButton() As String, ByRef nToolButtons As Integer)

    Dim i As Integer
    Dim buttonx As New List(Of ToolStripItem)

    nToolButtons = 0
    ' assign New Icons
    For i = 0 To List2.Items.Count - 1
      nToolButtons += 1
      If List2.Items(i) = Separator Then
        ToolButton(nToolButtons) = "---"
      Else
        For Each b As ToolStripItem In Buttons
          If b.Text = List2.Items(i) Then
            ToolButton(nToolButtons) = CStr(b.Tag)
            Exit For
          End If
        Next b
      End If
    Next i

    If OptionSmall.Checked Then
      iniButtonSize = 0
    Else
      iniButtonSize = 1
    End If

    If chkToolbarText.Checked Then
      iniToolbarText = True
    Else
      iniToolbarText = False
    End If

    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Private Sub cmdRemove_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdRemove.Click

    Dim k As Integer

    k = List2.SelectedIndex
    If k < 0 Then Exit Sub

    If List2.Items(k) <> List1.Items(0) Then
      insert(List1, CStr(List2.Items(k)))
    End If

    List2.Items.RemoveAt(k) ' keep separator
    If k >= List2.Items.Count Then k -= 1
    List2.SelectedIndex = k

  End Sub

  Sub insert(ByRef List1 As ListBox, ByRef txt As String)
    ' inserts text into the list box in ascending order

    Dim i, k As Integer

    List1.Items.Add("")
    For i = List1.Items.Count - 1 To 1 Step -1
      k = i
      If txt > List1.Items(i - 1) Then Exit For
      List1.Items(i) = List1.Items(i - 1)
    Next i
    List1.Items(k) = txt
    List1.SelectedIndex = k

  End Sub

  Private Sub cmdCancel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
    Me.DialogResult = DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub cmdReset_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdReset.Click
    resetTools() ' reset to default tools
    If callingForm Is frmExplore Then
      loadTools(iniVButton, iniVToolButton, nVToolButtons)
    Else
      loadTools(iniButton, iniToolButton, nToolButtons)
    End If
    OptionSmall.Checked = True
    chkToolbarText.Checked = False
  End Sub

  Private Sub frmToolbar_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    If callingForm Is frmExplore Then
      loadTools(iniVButton, iniVToolButton, nVToolButtons)
    Else
      loadTools(iniButton, iniToolButton, nToolButtons)
    End If

    If iniButtonSize = 0 Then
      OptionSmall.Checked = True
    Else
      OptionLarge.Checked = True
    End If

    If iniToolbarText Then
      chkToolbarText.Checked = True
    Else
      chkToolbarText.Checked = False
    End If

  End Sub
  Private Sub loadTools(ByRef Buttons As Collection, ByRef ToolButton() As String, ByRef nToolButtons As Integer)

    Dim j, i, k As Integer
    Dim b As ToolStripItem

    List1.Items.Clear()
    List2.Items.Clear()

    Separator = "---Separator---"
    List1.Items.Add(Separator)

    For i = 1 To nToolButtons
      If ToolButton(i) = "---" Then
        List2.Items.Add(Separator)
      Else
        Try
          List2.Items.Add(Buttons.Item(ToolButton(i)).text)
        Catch
        End Try
      End If
    Next i

    For Each b In Buttons
      k = 0
      For j = 1 To nToolButtons
        If b.Tag = ToolButton(j) Then ' put it on the right side
          k = 1
          Exit For
        End If
      Next j
      If k = 0 And CStr(b.Tag) <> "---" Then insert(List1, b.Text)
    Next b

    If List1.Items.Count > 0 Then List1.SelectedIndex = 0
    If List2.Items.Count > 0 Then List2.SelectedIndex = 0

    maxButtons = UBound(ToolButton)

  End Sub

End Class