USE [master]
GO
/****** Object:  Database [restaurantsByCuisine]    Script Date: 2/24/2016 6:18:25 PM ******/
CREATE DATABASE [restaurantsByCuisine]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'restaurantsByCuisine', FILENAME = N'C:\Users\doctorjuno\restaurantsByCuisine.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'restaurantsByCuisine_log', FILENAME = N'C:\Users\doctorjuno\restaurantsByCuisine_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [restaurantsByCuisine] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [restaurantsByCuisine].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [restaurantsByCuisine] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [restaurantsByCuisine] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [restaurantsByCuisine] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [restaurantsByCuisine] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [restaurantsByCuisine] SET ARITHABORT OFF 
GO
ALTER DATABASE [restaurantsByCuisine] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [restaurantsByCuisine] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [restaurantsByCuisine] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [restaurantsByCuisine] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [restaurantsByCuisine] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [restaurantsByCuisine] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [restaurantsByCuisine] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [restaurantsByCuisine] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [restaurantsByCuisine] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [restaurantsByCuisine] SET  ENABLE_BROKER 
GO
ALTER DATABASE [restaurantsByCuisine] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [restaurantsByCuisine] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [restaurantsByCuisine] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [restaurantsByCuisine] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [restaurantsByCuisine] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [restaurantsByCuisine] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [restaurantsByCuisine] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [restaurantsByCuisine] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [restaurantsByCuisine] SET  MULTI_USER 
GO
ALTER DATABASE [restaurantsByCuisine] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [restaurantsByCuisine] SET DB_CHAINING OFF 
GO
ALTER DATABASE [restaurantsByCuisine] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [restaurantsByCuisine] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [restaurantsByCuisine] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [restaurantsByCuisine] SET QUERY_STORE = OFF
GO
USE [restaurantsByCuisine]
GO
/****** Object:  Table [dbo].[cuisines]    Script Date: 2/24/2016 6:18:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cuisines](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cuisine_type] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[restaurants]    Script Date: 2/24/2016 6:18:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[restaurants](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[cuisine_id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [restaurantsByCuisine] SET  READ_WRITE 
GO
