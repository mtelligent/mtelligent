SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CohortProperties](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CohortId] [int] NOT NULL,
	[Name] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Value] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cohorts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SystemName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UID] [uniqueidentifier] NOT NULL,
	[Active] [int] NOT NULL,
	[Type] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CreatedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Created] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Updated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExperimentGoals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExperimentId] [int] NOT NULL,
	[GoalId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Experiments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SystemName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UID] [uniqueidentifier] NOT NULL,
	[TargetCohortId] [int] NOT NULL,
	[Active] [int] NOT NULL,
	[CreatedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Created] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Updated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExperimentSegments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SystemName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UID] [uniqueidentifier] NOT NULL,
	[TargetPercentage] [float] NOT NULL,
	[IsDefault] [int] NOT NULL,
	[ExperimentId] [int] NOT NULL,
	[CreatedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Created] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Updated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExperimentSegmentVariableValues](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExperimentSegmentId] [int] NOT NULL,
	[ExperimentVariableId] [int] NOT NULL,
	[Value] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExperimentVariables](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ExperimentId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Goals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SystemName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Active] [int] NOT NULL,
	[Value] [float] NOT NULL,
	[GACode] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CustomJS] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Created] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Updated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sites](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Active] [int] NOT NULL,
	[CreatedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Created] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Updated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiteUrls](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SiteId] [int] NOT NULL,
	[Url] [nvarchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Active] [int] NOT NULL,
	[CreatedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Created] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Updated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitorAttributes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VisitorId] [int] NOT NULL,
	[Name] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Value] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedyBy] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitorCohorts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VisitorId] [int] NOT NULL,
	[CohortId] [int] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitorConversions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VisitorId] [int] NOT NULL,
	[GoalId] [int] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitorLandingPages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VisitorId] [int] NOT NULL,
	[LandingPageUrl] [nvarchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitorReferrers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VisitorId] [int] NOT NULL,
	[ReferrerUrl] [nvarchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitorRequests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VisitorId] [int] NOT NULL,
	[RequestUrl] [nvarchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Visitors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UID] [uniqueidentifier] NOT NULL,
	[FirstVisit] [datetime] NOT NULL,
	[UserName] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsAuthenticated] [int] NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ReconcilledVisitorId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitorSegments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VisitorId] [int] NOT NULL,
	[SegmentId] [int] NOT NULL,
	[ExperimentId] [int] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET IDENTITY_INSERT [dbo].[CohortProperties] ON 

GO
INSERT [dbo].[CohortProperties] ([Id], [CohortId], [Name], [Value]) VALUES (2, 12, N'Role', N'Administrators')
GO
INSERT [dbo].[CohortProperties] ([Id], [CohortId], [Name], [Value]) VALUES (4, 14, N'Referrer', N'google.com')
GO
INSERT [dbo].[CohortProperties] ([Id], [CohortId], [Name], [Value]) VALUES (5, 15, N'LandingUrl', N'?Track=CODE')
GO
INSERT [dbo].[CohortProperties] ([Id], [CohortId], [Name], [Value]) VALUES (6, 16, N'Goal', N'PURCHASED')
GO
INSERT [dbo].[CohortProperties] ([Id], [CohortId], [Name], [Value]) VALUES (9, 17, N'ExperimentName', N'Shortcut')
GO
INSERT [dbo].[CohortProperties] ([Id], [CohortId], [Name], [Value]) VALUES (10, 17, N'Segment', N'Option 1')
GO
INSERT [dbo].[CohortProperties] ([Id], [CohortId], [Name], [Value]) VALUES (11, 13, N'FirstVisitStartDate', N'1/1/2014 12:00:00 AM')
GO
INSERT [dbo].[CohortProperties] ([Id], [CohortId], [Name], [Value]) VALUES (12, 13, N'FirstVisitEndDate', N'12/31/9999 11:59:59 PM')
GO
SET IDENTITY_INSERT [dbo].[CohortProperties] OFF
GO
SET IDENTITY_INSERT [dbo].[Cohorts] ON 

