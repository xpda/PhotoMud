Imports System.IO
Imports System.Collections.Generic

Public Class frmCalendarEvents

  Structure calEvent
    Dim descr As String
    Dim dt As Date
    Dim category As String
  End Structure

  Dim Processing As Boolean = False
  Dim Loading As Boolean = True

  Dim cEvent As calEvent
  Dim cEvents As New Collection
  Dim gridCategory As String ' current category in grid
  Dim iYear As Integer
  Dim delRow As DataGridViewRow = Nothing
  Dim delIndex As Integer

  Private Sub frmCalendarEvents_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Loading = True

    Grid1.Rows.Clear()
    Grid1.Columns("colDate").Width = Grid1.Width * 0.2
    iYear = Today.Year

    cmbCat.Items.Clear()
    cmbCat.Items.Add(customCalCat)

    loadFile()
    loadGrid(cmbCat.Items(0))
    cmbCat.SelectedIndex = 0

    Loading = False

  End Sub

  Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

    Dim ss As New List(Of String)
    Dim n As Integer
    Dim category As String
    'Dim cEvent As calEvent

    n = -1
    unloadGrid()

    For Each category In cmbCat.Items
      If Trim(category) = "" Then category = customCalCat
      ss.Add("-1")
      ss.Add(Trim(category))
      category = LCase(category)
      For Each cEvent As calEvent In cEvents
        If eqstr(cEvent.category, category) Then
          ss.Add("3")
          ss.Add(cEvent.dt)
          ss.Add(cEvent.descr)
        End If
      Next cEvent
    Next category

    Try
      File.WriteAllLines(dataPath & customCalFile, ss, System.Text.Encoding.GetEncoding("ISO-8859-1"))
    Catch ex As Exception
      MsgBox(ex.Message)
      Grid1.select()
      Exit Sub
    End Try

    Me.DialogResult = DialogResult.OK
    Me.Close()
  End Sub

  Private Sub Grid1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Grid1.KeyDown

    If e.KeyCode = Keys.Delete Then
      If Not Grid1.CurrentRow.IsNewRow Then
        delRow = Grid1.CurrentRow
        delIndex = Grid1.CurrentRow.Index
        Grid1.Rows.Remove(Grid1.CurrentRow)
      End If
    End If

    If e.KeyCode = Keys.Z And e.Control And delRow IsNot Nothing Then
      Grid1.Rows.Insert(delIndex, delRow)
      delRow = Nothing
    End If

  End Sub

  Private Sub mnxGridDelete_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnxGridDelete.Click

    If Not Grid1.CurrentRow.IsNewRow Then
      delRow = Grid1.CurrentRow
      delIndex = Grid1.CurrentRow.Index
      Grid1.Rows.Remove(Grid1.CurrentRow)
    End If

  End Sub

  Private Sub mnxGridInsert_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnxGridInsert.Click

    Dim nil(3) As Object

    nil(0) = ""
    nil(1) = ""
    Grid1.Rows.Insert(Grid1.CurrentRow.Index, nil)

  End Sub

  Private Sub mnxGrid_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles mnxGrid.Opening
    If delRow IsNot Nothing Then mnxGridUndo.Visible = True Else mnxGridUndo.Visible = False
  End Sub

  Private Sub mnxGridUndo_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnxGridUndo.Click
    Grid1.Rows.Insert(delIndex, delRow)
    delRow = Nothing
  End Sub

  Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
    Me.DialogResult = DialogResult.Cancel
    Me.Close()
  End Sub

  Sub loadFile()
    ' read the custom events file

    Dim ss() As String
    Dim s As String
    Dim iLine As Integer
    Dim descr As String
    Dim category As String
    Dim dt As DateTime

    ReDim ss(-1) ' null
    If File.Exists(dataPath & customCalFile) Then
      Try
        ss = File.ReadAllLines(dataPath & customCalFile)
      Catch
        Exit Sub
      End Try
    End If

    iLine = 0
    category = customCalCat

    Do While iLine <= UBound(ss)
      s = ss(iLine) : iLine = iLine + 1
      If IsNumeric(s) Then
        Select Case s
          Case -1 ' category
            category = ss(iLine) : iLine = iLine + 1
            ' add category to combo box if necessary
            If Not cmbCat.Items.Contains(category) Then cmbCat.Items.Insert(cmbCat.Items.Count, category)

          Case 3
            dt = CDate(ss(iLine)) : iLine = iLine + 1
            descr = ss(iLine) : iLine = iLine + 1
            cEvent = Nothing
            cEvent.descr = descr
            cEvent.dt = dt
            cEvent.category = category
            cEvents.Add(cEvent)
        End Select
      End If
    Loop

  End Sub

  Sub loadGrid(ByVal category As String)
    ' put the events for a category onto the grid
    Dim gRow As DataGridViewRow
    Dim cEvent As calEvent

    gRow = Grid1.Rows(0).Clone
    Grid1.Rows.Clear()
    For Each cEvent In cEvents
      If eqstr(cEvent.category, category) Then
        gRow = Grid1.Rows(0).Clone
        gRow.Cells(0).Value = cEvent.descr
        gRow.Cells(1).Value = Format(cEvent.dt, "short date")
        Grid1.Rows.Add(gRow)
      End If
    Next cEvent
    gridCategory = category

  End Sub

  Sub unloadGrid()

    Dim i As Integer
    Dim gRow As DataGridViewRow
    Dim s As String
    Dim cTmp As New Collection
    Dim cEvent As calEvent

    cTmp.Clear()

    For Each gRow In Grid1.Rows
      If (Not gRow.IsNewRow) And (gRow.Cells(0).Value IsNot Nothing) _
        And (gRow.Cells(1).Value IsNot Nothing) Then
        cEvent = Nothing
        cEvent.descr = gRow.Cells(0).Value
        cEvent.category = gridCategory
        s = gRow.Cells(1).Value

        If cEvent.descr.Trim <> "" And s.Trim <> "" Then
          cEvent.dt = Nothing
          If IsDate(s) Then ' check some possibilities
            cEvent.dt = CDate(s)
          ElseIf IsDate(s & "/" & iYear) Then
            cEvent.dt = CDate(s & "/" & iYear)
          ElseIf IsDate(s & "-" & iYear) Then
            cEvent.dt = CDate(s & "-" & iYear)
          ElseIf IsDate(s & ", " & iYear) Then
            cEvent.dt = CDate(s & ", " & iYear)
          ElseIf IsDate(s & " " & iYear) Then
            cEvent.dt = CDate(s & " " & iYear)
          Else
            MsgBox("Invalid Date")
            Grid1.CurrentCell = gRow.Cells(1)
            Grid1.select()
            Exit Sub
          End If

          cTmp.Add(cEvent) ' save temporarily so we can abort on a bad date
        End If
      End If
    Next gRow

    For i = cEvents.Count To 1 Step -1  ' remove events of the current category
      If eqstr(cEvents(i).category, gridCategory) Then
        cEvents.Remove(i)
      End If
    Next i

    For Each cEvent In cTmp ' add back in the events from the grid
      cEvents.Add(cEvent)
    Next cEvent

  End Sub

  Private Sub cmbCat_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCat.Leave
    Dim s, s1 As String

    If Loading Then Exit Sub

    Processing = True

    If cmbCat.SelectedIndex = -1 Then
      s = Trim(cmbCat.Text)
      For Each s1 In cmbCat.Items
        If eqstr(s1, s) Then
          Processing = False
          cmbCat.SelectedItem = s
          Exit Sub
        End If
      Next s1

      If Len(s) > 0 Then cmbCat.Items.Add(s)
      Processing = False
      cmbCat.SelectedItem = s
    End If

    Processing = False

  End Sub

  Private Sub cmbCat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCat.SelectedIndexChanged

    If Processing Or Loading Then Exit Sub

    If String.Compare(cmbCat.Text, gridCategory, True) <> 0 Then
      unloadGrid()
      loadGrid(cmbCat.Text)
    End If

  End Sub

  Private Sub cmdDelCat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelCat.Click

    Dim i As Integer
    Dim s As String

    If String.Compare(cmbCat.SelectedItem, customCalCat, True) <> 0 Then
      s = cmbCat.SelectedItem
      For i = cEvents.Count To 1 Step -1  ' remove events of the current category
        If eqstr(cEvents(i).category, s) Then
          cEvents.Remove(i)
        End If
      Next i
      cmbCat.Items.Remove(s) ' remove the combo item
    End If

  End Sub

End Class