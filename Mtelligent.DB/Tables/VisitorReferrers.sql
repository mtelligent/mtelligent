CREATE TABLE [dbo].[VisitorReferrers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [VisitorId] INT NOT NULL, 
    [ReferrerUrl] NVARCHAR(2000) NOT NULL,
	[Created] DATETIME NOT NULL DEFAULT getDate(), 
    [CreatedBy] NVARCHAR(100) NULL, 
    CONSTRAINT [FK_VisitorReferrers_ToVisitors] FOREIGN KEY ([VisitorId]) REFERENCES [Visitors]([Id])
)
