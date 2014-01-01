using Mtelligent.Data;
using Mtelligent.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtelligent.Dashboard.Data
{
    public class ExperimentRepository : SQLRepository, IExperimentRepository
    {
        public ExperimentRepository()
        {
            _db = GetDatabase();
        }

        public IQueryable<Experiment> GetAll()
        {
            var experiments = new List<Experiment>();

            using (DbCommand cmd = _db.GetSqlStringCommand(ExperimentQueries.GetExperiments))
            {
                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        experiments.Add(ReaderToExperiment(reader));
                    }
                }
            }

            return experiments.AsQueryable();            
        }

        public Experiment Get(int Id)
        {
            Experiment experiment = null;

            using (DbCommand cmd = _db.GetSqlStringCommand(ExperimentQueries.GetExperiment))
            {
                _db.AddInParameter(cmd, "@ExperimentId", DbType.Int32, Id);

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
                }
            }

            return experiment;
        }



        public Experiment Add(Experiment experiment)
        {
            Experiment rtnExperiment = null;

            using (DbCommand cmd = _db.GetSqlStringCommand(ExperimentQueries.AddExperiment))
            {
                _db.AddInParameter(cmd, "@Name", DbType.String, experiment.Name);
                _db.AddInParameter(cmd, "@SystemName", DbType.String, experiment.SystemName);
                _db.AddInParameter(cmd, "@TargetCohortId", DbType.Int32, experiment.TargetCohortId);
                _db.AddInParameter(cmd, "@CreatedBy", DbType.String, experiment.CreatedBy);

                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        rtnExperiment = ReaderToExperiment(reader);
                    }
                }
            }

            foreach (string variable in experiment.Variables)
            {
                using (DbCommand cmd = _db.GetSqlStringCommand(ExperimentQueries.AddExperimentVariable))
                {
                    _db.AddInParameter(cmd, "@ExperimentId", DbType.Int32, rtnExperiment.Id);
                    _db.AddInParameter(cmd, "@Name", DbType.String, variable);

                    rtnExperiment.Variables.Add(variable);

                    _db.ExecuteNonQuery(cmd);
                }
            }

            return rtnExperiment;
        }

        public Experiment Update(Experiment experiment)
        {
            using (DbCommand cmd = _db.GetSqlStringCommand(ExperimentQueries.UpdateExperiment))
            {
                _db.AddInParameter(cmd, "@Name", DbType.String, experiment.Name);
                _db.AddInParameter(cmd, "@UpdatedBy", DbType.String, experiment.UpdatedBy);
                _db.AddInParameter(cmd, "@TargetCohortId", DbType.Int32, experiment.TargetCohortId);
                _db.AddInParameter(cmd, "@ExperimentId", DbType.Int32, experiment.Id);

                _db.ExecuteNonQuery(cmd);
            }

            var updated = Get(experiment.Id);

            //only add new variables as they shouldn't be deleted/updated
            if (experiment.Variables.Count != updated.Variables.Count)
            {
                foreach (string variable in experiment.Variables)
                {
                    if (updated.Variables.IndexOf(variable) == -1)
                    {
                        using (DbCommand cmd = _db.GetSqlStringCommand(ExperimentQueries.AddExperimentVariable))
                        {
                            _db.AddInParameter(cmd, "@ExperimentId", DbType.Int32, experiment.Id);
                            _db.AddInParameter(cmd, "@Name", DbType.String, variable);

                            _db.ExecuteNonQuery(cmd);
                        }
                    }
                }
            }


            

            return experiment;
        }

        public void Delete(Experiment experiment)
        {
            using (DbCommand cmd = _db.GetSqlStringCommand(ExperimentQueries.DeleteExperiment))
            {
                _db.AddInParameter(cmd, "@ExperimentId", DbType.Int32, experiment.Id);
                _db.AddInParameter(cmd, "@UpdatedBy", DbType.String, experiment.UpdatedBy);

                _db.ExecuteNonQuery(cmd);
            }
        }

        public ExperimentSegment AddSegment(ExperimentSegment segment)
        {
            ExperimentSegment rtnSegment = null;

            using (DbCommand cmd = _db.GetSqlStringCommand(ExperimentQueries.AddExperimentSegment))
            {
                _db.AddInParameter(cmd, "@Name", DbType.String, segment.Name);
                _db.AddInParameter(cmd, "@SystemName", DbType.String, segment.SystemName);
                _db.AddInParameter(cmd, "@TargetPercentage", DbType.Double, segment.TargetPercentage);
                _db.AddInParameter(cmd, "@IsDefault", DbType.Int32, segment.IsDefault);
                _db.AddInParameter(cmd, "@ExperimentId", DbType.Int32, segment.ExperimentId);
                _db.AddInParameter(cmd, "@CreatedBy", DbType.String, segment.CreatedBy);

                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        rtnSegment = ReaderToExperimentSegment(reader);
                    }
                }
            }

            foreach (string variable in segment.Variables.Keys)
            {
                using (DbCommand cmd = _db.GetSqlStringCommand(ExperimentQueries.AddExperimentSegmentVariableValue))
                {
                    _db.AddInParameter(cmd, "@ExperimentId", DbType.Int32, segment.ExperimentId);
                    _db.AddInParameter(cmd, "@ExperimentSegmentID", DbType.Int32, rtnSegment.Id);
                    _db.AddInParameter(cmd, "@Name", DbType.String, variable);
                    _db.AddInParameter(cmd, "@Value", DbType.String, segment.Variables[variable]);

                    rtnSegment.Variables.Add(variable, segment.Variables[variable]);

                    _db.ExecuteNonQuery(cmd);
                }
            }

            return rtnSegment;
        }

        public ExperimentSegment UpdateSegment(ExperimentSegment segment)
        {
            using (DbCommand cmd = _db.GetSqlStringCommand(ExperimentQueries.UpdateExperimentSegment))
            {
                _db.AddInParameter(cmd, "@Name", DbType.String, segment.Name);
                _db.AddInParameter(cmd, "@UpdatedBy", DbType.String, segment.UpdatedBy);
                _db.AddInParameter(cmd, "@TargetPercentage", DbType.Double, segment.TargetPercentage);
                _db.AddInParameter(cmd, "@IsDefault", DbType.Int32, segment.IsDefault);

                _db.ExecuteNonQuery(cmd);
            }

            using (DbCommand cmd = _db.GetSqlStringCommand(ExperimentQueries.DeleteExperimentSegmentVariableValues))
            {
                _db.AddInParameter(cmd, "@ExperimentSegmentId", DbType.Int32, segment.Id);
                _db.ExecuteNonQuery(cmd);
            }

            foreach (string variable in segment.Variables.Keys)
            {
                using (DbCommand cmd = _db.GetSqlStringCommand(ExperimentQueries.AddExperimentSegmentVariableValue))
                {
                    _db.AddInParameter(cmd, "@ExperimentId", DbType.Int32, segment.ExperimentId);
                    _db.AddInParameter(cmd, "@ExperimentSegmentID", DbType.Int32, segment.Id);
                    _db.AddInParameter(cmd, "@Name", DbType.String, variable);
                    _db.AddInParameter(cmd, "@Value", DbType.String, segment.Variables[variable]);

                    _db.ExecuteNonQuery(cmd);
                }
            }

            return segment; 
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
            var segmentID = Convert.ToInt32(reader["ExperimentSegementID"]);
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
    }
}
