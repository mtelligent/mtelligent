using System;
using System.Collections.Generic;

namespace Mtelligent.Entities
{
	public class Visitor
	{
		public Visitor ()
		{
			Id = Guid.NewGuid ();

		}

		public Visitor(Guid visitorId){
			Id = visitorId;
		}

		public Guid Id { get; set; }

		public DateTime FirstVisit { get; set; }

		public List<Cohort> Cohorts { get; set; }

		public List<ExperimentSegment> ExperimentSegments { get; set; }

		public Dictionary<string, string> Attributes { get; set;}

        public bool IsAuthenticated { get; set; }

        public List<string> Roles { get; set; }

        public List<string> Referrers { get; set; }

        public List<string> LandingUrls { get; set; }

        public List<string> Converstions { get; set; }
	}
}

