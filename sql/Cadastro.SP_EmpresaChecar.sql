USE [SHIFT]
GO

DECLARE @RC int
DECLARE @Acao int
DECLARE @CodEmpresa varchar(4)
DECLARE @Nome varchar(50)
DECLARE @Cnpj varchar(14)

-- TODO: Defina valores de parâmetros aqui.

EXECUTE @RC = [Cadastro].[SP_EmpresaChecar] 
   @Acao
  ,@CodEmpresa
  ,@Nome
  ,@Cnpj
GO

