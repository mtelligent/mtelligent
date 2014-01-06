using System;
using System.Collections.Generic;

namespace Mtelligent.Entities
{
	public class Cohort : DBEntity
	{
		public Cohort ()
		{
			
		}

		public int Id { get; set; }
        public Guid UID { get; set; }        
        public string SystemName { get; set; }
        public string TypeName { get; set; }
        public string Name { get; set; }
        
        public Dictionary<string, string> Properties = new Dictionary<string, string>();

        public virtual bool IsInCohort(Visitor visitor)
        {
            return false;
        }

        public virtual bool RequiresDetails
        {
            get
            {
                return false;
            }
        }

        public virtual bool RequiresAttributes
        {
            get
            {
                return false;
            }
        }

        public virtual bool RequiresConversions
        {
            get
            {
                return false;
            }
        }

        public virtual bool RequiresReferrers
        {
            get
            {
                return false;
            }
        }

        public virtual bool RequiresLandingUrls
        {
            get
            {
                return false;
            }
        }
	}
}

