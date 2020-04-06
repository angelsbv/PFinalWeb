USE [master]
GO
/****** Object:  Database [PFinalWeb]    Script Date: 4/4/2020 01:29:14 ******/
CREATE DATABASE [PFinalWeb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PFinalWeb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PFinalWeb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PFinalWeb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PFinalWeb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PFinalWeb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PFinalWeb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PFinalWeb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PFinalWeb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PFinalWeb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PFinalWeb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PFinalWeb] SET ARITHABORT OFF 
GO
ALTER DATABASE [PFinalWeb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PFinalWeb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PFinalWeb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PFinalWeb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PFinalWeb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PFinalWeb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PFinalWeb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PFinalWeb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PFinalWeb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PFinalWeb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PFinalWeb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PFinalWeb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PFinalWeb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PFinalWeb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PFinalWeb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PFinalWeb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PFinalWeb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PFinalWeb] SET RECOVERY FULL 
GO
ALTER DATABASE [PFinalWeb] SET  MULTI_USER 
GO
ALTER DATABASE [PFinalWeb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PFinalWeb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PFinalWeb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PFinalWeb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PFinalWeb] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'PFinalWeb', N'ON'
GO
ALTER DATABASE [PFinalWeb] SET QUERY_STORE = OFF
GO
USE [PFinalWeb]
GO
/****** Object:  Table [dbo].[Admins]    Script Date: 4/4/2020 01:29:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Email] [varchar](320) NOT NULL,
	[Password] [varchar](max) NOT NULL,
	[IsEmailVerified] [bit] NOT NULL,
	[VerificationCode] [uniqueidentifier] NOT NULL,
	[RegisterDate] [datetime] NULL,
	[Birthdate] [date] NULL,
	[RecoverPWDCode] [nvarchar](100) NULL,
 CONSTRAINT [PK__Admins__1788CCACF2AFD95F] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [PFinalWeb] SET  READ_WRITE 
GO
