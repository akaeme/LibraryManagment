<Serializable()> Public Class Editora
    Private _codEditora As String
    Private _nomeEditora As String
    Private _endereco As String
    Private _telefone As String

    Property CodEditora As String
        Get
            Return _codEditora
        End Get
        Set(value As String)
            _codEditora = value
        End Set
    End Property

    Property NomeEditora() As String
        Get
            NomeEditora = _nomeEditora
        End Get
        Set(value As String)
            _nomeEditora = value
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

    Property Telefone() As String
        Get
            Telefone = _telefone
        End Get
        Set(value As String)
            _telefone = value
        End Set
    End Property

    Overrides Function ToString() As String
        Return _codEditora & "   " & _nomeEditora
    End Function

    Public Sub New()
        MyBase.New()
    End Sub
End Class
