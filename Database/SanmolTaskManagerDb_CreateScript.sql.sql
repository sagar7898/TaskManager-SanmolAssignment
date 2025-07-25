USE [SanmolTaskManagerDb]
GO
/****** Object:  Schema [HangFire]    Script Date: 7/16/2025 9:33:11 PM ******/
CREATE SCHEMA [HangFire]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 7/16/2025 9:33:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 7/16/2025 9:33:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskItems]    Script Date: 7/16/2025 9:33:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[DueDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[Priority] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_TaskItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[AggregatedCounter]    Script Date: 7/16/2025 9:33:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[AggregatedCounter](
	[Key] [nvarchar](100) NOT NULL,
	[Value] [bigint] NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_CounterAggregated] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Counter]    Script Date: 7/16/2025 9:33:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Counter](
	[Key] [nvarchar](100) NOT NULL,
	[Value] [int] NOT NULL,
	[ExpireAt] [datetime] NULL,
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_HangFire_Counter] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Hash]    Script Date: 7/16/2025 9:33:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Hash](
	[Key] [nvarchar](100) NOT NULL,
	[Field] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[ExpireAt] [datetime2](7) NULL,
 CONSTRAINT [PK_HangFire_Hash] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Field] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = ON, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Job]    Script Date: 7/16/2025 9:33:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Job](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StateId] [bigint] NULL,
	[StateName] [nvarchar](20) NULL,
	[InvocationData] [nvarchar](max) NOT NULL,
	[Arguments] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_Job] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[JobParameter]    Script Date: 7/16/2025 9:33:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[JobParameter](
	[JobId] [bigint] NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_HangFire_JobParameter] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[JobQueue]    Script Date: 7/16/2025 9:33:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[JobQueue](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[JobId] [bigint] NOT NULL,
	[Queue] [nvarchar](50) NOT NULL,
	[FetchedAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_JobQueue] PRIMARY KEY CLUSTERED 
(
	[Queue] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[List]    Script Date: 7/16/2025 9:33:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[List](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_List] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Schema]    Script Date: 7/16/2025 9:33:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Schema](
	[Version] [int] NOT NULL,
 CONSTRAINT [PK_HangFire_Schema] PRIMARY KEY CLUSTERED 
(
	[Version] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Server]    Script Date: 7/16/2025 9:33:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Server](
	[Id] [nvarchar](200) NOT NULL,
	[Data] [nvarchar](max) NULL,
	[LastHeartbeat] [datetime] NOT NULL,
 CONSTRAINT [PK_HangFire_Server] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Set]    Script Date: 7/16/2025 9:33:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Set](
	[Key] [nvarchar](100) NOT NULL,
	[Score] [float] NOT NULL,
	[Value] [nvarchar](256) NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_Set] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = ON, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[State]    Script Date: 7/16/2025 9:33:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[State](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[JobId] [bigint] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Reason] [nvarchar](100) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[Data] [nvarchar](max) NULL,
 CONSTRAINT [PK_HangFire_State] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250711055805_InitialCreate', N'9.0.7')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250711121342_UpdateTaskItemWithStatusAndPriority', N'9.0.7')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250711122749_UpdateTaskItemModel', N'9.0.7')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250711203948_AddCreatedAtUpdatedAtToTaskItem', N'9.0.7')
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (1, N'Sagar Patil', N'Sagar12@gmail.com', N'9854651245', 1)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (2, N'Akash Patil', N'akash12@gmail.com', N'+919623307854', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (3, N'Amit Pawar', N'amit12@gmail.com', N'+919623547854', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (4, N'Suraj patil', N'suraj12@gmail.com', N'+917457896541', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (5, N'Prince Shukla', N'prince12@gmail.com', N'+36201456396', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (6, N'Shamal Chavan', N'shamal12@gmail.com', N'+919623302015', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (7, N'Ved Rathod', N'ved12@gmail.com', N'+919623302017', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (8, N'Sandeep Verma', N'sandeep12@gmail.com', N'+917878784545', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (9, N'Joseph Dsoza', N'Joseph@gmail.com', N'+919623302018', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (10, N'Sagar Patil', N'sagar123@gmail.com', N'+36201456398', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (11, N'Sagar Pandit', N'sagarP@gmail.com', N'+919627302017', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (12, N'Sandeep Patil', N'sandeepP@gmail.com', N'+917845785412', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (13, N'Rutuja Verma', N'RutujaV12@gmail.com', N'+919028439088', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (14, N'Shashi Rahate', N'ShashiR12@gmail.com', N'+917851788898', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (15, N'Karan Rathod', N'karan12@gmail.com', N'+919874565478', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (16, N'Sagar Patil', N'sagarPatil12@gmail.com', N'+919874563214', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (17, N'Suraj Rahate', N'SurajR@gmail.com', N'+917458965214', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (18, N'Akash Pawar', N'akashP12@gmail.com', N'+917458745874', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (19, N'Sagar Nachankar', N'Sagarnachankar@gmail.com', N'+917896541245', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (20, N'Ashish Pawar', N'AshishP@gmail.com', N'7878789858', 1)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (21, N'Prince Shukla', N'Pshukla12@gmail.com', N'+917457856978', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (22, N'Nakul Vavale', N'NakulV12@gmail.com', N'+917845124578', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (23, N'Nakul Vavale', N'NakulV123@gmail.com', N'+917458965215', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (24, N'Rishabh Singh', N'RB123@gmail.com', N'+919623564785', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (25, N'Rishabh Pant', N'RishabhP@gmail.com', N'+917845214596', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (26, N'Test', N'test12@gmail.com', N'+919854125632', 1)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (27, N'test2', N'test@gmail.com', N'+919874563254', 1)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (28, N'test2', N'test@gmail.com', N'+919874563254', 1)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (29, N'test3', N'test3@gmail.com', N'+919658745213', 1)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (30, N'test3', N'test3@gmail.com', N'+919658745213', 1)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (31, N'test4', N'test4@gmail.com', N'+36201456984', 1)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (32, N'test1', N'test1@gmail.com', N'+628124758965', 1)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Phone], [IsDeleted]) VALUES (33, N'test', N'test6@gmail.com', N'+919874565215', 1)
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[TaskItems] ON 

INSERT [dbo].[TaskItems] ([Id], [Title], [Description], [DueDate], [IsDeleted], [CustomerId], [Priority], [Status], [CreatedAt], [UpdatedAt]) VALUES (1, N'Task 1', N'Working on task 1', CAST(N'2025-07-18T00:00:00.0000000' AS DateTime2), 0, 3, N'High', N'Completed', CAST(N'2025-07-15T04:07:19.6215798' AS DateTime2), CAST(N'2025-07-15T09:40:08.7461752' AS DateTime2))
INSERT [dbo].[TaskItems] ([Id], [Title], [Description], [DueDate], [IsDeleted], [CustomerId], [Priority], [Status], [CreatedAt], [UpdatedAt]) VALUES (2, N'Task 2', N'Working on Task 2', CAST(N'2025-07-24T00:00:00.0000000' AS DateTime2), 0, 6, N'Medium', N'Pending', CAST(N'2025-07-15T04:07:57.7834208' AS DateTime2), CAST(N'2025-07-15T04:07:57.7834211' AS DateTime2))
INSERT [dbo].[TaskItems] ([Id], [Title], [Description], [DueDate], [IsDeleted], [CustomerId], [Priority], [Status], [CreatedAt], [UpdatedAt]) VALUES (3, N'Task 3', N'working on task 3', CAST(N'2025-07-17T00:00:00.0000000' AS DateTime2), 0, 7, N'Low', N'Completed', CAST(N'2025-07-15T04:08:32.3591607' AS DateTime2), CAST(N'2025-07-15T04:09:41.3157648' AS DateTime2))
INSERT [dbo].[TaskItems] ([Id], [Title], [Description], [DueDate], [IsDeleted], [CustomerId], [Priority], [Status], [CreatedAt], [UpdatedAt]) VALUES (4, N'Task 4', N'Working on Task 4', CAST(N'2025-07-10T00:00:00.0000000' AS DateTime2), 0, 5, N'High', N'Pending', CAST(N'2025-07-15T04:09:34.4359649' AS DateTime2), CAST(N'2025-07-15T04:09:34.4359651' AS DateTime2))
INSERT [dbo].[TaskItems] ([Id], [Title], [Description], [DueDate], [IsDeleted], [CustomerId], [Priority], [Status], [CreatedAt], [UpdatedAt]) VALUES (5, N'Task 5', N'Working on task 5', CAST(N'2025-07-08T00:00:00.0000000' AS DateTime2), 0, 6, N'Medium', N'Pending', CAST(N'2025-07-15T09:37:31.1765035' AS DateTime2), CAST(N'2025-07-15T09:37:31.1766592' AS DateTime2))
INSERT [dbo].[TaskItems] ([Id], [Title], [Description], [DueDate], [IsDeleted], [CustomerId], [Priority], [Status], [CreatedAt], [UpdatedAt]) VALUES (6, N'Edit Demo', N'Working on task 6', CAST(N'2025-07-16T00:00:00.0000000' AS DateTime2), 0, 5, N'High', N'Completed', CAST(N'2025-07-15T20:00:51.3304841' AS DateTime2), CAST(N'2025-07-15T20:00:51.3319994' AS DateTime2))
INSERT [dbo].[TaskItems] ([Id], [Title], [Description], [DueDate], [IsDeleted], [CustomerId], [Priority], [Status], [CreatedAt], [UpdatedAt]) VALUES (7, N'Task 7', N'Working on task 7', CAST(N'2025-07-17T00:00:00.0000000' AS DateTime2), 0, 5, N'Medium', N'Pending', CAST(N'2025-07-15T09:39:40.5447811' AS DateTime2), CAST(N'2025-07-15T09:39:40.5447816' AS DateTime2))
INSERT [dbo].[TaskItems] ([Id], [Title], [Description], [DueDate], [IsDeleted], [CustomerId], [Priority], [Status], [CreatedAt], [UpdatedAt]) VALUES (8, N'Task 7', N'working on Task 7', CAST(N'2025-07-23T00:00:00.0000000' AS DateTime2), 0, 24, N'Medium', N'Pending', CAST(N'2025-07-15T10:12:19.1362188' AS DateTime2), CAST(N'2025-07-15T10:12:19.1363392' AS DateTime2))
INSERT [dbo].[TaskItems] ([Id], [Title], [Description], [DueDate], [IsDeleted], [CustomerId], [Priority], [Status], [CreatedAt], [UpdatedAt]) VALUES (9, N'Task 8', N'Working on task 8', CAST(N'2025-07-16T00:00:00.0000000' AS DateTime2), 0, 22, N'High', N'Pending', CAST(N'2025-07-16T21:29:18.2203727' AS DateTime2), CAST(N'2025-07-16T21:29:18.2282448' AS DateTime2))
INSERT [dbo].[TaskItems] ([Id], [Title], [Description], [DueDate], [IsDeleted], [CustomerId], [Priority], [Status], [CreatedAt], [UpdatedAt]) VALUES (10, N'Task 9', N'working on task 9', CAST(N'2025-07-16T00:00:00.0000000' AS DateTime2), 0, 18, N'Low', N'Pending', CAST(N'2025-07-16T21:24:49.2154684' AS DateTime2), CAST(N'2025-07-16T21:24:49.2360067' AS DateTime2))
INSERT [dbo].[TaskItems] ([Id], [Title], [Description], [DueDate], [IsDeleted], [CustomerId], [Priority], [Status], [CreatedAt], [UpdatedAt]) VALUES (11, N'Task 10', N'Working on task 10', CAST(N'2025-07-09T00:00:00.0000000' AS DateTime2), 0, 25, N'High', N'Pending', CAST(N'2025-07-15T10:14:04.2407739' AS DateTime2), CAST(N'2025-07-15T10:14:04.2407748' AS DateTime2))
INSERT [dbo].[TaskItems] ([Id], [Title], [Description], [DueDate], [IsDeleted], [CustomerId], [Priority], [Status], [CreatedAt], [UpdatedAt]) VALUES (12, N'Task 11', N'Working on Task 11', CAST(N'2025-07-23T00:00:00.0000000' AS DateTime2), 0, 21, N'Low', N'Pending', CAST(N'2025-07-15T10:14:47.1786045' AS DateTime2), CAST(N'2025-07-15T10:14:47.1786054' AS DateTime2))
INSERT [dbo].[TaskItems] ([Id], [Title], [Description], [DueDate], [IsDeleted], [CustomerId], [Priority], [Status], [CreatedAt], [UpdatedAt]) VALUES (13, N'Task 12', N'Working on Task 12', CAST(N'2025-07-17T00:00:00.0000000' AS DateTime2), 0, 4, N'Medium', N'Pending', CAST(N'2025-07-15T19:59:58.0919574' AS DateTime2), CAST(N'2025-07-15T19:59:58.0920762' AS DateTime2))
SET IDENTITY_INSERT [dbo].[TaskItems] OFF
GO
INSERT [HangFire].[AggregatedCounter] ([Key], [Value], [ExpireAt]) VALUES (N'stats:succeeded', 7, NULL)
INSERT [HangFire].[AggregatedCounter] ([Key], [Value], [ExpireAt]) VALUES (N'stats:succeeded:2025-07-13', 1, CAST(N'2025-08-13T14:48:28.460' AS DateTime))
INSERT [HangFire].[AggregatedCounter] ([Key], [Value], [ExpireAt]) VALUES (N'stats:succeeded:2025-07-14', 1, CAST(N'2025-08-14T06:01:50.977' AS DateTime))
INSERT [HangFire].[AggregatedCounter] ([Key], [Value], [ExpireAt]) VALUES (N'stats:succeeded:2025-07-15', 4, CAST(N'2025-08-15T04:38:41.920' AS DateTime))
INSERT [HangFire].[AggregatedCounter] ([Key], [Value], [ExpireAt]) VALUES (N'stats:succeeded:2025-07-16', 1, CAST(N'2025-08-16T11:08:56.537' AS DateTime))
INSERT [HangFire].[AggregatedCounter] ([Key], [Value], [ExpireAt]) VALUES (N'stats:succeeded:2025-07-16-11', 1, CAST(N'2025-07-17T11:08:56.537' AS DateTime))
GO
INSERT [HangFire].[Hash] ([Key], [Field], [Value], [ExpireAt]) VALUES (N'recurring-job:daily-task-reminder', N'CreatedAt', N'2025-07-12T21:54:01.4619824Z', NULL)
INSERT [HangFire].[Hash] ([Key], [Field], [Value], [ExpireAt]) VALUES (N'recurring-job:daily-task-reminder', N'Cron', N'0 0 * * *', NULL)
INSERT [HangFire].[Hash] ([Key], [Field], [Value], [ExpireAt]) VALUES (N'recurring-job:daily-task-reminder', N'Job', N'{"Type":"SanmolTaskManager_BLL.Interfaces.IEmailReminderJob, SanmolTaskManager_BLL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null","Method":"SendDailyOverdueSummary","ParameterTypes":"[]","Arguments":"[]"}', NULL)
INSERT [HangFire].[Hash] ([Key], [Field], [Value], [ExpireAt]) VALUES (N'recurring-job:daily-task-reminder', N'LastExecution', N'2025-07-16T11:08:48.9893554Z', NULL)
INSERT [HangFire].[Hash] ([Key], [Field], [Value], [ExpireAt]) VALUES (N'recurring-job:daily-task-reminder', N'LastJobId', N'7', NULL)
INSERT [HangFire].[Hash] ([Key], [Field], [Value], [ExpireAt]) VALUES (N'recurring-job:daily-task-reminder', N'NextExecution', N'2025-07-17T00:00:00.0000000Z', NULL)
INSERT [HangFire].[Hash] ([Key], [Field], [Value], [ExpireAt]) VALUES (N'recurring-job:daily-task-reminder', N'Queue', N'default', NULL)
INSERT [HangFire].[Hash] ([Key], [Field], [Value], [ExpireAt]) VALUES (N'recurring-job:daily-task-reminder', N'TimeZoneId', N'UTC', NULL)
INSERT [HangFire].[Hash] ([Key], [Field], [Value], [ExpireAt]) VALUES (N'recurring-job:daily-task-reminder', N'V', N'2', NULL)
GO
SET IDENTITY_INSERT [HangFire].[Job] ON 

INSERT [HangFire].[Job] ([Id], [StateId], [StateName], [InvocationData], [Arguments], [CreatedAt], [ExpireAt]) VALUES (7, 21, N'Succeeded', N'{"Type":"SanmolTaskManager_BLL.Interfaces.IEmailReminderJob, SanmolTaskManager_BLL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null","Method":"SendDailyOverdueSummary","ParameterTypes":"[]","Arguments":null}', N'[]', CAST(N'2025-07-16T11:08:49.113' AS DateTime), CAST(N'2025-07-17T11:08:56.537' AS DateTime))
SET IDENTITY_INSERT [HangFire].[Job] OFF
GO
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (7, N'CurrentCulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (7, N'CurrentUICulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (7, N'RecurringJobId', N'"daily-task-reminder"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (7, N'Time', N'1752664128')
GO
INSERT [HangFire].[Schema] ([Version]) VALUES (9)
GO
INSERT [HangFire].[Server] ([Id], [Data], [LastHeartbeat]) VALUES (N'laptop-8ltcpdve:42656:88c6fc2c-e8a4-49e9-88e2-93dc938b7179', N'{"WorkerCount":20,"Queues":["default"],"StartedAt":"2025-07-16T15:49:51.2831009Z"}', CAST(N'2025-07-16T15:57:51.723' AS DateTime))
INSERT [HangFire].[Server] ([Id], [Data], [LastHeartbeat]) VALUES (N'laptop-8ltcpdve:51916:564f090d-333f-4f84-bb09-898cb091c9a5', N'{"WorkerCount":20,"Queues":["default"],"StartedAt":"2025-07-16T15:58:29.2923043Z"}', CAST(N'2025-07-16T15:59:29.493' AS DateTime))
GO
INSERT [HangFire].[Set] ([Key], [Score], [Value], [ExpireAt]) VALUES (N'recurring-jobs', 1752710400, N'daily-task-reminder', NULL)
GO
SET IDENTITY_INSERT [HangFire].[State] ON 

INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (19, 7, N'Enqueued', N'Triggered by recurring job scheduler', CAST(N'2025-07-16T11:08:49.467' AS DateTime), N'{"EnqueuedAt":"2025-07-16T11:08:49.1385047Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (20, 7, N'Processing', NULL, CAST(N'2025-07-16T11:08:49.687' AS DateTime), N'{"StartedAt":"2025-07-16T11:08:49.5274374Z","ServerId":"laptop-8ltcpdve:51420:02d4030b-462c-41d6-b802-fe36e979f13e","WorkerId":"1c144f35-53eb-4d8d-945e-4ec33fb2e115"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (21, 7, N'Succeeded', NULL, CAST(N'2025-07-16T11:08:56.533' AS DateTime), N'{"SucceededAt":"2025-07-16T11:08:56.5221131Z","PerformanceDuration":"6829","Latency":"579"}')
SET IDENTITY_INSERT [HangFire].[State] OFF
GO
ALTER TABLE [dbo].[TaskItems] ADD  DEFAULT (N'') FOR [Priority]
GO
ALTER TABLE [dbo].[TaskItems] ADD  DEFAULT (N'') FOR [Status]
GO
ALTER TABLE [dbo].[TaskItems] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreatedAt]
GO
ALTER TABLE [dbo].[TaskItems] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[TaskItems]  WITH CHECK ADD  CONSTRAINT [FK_TaskItems_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TaskItems] CHECK CONSTRAINT [FK_TaskItems_Customers_CustomerId]
GO
ALTER TABLE [HangFire].[JobParameter]  WITH CHECK ADD  CONSTRAINT [FK_HangFire_JobParameter_Job] FOREIGN KEY([JobId])
REFERENCES [HangFire].[Job] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [HangFire].[JobParameter] CHECK CONSTRAINT [FK_HangFire_JobParameter_Job]
GO
ALTER TABLE [HangFire].[State]  WITH CHECK ADD  CONSTRAINT [FK_HangFire_State_Job] FOREIGN KEY([JobId])
REFERENCES [HangFire].[Job] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [HangFire].[State] CHECK CONSTRAINT [FK_HangFire_State_Job]
GO
