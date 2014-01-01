CREATE TABLE [dbo].[Goals]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [SystemName] NVARCHAR(100) NOT NULL, 
    [Active] INT NOT NULL DEFAULT 1, 
    [GACode] NVARCHAR(20) NULL, 
    [CustomJS] NVARCHAR(MAX) NULL, 
    [CreatedBy] NVARCHAR(50) NOT NULL, 
    [Created] DATETIME NOT NULL, 
    [UpdatedBy] NVARCHAR(50) NULL, 
    [Updated] DATETIME NULL, 
    CONSTRAINT [CK_Goals_SystemName_unique] Unique (SystemName)
)
