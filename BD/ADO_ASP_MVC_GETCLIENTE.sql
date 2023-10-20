USE [NL]
GO

/****** Object:  StoredProcedure [mantenimiento].[ADO_ASP_MVC_GETCLIENTE]    Script Date: 20/10/2023 01:03:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mantenimiento].[ADO_ASP_MVC_GETCLIENTE]
@ID_CLIENTE INT
AS
BEGIN
	SELECT	id_cliente,
			nombre,
			direccion,
			telefono
	FROM mantenimiento.Cliente
	WHERE id_cliente = @ID_CLIENTE
END
GO

