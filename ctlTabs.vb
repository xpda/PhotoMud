Imports System.Windows.Forms
' this displays a tab control without the tabs if there is only one tabpage.
Public Class ctlTabs
Inherits TabControl

Protected NotOverridable Overrides Sub wndproc(ByRef m As Message)
' Hide tabs by trapping the TCM_ADJUSTRECT message
    If m.Msg <> &H1328 Or Me.TabCount <> 1 Then MyBase.WndProc(m)
End Sub

End Class
