USE [NL]
GO

/****** Object:  StoredProcedure [mantenimiento].[ADO_ASP_MVC_DELETEAREA]    Script Date: 24/10/2023 15:50:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mantenimiento].[ADO_ASP_MVC_DELETEAREA]
(
@ID_AREA INT,
@OUTPUTMESSAGE VARCHAR(50) OUTPUT
 )
AS
BEGIN
	DECLARE @ROWCOUNT INT = 0

    BEGIN TRY
		
		SET @ROWCOUNT = (SELECT COUNT(1) FROM mantenimiento.Area WHERE id_area = @ID_AREA)

		IF(@ROWCOUNT > 0)
			BEGIN
				DELETE FROM mantenimiento.Area
				WHERE id_area = @ID_AREA;
				SET @OUTPUTMESSAGE = 'Area eliminada correctamente.';
			END
		ELSE
			BEGIN
				SET @OUTPUTMESSAGE = 'Area no existe.';
			END        
    END TRY
    BEGIN CATCH
        SET @OUTPUTMESSAGE = ERROR_MESSAGE();
    END CATCH
END
GO

