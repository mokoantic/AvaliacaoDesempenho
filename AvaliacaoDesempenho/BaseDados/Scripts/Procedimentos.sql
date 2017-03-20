--------------------------------------------
--          Tabela Grupo      
-------------------------------------------------

-- Select Dynamic
IF OBJECT_ID(N'[dbo].[Proc_SelectGruposDynamic]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_SelectGruposDynamic]
GO

CREATE PROCEDURE [dbo].[Proc_SelectGruposDynamic]
	@WhereCondition nvarchar(500),
	@OrderByExpression nvarchar(250) = NULL
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

DECLARE @SQL nvarchar(3250)

SET @SQL = '
SELECT
	[Gru_Id],
	[Gru_Descricao],
	[Gru_Ativo]
FROM
	[dbo].[Grupo]
WHERE
	' + @WhereCondition

IF @OrderByExpression IS NOT NULL AND LEN(@OrderByExpression) > 0
BEGIN
	SET @SQL = @SQL + '
ORDER BY
	' + @OrderByExpression
END

EXEC sp_executesql @SQL

GO

-- Insert
IF OBJECT_ID(N'[dbo].[Proc_InsertGrupo]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_InsertGrupo]
GO

CREATE PROCEDURE [dbo].[Proc_InsertGrupo]
	@Gru_Descricao nvarchar(300),
	@Gru_Ativo bit,
	@Gru_Id int OUTPUT
AS

SET NOCOUNT ON

INSERT INTO [dbo].[Grupo] (
	[Gru_Descricao],
	[Gru_Ativo]
) VALUES (
	@Gru_Descricao,
	@Gru_Ativo
)

SET @Gru_Id = SCOPE_IDENTITY()

GO

-- Update
IF OBJECT_ID(N'[dbo].[Proc_UpdateGrupo]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_UpdateGrupo]
GO

CREATE PROCEDURE [dbo].[Proc_UpdateGrupo]
	@Gru_Id int,
	@Gru_Descricao nvarchar(300),
	@Gru_Ativo bit
AS

SET NOCOUNT ON

UPDATE [dbo].[Grupo] SET
	[Gru_Descricao] = @Gru_Descricao,
	[Gru_Ativo] = @Gru_Ativo
WHERE
	[Gru_Id] = @Gru_Id

GO


-- Delete by Index
IF OBJECT_ID(N'[dbo].[Proc_DeleteGruposByGru_Id]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_DeleteGruposByGru_Id]
GO

CREATE PROCEDURE [dbo].[Proc_DeleteGruposByGru_Id]
	@Gru_Id int
AS

SET NOCOUNT ON

DELETE FROM [dbo].[Grupo]
WHERE
	[Gru_Id] = @Gru_Id

GO

--------------------------------------------
--          Tabela Utilizador      
-------------------------------------------------

-- Select Dynamic
IF OBJECT_ID(N'[dbo].[Proc_SelectUtilizadorsDynamic]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_SelectUtilizadorsDynamic]
GO

CREATE PROCEDURE [dbo].[Proc_SelectUtilizadorsDynamic]
	@WhereCondition nvarchar(500),
	@OrderByExpression nvarchar(250) = NULL
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

DECLARE @SQL nvarchar(3250)

SET @SQL = '
SELECT
	Utilizador.Uti_ID,
	Utilizador.Uti_Gru_Id,
	Grupo.Gru_Descricao,
	Utilizador.Uti_Login,
	Utilizador.Uti_Password,
	Utilizador.Uti_PrimeiroNome,
	Utilizador.Uti_UltimoNome,
	Utilizador.Uti_PastaFicheiro, 
    Utilizador.Uti_NomeFicheiro,
	Utilizador.Uti_Email,
	Utilizador.Uti_EmailConfirmado,
	Utilizador.Uti_Activo
FROM
	Grupo
	INNER JOIN Utilizador
	ON Grupo.Gru_Id = Utilizador.Uti_Gru_Id
WHERE
	' + @WhereCondition

IF @OrderByExpression IS NOT NULL AND LEN(@OrderByExpression) > 0
BEGIN
	SET @SQL = @SQL + '
ORDER BY
	' + @OrderByExpression
END

EXEC sp_executesql @SQL

GO

-- Insert
IF OBJECT_ID(N'[dbo].[Proc_InsertUtilizador]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_InsertUtilizador]
GO

CREATE PROCEDURE [dbo].[Proc_InsertUtilizador]
	@Uti_Gru_Id int,
	@Uti_Login varchar(50),
	@Uti_Password varchar(300),
	@Uti_PrimeiroNome varchar(50),
	@Uti_UltimoNome varchar(50),
	@Uti_PastaFicheiro varchar(250),
	@Uti_NomeFicheiro varchar(250),
	@Uti_Email varchar(250),
	@Uti_EmailConfirmado bit,
	@Uti_Activo bit,
	@Uti_ID int OUTPUT
AS

SET NOCOUNT ON

INSERT INTO [dbo].[Utilizador] (
	[Uti_Gru_Id],
	[Uti_Login],
	[Uti_Password],
	[Uti_PrimeiroNome],
	[Uti_UltimoNome],
	[Uti_PastaFicheiro],
	[Uti_NomeFicheiro],
	[Uti_Email],
	[Uti_EmailConfirmado],
	[Uti_Activo]
) VALUES (
	@Uti_Gru_Id,
	@Uti_Login,
	@Uti_Password,
	@Uti_PrimeiroNome,
	@Uti_UltimoNome,
	@Uti_PastaFicheiro,
	@Uti_NomeFicheiro,
	@Uti_Email,
	@Uti_EmailConfirmado,
	@Uti_Activo
)

SET @Uti_ID = SCOPE_IDENTITY()

GO

-- Update
IF OBJECT_ID(N'[dbo].[Proc_UpdateUtilizador]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_UpdateUtilizador]
GO

CREATE PROCEDURE [dbo].[Proc_UpdateUtilizador]
	@Uti_ID int,
	@Uti_Gru_Id int,
	@Uti_Login varchar(50),
	@Uti_Password varchar(300),
	@Uti_PrimeiroNome varchar(50),
	@Uti_UltimoNome varchar(50),
	@Uti_PastaFicheiro varchar(250),
	@Uti_NomeFicheiro varchar(250),
	@Uti_Email varchar(250),
	@Uti_EmailConfirmado bit,
	@Uti_Activo bit
AS

SET NOCOUNT ON

UPDATE [dbo].[Utilizador] SET
	[Uti_Gru_Id] = @Uti_Gru_Id,
	[Uti_Login] = @Uti_Login,
	[Uti_Password] = @Uti_Password,
	[Uti_PrimeiroNome] = @Uti_PrimeiroNome,
	[Uti_UltimoNome] = @Uti_UltimoNome,
	[Uti_PastaFicheiro] = @Uti_PastaFicheiro,
	[Uti_NomeFicheiro] = @Uti_NomeFicheiro,
	[Uti_Email] = @Uti_Email,
	[Uti_EmailConfirmado] = @Uti_EmailConfirmado,
	[Uti_Activo] = @Uti_Activo
WHERE
	[Uti_ID] = @Uti_ID

GO

-- Delete by Index
IF OBJECT_ID(N'[dbo].[Proc_DeleteUtilizadorsByUti_ID]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_DeleteUtilizadorsByUti_ID]
GO

CREATE PROCEDURE [dbo].[Proc_DeleteUtilizadorsByUti_ID]
	@Uti_ID int
AS

SET NOCOUNT ON

DELETE FROM [dbo].[Utilizador]
WHERE
	[Uti_ID] = @Uti_ID

GO

--------------------------------------------
--          Tabela Quadrante      
-------------------------------------------------

-- Select Dynamic

IF OBJECT_ID(N'[dbo].[Proc_SelectQuadrantesDynamic]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_SelectQuadrantesDynamic]
GO

CREATE PROCEDURE [dbo].[Proc_SelectQuadrantesDynamic]
	@WhereCondition nvarchar(500),
	@OrderByExpression nvarchar(250) = NULL
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

DECLARE @SQL nvarchar(3250)

SET @SQL = '
SELECT
	[Qua_Id],
	[Qua_Descricao],
	[Qua_Peso]
FROM
	[dbo].[Quadrante]
WHERE
	' + @WhereCondition

IF @OrderByExpression IS NOT NULL AND LEN(@OrderByExpression) > 0
BEGIN
	SET @SQL = @SQL + '
ORDER BY
	' + @OrderByExpression
END

EXEC sp_executesql @SQL

GO

-- Insert

IF OBJECT_ID(N'[dbo].[Proc_InsertQuadrante]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_InsertQuadrante]
GO

CREATE PROCEDURE [dbo].[Proc_InsertQuadrante]
	@Qua_Descricao nvarchar(300),
	@Qua_Peso int,
	@Qua_Id int OUTPUT
AS

SET NOCOUNT ON

INSERT INTO [dbo].[Quadrante] (
	[Qua_Descricao],
	[Qua_Peso]
) VALUES (
	@Qua_Descricao,
	@Qua_Peso
)

SET @Qua_Id = SCOPE_IDENTITY()

GO

-- Update

IF OBJECT_ID(N'[dbo].[Proc_UpdateQuadrante]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_UpdateQuadrante]
GO

CREATE PROCEDURE [dbo].[Proc_UpdateQuadrante]
	@Qua_Id int,
	@Qua_Descricao nvarchar(300),
	@Qua_Peso int
AS

SET NOCOUNT ON

UPDATE [dbo].[Quadrante] SET
	[Qua_Descricao] = @Qua_Descricao,
	[Qua_Peso] = @Qua_Peso
WHERE
	[Qua_Id] = @Qua_Id

GO

-- Delete by Index

IF OBJECT_ID(N'[dbo].[Proc_DeleteQuadrantesByQua_Id]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_DeleteQuadrantesByQua_Id]
GO

CREATE PROCEDURE [dbo].[Proc_DeleteQuadrantesByQua_Id]
	@Qua_Id int
AS

SET NOCOUNT ON

DELETE FROM [dbo].[Quadrante]
WHERE
	[Qua_Id] = @Qua_Id

GO

--------------------------------------------
--          Tabela Classificacao      
-------------------------------------------------

-- Select Dynamic

IF OBJECT_ID(N'[dbo].[Proc_SelectClassificacaosDynamic]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_SelectClassificacaosDynamic]
GO

CREATE PROCEDURE [dbo].[Proc_SelectClassificacaosDynamic]
	@WhereCondition nvarchar(500),
	@OrderByExpression nvarchar(250) = NULL
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

DECLARE @SQL nvarchar(3250)

SET @SQL = '
SELECT
	Classificacao.Cla_Id,
	Classificacao.Cla_Qua_Id,
	Quadrante.Qua_Descricao,
	Classificacao.Cla_Descricao,
	Classificacao.Cla_Cotacao
FROM
	Classificacao LEFT JOIN
    Quadrante ON Classificacao.Cla_Qua_Id = Quadrante.Qua_Id
WHERE
	' + @WhereCondition

IF @OrderByExpression IS NOT NULL AND LEN(@OrderByExpression) > 0
BEGIN
	SET @SQL = @SQL + '
ORDER BY
	' + @OrderByExpression
END

EXEC sp_executesql @SQL

GO

-- Insert

IF OBJECT_ID(N'[dbo].[Proc_InsertClassificacao]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_InsertClassificacao]
GO

CREATE PROCEDURE [dbo].[Proc_InsertClassificacao]
	@Cla_Qua_Id int,
	@Cla_Descricao nvarchar(300),
	@Cla_Cotacao int,
	@Cla_Id int OUTPUT
AS

SET NOCOUNT ON

INSERT INTO [dbo].[Classificacao] (
	[Cla_Qua_Id],
	[Cla_Descricao],
	[Cla_Cotacao]
) VALUES (
	@Cla_Qua_Id,
	@Cla_Descricao,
	@Cla_Cotacao
)

SET @Cla_Id = SCOPE_IDENTITY()

GO

-- Update

IF OBJECT_ID(N'[dbo].[Proc_UpdateClassificacao]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_UpdateClassificacao]
GO

CREATE PROCEDURE [dbo].[Proc_UpdateClassificacao]
	@Cla_Id int,
	@Cla_Qua_Id int,
	@Cla_Descricao nvarchar(300),
	@Cla_Cotacao int
AS

SET NOCOUNT ON

UPDATE [dbo].[Classificacao] SET
	[Cla_Qua_Id] = @Cla_Qua_Id,
	[Cla_Descricao] = @Cla_Descricao,
	[Cla_Cotacao] = @Cla_Cotacao
WHERE
	[Cla_Id] = @Cla_Id

GO

-- Delete by Index

IF OBJECT_ID(N'[dbo].[Proc_DeleteClassificacaosByCla_Id]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_DeleteClassificacaosByCla_Id]
GO

CREATE PROCEDURE [dbo].[Proc_DeleteClassificacaosByCla_Id]
	@Cla_Id int
AS

SET NOCOUNT ON

DELETE FROM [dbo].[Classificacao]
WHERE
	[Cla_Id] = @Cla_Id
GO

--------------------------------------------
--          Tabela Questao      
-------------------------------------------------

-- Select Dynamic

IF OBJECT_ID(N'[dbo].[Proc_SelectQuestaosDynamic]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_SelectQuestaosDynamic]
GO

CREATE PROCEDURE [dbo].[Proc_SelectQuestaosDynamic]
	@WhereCondition nvarchar(500),
	@OrderByExpression nvarchar(250) = NULL
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

DECLARE @SQL nvarchar(3250)

SET @SQL = '
SELECT
	Questao.Que_Id,
	Questao.Que_Qua_Id,
	Quadrante.Qua_Descricao,
	Questao.Que_Descricao,
	Questao.Que_Peso
FROM
	Questao INNER JOIN
    Quadrante ON Questao.Que_Qua_Id = Quadrante.Qua_Id
WHERE
	' + @WhereCondition

IF @OrderByExpression IS NOT NULL AND LEN(@OrderByExpression) > 0
BEGIN
	SET @SQL = @SQL + '
ORDER BY
	' + @OrderByExpression
END

EXEC sp_executesql @SQL

GO

-- Insert

IF OBJECT_ID(N'[dbo].[Proc_InsertQuestao]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_InsertQuestao]
GO

CREATE PROCEDURE [dbo].[Proc_InsertQuestao]
	@Que_Qua_Id int,
	@Que_Descricao nvarchar(300),
	@Que_Peso int,
	@Que_Id int OUTPUT
AS

SET NOCOUNT ON

INSERT INTO [dbo].[Questao] (
	[Que_Qua_Id],
	[Que_Descricao],
	[Que_Peso]
) VALUES (
	@Que_Qua_Id,
	@Que_Descricao,
	@Que_Peso
)

SET @Que_Id = SCOPE_IDENTITY()

GO

-- Update

IF OBJECT_ID(N'[dbo].[Proc_UpdateQuestao]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_UpdateQuestao]
GO

CREATE PROCEDURE [dbo].[Proc_UpdateQuestao]
	@Que_Id int,
	@Que_Qua_Id int,
	@Que_Descricao nvarchar(300),
	@Que_Peso int
AS

SET NOCOUNT ON

UPDATE [dbo].[Questao] SET
	[Que_Qua_Id] = @Que_Qua_Id,
	[Que_Descricao] = @Que_Descricao,
	[Que_Peso] = @Que_Peso
WHERE
	[Que_Id] = @Que_Id

GO

-- Delete by Index

IF OBJECT_ID(N'[dbo].[Proc_DeleteQuestaosByQue_Id]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_DeleteQuestaosByQue_Id]
GO

CREATE PROCEDURE [dbo].[Proc_DeleteQuestaosByQue_Id]
	@Que_Id int
AS

SET NOCOUNT ON

DELETE FROM [dbo].[Questao]
WHERE
	[Que_Id] = @Que_Id

GO

--------------------------------------------
--          Tabela Avaliacao      
-------------------------------------------------

-- Select Dynamic

IF OBJECT_ID(N'[dbo].[Proc_SelectAvaliacaosDynamic]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_SelectAvaliacaosDynamic]
GO

CREATE PROCEDURE [dbo].[Proc_SelectAvaliacaosDynamic]
	@WhereCondition nvarchar(500),
	@OrderByExpression nvarchar(250) = NULL
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

DECLARE @SQL nvarchar(3250)

SET @SQL = '
SELECT
	[Ava_Id],
	[Ava_Uti_Id],
	[Ava_Peso_Quadrante_Um],
	[Ava_Peso_Quadrante_Dois],
	[Ava_Peso_Quadrante_Tres],
	[Ava_Peso_Quadrante_Quatro],
	[Ava_Classificacao_Um],
	[Ava_Classificacao_Dois],
	[Ava_Classificacao_Tres],
	[Ava_Classificacao_Quatro],
	[Ava_Classificacao_Final]
FROM
	[dbo].[Avaliacao]
WHERE
	' + @WhereCondition

IF @OrderByExpression IS NOT NULL AND LEN(@OrderByExpression) > 0
BEGIN
	SET @SQL = @SQL + '
ORDER BY
	' + @OrderByExpression
END

EXEC sp_executesql @SQL

GO

-- Insert

IF OBJECT_ID(N'[dbo].[Proc_InsertAvaliacao]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_InsertAvaliacao]
GO

CREATE PROCEDURE [dbo].[Proc_InsertAvaliacao]
	@Ava_Uti_Id int,
	@Ava_Peso_Quadrante_Um int,
	@Ava_Peso_Quadrante_Dois int,
	@Ava_Peso_Quadrante_Tres int,
	@Ava_Peso_Quadrante_Quatro int,
	@Ava_Classificacao_Um int,
	@Ava_Classificacao_Dois int,
	@Ava_Classificacao_Tres int,
	@Ava_Classificacao_Quatro int,
	@Ava_Classificacao_Final int,
	@Ava_Id int OUTPUT
AS

SET NOCOUNT ON

INSERT INTO [dbo].[Avaliacao] (
	[Ava_Uti_Id],
	[Ava_Peso_Quadrante_Um],
	[Ava_Peso_Quadrante_Dois],
	[Ava_Peso_Quadrante_Tres],
	[Ava_Peso_Quadrante_Quatro],
	[Ava_Classificacao_Um],
	[Ava_Classificacao_Dois],
	[Ava_Classificacao_Tres],
	[Ava_Classificacao_Quatro],
	[Ava_Classificacao_Final]
) VALUES (
	@Ava_Uti_Id,
	@Ava_Peso_Quadrante_Um,
	@Ava_Peso_Quadrante_Dois,
	@Ava_Peso_Quadrante_Tres,
	@Ava_Peso_Quadrante_Quatro,
	@Ava_Classificacao_Um,
	@Ava_Classificacao_Dois,
	@Ava_Classificacao_Tres,
	@Ava_Classificacao_Quatro,
	@Ava_Classificacao_Final
)

SET @Ava_Id = SCOPE_IDENTITY()

GO

-- Update

IF OBJECT_ID(N'[dbo].[Proc_UpdateAvaliacao]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_UpdateAvaliacao]
GO

CREATE PROCEDURE [dbo].[Proc_UpdateAvaliacao]
	@Ava_Id int,
	@Ava_Uti_Id int,
	@Ava_Peso_Quadrante_Um int,
	@Ava_Peso_Quadrante_Dois int,
	@Ava_Peso_Quadrante_Tres int,
	@Ava_Peso_Quadrante_Quatro int,
	@Ava_Classificacao_Um int,
	@Ava_Classificacao_Dois int,
	@Ava_Classificacao_Tres int,
	@Ava_Classificacao_Quatro int,
	@Ava_Classificacao_Final int
AS

SET NOCOUNT ON

UPDATE [dbo].[Avaliacao] SET
	[Ava_Uti_Id] = @Ava_Uti_Id,
	[Ava_Peso_Quadrante_Um] = @Ava_Peso_Quadrante_Um,
	[Ava_Peso_Quadrante_Dois] = @Ava_Peso_Quadrante_Dois,
	[Ava_Peso_Quadrante_Tres] = @Ava_Peso_Quadrante_Tres,
	[Ava_Peso_Quadrante_Quatro] = @Ava_Peso_Quadrante_Quatro,
	[Ava_Classificacao_Um] = @Ava_Classificacao_Um,
	[Ava_Classificacao_Dois] = @Ava_Classificacao_Dois,
	[Ava_Classificacao_Tres] = @Ava_Classificacao_Tres,
	[Ava_Classificacao_Quatro] = @Ava_Classificacao_Quatro,
	[Ava_Classificacao_Final] = @Ava_Classificacao_Final
WHERE
	[Ava_Id] = @Ava_Id

GO

-- Delete by Index

IF OBJECT_ID(N'[dbo].[Proc_DeleteAvaliacaosByAva_Id]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_DeleteAvaliacaosByAva_Id]
GO

CREATE PROCEDURE [dbo].[Proc_DeleteAvaliacaosByAva_Id]
	@Ava_Id int
AS

SET NOCOUNT ON

DELETE FROM [dbo].[Avaliacao]
WHERE
	[Ava_Id] = @Ava_Id

GO

--------------------------------------------
--          Tabela LinhaAvaliacao      
-------------------------------------------------

-- Select Dynamic

IF OBJECT_ID(N'[dbo].[Proc_SelectLinhaAvaliacaosDynamic]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_SelectLinhaAvaliacaosDynamic]
GO

CREATE PROCEDURE [dbo].[Proc_SelectLinhaAvaliacaosDynamic]
	@WhereCondition nvarchar(500),
	@OrderByExpression nvarchar(250) = NULL
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

DECLARE @SQL nvarchar(3250)

SET @SQL = '
SELECT
	LinhaAvaliacao.LAva_Id,
	LinhaAvaliacao.LAva_Ava_Id,
	LinhaAvaliacao.LAva_Qua_Id,
	Quadrante.Qua_Descricao,
	LinhaAvaliacao.LAva_PesoQuadrante,
	LinhaAvaliacao.LAva_Questao, 
    LinhaAvaliacao.LAva_PesoQuestao,
	LinhaAvaliacao.LAva_Classificacao,
	LinhaAvaliacao.LAva_Pontos
FROM
	LinhaAvaliacao LEFT JOIN
    Quadrante ON LinhaAvaliacao.LAva_Qua_Id = Quadrante.Qua_Id
WHERE
	' + @WhereCondition

IF @OrderByExpression IS NOT NULL AND LEN(@OrderByExpression) > 0
BEGIN
	SET @SQL = @SQL + '
ORDER BY
	' + @OrderByExpression
END

EXEC sp_executesql @SQL

GO

-- Insert

IF OBJECT_ID(N'[dbo].[Proc_InsertLinhaAvaliacao]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_InsertLinhaAvaliacao]
GO

CREATE PROCEDURE [dbo].[Proc_InsertLinhaAvaliacao]
	@LAva_Ava_Id int,
	@LAva_Qua_Id int,
	@LAva_PesoQuadrante int,
	@LAva_Questao nvarchar(300),
	@LAva_PesoQuestao int,
	@LAva_Classificacao int,
	@LAva_Pontos int,
	@LAva_Id int OUTPUT
AS

SET NOCOUNT ON

INSERT INTO [dbo].[LinhaAvaliacao] (
	[LAva_Ava_Id],
	[LAva_Qua_Id],
	[LAva_PesoQuadrante],
	[LAva_Questao],
	[LAva_PesoQuestao],
	[LAva_Classificacao],
	[LAva_Pontos]
) VALUES (
	@LAva_Ava_Id,
	@LAva_Qua_Id,
	@LAva_PesoQuadrante,
	@LAva_Questao,
	@LAva_PesoQuestao,
	@LAva_Classificacao,
	@LAva_Pontos
)

SET @LAva_Id = SCOPE_IDENTITY()

GO

-- Update

IF OBJECT_ID(N'[dbo].[Proc_UpdateLinhaAvaliacao]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_UpdateLinhaAvaliacao]
GO

CREATE PROCEDURE [dbo].[Proc_UpdateLinhaAvaliacao]
	@LAva_Id int,
	@LAva_Ava_Id int,
	@LAva_Qua_Id int,
	@LAva_PesoQuadrante int,
	@LAva_Questao nvarchar(300),
	@LAva_PesoQuestao int,
	@LAva_Classificacao int,
	@LAva_Pontos int
AS

SET NOCOUNT ON

UPDATE [dbo].[LinhaAvaliacao] SET
	[LAva_Ava_Id] = @LAva_Ava_Id,
	[LAva_Qua_Id] = @LAva_Qua_Id,
	[LAva_PesoQuadrante] = @LAva_PesoQuadrante,
	[LAva_Questao] = @LAva_Questao,
	[LAva_PesoQuestao] = @LAva_PesoQuestao,
	[LAva_Classificacao] = @LAva_Classificacao,
	[LAva_Pontos] = @LAva_Pontos
WHERE
	[LAva_Id] = @LAva_Id

GO

-- Delete by Index

IF OBJECT_ID(N'[dbo].[Proc_DeleteLinhaAvaliacaosByLAva_Id]') IS NOT NULL
	DROP PROCEDURE [dbo].[Proc_DeleteLinhaAvaliacaosByLAva_Id]
GO

CREATE PROCEDURE [dbo].[Proc_DeleteLinhaAvaliacaosByLAva_Id]
	@LAva_Id int
AS

SET NOCOUNT ON

DELETE FROM [dbo].[LinhaAvaliacao]
WHERE
	[LAva_Id] = @LAva_Id

GO