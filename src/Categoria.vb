<Serializable()> Public Class Categoria
    Private _codCategoria As String
    Private _categoria As String

    Property CodCategoria As String
        Get
            Return _codCategoria
        End Get
        Set(value As String)
            _codCategoria = value
        End Set
    End Property

    Property Categoria() As String
        Get
            Return _categoria
        End Get
        Set(value As String)
            _categoria = value
        End Set
    End Property

    Overrides Function ToString() As String
        Return _codCategoria & "   " & _categoria
    End Function

    Public Sub New()
        MyBase.New()
    End Sub
End Class
