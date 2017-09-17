Public Class Fabricante
    Private _codFabricante As String
    Private _fabricante As String
    Private _endereco As String
    Private _telefone As String

    Property CodFabricante As String
        Get
            Return _codFabricante
        End Get
        Set(value As String)
            _codFabricante = value
        End Set
    End Property

    Property Fabricante() As String
        Get
            Fabricante = _fabricante
        End Get
        Set(value As String)
            _fabricante = value
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
        Return _codFabricante & "   " & _fabricante
    End Function

    Public Sub New()
        MyBase.New()
    End Sub
End Class
