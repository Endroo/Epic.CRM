CREATE TABLE [dbo].[Work] (
    [WorkId]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (250) NULL,
    [CustomerId]   INT            NULL,
    [WorkDateTime] DATETIME       NULL,
    [Description]  NVARCHAR (MAX) NULL,
    [AddressId]    INT            NULL,
    [WorkStatusId] INT            NULL,
    [AppUserId]    INT            NOT NULL,
    [Price]        INT            NULL,
    CONSTRAINT [PK_Work] PRIMARY KEY CLUSTERED ([WorkId] ASC),
    CONSTRAINT [FK_Work_Address] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([AddressId]),
    CONSTRAINT [FK_Work_AppUser] FOREIGN KEY ([AppUserId]) REFERENCES [dbo].[AppUser] ([AppUserId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Work_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId]),
    CONSTRAINT [FK_Work_WorkStatus] FOREIGN KEY ([WorkStatusId]) REFERENCES [dbo].[WorkStatus] ([WorkStatusId])
);

