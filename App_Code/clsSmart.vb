Imports Microsoft.VisualBasic
Imports clsSmart
Imports System.Data
Imports System.IO
Imports System.Net.Mail
Imports System.Xml
Imports System.IO.Directory
Public Class clsSmart
    Enum TpBancoDestino
        SQLSERVER
        MYSQL
    End Enum
    Enum tpOperacaoComparaTabelas
        igual
        diferente
    End Enum
    Enum gravardata
        gravar
        recuperar
    End Enum
    Enum tipoestado
        sigla
        nome
    End Enum
    Public Shared meucliente As String = "Rastreamento"
    Public Shared meuproduto As String = "Rastreamento"
    Private Shared meubanco As New clsBanco
    Public Shared minhaversao As String = "1.01  (release " & sonumeros(meubanco.servidor) & ")"
    Public Shared Function valordata(xdata As String, Optional xformatadoObjData As Boolean = False) As String
        If IsDate(xdata) = False Then xdata = "01/01/1900"
        If xformatadoObjData Then
            Return CDate(xdata).ToString("yyyy-MM-dd")
        End If
        Return FormatDateTime(xdata, DateFormat.ShortDate)

    End Function
    Public Shared Function numeros(valor As String, Optional v As Boolean = False) As Decimal
        If valor = "" OrElse Not IsNumeric(valor) Then
            Return 0
        End If
        Return CType(valor, Decimal)
    End Function
    Public Shared Function tratanumeros(ByVal expressao As String) As String
        If expressao = "" Then
            expressao = 0
        End If
        expressao = Replace(expressao, ",", ".")
        Return expressao
    End Function
    Public Shared Function SearchForFiles(ByVal RootFolder As String, ByVal FileFilter() As String) As List(Of String)
        Dim ReturnedData As New List(Of String)                             'List to hold the search results
        Dim FolderStack As New Stack(Of String)                             'Stack for searching the folders
        FolderStack.Push(RootFolder)                                        'Start at the specified root folder
        Do While FolderStack.Count > 0                                      'While there are things in the stack
            Dim ThisFolder As String = FolderStack.Pop                      'Grab the next folder to process
            Try                                                             'Use a try to catch any errors
                For Each SubFolder In GetDirectories(ThisFolder)            'Loop through each sub folder in this folder
                    FolderStack.Push(SubFolder)                             'Add to the stack for further processing
                Next                                                        'Process next sub folder
                For Each FileExt In FileFilter                              'For each File filter specified


                    ReturnedData.AddRange(GetFiles(ThisFolder, FileExt))    'Search for and return the matched file names


                Next                                                        'Process next FileFilter
            Catch ex As Exception                                           'For simplicity sake
            End Try                                                         'We'll ignore the errors
        Loop                                                                'Process next folder in the stack
        Return ReturnedData                                                 'Return the list of files that match
    End Function
    Public Shared Function encontrachr(ByVal texto As String, ByVal caract As String, ByVal posicao As Integer) As String
        Dim y As Integer
        Dim posicoes As Integer
        Dim ultima As Integer
        Dim achou As Boolean
        Dim retorno As String
        posicoes = 1
        ultima = 0
        achou = False
        retorno = ""
        For y = 1 To Len(texto)

            If Mid(texto, y, 1) = caract Then
                If posicoes = posicao Then
                    retorno = Mid(texto, IIf(ultima = 0, 1, ultima), IIf(ultima = 0, y - ultima - 1, y - ultima))
                    achou = True
                    Exit For
                Else
                    achou = False
                End If
                posicoes = posicoes + 1
                ultima = y + 1
            End If
        Next
        If achou = False Then
            retorno = Mid(texto, IIf(ultima = 0, 1, ultima), IIf(ultima = 0, y - ultima - 1, y - ultima))
        End If
        encontrachr = retorno
    End Function
    Public Shared Function lerxml2(documento As XmlDocument, retorno As Boolean) As Data.DataTable
        Dim tbdados As New Data.DataTable
        tbdados.Columns.Clear()
        tbdados.Rows.Clear()
        If documento.ChildNodes.Count = 0 Then
            tbdados.Columns.Add("Erro")
            tbdados.Rows.Add()
            tbdados.Rows(0)("Erro") = ("Nenhum registro localizado !")
            Return tbdados
        End If
        For Y As Integer = 0 To documento.ChildNodes(0).ChildNodes(0).ChildNodes.Count - 1
            If tbdados.Columns.Contains(documento.ChildNodes(0).ChildNodes(0).ChildNodes(Y).Name) Then
                Exit For
            End If
            ' Debug.Print(documento.ChildNodes(0).ChildNodes(0).ChildNodes(Y).Name)
            tbdados.Columns.Add(documento.ChildNodes(0).ChildNodes(0).ChildNodes(Y).Name)
        Next
        'MsgBox(documento.ChildNodes(0).ChildNodes(0).ChildNodes.Count)
        '   MsgBox(tbdados.Columns.Count)
        For x As Integer = 0 To documento.ChildNodes(0).ChildNodes.Count - 1
            tbdados.Rows.Add()
            '  wtenviasite_clientes_atual = "Convertendo linha :" & x & " de " & documento.ChildNodes(0).ChildNodes.Count & " linhas"
            For Y As Integer = 0 To documento.ChildNodes(0).ChildNodes(0).ChildNodes.Count - 1
                'Debug.Print(documento.ChildNodes(0).ChildNodes(0).ChildNodes(Y).Name)
                'Debug.Print(documento.ChildNodes(0).ChildNodes(0).ChildNodes.Count)
                'Debug.Print(documento.ChildNodes(0).ChildNodes(0).ChildNodes(Y).InnerText)
                tbdados.Rows(x)(documento.ChildNodes(0).ChildNodes(0).ChildNodes(Y).Name) = documento.ChildNodes(0).ChildNodes(x).ChildNodes(Y).InnerText
            Next
        Next
        'Debug.Print(tbdados.Rows.Count)
        Return tbdados
    End Function

    Public Shared Function lerxml2(documento As XmlDocument) As Data.DataTable

        Try



            Dim tbdados As New Data.DataTable
            tbdados.Columns.Clear()
            tbdados.Rows.Clear()
            If documento.ChildNodes.Count = 0 Then
                tbdados.Columns.Add("Erro")
                tbdados.Rows.Add()
                tbdados.Rows(0)("Erro") = ("Nenhum registro localizado !")
                Return tbdados
            End If
            For Y As Integer = 0 To documento.ChildNodes(1).ChildNodes(0).ChildNodes.Count - 1
                If tbdados.Columns.Contains(documento.ChildNodes(1).ChildNodes(0).ChildNodes(Y).Name) Then
                    Exit For
                End If

                tbdados.Columns.Add(documento.ChildNodes(1).ChildNodes(0).ChildNodes(Y).Name)
            Next
            'MsgBox(documento.ChildNodes(0).ChildNodes(0).ChildNodes.Count)
            '   MsgBox(tbdados.Columns.Count)
            For x As Integer = 0 To 20 ' documento.ChildNodes(0).ChildNodes(0).ChildNodes.Count - 1
                tbdados.Rows.Add()

                For Y As Integer = 0 To documento.ChildNodes(1).ChildNodes(0).ChildNodes.Count - 1
                    'Debug.Print(documento.ChildNodes(0).ChildNodes(0).ChildNodes(Y).Name)
                    'Debug.Print(documento.ChildNodes(0).ChildNodes(0).ChildNodes.Count)
                    'Debug.Print(documento.ChildNodes(0).ChildNodes(0).ChildNodes(Y).InnerText)
                    tbdados.Rows(x)(documento.ChildNodes(1).ChildNodes(0).ChildNodes(Y).Name) = documento.ChildNodes(1).ChildNodes(x).ChildNodes(Y).InnerText
                Next
            Next
            'Debug.Print(tbdados.Rows.Count)
            Return tbdados
        Catch ex As Exception
            Dim lixo1 = ex.Message
            lixo1 &= "1"
        End Try
    End Function
    Public Shared Function lerxml2(doc1 As String) As Data.DataTable
        Try

            Dim documento As New XmlDocument
            documento.Load(doc1)

            Dim tbdados As New Data.DataTable
            tbdados.Columns.Clear()
            tbdados.Rows.Clear()
            If documento.ChildNodes.Count = 0 Then
                tbdados.Columns.Add("Erro")
                tbdados.Rows.Add()
                tbdados.Rows(0)("Erro") = ("Nenhum registro localizado !")
                Return tbdados
            End If
            For Y As Integer = 0 To documento.ChildNodes(0).ChildNodes(0).ChildNodes.Count - 1
                If tbdados.Columns.Contains(documento.ChildNodes(0).ChildNodes(0).ChildNodes(Y).Name) Then
                    Exit For
                End If

                tbdados.Columns.Add(documento.ChildNodes(0).ChildNodes(0).ChildNodes(Y).Name)
            Next
            'MsgBox(documento.ChildNodes(0).ChildNodes(0).ChildNodes.Count)
            '   MsgBox(tbdados.Columns.Count)
            For x As Integer = 0 To documento.ChildNodes(0).ChildNodes.Count - 1
                tbdados.Rows.Add()

                For Y As Integer = 0 To documento.ChildNodes(0).ChildNodes(0).ChildNodes.Count - 1
                    'Debug.Print(documento.ChildNodes(0).ChildNodes(0).ChildNodes(Y).Name)
                    'Debug.Print(documento.ChildNodes(0).ChildNodes(0).ChildNodes.Count)
                    'Debug.Print(documento.ChildNodes(0).ChildNodes(0).ChildNodes(Y).InnerText)
                    tbdados.Rows(x)(documento.ChildNodes(0).ChildNodes(0).ChildNodes(Y).Name) = documento.ChildNodes(0).ChildNodes(x).ChildNodes(Y).InnerText
                Next
            Next
            'Debug.Print(tbdados.Rows.Count)
            Return tbdados

        Catch ex2 As Exception
            Threading.Thread.Sleep(10)
        End Try
    End Function
    Public Shared Function mARQUIVO(ByVal wcCAMINHO As String, Optional ByVal web As Boolean = False) As String
        Dim xx As Integer
        Dim simbolo As String = "\"
        If web Then
            simbolo = "/"
        End If
        For xx = 1 To Microsoft.VisualBasic.Len(wcCAMINHO)
            'Debug.Print(Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(wcCAMINHO, xx + 1), 1))
            If Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(wcCAMINHO, xx + 1), 1) = simbolo Then
                mARQUIVO = Microsoft.VisualBasic.Right(wcCAMINHO, xx)
                xx = Microsoft.VisualBasic.Len(wcCAMINHO)
                Exit For
            End If
        Next

    End Function
    Public Shared Function alteranome(nome As String) As String
        Dim novonome As String = mPASTA(nome) & encontrachr(mARQUIVO(nome), ".", 1) & "(" & Directory.GetFiles(mPASTA(nome), (encontrachr(mARQUIVO(nome), ".", 1) & "*." & encontrachr(mARQUIVO(nome), ".", 0))).Length & ")" & "." & encontrachr(mARQUIVO(nome), ".", 0)
        Return novonome
    End Function
    Public Shared Function logico(valor As String, tela As Boolean) As String

        If valor = "1" OrElse valor.ToLower = "true" Then
            Return "Sim"
        Else
            Return "Não"
        End If
    End Function

    Public Shared Function mPASTA(ByVal wcCAMINHO As String) As String
        Dim wctemp
        Dim xx As Integer
        For xx = 1 To wcCAMINHO.Length
            If Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(wcCAMINHO, xx + 1), 1) = "\" Then
                wctemp = (Microsoft.VisualBasic.Right(wcCAMINHO, xx).Length)
                mPASTA = Microsoft.VisualBasic.Left(wcCAMINHO, (wcCAMINHO.Length) - wctemp)
                xx = (wcCAMINHO.Length)
            End If
        Next

    End Function

    Public Shared Function tratadata(data As String, Optional tipo As gravardata = gravardata.gravar) As String
        If IsDate(data) = False Then data = Date.Now.Date
        If tipo = gravardata.gravar Then
            Return Format(CType(data, Date), "MM/dd/yyyy")
        Else
            Return Format(CType(data, Date), "dd/MM/yyyy")
        End If
    End Function
    Public Shared Function tratamoeda(ByVal expressao As String) As String
        If expressao = "" Then
            Return "0"
        Else
            expressao = Replace(expressao, ".", "")
            ' expressao = FormatNumber(expressao, 2)
            expressao = Replace(expressao, ",", ".")
        End If

        Return expressao


    End Function
    Public Shared Function validadata(ByVal Texto As String) As Boolean
        Dim Dia As String
        Dim Mes As String
        Dim Ano As String
        Texto = Replace(Texto, "/", "")
        Dia = Left(Texto, 2)
        Mes = Mid(Texto, 3, 2)
        Ano = Right(Texto, 4)
        Try
            Dim data As String = CDate(Mes & "/" & Dia & "/" & Ano)
            If IsDate(data) = True Then
                validadata = True
            Else
                validadata = False
            End If
        Catch ex As Exception
            validadata = False
        End Try


    End Function
    Public Shared Matricula As String
    Public Shared usuariologado As String
    Public Shared Function tratatexto(ByVal texto As String, Optional comaspas As Boolean = True, Optional tamanho As Integer = 0) As String
        Try
            If comaspas Then
                Return IIf(tamanho = 0, texto.Replace("'", "''"), mLeft(texto.Replace("'", "''"), tamanho))
            Else
                Return IIf(tamanho = 0, texto.Replace("'", ""), mLeft(texto.Replace("'", ""), tamanho))
            End If
        Catch ex As Exception
            Return ""
        End Try



    End Function
    Public Shared Function tratatexto(ByVal texto As String, comaspas As Boolean) As String
        Try
            If comaspas Then
                Return texto.Replace("'", "''")
            Else
                Return texto.Replace("'", "")
            End If
        Catch ex As Exception
            Return ""
        End Try



    End Function

    Public Shared Function Formatadatamysql(ByVal data As String, Optional ByVal inverter As Boolean = False)
        Dim novadata As Date
        If IsDate(data) = False Then
            novadata = CDate("01/01/1900")
        Else
            novadata = CDate(data)
        End If
        If Not inverter Then
            Return Format(novadata, "yyyy/MM/dd")
        Else
            Return Format(novadata, "dd/MM/yyyy")
        End If
    End Function
    Public Shared Function Moeda(ByVal valor As String, Optional ByVal tela As Boolean = False)

        Dim negativo As Boolean = False
        If valor = "" Then
            Moeda = 0
            Exit Function
        End If
        'If valor.Contains("-") = True Then
        '    negativo = True
        'End If
        Dim novovalor As String = valor.Replace("R$", "").Replace(".", "")
        If tela = False Then
            Return novovalor.Replace(",", ".").Trim
        Else
            Return novovalor
        End If
    End Function
    Public Shared Function zeros(ByVal expr As String, ByVal qtd As Integer) As String
        Dim dd As String
        Dim x As Integer
        dd = ""
        If qtd <= Len(expr) Then
            Return expr
            Exit Function
        End If
        For x = 1 To (qtd - Len(expr))
            dd = dd & "0"
        Next
        Return dd & expr

    End Function
    Public Shared Function sonumeros(ByVal valor As String) As String
        Dim wcnumero As String, x As Integer
        wcnumero = ""
        For x = 1 To Len(valor)
            If IsNumeric(Mid(valor, x, 1)) Then
                wcnumero = wcnumero & Mid(valor, x, 1)
            End If
        Next
        sonumeros = (wcnumero)
    End Function
    Public Shared Function gerarnrseqcontrole() As String
        Dim var As String = ""

        var = "Semusuario" & Replace(Date.Now.ToString, "/", "")

        'var = HttpContext.Current.Session("usuario") & Replace(Date.Now.ToString, "/", "")



        var = Replace(var, ":", "")
        Return var
    End Function
    Public Shared Function mLeft(ByVal expressao As String, ByVal qtd As Integer) As String
        Return Microsoft.VisualBasic.Left(expressao, qtd)
    End Function
    Public Shared Function mRight(ByVal expressao As String, ByVal qtd As Integer) As String
        Return Microsoft.VisualBasic.Right(expressao, qtd)
    End Function
    Public Shared Function mmeio(ByVal expressao As String, ByVal inicio As Integer, ByVal qtd As Integer) As String
        Return Microsoft.VisualBasic.Mid(expressao, inicio, qtd)
    End Function
    Public Shared Function data() As Date

        Return Date.Now.Date
    End Function
    Public Shared Function data(nome As Boolean) As String

        Return Year(Date.Now.Date) & zeros(Date.Now.Date.Month, 2) & zeros(Date.Now.Date.Day, 2) & "_" & zeros(Date.Now.TimeOfDay.Hours, 2) & zeros(Date.Now.TimeOfDay.Minutes, 2) & zeros(Date.Now.TimeOfDay.Seconds, 2)


    End Function
    Public Shared Function hora(Optional segundos As Boolean = False) As String

        Return IIf(Not segundos, mLeft(Date.Now.TimeOfDay.ToString, 5), mLeft(Date.Now.TimeOfDay.ToString, 8))

    End Function

    Public Shared Function hoje() As String
        Return Formatadatamysql(data())
    End Function
    Public Shared Function logico(valor As String) As String

        If valor = "1" OrElse valor.ToLower = "true" Then
            Return "True"
        Else
            Return "False"
        End If
    End Function
End Class



Public Class DataSetHelper
    Public ds As DataSet




End Class



























