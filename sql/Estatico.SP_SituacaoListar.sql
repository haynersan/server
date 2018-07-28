USE [SHIFT]
GO

/****** Object:  StoredProcedure [Estatico].[SP_SituacaoListar]    Script Date: 28/07/2018 11:45:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [Estatico].[SP_SituacaoListar]

AS
BEGIN

	SELECT

		IdSituacao,

		DescSituacao

	FROM Estatico.Situacao
	
	WHERE
		Excluido = 0

	ORDER BY

		DescSituacao;

END;



GO

