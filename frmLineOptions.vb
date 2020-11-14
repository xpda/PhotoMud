'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math

Public Class frmLineOptions
  Inherits Form

  Dim styles(5) As Integer
  Dim stylename(5) As String
  Dim tmpDrawPenStyle As Integer
  Dim tmpDrawPenWidth As Integer
  Dim Loading As Boolean = True

  Private Sub cmdCancel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
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

    frmMain.DrawPenWidth = nmThickness.Value
    frmMain.DrawPenStyle = styles(Combo1.SelectedIndex)

    If chkDefault.Checked Then
      iniDrawPenWidth = nmThickness.Value
      iniDrawPenStyle = styles(Combo1.SelectedIndex)
    End If

    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Private Sub Combo1_SelectedIndexChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles Combo1.SelectedIndexChanged

    If Loading Then Exit Sub
    tmpDrawPenStyle = styles(Combo1.SelectedIndex)
    tmpDrawPenWidth = nmThickness.Value
    DrawLine()

  End Sub

  Private Sub frmLineOptions_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Dim i, k As Integer

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Loading = True

    Using bmp As New Bitmap(pView1.ClientSize.Width, pView1.ClientSize.Height, PixelFormat.Format32bppPArgb)
      pView1.setBitmap(bmp)
    End Using

    pView1.Fill(Color.White)

    tmpDrawPenWidth = frmMain.DrawPenWidth
    tmpDrawPenStyle = frmMain.DrawPenStyle
    nmThickness.Value = tmpDrawPenWidth

    styles(0) = DashStyle.Solid
    stylename(0) = "solid"
    styles(1) = DashStyle.Dash
    stylename(1) = "dashed"
    styles(2) = DashStyle.Dot
    stylename(2) = "dotted"
    styles(3) = DashStyle.DashDot
    stylename(3) = "dash-dot"
    styles(4) = DashStyle.DashDotDot
    stylename(4) = "dash-dot-dot"

    k = 0
    For i = 0 To 4
      Combo1.Items.Insert(i, stylename(i))
      If styles(i) = tmpDrawPenStyle Then k = i
    Next i
    Combo1.SelectedItem = Combo1.Items(k)

    chkDefault.Checked = False
    Loading = False
    DrawLine()

  End Sub

  Sub DrawLine()

    Dim x As Integer

    pView1.Refresh()

    x = tmpDrawPenWidth

    Using _
        g As Graphics = Graphics.FromImage(pView1.Bitmap), _
        gpen As Pen = New Pen(Color.DarkBlue, tmpDrawPenWidth)
      g.Clear(Color.White)
      gpen.DashStyle = tmpDrawPenStyle
      g.SmoothingMode = SmoothingMode.AntiAlias
      g.PixelOffsetMode = PixelOffsetMode.HighQuality
      g.DrawLine(gpen, x, pView1.Bitmap.Height - x * 2, pView1.Bitmap.Width - x * 2, x)
      pView1.Zoom()

    End Using

  End Sub

  Private Sub nmThickness_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles nmThickness.ValueChanged

    If Loading Then Exit Sub
    tmpDrawPenWidth = nmThickness.Value
    DrawLine()

  End Sub

End Class