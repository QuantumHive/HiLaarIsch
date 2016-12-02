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
ALTER TABLE [dbo].[CustomerClasses] DROP CONSTRAINT [FK_dbo.CustomerClasses_dbo.Horses_FK_HorseId]
GO
ALTER TABLE [dbo].[CustomerClasses] DROP CONSTRAINT [FK_dbo.CustomerClasses_dbo.Customers_FK_CustomerId]
GO
ALTER TABLE [dbo].[CustomerClasses] DROP CONSTRAINT [FK_dbo.CustomerClasses_dbo.Classes_FK_ClassId]
GO
/****** Object:  Index [IX_Email]    Script Date: 2-12-2016 13:33:21 ******/
DROP INDEX [IX_Email] ON [dbo].[Users]
GO
/****** Object:  Index [IX_FK_UserId]    Script Date: 2-12-2016 13:33:21 ******/
DROP INDEX [IX_FK_UserId] ON [dbo].[Customers]
GO
/****** Object:  Index [IX_FK_HorseId]    Script Date: 2-12-2016 13:33:21 ******/
DROP INDEX [IX_FK_HorseId] ON [dbo].[CustomerClasses]
GO
/****** Object:  Index [IX_FK_CustomerId]    Script Date: 2-12-2016 13:33:21 ******/
DROP INDEX [IX_FK_CustomerId] ON [dbo].[CustomerClasses]
GO
/****** Object:  Index [IX_FK_ClassId]    Script Date: 2-12-2016 13:33:21 ******/
DROP INDEX [IX_FK_ClassId] ON [dbo].[CustomerClasses]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2-12-2016 13:33:21 ******/
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[Horses]    Script Date: 2-12-2016 13:33:21 ******/
DROP TABLE [dbo].[Horses]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 2-12-2016 13:33:21 ******/
DROP TABLE [dbo].[Customers]
GO
/****** Object:  Table [dbo].[CustomerClasses]    Script Date: 2-12-2016 13:33:21 ******/
DROP TABLE [dbo].[CustomerClasses]
GO
/****** Object:  Table [dbo].[Classes]    Script Date: 2-12-2016 13:33:21 ******/
DROP TABLE [dbo].[Classes]
GO

------------
-- CREATE --
------------

/****** Object:  Table [dbo].[Classes]    Script Date: 2-12-2016 13:33:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [tinyint] NOT NULL,
	[Level] [tinyint] NOT NULL,
	[Day] [tinyint] NOT NULL,
	[Length] [tinyint] NOT NULL,
	[Time] [time](7) NOT NULL,
 CONSTRAINT [PK_dbo.Classes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomerClasses]    Script Date: 2-12-2016 13:33:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerClasses](
	[FK_CustomerId] [int] NOT NULL,
	[FK_ClassId] [int] NOT NULL,
	[FK_HorseId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.CustomerClasses] PRIMARY KEY CLUSTERED 
(
	[FK_CustomerId] ASC,
	[FK_ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customers]    Script Date: 2-12-2016 13:33:22 ******/
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
/****** Object:  Table [dbo].[Horses]    Script Date: 2-12-2016 13:33:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Horses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_dbo.Horses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 2-12-2016 13:33:22 ******/
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
/****** Object:  Index [IX_FK_ClassId]    Script Date: 2-12-2016 13:33:22 ******/
CREATE NONCLUSTERED INDEX [IX_FK_ClassId] ON [dbo].[CustomerClasses]
(
	[FK_ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_CustomerId]    Script Date: 2-12-2016 13:33:22 ******/
CREATE NONCLUSTERED INDEX [IX_FK_CustomerId] ON [dbo].[CustomerClasses]
(
	[FK_CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_HorseId]    Script Date: 2-12-2016 13:33:22 ******/
CREATE NONCLUSTERED INDEX [IX_FK_HorseId] ON [dbo].[CustomerClasses]
(
	[FK_HorseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_UserId]    Script Date: 2-12-2016 13:33:22 ******/
CREATE NONCLUSTERED INDEX [IX_FK_UserId] ON [dbo].[Customers]
(
	[FK_UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Email]    Script Date: 2-12-2016 13:33:22 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Email] ON [dbo].[Users]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CustomerClasses]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CustomerClasses_dbo.Classes_FK_ClassId] FOREIGN KEY([FK_ClassId])
REFERENCES [dbo].[Classes] ([Id])
GO
ALTER TABLE [dbo].[CustomerClasses] CHECK CONSTRAINT [FK_dbo.CustomerClasses_dbo.Classes_FK_ClassId]
GO
ALTER TABLE [dbo].[CustomerClasses]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CustomerClasses_dbo.Customers_FK_CustomerId] FOREIGN KEY([FK_CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[CustomerClasses] CHECK CONSTRAINT [FK_dbo.CustomerClasses_dbo.Customers_FK_CustomerId]
GO
ALTER TABLE [dbo].[CustomerClasses]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CustomerClasses_dbo.Horses_FK_HorseId] FOREIGN KEY([FK_HorseId])
REFERENCES [dbo].[Horses] ([Id])
GO
ALTER TABLE [dbo].[CustomerClasses] CHECK CONSTRAINT [FK_dbo.CustomerClasses_dbo.Horses_FK_HorseId]
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