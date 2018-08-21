USE [SHIFT]
GO

/****** Object:  StoredProcedure [Cadastro].[SP_EmpresaListar]    Script Date: 30/07/2018 11:43:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [Cadastro].[SP_EmpresaListar]


@NumeroDaPagina		AS INT,

@TamanhoDaPagina	AS INT

AS
BEGIN

	SELECT

		EMP.CodEmpresa,

		EMP.Nome,

		EMP.Cnpj,

		EMP.IdSituacao,

		SIT.DescSituacao

	FROM [Cadastro].[Empresas]			AS EMP WITH(NOLOCK)

	INNER JOIN [Estatico].[Situacao]	AS SIT WITH(NOLOCK) ON EMP.IdSituacao = SIT.IdSituacao

	WHERE
		EMP.Excluido = 0 --FALSE;

	ORDER BY
		EMP.CodEmpresa

	OFFSET (@NumeroDaPagina - 1) * @TamanhoDaPagina ROWS
	FETCH NEXT @TamanhoDaPagina ROWS ONLY

END;


--Fonte: https://stackoverflow.com/questions/40800512/how-to-do-paging-in-related-tables-and-map-to-dapper-query
GO

