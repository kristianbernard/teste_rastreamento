Imports Microsoft.VisualBasic
Imports System.IO
Imports clsSmart
Public Class clslogs

    Dim _novostream 'As StreamWriter()
    Dim _arquivo As String = ""
    Dim _entrada As String = ""
    Dim _caminho As String = "padrao"
    Dim _usuario As String = ""


    Public Sub New(xid As String, xtipolog As String)
        _usuario = HttpContext.Current.Session("usuario")

        _arquivo = "log_" & xtipolog & "_" & xid & "_" & _usuario & ".log"
        If _caminho = "padrao" Then
            _caminho = HttpContext.Current.Server.MapPath("~") & "logerros"
        Else
            _caminho = HttpContext.Current.Server.MapPath("~") & "" & _caminho
            criarpastas(_caminho)

        End If
        _arquivo = alteranome(_caminho & "\" & _arquivo)

        _novostream = New StreamWriter(_arquivo)
        _novostream.WriteLine("Início do LOG")
        _novostream.WriteLine("Tipo:" & xtipolog)
        _novostream.WriteLine("Usuário:" & _usuario)
        _novostream.WriteLine("URL:" & HttpContext.Current.Request.Url.AbsoluteUri)
        _novostream.WriteLine("ID:" & xid)
        _novostream.WriteLine("Data:" & Data())
        _novostream.WriteLine("Hora:" & hora())
        _novostream.WriteLine("******************************************************")
        _novostream.WriteLine("Lista de Eventos:")



    End Sub

    Private Sub criarpastas(caminho As String)
        Throw New NotImplementedException()
    End Sub

    Public Sub New(vazio As Boolean)




    End Sub
    Public Property Arquivo As String
        Get
            Return _arquivo
        End Get
        Set(value As String)
            _arquivo = value
        End Set
    End Property



    Public Property Usuario As String
        Get
            Return _usuario
        End Get
        Set(value As String)
            _usuario = value
        End Set
    End Property



    Public Property Entrada As String
        Get
            Return _entrada
        End Get
        Set(value As String)
            _entrada = value
            _novostream.WriteLine("Data: " & Data() & " - Hora: " & hora() & " Evento:" & _entrada)
        End Set
    End Property

    Public Property Caminho As String
        Get
            Return _caminho
        End Get
        Set(value As String)
            _caminho = value
        End Set
    End Property
End Class
Partial Public Class clslogs
    Public Function finalizar() As Boolean
        Try
            _novostream.WriteLine("******************************************************")
            _novostream.WriteLine("Data: " & Data() & " - Hora: " & hora() & " Fim dos Eventos.")
            _novostream.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function


End Class