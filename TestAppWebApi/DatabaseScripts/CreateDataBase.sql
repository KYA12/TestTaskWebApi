USE [master]
GO

/****** Object:  Database [ShopDataBase]    Script Date: 24.03.2020 17:12:25 ******/
CREATE DATABASE [ShopDataBase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ShopDataBase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ShopDataBase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ShopDataBase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ShopDataBase_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ShopDataBase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [ShopDataBase] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [ShopDataBase] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [ShopDataBase] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [ShopDataBase] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [ShopDataBase] SET ARITHABORT OFF 
GO

ALTER DATABASE [ShopDataBase] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [ShopDataBase] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [ShopDataBase] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [ShopDataBase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [ShopDataBase] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [ShopDataBase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [ShopDataBase] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [ShopDataBase] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [ShopDataBase] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [ShopDataBase] SET  DISABLE_BROKER 
GO

ALTER DATABASE [ShopDataBase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [ShopDataBase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [ShopDataBase] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [ShopDataBase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [ShopDataBase] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [ShopDataBase] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [ShopDataBase] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [ShopDataBase] SET RECOVERY FULL 
GO

ALTER DATABASE [ShopDataBase] SET  MULTI_USER 
GO

ALTER DATABASE [ShopDataBase] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [ShopDataBase] SET DB_CHAINING OFF 
GO

ALTER DATABASE [ShopDataBase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [ShopDataBase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [ShopDataBase] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [ShopDataBase] SET QUERY_STORE = OFF
GO

ALTER DATABASE [ShopDataBase] SET  READ_WRITE 
GO


