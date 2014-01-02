using System;
using Mtelligent.Configuration;
using System.Configuration;

namespace Mtelligent.Data
{
	public class VisitProviderFactory
	{
		private MtelligentSection _config;

		public VisitProviderFactory()
		{
			_config = (MtelligentSection) ConfigurationManager.GetSection ("Mtelligent");
		}

		public VisitProviderFactory(MtelligentSection config)
		{
			_config = config;
		}

		public IVistorProvider CreateProvider(){
			Type t = Type.GetType(_config.Data.VisitProviderType);
			return Activator.CreateInstance(t) as IVistorProvider;
		}
	}
}

