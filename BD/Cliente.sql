USE [NL]
GO

/****** Object:  Table [mantenimiento].[Cliente]    Script Date: 20/10/2023 01:06:03 ******/
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


---CREACION DE TYPES

CREATE TYPE mantenimiento.ClienteTYPE AS TABLE
(
    id_cliente INT,
    nombre VARCHAR(255),
    direccion VARCHAR(255),
    telefono VARCHAR(15)
);
