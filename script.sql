USE [ViewManager]
GO
/****** Object:  Table [dbo].[Log]    Script Date: 1/16/2023 8:56:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LogByOffice]    Script Date: 1/16/2023 8:56:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogByOffice](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OfficeID] [nvarchar](10) NOT NULL,
	[LogID] [int] NOT NULL,
	[StatusByLogID] [int] NOT NULL,
	[Time] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Office]    Script Date: 1/16/2023 8:56:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Office](
	[ID] [nvarchar](10) NOT NULL,
	[Value] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 1/16/2023 8:56:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Specialization]    Script Date: 1/16/2023 8:56:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specialization](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StatusByLog]    Script Date: 1/16/2023 8:56:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusByLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 1/16/2023 8:56:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[SecondName] [nvarchar](50) NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](150) NOT NULL,
	[RoleID] [int] NOT NULL,
	[OfficeID] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK__User__3214EC27324AD3A8] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserOfSpecialization]    Script Date: 1/16/2023 8:56:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserOfSpecialization](
	[UserID] [nvarchar](50) NOT NULL,
	[SpecializationID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[SpecializationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Office] ([ID], [Value]) VALUES (N'32', N'Database Development Laboratory')
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([ID], [Value]) VALUES (1, N'Teacher')
INSERT [dbo].[Role] ([ID], [Value]) VALUES (2, N'Admin')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Specialization] ON 

INSERT [dbo].[Specialization] ([ID], [Value]) VALUES (3, N'string')
SET IDENTITY_INSERT [dbo].[Specialization] OFF
GO
INSERT [dbo].[User] ([ID], [FirstName], [LastName], [SecondName], [Login], [Password], [RoleID], [OfficeID]) VALUES (N'1', N'Andrew', N'Fedoseev', N'Yuirivich', N'qwerty', N'AHcp0TRF+/Dfc8o+dJWRLhEkT+SJT4MJyOJxUh9FlHn8lUJWuv3uaqezSGd5/SeYKQ==', 1, N'32')
INSERT [dbo].[User] ([ID], [FirstName], [LastName], [SecondName], [Login], [Password], [RoleID], [OfficeID]) VALUES (N'2', N'Yaroslav', N'Dubrovin', N'Konstantinovich', N'asd', N'AFTkuuWr/qS0x6GnX1pXJi8ot/fqaMgokOEUNWYfvh3757z7wIz6quywCRHDHbxpDQ==', 1, N'32')
INSERT [dbo].[User] ([ID], [FirstName], [LastName], [SecondName], [Login], [Password], [RoleID], [OfficeID]) VALUES (N'597fb4ae-fc76-4a64-a34b-3d39286f5d66', N'Sergei', N'Andreev', N'Andreevich', N'dqcgN3', N'AKN1s0v/fDo3nCa3Lxyq9/d3KnOwQp1wzmdy/m7T4oDVt9lGFh8Q8FJWMbQQ4p0Yxg==', 1, N'32')
GO
INSERT [dbo].[UserOfSpecialization] ([UserID], [SpecializationID]) VALUES (N'597fb4ae-fc76-4a64-a34b-3d39286f5d66', 3)
GO
ALTER TABLE [dbo].[LogByOffice]  WITH CHECK ADD FOREIGN KEY([LogID])
REFERENCES [dbo].[Log] ([ID])
GO
ALTER TABLE [dbo].[LogByOffice]  WITH CHECK ADD FOREIGN KEY([OfficeID])
REFERENCES [dbo].[Office] ([ID])
GO
ALTER TABLE [dbo].[LogByOffice]  WITH CHECK ADD FOREIGN KEY([StatusByLogID])
REFERENCES [dbo].[StatusByLog] ([ID])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK__User__OfficeID__2B3F6F97] FOREIGN KEY([OfficeID])
REFERENCES [dbo].[Office] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK__User__OfficeID__2B3F6F97]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK__User__RoleID__2A4B4B5E] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK__User__RoleID__2A4B4B5E]
GO
ALTER TABLE [dbo].[UserOfSpecialization]  WITH CHECK ADD FOREIGN KEY([SpecializationID])
REFERENCES [dbo].[Specialization] ([ID])
GO
ALTER TABLE [dbo].[UserOfSpecialization]  WITH CHECK ADD  CONSTRAINT [FK__UserOfSpe__UserI__36B12243] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[UserOfSpecialization] CHECK CONSTRAINT [FK__UserOfSpe__UserI__36B12243]
GO
