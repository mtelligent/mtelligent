using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Mtelligent.Entities.Cohorts
{
    public class LandingUrlCohort : Cohort
    {
        [UserEditable]
        [Required]
        [Display(Name = "Landing Page Url")]
        public string LandingUrl
        {
            get
            {
                if (Properties.ContainsKey("LandingUrl"))
                {
                    return Properties["LandingUrl"];
                }
                return string.Empty;
            }
            set
            {
                if (Properties.ContainsKey("LandingUrl"))
                {
                    Properties["LandingUrl"] = value;
                }
                else
                {
                    Properties.Add("LandingUrl", value);
                }
            }
        }


        public override bool IsInCohort(Visitor visitor)
        {
            if (visitor.LandingUrls.Where(a => a.IndexOf(this.LandingUrl) > -1).FirstOrDefault() != null)
            {
                return true;
            }
            return false;
        }

        public override bool RequiresLandingUrls
        {
            get
            {
                return true;
            }
        }
    }
}
