Imports Microsoft.VisualBasic
Imports System.IO
Public Class clsredimensiona


    Dim _arquivo As String
    Dim _max As Integer = 16
    Dim _novaaltura As Integer = 0
    Dim _novalargura As Integer = 0

    Public Property Arquivo As String
        Get
            Return _arquivo
        End Get
        Set(value As String)
            _arquivo = value
        End Set
    End Property

    Public Property Novaaltura As Integer
        Get
            Return _novaaltura
        End Get
        Set(value As Integer)
            _novaaltura = value
        End Set
    End Property

    Public Property Novalargura As Integer
        Get
            Return _novalargura
        End Get
        Set(value As Integer)
            _novalargura = value
        End Set
    End Property

    Public Property Max As Integer
        Get
            Return _max
        End Get
        Set(value As Integer)
            _max = value
        End Set
    End Property

    Public Function redimensionar() As Boolean
        Try

            If Not File.Exists(Arquivo) Then
                Return False
            End If
            Dim img As System.Drawing.Image = System.Drawing.Image.FromFile(Arquivo)
            Dim imgaltura As Decimal = img.PhysicalDimension.Height
            Dim imglargura As Decimal = img.PhysicalDimension.Width
            Dim fator As Decimal = Int((imgaltura / imglargura) * _max)
            If (img.Width >= img.Height) Then
                fator = (_max / img.Width)
            Else
                fator = (_max / img.Height)
            End If
            Novaaltura = CInt(img.Height * fator)
            Novalargura = CInt(img.Width * fator)
            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function




End Class
