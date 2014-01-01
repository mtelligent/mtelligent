using Mtelligent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtelligent.Dashboard.Data
{
    public interface IGoalRepository
    {
        IQueryable<Goal> GetAll();
        Goal Get(int Id);
        Goal Add(Goal goal);
        Goal Update(Goal goal);
        void Delete(Goal goal);
    }
}
