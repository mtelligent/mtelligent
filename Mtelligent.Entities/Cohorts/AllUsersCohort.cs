using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mtelligent.Entities.Cohorts
{
    /// <summary>
    /// All Users are In the All Users Cohort
    /// </summary>
    public class AllUsersCohort : Cohort
    {
        public override bool IsInCohort(Visitor visitor)
        {
            return true;
        }
    }
}
