using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Mtelligent.Entities.Cohorts
{
    public class FirstVisitCohort : Cohort
    {
        [UserEditable]
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate
        {
            get
            {
                if (Properties.ContainsKey("FirstVisitStartDate"))
                {
                    var dateVal = Properties["FirstVisitStartDate"];
                    if (!string.IsNullOrEmpty(dateVal))
                    {
                        return DateTime.Parse(dateVal);
                    }
                }
                return DateTime.MinValue;
            }
            set
            {
                if (Properties.ContainsKey("FirstVisitStartDate"))
                {
                    Properties["FirstVisitStartDate"] = value.ToString();
                }
                else
                {
                    Properties.Add("FirstVisitStartDate", value.ToString());
                }
            }
        }

        [UserEditable]
        [Display(Name = "End Date")]
        public DateTime EndDate
        {
            get
            {
                if (Properties.ContainsKey("FirstVisitEndDate"))
                {
                    var dateVal = Properties["FirstVisitEndDate"];
                    if (!string.IsNullOrEmpty(dateVal))
                    {
                        return DateTime.Parse(dateVal);
                    }
                }
                return DateTime.MaxValue;
            }
            set
            {
                if (Properties.ContainsKey("FirstVisitEndDate"))
                {
                    Properties["FirstVisitEndDate"] = value.ToString();
                }
                else
                {
                    Properties.Add("FirstVisitEndDate", value.ToString());
                }
            }
        }

        public override bool IsInCohort(Visitor visitor)
        {
            if (visitor.FirstVisit >= this.StartDate && visitor.FirstVisit <= this.EndDate)
            {
                return true;
            }
            return false;
        }
    }
}
