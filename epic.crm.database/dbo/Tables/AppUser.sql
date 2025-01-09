CREATE TABLE [dbo].[AppUser] (
    [AppUserId]    INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (250) NOT NULL,
    [Email]        NVARCHAR (250) NULL,
    [Profession]   NVARCHAR (50)  NOT NULL,
    [IsAdmin]      BIT            NOT NULL,
    [AspNetUserId] NVARCHAR (450) NOT NULL,
    CONSTRAINT [PK_AppUser] PRIMARY KEY CLUSTERED ([AppUserId] ASC),
    CONSTRAINT [FK_AppUser_AspNetUsers] FOREIGN KEY ([AspNetUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

