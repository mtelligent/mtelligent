CREATE TABLE [dbo].[Experiments]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [SystemName] NVARCHAR(100) NOT NULL, 
    [UID] UNIQUEIDENTIFIER NOT NULL DEFAULT newID(), 
    [TargetCohortId] INT NOT NULL,
	[Active] INT NOT NULL DEFAULT 1,
	[CreatedBy] NVARCHAR(50) NOT NULL, 
    [Created] DATETIME NOT NULL, 
    [UpdatedBy] NVARCHAR(50) NULL, 
    [Updated] DATETIME NULL, 
    CONSTRAINT [CK_Experiments_SystemName_unique] Unique (SystemName), 
    CONSTRAINT [FK_Experiments_ToCohorts] FOREIGN KEY ([TargetCohortId]) REFERENCES [Cohorts]([Id])
)
