CREATE TABLE [dbo].[VisitorSegments]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [VisitorId] INT NOT NULL, 
    [SegmentId] INT NOT NULL, 
    [ExperimentId] INT NOT NULL,
	[Created] DATETIME NOT NULL DEFAULT getDate(), 
    [CreatedBy] NVARCHAR(100) NULL, 
    CONSTRAINT [FK_VisitorSegments_ToVisitors] FOREIGN KEY ([VisitorId]) REFERENCES [Visitors]([Id]), 
    CONSTRAINT [FK_VisitorSegments_ToExperimentSegments] FOREIGN KEY ([SegmentId]) REFERENCES [ExperimentSegments]([Id]),
	CONSTRAINT [FK_VisitorSegments_ToExperiments] FOREIGN KEY ([ExperimentId]) REFERENCES [Experiments]([Id])
)
