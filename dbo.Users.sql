CREATE TABLE [dbo].[Users] (
    [user_id]      INT           IDENTITY (1, 1) NOT NULL,
    [email]        VARCHAR (80)  NOT NULL,
    [first_name]   VARCHAR (100) NOT NULL,
    [last_name]    VARCHAR (100) NOT NULL,
    [password]     CHAR (41)     NOT NULL,
    [phone_number] VARCHAR(50)           NOT NULL,
    PRIMARY KEY CLUSTERED ([user_id] ASC),
    UNIQUE NONCLUSTERED ([email] ASC)
);

