USE [master]
GO
/****** Object:  Database [CreditoAuto]    Script Date: 10/4/2022 9:48:00 AM ******/
CREATE DATABASE [CreditoAuto]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CreditoAuto', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CreditoAuto.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CreditoAuto_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CreditoAuto_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CreditoAuto] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CreditoAuto].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CreditoAuto] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CreditoAuto] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CreditoAuto] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CreditoAuto] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CreditoAuto] SET ARITHABORT OFF 
GO
ALTER DATABASE [CreditoAuto] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CreditoAuto] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CreditoAuto] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CreditoAuto] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CreditoAuto] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CreditoAuto] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CreditoAuto] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CreditoAuto] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CreditoAuto] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CreditoAuto] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CreditoAuto] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CreditoAuto] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CreditoAuto] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CreditoAuto] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CreditoAuto] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CreditoAuto] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CreditoAuto] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CreditoAuto] SET RECOVERY FULL 
GO
ALTER DATABASE [CreditoAuto] SET  MULTI_USER 
GO
ALTER DATABASE [CreditoAuto] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CreditoAuto] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CreditoAuto] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CreditoAuto] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CreditoAuto] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'CreditoAuto', N'ON'
GO
ALTER DATABASE [CreditoAuto] SET QUERY_STORE = OFF
GO
USE [CreditoAuto]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 10/4/2022 9:48:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[ClienteID] [int] IDENTITY(1,1) NOT NULL,
	[Identificacion] [varchar](13) NOT NULL,
	[Nombres] [varchar](50) NOT NULL,
	[Apellidos] [varchar](50) NOT NULL,
	[Edad] [int] NOT NULL,
	[FechaNacimiento] [date] NOT NULL,
	[Direccion] [varchar](50) NOT NULL,
	[Telefono] [varchar](50) NOT NULL,
	[EstadoCivil] [varchar](50) NOT NULL,
	[IdentificacionConyuge] [varchar](13) NOT NULL,
	[NombreConyuge] [varchar](50) NOT NULL,
	[SujetoCredito] [bit] NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[ClienteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientePatio]    Script Date: 10/4/2022 9:48:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientePatio](
	[ClientePatioID] [int] IDENTITY(1,1) NOT NULL,
	[IdentificacionCliente] [varchar](13) NOT NULL,
	[PatioID] [int] NOT NULL,
	[FechaAsignacion] [datetime] NOT NULL,
 CONSTRAINT [PK_ClientePatio] PRIMARY KEY CLUSTERED 
(
	[ClientePatioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ejecutivo]    Script Date: 10/4/2022 9:48:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ejecutivo](
	[EjecutivoID] [int] IDENTITY(1,1) NOT NULL,
	[Identificacion] [varchar](50) NOT NULL,
	[Nombres] [varchar](50) NOT NULL,
	[Apellidos] [varchar](50) NOT NULL,
	[Direccion] [varchar](50) NOT NULL,
	[TelefonoConvencional] [varchar](50) NOT NULL,
	[Celular] [varchar](50) NOT NULL,
	[Edad] [int] NOT NULL,
	[PatioID] [int] NOT NULL,
 CONSTRAINT [PK_Ejecutivo] PRIMARY KEY CLUSTERED 
(
	[EjecutivoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Marca]    Script Date: 10/4/2022 9:48:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Marca](
	[MarcaID] [int] IDENTITY(1,1) NOT NULL,
	[NombreMarca] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Marca] PRIMARY KEY CLUSTERED 
(
	[MarcaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patio]    Script Date: 10/4/2022 9:48:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patio](
	[PatioID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Direccion] [varchar](50) NOT NULL,
	[Telefono] [varchar](50) NOT NULL,
	[NumeroPuntoVenta] [int] NOT NULL,
 CONSTRAINT [PK_Patio] PRIMARY KEY CLUSTERED 
(
	[PatioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SolicitudCredito]    Script Date: 10/4/2022 9:48:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolicitudCredito](
	[SolicitudID] [int] IDENTITY(1,1) NOT NULL,
	[ClienteID] [int] NOT NULL,
	[PatioID] [int] NOT NULL,
	[VehiculoID] [int] NOT NULL,
	[EjecutivoID] [int] NOT NULL,
	[FechaSolicitud] [datetime] NOT NULL,
	[MesesPlazo] [int] NOT NULL,
	[Cuotas] [decimal](18, 0) NOT NULL,
	[Entrada] [decimal](18, 0) NOT NULL,
	[EstadoSolicitud] [varchar](50) NOT NULL,
	[Observacion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_SolicitudCredito] PRIMARY KEY CLUSTERED 
(
	[SolicitudID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehiculo]    Script Date: 10/4/2022 9:48:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehiculo](
	[VehiculoID] [int] IDENTITY(1,1) NOT NULL,
	[Placa] [varchar](50) NOT NULL,
	[Modelo] [varchar](50) NOT NULL,
	[NumeroChasis] [varchar](50) NOT NULL,
	[MarcaID] [int] NOT NULL,
	[Tipo] [varchar](50) NOT NULL,
	[Cilindraje] [varchar](50) NOT NULL,
	[Avaluo] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_Vehiculo] PRIMARY KEY CLUSTERED 
(
	[VehiculoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [CreditoAuto] SET  READ_WRITE 
GO
