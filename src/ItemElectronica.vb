Public Class ItemEletronica
    Private _codItemEletronica As String
    Private _codBib As String
    Private _codTipo As String
    Private _codFabricante As String

    Property CodItemEletronica As String
        Get
            Return _codItemEletronica
        End Get
        Set(value As String)
            _codItemEletronica = value
        End Set
    End Property

    Property CodBib() As String
        Get
            CodBib = _codBib
        End Get
        Set(value As String)
            _codBib = value
        End Set
    End Property

    Property CodTipo() As String
        Get
            CodTipo = _codTipo
        End Get
        Set(value As String)
            _codTipo = value
        End Set
    End Property

    Property CodFabrincante() As String
        Get
            CodFabrincante = _codFabricante
        End Get
        Set(value As String)
            _codFabricante = value
        End Set
    End Property

    Overrides Function ToString() As String
        Return _codItemEletronica & "   " & _codBib
    End Function

    Public Sub New()
        MyBase.New()
    End Sub
End Class
