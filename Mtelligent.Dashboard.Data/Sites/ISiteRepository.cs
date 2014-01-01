using Mtelligent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtelligent.Dashboard.Data
{
    public interface ISiteRepository
    {
        IQueryable<Site> GetAll();
        Site Get(int Id);
        Site Add(Site site);
        Site Update(Site site);
        void Delete(Site site);

        SiteUrl AddSiteUrl(SiteUrl url);
        void DeleteSiteUrl(SiteUrl url);
    }
}
