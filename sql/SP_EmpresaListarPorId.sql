USE [SHIFT]
GO

/****** Object:  StoredProcedure [Cadastro].[SP_EmpresaListarPorId]    Script Date: 22/08/2018 15:02:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [Cadastro].[SP_EmpresaListarPorId]

@CodEmpresa AS VARCHAR(04)

AS
BEGIN

	SELECT

		EMP.CodEmpresa,

		EMP.Nome,

		EMP.Cnpj,

		EMP.IdSituacao,

		SIT.DescSituacao

	FROM [Cadastro].[Empresas]			AS EMP WITH(NOLOCK)

	INNER JOIN [Estatico].[Situacoes]	AS SIT WITH(NOLOCK) ON EMP.IdSituacao = SIT.IdSituacao

	WHERE

		EMP.CodEmpresa	= @CodEmpresa

	AND EMP.Excluido	= 0; --FALSE;

END;
GO

