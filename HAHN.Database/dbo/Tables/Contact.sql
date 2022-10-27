CREATE TABLE [dbo].[Contact] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Name]  VARCHAR (250) NULL,
    [Email] VARCHAR (250) NULL,
    [Phone] VARCHAR (50)  NULL,
    [Age]   INT           NULL,
    CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED ([Id] ASC)
);

