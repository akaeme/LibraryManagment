<Serializable()> Public Class Biblioteca
    Private _codBiblioteca As String
    Private _nome As String
    Private _endereco As String

    Property CodBiblioteca As String
        Get
            Return _codBiblioteca
        End Get
        Set(value As String)
            _codBiblioteca = value
        End Set
    End Property

    Property Nome() As String
        Get
            Nome = _nome
        End Get
        Set(value As String)
            _nome = value
        End Set
    End Property

    Property Endereco() As String
        Get
            Endereco = _endereco
        End Get
        Set(value As String)
            _endereco = value
        End Set
    End Property

    Overrides Function ToString() As String
        Return _codBiblioteca & "   " & _nome
    End Function

    Public Sub New()
        MyBase.New()
    End Sub
End Class
