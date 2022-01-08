Imports Microsoft.VisualBasic
Imports clsSmart

Public Class clslogin
    Dim _usuario As String

    Dim _senha As String

    Dim _mensagemerro As String

    Dim _tentativas As Integer

    Dim _urlretorno As String

    Dim _cliente As String = "Rastreamento"

    Dim _produto As String = "rastreamento"

    Dim _tipo As String = ""

    Dim _nrseq As Integer

    Dim _img As String = ""

    Dim _url As String = ""

    Dim _nrseqperfil As Integer = 0

    Dim _perfil As String = ""

    Dim _listaclasse As New List(Of clslogin)



    Public Property Usuario As String
        Get
            Return _usuario
        End Get
        Set(value As String)
            _usuario = value
        End Set
    End Property

    Public Property Senha As String
        Get
            Return _senha
        End Get
        Set(value As String)
            _senha = value
        End Set
    End Property

    Public Property Mensagemerro As String
        Get
            Return _mensagemerro
        End Get
        Set(value As String)
            _mensagemerro = value
        End Set
    End Property

    Public Property Tentativas As Integer
        Get
            Return _tentativas
        End Get
        Set(value As Integer)
            _tentativas = value
        End Set
    End Property

    Public Property Urlretorno As String
        Get
            Return _urlretorno
        End Get
        Set(value As String)
            _urlretorno = value
        End Set
    End Property

    Public Property Cliente As String
        Get
            Return _cliente
        End Get
        Set(value As String)
            _cliente = value
        End Set
    End Property

    Public Property Produto As String
        Get
            Return _produto
        End Get
        Set(value As String)
            _produto = value
        End Set
    End Property

    Public Property Tipo As String
        Get
            Return _tipo
        End Get
        Set(value As String)
            _tipo = value
        End Set
    End Property

    Public Property Nrseq As Integer
        Get
            Return _nrseq
        End Get
        Set(value As Integer)
            _nrseq = value
        End Set
    End Property

    Public Property Img As String
        Get
            Return _img
        End Get
        Set(value As String)
            _img = value
        End Set
    End Property

    Public Property Listaclasse As List(Of clslogin)
        Get
            Return _listaclasse
        End Get
        Set(value As List(Of clslogin))
            _listaclasse = value
        End Set
    End Property

    Public Property Url As String
        Get
            Return _url
        End Get
        Set(value As String)
            _url = value
        End Set
    End Property

    Public Property Nrseqperfil As Integer
        Get
            Return _nrseqperfil
        End Get
        Set(value As Integer)
            _nrseqperfil = value
        End Set
    End Property

    Public Property Perfil As String
        Get
            Return _perfil
        End Get
        Set(value As String)
            _perfil = value
        End Set
    End Property

    Public Function logar() As Boolean

        Try

            Dim tblogin As New Data.DataTable
            Dim tablogin As New clsBanco

            If _usuario = "" Then
                _mensagemerro = "Digite um nome válido  !"
                Return False
            End If



            tblogin = tablogin.conectar("Select * from tbusuarios where usuario = '" & _usuario & "' or email = '" & _usuario & "'")


            If tblogin.Rows.Count = 0 Then
                _mensagemerro = "O usuário não existe !"
                Return False
            Else

                If tblogin.Rows(0)("senha").ToString <> _senha Then
                    _mensagemerro = "Senha inválida !"
                    Return False
                End If
                If tblogin.Rows(0)("ativo").ToString = "0" Then
                    _mensagemerro = "Acesso Revogado: Seu acesso foi removido !"
                    Return False
                End If
                If tblogin.Rows(0)("suspenso").ToString = "1" Then
                    _mensagemerro = "Acesso Revogado: Sua conta está suspensa. Contate o administrador !"
                    Return False
                End If
                If tblogin.Rows(0)("master").ToString = "1" Then
                    HttpContext.Current.Session("usuariomaster") = "sim"
                Else
                    HttpContext.Current.Session("usuariomaster") = ""
                End If



                HttpContext.Current.Session("id") = String.Format("Session ID: {0}", HttpContext.Current.Session.SessionID)
                HttpContext.Current.Session("clienteID") = tblogin.Rows(0)("nrseq").ToString

                HttpContext.Current.Session("usuario") = tblogin.Rows(0)("usuario").ToString
                HttpContext.Current.Session("email") = tblogin.Rows(0)("Email").ToString
                HttpContext.Current.Session("nrseqtipoapontamento") = numeros(tblogin.Rows(0)("nrseqtipoapontamento").ToString)

                HttpContext.Current.Session("imagemperfil") = tblogin.Rows(0)("imagemperfil").ToString

                If tblogin.Rows(0)("alterado").ToString <> "True" AndAlso tblogin.Rows(0)("alterado").ToString <> "1" Then
                    HttpContext.Current.Session("usuarioalterado") = "não"
                Else
                    HttpContext.Current.Session("usuarioalterado") = "sim"
                End If


                Dim aCookie As New HttpCookie("smrastreamento")
                aCookie.Values("id") = HttpContext.Current.Session("id")
                aCookie.Values("nomeusuario") = HttpContext.Current.Session("nomeusuario")
                aCookie.Values("lastVisit") = DateTime.Now.ToString()
                aCookie.Values("usuario") = HttpContext.Current.Session("usuario")
                aCookie.Values("email") = HttpContext.Current.Session("email")
                aCookie.Values("clienteID") = HttpContext.Current.Session("clienteID")
                aCookie.Values("tipo") = HttpContext.Current.Session("tipo")

                aCookie.Values("usuariomaster") = HttpContext.Current.Session("usuariomaster")
                aCookie.Values("usuarioalterado") = HttpContext.Current.Session("usuarioalterado")
                aCookie.Values("imagemperfil") = HttpContext.Current.Session("imagemperfil")

                aCookie.Expires = DateTime.Now.AddDays(4)
                HttpContext.Current.Response.Cookies.Add(aCookie)
                If HttpContext.Current.Session("usuarioalterado") = "não" Then
                    _urlretorno = "restrito/seguranca.aspx"
                    Return True
                End If




                Dim tblog As New Data.DataTable
                Dim tablog As New clsBanco
                tblog = tablog.IncluirAlterarDados("insert into tblog(usercad,dtcad,hora,acao) values ('" & HttpContext.Current.Session("usuario") & "','" & Formatadatamysql(Date.Now.Date) & "','" & TimeOfDay.AddHours(4) & "','Entrou no sistema')")


                Dim tbx As New Data.DataTable
                Dim tabx As New clsBanco

                tbx = tabx.conectar("select * from tbperfil where nrseq = " & numeros(tblogin.Rows(0)("nrseqperfil").ToString))

                Perfil = tbx.Rows(0)("descricao").ToString

                Select Case Perfil
                    Case Is = "EMPLOEE"
                        _urlretorno = "restrito/relatoriolog.aspx"
                    Case Else
                        _urlretorno = "login.aspx"
                End Select

            End If
            Return True
        Catch ex As Exception
            Mensagemerro = ex.Message
            Return False
        End Try
        Return True
    End Function

    Public Function salvausuariosenha() As Boolean
        Try
            Dim tb1 As New Data.DataTable
            Dim tab1 As New clsBanco

            Dim xsql As String
            xsql = " update tbusuarios Set ativo = True"

            If Usuario <> "" Then
                xsql &= ",usuario = '" & tratatexto(Usuario) & "'"
            End If
            If Senha <> "" Then
                xsql &= ",senha = '" & tratatexto(Senha) & "'"
            End If

            xsql &= " where nrseq = " & Nrseq
            tb1 = tab1.IncluirAlterarDados(xsql)
            Return True
        Catch exsalvar As Exception
            Mensagemerro = exsalvar.Message
        Return False
        End Try

    End Function

    Public Function verifica() As Boolean
        Try
            Dim tb1 As New Data.DataTable
            Dim tab1 As New clsBanco
            tb1 = tab1.conectar("select * from vwclientes_usuarios where usuario = '" & Usuario & "'")
            _senha = tb1.Rows(0)("senha").ToString
        Catch ex As Exception
            Mensagemerro = ex.Message
            Return False
        End Try
        Return True
    End Function
    Private Function trataretornosmart(resultado As String) As Boolean
        Select Case resultado.ToLower
            Case Is = "concluído"
                Return True
            Case Is = "Erro base de dados"
                Return True
            Case Is = "cliente bloqueado"
                _mensagemerro = "Seu acesso ao sistema está suspenso! Por favor, entre em contato com o suporte!"
                Return False
        End Select
    End Function

End Class
