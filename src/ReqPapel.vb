Public Class ReqPapel
    Private _codItemPapel As String
    Private _codLeitor As String
    Private _codReq As String
    Private _data_req As String
    Private _multa As String
    Private _data_real As String
    Private _data_prev As String


    Property CodItemPapel As String
        Get
            Return _codItemPapel
        End Get
        Set(value As String)
            _codItemPapel = value
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

    Property DataReq() As String
        Get
            DataReq = _data_req
        End Get
        Set(value As String)
            _data_req = value
        End Set
    End Property

    Property Multa() As String
        Get
            Multa = _multa
        End Get
        Set(value As String)
            _multa = value
        End Set
    End Property

    Property DataReal() As String
        Get
            DataReal = _data_real
        End Get
        Set(value As String)
            _data_real = value
        End Set
    End Property

    Property DataPrev() As String
        Get
            DataPrev = _data_prev
        End Get
        Set(value As String)
            _data_prev = value
        End Set
    End Property

    Overrides Function ToString() As String
        Return _codReq & "   " & _codItemPapel & "   " & _codLeitor
    End Function

    Public Sub New()
        MyBase.New()
    End Sub
End Class
