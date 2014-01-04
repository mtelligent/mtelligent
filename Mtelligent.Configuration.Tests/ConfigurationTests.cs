using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Mtelligent.Configuration.Tests
{
	[TestClass]
	public class Configuration
	{
		[TestMethod]
		public void TestCookie ()
		{
			var config = (MtelligentSection)ConfigurationManager.GetSection ("Mtelligent");
            Assert.AreEqual("tracker", config.Web.Cookie.Name);
            Assert.AreEqual("contoso.com", config.Web.Cookie.Domain);
            Assert.AreEqual<int>(365, config.Web.Cookie.Expires);
		}

        [TestMethod]
        public void TestCohorts()
        {
            var config = (MtelligentSection)ConfigurationManager.GetSection("Mtelligent");
            Assert.AreNotEqual<int>(0, config.Cohorts.Count);
            Assert.IsNotNull(config.Cohorts["All Users"]);
            Assert.AreEqual<int>(config.Cohorts.Count, config.Cohorts.ToList().Count);
        }

        [TestMethod]
        public void TestData()
        {
            var config = (MtelligentSection)ConfigurationManager.GetSection("Mtelligent");
            Assert.IsNotNull(config.Data.ProviderType);
            Assert.AreEqual("Mtelligent.Data.MtelligentRepository, Mtelligent.Data", config.Data.ProviderType);
        }

        [TestMethod]
        public void TestWeb()
        {
            var config = (MtelligentSection)ConfigurationManager.GetSection("Mtelligent");
            Assert.AreEqual<bool>(true, config.Web.UseSession);
            Assert.AreEqual<bool>(false, config.Web.CaptureAllRequests);
        }
	}
}

