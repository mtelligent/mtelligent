using Mtelligent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtelligent.Dashboard.Data
{
    public interface IExperimentRepository
    {
        IQueryable<Experiment> GetAll();
        Experiment Get(int Id);
        Experiment Add(Experiment experiment);
        Experiment Update(Experiment experiment);
        void Delete(Experiment experiment);

        ExperimentSegment AddSegment(ExperimentSegment segment);
        ExperimentSegment UpdateSegment(ExperimentSegment segment);

    }
}
