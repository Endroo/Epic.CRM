CREATE TABLE [dbo].[Address] (
    [AddressId] INT            IDENTITY (1, 1) NOT NULL,
    [ZipCode]   INT            NULL,
    [City]      NVARCHAR (250) NULL,
    [Address]   NVARCHAR (250) NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([AddressId] ASC)
);

