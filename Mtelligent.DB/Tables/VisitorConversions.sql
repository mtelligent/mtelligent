CREATE TABLE [dbo].[VisitorConversions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [VisitorId] INT NOT NULL, 
    [GoalId] INT NOT NULL,
	[Created] DATETIME NOT NULL DEFAULT getDate(), 
    [CreatedBy] NVARCHAR(100) NULL, 
    CONSTRAINT [FK_VisitorConversions_ToVisitors] FOREIGN KEY ([VisitorId]) REFERENCES [Visitors]([Id]), 
    CONSTRAINT [FK_VisitorCohorts_ToGoals] FOREIGN KEY ([GoalId]) REFERENCES [Goals]([Id])
)
