CREATE TABLE [dbo].[ExperimentSegments]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [SystemName] NVARCHAR(100) NOT NULL, 
    [UID] UNIQUEIDENTIFIER NOT NULL DEFAULT newID(), 
    [TargetPercentage] FLOAT NOT NULL DEFAULT 0, 
    [IsDefault] INT NOT NULL DEFAULT 0, 
    [ExperimentId] INT NOT NULL,
	[CreatedBy] NVARCHAR(50) NOT NULL, 
    [Created] DATETIME NOT NULL, 
    [UpdatedBy] NVARCHAR(50) NULL, 
    [Updated] DATETIME NULL, 
    CONSTRAINT [CK_ExperimentSegments_SystemName_unique] Unique (SystemName), 
    CONSTRAINT [FK_ExperimentSegments_ToExperiments] FOREIGN KEY ([ExperimentId]) REFERENCES [Experiments]([Id])
)
