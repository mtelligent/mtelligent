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
    public class CohortRepository : SQLRepository, ICohortRepository
    {

        public CohortRepository()
        {
            _db = GetDatabase();
        }

        public IQueryable<Cohort> GetAll()
        {
            var cohorts = new List<Cohort>();

            using (DbCommand cmd = _db.GetSqlStringCommand(CohortQueries.GetCohorts))
            {
                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        cohorts.Add(ReaderToCohort(reader));
                    }
                }
            }

            return cohorts.AsQueryable();   
        }

        public Cohort Get(int Id)
        {
            Cohort cohort = null;

            using (DbCommand cmd = _db.GetSqlStringCommand(CohortQueries.GetCohort))
            {
                _db.AddInParameter(cmd, "@CohortId", DbType.Int32, Id);

                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        cohort = ReaderToCohort(reader);
                        break;
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        var kvp = ReaderToKVP(reader);
                        cohort.Properties.Add(kvp.Key, kvp.Value);
                    }
                }
            }

            return cohort;
        }

        public Cohort Add(Cohort cohort)
        {
            Cohort rtnCohort = null;

            using (DbCommand cmd = _db.GetSqlStringCommand(CohortQueries.AddCohort))
            {
                _db.AddInParameter(cmd, "@Name", DbType.String, cohort.Name);
                _db.AddInParameter(cmd, "@SystemName", DbType.String, cohort.SystemName);
                _db.AddInParameter(cmd, "@Type", DbType.String, cohort.TypeName);
                _db.AddInParameter(cmd, "@CreatedBy", DbType.String, cohort.CreatedBy);

                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        rtnCohort = ReaderToCohort(reader);
                    }
                }
            }

            foreach (string key in cohort.Properties.Keys)
            {
                using (DbCommand cmd = _db.GetSqlStringCommand(CohortQueries.AddCohortProperty))
                {
                    _db.AddInParameter(cmd, "@CohortId", DbType.Int32, rtnCohort.Id);
                    _db.AddInParameter(cmd, "@Name", DbType.String, key);
                    _db.AddInParameter(cmd, "@Value", DbType.String, cohort.Properties[key]);

                    _db.ExecuteNonQuery(cmd);
                }
            }

            return rtnCohort;
        }

        public Cohort Update(Cohort cohort)
        {
            using (DbCommand cmd = _db.GetSqlStringCommand(CohortQueries.UpdateCohort))
            {
                _db.AddInParameter(cmd, "@Name", DbType.String, cohort.Name);
                _db.AddInParameter(cmd, "@UpdatedBy", DbType.String, cohort.UpdatedBy);
                _db.AddInParameter(cmd, "@CohortId", DbType.Int32, cohort.Id);

                _db.ExecuteNonQuery(cmd);
            }

            using (DbCommand cmd = _db.GetSqlStringCommand(CohortQueries.DeleteCohortProperties))
            {
                _db.AddInParameter(cmd, "@CohortId", DbType.Int32, cohort.Id);
                _db.ExecuteNonQuery(cmd);
            }


            foreach (string key in cohort.Properties.Keys)
            {
                using (DbCommand cmd = _db.GetSqlStringCommand(CohortQueries.AddCohortProperty))
                {
                    _db.AddInParameter(cmd, "@CohortId", DbType.Int32, cohort.Id);
                    _db.AddInParameter(cmd, "@Name", DbType.String, key);
                    _db.AddInParameter(cmd, "@Value", DbType.String, cohort.Properties[key]);

                    _db.ExecuteNonQuery(cmd);
                }
            }

            return cohort;
        }

        public void Delete(Cohort cohort)
        {
            using (DbCommand cmd = _db.GetSqlStringCommand(CohortQueries.DeleteCohort))
            {
                _db.AddInParameter(cmd, "@CohortId", DbType.Int32, cohort.Id);
                _db.AddInParameter(cmd, "@UpdatedBy", DbType.String, cohort.UpdatedBy);

                _db.ExecuteNonQuery(cmd);
            }
        }

    }
}
