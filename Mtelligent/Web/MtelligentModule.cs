using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mtelligent.Web
{
    public class MtelligentModule : IHttpModule
    {
        public MtelligentModule()
        {

        }

        public string ModuleName
        {
            get { return "MtelligentModule"; }
        }

        public void Init(HttpApplication context)
        {
            ExperimentManager.Current.Initialize(context);
        }

        public void Dispose()
        {

        }
    }
}
