using System;
using System.Collections.Generic;

namespace Mtelligent.Entities
{
	public class Experiment : DBEntity
	{
		public Experiment ()
		{
			
		}

		public int Id { get; set; }

		public string Name { get; set; }
        public string SystemName { get; set; }
        public int TargetCohortId { get; set; }
        public Guid UID { get; set; }


		public List<ExperimentSegment> Segments { get; set; }
        public List<string> Variables { get; set; }

        public int SegmentCount { get; set; }

        
	}
}

