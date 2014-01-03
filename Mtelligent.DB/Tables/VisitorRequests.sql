CREATE TABLE [dbo].[VisitorRequests]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [VisitorId] INT NOT NULL, 
    [RequestUrl] NVARCHAR(2000) NOT NULL,
	[Created] DATETIME NOT NULL DEFAULT getDate(), 
    [CreatedBy] NVARCHAR(100) NULL, 
    CONSTRAINT [FK_VisitorRequests_ToVisitors] FOREIGN KEY ([VisitorId]) REFERENCES [Visitors]([Id])
)
