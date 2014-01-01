using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtelligent.Dashboard.Data
{
    public class ExperimentQueries
    {
        public const string GetExperiments = @"Select * from Experiments A inner join (select ExperimentId, count(*) 'Segments' from ExperimentSegments group by ExperimentID) B on A.Id = B.ExperimentID where A.Active=1";
        public const string GetExperiment = @"Select * from Experiments where Id = @ExperimentId; Select * from ExperimentSegments where ExperimentId = @ExperimentId; Select ExperimentSegmentId, Name, Value from ExperimentVariables A inner join ExperimentSegmentVariableValues B on A.Id = B.ExperimentVariableId Where A.ExperimentID = @ExperimentId";
        public const string AddExperiment =
                @"Insert into Experiments (Name, SystemName, TargetCohortId, Created, CreatedBy) Values (@Name, @SystemName, @TargetCohortId, getDate(), @CreatedBy);
                select * from Experiments where Id = scope_Identity()";
        public const string UpdateExperiment = @"Update Experiments Set Name=@Name, TargetCohortId=@TargetCohortId, Updated=getDate(), UpdatedBy=@UpdatedBy Where Id = @ExperimentId";
        public const string DeleteExperiment = @"Update Experiments set Active=0, Updated=getDate(), UpdatedBy=@UpdatedBy where Id = @ExperimentId";

        public const string AddExperimentVariable = @"Insert into ExperimentVariables(Name, ExperimentId) Values (@Name, @ExperimentId);";
        
        public const string AddExperimentSegment = @"Insert into ExperimentSegments (Name, SystemName, TargetPercentage, IsDefault, ExperimentId, Created, CreatedBy) Values (@Name, @SystemName, @TargetPercentage, @IsDefault, @ExperimentId, getDate(), @CreatedBy);
                select * from ExperimentSegments where Id = scope_Identity()";
        public const string UpdateExperimentSegment = @"Update ExperimentSegments Set Name=@Name, TargetPercentage=@TargetPercentage, IsDefault=@IsDefault; Updated=getDate(), UpdatedBy=@UpdatedBy Where Id = @ExperimentSegmentId";

        public const string AddExperimentSegmentVariableValue = @"Select @ExperimentVariableID=Id from ExperimentVariables where Name=@Name and ExperimentID = @ExperimentID; 
                Insert into ExperimentSegmentVariableValues (ExperimentSegmentId, ExperimentVariableID, ValueID) Values (@ExperimentSegmentID, @ExperimentValueID, @Value)";
        public const string DeleteExperimentSegmentVariableValues = @"Delete from ExperimentSegmentVAriableValues where ExperimentSegmentID = @ExperimentSegmentId";
    }
}
