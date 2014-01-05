CREATE TABLE [dbo].[ExperimentGoals]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ExperimentId] INT NOT NULL, 
    [GoalId] INT NOT NULL, 
    CONSTRAINT [FK_ExperimentGoals_ToExperiments] FOREIGN KEY ([ExperimentId]) REFERENCES [Experiments]([Id]),
	CONSTRAINT [FK_ExperimentGoals_ToGoals] FOREIGN KEY ([GoalId]) REFERENCES [Goals]([Id])
)
