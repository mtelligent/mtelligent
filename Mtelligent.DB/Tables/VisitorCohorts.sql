CREATE TABLE [dbo].[VisitorCohorts]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [VisitorId] INT NOT NULL, 
    [CohortId] INT NOT NULL,
	[Created] DATETIME NOT NULL DEFAULT getDate(), 
    [CreatedBy] NVARCHAR(100) NULL, 
    CONSTRAINT [FK_VisitorCohorts_ToVisitors] FOREIGN KEY ([VisitorId]) REFERENCES [Visitors]([Id]), 
    CONSTRAINT [FK_VisitorCohorts_ToCohorts] FOREIGN KEY ([CohortId]) REFERENCES [Cohorts]([Id])
)
