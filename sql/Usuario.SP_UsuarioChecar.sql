USE [SHIFT]
GO

/****** Object:  StoredProcedure [Usuario].[SP_ChecarUsuario]    Script Date: 19/07/2018 12:27:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





---------------------------------------------------------------------------------------------------------------------------------
-- Autor: Wellington Hayner
-- Data: 19/07/2018
-- Objetivo: Checar se a matrícula cadastrada já existe
---------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [Usuario].[SP_ChecarUsuario]


@Acao		AS INT,

@Id			AS UNIQUEIDENTIFIER = NULL,

@UserName	AS VARCHAR(50),

@Matricula	AS VARCHAR(6)

AS

BEGIN

	--Adicionar
	IF(@Acao = 1)
		BEGIN
			SELECT 
				CASE WHEN EXISTS
					(

						SELECT [Id] FROM AspNetUsers
						WHERE
							UserName	= @UserName
					
						OR	Matricula	= @Matricula
					)
			
					THEN CAST(1 AS BIT) --TRUE
			
					ELSE CAST(0 AS BIT) --FALSE
				END;
		END;

	--Atualizar
	ELSE IF(@Acao = 2)
		BEGIN
			SELECT 
				CASE WHEN EXISTS
					(

						SELECT [Id] FROM AspNetUsers
						WHERE
							Id			<>	@Id
					
						OR UserName		= @UserName
					
						OR	Matricula	= @Matricula
					)
			
					THEN CAST(1 AS BIT) --TRUE
			
					ELSE CAST(0 AS BIT) --FALSE
				END;

		END;
		
END;
GO

