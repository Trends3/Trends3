CREATE TABLE [dbo].[Outgoing_request] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Ticket]        NVARCHAR (50)  NOT NULL,
    [ApplicationId] NVARCHAR (50)  NOT NULL,
    [DocumentType]  NVARCHAR (50)  NOT NULL,
    [Payload]       NVARCHAR (500) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

