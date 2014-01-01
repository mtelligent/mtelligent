using System;
using System.Configuration;

namespace Mtelligent.Configuration
{
	public class MtelligentSection : ConfigurationSection
	{

		[ConfigurationProperty("web")]
		public WebElement Web
        {
			get 
			{
				return (WebElement)this ["web"];
			}
			set
			{
				this ["web"] = value;
			}
		}

		[ConfigurationProperty("data")]
		public DataElement Data
		{
			get 
			{
				return (DataElement)this ["data"];
			}
			set
			{
				this ["data"] = value;
			}
		}

        [ConfigurationProperty("cohorts")]
        public CohortTypes Cohorts
        {
            get
            {
                return (CohortTypes)this["cohorts"];
            }
        }
	}
}

