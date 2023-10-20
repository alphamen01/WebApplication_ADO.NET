USE [NL]
GO

/****** Object:  StoredProcedure [mantenimiento].[ADO_ASP_MVC_DELETECLIENTE]    Script Date: 20/10/2023 01:02:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mantenimiento].[ADO_ASP_MVC_DELETECLIENTE]
(
@ID_CLIENTE INT,
@OUTPUTMESSAGE VARCHAR(50) OUTPUT
 )
AS
BEGIN
	DECLARE @ROWCOUNT INT = 0

    BEGIN TRY
		
		SET @ROWCOUNT = (SELECT COUNT(1) FROM mantenimiento.Cliente WHERE id_cliente = @ID_CLIENTE)

		IF(@ROWCOUNT > 0)
			BEGIN
				DELETE FROM mantenimiento.Cliente
				WHERE id_cliente = @ID_CLIENTE;
				SET @OUTPUTMESSAGE = 'Cliente eliminado correctamente.';
			END
		ELSE
			BEGIN
				SET @OUTPUTMESSAGE = 'Cliente no existe.';
			END        
    END TRY
    BEGIN CATCH
        SET @OUTPUTMESSAGE = ERROR_MESSAGE();
    END CATCH
END
GO

