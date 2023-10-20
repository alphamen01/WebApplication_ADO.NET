USE [NL]
GO

/****** Object:  StoredProcedure [mantenimiento].[ADO_ASP_MVC_INSERTCLIENTE]    Script Date: 20/10/2023 01:03:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mantenimiento].[ADO_ASP_MVC_INSERTCLIENTE]
 @CLIENTE_TYPE mantenimiento.ClienteTYPE READONLY
AS
BEGIN
    DECLARE @NOMBRE VARCHAR(255),
            @DIRECCION VARCHAR(255),
            @TELEFONO VARCHAR(15)

    SELECT TOP 1    
        @NOMBRE = nombre,
        @DIRECCION = direccion,
        @TELEFONO = telefono
    FROM @CLIENTE_TYPE

    DECLARE @ROWCOUNT INT = 0

    -- Verificar si ya existe un cliente con el mismo nombre
    SET @ROWCOUNT = (SELECT COUNT(1) FROM mantenimiento.Cliente WHERE nombre = @NOMBRE)

    -- Comprobar si el cliente ya existe antes de insertar
    IF @ROWCOUNT = 0
    BEGIN
        BEGIN TRY
            INSERT INTO mantenimiento.Cliente (nombre, direccion, telefono)
            VALUES (@NOMBRE, @DIRECCION, @TELEFONO)
        END TRY
        BEGIN CATCH
            -- Captura el mensaje de error y lo devuelve
            SELECT 'Error: ' + ERROR_MESSAGE() AS ErrorMessage
        END CATCH
    END
    ELSE
    BEGIN
        -- Devolver un mensaje si el cliente ya existe
        SELECT 'Cliente ya existe' AS ErrorMessage
    END
END
GO

