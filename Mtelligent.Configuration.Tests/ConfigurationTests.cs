using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Mtelligent.Configuration.Tests
{
	[TestClass ()]
	public class Configuration
	{
		[TestMethod ()]
		public void TestCookie ()
		{
			var config = (MtelligentSection)ConfigurationManager.GetSection ("Mtelligent");
            Assert.AreEqual("tracker", config.Web.Cookie.Name);
            Assert.AreEqual("contoso.com", config.Web.Cookie.Domain);
		}

        [TestMethod()]
        public void TestCohorts()
        {
            var config = (MtelligentSection)ConfigurationManager.GetSection("Mtelligent");
            Assert.AreNotEqual<int>(0, config.Cohorts.Count);
            Assert.IsNotNull(config.Cohorts["All Users"]);
            Assert.AreEqual<int>(config.Cohorts.Count, config.Cohorts.ToList().Count);
        }
	}
}

