Imports Microsoft.VisualBasic

Public Class clscookies
    Dim _valor As String
    Dim _chave As String

    Public Property Valor As String
        Get
            Return _valor
        End Get
        Set(value As String)
            _valor = value
        End Set
    End Property

    Public Property Chave As String
        Get
            Return _chave
        End Get
        Set(value As String)
            _chave = value
        End Set
    End Property
End Class
