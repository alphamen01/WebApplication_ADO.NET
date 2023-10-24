USE [NL]
GO

/****** Object:  StoredProcedure [mantenimiento].[ADO_ASP_MVC_UPDATEAREA]    Script Date: 24/10/2023 15:52:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mantenimiento].[ADO_ASP_MVC_UPDATEAREA]
 @AREA_TYPE mantenimiento.AreaTYPE READONLY
AS
BEGIN
	-- Declarar variables
    DECLARE	@ID_AREA INT,
			@DESCRIPCION VARCHAR(255),
            @ID_CLIENTE INT,
            @ENU_ESTADO_REGISTRO CHAR(1),
			@USUARIO_CREACION VARCHAR(255),
			@FECHA_CREACION DATETIME,
			@USUARIO_MODIFICACION VARCHAR(255),
			@FECHA_MODIFICACION DATETIME
	-- Recuperar datos del primer registro en @AREA_TYPE
    SELECT TOP 1 
		@ID_AREA = id_area,
        @DESCRIPCION = descripcion,
        @ID_CLIENTE = id_cliente,
        @ENU_ESTADO_REGISTRO = enu_estado_registro,
		@USUARIO_CREACION = usuario_creacion,
		@FECHA_CREACION = fecha_creacion,
		@USUARIO_MODIFICACION = usuario_modificacion,
		@FECHA_MODIFICACION = fecha_modificacion
    FROM @AREA_TYPE

    DECLARE @ROWCOUNT INT = 0

    -- Verificar si ya existe un area con la misma descripcion
    SET @ROWCOUNT = (SELECT COUNT(1) FROM mantenimiento.Area WHERE descripcion = @DESCRIPCION AND id_area <> @ID_AREA)

     -- Comprobar si la area ya existe antes de actualizar
    IF @ROWCOUNT = 0 
    BEGIN
        BEGIN TRY
            UPDATE mantenimiento.Area
            SET descripcion = @DESCRIPCION, id_cliente = @ID_CLIENTE, enu_estado_registro = @ENU_ESTADO_REGISTRO, usuario_creacion = @USUARIO_CREACION, fecha_creacion = @FECHA_CREACION, usuario_modificacion = @USUARIO_MODIFICACION, fecha_modificacion = @FECHA_MODIFICACION
            WHERE id_area = @ID_AREA
        END TRY
        BEGIN CATCH
            -- Captura el mensaje de error y lo devuelve
            SELECT 'Error: ' + ERROR_MESSAGE() AS ErrorMessage
        END CATCH
    END
    ELSE
    BEGIN
        -- Devolver un mensaje si la area ya existe
        SELECT 'Area con la misma descripcion ya existe' AS ErrorMessage
    END
END
GO

