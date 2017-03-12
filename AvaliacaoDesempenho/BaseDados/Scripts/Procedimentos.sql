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