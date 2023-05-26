USE [ViewManager]
GO
/****** Object:  Table [dbo].[Log]    Script Date: 5/26/2023 10:10:09 PM ******/
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
/****** Object:  Table [dbo].[LogByOffice]    Script Date: 5/26/2023 10:10:09 PM ******/
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
/****** Object:  Table [dbo].[Office]    Script Date: 5/26/2023 10:10:09 PM ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 5/26/2023 10:10:09 PM ******/
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
/****** Object:  Table [dbo].[Specialization]    Script Date: 5/26/2023 10:10:09 PM ******/
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
/****** Object:  Table [dbo].[StatusByLog]    Script Date: 5/26/2023 10:10:09 PM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 5/26/2023 10:10:09 PM ******/
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
/****** Object:  Table [dbo].[UserOfSpecialization]    Script Date: 5/26/2023 10:10:09 PM ******/
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
INSERT [dbo].[Office] ([ID], [Value]) VALUES (N'1', N'Gym')
INSERT [dbo].[Office] ([ID], [Value]) VALUES (N'32', N'Database Development Laboratory')
INSERT [dbo].[Office] ([ID], [Value]) VALUES (N'33', N'Software protection')
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([ID], [Value]) VALUES (1, N'Teacher')
INSERT [dbo].[Role] ([ID], [Value]) VALUES (2, N'Admin')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Specialization] ON 

INSERT [dbo].[Specialization] ([ID], [Value]) VALUES (3, N'Math')
INSERT [dbo].[Specialization] ([ID], [Value]) VALUES (4, N'Network and system administration')
INSERT [dbo].[Specialization] ([ID], [Value]) VALUES (5, N'Economics and Accounting')
INSERT [dbo].[Specialization] ([ID], [Value]) VALUES (6, N'Banking')
INSERT [dbo].[Specialization] ([ID], [Value]) VALUES (7, N'Information systems and programming')
INSERT [dbo].[Specialization] ([ID], [Value]) VALUES (8, N'Protection in emergency situations')
INSERT [dbo].[Specialization] ([ID], [Value]) VALUES (9, N'Hotel business')
INSERT [dbo].[Specialization] ([ID], [Value]) VALUES (10, N'Cooking and confectionery business')
INSERT [dbo].[Specialization] ([ID], [Value]) VALUES (11, N'Law and organization of social security')
INSERT [dbo].[Specialization] ([ID], [Value]) VALUES (13, N'Tourism')
INSERT [dbo].[Specialization] ([ID], [Value]) VALUES (14, N'Pharmacy')
INSERT [dbo].[Specialization] ([ID], [Value]) VALUES (17, N'Physical Culture')
SET IDENTITY_INSERT [dbo].[Specialization] OFF
GO
INSERT [dbo].[User] ([ID], [FirstName], [LastName], [SecondName], [Login], [Password], [RoleID], [OfficeID]) VALUES (N'1', N'Andrew', N'Fedoseev', N'Yuirivich', N'qwerty', N'AHcp0TRF+/Dfc8o+dJWRLhEkT+SJT4MJyOJxUh9FlHn8lUJWuv3uaqezSGd5/SeYKQ==', 1, N'32')
INSERT [dbo].[User] ([ID], [FirstName], [LastName], [SecondName], [Login], [Password], [RoleID], [OfficeID]) VALUES (N'1803faab-3c2c-4caa-aa31-0adfb4d5812a', N'Clown', N'Ilya', N'Aleksandrovich', N'C7rX2N', N'AEhs52CAqyfVmkPgndeDShmjWfTx7ollIuTuwjF6O/KKj308XCNxuxu0TvhRE0etIQ==', 2, N'32')
INSERT [dbo].[User] ([ID], [FirstName], [LastName], [SecondName], [Login], [Password], [RoleID], [OfficeID]) VALUES (N'597fb4ae-fc76-4a64-a34b-3d39286f5d66', N'Sergei', N'Andreev', N'Andreevich', N'dqcgN', N'ADYJd8pjgeEBlCIgJVhAGLpOelGWI4+lORIew4uudQ1NMmkQAQpv8AzjXb4xzzRXUg==', 1, N'32')
INSERT [dbo].[User] ([ID], [FirstName], [LastName], [SecondName], [Login], [Password], [RoleID], [OfficeID]) VALUES (N'839d14d1-f286-4b6f-977c-b15bb30747e1', N'Damir', N'Dshigunov', N'Sultanovich', N'oqKPbn', N'AFeHRnk9+PgztKEYuPi5U7NsemaFlppO8XtiHLSXwkDe4x3PCg8H/vp4LpG0DJ2Tjw==', 1, N'32')
INSERT [dbo].[User] ([ID], [FirstName], [LastName], [SecondName], [Login], [Password], [RoleID], [OfficeID]) VALUES (N'a2b8f507-e33c-4e62-b57c-ac0b3dc4b02b', N'Denis', N'Ponomarenko', N'Yurivich', N'ucHe6v', N'AHOAWvrxX635QGFzOQn7G8XatfW7PEoFBlr1SbtNRXSUipx+K/bXxkzeFImCDyoKng==', 1, N'32')
GO
INSERT [dbo].[UserOfSpecialization] ([UserID], [SpecializationID]) VALUES (N'1803faab-3c2c-4caa-aa31-0adfb4d5812a', 3)
INSERT [dbo].[UserOfSpecialization] ([UserID], [SpecializationID]) VALUES (N'597fb4ae-fc76-4a64-a34b-3d39286f5d66', 3)
INSERT [dbo].[UserOfSpecialization] ([UserID], [SpecializationID]) VALUES (N'839d14d1-f286-4b6f-977c-b15bb30747e1', 3)
INSERT [dbo].[UserOfSpecialization] ([UserID], [SpecializationID]) VALUES (N'a2b8f507-e33c-4e62-b57c-ac0b3dc4b02b', 3)
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
