using Mtelligent.Dashboard.Data.Entities;
using Mtelligent.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtelligent.Dashboard.Data
{
    public class DashboardRepository : SQLRepository, IDashboardRepository
    {
        public DashboardRepository()
        {
            _db = GetDatabase();
        }

        public List<ExperimentSummary> GetExperimentStatuses()
        {
            var summaries = new Dictionary<int, ExperimentSummary>();

            using (DbCommand cmd = _db.GetSqlStringCommand(DashboardQueries.GetExperimentStatuses))
            {
                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var experimentid = Convert.ToInt32(reader["Experiment Id"]);

                        if (!summaries.ContainsKey(experimentid))
                        {
                            var newSummary = new ExperimentSummary();
                            newSummary.Name = reader["Experiment Name"].ToString();
                            summaries.Add(experimentid, newSummary);
                        }

                        var summary = summaries[experimentid];

                        var hypothesisId = Convert.ToInt32(reader["Hypothesis Id"]);

                        if (!summary.Hypotheses.ContainsKey(hypothesisId))
                        {
                            var newHyp = new HypothesisSummary();
                            newHyp.Name = reader["Hypothesis Name"].ToString();
                            newHyp.Visitors = Convert.ToInt32(reader["Visitors"]);

                            summary.Hypotheses.Add(hypothesisId, newHyp);
                        }

                        var hypothesis = summary.Hypotheses[hypothesisId];

                        var goalName = reader["Goal Name"].ToString();

                        if (!summary.Goals.ContainsKey(goalName))
                        {
                            GoalSummary goalSummary = new GoalSummary();
                            goalSummary.Name = goalName;
                            goalSummary.GoalValue = Convert.ToDouble(reader["Goal Value"]);
                            summary.Goals.Add(goalName, goalSummary);
                        }

                        
                        var conversions = Convert.ToInt32(reader["Conversions"]);
                        var convValue = Convert.ToDouble(reader["Conversion Value"]);

                        hypothesis.GoalConversions.Add(goalName, conversions);
                        hypothesis.GoalValues.Add(goalName, convValue);

                    }
                }
            }

            return summaries.Values.ToList();
        }

    }
}
