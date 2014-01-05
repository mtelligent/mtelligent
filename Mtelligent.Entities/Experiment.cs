using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mtelligent.Entities
{
	public class Experiment : DBEntity
	{
		public Experiment ()
		{
            GoalIds = new List<int>();
		}

		public int Id { get; set; }

        [Required]
		public string Name { get; set; }

        [Required]
        [Display(Name="System Name")]
        public string SystemName { get; set; }

        [Required]
        [Display(Name="Target Cohort")]
        public int TargetCohortId { get; set; }

        public Cohort TargetCohort { get; set; }
        
        public string TargetCohortName { get; set; }
        public Guid UID { get; set; }


		public List<ExperimentSegment> Segments { get; set; }
        public List<string> Variables { get; set; }

        public int SegmentCount { get; set; }

        [Display(Name = "Goals")]
        public List<int> GoalIds { get; set; }

        
	}
}

