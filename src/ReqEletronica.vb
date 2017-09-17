Public Class ReqEletronica
    Private _codItemEletronica As String
    Private _codLeitor As String
    Private _codReq As String
    Private _marcacao As String
    Private _duracao As String
    Private _horaInicio As String

    Property CodItemEletronica As String
        Get
            Return _codItemEletronica
        End Get
        Set(value As String)
            _codItemEletronica = value
        End Set
    End Property

    Property CodLeitor() As String
        Get
            CodLeitor = _codLeitor
        End Get
        Set(value As String)
            _codLeitor = value
        End Set
    End Property

    Property CodReq() As String
        Get
            CodReq = _codReq
        End Get
        Set(value As String)
            _codReq = value
        End Set
    End Property

    Property Marcacao() As String
        Get
            Marcacao = _marcacao
        End Get
        Set(value As String)
            _marcacao = value
        End Set
    End Property

    Property Duracao() As String
        Get
            Duracao = _duracao
        End Get
        Set(value As String)
            _duracao = value
        End Set
    End Property

    Property HoraInicio As String
        Get
            HoraInicio = _horaInicio
        End Get
        Set(value As String)
            _horaInicio = value
        End Set
    End Property


    Overrides Function ToString() As String
        Return _codReq & "   " & _codItemEletronica & "   " & _codLeitor
    End Function

    Public Sub New()
        MyBase.New()
    End Sub
End Class
