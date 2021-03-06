--Drop TABLE PROJECT.REQUISICAO_ITEM_PAPEL;
--Drop TABLE PROJECT.REQUISICAO_ITEM_ELECT;
--Drop TABLE PROJECT.ITEM_PAPEL_AUTOR;
--Drop TABLE PROJECT.BIBLIOTECARIO;
--Drop TABLE PROJECT.LEITOR;
--Drop TABLE PROJECT.ITEM_PAPEL;
--Drop TABLE PROJECT.ITEM_ELECTRONICA;
--Drop TABLE PROJECT.AUTOR;
--Drop TABLE PROJECT.PESSOA;
--Drop TABLE PROJECT.CATEGORIA;
--Drop TABLE PROJECT.TIPO;
--Drop TABLE PROJECT.FABRICANTE;
--Drop TABLE PROJECT.EDITORA;
--Drop  TABLE PROJECT.BIBLIOTECA;

--DROP SCHEMA PROJECT;
--CREATE SCHEMA PROJECT; 
--GO
--CREATE TABLE PROJECT.BIBLIOTECA(
--	COD_BIB	INTEGER CHECK (COD_BIB like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	NOME Varchar(70) NOT NULL, 
--	ENDERECO Varchar(50) NOT NULL,
--	CONSTRAINT PKBIBLIOTECA PRIMARY KEY(COD_BIB));
--CREATE TABLE PROJECT.EDITORA(
--	COD_EDITORA INTEGER CHECK(COD_EDITORA LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	NOME Varchar(70) NOT NULL, 
--	ENDERECO Varchar(50) DEFAULT('UNKNOWN'),
--	TELEFONE INTEGER CHECK( TELEFONE LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]') ,
--	CONSTRAINT PKEDITORA PRIMARY KEY(COD_EDITORA));
--CREATE TABLE PROJECT.FABRICANTE(
--	COD_FABRICANTE INTEGER CHECK( COD_FABRICANTE LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	FABRICANTE Varchar(50) NOT NULL,
--	ENDERECO Varchar(50) DEFAULT('UNKNOWN'),
--	TELEFONE INTEGER  CHECK(TELEFONE LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]') ,
--	CONSTRAINT PKFABRICANTE PRIMARY KEY(COD_FABRICANTE));
--CREATE TABLE PROJECT.TIPO(
--	COD_TIPO INTEGER  CHECK(COD_TIPO LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	TIPO Varchar(50) NOT NULL, 
--	CONSTRAINT PKTIPO PRIMARY KEY(COD_TIPO));
--CREATE TABLE PROJECT.CATEGORIA(
--	COD_CATEGORIA INTEGER CHECK(COD_CATEGORIA LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	CATEGORIA Varchar(50) NOT NULL,
--	CONSTRAINT PKCATEGORIA PRIMARY KEY(COD_CATEGORIA));
--CREATE TABLE PROJECT.PESSOA(
--	ID_PESSOA INTEGER CHECK(ID_PESSOA LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	PRIMEIRO_NOME Varchar(70) NOT NULL,
--	ULTIMO_NOME Varchar(70),
--	MORADA Varchar(50) NOT NULL,
--	GENERO Char(1) CHECK(GENERO='F' OR GENERO='M'),
--	DATA_NASC DATE ,
--	TLM INTEGER,
--	CC INTEGER  CHECK(CC LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	NIF INTEGER CHECK(NIF LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	CONSTRAINT PKPESSOA PRIMARY KEY(ID_PESSOA));
--CREATE TABLE PROJECT.AUTOR(
--	COD_AUTOR INTEGER CHECK( COD_AUTOR LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' ), 
--	ID_PESSOA INTEGER CHECK( ID_PESSOA LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')  ,
--	CONSTRAINT FKAUTOR FOREIGN KEY(ID_PESSOA) REFERENCES PROJECT.PESSOA(ID_PESSOA) ON DELETE SET NULL,
--	CONSTRAINT PKAUTOR PRIMARY KEY(COD_AUTOR));
--CREATE TABLE PROJECT.ITEM_ELECTRONICA(
--	COD_ITEM_ELECT INTEGER CHECK( COD_ITEM_ELECT LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	COD_BIB INTEGER CHECK( COD_BIB LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]') ,
--	COD_TIPO INTEGER CHECK( COD_TIPO LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	COD_FABRICANTE INTEGER CHECK( COD_FABRICANTE LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	CONSTRAINT PKITEM_ELECT PRIMARY KEY(COD_ITEM_ELECT),
--	CONSTRAINT FKITEM_ELECT_COD_BIB FOREIGN KEY(COD_BIB) REFERENCES PROJECT.BIBLIOTECA(COD_BIB) ON DELETE SET NULL,
--	CONSTRAINT FKITEM_ELECT_COD_TIPO FOREIGN KEY(COD_TIPO) REFERENCES PROJECT.TIPO(COD_TIPO) ON DELETE SET NULL,
--	CONSTRAINT FKITEM_ELECT_COD_FABRICANTE FOREIGN KEY(COD_FABRICANTE) REFERENCES PROJECT.FABRICANTE(COD_FABRICANTE) ON DELETE SET NULL);
--CREATE TABLE PROJECT.ITEM_PAPEL(
--	COD_ITEM_PAPEL INTEGER CHECK( COD_ITEM_PAPEL LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	COD_BIB INTEGER CHECK( COD_BIB LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	COD_TIPO INTEGER CHECK( COD_TIPO LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	COD_CATEGORIA INTEGER CHECK( COD_CATEGORIA  LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	COD_EDITORA INTEGER CHECK( COD_EDITORA LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	TITULO Varchar(100) NOT NULL,
--	EDICAO Integer NOT NULL CHECK(  EDICAO LIKE '[0-9][0-9][0-9]'),
--	IDIOMA Varchar(10) NOT NULL,
--	DIMENSOES Varchar(12) DEFAULT('UNKNOWN'),
--	PERMISSAO BIT NOT NULL CHECK(PERMISSAO=0 or PERMISSAO=1),
--	VOLUME Integer  CHECK(VOLUME LIKE '[0-9][0-9][0-9]') ,
--	DATA_PUB DATE ,
--	CLASSIFICACAO INTEGER CHECK(CLASSIFICACAO < 11), 
--	DESCRICAO Varchar(250) DEFAULT('NO DESCRIPTION'), 
--	COTA Integer NOT NULL, 
--	CONSTRAINT PKITEM_PAPEL PRIMARY KEY(COD_ITEM_PAPEL),
--	CONSTRAINT FKITEM_PAPEL_COD_BIB FOREIGN KEY(COD_BIB) REFERENCES PROJECT.BIBLIOTECA(COD_BIB) ON DELETE SET NULL,
--	CONSTRAINT FKITEM_PAPEL_COD_TIPO FOREIGN KEY(COD_TIPO) REFERENCES PROJECT.TIPO(COD_TIPO) ON DELETE SET NULL,
--	CONSTRAINT FKITEM_PAPEL_COD_CATEGORIA FOREIGN KEY(COD_CATEGORIA) REFERENCES PROJECT.CATEGORIA(COD_CATEGORIA) ON DELETE SET NULL,
--	CONSTRAINT FKITEM_PAPEL_COD_EDITORA FOREIGN KEY(COD_EDITORA) REFERENCES PROJECT.EDITORA(COD_EDITORA) ON DELETE SET NULL);
--CREATE TABLE PROJECT.LEITOR(
--	COD_LEITOR INTEGER CHECK(COD_LEITOR LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	ID_PESSOA INTEGER  CHECK(ID_PESSOA LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	USERNAME Varchar(10) NOT NULL,
--	PASS Varchar(15) NOT NULL,
--	DATA_EXPIRO DATE NOT NULL,
--	DATA_REGISTO DATE NOT NULL ,
--	COD_BIB	INTEGER CHECK(COD_BIB LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	CONSTRAINT PKLEITOR_COD_BIB FOREIGN KEY(COD_BIB) REFERENCES PROJECT.BIBLIOTECA(COD_BIB) ON DELETE SET NULL,
--	CONSTRAINT PKLEITOR PRIMARY KEY(COD_LEITOR),
--	CONSTRAINT FKLEITOR_ID_PESSOA FOREIGN KEY(ID_PESSOA) REFERENCES PROJECT.PESSOA(ID_PESSOA) ON DELETE SET NULL,
--	CHECK (DATA_REGISTO<DATA_EXPIRO)); 
--CREATE TABLE PROJECT.BIBLIOTECARIO(
--	COD_BIBLIOTECARIO INTEGER CHECK(COD_BIBLIOTECARIO LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	ID_PESSOA INTEGER CHECK(ID_PESSOA LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	COD_BIB INTEGER CHECK(COD_BIB LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	SALARY DECIMAL(6,2) NOT NULL CHECK(SALARY < 2000),
--	USERNAME Varchar(10) NOT NULL,
--	PASS Varchar(15) NOT NULL,
--	CONSTRAINT PKBIBLIOTECARIO PRIMARY KEY(COD_BIBLIOTECARIO),
--	CONSTRAINT FKBIBLIOTECARIO_ID_PESSOA FOREIGN KEY(ID_PESSOA) REFERENCES PROJECT.PESSOA(ID_PESSOA) ON DELETE SET NULL,
--	CONSTRAINT FKBIBLIOTECARIO_COD_BIB FOREIGN KEY(COD_BIB) REFERENCES PROJECT.BIBLIOTECA(COD_BIB) ON DELETE SET NULL);
--CREATE TABLE PROJECT.ITEM_PAPEL_AUTOR(
--	COD_ITEM_PAPEL INTEGER CHECK(COD_ITEM_PAPEL LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	COD_AUTOR INTEGER CHECK(COD_AUTOR LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	CONSTRAINT FKITEM_PAPEL_AUTOR_ITEM FOREIGN KEY(COD_ITEM_PAPEL) REFERENCES PROJECT.ITEM_PAPEL(COD_ITEM_PAPEL),
--	CONSTRAINT FKITEM_PAPEL_AUTOR_AUTOR FOREIGN KEY(COD_AUTOR) REFERENCES PROJECT.AUTOR(COD_AUTOR),
--	CONSTRAINT PKITEM_PAPEL_AUTOR PRIMARY KEY(COD_ITEM_PAPEL, COD_AUTOR));
--CREATE TABLE PROJECT.REQUISICAO_ITEM_ELECT(
--	COD_REQUISICAO INTEGER CHECK(COD_REQUISICAO LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	COD_ITEM_ELECT INTEGER CHECK(COD_ITEM_ELECT LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	COD_LEITOR INTEGER CHECK(COD_LEITOR LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	MARCACAO DATE DEFAULT('01/01/0000'),
--	DURACAO INTEGER DEFAULT('0'),
--	INICIO TIME DEFAULT('00:00:00'),
--	CONSTRAINT FKREQUISICAO_ITEM_ELECT_COD_ITEM_ELECT FOREIGN KEY(COD_ITEM_ELECT) REFERENCES PROJECT.ITEM_ELECTRONICA(COD_ITEM_ELECT),
--	CONSTRAINT FKREQUISICAO_ITEM_ELECT_COD_LEITOR FOREIGN KEY(COD_LEITOR) REFERENCES PROJECT.LEITOR(COD_LEITOR),
--	CONSTRAINT PKREQUISICAO_ITEM_ELECT PRIMARY KEY(COD_REQUISICAO));
--CREATE TABLE PROJECT.REQUISICAO_ITEM_PAPEL(
--	COD_ITEM_PAPEL INTEGER CHECK(COD_ITEM_PAPEL LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	COD_LEITOR INTEGER CHECK(COD_LEITOR LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	COD_REQUISICAO INTEGER CHECK(COD_REQUISICAO LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
--	DATA_REQUISICAO DATE DEFAULT('01/01/0000'),
--	DT_EN_REAL DATE DEFAULT('01/01/0000'),
--	DT_EN_PREVISTA DATE DEFAULT('01/01/0000'),
--	MULTA DECIMAL(5,2) CHECK(MULTA < 200),
--	CONSTRAINT FKREQUISICAO_ITEM_PAPEL_COD_ITEM_PAPEL FOREIGN KEY(COD_ITEM_PAPEL) REFERENCES PROJECT.ITEM_PAPEL(COD_ITEM_PAPEL),
--	CONSTRAINT FKREQUISICAO_ITEM_PAPEL_COD_LEITOR FOREIGN KEY(COD_LEITOR) REFERENCES PROJECT.LEITOR(COD_LEITOR),
--	CONSTRAINT PKREQUISICAO_ITEM_PAPEL PRIMARY KEY(COD_REQUISICAO));




--GO
--CREATE TRIGGER MARCACAO_ON_REQ_ITEM_ELECT ON PROJECT.REQUISICAO_ITEM_ELECT
--INSTEAD OF INSERT, UPDATE
--AS
--BEGIN
--	DECLARE @DOM VARCHAR(10)
--	DECLARE @DATE DATE
--	DECLARE @DURACAO INTEGER
--	DECLARE @INICIO TIME
--	DECLARE @FREETIME INTEGER
--	DECLARE @BEGINS TIME = '10:00:00'
--	DECLARE @ENDF TIME = '19:00:00'
--	DECLARE @ENDS TIME = '13:00:00'
--	DECLARE @ACTUAL_DATE DATE
--	DECLARE @action as char(1);
--	DECLARE @CIE INT
--	DECLARE @CL INT
--	DECLARE @CR INT 
--	SELECT @DATE=MARCACAO, @DURACAO=DURACAO, @INICIO=INICIO, @CIE=COD_ITEM_ELECT, @CL=COD_LEITOR, @CR=COD_REQUISICAO FROM inserted
--	SET @DOM = dbo.GETDOM(@DATE)
--	SET @ACTUAL_DATE = GETDATE()

--	SET @action = 'I'; 
--	IF EXISTS(SELECT * FROM DELETED)
--	BEGIN
--		SET @action=
--			CASE
--				WHEN EXISTS(SELECT * FROM INSERTED) THEN 'U'
--				ELSE 'D'
--			END
--	END

--	IF @DATE < @ACTUAL_DATE
--		BEGIN
--			RAISERROR('Data inv�lida! Tem de inserir uma data igual ou superior � data atual!',16,1);
--			RETURN;
--		END
--	ELSE
--		IF @DOM = 'Monday' OR @DOM = 'Tuesday' OR @DOM = 'Wednesday' OR @DOM = 'Thursday' OR @DOM = 'Friday'
--		BEGIN
--			IF @INICIO >= @BEGINS AND @INICIO <= @ENDF
--				BEGIN
--					SET @FREETIME = (SELECT DATEDIFF(MI, @INICIO, @ENDF))
--					IF @DURACAO <= @FREETIME
--						IF @action='I'
--							INSERT INTO PROJECT.REQUISICAO_ITEM_ELECT SELECT * FROM inserted;
--						ELSE
--							UPDATE PROJECT.REQUISICAO_ITEM_ELECT SET COD_ITEM_ELECT=@CIE, COD_LEITOR=@CL,MARCACAO=@DATE,DURACAO=@DURACAO,INICIO=@INICIO	WHERE COD_REQUISICAO=@CR
--					ELSE
--						RAISERROR('A dura��o da marca��o tem que ser compat�vel com o hor�rio da biblioteca!',16,1);	
--				END
--			ELSE
--				RAISERROR('O inicio da marca��o tem que ser compat�vel com o hor�rio da biblioteca!',16,1);		
--		END 
--		IF @DOM = 'Saturday'
--			BEGIN
--				IF @INICIO >= @BEGINS AND @INICIO <= @ENDS
--					BEGIN
--						SET @FREETIME = (SELECT DATEDIFF(MI, @INICIO, @ENDS))
--						IF @DURACAO <= @FREETIME
--							IF @action='I'
--								INSERT INTO PROJECT.REQUISICAO_ITEM_ELECT SELECT * FROM inserted;
--							ELSE
--								UPDATE PROJECT.REQUISICAO_ITEM_ELECT SET COD_ITEM_ELECT=@CIE, COD_LEITOR=@CL,MARCACAO=@DATE,DURACAO=@DURACAO,INICIO=@INICIO	WHERE COD_REQUISICAO=@CR
--						ELSE
--							RAISERROR('A dura��o da marca��o tem que ser compat�vel com o hor�rio da biblioteca!',16,1);
--					END
--				ELSE
--					RAISERROR('O inicio da marca��o tem que ser compat�vel com o hor�rio da biblioteca!',16,1);		
--			END 
--		IF @DOM='Sunday'
--			BEGIN
--				RAISERROR('O hor�rio da biblioteca � de Segunda a S�bado. Por favor escolha outro dia da semana!',16,1);
--			END
--END

--CREATE TRIGGER DELETEITEMPAPEL ON   PROJECT.ITEM_PAPEL
--INSTEAD OF DELETE
--AS
--BEGIN
--	DELETE FROM PROJECT.ITEM_PAPEL_AUTOR WHERE COD_ITEM_PAPEL = (SELECT COD_ITEM_PAPEL FROM deleted);
--	DELETE FROM PROJECT.REQUISICAO_ITEM_PAPEL WHERE COD_ITEM_PAPEL=(SELECT COD_ITEM_PAPEL FROM deleted);
--	DELETE FROM PROJECT.ITEM_PAPEL WHERE COD_ITEM_PAPEL = (SELECT COD_ITEM_PAPEL FROM deleted);
--END
--GO

--CREATE FUNCTION PROJECT.GETDOM (@mrc Date) RETURNS VARCHAR(10)
--AS
--	BEGIN
--		DECLARE @DOM VARCHAR(10)			
--		SET @DOM = (Select DATENAME(dw, (@mrc)))
--	RETURN @DOM
--END



--CREATE TRIGGER DELETELEITOR ON PROJECT.LEITOR
--INSTEAD OF DELETE
--AS
--BEGIN
--	DELETE FROM PROJECT.REQUISICAO_ITEM_ELECT where COD_LEITOR = (SELECT COD_LEITOR FROM deleted);
--	DELETE FROM PROJECT.REQUISICAO_ITEM_PAPEL where COD_LEITOR = (SELECT COD_LEITOR FROM deleted);
--	DELETE FROM PROJECT.LEITOR WHERE COD_LEITOR = (SELECT COD_LEITOR FROM deleted);
--END
--GO
--CREATE TRIGGER DELETE_ITEM_ELECT ON PROJECT.ITEM_ELECTRONICA
--INSTEAD OF DELETE
--AS
--BEGIN
--	DELETE FROM PROJECT.REQUISICAO_ITEM_ELECT WHERE COD_ITEM_ELECT = (SELECT COD_ITEM_ELECT FROM deleted);
--	DELETE FROM PROJECT.ITEM_ELECTRONICA WHERE COD_ITEM_ELECT = (SELECT COD_ITEM_ELECT FROM deleted);
--END
--GO
	
--go
--CREATE TRIGGER update_RIP ON PROJECT.REQUISICAO_ITEM_PAPEL
--INSTEAD OF INSERT, UPDATE
--AS
--BEGIN
--	DECLARE @DATA_PREV DATE;
--	DECLARE @DATA_REAL DATE;
--	DECLARE @DATA_DIF INT;
--	DECLARE @DATA_REQ DATE;
--	DECLARE @DATA_RR INT;
--	DECLARE @DATA_RP INT;
--	DECLARE @MULTA DECIMAL(5,2);
--	DECLARE @COD_REQ INT;
--	DECLARE @COD_IP INT;
--	DECLARE @COD_L INT;
--	DECLARE @action AS char(1);
--	DECLARE @PERMISSION AS BIT;
--	DECLARE @DATA_ATUAL AS DATE;
--	SELECT  @COD_REQ=COD_REQUISICAO, @COD_IP=COD_ITEM_PAPEL, @COD_L=COD_LEITOR, @DATA_REQ=DATA_REQUISICAO, @DATA_PREV= DT_EN_PREVISTA , @DATA_REAL= DT_EN_REAL, @MULTA=MULTA FROM inserted;
	
	

--	SET @DATA_RR= (SELECT DATEDIFF(day, @DATA_REAL, @DATA_REQ))
--	SET @DATA_RP= (SELECT DATEDIFF(day, @DATA_PREV, @DATA_REQ))
--	SET @DATA_DIF=(SELECT DATEDIFF(day, @DATA_PREV, @DATA_REAL))
--	SET @PERMISSION = dbo.GETPERMISSION(@COD_IP)
--	SET @DATA_ATUAL = GETDATE()
--	SET @action = 'I'; 
--	IF EXISTS(SELECT * FROM DELETED)
--	BEGIN
--		SET @action=
--			CASE
--				WHEN EXISTS(SELECT * FROM INSERTED) THEN 'U'
--				ELSE 'D'
--			END
--	END

--	IF @PERMISSION=0
--		BEGIN 
--			RAISERROR('O item de papel selecionado n�o tem permiss�o de requisi��o!',16,1);
--			RETURN;
--		END

--	IF @DATA_REQ < @DATA_ATUAL
--		BEGIN
--			RAISERROR('Data inv�lida! Tem de inserir uma data igual ou superior � data atual!',16,1);
--			RETURN;
--		END

--	IF @DATA_RR >0
--		BEGIN
--			RAISERROR('A data da requisi��o tem que ser uma data mais recente do que a data de entrega do item!',16,1);
--			RETURN;
--		END
	
--	IF @DATA_RP >0
--		BEGIN
--			RAISERROR('A data da requisi��o tem que ser uma data mais recente do que a data prevista de entrega do item!',16,1);
--			RETURN;	
--		END
	

--	if @DATA_DIF>0
--		BEGIN
--			IF @action='I'
--				BEGIN
--					SET @MULTA= ABS(@DATA_DIF)*0.25;
--					INSERT INTO PROJECT.REQUISICAO_ITEM_PAPEL (COD_REQUISICAO, COD_ITEM_PAPEL, COD_LEITOR, DATA_REQUISICAO, DT_EN_REAL, DT_EN_PREVISTA, MULTA) VALUES (@COD_REQ, @COD_IP, @COD_L, @DATA_REQ, @DATA_REAL,@DATA_PREV, @MULTA); 
--				END
--			ELSE
--				BEGIN
--					SET @MULTA= ABS(@DATA_DIF)*0.25;
--					UPDATE PROJECT.REQUISICAO_ITEM_PAPEL SET COD_ITEM_PAPEL= @COD_IP, COD_LEITOR= @COD_L, DATA_REQUISICAO=@DATA_REQ, DT_EN_REAL=@DATA_REAL, DT_EN_PREVISTA=@DATA_PREV, MULTA=@MULTA WHERE PROJECT.REQUISICAO_ITEM_PAPEL.COD_REQUISICAO=@COD_REQ;
--				END
--		END
--	ELSE
--		BEGIN 
--			IF @action='I'
--				BEGIN
--					SET @MULTA=0;
--					INSERT INTO PROJECT.REQUISICAO_ITEM_PAPEL (COD_REQUISICAO, COD_ITEM_PAPEL, COD_LEITOR, DATA_REQUISICAO, DT_EN_REAL, DT_EN_PREVISTA, MULTA) VALUES (@COD_REQ, @COD_IP, @COD_L, @DATA_REQ, @DATA_REAL,@DATA_PREV, @MULTA);
--				END
--			ELSE
--				BEGIN
--					SET @MULTA= 0
--					UPDATE PROJECT.REQUISICAO_ITEM_PAPEL SET COD_ITEM_PAPEL= @COD_IP, COD_LEITOR= @COD_L, DATA_REQUISICAO=@DATA_REQ, DT_EN_REAL=@DATA_REAL, DT_EN_PREVISTA=@DATA_PREV, MULTA=@MULTA WHERE PROJECT.REQUISICAO_ITEM_PAPEL.COD_REQUISICAO=@COD_REQ;
--				END
--		END
--END

--CREATE FUNCTION PROJECT.GETFREEPEOPLE() 
--RETURNS TABLE
--AS
--	RETURN
--	(Select PROJECT.PESSOA.ID_PESSOA,PROJECT.PESSOA.PRIMEIRO_NOME, PROJECT.PESSOA.ULTIMO_NOME,PROJECT.PESSOA.MORADA, PROJECT.PESSOA.GENERO,  
--	PROJECT.PESSOA.DATA_NASC, PROJECT.PESSOA.TLM, PROJECT.PESSOA.CC, PROJECT.PESSOA.NIF
--FROM PROJECT.PESSOA LEFT JOIN PROJECT.LEITOR ON PROJECT.PESSOA.ID_PESSOA=PROJECT.LEITOR.ID_PESSOA 
--		LEFT JOIN PROJECT.BIBLIOTECARIO ON PROJECT.PESSOA.ID_PESSOA=PROJECT.BIBLIOTECARIO.ID_PESSOA
--		LEFT JOIN PROJECT.AUTOR ON PROJECT.PESSOA.ID_PESSOA=PROJECT.AUTOR.ID_PESSOA
--		WHERE PROJECT.LEITOR.USERNAME is NULL and PROJECT.BIBLIOTECARIO.SALARY is NULL and PROJECT.AUTOR.COD_AUTOR is Null)
--GO


--CREATE TRIGGER DELETEAUTOR ON PROJECT.AUTOR
--INSTEAD OF DELETE
--AS
--BEGIN
--	DELETE FROM PROJECT.ITEM_PAPEL_AUTOR WHERE COD_AUTOR = (SELECT COD_AUTOR FROM deleted);
--	DELETE FROM PROJECT.AUTOR WHERE COD_AUTOR = (SELECT COD_AUTOR FROM deleted);
----END
--GO
--CREATE FUNCTION PROJECT.GETPERMISSION (@book_id INT) RETURNS BIT
--AS
--	BEGIN
--		DECLARE @perm BIT			
--		SELECT @perm=PERMISSAO FROM PROJECT.ITEM_PAPEL WHERE COD_ITEM_PAPEL=@book_id
--	RETURN @perm
--END

--CREATE FUNCTION PROJECT.REQ_PESSOA (@ID INT) RETURNS INT
--AS
--	BEGIN
--		DECLARE @NR_REQ_PAPEL INT			
--		DECLARE @NR_REQ_ELECT INT
--		SELECT @NR_REQ_PAPEL = count(*) 
--		FROM PROJECT.REQUISICAO_ITEM_PAPEL
--		WHERE PROJECT.REQUISICAO_ITEM_PAPEL.COD_LEITOR=@ID

--		SELECT @NR_REQ_ELECT = count(*) 
--		FROM PROJECT.REQUISICAO_ITEM_ELECT
--		WHERE PROJECT.REQUISICAO_ITEM_ELECT.COD_LEITOR=@ID
		
--	RETURN @NR_REQ_PAPEL+@NR_REQ_ELECT
--END

--CREATE FUNCTION PROJECT.TotalMoney() RETURNS Decimal(10,2)
--AS
--	BEGIN
--		DECLARE @total Decimal(10,2)
--		SELECT @total = sum(MULTA) 
--		FROM PROJECT.REQUISICAO_ITEM_PAPEL
		
--	RETURN @TOTAL
--END

--CREATE PROC PROJECT.UPDATE_LEITOR @ID_PESSOA INT, @COD_LEITOR INT, @COD_BIB INT,@USERNAME VARCHAR(10), @PASS VARCHAR(15), 
--						  @DATA_EXP DATE, @DATA_REG DATE, @PN VARCHAR(70),@UN VARCHAR(70), @MORADA VARCHAR(50), 
--						  @GENERO CHAR(1),@DATA_NASC DATE, @TLM INT, @CC INT,  @NIF INT
--AS
--BEGIN
--	BEGIN TRANSACTION;
--	BEGIN TRY
--		UPDATE PROJECT.LEITOR SET ID_PESSOA=@ID_PESSOA, USERNAME=@USERNAME, PASS=@PASS, DATA_EXPIRO=@DATA_EXP, DATA_REGISTO=@DATA_REG, COD_BIB=@COD_BIB 
--		WHERE PROJECT.LEITOR.COD_LEITOR=@COD_LEITOR;
--		UPDATE PROJECT.PESSOA SET PRIMEIRO_NOME=@PN, ULTIMO_NOME=@UN, CC=@CC, NIF=@NIF, MORADA=@MORADA, GENERO=@GENERO, TLM=@TLM, DATA_NASC=@DATA_NASC 
--		WHERE PROJECT.PESSOA.ID_PESSOA=@ID_PESSOA;
--	END TRY

--	BEGIN CATCH
--		SELECT 
--			ERROR_NUMBER() AS ErrorNumber
--			,ERROR_SEVERITY() AS ErrorSeverity
--			,ERROR_STATE() AS ErrorState
--			,ERROR_PROCEDURE() AS ErrorProcedure
--			,ERROR_LINE() AS ErrorLine
--			,ERROR_MESSAGE() AS ErrorMessage;

--		IF @@TRANCOUNT > 0
--			ROLLBACK TRANSACTION;
--	END CATCH;
--	IF @@TRANCOUNT > 0
--		COMMIT TRANSACTION;
--END

go
CREATE PROC PROJECT.UPDATE_BIBLIOTECARIO @ID_PESSOA INT, @COD_BIBLIOTECARIO INT, @COD_BIB INT,@USERNAME VARCHAR(10), @PASS VARCHAR(15),@SALARY AS DECIMAL(10,2),
						         @PN VARCHAR(70),@UN VARCHAR(70), @MORADA VARCHAR(50), @GENERO CHAR(1),@DATA_NASC DATE, @TLM INT, @CC INT,  @NIF INT
AS
BEGIN
	BEGIN TRANSACTION;
	BEGIN TRY
		UPDATE PROJECT.BIBLIOTECARIO SET ID_PESSOA=@ID_PESSOA, USERNAME=@USERNAME, PASS=@PASS, COD_BIB=@COD_BIB, SALARY=@SALARY 
		WHERE PROJECT.BIBLIOTECARIO.COD_BIBLIOTECARIO=@COD_BIBLIOTECARIO;
		UPDATE PROJECT.PESSOA SET PRIMEIRO_NOME=@PN, ULTIMO_NOME=@UN, CC=@CC, NIF=@NIF, MORADA=@MORADA, GENERO=@GENERO, TLM=@TLM, DATA_NASC=@DATA_NASC 
		WHERE PROJECT.PESSOA.ID_PESSOA=@ID_PESSOA;
	END TRY

	BEGIN CATCH
		SELECT 
			ERROR_NUMBER() AS ErrorNumber
			,ERROR_SEVERITY() AS ErrorSeverity
			,ERROR_STATE() AS ErrorState
			,ERROR_PROCEDURE() AS ErrorProcedure
			,ERROR_LINE() AS ErrorLine
			,ERROR_MESSAGE() AS ErrorMessage;

		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
	END CATCH;
	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



--CREATE PROC UPDATE_AUTOR @ID_PESSOA INT, @COD_AUTOR INT,@PN VARCHAR(70),@UN VARCHAR(70), @MORADA VARCHAR(50),
--						 @GENERO CHAR(1),@DATA_NASC DATE, @TLM INT, @CC INT,  @NIF INT
--AS
--BEGIN
--	BEGIN TRANSACTION;
--	BEGIN TRY
--		UPDATE PROJECT.AUTOR SET ID_PESSOA=@ID_PESSOA
--		WHERE PROJECT.AUTOR.COD_AUTOR=@COD_AUTOR;
--		UPDATE PROJECT.PESSOA SET PRIMEIRO_NOME=@PN, ULTIMO_NOME=@UN, CC=@CC, NIF=@NIF, MORADA=@MORADA, GENERO=@GENERO, TLM=@TLM, DATA_NASC=@DATA_NASC 
--		WHERE PROJECT.PESSOA.ID_PESSOA=@ID_PESSOA;
--	END TRY

--	BEGIN CATCH
--		SELECT 
--			ERROR_NUMBER() AS ErrorNumber
--			,ERROR_SEVERITY() AS ErrorSeverity
--			,ERROR_STATE() AS ErrorState
--			,ERROR_PROCEDURE() AS ErrorProcedure
--			,ERROR_LINE() AS ErrorLine
--			,ERROR_MESSAGE() AS ErrorMessage;

--		IF @@TRANCOUNT > 0
--			ROLLBACK TRANSACTION;
--	END CATCH;
--	IF @@TRANCOUNT > 0
--		COMMIT TRANSACTION;
--END


--go
--CREATE PROC INSERT_PESSOA @ID_PESSOA INT, @PN VARCHAR(70),@UN VARCHAR(70), @MORADA VARCHAR(50), @GENERO CHAR(1),@DATA_NASC DATE, @TLM INT, @CC INT,  @NIF INT
--WITH ENCRYPTION
--AS
--BEGIN
--	BEGIN TRANSACTION;
--	BEGIN TRY
--		INSERT INTO PROJECT.PESSOA VALUES(@ID_PESSOA, @PN, @UN, @MORADA, @GENERO, @DATA_NASC, @TLM, @CC,@NIF);
--	END TRY

--	BEGIN CATCH
--		SELECT 
--			ERROR_NUMBER() AS ErrorNumber
--			,ERROR_SEVERITY() AS ErrorSeverity
--			,ERROR_STATE() AS ErrorState
--			,ERROR_PROCEDURE() AS ErrorProcedure
--			,ERROR_LINE() AS ErrorLine
--			,ERROR_MESSAGE() AS ErrorMessage;

--		IF @@TRANCOUNT > 0
--			ROLLBACK TRANSACTION;
--	END CATCH;
--	IF @@TRANCOUNT > 0
--		COMMIT TRANSACTION;
--END

--go
--CREATE PROC INSERT_AUTOR @COD_AUTOR INT,@ID_PESSOA INT
--WITH ENCRYPTION
--AS
--BEGIN
--	BEGIN TRANSACTION;
--	BEGIN TRY
--		INSERT INTO PROJECT.AUTOR VALUES(@COD_AUTOR, @ID_PESSOA);
--	END TRY

--	BEGIN CATCH
--		SELECT 
--			ERROR_NUMBER() AS ErrorNumber
--			,ERROR_SEVERITY() AS ErrorSeverity
--			,ERROR_STATE() AS ErrorState
--			,ERROR_PROCEDURE() AS ErrorProcedure
--			,ERROR_LINE() AS ErrorLine
--			,ERROR_MESSAGE() AS ErrorMessage;

--		IF @@TRANCOUNT > 0
--			ROLLBACK TRANSACTION;
--	END CATCH;
--	IF @@TRANCOUNT > 0
--		COMMIT TRANSACTION;
--END


--go
--CREATE PROC INSERT_LEITOR @COD_LEITOR INT,@ID_PESSOA INT,@USERNAME VARCHAR(10), @PASS VARCHAR(15), 
--						  @DATA_EXP DATE, @DATA_REG DATE, @COD_BIB INT
--WITH ENCRYPTION
--AS
--BEGIN
--	BEGIN TRANSACTION;
--	BEGIN TRY
--		INSERT INTO PROJECT.LEITOR VALUES( @COD_LEITOR,@ID_PESSOA, @USERNAME , @PASS, @DATA_EXP , @DATA_REG, @COD_BIB);
--	END TRY

--	BEGIN CATCH
--		SELECT 
--			ERROR_NUMBER() AS ErrorNumber
--			,ERROR_SEVERITY() AS ErrorSeverity
--			,ERROR_STATE() AS ErrorState
--			,ERROR_PROCEDURE() AS ErrorProcedure
--			,ERROR_LINE() AS ErrorLine
--			,ERROR_MESSAGE() AS ErrorMessage;

--		IF @@TRANCOUNT > 0
--			ROLLBACK TRANSACTION;
--	END CATCH;
--	IF @@TRANCOUNT > 0
--		COMMIT TRANSACTION;
--END


--go
--CREATE PROC INSERT_BIBLIOTECARIO @COD_BIBLIOTECARIO INT,@ID_PESSOA INT, @COD_BIB INT,@SALARY AS DECIMAL(10,2), @USERNAME VARCHAR(10), @PASS VARCHAR(15)
--WITH ENCRYPTION
--AS
--BEGIN
--	BEGIN TRANSACTION;
--	BEGIN TRY
--		INSERT INTO PROJECT.BIBLIOTECARIO VALUES( @COD_BIBLIOTECARIO, @ID_PESSOA , @COD_BIB,@SALARY,@USERNAME, @PASS);
--	END TRY

--	BEGIN CATCH
--		SELECT 
--			ERROR_NUMBER() AS ErrorNumber
--			,ERROR_SEVERITY() AS ErrorSeverity
--			,ERROR_STATE() AS ErrorState
--			,ERROR_PROCEDURE() AS ErrorProcedure
--			,ERROR_LINE() AS ErrorLine
--			,ERROR_MESSAGE() AS ErrorMessage;

--		IF @@TRANCOUNT > 0
--			ROLLBACK TRANSACTION;
--	END CATCH;
--	IF @@TRANCOUNT > 0
--		COMMIT TRANSACTION;
--END
--Os stored procedures foram todos implementados com encripta��o, contudo foi retirado para a gera��o do script.
--A nossa inten��o � que os sp's contenham encripta��o como foi mencionado no relat�rio. 









