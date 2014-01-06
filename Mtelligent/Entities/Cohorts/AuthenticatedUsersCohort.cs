using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mtelligent.Entities.Cohorts
{
    public class AuthenticatedUsersCohort : Cohort
    {
        public override bool IsInCohort(Visitor visitor)
        {
            if (visitor.IsAuthenticated)
            {
                return true;
            }
            return false;
        }
    }
}
