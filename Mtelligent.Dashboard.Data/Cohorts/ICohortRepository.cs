using Mtelligent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtelligent.Dashboard.Data
{
    public interface ICohortRepository
    {
        IQueryable<Cohort> GetAll();
        Cohort Get(int Id);
        Cohort Add(Cohort cohort);
        Cohort Update(Cohort cohort);
        void Delete(Cohort cohort);
    }
}
