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


        public Entities.Visitor GetDetails(Entities.Visitor visitor)
        {
            throw new NotImplementedException();
        }

        public Visitor ReconcileUser(Visitor visitor)
        {
            throw new NotImplementedException();
        }

        public Entities.Visitor GetLandingPages(Entities.Visitor visitor)
        {
            throw new NotImplementedException();
        }

        public Entities.Visitor GetReferrers(Entities.Visitor visitor)
        {
            throw new NotImplementedException();
        }

        public Visitor GetAttributes(Visitor visitor)
        {
            throw new NotImplementedException();
        }

        public Entities.Visitor GetCohorts(Entities.Visitor visitor)
        {
            throw new NotImplementedException();
        }

        public Entities.Visitor GetSegments(Entities.Visitor visitor)
        {
            throw new NotImplementedException();
        }

        public Entities.Visitor GetConversions(Entities.Visitor visitor)
        {
            throw new NotImplementedException();
        }

        public Entities.Visitor SaveChanges(Entities.Visitor visitor)
        {
            throw new NotImplementedException();
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

        private Experiment ReaderToExperiment(IDataReader reader)
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

        private ExperimentSegment ReaderToExperimentSegment(IDataReader reader)
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

        private void AddVariableToSegment(Experiment experiment, IDataReader reader)
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

        public Cohort ReaderToCohort(IDataReader reader)
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

        public KeyValuePair<string, string> ReaderToKVP(IDataReader reader)
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


        private Goal ReaderToGoal(IDataReader reader)
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





       
    }
}
