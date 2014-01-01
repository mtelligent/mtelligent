using System;
using System.Configuration;

namespace Mtelligent.Configuration
{

	public class DataElement : ConfigurationElement
	{
		[ConfigurationProperty("visitProviderType", DefaultValue="", IsRequired = true)]
		public string VisitProviderType
		{
			get 
			{
				return (string) this["visitProviderType"];
			}
			set
			{
                this["visitProviderType"] = value;
			}
		}

	}
}

