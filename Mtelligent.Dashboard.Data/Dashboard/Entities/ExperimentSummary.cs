using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtelligent.Dashboard.Data.Entities
{
    public class ExperimentSummary
    {
        public ExperimentSummary()
        {
            this.Hypotheses = new Dictionary<int, HypothesisSummary>();
            this.Goals = new Dictionary<string, GoalSummary>();
        }

        public string Name { get; set; }
        public Dictionary<string, GoalSummary> Goals { get; set; }
        public Dictionary<int, HypothesisSummary> Hypotheses { get; set; }
    }
}
