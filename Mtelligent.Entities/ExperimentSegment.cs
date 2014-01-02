using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mtelligent.Entities
{
	public class ExperimentSegment : DBEntity
	{
		public ExperimentSegment ()
		{
		}

		public int Id { get; set; }
        public Guid UID { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name="System Name")]
        public string SystemName { get; set; }
        public int ExperimentId { get; set; }

        [Display(Name = "Target Percentage")]
        public double TargetPercentage { get; set; }

        [Display(Name = "Is Default")]
        public bool IsDefault { get; set; }

        public Dictionary<string, string> Variables { get; set; }

		
	}
}

