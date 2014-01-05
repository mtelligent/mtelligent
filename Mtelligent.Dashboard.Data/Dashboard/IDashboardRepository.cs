using Mtelligent.Dashboard.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtelligent.Dashboard.Data
{
    public interface IDashboardRepository
    {
        List<ExperimentSummary> GetExperimentStatuses();
    }
}
