USE [MyDB]
GO
/****** Object:  Table [dbo].[Expenses]    Script Date: 31-01-2026 17:55:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expenses](
	[ExpenseId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[TypeId] [int] NOT NULL,
	[Amount] [decimal](15, 2) NOT NULL,
	[Description] [varchar](255) NULL,
	[ExpenseDate] [date] NOT NULL,
	[CreatedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[ExpenseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Income]    Script Date: 31-01-2026 17:55:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Income](
	[IncomeId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[TypeId] [int] NOT NULL,
	[Amount] [decimal](15, 2) NOT NULL,
	[Description] [varchar](255) NULL,
	[IncomeDate] [date] NOT NULL,
	[CreatedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[IncomeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeMaster]    Script Date: 31-01-2026 17:55:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeMaster](
	[TypeId] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [varchar](50) NOT NULL,
	[Description] [varchar](255) NULL,
	[IsActive] [bit] NULL,
	[IsIncome] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[TypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 31-01-2026 17:55:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[PasswordHash] [varchar](255) NOT NULL,
	[CreatedAt] [datetime2](7) NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Expenses] ON 
GO
INSERT [dbo].[Expenses] ([ExpenseId], [UserId], [TypeId], [Amount], [Description], [ExpenseDate], [CreatedAt]) VALUES (1, 1, 2, CAST(15000.00 AS Decimal(15, 2)), N'Groceries January', CAST(N'2026-01-10' AS Date), CAST(N'2026-01-09T21:03:26.9666667' AS DateTime2))
GO
INSERT [dbo].[Expenses] ([ExpenseId], [UserId], [TypeId], [Amount], [Description], [ExpenseDate], [CreatedAt]) VALUES (2, 1, 2, CAST(25000.00 AS Decimal(15, 2)), N'January Rent', CAST(N'2026-01-01' AS Date), CAST(N'2026-01-09T21:03:26.9666667' AS DateTime2))
GO
INSERT [dbo].[Expenses] ([ExpenseId], [UserId], [TypeId], [Amount], [Description], [ExpenseDate], [CreatedAt]) VALUES (3, 1, 2, CAST(20000.00 AS Decimal(15, 2)), N'Other', CAST(N'2026-01-01' AS Date), CAST(N'2026-01-09T21:03:26.9666667' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Expenses] OFF
GO
SET IDENTITY_INSERT [dbo].[Income] ON 
GO
INSERT [dbo].[Income] ([IncomeId], [UserId], [TypeId], [Amount], [Description], [IncomeDate], [CreatedAt]) VALUES (10, 1, 1, CAST(100000.00 AS Decimal(15, 2)), N' Salary', CAST(N'2026-01-05' AS Date), CAST(N'2026-01-09T21:01:55.0900000' AS DateTime2))
GO
INSERT [dbo].[Income] ([IncomeId], [UserId], [TypeId], [Amount], [Description], [IncomeDate], [CreatedAt]) VALUES (11, 1, 1, CAST(30000.00 AS Decimal(15, 2)), N'Other income', CAST(N'2026-02-05' AS Date), CAST(N'2026-01-09T21:01:55.0900000' AS DateTime2))
GO
INSERT [dbo].[Income] ([IncomeId], [UserId], [TypeId], [Amount], [Description], [IncomeDate], [CreatedAt]) VALUES (12, 1, 1, CAST(8000.00 AS Decimal(15, 2)), N'share divident', CAST(N'2026-01-05' AS Date), CAST(N'2026-01-09T21:01:55.0900000' AS DateTime2))
GO
INSERT [dbo].[Income] ([IncomeId], [UserId], [TypeId], [Amount], [Description], [IncomeDate], [CreatedAt]) VALUES (13, 1, 1, CAST(980000.00 AS Decimal(15, 2)), N'divident', CAST(N'2026-01-11' AS Date), CAST(N'2026-01-11T11:59:36.3286421' AS DateTime2))
GO
INSERT [dbo].[Income] ([IncomeId], [UserId], [TypeId], [Amount], [Description], [IncomeDate], [CreatedAt]) VALUES (14, 1, 1, CAST(200.00 AS Decimal(15, 2)), N'div', CAST(N'2026-01-11' AS Date), CAST(N'2026-01-11T13:09:21.0578857' AS DateTime2))
GO
INSERT [dbo].[Income] ([IncomeId], [UserId], [TypeId], [Amount], [Description], [IncomeDate], [CreatedAt]) VALUES (15, 1, 2, CAST(45554.00 AS Decimal(15, 2)), N'', CAST(N'2026-01-11' AS Date), CAST(N'2026-01-11T13:11:01.5955774' AS DateTime2))
GO
INSERT [dbo].[Income] ([IncomeId], [UserId], [TypeId], [Amount], [Description], [IncomeDate], [CreatedAt]) VALUES (16, 1, 3, CAST(450.00 AS Decimal(15, 2)), N'', CAST(N'2026-01-12' AS Date), CAST(N'2026-01-12T06:28:51.6282606' AS DateTime2))
GO
INSERT [dbo].[Income] ([IncomeId], [UserId], [TypeId], [Amount], [Description], [IncomeDate], [CreatedAt]) VALUES (17, 1, 3, CAST(6800.00 AS Decimal(15, 2)), N'', CAST(N'2026-01-12' AS Date), CAST(N'2026-01-12T06:49:41.2477318' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Income] OFF
GO
SET IDENTITY_INSERT [dbo].[TypeMaster] ON 
GO
INSERT [dbo].[TypeMaster] ([TypeId], [TypeName], [Description], [IsActive], [IsIncome]) VALUES (1, N'Salary', N'All income sources', 1, 1)
GO
INSERT [dbo].[TypeMaster] ([TypeId], [TypeName], [Description], [IsActive], [IsIncome]) VALUES (2, N'Glossary', N'All expense categories', 1, 0)
GO
INSERT [dbo].[TypeMaster] ([TypeId], [TypeName], [Description], [IsActive], [IsIncome]) VALUES (3, N'Divident', N'Income from share', 1, 1)
GO
INSERT [dbo].[TypeMaster] ([TypeId], [TypeName], [Description], [IsActive], [IsIncome]) VALUES (4, N'Petroal', N'Travel expenses', 1, 0)
GO
INSERT [dbo].[TypeMaster] ([TypeId], [TypeName], [Description], [IsActive], [IsIncome]) VALUES (5, N'Party', N'expenses', 1, 0)
GO
INSERT [dbo].[TypeMaster] ([TypeId], [TypeName], [Description], [IsActive], [IsIncome]) VALUES (6, N'Outstation Travel', N'expenses', 1, 0)
GO
INSERT [dbo].[TypeMaster] ([TypeId], [TypeName], [Description], [IsActive], [IsIncome]) VALUES (7, N'Electricity Bill', N'expenses', 1, 0)
GO
INSERT [dbo].[TypeMaster] ([TypeId], [TypeName], [Description], [IsActive], [IsIncome]) VALUES (8, N'Mobile Bill', N'expenses', 1, 0)
GO
INSERT [dbo].[TypeMaster] ([TypeId], [TypeName], [Description], [IsActive], [IsIncome]) VALUES (9, N'Dairy products', N'expenses', 1, 0)
GO
INSERT [dbo].[TypeMaster] ([TypeId], [TypeName], [Description], [IsActive], [IsIncome]) VALUES (10, N'Shopping', N'expenses', 1, 0)
GO
INSERT [dbo].[TypeMaster] ([TypeId], [TypeName], [Description], [IsActive], [IsIncome]) VALUES (11, N'Broadband recharge', N'expenses', 1, 0)
GO
INSERT [dbo].[TypeMaster] ([TypeId], [TypeName], [Description], [IsActive], [IsIncome]) VALUES (12, N'Rent', N'expenses', 1, 0)
GO
SET IDENTITY_INSERT [dbo].[TypeMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([UserId], [Username], [Email], [PasswordHash], [CreatedAt], [IsActive]) VALUES (1, N'harshal', N'harshal@gmail.com', N'HASH123', CAST(N'2026-01-09T20:54:46.5200000' AS DateTime2), 1)
GO
INSERT [dbo].[Users] ([UserId], [Username], [Email], [PasswordHash], [CreatedAt], [IsActive]) VALUES (2, N'rahul', N'rahul@gmail.com', N'HASH456', CAST(N'2026-01-09T20:54:46.5200000' AS DateTime2), 1)
GO
INSERT [dbo].[Users] ([UserId], [Username], [Email], [PasswordHash], [CreatedAt], [IsActive]) VALUES (3, N'priya', N'priya@gmail.com', N'HASH789', CAST(N'2026-01-09T20:54:46.5200000' AS DateTime2), 1)
GO
INSERT [dbo].[Users] ([UserId], [Username], [Email], [PasswordHash], [CreatedAt], [IsActive]) VALUES (4, N'Naipunya', N'naipunya@gmail.com', N'nniti', CAST(N'2026-01-11T04:44:25.8140000' AS DateTime2), 1)
GO
INSERT [dbo].[Users] ([UserId], [Username], [Email], [PasswordHash], [CreatedAt], [IsActive]) VALUES (5, N'vishi', N'vv@gmail.com', N'gukashdh', CAST(N'2026-01-11T10:05:40.2608401' AS DateTime2), 1)
GO
INSERT [dbo].[Users] ([UserId], [Username], [Email], [PasswordHash], [CreatedAt], [IsActive]) VALUES (6, N'Mani', N'Mani@gamil.com', N'Mani', CAST(N'2026-01-11T11:10:44.5264326' AS DateTime2), 1)
GO
INSERT [dbo].[Users] ([UserId], [Username], [Email], [PasswordHash], [CreatedAt], [IsActive]) VALUES (7, N'Vithal', N'Vithal@gmail.com', N'Vithal', CAST(N'2026-01-11T11:25:49.0280636' AS DateTime2), 1)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__536C85E4462E309B]    Script Date: 31-01-2026 17:55:53 ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A9D10534E70F0009]    Script Date: 31-01-2026 17:55:53 ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Expenses] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Income] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[TypeMaster] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Expenses]  WITH CHECK ADD FOREIGN KEY([TypeId])
REFERENCES [dbo].[TypeMaster] ([TypeId])
GO
ALTER TABLE [dbo].[Expenses]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Income]  WITH CHECK ADD FOREIGN KEY([TypeId])
REFERENCES [dbo].[TypeMaster] ([TypeId])
GO
ALTER TABLE [dbo].[Income]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
