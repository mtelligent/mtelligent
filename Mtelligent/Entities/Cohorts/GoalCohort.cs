using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Mtelligent.Entities.Cohorts
{
    public class GoalCohort : Cohort
    {
        [UserEditable]
        [Required]
        public string Goal
        {
            get
            {
                if (Properties.ContainsKey("Goal"))
                {
                    return Properties["Goal"];
                }
                return string.Empty;
            }
            set
            {
                if (Properties.ContainsKey("Goal"))
                {
                    Properties["Goal"] = value;
                }
                else
                {
                    Properties.Add("Goal", value);
                }
            }
        }

        public override bool IsInCohort(Visitor visitor)
        {
            return visitor.Conversions.Where(a => a.Name == this.Goal).FirstOrDefault() != null;
        }

        public override bool RequiresConversions
        {
            get
            {
                return true;
            }
        }
    }


}
