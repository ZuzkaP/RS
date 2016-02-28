
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/21/2016 20:08:46
-- Generated from EDMX file: C:\Users\Zuzana\Documents\Visual Studio 2015\Projects\RS\RS\Models\All.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Database1];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__users_rol__roles__2A4B4B5E]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[users_roles] DROP CONSTRAINT [FK__users_rol__roles__2A4B4B5E];
GO
IF OBJECT_ID(N'[dbo].[FK__users_rol__users__29572725]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[users_roles] DROP CONSTRAINT [FK__users_rol__users__29572725];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[users_roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[users_roles];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int  NOT NULL,
    [name] varchar(50)  NOT NULL,
    [decription] varchar(50)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [user_id] int IDENTITY(1,1) NOT NULL,
    [email] varchar(80)  NOT NULL,
    [first_name] varchar(100)  NOT NULL,
    [last_name] varchar(100)  NOT NULL,
    [password] char(41)  NOT NULL,
    [phone_number] varchar(50)  NOT NULL
);
GO

-- Creating table 'users_roles'
CREATE TABLE [dbo].[users_roles] (
    [users_roles_id] int  NOT NULL,
    [users_id] int  NULL,
    [roles_id] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [user_id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([user_id] ASC);
GO

-- Creating primary key on [users_roles_id] in table 'users_roles'
ALTER TABLE [dbo].[users_roles]
ADD CONSTRAINT [PK_users_roles]
    PRIMARY KEY CLUSTERED ([users_roles_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [roles_id] in table 'users_roles'
ALTER TABLE [dbo].[users_roles]
ADD CONSTRAINT [FK__users_rol__roles__2A4B4B5E]
    FOREIGN KEY ([roles_id])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__users_rol__roles__2A4B4B5E'
CREATE INDEX [IX_FK__users_rol__roles__2A4B4B5E]
ON [dbo].[users_roles]
    ([roles_id]);
GO

-- Creating foreign key on [users_id] in table 'users_roles'
ALTER TABLE [dbo].[users_roles]
ADD CONSTRAINT [FK__users_rol__users__29572725]
    FOREIGN KEY ([users_id])
    REFERENCES [dbo].[Users]
        ([user_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__users_rol__users__29572725'
CREATE INDEX [IX_FK__users_rol__users__29572725]
ON [dbo].[users_roles]
    ([users_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------