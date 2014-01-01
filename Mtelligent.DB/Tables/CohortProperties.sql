CREATE TABLE [dbo].[CohortProperties]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CohortId] INT NOT NULL, 
    [Name] NVARCHAR(255) NOT NULL, 
    [Value] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_CohortProperties_ToCohort] FOREIGN KEY (CohortId) REFERENCES [Cohorts](Id)
)
