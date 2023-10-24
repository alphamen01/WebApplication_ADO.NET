USE [NL]
GO
/****** Object:  Schema [mantenimiento]    Script Date: 23/10/2023 11:44:58 ******/
CREATE SCHEMA [mantenimiento]
GO
/****** Object:  UserDefinedTableType [mantenimiento].[ClienteTYPE]    Script Date: 23/10/2023 11:44:58 ******/
CREATE TYPE [mantenimiento].[ClienteTYPE] AS TABLE(
	[id_cliente] [int] NULL,
	[nombre] [varchar](255) NULL,
	[direccion] [varchar](255) NULL,
	[telefono] [varchar](15) NULL
)
GO

CREATE TYPE mantenimiento.AreaTYPE AS TABLE(
	[id_area] [int] NULL,
	[descripcion] [varchar](255) NULL,
	[id_cliente] [int] NULL,
	[enu_estado_registro] [char](1) NULL,
	[usuario_creacion] [varchar](255) NULL,
	[fecha_creacion][datetime] NULL,
	[usuario_modificacion] [varchar](255) NULL,
	[fecha_modificacion] [datetime] NULL
	
)
GO
/****** Object:  Table [mantenimiento].[Area]    Script Date: 23/10/2023 11:44:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mantenimiento].[Area](
	[id_area] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](255) NOT NULL,
	[id_cliente] [int] NOT NULL,
	[enu_estado_registro] [char](1) NOT NULL,
	[usuario_creacion] [varchar](255) NULL,
	[fecha_creacion] [datetime] NULL,
	[usuario_modificacion] [varchar](255) NULL,
	[fecha_modificacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_area] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [mantenimiento].[Cliente]    Script Date: 23/10/2023 11:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mantenimiento].[Cliente](
	[id_cliente] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](255) NOT NULL,
	[direccion] [varchar](255) NOT NULL,
	[telefono] [varchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [mantenimiento].[Usuario]    Script Date: 23/10/2023 11:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mantenimiento].[Usuario](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[correo] [varchar](100) NULL,
	[clave] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [mantenimiento].[Area]  WITH CHECK ADD FOREIGN KEY([id_cliente])
REFERENCES [mantenimiento].[Cliente] ([id_cliente])
GO
/****** Object:  StoredProcedure [mantenimiento].[ADO_ASP_MVC_DELETECLIENTE]    Script Date: 23/10/2023 11:44:59 ******/
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
/****** Object:  StoredProcedure [mantenimiento].[ADO_ASP_MVC_GETALLCLIENTES]    Script Date: 23/10/2023 11:44:59 ******/
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
/****** Object:  StoredProcedure [mantenimiento].[ADO_ASP_MVC_GETCLIENTE]    Script Date: 23/10/2023 11:44:59 ******/
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
/****** Object:  StoredProcedure [mantenimiento].[ADO_ASP_MVC_INSERTCLIENTE]    Script Date: 23/10/2023 11:44:59 ******/
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
/****** Object:  StoredProcedure [mantenimiento].[ADO_ASP_MVC_REGISTRARUSUARIO]    Script Date: 23/10/2023 11:44:59 ******/
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
/****** Object:  StoredProcedure [mantenimiento].[ADO_ASP_MVC_UPDATECLIENTE]    Script Date: 23/10/2023 11:44:59 ******/
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
/****** Object:  StoredProcedure [mantenimiento].[ADO_ASP_MVC_VALIDARUSUARIO]    Script Date: 23/10/2023 11:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [mantenimiento].[ADO_ASP_MVC_VALIDARUSUARIO](
@CORREO VARCHAR(100),
@CLAVE VARCHAR(500)
)
AS
BEGIN
	IF(EXISTS(SELECT * FROM mantenimiento.Usuario WHERE correo = @CORREO AND clave = @CLAVE))
		SELECT id_usuario FROM mantenimiento.Usuario WHERE correo = @CORREO AND clave = @CLAVE
	ELSE
		SELECT '0'
END
GO
/****** Object:  StoredProcedure [mantenimiento].[USP_CONTAR_CLIENTES]    Script Date: 23/10/2023 11:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [mantenimiento].[USP_CONTAR_CLIENTES]
AS
BEGIN
	SELECT COUNT(id_cliente) AS nro_regitros from mantenimiento.Cliente; 
END
GO
/****** Object:  StoredProcedure [mantenimiento].[USP_CONTAR_CLIENTESR]    Script Date: 23/10/2023 11:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [mantenimiento].[USP_CONTAR_CLIENTESR]
AS
BEGIN
	DECLARE @nroR INT;
	SET @nroR = 0;
	SELECT @nroR = COUNT(id_cliente) from mantenimiento.Cliente; 
	RETURN @nroR
END
GO
/****** Object:  StoredProcedure [mantenimiento].[USP_CONTAR_CLIENTESS]    Script Date: 23/10/2023 11:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [mantenimiento].[USP_CONTAR_CLIENTESS]
AS
BEGIN
	
	SELECT COUNT(id_cliente) from mantenimiento.Cliente; 
END
GO
/****** Object:  StoredProcedure [mantenimiento].[USP_CONTAR_CLIENTESV]    Script Date: 23/10/2023 11:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [mantenimiento].[USP_CONTAR_CLIENTESV]
@NRO_REGISTROS INT OUT
AS
BEGIN
	DECLARE @nroR INT;
	SET @nroR = 0;
	SELECT @nroR = COUNT(id_cliente) from mantenimiento.Cliente; 
	SET @NRO_REGISTROS = @nroR
END
GO
/****** Object:  StoredProcedure [mantenimiento].[USP_ELIMINAR_CLIENTE]    Script Date: 23/10/2023 11:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [mantenimiento].[USP_ELIMINAR_CLIENTE]
@ID_CLIENTE INT
AS 
BEGIN
	DELETE mantenimiento.Cliente WHERE id_cliente = @ID_CLIENTE;
END
GO
/****** Object:  StoredProcedure [mantenimiento].[USP_MANT_CLIENTE]    Script Date: 23/10/2023 11:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [mantenimiento].[USP_MANT_CLIENTE]
    @ID_CLIENTE INT = NULL,
    @NOMBRE VARCHAR(255),
    @DIRECCION VARCHAR(255),
    @TELEFONO VARCHAR(15)
AS
BEGIN
    -- Verificar si el ID_CLIENTE proporcionado ya existe en la tabla
    IF EXISTS (SELECT 1 FROM mantenimiento.Cliente WHERE id_cliente = @ID_CLIENTE)
    BEGIN
        -- El registro ya existe, realizar una actualización
        UPDATE mantenimiento.Cliente
        SET nombre = @NOMBRE,
            direccion = @DIRECCION,
            telefono = @TELEFONO
        WHERE id_cliente = @ID_CLIENTE
    END
    ELSE
    BEGIN
        -- El registro no existe, realizar una inserción
        INSERT INTO mantenimiento.Cliente (nombre, direccion, telefono)
        VALUES (@NOMBRE, @DIRECCION, @TELEFONO)

        -- Obtener el ID asignado al nuevo cliente
        SET @ID_CLIENTE = SCOPE_IDENTITY()
    END
END
GO
/****** Object:  StoredProcedure [mantenimiento].[USP_OBTENER_CLIENTE]    Script Date: 23/10/2023 11:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [mantenimiento].[USP_OBTENER_CLIENTE]
@ID_CLIENTE INT
AS
BEGIN
	SELECT	
		id_cliente,
		nombre,
		direccion,
		telefono
	FROM mantenimiento.Cliente
	WHERE 
	id_cliente = @ID_CLIENTE
END
GO
