/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


IF EXISTS (SELECT name FROM sysindexes WHERE name = 'idx_Visitors_UID') DROP INDEX Visitors.idx_Visitors_UID

CREATE INDEX idx_Visitors_UID
ON Visitors (UID)

IF EXISTS (SELECT name FROM sysindexes WHERE name = 'idx_Experiments_SystemName') DROP INDEX Experiments.idx_Experiments_SystemName

CREATE INDEX idx_Experiments_SystemName
ON Experiments (SystemName)

IF EXISTS (SELECT name FROM sysindexes WHERE name = 'idx_Goals_SystemName') DROP INDEX Goals.idx_Goals_SystemName

CREATE INDEX idx_Goals_SystemName
ON Goals (SystemName)