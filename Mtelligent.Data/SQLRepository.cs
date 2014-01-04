using Microsoft.Practices.EnterpriseLibrary.Data;
using Mtelligent.Entities;
using System;
using System.Collections.Generic;
using System.Data;
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

        protected Visitor ReaderToVisitor(IDataReader reader)
        {
            var visitor = new Visitor();

            if (reader["Id"] != DBNull.Value)
            {
                visitor.Id = Convert.ToInt32(reader["Id"]);
            }

            if (reader["UID"] != DBNull.Value)
            {
                visitor.UID = reader.GetGuid(reader.GetOrdinal("UID"));
            }

            if (reader["FirstVisit"] != DBNull.Value)
            {
                visitor.FirstVisit = Convert.ToDateTime(reader["FirstVisit"]);
            }

            if (reader["UserName"] != DBNull.Value)
            {
                visitor.UserName = reader["UserName"].ToString();
            }

            if (reader["IsAuthenticated"] != DBNull.Value)
            {
                visitor.IsAuthenticated = Convert.ToBoolean(reader["IsAuthenticated"]);
            }

            return visitor;
        }

        protected Experiment ReaderToExperiment(IDataReader reader)
        {
            var experiment = new Experiment();

            if (reader["Id"] != DBNull.Value)
            {
                experiment.Id = Convert.ToInt32(reader["Id"]);
            }

            if (reader["UID"] != DBNull.Value)
            {
                experiment.UID = reader.GetGuid(reader.GetOrdinal("UID"));
            }

            if (reader["Name"] != DBNull.Value)
            {
                experiment.Name = reader["Name"].ToString();
            }

            if (reader["SystemName"] != DBNull.Value)
            {
                experiment.SystemName = reader["SystemName"].ToString();
            }

            if (reader["TargetCohortId"] != DBNull.Value)
            {
                experiment.TargetCohortId = Convert.ToInt32(reader["TargetCohortId"]);
            }

            if (reader.HasColumn("Segments") && reader["Segments"] != DBNull.Value)
            {
                experiment.SegmentCount = Convert.ToInt32(reader["Segments"]);
            }

            if (reader.HasColumn("TargetCohortName") && reader["TargetCohortName"] != DBNull.Value)
            {
                experiment.TargetCohortName = reader["TargetCohortName"].ToString();
            }

            if (reader["CreatedBy"] != DBNull.Value)
            {
                experiment.CreatedBy = reader["CreatedBy"].ToString();
            }

            if (reader["UpdatedBy"] != DBNull.Value)
            {
                experiment.UpdatedBy = reader["UpdatedBy"].ToString();
            }

            if (reader["Created"] != DBNull.Value)
            {
                experiment.Created = Convert.ToDateTime(reader["Created"]);
            }

            if (reader["Updated"] != DBNull.Value)
            {
                experiment.Updated = Convert.ToDateTime(reader["Updated"]);
            }

            experiment.Segments = new List<ExperimentSegment>();
            experiment.Variables = new List<string>();

            return experiment;
        }

        protected ExperimentSegment ReaderToExperimentSegment(IDataReader reader)
        {
            var segment = new ExperimentSegment();

            if (reader["Id"] != DBNull.Value)
            {
                segment.Id = Convert.ToInt32(reader["Id"]);
            }

            if (reader["UID"] != DBNull.Value)
            {
                segment.UID = reader.GetGuid(reader.GetOrdinal("UID"));
            }

            if (reader["Name"] != DBNull.Value)
            {
                segment.Name = reader["Name"].ToString();
            }

            if (reader["SystemName"] != DBNull.Value)
            {
                segment.SystemName = reader["SystemName"].ToString();
            }

            if (reader["TargetPercentage"] != DBNull.Value)
            {
                segment.TargetPercentage = Convert.ToDouble(reader["TargetPercentage"]);
            }

            if (reader["IsDefault"] != DBNull.Value)
            {
                segment.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }

            if (reader["ExperimentId"] != DBNull.Value)
            {
                segment.ExperimentId = Convert.ToInt32(reader["ExperimentId"]);
            }

            if (reader["CreatedBy"] != DBNull.Value)
            {
                segment.CreatedBy = reader["CreatedBy"].ToString();
            }

            if (reader["UpdatedBy"] != DBNull.Value)
            {
                segment.UpdatedBy = reader["UpdatedBy"].ToString();
            }

            if (reader["Created"] != DBNull.Value)
            {
                segment.Created = Convert.ToDateTime(reader["Created"]);
            }

            if (reader["Updated"] != DBNull.Value)
            {
                segment.Updated = Convert.ToDateTime(reader["Updated"]);
            }

            segment.Variables = new Dictionary<string, string>();
            return segment;
        }

        protected void AddVariableToSegment(Experiment experiment, IDataReader reader)
        {
            var segmentID = Convert.ToInt32(reader["ExperimentSegmentId"]);
            var segment = experiment.Segments.FirstOrDefault(a => a.Id == segmentID);
            if (segment != null)
            {
                var variableName = reader["Name"].ToString();
                segment.Variables.Add(variableName, reader["Value"].ToString());

                if (experiment.Variables.IndexOf(variableName) == -1)
                {
                    experiment.Variables.Add(variableName);
                }
            }
        }

        protected Cohort ReaderToCohort(IDataReader reader)
        {
            string cohortType = null;

            if (reader["Type"] != DBNull.Value)
            {
                cohortType = reader["Type"].ToString();
            }

            if (cohortType == null)
            {
                throw new ArgumentException("Cohort Type not specified.");
            }

            Cohort cohort = null;

            try
            {
                Type t = Type.GetType(cohortType);
                cohort = Activator.CreateInstance(t) as Cohort;
                cohort.TypeName = cohortType;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid Cohort Type. Couldn't create instance.", ex);
            }

            if (reader["Id"] != DBNull.Value)
            {
                cohort.Id = Convert.ToInt32(reader["Id"]);
            }

            if (reader["UID"] != DBNull.Value)
            {
                cohort.UID = reader.GetGuid(reader.GetOrdinal("UID"));
            }

            if (reader["Name"] != DBNull.Value)
            {
                cohort.Name = reader["Name"].ToString();
            }

            if (reader["SystemName"] != DBNull.Value)
            {
                cohort.SystemName = reader["SystemName"].ToString();
            }

            if (reader["CreatedBy"] != DBNull.Value)
            {
                cohort.CreatedBy = reader["CreatedBy"].ToString();
            }

            if (reader["UpdatedBy"] != DBNull.Value)
            {
                cohort.UpdatedBy = reader["UpdatedBy"].ToString();
            }

            if (reader["Created"] != DBNull.Value)
            {
                cohort.Created = Convert.ToDateTime(reader["Created"]);
            }

            if (reader["Updated"] != DBNull.Value)
            {
                cohort.Updated = Convert.ToDateTime(reader["Updated"]);
            }

            cohort.Properties = new Dictionary<string, string>();

            return cohort;
        }

        protected KeyValuePair<string, string> ReaderToKVP(IDataReader reader)
        {
            string key = null, value = null;

            if (reader["Name"] != DBNull.Value)
            {
                key = reader["Name"].ToString();
            }

            if (reader["Value"] != DBNull.Value)
            {
                value = reader["Value"].ToString();
            }

            return new KeyValuePair<string, string>(key, value);
        }


        protected Goal ReaderToGoal(IDataReader reader)
        {
            var goal = new Goal();

            if (reader["Id"] != DBNull.Value)
            {
                goal.Id = Convert.ToInt32(reader["Id"]);
            }

            if (reader["Name"] != DBNull.Value)
            {
                goal.Name = reader["Name"].ToString();
            }

            if (reader["SystemName"] != DBNull.Value)
            {
                goal.SystemName = reader["SystemName"].ToString();
            }

            if (reader["GACode"] != DBNull.Value)
            {
                goal.GACode = reader["GACode"].ToString();
            }

            if (reader["CustomJS"] != DBNull.Value)
            {
                goal.CustomJS = reader["CustomJS"].ToString();
            }

            if (reader["CreatedBy"] != DBNull.Value)
            {
                goal.CreatedBy = reader["CreatedBy"].ToString();
            }

            if (reader["UpdatedBy"] != DBNull.Value)
            {
                goal.UpdatedBy = reader["UpdatedBy"].ToString();
            }

            if (reader["Created"] != DBNull.Value)
            {
                goal.Created = Convert.ToDateTime(reader["Created"]);
            }

            if (reader["Updated"] != DBNull.Value)
            {
                goal.Updated = Convert.ToDateTime(reader["Updated"]);
            }

            return goal;
        }


        protected Site ReadertoSite(IDataReader reader)
        {
            var site = new Site();

            if (reader["Id"] != DBNull.Value)
            {
                site.Id = Convert.ToInt32(reader["Id"]);
            }

            if (reader["Name"] != DBNull.Value)
            {
                site.Name = reader["Name"].ToString();
            }

            if (reader["CreatedBy"] != DBNull.Value)
            {
                site.CreatedBy = reader["CreatedBy"].ToString();
            }

            if (reader["UpdatedBy"] != DBNull.Value)
            {
                site.UpdatedBy = reader["UpdatedBy"].ToString();
            }

            if (reader["Created"] != DBNull.Value)
            {
                site.Created = Convert.ToDateTime(reader["Created"]);
            }

            if (reader["Updated"] != DBNull.Value)
            {
                site.Updated = Convert.ToDateTime(reader["Updated"]);
            }

            site.Urls = new List<SiteUrl>();

            return site;
        }

        protected SiteUrl ReadertoSiteUrl(IDataReader reader)
        {
            var siteUrl = new SiteUrl();

            if (reader["Id"] != DBNull.Value)
            {
                siteUrl.Id = Convert.ToInt32(reader["Id"]);
            }

            if (reader["SiteId"] != DBNull.Value)
            {
                siteUrl.SiteId = Convert.ToInt32(reader["SiteId"]);
            }

            if (reader["Url"] != DBNull.Value)
            {
                siteUrl.Url = reader["Url"].ToString();
            }

            if (reader["CreatedBy"] != DBNull.Value)
            {
                siteUrl.CreatedBy = reader["CreatedBy"].ToString();
            }

            if (reader["UpdatedBy"] != DBNull.Value)
            {
                siteUrl.UpdatedBy = reader["UpdatedBy"].ToString();
            }

            if (reader["Created"] != DBNull.Value)
            {
                siteUrl.Created = Convert.ToDateTime(reader["Created"]);
            }

            if (reader["Updated"] != DBNull.Value)
            {
                siteUrl.Updated = Convert.ToDateTime(reader["Updated"]);
            }

            return siteUrl;
        }
    }
}
