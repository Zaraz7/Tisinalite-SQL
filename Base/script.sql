USE [master]
GO
/****** Object:  Database [TisinaliteDB]    Script Date: 10.06.2022 11:43:43 ******/
CREATE DATABASE [TisinaliteDB]
GO
ALTER DATABASE [TisinaliteDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TisinaliteDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TisinaliteDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TisinaliteDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TisinaliteDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TisinaliteDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TisinaliteDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [TisinaliteDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TisinaliteDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TisinaliteDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TisinaliteDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TisinaliteDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TisinaliteDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TisinaliteDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TisinaliteDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TisinaliteDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TisinaliteDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TisinaliteDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TisinaliteDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TisinaliteDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TisinaliteDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TisinaliteDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TisinaliteDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TisinaliteDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TisinaliteDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TisinaliteDB] SET  MULTI_USER 
GO
ALTER DATABASE [TisinaliteDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TisinaliteDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TisinaliteDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TisinaliteDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TisinaliteDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TisinaliteDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [TisinaliteDB] SET QUERY_STORE = OFF
GO
USE [TisinaliteDB]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 10.06.2022 11:43:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[MasterID] [int] NOT NULL,
	[Access] [varchar](10) NULL,
	[Password] [varchar](20) NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Images]    Script Date: 10.06.2022 11:43:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BinaryFile] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notes]    Script Date: 10.06.2022 11:43:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Contents] [nvarchar](max) NULL,
	[GroupID] [int] NULL,
 CONSTRAINT [PK_Notes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10.06.2022 11:43:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Login] [varchar](20) NULL,
	[Password] [varchar](20) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersOfGroups]    Script Date: 10.06.2022 11:43:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersOfGroups](
	[UserID] [int] NULL,
	[GroupID] [int] NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_UsersOfGroups] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Groups]  WITH CHECK ADD  CONSTRAINT [FK_Groups_Users] FOREIGN KEY([MasterID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Groups] CHECK CONSTRAINT [FK_Groups_Users]
GO
ALTER TABLE [dbo].[Notes]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Groups] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Groups] ([ID])
GO
ALTER TABLE [dbo].[Notes] CHECK CONSTRAINT [FK_Notes_Groups]
GO
ALTER TABLE [dbo].[UsersOfGroups]  WITH CHECK ADD  CONSTRAINT [FK_UsersOfGroups_Groups] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Groups] ([ID])
GO
ALTER TABLE [dbo].[UsersOfGroups] CHECK CONSTRAINT [FK_UsersOfGroups_Groups]
GO
ALTER TABLE [dbo].[UsersOfGroups]  WITH CHECK ADD  CONSTRAINT [FK_UsersOfGroups_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[UsersOfGroups] CHECK CONSTRAINT [FK_UsersOfGroups_Users]
GO
USE [master]
GO
ALTER DATABASE [TisinaliteDB] SET  READ_WRITE 
GO
