using System;
using System.Configuration;

namespace Mtelligent.Configuration
{
    public class CookieElement : ConfigurationElement
    {

        [ConfigurationProperty("name", DefaultValue = "ABMVC", IsRequired = false)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("domain", DefaultValue = "", IsRequired = false)]
        public string Domain
        {
            get
            {
                return (string)this["domain"];
            }
            set
            {
                this["domain"] = value;
            }
        }

        [ConfigurationProperty("expires", DefaultValue = "365", IsRequired = false)]
        public int Expires
        {
            get
            {
                return (int)this["expires"];
            }
            set
            {
                this["expires"] = value;
            }
        }
    }
}

