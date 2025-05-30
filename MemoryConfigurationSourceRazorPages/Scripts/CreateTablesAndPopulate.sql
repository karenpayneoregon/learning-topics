USE [AppsettingsConfigurations]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 3/3/2025 4:10:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Section] [nvarchar](max) NOT NULL,
	[Key] [nvarchar](max) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Settings] ON 

INSERT [dbo].[Settings] ([Id], [Section], [Key], [Value]) VALUES (1, N'HelpDesk', N'Phone', N'555-555-1234')
INSERT [dbo].[Settings] ([Id], [Section], [Key], [Value]) VALUES (2, N'HelpDesk', N'Email', N'ServiceDesk@SomeCompany.net')
SET IDENTITY_INSERT [dbo].[Settings] OFF
GO
USE [master]
GO
ALTER DATABASE [AppsettingsConfigurations] SET  READ_WRITE 
GO
