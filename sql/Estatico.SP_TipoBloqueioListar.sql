USE [SHIFT]
GO

/****** Object:  StoredProcedure [Estatico].[SP_TipoBloqueioListar]    Script Date: 27/07/2018 19:58:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [Estatico].[SP_TipoBloqueioListar]

AS
BEGIN

	SELECT

		Codigo,

		Tipo

	FROM Estatico.TipoBloqueio
	
	WHERE
		Excluido = 0;

END;
GO

