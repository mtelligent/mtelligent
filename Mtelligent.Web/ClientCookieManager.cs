using System;
using System.Collections.Generic;
using Mtelligent.Entities;

namespace Mtelligent.Web
{
	public class ClientCookieManager
	{
		public ClientCookieManager (string cookieValue)
		{
			var settings = cookieValue.Split ('|');
			if (settings.Length == 4) {
				this.VisitorId = settings [0];
				this.FirstVisit = DateTime.ParseExact (settings [1], "yyyyMMdd", null);
				this.IsReconcilled = settings [2] == "1";
				var particpants = settings [3].Split(',');
				this.ParticpantIds = new List<string> ();
				this.ParticpantIds.AddRange (particpants);				                  
			}
		}

		public ClientCookieManager(Visitor vistor)
		{
			this.VisitorId = vistor.Id.ToString ();
			this.FirstVisit = vistor.FirstVisit;

			List<string> pIds = new List<string> ();
			vistor.ExperimentSegments.ForEach(a=> pIds.Add(a.Id.ToString()));
			this.ParticpantIds = pIds;
		}

		public string VisitorId { get; set; }
		public DateTime FirstVisit { get; set; }
		public bool IsReconcilled { get; set;}
		public List<string> ParticpantIds { get; set; }

		public string ToCookieValue(){
			return string.Format ("{0}|{1}|{2}|{3}", 
				this.VisitorId, 
				this.FirstVisit.ToString("yyyyMMdd"),
				this.IsReconcilled ? "1" : "0", 
				string.Join (",", this.ParticpantIds));
		}
	}
}

