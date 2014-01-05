using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtelligent.Dashboard.Data.Entities
{
    public class HypothesisSummary
    {
        public HypothesisSummary()
        {
            GoalConversions = new Dictionary<string, int>();
            GoalValues = new Dictionary<string, double>();
        }

        public string Name { get; set; }
        public int Visitors { get; set; }
        public Dictionary<string, int> GoalConversions { get; set; }
        public Dictionary<string, double> GoalValues { get; set; }
    }
}
