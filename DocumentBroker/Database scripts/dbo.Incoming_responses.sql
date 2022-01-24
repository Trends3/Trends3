CREATE TABLE [dbo].[Incoming_responses] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [Ticket] NVARCHAR (50)  NOT NULL,
    [Error]  NVARCHAR (250) NULL,
    [Status] NVARCHAR (50)  NOT NULL,
    [Binary] NVARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

