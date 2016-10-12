CREATE TABLE [dbo].[Users] (
    [Id] uniqueidentifier NOT NULL,
    [Username] nvarchar(100) NOT NULL,
    [PasswordHash] nvarchar(100) NULL,
    [Email] nvarchar(100) NULL,
    [EmailConfirmed] bit NOT NULL,
    [SecurityStamp] uniqueidentifier NULL,
    [Role] tinyint NOT NULL,
    CONSTRAINT [PK_UserId] PRIMARY KEY ([Id])
)
GO

CREATE TABLE [dbo].[Customers] (
    [Id] uniqueidentifier NOT NULL,
    [Surname] nvarchar(100) NOT NULL,
    [Firstname] nvarchar(100) NOT NULL,
    [DateOfBirth] date NOT NULL,
    [Gender] bit NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_CustomerId] PRIMARY KEY ([Id])
)
GO

ALTER TABLE [dbo].[Customers] WITH CHECK ADD CONSTRAINT [FK_Customers_Users] FOREIGN KEY([UserId]) REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_Users]
GO

INSERT INTO [dbo].[Users] VALUES ('1f88d96d-ae68-4a25-9e92-2e27bbaf750f', 'admin', 'AGtWGj6m/ICb16LNfGINdZTceQH9xdE9l9QbFKhuv0DaoDI2Ja/AeVCCK8BZey3I9g==', 'admin@managearnhem.nl', 1, NULL, 1)
GO