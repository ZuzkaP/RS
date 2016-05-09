
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/09/2016 20:01:03
-- Generated from EDMX file: C:\Users\BeePM\Documents\Visual Studio 2015\Projects\RS\RS\Models\UsersModel.edmx
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

IF OBJECT_ID(N'[dbo].[FK__Permanent__user___3E1D39E1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Permanents] DROP CONSTRAINT [FK__Permanent__user___3E1D39E1];
GO
IF OBJECT_ID(N'[dbo].[FK__Trainings__user___29221CFB]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Trainings] DROP CONSTRAINT [FK__Trainings__user___29221CFB];
GO
IF OBJECT_ID(N'[dbo].[FK__TrainingU__train__41EDCAC5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TrainingUsers] DROP CONSTRAINT [FK__TrainingU__train__41EDCAC5];
GO
IF OBJECT_ID(N'[dbo].[FK__TrainingU__user___40F9A68C]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TrainingUsers] DROP CONSTRAINT [FK__TrainingU__user___40F9A68C];
GO
IF OBJECT_ID(N'[dbo].[FK__UsersRole__roles__6E01572D]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsersRoles] DROP CONSTRAINT [FK__UsersRole__roles__6E01572D];
GO
IF OBJECT_ID(N'[dbo].[FK__UsersRole__user___656C112C]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsersRoles] DROP CONSTRAINT [FK__UsersRole__user___656C112C];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Permanents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Permanents];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Trainings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trainings];
GO
IF OBJECT_ID(N'[dbo].[TrainingUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TrainingUsers];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[UsersRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsersRoles];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [roles_id] int  NOT NULL,
    [name] nvarchar(50)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [user_id] int IDENTITY(1,1) NOT NULL,
    [email] varchar(80)  NOT NULL,
    [first_name] varchar(100)  NOT NULL,
    [last_name] varchar(100)  NOT NULL,
    [password] nvarchar(100)  NOT NULL,
    [phone_number] varchar(50)  NOT NULL
);
GO

-- Creating table 'UsersRoles'
CREATE TABLE [dbo].[UsersRoles] (
    [user_id] int  NOT NULL,
    [roles_id] int  NOT NULL,
    [id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Permanents'
CREATE TABLE [dbo].[Permanents] (
    [user_id] int  NOT NULL,
    [permanent_id] int  NOT NULL,
    [counter] int  NULL,
    [free_entries] int  NULL
);
GO

-- Creating table 'Trainings'
CREATE TABLE [dbo].[Trainings] (
    [user_id] int  NOT NULL,
    [training_id] int  NOT NULL,
    [time] datetime  NOT NULL,
    [kapacita] int  NOT NULL
);
GO

-- Creating table 'TrainingUsers'
CREATE TABLE [dbo].[TrainingUsers] (
    [Id] int  NOT NULL,
    [user_id] int  NOT NULL,
    [training_id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [roles_id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([roles_id] ASC);
GO

-- Creating primary key on [user_id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([user_id] ASC);
GO

-- Creating primary key on [id] in table 'UsersRoles'
ALTER TABLE [dbo].[UsersRoles]
ADD CONSTRAINT [PK_UsersRoles]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [permanent_id] in table 'Permanents'
ALTER TABLE [dbo].[Permanents]
ADD CONSTRAINT [PK_Permanents]
    PRIMARY KEY CLUSTERED ([permanent_id] ASC);
GO

-- Creating primary key on [training_id] in table 'Trainings'
ALTER TABLE [dbo].[Trainings]
ADD CONSTRAINT [PK_Trainings]
    PRIMARY KEY CLUSTERED ([training_id] ASC);
GO

-- Creating primary key on [Id] in table 'TrainingUsers'
ALTER TABLE [dbo].[TrainingUsers]
ADD CONSTRAINT [PK_TrainingUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [roles_id] in table 'UsersRoles'
ALTER TABLE [dbo].[UsersRoles]
ADD CONSTRAINT [FK__UsersRole__roles__6E01572D]
    FOREIGN KEY ([roles_id])
    REFERENCES [dbo].[Roles]
        ([roles_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__UsersRole__roles__6E01572D'
CREATE INDEX [IX_FK__UsersRole__roles__6E01572D]
ON [dbo].[UsersRoles]
    ([roles_id]);
GO

-- Creating foreign key on [user_id] in table 'UsersRoles'
ALTER TABLE [dbo].[UsersRoles]
ADD CONSTRAINT [FK__UsersRole__user___656C112C]
    FOREIGN KEY ([user_id])
    REFERENCES [dbo].[Users]
        ([user_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__UsersRole__user___656C112C'
CREATE INDEX [IX_FK__UsersRole__user___656C112C]
ON [dbo].[UsersRoles]
    ([user_id]);
GO

-- Creating foreign key on [user_id] in table 'Permanents'
ALTER TABLE [dbo].[Permanents]
ADD CONSTRAINT [FK__Permanent__user___3E1D39E1]
    FOREIGN KEY ([user_id])
    REFERENCES [dbo].[Users]
        ([user_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Permanent__user___3E1D39E1'
CREATE INDEX [IX_FK__Permanent__user___3E1D39E1]
ON [dbo].[Permanents]
    ([user_id]);
GO

-- Creating foreign key on [user_id] in table 'Trainings'
ALTER TABLE [dbo].[Trainings]
ADD CONSTRAINT [FK__Trainings__user___29221CFB]
    FOREIGN KEY ([user_id])
    REFERENCES [dbo].[Users]
        ([user_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Trainings__user___29221CFB'
CREATE INDEX [IX_FK__Trainings__user___29221CFB]
ON [dbo].[Trainings]
    ([user_id]);
GO

-- Creating foreign key on [training_id] in table 'TrainingUsers'
ALTER TABLE [dbo].[TrainingUsers]
ADD CONSTRAINT [FK__TrainingU__train__41EDCAC5]
    FOREIGN KEY ([training_id])
    REFERENCES [dbo].[Trainings]
        ([training_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TrainingU__train__41EDCAC5'
CREATE INDEX [IX_FK__TrainingU__train__41EDCAC5]
ON [dbo].[TrainingUsers]
    ([training_id]);
GO

-- Creating foreign key on [user_id] in table 'TrainingUsers'
ALTER TABLE [dbo].[TrainingUsers]
ADD CONSTRAINT [FK__TrainingU__user___40F9A68C]
    FOREIGN KEY ([user_id])
    REFERENCES [dbo].[Users]
        ([user_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TrainingU__user___40F9A68C'
CREATE INDEX [IX_FK__TrainingU__user___40F9A68C]
ON [dbo].[TrainingUsers]
    ([user_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------