using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtelligent.Dashboard.Data
{
    public class CohortQueries
    {
        public const string GetCohorts = @"Select * from Cohorts where Active=1";
        public const string GetCohort = @"Select * from Cohorts where Id = @CohortId; Select * from CohortProperties where CohortId = @CohortId";
        public const string AddCohort =
                @"Insert into Cohorts (Name, SystemName, Type, Created, CreatedBy) Values (@Name, @SystemName, @Type, getDate(), @CreatedBy);
                select * from Cohorts where Id = scope_Identity(); ";
        public const string UpdateCohort = @"Update Cohorts Set Name=@Name, Updated=getDate(), UpdatedBy=@UpdatedBy Where Id = @CohortId";
        public const string DeleteCohort = @"Update Cohorts set Active=0, Updated=getDate(), UpdatedBy=@UpdatedBy where Id = @CohortId";

        public const string AddCohortProperty = @"Insert into CohortProperties (CohortId, Name, Value) Values (@CohortId, @Name, @Value)";
        public const string DeleteCohortProperties = @"Delete from CohortProperties where CohortId = @CohortId";
    }
}
