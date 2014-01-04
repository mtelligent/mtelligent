using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtelligent.Dashboard.Data
{
    public class ExperimentQueries
    {
        public const string GetExperiments = @"Select *, B.Segments, C.Name 'TargetCohortName' from Experiments  (nolock) A 
                                               left join (select ExperimentId, count(*) 'Segments' from ExperimentSegments  (nolock) group by ExperimentID) B on A.Id = B.ExperimentID 
                                               inner join Cohorts C  (nolock) on A.TargetCohortId = C.ID where A.Active=1";
        public const string GetExperiment = @"Select A.*, C.Name 'TargetCohortName' from Experiments  (nolock) A 
                                                inner join Cohorts  (nolock) C on A.TargetCohortId = C.Id where A.Id = @ExperimentId; 
            Select * from ExperimentSegments (nolock) where ExperimentId = @ExperimentId; 
            Select * from ExperimentVariables (nolock) where ExperimentID = @ExperimentId; 
            Select ExperimentSegmentId, Name, Value from ExperimentVariables (nolock) A 
            inner join ExperimentSegmentVariableValues (nolock) B on A.Id = B.ExperimentVariableId Where A.ExperimentID = @ExperimentId";
        public const string AddExperiment =
                @"Insert into Experiments (Name, SystemName, TargetCohortId, Created, CreatedBy) Values (@Name, @SystemName, @TargetCohortId, getDate(), @CreatedBy);
                select * from Experiments (nolock) where Id = scope_Identity()";

        public const string UpdateExperiment = @"Update Experiments Set Name=@Name, TargetCohortId=@TargetCohortId, Updated=getDate(), UpdatedBy=@UpdatedBy Where Id = @ExperimentId";
        public const string DeleteExperiment = @"Update Experiments set Active=0, Updated=getDate(), UpdatedBy=@UpdatedBy where Id = @ExperimentId";

        public const string AddExperimentVariable = @"Insert into ExperimentVariables(Name, ExperimentId) Values (@Name, @ExperimentId);";
        
        public const string AddExperimentSegment = @"Insert into ExperimentSegments (Name, SystemName, TargetPercentage, IsDefault, ExperimentId, Created, CreatedBy) Values (@Name, @SystemName, @TargetPercentage, @IsDefault, @ExperimentId, getDate(), @CreatedBy);
                select * from ExperimentSegments (nolock) where Id = scope_Identity()";
        public const string UpdateExperimentSegment = @"Update ExperimentSegments Set Name=@Name, TargetPercentage=@TargetPercentage, IsDefault=@IsDefault, Updated=getDate(), UpdatedBy=@UpdatedBy Where Id = @ExperimentSegmentId";

        public const string AddExperimentSegmentVariableValue = @"Select @ExperimentVariableID=Id from ExperimentVariables (nolock) where Name=@Name and ExperimentID = @ExperimentId
                Insert into ExperimentSegmentVariableValues (ExperimentSegmentId, ExperimentVariableID, Value) Values (@ExperimentSegmentID, @ExperimentVariableID, @Value)";

        public const string DeleteExperimentSegmentVariableValues = @"Delete from ExperimentSegmentVAriableValues where ExperimentSegmentID = @ExperimentSegmentId";
    }
}
