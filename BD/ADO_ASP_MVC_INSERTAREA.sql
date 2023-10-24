USE [NL]
GO

/****** Object:  StoredProcedure [mantenimiento].[ADO_ASP_MVC_INSERTAREA]    Script Date: 24/10/2023 15:51:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mantenimiento].[ADO_ASP_MVC_INSERTAREA]
 @AREA_TYPE mantenimiento.AreaTYPE READONLY
AS
BEGIN
    DECLARE @DESCRIPCION VARCHAR(255),
            @ID_CLIENTE INT,
            @ENU_ESTADO_REGISTRO CHAR(1),
			@USUARIO_CREACION VARCHAR(255),
			@FECHA_CREACION DATETIME
			--@USUARIO_MODIFICACION VARCHAR(255),
			--@FECHA_MODIFICACION DATETIME

    SELECT TOP 1    
        @DESCRIPCION = descripcion,
        @ID_CLIENTE = id_cliente,
        @ENU_ESTADO_REGISTRO = enu_estado_registro,
		@USUARIO_CREACION = usuario_creacion,
		@FECHA_CREACION = fecha_creacion

    FROM @AREA_TYPE

    DECLARE @ROWCOUNT INT = 0

    -- Verificar si ya existe un area con la misma descripcion
    SET @ROWCOUNT = (SELECT COUNT(1) FROM mantenimiento.Area WHERE descripcion = @DESCRIPCION)

    -- Comprobar si la area ya existe antes de insertar
    IF @ROWCOUNT = 0
    BEGIN
        BEGIN TRY
            INSERT INTO mantenimiento.Area(descripcion, id_cliente, enu_estado_registro,usuario_creacion,fecha_creacion)
            VALUES (@DESCRIPCION, @ID_CLIENTE, @ENU_ESTADO_REGISTRO,@USUARIO_CREACION,@FECHA_CREACION)
        END TRY
        BEGIN CATCH
            -- Captura el mensaje de error y lo devuelve
            SELECT 'Error: ' + ERROR_MESSAGE() AS ErrorMessage
        END CATCH
    END
    ELSE
    BEGIN
        -- Devolver un mensaje si la area ya existe
        SELECT 'Area ya existe' AS ErrorMessage
    END
END
GO

