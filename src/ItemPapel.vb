Public Class ItemPapel
    Private _codItemPapel As String
    Private _codBib As String
    Private _codTipo As String
    Private _codCategoria As String
    Private _codEditora As String
    Private _codAutor As String
    Private _titulo As String
    Private _edicao As String
    Private _idioma As String
    Private _dimensoes As String
    Private _permissao As String
    Private _volume As String
    Private _data_pub As String
    Private _classificacao As String
    Private _descricao As String
    Private _cota As String

    Property CodItemPapel As String
        Get
            Return _codItemPapel
        End Get
        Set(value As String)
            _codItemPapel = value
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

    Property CodEditora() As String
        Get
            CodEditora = _codEditora
        End Get
        Set(value As String)
            _codEditora = value
        End Set
    End Property

    Property CodCategoria() As String
        Get
            CodCategoria = _codCategoria
        End Get
        Set(value As String)
            _codCategoria = value

        End Set
    End Property

    Property CodAutor() As String
        Get
            CodAutor = _codAutor
        End Get
        Set(value As String)
            _codAutor = value
        End Set
    End Property

    Property Titulo As String
        Get
            Titulo = _titulo
        End Get
        Set(value As String)
            _titulo = value
        End Set
    End Property

    Property Edicao() As String
        Get
            Edicao = _edicao
        End Get
        Set(value As String)
            _edicao = value
        End Set
    End Property

    Property Idioma() As String
        Get
            Idioma = _idioma
        End Get
        Set(value As String)
            _idioma = value
        End Set
    End Property

    Property Dimensoes() As String
        Get
            Dimensoes = _dimensoes
        End Get
        Set(value As String)
            _dimensoes = value
        End Set
    End Property

    Property Permissao() As String
        Get
            Permissao = _permissao
        End Get
        Set(value As String)
            _permissao = value
        End Set
    End Property
    Property Volume() As String
        Get
            Volume = _volume
        End Get
        Set(value As String)
            _volume = value
        End Set
    End Property

    Property DataPub() As String
        Get
            DataPub = _data_pub
        End Get
        Set(value As String)
            _data_pub = value
        End Set
    End Property

    Property Classificacao() As String
        Get
            Classificacao = _classificacao
        End Get
        Set(value As String)
            _classificacao = value
        End Set
    End Property

    Property Descricao() As String
        Get
            Descricao = _descricao
        End Get
        Set(value As String)
            _descricao = value
        End Set
    End Property

    Property Cota() As String
        Get
            Cota = _cota
        End Get
        Set(value As String)
            _cota = value
        End Set
    End Property

    Overrides Function ToString() As String
        Return _codItemPapel & "   " & _codBib & "   " & _titulo
    End Function
    Function teste1() As String
        Return _codItemPapel & "   " & _titulo
    End Function
    Public Sub New()
        MyBase.New()
    End Sub
End Class