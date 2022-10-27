CREATE TABLE [dbo].[Blogs] (
    [BlogId] INT           IDENTITY (1, 1) NOT NULL,
    [Url]    VARCHAR (250) NULL,
    [Rating] INT           NULL,
    CONSTRAINT [PK_Blogs] PRIMARY KEY CLUSTERED ([BlogId] ASC)
);

