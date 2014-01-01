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
		private static IVisitProvider _visitProvider;

		private Mtelligent ()
		{
			_config = (MtelligentSection) ConfigurationManager.GetSection ("ABMVC");

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

        public void HandlePreSendRequestHeaders(object sender, EventArgs e)
		{
			var visit = HttpContext.Current.Items ["ABMVC.Visit"] as Visit;

			if (visit != null) {
				ClientCookieManager cManager = new ClientCookieManager (visit.Visitor);
				HttpCookie cookie = new HttpCookie (_config.Web.Cookie.Name, cManager.ToCookieValue ());
				HttpContext.Current.Response.Cookies.Add (cookie);
			}

            ThreadPool.QueueUserWorkItem(delegate(object o)
            {
                var v = o as Visit;
                _visitProvider.RecordVisit(v);
            }, visit);
		}

		public void HandleBeginRequest(object sender, EventArgs e)
		{
			var visit = TrackRequest (HttpContext.Current.Request);
			//Store in Context to be able to fetch when needed.
			HttpContext.Current.Items.Add ("ABMVC.Visit", visit);
		}

		public Visit TrackRequest(HttpRequest request) {

			Visit visit = new Visit();

			if (request.Cookies [_config.Web.Cookie.Name] != null) 
			{
				ClientCookieManager cManager = new ClientCookieManager (request.Cookies [_config.Web.Cookie.Name].Value);
				visit.Visitor = new Visitor (new Guid(cManager.VisitorId));
			} 
			else 
			{
				visit.Visitor = new Visitor ();
			}

			visit.RequestedUrl = request.Url.ToString ();
			visit.Referrer = request.UrlReferrer.ToString ();

			return visit;
		}

	}

}

