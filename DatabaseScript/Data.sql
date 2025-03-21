USE [master]
GO
/****** Object:  Database [QuanLyQuanCafe]    Script Date: 20/03/2025 11:34:44 ******/
CREATE DATABASE [QuanLyQuanCafe]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuanLyQuanCafe', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\QuanLyQuanCafe.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QuanLyQuanCafe_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\QuanLyQuanCafe_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [QuanLyQuanCafe] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanLyQuanCafe].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuanLyQuanCafe] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuanLyQuanCafe] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuanLyQuanCafe] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QuanLyQuanCafe] SET  MULTI_USER 
GO
ALTER DATABASE [QuanLyQuanCafe] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuanLyQuanCafe] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuanLyQuanCafe] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QuanLyQuanCafe] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [QuanLyQuanCafe] SET QUERY_STORE = ON
GO
ALTER DATABASE [QuanLyQuanCafe] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [QuanLyQuanCafe]
GO
/****** Object:  Table [dbo].[ACCOUNT]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACCOUNT](
	[Username] [varchar](100) NOT NULL,
	[DisplayName] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[AccountType] [varchar](50) NOT NULL,
	[DateOfBirth] [date] NULL,
	[Email] [nvarchar](100) NULL,
	[WorkBegin] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BILL]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BILL](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TimeCheckIn] [datetime] NOT NULL,
	[TimeCheckOut] [datetime] NULL,
	[TableID] [int] NOT NULL,
	[Status] [bit] NULL,
	[Discount] [int] NULL,
	[TotalPrice] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BILL_INFOMATION]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BILL_INFOMATION](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BillID] [int] NOT NULL,
	[DrinkID] [int] NOT NULL,
	[Count] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DRINK_CATEGORY]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DRINK_CATEGORY](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DRINKS]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DRINKS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[DrinkCategoryID] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[ImagePath] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[History]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[History](
	[ID] [int] NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[Count] [int] NULL,
	[Unit] [nvarchar](10) NOT NULL,
	[Price] [float] NOT NULL,
	[TimeImport] [date] NULL,
	[DisplayName] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PRODUCT]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCT](
	[ID] [int] IDENTITY(101,1) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[Count] [int] NULL,
	[Unit] [nvarchar](10) NOT NULL,
	[Price] [float] NOT NULL,
	[TimeImport] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PRODUCT_ACCOUNT]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCT_ACCOUNT](
	[IDStock] [int] NOT NULL,
	[IDAccount] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IDStock] ASC,
	[IDAccount] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TABLES]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TABLES](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TableNumber] [nvarchar](100) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ACCOUNT] ([Username], [DisplayName], [Password], [AccountType], [DateOfBirth], [Email], [WorkBegin]) VALUES (N'ADMIN1000', N'Nhật Trường', N'fUkDHoTmAtAI96ytq7wPptGQfLvL07uGz1RoJ1RObw7VFL2r', N'ADMIN', CAST(N'2004-05-10' AS Date), N'netruong707@gmail.com', CAST(N'2024-10-10' AS Date))
INSERT [dbo].[ACCOUNT] ([Username], [DisplayName], [Password], [AccountType], [DateOfBirth], [Email], [WorkBegin]) VALUES (N'ThanhMom123', N'Thành', N'uS/mxzJX+phPU9oU7x+MmSgNG0RVx5lv6LOehyHX9kZBILhv', N'STAFF', CAST(N'2004-07-10' AS Date), N'thanhlai454@gmail.com', CAST(N'2025-03-17' AS Date))
GO
SET IDENTITY_INSERT [dbo].[BILL] ON 

INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (66, CAST(N'2024-08-26T10:20:56.660' AS DateTime), CAST(N'2024-08-26T11:19:17.870' AS DateTime), 2, 1, 50, 12500)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (69, CAST(N'2024-08-26T11:49:02.783' AS DateTime), CAST(N'2024-08-26T11:49:15.707' AS DateTime), 6, 1, 0, 35000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (70, CAST(N'2024-08-26T11:54:53.383' AS DateTime), CAST(N'2024-08-26T14:26:59.297' AS DateTime), 30, 1, 0, 30000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (71, CAST(N'2024-08-26T11:55:02.370' AS DateTime), CAST(N'2024-08-26T13:58:39.130' AS DateTime), 6, 1, 0, 130000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (72, CAST(N'2024-08-26T11:55:37.787' AS DateTime), CAST(N'2024-08-26T11:55:51.853' AS DateTime), 2, 1, 90, 3000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (73, CAST(N'2024-08-26T14:26:38.363' AS DateTime), CAST(N'2024-08-26T16:40:25.487' AS DateTime), 19, 1, 0, 40000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (74, CAST(N'2024-08-26T14:35:02.200' AS DateTime), CAST(N'2024-08-26T16:38:00.590' AS DateTime), 9, 1, 0, 60000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (75, CAST(N'2024-08-26T16:37:43.393' AS DateTime), CAST(N'2024-08-26T21:03:03.797' AS DateTime), 3, 1, 0, 140000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (76, CAST(N'2024-08-26T21:02:56.647' AS DateTime), CAST(N'2024-08-26T21:21:24.050' AS DateTime), 29, 1, 0, 70000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (77, CAST(N'2024-08-26T21:03:08.440' AS DateTime), CAST(N'2024-08-26T21:20:41.667' AS DateTime), 9, 1, 0, 115000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (78, CAST(N'2024-08-26T21:03:27.963' AS DateTime), CAST(N'2024-08-26T21:20:53.967' AS DateTime), 10, 1, 0, 65000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (79, CAST(N'2024-08-26T21:03:41.357' AS DateTime), CAST(N'2024-08-26T21:20:28.717' AS DateTime), 2, 1, 0, 80000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (80, CAST(N'2024-08-26T21:58:46.517' AS DateTime), CAST(N'2024-08-26T23:12:52.490' AS DateTime), 5, 1, 0, 40000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (81, CAST(N'2024-08-26T21:58:54.160' AS DateTime), CAST(N'2024-08-26T23:13:26.913' AS DateTime), 26, 1, 0, 70000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (82, CAST(N'2024-08-26T21:59:00.523' AS DateTime), CAST(N'2024-08-26T23:13:04.453' AS DateTime), 10, 1, 0, 60000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (83, CAST(N'2024-08-26T21:59:09.500' AS DateTime), CAST(N'2024-08-26T23:13:21.900' AS DateTime), 25, 1, 0, 45000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (84, CAST(N'2024-08-26T21:59:40.013' AS DateTime), CAST(N'2024-08-26T23:13:10.690' AS DateTime), 19, 1, 0, 105000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (85, CAST(N'2024-08-27T10:54:20.973' AS DateTime), CAST(N'2024-08-27T23:13:39.760' AS DateTime), 34, 1, 0, 105000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (86, CAST(N'2024-08-27T10:54:43.457' AS DateTime), CAST(N'2024-08-27T12:10:16.180' AS DateTime), 2, 1, 0, 115000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (87, CAST(N'2024-08-27T10:55:10.837' AS DateTime), CAST(N'2024-08-27T11:36:31.460' AS DateTime), 6, 1, 0, 85000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (88, CAST(N'2024-08-27T10:58:31.287' AS DateTime), CAST(N'2024-08-27T12:25:46.897' AS DateTime), 19, 1, 0, 105000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (89, CAST(N'2024-08-27T11:01:27.063' AS DateTime), CAST(N'2024-08-27T12:25:16.840' AS DateTime), 12, 1, 0, 35000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (90, CAST(N'2024-08-27T11:01:32.460' AS DateTime), CAST(N'2024-08-27T16:12:01.233' AS DateTime), 2, 1, 0, 70000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (91, CAST(N'2024-08-27T12:25:31.083' AS DateTime), CAST(N'2024-08-27T15:21:22.723' AS DateTime), 26, 1, 0, 115000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (92, CAST(N'2024-08-27T15:21:04.833' AS DateTime), CAST(N'2024-08-27T23:07:54.920' AS DateTime), 7, 1, 0, 115000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (94, CAST(N'2024-08-27T15:33:12.190' AS DateTime), CAST(N'2024-08-27T21:27:49.233' AS DateTime), 15, 1, 0, 60000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (96, CAST(N'2024-08-27T16:12:14.550' AS DateTime), CAST(N'2024-08-27T17:20:22.193' AS DateTime), 5, 1, 0, 95000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (97, CAST(N'2024-08-27T21:29:43.083' AS DateTime), CAST(N'2024-08-27T23:13:51.433' AS DateTime), 25, 1, 20, 120000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (100, CAST(N'2024-08-28T12:02:48.647' AS DateTime), CAST(N'2024-08-28T23:36:28.327' AS DateTime), 7, 1, 0, 60000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (101, CAST(N'2024-08-28T17:22:07.980' AS DateTime), CAST(N'2024-08-28T23:36:32.347' AS DateTime), 26, 1, 0, 40000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (102, CAST(N'2024-08-29T09:21:38.523' AS DateTime), CAST(N'2024-08-29T11:07:58.473' AS DateTime), 25, 1, 100, 0)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (103, CAST(N'2024-08-29T09:33:35.983' AS DateTime), CAST(N'2024-08-29T11:07:28.580' AS DateTime), 16, 1, 0, 85000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (104, CAST(N'2024-08-29T10:01:07.827' AS DateTime), CAST(N'2024-08-29T10:15:30.780' AS DateTime), 2, 1, 10, 94500)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (105, CAST(N'2024-08-29T15:05:44.900' AS DateTime), CAST(N'2024-08-29T23:12:30.970' AS DateTime), 3, 1, 0, 60000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (106, CAST(N'2024-08-30T11:05:56.277' AS DateTime), NULL, 12, 0, 0, NULL)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (107, CAST(N'2024-08-30T13:59:36.130' AS DateTime), CAST(N'2024-08-31T10:16:41.210' AS DateTime), 3, 1, 0, 110000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (108, CAST(N'2024-08-30T13:59:43.490' AS DateTime), CAST(N'2024-08-30T18:19:32.523' AS DateTime), 20, 1, 0, 85000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (109, CAST(N'2024-08-31T10:17:46.313' AS DateTime), CAST(N'2024-08-31T10:17:49.353' AS DateTime), 16, 1, 0, 40000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (110, CAST(N'2024-08-31T10:27:51.853' AS DateTime), CAST(N'2024-09-08T22:45:45.260' AS DateTime), 8, 1, 0, 55000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (111, CAST(N'2024-08-31T10:28:10.617' AS DateTime), CAST(N'2024-09-03T22:42:29.307' AS DateTime), 3, 1, 0, 105000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (112, CAST(N'2024-09-03T22:32:00.033' AS DateTime), CAST(N'2024-09-08T22:56:44.800' AS DateTime), 30, 1, 20, 76000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (113, CAST(N'2024-09-23T15:17:02.027' AS DateTime), CAST(N'2024-09-23T21:09:21.387' AS DateTime), 11, 1, 0, 70000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (114, CAST(N'2024-09-23T16:49:07.850' AS DateTime), CAST(N'2024-09-25T00:22:51.597' AS DateTime), 25, 1, 0, 60000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (115, CAST(N'2024-09-24T11:27:17.543' AS DateTime), CAST(N'2024-09-25T17:08:07.133' AS DateTime), 16, 1, 10, 90000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (116, CAST(N'2024-09-25T20:59:46.820' AS DateTime), CAST(N'2024-09-27T23:46:36.093' AS DateTime), 11, 1, 0, 95000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (118, CAST(N'2024-09-27T16:55:11.057' AS DateTime), CAST(N'2024-10-07T15:13:05.977' AS DateTime), 9, 1, 0, 95000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (119, CAST(N'2024-09-27T23:48:01.323' AS DateTime), CAST(N'2024-09-28T22:31:32.430' AS DateTime), 7, 1, 0, 160000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (120, CAST(N'2024-09-28T10:47:55.107' AS DateTime), CAST(N'2024-10-10T17:01:44.503' AS DateTime), 3, 1, 0, 165000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (121, CAST(N'2024-09-28T13:19:11.053' AS DateTime), CAST(N'2024-10-08T17:40:59.087' AS DateTime), 2, 1, 0, 100000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (127, CAST(N'2025-03-12T16:17:53.093' AS DateTime), CAST(N'2025-03-14T22:39:43.460' AS DateTime), 6, 1, 0, 185000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (128, CAST(N'2025-03-12T22:12:34.227' AS DateTime), CAST(N'2025-03-15T19:26:39.260' AS DateTime), 29, 1, 0, 80000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (129, CAST(N'2025-03-12T22:12:47.660' AS DateTime), CAST(N'2025-03-15T19:26:43.043' AS DateTime), 17, 1, 0, 100000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (130, CAST(N'2025-03-12T23:24:27.103' AS DateTime), CAST(N'2025-03-12T23:24:39.340' AS DateTime), 4, 1, 0, 40000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (131, CAST(N'2025-03-15T10:12:12.180' AS DateTime), CAST(N'2025-03-15T19:26:30.667' AS DateTime), 3, 1, 0, 35000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (132, CAST(N'2025-03-15T10:12:49.830' AS DateTime), CAST(N'2025-03-15T19:26:34.443' AS DateTime), 5, 1, 0, 45000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (133, CAST(N'2025-03-16T17:30:42.323' AS DateTime), CAST(N'2025-03-17T10:55:59.427' AS DateTime), 33, 1, 0, 35000)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (135, CAST(N'2025-03-20T10:09:06.353' AS DateTime), NULL, 29, 0, 0, NULL)
INSERT [dbo].[BILL] ([ID], [TimeCheckIn], [TimeCheckOut], [TableID], [Status], [Discount], [TotalPrice]) VALUES (136, CAST(N'2025-03-20T10:09:26.810' AS DateTime), CAST(N'2025-03-20T10:52:10.910' AS DateTime), 17, 1, 0, 60000)
SET IDENTITY_INSERT [dbo].[BILL] OFF
GO
SET IDENTITY_INSERT [dbo].[BILL_INFOMATION] ON 

INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (99, 66, 53, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (103, 69, 71, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (104, 70, 16, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (105, 71, 22, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (106, 71, 24, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (107, 72, 54, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (108, 73, 13, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (109, 74, 53, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (110, 74, 55, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (111, 75, 62, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (112, 75, 54, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (113, 76, 38, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (114, 77, 43, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (115, 77, 55, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (116, 78, 77, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (117, 78, 61, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (118, 79, 18, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (119, 80, 61, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (120, 81, 39, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (121, 82, 69, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (122, 83, 48, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (123, 84, 55, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (124, 84, 56, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (125, 85, 53, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (126, 86, 55, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (127, 86, 54, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (128, 87, 22, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (129, 88, 61, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (130, 86, 73, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (131, 88, 62, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (132, 88, 75, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (133, 87, 68, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (134, 89, 72, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (135, 90, 72, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (136, 90, 70, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (137, 91, 13, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (138, 91, 78, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (139, 85, 68, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (140, 92, 13, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (141, 85, 38, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (142, 92, 78, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (143, 92, 61, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (144, 94, 54, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (146, 96, 39, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (147, 96, 53, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (148, 97, 55, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (150, 97, 61, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (152, 100, 53, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (153, 100, 55, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (154, 101, 61, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (155, 102, 53, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (156, 103, 23, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (157, 103, 61, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (158, 104, 53, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (159, 104, 54, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (160, 104, 73, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (161, 105, 54, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (162, 107, 61, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (163, 107, 71, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (164, 108, 22, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (165, 108, 48, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (166, 109, 13, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (167, 110, 69, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (168, 110, 76, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (169, 111, 55, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (170, 111, 56, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (171, 112, 53, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (172, 112, 55, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (173, 113, 13, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (174, 113, 54, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (175, 114, 53, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (176, 114, 38, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (177, 115, 69, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (178, 115, 59, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (179, 116, 56, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (180, 116, 54, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (181, 118, 18, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (182, 118, 54, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (183, 118, 53, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (184, 119, 55, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (185, 119, 72, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (186, 119, 70, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (187, 119, 73, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (188, 120, 24, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (189, 120, 83, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (190, 120, 73, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (191, 121, 54, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (192, 121, 55, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (193, 121, 58, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (195, 127, 53, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (197, 127, 18, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (198, 127, 53, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (199, 127, 23, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (201, 128, 15, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (202, 128, 39, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (203, 129, 70, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (204, 129, 54, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (205, 130, 62, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (206, 132, 15, 1)
GO
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (207, 131, 70, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (208, 133, 78, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (209, 135, 53, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (210, 135, 13, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (211, 136, 69, 2)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (212, 135, 77, 1)
INSERT [dbo].[BILL_INFOMATION] ([ID], [BillID], [DrinkID], [Count]) VALUES (213, 135, 38, 1)
SET IDENTITY_INSERT [dbo].[BILL_INFOMATION] OFF
GO
SET IDENTITY_INSERT [dbo].[DRINK_CATEGORY] ON 

INSERT [dbo].[DRINK_CATEGORY] ([ID], [Name]) VALUES (4, N'Nước ép')
INSERT [dbo].[DRINK_CATEGORY] ([ID], [Name]) VALUES (5, N'Trà sữa')
INSERT [dbo].[DRINK_CATEGORY] ([ID], [Name]) VALUES (7, N'Trà')
INSERT [dbo].[DRINK_CATEGORY] ([ID], [Name]) VALUES (10, N'Cà phê')
INSERT [dbo].[DRINK_CATEGORY] ([ID], [Name]) VALUES (11, N'Sinh tố')
INSERT [dbo].[DRINK_CATEGORY] ([ID], [Name]) VALUES (12, N'Sữa chua')
INSERT [dbo].[DRINK_CATEGORY] ([ID], [Name]) VALUES (13, N'Ăn vặt')
SET IDENTITY_INSERT [dbo].[DRINK_CATEGORY] OFF
GO
SET IDENTITY_INSERT [dbo].[DRINKS] ON 

INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (13, N'Nước ép cam', 4, 40000, N'Images\Uploads\camep_OCMA4.jpg')
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (14, N'Nước ép cam dứa', 4, 45000, N'Images\Uploads\nuoc-ep-cam-dua-thom-ngon-tai-nha4.jpg')
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (15, N'Nước ép cam cà rốt', 4, 45000, N'Images\Uploads\nuoc-ep-cam-ca-rot-4.jpg')
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (16, N'Nước ép chanh tươi', 4, 30000, N'Images\Uploads\cong-thuc-pha-nuoc-chanh-vua-dep-vua-ngon-nhin-chang-no-uong-4.jpg')
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (18, N'Nước ép dưa hấu', 4, 40000, N'Images\Uploads\nuoc-ep-dua-hau-giai-nhiet4.jpg')
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (22, N'Trà sữa thái', 5, 40000, N'Images\Uploads\cach-lam-tra-sua-thai-xanh5.jpg')
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (23, N'Matcha đá xay', 5, 45000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (24, N'Matcha kem trứng', 5, 50000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (38, N'Trà nhài ổi hồng', 7, 35000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (39, N'Trà sen vàng lá nếp', 7, 35000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (40, N'Trà mạn', 7, 25000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (41, N'Trà cam quế đào', 7, 35000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (43, N'Trà Olong vải', 7, 40000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (44, N'Trà vải hoa hồng', 7, 40000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (47, N'Trà đào cam xả', 7, 35000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (48, N'Trà sữa trân trâu đường đen', 5, 45000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (50, N'Nước ép chanh leo ', 4, 35000, N'Images\Uploads\co-the-se-nhan-duoc-nhung-loi-ich-vang-khi-su-dung-chanh-leo-vao-mua-he-4.jpg')
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (53, N'Cà phê đen', 10, 25000, N'Images\Uploads\ca-phe-den-10.jpg')
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (54, N'Cà phê sữa', 10, 30000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (55, N'Bạc xỉu', 10, 35000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (56, N'Cacao sữa', 10, 35000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (57, N'Cà phê kem muối', 10, 35000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (58, N'Cà phê kem trứng', 10, 35000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (59, N'Cà phê cốt dừa', 10, 40000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (60, N'Cà phê chai', 10, 99000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (61, N'Sinh tố bơ', 11, 40000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (62, N'Sinh tố mãng cầu', 11, 40000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (63, N'Sinh tố dâu tây', 11, 40000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (65, N'Sinh tố xoài', 11, 40000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (66, N'Sinh tố chanh leo', 11, 40000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (67, N'Cappuccino', 10, 35000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (68, N'Trà sữa nướng', 5, 45000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (69, N'Sữa chua thạch', 12, 30000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (70, N'Sữa chua cà phê', 12, 35000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (71, N'Sữa chua hoa quả', 12, 35000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (72, N'Sữa chua việt quất', 12, 35000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (73, N'Hướng dương', 13, 20000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (74, N'Bò khô', 13, 30000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (75, N'Khô gà lá chanh', 13, 25000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (76, N'Khoai tây lắc phô mai', 13, 25000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (77, N'Sữa chua dầm đá', 12, 25000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (78, N'Nước ép ổi', 4, 35000, N'Images\Uploads\thanh-pham-4.jpg')
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (79, N'Trà cúc mật ong', 7, 40000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (83, N'Sinh tố thanh long', 11, 45000, NULL)
INSERT [dbo].[DRINKS] ([ID], [Name], [DrinkCategoryID], [Price], [ImagePath]) VALUES (84, N'Sinh tố Kiwi', 11, 40000, NULL)
SET IDENTITY_INSERT [dbo].[DRINKS] OFF
GO
INSERT [dbo].[History] ([ID], [ProductName], [Count], [Unit], [Price], [TimeImport], [DisplayName]) VALUES (110, N'Sữa đặc', 7, N'Thùng', 105000, CAST(N'2024-09-25' AS Date), N'Thành')
INSERT [dbo].[History] ([ID], [ProductName], [Count], [Unit], [Price], [TimeImport], [DisplayName]) VALUES (111, N'Sữa tươi Vinamilk', 11, N'Thùng', 95000, CAST(N'2024-09-25' AS Date), N'Thành')
INSERT [dbo].[History] ([ID], [ProductName], [Count], [Unit], [Price], [TimeImport], [DisplayName]) VALUES (117, N'Cà phê', 5, N'Túi', 97000, CAST(N'2024-09-26' AS Date), N'Thành')
INSERT [dbo].[History] ([ID], [ProductName], [Count], [Unit], [Price], [TimeImport], [DisplayName]) VALUES (122, N'Cam vàng', 5, N'Kg', 105000, CAST(N'2024-09-28' AS Date), N'Thành')
INSERT [dbo].[History] ([ID], [ProductName], [Count], [Unit], [Price], [TimeImport], [DisplayName]) VALUES (123, N'Cam vàng', 2, N'Kg', 105000, CAST(N'2024-09-28' AS Date), N'Thành')
INSERT [dbo].[History] ([ID], [ProductName], [Count], [Unit], [Price], [TimeImport], [DisplayName]) VALUES (124, N'Sữa đặc', 6, N'Thùng', 93000, CAST(N'2024-09-28' AS Date), N'Thành')
INSERT [dbo].[History] ([ID], [ProductName], [Count], [Unit], [Price], [TimeImport], [DisplayName]) VALUES (125, N'Sữa chua', 7, N'Thùng', 104000, CAST(N'2024-09-28' AS Date), N'Thành')
INSERT [dbo].[History] ([ID], [ProductName], [Count], [Unit], [Price], [TimeImport], [DisplayName]) VALUES (131, N'Sữa đặc', 10, N'Thùng', 205000, CAST(N'2025-03-16' AS Date), N'Vũ Kim Tú')
INSERT [dbo].[History] ([ID], [ProductName], [Count], [Unit], [Price], [TimeImport], [DisplayName]) VALUES (132, N'Cam vàng', 5, N'Kg', 110000, CAST(N'2025-03-17' AS Date), N'Lương Nhật Trường')
INSERT [dbo].[History] ([ID], [ProductName], [Count], [Unit], [Price], [TimeImport], [DisplayName]) VALUES (133, N'Sữa Chua', 6, N'Thùng', 93000, CAST(N'2025-03-20' AS Date), N'Nhật Trường')
GO
SET IDENTITY_INSERT [dbo].[PRODUCT] ON 

INSERT [dbo].[PRODUCT] ([ID], [ProductName], [Count], [Unit], [Price], [TimeImport]) VALUES (125, N'Sữa tươi', 4, N'Thùng', 78000, CAST(N'2024-09-28' AS Date))
INSERT [dbo].[PRODUCT] ([ID], [ProductName], [Count], [Unit], [Price], [TimeImport]) VALUES (127, N'Sữa tươi', 5, N'Thùng', 82000, CAST(N'2024-09-28' AS Date))
INSERT [dbo].[PRODUCT] ([ID], [ProductName], [Count], [Unit], [Price], [TimeImport]) VALUES (128, N'Cam vàng', 5, N'Kg', 105000, CAST(N'2024-09-28' AS Date))
INSERT [dbo].[PRODUCT] ([ID], [ProductName], [Count], [Unit], [Price], [TimeImport]) VALUES (130, N'Bơ', 5, N'Kg', 24000, CAST(N'2024-10-15' AS Date))
INSERT [dbo].[PRODUCT] ([ID], [ProductName], [Count], [Unit], [Price], [TimeImport]) VALUES (131, N'Sữa đặc', 6, N'Thùng', 205000, CAST(N'2025-03-16' AS Date))
INSERT [dbo].[PRODUCT] ([ID], [ProductName], [Count], [Unit], [Price], [TimeImport]) VALUES (132, N'Cam vàng', 5, N'Kg', 110000, CAST(N'2025-03-17' AS Date))
INSERT [dbo].[PRODUCT] ([ID], [ProductName], [Count], [Unit], [Price], [TimeImport]) VALUES (133, N'Sữa Chua', 6, N'Thùng', 93000, CAST(N'2025-03-20' AS Date))
SET IDENTITY_INSERT [dbo].[PRODUCT] OFF
GO
INSERT [dbo].[PRODUCT_ACCOUNT] ([IDStock], [IDAccount]) VALUES (125, N'ADMIN1000')
INSERT [dbo].[PRODUCT_ACCOUNT] ([IDStock], [IDAccount]) VALUES (127, N'ADMIN1000')
INSERT [dbo].[PRODUCT_ACCOUNT] ([IDStock], [IDAccount]) VALUES (128, N'ADMIN1000')
INSERT [dbo].[PRODUCT_ACCOUNT] ([IDStock], [IDAccount]) VALUES (130, N'ADMIN1000')
INSERT [dbo].[PRODUCT_ACCOUNT] ([IDStock], [IDAccount]) VALUES (131, N'ADMIN1000')
INSERT [dbo].[PRODUCT_ACCOUNT] ([IDStock], [IDAccount]) VALUES (132, N'ADMIN1000')
INSERT [dbo].[PRODUCT_ACCOUNT] ([IDStock], [IDAccount]) VALUES (133, N'ADMIN1000')
GO
SET IDENTITY_INSERT [dbo].[TABLES] ON 

INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (2, N'Bàn 2', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (3, N'Bàn 3', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (4, N'Bàn 22', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (5, N'Bàn 32', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (6, N'Bàn 6', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (7, N'Bàn 7', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (8, N'Bàn 8', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (9, N'Bàn 9', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (10, N'Bàn 10', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (11, N'Bàn 11', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (12, N'Bàn 12', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (13, N'Bàn 13', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (15, N'Bàn 15', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (16, N'Bàn 16', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (17, N'Bàn 17', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (18, N'Bàn 18', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (19, N'Bàn 19', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (20, N'Bàn 20', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (21, N'Bàn 21', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (22, N'Bàn 23', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (24, N'Bàn 24', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (25, N'Bàn 25', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (26, N'Bàn 26', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (27, N'Bàn 27', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (28, N'Bàn 28', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (29, N'Bàn 29', N'Có khách')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (30, N'Bàn 30', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (32, N'Bàn 1', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (33, N'Bàn 35', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (34, N'Bàn 34', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (37, N'Bàn 36', N'Trống')
INSERT [dbo].[TABLES] ([ID], [TableNumber], [Status]) VALUES (38, N'Bàn 37', N'Trống')
SET IDENTITY_INSERT [dbo].[TABLES] OFF
GO
ALTER TABLE [dbo].[ACCOUNT] ADD  DEFAULT ('???') FOR [Username]
GO
ALTER TABLE [dbo].[ACCOUNT] ADD  DEFAULT (getdate()) FOR [WorkBegin]
GO
ALTER TABLE [dbo].[BILL] ADD  DEFAULT (getdate()) FOR [TimeCheckIn]
GO
ALTER TABLE [dbo].[BILL] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[BILL] ADD  DEFAULT ((0)) FOR [Discount]
GO
ALTER TABLE [dbo].[BILL_INFOMATION] ADD  DEFAULT ((0)) FOR [Count]
GO
ALTER TABLE [dbo].[PRODUCT] ADD  DEFAULT ((0)) FOR [Count]
GO
ALTER TABLE [dbo].[TABLES] ADD  DEFAULT (N'TRỐNG') FOR [Status]
GO
ALTER TABLE [dbo].[BILL]  WITH CHECK ADD FOREIGN KEY([TableID])
REFERENCES [dbo].[TABLES] ([ID])
GO
ALTER TABLE [dbo].[BILL]  WITH CHECK ADD FOREIGN KEY([TableID])
REFERENCES [dbo].[TABLES] ([ID])
GO
ALTER TABLE [dbo].[BILL_INFOMATION]  WITH CHECK ADD FOREIGN KEY([BillID])
REFERENCES [dbo].[BILL] ([ID])
GO
ALTER TABLE [dbo].[BILL_INFOMATION]  WITH CHECK ADD FOREIGN KEY([BillID])
REFERENCES [dbo].[BILL] ([ID])
GO
ALTER TABLE [dbo].[BILL_INFOMATION]  WITH CHECK ADD FOREIGN KEY([DrinkID])
REFERENCES [dbo].[DRINKS] ([ID])
GO
ALTER TABLE [dbo].[BILL_INFOMATION]  WITH CHECK ADD FOREIGN KEY([DrinkID])
REFERENCES [dbo].[DRINKS] ([ID])
GO
ALTER TABLE [dbo].[DRINKS]  WITH CHECK ADD FOREIGN KEY([DrinkCategoryID])
REFERENCES [dbo].[DRINK_CATEGORY] ([ID])
GO
ALTER TABLE [dbo].[DRINKS]  WITH CHECK ADD FOREIGN KEY([DrinkCategoryID])
REFERENCES [dbo].[DRINK_CATEGORY] ([ID])
GO
ALTER TABLE [dbo].[PRODUCT_ACCOUNT]  WITH CHECK ADD FOREIGN KEY([IDAccount])
REFERENCES [dbo].[ACCOUNT] ([Username])
GO
ALTER TABLE [dbo].[PRODUCT_ACCOUNT]  WITH CHECK ADD FOREIGN KEY([IDAccount])
REFERENCES [dbo].[ACCOUNT] ([Username])
GO
ALTER TABLE [dbo].[PRODUCT_ACCOUNT]  WITH CHECK ADD FOREIGN KEY([IDStock])
REFERENCES [dbo].[PRODUCT] ([ID])
GO
ALTER TABLE [dbo].[PRODUCT_ACCOUNT]  WITH CHECK ADD FOREIGN KEY([IDStock])
REFERENCES [dbo].[PRODUCT] ([ID])
GO
/****** Object:  StoredProcedure [dbo].[SP_AddAccount]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_AddAccount]
@USERNAME varchar(100), @DISPLAYNAME NVARCHAR(100), @ACCOUNTTYPE VARCHAR(50), @BIRTH DATE, @EMAIL VARCHAR(100), @PASSWORD NVARCHAR(MAX)
AS
BEGIN	
     DECLARE @CNT INT = 0
	 SELECT @CNT = COUNT(*) FROM ACCOUNT WHERE Username = @USERNAME AND DisplayName = @DISPLAYNAME
	 IF @CNT = 0
	 INSERT INTO ACCOUNT (username, displayname, Password, accounttype, DateOfBirth, Email) VALUES (@USERNAME, @DISPLAYNAME, @PASSWORD, @ACCOUNTTYPE, @BIRTH, @EMAIL)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddBill]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_AddBill]
@IDTable int, @cnt int
as
begin
      if(@cnt > 0)
	  BEGIN
		   INSERT INTO BILL (TimeCheckIn, TimeCheckOut, TableID, Status)
		   VALUES(GETDATE(), NULL, @IDTable, 0)
	  END
end
GO
/****** Object:  StoredProcedure [dbo].[SP_AddOrUpdateBillinfor]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_AddOrUpdateBillinfor]
@IDBill int, @IDDrink int, @Count int
as
begin
	 DECLARE @DEM INT = 0 
	 select @DEM = COUNT(*) from BILL_INFOMATION where BillID = @IDBill and DrinkID = @IDDrink

	 --Kiểm tra xem nếu tồn tại hóa đơn thì cập nhật, ngược lại sẽ thêm mới
	 if @DEM > 0 
	 begin
	     DECLARE @CNT INT, @CurrentCount int --Lấy ra số lượng hóa đơn trước và sau khi update

		 SELECT @CNT = COUNT FROM BILL_INFOMATION where BillID = @IDBill and DrinkID = @IDDrink

		 update BILL_INFOMATION set Count = @CNT + @Count where BillID = @IDBill and DrinkID = @IDDrink

		 select @CurrentCount = count from BILL_INFOMATION where BillID = @IDBill and DrinkID = @IDDrink
		 --Nếu số lượng hóa đơn sau khi cập nhật nhỏ hơn hoặc bằng 0 thì tiến hành xóa dữ liệu
		 if @CurrentCount <= 0
			BEGIN
				 DELETE from BILL_INFOMATION where BillID = @IDBill and DrinkID = @IDDrink
				 DELETE from BILL WHERE ID = @IDBill
			END
	 end
	 else if @Count > 0
	 INSERT INTO BILL_INFOMATION VALUES(@IDBill, @IDDrink, @Count)
end

GO
/****** Object:  StoredProcedure [dbo].[SP_AddProduct]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_AddProduct] @NAME nvarchar(50), @CNT int, @UNIT nvarchar(10), @PRICE float, @TIME date, @USERID varchar(100)
as
begin
	INSERT INTO PRODUCT VALUES (@NAME, @CNT, @UNIT, @PRICE, @TIME)
	DECLARE @ID INT
	SELECT @ID = MAX(ID) FROM PRODUCT
	INSERT INTO PRODUCT_ACCOUNT VALUES (@ID, @USERID)

	DECLARE @DISPLAYNAME NVARCHAR(100)
	SELECT @DISPLAYNAME = DisplayName from ACCOUNT WHERE Username = @USERID
	INSERT INTO History VALUES (@ID, @NAME, @CNT, @UNIT, @PRICE, @TIME, @DISPLAYNAME)
end
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteAccount]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_DeleteAccount] @USERNAME varchar(100), @DISPLAYNAME nvarchar(100)
as
begin
	DELETE PRODUCT_ACCOUNT WHERE IDAccount = @USERNAME
	DELETE ACCOUNT WHERE Username = @USERNAME AND Displayname = @DISPLAYNAME
end
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteCategory]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DeleteCategory]
@ID int, @NAME NVARCHAR(50)
as
begin
  
      DECLARE @CHK INT = 0
      SELECT @CHK = COUNT(*) FROM DRINK_CATEGORY WHERE ID = @ID AND Name = @NAME

	  IF @CHK > 0
	  BEGIN
		--Lấy ra các iddrink cần xóa từ id category
		SELECT ID INTO DrinkIDList FROM DRINKS WHERE DrinkCategoryID = @ID

		--Lấy ra danh sách các billid chứa iddrink cần xóa
		SELECT BILLID INTO BillIDList FROM BILL_INFOMATION WHERE DrinkID IN (SELECT * FROM DrinkIDList)

		--Xóa 
		DELETE BILL_INFOMATION WHERE DrinkID IN (SELECT * FROM DrinkIDList)
		DELETE DRINKS WHERE DrinkCategoryID = @ID
		DELETE DRINK_CATEGORY WHERE ID = @ID

		--Cập nhật lại danh sách billid sau khi xóa billInfor
		--tất cả những hóa đơn có id đồ uống ko nằm trong danh sách cần xóa sẽ bị lọc ra khỏi billidlist
		SELECT BillID INTO Cur_BillIDList FROM BILL_INFOMATION WHERE BillID IN (SELECT * FROM BillIDList)
		DELETE BillIDList WHERE BillID IN (SELECT * FROM Cur_BillIDList)


		--Lấy ra danh sách bàn thuộc hóa đơn chuẩn bị xóa
		SELECT TableID INTO IDTableList FROM BILL WHERE ID IN (SELECT * FROM BillIDList)

		DELETE BILL WHERE ID IN (SELECT * FROM BillIDList) AND Status = 0

		--Cập nhật trạng thái các bàn nằm trong danh sách
		UPDATE TABLES SET Status = N'Trống' WHERE ID IN (SELECT * FROM IDTableList)

		--Giải phóng các danh sách sau khi dùng xong
		DROP TABLE BillIDList
		DROP TABLE Cur_BillIDList
		DROP TABLE DrinkIDList
		DROP TABLE IDTableList
	END
end
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteDrink]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DeleteDrink] 
@ID int, @DRINKNAME nvarchar(100)
as
begin

     DECLARE @CHK INT = 0
	 SELECT @CHK = COUNT(*) FROM DRINKS WHERE ID = @ID AND Name = @DRINKNAME

	 IF @CHK > 0
	 BEGIN
		 --Lấy ra hóa đơn của đồ uống cần xóa
		 DECLARE @BILLID INT
		 SELECT @BILLID = BILLID FROM BILL_INFOMATION WHERE DrinkID = @ID

		 --Lấy ra số lượng đồ uống tương ứng với hóa đơn của đồ uống cần xóa
		 DECLARE @CNT INT
		 SELECT @CNT = COUNT(*) FROM BILL_INFOMATION WHERE BillID = @BILLID


		 delete BILL_INFOMATION where DrinkID = @ID
		 delete DRINKS where id = @ID


		 --Nếu như chỉ tồn tại duy nhất 1 đồ uống trên hóa đơn đó thì sẽ thực hiện xóa hóa đơn đó
		 IF @CNT <= 1
			DELETE BILL WHERE ID = @BILLID AND Status = 0
	END
end
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteProduct]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_DeleteProduct] @ID INT --, @NAME nvarchar(50), @CNT int, @UNIT nvarchar(10), @PRICE float, @TIME date, @SUPPLIER nvarchar(50), @USERID varchar(100)
AS
BEGIN
	DELETE PRODUCT_ACCOUNT WHERE IDStock = @ID
	DELETE PRODUCT WHERE ID = @ID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteSupplier]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_DeleteSupplier] @NAME NVARCHAR(50)
AS
BEGIN
	 DELETE STOCKACCOUNT WHERE SupplierName = @NAME
	 DELETE STOCK WHERE SupplierName = @NAME
	 DELETE SUPPLIERS WHERE CompanyName = @NAME
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteTable]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DeleteTable] 
@ID INT, @TABLENAME NVARCHAR(100)
AS
BEGIN
	 DECLARE @CNT INT = 0
	 SELECT @CNT = COUNT(*) FROM BILL WHERE TableID = @ID

	 IF @CNT = 0
	   DELETE TABLES WHERE ID = @ID AND TableNumber = @TABLENAME
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ExtractBill]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ExtractBill]
@ID int
as
begin
    select BillID, DRINKS.Name, BILL_INFOMATION.Count, Price, BILL_INFOMATION.Count * Price AS SUM from DRINKS join BILL_INFOMATION ON DRINKS.ID = BILL_INFOMATION.DrinkID JOIN BILL ON BILL.ID = BILL_INFOMATION.BillID WHERE TableID = @ID AND BILL.Status = 0
end
GO
/****** Object:  StoredProcedure [dbo].[SP_GetDatetimeBill]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetDatetimeBill]
@IDTABLE INT
AS
BEGIN	
	SELECT TimeCheckIn FROM BILL JOIN TABLES ON BILL.TableID = TABLES.ID WHERE TableID = @IDTABLE AND BILL.Status = 0
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetTableIDByDrinkID]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetTableIDByDrinkID]
@IDDrink int
as
begin
	    SELECT TABLES.ID 
		FROM TABLES JOIN BILL ON TABLES.ID = BILL.TableID 
		JOIN BILL_INFOMATION ON BILL.ID = BILL_INFOMATION.BillID  
		JOIN DRINKS ON BILL_INFOMATION.DrinkID = DRINKS.ID
		WHERE DRINKS.ID = @IDDrink AND BILL.Status = 0
end
GO
/****** Object:  StoredProcedure [dbo].[SP_GetTotalIncomeByMonth]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_GetTotalIncomeByMonth] @YEAR int
as
begin
	SELECT MONTH(TimeCheckOut) as Month, SUM(TotalPrice) as TotalIncome FROM BILL 
	WHERE Status = 1 AND YEAR(TimeCheckOut) = @YEAR
	GROUP BY MONTH(TimeCheckOut)
end
GO
/****** Object:  StoredProcedure [dbo].[SP_MergeTable]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_MergeTable]
@IDLeftTable int, @IDRightTable int
as
begin
	  DECLARE @IDLeftBill int, @IDRightBill int
	  SELECT @IDLeftBill = ID FROM BILL WHERE TableID = @IDLeftTable AND Status = 0
	  SELECT @IDRightBill = ID FROM BILL WHERE TableID = @IDRightTable AND Status = 0

	  UPDATE BILL_INFOMATION SET BillID = @IDRightBill WHERE BillID = @IDLeftBill
	  DELETE BILL WHERE ID = @IDLeftBill AND Status = 0

	  UPDATE TABLES SET Status = N'Trống' WHERE ID = @IDLeftTable
	  UPDATE TABLES SET Status = N'Có khách' WHERE ID = @IDRightTable
end
GO
/****** Object:  StoredProcedure [dbo].[SP_SearchBill]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_SearchBill]
@In DATETIME, @Out DATETIME, @Page int
as
begin
     --Trong đó: 10 là số dòng muốn hiển thị trên 1 trang, page là trang mà người dùng chọn
	 DECLARE @PageSelect int = 10
	 DECLARE @PageExcept int = (@Page - 1) * 10

	 SELECT BILL.ID, TableNumber, TimeCheckIn, TimeCheckOut, Discount, TotalPrice
	 INTO TEMPBILL
	 FROM TABLES JOIN BILL ON TABLES.ID = BILL.TableID 
	 where BILL.Status = 1 AND TimeCheckOut >= @In AND TimeCheckOut <= @Out

	 SELECT TOP (@PageSelect) * FROM TEMPBILL WHERE ID NOT IN (SELECT TOP (@PageExcept) ID FROM TEMPBILL)

	 DROP TABLE TEMPBILL
end
GO
/****** Object:  StoredProcedure [dbo].[SP_SupplierSearch]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_SupplierSearch] @Name nvarchar(50)
as
begin
	select CompanyName as [Tên công ty], PhoneNumber as [Số điện thoại], Address as [Địa chỉ] from SUPPLIERS WHERE CompanyName COLLATE SQL_Latin1_General_CP1_CI_AI LIKE @Name
end
GO
/****** Object:  StoredProcedure [dbo].[SP_SwapTable]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_SwapTable]
@IDLeftTable int, @IDRightTable int
as
begin
	  DECLARE @IDLeftBill int, @IDRightBill int
	  SELECT @IDLeftBill = ID FROM BILL WHERE TableID = @IDLeftTable AND Status = 0
	  SELECT @IDRightBill = ID FROM BILL WHERE TableID = @IDRightTable AND Status = 0

	  IF (@IDLeftBill IS NULL)
	  BEGIN

		    UPDATE BILL SET TableID = @IDLeftTable WHERE ID = @IDRightBill 
			
			--Cập nhật trạng thái cho 2 bàn sau khi chuyển 
			UPDATE TABLES SET Status = N'Trống' WHERE ID = @IDRightTable
			UPDATE TABLES SET Status = N'Có khách' WHERE ID = @IDLeftTable

	  END
	  ELSE IF (@IDRightBill IS NULL)
	  BEGIN

			UPDATE BILL SET TableID = @IDRightTable WHERE ID = @IDLeftBill 

			print 1
			--Cập nhật trạng thái cho 2 bàn sau khi chuyển 
			UPDATE TABLES SET Status = N'Trống' WHERE ID = @IDLeftTable
			UPDATE TABLES SET Status = N'Có khách' WHERE ID = @IDRightTable

	  END
      ELSE
	  BEGIN
			SELECT ID INTO TMPBillInfor from BILL_INFOMATION where BillID = @IDRightBill

			update BILL_INFOMATION set BillID = @IDRightBill WHERE BillID = @IDLeftBill

			update BILL_INFOMATION set BillID = @IDLeftBill WHERE ID IN (SELECT * FROM TMPBillInfor)

			DROP table TMPBillInfor
	  END
end
GO
/****** Object:  StoredProcedure [dbo].[SP_TableLoad]    Script Date: 20/03/2025 11:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_TableLoad] 
as
begin
	SELECT ID, TableNumber AS [Tên bàn], Status as [Trạng thái] FROM TABLES
end
GO
USE [master]
GO
ALTER DATABASE [QuanLyQuanCafe] SET  READ_WRITE 
GO
