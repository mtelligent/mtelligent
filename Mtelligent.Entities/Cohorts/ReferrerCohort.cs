using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Mtelligent.Entities.Cohorts
{
    public class ReferrerCohort : Cohort
    {
        [UserEditable]
        [Required]
        public string Referrer
        {
            get
            {
                if (Properties.ContainsKey("Referrer"))
                {
                    return Properties["Referrer"];
                }
                return string.Empty;
            }
            set
            {
                if (Properties.ContainsKey("Referrer"))
                {
                    Properties["Referrer"] = value;
                }
                else
                {
                    Properties.Add("Referrer", value);
                }
            }
        }


        public override bool IsInCohort(Visitor visitor)
        {
            if (visitor.Referrers.Where(a => a.IndexOf(this.Referrer) > -1).FirstOrDefault() != null)
            {
                return true;
            }
            return false;
        }
    }
}
