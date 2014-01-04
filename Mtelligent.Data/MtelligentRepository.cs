using Mtelligent.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtelligent.Data
{
    public class MtelligentRepository : SQLRepository, IMtelligentRepository
    {
        public MtelligentRepository()
        {
            _db = GetDatabase();
        }


        public Visitor GetDetails(Visitor visitor)
        {
            Visitor updated = null;

            using (DbCommand cmd = _db.GetSqlStringCommand(MtelligentQueries.GetVisitor))
            {
                _db.AddInParameter(cmd, "@UID", DbType.Guid, visitor.UID);

                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        updated = ReaderToVisitor(reader);
                    }
                }

            }

            return updated;
        }

        public Visitor GetVisitor(string userName)
        {
            Visitor visitor = null;

            using (DbCommand cmd = _db.GetSqlStringCommand(MtelligentQueries.GetVisitorFromUserName))
            {
                _db.AddInParameter(cmd, "@UserName", DbType.String, userName);

                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        visitor = ReaderToVisitor(reader);
                    }
                }

            }

            return visitor;
        }

        public Visitor ReconcileUser(Visitor visitor)
        {
            using (DbCommand cmd = _db.GetSqlStringCommand(MtelligentQueries.ReconcileVisitor))
            {
                _db.AddInParameter(cmd, "@UID", DbType.Guid, visitor.UID);
                _db.AddInParameter(cmd, "@UserName", DbType.String, visitor.UserName);

                _db.AddOutParameter(cmd, "@ExistingID", DbType.Int32, 8);
                _db.AddOutParameter(cmd, "@VisitorID", DbType.Int32, 8);

                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {

                        var oldRequest = visitor.Request;

                        visitor = ReaderToVisitor(reader);

                        //update to user original request to preserve inflight changes
                        visitor.Request = oldRequest;
                    }
                }

            }

            return visitor;
        }

        public Visitor GetLandingPages(Visitor visitor)
        {
            if (visitor.LandingUrls == null)
            {
                visitor.LandingUrls = new List<string>();
            }

            using (DbCommand cmd = _db.GetSqlStringCommand(MtelligentQueries.GetVisitorLandingPages))
            {
                _db.AddInParameter(cmd, "@UID", DbType.Guid, visitor.UID);
                
                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {

                        if (reader["LandingPageUrl"] != DBNull.Value)
                        {
                            visitor.LandingUrls.Add(reader["LandingPageUrl"].ToString());
                        }
                    }
                }

            }

            return visitor;
        }

        public Entities.Visitor GetReferrers(Entities.Visitor visitor)
        {
            if (visitor.Referrers == null)
            {
                visitor.Referrers = new List<string>();
            }

            using (DbCommand cmd = _db.GetSqlStringCommand(MtelligentQueries.GetVisitorReferrers))
            {
                _db.AddInParameter(cmd, "@UID", DbType.Guid, visitor.UID);

                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {

                        if (reader["ReferrerUrl"] != DBNull.Value)
                        {
                            visitor.Referrers.Add(reader["ReferrerUrl"].ToString());
                        }
                    }
                }

            }

            return visitor;
        }

        public Visitor GetAttributes(Visitor visitor)
        {
            if (visitor.Attributes == null)
            {
                visitor.Attributes = new Dictionary<string, string>();
            }

            using (DbCommand cmd = _db.GetSqlStringCommand(MtelligentQueries.GetVisitorAttributes))
            {
                _db.AddInParameter(cmd, "@UID", DbType.Guid, visitor.UID);

                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
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

                        visitor.Attributes.Add(key, value);
                    }
                }

            }

            return visitor;
        }

        public Entities.Visitor GetCohorts(Entities.Visitor visitor)
        {
            if (visitor.Cohorts == null)
            {
                visitor.Cohorts = new List<Cohort>();
            }

            using (DbCommand cmd = _db.GetSqlStringCommand(MtelligentQueries.GetVisitorCohorts))
            {
                _db.AddInParameter(cmd, "@UID", DbType.Guid, visitor.UID);

                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        visitor.Cohorts.Add(ReaderToCohort(reader));
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        int cohortId = Convert.ToInt32(reader["CohortId"]);
                        var cohort = visitor.Cohorts.FirstOrDefault(a => a.Id == cohortId);
                        if (cohort != null)
                        {
                            var kvp = ReaderToKVP(reader);
                            cohort.Properties.Add(kvp.Key, kvp.Value);
                        }
                    }
                }

            }

            return visitor;
        }

        public Entities.Visitor GetSegments(Entities.Visitor visitor)
        {
            if (visitor.ExperimentSegments == null)
            {
                visitor.ExperimentSegments = new List<ExperimentSegment>();
            }

            using (DbCommand cmd = _db.GetSqlStringCommand(MtelligentQueries.GetVisitorSegments))
            {
                _db.AddInParameter(cmd, "@UID", DbType.Guid, visitor.UID);

                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        visitor.ExperimentSegments.Add(ReaderToExperimentSegment(reader));
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        int segmentId = Convert.ToInt32(reader["ExperimentSegmentId"]);
                        var segment = visitor.ExperimentSegments.FirstOrDefault(a => a.Id == segmentId);
                        if (segment != null)
                        {
                            var variableName = reader["Name"].ToString();
                            segment.Variables.Add(variableName, reader["Value"].ToString());
                        }
                    }
                }

            }

            return visitor;
        }

        public Entities.Visitor GetConversions(Entities.Visitor visitor)
        {
            if (visitor.Conversions == null)
            {
                visitor.Conversions = new List<Goal>();
            }

            using (DbCommand cmd = _db.GetSqlStringCommand(MtelligentQueries.GetVisitorConversions))
            {
                _db.AddInParameter(cmd, "@UID", DbType.Guid, visitor.UID);

                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        visitor.Conversions.Add(ReaderToGoal(reader));
                    }
                }

            }

            return visitor;
        }

        public Entities.Visitor SaveChanges(Entities.Visitor visitor, bool saveRequest)
        {
            if (visitor.IsNew)
            {
                visitor = AddVisitor(visitor);
            }
            else
            {
                if (visitor.IsDirty)
                {
                    UpdateVisitor(visitor);
                }
            }

            //We need the ID for the rest of the inserts.
            if (visitor.Id == 0 && (
                (visitor.Request.Attributes != null && visitor.Request.Attributes.Count > 0) ||
                (visitor.Request.Cohorts != null && visitor.Request.Cohorts.Count > 0) ||
                (visitor.Request.Conversions != null && visitor.Request.Conversions.Count > 0) ||
                (visitor.Request.ExperimentSegments != null && visitor.Request.ExperimentSegments.Count > 0) ||
                (!string.IsNullOrEmpty(visitor.Request.FilteredReferrer)) ||
                (!string.IsNullOrEmpty(visitor.Request.LandingUrl)) ||
                saveRequest
                ))                
            {
                var saved = GetDetails(visitor);
                visitor.Id = saved.Id;
            }

            if (visitor.Request.Attributes != null && visitor.Request.Attributes.Count > 0)
            {
                AddAttributes(visitor);
            }

            if (visitor.Request.Cohorts != null && visitor.Request.Cohorts.Count > 0) 
            {
                AddCohorts(visitor);
            }

            if (visitor.Request.Conversions != null && visitor.Request.Conversions.Count > 0)
            {
                AddConversions(visitor);
            }

            if (visitor.Request.ExperimentSegments != null && visitor.Request.ExperimentSegments.Count > 0)
            {
                AddSegments(visitor);
            }

            if (!string.IsNullOrEmpty(visitor.Request.FilteredReferrer))
            {
                AddReferrer(visitor);
            }

            if (!string.IsNullOrEmpty(visitor.Request.LandingUrl))
            {
                AddLandingPage(visitor);
            }

            if (saveRequest)
            {
                AddRequest(visitor);
            }

            return visitor;
        }

        public Visitor AddVisitor(Visitor visitor)
        {
            using (DbCommand cmd = _db.GetSqlStringCommand(MtelligentQueries.AddVisitor))
            {
                _db.AddInParameter(cmd, "@UID", DbType.Guid, visitor.UID);
                _db.AddInParameter(cmd, "@FirstVisit", DbType.DateTime, visitor.FirstVisit);
                _db.AddInParameter(cmd, "@UserName", DbType.String, visitor.UserName ?? string.Empty);
                _db.AddInParameter(cmd, "@IsAuthenticated", DbType.Int32, visitor.IsAuthenticated ? 1 : 0);

                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var saved = ReaderToVisitor(reader);

                        //It's new, just take the ID
                        visitor.Id = saved.Id;
                    }
                }

            }

            return visitor;

        }

        protected void UpdateVisitor(Visitor visitor)
        {
            using (DbCommand cmd = _db.GetSqlStringCommand(MtelligentQueries.UpdateVisitor))
            {
                _db.AddInParameter(cmd, "@UID", DbType.Guid, visitor.UID);
                _db.AddInParameter(cmd, "@UserName", DbType.String, visitor.UserName ?? string.Empty);
                _db.AddInParameter(cmd, "@IsAuthenticated", DbType.Int32, visitor.IsAuthenticated ? 1 : 0);

                _db.ExecuteNonQuery(cmd);
            }
        }

        protected void AddAttributes(Visitor visitor)
        {
            foreach (var key in visitor.Request.Attributes.Keys)
            {
                using (DbCommand cmd = _db.GetSqlStringCommand(MtelligentQueries.AddVisitorAttribute))
                {
                    _db.AddInParameter(cmd, "@VisitorId", DbType.Int32, visitor.Id);
                    _db.AddInParameter(cmd, "@Name", DbType.String, key);
                    _db.AddInParameter(cmd, "@Value", DbType.String, visitor.Request.Attributes[key]);

                    _db.ExecuteNonQuery(cmd);
                }
            }
        }

        protected void AddCohorts(Visitor visitor)
        {
            foreach (var cohort in visitor.Request.Cohorts)
            {
                using (DbCommand cmd = _db.GetSqlStringCommand(MtelligentQueries.AddVisitorCohort))
                {
                    _db.AddInParameter(cmd, "@VisitorId", DbType.Int32, visitor.Id);
                    _db.AddInParameter(cmd, "@CohortId", DbType.Int32, cohort.Id);

                    _db.ExecuteNonQuery(cmd);
                }
            }
        }

        protected void AddConversions(Visitor visitor)
        {
            foreach (var goal in visitor.Request.Conversions)
            {
                using (DbCommand cmd = _db.GetSqlStringCommand(MtelligentQueries.AddVisitorConversion))
                {
                    _db.AddInParameter(cmd, "@VisitorId", DbType.Int32, visitor.Id);
                    _db.AddInParameter(cmd, "@GoalId", DbType.Int32, goal.Id);

                    _db.ExecuteNonQuery(cmd);
                }
            }
        }

        protected void AddSegments(Visitor visitor)
        {
            foreach (var segment in visitor.Request.ExperimentSegments)
            {
                using (DbCommand cmd = _db.GetSqlStringCommand(MtelligentQueries.AddVisitorSegment))
                {
                    _db.AddInParameter(cmd, "@VisitorId", DbType.Int32, visitor.Id);
                    _db.AddInParameter(cmd, "@ExperimentId", DbType.Int32, segment.ExperimentId);
                    _db.AddInParameter(cmd, "@SegmentId", DbType.Int32, segment.Id);

                    _db.ExecuteNonQuery(cmd);
                }
            }
        }

        protected void AddReferrer(Visitor visitor)
        {
            using (DbCommand cmd = _db.GetSqlStringCommand(MtelligentQueries.AddVisitorReferrer))
            {
                _db.AddInParameter(cmd, "@VisitorId", DbType.Int32, visitor.Id);
                _db.AddInParameter(cmd, "@ReferrerUrl", DbType.String, visitor.Request.FilteredReferrer);

                _db.ExecuteNonQuery(cmd);
            }
        }

        protected void AddRequest(Visitor visitor)
        {
            if (visitor.Request.RequestUrl.IndexOf("Glimpse.axd") == -1)
            {
                using (DbCommand cmd = _db.GetSqlStringCommand(MtelligentQueries.AddVisitorRequest))
                {
                    _db.AddInParameter(cmd, "@VisitorId", DbType.Int32, visitor.Id);
                    _db.AddInParameter(cmd, "@RequestUrl", DbType.String, visitor.Request.RequestUrl);

                    _db.ExecuteNonQuery(cmd);
                }
            }
        }

        protected void AddLandingPage(Visitor visitor)
        {
            using (DbCommand cmd = _db.GetSqlStringCommand(MtelligentQueries.AddVisitorLandingPage))
            {
                _db.AddInParameter(cmd, "@VisitorId", DbType.Int32, visitor.Id);
                _db.AddInParameter(cmd, "@LandingPageUrl", DbType.String, visitor.Request.LandingUrl);

                _db.ExecuteNonQuery(cmd);
            }
        }

        public Entities.Experiment GetExperiment(string experimentName)
        {
            Experiment experiment = null;

            using (DbCommand cmd = _db.GetSqlStringCommand(MtelligentQueries.GetExperiment))
            {
                _db.AddInParameter(cmd, "@SystemName", DbType.String, experimentName);

                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        experiment = ReaderToExperiment(reader);
                        break;
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        experiment.Segments.Add(ReaderToExperimentSegment(reader));
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        AddVariableToSegment(experiment, reader);
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        experiment.TargetCohort = ReaderToCohort(reader);
                        break;
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        var kvp = ReaderToKVP(reader);
                        experiment.TargetCohort.Properties.Add(kvp.Key, kvp.Value);
                    }
                }
            }

            return experiment;
        }

        public Goal GetGoal(string goalName)
        {
            using (DbCommand cmd = _db.GetSqlStringCommand(MtelligentQueries.GetGoal))
            {
                _db.AddInParameter(cmd, "@SystemName", DbType.String, goalName);

                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        return ReaderToGoal(reader);
                    }
                }
            }

            return null;
        }

       
    }
}
