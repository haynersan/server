USE [SHIFT]
GO

DECLARE @RC int
DECLARE @Acao int
DECLARE @Id uniqueidentifier
DECLARE @UserName varchar(50)
DECLARE @Matricula varchar(6)

-- TODO: Defina valores de parâmetros aqui.

EXECUTE @RC = [Usuario].[SP_UsuarioChecar] 
   @Acao
  ,@Id
  ,@UserName
  ,@Matricula
GO

