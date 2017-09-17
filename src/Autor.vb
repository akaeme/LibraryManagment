<Serializable()> Public Class Autor
    Inherits Pessoa
    Private _codAutor As String

    Property CodAutor As String
        Get
            Return _codAutor
        End Get
        Set(value As String)
            _codAutor = value
        End Set
    End Property

    Overrides Function ToString() As String
        Return _codAutor & "   " & MyBase.PrimeiroNome & "   " & MyBase.UltimoNome
    End Function
    Function teste1() As String
        Return _codAutor & "   " & MyBase.PrimeiroNome
    End Function
    Public Sub New()
        MyBase.New()
    End Sub
End Class
