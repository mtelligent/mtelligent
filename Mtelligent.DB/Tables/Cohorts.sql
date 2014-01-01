CREATE TABLE [dbo].[Cohorts]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [SystemName] NVARCHAR(100) NOT NULL, 
    [UID] UNIQUEIDENTIFIER NOT NULL DEFAULT newID(), 
    [Active] INT NOT NULL DEFAULT 1, 
    [Type] NVARCHAR(255) NOT NULL,
	[CreatedBy] NVARCHAR(50) NOT NULL, 
    [Created] DATETIME NOT NULL, 
    [UpdatedBy] NVARCHAR(50) NULL, 
    [Updated] DATETIME NULL, 
    CONSTRAINT [CK_Cohorts_SystemName] Unique (SystemName)
)
