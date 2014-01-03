using System;
using System.Configuration;

namespace Mtelligent.Configuration
{

	public class DataElement : ConfigurationElement
	{
        [ConfigurationProperty("providerType", DefaultValue = "Mtelligent.Data.MtelligentRepository, Mtelligent.Data", IsRequired = true)]
		public string ProviderType
		{
			get 
			{
                return (string)this["providerType"];
			}
			set
			{
                this["providerType"] = value;
			}
		}

	}
}

