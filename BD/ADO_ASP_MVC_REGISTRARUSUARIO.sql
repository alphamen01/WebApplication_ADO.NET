USE [NL]
GO

/****** Object:  StoredProcedure [mantenimiento].[ADO_ASP_MVC_REGISTRARUSUARIO]    Script Date: 23/10/2023 11:40:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mantenimiento].[ADO_ASP_MVC_REGISTRARUSUARIO](
@CORREO VARCHAR(100),
@CLAVE VARCHAR(500),
@REGISTRADO BIT OUTPUT,
@MENSAJE VARCHAR(100) OUTPUT
)
AS
BEGIN
	IF(NOT EXISTS(SELECT * FROM mantenimiento.Usuario WHERE correo = @CORREO))
		BEGIN
			INSERT INTO mantenimiento.Usuario(correo,clave) VALUES (@CORREO,@CLAVE)
			SET @REGISTRADO = 1
			SET @MENSAJE = 'usuario registrado'
		END
	ELSE
		BEGIN
			SET @REGISTRADO = 0
			SET @MENSAJE = 'correo ya existe'
		END		
END
GO

