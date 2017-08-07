Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D

Public Class frmExpand
  Inherits Form

  Dim oldXres, oldYres As Integer

  Private Sub cmdCancel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
    clearBitmap(qImage)
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

    Dim newX, newY As Integer
    Dim ix, iy As Integer

    Me.Cursor = Cursors.WaitCursor
    newX = nmXres.Value
    newY = nmYres.Value

    qImage = New Bitmap(newX, newY, PixelFormat.Format32bppPArgb)
    Using g As Graphics = Graphics.FromImage(qImage)
      g.Clear(mBackColor)
    End Using
    If optRight.Checked = True Then ix = 0 Else ix = newX - oldXres
    If optDown.Checked = True Then iy = 0 Else iy = newY - oldYres
    bmpMerge(frmMain.mView.Bitmap, qImage, mergeOp.opReplace, New Rectangle(ix, iy, oldXres, oldYres))

    Me.Cursor = Cursors.Default
    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Private Sub frmExpand_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    optRight.Checked = True
    optDown.Checked = True

    oldXres = frmMain.mView.Bitmap.Width
    oldYres = frmMain.mView.Bitmap.Height

    nmXres.Value = oldXres
    nmYres.Value = oldYres
    nmXres.Minimum = oldXres
    nmYres.Minimum = oldYres
    nmXres.Maximum = MaxRes
    nmYres.Maximum = MaxRes

    lbXres.Text = "&Horizontal Size (currently " & oldXres & " pixels):"
    lbYres.Text = "&Vertical Size (currently " & oldYres & " pixels):"
  End Sub

End Class