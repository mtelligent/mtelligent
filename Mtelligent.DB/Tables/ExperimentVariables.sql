CREATE TABLE [dbo].[ExperimentVariables]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [ExperimentId] INT NOT NULL, 
    CONSTRAINT [CK_ExperimentVariables_Name] Unique (Id,Name), 
    CONSTRAINT [FK_ExperimentVariables_ToExperiments] FOREIGN KEY ([ExperimentId]) REFERENCES [Experiments]([Id])
)
