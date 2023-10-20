USE [NL]
GO

/****** Object:  StoredProcedure [mantenimiento].[ADO_ASP_MVC_UPDATECLIENTE]    Script Date: 20/10/2023 01:04:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mantenimiento].[ADO_ASP_MVC_UPDATECLIENTE]
 @CLIENTE_TYPE mantenimiento.ClienteTYPE READONLY
AS
BEGIN
	-- Declarar variables
    DECLARE	@ID_CLIENTE INT,
			@NOMBRE VARCHAR(255),
            @DIRECCION VARCHAR(255),
            @TELEFONO VARCHAR(15)
	-- Recuperar datos del primer registro en @CLIENTE_TYPE
    SELECT TOP 1 
		@ID_CLIENTE = id_cliente,
        @NOMBRE = nombre,
        @DIRECCION = direccion,
        @TELEFONO = telefono
    FROM @CLIENTE_TYPE

    DECLARE @ROWCOUNT INT = 0

    -- Verificar si ya existe un cliente con el mismo nombre
    SET @ROWCOUNT = (SELECT COUNT(1) FROM mantenimiento.Cliente WHERE nombre = @NOMBRE AND id_cliente <> @ID_CLIENTE)

     -- Comprobar si el cliente ya existe antes de actualizar
    IF @ROWCOUNT = 0 
    BEGIN
        BEGIN TRY
            UPDATE mantenimiento.Cliente
            SET nombre = @NOMBRE, direccion = @DIRECCION, telefono = @TELEFONO
            WHERE id_cliente = @ID_CLIENTE
        END TRY
        BEGIN CATCH
            -- Captura el mensaje de error y lo devuelve
            SELECT 'Error: ' + ERROR_MESSAGE() AS ErrorMessage
        END CATCH
    END
    ELSE
    BEGIN
        -- Devolver un mensaje si el cliente ya existe
        SELECT 'Cliente con el mismo nombre ya existe' AS ErrorMessage
    END
END
GO

