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
    public class GoalRepository : SQLRepository, IGoalRepository
    {
        public GoalRepository()
        {
            _db = GetDatabase();
        }

        public IQueryable<Goal> GetAll()
        {
            var goals = new List<Goal>();

            using (DbCommand cmd = _db.GetSqlStringCommand(GoalQueries.GetGoals))
            {
                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        goals.Add(ReaderToGoal(reader));
                    }
                }
            }

            return goals.AsQueryable();            
        }

        public Goal Get(int Id)
        {
            using (DbCommand cmd = _db.GetSqlStringCommand(GoalQueries.GetGoal))
            {
                _db.AddInParameter(cmd, "@GoalID", DbType.Int32, Id);

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

        public Entities.Goal Add(Entities.Goal goal)
        {
            using (DbCommand cmd = _db.GetSqlStringCommand(GoalQueries.AddGoal))
            {
                _db.AddInParameter(cmd, "@Name", DbType.String, goal.Name);
                _db.AddInParameter(cmd, "@SystemName", DbType.String, goal.SystemName);
                _db.AddInParameter(cmd, "@GACode", DbType.String, goal.GACode);
                _db.AddInParameter(cmd, "@CustomJS", DbType.String, goal.CustomJS);
                _db.AddInParameter(cmd, "@CreatedBy", DbType.String, goal.CreatedBy);

                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        return ReaderToGoal(reader);
                    }
                }
            }

            return goal;
        }

        public Entities.Goal Update(Entities.Goal goal)
        {
            using (DbCommand cmd = _db.GetSqlStringCommand(GoalQueries.UpdateGoal))
            {
                _db.AddInParameter(cmd, "@Name", DbType.String, goal.Name);
                _db.AddInParameter(cmd, "@GACode", DbType.String, goal.GACode);
                _db.AddInParameter(cmd, "@CustomJS", DbType.String, goal.CustomJS);
                _db.AddInParameter(cmd, "@UpdatedBy", DbType.String, goal.UpdatedBy);
                _db.AddInParameter(cmd, "@GoalID", DbType.Int32, goal.Id);

                _db.ExecuteNonQuery(cmd);
            }

            return goal;
        }

        public void Delete(Entities.Goal goal)
        {
            using (DbCommand cmd = _db.GetSqlStringCommand(GoalQueries.DeleteGoal))
            {
                _db.AddInParameter(cmd, "@GoalID", DbType.Int32, goal.Id);
                _db.AddInParameter(cmd, "@UpdatedBy", DbType.Int32, goal.UpdatedBy);
                _db.ExecuteNonQuery(cmd);
            }

        }

    }
}
