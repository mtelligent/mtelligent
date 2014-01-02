using System;
using System.Web;
using Mtelligent.Configuration;
using Mtelligent.Data;
using System.Configuration;
using Mtelligent.Entities;
using System.Threading;

namespace Mtelligent.Web
{
	public class Mtelligent
	{
		private static Mtelligent _instance = new Mtelligent();
		private static MtelligentSection _config;
		private static IVistorProvider _visitProvider;

        public Visitor CurrentVisitor { get; set; }

		private Mtelligent ()
		{
			_config = (MtelligentSection) ConfigurationManager.GetSection ("Mtelligent");

			VisitProviderFactory factory = new VisitProviderFactory (_config);
			_visitProvider = factory.CreateProvider ();
		}

		public static Mtelligent Current 
		{
			get { return _instance; }
		}

		public void Initialize(HttpApplication app){
			app.BeginRequest += HandleBeginRequest;
            app.PreSendRequestHeaders += HandlePreSendRequestHeaders;
            
		}

        public void HandleBeginRequest(object sender, EventArgs e)
        {
            var visitor = GetVisitor(HttpContext.Current);
        }

        public void HandlePreSendRequestHeaders(object sender, EventArgs e)
		{
			if (CurrentVisitor != null) {
                HttpCookie cookie = new HttpCookie(_config.Web.Cookie.Name, CurrentVisitor.UID.ToString());
				HttpContext.Current.Response.Cookies.Add (cookie);
			}

            if (CurrentVisitor.IsDirty)
            {
                ThreadPool.QueueUserWorkItem(delegate(object o)
                {
                    var v = o as Visitor;
                    _visitProvider.SaveChanges(v);
                }, CurrentVisitor);
            }
		}



		public Visitor GetVisitor(HttpContext context) {
            var request = context.Request;

            if (request.Cookies [_config.Web.Cookie.Name] != null) 
			{
                CurrentVisitor = new Visitor(new Guid(request.Cookies[_config.Web.Cookie.Name].Value));
			} 
			else 
			{
                CurrentVisitor = new Visitor();
                CurrentVisitor.FirstVisit = DateTime.Now;
                CurrentVisitor.IsNew = true;
                CurrentVisitor.IsDirty = true;
                if (request.IsAuthenticated)
                {
                    CurrentVisitor.IsAuthenticated = true;
                    CurrentVisitor.UserName = HttpContext.Current.User.Identity.Name;
                }
			}

            CurrentVisitor.Request.RequestUrl = request.Url.ToString();
            CurrentVisitor.Request.ReferrerUrl = request.UrlReferrer != null ? request.UrlReferrer.ToString() : string.Empty;

			return CurrentVisitor;
		}

	}

}

