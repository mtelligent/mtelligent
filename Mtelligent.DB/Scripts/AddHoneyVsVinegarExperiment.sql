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

Select * from Experiments where SystemName = 'Honey vs Vinegar'

if @@ROWCOUNT = 0
begin
	declare @AllUsersCohortId int
	declare @ExperimentId int
	declare @TitleVariableId int
	declare @TitleColorVariableId int
	declare @CopyVariableId int
	declare @ImageSrcVariableId int
	declare @HoneyHypothesisId int
	declare @VinegarHypothesisId int
	declare @goalId int

	Select @AllUsersCohortId = Id from Cohorts where SystemName = 'All Users'

	insert into Experiments (Name, SystemName, [UID], TargetCohortId, Active, CreatedBy, Created) values ('Honey vs Vinegar', 'Honey vs Vinegar', NEWID(), @AllUsersCohortId, 1, 'System', getDate())
	select @ExperimentId = SCOPE_IDENTITY()

	insert into ExperimentVariables( Name, ExperimentId) values ('Title', @ExperimentId)
	select @TitleVariableId = SCOPE_IDENTITY()

	insert into ExperimentVariables( Name, ExperimentId) values ('TitleColor', @ExperimentId)
	select @TitleColorVariableId = SCOPE_IDENTITY()

	insert into ExperimentVariables( Name, ExperimentId) values ('Copy', @ExperimentId)
	select @CopyVariableId = SCOPE_IDENTITY()

	insert into ExperimentVariables( Name, ExperimentId) values ('Image Source', @ExperimentId)
	select @ImageSrcVariableId = SCOPE_IDENTITY()

	insert into ExperimentSegments (Name, SystemName, [UID], TargetPercentage, IsDefault, ExperimentId, CreatedBy, Created) values ('Honey', 'Honey', NEWID(), 50, 0, @ExperimentId, 'System', getDate())
	select @HoneyHypothesisId = SCOPE_IDENTITY()

	insert into ExperimentSegments (Name, SystemName, [UID], TargetPercentage, IsDefault, ExperimentId, CreatedBy, Created) values ('Vinegar', 'Vinegar', NEWID(), 50, 0, @ExperimentId, 'System', getDate())
	select @VinegarHypothesisId = SCOPE_IDENTITY()

	insert into [ExperimentSegmentVariableValues] (ExperimentSegmentId, ExperimentVariableId, Value) values (@HoneyHypothesisId, @TitleVariableId, 'You are awesome')
	insert into [ExperimentSegmentVariableValues] (ExperimentSegmentId, ExperimentVariableId, Value) values (@HoneyHypothesisId, @TitleColorVariableId, 'DarkOrange')
	insert into [ExperimentSegmentVariableValues] (ExperimentSegmentId, ExperimentVariableId, Value) values (@HoneyHypothesisId, @CopyVariableId, 'You are smart, good looking and have we are so lucky to have you on this page. Would you mind doing us a favor and clicking the button below.')
	insert into [ExperimentSegmentVariableValues] (ExperimentSegmentId, ExperimentVariableId, Value) values (@HoneyHypothesisId, @ImageSrcVariableId, 'http://img837.imageshack.us/img837/8681/ge1j.png')

	insert into [ExperimentSegmentVariableValues] (ExperimentSegmentId, ExperimentVariableId, Value) values (@VinegarHypothesisId, @TitleVariableId, 'Attention Jerk!')
	insert into [ExperimentSegmentVariableValues] (ExperimentSegmentId, ExperimentVariableId, Value) values (@VinegarHypothesisId, @TitleColorVariableId, 'red')
	insert into [ExperimentSegmentVariableValues] (ExperimentSegmentId, ExperimentVariableId, Value) values (@VinegarHypothesisId, @CopyVariableId, 'I know you find things difficult to do, but I want you to scroll down till you find a button and click it. DO IT NOW!!!')
	insert into [ExperimentSegmentVariableValues] (ExperimentSegmentId, ExperimentVariableId, Value) values (@VinegarHypothesisId, @ImageSrcVariableId, 'http://img15.imageshack.us/img15/8018/obeyeyeposterfnl.jpg')

	insert into Goals (Name, SystemName, Active, Value, CreatedBy, Created) values ('Honey vs Vinegar', 'Honey vs Vinegar', 1, 1, 'System', getDate())
	select @goalId = SCOPE_IDENTITY()

	Insert into ExperimentGoals (ExperimentId, GoalId) values (@ExperimentId, @goalId)
end