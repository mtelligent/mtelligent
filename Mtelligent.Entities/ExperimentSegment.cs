using System;
using System.Collections.Generic;

namespace Mtelligent.Entities
{
	public class ExperimentSegment : DBEntity
	{
		public ExperimentSegment ()
		{
		}

		public int Id { get; set; }
        public Guid UID { get; set; }
        public string Name { get; set; }
        public string SystemName { get; set; }
        public int ExperimentId { get; set; }
        public double TargetPercentage { get; set; }
        public bool IsDefault { get; set; }

        public Dictionary<string, string> Variables { get; set; }

		
	}
}

