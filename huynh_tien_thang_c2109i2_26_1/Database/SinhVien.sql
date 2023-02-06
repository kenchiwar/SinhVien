USE [master]
GO
/****** Object:  Database [SinhVien]    Script Date: 1/31/2023 11:09:04 AM ******/
CREATE DATABASE [SinhVien]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SinhVien', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SinhVien.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SinhVien_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SinhVien_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SinhVien] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SinhVien].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SinhVien] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SinhVien] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SinhVien] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SinhVien] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SinhVien] SET ARITHABORT OFF 
GO
ALTER DATABASE [SinhVien] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SinhVien] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SinhVien] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SinhVien] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SinhVien] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SinhVien] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SinhVien] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SinhVien] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SinhVien] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SinhVien] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SinhVien] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SinhVien] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SinhVien] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SinhVien] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SinhVien] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SinhVien] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SinhVien] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SinhVien] SET RECOVERY FULL 
GO
ALTER DATABASE [SinhVien] SET  MULTI_USER 
GO
ALTER DATABASE [SinhVien] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SinhVien] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SinhVien] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SinhVien] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SinhVien] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SinhVien] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SinhVien', N'ON'
GO
ALTER DATABASE [SinhVien] SET QUERY_STORE = OFF
GO
USE [SinhVien]
GO
/****** Object:  Table [dbo].[TblCourse]    Script Date: 1/31/2023 11:09:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblCourse](
	[CouId] [int] IDENTITY(1,1) NOT NULL,
	[CouName] [nvarchar](50) NULL,
	[CouSemester] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[CouId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblExam]    Script Date: 1/31/2023 11:09:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblExam](
	[ExamId] [int] IDENTITY(1,1) NOT NULL,
	[ExamName] [nvarchar](50) NULL,
	[ExamMark] [float] NULL,
	[ExamDate] [date] NULL,
	[StuId] [int] NULL,
	[CouId] [int] NULL,
	[Comment] [text] NULL,
	[MarkPass] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ExamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblStudent]    Script Date: 1/31/2023 11:09:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblStudent](
	[StuId] [int] IDENTITY(1,1) NOT NULL,
	[StuPass] [nvarchar](50) NULL,
	[StuAdress] [nvarchar](50) NULL,
	[StuPhone] [nvarchar](50) NULL,
	[StuEmail] [nvarchar](50) NULL,
	[StuName] [nvarchar](50) NULL,
	[deptId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[StuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[TblCourse] ON 

INSERT [dbo].[TblCourse] ([CouId], [CouName], [CouSemester]) VALUES (1, N'Ko hay rồi', N'Kỳ 1 - 2022')
INSERT [dbo].[TblCourse] ([CouId], [CouName], [CouSemester]) VALUES (2, N'Ko hay rồi ta', N'Kỳ 2 - 2023')
INSERT [dbo].[TblCourse] ([CouId], [CouName], [CouSemester]) VALUES (3, N'Ko hay rồi', N'Kỳ 1 - 2022')
INSERT [dbo].[TblCourse] ([CouId], [CouName], [CouSemester]) VALUES (4, N'Ko hay rồi ta', N'Kỳ 2 - 2023')
INSERT [dbo].[TblCourse] ([CouId], [CouName], [CouSemester]) VALUES (5, N'', N'Kỳ 2-2022 ')
INSERT [dbo].[TblCourse] ([CouId], [CouName], [CouSemester]) VALUES (6, N'dfsafdsafsafsadfsfdsa', N'Kỳ 1-2024')
INSERT [dbo].[TblCourse] ([CouId], [CouName], [CouSemester]) VALUES (7, N'dfsafdsafsafsadfsfdsa', N'Học kỳ hè-2024')
INSERT [dbo].[TblCourse] ([CouId], [CouName], [CouSemester]) VALUES (8, N'Rởt đê ', N'Kỳ 1-2023')
INSERT [dbo].[TblCourse] ([CouId], [CouName], [CouSemester]) VALUES (9, N'Riot game', N'Kỳ 1-2023')
INSERT [dbo].[TblCourse] ([CouId], [CouName], [CouSemester]) VALUES (10, N' daỏ ngân hà vũ trụ', N'Kỳ 1-2023')
INSERT [dbo].[TblCourse] ([CouId], [CouName], [CouSemester]) VALUES (11, N'chans roi do', N'Học kỳ hè-2025')
INSERT [dbo].[TblCourse] ([CouId], [CouName], [CouSemester]) VALUES (12, N' Yeah a', N'Kỳ 1-2027')
SET IDENTITY_INSERT [dbo].[TblCourse] OFF
GO
SET IDENTITY_INSERT [dbo].[TblExam] ON 

INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (1, N'Hay mệt', 5, CAST(N'2020-03-03' AS Date), 1, 1, N'H?c hành ko ?n l?m', 6)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (2, N'Hay mệt', 6, CAST(N'2020-03-03' AS Date), 2, 1, N'H?c hành ko ?n l?m', 6)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (3, N'Hay mệt', 7, CAST(N'2020-03-03' AS Date), 3, 1, N'H?c hành ko ?n l?m', 6)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (4, N'Hay mệt', 8, CAST(N'2020-03-03' AS Date), 4, 1, N'H?c hành ko ?n l?m', 6)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (37, N'Rớt hết', 9, CAST(N'2023-03-16' AS Date), 6, 8, N' ', 5)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (38, N'roit met', 5, CAST(N'2023-01-27' AS Date), 6, 9, N' ', 5)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (39, N'riot ko mEt', 0, CAST(N'2023-01-27' AS Date), 1, 10, N' ', 6)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (40, N'riot ko mEt', 0, CAST(N'2023-01-27' AS Date), 2, 10, N' ', 6)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (41, N'riot ko mEt', 0, CAST(N'2023-01-27' AS Date), 3, 10, N' ', 6)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (42, N'riot ko mEt', 0, CAST(N'2023-01-27' AS Date), 4, 10, N' ', 6)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (44, N'riot ko mEt', 0, CAST(N'2023-01-27' AS Date), 6, 10, N' ', 6)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (45, N'dsafsa', 8, CAST(N'2023-01-27' AS Date), 1, 11, N' ', 4)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (53, N'dsafsa', 9, CAST(N'2023-01-27' AS Date), 10, 11, N' ', 4)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (54, N'dsafsa', 4, CAST(N'2023-01-27' AS Date), 4, 11, N' ', 4)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (56, N'dsafsa', 0, CAST(N'2023-01-27' AS Date), 3, 11, N' ', 4)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (57, N'dsafsa', 0, CAST(N'2023-01-27' AS Date), 6, 11, N' ', 4)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (58, N'dsafsa', 0, CAST(N'2023-01-27' AS Date), 2, 11, N' ', 4)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (65, N'Rớt hết', 3, CAST(N'2023-03-16' AS Date), 2, 8, N' ', 5)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (66, N'Rớt hết', 4, CAST(N'2023-03-16' AS Date), 3, 8, N' ', 5)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (67, N'Rớt hết', 6, CAST(N'2023-03-16' AS Date), 4, 8, N' ', 5)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (68, N'Rớt hết', 7, CAST(N'2023-03-16' AS Date), 9, 8, N' ', 5)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (69, N'roit met', 4, CAST(N'2023-01-27' AS Date), 3, 9, N' ', 5)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (70, N'roit met', 9, CAST(N'2023-01-27' AS Date), 4, 9, N' ', 5)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (71, N'roit met', 6, CAST(N'2023-01-27' AS Date), 11, 9, N' ', 5)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (72, N'roit met', 7, CAST(N'2023-01-27' AS Date), 9, 9, N' ', 5)
INSERT [dbo].[TblExam] ([ExamId], [ExamName], [ExamMark], [ExamDate], [StuId], [CouId], [Comment], [MarkPass]) VALUES (73, N'roit met', 8, CAST(N'2023-01-27' AS Date), 8, 9, N' ', 5)
SET IDENTITY_INSERT [dbo].[TblExam] OFF
GO
SET IDENTITY_INSERT [dbo].[TblStudent] ON 

INSERT [dbo].[TblStudent] ([StuId], [StuPass], [StuAdress], [StuPhone], [StuEmail], [StuName], [deptId]) VALUES (1, N'1003241', N'hay qua ha ', N'096784933234', N'huynhtienthang@gmal.com', N'hay ko ', 32)
INSERT [dbo].[TblStudent] ([StuId], [StuPass], [StuAdress], [StuPhone], [StuEmail], [StuName], [deptId]) VALUES (2, N'1003252', N'đường võ đức  ', N'096784933890', N'tienthang@gmal.com', N'huỳnh tiến  ', 35)
INSERT [dbo].[TblStudent] ([StuId], [StuPass], [StuAdress], [StuPhone], [StuEmail], [StuName], [deptId]) VALUES (3, N'1003263', N'hay ko  ', N'098989087332', N'hang@gmal.com', N'hay quá  ', 63)
INSERT [dbo].[TblStudent] ([StuId], [StuPass], [StuAdress], [StuPhone], [StuEmail], [StuName], [deptId]) VALUES (4, N'1003241', N'hay qua ha ', N'096784933234', N'huynhtienthang@gmal.com', N'hay ko ', 32)
INSERT [dbo].[TblStudent] ([StuId], [StuPass], [StuAdress], [StuPhone], [StuEmail], [StuName], [deptId]) VALUES (6, N'1003263', N'hay ko  ', N'098989087332', N'hang@gmal.com', N'hay quá  ', 63)
INSERT [dbo].[TblStudent] ([StuId], [StuPass], [StuAdress], [StuPhone], [StuEmail], [StuName], [deptId]) VALUES (8, N'10032635', N'Ngô đá dde', N'098989084332', N'quan@gmal.com', N'12312312  ', 23)
INSERT [dbo].[TblStudent] ([StuId], [StuPass], [StuAdress], [StuPhone], [StuEmail], [StuName], [deptId]) VALUES (9, N'1003252', N'đường võ đức f ', N'096784933890', N'tienthang@gmal.com', N'huỳnh tiến  ', 35)
INSERT [dbo].[TblStudent] ([StuId], [StuPass], [StuAdress], [StuPhone], [StuEmail], [StuName], [deptId]) VALUES (10, N'1003263', N'hay ko f ', N'098989087332', N'hang@gmal.com', N'hay quá  ', 63)
INSERT [dbo].[TblStudent] ([StuId], [StuPass], [StuAdress], [StuPhone], [StuEmail], [StuName], [deptId]) VALUES (11, N'10032635', N'Ngô đá f', N'098989084332', N'quan@gmal.com', N'12312312  ', 23)
SET IDENTITY_INSERT [dbo].[TblStudent] OFF
GO
ALTER TABLE [dbo].[TblExam]  WITH CHECK ADD  CONSTRAINT [Fk_TblExam_TblCourse] FOREIGN KEY([CouId])
REFERENCES [dbo].[TblCourse] ([CouId])
GO
ALTER TABLE [dbo].[TblExam] CHECK CONSTRAINT [Fk_TblExam_TblCourse]
GO
ALTER TABLE [dbo].[TblExam]  WITH CHECK ADD  CONSTRAINT [FK_TblExam_TblStudent] FOREIGN KEY([StuId])
REFERENCES [dbo].[TblStudent] ([StuId])
GO
ALTER TABLE [dbo].[TblExam] CHECK CONSTRAINT [FK_TblExam_TblStudent]
GO
USE [master]
GO
ALTER DATABASE [SinhVien] SET  READ_WRITE 
GO
