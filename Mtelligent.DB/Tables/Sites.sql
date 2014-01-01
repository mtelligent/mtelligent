CREATE TABLE [dbo].[Sites]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(100) NOT NULL,
	[Active] int Not Null DEFAULT 1,
	[CreatedBy] NVARCHAR(50) NOT NULL, 
    [Created] DATETIME NOT NULL, 
    [UpdatedBy] NVARCHAR(50) NULL, 
    [Updated] DATETIME NULL, 
    CONSTRAINT [CK_Sites_NameUnique] Unique (Name)
)