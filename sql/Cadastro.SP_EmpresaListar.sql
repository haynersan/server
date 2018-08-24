USE [SHIFT]
GO

DECLARE @RC int
DECLARE @Nome varchar(50)

-- TODO: Defina valores de parâmetros aqui.

EXECUTE @RC = [Cadastro].[SP_EmpresaListar] 
   @Nome
GO

