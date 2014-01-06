using System;
using System.Configuration;

namespace Mtelligent.Configuration
{
	public class WebElement : ConfigurationElement
	{
		[ConfigurationProperty("cookie")]
		public CookieElement Cookie
		{
			get 
			{
				return (CookieElement)this ["cookie"];
			}
			set
			{
				this ["cookie"] = value;
			}
		}


        [ConfigurationProperty("useSession", DefaultValue = "false", IsRequired = false)]
        public bool UseSession
        {
            get
            {
                return (bool)this["useSession"];
            }
            set
            {
                this["useSession"] = value;
            }
        }

        [ConfigurationProperty("captureAllRequests", DefaultValue = "false", IsRequired = false)]
        public bool CaptureAllRequests
        {
            get
            {
                return (bool)this["captureAllRequests"];
            }
            set
            {
                this["captureAllRequests"] = value;
            }
        }

        [ConfigurationProperty("cacheDuration", DefaultValue = "60", IsRequired = false)]
        public int CacheDuration
        {
            get
            {
                return (int)this["cacheDuration"];
            }
            set
            {
                this["cacheDuration"] = value;
            }
        }
	}
}

