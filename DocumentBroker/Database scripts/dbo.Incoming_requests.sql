CREATE TABLE [dbo].[Incoming_requests] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [ApplicationId] NVARCHAR (50)  NOT NULL,
    [RequestId]     NVARCHAR (50)  NOT NULL,
    [RequestType]   NVARCHAR (50)  NOT NULL,
    [Body]          NVARCHAR (500) NOT NULL,
    [Status]        NVARCHAR (25)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

