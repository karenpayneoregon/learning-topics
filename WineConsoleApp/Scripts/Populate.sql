USE [EF.Wines]
GO
/****** Object:  Table [dbo].[Wines]    Script Date: 7/5/2025 9:06:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wines](
	[WineId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[WineType] [int] NOT NULL,
 CONSTRAINT [PK_Wines] PRIMARY KEY CLUSTERED 
(
	[WineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WineType]    Script Date: 7/5/2025 9:06:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WineType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_WineType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Wines] ON 
GO
INSERT [dbo].[Wines] ([WineId], [Name], [WineType]) VALUES (1, N'Veuve Clicquot Rose', 1)
GO
INSERT [dbo].[Wines] ([WineId], [Name], [WineType]) VALUES (2, N'Whispering Angel Rose', 3)
GO
INSERT [dbo].[Wines] ([WineId], [Name], [WineType]) VALUES (3, N'Pinot Grigi', 2)
GO
INSERT [dbo].[Wines] ([WineId], [Name], [WineType]) VALUES (4, N'White Zinfandel', 3)
GO
INSERT [dbo].[Wines] ([WineId], [Name], [WineType]) VALUES (5, N'Chateau Ste. Michelle Riesling', 2)
GO
INSERT [dbo].[Wines] ([WineId], [Name], [WineType]) VALUES (6, N'Robert Mondavi Cabernet Sauvignon', 1)
GO
INSERT [dbo].[Wines] ([WineId], [Name], [WineType]) VALUES (7, N'Kim Crawford Sauvignon Blanc', 2)
GO
INSERT [dbo].[Wines] ([WineId], [Name], [WineType]) VALUES (8, N'Beringer White Merlot', 3)
GO
INSERT [dbo].[Wines] ([WineId], [Name], [WineType]) VALUES (9, N'La Marca Prosecco', 2)
GO
INSERT [dbo].[Wines] ([WineId], [Name], [WineType]) VALUES (10, N'Josh Cellars Pinot Noir', 1)
GO
INSERT [dbo].[Wines] ([WineId], [Name], [WineType]) VALUES (11, N'Sutter Home Moscato', 2)
GO
INSERT [dbo].[Wines] ([WineId], [Name], [WineType]) VALUES (12, N'Apothic Red Blend', 1)
GO
INSERT [dbo].[Wines] ([WineId], [Name], [WineType]) VALUES (13, N'Barefoot Pink Moscato', 3)
GO
INSERT [dbo].[Wines] ([WineId], [Name], [WineType]) VALUES (14, N'Kendall-Jackson Vintner’s Reserve Chardonnay', 2)
GO
INSERT [dbo].[Wines] ([WineId], [Name], [WineType]) VALUES (15, N'Meiomi Pinot Noir', 1)
GO
INSERT [dbo].[Wines] ([WineId], [Name], [WineType]) VALUES (16, N'Ménage à Trois Red Blend', 1)
GO
SET IDENTITY_INSERT [dbo].[Wines] OFF
GO
SET IDENTITY_INSERT [dbo].[WineType] ON 
GO
INSERT [dbo].[WineType] ([Id], [TypeName], [Description]) VALUES (1, N'Red', N'Classic red')
GO
INSERT [dbo].[WineType] ([Id], [TypeName], [Description]) VALUES (2, N'White', N'Dinner white')
GO
INSERT [dbo].[WineType] ([Id], [TypeName], [Description]) VALUES (3, N'Rose', N'Imported rose')
GO
SET IDENTITY_INSERT [dbo].[WineType] OFF
GO
USE [master]
GO
ALTER DATABASE [EF.Wines] SET  READ_WRITE 
GO
