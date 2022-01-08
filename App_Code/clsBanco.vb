Imports Microsoft.VisualBasic
Imports System.IO
Imports clsSmart
Public Class clsBanco

    Private _banco_conexao As MySql.Data.MySqlClient.MySqlConnection
    Private _banco_comando As MySql.Data.MySqlClient.MySqlCommand
    Private _banco_adaptador As MySql.Data.MySqlClient.MySqlDataAdapter
    Private _tabela_dataset As New Data.DataSet
    Dim _cliente As String
    Dim _banco As String
    Private _tabela_nome As String
    Private _sql As String
    Private _erro As String
    Dim _mensagemerro As String

    Protected _versaoteste As Boolean = False

    Protected _dados_servidor As String = "dbrastreamento.mysql.database.azure.com"
    Protected _dados_banco As String = "dbrastreamento"
    Protected _dados_usuario As String = "azadmin"
    Protected _dados_senha As String = "Kr@@02112001"
    Private Function buscarchave() As Boolean
        Try
            If HttpContext.Current Is Nothing Then Exit Function

            Dim xpasta As String = HttpContext.Current.Server.MapPath("\chaves")
            If System.IO.Directory.GetFiles(xpasta).Count > 0 Then
                Dim tbx As New Data.DataTable
                tbx.TableName = "chave"

                tbx = lerxml2(System.IO.Directory.GetFiles(xpasta)(0))

                If tbx.Rows.Count > 0 Then
                    _dados_servidor = tbx.Rows(0)("servidor").ToString
                    _dados_banco = tbx.Rows(0)("banco").ToString
                    _dados_usuario = tbx.Rows(0)("usuario").ToString
                    _dados_senha = tbx.Rows(0)("senha").ToString
                End If
                Return True
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Sub New()
        buscarchave()


        Dim xcls As New clslogin
        Cliente = xcls.Cliente
        meuproduto = xcls.Produto
        _banco = _dados_banco
    End Sub
    Public Sub New(xuser As String)
        buscarchave()
        Dim xcls As New clslogin
        Cliente = xcls.Cliente
        meuproduto = xcls.Produto
        _banco = _dados_banco
    End Sub

    Public Overridable ReadOnly Property conexao As String
        Get
            Return "server=" & _dados_servidor & ";Database=" & _dados_banco & ";UID=" & _dados_usuario & ";PWD=" & _dados_senha & ";PORT = 3306;allow zero datetime=true;Connect Timeout=9000"
        End Get
    End Property
    Public Overridable ReadOnly Property servidor As String
        Get
            Return _dados_servidor
        End Get
    End Property
    Public Overridable ReadOnly Property servidorusado As String
        Get

            Dim tbtesteserver As New Data.DataTable
            tbtesteserver = Me.conectar("select * from tbusuarios")
            If tbtesteserver.Rows.Count > 0 Then
                Return _dados_servidor & " online"
            Else
                Return _dados_servidor & " offline"
            End If
        End Get
    End Property
    Public Property ExecutaSql() As String
        Get
            Return _sql
        End Get
        Set(ByVal value As String)
            _sql = value
        End Set
    End Property
    Public Property erro() As String
        Get
            Return _erro
        End Get
        Set(ByVal value As String)
            _erro = value
        End Set
    End Property

    Public ReadOnly Property conectar(Optional sqlquery As String = "") As Data.DataTable
        Get
            If sqlquery <> "" Then _sql = sqlquery
            If _sql = "" Then Return Nothing
            If _tabela_dataset.Tables.Count > 0 Then
                _tabela_dataset.Tables.Clear()

            End If


            _tabela_dataset.Clear()
            _banco_conexao = New MySql.Data.MySqlClient.MySqlConnection(conexao)
            _banco_comando = New MySql.Data.MySqlClient.MySqlCommand(_sql, _banco_conexao)
            _banco_adaptador = New MySql.Data.MySqlClient.MySqlDataAdapter(_banco_comando)
            'rs.Open(_sql, _strcon)
            Try
                _banco_comando.CommandTimeout = 0
                _banco_adaptador.Fill(_tabela_dataset)
                If _sql.Substring(0, 1).ToUpper = "S" Then
                    NomeDaTabela = _tabela_dataset.Tables(0).TableName

                    _tabela_dataset.Tables(0).Namespace = _sql
                    Return _tabela_dataset.Tables(0)
                End If
            Catch ex As Exception

                Dim resultadoenvioemail As String


                Dim logerro As New StreamWriter(HttpContext.Current.Server.MapPath("~") & "logerros\errosql" & sonumeros(HttpContext.Current.Request.UserHostAddress) & sonumeros(data) & sonumeros(hora(True)) & ".txt")
                logerro.WriteLine(_sql)
                logerro.WriteLine("Log de erro - Data: " & data() & " / Hora: " & hora(True) & " Mensagem: " & ex.Message)

                logerro.WriteLine("Página: " & HttpContext.Current.Request.Url.Segments(HttpContext.Current.Request.Url.Segments.Count - 1))
                logerro.WriteLine("Usuário: " & HttpContext.Current.Session("usuario"))
                logerro.Close()
                If Not ex.Message.Contains("Unable to connect to any of the specified MySQL host") Then
                    HttpContext.Current.Response.Redirect("\aviso.aspx?mensagem=Erro de acesso à base de dados !" & ex.Message, True)

                End If


            Finally
                _banco_conexao.Close()
            End Try



            Return Nothing
        End Get
    End Property

    Public ReadOnly Property IncluirAlterarDados(Optional sqlquery As String = "") As Data.DataTable
        Get
            If sqlquery <> "" Then _sql = sqlquery
            If _sql = "" Then Return Nothing
            Try

                If _tabela_dataset.Tables.Count > 0 Then
                    _tabela_dataset.Tables(0).Rows.Clear()
                    _tabela_dataset.Tables(0).Columns.Clear()
                End If
                _tabela_dataset.Clear()
                _banco_conexao = New MySql.Data.MySqlClient.MySqlConnection(conexao)
                _banco_comando = New MySql.Data.MySqlClient.MySqlCommand(_sql, _banco_conexao)
                _banco_adaptador = New MySql.Data.MySqlClient.MySqlDataAdapter(_banco_comando)
                'rs.Open(_sql, _strcon)
                _banco_comando.CommandTimeout = 0
                _banco_adaptador.Fill(_tabela_dataset)
                If _sql.Substring(0, 1).ToUpper = "S" Then
                    NomeDaTabela = _tabela_dataset.Tables(0).TableName
                Else
                    _tabela_dataset.Tables.Add()
                    _tabela_dataset.Tables(0).Namespace = _sql
                    _tabela_dataset.Tables(0).Columns.Add("operacao")
                    _tabela_dataset.Tables(0).Rows.Add()
                    _tabela_dataset.Tables(0).Rows(_tabela_dataset.Tables(0).Rows.Count - 1)("operacao") = _sql

                End If


                Return _tabela_dataset.Tables(0)
            Catch ex As Exception
                Mensagemerro = ex.Message
                Dim resultadoenvioemail As String

                Dim logerro As New StreamWriter(HttpContext.Current.Server.MapPath("~") & "\logerros\errosql" & sonumeros(HttpContext.Current.Request.UserHostAddress) & sonumeros(data) & sonumeros(hora(True)) & ".txt")
                logerro.WriteLine(_sql)
                logerro.WriteLine("Log de erro - Data: " & data() & " / Hora: " & hora(True) & " Mensagem: " & ex.Message)
                logerro.WriteLine("Resultado notificação por e-mail " & resultadoenvioemail)
                logerro.WriteLine("Página: " & HttpContext.Current.Request.Url.Segments(HttpContext.Current.Request.Url.Segments.Count - 1))
                logerro.WriteLine("Usuário: " & HttpContext.Current.Session("usuario"))
                logerro.Close()
                If Not ex.Message.Contains("Unable to connect to any of the specified MySQL host") Then
                    HttpContext.Current.Response.Redirect("\aviso.aspx?mensagem=Erro de acesso à base de dados !" & ex.Message, True)

                End If

            Finally
                _banco_conexao.Close()
            End Try
        End Get
    End Property

    Public Property NomeDaTabela() As String
        Get
            Return _tabela_nome
        End Get
        Set(ByVal value As String)
            _tabela_nome = value
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

    Public Property Banco As String
        Get
            Return _banco
        End Get
        Set(value As String)
            _banco = value
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
End Class
