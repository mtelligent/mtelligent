using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtelligent.Entities.Cohorts
{

    public class NonAuthenticatedUsersCohort : Cohort
    {
        public override bool IsInCohort(Visitor visitor)
        {
            if (visitor.IsAuthenticated)
            {
                return false;
            }
            return true;
        }
    }
}
