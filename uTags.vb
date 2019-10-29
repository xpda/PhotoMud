'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Public Class uTag

  Dim uKey As String
  Dim uTagg As Integer
  Dim uValue As Object
  Dim uLink As UInteger
  Dim uDataType As Integer
  Public IFD As uExif

  Public onlyRead As Boolean = False  ' flag for saving

  Property tag() As Integer
    Get
      tag = uTagg
    End Get
    Set(ByVal Value As Integer)
      uTagg = Value
      uKey = Right("0000" & Hex(tag), 4)
    End Set
  End Property

  Property Value() As Object
    Get
      If IsArray(uValue) Then Value = uValue.clone Else Value = uValue
    End Get
    Set(ByVal Value As Object)
      If IsArray(Value) Then uValue = Value.clone Else uValue = Value
    End Set
  End Property

  ReadOnly Property singleValue(Optional ByVal Subscript As Integer = 0) As Object
    Get
      singleValue = Nothing
      If Not IsArray(uValue) Then
        singleValue = uValue
      Else
        If Subscript >= 0 And Subscript <= uuBound(uValue) Then singleValue = uValue(Subscript)
      End If
    End Get
  End Property

  Property Link() As UInteger
    Get
      Link = uLink
    End Get
    Set(ByVal Value As UInteger)
      uLink = Value
    End Set
  End Property

  Property dataType() As Integer
    Get
      dataType = uDataType
    End Get
    Set(ByVal Value As Integer)
      uDataType = Value
    End Set
  End Property

  ReadOnly Property key() As String
    Get
      key = uKey
    End Get
  End Property

  Public Sub New()
    MyBase.New()
    uTagg = -1
  End Sub
End Class