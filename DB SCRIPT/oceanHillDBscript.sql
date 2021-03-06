USE [master]
GO
/****** Object:  Database [OceanHillDB]    Script Date: 08/04/2021 09:19:39 p. m. ******/
CREATE DATABASE [OceanHillDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OceanHillDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\OceanHillDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OceanHillDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\OceanHillDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [OceanHillDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OceanHillDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OceanHillDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OceanHillDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OceanHillDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OceanHillDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OceanHillDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [OceanHillDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OceanHillDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OceanHillDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OceanHillDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OceanHillDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OceanHillDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OceanHillDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OceanHillDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OceanHillDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OceanHillDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OceanHillDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OceanHillDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OceanHillDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OceanHillDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OceanHillDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OceanHillDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OceanHillDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OceanHillDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [OceanHillDB] SET  MULTI_USER 
GO
ALTER DATABASE [OceanHillDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OceanHillDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OceanHillDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OceanHillDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OceanHillDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OceanHillDB] SET QUERY_STORE = OFF
GO
USE [OceanHillDB]
GO
/****** Object:  Table [dbo].[Credenciales]    Script Date: 08/04/2021 09:19:39 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Credenciales](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Pass] [varchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Habitaciones]    Script Date: 08/04/2021 09:19:39 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Habitaciones](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nro_habitacion] [int] NOT NULL,
	[Esta_ocupada] [bit] NOT NULL,
	[Precio_por_dia] [float] NOT NULL,
	[Capacidad_max] [int] NOT NULL,
 CONSTRAINT [PK__Habitaci__3214EC276E45F2A2] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Huespedes]    Script Date: 08/04/2021 09:19:39 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Huespedes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](20) NOT NULL,
	[Apellido] [varchar](20) NOT NULL,
	[DNI] [int] NOT NULL,
	[Nro_habitacion] [int] NOT NULL,
	[Fecha_ingreso] [datetime] NOT NULL,
	[Fecha_egreso] [datetime] NOT NULL,
	[Dias_hospedado] [float] NOT NULL,
 CONSTRAINT [PK__Huespede__3214EC27914705A5] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Registro]    Script Date: 08/04/2021 09:19:39 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Registro](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](40) NOT NULL,
	[Apellido] [varchar](40) NOT NULL,
	[DNI] [int] NOT NULL,
	[Nro_habitacion] [int] NOT NULL,
	[Fecha_ingreso] [datetime] NOT NULL,
	[Fecha_egreso] [datetime] NOT NULL,
	[Dias_hospedado] [float] NOT NULL,
	[Id_activo] [int] NULL,
 CONSTRAINT [PK__Registro__3214EC27D195C06B] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Credenciales] ON 

INSERT [dbo].[Credenciales] ([ID], [Pass]) VALUES (1, N'admin2413')
SET IDENTITY_INSERT [dbo].[Credenciales] OFF
GO
SET IDENTITY_INSERT [dbo].[Habitaciones] ON 

INSERT [dbo].[Habitaciones] ([ID], [Nro_habitacion], [Esta_ocupada], [Precio_por_dia], [Capacidad_max]) VALUES (11, 2, 1, 550, 4)
INSERT [dbo].[Habitaciones] ([ID], [Nro_habitacion], [Esta_ocupada], [Precio_por_dia], [Capacidad_max]) VALUES (12, 8, 0, 520, 4)
INSERT [dbo].[Habitaciones] ([ID], [Nro_habitacion], [Esta_ocupada], [Precio_por_dia], [Capacidad_max]) VALUES (13, 12, 0, 250, 2)
INSERT [dbo].[Habitaciones] ([ID], [Nro_habitacion], [Esta_ocupada], [Precio_por_dia], [Capacidad_max]) VALUES (14, 21, 0, 350, 3)
INSERT [dbo].[Habitaciones] ([ID], [Nro_habitacion], [Esta_ocupada], [Precio_por_dia], [Capacidad_max]) VALUES (15, 25, 0, 635, 4)
INSERT [dbo].[Habitaciones] ([ID], [Nro_habitacion], [Esta_ocupada], [Precio_por_dia], [Capacidad_max]) VALUES (16, 35, 0, 490, 3)
INSERT [dbo].[Habitaciones] ([ID], [Nro_habitacion], [Esta_ocupada], [Precio_por_dia], [Capacidad_max]) VALUES (17, 32, 0, 1500, 4)
INSERT [dbo].[Habitaciones] ([ID], [Nro_habitacion], [Esta_ocupada], [Precio_por_dia], [Capacidad_max]) VALUES (18, 39, 0, 285, 2)
INSERT [dbo].[Habitaciones] ([ID], [Nro_habitacion], [Esta_ocupada], [Precio_por_dia], [Capacidad_max]) VALUES (19, 95, 0, 230, 2)
INSERT [dbo].[Habitaciones] ([ID], [Nro_habitacion], [Esta_ocupada], [Precio_por_dia], [Capacidad_max]) VALUES (1020, 102, 0, 485, 4)
INSERT [dbo].[Habitaciones] ([ID], [Nro_habitacion], [Esta_ocupada], [Precio_por_dia], [Capacidad_max]) VALUES (1021, 152, 0, 585, 4)
INSERT [dbo].[Habitaciones] ([ID], [Nro_habitacion], [Esta_ocupada], [Precio_por_dia], [Capacidad_max]) VALUES (1023, 300, 0, 690, 2)
SET IDENTITY_INSERT [dbo].[Habitaciones] OFF
GO
SET IDENTITY_INSERT [dbo].[Huespedes] ON 

INSERT [dbo].[Huespedes] ([ID], [Nombre], [Apellido], [DNI], [Nro_habitacion], [Fecha_ingreso], [Fecha_egreso], [Dias_hospedado]) VALUES (2050, N'sujeto111111', N'apellido1', 6543215, 2, CAST(N'2020-11-30T19:37:00.000' AS DateTime), CAST(N'2020-12-03T19:37:00.000' AS DateTime), 3)
SET IDENTITY_INSERT [dbo].[Huespedes] OFF
GO
SET IDENTITY_INSERT [dbo].[Registro] ON 

INSERT [dbo].[Registro] ([ID], [Nombre], [Apellido], [DNI], [Nro_habitacion], [Fecha_ingreso], [Fecha_egreso], [Dias_hospedado], [Id_activo]) VALUES (37, N'nicolas', N'calezich', 42300580, 32, CAST(N'2020-11-22T03:48:00.000' AS DateTime), CAST(N'2020-11-27T03:48:00.000' AS DateTime), 5, 45)
INSERT [dbo].[Registro] ([ID], [Nombre], [Apellido], [DNI], [Nro_habitacion], [Fecha_ingreso], [Fecha_egreso], [Dias_hospedado], [Id_activo]) VALUES (38, N'juan', N'perez', 359856, 2, CAST(N'2020-11-22T12:19:00.000' AS DateTime), CAST(N'2020-11-26T12:19:00.000' AS DateTime), 4, 46)
INSERT [dbo].[Registro] ([ID], [Nombre], [Apellido], [DNI], [Nro_habitacion], [Fecha_ingreso], [Fecha_egreso], [Dias_hospedado], [Id_activo]) VALUES (39, N'batata', N'galzina', 6432188, 2, CAST(N'2020-11-22T15:27:00.000' AS DateTime), CAST(N'2020-11-28T15:27:00.000' AS DateTime), 6, 47)
INSERT [dbo].[Registro] ([ID], [Nombre], [Apellido], [DNI], [Nro_habitacion], [Fecha_ingreso], [Fecha_egreso], [Dias_hospedado], [Id_activo]) VALUES (40, N'camila', N'diaz', 3854565, 32, CAST(N'2020-11-23T00:38:00.000' AS DateTime), CAST(N'2020-12-04T00:38:00.000' AS DateTime), 11, 48)
INSERT [dbo].[Registro] ([ID], [Nombre], [Apellido], [DNI], [Nro_habitacion], [Fecha_ingreso], [Fecha_egreso], [Dias_hospedado], [Id_activo]) VALUES (41, N'nicolas', N'calezich', 42300580, 12, CAST(N'2020-11-24T00:02:00.000' AS DateTime), CAST(N'2020-12-03T00:02:00.000' AS DateTime), 9, 49)
INSERT [dbo].[Registro] ([ID], [Nombre], [Apellido], [DNI], [Nro_habitacion], [Fecha_ingreso], [Fecha_egreso], [Dias_hospedado], [Id_activo]) VALUES (42, N'sergio', N'perez', 3645656, 35, CAST(N'2020-11-24T12:42:00.000' AS DateTime), CAST(N'2020-11-28T12:42:00.000' AS DateTime), 4, 50)
INSERT [dbo].[Registro] ([ID], [Nombre], [Apellido], [DNI], [Nro_habitacion], [Fecha_ingreso], [Fecha_egreso], [Dias_hospedado], [Id_activo]) VALUES (44, N'test', N'galzina', 23132, 2, CAST(N'2020-11-26T01:01:00.000' AS DateTime), CAST(N'2020-11-27T01:01:00.000' AS DateTime), 1, 50)
INSERT [dbo].[Registro] ([ID], [Nombre], [Apellido], [DNI], [Nro_habitacion], [Fecha_ingreso], [Fecha_egreso], [Dias_hospedado], [Id_activo]) VALUES (1041, N'carlos', N'liming', 37321231, 12, CAST(N'2020-11-27T16:42:00.000' AS DateTime), CAST(N'2020-11-30T16:42:00.000' AS DateTime), 3, 1050)
INSERT [dbo].[Registro] ([ID], [Nombre], [Apellido], [DNI], [Nro_habitacion], [Fecha_ingreso], [Fecha_egreso], [Dias_hospedado], [Id_activo]) VALUES (1042, N'fede', N'perez', 3845654, 102, CAST(N'2020-11-27T16:57:00.000' AS DateTime), CAST(N'2020-12-05T16:57:00.000' AS DateTime), 8, 1051)
INSERT [dbo].[Registro] ([ID], [Nombre], [Apellido], [DNI], [Nro_habitacion], [Fecha_ingreso], [Fecha_egreso], [Dias_hospedado], [Id_activo]) VALUES (1043, N'batata', N'perez', 4198652, 2, CAST(N'2020-11-27T17:19:00.000' AS DateTime), CAST(N'2020-11-30T17:19:00.000' AS DateTime), 3, 1052)
INSERT [dbo].[Registro] ([ID], [Nombre], [Apellido], [DNI], [Nro_habitacion], [Fecha_ingreso], [Fecha_egreso], [Dias_hospedado], [Id_activo]) VALUES (1044, N'dsa', N'sd', 213, 2, CAST(N'2020-11-28T17:23:00.000' AS DateTime), CAST(N'2020-12-03T17:23:00.000' AS DateTime), 5, 1053)
INSERT [dbo].[Registro] ([ID], [Nombre], [Apellido], [DNI], [Nro_habitacion], [Fecha_ingreso], [Fecha_egreso], [Dias_hospedado], [Id_activo]) VALUES (1046, N'fransis', N'leic', 654621, 8, CAST(N'2020-11-27T17:39:00.000' AS DateTime), CAST(N'2020-11-30T17:39:00.000' AS DateTime), 3, 1055)
INSERT [dbo].[Registro] ([ID], [Nombre], [Apellido], [DNI], [Nro_habitacion], [Fecha_ingreso], [Fecha_egreso], [Dias_hospedado], [Id_activo]) VALUES (1047, N'nicolas', N'calezich', 4131456, 2, CAST(N'2020-11-27T17:47:00.000' AS DateTime), CAST(N'2020-12-02T17:47:00.000' AS DateTime), 5, 1056)
INSERT [dbo].[Registro] ([ID], [Nombre], [Apellido], [DNI], [Nro_habitacion], [Fecha_ingreso], [Fecha_egreso], [Dias_hospedado], [Id_activo]) VALUES (1048, N'sadsad', N'fsdsadsa', 213321, 95, CAST(N'2020-11-27T17:49:00.000' AS DateTime), CAST(N'2020-11-30T17:49:00.000' AS DateTime), 3, 1057)
INSERT [dbo].[Registro] ([ID], [Nombre], [Apellido], [DNI], [Nro_habitacion], [Fecha_ingreso], [Fecha_egreso], [Dias_hospedado], [Id_activo]) VALUES (1049, N'nicolas', N'calezich', 4131456, 2, CAST(N'2020-11-27T17:47:00.000' AS DateTime), CAST(N'2020-12-17T17:47:00.000' AS DateTime), 20, 1059)
INSERT [dbo].[Registro] ([ID], [Nombre], [Apellido], [DNI], [Nro_habitacion], [Fecha_ingreso], [Fecha_egreso], [Dias_hospedado], [Id_activo]) VALUES (2041, N'manolo', N'diaz', 6542165, 25, CAST(N'2020-11-29T18:28:00.000' AS DateTime), CAST(N'2020-12-10T18:28:00.000' AS DateTime), 11, 2049)
INSERT [dbo].[Registro] ([ID], [Nombre], [Apellido], [DNI], [Nro_habitacion], [Fecha_ingreso], [Fecha_egreso], [Dias_hospedado], [Id_activo]) VALUES (2042, N'sujeto111111', N'apellido1', 6543215, 2, CAST(N'2020-11-30T19:37:00.000' AS DateTime), CAST(N'2020-12-03T19:37:00.000' AS DateTime), 3, 2050)
INSERT [dbo].[Registro] ([ID], [Nombre], [Apellido], [DNI], [Nro_habitacion], [Fecha_ingreso], [Fecha_egreso], [Dias_hospedado], [Id_activo]) VALUES (2043, N'sujeto2', N'apellido2', 43546832, 32, CAST(N'2020-11-30T19:37:00.000' AS DateTime), CAST(N'2020-12-03T19:37:00.000' AS DateTime), 3, 2051)
INSERT [dbo].[Registro] ([ID], [Nombre], [Apellido], [DNI], [Nro_habitacion], [Fecha_ingreso], [Fecha_egreso], [Dias_hospedado], [Id_activo]) VALUES (2044, N'sujeto3', N'apellio2', 21654213, 21, CAST(N'2020-11-30T19:37:00.000' AS DateTime), CAST(N'2020-12-09T19:37:00.000' AS DateTime), 9, 2051)
SET IDENTITY_INSERT [dbo].[Registro] OFF
GO
USE [master]
GO
ALTER DATABASE [OceanHillDB] SET  READ_WRITE 
GO
