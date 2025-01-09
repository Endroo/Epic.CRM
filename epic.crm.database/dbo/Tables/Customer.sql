CREATE TABLE [dbo].[Customer] (
    [CustomerId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (250) NULL,
    [Email]      NVARCHAR (250) NULL,
    [AddressId]  INT            NULL,
    [AppUserId]  INT            NOT NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([CustomerId] ASC),
    CONSTRAINT [FK_Customer_Address] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([AddressId]),
    CONSTRAINT [FK_Customer_AppUser] FOREIGN KEY ([AppUserId]) REFERENCES [dbo].[AppUser] ([AppUserId]) ON DELETE CASCADE
);

