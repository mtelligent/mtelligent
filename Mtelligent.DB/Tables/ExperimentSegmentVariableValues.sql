CREATE TABLE [dbo].[ExperimentSegmentVariableValues]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ExperimentSegmentId] INT NOT NULL, 
    [ExperimentVariableId] INT NOT NULL, 
    [Value] NVARCHAR(MAX) NOT NULL DEFAULT '', 
    CONSTRAINT [FK_ExperimentSegmentVariableValues_ToExperimentSegments] FOREIGN KEY ([ExperimentSegmentId]) REFERENCES [ExperimentSegments]([Id]),
	CONSTRAINT [FK_ExperimentSegmentVariableValues_ToExperimentVariables] FOREIGN KEY ([ExperimentVariableId]) REFERENCES [ExperimentVariables]([Id])
)
