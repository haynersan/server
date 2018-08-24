USE [SHIFT]
GO

DECLARE @RC int
DECLARE @Id uniqueidentifier

-- TODO: Defina valores de parâmetros aqui.

EXECUTE @RC = [Usuario].[SP_UsuarioIdEhValido] 
   @Id
GO

