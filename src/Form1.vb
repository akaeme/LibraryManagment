Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Data.SqlClient
Public Class Form1
    Dim CN As SqlConnection
    Dim CMD As SqlCommand
    Dim currentSelectedPessoa As Integer
    Dim currentSelectedLeitor As Integer
    Dim currentSelectedBibliotecario As Integer
    Dim currentSelectedFabricante As Integer
    Dim currentSelectedTipo As Integer
    Dim currentSelectedCategoria As Integer
    Dim currentSelectedEditora As Integer
    Dim currentSelectedBiblioteca As Integer
    Dim currentSelectedReqItemElect As Integer
    Dim currentSelectedReqItemPapel As Integer
    Dim currentSelectedItemElect As Integer
    Dim currentSelectedItemPapel As Integer
    Dim currentSelectedAutor As Integer
    Dim adding As Boolean
    Dim update_flag As Boolean
    Dim add As Boolean
    Dim fromBack As Boolean = False
    Dim dict = New Dictionary(Of Integer, String) From {{0, "P_Inicio"}, {1, "P_Pessoa"}, {2, "P_Leitor"}, {3, "P_Bibliotecario"}, {4, "P_Autor"}, {5, "P_Items_Papel"},
        {6, "P_ItemElect"}, {7, "P_ReqItemP"}, {8, "P_ReqItemElect"}, {9, "P_Biblioteca"}, {10, "P_Editora"}, {11, "P_Categoria"}, {12, "P_Tipos"}, {13, "P_Fabricantes"}}
    Dim SortCode As List(Of Integer) = New List(Of Integer)
    Dim SortFname As List(Of KeyValuePair(Of Integer, String)) = New List(Of KeyValuePair(Of Integer, String))
    Dim SortLname As List(Of KeyValuePair(Of Integer, String)) = New List(Of KeyValuePair(Of Integer, String))
    Dim SortCodItem As List(Of KeyValuePair(Of Integer, Integer)) = New List(Of KeyValuePair(Of Integer, Integer))
    Dim SortCodLeitor As List(Of KeyValuePair(Of Integer, Integer)) = New List(Of KeyValuePair(Of Integer, Integer))
    'Dim auxList As List(Of ListClass)
    Dim auxListBib As List(Of Bibliotecario)
    Dim auxListPessoa As List(Of Pessoa)
    Dim auxListAutor As List(Of Autor)
    Dim auxListLeitor As List(Of Leitor)
    Dim auxListBiblioteca As List(Of Biblioteca)
    Dim auxListCategoria As List(Of Categoria)
    Dim auxListEditora As List(Of Editora)
    Dim auxListFabricante As List(Of Fabricante)
    Dim auxListTipo As List(Of Tipo)
    Dim auxListRIP As List(Of ReqPapel)
    Dim auxListRIE As List(Of ReqEletronica)
    Dim auxListIE As List(Of ItemEletronica)
    Dim auxListIP As List(Of ItemPapel)
    Public Sub ShowPanel(painel As Integer)
        For index As Integer = 0 To 13
            Dim testString As String = ""
            If index = painel Then
                If dict.TryGetValue(index, testString) Then
                    Dim P As Panel = Nothing
                    Dim Ctr As Control = Controls(testString)

                    If TypeOf Ctr Is Panel Then
                        P = DirectCast(Ctr, Panel)
                        P.Visible = True
                    End If
                End If
            Else
                If dict.TryGetValue(index, testString) Then
                    Dim P As Panel = Nothing
                    Dim Ctr As Control = Controls(testString)

                    If TypeOf Ctr Is Panel Then
                        P = DirectCast(Ctr, Panel)
                        P.Visible = False
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'CN = New SqlConnection("data source=localhost\SQLEXPRESS;integrated security=true;initial catalog=BDProj")
        CN = New SqlConnection("Server = tcp: 193.136.175.33\SQLSERVER2012,8293;" & "Database = p6g4; uid = p6g4;" & "password = bd1516p6g4")
        CMD = New SqlCommand
        CMD.Connection = CN
        ShowPanel(0)
    End Sub

    Private Sub PessoaToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PessoaToolStripMenuItem1.Click
        ShowPanel(1)
        HideShowButtons(1, True)
        LockUnlockControls(1, False)
        CMD.CommandText = "SELECT * FROM Project.Pessoa"
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        LB_Pessoa.Items.Clear()
        auxListPessoa = New List(Of Pessoa)
        While RDR.Read
            Dim P As New Pessoa
            P.IDPessoa = RDR.Item("ID_PESSOA")
            P.PrimeiroNome = RDR.Item("PRIMEIRO_NOME")
            P.UltimoNome = RDR.Item("ULTIMO_NOME")
            P.Morada = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("MORADA")), "", RDR.Item("MORADA")))
            P.Tlm = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("TLM")), "", RDR.Item("TLM")))
            P.Cc = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("CC")), "", RDR.Item("CC")))
            P.Nif = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("NIF")), "", RDR.Item("NIF")))
            P.DataNasc = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DATA_NASC")), "", RDR.Item("DATA_NASC")))
            P.Genero = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("GENERO")), "", RDR.Item("GENERO")))
            LB_Pessoa.Items.Add(P)
            auxListPessoa.Add(P)
        End While
        CN.Close()
        currentSelectedPessoa = 0
        ShowPessoa()
    End Sub

    Private Sub LeitorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LeitorToolStripMenuItem.Click
        ShowPanel(2)
        HideShowButtons(2, True)
        LockUnlockControls(2, False)
        CMD.CommandText = "SELECT * FROM PROJECT.LEITOR JOIN PROJECT.PESSOA ON PROJECT.LEITOR.ID_PESSOA = PROJECT.PESSOA.ID_PESSOA"
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        LB_Leitor.Items.Clear()
        auxListLeitor = New List(Of Leitor)
        While RDR.Read
            Dim L As New Leitor
            L.CodLeitor = RDR.Item("COD_LEITOR")
            L.PrimeiroNome = RDR.Item("PRIMEIRO_NOME")
            L.UltimoNome = RDR.Item("ULTIMO_NOME")
            L.IDPessoa = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("ID_PESSOA")), "", RDR.Item("ID_PESSOA")))
            L.Morada = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("MORADA")), "", RDR.Item("MORADA")))
            L.Tlm = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("TLM")), "", RDR.Item("TLM")))
            L.Cc = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("CC")), "", RDR.Item("CC")))
            L.Nif = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("NIF")), "", RDR.Item("NIF")))
            L.DataNasc = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DATA_NASC")), "", RDR.Item("DATA_NASC")))
            L.Genero = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("GENERO")), "", RDR.Item("GENERO")))
            L.Username = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("USERNAME")), "", RDR.Item("USERNAME")))
            L.Password = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("PASS")), "", RDR.Item("PASS")))
            L.DataExpiro = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DATA_EXPIRO")), "", RDR.Item("DATA_EXPIRO")))
            L.DataRegisto = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DATA_REGISTO")), "", RDR.Item("DATA_REGISTO")))
            L.CodBiblioteca = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("COD_BIB")), "", RDR.Item("COD_BIB")))
            LB_Leitor.Items.Add(L)
            auxListLeitor.Add(L)
        End While
        CN.Close()
        currentSelectedLeitor = 0
        ShowLeitor()
    End Sub

    Private Sub InicioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InicioToolStripMenuItem.Click
        ShowPanel(0)
    End Sub

    Private Sub BibliotecarioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BibliotecarioToolStripMenuItem.Click
        ShowPanel(3)
        HideShowButtons(3, True)
        LockUnlockControls(3, False)
        CMD.CommandText = "SELECT * FROM PROJECT.BIBLIOTECARIO JOIN PROJECT.PESSOA ON PROJECT.BIBLIOTECARIO.ID_PESSOA = PROJECT.PESSOA.ID_PESSOA"
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        LB_BIB.Items.Clear()
        'auxList = New List(Of ListClass)
        auxListBib = New List(Of Bibliotecario)
        While RDR.Read
            Dim B As New Bibliotecario
            B.CodBibliotecario = RDR.Item("COD_BIBLIOTECARIO")
            B.PrimeiroNome = RDR.Item("PRIMEIRO_NOME")
            B.UltimoNome = RDR.Item("ULTIMO_NOME")
            B.IDPessoa = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("ID_PESSOA")), "", RDR.Item("ID_PESSOA")))
            B.CodBiblioteca = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("COD_BIB")), "", RDR.Item("COD_BIB")))
            B.Morada = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("MORADA")), "", RDR.Item("MORADA")))
            B.Tlm = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("TLM")), "", RDR.Item("TLM")))
            B.Cc = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("CC")), "", RDR.Item("CC")))
            B.Nif = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("NIF")), "", RDR.Item("NIF")))
            B.DataNasc = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DATA_NASC")), "", RDR.Item("DATA_NASC")))
            B.Genero = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("GENERO")), "", RDR.Item("GENERO")))
            B.Salary = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("SALARY")), "", RDR.Item("SALARY")))
            B.Password = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("PASS")), "", RDR.Item("PASS")))
            B.Username = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("USERNAME")), "", RDR.Item("USERNAME")))
            LB_BIB.Items.Add(B)
            'auxList.Add(New ListClass(B.CodBibliotecario, B.PrimeiroNome, B.UltimoNome, B.IDPessoa, B.CodBiblioteca, B.Morada, B.Tlm, B.Cc, B.Nif, B.DataNasc, B.Genero, B.Salary, B.Password, Nothing))
            auxListBib.Add(B)
        End While
        CN.Close()
        currentSelectedBibliotecario = 0
        ShowBibliotecario()
    End Sub

    Private Sub AutorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AutorToolStripMenuItem.Click
        ShowPanel(4)
        HideShowButtons(4, True)
        LockUnlockControls(4, False)
        CMD.CommandText = "SELECT * FROM PROJECT.AUTOR JOIN PROJECT.PESSOA ON PROJECT.AUTOR.ID_PESSOA = PROJECT.PESSOA.ID_PESSOA"
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        LB_AUTOR.Items.Clear()
        auxListAutor = New List(Of Autor)
        While RDR.Read
            Dim A As New Autor
            A.CodAutor = RDR.Item("COD_AUTOR")
            A.PrimeiroNome = RDR.Item("PRIMEIRO_NOME")
            A.UltimoNome = RDR.Item("ULTIMO_NOME")
            A.IDPessoa = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("ID_PESSOA")), "", RDR.Item("ID_PESSOA")))
            A.Morada = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("MORADA")), "", RDR.Item("MORADA")))
            A.Tlm = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("TLM")), "", RDR.Item("TLM")))
            A.Cc = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("CC")), "", RDR.Item("CC")))
            A.Nif = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("NIF")), "", RDR.Item("NIF")))
            A.DataNasc = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DATA_NASC")), "", RDR.Item("DATA_NASC")))
            A.Genero = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("GENERO")), "", RDR.Item("GENERO")))
            LB_AUTOR.Items.Add(A)
            auxListAutor.Add(A)
        End While
        CN.Close()
        currentSelectedAutor = 0
        ShowAutor()
    End Sub

    Private Sub ItemsPapelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ItemsPapelToolStripMenuItem.Click
        ShowPanel(5)
        HideShowButtons(5, True)
        LockUnlockControls(5, False)
        CMD.CommandText = "SELECT * FROM PROJECT.ITEM_PAPEL JOIN PROJECT.ITEM_PAPEL_AUTOR ON PROJECT.ITEM_PAPEL.COD_ITEM_PAPEL = PROJECT.ITEM_PAPEL_AUTOR.COD_ITEM_PAPEL"
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        LB_ITEMS_PAPEL.Items.Clear()
        auxListIP = New List(Of ItemPapel)
        While RDR.Read
            Dim IP As New ItemPapel
            IP.CodItemPapel = RDR.Item("COD_ITEM_PAPEL")
            IP.CodBib = RDR.Item("COD_BIB")
            IP.CodTipo = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("COD_TIPO")), "", RDR.Item("COD_TIPO")))
            IP.CodCategoria = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("COD_CATEGORIA")), "", RDR.Item("COD_CATEGORIA")))
            IP.CodEditora = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("COD_EDITORA")), "", RDR.Item("COD_EDITORA")))
            IP.CodAutor = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("COD_AUTOR")), "", RDR.Item("COD_AUTOR")))
            IP.Titulo = RDR.Item("TITULO")
            IP.Edicao = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("EDICAO")), "", RDR.Item("EDICAO")))
            IP.Idioma = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("IDIOMA")), "", RDR.Item("IDIOMA")))
            IP.Dimensoes = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DIMENSOES")), "", RDR.Item("DIMENSOES"))) ''corrigir no ddl
            IP.Permissao = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("PERMISSAO")), "", RDR.Item("PERMISSAO")))
            IP.Volume = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("VOLUME")), "", RDR.Item("VOLUME")))
            IP.DataPub = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DATA_PUB")), "", RDR.Item("DATA_PUB")))
            IP.Classificacao = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("CLASSIFICACAO")), "", RDR.Item("CLASSIFICACAO")))
            IP.Descricao = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DESCRICAO")), "", RDR.Item("DESCRICAO")))
            IP.Cota = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("COTA")), "", RDR.Item("COTA")))
            LB_ITEMS_PAPEL.Items.Add(IP)
            auxListIP.Add(IP)
        End While
        CN.Close()
        currentSelectedItemPapel = 0
        ShowItemPapel()
    End Sub

    Private Sub ItemsElectronicaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ItemsElectronicaToolStripMenuItem.Click
        ''PODE TER VALORES NULL
        ShowPanel(6)
        HideShowButtons(6, True)
        LockUnlockControls(6, False)
        CMD.CommandText = "SELECT * FROM PROJECT.ITEM_ELECTRONICA"
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        auxListIE = New List(Of ItemEletronica)
        LB_ITEM_ELECT.Items.Clear()
        While RDR.Read
            Dim IE As New ItemEletronica
            IE.CodItemEletronica = RDR.Item("COD_ITEM_ELECT")
            IE.CodBib = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("COD_BIB")), "", RDR.Item("COD_BIB")))
            IE.CodTipo = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("COD_TIPO")), "", RDR.Item("COD_TIPO")))
            IE.CodFabrincante = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("COD_FABRICANTE")), "", RDR.Item("COD_FABRICANTE")))
            LB_ITEM_ELECT.Items.Add(IE)
            auxListIE.Add(IE)
        End While
        CN.Close()
        currentSelectedItemElect = 0
        ShowItemElect()
    End Sub

    Private Sub ItemsPapelToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ItemsPapelToolStripMenuItem1.Click
        ShowPanel(7)
        HideShowButtons(7, True)
        LockUnlockControls(7, False)
        CMD.CommandText = "SELECT * FROM PROJECT.REQUISICAO_ITEM_PAPEL"
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        LB_ReqItemP.Items.Clear()
        auxListRIP = New List(Of ReqPapel)
        While RDR.Read
            Dim RIP As New ReqPapel
            RIP.CodReq = RDR.Item("COD_REQUISICAO")
            RIP.CodItemPapel = RDR.Item("COD_ITEM_PAPEL")
            'RIP.CodLeitor = RDR.Item("COD_LEITOR")
            RIP.CodLeitor = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("COD_LEITOR")), "", RDR.Item("COD_LEITOR")))
            RIP.DataReq = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DATA_REQUISICAO")), "", RDR.Item("DATA_REQUISICAO")))
            RIP.DataReal = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DT_EN_REAL")), "", RDR.Item("DT_EN_REAL")))
            RIP.DataPrev = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DT_EN_PREVISTA")), "", RDR.Item("DT_EN_PREVISTA")))
            RIP.Multa = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("MULTA")), "", RDR.Item("MULTA")))
            LB_ReqItemP.Items.Add(RIP)
            auxListRIP.Add(RIP)
        End While
        CN.Close()
        currentSelectedReqItemPapel = 0
        ShowReqItemPapel()
    End Sub

    Private Sub ItemsElectrónicaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ItemsElectrónicaToolStripMenuItem.Click
        ShowPanel(8)
        HideShowButtons(8, True)
        LockUnlockControls(8, False)
        CMD.CommandText = "SELECT * FROM PROJECT.REQUISICAO_ITEM_ELECT"
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        LB_ReqItemElect.Items.Clear()
        auxListRIE = New List(Of ReqEletronica)
        While RDR.Read
            Dim RIE As New ReqEletronica
            RIE.CodReq = RDR.Item("COD_REQUISICAO")
            RIE.CodItemEletronica = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("COD_ITEM_ELECT")), "", RDR.Item("COD_ITEM_ELECT")))
            RIE.CodLeitor = RDR.Item("COD_LEITOR")
            RIE.Marcacao = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("MARCACAO")), "", RDR.Item("MARCACAO")))
            RIE.Duracao = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DURACAO")), "", RDR.Item("DURACAO")))
            RIE.HoraInicio = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("INICIO")), "", RDR.Item("INICIO")))
            LB_ReqItemElect.Items.Add(RIE)
            auxListRIE.Add(RIE)
        End While
        CN.Close()
        currentSelectedReqItemElect = 0
        ShowReqItemElect()
    End Sub

    Private Sub BibliotecasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BibliotecasToolStripMenuItem.Click
        ShowPanel(9)
        HideShowButtons(9, True)
        LockUnlockControls(9, False)
        CMD.CommandText = "SELECT * FROM PROJECT.BIBLIOTECA"
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        LB_Biblioteca.Items.Clear()
        auxListBiblioteca = New List(Of Biblioteca)
        While RDR.Read
            Dim Biblioteca As New Biblioteca
            Biblioteca.CodBiblioteca = RDR.Item("COD_BIB")
            Biblioteca.Nome = RDR.Item("NOME")
            Biblioteca.Endereco = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("ENDERECO")), "", RDR.Item("ENDERECO")))
            LB_Biblioteca.Items.Add(Biblioteca)
            auxListBiblioteca.Add(Biblioteca)
        End While
        CN.Close()
        currentSelectedBiblioteca = 0
        ShowBibliotecas()
    End Sub

    Private Sub EditorasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditorasToolStripMenuItem.Click
        ShowPanel(10)
        HideShowButtons(10, True)
        LockUnlockControls(10, False)
        CMD.CommandText = "SELECT * FROM PROJECT.EDITORA"
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        auxListEditora = New List(Of Editora)
        LB_Editora.Items.Clear()
        While RDR.Read
            Dim Ed As New Editora
            Ed.CodEditora = RDR.Item("COD_EDITORA")
            Ed.NomeEditora = RDR.Item("NOME")
            Ed.Endereco = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("ENDERECO")), "", RDR.Item("ENDERECO")))
            Ed.Telefone = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("TELEFONE")), "", RDR.Item("TELEFONE")))
            LB_Editora.Items.Add(Ed)
            auxListEditora.Add(Ed)
        End While
        CN.Close()
        currentSelectedEditora = 0
        ShowEditoras()
    End Sub

    Private Sub CategoriasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CategoriasToolStripMenuItem.Click
        ShowPanel(11)
        HideShowButtons(11, True)
        LockUnlockControls(11, False)
        CMD.CommandText = "SELECT * FROM PROJECT.CATEGORIA"
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        LB_CATEGORIA.Items.Clear()
        auxListCategoria = New List(Of Categoria)
        While RDR.Read
            Dim C As New Categoria
            C.CodCategoria = RDR.Item("COD_CATEGORIA")
            C.Categoria = RDR.Item("CATEGORIA")
            LB_CATEGORIA.Items.Add(C)
            auxListCategoria.Add(C)
        End While
        CN.Close()
        currentSelectedCategoria = 0
        ShowCategorias()
    End Sub

    Private Sub TiposToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TiposToolStripMenuItem.Click
        ShowPanel(12)
        HideShowButtons(12, True)
        LockUnlockControls(12, False)
        CMD.CommandText = "SELECT * FROM PROJECT.TIPO"
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        LB_TIPOS.Items.Clear()
        auxListTipo = New List(Of Tipo)
        While RDR.Read
            Dim T As New Tipo
            T.CodTipo = RDR.Item("COD_TIPO")
            T.Tipo = RDR.Item("TIPO")
            LB_TIPOS.Items.Add(T)
            auxListTipo.Add(T)
        End While
        CN.Close()
        currentSelectedTipo = 0
        ShowTipos()
    End Sub

    Private Sub FabricantesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FabricantesToolStripMenuItem.Click
        ShowPanel(13)
        HideShowButtons(13, True)
        LockUnlockControls(13, False)
        CMD.CommandText = "select * from project.fabricante"
        CN.Open()
        Dim rdr As SqlDataReader
        rdr = CMD.ExecuteReader
        LB_FABRICANTES.Items.Clear()
        auxListFabricante = New List(Of Fabricante)
        While rdr.Read
            Dim f As New Fabricante
            f.CodFabricante = rdr.Item("cod_fabricante")
            f.Fabricante = rdr.Item("fabricante")
            f.Endereco = Convert.ToString(IIf(rdr.IsDBNull(rdr.GetOrdinal("endereco")), "", rdr.Item("endereco")))
            f.Telefone = Convert.ToString(IIf(rdr.IsDBNull(rdr.GetOrdinal("telefone")), "", rdr.Item("telefone")))
            LB_FABRICANTES.Items.Add(f)
            auxListFabricante.Add(f)
        End While
        CN.Close()
        currentSelectedFabricante = 0
        ShowFabricantes()
    End Sub

    Private Sub LB_Pessoa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LB_Pessoa.SelectedIndexChanged
        If LB_Pessoa.SelectedIndex > -1 Then
            currentSelectedPessoa = LB_Pessoa.SelectedIndex
            ShowPessoa()
        End If
    End Sub
    Sub ShowPessoa()
        If LB_Pessoa.Items.Count = 0 Or currentSelectedPessoa < 0 Then Exit Sub
        Dim p As New Pessoa
        p = CType(LB_Pessoa.Items.Item(currentSelectedPessoa), Pessoa)
        I_PESSOA.Text = LB_Pessoa.Items.Count
        I_PESSOA.Enabled = False
        TB_PN_PESSOA.Text = p.PrimeiroNome
        TB_UN_PESSOA.Text = p.UltimoNome
        TB_CC_PESSOA.Text = p.Cc
        TB_NIF_PESSOA.Text = p.Nif
        TB_MORADA_PESSOA.Text = p.Morada
        TB_GENERO_PESSOA.Text = p.Genero
        TB_TLM_PESSOA.Text = p.Tlm
        TB_IDPESSOA_P.Text = p.IDPessoa
        TB_DATAN_PESSOA.Text = p.DataNasc
    End Sub

    Private Sub LB_Leitor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LB_Leitor.SelectedIndexChanged
        If LB_Leitor.SelectedIndex > -1 Then
            currentSelectedLeitor = LB_Leitor.SelectedIndex
            ShowLeitor()
        End If
    End Sub
    Sub ShowLeitor()
        CB_IDPessoa_L.Hide()
        CB_CODBIB_L.Hide()
        If LB_Leitor.Items.Count = 0 Or currentSelectedLeitor < 0 Then Exit Sub
        Dim l As New Leitor
        l = CType(LB_Leitor.Items.Item(currentSelectedLeitor), Leitor)
        I_LEITOR.Text = LB_Leitor.Items.Count
        I_LEITOR.Enabled = False
        TB_PM_LEITOR.Text = l.PrimeiroNome
        TB_UN_LEITOR.Text = l.UltimoNome
        TB_CC_LEITOR.Text = l.Cc
        TB_NIF_LEITOR.Text = l.Nif
        TB_MORADA_LEITOR.Text = l.Morada
        TB_GENERO_LEITOR.Text = l.Genero
        TB_TLM_LEITOR.Text = l.Tlm
        TB_IDPESSOA_L.Text = l.IDPessoa
        TB_DATAN_LEITOR.Text = l.DataNasc
        TB_COD_BIB_LEITOR.Text = l.CodBiblioteca
        TB_COD_LEITOR.Text = l.CodLeitor
        TB_USERNAME_LEITOR.Text = l.Username
        TB_PASS_LEITOR.Text = l.Password
        TB_DATAR_LEITOR.Text = l.DataRegisto
        TB_DATAE_LEITOR.Text = l.DataExpiro
    End Sub

    Private Sub LB_BIB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LB_BIB.SelectedIndexChanged
        If LB_BIB.SelectedIndex > -1 Then
            currentSelectedBibliotecario = LB_BIB.SelectedIndex
            ShowBibliotecario()
        End If
    End Sub
    Sub ShowBibliotecario()
        CB_IDPessoa_B.Hide()
        CB_CODBIB_B.Hide()
        If LB_BIB.Items.Count = 0 Or currentSelectedBibliotecario < 0 Then Exit Sub
        Dim b As New Bibliotecario
        b = CType(LB_BIB.Items.Item(currentSelectedBibliotecario), Bibliotecario)
        I_BIB.Text = LB_BIB.Items.Count
        I_BIB.Enabled = False
        TB_PN_BIB.Text = b.PrimeiroNome
        TB_UN_BIB.Text = b.UltimoNome
        TB_CC_BIB.Text = b.Cc
        TB_NIF_BIB.Text = b.Nif
        TB_MORARA_BIB.Text = b.Morada
        TB_GENERO_BIB.Text = b.Genero
        TB_TLM_BIB.Text = b.Tlm
        TB_IDPESSOA_B.Text = b.IDPessoa
        TB_DATAN_BIB.Text = b.DataNasc
        TB_CODBIB_BIB.Text = b.CodBiblioteca
        TB_CODBIB.Text = b.CodBibliotecario
        TB_USERNAME_BIB.Text = b.Username
        TB_PASS_BIB.Text = b.Password
        TB_SALARIO_BIB.Text = b.Salary

    End Sub

    Private Sub LB_FABRICANTES_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LB_FABRICANTES.SelectedIndexChanged
        If LB_FABRICANTES.SelectedIndex > -1 Then
            currentSelectedFabricante = LB_FABRICANTES.SelectedIndex
            ShowFabricantes()
        End If
    End Sub
    Sub ShowFabricantes()
        If LB_FABRICANTES.Items.Count = 0 Or currentSelectedFabricante < 0 Then Exit Sub
        Dim b As New Fabricante
        b = CType(LB_FABRICANTES.Items.Item(currentSelectedFabricante), Fabricante)
        I_FABRICANTE.Text = LB_FABRICANTES.Items.Count
        I_FABRICANTE.Enabled = False
        TB_CODFABRICANTE.Text = b.CodFabricante
        TB_FABRICANTE.Text = b.Fabricante
        TB_ENDERECO_FAB.Text = b.Endereco
        TB_TELEFONE_FAB.Text = b.Telefone
    End Sub

    Private Sub LB_TIPOS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LB_TIPOS.SelectedIndexChanged
        If LB_TIPOS.SelectedIndex > -1 Then
            currentSelectedTipo = LB_TIPOS.SelectedIndex
            ShowTipos()
        End If
    End Sub
    Sub ShowTipos()
        If LB_TIPOS.Items.Count = 0 Or currentSelectedTipo < 0 Then Exit Sub
        Dim p As New Tipo
        p = CType(LB_TIPOS.Items.Item(currentSelectedTipo), Tipo)
        I_TIPOS.Text = LB_TIPOS.Items.Count
        I_TIPOS.Enabled = False
        TB_CODTIPO.Text = p.CodTipo
        TB_TIPO.Text = p.Tipo
    End Sub

    Private Sub LB_CATEGORIA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LB_CATEGORIA.SelectedIndexChanged
        If LB_CATEGORIA.SelectedIndex > -1 Then
            currentSelectedCategoria = LB_CATEGORIA.SelectedIndex
            ShowCategorias()
        End If
    End Sub
    Sub ShowCategorias()
        If LB_CATEGORIA.Items.Count = 0 Or currentSelectedCategoria < 0 Then Exit Sub
        Dim p As New Categoria
        p = CType(LB_CATEGORIA.Items.Item(currentSelectedCategoria), Categoria)
        I_CATEGORIA.Text = LB_CATEGORIA.Items.Count
        I_CATEGORIA.Enabled = False
        TB_CODCATEGORIA.Text = p.CodCategoria
        TB_CATEGORIA.Text = p.Categoria
    End Sub

    Private Sub LB_Editora_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LB_Editora.SelectedIndexChanged
        If LB_Editora.SelectedIndex > -1 Then
            currentSelectedEditora = LB_Editora.SelectedIndex
            ShowEditoras()
        End If
    End Sub
    Sub ShowEditoras()
        If LB_Editora.Items.Count = 0 Or currentSelectedEditora < 0 Then Exit Sub
        Dim editora As New Editora
        editora = CType(LB_Editora.Items.Item(currentSelectedEditora), Editora)
        I_EDITORA.Text = LB_Editora.Items.Count
        I_EDITORA.Enabled = False
        TB_CODEDITORA.Text = editora.CodEditora
        TB_NOME_EDITORA.Text = editora.NomeEditora
        TB_ENDERECO_EDITORA.Text = editora.Endereco
        TB_TELEFONE_EDITORA.Text = editora.Telefone
    End Sub

    Private Sub LB_Biblioteca_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LB_Biblioteca.SelectedIndexChanged
        If LB_Biblioteca.SelectedIndex > -1 Then
            currentSelectedBiblioteca = LB_Biblioteca.SelectedIndex
            ShowBibliotecas()
        End If
    End Sub
    Sub ShowBibliotecas()
        If LB_Biblioteca.Items.Count = 0 Or currentSelectedBiblioteca < 0 Then Exit Sub
        Dim biblioteca As New Biblioteca
        biblioteca = CType(LB_Biblioteca.Items.Item(currentSelectedBiblioteca), Biblioteca)
        I_BIBLIOTECA.Text = LB_Biblioteca.Items.Count
        I_BIBLIOTECA.Enabled = False
        TB_NOME_BIB.Text = biblioteca.Nome
        TB_CODIGO_BIB.Text = biblioteca.CodBiblioteca
        TB_ENDERECO_BIB.Text = biblioteca.Endereco
    End Sub

    Private Sub LB_ReqItemElect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LB_ReqItemElect.SelectedIndexChanged
        If LB_ReqItemElect.SelectedIndex > -1 Then
            currentSelectedReqItemElect = LB_ReqItemElect.SelectedIndex
            ShowReqItemElect()
        End If
    End Sub
    Sub ShowReqItemElect()
        CB_CodLeitor_RIE.Hide()
        CB_CodItem_RIE.Hide()
        If LB_ReqItemElect.Items.Count = 0 Or currentSelectedReqItemElect < 0 Then Exit Sub
        Dim re As New ReqEletronica
        re = CType(LB_ReqItemElect.Items.Item(currentSelectedReqItemElect), ReqEletronica)
        I_RIE.Text = LB_ReqItemElect.Items.Count
        I_RIE.Enabled = False
        TB_CODITEMELECT_RIE.Text = re.CodItemEletronica
        TB_CODLEITOR_RIE.Text = re.CodLeitor
        TB_CODREQ_RIE.Text = re.CodReq
        TB_MARCACAO_RIE.Text = re.Marcacao
        TB_DURACAO_RIE.Text = re.Duracao
        TB_HORAINICIO_RIE.Text = re.HoraInicio
    End Sub

    Private Sub LB_ReqItemP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LB_ReqItemP.SelectedIndexChanged
        If LB_ReqItemP.SelectedIndex > -1 Then
            currentSelectedReqItemPapel = LB_ReqItemP.SelectedIndex
            ShowReqItemPapel()
        End If
    End Sub
    Sub ShowReqItemPapel()
        CB_CODITEM_RIP.Hide()
        CB_CODLEITOR_RIP.Hide()
        If LB_ReqItemP.Items.Count = 0 Or currentSelectedReqItemPapel < 0 Then Exit Sub
        Dim rp As New ReqPapel
        rp = CType(LB_ReqItemP.Items.Item(currentSelectedReqItemPapel), ReqPapel)
        I_RIP.Text = LB_ReqItemP.Items.Count
        I_RIP.Enabled = False
        TB_CODIP_RIP.Text = rp.CodItemPapel
        TB_CODLEITOR_RIP.Text = rp.CodLeitor
        TB_CODREQ_RIP.Text = rp.CodReq
        TB_DATAREQ_RIP.Text = rp.DataReq
        TB_MULTA_RIP.Text = rp.Multa
        TB_DATAER_RIP.Text = rp.DataReal
        TB_DATAEP_RIP.Text = rp.DataPrev
    End Sub

    Private Sub LB_ITEM_ELECT_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LB_ITEM_ELECT.SelectedIndexChanged
        If LB_ITEM_ELECT.SelectedIndex > -1 Then
            currentSelectedItemElect = LB_ITEM_ELECT.SelectedIndex
            ShowItemElect()
        End If
    End Sub
    Sub ShowItemElect()
        CB_CODTIPO_IE.Hide()
        CB_CODBIB_IE.Hide()
        CB_CODFABRICANTE_IE.Hide()
        If LB_ITEM_ELECT.Items.Count = 0 Or currentSelectedItemElect < 0 Then Exit Sub
        Dim ie As New ItemEletronica
        ie = CType(LB_ITEM_ELECT.Items.Item(currentSelectedItemElect), ItemEletronica)
        I_IE.Text = LB_ITEM_ELECT.Items.Count
        I_IE.Enabled = False
        TB_CODITEMELECT_IE.Text = ie.CodItemEletronica
        TB_CODBIB_IE.Text = ie.CodBib
        TB_CODTIPO_IE.Text = ie.CodTipo
        TB_CODFABRICANTE_IE.Text = ie.CodFabrincante

    End Sub

    Private Sub LB_ITEMS_PAPEL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LB_ITEMS_PAPEL.SelectedIndexChanged
        If LB_ITEMS_PAPEL.SelectedIndex > -1 Then
            currentSelectedItemPapel = LB_ITEMS_PAPEL.SelectedIndex
            ShowItemPapel()
        End If
    End Sub
    Sub ShowItemPapel()
        CB_CODTIPO_IP.Hide()
        CB_EDITORA_IP.Hide()
        CB_CODCATEGORIA_IP.Hide()
        CB_CODBIB_IP.Hide()
        CB_CODAUTOR_IP.Hide()
        If LB_ITEMS_PAPEL.Items.Count = 0 Or currentSelectedItemPapel < 0 Then Exit Sub
        Dim ip As New ItemPapel
        ip = CType(LB_ITEMS_PAPEL.Items.Item(currentSelectedItemPapel), ItemPapel)
        I_IP.Text = LB_ITEMS_PAPEL.Items.Count
        I_IP.Enabled = False
        TB_CODITEMPAPEL_IP.Text = ip.CodItemPapel
        TB_CODBIB_IP.Text = ip.CodBib
        TB_CODTIPO_IP.Text = ip.CodTipo
        TB_CODCATEGORIA_IP.Text = ip.CodCategoria
        TB_CODEDITORA_IP.Text = ip.CodEditora
        TB_CODAUTOR_IP.Text = ip.CodAutor
        TB_TITULO_IP.Text = ip.Titulo
        TB_EDICAO_IP.Text = ip.Edicao
        TB_IDIOMA_IP.Text = ip.Idioma
        TB_DIMENSOES_IP.Text = ip.Dimensoes
        TB_PERMISSAO_IP.Text = ip.Permissao
        TB_VOLUME_IP.Text = ip.Volume
        TB_DATAPUB_IP.Text = ip.DataPub
        TB_CLASSIFICACAO_IP.Text = ip.Classificacao
        TB_DESCRICAO_IP.Text = ip.Descricao
        TB_COTA_IP.Text = ip.Cota
    End Sub

    Private Sub LB_AUTOR_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LB_AUTOR.SelectedIndexChanged
        If LB_AUTOR.SelectedIndex > -1 Then
            currentSelectedAutor = LB_AUTOR.SelectedIndex
            ShowAutor()
        End If
    End Sub

    Sub ShowAutor()
        CB_IDPessoa_A.Hide()
        If LB_AUTOR.Items.Count = 0 Or currentSelectedAutor < 0 Then Exit Sub
        Dim a As New Autor
        a = CType(LB_AUTOR.Items.Item(currentSelectedAutor), Autor)
        I_AUTOR.Text = LB_AUTOR.Items.Count
        I_AUTOR.Enabled = False
        TB_PN_AUTOR.Text = a.PrimeiroNome
        TB_UN_AUTOR.Text = a.UltimoNome
        TB_CC_AUTOR.Text = a.Cc
        TB_NIF_AUTOR.Text = a.Nif
        TB_MORADA_AUTOR.Text = a.Morada
        TB_GENERO_AUTOR.Text = a.Genero
        TB_TLM_AUTOR.Text = a.Tlm
        TB_IDPESSOA_A.Text = a.IDPessoa
        TB_DATAN_AUTOR.Text = a.DataNasc
        TB_COD_AUTOR.Text = a.CodAutor
    End Sub

    Private Sub Add_Pessoa_Click(sender As Object, e As EventArgs) Handles Add_Pessoa.Click
        adding = True
        ClearFields(1)
        HideShowButtons(1, False)
        LB_Pessoa.Enabled = False
        Dim bool As Boolean = True
        Dim pk As String = ""
        Dim random As New Random
        While bool
            pk = ""
            For i As Integer = 0 To 8
                If i = 0 Then
                    Dim value As String = Convert.ToString(random.Next(1, 9))
                    pk = pk + value
                Else
                    Dim value As String = Convert.ToString(random.Next(0, 9))
                    pk = pk + value
                End If
            Next
            For Each obj In auxListPessoa
                If Equals(obj.IDPessoa, pk) Then
                    Continue For
                Else
                    bool = False
                End If
            Next
        End While
        TB_IDPESSOA_P.Text = pk
        TB_IDPESSOA_P.ReadOnly = True
    End Sub

    Private Sub Confirm_Pessoa_Click(sender As Object, e As EventArgs) Handles Confirm_Pessoa.Click
        Try
            SavePessoa()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        LB_Pessoa.Enabled = True
        Dim idx As Integer = LB_Pessoa.FindString(TB_PN_PESSOA.Text)
        LB_Pessoa.SelectedIndex = idx
        HideShowButtons(1, True)
        LockUnlockControls(1, False)
    End Sub
    Private Function SavePessoa() As Boolean
        Dim pessoa As New Pessoa
        Try
            pessoa.PrimeiroNome = TB_PN_PESSOA.Text
            pessoa.UltimoNome = TB_UN_PESSOA.Text
            pessoa.Cc = TB_CC_PESSOA.Text
            pessoa.Nif = TB_NIF_PESSOA.Text
            pessoa.Morada = TB_MORADA_PESSOA.Text
            pessoa.Genero = TB_GENERO_PESSOA.Text
            pessoa.Tlm = TB_TLM_PESSOA.Text
            pessoa.IDPessoa = TB_IDPESSOA_P.Text
            pessoa.DataNasc = TB_DATAN_PESSOA.Value.ToString()


        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        If adding Then
            submitPessoa(pessoa)
            LB_Pessoa.Items.Add(pessoa)
        Else
            UpdatePessoa(pessoa)
            LB_Pessoa.Items(currentSelectedPessoa) = pessoa
        End If
        Return True
    End Function
    Private Sub submitPessoa(ByVal pessoa As Pessoa)
        CMD.CommandText = "PROJECT.INSERT_PESSOA @IDPessoa, @primeiroNome ,@ultimoNome, @Morada, @Genero,@DataNasc, @Tlm, @Cc, @Nif"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@primeiroNome", pessoa.PrimeiroNome)
        CMD.Parameters.AddWithValue("@ultimoNome", pessoa.UltimoNome)
        CMD.Parameters.AddWithValue("@Cc", pessoa.Cc)
        CMD.Parameters.AddWithValue("@Nif", pessoa.Nif)
        CMD.Parameters.AddWithValue("@Morada", pessoa.Morada)
        CMD.Parameters.AddWithValue("@Genero", pessoa.Genero)
        CMD.Parameters.AddWithValue("@Tlm", pessoa.Tlm)
        CMD.Parameters.AddWithValue("@IDPessoa", pessoa.IDPessoa)
        CMD.Parameters.AddWithValue("@DataNasc", Date.Parse(pessoa.DataNasc))
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to submit Pessoa in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
        CN.Close()
    End Sub

    Private Sub UpdatePessoa(ByVal P As Pessoa)
        CMD.CommandText = "UPDATE PROJECT.PESSOA " &
            "SET PRIMEIRO_NOME = @primeiroNome, " &
            "    ULTIMO_NOME = @ultimoNome, " &
            "    CC = @Cc, " &
            "    NIF = @Nif, " &
            "    MORADA = @Morada, " &
            "    GENERO = @Genero, " &
            "    TLM = @Tlm, " &
            "    DATA_NASC = @DataNasc " &
            "WHERE ID_Pessoa = @IDPessoa"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@IDPessoa", P.IDPessoa)
        CMD.Parameters.AddWithValue("@primeiroNome", P.PrimeiroNome)
        CMD.Parameters.AddWithValue("@ultimoNome", P.UltimoNome)
        CMD.Parameters.AddWithValue("@Cc", P.Cc)
        CMD.Parameters.AddWithValue("@Nif", P.Nif)
        CMD.Parameters.AddWithValue("@Morada", P.Morada)
        CMD.Parameters.AddWithValue("@Genero", P.Genero)
        CMD.Parameters.AddWithValue("@Tlm", P.Tlm)
        CMD.Parameters.AddWithValue("@DataNasc", Date.Parse(P.DataNasc)) 'Hello'
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update contact in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
    End Sub
    Sub HideShowButtons(painel As Integer, flag As Boolean) ''Show-True Hide-False
        Dim P As New Panel
        P = GetPanel(painel)
        LockUnlockControls(painel, True) ''Lock
        If flag Then
            For Each btn As Button In P.Controls.OfType(Of Button)()
                If btn.Name.Contains("Add_") Then
                    btn.Visible = True
                End If
                If btn.Name.Contains("Confirm_") Then
                    btn.Visible = False
                End If
                If btn.Name.Contains("Edit_") Then
                    btn.Visible = True
                End If
                If btn.Name.Contains("Cancel_") Then
                    btn.Visible = False
                End If
                If btn.Name.Contains("Del_") Then
                    btn.Visible = True
                End If
            Next
            For Each cb As ComboBox In P.Controls.OfType(Of ComboBox)()
                If cb.Name.Contains("LISTBY_") Then
                    cb.Enabled = True
                End If
            Next
        Else
            For Each btn As Button In P.Controls.OfType(Of Button)()
                If btn.Name.Contains("Add_") Then
                    btn.Visible = False
                End If
                If btn.Name.Contains("Confirm_") Then
                    btn.Visible = True
                End If
                If btn.Name.Contains("Edit_") Then
                    btn.Visible = False
                End If
                If btn.Name.Contains("Cancel_") Then
                    btn.Visible = True
                End If
                If btn.Name.Contains("Del_") Then
                    btn.Visible = False
                End If
            Next
            For Each cb As ComboBox In P.Controls.OfType(Of ComboBox)()
                If cb.Name.Contains("LISTBY_") Then
                    cb.Enabled = False
                End If
            Next
        End If
    End Sub
    Sub ClearFields(painel As Integer)
        Dim P As New Panel
        P = GetPanel(painel)
        ' Clear all the TextBoxes on the form.
        For Each ctl In P.Controls
            If TypeOf ctl Is TextBox Then
                ctl.Text = ""
            End If
        Next ctl

    End Sub
    Sub LockUnlockControls(painel As Integer, flag As Boolean) ''Lock-False Unlock-True
        Dim P As New Panel
        P = GetPanel(painel)
        ' Clear all the TextBoxes on the form.
        If flag Then
            For Each ctl In P.Controls
                If TypeOf ctl Is TextBox Then
                    ctl.ReadOnly = False
                End If

                If TypeOf ctl Is DateTimePicker Then
                    ctl.Enabled = True
                End If
            Next ctl
        Else
            For Each ctl In P.Controls
                If TypeOf ctl Is TextBox Then
                    ctl.ReadOnly = True
                End If

                If TypeOf ctl Is DateTimePicker Then
                    ctl.Enabled = False
                End If
            Next ctl
        End If
    End Sub

    Private Sub Cancel_Pessoa_Click(sender As Object, e As EventArgs) Handles Cancel_Pessoa.Click
        LB_Pessoa.Enabled = True
        If LB_Pessoa.Items.Count > 0 Then
            currentSelectedPessoa = LB_Pessoa.SelectedIndex
            If currentSelectedPessoa < 0 Then currentSelectedPessoa = 0
            ShowPessoa()
        Else
            ClearFields(1)
            LockUnlockControls(1, False)
        End If
        HideShowButtons(1, True)
        LockUnlockControls(1, False)
    End Sub
    Function GetPanel(painel As Integer) As Panel
        Dim P As Panel = Nothing
        For index As Integer = 0 To 13
            Dim testString As String = ""
            If index = painel Then
                If dict.TryGetValue(index, testString) Then
                    Dim Ctr As Control = Controls(testString)
                    If TypeOf Ctr Is Panel Then
                        P = DirectCast(Ctr, Panel)
                    End If
                End If
            End If
        Next
        Return P
    End Function

    Private Sub Add_Autor_Click(sender As Object, e As EventArgs) Handles Add_Autor.Click
        adding = True
        add = True
        ClearFields(4)
        HideShowButtons(4, False)
        LB_AUTOR.Enabled = False
        GetFreePeople("Autor")
        CB_IDPessoa_A.SelectedIndex = -1
        Dim bool As Boolean = True
        Dim pk As String = ""
        Dim random As New Random
        While bool
            pk = ""
            For i As Integer = 0 To 8
                If i = 0 Then
                    Dim value As String = Convert.ToString(random.Next(1, 9))
                    pk = pk + value
                Else
                    Dim value As String = Convert.ToString(random.Next(0, 9))
                    pk = pk + value
                End If
            Next
            For Each obj In auxListAutor
                If Equals(obj.CodAutor, pk) Then
                    Continue For
                Else
                    bool = False
                End If
            Next
        End While
        TB_COD_AUTOR.Text = pk
        TB_COD_AUTOR.ReadOnly = True

    End Sub
    Private Sub GetFreePeople(Pdependent As String)
        Select Case Pdependent
            Case "Autor"
                TB_IDPESSOA_A.Hide()
                CB_IDPessoa_A.Show()
                CMD.CommandText = "SELECT * FROM PROJECT.GETFREEPEOPLE()"
                CN.Open()
                Dim RDRR As SqlDataReader
                RDRR = CMD.ExecuteReader
                CB_IDPessoa_A.Items.Clear()
                auxListPessoa = New List(Of Pessoa)
                While RDRR.Read
                    Dim FreeP As New Pessoa
                    FreeP.PrimeiroNome = RDRR.Item("PRIMEIRO_NOME")
                    FreeP.UltimoNome = RDRR.Item("ULTIMO_NOME")
                    FreeP.IDPessoa = Convert.ToString(IIf(RDRR.IsDBNull(RDRR.GetOrdinal("ID_PESSOA")), "", RDRR.Item("ID_PESSOA")))
                    FreeP.Morada = Convert.ToString(IIf(RDRR.IsDBNull(RDRR.GetOrdinal("MORADA")), "", RDRR.Item("MORADA")))
                    FreeP.Tlm = Convert.ToString(IIf(RDRR.IsDBNull(RDRR.GetOrdinal("TLM")), "", RDRR.Item("TLM")))
                    FreeP.Cc = Convert.ToString(IIf(RDRR.IsDBNull(RDRR.GetOrdinal("CC")), "", RDRR.Item("CC")))
                    FreeP.Nif = Convert.ToString(IIf(RDRR.IsDBNull(RDRR.GetOrdinal("NIF")), "", RDRR.Item("NIF")))
                    FreeP.DataNasc = Convert.ToString(IIf(RDRR.IsDBNull(RDRR.GetOrdinal("DATA_NASC")), "", RDRR.Item("DATA_NASC")))
                    FreeP.Genero = Convert.ToString(IIf(RDRR.IsDBNull(RDRR.GetOrdinal("GENERO")), "", RDRR.Item("GENERO")))
                    CB_IDPessoa_A.Items.Add(FreeP.teste())
                    auxListPessoa.Add(FreeP)
                End While
                CN.Close()
            Case "Bibliotecario"
                TB_IDPESSOA_B.Hide()
                CB_IDPessoa_B.Show()
                Try
                    CMD.CommandText = "SELECT * FROM PROJECT.GETFREEPEOPLE()"
                    CN.Open()
                    Dim RDR As SqlDataReader
                    RDR = CMD.ExecuteReader
                    CB_IDPessoa_B.Items.Clear()
                    auxListPessoa = New List(Of Pessoa)
                    While RDR.Read
                        Dim FreeP As New Pessoa
                        FreeP.PrimeiroNome = RDR.Item("PRIMEIRO_NOME")
                        FreeP.UltimoNome = RDR.Item("ULTIMO_NOME")
                        FreeP.IDPessoa = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("ID_PESSOA")), "", RDR.Item("ID_PESSOA")))
                        FreeP.Morada = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("MORADA")), "", RDR.Item("MORADA")))
                        FreeP.Tlm = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("TLM")), "", RDR.Item("TLM")))
                        FreeP.Cc = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("CC")), "", RDR.Item("CC")))
                        FreeP.Nif = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("NIF")), "", RDR.Item("NIF")))
                        FreeP.DataNasc = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DATA_NASC")), "", RDR.Item("DATA_NASC")))
                        FreeP.Genero = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("GENERO")), "", RDR.Item("GENERO")))
                        CB_IDPessoa_B.Items.Add(FreeP.teste())
                        auxListPessoa.Add(FreeP)
                    End While
                    CN.Close()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            Case "Leitor"
                TB_IDPESSOA_L.Hide()
                CB_IDPessoa_L.Show()
                Try
                    CMD.CommandText = "SELECT * FROM PROJECT.GETFREEPEOPLE()"
                    CN.Open()
                    Dim RDR As SqlDataReader
                    RDR = CMD.ExecuteReader
                    CB_IDPessoa_L.Items.Clear()
                    auxListPessoa = New List(Of Pessoa)
                    While RDR.Read
                        Dim FreeP As New Pessoa
                        FreeP.PrimeiroNome = RDR.Item("PRIMEIRO_NOME")
                        FreeP.UltimoNome = RDR.Item("ULTIMO_NOME")
                        FreeP.IDPessoa = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("ID_PESSOA")), "", RDR.Item("ID_PESSOA")))
                        FreeP.Morada = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("MORADA")), "", RDR.Item("MORADA")))
                        FreeP.Tlm = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("TLM")), "", RDR.Item("TLM")))
                        FreeP.Cc = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("CC")), "", RDR.Item("CC")))
                        FreeP.Nif = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("NIF")), "", RDR.Item("NIF")))
                        FreeP.DataNasc = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DATA_NASC")), "", RDR.Item("DATA_NASC")))
                        FreeP.Genero = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("GENERO")), "", RDR.Item("GENERO")))
                        CB_IDPessoa_L.Items.Add(FreeP.teste())
                        auxListPessoa.Add(FreeP)
                    End While
                    CN.Close()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
        End Select
    End Sub
    Private Sub Confirm_Autor_Click(sender As Object, e As EventArgs) Handles Confirm_Autor.Click
        Try
            SaveAutor()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        LB_AUTOR.Enabled = True
        Dim idx As Integer = LB_AUTOR.FindString(TB_PN_AUTOR.Text)
        LB_AUTOR.SelectedIndex = idx
        HideShowButtons(4, True)
        LockUnlockControls(4, False)
        CB_IDPessoa_A.Hide()
        TB_IDPESSOA_A.Show()
    End Sub
    Private Function SaveAutor() As Boolean
        Dim autor As New Autor
        Try
            autor.PrimeiroNome = TB_PN_AUTOR.Text
            autor.UltimoNome = TB_UN_AUTOR.Text
            autor.Cc = TB_CC_AUTOR.Text
            autor.Nif = TB_NIF_AUTOR.Text
            autor.Morada = TB_MORADA_AUTOR.Text
            autor.Genero = TB_GENERO_AUTOR.Text
            autor.Tlm = TB_TLM_AUTOR.Text
            autor.DataNasc = TB_DATAN_AUTOR.Value.ToString()
            autor.CodAutor = TB_COD_AUTOR.Text

            If add Then
                Dim help As String = CB_IDPessoa_A.SelectedItem.ToString()
                Dim help1 As String() = help.Split(New Char() {" "c})
                autor.IDPessoa = help1(0)
            Else
                autor.IDPessoa = TB_IDPESSOA_A.Text
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        If adding Then
            submitAutor(autor)
            LB_AUTOR.Items.Add(autor)
        Else
            updateAutor(autor)
            LB_AUTOR.Items(currentSelectedAutor) = autor
        End If
        Return True
    End Function



    Private Sub updateAutor(ByRef autor As Autor)
        CN.Open()

        CMD.CommandText = "PROJECT.UPDATE_AUTOR @ID_PESSOA, @COD_AUTOR,@PN,@UN, @MORADA, @GENERO,@DATA_NASC, @TLM, @CC, @NIF"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@ID_PESSOA", autor.IDPessoa)
        CMD.Parameters.AddWithValue("@COD_AUTOR", autor.CodAutor)
        CMD.Parameters.AddWithValue("@PN", autor.PrimeiroNome)
        CMD.Parameters.AddWithValue("@UN", autor.UltimoNome)
        CMD.Parameters.AddWithValue("@CC", autor.Cc)
        CMD.Parameters.AddWithValue("@NIF", autor.Nif)
        CMD.Parameters.AddWithValue("@MORADA", autor.Morada)
        CMD.Parameters.AddWithValue("@GENERO", autor.Genero)
        CMD.Parameters.AddWithValue("@TLM", autor.Tlm)
        CMD.Parameters.AddWithValue("@DATA_NASC", Date.Parse(autor.DataNasc)) 'Hello'
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update author in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
        CN.Close()

    End Sub
    Private Sub submitAutor(ByVal autor As Autor)
        ''macro to check if fields r completed
        CN.Open()
        CMD.CommandText = "PROJECT.INSERT_AUTOR @ID_PESSOA, @COD_AUTOR"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@ID_PESSOA", autor.IDPessoa)
        CMD.Parameters.AddWithValue("@COD_AUTOR", autor.CodAutor)
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update author in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
        CN.Close()
    End Sub
    Private Sub Cancel_Autor_Click(sender As Object, e As EventArgs) Handles Cancel_Autor.Click
        LB_AUTOR.Enabled = True
        If LB_AUTOR.Items.Count > 0 Then
            currentSelectedAutor = LB_AUTOR.SelectedIndex
            If currentSelectedAutor < 0 Then currentSelectedAutor = 0
            ShowAutor()
        Else
            ClearFields(4)
            LockUnlockControls(4, False)
        End If
        HideShowButtons(4, True)
        LockUnlockControls(4, False)
        CB_IDPessoa_A.Hide()
        TB_IDPESSOA_A.Show()
    End Sub

    Private Sub Add_Bib_Click(sender As Object, e As EventArgs) Handles Add_Bib.Click
        adding = True
        add = True
        ClearFields(3)
        HideShowButtons(3, False)
        LB_BIB.Enabled = False
        GetFreePeople("Bibliotecario")
        GETBibliotecas(CB_CODBIB_B, TB_CODBIB_BIB)
        CB_IDPessoa_B.SelectedIndex = -1
        CB_CODBIB_B.SelectedIndex = -1
        Dim bool As Boolean = True
        Dim pk As String = ""
        Dim random As New Random
        While bool
            pk = ""
            For i As Integer = 0 To 8
                If i = 0 Then
                    Dim value As String = Convert.ToString(random.Next(1, 9))
                    pk = pk + value
                Else
                    Dim value As String = Convert.ToString(random.Next(0, 9))
                    pk = pk + value
                End If
            Next
            For Each obj In auxListBib
                If Equals(obj.CodBibliotecario, pk) Then
                    Continue For
                Else
                    bool = False
                End If
            Next
        End While
        TB_CODBIB.Text = pk
        TB_CODBIB.ReadOnly = True
    End Sub

    Private Sub Cancel_Bib_Click(sender As Object, e As EventArgs) Handles Cancel_Bib.Click
        LB_BIB.Enabled = True
        If LB_BIB.Items.Count > 0 Then
            currentSelectedBibliotecario = LB_BIB.SelectedIndex
            If currentSelectedBibliotecario < 0 Then currentSelectedBibliotecario = 0
            ShowBibliotecario()
        Else
            ClearFields(3)
            LockUnlockControls(3, False)
        End If
        HideShowButtons(3, True)
        LockUnlockControls(3, False)
        CB_IDPessoa_B.Hide()
        CB_CODBIB_B.Hide()
        TB_CODBIB_BIB.Show()
        TB_IDPESSOA_B.Show()
    End Sub

    Private Sub Confirm_Bib_Click(sender As Object, e As EventArgs) Handles Confirm_Bib.Click
        Try
            SaveBibliotecario()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        LB_BIB.Enabled = True
        Dim idx As Integer = LB_BIB.FindString(TB_PN_BIB.Text)
        LB_BIB.SelectedIndex = idx
        HideShowButtons(3, True)
        LockUnlockControls(3, False)
        CB_IDPessoa_B.Hide()
        CB_CODBIB_B.Hide()
        TB_CODBIB_BIB.Show()
        TB_IDPESSOA_B.Show()
    End Sub
    Private Function SaveBibliotecario() As Boolean
        Dim bib As New Bibliotecario
        Try
            bib.PrimeiroNome = TB_PN_BIB.Text
            bib.UltimoNome = TB_UN_BIB.Text
            bib.Cc = TB_CC_BIB.Text
            bib.Nif = TB_NIF_BIB.Text
            bib.Morada = TB_MORARA_BIB.Text
            bib.Genero = TB_GENERO_BIB.Text
            bib.Tlm = TB_TLM_BIB.Text
            bib.DataNasc = TB_DATAN_BIB.Value.ToString()
            bib.CodBibliotecario = TB_CODBIB.Text
            bib.Username = TB_USERNAME_BIB.Text
            bib.Password = TB_PASS_BIB.Text
            bib.Salary = TB_SALARIO_BIB.Text
            If add Then
                Dim help As String = CB_IDPessoa_B.SelectedItem.ToString()
                Dim help1 As String() = help.Split(New Char() {" "c})
                bib.IDPessoa = help1(0)
                bib.CodBiblioteca = CB_CODBIB_B.SelectedItem.ToString()
            Else
                bib.IDPessoa = TB_IDPESSOA_B.Text
                bib.CodBiblioteca = TB_CODBIB_BIB.Text
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        If adding Then
            submitBibliotecario(bib)
            LB_BIB.Items.Add(bib)
        Else
            UpdateBibliotecario(bib)
            LB_BIB.Items(currentSelectedBibliotecario) = bib
        End If
        Return True
    End Function
    Private Sub submitBibliotecario(ByVal bib As Bibliotecario)
        CN.Open()
        CMD.CommandText = "PROJECT.INSERT_BIBLIOTECARIO @COD_BIBLIOTECARIO , @ID_PESSOA, @COD_BIB, @SALARY, @USERNAME, @PASS"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@ID_PESSOA", bib.IDPessoa)
        CMD.Parameters.AddWithValue("@COD_BIB", bib.CodBiblioteca)
        CMD.Parameters.AddWithValue("@COD_BIBLIOTECARIO", bib.CodBibliotecario)
        CMD.Parameters.AddWithValue("@SALARY", Decimal.Parse(bib.Salary))
        CMD.Parameters.AddWithValue("@PASS", bib.Password)
        CMD.Parameters.AddWithValue("@USERNAME", bib.Username)
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to submit bibliotecario in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
        CN.Close()
    End Sub


    Private Sub UpdateBibliotecario(ByVal bib As Bibliotecario)
        CN.Open()
        CMD.CommandText = "PROJECT.UPDATE_BIBLIOTECARIO @ID_PESSOA, @COD_BIBLIOTECARIO , @COD_BIB,@USERNAME, @PASS,@SALARY,@PN,@UN, @MORADA, @GENERO,@DATA_NASC, @TLM, @CC, @NIF"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@ID_PESSOA", bib.IDPessoa)
        CMD.Parameters.AddWithValue("@COD_BIB", bib.CodBiblioteca)
        CMD.Parameters.AddWithValue("@COD_BIBLIOTECARIO", bib.CodBibliotecario)
        CMD.Parameters.AddWithValue("@SALARY", Decimal.Parse(bib.Salary))
        CMD.Parameters.AddWithValue("@PASS", bib.Password)
        CMD.Parameters.AddWithValue("@USERNAME", bib.Username)
        CMD.Parameters.AddWithValue("@PN", bib.PrimeiroNome)
        CMD.Parameters.AddWithValue("@UN", bib.UltimoNome)
        CMD.Parameters.AddWithValue("@CC", bib.Cc)
        CMD.Parameters.AddWithValue("@NIF", bib.Nif)
        CMD.Parameters.AddWithValue("@MORADA", bib.Morada)
        CMD.Parameters.AddWithValue("@GENERO", bib.Genero)
        CMD.Parameters.AddWithValue("@TLM", bib.Tlm)
        CMD.Parameters.AddWithValue("@DATA_NASC", Date.Parse(bib.DataNasc))
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update Reader in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
        CN.Close()
    End Sub

    Private Sub Add_Leitor_Click(sender As Object, e As EventArgs) Handles Add_Leitor.Click
        adding = True
        add = True
        ClearFields(2)
        HideShowButtons(2, False)
        LB_Leitor.Enabled = False
        GetFreePeople("Leitor")
        GETBibliotecas(CB_CODBIB_L, TB_COD_BIB_LEITOR)
        CB_IDPessoa_L.SelectedIndex = -1
        CB_CODBIB_L.SelectedIndex = -1

        Dim bool As Boolean = True
        Dim pk As String = ""
        Dim random As New Random
        While bool
            pk = ""
            For i As Integer = 0 To 8
                If i = 0 Then
                    Dim value As String = Convert.ToString(random.Next(1, 9))
                    pk = pk + value
                Else
                    Dim value As String = Convert.ToString(random.Next(0, 9))
                    pk = pk + value
                End If
            Next
            For Each obj In auxListLeitor
                If Equals(obj.CodLeitor, pk) Then
                    Continue For
                Else
                    bool = False
                End If
            Next
        End While
        TB_COD_LEITOR.Text = pk
        TB_COD_LEITOR.ReadOnly = True

    End Sub
    Private Sub GETBibliotecas(cb As ComboBox, tb As TextBox)
        cb.Show()
        tb.Hide()
        CMD.CommandText = "SELECT * FROM PROJECT.BIBLIOTECA"
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        CB_CODBIB_L.Items.Clear()
        auxListBiblioteca = New List(Of Biblioteca)
        While RDR.Read
            Dim Biblioteca As New Biblioteca
            Biblioteca.CodBiblioteca = RDR.Item("COD_BIB")
            Biblioteca.Nome = RDR.Item("NOME")
            Biblioteca.Endereco = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("ENDERECO")), "", RDR.Item("ENDERECO")))
            cb.Items.Add(Biblioteca.CodBiblioteca)
            auxListBiblioteca.Add(Biblioteca)
        End While
        CN.Close()
    End Sub
    Private Sub Cancel_Leitor_Click(sender As Object, e As EventArgs) Handles Cancel_Leitor.Click
        LB_Leitor.Enabled = True
        If LB_Leitor.Items.Count > 0 Then
            currentSelectedLeitor = LB_Leitor.SelectedIndex
            If currentSelectedLeitor < 0 Then currentSelectedLeitor = 0
            ShowLeitor()
        Else
            ClearFields(2)
            LockUnlockControls(2, False)
        End If
        HideShowButtons(2, True)
        LockUnlockControls(2, False)
        CB_IDPessoa_L.Hide()
        CB_CODBIB_L.Hide()
        TB_COD_BIB_LEITOR.Show()
        TB_IDPESSOA_L.Show()
    End Sub

    Private Sub Confirm_leitor_Click(sender As Object, e As EventArgs) Handles Confirm_leitor.Click
        Try
            SaveLeitor()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        LB_Leitor.Enabled = True
        Dim idx As Integer = LB_Leitor.FindString(TB_PM_LEITOR.Text)
        LB_Leitor.SelectedIndex = idx
        HideShowButtons(2, True)
        LockUnlockControls(2, False)
        CB_IDPessoa_L.Hide()
        CB_CODBIB_L.Hide()
        TB_IDPESSOA_L.Show()
        TB_COD_BIB_LEITOR.Show()
    End Sub
    Private Function SaveLeitor() As Boolean
        Dim leitor As New Leitor
        Try
            leitor.PrimeiroNome = TB_PM_LEITOR.Text
            leitor.UltimoNome = TB_UN_LEITOR.Text
            leitor.Cc = TB_CC_LEITOR.Text
            leitor.Nif = TB_NIF_LEITOR.Text
            leitor.Morada = TB_MORADA_LEITOR.Text
            leitor.Genero = TB_GENERO_LEITOR.Text
            leitor.Tlm = TB_TLM_LEITOR.Text
            leitor.DataNasc = TB_DATAN_LEITOR.Value.ToString()
            leitor.CodLeitor = TB_COD_LEITOR.Text
            leitor.Username = TB_USERNAME_LEITOR.Text
            leitor.Password = TB_PASS_LEITOR.Text
            leitor.DataRegisto = TB_DATAR_LEITOR.Value.ToString()
            leitor.DataExpiro = TB_DATAE_LEITOR.Value.ToString()
            If add Then
                leitor.CodBiblioteca = CB_CODBIB_L.Text()
                Dim help As String = CB_IDPessoa_L.SelectedItem.ToString()
                Dim help1 As String() = help.Split(New Char() {" "c})
                leitor.IDPessoa = help1(0)

            Else
                leitor.CodBiblioteca = TB_COD_BIB_LEITOR.Text
                leitor.IDPessoa = TB_IDPESSOA_L.Text
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        If adding Then
            submitLeitor(leitor)
            LB_Leitor.Items.Add(leitor)
            LB_Leitor.Items(currentSelectedLeitor) = leitor
        Else
            updateLeitor(leitor)
            LB_Leitor.Items(currentSelectedLeitor) = leitor
        End If
        Return True
    End Function


    Private Sub updateLeitor(ByVal leitor As Leitor)
        CN.Open()
        CMD.CommandText = "PROJECT.UPDATE_LEITOR @ID_PESSOA, @COD_LEITOR, @COD_BIB,  @USERNAME, @PASS,@DATA_EXP, @DATA_REG,@PN,@UN, @MORADA, @GENERO,@DATA_NASC,@TLM, @CC, @NIF "
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@ID_PESSOA", leitor.IDPessoa)
        CMD.Parameters.AddWithValue("@COD_BIB", leitor.CodBiblioteca)
        CMD.Parameters.AddWithValue("@COD_LEITOR", leitor.CodLeitor)
        CMD.Parameters.AddWithValue("@DATA_EXP", Date.Parse(leitor.DataExpiro)) 'Hello'
        CMD.Parameters.AddWithValue("@DATA_REG", Date.Parse(leitor.DataRegisto)) 'Hello'
        CMD.Parameters.AddWithValue("@PASS", leitor.Password)
        CMD.Parameters.AddWithValue("@USERNAME", leitor.Username)
        CMD.Parameters.AddWithValue("@PN", leitor.PrimeiroNome)
        CMD.Parameters.AddWithValue("@UN", leitor.UltimoNome)
        CMD.Parameters.AddWithValue("@CC", leitor.Cc)
        CMD.Parameters.AddWithValue("@NIF", leitor.Nif)
        CMD.Parameters.AddWithValue("@MORADA", leitor.Morada)
        CMD.Parameters.AddWithValue("@GENERO", leitor.Genero)
        CMD.Parameters.AddWithValue("@TLM", leitor.Tlm)
        CMD.Parameters.AddWithValue("@DATA_NASC", Date.Parse(leitor.DataNasc))
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update Reader in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
        CN.Close()


    End Sub



    Private Sub submitLeitor(ByVal leitor As Leitor)
        CN.Open()
        CMD.CommandText = "PROJECT.INSERT_LEITOR @COD_LEITOR, @ID_PESSOA, @USERNAME, @PASS, @DATA_EXP, @DATA_REG, @COD_BIB"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@ID_PESSOA", leitor.IDPessoa)
        CMD.Parameters.AddWithValue("@COD_BIB", leitor.CodBiblioteca)
        CMD.Parameters.AddWithValue("@COD_LEITOR", leitor.CodLeitor)
        CMD.Parameters.AddWithValue("@DATA_EXP", Date.Parse(leitor.DataExpiro)) 'Hello'
        CMD.Parameters.AddWithValue("@DATA_REG", Date.Parse(leitor.DataRegisto)) 'Hello'
        CMD.Parameters.AddWithValue("@PASS", leitor.Password)
        CMD.Parameters.AddWithValue("@USERNAME", leitor.Username)
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to submit Reader in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
        CN.Close()

    End Sub

    Private Sub Add_Fab_Click(sender As Object, e As EventArgs) Handles Add_Fab.Click
        adding = True
        ClearFields(13)
        HideShowButtons(13, False)
        LB_FABRICANTES.Enabled = False
    End Sub

    Private Sub Cancel_Fab_Click(sender As Object, e As EventArgs) Handles Cancel_Fab.Click
        LB_FABRICANTES.Enabled = True
        If LB_FABRICANTES.Items.Count > 0 Then
            currentSelectedFabricante = LB_FABRICANTES.SelectedIndex
            If currentSelectedFabricante < 0 Then currentSelectedFabricante = 0
            ShowFabricantes()
        Else
            ClearFields(13)
            LockUnlockControls(13, False)
        End If
        HideShowButtons(13, True)
        LockUnlockControls(13, False)
    End Sub

    Private Sub Confirm_Fab_Click(sender As Object, e As EventArgs) Handles Confirm_Fab.Click
        Try
            SaveFabricante()
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        LB_FABRICANTES.Enabled = True
        Dim idx As Integer = LB_FABRICANTES.FindString(TB_CODFABRICANTE.Text)
        LB_FABRICANTES.SelectedIndex = idx
        HideShowButtons(13, True)
        LockUnlockControls(13, False)
    End Sub
    Private Function SaveFabricante() As Boolean
        Dim fabricante As New Fabricante
        Try
            fabricante.CodFabricante = TB_CODFABRICANTE.Text
            fabricante.Fabricante = TB_FABRICANTE.Text
            fabricante.Endereco = TB_ENDERECO_FAB.Text
            fabricante.Telefone = TB_TELEFONE_FAB.Text

        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        If adding Then
            submitFabricante(fabricante)
            LB_Leitor.Items.Add(fabricante)
        Else
            UpdateFabricante(fabricante)
            LB_FABRICANTES.Items(currentSelectedFabricante) = fabricante
        End If
        Return True
    End Function
    Private Sub submitFabricante(ByVal fabricante As Fabricante)
        CMD.CommandText = "INSERT INTO PROJECT.FABRICANTE (COD_FABRICANTE, FABRICANTE, ENDERECO, TELEFONE) " &
                          "VALUES (@CodFab, @Fabricante, @Endereco, @Telefone) "
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodFab", fabricante.CodFabricante)
        CMD.Parameters.AddWithValue("@Fabricante", fabricante.Fabricante)
        CMD.Parameters.AddWithValue("@Endereco", fabricante.Endereco)
        CMD.Parameters.AddWithValue("@Telefone", fabricante.Telefone)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update Fabricante in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
        CN.Close()
    End Sub

    Private Sub UpdateFabricante(ByVal fabricante As Fabricante)
        CMD.CommandText = "UPDATE PROJECT.FABRICANTE " &
            "SET FABRICANTE = @Fabricante, " &
            "    ENDERECO = @Endereco, " &
            "    TELEFONE = @Telefone " &
            "WHERE COD_FABRICANTE = @CodFab"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodFab", fabricante.CodFabricante)
        CMD.Parameters.AddWithValue("@Fabricante", fabricante.Fabricante)
        CMD.Parameters.AddWithValue("@Endereco", fabricante.Endereco)
        CMD.Parameters.AddWithValue("@Telefone", fabricante.Telefone)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update contact in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
    End Sub

    Private Sub RemoveFabricante(ByVal COD_FABRICANTE As String)
        CMD.CommandText = "DELETE PROJECT.FABRICANTE WHERE COD_FABRICANTE=@CodFab "
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodFab", COD_FABRICANTE)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to delete Fabricante in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
    End Sub

    Private Sub Add_Tipo_Click(sender As Object, e As EventArgs) Handles Add_Tipo.Click
        adding = True
        ClearFields(12)
        HideShowButtons(12, False)
        LB_TIPOS.Enabled = False
    End Sub

    Private Sub Cancel_Tipo_Click(sender As Object, e As EventArgs) Handles Cancel_Tipo.Click
        LB_TIPOS.Enabled = True
        If LB_TIPOS.Items.Count > 0 Then
            currentSelectedTipo = LB_TIPOS.SelectedIndex
            If currentSelectedTipo < 0 Then currentSelectedTipo = 0
            ShowTipos()
        Else
            ClearFields(12)
            LockUnlockControls(12, False)
        End If
        HideShowButtons(12, True)
        LockUnlockControls(12, False)
    End Sub

    Private Sub Confirm_Tipo_Click(sender As Object, e As EventArgs) Handles Confirm_Tipo.Click
        Try
            SaveTipo()
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        LB_TIPOS.Enabled = True
        Dim idx As Integer = LB_TIPOS.FindString(TB_CODTIPO.Text)
        LB_TIPOS.SelectedIndex = idx
        HideShowButtons(12, True)
        LockUnlockControls(12, False)
    End Sub
    Private Function SaveTipo() As Boolean
        Dim tipo As New Tipo
        Try
            tipo.CodTipo = TB_CODTIPO.Text
            tipo.Tipo = TB_TIPO.Text

        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        If adding Then
            submitTipo(tipo)
            LB_TIPOS.Items.Add(tipo)
        Else
            UpdateTipo(tipo)
            LB_TIPOS.Items(currentSelectedTipo) = tipo
        End If
        Return True
    End Function
    Private Sub submitTipo(ByVal tipo As Tipo)
        CMD.CommandText = "INSERT INTO PROJECT.TIPO (COD_FABRICANTE, FABRICANTE) " &
                          "VALUES (@CodTipo, @Tipo) "
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodTipo", tipo.CodTipo)
        CMD.Parameters.AddWithValue("@Tipo", tipo.Tipo)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update tipo in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
        CN.Close()
    End Sub

    Private Sub UpdateTipo(ByVal T As Tipo)
        CMD.CommandText = "UPDATE PROJECT.TIPO " &
            "SET TIPO = @Tipo " &
            "WHERE COD_TIPO = @CodTipo"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodTipo", T.CodTipo)
        CMD.Parameters.AddWithValue("@Tipo", T.Tipo)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update contact in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
    End Sub

    Private Sub Add_Categoria_Click(sender As Object, e As EventArgs) Handles Add_Categoria.Click
        adding = True
        ClearFields(11)
        HideShowButtons(11, False)
        LB_CATEGORIA.Enabled = False
    End Sub

    Private Sub Cancel_Categoria_Click(sender As Object, e As EventArgs) Handles Cancel_Categoria.Click
        LB_CATEGORIA.Enabled = True
        If LB_CATEGORIA.Items.Count > 0 Then
            currentSelectedCategoria = LB_CATEGORIA.SelectedIndex
            If currentSelectedCategoria < 0 Then currentSelectedCategoria = 0
            ShowCategorias()
        Else
            ClearFields(11)
            LockUnlockControls(11, False)
        End If
        HideShowButtons(11, True)
        LockUnlockControls(11, False)
    End Sub

    Private Sub Confirm_Categoria_Click(sender As Object, e As EventArgs) Handles Confirm_Categoria.Click
        Try
            SaveCategoria()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        LB_CATEGORIA.Enabled = True
        Dim idx As Integer = LB_CATEGORIA.FindString(TB_CODCATEGORIA.Text)
        LB_CATEGORIA.SelectedIndex = idx
        HideShowButtons(11, True)
        LockUnlockControls(11, False)
    End Sub
    Private Function SaveCategoria() As Boolean
        Dim categoria As New Categoria
        Try
            categoria.CodCategoria = TB_CODCATEGORIA.Text
            categoria.Categoria = TB_CATEGORIA.Text

        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        If adding Then
            submitCategoria(categoria)
            LB_CATEGORIA.Items.Add(categoria)
        Else
            UpdateCategoria(categoria)
            LB_CATEGORIA.Items(currentSelectedCategoria) = categoria
        End If
        Return True
    End Function
    Private Sub submitCategoria(ByVal categoria As Categoria)
        CMD.CommandText = "INSERT INTO PROJECT.CATEGORIA (COD_CATEGORIA, CATEGORIA) " &
                          "VALUES (@CodCategoria, @Categoria) "
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodCategoria", categoria.CodCategoria)
        CMD.Parameters.AddWithValue("@Categoria", categoria.Categoria)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update categoria in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
        CN.Close()
    End Sub

    Private Sub UpdateCategoria(ByVal C As Categoria)
        CMD.CommandText = "UPDATE PROJECT.CATEGORIA " &
            "SET CATEGORIA = @Categoria " &
            "WHERE COD_CATEGORIA = @CodCategoria"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodCategoria", C.CodCategoria)
        CMD.Parameters.AddWithValue("@Categoria", C.Categoria)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update contact in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
    End Sub

    Private Sub Add_Editora_Click(sender As Object, e As EventArgs) Handles Add_Editora.Click
        adding = True
        ClearFields(10)
        HideShowButtons(10, False)
        LB_Editora.Enabled = False
    End Sub

    Private Sub Cancel_Editora_Click(sender As Object, e As EventArgs) Handles Cancel_Editora.Click
        LB_Editora.Enabled = True
        If LB_Editora.Items.Count > 0 Then
            currentSelectedEditora = LB_Editora.SelectedIndex
            If currentSelectedEditora < 0 Then currentSelectedEditora = 0
            ShowEditoras()
        Else
            ClearFields(10)
            LockUnlockControls(10, False)
        End If
        HideShowButtons(10, True)
        LockUnlockControls(10, False)
    End Sub

    Private Sub Confirm_editora_Click(sender As Object, e As EventArgs) Handles Confirm_editora.Click
        Try
            SaveEditora()
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        LB_Editora.Enabled = True
        Dim idx As Integer = LB_Editora.FindString(TB_CODEDITORA.Text)
        LB_Editora.SelectedIndex = idx
        HideShowButtons(10, True)
        LockUnlockControls(10, False)
    End Sub
    Private Function SaveEditora() As Boolean
        Dim editora As New Editora
        Try
            editora.CodEditora = TB_CODEDITORA.Text
            editora.NomeEditora = TB_NOME_EDITORA.Text
            editora.Endereco = TB_ENDERECO_EDITORA.Text
            editora.Telefone = TB_TELEFONE_EDITORA.Text

        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        If adding Then
            submitEditora(editora)
            LB_Editora.Items.Add(editora)
        Else
            UpdateEditora(editora)
            LB_Editora.Items(currentSelectedEditora) = editora
        End If
        Return True
    End Function
    Private Sub submitEditora(ByVal editora As Editora)
        CMD.CommandText = "INSERT INTO PROJECT.EDITORA (COD_EDITORA, NOME, ENDERECO, TELEFONE) " &
                          "VALUES (@CodEditora, @NomeEditora, @Endereco, @Telefone) "
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodEditora", editora.CodEditora)
        CMD.Parameters.AddWithValue("@NomeEditora", editora.NomeEditora)
        CMD.Parameters.AddWithValue("@Endereco", editora.Endereco)
        CMD.Parameters.AddWithValue("@Telefone", editora.Telefone)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to submit editora in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
        CN.Close()
    End Sub

    Private Sub UpdateEditora(ByVal E As Editora)
        CMD.CommandText = "UPDATE PROJECT.EDITORA " &
            "SET NOME = @NomeEditora, " &
            "    ENDERECO = @Endereco, " &
            "    TELEFONE = @Telefone " &
            "WHERE COD_EDITORA = @CodEditora"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodEditora", E.CodEditora)
        CMD.Parameters.AddWithValue("@NomeEditora", E.NomeEditora)
        CMD.Parameters.AddWithValue("@Endereco", E.Endereco)
        CMD.Parameters.AddWithValue("@Telefone", E.Telefone)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update editora in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
    End Sub

    Private Sub Add_biblioteca_Click(sender As Object, e As EventArgs) Handles Add_biblioteca.Click
        adding = True
        ClearFields(9)
        HideShowButtons(9, False)
        LB_Biblioteca.Enabled = False
    End Sub

    Private Sub Cancel_Biblioteca_Click(sender As Object, e As EventArgs) Handles Cancel_Biblioteca.Click
        LB_Biblioteca.Enabled = True
        If LB_Biblioteca.Items.Count > 0 Then
            currentSelectedBiblioteca = LB_Biblioteca.SelectedIndex
            If currentSelectedBiblioteca < 0 Then currentSelectedBiblioteca = 0
            ShowBibliotecas()
        Else
            ClearFields(9)
            LockUnlockControls(9, False)
        End If
        HideShowButtons(9, True)
        LockUnlockControls(9, False)
    End Sub

    Private Sub Confirm_Biblioteca_Click(sender As Object, e As EventArgs) Handles Confirm_Biblioteca.Click
        Try
            SaveBiblioteca()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        LB_Biblioteca.Enabled = True
        Dim idx As Integer = LB_Biblioteca.FindString(TB_NOME_BIB.Text)
        LB_Biblioteca.SelectedIndex = idx
        HideShowButtons(9, True)
    End Sub
    Private Function SaveBiblioteca() As Boolean
        Dim biblioteca As New Biblioteca
        Try
            biblioteca.CodBiblioteca = TB_CODIGO_BIB.Text
            biblioteca.Nome = TB_NOME_BIB.Text
            biblioteca.Endereco = TB_ENDERECO_BIB.Text

        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        If adding Then
            submitBiblioteca(biblioteca)
            LB_Biblioteca.Items.Add(biblioteca)
        Else
            UpdateBiblioteca(biblioteca)
            LB_Biblioteca.Items(currentSelectedBiblioteca) = biblioteca
        End If
        Return True
    End Function
    Private Sub submitBiblioteca(ByVal biblioteca As Biblioteca)
        CMD.CommandText = "INSERT INTO PROJECT.BIBLIOTECA (COD_BIB, NOME, ENDERECO) " &
                          "VALUES (@CodBiblioteca, @NomeBiblioteca, @Endereco) "
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodBiblioteca", biblioteca.CodBiblioteca)
        CMD.Parameters.AddWithValue("@NomeBiblioteca", biblioteca.Nome)
        CMD.Parameters.AddWithValue("@Endereco", biblioteca.Endereco)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update biblioteca in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
        CN.Close()
    End Sub

    Private Sub UpdateBiblioteca(ByVal B As Biblioteca)
        CMD.CommandText = "UPDATE PROJECT.BIBLIOTECA " &
            "SET NOME = @NomeBiblioteca, " &
            "    ENDERECO = @Endereco " &
            "WHERE COD_BIB = @CodBiblioteca"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@NomeBiblioteca", B.Nome)
        CMD.Parameters.AddWithValue("@Endereco", B.Endereco)
        CMD.Parameters.AddWithValue("@CodBiblioteca", B.CodBiblioteca)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update contact in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
    End Sub

    Private Sub Add_IE_Click(sender As Object, e As EventArgs) Handles Add_IE.Click
        adding = True
        add = True
        ClearFields(6)
        HideShowButtons(6, False)
        LB_ITEM_ELECT.Enabled = False
        CB_CODTIPO_IE.Show()
        CB_CODBIB_IE.Show()
        CB_CODFABRICANTE_IE.Show()
        TB_CODTIPO_IE.Hide()
        TB_CODFABRICANTE_IE.Hide()
        TB_CODBIB_IE.Hide()
        CB_CODTIPO_IE.SelectedIndex = -1
        CB_CODFABRICANTE_IE.SelectedIndex = -1
        CB_CODBIB_IE.SelectedItem = -1
        GETBibliotecas(CB_CODBIB_IE, TB_CODBIB_IE)
        setToDD(False)
        Dim bool As Boolean = True
        Dim pk As String = ""
        Dim random As New Random
        While bool
            pk = ""
            For i As Integer = 0 To 8
                If i = 0 Then
                    Dim value As String = Convert.ToString(random.Next(1, 9))
                    pk = pk + value
                Else
                    Dim value As String = Convert.ToString(random.Next(0, 9))
                    pk = pk + value
                End If
            Next
            For Each obj In auxListIE
                If Equals(obj.CodItemEletronica, pk) Then
                    Continue For
                Else
                    bool = False
                End If
            Next
        End While
        TB_CODITEMELECT_IE.Text = pk
        TB_CODITEMELECT_IE.ReadOnly = True
    End Sub

    Private Sub Cancel_IE_Click(sender As Object, e As EventArgs) Handles Cancel_IE.Click
        LB_ITEM_ELECT.Enabled = True
        If LB_ITEM_ELECT.Items.Count > 0 Then
            currentSelectedItemElect = LB_ITEM_ELECT.SelectedIndex
            If currentSelectedItemElect < 0 Then currentSelectedItemElect = 0
            ShowItemElect()
        Else
            ClearFields(6)
            LockUnlockControls(6, False)
        End If
        HideShowButtons(6, True)
        LockUnlockControls(6, False)
        CB_CODTIPO_IE.Hide()
        CB_CODBIB_IE.Hide()
        CB_CODFABRICANTE_IE.Hide()
        TB_CODTIPO_IE.Show()
        TB_CODBIB_IE.Show()
        TB_CODFABRICANTE_IE.Show()
    End Sub

    Private Sub Confirm_IE_Click(sender As Object, e As EventArgs) Handles Confirm_IE.Click
        Try
            SaveItemElect()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        LB_ITEM_ELECT.Enabled = True
        Dim idx As Integer = LB_ITEM_ELECT.FindString(TB_CODITEMELECT_IE.Text)
        LB_ITEM_ELECT.SelectedIndex = idx
        CB_CODTIPO_IE.Hide()
        CB_CODBIB_IE.Hide()
        CB_CODFABRICANTE_IE.Hide()
        TB_CODTIPO_IE.Show()
        TB_CODBIB_IE.Show()
        TB_CODFABRICANTE_IE.Show()

        HideShowButtons(6, True)
        LockUnlockControls(6, False)
    End Sub
    Private Function SaveItemElect() As Boolean
        Dim ie As New ItemEletronica
        Try
            If add Then
                ie.CodItemEletronica = TB_CODITEMELECT_IE.Text
                ie.CodBib = CB_CODBIB_IE.SelectedItem.ToString()
                Dim help As String = CB_CODTIPO_IE.SelectedItem.ToString()
                Dim help1 As String() = help.Split(New Char() {" "c})
                ie.CodTipo = help1(0)
                help = CB_CODFABRICANTE_IE.SelectedItem.ToString()
                help1 = help.Split(New Char() {" "c})
                ie.CodFabrincante = help1(0)
            Else
                ie.CodItemEletronica = TB_CODITEMELECT_IE.Text
                ie.CodBib = TB_CODBIB_IE.Text
                ie.CodTipo = TB_CODTIPO_IE.Text
                ie.CodFabrincante = TB_CODFABRICANTE_IE.Text
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        If adding Then
            submitItemElect(ie)
            LB_ITEM_ELECT.Items.Add(ie)
        Else
            UpdateItemEletronica(ie)
            LB_ITEM_ELECT.Items(currentSelectedItemElect) = ie
        End If
        Return True
    End Function
    Private Sub submitItemElect(ByVal ie As ItemEletronica)
        CMD.CommandText = "INSERT INTO PROJECT.ITEM_ELECTRONICA (COD_ITEM_ELECT, COD_BIB, COD_TIPO, COD_FABRICANTE) " &
                          "VALUES (@CodItemElect, @CodBiblioteca, @CodTipo, @CodFabricante) "
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodItemElect", ie.CodItemEletronica)
        CMD.Parameters.AddWithValue("@CodBiblioteca", ie.CodBib)
        CMD.Parameters.AddWithValue("@CodTipo", ie.CodTipo)
        CMD.Parameters.AddWithValue("@CodFabricante", ie.CodFabrincante)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update ItemElectronica in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
        CN.Close()
    End Sub

    Private Sub UpdateItemEletronica(ByVal IE As ItemEletronica)
        CN.Open()
        CMD.CommandText = "UPDATE PROJECT.ITEM_ELECTRONICA " &
                                        "SET COD_BIB = @CodBib, " &
                                        "    COD_Tipo = @CodTipo, " &
                                        "    COD_FABRICANTE = @CodFabricante " &
                          "WHERE COD_ITEM_ELECT = @CodIE"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodBib", IE.CodBib)
        CMD.Parameters.AddWithValue("@CodtIPO", IE.CodTipo)
        CMD.Parameters.AddWithValue("@CodFabricante", IE.CodFabrincante)
        CMD.Parameters.AddWithValue("@CodIE", IE.CodItemEletronica)
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update ReqItemElectronica in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
        CN.Close()
    End Sub



    Private Sub Add_IP_Click(sender As Object, e As EventArgs) Handles Add_IP.Click
        adding = True
        add = True
        ClearFields(5)
        HideShowButtons(5, False)
        LB_ITEMS_PAPEL.Enabled = False
        CB_CODTIPO_IP.Show()
        CB_EDITORA_IP.Show()
        CB_CODCATEGORIA_IP.Show()
        CB_CODBIB_IP.Show()
        CB_CODAUTOR_IP.Show()
        TB_CODTIPO_IP.Hide()
        TB_CODEDITORA_IP.Hide()
        TB_CODCATEGORIA_IP.Hide()
        TB_CODAUTOR_IP.Hide()
        TB_CODBIB_IP.Hide()
        CB_CODTIPO_IP.SelectedIndex = -1
        CB_EDITORA_IP.SelectedIndex = -1
        CB_CODCATEGORIA_IP.SelectedIndex = -1
        CB_CODAUTOR_IP.SelectedIndex = -1
        CB_CODBIB_IP.SelectedItem = -1
        setToDD(True)
        GETBibliotecas(CB_CODBIB_IP, TB_CODBIB_IP)
        Dim bool As Boolean = True
        Dim pk As String = ""
        Dim random As New Random
        While bool
            pk = ""
            For i As Integer = 0 To 8
                If i = 0 Then
                    Dim value As String = Convert.ToString(random.Next(1, 9))
                    pk = pk + value
                Else
                    Dim value As String = Convert.ToString(random.Next(0, 9))
                    pk = pk + value
                End If
            Next
            For Each obj In auxListIP
                If Equals(obj.CodItemPapel, pk) Then
                    Continue For
                Else
                    bool = False
                End If
            Next
        End While
        TB_CODITEMPAPEL_IP.Text = pk
        TB_CODITEMPAPEL_IP.ReadOnly = True
    End Sub
    Private Sub setToDD(bool As Boolean)
        If bool Then    'IP
            CMD.CommandText = "SELECT * FROM PROJECT.TIPO"
            CN.Open()
            Dim RDR As SqlDataReader
            RDR = CMD.ExecuteReader
            CB_CODTIPO_IP.Items.Clear()
            auxListTipo = New List(Of Tipo)
            While RDR.Read
                Dim T As New Tipo
                T.CodTipo = RDR.Item("COD_TIPO")
                T.Tipo = RDR.Item("TIPO")
                CB_CODTIPO_IP.Items.Add(T)
                auxListTipo.Add(T)
            End While
            CN.Close()
            CMD.CommandText = "SELECT * FROM PROJECT.EDITORA"
            CN.Open()
            RDR = CMD.ExecuteReader
            auxListEditora = New List(Of Editora)
            CB_EDITORA_IP.Items.Clear()
            While RDR.Read
                Dim Ed As New Editora
                Ed.CodEditora = RDR.Item("COD_EDITORA")
                Ed.NomeEditora = RDR.Item("NOME")
                Ed.Endereco = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("ENDERECO")), "", RDR.Item("ENDERECO")))
                Ed.Telefone = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("TELEFONE")), "", RDR.Item("TELEFONE")))
                CB_EDITORA_IP.Items.Add(Ed)
                auxListEditora.Add(Ed)
            End While
            CN.Close()
            CMD.CommandText = "SELECT * FROM PROJECT.CATEGORIA"
            CN.Open()
            RDR = CMD.ExecuteReader
            CB_CODCATEGORIA_IP.Items.Clear()
            auxListCategoria = New List(Of Categoria)
            While RDR.Read
                Dim C As New Categoria
                C.CodCategoria = RDR.Item("COD_CATEGORIA")
                C.Categoria = RDR.Item("CATEGORIA")
                CB_CODCATEGORIA_IP.Items.Add(C)
                auxListCategoria.Add(C)
            End While
            CN.Close()
            CMD.CommandText = "SELECT * FROM PROJECT.AUTOR JOIN PROJECT.PESSOA ON PROJECT.AUTOR.ID_PESSOA = PROJECT.PESSOA.ID_PESSOA"
            CN.Open()
            RDR = CMD.ExecuteReader
            CB_CODAUTOR_IP.Items.Clear()
            auxListAutor = New List(Of Autor)
            While RDR.Read
                Dim A As New Autor
                A.CodAutor = RDR.Item("COD_AUTOR")
                A.PrimeiroNome = RDR.Item("PRIMEIRO_NOME")
                A.UltimoNome = RDR.Item("ULTIMO_NOME")
                A.IDPessoa = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("ID_PESSOA")), "", RDR.Item("ID_PESSOA")))
                A.Morada = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("MORADA")), "", RDR.Item("MORADA")))
                A.Tlm = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("TLM")), "", RDR.Item("TLM")))
                A.Cc = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("CC")), "", RDR.Item("CC")))
                A.Nif = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("NIF")), "", RDR.Item("NIF")))
                A.DataNasc = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DATA_NASC")), "", RDR.Item("DATA_NASC")))
                A.Genero = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("GENERO")), "", RDR.Item("GENERO")))
                CB_CODAUTOR_IP.Items.Add(A.teste1())
                auxListAutor.Add(A)
            End While
            CN.Close()
        Else    'IE
            CMD.CommandText = "SELECT * FROM PROJECT.TIPO"
            CN.Open()
            Dim RDR As SqlDataReader
            RDR = CMD.ExecuteReader
            CB_CODTIPO_IE.Items.Clear()
            auxListTipo = New List(Of Tipo)
            While RDR.Read
                Dim T As New Tipo
                T.CodTipo = RDR.Item("COD_TIPO")
                T.Tipo = RDR.Item("TIPO")
                CB_CODTIPO_IE.Items.Add(T)
                auxListTipo.Add(T)
            End While
            CN.Close()
            CMD.CommandText = "select * from project.fabricante"
            CN.Open()
            RDR = CMD.ExecuteReader
            CB_CODFABRICANTE_IE.Items.Clear()
            auxListFabricante = New List(Of Fabricante)
            While RDR.Read
                Dim f As New Fabricante
                f.CodFabricante = RDR.Item("cod_fabricante")
                f.Fabricante = RDR.Item("fabricante")
                f.Endereco = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("endereco")), "", RDR.Item("endereco")))
                f.Telefone = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("telefone")), "", RDR.Item("telefone")))
                CB_CODFABRICANTE_IE.Items.Add(f)
                auxListFabricante.Add(f)
            End While
            CN.Close()
        End If
    End Sub
    Private Sub Cancel_IP_Click(sender As Object, e As EventArgs) Handles Cancel_IP.Click
        LB_ITEMS_PAPEL.Enabled = True
        If LB_ITEMS_PAPEL.Items.Count > 0 Then
            currentSelectedItemPapel = LB_ITEMS_PAPEL.SelectedIndex
            If currentSelectedItemPapel < 0 Then currentSelectedItemPapel = 0
            ShowItemPapel()
        Else
            ClearFields(5)
            LockUnlockControls(5, False)
        End If
        HideShowButtons(5, True)
        LockUnlockControls(5, False)
        CB_CODTIPO_IP.Hide()
        CB_EDITORA_IP.Hide()
        CB_CODCATEGORIA_IP.Hide()
        CB_CODBIB_IP.Hide()
        CB_CODAUTOR_IP.Hide()
        TB_CODTIPO_IP.Show()
        TB_CODEDITORA_IP.Show()
        TB_CODCATEGORIA_IP.Show()
        TB_CODAUTOR_IP.Show()
        TB_CODBIB_IP.Show()
    End Sub

    Private Sub Confirm_IP_Click(sender As Object, e As EventArgs) Handles Confirm_IP.Click
        Try
            SaveItemPapel()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        LB_ITEMS_PAPEL.Enabled = True
        Dim idx As Integer = LB_ITEMS_PAPEL.FindString(TB_CODITEMPAPEL_IP.Text)
        LB_ITEMS_PAPEL.SelectedIndex = idx
        HideShowButtons(5, True)
        LockUnlockControls(5, False)
        CB_CODTIPO_IP.Hide()
        CB_EDITORA_IP.Hide()
        CB_CODCATEGORIA_IP.Hide()
        CB_CODAUTOR_IP.Hide()
        CB_CODBIB_IP.Hide()
        TB_CODTIPO_IP.Show()
        TB_CODEDITORA_IP.Show()
        TB_CODCATEGORIA_IP.Show()
        TB_CODAUTOR_IP.Show()
        TB_CODBIB_IP.Show()
    End Sub
    Private Function SaveItemPapel() As Boolean
        Dim ip As New ItemPapel
        Try
            ip.CodItemPapel = TB_CODITEMPAPEL_IP.Text
            ip.Titulo = TB_TITULO_IP.Text
            ip.Edicao = TB_EDICAO_IP.Text
            ip.Idioma = TB_IDIOMA_IP.Text
            ip.Permissao = TB_PERMISSAO_IP.Text
            ip.Dimensoes = TB_DIMENSOES_IP.Text
            ip.Volume = TB_VOLUME_IP.Text
            ip.DataPub = TB_DATAPUB_IP.Value.ToString()
            ip.Classificacao = TB_CLASSIFICACAO_IP.Text
            ip.Descricao = TB_DESCRICAO_IP.Text
            ip.Cota = TB_COTA_IP.Text
            If add Then
                Dim help As String = CB_CODTIPO_IP.SelectedItem.ToString()
                Dim help1 As String() = help.Split(New Char() {" "c})
                ip.CodTipo = help1(0)
                help = CB_CODCATEGORIA_IP.SelectedItem.ToString()
                help1 = help.Split(New Char() {" "c})
                ip.CodCategoria = help1(0)
                help = CB_EDITORA_IP.SelectedItem.ToString()
                help1 = help.Split(New Char() {" "c})
                ip.CodEditora = help1(0)
                help = CB_CODAUTOR_IP.SelectedItem.ToString()
                help1 = help.Split(New Char() {" "c})
                ip.CodAutor = help1(0)
                ip.CodBib = CB_CODBIB_IP.SelectedItem.ToString()
            Else
                ip.CodTipo = TB_CODTIPO_IP.Text
                ip.CodCategoria = TB_CODCATEGORIA_IP.Text
                ip.CodEditora = TB_CODEDITORA_IP.Text
                ip.CodAutor = TB_CODAUTOR_IP.Text
                ip.CodBib = TB_CODBIB_IP.Text
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        If adding Then
            submitItemPapel(ip)
            LB_ITEMS_PAPEL.Items.Add(ip)

        Else
            UpdateItemPapel(ip)
            LB_ITEMS_PAPEL.Items.Add(ip)
            LB_ITEMS_PAPEL.Items(currentSelectedItemPapel) = ip
        End If
        Return True
    End Function
    Private Sub submitItemPapel(ByVal ip As ItemPapel)
        CMD.CommandText = "INSERT INTO PROJECT.ITEM_PAPEL (COD_ITEM_PAPEL, COD_BIB, COD_TIPO, COD_CATEGORIA, COD_EDITORA," &
                          "TITULO, EDICAO, IDIOMA, DIMENSOES, PERMISSAO, VOLUME, DATA_PUB, CLASSIFICACAO, DESCRICAO, COTA" &
                          ")VALUES (@CodItemPapel, @CodBiblioteca, @CodTipo, @CodCategoria, @CodEditora, @Titulo, @Edicao" &
                          ", @Idioma, @Dimensoes, @Permissao, @Volume, @DataPub, @Classificacao, @Descricao, @Cota) "
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodItemPapel", ip.CodItemPapel)
        CMD.Parameters.AddWithValue("@CodBiblioteca", ip.CodBib)
        CMD.Parameters.AddWithValue("@CodTipo", ip.CodTipo)
        CMD.Parameters.AddWithValue("@CodCategoria", ip.CodCategoria)
        CMD.Parameters.AddWithValue("@CodEditora", ip.CodEditora)
        CMD.Parameters.AddWithValue("@Titulo", ip.Titulo)
        CMD.Parameters.AddWithValue("@Edicao", ip.Edicao)
        CMD.Parameters.AddWithValue("@Idioma", ip.Idioma)
        CMD.Parameters.AddWithValue("@Dimensoes", ip.Dimensoes)
        CMD.Parameters.AddWithValue("@Permissao", ip.Permissao)
        CMD.Parameters.AddWithValue("@Volume", ip.Volume)
        CMD.Parameters.AddWithValue("@DataPub", Date.Parse(ip.DataPub))
        CMD.Parameters.AddWithValue("@Classificacao", ip.Classificacao)
        CMD.Parameters.AddWithValue("@Descricao", ip.Descricao)
        CMD.Parameters.AddWithValue("@Cota", ip.Cota)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update ItemPapel in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
        CN.Close()
        CN.Open()
        CMD.CommandText = "INSERT INTO PROJECT.ITEM_PAPEL_AUTOR(COD_ITEM_PAPEL, COD_AUTOR) VALUES (@CodItemPapel, @CodAutor)"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodItemPapel", ip.CodItemPapel)
        CMD.Parameters.AddWithValue("@CodAutor", ip.CodAutor)
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update ItemPapel in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
        CN.Close()
    End Sub

    Private Sub UpdateItemPapel(ByVal IP As ItemPapel)
        CN.Open()
        CMD.CommandText = "UPDATE PROJECT.ITEM_PAPEL " &
                            "SET COD_BIB = @CodBiblioteca, " &
                            "    COD_TIPO = @CodTipo, " &
                            "    COD_CATEGORIA = @CodCategoria, " &
                            "    COD_EDITORA = @CodEditora, " &
                            "    TITULO = @Titulo, " &
                            "    EDICAO = @Edicao, " &
                            "    IDIOMA = @Idioma, " &
                            "    DIMENSOES = @Dimensoes, " &
                            "    PERMISSAO = @Permissao, " &
                            "    VOLUME = @Volume, " &
                            "    DATA_PUB = @DataPub, " &
                            "    CLASSIFICACAO = @Classificacao, " &
                            "    DESCRICAO = @Descricao, " &
                            "    COTA = @Cota " &
                            "WHERE COD_ITEM_PAPEL = @CodItemPapel"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodItemPapel", IP.CodItemPapel)
        CMD.Parameters.AddWithValue("@CodBiblioteca", IP.CodBib)
        CMD.Parameters.AddWithValue("@CodTipo", IP.CodTipo)
        CMD.Parameters.AddWithValue("@CodCategoria", IP.CodCategoria)
        CMD.Parameters.AddWithValue("@CodEditora", IP.CodEditora)
        CMD.Parameters.AddWithValue("@Titulo", IP.Titulo)
        CMD.Parameters.AddWithValue("@Edicao", IP.Edicao)
        CMD.Parameters.AddWithValue("@Idioma", IP.Edicao)
        CMD.Parameters.AddWithValue("@Dimensoes", IP.Dimensoes)
        CMD.Parameters.AddWithValue("@Permissao", IP.Permissao)
        CMD.Parameters.AddWithValue("@Volume", IP.Volume)
        CMD.Parameters.AddWithValue("@DataPub", Date.Parse(IP.DataPub)) 'Hello'
        CMD.Parameters.AddWithValue("@Classificacao", IP.Classificacao)
        CMD.Parameters.AddWithValue("@Descricao", IP.Descricao)
        CMD.Parameters.AddWithValue("@Cota", IP.Cota)
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update contact in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try

        CN.Close()
    End Sub

    Private Sub Edit_IP_Click(sender As Object, e As EventArgs) Handles Edit_IP.Click
        currentSelectedItemPapel = LB_ITEMS_PAPEL.SelectedIndex
        If currentSelectedItemPapel < 0 Then
            MsgBox("Por favor selecione um Item Papel para editar!")
            Exit Sub
        End If
        adding = False
        add = False
        HideShowButtons(5, False)
        LockUnlockControls(5, True)
        LB_ITEMS_PAPEL.Enabled = False
        TB_CODITEMPAPEL_IP.ReadOnly = True
        TB_CODBIB_IP.ReadOnly = True
        TB_CODCATEGORIA_IP.ReadOnly = True
        TB_CODAUTOR_IP.ReadOnly = True
        TB_CODTIPO_IP.ReadOnly = True
        TB_CODEDITORA_IP.ReadOnly = True

    End Sub


    Private Sub Edit_Pessoa_Click(sender As Object, e As EventArgs) Handles Edit_Pessoa.Click
        currentSelectedPessoa = LB_Pessoa.SelectedIndex
        If currentSelectedPessoa < 0 Then
            MsgBox("Por favor selecione uma Pessoa para editar!")
            Exit Sub
        End If
        adding = False
        HideShowButtons(1, False)
        LockUnlockControls(1, True)
        LB_Pessoa.Enabled = False
        TB_IDPESSOA_P.ReadOnly = True
    End Sub

    Private Sub Edit_Fab_Click(sender As Object, e As EventArgs) Handles Edit_Fab.Click
        currentSelectedFabricante = LB_FABRICANTES.SelectedIndex
        If currentSelectedFabricante < 0 Then
            MsgBox("Por favor selecione um Fabricante para editar!")
            Exit Sub
        End If
        adding = False
        HideShowButtons(13, False)
        LockUnlockControls(13, True)
        TB_CODFABRICANTE.ReadOnly = True
        LB_FABRICANTES.Enabled = False
    End Sub

    Private Sub Del_Fab_Click(sender As Object, e As EventArgs) Handles Del_Fab.Click

        If LB_FABRICANTES.SelectedIndex > -1 Then
            Try
                RemoveFabricante(CType(LB_FABRICANTES.SelectedItem, Fabricante).CodFabricante)
            Catch ex As Exception
                MsgBox(ex.Message)
                Exit Sub
            End Try
            LB_FABRICANTES.Items.RemoveAt(LB_FABRICANTES.SelectedIndex)
            If currentSelectedFabricante = LB_FABRICANTES.Items.Count Then currentSelectedFabricante = LB_FABRICANTES.Items.Count - 1
            If currentSelectedFabricante = -1 Then
                LB_FABRICANTES.Items.Clear()
                MsgBox("There are no more contacts")
            Else
                ShowFabricantes()
            End If
        End If
    End Sub

    Private Sub Edit_Categoria_Click(sender As Object, e As EventArgs) Handles Edit_Categoria.Click
        currentSelectedCategoria = LB_CATEGORIA.SelectedIndex
        If currentSelectedCategoria < 0 Then
            MsgBox("Por favor selecione uma Categoria para editar!")
            Exit Sub
        End If
        adding = False
        HideShowButtons(11, False)
        LockUnlockControls(11, True)
        TB_CODCATEGORIA.ReadOnly = True
        LB_CATEGORIA.Enabled = False
    End Sub

    Private Sub Edit_Tipo_Click(sender As Object, e As EventArgs) Handles Edit_Tipo.Click
        currentSelectedTipo = LB_TIPOS.SelectedIndex
        If currentSelectedTipo < 0 Then
            MsgBox("Por favor selecione um Tipo para editar!")
            Exit Sub
        End If
        adding = False
        HideShowButtons(12, False)
        LockUnlockControls(12, True)
        TB_CODTIPO.ReadOnly = True
        LB_TIPOS.Enabled = False
    End Sub

    Private Sub Edit_Editora_Click(sender As Object, e As EventArgs) Handles Edit_Editora.Click
        currentSelectedEditora = LB_Editora.SelectedIndex
        If currentSelectedEditora < 0 Then
            MsgBox("Por favor selecione uma Editora para editar!")
            Exit Sub
        End If
        adding = False
        HideShowButtons(10, False)
        LockUnlockControls(10, True)
        TB_CODEDITORA.ReadOnly = True
        LB_Editora.Enabled = False
    End Sub

    Private Sub Edit_Biblioteca_Click(sender As Object, e As EventArgs) Handles Edit_Biblioteca.Click
        currentSelectedBiblioteca = LB_Biblioteca.SelectedIndex
        If currentSelectedBiblioteca < 0 Then
            MsgBox("Por favor selecione uma Biblioteca para editar!")
            Exit Sub
        End If
        adding = False
        HideShowButtons(9, False)
        LockUnlockControls(9, True)
        TB_CODIGO_BIB.ReadOnly = True
        LB_Biblioteca.Enabled = False
    End Sub

    Private Sub Add_RIE_Click(sender As Object, e As EventArgs) Handles Add_RIE.Click
        adding = True
        add = True
        fromBack = False
        ClearFields(8)
        HideShowButtons(8, False)
        LB_ReqItemElect.Enabled = False
        GetCI(CB_CodItem_RIE, TB_CODITEMELECT_RIE)
        GETCL(CB_CodLeitor_RIE, TB_CODLEITOR_RIE)
        Dim todaysdate As String = String.Format("{0:dd/MM/yyyy}", DateTime.Now) 'Hello'
        TB_MARCACAO_RIE.Text = todaysdate
        CB_CodLeitor_RIE.SelectedIndex = 0
        CB_CodItem_RIE.SelectedIndex = 0
        Dim bool As Boolean = True
        Dim pk As String = ""
        Dim random As New Random
        While bool
            pk = ""
            For i As Integer = 0 To 8
                If i = 0 Then
                    Dim value As String = Convert.ToString(random.Next(1, 9))
                    pk = pk + value
                Else
                    Dim value As String = Convert.ToString(random.Next(0, 9))
                    pk = pk + value
                End If
            Next
            For Each obj In auxListRIE
                If Equals(obj.CodReq, pk) Then
                    Continue For
                Else
                    bool = False
                End If
            Next
        End While
        TB_CODREQ_RIE.Text = pk
        TB_CODREQ_RIE.ReadOnly = True
    End Sub
    Private Sub GetCI(cb1 As ComboBox, tb1 As TextBox)
        cb1.Show()
        tb1.Hide()
        CMD.CommandText = "SELECT * FROM PROJECT.ITEM_ELECTRONICA"
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        cb1.Items.Clear()
        auxListIE = New List(Of ItemEletronica)
        While RDR.Read
            Dim IE As New ItemEletronica
            IE.CodItemEletronica = RDR.Item("COD_ITEM_ELECT")
            IE.CodBib = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("COD_BIB")), "", RDR.Item("COD_BIB")))
            IE.CodTipo = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("COD_TIPO")), "", RDR.Item("COD_TIPO")))
            IE.CodFabrincante = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("COD_FABRICANTE")), "", RDR.Item("COD_FABRICANTE")))
            cb1.Items.Add(IE.CodItemEletronica)
            auxListIE.Add(IE)
        End While
        CN.Close()
    End Sub
    Private Sub GETCL(cb1 As ComboBox, tb1 As TextBox)
        cb1.Show()
        tb1.Hide()
        CMD.CommandText = "SELECT * FROM PROJECT.LEITOR JOIN PROJECT.PESSOA ON PROJECT.LEITOR.ID_PESSOA = PROJECT.PESSOA.ID_PESSOA"
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        cb1.Items.Clear()
        auxListLeitor = New List(Of Leitor)
        While RDR.Read
            Dim L As New Leitor
            L.CodLeitor = RDR.Item("COD_LEITOR")
            L.PrimeiroNome = RDR.Item("PRIMEIRO_NOME")
            L.UltimoNome = RDR.Item("ULTIMO_NOME")
            L.IDPessoa = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("ID_PESSOA")), "", RDR.Item("ID_PESSOA")))
            L.Morada = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("MORADA")), "", RDR.Item("MORADA")))
            L.Tlm = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("TLM")), "", RDR.Item("TLM")))
            L.Cc = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("CC")), "", RDR.Item("CC")))
            L.Nif = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("NIF")), "", RDR.Item("NIF")))
            L.DataNasc = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DATA_NASC")), "", RDR.Item("DATA_NASC")))
            L.Genero = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("GENERO")), "", RDR.Item("GENERO")))
            L.Username = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("USERNAME")), "", RDR.Item("USERNAME")))
            L.Password = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("PASS")), "", RDR.Item("PASS")))
            L.DataExpiro = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DATA_EXPIRO")), "", RDR.Item("DATA_EXPIRO")))
            L.DataRegisto = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DATA_REGISTO")), "", RDR.Item("DATA_REGISTO")))
            L.CodBiblioteca = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("COD_BIB")), "", RDR.Item("COD_BIB")))
            cb1.Items.Add(L.teste1())
            auxListLeitor.Add(L)
        End While
        CN.Close()
    End Sub
    Private Sub Cancel_RIE_Click(sender As Object, e As EventArgs) Handles Cancel_RIE.Click
        LB_ReqItemElect.Enabled = True
        If LB_ReqItemElect.Items.Count > 0 Then
            currentSelectedReqItemElect = LB_ReqItemElect.SelectedIndex
            If currentSelectedReqItemElect < 0 Then currentSelectedReqItemElect = 0
            ShowReqItemElect()
        Else
            ClearFields(8)
            LockUnlockControls(8, False)
        End If
        HideShowButtons(8, True)
        LockUnlockControls(8, False)
        CB_CodItem_RIE.Hide()
        CB_CodLeitor_RIE.Hide()
        TB_CODITEMELECT_RIE.Show()
        TB_CODLEITOR_RIE.Show()
    End Sub

    Private Sub Confirm_RIE_Click(sender As Object, e As EventArgs) Handles Confirm_RIE.Click
        Try
            SaveReqItemElect()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        LB_ReqItemElect.Enabled = True
        Dim idx As Integer = LB_ReqItemElect.FindString(TB_CODREQ_RIE.Text)
        LB_ReqItemElect.SelectedIndex = idx
        HideShowButtons(8, True)
        LockUnlockControls(8, False)
        CB_CodItem_RIE.Hide()
        CB_CodLeitor_RIE.Hide()
        TB_CODITEMELECT_RIE.Show()
        TB_CODLEITOR_RIE.show()
    End Sub
    Private Function SaveReqItemElect() As Boolean
        Dim RIE As New ReqEletronica
        Try
            If add Then
                If fromBack Then
                    RIE.CodItemEletronica = TB_CODITEMELECT_RIE.Text
                    Dim help As String = CB_CodLeitor_RIE.SelectedItem.ToString()
                    Dim help1 As String() = help.Split(New Char() {" "c})
                    RIE.CodLeitor = help1(0)
                Else
                    RIE.CodItemEletronica = CB_CodItem_RIE.SelectedItem.ToString()
                    Dim help As String = CB_CodLeitor_RIE.SelectedItem.ToString()
                    Dim help1 As String() = help.Split(New Char() {" "c})
                    RIE.CodLeitor = help1(0)
                End If
            Else
                RIE.CodItemEletronica = TB_CODITEMELECT_RIE.Text
                RIE.CodLeitor = TB_CODLEITOR_RIE.Text
            End If
            RIE.CodReq = TB_CODREQ_RIE.Text
            RIE.Marcacao = TB_MARCACAO_RIE.Value.ToString()
            RIE.Duracao = TB_DURACAO_RIE.Text
            RIE.HoraInicio = TB_HORAINICIO_RIE.Value.ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        If adding Then
            submitReqItemElect(RIE)
            LB_ReqItemElect.Items.Add(RIE)
        Else
            UpdateReqItemEletronica(RIE)
            LB_ReqItemElect.Items(currentSelectedItemElect) = RIE
        End If
        Return True
    End Function
    Private Sub submitReqItemElect(ByVal RIE As ReqEletronica)
        CMD.CommandText = "SELECT * FROM PROJECT.ITEM_ELECTRONICA WHERE PROJECT.ITEM_ELECTRONICA.COD_ITEM_ELECT=@CodItemElect"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodItemElect", RIE.CodItemEletronica)
        CN.Open()
        Dim RDR As Integer
        RDR = CMD.ExecuteScalar
        If RDR > 1 Then
            CMD.CommandText = "SELECT * FROM PROJECT.LEITOR WHERE PROJECT.LEITOR.COD_LEITOR=@CodLeitor"
            CMD.Parameters.Clear()
            CMD.Parameters.AddWithValue("@CodLeitor", RIE.CodLeitor)
            Dim RDR1 As Integer
            RDR1 = CMD.ExecuteScalar
            If RDR1 > 1 Then
                CMD.CommandText = "INSERT INTO PROJECT.REQUISICAO_ITEM_ELECT (COD_REQUISICAO, COD_ITEM_ELECT, COD_LEITOR, MARCACAO, DURACAO, INICIO) " &
                          "VALUES (@CodRequisicao, @CodItemElect, @CodLeitor, @Marcacao, @Duracao, @Inicio)"
                CMD.Parameters.Clear()
                CMD.Parameters.AddWithValue("@CodItemElect", RIE.CodItemEletronica)
                CMD.Parameters.AddWithValue("@CodLeitor", RIE.CodLeitor)
                CMD.Parameters.AddWithValue("@CodRequisicao", RIE.CodReq)
                CMD.Parameters.AddWithValue("@Marcacao", RIE.Marcacao)
                CMD.Parameters.AddWithValue("@Duracao", RIE.Duracao)
                CMD.Parameters.AddWithValue("@Inicio", RIE.HoraInicio)

                Try
                    CMD.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Failed to update ReqItemElectronica in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
                Finally
                    CN.Close()
                End Try
            End If
        End If
        CN.Close()
    End Sub

    Private Sub UpdateReqItemEletronica(ByVal Req_elect As ReqEletronica)
        CN.Open()
        CMD.CommandText = "UPDATE PROJECT.REQUISICAO_ITEM_ELECT " &
                                    "SET COD_ITEM_ELECT = @CodItemElect, " &
                                    "    COD_LEITOR = @CodLeitor, " &
                                    "    MARCACAO = @Marcacao, " &
                                    "    DURACAO = @Duracao, " &
                                    "    INICIO = @Inicio " &
                                    "WHERE COD_REQUISICAO = @CodReq"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodReq", Req_elect.CodReq)
        CMD.Parameters.AddWithValue("@CodLeitor", Req_elect.CodLeitor)
        CMD.Parameters.AddWithValue("@Marcacao", Req_elect.Marcacao)
        CMD.Parameters.AddWithValue("@Duracao", Req_elect.Duracao)
        CMD.Parameters.AddWithValue("@Inicio", Req_elect.HoraInicio)
        CMD.Parameters.AddWithValue("@CodItemElect", Req_elect.CodItemEletronica)
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update ReqItemElectronica in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
        CN.Close()
    End Sub



    Private Sub Add_RIP_Click(sender As Object, e As EventArgs) Handles Add_RIP.Click
        adding = True
        add = True
        ClearFields(7)
        HideShowButtons(7, False)
        Dim todaysdate As String = String.Format("{0:dd/MM/yyyy}", DateTime.Now) 'Hello'
        TB_DATAREQ_RIP.Text = todaysdate
        LB_ReqItemP.Enabled = False
        getItems(CB_CODITEM_RIP, TB_CODIP_RIP)
        GETCL(CB_CODLEITOR_RIP, TB_CODLEITOR_RIP)
        CB_CODLEITOR_RIP.SelectedIndex = 0
        CB_CODITEM_RIP.SelectedIndex = 0
        Dim bool As Boolean = True
        Dim pk As String = ""
        Dim random As New Random
        While bool
            pk = ""
            For i As Integer = 0 To 8
                If i = 0 Then
                    Dim value As String = Convert.ToString(random.Next(1, 9))
                    pk = pk + value
                Else
                    Dim value As String = Convert.ToString(random.Next(0, 9))
                    pk = pk + value
                End If
            Next
            For Each obj In auxListRIP
                If Equals(obj.CodReq, pk) Then
                    Continue For
                Else
                    bool = False
                End If
            Next
        End While

        fromBack = False
        TB_CODREQ_RIP.Text = pk
        TB_CODREQ_RIP.ReadOnly = True
        TB_MULTA_RIP.ReadOnly = True
        TB_MULTA_RIP.Text = "Política interna"
    End Sub
    Private Sub getItems(cb1 As ComboBox, tb1 As TextBox)
        cb1.Show()
        tb1.Hide()
        CMD.CommandText = "SELECT * FROM PROJECT.ITEM_PAPEL JOIN PROJECT.ITEM_PAPEL_AUTOR ON PROJECT.ITEM_PAPEL.COD_ITEM_PAPEL = PROJECT.ITEM_PAPEL_AUTOR.COD_ITEM_PAPEL"
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        cb1.Items.Clear()
        auxListIP = New List(Of ItemPapel)
        While RDR.Read
            Dim IP As New ItemPapel
            IP.CodItemPapel = RDR.Item("COD_ITEM_PAPEL")
            IP.CodBib = RDR.Item("COD_BIB")
            IP.CodTipo = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("COD_TIPO")), "", RDR.Item("COD_TIPO")))
            IP.CodCategoria = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("COD_CATEGORIA")), "", RDR.Item("COD_CATEGORIA")))
            IP.CodEditora = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("COD_EDITORA")), "", RDR.Item("COD_EDITORA")))
            IP.CodAutor = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("COD_AUTOR")), "", RDR.Item("COD_AUTOR")))
            IP.Titulo = RDR.Item("TITULO")
            IP.Edicao = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("EDICAO")), "", RDR.Item("EDICAO")))
            IP.Idioma = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("IDIOMA")), "", RDR.Item("IDIOMA")))
            IP.Dimensoes = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DIMENSOES")), "", RDR.Item("DIMENSOES"))) ''corrigir no ddl
            IP.Permissao = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("PERMISSAO")), "", RDR.Item("PERMISSAO")))
            IP.Volume = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("VOLUME")), "", RDR.Item("VOLUME")))
            IP.DataPub = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DATA_PUB")), "", RDR.Item("DATA_PUB")))
            IP.Classificacao = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("CLASSIFICACAO")), "", RDR.Item("CLASSIFICACAO")))
            IP.Descricao = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DESCRICAO")), "", RDR.Item("DESCRICAO")))
            IP.Cota = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("COTA")), "", RDR.Item("COTA")))
            cb1.Items.Add(IP.teste1())
            auxListIP.Add(IP)
        End While
        CN.Close()
    End Sub
    Private Sub Cancel_RIP_Click(sender As Object, e As EventArgs) Handles Cancel_RIP.Click
        LB_ReqItemP.Enabled = True
        If LB_ReqItemP.Items.Count > 0 Then
            currentSelectedReqItemPapel = LB_ReqItemP.SelectedIndex
            If currentSelectedReqItemPapel < 0 Then currentSelectedReqItemPapel = 0
            ShowReqItemPapel()
        Else
            ClearFields(7)
            LockUnlockControls(7, False)
        End If
        HideShowButtons(7, True)
        LockUnlockControls(7, False)
        TB_DATAREQ_RIP.Text = ""
        CB_CODITEM_RIP.Hide()
        CB_CODLEITOR_RIP.Hide()
        TB_CODIP_RIP.Show()
        TB_CODLEITOR_RIP.Show()
    End Sub

    Private Sub Confirm_RIP_Click(sender As Object, e As EventArgs) Handles Confirm_RIP.Click
        Try
            SaveReqItemPapel()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        LB_ReqItemP.Enabled = True
        Dim idx As Integer = LB_ReqItemP.FindString(TB_CODITEMPAPEL_IP.Text)
        LB_ReqItemP.SelectedIndex = idx
        HideShowButtons(7, True)
        LockUnlockControls(7, False)
        CB_CODITEM_RIP.Hide()
        CB_CODLEITOR_RIP.Hide()
        TB_CODIP_RIP.Show()
        TB_CODLEITOR_RIP.Show()
    End Sub
    Private Function SaveReqItemPapel() As Boolean
        Dim RIP As New ReqPapel
        Try
            If add Then
                If (fromBack) Then
                    RIP.CodItemPapel = TB_CODIP_RIP.Text
                    Dim help As String = CB_CODLEITOR_RIP.SelectedItem.ToString()
                    Dim help1 As String() = help.Split(New Char() {" "c})
                    RIP.CodLeitor = help1(0)
                Else
                    Dim help As String = CB_CODITEM_RIP.SelectedItem.ToString()
                    Dim help1 As String() = help.Split(New Char() {" "c})
                    RIP.CodItemPapel = help1(0)
                    help = CB_CODLEITOR_RIP.SelectedItem.ToString()
                    help1 = help.Split(New Char() {" "c})
                    RIP.CodLeitor = help1(0)
                End If
            Else
                RIP.CodItemPapel = TB_CODIP_RIP.Text
                RIP.CodLeitor = TB_CODLEITOR_RIP.Text
            End If
            RIP.CodReq = TB_CODREQ_RIP.Text
            RIP.DataReq = TB_DATAREQ_RIP.Text
            RIP.Multa = TB_MULTA_RIP.Text
            RIP.DataReal = TB_DATAER_RIP.Value.ToString()
            RIP.DataPrev = TB_DATAEP_RIP.Value.ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try

        If adding Then
            submitReqItemPapel(RIP)
            CMD.CommandText = "SELECT * FROM PROJECT.REQUISICAO_ITEM_PAPEL WHERE COD_REQUISICAO=@CodReq"
            CN.Open()
            CMD.Parameters.Clear()
            CMD.Parameters.AddWithValue("@CodReq", RIP.CodReq)
            Dim RDR As SqlDataReader
            RDR = CMD.ExecuteReader
            While RDR.Read
                RIP.CodReq = RDR.Item("COD_REQUISICAO")
                RIP.CodItemPapel = RDR.Item("COD_ITEM_PAPEL")
                RIP.CodLeitor = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("COD_LEITOR")), "", RDR.Item("COD_LEITOR")))
                RIP.DataReq = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DATA_REQUISICAO")), "", RDR.Item("DATA_REQUISICAO")))
                RIP.DataReal = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DT_EN_REAL")), "", RDR.Item("DT_EN_REAL")))
                RIP.DataPrev = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DT_EN_PREVISTA")), "", RDR.Item("DT_EN_PREVISTA")))
                RIP.Multa = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("MULTA")), "", RDR.Item("MULTA")))
                LB_ReqItemP.Items.Add(RIP)
            End While
            CN.Close()
            LB_ReqItemP.SelectedIndex = LB_ReqItemP.Items.Count - 1
        Else
            UpdateReqItemPapel(RIP)
            LB_ReqItemP.Items(currentSelectedItemPapel) = RIP
        End If
        Return True
    End Function
    Private Sub submitReqItemPapel(ByVal RIP As ReqPapel)
        CN.Open()
        CMD.CommandText = "INSERT INTO PROJECT.REQUISICAO_ITEM_PAPEL (COD_REQUISICAO, COD_ITEM_PAPEL, COD_LEITOR, DATA_REQUISICAO, DT_EN_REAL, DT_EN_PREVISTA, MULTA) " &
                          "VALUES (@CodRequisicao, @CodItemPapel, @CodLeitor, @Data, @DataReal, @DataPrev, @Multa)"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodRequisicao", RIP.CodReq)
        CMD.Parameters.AddWithValue("@CodItemPapel", RIP.CodItemPapel)
        CMD.Parameters.AddWithValue("@CodLeitor", RIP.CodLeitor)
        CMD.Parameters.AddWithValue("@Data", Date.Parse(RIP.DataReq))
        CMD.Parameters.AddWithValue("@DataReal", Date.Parse(RIP.DataReal))
        CMD.Parameters.AddWithValue("@DataPrev", Date.Parse(RIP.DataPrev))
        CMD.Parameters.AddWithValue("@Multa", 0)
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update ReqItemPapel in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try


        CN.Close()
    End Sub

    Private Sub UpdateReqItemPapel(ByVal Req_papel As ReqPapel)
        CN.Open()
        CMD.CommandText = "UPDATE PROJECT.REQUISICAO_ITEM_PAPEL " &
                    "SET COD_ITEM_PAPEL = @CodItemPapel, " &
                    "    COD_LEITOR = @CodLeitor, " &
                    "    DATA_REQUISICAO = @DataReq, " &
                    "    DT_EN_REAL = @DataReal, " &
                    "    DT_EN_PREVISTA = @DataPrev, " &
                    "    MULTA = @Multa " &
                    "WHERE COD_REQUISICAO = @CodReq"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodItemPapel", Req_papel.CodItemPapel)
        CMD.Parameters.AddWithValue("@CodLeitor", Req_papel.CodLeitor)
        CMD.Parameters.AddWithValue("@DataReq", Date.Parse(Req_papel.DataReq)) 'Hello'
        CMD.Parameters.AddWithValue("@DataReal", Date.Parse(Req_papel.DataReal)) 'Hello'
        CMD.Parameters.AddWithValue("@DataPrev", Date.Parse(Req_papel.DataPrev)) 'Hello'
        CMD.Parameters.AddWithValue("@Multa", 0)
        CMD.Parameters.AddWithValue("@CodReq", Req_papel.CodReq)
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update ReqItemPapel in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try

        CN.Close()
    End Sub


    Private Sub Del_RIE_Click(sender As Object, e As EventArgs) Handles Del_RIE.Click
        If LB_ReqItemElect.SelectedIndex > -1 Then
            Try
                RemoveReqItemElect(CType(LB_ReqItemElect.SelectedItem, ReqEletronica).CodReq)
            Catch ex As Exception
                MsgBox(ex.Message)
                Exit Sub
            End Try
            LB_ReqItemElect.Items.RemoveAt(LB_ReqItemElect.SelectedIndex)
            If currentSelectedReqItemElect = LB_ReqItemElect.Items.Count Then currentSelectedReqItemElect = LB_ReqItemElect.Items.Count - 1
            If currentSelectedReqItemElect = -1 Then
                LB_ReqItemElect.Items.Clear()
                MsgBox("There are no more req of electronical items")
            Else
                ShowReqItemElect()
            End If
        End If
    End Sub
    Private Sub RemoveReqItemElect(ByVal RIE_CodReq As String)
        CMD.CommandText = "DELETE FROM PROJECT.REQUISICAO_ITEM_ELECT WHERE COD_REQUISICAO=@CodReqItemElect "
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodReqItemElect", RIE_CodReq)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to delete ReqItemElect in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
    End Sub

    Private Sub Del_RIP_Click(sender As Object, e As EventArgs) Handles Del_RIP.Click
        If LB_ReqItemP.SelectedIndex > -1 Then
            Try
                RemoveReqItemPapel(CType(LB_ReqItemP.SelectedItem, ReqPapel).CodReq)
            Catch ex As Exception
                MsgBox(ex.Message)
                Exit Sub
            End Try
            LB_ReqItemP.Items.RemoveAt(LB_ReqItemP.SelectedIndex)
            If currentSelectedReqItemPapel = LB_ReqItemP.Items.Count Then currentSelectedReqItemPapel = LB_ReqItemP.Items.Count - 1
            If currentSelectedReqItemPapel = -1 Then
                LB_ReqItemP.Items.Clear()
                MsgBox("There are no more req of paper items")
            Else
                ShowReqItemPapel()
            End If
        End If
    End Sub
    Private Sub RemoveReqItemPapel(ByVal RIP_CodReq As String)
        CMD.CommandText = "DELETE FROM PROJECT.REQUISICAO_ITEM_PAPEL WHERE COD_REQUISICAO=@CodReqItemPapel "
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodReqItemPapel", RIP_CodReq)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to delete ReqItemPapel in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
    End Sub

    Private Sub Del_IE_Click(sender As Object, e As EventArgs) Handles Del_IE.Click
        If LB_ITEM_ELECT.SelectedIndex > -1 Then
            Try
                RemoveItemElectonica(CType(LB_ITEM_ELECT.SelectedItem, ItemEletronica).CodItemEletronica)
            Catch ex As Exception
                MsgBox(ex.Message)
                Exit Sub
            End Try
            LB_ITEM_ELECT.Items.RemoveAt(LB_ITEM_ELECT.SelectedIndex)
            If currentSelectedItemElect = LB_ITEM_ELECT.Items.Count Then currentSelectedItemElect = LB_ITEM_ELECT.Items.Count - 1
            If currentSelectedItemElect = -1 Then
                LB_ITEM_ELECT.Items.Clear()
                MsgBox("There are no more electronical items")
            Else
                ShowItemElect()
            End If
        End If
    End Sub
    Private Sub RemoveItemElectonica(ByVal IE_CodItemElect As String)
        CMD.CommandText = "DELETE FROM PROJECT.ITEM_ELECTRONICA WHERE COD_ITEM_ELECT=@CodItemElect "
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodItemElect", IE_CodItemElect)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to delete Electronical Item in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
    End Sub

    Private Sub Del_IP_Click(sender As Object, e As EventArgs) Handles Del_IP.Click
        If LB_ITEMS_PAPEL.SelectedIndex > -1 Then
            Try
                RemoveItemPapel(CType(LB_ITEMS_PAPEL.SelectedItem, ItemPapel).CodItemPapel)
            Catch ex As Exception
                MsgBox(ex.Message)
                Exit Sub
            End Try
            LB_ITEMS_PAPEL.Items.RemoveAt(LB_ITEMS_PAPEL.SelectedIndex)
            If currentSelectedItemPapel = LB_ITEMS_PAPEL.Items.Count Then currentSelectedItemPapel = LB_ITEMS_PAPEL.Items.Count - 1
            If currentSelectedItemPapel = -1 Then
                LB_ITEMS_PAPEL.Items.Clear()
                MsgBox("There are no more paper items items")
            Else
                ShowItemPapel()
            End If
        End If
    End Sub
    Private Sub RemoveItemPapel(ByVal IP_CodItemPapel As String)
        CMD.CommandText = "DELETE FROM PROJECT.ITEM_PAPEL WHERE COD_ITEM_PAPEL=@CodItemPapel "
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodItemPapel", IP_CodItemPapel)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to delete Paper Item in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
    End Sub

    Private Sub Del_Autor_Click(sender As Object, e As EventArgs) Handles Del_Autor.Click
        If LB_AUTOR.SelectedIndex > -1 Then
            Try
                RemoveAutor(CType(LB_AUTOR.SelectedItem, Autor).CodAutor)
            Catch ex As Exception
                MsgBox(ex.Message)
                Exit Sub
            End Try
            LB_AUTOR.Items.RemoveAt(LB_AUTOR.SelectedIndex)
            If currentSelectedAutor = LB_AUTOR.Items.Count Then currentSelectedAutor = LB_AUTOR.Items.Count - 1
            If currentSelectedAutor = -1 Then
                LB_AUTOR.Items.Clear()
                MsgBox("There are no more autors")
            Else
                ShowAutor()
            End If
        End If
    End Sub
    Private Sub RemoveAutor(ByVal Autor_CodAutor As String)
        CMD.CommandText = "DELETE FROM PROJECT.AUTOR WHERE COD_AUTOR=@CodAutor "
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodAutor", Autor_CodAutor)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to delete Autor in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
    End Sub

    Private Sub Del_Bib_Click(sender As Object, e As EventArgs) Handles Del_Bib.Click
        If LB_BIB.SelectedIndex > -1 Then
            Try
                RemoveBibliotecario(CType(LB_BIB.SelectedItem, Bibliotecario).CodBibliotecario)
            Catch ex As Exception
                MsgBox(ex.Message)
                Exit Sub
            End Try
            LB_BIB.Items.RemoveAt(LB_BIB.SelectedIndex)
            If currentSelectedBibliotecario = LB_AUTOR.Items.Count Then currentSelectedBibliotecario = LB_BIB.Items.Count - 1
            If currentSelectedBibliotecario = -1 Then
                LB_BIB.Items.Clear()
                MsgBox("There are no more bibliotecarios")
            Else
                ShowBibliotecario()
            End If
        End If
    End Sub
    Private Sub RemoveBibliotecario(ByVal Bib_CodBib As String)
        CMD.CommandText = "DELETE FROM PROJECT.BIBLIOTECARIO WHERE COD_BIBLIOTECARIO=@CodBib "
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodBib", Bib_CodBib)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to delete bibliotecario in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
    End Sub

    Private Sub Del_Leitor_Click(sender As Object, e As EventArgs) Handles Del_Leitor.Click
        If LB_Leitor.SelectedIndex > -1 Then
            Try
                RemoveLeitor(CType(LB_Leitor.SelectedItem, Leitor).CodLeitor)
            Catch ex As Exception
                MsgBox(ex.Message)
                Exit Sub
            End Try
            LB_Leitor.Items.RemoveAt(LB_Leitor.SelectedIndex)
            If currentSelectedLeitor = LB_Leitor.Items.Count Then currentSelectedLeitor = LB_Leitor.Items.Count - 1
            If currentSelectedLeitor = -1 Then
                LB_Leitor.Items.Clear()
                MsgBox("Não existem mais leitores para remover!")
            Else
                ShowLeitor()
            End If
        End If
    End Sub
    Private Sub RemoveLeitor(ByVal Leitor_CodLeitor As String)
        CMD.CommandText = "DELETE FROM PROJECT.LEITOR WHERE COD_LEITOR=@CodLeitor "
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodLeitor", Leitor_CodLeitor)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Falha ao remover leitor! " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
    End Sub

    Private Sub Del_Pessoa_Click(sender As Object, e As EventArgs) Handles Del_Pessoa.Click
        If LB_Pessoa.SelectedIndex > -1 Then
            Try
                RemovePessoa(CType(LB_Pessoa.SelectedItem, Pessoa).IDPessoa)
            Catch ex As Exception
                MsgBox(ex.Message)
                Exit Sub
            End Try
            LB_Pessoa.Items.RemoveAt(LB_Pessoa.SelectedIndex)
            If currentSelectedPessoa = LB_Pessoa.Items.Count Then currentSelectedPessoa = LB_Pessoa.Items.Count - 1
            If currentSelectedPessoa = -1 Then
                LB_Pessoa.Items.Clear()
                MsgBox("Não existem mais Pessoas para remover!")
            Else
                ShowPessoa()
            End If
        End If
    End Sub
    Private Sub RemovePessoa(ByVal Pessoa_ID As String)
        CMD.CommandText = "DELETE FROM PROJECT.PESSOA WHERE ID_PESSOA=@IDPessoa "
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@IDPessoa", Pessoa_ID)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Falha ao remover Pessoa! " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
    End Sub

    Private Sub Del_Tipo_Click(sender As Object, e As EventArgs) Handles Del_Tipo.Click
        If LB_TIPOS.SelectedIndex > -1 Then
            Try
                RemoveTipos(CType(LB_TIPOS.SelectedItem, Tipo).CodTipo)
            Catch ex As Exception
                MsgBox(ex.Message)
                Exit Sub
            End Try
            LB_TIPOS.Items.RemoveAt(LB_TIPOS.SelectedIndex)
            If currentSelectedTipo = LB_TIPOS.Items.Count Then currentSelectedTipo = LB_TIPOS.Items.Count - 1
            If currentSelectedTipo = -1 Then
                LB_TIPOS.Items.Clear()
                MsgBox("Não existem mais Tipos para remover!")
            Else
                ShowTipos()
            End If
        End If
    End Sub
    Private Sub RemoveTipos(ByVal Tipo_CodTipo As String)
        CMD.CommandText = "DELETE FROM PROJECT.TIPO WHERE COD_TIPO=@CodTipo "
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodTipo", Tipo_CodTipo)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Falha ao remover Tipos! " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
    End Sub

    Private Sub Del_Categoria_Click(sender As Object, e As EventArgs) Handles Del_Categoria.Click
        If LB_CATEGORIA.SelectedIndex > -1 Then
            Try
                RemoveCategorias(CType(LB_CATEGORIA.SelectedItem, Categoria).CodCategoria)
            Catch ex As Exception
                MsgBox(ex.Message)
                Exit Sub
            End Try
            LB_CATEGORIA.Items.RemoveAt(LB_CATEGORIA.SelectedIndex)
            If currentSelectedCategoria = LB_CATEGORIA.Items.Count Then currentSelectedCategoria = LB_CATEGORIA.Items.Count - 1
            If currentSelectedCategoria = -1 Then
                LB_CATEGORIA.Items.Clear()
                MsgBox("Não existem mais Categorias para remover!")
            Else
                ShowCategorias()
            End If
        End If
    End Sub
    Private Sub RemoveCategorias(ByVal Categoria_CodCategoria As String)
        CMD.CommandText = "DELETE FROM PROJECT.CATEGORIA WHERE COD_CATEGORIA=@CodCategoria "
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodCategoria", Categoria_CodCategoria)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Falha ao remover Categoria! " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
    End Sub

    Private Sub Del_Editora_Click(sender As Object, e As EventArgs) Handles Del_Editora.Click
        If LB_Editora.SelectedIndex > -1 Then
            Try
                RemoveEditoras(CType(LB_Editora.SelectedItem, Editora).CodEditora)
            Catch ex As Exception
                MsgBox(ex.Message)
                Exit Sub
            End Try
            LB_Editora.Items.RemoveAt(LB_Editora.SelectedIndex)
            If currentSelectedEditora = LB_Editora.Items.Count Then currentSelectedEditora = LB_Editora.Items.Count - 1
            If currentSelectedEditora = -1 Then
                LB_Editora.Items.Clear()
                MsgBox("Não existem mais Editoras para remover!")
            Else
                ShowEditoras()
            End If
        End If
    End Sub
    Private Sub RemoveEditoras(ByVal Editora_CodEditora As String)
        CMD.CommandText = "DELETE FROM PROJECT.EDITORA WHERE COD_EDITORA=@CodEditora "
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodEditora", Editora_CodEditora)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Falha ao remover editora! " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
    End Sub

    Private Sub Del_Biblioteca_Click(sender As Object, e As EventArgs) Handles Del_Biblioteca.Click
        If LB_Biblioteca.SelectedIndex > -1 Then
            Try
                RemoveBibliotecas(CType(LB_Biblioteca.SelectedItem, Biblioteca).CodBiblioteca)
            Catch ex As Exception
                MsgBox(ex.Message)
                Exit Sub
            End Try
            LB_Biblioteca.Items.RemoveAt(LB_Biblioteca.SelectedIndex)
            If currentSelectedBiblioteca = LB_Biblioteca.Items.Count Then currentSelectedBiblioteca = LB_Biblioteca.Items.Count - 1
            If currentSelectedBiblioteca = -1 Then
                LB_Biblioteca.Items.Clear()
                MsgBox("Não existem mais Bibliotecas para remover!")
            Else
                ShowBibliotecas()
            End If
        End If
    End Sub
    Private Sub RemoveBibliotecas(ByVal Biblioteca_CodBib As String)
        CMD.CommandText = "DELETE FROM PROJECT.BIBLIOTECA WHERE COD_BIB=@CodBib "
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@CodBib", Biblioteca_CodBib)
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Falha ao remover biblioteca! " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
    End Sub

    Private Sub Edit_RIE_Click(sender As Object, e As EventArgs) Handles Edit_RIE.Click
        currentSelectedReqItemElect = LB_ReqItemElect.SelectedIndex
        If currentSelectedReqItemElect < 0 Then
            MsgBox("Por favor selecione a Requisição de um Item Electronico para editar!")
            Exit Sub
        End If
        adding = False
        add = False
        HideShowButtons(8, False)
        LockUnlockControls(8, True)
        TB_CODREQ_RIE.ReadOnly = True
        TB_MULTA_RIP.ReadOnly = True
        LB_ReqItemElect.Enabled = False
    End Sub

    Private Sub Edit_RIP_Click(sender As Object, e As EventArgs) Handles Edit_RIP.Click
        currentSelectedReqItemPapel = LB_ReqItemP.SelectedIndex
        If currentSelectedReqItemPapel < 0 Then
            MsgBox("Por favor selecione a Requisição de um Item Papel para editar!")
            Exit Sub
        End If
        adding = False
        add = False
        HideShowButtons(7, False)
        LockUnlockControls(7, True)
        TB_CODREQ_RIP.ReadOnly = True
        TB_MULTA_RIP.ReadOnly = True
        TB_CODITEMPAPEL_IP.ReadOnly = True
        LB_ReqItemP.Enabled = False
        TB_MULTA_RIP.Text = "Política interna"
    End Sub

    Private Sub Edit_IE_Click(sender As Object, e As EventArgs) Handles Edit_IE.Click
        currentSelectedItemElect = LB_ITEM_ELECT.SelectedIndex
        If currentSelectedItemElect < 0 Then
            MsgBox("Por favor selecione um Item Electronico para editar!")
            Exit Sub
        End If
        adding = False
        add = False
        HideShowButtons(6, False)
        LockUnlockControls(6, True)
        TB_CODITEMELECT_IE.ReadOnly = True
        LB_ITEMS_PAPEL.Enabled = False
    End Sub

    Private Sub Edit_Autor_Click(sender As Object, e As EventArgs) Handles Edit_Autor.Click
        currentSelectedAutor = LB_AUTOR.SelectedIndex
        If currentSelectedAutor < 0 Then
            MsgBox("Por favor selecione um Autor para editar!")
            Exit Sub
        End If
        adding = False
        add = False
        HideShowButtons(4, False)
        LockUnlockControls(4, True)
        TB_COD_AUTOR.ReadOnly = True
        TB_IDPESSOA_A.ReadOnly = True
        LB_AUTOR.Enabled = False
    End Sub

    Private Sub Edit_Bib_Click(sender As Object, e As EventArgs) Handles Edit_Bib.Click
        currentSelectedBibliotecario = LB_BIB.SelectedIndex
        If currentSelectedBibliotecario < 0 Then
            MsgBox("Por favor selecione um Bibliotecário para editar!")
            Exit Sub
        End If
        adding = False
        add = False
        HideShowButtons(3, False)
        LockUnlockControls(3, True)
        TB_CODBIB.ReadOnly = True
        TB_IDPESSOA_B.ReadOnly = True
        TB_CODBIB_BIB.ReadOnly = True
        LB_BIB.Enabled = False

    End Sub

    Private Sub Edit_Leitor_Click(sender As Object, e As EventArgs) Handles Edit_Leitor.Click
        currentSelectedLeitor = LB_Leitor.SelectedIndex
        If currentSelectedLeitor < 0 Then
            MsgBox("Por favor selecione um Leitor para editar!")
            Exit Sub
        End If
        adding = False
        add = False
        HideShowButtons(2, False)
        LockUnlockControls(2, True)
        TB_COD_LEITOR.ReadOnly = True
        TB_IDPESSOA_L.ReadOnly = True
        TB_COD_BIB_LEITOR.ReadOnly = True
        LB_Leitor.Enabled = False
    End Sub
    Private Sub SetLists(ByVal LB As ListBox)
        Dim ItemAsString As String = ""
        For value As Integer = 0 To LB.Items.Count - 1
            ItemAsString = LB.Items.Item(value).ToString
            Dim EachField() As String = ItemAsString.Split(New Char() {" "c}) '0=Cod 3=Fname 6=Lname cuz of double auto space
            SortCode.Add(EachField(0))
            SortFname.Add(New KeyValuePair(Of Integer, String)(EachField(0), EachField(3)))
            SortLname.Add(New KeyValuePair(Of Integer, String)(EachField(0), EachField(6)))
        Next
    End Sub
    Private Sub SortAndDisplay(ByVal LB As ListBox, ByVal what As String, ByVal className As String)
        Dim LBItems As New List(Of Tuple(Of Integer, String, String))
        Select Case what
            Case "code"
                SortCode.Sort() 'Values of dic sorted(CodLeitor sorted)
                For index As Integer = 0 To SortCode.Count - 1
                    For Each pair As KeyValuePair(Of Integer, String) In SortFname
                        If SortCode(index) = pair.Key Then
                            For Each pair1 As KeyValuePair(Of Integer, String) In SortLname
                                If SortCode(index) = pair1.Key Then
                                    LBItems.Add(New Tuple(Of Integer, String, String)(SortCode(index), pair.Value, pair1.Value))
                                End If
                            Next
                        End If
                    Next
                Next
            Case "fname"
                SortFname = SortFname.OrderBy(Function(x) x.Value).ToList() 'sorted by Fname
                For Each pair As KeyValuePair(Of Integer, String) In SortFname
                    For Each pair1 As KeyValuePair(Of Integer, String) In SortLname
                        If pair.Key = pair1.Key Then
                            LBItems.Add(New Tuple(Of Integer, String, String)(pair.Key, pair.Value, pair1.Value))
                        End If
                    Next
                Next
            Case "lname"
                SortLname = SortLname.OrderBy(Function(x) x.Value).ToList() 'sorted by Lname
                For Each pair As KeyValuePair(Of Integer, String) In SortLname
                    For Each pair1 As KeyValuePair(Of Integer, String) In SortFname
                        If pair.Key = pair1.Key Then
                            LBItems.Add(New Tuple(Of Integer, String, String)(pair.Key, pair1.Value, pair.Value))
                        End If
                    Next
                Next
        End Select
        LB.Items.Clear()
        Select Case className
            Case "leitor"
                For i As Integer = 0 To LBItems.Count - 1
                    For Each obj In auxListLeitor
                        If LBItems(i).Item1 = obj.CodLeitor Then
                            Dim L As New Leitor
                            L.CodLeitor = LBItems(i).Item1.ToString
                            L.PrimeiroNome = LBItems(i).Item2.ToString
                            L.UltimoNome = LBItems(i).Item3.ToString
                            L.IDPessoa = obj.IDPessoa
                            L.Morada = obj.Morada
                            L.Tlm = obj.Tlm
                            L.Cc = obj.Cc
                            L.Nif = obj.Nif
                            L.DataNasc = obj.DataNasc
                            L.Genero = obj.Genero
                            L.Username = obj.Username
                            L.Password = obj.Password
                            L.DataExpiro = obj.DataExpiro
                            L.DataRegisto = obj.DataRegisto
                            L.CodBiblioteca = obj.CodBiblioteca
                            LB.Items.Add(L)
                        End If
                    Next
                Next
                currentSelectedLeitor = 0
                ShowLeitor()
            Case "pessoa"
                For i As Integer = 0 To LBItems.Count - 1
                    For Each obj In auxListPessoa
                        If LBItems(i).Item1 = obj.IDPessoa Then
                            Dim P As New Pessoa
                            P.IDPessoa = LBItems(i).Item1.ToString
                            P.PrimeiroNome = LBItems(i).Item2.ToString
                            P.UltimoNome = LBItems(i).Item3.ToString
                            P.Morada = obj.Morada
                            P.Tlm = obj.Tlm
                            P.Cc = obj.Cc
                            P.Nif = obj.Nif
                            P.DataNasc = obj.DataNasc
                            P.Genero = obj.Genero
                            LB.Items.Add(P)
                        End If
                    Next
                Next
                currentSelectedPessoa = 0
                ShowPessoa()
            Case "autor"
                For i As Integer = 0 To LBItems.Count - 1
                    For Each obj In auxListAutor
                        If LBItems(i).Item1 = obj.CodAutor Then
                            Dim A As New Autor
                            A.CodAutor = LBItems(i).Item1.ToString
                            A.PrimeiroNome = LBItems(i).Item2.ToString
                            A.UltimoNome = LBItems(i).Item3.ToString
                            A.IDPessoa = obj.IDPessoa
                            A.Morada = obj.Morada
                            A.Tlm = obj.Tlm
                            A.Cc = obj.Cc
                            A.Nif = obj.Nif
                            A.DataNasc = obj.DataNasc
                            A.Genero = obj.Genero
                            LB.Items.Add(A)
                        End If
                    Next
                Next
                currentSelectedAutor = 0
                ShowAutor()
            Case "bibliotecario"
                For i As Integer = 0 To LBItems.Count - 1
                    For Each Obj In auxListBib
                        If LBItems(i).Item1.ToString = Obj.CodBibliotecario Then
                            Dim B As New Bibliotecario
                            B.CodBibliotecario = LBItems(i).Item1.ToString
                            B.PrimeiroNome = LBItems(i).Item2.ToString
                            B.UltimoNome = LBItems(i).Item3.ToString
                            B.IDPessoa = Obj.IDPessoa
                            B.CodBiblioteca = Obj.CodBiblioteca
                            B.Morada = Obj.Morada
                            B.Tlm = Obj.Tlm
                            B.Cc = Obj.Cc
                            B.Nif = Obj.Nif
                            B.DataNasc = Obj.DataNasc
                            B.Genero = Obj.Genero
                            B.Salary = Obj.Salary
                            B.Password = Obj.Password
                            B.Username = Obj.Username
                            LB.Items.Add(B)
                        End If
                    Next
                Next
                currentSelectedBibliotecario = 0
                ShowBibliotecario()
        End Select

    End Sub
    Private Sub ListByLeitor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LISTBY_LEITOR.SelectedIndexChanged
        SortCode.Clear()
        SortFname.Clear()
        SortLname.Clear()
        Dim selected As String = ""
        selected = LISTBY_LEITOR.SelectedItem.ToString()
        SetLists(LB_Leitor)
        Select Case selected
            Case "Código Leitor"
                SortAndDisplay(LB_Leitor, "code", "leitor")
            Case "Primeiro Nome"
                SortAndDisplay(LB_Leitor, "fname", "leitor")
            Case "Último Nome"
                SortAndDisplay(LB_Leitor, "lname", "leitor")
        End Select
    End Sub

    Private Sub CB_PESSOA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LISTBY_PESSOA.SelectedIndexChanged
        SortCode.Clear()
        SortFname.Clear()
        SortLname.Clear()
        Dim selected As String = ""
        selected = LISTBY_PESSOA.SelectedItem.ToString()
        SetLists(LB_Pessoa)
        Select Case selected
            Case "Código Leitor"
                SortAndDisplay(LB_Pessoa, "code", "pessoa")
            Case "Primeiro Nome"
                SortAndDisplay(LB_Pessoa, "fname", "pessoa")
            Case "Último Nome"
                SortAndDisplay(LB_Pessoa, "lname", "pessoa")
        End Select
    End Sub

    Private Sub CB_AUTOR_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LISTBY_AUTOR.SelectedIndexChanged
        SortCode.Clear()
        SortFname.Clear()
        SortLname.Clear()
        Dim selected As String = ""
        selected = LISTBY_AUTOR.SelectedItem.ToString()
        SetLists(LB_AUTOR)
        Select Case selected
            Case "Código Autor"
                SortAndDisplay(LB_AUTOR, "code", "autor")
            Case "Primeiro Nome"
                SortAndDisplay(LB_AUTOR, "fname", "autor")
            Case "Último Nome"
                SortAndDisplay(LB_AUTOR, "lname", "autor")
        End Select
    End Sub

    Private Sub CB_Bibliotecario_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LISTBY_BIBLIOTECARIO.SelectedIndexChanged
        SortCode.Clear()
        SortFname.Clear()
        SortLname.Clear()
        Dim selected As String = ""
        selected = LISTBY_BIBLIOTECARIO.SelectedItem.ToString()
        SetLists(LB_BIB)
        Select Case selected
            Case "Cod.Bibliotecário"
                SortAndDisplay(LB_BIB, "code", "bibliotecario")
            Case "Primeiro Nome"
                SortAndDisplay(LB_BIB, "fname", "bibliotecario")
            Case "Último Nome"
                SortAndDisplay(LB_BIB, "lname", "bibliotecario")
        End Select
    End Sub
    Private Sub SetLists2(ByVal LB As ListBox)
        Dim ItemAsString As String = ""
        For value As Integer = 0 To LB.Items.Count - 1
            ItemAsString = LB.Items.Item(value).ToString
            Dim EachField() As String = ItemAsString.Split(New Char() {" "c}) '0=Cod 3=Stringcuz of double auto space
            SortCode.Add(EachField(0))
            SortFname.Add(New KeyValuePair(Of Integer, String)(EachField(0), EachField(3)))
        Next
    End Sub
    Private Sub SortAndDisplay2(ByVal LB As ListBox, ByVal what As String, ByVal className As String)
        Dim LBItems As New List(Of Tuple(Of Integer, String))
        Select Case what
            Case "code"
                SortCode.Sort() 'Values of dic sorted(Code sorted)
                For index As Integer = 0 To SortCode.Count - 1
                    For Each pair As KeyValuePair(Of Integer, String) In SortFname
                        If SortCode(index) = pair.Key Then
                            LBItems.Add(New Tuple(Of Integer, String)(SortCode(index), pair.Value))
                        End If
                    Next
                Next
            Case "name"
                SortFname = SortFname.OrderBy(Function(x) x.Value).ToList() 'sorted by Fname
                For Each pair As KeyValuePair(Of Integer, String) In SortFname
                    LBItems.Add(New Tuple(Of Integer, String)(pair.Key, pair.Value))
                Next
        End Select
        LB.Items.Clear()
        Select Case className
            Case "categoria"
                For i As Integer = 0 To LBItems.Count - 1
                    For Each obj In auxListCategoria
                        If LBItems(i).Item1 = obj.CodCategoria Then 'Not necessary, just to keep like the others(Categoria just have 2 fields)
                            Dim C As New Categoria
                            C.CodCategoria = LBItems(i).Item1.ToString
                            C.Categoria = LBItems(i).Item2.ToString
                            LB.Items.Add(C)
                        End If
                    Next
                Next
                currentSelectedCategoria = 0
                ShowCategorias()
            Case "editora"
                For i As Integer = 0 To LBItems.Count - 1
                    For Each obj In auxListEditora
                        If LBItems(i).Item1 = obj.CodEditora Then
                            Dim Ed As New Editora
                            Ed.CodEditora = LBItems(i).Item1.ToString
                            Ed.NomeEditora = LBItems(i).Item2.ToString
                            Ed.Endereco = obj.Endereco
                            Ed.Telefone = obj.Telefone
                            LB.Items.Add(Ed)
                        End If
                    Next
                Next
                currentSelectedEditora = 0
                ShowEditoras()
            Case "fabricante"
                For i As Integer = 0 To LBItems.Count - 1
                    For Each obj In auxListFabricante
                        If LBItems(i).Item1 = obj.CodFabricante Then
                            Dim f As New Fabricante
                            f.CodFabricante = LBItems(i).Item1.ToString
                            f.Fabricante = LBItems(i).Item2.ToString
                            f.Endereco = obj.Endereco
                            f.Telefone = obj.Telefone
                            LB.Items.Add(f)
                        End If
                    Next
                Next
                currentSelectedFabricante = 0
                ShowFabricantes()
            Case "tipo"
                For i As Integer = 0 To LBItems.Count - 1
                    For Each obj In auxListTipo
                        If LBItems(i).Item1 = obj.CodTipo Then 'Not necessary, just to keep like the others(Tipo just have 2 fields)
                            Dim T As New Tipo
                            T.CodTipo = LBItems(i).Item1.ToString
                            T.Tipo = LBItems(i).Item2.ToString
                            LB.Items.Add(T)
                        End If
                    Next
                Next
                currentSelectedTipo = 0
                ShowTipos()
            Case "biblioteca"
                For i As Integer = 0 To LBItems.Count - 1
                    For Each Obj In auxListBiblioteca
                        If LBItems(i).Item1.ToString = Obj.CodBiblioteca Then
                            Dim B As New Biblioteca
                            B.CodBiblioteca = LBItems(i).Item1.ToString
                            B.Nome = LBItems(i).Item2.ToString
                            B.Endereco = Obj.Endereco
                            LB.Items.Add(B)
                        End If
                    Next
                Next
                currentSelectedBiblioteca = 0
                ShowBibliotecas()
        End Select
    End Sub
    Private Sub CB_Fabricantes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LISTBY_FABRICANTE.SelectedIndexChanged
        SortCode.Clear()
        SortFname.Clear()
        Dim selected As String = ""
        selected = LISTBY_FABRICANTE.SelectedItem.ToString()
        SetLists2(LB_FABRICANTES)
        Select Case selected
            Case "Cód. Fabricante"
                SortAndDisplay2(LB_FABRICANTES, "code", "fabricante")
            Case "Fabricante"
                SortAndDisplay2(LB_FABRICANTES, "name", "fabricante")
        End Select
    End Sub

    Private Sub CB_Tipos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LISTBY_TIPOS.SelectedIndexChanged
        SortCode.Clear()
        SortFname.Clear()
        Dim selected As String = ""
        selected = LISTBY_TIPOS.SelectedItem.ToString()
        SetLists2(LB_TIPOS)
        Select Case selected
            Case "Cód. Tipo"
                SortAndDisplay2(LB_TIPOS, "code", "tipo")
            Case "Tipo"
                SortAndDisplay2(LB_TIPOS, "name", "tipo")
        End Select
    End Sub

    Private Sub CB_Categorias_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LISTBY_CATEGORIA.SelectedIndexChanged
        SortCode.Clear()
        SortFname.Clear()
        Dim selected As String = ""
        selected = LISTBY_CATEGORIA.SelectedItem.ToString()
        SetLists2(LB_CATEGORIA)
        Select Case selected
            Case "Cód. Categoria"
                SortAndDisplay2(LB_CATEGORIA, "code", "categoria")
            Case "Categoria"
                SortAndDisplay2(LB_CATEGORIA, "name", "categoria")
        End Select
    End Sub

    Private Sub CB_Editoras_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LISTBY_EDITORA.SelectedIndexChanged
        SortCode.Clear()
        SortFname.Clear()
        Dim selected As String = ""
        selected = LISTBY_EDITORA.SelectedItem.ToString()
        SetLists2(LB_Editora)
        Select Case selected
            Case "Cód. Editora"
                SortAndDisplay2(LB_Editora, "code", "editora")
            Case "Editora"
                SortAndDisplay2(LB_Editora, "name", "editora")
        End Select
    End Sub

    Private Sub CB_Biblioteca_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LISTBY_BIBLIOTECA.SelectedIndexChanged
        SortCode.Clear()
        SortFname.Clear()
        Dim selected As String = ""
        selected = LISTBY_BIBLIOTECA.SelectedItem.ToString()
        SetLists2(LB_Biblioteca)
        Select Case selected
            Case "Código"
                SortAndDisplay2(LB_Biblioteca, "code", "biblioteca")
            Case "Nome"
                SortAndDisplay2(LB_Biblioteca, "name", "biblioteca")
        End Select
    End Sub

    Private Sub REQ_IP_Click(sender As Object, e As EventArgs) Handles REQ_IP.Click
        currentSelectedItemPapel = LB_ITEMS_PAPEL.SelectedIndex
        If currentSelectedItemPapel < 0 Then
            MsgBox("Por favor selecione um Item Papel para requisitar!")
            Exit Sub
        End If
        Dim EachField() As String = LB_ITEMS_PAPEL.SelectedItem.ToString().Split(New Char() {" "c})
        'EachField(0) - code pk
        ShowPanel(7)
        LockUnlockControls(7, False)
        CMD.CommandText = "SELECT * FROM PROJECT.REQUISICAO_ITEM_PAPEL"
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        LB_ReqItemP.Items.Clear()
        auxListRIP = New List(Of ReqPapel)
        While RDR.Read
            Dim RIP As New ReqPapel
            RIP.CodReq = RDR.Item("COD_REQUISICAO")
            RIP.CodItemPapel = RDR.Item("COD_ITEM_PAPEL")
            RIP.CodLeitor = RDR.Item("COD_LEITOR")
            RIP.DataReq = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DATA_REQUISICAO")), "", RDR.Item("DATA_REQUISICAO")))
            RIP.DataReal = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DT_EN_REAL")), "", RDR.Item("DT_EN_REAL")))
            RIP.DataPrev = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DT_EN_PREVISTA")), "", RDR.Item("DT_EN_PREVISTA")))
            RIP.Multa = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("MULTA")), "", RDR.Item("MULTA")))
            LB_ReqItemP.Items.Add(RIP)
            auxListRIP.Add(RIP)
        End While
        CN.Close()
        currentSelectedReqItemPapel = 0
        ShowReqItemPapel()
        adding = True
        ClearFields(7)
        HideShowButtons(7, False)
        Dim todaysdate As String = String.Format("{0:dd/MM/yyyy}", DateTime.Now)  'hello'
        TB_DATAREQ_RIP.Text = todaysdate
        'TB_MULTA_RIP.ReadOnly = True
        LB_ReqItemP.Enabled = False
        TB_CODIP_RIP.Text = EachField(0)
        Dim bool As Boolean = True
        Dim pk As String = ""

        GETCL(CB_CODLEITOR_RIP, TB_CODLEITOR_RIP)
        CB_CODLEITOR_RIP.SelectedIndex = 0
        Dim random As New Random
        While bool
            pk = ""
            For i As Integer = 0 To 8
                If i = 0 Then
                    Dim value As String = Convert.ToString(random.Next(1, 9))
                    pk = pk + value
                Else
                    Dim value As String = Convert.ToString(random.Next(0, 9))
                    pk = pk + value
                End If
            Next
            For Each obj In auxListRIP
                If Equals(obj.CodReq, pk) Then
                    Continue For
                Else
                    bool = False
                End If
            Next
        End While
        adding = True
        add = True
        fromBack = True
        TB_CODREQ_RIP.Text = pk
        TB_CODREQ_RIP.ReadOnly = True
        TB_CODIP_RIP.ReadOnly = True
        TB_MULTA_RIP.ReadOnly = True
        TB_MULTA_RIP.Text = "Política interna"
    End Sub
    Private Sub SetLists3(ByVal LB As ListBox)
        Dim ItemAsString As String = ""
        For value As Integer = 0 To LB.Items.Count - 1
            ItemAsString = LB.Items.Item(value).ToString
            Dim EachField() As String = ItemAsString.Split(New Char() {" "c}) '0=Cod 3=Fname 6=Lname cuz of double auto space
            SortCode.Add(EachField(0)) 'CodReq pk
            SortCodItem.Add(New KeyValuePair(Of Integer, Integer)(EachField(0), EachField(3))) 'CodItem fk
            SortCodLeitor.Add(New KeyValuePair(Of Integer, Integer)(EachField(0), EachField(6))) 'CodLeitor fk
        Next
    End Sub
    Private Sub SortAndDisplay3(ByVal LB As ListBox, ByVal what As String, ByVal className As String)
        Dim LBItems As New List(Of Tuple(Of Integer, Integer, Integer))
        Select Case what
            Case "code"
                SortCode.Sort() 'Values of dic sorted(CodReq sorted)
                For index As Integer = 0 To SortCode.Count - 1
                    For Each pair As KeyValuePair(Of Integer, Integer) In SortCodItem 'pair.value=CodItem
                        If SortCode(index) = pair.Key Then
                            For Each pair1 As KeyValuePair(Of Integer, Integer) In SortCodLeitor 'pair.value=CodLeitor
                                If SortCode(index) = pair1.Key Then
                                    LBItems.Add(New Tuple(Of Integer, Integer, Integer)(SortCode(index), pair.Value, pair1.Value))
                                End If
                            Next
                        End If
                    Next
                Next
            Case "codeI"
                SortCodItem = SortCodItem.OrderBy(Function(x) x.Value).ToList() 'sorted by CodItem
                For Each pair As KeyValuePair(Of Integer, Integer) In SortCodItem
                    For Each pair1 As KeyValuePair(Of Integer, Integer) In SortCodLeitor
                        If pair.Key = pair1.Key Then
                            LBItems.Add(New Tuple(Of Integer, Integer, Integer)(pair.Key, pair.Value, pair1.Value))
                        End If
                    Next
                Next

            Case "codeL"
                SortCodLeitor = SortCodLeitor.OrderBy(Function(x) x.Value).ToList() 'sorted by Lname
                For Each pair As KeyValuePair(Of Integer, Integer) In SortCodLeitor
                    For Each pair1 As KeyValuePair(Of Integer, Integer) In SortCodItem
                        If pair.Key = pair1.Key Then
                            LBItems.Add(New Tuple(Of Integer, Integer, Integer)(pair.Key, pair1.Value, pair.Value))
                        End If
                    Next
                Next
        End Select
        LB.Items.Clear()
        Select Case className
            Case "RIP"
                For i As Integer = 0 To LBItems.Count - 1
                    For Each obj In auxListRIP
                        If LBItems(i).Item1 = obj.CodReq Then
                            Dim RIP As New ReqPapel
                            RIP.CodReq = LBItems(i).Item1.ToString
                            RIP.CodItemPapel = LBItems(i).Item2.ToString
                            RIP.CodLeitor = LBItems(i).Item3.ToString
                            RIP.DataReq = obj.DataReq
                            RIP.DataReal = obj.DataReal
                            RIP.DataPrev = obj.DataPrev
                            RIP.Multa = obj.Multa
                            LB.Items.Add(RIP)
                        End If
                    Next
                Next
                currentSelectedReqItemPapel = 0
                ShowReqItemPapel()
            Case "RIE"
                For i As Integer = 0 To LBItems.Count - 1
                    For Each obj In auxListRIE
                        If LBItems(i).Item1 = obj.CodReq Then
                            Dim RIE As New ReqEletronica
                            RIE.CodReq = LBItems(i).Item1.ToString
                            RIE.CodItemEletronica = LBItems(i).Item2.ToString
                            RIE.CodLeitor = LBItems(i).Item3.ToString
                            RIE.Marcacao = obj.Marcacao
                            RIE.Duracao = obj.Duracao
                            RIE.HoraInicio = obj.HoraInicio
                            LB.Items.Add(RIE)
                        End If
                    Next
                Next
                currentSelectedReqItemElect = 0
                ShowReqItemElect()
        End Select

    End Sub

    Private Sub CB_RIP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LISTBY_RIP.SelectedIndexChanged
        SortCode.Clear()
        SortCodItem.Clear()
        SortCodLeitor.Clear()
        Dim selected As String = ""
        selected = LISTBY_RIP.SelectedItem.ToString()
        SetLists3(LB_ReqItemP)
        Select Case selected
            Case "Cód. Requisição"
                SortAndDisplay3(LB_ReqItemP, "code", "RIP")
            Case "Cód. Item Papel"
                SortAndDisplay3(LB_ReqItemP, "codeI", "RIP")
            Case "Cód. Leitor"
                SortAndDisplay3(LB_ReqItemP, "codeL", "RIP")
        End Select
    End Sub

    Private Sub CB_RIE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LISTBY_RIE.SelectedIndexChanged
        SortCode.Clear()
        SortCodItem.Clear()
        SortCodLeitor.Clear()
        Dim selected As String = ""
        selected = LISTBY_RIE.SelectedItem.ToString()
        SetLists3(LB_ReqItemElect)
        Select Case selected
            Case "Cód. Requisição"
                SortAndDisplay3(LB_ReqItemElect, "code", "RIE")
            Case "Cód. Item Electrónica"
                SortAndDisplay3(LB_ReqItemElect, "codeI", "RIE")
            Case "Cód. Leitor"
                SortAndDisplay3(LB_ReqItemElect, "codeL", "RIE")
        End Select
    End Sub
    Private Sub SetListsIE(ByVal LB As ListBox)
        Dim ItemAsString As String = ""
        For value As Integer = 0 To LB.Items.Count - 1
            ItemAsString = LB.Items.Item(value).ToString
            Dim EachField() As String = ItemAsString.Split(New Char() {" "c}) '0=Cod 3=Fname 6=Lname cuz of double auto space
            SortCode.Add(EachField(0)) 'CodReq pk
            SortCodItem.Add(New KeyValuePair(Of Integer, Integer)(EachField(0), EachField(3))) 'CodItem fk
        Next
    End Sub

    Private Sub CB_IE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LISTBY_IE.SelectedIndexChanged
        SortCode.Clear()
        SortCodItem.Clear()
        Dim selected As String = ""
        selected = LISTBY_IE.SelectedItem.ToString()
        SetListsIE(LB_ITEM_ELECT)
        Select Case selected
            Case "Cód. Item Eletrónica"
                SortAndDisplayIE(LB_ITEM_ELECT, "code")
            Case "Cód. Biblioteca"
                SortAndDisplayIE(LB_ITEM_ELECT, "codeI")
        End Select
    End Sub
    Private Sub SortAndDisplayIE(ByVal LB As ListBox, ByVal what As String)
        Dim LBItems As New List(Of Tuple(Of Integer, Integer))
        Select Case what
            Case "code"
                SortCode.Sort() 'Values of dic sorted(CodReq sorted)
                For index As Integer = 0 To SortCode.Count - 1
                    For Each pair As KeyValuePair(Of Integer, Integer) In SortCodItem 'pair.value=CodItem
                        If SortCode(index) = pair.Key Then
                            LBItems.Add(New Tuple(Of Integer, Integer)(SortCode(index), pair.Value))
                        End If
                    Next
                Next
            Case "codeI"
                SortCodItem = SortCodItem.OrderBy(Function(x) x.Value).ToList() 'sorted by CodItem
                For Each pair As KeyValuePair(Of Integer, Integer) In SortCodItem
                    LBItems.Add(New Tuple(Of Integer, Integer)(pair.Key, pair.Value))
                Next
        End Select
        LB.Items.Clear()
        For i As Integer = 0 To LBItems.Count - 1
            For Each obj In auxListIE
                If LBItems(i).Item1 = obj.CodItemEletronica Then
                    Dim IE As New ItemEletronica
                    IE.CodItemEletronica = LBItems(i).Item1.ToString
                    IE.CodBib = LBItems(i).Item2.ToString
                    IE.CodTipo = obj.CodTipo
                    IE.CodFabrincante = obj.CodFabrincante
                    LB.Items.Add(IE)
                End If
            Next
        Next
        currentSelectedItemElect = 0
        ShowReqItemElect()
    End Sub
    Private Sub SetListsIP(ByVal LB As ListBox)
        Dim ItemAsString As String = ""
        For value As Integer = 0 To LB.Items.Count - 1
            ItemAsString = LB.Items.Item(value).ToString
            Dim EachField() As String = ItemAsString.Split(New Char() {" "c}) '0=Cod 3=Fname 6=Lname cuz of double auto space
            SortCode.Add(EachField(0)) 'CodItem pk
            SortCodItem.Add(New KeyValuePair(Of Integer, Integer)(EachField(0), EachField(3))) 'CodBib fk
            SortFname.Add(New KeyValuePair(Of Integer, String)(EachField(0), EachField(6))) 'Titulo fk
        Next
    End Sub
    Private Sub SortAndDisplayIP(ByVal LB As ListBox, ByVal what As String)
        Dim LBItems As New List(Of Tuple(Of Integer, Integer, String))
        Select Case what
            Case "code"
                SortCode.Sort() 'Values of dic sorted(CodLeitor sorted)
                For index As Integer = 0 To SortCode.Count - 1
                    For Each pair As KeyValuePair(Of Integer, Integer) In SortCodItem 'codbib
                        If SortCode(index) = pair.Key Then
                            For Each pair1 As KeyValuePair(Of Integer, String) In SortFname 'titulo
                                If SortCode(index) = pair1.Key Then
                                    LBItems.Add(New Tuple(Of Integer, Integer, String)(SortCode(index), pair.Value, pair1.Value))
                                End If
                            Next
                        End If
                    Next
                Next
            Case "codeI"
                SortCodItem = SortCodItem.OrderBy(Function(x) x.Value).ToList() 'sorted by Fname
                For Each pair As KeyValuePair(Of Integer, Integer) In SortCodItem
                    For Each pair1 As KeyValuePair(Of Integer, String) In SortFname
                        If pair.Key = pair1.Key Then
                            LBItems.Add(New Tuple(Of Integer, Integer, String)(pair.Key, pair.Value, pair1.Value))
                        End If
                    Next
                Next
            Case "Titulo"
                SortFname = SortFname.OrderBy(Function(x) x.Value).ToList() 'sorted by Lname
                For Each pair As KeyValuePair(Of Integer, String) In SortFname
                    For Each pair1 As KeyValuePair(Of Integer, Integer) In SortCodItem
                        If pair.Key = pair1.Key Then
                            LBItems.Add(New Tuple(Of Integer, Integer, String)(pair.Key, pair1.Value, pair.Value))
                        End If
                    Next
                Next
        End Select
        LB.Items.Clear()
        For i As Integer = 0 To LBItems.Count - 1
            For Each obj In auxListIP
                If LBItems(i).Item1 = obj.CodItemPapel Then
                    Dim IP As New ItemPapel
                    IP.CodItemPapel = LBItems(i).Item1.ToString
                    IP.CodBib = LBItems(i).Item2.ToString
                    IP.CodTipo = obj.CodTipo
                    IP.CodCategoria = obj.CodCategoria
                    IP.CodEditora = obj.CodEditora
                    IP.CodAutor = obj.CodAutor
                    IP.Titulo = LBItems(i).Item3.ToString
                    IP.Edicao = obj.Edicao
                    IP.Idioma = obj.Idioma
                    IP.Dimensoes = obj.Dimensoes
                    IP.Permissao = obj.Permissao
                    IP.Volume = obj.Volume
                    IP.DataPub = obj.DataPub
                    IP.Classificacao = obj.Classificacao
                    IP.Descricao = obj.Descricao
                    IP.Cota = obj.Cota
                    LB.Items.Add(IP)
                End If
            Next
        Next
        currentSelectedItemPapel = 0
        ShowItemPapel()
    End Sub

    Private Sub CB_IP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LISTBY_IP.SelectedIndexChanged
        SortCode.Clear()
        SortCodItem.Clear()
        SortFname.Clear()
        Dim selected As String = ""
        selected = LISTBY_IP.SelectedItem.ToString()
        SetListsIP(LB_ITEMS_PAPEL)
        Select Case selected
            Case "Cód. Item Papel"
                SortAndDisplayIP(LB_ITEMS_PAPEL, "code")
            Case "Cód. Biblioteca"
                SortAndDisplayIP(LB_ITEMS_PAPEL, "codeI")
            Case "Título"
                SortAndDisplayIP(LB_ITEMS_PAPEL, "Titulo")
        End Select
    End Sub

    Private Sub CB_IDPessoa_A_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_IDPessoa_A.SelectedIndexChanged
        If CB_IDPessoa_A.SelectedIndex = -1 Then
            Exit Sub
        End If
        Dim help As String = CB_IDPessoa_A.SelectedItem.ToString()
        Dim help1 As String() = help.Split(New Char() {" "c})
        For Each obj In auxListPessoa
            If obj.IDPessoa = help1(0) Then
                TB_PN_AUTOR.Text = obj.PrimeiroNome
                TB_UN_AUTOR.Text = obj.UltimoNome
                TB_CC_AUTOR.Text = obj.Cc
                TB_NIF_AUTOR.Text = obj.Nif
                TB_MORADA_AUTOR.Text = obj.Morada
                TB_GENERO_AUTOR.Text = obj.Genero
                TB_TLM_AUTOR.Text = obj.Tlm
                TB_DATAN_AUTOR.Text = obj.DataNasc
            End If
        Next
    End Sub

    Private Sub CB_IDPessoa_L_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_IDPessoa_L.SelectedIndexChanged
        If CB_IDPessoa_L.SelectedIndex = -1 Then
            Exit Sub
        End If
        Dim help As String = CB_IDPessoa_L.SelectedItem.ToString()
        Dim help1 As String() = help.Split(New Char() {" "c})
        For Each obj In auxListPessoa
            If obj.IDPessoa = help1(0) Then
                TB_PM_LEITOR.Text = obj.PrimeiroNome
                TB_UN_LEITOR.Text = obj.UltimoNome
                TB_CC_LEITOR.Text = obj.Cc
                TB_NIF_LEITOR.Text = obj.Nif
                TB_MORADA_LEITOR.Text = obj.Morada
                TB_GENERO_LEITOR.Text = obj.Genero
                TB_TLM_LEITOR.Text = obj.Tlm
                TB_IDPESSOA_L.Text = obj.IDPessoa
                TB_DATAN_LEITOR.Text = obj.DataNasc
            End If

        Next
    End Sub

    Private Sub CB_IDPessoa_B_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_IDPessoa_B.SelectedIndexChanged
        If CB_IDPessoa_B.SelectedIndex = -1 Then
            Exit Sub
        End If
        Dim help As String = CB_IDPessoa_B.SelectedItem.ToString()
        Dim help1 As String() = help.Split(New Char() {" "c})
        For Each obj In auxListPessoa
            If obj.IDPessoa = help1(0) Then
                TB_PN_BIB.Text = obj.PrimeiroNome
                TB_UN_BIB.Text = obj.UltimoNome
                TB_CC_BIB.Text = obj.Cc
                TB_NIF_BIB.Text = obj.Nif
                TB_MORARA_BIB.Text = obj.Morada
                TB_GENERO_BIB.Text = obj.Genero
                TB_TLM_BIB.Text = obj.Tlm
                TB_IDPESSOA_B.Text = obj.IDPessoa
                TB_DATAN_BIB.Text = obj.DataNasc
            End If
        Next
    End Sub

    Private Sub reqIE_Click(sender As Object, e As EventArgs) Handles reqIE.Click
        currentSelectedItemElect = LB_ITEM_ELECT.SelectedIndex
        If currentSelectedItemElect < 0 Then
            MsgBox("Por favor selecione um Item Eletrónica para requisitar!")
            Exit Sub
        End If
        Dim EachField() As String = LB_ITEM_ELECT.SelectedItem.ToString().Split(New Char() {" "c})
        ShowPanel(8)
        LockUnlockControls(8, False)

        CMD.CommandText = "SELECT * FROM PROJECT.REQUISICAO_ITEM_ELECT"
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        LB_ReqItemElect.Items.Clear()
        auxListRIE = New List(Of ReqEletronica)
        While RDR.Read
            Dim RIE As New ReqEletronica
            RIE.CodReq = RDR.Item("COD_REQUISICAO")
            RIE.CodItemEletronica = RDR.Item("COD_ITEM_ELECT")
            RIE.CodLeitor = RDR.Item("COD_LEITOR")
            RIE.Marcacao = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("MARCACAO")), "", RDR.Item("MARCACAO")))
            RIE.Duracao = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("DURACAO")), "", RDR.Item("DURACAO")))
            RIE.HoraInicio = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("INICIO")), "", RDR.Item("INICIO")))
            LB_ReqItemElect.Items.Add(RIE)
            auxListRIE.Add(RIE)
        End While
        CN.Close()
        adding = True
        currentSelectedReqItemPapel = 0
        ShowReqItemElect()
        ClearFields(8)
        HideShowButtons(8, False)
        LB_ReqItemElect.Enabled = False
        TB_CODITEMELECT_RIE.Text = EachField(0)
        GETCL(CB_CodLeitor_RIE, TB_CODLEITOR_RIE)
        Dim todaysdate As String = String.Format("{0:MM/dd/yyyy}", DateTime.Now) 'Hello'
        TB_MARCACAO_RIE.Text = todaysdate
        CB_CodLeitor_RIE.SelectedIndex = 0
        Dim bool As Boolean = True
        Dim pk As String = ""
        Dim random As New Random
        While bool
            pk = ""
            For i As Integer = 0 To 8
                If i = 0 Then
                    Dim value As String = Convert.ToString(random.Next(1, 9))
                    pk = pk + value
                Else
                    Dim value As String = Convert.ToString(random.Next(0, 9))
                    pk = pk + value
                End If
            Next
            For Each obj In auxListRIE
                If Equals(obj.CodReq, pk) Then
                    Continue For
                Else
                    bool = False
                End If
            Next
        End While
        fromBack = True
        add = True
        TB_CODREQ_RIE.Text = pk
        TB_CODREQ_RIE.ReadOnly = True
        TB_CODITEMELECT_RIE.ReadOnly = True
    End Sub

    Private Sub SairToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SairToolStripMenuItem.Click
        End
    End Sub

    Private Sub BReq_Click(sender As Object, e As EventArgs) Handles BReq.Click

        currentSelectedLeitor = LB_Leitor.SelectedIndex
        If currentSelectedLeitor < 0 Then
            MsgBox("Por favor selecione um leitor!")
            Exit Sub
        End If
        CMD.CommandText = "Select PROJECT.REQ_PESSOA(@ID_PESSOA)"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@ID_PESSOA", TB_COD_LEITOR.Text)
        CN.Open()
        Dim count As Integer
        count = CMD.ExecuteScalar()
        MsgBox("O número total de requisições para o leitor selecionado é: " & count)
        CN.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CMD.CommandText = "Select PROJECT.TotalMoney()"
        CN.Open()
        Dim count As Decimal
        count = CMD.ExecuteScalar()
        MsgBox("O ganho total em multas é: " & count & " Euros(€).")
        CN.Close()
    End Sub

End Class