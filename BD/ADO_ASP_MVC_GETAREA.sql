USE [NL]
GO

/****** Object:  StoredProcedure [mantenimiento].[ADO_ASP_MVC_GETAREA]    Script Date: 24/10/2023 15:51:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mantenimiento].[ADO_ASP_MVC_GETAREA]
@ID_AREA INT
AS
BEGIN
	SELECT	A.id_area,
			A.descripcion,
			A.id_cliente,
			A.enu_estado_registro,
			A.usuario_creacion,
			A.fecha_creacion,
			A.usuario_modificacion,
			A.fecha_modificacion,
			C.nombre AS nombre_cliente
	FROM mantenimiento.Area AS A WITH (NOLOCK)
	INNER JOIN [mantenimiento].[Cliente] AS C WITH (NOLOCK) ON A.id_cliente = C.id_cliente
	WHERE id_area = @ID_AREA
END
GO

