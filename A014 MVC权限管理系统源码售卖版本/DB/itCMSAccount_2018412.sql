USE [master]
GO
/****** Object:  Database [itCMSAccount]    Script Date: 2018/4/12 12:50:36 ******/
CREATE DATABASE [itCMSAccount]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'itCMSAccount', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\itCMSAccount.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'itCMSAccount_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\itCMSAccount_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [itCMSAccount] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [itCMSAccount].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [itCMSAccount] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [itCMSAccount] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [itCMSAccount] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [itCMSAccount] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [itCMSAccount] SET ARITHABORT OFF 
GO
ALTER DATABASE [itCMSAccount] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [itCMSAccount] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [itCMSAccount] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [itCMSAccount] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [itCMSAccount] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [itCMSAccount] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [itCMSAccount] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [itCMSAccount] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [itCMSAccount] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [itCMSAccount] SET  DISABLE_BROKER 
GO
ALTER DATABASE [itCMSAccount] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [itCMSAccount] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [itCMSAccount] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [itCMSAccount] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [itCMSAccount] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [itCMSAccount] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [itCMSAccount] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [itCMSAccount] SET RECOVERY FULL 
GO
ALTER DATABASE [itCMSAccount] SET  MULTI_USER 
GO
ALTER DATABASE [itCMSAccount] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [itCMSAccount] SET DB_CHAINING OFF 
GO
ALTER DATABASE [itCMSAccount] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [itCMSAccount] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [itCMSAccount] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'itCMSAccount', N'ON'
GO
USE [itCMSAccount]
GO
/****** Object:  Table [dbo].[oa_student_course]    Script Date: 2018/4/12 12:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[oa_student_course](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_oa_student_course] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sys_acl_group]    Script Date: 2018/4/12 12:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sys_acl_group](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[groupID] [int] NOT NULL,
	[actionID] [int] NOT NULL,
	[access] [bit] NOT NULL,
 CONSTRAINT [PK_sys_acl_group] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sys_acl_user]    Script Date: 2018/4/12 12:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sys_acl_user](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NOT NULL,
	[actionID] [int] NOT NULL,
	[access] [bit] NOT NULL,
 CONSTRAINT [PK_sys_acl_user] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sys_action]    Script Date: 2018/4/12 12:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sys_action](
	[actionID] [int] IDENTITY(1,1) NOT NULL,
	[moduleKey] [nvarchar](100) NOT NULL,
	[actionKey] [nvarchar](100) NOT NULL,
	[actionName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_sys_action] PRIMARY KEY CLUSTERED 
(
	[actionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sys_config]    Script Date: 2018/4/12 12:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sys_config](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [nvarchar](50) NULL,
	[name] [nvarchar](50) NULL,
	[name_cn] [nvarchar](50) NULL,
	[sort_id] [int] NULL,
	[is_lock] [bit] NULL,
	[comment] [nvarchar](150) NULL,
 CONSTRAINT [PK_sys_config] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sys_Department]    Script Date: 2018/4/12 12:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sys_Department](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DeparName] [nvarchar](max) NOT NULL,
	[parentID] [int] NOT NULL,
	[sort] [int] NULL,
 CONSTRAINT [PK_sys_Department] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sys_group]    Script Date: 2018/4/12 12:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sys_group](
	[groupID] [int] IDENTITY(1,1) NOT NULL,
	[groupName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_sys_group] PRIMARY KEY CLUSTERED 
(
	[groupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sys_group_user]    Script Date: 2018/4/12 12:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sys_group_user](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[groupID] [int] NOT NULL,
	[userID] [int] NOT NULL,
 CONSTRAINT [PK_sys_group_user] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sys_module]    Script Date: 2018/4/12 12:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sys_module](
	[moduleID] [int] IDENTITY(1,1) NOT NULL,
	[moduleKey] [nvarchar](100) NULL,
	[moduleName] [nvarchar](50) NULL,
	[parentID] [int] NULL,
	[urls] [nvarchar](50) NULL,
	[isMenu] [bit] NULL,
	[isDisplay] [bit] NULL,
	[sort] [int] NULL,
	[icon] [nvarchar](50) NULL,
	[class_layer] [int] NULL,
 CONSTRAINT [PK_sys_module] PRIMARY KEY CLUSTERED 
(
	[moduleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sys_token]    Script Date: 2018/4/12 12:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sys_token](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DevId] [nvarchar](100) NULL,
	[AppId] [nvarchar](100) NULL,
	[CertId] [nvarchar](100) NULL,
	[ApiToken] [text] NULL,
	[ebay_account] [nvarchar](50) NULL,
	[name_cn] [nvarchar](50) NULL,
	[name_en] [nvarchar](50) NULL,
 CONSTRAINT [PK_dt_token] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sys_user]    Script Date: 2018/4/12 12:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sys_user](
	[userID] [int] IDENTITY(1,1) NOT NULL,
	[userAccount] [nvarchar](50) NULL,
	[userPasswd] [nvarchar](50) NULL,
	[telephone] [nvarchar](20) NULL,
	[email] [nvarchar](50) NULL,
	[status] [bit] NULL,
	[departID] [int] NULL,
	[acctounName] [nvarchar](50) NULL,
 CONSTRAINT [PK_sys_user] PRIMARY KEY CLUSTERED 
(
	[userID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[oa_student_course] ON 

INSERT [dbo].[oa_student_course] ([ID], [Name], [Price]) VALUES (1, N'asp.net', CAST(500 AS Decimal(18, 0)))
INSERT [dbo].[oa_student_course] ([ID], [Name], [Price]) VALUES (2, N'Java基础', CAST(900 AS Decimal(18, 0)))
INSERT [dbo].[oa_student_course] ([ID], [Name], [Price]) VALUES (3, N'C语言能干大事', CAST(699 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[oa_student_course] OFF
SET IDENTITY_INSERT [dbo].[sys_acl_group] ON 

INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (1, 1, 11, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (2, 1, 12, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (3, 1, 13, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (4, 2, 11, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (5, 2, 12, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (6, 2, 13, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (7, 2, 41, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (12, 2, 28, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (13, 2, 1, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (14, 2, 8, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (15, 2, 9, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (16, 2, 27, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (17, 2, 15, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (18, 2, 16, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (19, 2, 17, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (20, 2, 29, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (21, 2, 23, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (22, 2, 24, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (23, 2, 25, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (24, 2, 30, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (25, 2, 31, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (26, 2, 32, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (27, 2, 33, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (28, 2, 34, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (29, 2, 35, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (30, 2, 36, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (31, 2, 37, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (32, 2, 38, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (33, 2, 39, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (34, 2, 40, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (35, 2, 65, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (36, 2, 66, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (37, 2, 67, 1)
INSERT [dbo].[sys_acl_group] ([ID], [groupID], [actionID], [access]) VALUES (38, 2, 68, 1)
SET IDENTITY_INSERT [dbo].[sys_acl_group] OFF
SET IDENTITY_INSERT [dbo].[sys_acl_user] ON 

INSERT [dbo].[sys_acl_user] ([ID], [userID], [actionID], [access]) VALUES (1, 2, 11, 1)
INSERT [dbo].[sys_acl_user] ([ID], [userID], [actionID], [access]) VALUES (2, 2, 12, 1)
SET IDENTITY_INSERT [dbo].[sys_acl_user] OFF
SET IDENTITY_INSERT [dbo].[sys_action] ON 

INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (1, N'Action', N'Add', N'添加')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (8, N'Action', N'List', N'查看')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (9, N'Action', N'Edit', N'修改')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (10, N'Action', N'Delete', N'删除')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (11, N'Model', N'List', N'查看')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (12, N'Model', N'Add', N'添加')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (13, N'Model', N'Edit', N'修改')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (15, N'User', N'List', N'查看')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (16, N'User', N'Add', N'添加')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (17, N'User', N'Edit', N'修改')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (18, N'User', N'Delete', N'删除')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (19, N'User', N'Authorize', N'授权')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (23, N'Roles', N'List', N'查看')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (24, N'Roles', N'Add', N'添加')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (25, N'Roles', N'Edit', N'修改')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (26, N'Auth', N'Index', N'首页')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (27, N'Action', N'Show', N'显示')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (28, N'Model', N'Show', N'显示')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (29, N'User', N'Show', N'显示')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (30, N'Roles', N'Show', N'显示')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (31, N'Department', N'Show', N'显示')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (32, N'Department', N'List', N'查看')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (33, N'Department', N'Add', N'添加')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (34, N'Department', N'Edit', N'修改')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (35, N'Department', N'Delete', N'删除')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (36, N'sys_mang', N'Show', N'显示')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (37, N'Settings', N'Show', N'显示')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (38, N'Settings', N'List', N'查看')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (39, N'Settings', N'Add', N'添加')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (40, N'Settings', N'Edit', N'修改')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (41, N'test', N'Show', N'显示')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (65, N'yang', N'Show', N'显示')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (66, N'yang', N'List', N'查看')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (67, N'yang', N'Add', N'添加')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (68, N'yang', N'Edit', N'修改')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (69, N'StudentMaintenance', N'Show', N'显示')
INSERT [dbo].[sys_action] ([actionID], [moduleKey], [actionKey], [actionName]) VALUES (70, N'StudentMaintenance', N'List', N'查看')
SET IDENTITY_INSERT [dbo].[sys_action] OFF
SET IDENTITY_INSERT [dbo].[sys_Department] ON 

INSERT [dbo].[sys_Department] ([ID], [DeparName], [parentID], [sort]) VALUES (1, N'总经办', 0, 0)
INSERT [dbo].[sys_Department] ([ID], [DeparName], [parentID], [sort]) VALUES (2, N'人事部', 1, 1)
INSERT [dbo].[sys_Department] ([ID], [DeparName], [parentID], [sort]) VALUES (3, N'采购部', 1, 2)
INSERT [dbo].[sys_Department] ([ID], [DeparName], [parentID], [sort]) VALUES (4, N'财务部', 1, 3)
INSERT [dbo].[sys_Department] ([ID], [DeparName], [parentID], [sort]) VALUES (6, N'IT部门', 1, 6)
INSERT [dbo].[sys_Department] ([ID], [DeparName], [parentID], [sort]) VALUES (7, N'Test1', 4, 1)
SET IDENTITY_INSERT [dbo].[sys_Department] OFF
SET IDENTITY_INSERT [dbo].[sys_group] ON 

INSERT [dbo].[sys_group] ([groupID], [groupName]) VALUES (1, N'系统管理组')
INSERT [dbo].[sys_group] ([groupID], [groupName]) VALUES (2, N'超级管理组')
SET IDENTITY_INSERT [dbo].[sys_group] OFF
SET IDENTITY_INSERT [dbo].[sys_group_user] ON 

INSERT [dbo].[sys_group_user] ([ID], [groupID], [userID]) VALUES (1, 2, 1)
INSERT [dbo].[sys_group_user] ([ID], [groupID], [userID]) VALUES (2, 1, 2)
INSERT [dbo].[sys_group_user] ([ID], [groupID], [userID]) VALUES (3, 2, 3)
SET IDENTITY_INSERT [dbo].[sys_group_user] OFF
SET IDENTITY_INSERT [dbo].[sys_module] ON 

INSERT [dbo].[sys_module] ([moduleID], [moduleKey], [moduleName], [parentID], [urls], [isMenu], [isDisplay], [sort], [icon], [class_layer]) VALUES (2, N'Model', N'菜单管理', 9, N'/Account/Model/Show', 1, 1, 1, NULL, 2)
INSERT [dbo].[sys_module] ([moduleID], [moduleKey], [moduleName], [parentID], [urls], [isMenu], [isDisplay], [sort], [icon], [class_layer]) VALUES (5, N'Action', N'权限管理', 9, N'/Account/Action/Show', 1, 1, 2, NULL, 2)
INSERT [dbo].[sys_module] ([moduleID], [moduleKey], [moduleName], [parentID], [urls], [isMenu], [isDisplay], [sort], [icon], [class_layer]) VALUES (6, N'User', N'用户管理', 9, N'/OA/User/Show', 1, 1, 3, NULL, 2)
INSERT [dbo].[sys_module] ([moduleID], [moduleKey], [moduleName], [parentID], [urls], [isMenu], [isDisplay], [sort], [icon], [class_layer]) VALUES (7, N'Roles', N'角色管理', 9, N'/OA/Roles/Show', 1, 1, 4, NULL, 2)
INSERT [dbo].[sys_module] ([moduleID], [moduleKey], [moduleName], [parentID], [urls], [isMenu], [isDisplay], [sort], [icon], [class_layer]) VALUES (8, N'Department', N'部门管理', 9, N'/OA/Department/Show', 1, 1, 5, NULL, 2)
INSERT [dbo].[sys_module] ([moduleID], [moduleKey], [moduleName], [parentID], [urls], [isMenu], [isDisplay], [sort], [icon], [class_layer]) VALUES (9, N'sys_mang', N'系统设置', 0, NULL, 1, 1, 0, NULL, 1)
INSERT [dbo].[sys_module] ([moduleID], [moduleKey], [moduleName], [parentID], [urls], [isMenu], [isDisplay], [sort], [icon], [class_layer]) VALUES (10, N'Settings', N'配置信息', 9, N'/Systems/Settings/Show', 1, 1, 6, NULL, 2)
INSERT [dbo].[sys_module] ([moduleID], [moduleKey], [moduleName], [parentID], [urls], [isMenu], [isDisplay], [sort], [icon], [class_layer]) VALUES (11, N'test', N'测试', 0, N'/Account/Model/test', 1, 1, 7, NULL, 1)
INSERT [dbo].[sys_module] ([moduleID], [moduleKey], [moduleName], [parentID], [urls], [isMenu], [isDisplay], [sort], [icon], [class_layer]) VALUES (12, N'yang', N'yang', 11, NULL, 1, 1, 8, NULL, 2)
INSERT [dbo].[sys_module] ([moduleID], [moduleKey], [moduleName], [parentID], [urls], [isMenu], [isDisplay], [sort], [icon], [class_layer]) VALUES (13, N'StudentMaintenance', N'学生管理', 0, N'/StudentMaintenance/StudentCourse/Show', 1, 1, 1, N'icon-desktop', 1)
SET IDENTITY_INSERT [dbo].[sys_module] OFF
SET IDENTITY_INSERT [dbo].[sys_user] ON 

INSERT [dbo].[sys_user] ([userID], [userAccount], [userPasswd], [telephone], [email], [status], [departID], [acctounName]) VALUES (1, N'admin', N'95-7F-37-73-9F-F2-9A-3D-CB-48-BE-72-AC-46-95-8E', N'18111111111', N'hao1232@bfe.me', 1, 6, N'超级管理员')
INSERT [dbo].[sys_user] ([userID], [userAccount], [userPasswd], [telephone], [email], [status], [departID], [acctounName]) VALUES (2, N'a', N'B9-77-3C-B8-52-C7-E5-BA-B8-AC-3D-BA-14-7F-7A-D8', N'18611111111', N'12w@23.com', 1, 6, N'w')
INSERT [dbo].[sys_user] ([userID], [userAccount], [userPasswd], [telephone], [email], [status], [departID], [acctounName]) VALUES (3, N'admin', N'95-7F-37-73-9F-F2-9A-3D-CB-48-BE-72-AC-46-95-8E', N'13200000000', N'111@qq.com', 1, 1, N'admin')
SET IDENTITY_INSERT [dbo].[sys_user] OFF
USE [master]
GO
ALTER DATABASE [itCMSAccount] SET  READ_WRITE 
GO
