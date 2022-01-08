Imports Microsoft.VisualBasic
Imports clsSmart
Imports System.IO
Public Class clssessoes
    Inherits System.Web.UI.Page
    Private Shared _mensagemerro As String
    Public Shared cookieName As String = "rastreamento"
    Private Shared _listacookiessistema As New List(Of String)

    Public Sub New()

    End Sub

    Public Shared Sub versessao()

    End Sub

    Private Shared Sub carregacookies()
        Try
            _listacookiessistema.Clear()
            _listacookiessistema.Add("id")
            _listacookiessistema.Add("idusuario")
            _listacookiessistema.Add("usuario")
            _listacookiessistema.Add("nomeusuario")
            _listacookiessistema.Add("usuario")
            _listacookiessistema.Add("email")
            _listacookiessistema.Add("permissao")
            _listacookiessistema.Add("usuariomaster")
            _listacookiessistema.Add("imagemperfil")
            _listacookiessistema.Add("urlretorno")
            _listacookiessistema.Add("tipo")
        Catch ex As Exception

        End Try




    End Sub

    Public Shared Sub Zerarcookie()

        carregacookies()
        Dim xCoockie As HttpCookie
        xCoockie = verificarCookies()

        For x As Integer = 0 To _listacookiessistema.Count - 1
            Try
                If _listacookiessistema(x) Is Nothing Then Exit For

                If xCoockie.Values(_listacookiessistema(x)) Is Nothing Then
                    xCoockie.Values(_listacookiessistema(x)) = ""


                End If

                HttpContext.Current.Session(_listacookiessistema(x)) = HttpContext.Current.Server.HtmlDecode(xCoockie.Values(_listacookiessistema(x)))
            Catch extam As Exception
                Exit For
            End Try



        Next

        xCoockie.Expires = DateTime.Now.AddDays(-1)
        HttpContext.Current.Response.Cookies.Add(xCoockie)
    End Sub

    Public Shared Sub limparCookieTemporario()



        carregacookies()
        Dim xCoockie As HttpCookie
        xCoockie = verificarCookies()

        For x As Integer = 0 To _listacookiessistema.Count - 1
            Try
                If _listacookiessistema(x) Is Nothing Then Exit For

                If xCoockie.Values(_listacookiessistema(x)) Is Nothing Then
                    xCoockie.Values(_listacookiessistema(x)) = ""


                End If

                HttpContext.Current.Session(_listacookiessistema(x)) = HttpContext.Current.Server.HtmlDecode(xCoockie.Values(_listacookiessistema(x)))
            Catch extam As Exception
                Exit For
            End Try



        Next

        xCoockie.Expires = DateTime.Now.AddDays(1)
        HttpContext.Current.Response.Cookies.Add(xCoockie)
    End Sub

    Public Shared Sub cookie_sessao(Optional chave As String = "")

        carregacookies()
        Dim xCoockie As HttpCookie
        xCoockie = verificarCookies()

        For x As Integer = 0 To _listacookiessistema.Count - 1
            Try
                If _listacookiessistema(x) Is Nothing Then Exit For
                If chave = "" Then
                    If xCoockie.Values(_listacookiessistema(x)) Is Nothing Then
                        xCoockie.Values(_listacookiessistema(x)) = ""


                    End If
                    If HttpContext.Current.Server.HtmlDecode(xCoockie.Values(_listacookiessistema(x))) <> "" Then
                        HttpContext.Current.Session(_listacookiessistema(x)) = IIf(HttpContext.Current.Server.HtmlDecode(xCoockie.Values(_listacookiessistema(x))).Substring(0, 1) = ",", HttpContext.Current.Server.HtmlDecode(xCoockie.Values(_listacookiessistema(x))).Substring(1), HttpContext.Current.Server.HtmlDecode(xCoockie.Values(_listacookiessistema(x))))
                    Else
                        HttpContext.Current.Session(_listacookiessistema(x)) = ""
                    End If
                Else
                    If _listacookiessistema(x).ToLower = chave.ToLower Then
                        HttpContext.Current.Session(_listacookiessistema(x)) = IIf(HttpContext.Current.Server.HtmlDecode(xCoockie.Values(_listacookiessistema(x))).Substring(0, 1) = ",", HttpContext.Current.Server.HtmlDecode(xCoockie.Values(_listacookiessistema(x))).Substring(1), HttpContext.Current.Server.HtmlDecode(xCoockie.Values(_listacookiessistema(x))))
                    End If
                End If

            Catch extam As Exception
                Exit For
            End Try

        Next

        xCoockie.Expires = DateTime.Now.AddDays(4)
        HttpContext.Current.Response.Cookies.Add(xCoockie)
    End Sub



    Public Shared Function gravarcookie(chave As String, valor As String) As Boolean

        Dim xCoockie As HttpCookie
        xCoockie = verificarCookies()


        xCoockie.Values(chave) = ""
        xCoockie.Values.Add(chave, valor)
        HttpContext.Current.Response.AppendCookie(xCoockie)
        Return True


    End Function


    Public Shared Function buscarsessoes(chave As String) As String

        carregacookies()

        Dim xCoockie As HttpCookie
        xCoockie = verificarCookies()

        For x As Integer = 0 To _listacookiessistema.Count - 1
            If _listacookiessistema(x) Is Nothing Then Exit For

            If xCoockie.Values(_listacookiessistema(x)) Is Nothing Then
                xCoockie.Values(_listacookiessistema(x)) = ""


            End If




        Next

        Dim valorretorno As String = ""
        If chave <> "" Then
            valorretorno = xCoockie.Values(chave)
        End If
        If valorretorno.Length > 0 AndAlso valorretorno.Substring(0, 1) = "," Then
            valorretorno = valorretorno.Substring(1, valorretorno.Length - 1)
        End If
        xCoockie.Expires = DateTime.Now.AddDays(30)
        HttpContext.Current.Response.Cookies.Add(xCoockie)

        Return valorretorno

    End Function

    Public Shared Function gravarcookie(xcookies As List(Of clscookies)) As Boolean

        Dim xCoockie As HttpCookie
        xCoockie = verificarCookies()
        For x As Integer = 0 To xcookies.Count - 1
            xCoockie.Values(xcookies(x).Chave) = ""
            xCoockie.Values.Add(xcookies(x).Chave, xcookies(x).Valor)
            HttpContext.Current.Response.AppendCookie(xCoockie)
        Next
        Return True
    End Function

    Private Shared Function verificarCookies() As HttpCookie
        Try
            If HttpContext.Current.Request.Cookies(cookieName) Is Nothing Then
                Dim aCookie As New HttpCookie(cookieName)

                aCookie.Expires = DateTime.Now.AddDays(30)
                HttpContext.Current.Response.Cookies.Add(aCookie)
                Return aCookie
            Else

                Return HttpContext.Current.Request.Cookies(cookieName)
            End If
        Catch excoockie As Exception
            _mensagemerro = excoockie.Message
            Return Nothing
        End Try

    End Function
End Class
