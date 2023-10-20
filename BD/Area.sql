USE [NL]
GO

/****** Object:  Table [mantenimiento].[Area]    Script Date: 20/10/2023 01:07:44 ******/
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

ALTER TABLE [mantenimiento].[Area]  WITH CHECK ADD FOREIGN KEY([id_cliente])
REFERENCES [mantenimiento].[Cliente] ([id_cliente])
GO

