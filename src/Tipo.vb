<Serializable()> Public Class Tipo
    Private _codTipo As String
    Private _tipo As String

    Property CodTipo As String
        Get
            Return _codTipo
        End Get
        Set(value As String)
            _codTipo = value
        End Set
    End Property

    Property Tipo() As String
        Get
            Tipo = _tipo
        End Get
        Set(value As String)
            _tipo = value
        End Set
    End Property

    Overrides Function ToString() As String
        Return _codTipo & "   " & _tipo
    End Function

    Public Sub New()
        MyBase.New()
    End Sub


End Class
