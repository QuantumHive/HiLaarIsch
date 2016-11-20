/* NOTE
 * This script is intended for the LIVE development/test database deployed in the cloud.
 * This scripts will be maintained solely for the purpose of the live deployment of this application.
 * For every database changes, the LIVE database will be reset based on this script.
 * !!!Do NOT use this on your local database!!! Entity Framework's DB Initialization Strategy is used for local development.
 */

----------
-- DROP --
----------

ALTER TABLE [dbo].[Customers] DROP CONSTRAINT [FK_dbo.Customers_dbo.Users_FK_UserId]
GO
/****** Object:  Index [PK_UserId]    Script Date: 20-11-2016 21:04:13 ******/
DROP INDEX [PK_UserId] ON [dbo].[Users]
GO
/****** Object:  Index [IX_Email]    Script Date: 20-11-2016 21:04:13 ******/
DROP INDEX [IX_Email] ON [dbo].[Users]
GO
/****** Object:  Index [PK_CustomerId]    Script Date: 20-11-2016 21:04:13 ******/
DROP INDEX [PK_CustomerId] ON [dbo].[Customers]
GO
/****** Object:  Index [IX_FK_UserId]    Script Date: 20-11-2016 21:04:13 ******/
DROP INDEX [IX_FK_UserId] ON [dbo].[Customers]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 20-11-2016 21:04:13 ******/
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 20-11-2016 21:04:13 ******/
DROP TABLE [dbo].[Customers]
GO

------------
-- CREATE --
------------

/****** Object:  Table [dbo].[Customers]    Script Date: 20-11-2016 21:04:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [nvarchar](100) NOT NULL,
	[Firstname] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[PhoneNumber] [nvarchar](100) NOT NULL,
	[EmergencyNumber] [nvarchar](100) NOT NULL,
	[Level] [tinyint] NOT NULL,
	[FK_UserId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 20-11-2016 21:04:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](100) NULL,
	[Role] [tinyint] NOT NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Index [IX_FK_UserId]    Script Date: 20-11-2016 21:04:13 ******/
CREATE NONCLUSTERED INDEX [IX_FK_UserId] ON [dbo].[Customers]
(
	[FK_UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_CustomerId]    Script Date: 20-11-2016 21:04:13 ******/
CREATE NONCLUSTERED INDEX [PK_CustomerId] ON [dbo].[Customers]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Email]    Script Date: 20-11-2016 21:04:13 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Email] ON [dbo].[Users]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_UserId]    Script Date: 20-11-2016 21:04:13 ******/
CREATE NONCLUSTERED INDEX [PK_UserId] ON [dbo].[Users]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Customers_dbo.Users_FK_UserId] FOREIGN KEY([FK_UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_dbo.Customers_dbo.Users_FK_UserId]
GO

----------
-- SEED --
----------

INSERT INTO [dbo].[Users] ([PasswordHash], [Email], [EmailConfirmed], [Role]) VALUES ('AGtWGj6m/ICb16LNfGINdZTceQH9xdE9l9QbFKhuv0DaoDI2Ja/AeVCCK8BZey3I9g==', 'admin@manegearnhem.nl', 1, 1)
GO