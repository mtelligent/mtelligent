using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Mtelligent.Entities.Cohorts
{
    public class SegmentCohort : Cohort
    {
        [UserEditable]
        [Required]
        [Display(Name = "Experiment Name")]
        public string ExperimentName
        {
            get
            {
                if (Properties.ContainsKey("ExperimentName"))
                {
                    return Properties["ExperimentName"];
                }
                return string.Empty;
            }
            set
            {
                if (Properties.ContainsKey("ExperimentName"))
                {
                    Properties["ExperimentName"] = value;
                }
                else
                {
                    Properties.Add("ExperimentName", value);
                }
            }
        }

        [UserEditable]
        public string Segment
        {
            get
            {
                if (Properties.ContainsKey("Segment"))
                {
                    return Properties["Segment"];
                }
                return string.Empty;
            }
            set
            {
                if (Properties.ContainsKey("Segment"))
                {
                    Properties["Segment"] = value;
                }
                else
                {
                    Properties.Add("Segment", value);
                }
            }
        }

        public override bool IsInCohort(Visitor visitor)
        {
            var segments = visitor.ExperimentSegments.Where(a => a.SystemName == this.Segment).ToList();
            if (segments != null && segments.Count > 0)
            {
                foreach (var segment in segments)
                {
                    //to do get experiment based on id
                    //return true only if names match.
                    return true;
                }
            }
            return false;
        }
    }
}
