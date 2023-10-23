USE [NL]
GO

/****** Object:  StoredProcedure [mantenimiento].[ADO_ASP_MVC_VALIDARUSUARIO]    Script Date: 23/10/2023 11:40:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mantenimiento].[ADO_ASP_MVC_VALIDARUSUARIO](
@CORREO VARCHAR(100),
@CLAVE VARCHAR(500)
)
AS
BEGIN
	IF(EXISTS(SELECT * FROM mantenimiento.Usuario WHERE correo = @CORREO AND clave = @CLAVE))
		SELECT id_usuario FROM mantenimiento.Usuario WHERE correo = @CORREO AND clave = @CLAVE
	ELSE
		SELECT '0'
END
GO

