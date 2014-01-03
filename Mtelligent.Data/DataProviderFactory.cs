using System;
using Mtelligent.Configuration;
using System.Configuration;

namespace Mtelligent.Data
{
	public class DataProviderFactory
	{
		private MtelligentSection _config;

		public DataProviderFactory()
		{
			_config = (MtelligentSection) ConfigurationManager.GetSection ("Mtelligent");
		}

		public DataProviderFactory(MtelligentSection config)
		{
			_config = config;
		}

		public IMtelligentRepository CreateProvider(){
			Type t = Type.GetType(_config.Data.ProviderType);
			return Activator.CreateInstance(t) as IMtelligentRepository;
		}
	}
}

