CREATE TABLE [dbo].[WorkStatus] (
    [WorkStatusId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_WorkStatus] PRIMARY KEY CLUSTERED ([WorkStatusId] ASC)
);

