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
	}
}

