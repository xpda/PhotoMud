'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math

Public Class frmNew
  Inherits Form

  Dim gImage As Bitmap

  Private Sub cmdHelp_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdHelp.Click
    Try
      Help.ShowHelp(Me, helpFile, HelpNavigator.Topic, Me.Name & ".html")
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

  Private Sub cmdOK_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdOK.Click

    Dim rview As mudViewer

    iniNewXres = nmXres.Value
    iniNewYres = nmYres.Value

    rview = newWindow(iniNewXres, iniNewYres)
    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Private Sub cmdCancel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
    Me.DialogResult = DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub frmNew_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    nmXres.Value = iniNewXres
    nmYres.Value = iniNewYres

    ' Copy the size from the clipboard
    If Clipboard.ContainsImage Then
      gImage = Clipboard.GetImage
      If gImage IsNot Nothing AndAlso gImage.Width >= nmXres.Minimum And gImage.Height >= nmYres.Minimum Then
        nmXres.Value = gImage.Width
        nmYres.Value = gImage.Height
      End If
      clearBitmap(gImage)
    End If

  End Sub

  Private Sub frmNew_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    clearBitmap(gImage)
    If components IsNot Nothing Then components.Dispose() : components = Nothing
  End Sub

End Class