CREATE TABLE [dbo].[VisitorAttributes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [VisitorId] INT NOT NULL, 
    [Name] NVARCHAR(255) NOT NULL, 
    [Value] NVARCHAR(MAX) NOT NULL, 
    [Created] DATETIME NOT NULL DEFAULT getDate(), 
    [CreatedyBy] NVARCHAR(100) NULL, 
    CONSTRAINT [FK_VisitorAttributes_ToVisitors] FOREIGN KEY ([VisitorId]) REFERENCES [Visitors]([Id])
)