GO
INSERT [dbo].[Cohorts] ([Id], [Name], [SystemName], [UID], [Active], [Type], [CreatedBy], [Created], [UpdatedBy], [Updated]) VALUES (10, N'All Users', N'All users', N'a6a718c3-773a-4e71-8936-4b288c0bc9d5', 1, N'Mtelligent.Entities.Cohorts.AllUsersCohort, Mtelligent', N'System', CAST(0x0000A2A5017F82D6 AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Cohorts] ([Id], [Name], [SystemName], [UID], [Active], [Type], [CreatedBy], [Created], [UpdatedBy], [Updated]) VALUES (11, N'Authenticated Users', N'Authenticated users', N'023ad9c7-3c61-4a27-90ee-80176f3ef4b3', 1, N'Mtelligent.Entities.Cohorts.AuthenticatedUsersCohort, Mtelligent', N'System', CAST(0x0000A2A5017F82D6 AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Cohorts] ([Id], [Name], [SystemName], [UID], [Active], [Type], [CreatedBy], [Created], [UpdatedBy], [Updated]) VALUES (12, N'Admin', N'Admin', N'd1eaa5fe-8434-4e24-9c06-f41097664388', 1, N'Mtelligent.Entities.Cohorts.RoleCohort, Mtelligent', N'admin', CAST(0x0000A2A5018073F9 AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Cohorts] ([Id], [Name], [SystemName], [UID], [Active], [Type], [CreatedBy], [Created], [UpdatedBy], [Updated]) VALUES (13, N'New for 2014', N'New for 2014', N'ce509b7e-f1df-4836-80bb-a0d897b5afc8', 1, N'Mtelligent.Entities.Cohorts.FirstVisitCohort, Mtelligent', N'admin', CAST(0x0000A2A5018506E5 AS DateTime), N'admin', CAST(0x0000A2A6000468FF AS DateTime))
GO
INSERT [dbo].[Cohorts] ([Id], [Name], [SystemName], [UID], [Active], [Type], [CreatedBy], [Created], [UpdatedBy], [Updated]) VALUES (14, N'From Google', N'From Google', N'8555f903-0526-4d54-90dd-528c07bc1dce', 1, N'Mtelligent.Entities.Cohorts.ReferrerCohort, Mtelligent', N'admin', CAST(0x0000A2A501864124 AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Cohorts] ([Id], [Name], [SystemName], [UID], [Active], [Type], [CreatedBy], [Created], [UpdatedBy], [Updated]) VALUES (15, N'With My Tracking Code', N'With My Tracking Code', N'd2e9b7f3-c1d8-470a-af52-744b2bc8dcb0', 1, N'Mtelligent.Entities.Cohorts.LandingUrlCohort, Mtelligent', N'admin', CAST(0x0000A2A50186648B AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Cohorts] ([Id], [Name], [SystemName], [UID], [Active], [Type], [CreatedBy], [Created], [UpdatedBy], [Updated]) VALUES (16, N'People Who Bought', N'People Who Bought', N'7102a88f-564f-465e-9d46-8ad68bed15c4', 1, N'Mtelligent.Entities.Cohorts.GoalCohort, Mtelligent', N'admin', CAST(0x0000A2A501867D39 AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Cohorts] ([Id], [Name], [SystemName], [UID], [Active], [Type], [CreatedBy], [Created], [UpdatedBy], [Updated]) VALUES (17, N'Shortcut Option 1', N'Shortcut Segment 1', N'2a1f8416-1cc1-4176-901d-d40803778ef2', 1, N'Mtelligent.Entities.Cohorts.SegmentCohort, Mtelligent', N'admin', CAST(0x0000A2A50186B4A9 AS DateTime), N'admin', CAST(0x0000A2A600044FE2 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Cohorts] OFF
GO
SET IDENTITY_INSERT [dbo].[ExperimentGoals] ON 

GO
INSERT [dbo].[ExperimentGoals] ([Id], [ExperimentId], [GoalId]) VALUES (8, 5, 9)
GO
SET IDENTITY_INSERT [dbo].[ExperimentGoals] OFF
GO
SET IDENTITY_INSERT [dbo].[Experiments] ON 

GO
INSERT [dbo].[Experiments] ([Id], [Name], [SystemName], [UID], [TargetCohortId], [Active], [CreatedBy], [Created], [UpdatedBy], [Updated]) VALUES (5, N'Honey vs Vinegar', N'Honey vs Vinegar', N'f428f7f4-d286-4ea7-8ed4-268a167c09b4', 10, 1, N'System', CAST(0x0000A2B000C9F114 AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Experiments] OFF
GO
SET IDENTITY_INSERT [dbo].[ExperimentSegments] ON 

GO
INSERT [dbo].[ExperimentSegments] ([Id], [Name], [SystemName], [UID], [TargetPercentage], [IsDefault], [ExperimentId], [CreatedBy], [Created], [UpdatedBy], [Updated]) VALUES (23, N'Honey', N'Honey', N'b11f61bc-7ae9-4f67-a5a5-ddc002b0d0f7', 50, 0, 5, N'System', CAST(0x0000A2B000C9F114 AS DateTime), NULL, NULL)
GO
INSERT [dbo].[ExperimentSegments] ([Id], [Name], [SystemName], [UID], [TargetPercentage], [IsDefault], [ExperimentId], [CreatedBy], [Created], [UpdatedBy], [Updated]) VALUES (24, N'Vinegar', N'Vinegar', N'b6adc087-d7fe-43bb-962d-f5281acf85dc', 50, 0, 5, N'System', CAST(0x0000A2B000C9F114 AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[ExperimentSegments] OFF
GO
SET IDENTITY_INSERT [dbo].[ExperimentSegmentVariableValues] ON 

GO
INSERT [dbo].[ExperimentSegmentVariableValues] ([Id], [ExperimentSegmentId], [ExperimentVariableId], [Value]) VALUES (99, 23, 15, N'You are awesome')
GO
INSERT [dbo].[ExperimentSegmentVariableValues] ([Id], [ExperimentSegmentId], [ExperimentVariableId], [Value]) VALUES (100, 23, 16, N'DarkOrange')
GO
INSERT [dbo].[ExperimentSegmentVariableValues] ([Id], [ExperimentSegmentId], [ExperimentVariableId], [Value]) VALUES (101, 23, 17, N'You are smart, good looking and have we are so lucky to have you on this page. Would you mind doing us a favor and clicking the button below.')
GO
INSERT [dbo].[ExperimentSegmentVariableValues] ([Id], [ExperimentSegmentId], [ExperimentVariableId], [Value]) VALUES (102, 23, 18, N'http://img837.imageshack.us/img837/8681/ge1j.png')
GO
INSERT [dbo].[ExperimentSegmentVariableValues] ([Id], [ExperimentSegmentId], [ExperimentVariableId], [Value]) VALUES (103, 24, 15, N'Attention Jerk!')
GO
INSERT [dbo].[ExperimentSegmentVariableValues] ([Id], [ExperimentSegmentId], [ExperimentVariableId], [Value]) VALUES (104, 24, 16, N'red')
GO
INSERT [dbo].[ExperimentSegmentVariableValues] ([Id], [ExperimentSegmentId], [ExperimentVariableId], [Value]) VALUES (105, 24, 17, N'I know you find things difficult to do, but I want you to scroll down till you find a button and click it. DO IT NOW!!!')
GO
INSERT [dbo].[ExperimentSegmentVariableValues] ([Id], [ExperimentSegmentId], [ExperimentVariableId], [Value]) VALUES (106, 24, 18, N'http://img15.imageshack.us/img15/8018/obeyeyeposterfnl.jpg')
GO
SET IDENTITY_INSERT [dbo].[ExperimentSegmentVariableValues] OFF
GO
SET IDENTITY_INSERT [dbo].[ExperimentVariables] ON 

GO
INSERT [dbo].[ExperimentVariables] ([Id], [Name], [ExperimentId]) VALUES (15, N'Title', 5)
GO
INSERT [dbo].[ExperimentVariables] ([Id], [Name], [ExperimentId]) VALUES (16, N'TitleColor', 5)
GO
INSERT [dbo].[ExperimentVariables] ([Id], [Name], [ExperimentId]) VALUES (17, N'Copy', 5)
GO
INSERT [dbo].[ExperimentVariables] ([Id], [Name], [ExperimentId]) VALUES (18, N'Image Source', 5)
GO
SET IDENTITY_INSERT [dbo].[ExperimentVariables] OFF
GO
SET IDENTITY_INSERT [dbo].[Goals] ON 

GO
INSERT [dbo].[Goals] ([Id], [Name], [SystemName], [Active], [Value], [GACode], [CustomJS], [CreatedBy], [Created], [UpdatedBy], [Updated]) VALUES (3, N'Sign Up', N'Sign Up', 1, 10, NULL, NULL, N'admin', CAST(0x0000A2A4010D1601 AS DateTime), N'admin', CAST(0x0000A2A9012A06E2 AS DateTime))
GO
INSERT [dbo].[Goals] ([Id], [Name], [SystemName], [Active], [Value], [GACode], [CustomJS], [CreatedBy], [Created], [UpdatedBy], [Updated]) VALUES (4, N'Register', N'Register', 1, 5, NULL, NULL, N'admin', CAST(0x0000A2A4010EE8A6 AS DateTime), N'admin', CAST(0x0000A2AA0167D6BF AS DateTime))
GO
INSERT [dbo].[Goals] ([Id], [Name], [SystemName], [Active], [Value], [GACode], [CustomJS], [CreatedBy], [Created], [UpdatedBy], [Updated]) VALUES (5, N'Another goal', N'Another goal', 1, 0.5, NULL, NULL, N'admin', CAST(0x0000A2A4010FA0D2 AS DateTime), N'admin', CAST(0x0000A2AA0167E68E AS DateTime))
GO
INSERT [dbo].[Goals] ([Id], [Name], [SystemName], [Active], [Value], [GACode], [CustomJS], [CreatedBy], [Created], [UpdatedBy], [Updated]) VALUES (6, N'Click Sample Button', N'Click Sample Button', 1, 1, NULL, NULL, N'admin', CAST(0x0000A2A9016A5C1A AS DateTime), N'admin', CAST(0x0000A2A9016A8BB4 AS DateTime))
GO
INSERT [dbo].[Goals] ([Id], [Name], [SystemName], [Active], [Value], [GACode], [CustomJS], [CreatedBy], [Created], [UpdatedBy], [Updated]) VALUES (9, N'Honey vs Vinegar', N'Honey vs Vinegar', 1, 1, NULL, NULL, N'System', CAST(0x0000A2B000C9F115 AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Goals] OFF
GO
SET IDENTITY_INSERT [dbo].[Sites] ON 

GO
INSERT [dbo].[Sites] ([Id], [Name], [Active], [CreatedBy], [Created], [UpdatedBy], [Updated]) VALUES (5, N'Main Sites', 1, N'admin', CAST(0x0000A2A4016E9181 AS DateTime), N'admin', CAST(0x0000A2A4016EA4E6 AS DateTime))
GO
INSERT [dbo].[Sites] ([Id], [Name], [Active], [CreatedBy], [Created], [UpdatedBy], [Updated]) VALUES (6, N'Blogs', 1, N'admin', CAST(0x0000A2A4016E9DD9 AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Sites] OFF
GO
SET IDENTITY_INSERT [dbo].[SiteUrls] ON 

GO
INSERT [dbo].[SiteUrls] ([Id], [SiteId], [Url], [Active], [CreatedBy], [Created], [UpdatedBy], [Updated]) VALUES (1, 5, N'http://www.google.com', 1, N'admin', CAST(0x0000A2A4017F8434 AS DateTime), NULL, NULL)
GO
INSERT [dbo].[SiteUrls] ([Id], [SiteId], [Url], [Active], [CreatedBy], [Created], [UpdatedBy], [Updated]) VALUES (2, 5, N'http://yahoo.com', 0, N'admin', CAST(0x0000A2A4017F9081 AS DateTime), N'admin', CAST(0x0000A2A40181BD75 AS DateTime))
GO
INSERT [dbo].[SiteUrls] ([Id], [SiteId], [Url], [Active], [CreatedBy], [Created], [UpdatedBy], [Updated]) VALUES (4, 5, N'http://www.yahoo.com', 1, N'admin', CAST(0x0000A2A40181D75F AS DateTime), NULL, NULL)
GO
INSERT [dbo].[SiteUrls] ([Id], [SiteId], [Url], [Active], [CreatedBy], [Created], [UpdatedBy], [Updated]) VALUES (5, 6, N'yahoo.com', 1, N'admin', CAST(0x0000A2A700FC7F6D AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[SiteUrls] OFF
GO
SET IDENTITY_INSERT [dbo].[VisitorCohorts] ON 

GO
INSERT [dbo].[VisitorCohorts] ([Id], [VisitorId], [CohortId], [Created], [CreatedBy]) VALUES (1, 1, 10, CAST(0x0000A2A9016D4DB8 AS DateTime), NULL)
GO
INSERT [dbo].[VisitorCohorts] ([Id], [VisitorId], [CohortId], [Created], [CreatedBy]) VALUES (2, 4, 10, CAST(0x0000A2AD00B60CFC AS DateTime), NULL)
GO
INSERT [dbo].[VisitorCohorts] ([Id], [VisitorId], [CohortId], [Created], [CreatedBy]) VALUES (3, 7, 10, CAST(0x0000A2AD00B637D0 AS DateTime), NULL)
GO
INSERT [dbo].[VisitorCohorts] ([Id], [VisitorId], [CohortId], [Created], [CreatedBy]) VALUES (4, 10, 10, CAST(0x0000A2AD00B6D6CC AS DateTime), NULL)
GO
INSERT [dbo].[VisitorCohorts] ([Id], [VisitorId], [CohortId], [Created], [CreatedBy]) VALUES (5, 11, 10, CAST(0x0000A2AD00B71ACA AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[VisitorCohorts] OFF
GO
SET IDENTITY_INSERT [dbo].[VisitorConversions] ON 

GO
INSERT [dbo].[VisitorConversions] ([Id], [VisitorId], [GoalId], [Created], [CreatedBy]) VALUES (5, 1, 9, CAST(0x0000A2B000CA1D5A AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[VisitorConversions] OFF
GO
SET IDENTITY_INSERT [dbo].[VisitorLandingPages] ON 

GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (1, 1, N'http://localhost:32137/', CAST(0x0000A2A80177A495 AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (2, 1, N'http://localhost:32137/', CAST(0x0000A2A80178991D AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (3, 1, N'http://localhost:32137/', CAST(0x0000A2A80179045A AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (4, 1, N'http://localhost:32137/', CAST(0x0000A2A801790E54 AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (5, 1, N'http://localhost:32137/Sites', CAST(0x0000A2A8017E46D0 AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (6, 1, N'http://localhost:32137/Account/Login?ReturnUrl=%2fSites', CAST(0x0000A2A8017E50A8 AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (7, 3, N'http://localhost:32137/', CAST(0x0000A2A8017EDE8A AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (8, 3, N'http://localhost:32137/Account/Login?ReturnUrl=%2f', CAST(0x0000A2A8017EDEA7 AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (9, 3, N'http://localhost:32137/Experiments', CAST(0x0000A2A8017F4724 AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (10, 1, N'http://localhost:32137/', CAST(0x0000A2AA014FE92C AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (11, 1, N'http://localhost:32137/Account/Login?ReturnUrl=%2f', CAST(0x0000A2AA014FEF1D AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (12, 1, N'http://localhost:32137/Samples/?Hypothesis=Honey', CAST(0x0000A2AA015004B4 AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (13, 1, N'http://localhost:32137/Samples/?Hypothesis=Vinegar', CAST(0x0000A2AA01500B81 AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (14, 1, N'http://localhost:32137/Samples/', CAST(0x0000A2AA01501059 AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (15, 2, N'http://localhost:32137/', CAST(0x0000A2AA015FB1AD AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (16, 2, N'http://localhost:32137/Account/Login?ReturnUrl=%2f', CAST(0x0000A2AA015FB816 AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (17, 2, N'http://localhost:1234/', CAST(0x0000A2AB011A0FF8 AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (18, 2, N'http://localhost:1234/Account/Login?ReturnUrl=%2f', CAST(0x0000A2AB011A1708 AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (19, 1, N'http://localhost:1234/Samples/?Hypothesis=Honey', CAST(0x0000A2AB011A690C AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (20, 1, N'http://localhost:24614/default.aspx', CAST(0x0000A2AD00B02290 AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (21, 1, N'http://localhost:24614/default.aspx?Hypothesis=Honey', CAST(0x0000A2AD00B07917 AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (22, 1, N'http://localhost:25020/', CAST(0x0000A2AD00B3BA67 AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (23, 4, N'http://localhost:24614/default.aspx', CAST(0x0000A2AD00B60D36 AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (24, 7, N'http://localhost:24614/default.aspx', CAST(0x0000A2AD00B637D2 AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (25, 10, N'http://localhost:24614/default.aspx', CAST(0x0000A2AD00B6D6CC AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (26, 11, N'http://localhost:24614/default.aspx', CAST(0x0000A2AD00B71ACD AS DateTime), NULL)
GO
INSERT [dbo].[VisitorLandingPages] ([Id], [VisitorId], [LandingPageUrl], [Created], [CreatedBy]) VALUES (27, 1, N'http://localhost:1234/', CAST(0x0000A2AF0085E661 AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[VisitorLandingPages] OFF
GO
SET IDENTITY_INSERT [dbo].[VisitorRequests] ON 

GO
INSERT [dbo].[VisitorRequests] ([Id], [VisitorId], [RequestUrl], [Created], [CreatedBy]) VALUES (317, 1, N'http://localhost:32137/', CAST(0x0000A2A80180971D AS DateTime), NULL)
GO
INSERT [dbo].[VisitorRequests] ([Id], [VisitorId], [RequestUrl], [Created], [CreatedBy]) VALUES (318, 1, N'http://localhost:32137/Experiments', CAST(0x0000A2A80180B53B AS DateTime), NULL)
GO
INSERT [dbo].[VisitorRequests] ([Id], [VisitorId], [RequestUrl], [Created], [CreatedBy]) VALUES (319, 3, N'http://localhost:32137/Goals', CAST(0x0000A2A80180C297 AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[VisitorRequests] OFF
GO
SET IDENTITY_INSERT [dbo].[Visitors] ON 

GO
INSERT [dbo].[Visitors] ([Id], [UID], [FirstVisit], [UserName], [IsAuthenticated], [Created], [CreatedBy], [ReconcilledVisitorId]) VALUES (1, N'00000000-0000-0000-0000-000000000000', CAST(0x0000A2A801656F6B AS DateTime), N'admin', 1, CAST(0x0000A2A801656F6C AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Visitors] ([Id], [UID], [FirstVisit], [UserName], [IsAuthenticated], [Created], [CreatedBy], [ReconcilledVisitorId]) VALUES (2, N'b2f45b63-f260-48e3-8d2c-02099dc881c1', CAST(0x0000A2A8017E46B6 AS DateTime), N'', 0, CAST(0x0000A2A8017E46CF AS DateTime), NULL, 1)
GO
INSERT [dbo].[Visitors] ([Id], [UID], [FirstVisit], [UserName], [IsAuthenticated], [Created], [CreatedBy], [ReconcilledVisitorId]) VALUES (3, N'd0f56efd-cbfc-4fc4-8a26-78cc9358ba89', CAST(0x0000A2A8017EDE88 AS DateTime), N'AnonUser', 1, CAST(0x0000A2A8017EDE89 AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Visitors] ([Id], [UID], [FirstVisit], [UserName], [IsAuthenticated], [Created], [CreatedBy], [ReconcilledVisitorId]) VALUES (4, N'2422dfc4-7a22-431c-8eb8-a55f67f74c7f', CAST(0x0000A2AD00B60CAB AS DateTime), N'', 0, CAST(0x0000A2AD00B60CC0 AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Visitors] ([Id], [UID], [FirstVisit], [UserName], [IsAuthenticated], [Created], [CreatedBy], [ReconcilledVisitorId]) VALUES (5, N'850b12de-6367-46ac-86f5-217aee4b24d3', CAST(0x0000A2AD00B60D5E AS DateTime), N'', 0, CAST(0x0000A2AD00B60D60 AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Visitors] ([Id], [UID], [FirstVisit], [UserName], [IsAuthenticated], [Created], [CreatedBy], [ReconcilledVisitorId]) VALUES (6, N'5c3049f2-57c2-4ebd-98b2-0ef30b694480', CAST(0x0000A2AD00B60D5E AS DateTime), N'', 0, CAST(0x0000A2AD00B60D62 AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Visitors] ([Id], [UID], [FirstVisit], [UserName], [IsAuthenticated], [Created], [CreatedBy], [ReconcilledVisitorId]) VALUES (7, N'f3cf8b23-5d05-4094-a60d-edb04ce19811', CAST(0x0000A2AD00B637C9 AS DateTime), N'', 0, CAST(0x0000A2AD00B637CF AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Visitors] ([Id], [UID], [FirstVisit], [UserName], [IsAuthenticated], [Created], [CreatedBy], [ReconcilledVisitorId]) VALUES (8, N'a3637197-f26b-4094-8d29-c038909eca9a', CAST(0x0000A2AD00B637D3 AS DateTime), N'', 0, CAST(0x0000A2AD00B637D4 AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Visitors] ([Id], [UID], [FirstVisit], [UserName], [IsAuthenticated], [Created], [CreatedBy], [ReconcilledVisitorId]) VALUES (9, N'94a315e9-ce7d-412a-adda-d0e9aacd4a7d', CAST(0x0000A2AD00B637D3 AS DateTime), N'', 0, CAST(0x0000A2AD00B637D5 AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Visitors] ([Id], [UID], [FirstVisit], [UserName], [IsAuthenticated], [Created], [CreatedBy], [ReconcilledVisitorId]) VALUES (10, N'b19312b9-7a71-477b-85c1-4a8ed75511ad', CAST(0x0000A2AD00B6B761 AS DateTime), N'', 0, CAST(0x0000A2AD00B6D6CC AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Visitors] ([Id], [UID], [FirstVisit], [UserName], [IsAuthenticated], [Created], [CreatedBy], [ReconcilledVisitorId]) VALUES (11, N'43e6d586-193a-4611-beb7-7283ecd0d898', CAST(0x0000A2AD00B71967 AS DateTime), N'', 0, CAST(0x0000A2AD00B71AC8 AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Visitors] OFF
GO
SET IDENTITY_INSERT [dbo].[VisitorSegments] ON 

GO
INSERT [dbo].[VisitorSegments] ([Id], [VisitorId], [SegmentId], [ExperimentId], [Created], [CreatedBy]) VALUES (31, 1, 24, 5, CAST(0x0000A2B000CA18EE AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[VisitorSegments] OFF
GO
SET ANSI_PADDING ON

GO
ALTER TABLE [dbo].[Cohorts] ADD  CONSTRAINT [CK_Cohorts_SystemName] UNIQUE NONCLUSTERED 
(
	[SystemName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
ALTER TABLE [dbo].[Experiments] ADD  CONSTRAINT [CK_Experiments_SystemName_unique] UNIQUE NONCLUSTERED 
(
	[SystemName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
ALTER TABLE [dbo].[ExperimentSegments] ADD  CONSTRAINT [CK_ExperimentSegments_SystemName_unique] UNIQUE NONCLUSTERED 
(
	[SystemName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
ALTER TABLE [dbo].[ExperimentVariables] ADD  CONSTRAINT [CK_ExperimentVariables_Name] UNIQUE NONCLUSTERED 
(
	[Id] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
ALTER TABLE [dbo].[Goals] ADD  CONSTRAINT [CK_Goals_SystemName_unique] UNIQUE NONCLUSTERED 
(
	[SystemName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
ALTER TABLE [dbo].[Sites] ADD  CONSTRAINT [CK_Sites_NameUnique] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
ALTER TABLE [dbo].[SiteUrls] ADD  CONSTRAINT [CK_SiteUrls_Url_Unique] UNIQUE NONCLUSTERED 
(
	[Url] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
ALTER TABLE [dbo].[Cohorts] ADD  DEFAULT (newid()) FOR [UID]
GO
ALTER TABLE [dbo].[Cohorts] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Experiments] ADD  DEFAULT (newid()) FOR [UID]
GO
ALTER TABLE [dbo].[Experiments] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[ExperimentSegments] ADD  DEFAULT (newid()) FOR [UID]
GO
ALTER TABLE [dbo].[ExperimentSegments] ADD  DEFAULT ((0)) FOR [TargetPercentage]
GO
ALTER TABLE [dbo].[ExperimentSegments] ADD  DEFAULT ((0)) FOR [IsDefault]
GO
ALTER TABLE [dbo].[ExperimentSegmentVariableValues] ADD  DEFAULT ('') FOR [Value]
GO
ALTER TABLE [dbo].[Goals] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Goals] ADD  DEFAULT ((0)) FOR [Value]
GO
ALTER TABLE [dbo].[Sites] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[SiteUrls] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[VisitorAttributes] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[VisitorCohorts] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[VisitorConversions] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[VisitorLandingPages] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[VisitorReferrers] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[VisitorRequests] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[Visitors] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[VisitorSegments] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[CohortProperties]  WITH CHECK ADD  CONSTRAINT [FK_CohortProperties_ToCohort] FOREIGN KEY([CohortId])
REFERENCES [dbo].[Cohorts] ([Id])
GO
ALTER TABLE [dbo].[CohortProperties] CHECK CONSTRAINT [FK_CohortProperties_ToCohort]
GO
ALTER TABLE [dbo].[ExperimentGoals]  WITH CHECK ADD  CONSTRAINT [FK_ExperimentGoals_ToExperiments] FOREIGN KEY([ExperimentId])
REFERENCES [dbo].[Experiments] ([Id])
GO
ALTER TABLE [dbo].[ExperimentGoals] CHECK CONSTRAINT [FK_ExperimentGoals_ToExperiments]
GO
ALTER TABLE [dbo].[ExperimentGoals]  WITH CHECK ADD  CONSTRAINT [FK_ExperimentGoals_ToGoals] FOREIGN KEY([GoalId])
REFERENCES [dbo].[Goals] ([Id])
GO
ALTER TABLE [dbo].[ExperimentGoals] CHECK CONSTRAINT [FK_ExperimentGoals_ToGoals]
GO
ALTER TABLE [dbo].[Experiments]  WITH CHECK ADD  CONSTRAINT [FK_Experiments_ToCohorts] FOREIGN KEY([TargetCohortId])
REFERENCES [dbo].[Cohorts] ([Id])
GO
ALTER TABLE [dbo].[Experiments] CHECK CONSTRAINT [FK_Experiments_ToCohorts]
GO
ALTER TABLE [dbo].[ExperimentSegments]  WITH CHECK ADD  CONSTRAINT [FK_ExperimentSegments_ToExperiments] FOREIGN KEY([ExperimentId])
REFERENCES [dbo].[Experiments] ([Id])
GO
ALTER TABLE [dbo].[ExperimentSegments] CHECK CONSTRAINT [FK_ExperimentSegments_ToExperiments]
GO
ALTER TABLE [dbo].[ExperimentSegmentVariableValues]  WITH CHECK ADD  CONSTRAINT [FK_ExperimentSegmentVariableValues_ToExperimentSegments] FOREIGN KEY([ExperimentSegmentId])
REFERENCES [dbo].[ExperimentSegments] ([Id])
GO
ALTER TABLE [dbo].[ExperimentSegmentVariableValues] CHECK CONSTRAINT [FK_ExperimentSegmentVariableValues_ToExperimentSegments]
GO
ALTER TABLE [dbo].[ExperimentSegmentVariableValues]  WITH CHECK ADD  CONSTRAINT [FK_ExperimentSegmentVariableValues_ToExperimentVariables] FOREIGN KEY([ExperimentVariableId])
REFERENCES [dbo].[ExperimentVariables] ([Id])
GO
ALTER TABLE [dbo].[ExperimentSegmentVariableValues] CHECK CONSTRAINT [FK_ExperimentSegmentVariableValues_ToExperimentVariables]
GO
ALTER TABLE [dbo].[ExperimentVariables]  WITH CHECK ADD  CONSTRAINT [FK_ExperimentVariables_ToExperiments] FOREIGN KEY([ExperimentId])
REFERENCES [dbo].[Experiments] ([Id])
GO
ALTER TABLE [dbo].[ExperimentVariables] CHECK CONSTRAINT [FK_ExperimentVariables_ToExperiments]
GO
ALTER TABLE [dbo].[SiteUrls]  WITH CHECK ADD  CONSTRAINT [FK_SiteUrls_ToSites] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Sites] ([Id])
GO
ALTER TABLE [dbo].[SiteUrls] CHECK CONSTRAINT [FK_SiteUrls_ToSites]
GO
ALTER TABLE [dbo].[VisitorAttributes]  WITH CHECK ADD  CONSTRAINT [FK_VisitorAttributes_ToVisitors] FOREIGN KEY([VisitorId])
REFERENCES [dbo].[Visitors] ([Id])
GO
ALTER TABLE [dbo].[VisitorAttributes] CHECK CONSTRAINT [FK_VisitorAttributes_ToVisitors]
GO
ALTER TABLE [dbo].[VisitorCohorts]  WITH CHECK ADD  CONSTRAINT [FK_VisitorCohorts_ToCohorts] FOREIGN KEY([CohortId])
REFERENCES [dbo].[Cohorts] ([Id])
GO
ALTER TABLE [dbo].[VisitorCohorts] CHECK CONSTRAINT [FK_VisitorCohorts_ToCohorts]
GO
ALTER TABLE [dbo].[VisitorCohorts]  WITH CHECK ADD  CONSTRAINT [FK_VisitorCohorts_ToVisitors] FOREIGN KEY([VisitorId])
REFERENCES [dbo].[Visitors] ([Id])
GO
ALTER TABLE [dbo].[VisitorCohorts] CHECK CONSTRAINT [FK_VisitorCohorts_ToVisitors]
GO
ALTER TABLE [dbo].[VisitorConversions]  WITH CHECK ADD  CONSTRAINT [FK_VisitorCohorts_ToGoals] FOREIGN KEY([GoalId])
REFERENCES [dbo].[Goals] ([Id])
GO
ALTER TABLE [dbo].[VisitorConversions] CHECK CONSTRAINT [FK_VisitorCohorts_ToGoals]
GO
ALTER TABLE [dbo].[VisitorConversions]  WITH CHECK ADD  CONSTRAINT [FK_VisitorConversions_ToVisitors] FOREIGN KEY([VisitorId])
REFERENCES [dbo].[Visitors] ([Id])
GO
ALTER TABLE [dbo].[VisitorConversions] CHECK CONSTRAINT [FK_VisitorConversions_ToVisitors]
GO
ALTER TABLE [dbo].[VisitorLandingPages]  WITH CHECK ADD  CONSTRAINT [FK_VisitorLandingPages_ToVisitors] FOREIGN KEY([VisitorId])
REFERENCES [dbo].[Visitors] ([Id])
GO
ALTER TABLE [dbo].[VisitorLandingPages] CHECK CONSTRAINT [FK_VisitorLandingPages_ToVisitors]
GO
ALTER TABLE [dbo].[VisitorReferrers]  WITH CHECK ADD  CONSTRAINT [FK_VisitorReferrers_ToVisitors] FOREIGN KEY([VisitorId])
REFERENCES [dbo].[Visitors] ([Id])
GO
ALTER TABLE [dbo].[VisitorReferrers] CHECK CONSTRAINT [FK_VisitorReferrers_ToVisitors]
GO
ALTER TABLE [dbo].[VisitorRequests]  WITH CHECK ADD  CONSTRAINT [FK_VisitorRequests_ToVisitors] FOREIGN KEY([VisitorId])
REFERENCES [dbo].[Visitors] ([Id])
GO
ALTER TABLE [dbo].[VisitorRequests] CHECK CONSTRAINT [FK_VisitorRequests_ToVisitors]
GO
ALTER TABLE [dbo].[VisitorSegments]  WITH CHECK ADD  CONSTRAINT [FK_VisitorSegments_ToExperiments] FOREIGN KEY([ExperimentId])
REFERENCES [dbo].[Experiments] ([Id])
GO
ALTER TABLE [dbo].[VisitorSegments] CHECK CONSTRAINT [FK_VisitorSegments_ToExperiments]
GO
ALTER TABLE [dbo].[VisitorSegments]  WITH CHECK ADD  CONSTRAINT [FK_VisitorSegments_ToExperimentSegments] FOREIGN KEY([SegmentId])
REFERENCES [dbo].[ExperimentSegments] ([Id])
GO
ALTER TABLE [dbo].[VisitorSegments] CHECK CONSTRAINT [FK_VisitorSegments_ToExperimentSegments]
GO
ALTER TABLE [dbo].[VisitorSegments]  WITH CHECK ADD  CONSTRAINT [FK_VisitorSegments_ToVisitors] FOREIGN KEY([VisitorId])
REFERENCES [dbo].[Visitors] ([Id])
GO
ALTER TABLE [dbo].[VisitorSegments] CHECK CONSTRAINT [FK_VisitorSegments_ToVisitors]
GO
