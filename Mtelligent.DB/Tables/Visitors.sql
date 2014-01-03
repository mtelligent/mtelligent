CREATE TABLE [dbo].[Visitors]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UID] UNIQUEIDENTIFIER NOT NULL, 
    [FirstVisit] DATETIME NOT NULL, 
    [UserName] NVARCHAR(255) NULL, 
    [IsAuthenticated] INT NULL, 
    [Created] DATETIME NOT NULL DEFAULT getDate(), 
    [CreatedBy] NVARCHAR(100) NULL, 
    [ReconcilledVisitorId] INT NULL
)
