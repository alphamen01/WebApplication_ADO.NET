USE [NL]
GO

/****** Object:  Table [mantenimiento].[Usuario]    Script Date: 23/10/2023 11:39:16 ******/
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

