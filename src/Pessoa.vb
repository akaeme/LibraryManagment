<Serializable()> Public Class Pessoa
    Private _idPessoa As String
    Private _primeiroNome As String
    Private _ultimoNome As String
    Private _morada As String
    Private _tlm As String
    Private _cc As String
    Private _nif As String
    Private _dataNasc As String
    Private _genero As String

    Property IDPessoa As String
        Get
            Return _idPessoa
        End Get
        Set(value As String)
            _idPessoa = value
        End Set
    End Property


    Property PrimeiroNome() As String
        Get
            PrimeiroNome = _primeiroNome
        End Get
        Set(value As String)
            _primeiroNome = value
        End Set
    End Property


    Property UltimoNome() As String
        Get
            UltimoNome = _ultimoNome
        End Get
        Set(value As String)
            _ultimoNome = value
        End Set
    End Property

    Property Morada() As String
        Get
            Morada = _morada
        End Get
        Set(value As String)
            _morada = value
        End Set
    End Property

    Property Tlm() As String
        Get
            Tlm = _tlm
        End Get
        Set(value As String)
            _tlm = value
        End Set
    End Property

    Property Cc() As String
        Get
            Cc = _cc
        End Get
        Set(value As String)
            _cc = value
        End Set
    End Property

    Property Nif() As String
        Get
            Nif = _nif
        End Get
        Set(value As String)
            _nif = value
        End Set
    End Property

    Property DataNasc() As String
        Get
            DataNasc = _dataNasc
        End Get
        Set(value As String)
            _dataNasc = value
        End Set
    End Property

    Property Genero() As String
        Get
            Genero = _genero
        End Get
        Set(value As String)
            _genero = value
        End Set
    End Property

    Overrides Function ToString() As String
        Return _idPessoa & "   " & _primeiroNome & "   " & _ultimoNome
    End Function
    Function teste() As String
        Return _idPessoa & "   " & _primeiroNome
    End Function
    Public Sub New()
        MyBase.New()
    End Sub
End Class
