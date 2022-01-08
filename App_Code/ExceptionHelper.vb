Imports Microsoft.VisualBasic

Public Module ExceptionHelper
    <Runtime.CompilerServices.Extension>
    Public Function LineNumber(e As Exception) As Integer

        Dim linenum As Integer = 0
        Try
            linenum = Convert.ToInt32(e.StackTrace.Substring(e.StackTrace.LastIndexOf(":line") + 5))
            'Stack trace is not available!
        Catch
        End Try
        Return linenum
    End Function
End Module
