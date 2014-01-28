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
    /// <summary>
    /// Facade for Experiment Testing
    /// </summary>
	public class ExperimentManager
    {
        #region external Methods

        /// <summary>
        /// Need to call this in Constructor of Application/Global ASAX to hook into Request Events
        /// </summary>
        /// <param name="app"></param>
        public void Initialize(HttpApplication app)
        {
            app.PreRequestHandlerExecute += InitializeRequest;
            app.PreSendRequestHeaders += FinalizeRequest;
        }

        public void AddConversion(string goalName)
        {
            //get goal 
            var goal = _visitProvider.GetGoal(goalName);
            CurrentVisitor.Request.Conversions.Add(goal);
            CurrentVisitor.Conversions.Add(goal);
        }

        public void AddVisitorAttribute(string key, string value)
        {
            if (CurrentVisitor.Attributes == null)
            {
                CurrentVisitor.Attributes = new Dictionary<string, string>();
            }

            if (CurrentVisitor.Request.Attributes == null)
            {
                CurrentVisitor.Request.Attributes = new Dictionary<string, string>();
            }

            if (CurrentVisitor.Attributes.ContainsKey(key))
            {
                if (value != CurrentVisitor.Attributes[key])
                {
                    throw new ArgumentException("Can't add duplicate key to Visitor Attributes.");
                }
                return;
            }

            CurrentVisitor.Attributes.Add(key, value);
            CurrentVisitor.Request.Attributes.Add(key, value);
        }

        public void RemoveVisitorAttribute(string key)
        {
            if (CurrentVisitor != null)
            {
                if (CurrentVisitor.AttributesLoaded && CurrentVisitor.Attributes.ContainsKey(key))
                {
                    CurrentVisitor.Attributes.Remove(key);
                }
                if (CurrentVisitor.Request.Attributes != null && CurrentVisitor.Request.Attributes.ContainsKey(key))
                {
                    CurrentVisitor.Request.Attributes.Remove(key);
                }
                _visitProvider.RemoveVisitorAttribute(CurrentVisitor, key);
            }
        }

        public bool IsVisitorInCohort(string cohortSystemName)
        {
            var cohort = _visitProvider.GetCohort(cohortSystemName);

            if (cohort != null)
            {
                validateAndLoadVisitor();
                return cohort.IsInCohort(CurrentVisitor);
            }
            return false;
        }

        public ExperimentSegment GetHypothesis(string experimentName)
        {
            //get experiment from DB
            var experiment = getExperiment(experimentName);

            if (!string.IsNullOrEmpty(HttpContext.Current.Request["Hypothesis"]))
            {
                var overrideSegment = experiment.Segments.FirstOrDefault(a => a.SystemName == HttpContext.Current.Request["Hypothesis"]);
                if (overrideSegment != null)
                {
                    return overrideSegment;
                }
            }

            validateAndLoadVisitor();

            return GetHypothesis(experiment, CurrentVisitor);
        }

        /// <summary>
        /// Mostly for testing, allows you to specify experiment and visitor properties.
        /// </summary>
        /// <param name="experiment"></param>
        /// <param name="visitor"></param>
        /// <returns></returns>
        public ExperimentSegment GetHypothesis(Experiment experiment, Visitor visitor)
        {
            //See if user has current segments loaded.
            if (!visitor.IsNew && !visitor.ExperimentSegmentsLoaded)
            {
                visitor = _visitProvider.GetSegments(visitor);
                visitor.ExperimentSegmentsLoaded = true;
            }

            var existingSegment = visitor.ExperimentSegments.FirstOrDefault(a => a.ExperimentId == experiment.Id);
            if (existingSegment != null)
            {
                return existingSegment;
            }

            //check if user is in cohort.
            if (userIsInCohort(visitor, experiment.TargetCohort))
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
                            visitor.Request.ExperimentSegments.Add(segment);
                            visitor.ExperimentSegments.Add(segment);
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

        #endregion

        #region Singleton Implementation
        private static ExperimentManager _instance = new ExperimentManager();

        private ExperimentManager()
        {
            _config = (MtelligentSection)ConfigurationManager.GetSection(sectionName);

            DataProviderFactory factory = new DataProviderFactory(_config);
            _visitProvider = factory.CreateProvider();
        }

        public static ExperimentManager Current
        {
            get { return _instance; }
        }

        #endregion

        #region static properties
        private static MtelligentSection _config;
		private static IMtelligentRepository _visitProvider;
        private static Random _random = new Random();

        private const string currentVisitorKey = "Mtelligent.CurrentVisitor";
        private const string sectionName = "Mtelligent";

        /// <summary>
        /// Cache of experiments so we dont have to get each one more than once.
        /// May need to change to more flexible cache
        /// </summary>
        private static Dictionary<string, Experiment> experiments = new Dictionary<string, Experiment>();
        #endregion

        #region Context Methods
        /// <summary>
        /// Internal to prevent consumers of API 
        /// from modified Visitor Properties as they 
        /// shoudl managed by this framework.
        /// Will drive off of context or session depending on configuration.
        /// </summary>
        internal Visitor CurrentVisitor
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

        #endregion

        #region Request Handlers

        protected void InitializeRequest(object sender, EventArgs e)
        {
            GetVisitor(HttpContext.Current);
        }

        protected void FinalizeRequest(object sender, EventArgs e)
		{
            //reconcille user if user is authenticated and we have gotten the details that said they were not authenticated.
            validateAndLoadVisitor();

			if (CurrentVisitor != null) {
                HttpCookie cookie = new HttpCookie(_config.Web.Cookie.Name, CurrentVisitor.UID.ToString());
                if (!string.IsNullOrEmpty(_config.Web.Cookie.Domain))
                {
                    cookie.Domain = _config.Web.Cookie.Domain;
                }
                cookie.Expires = DateTime.Now.AddDays(_config.Web.Cookie.Expires);
				HttpContext.Current.Response.Cookies.Add (cookie);
			}
            
            object[] threadParams = new object[2];
            threadParams[0] = CurrentVisitor;
            threadParams[1] = _config.Web.CaptureAllRequests;


            ThreadPool.QueueUserWorkItem(delegate(object o)
            {
                var tParams = o as object[];
                var v = tParams[0] as Visitor;
                var b = (bool) tParams[1];

                _visitProvider.SaveChanges(v, b);
            }, threadParams);
            
		}

        #endregion
        
        #region Helper Methods

        private static Experiment getExperiment(string experimentName)
        {
            if (HttpContext.Current.Cache[experimentName] == null)
            {
                var experiment = _visitProvider.GetExperiment(experimentName);
                HttpContext.Current.Cache.Insert(experimentName, experiment, null, DateTime.Now.AddMinutes(_config.Web.CacheDuration), TimeSpan.Zero);
            }

            return HttpContext.Current.Cache[experimentName] as Experiment;
        }

        private bool userIsInCohort(Visitor visitor, Cohort cohort)
        {
            loadCohortPrerequisites(visitor, cohort);
            if (visitor.Cohorts.FirstOrDefault(a=>a.SystemName == cohort.SystemName) != null)
            {
                return true;    
            }
            if (cohort.IsInCohort(visitor))
            {
                visitor.Request.Cohorts.Add(cohort);
                visitor.Cohorts.Add(cohort);
                return true;
            }
            return false;
        }

        private void validateAndLoadVisitor()
        {
            if (CurrentVisitor == null)
            {
                CurrentVisitor = GetVisitor(HttpContext.Current);
            }

            if (!CurrentVisitor.DetailsLoaded)
                {
                    //reload Details
                    Visitor saved = null;

                    if (!CurrentVisitor.IsNew)
                    {
                        saved = _visitProvider.GetDetails(CurrentVisitor);


                        if (saved != null && !saved.IsAuthenticated && HttpContext.Current.Request.IsAuthenticated)
                        {
                            //Need to Reconcile User
                            var oldRequest = CurrentVisitor.Request;
                            CurrentVisitor.UserName = HttpContext.Current.User.Identity.Name;
                            CurrentVisitor = _visitProvider.ReconcileUser(CurrentVisitor);
                            CurrentVisitor.Request = oldRequest;
                            CurrentVisitor.DetailsLoaded = true;
                            
                            CurrentVisitor.IsAuthenticated = true;
                            CurrentVisitor.UserName = HttpContext.Current.User.Identity.Name;
                            //Need to save changes to user afterward regardless.
                            CurrentVisitor.IsDirty = true;
                            return;
                        }

                        if (saved == null)
                        {
                            //somehow we had a cookie, but no real visitor record
                            CurrentVisitor.FirstVisit = DateTime.Now;
                            if (HttpContext.Current.Request.IsAuthenticated)
                            {
                                CurrentVisitor.IsAuthenticated = true;
                                CurrentVisitor.UserName = HttpContext.Current.User.Identity.Name;
                            }
                            saved = _visitProvider.AddVisitor(CurrentVisitor);
                            CurrentVisitor.DetailsLoaded = true;

                        }
                    }
                    else
                    {
                        saved = CurrentVisitor;
                    }


                    if (!string.IsNullOrEmpty(saved.UserName) && HttpContext.Current.User != null && saved.UserName != HttpContext.Current.User.Identity.Name)
                    {
                        //Update CurrentVisitor to Saved User.
                        var oldRequest = CurrentVisitor.Request;
                        CurrentVisitor = _visitProvider.GetVisitor(HttpContext.Current.User.Identity.Name);
                        CurrentVisitor.Request = oldRequest;
                        CurrentVisitor.DetailsLoaded = true;
                        return;
                    }

                    CurrentVisitor.Id = saved.Id;
                    CurrentVisitor.IsAuthenticated = saved.IsAuthenticated;
                    CurrentVisitor.UserName = saved.UserName;
                    CurrentVisitor.DetailsLoaded = true;
                }
        }

        private void loadCohortPrerequisites(Visitor visitor, Cohort cohort)
        {
            //New Visitors don't have anything else
            if (!visitor.IsNew)
            {
                if (!visitor.CohortsLoaded)
                {
                    CurrentVisitor = _visitProvider.GetCohorts(visitor);

                    //short circuit if user is in cohort already.
                    if (visitor.Cohorts.FirstOrDefault(a => a.SystemName == cohort.SystemName) != null)
                    {
                        return;
                    }
                }

                if (cohort.RequiresAttributes && !visitor.AttributesLoaded)
                {
                    visitor = _visitProvider.GetAttributes(visitor);
                    visitor.AttributesLoaded = true;
                }

                if (cohort.RequiresLandingUrls && !visitor.LandingUrlsLoaded)
                {
                    visitor = _visitProvider.GetLandingPages(visitor);
                    visitor.LandingUrlsLoaded = true;
                }

                if (cohort.RequiresLandingUrls && !string.IsNullOrEmpty(visitor.Request.LandingUrl))
                {
                    visitor.LandingUrls.Add(visitor.Request.LandingUrl);
                }

                if (cohort.RequiresReferrers && !visitor.ReferrersLoaded)
                {
                    visitor = _visitProvider.GetReferrers(visitor);
                    visitor.ReferrersLoaded = true;
                }

                if (cohort.RequiresReferrers && !string.IsNullOrEmpty(visitor.Request.FilteredReferrer))
                {
                    visitor.Referrers.Add(visitor.Request.FilteredReferrer);
                }


            }
        }

		protected Visitor GetVisitor(HttpContext context) {

            var request = context.Request;

            if (CurrentVisitor == null)
            {
                if (request.Cookies[_config.Web.Cookie.Name] != null)
                {
                    CurrentVisitor = new Visitor(new Guid(request.Cookies[_config.Web.Cookie.Name].Value));
                }
                else
                {
                    if (HttpContext.Current.Request.IsAuthenticated)
                    {
                        //Get Visitor based on UserName
                        CurrentVisitor = _visitProvider.GetVisitor(HttpContext.Current.User.Identity.Name);
                        if (CurrentVisitor != null)
                        {
                            CurrentVisitor.DetailsLoaded = true;
                        }
                    }
                    
                    if (CurrentVisitor == null)
                    {
                        CurrentVisitor = new Visitor();
                        CurrentVisitor.FirstVisit = DateTime.Now;
                        CurrentVisitor.IsNew = true;

                        if (request.IsAuthenticated)
                        {
                            CurrentVisitor.IsAuthenticated = true;
                            CurrentVisitor.UserName = HttpContext.Current.User.Identity.Name;
                        }
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

        #endregion
    }

}

