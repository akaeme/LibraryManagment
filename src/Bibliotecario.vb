<Serializable()> Public Class Bibliotecario
    Inherits Pessoa
    Private _codBiblioteca As String
    Private _codBibliotecario As String
    Private _salary As String
    Private _password As String
    Private _username As String

    Property CodBibliotecario As String
        Get
            Return _codBibliotecario
        End Get
        Set(value As String)
            _codBibliotecario = value
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

    Property Salary() As String
        Get
            Salary = _salary
        End Get
        Set(value As String)
            _salary = value
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

    Overrides Function ToString() As String
        Return _codBibliotecario & "   " & MyBase.PrimeiroNome & "   " & MyBase.UltimoNome
    End Function

    Public Sub New()
        MyBase.New()
    End Sub
End Class
