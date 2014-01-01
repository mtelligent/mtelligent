CREATE TABLE [dbo].[SiteUrls]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SiteId] INT NOT NULL, 
    [Url] NVARCHAR(1000) NOT NULL, 
	[Active] INT NOT NULL DEFAULT 1,
	[CreatedBy] NVARCHAR(50) NOT NULL, 
    [Created] DATETIME NOT NULL, 
    [UpdatedBy] NVARCHAR(50) NULL, 
    [Updated] DATETIME NULL,
    CONSTRAINT [FK_SiteUrls_ToSites] FOREIGN KEY (SiteId) REFERENCES [Sites]([Id]), 
    CONSTRAINT [CK_SiteUrls_Url_Unique] Unique (Url)
)
