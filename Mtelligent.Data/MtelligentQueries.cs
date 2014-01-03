using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtelligent.Data
{
    public class MtelligentQueries
    {
        public const string GetExperiment = @"
            Select @CohortId=TargetCohortId, @ExperimentId=Id from Experiments where SystemName = @SystemName
            Select * from Experiments where Id = @ExperimentId
            Select * from ExperimentSegments where ExperimentId = @ExperimentId
            Select ExperimentSegmentId, Name, Value from ExperimentVariables A inner join ExperimentSegmentVariableValues B on A.Id = B.ExperimentVariableId Where A.ExperimentID = @ExperimentId
            Select * from Cohorts Where Id = @CohortId
            Select * from CohortProperties where CohortId = @CohortId";

        public const string GetGoal = @"Select * from Goals where SystemName = @GoalName";
    }
}
