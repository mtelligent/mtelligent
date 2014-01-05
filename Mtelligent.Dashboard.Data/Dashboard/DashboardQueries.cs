using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtelligent.Dashboard.Data
{
    public class DashboardQueries
    {
        public const string GetExperimentStatuses = @"
            Select 
              A.Id 'Experiment Id',
              A.Name 'Experiment Name',
              B.Id 'Hypothesis Id',
              B.Name 'Hypothesis Name',
              D.Id 'Goal Id',
              D.Name 'Goal Name',
              D.Value 'Goal Value',
              IsNull(E.Visitors, 0) 'Visitors',
              IsNull(F.Conversions, 0) 'Conversions',
              IsNull(F.Conversions, 0) * D.Value 'Conversion Value'

            from
              Experiments A (nolock)
              inner join ExperimentSegments (nolock) B on A.Id = B.ExperimentId and (B.IsDefault <> 1 or (B.IsDefault=1 and B.TargetPercentage > 0))
              inner join ExperimentGoals C on A.Id = C.ExperimentId
              inner join Goals D on C.GoalId = D.Id
              left join 
	            (Select SegmentId, count(distinct visitorId) 'Visitors' from VisitorSegments (nolock) group by SegmentId) E on B.Id = E.SegmentId
              left join
                (Select GoalId, SegmentId, count(distinct Z.visitorId) 'Conversions' from VisitorConversions Y (nolock) inner join VisitorSegments Z on Y.VisitorId = Z.VisitorId  group by GoalId, SegmentId) F on D.Id = F.GoalId and B.Id = F.SegmentId
            Where 
              A.Active =1";
    }
}
