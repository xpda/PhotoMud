Imports System.Runtime.InteropServices

Public Class api

  Friend Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" _
    (ByVal uAction As Integer, ByVal uParam As Integer, ByVal lpvParam As String, ByVal fuWinIni As Integer) As Integer

  Friend Declare Function GlobalLock Lib "kernel32" (ByVal hMem As IntPtr) As IntPtr
  Friend Declare Function GlobalUnlock Lib "kernel32" (ByVal hMem As IntPtr) As Integer
  Friend Declare Function GlobalFree Lib "kernel32" (ByVal wFlags As IntPtr) As IntPtr

  Friend Declare Function FormatMessage Lib "kernel32" Alias "FormatMessageA" (
     ByVal dwFlags As Integer,
     ByRef lpSource As Object,
     ByVal dwMessageId As Integer,
     ByVal dwLanguageId As Integer,
     ByVal lpBuffer As String,
     ByVal nSize As Integer,
     ByRef Arguments As Integer) As Integer

  ' 
  Friend Declare Function DocumentProperties Lib "winspool.drv" Alias "DocumentPropertiesW" (
    ByVal hWnd As IntPtr,
    ByVal hPrinter As IntPtr,
    <MarshalAs(UnmanagedType.LPWStr)> ByVal pDeviceName As String,
    ByVal pDevModeOutput As IntPtr,
    ByVal pDevModeInput As IntPtr,
    ByVal fMode As Int32) As Integer

End Class

Module apiConstants
  ' constants are in the module to be public, functions and subs are in a class.

  ' Wallpaper stuff =========================================
  Public Const SPIF_UPDATEINIFILE As Integer = 1
  Public Const SPIF_SENDWININICHANGE As Integer = 2

  Public Const SPI_SETDESKWALLPAPER As Short = 20
  Public Const SPI_SETSCREENSAVEACTIVE As Short = 17
  Public Const SPI_GETSCREENSAVEACTIVE As Short = 16
  Public Const SPI_GETLOWPOWERACTIVE As Short = 83
  Public Const SPI_SETLOWPOWERACTIVE As Short = 85
  Public Const SPI_SETPOWEROFFACTIVE As Short = 86
  Public Const SPI_GETPOWEROFFACTIVE As Short = 84

  Public Const IDOK As Short = 1
  Public Const IDCANCEL As Short = 2

  Public Const DM_IN_BUFFER As Short = 8
  Public Const DM_IN_PROMPT As Short = 4
  Public Const DM_OUT_BUFFER As Short = 2
  Public Const DM_OUT_DEFAULT As Short = 1

  Public Const FORMAT_MESSAGE_FROM_SYSTEM As Integer = &H1000

End Module
