<Serializable()> Public Class Leitor
    Inherits Pessoa
    Private _username As String
    Private _password As String
    Private _codBiblioteca As String
    Private _dataExpiro As String
    Private _dataRegisto As String
    Private _codLeitor As String

    Property CodLeitor As String
        Get
            Return _codLeitor
        End Get
        Set(value As String)
            _codLeitor = value
        End Set
    End Property

    Property CodBiblioteca() As String
        Get
            CodBiblioteca = _codBiblioteca
        End Get
        Set(value As String)
            _codBiblioteca = value
        End Set
    End Property

    Property Username() As String
        Get
            Username = _username
        End Get
        Set(value As String)
            _username = value
        End Set
    End Property

    Property Password() As String
        Get
            Password = _password
        End Get
        Set(value As String)
            _password = value
        End Set
    End Property

    Property DataExpiro() As String
        Get
            DataExpiro = _dataExpiro
        End Get
        Set(value As String)
            _dataExpiro = value
        End Set
    End Property

    Property DataRegisto() As String
        Get
            DataRegisto = _dataRegisto
        End Get
        Set(value As String)
            _dataRegisto = value

        End Set
    End Property

    Overrides Function ToString() As String
        Return _codLeitor & "   " & MyBase.PrimeiroNome & "   " & MyBase.UltimoNome
    End Function
    Function teste1() As String
        Return _codLeitor & "   " & MyBase.PrimeiroNome
    End Function
    Public Sub New()
        MyBase.New()
    End Sub
End Class

