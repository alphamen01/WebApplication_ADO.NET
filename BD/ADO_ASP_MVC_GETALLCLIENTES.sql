USE [NL]
GO

/****** Object:  StoredProcedure [mantenimiento].[ADO_ASP_MVC_GETALLCLIENTES]    Script Date: 20/10/2023 01:03:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mantenimiento].[ADO_ASP_MVC_GETALLCLIENTES]
AS
BEGIN
	SELECT id_cliente,nombre,direccion,telefono FROM mantenimiento.Cliente	WITH(NOLOCK)
END
GO

