using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mtelligent.Data
{
    public abstract class SQLRepository
    {
        protected static Database _db = null;

        protected static Database GetDatabase()
        {
            if (_db == null)
            {
                DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
                _db = DatabaseFactory.CreateDatabase("MtelligentDB");
            }
            return _db;
        }
    }
}
