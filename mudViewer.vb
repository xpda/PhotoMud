' Mudviewer is like pviewer, but it also supports the tabbs in frmMain
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Text
Imports System.Math
Imports System.IO
Imports System.Collections.Generic
Imports ImageMagick

Public Class mudViewer
  Inherits pViewer

  Property picName As String
  Property picPage As String
  Property originalFileName As String
  Property originalFormat As magickformat
  '  Property originalFormatExt As String
  Property picModified As Boolean
  Property newComment As Boolean
  Property pComments As List(Of PropertyItem) ' from gdi+, used for saving
  Property bitmapUndo As Bitmap

  Property nUndo As Integer ' number of undo's saved.
  Property nRedo As Integer ' top of redo stack
  Property firstUndo As Integer  ' bottom of undo stack

  Public WithEvents tabb As TabPage
  Public WithEvents ctlBright As ctlBrightness
  Public WithEvents ctlTolerance As ctlColorTolerance
  Public WithEvents ctlFeather As ctlFeatherControl
  Public WithEvents ctlText As ctlTextControl

  Event ctlBrightChanged(sender As Object, e As EventArgs)
  Event ctlBrightOK(sender As Object, e As EventArgs)
  Event ctlBrightCancel(sender As Object, e As EventArgs)

  Event ctlToleranceChanged(sender As Object, e As EventArgs)
  Event ctlToleranceOK(sender As Object, e As EventArgs)
  Event ctlToleranceCancel(sender As Object, e As EventArgs)

  Event ctlFeatherRangeChanged(sender As Object, e As EventArgs)
  Event ctlFeatherExpand(sender As Object, e As EventArgs)

  Event ctlTextChanged(sender As Object, e As EventArgs)
  Event ctlTextOK(sender As Object, e As EventArgs)
  Event ctlTextCancel(sender As Object, e As EventArgs)

  Event Activated(sender As Object, e As EventArgs)

  Event subKeyDown(sender As Object, e As KeyEventArgs)

  Public Sub New()

    MyBase.New()
    MyBase.Dock = DockStyle.Fill
    nextMview = nextMview + 1
    Me.Tag = nextMview
    pComments = New List(Of PropertyItem)

    mViews.Add(Me)
    tabb = New TabPage
    frmMain.mainTabs.TabPages.Add(tabb)
    tabb.Controls.Add(Me)
    activateStuff()

    ctlBright = New ctlBrightness
    ctlTolerance = New ctlColorTolerance
    ctlFeather = New ctlFeatherControl
    ctlText = New ctlTextControl
    ctlBright.Visible = False
    ctlTolerance.Visible = False
    ctlFeather.Visible = False
    ctlText.Visible = False
    tabb.Controls.Add(ctlBright)
    tabb.Controls.Add(ctlTolerance)
    tabb.Controls.Add(ctlFeather)
    tabb.Controls.Add(ctlText)

    nUndo = 0
    firstUndo = 1

  End Sub

  Property Caption() As String
    Get
      Caption = tabb.Text
    End Get
    Set(ByVal value As String)
      tabb.Text = value
      If tabb Is frmMain.mainTabs.SelectedTab Then frmMain.Text = value
    End Set
  End Property

  Private Sub activateStuff()
    ' you can't raise an event in a constructor, so New has to here instead of Activate

    Dim tc As TabControl

    tc = tabb.Parent
    tc.SelectedTab = tabb
    tabb.select()
    frmMain.mView = Me
    frmMain.Text = Me.Caption

    If Not frmMain.Focused Then
      changeForm(Form.ActiveForm, frmMain)
      frmMain.select()
    End If

  End Sub

  Public Sub Activate(sender As Object, e As EventArgs)
    activateStuff()
    RaiseEvent Activated(sender, e)
  End Sub

  Public Sub Close()

    Dim i As Integer
    Dim tc As TabControl
    Dim rv As mudViewer

    tc = tabb.Parent
    tc.TabPages.Remove(tabb)

    If tc.SelectedTab Is Nothing Then
      frmMain.mView = Nothing
      changeForm(Form.ActiveForm, frmExplore)
    Else
      rv = getMuddViewer(tc.SelectedTab)
      If rv IsNot Nothing Then rv.Activate(Nothing, Nothing)
    End If

    For i = 1 To mViews.Count
      If mViews.Item(i) Is Me Then
        mViews.Remove(i)
        Exit For
      End If
    Next i

    clearBitmap(bitmapUndo)
    If bitmapUndo IsNot Nothing Then bitmapUndo.Dispose()
    If tabb IsNot Nothing Then tabb.Dispose()

  End Sub

  Private Sub ctlBright_Changed(sender As Object, e As EventArgs) Handles ctlBright.Changed
    RaiseEvent ctlBrightChanged(sender, e)
  End Sub

  Private Sub ctlBright_OK(sender As Object, e As EventArgs) Handles ctlBright.OK
    RaiseEvent ctlBrightOK(sender, e)
  End Sub

  Private Sub ctlBright_Cancel(sender As Object, e As EventArgs) Handles ctlBright.Cancel
    RaiseEvent ctlBrightCancel(sender, e)
  End Sub

  Private Sub ctlTolerance_Changed(sender As Object, e As EventArgs) Handles ctlTolerance.ToleranceChanged
    RaiseEvent ctlToleranceChanged(sender, e)
  End Sub

  Private Sub ctlTolerance_OK(sender As Object, e As EventArgs) Handles ctlTolerance.OK
    RaiseEvent ctlToleranceOK(sender, e)
  End Sub

  Private Sub ctlTolerance_Cancel(sender As Object, e As EventArgs) Handles ctlTolerance.Cancel
    RaiseEvent ctlToleranceCancel(sender, e)
  End Sub

  Private Sub ctlFeather_RangeChanged(sender As Object, e As EventArgs) Handles ctlFeather.rangeChanged
    RaiseEvent ctlFeatherRangeChanged(sender, e)
  End Sub

  Private Sub ctlText_Changed(sender As Object, e As EventArgs) Handles ctlText.Changed
    RaiseEvent ctlTextChanged(sender, e)
  End Sub

  Private Sub ctlText_OK(sender As Object, e As EventArgs) Handles ctlText.OK
    RaiseEvent ctlTextOK(sender, e)
  End Sub

  Private Sub ctlText_Cancel(sender As Object, e As EventArgs) Handles ctlText.Cancel
    RaiseEvent ctlTextCancel(sender, e)
  End Sub

  Private Sub sub_KeyDown(ByVal Sender As Object, ByVal e As KeyEventArgs) _
    Handles tabb.KeyDown, ctlBright.KeyDown, ctlTolerance.KeyDown, ctlFeather.KeyDown, ctlText.KeyDown
    RaiseEvent subKeyDown(Sender, e)
  End Sub

  Private Sub mudViewer_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    Me.Dispose()
  End Sub
End Class
