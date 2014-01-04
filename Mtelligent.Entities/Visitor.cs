using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;


namespace Mtelligent.Entities
{
	public class Visitor
	{
        public Visitor(Guid UID) : this()
        {
            this.UID = UID;
        }

        public Visitor()
        {
            this.UID = Guid.NewGuid();
            this.Request = new VisitorRequest();
            this.Attributes = new Dictionary<string, string>();
            this.Cohorts = new List<Cohort>();
            this.Conversions = new List<Goal>();
            this.ExperimentSegments = new List<ExperimentSegment>();
            this.Referrers = new List<string>();
            this.LandingUrls = new List<string>();
        }

        public int Id { get; set; }
		public Guid UID { get; set; }

        public bool DetailsLoaded { get; set; }

		public DateTime FirstVisit { get; set; }
        public string UserName { get; set; }
        public bool IsAuthenticated { get; set; }

        public bool AttributesLoaded { get; set; }
        public Dictionary<string, string> Attributes { get; set; }

		public List<Cohort> Cohorts { get; set; }
        public bool CohortsLoaded { get; set; }

		public List<ExperimentSegment> ExperimentSegments { get; set; }
        public bool ExperimentSegmentsLoaded { get; set; }

		public List<Goal> Conversions { get; set; }
        public bool ConverstionsLoaded { get; set; }

        public List<string> Referrers { get; set; }
        public bool ReferrersLoaded { get; set; }

        public List<string> LandingUrls { get; set; }
        public bool LandingUrlsLoaded { get; set; }

        public bool IsNew { get; set; }
        public bool IsDirty { get; set; }

        public List<string> Roles
        {
            get
            {
                var roles = new List<string>();

                if (HttpContext.Current != null && HttpContext.Current.Request.IsAuthenticated)
                {
                    var memberRoles = System.Web.Security.Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name);
                    roles.AddRange(memberRoles);
                }

                return roles;
            }
        }

        public VisitorRequest Request { get; set; }
        
	}

    public class VisitorRequest
    {
        public string RequestUrl { get; set; }
        public string ReferrerUrl { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public DateTime RequestDate { get; set; }
        public List<Goal> Conversions { get; set; }
        public List<Cohort> Cohorts { get; set; }
        public List<ExperimentSegment> ExperimentSegments { get; set; }

        public string LandingUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(this.ReferrerUrl))
                {
                    var referrerURI = new Uri(this.ReferrerUrl);
                    var currentURI = new Uri(this.RequestUrl);

                    if (currentURI.Host != referrerURI.Host)
                    {
                        return this.RequestUrl;
                    }
                }
                else
                {
                    return this.RequestUrl;
                }

                return null;
            }
        }

        public string FilteredReferrer
        {
            get
            {
                if (!string.IsNullOrEmpty(this.ReferrerUrl))
                {
                    var referrerURI = new Uri(this.ReferrerUrl);
                    var currentURI = new Uri(this.RequestUrl);

                    if (currentURI.Host != referrerURI.Host)
                    {
                        return this.ReferrerUrl;
                    }
                }
                return null;
            }
        }
    }
}

