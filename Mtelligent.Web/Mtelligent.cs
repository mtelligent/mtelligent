using System;
using System.Web;
using Mtelligent.Configuration;
using Mtelligent.Data;
using System.Configuration;
using Mtelligent.Entities;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace Mtelligent.Web
{
	public class Mtelligent
	{
		private static Mtelligent _instance = new Mtelligent();
		private static MtelligentSection _config;
		private static IMtelligentRepository _visitProvider;
        private static Random _random = new Random();

        private const string currentVisitorKey = "Mtelligent.CurrentVisitor";
        private const string sectionName = "Mtelligent";

        public Visitor CurrentVisitor
        {
            get
            {
                if (_config.Web.UseSession)
                {
                    return HttpContext.Current.Session[currentVisitorKey] as Visitor;
                }
                else
                {
                    return HttpContext.Current.Items[currentVisitorKey] as Visitor;
                }
            }
            set
            {
                if (_config.Web.UseSession)
                {
                    HttpContext.Current.Session[currentVisitorKey] = value;
                }
                else
                {
                    if (HttpContext.Current.Items.Contains(currentVisitorKey))
                    {
                        HttpContext.Current.Items[currentVisitorKey] = value;
                    }
                    else
                    {
                        HttpContext.Current.Items.Add(currentVisitorKey, value);
                    }
                }
            }
        }

		private Mtelligent ()
		{
            _config = (MtelligentSection)ConfigurationManager.GetSection(sectionName);

			DataProviderFactory factory = new DataProviderFactory (_config);
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
            
            ThreadPool.QueueUserWorkItem(delegate(object o)
            {
                var v = o as Visitor;
                _visitProvider.SaveChanges(v);
            }, CurrentVisitor);
            
		}

        private static Dictionary<string, Experiment> experiments = new Dictionary<string, Experiment>();

        private static Experiment getExperiment(string experimentName)
        {
            if (!experiments.ContainsKey(experimentName))
            {
                experiments.Add(experimentName, _visitProvider.GetExperiment(experimentName));
            }

            return experiments[experimentName];
        }

        public void AddConversion(string goalName)
        {
            //get goal 
            var goal = _visitProvider.GetGoal(goalName);
            CurrentVisitor.Request.Conversions.Add(goal);
            CurrentVisitor.Conversions.Add(goal);
        }

        public ExperimentSegment GetSegment(string experimentName)
        {
            //get experiment from DB
            var experiment = getExperiment(experimentName);

            //See if user has current segments loaded.
            if (!CurrentVisitor.IsNew && !CurrentVisitor.ExperimentSegmentsLoaded)
            {
                CurrentVisitor = _visitProvider.GetSegments(CurrentVisitor);
            }

            var existingSegment = CurrentVisitor.ExperimentSegments.FirstOrDefault(a => a.ExperimentId == experiment.Id);
            if (existingSegment != null)
            {
                return existingSegment;
            }

            //check if user is in cohort.
            if (userIsInCohort(experiment.TargetCohort))
            {
                //Randomly select a segment
                int randNum = _random.Next(1, 100);
                double counter = 0;
                foreach (var segment in experiment.Segments)
                {
                    if (segment.TargetPercentage != 0)
                    {
                        if (counter + segment.TargetPercentage >= randNum)
                        {
                            CurrentVisitor.Request.ExperimentSegments.Add(segment);
                            return segment;
                        }
                        counter += segment.TargetPercentage;
                    }
                }
            }
            else
            {
                //Get Default Segment
                return experiment.Segments.FirstOrDefault(a => a.IsDefault);
            }

            return null;
        }

        private bool userIsInCohort(Cohort cohort)
        {
            loadCohortPrerequisites(cohort);
            if (CurrentVisitor.Cohorts.FirstOrDefault(a=>a.SystemName == cohort.SystemName) != null)
            {
                return true;    
            }
            if (cohort.IsInCohort(CurrentVisitor))
            {
                CurrentVisitor.Request.Cohorts.Add(cohort);
                CurrentVisitor.Cohorts.Add(cohort);
                return true;
            }
            return false;
        }

        private void loadCohortPrerequisites(Cohort cohort)
        {
            //New Visitors don't have anything else
            if (!CurrentVisitor.IsNew)
            {
                if (!CurrentVisitor.CohortsLoaded)
                {
                    CurrentVisitor = _visitProvider.GetCohorts(CurrentVisitor);

                    //short circuit if user is in cohort already.
                    if (CurrentVisitor.Cohorts.FirstOrDefault(a => a.SystemName == cohort.SystemName) != null)
                    {
                        return;
                    }
                }

                if (!CurrentVisitor.DetailsLoaded)
                {
                    //reload Details
                    CurrentVisitor = _visitProvider.GetDetails(CurrentVisitor);

                    if (string.IsNullOrEmpty(CurrentVisitor.UserName) && HttpContext.Current.Request.IsAuthenticated)
                    {
                        //Need to Reconcile User
                    }
                }

                if (cohort.RequiresAttributes && !CurrentVisitor.AttributesLoaded)
                {
                    CurrentVisitor = _visitProvider.GetAttributes(CurrentVisitor);
                }

                if (cohort.RequiresLandingUrls && !CurrentVisitor.LandingUrlsLoaded)
                {
                    CurrentVisitor = _visitProvider.GetLandingPages(CurrentVisitor);
                }

                if (cohort.RequiresLandingUrls && !string.IsNullOrEmpty(CurrentVisitor.Request.LandingUrl))
                {
                    CurrentVisitor.LandingUrls.Add(CurrentVisitor.Request.LandingUrl);
                }

                if (cohort.RequiresReferrers && !CurrentVisitor.ReferrersLoaded)
                {
                    CurrentVisitor = _visitProvider.GetReferrers(CurrentVisitor);
                }

                if (cohort.RequiresReferrers && !string.IsNullOrEmpty(CurrentVisitor.Request.FilteredReferrer))
                {
                    CurrentVisitor.Referrers.Add(CurrentVisitor.Request.FilteredReferrer);
                }


            }
        }

		public Visitor GetVisitor(HttpContext context) {
            var request = context.Request;

            if (CurrentVisitor == null)
            {
                if (request.Cookies[_config.Web.Cookie.Name] != null)
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
            }
            else
            {
                CurrentVisitor.IsNew = false;
            }

            CurrentVisitor.Request.RequestUrl = request.Url.ToString();
            CurrentVisitor.Request.ReferrerUrl = request.UrlReferrer != null ? request.UrlReferrer.ToString() : string.Empty;

			return CurrentVisitor;
		}

	}

}

