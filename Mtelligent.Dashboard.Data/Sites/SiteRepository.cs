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
    public class SiteRepository : SQLRepository, ISiteRepository
    {
        public SiteRepository()
        {
            _db = GetDatabase();
        }

        public IQueryable<Site> GetAll()
        {
            var sites = new List<Site>();

            using (DbCommand cmd = _db.GetSqlStringCommand(SiteQueries.GetSites))
            {
                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        sites.Add(ReadertoSite(reader));
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        SiteUrl siteUrl = ReadertoSiteUrl(reader);
                        var site = sites.FirstOrDefault(a => a.Id == siteUrl.SiteId);
                        if (site != null)
                        {
                            site.Urls.Add(siteUrl);
                        }
                    }
                }
            }

            return sites.AsQueryable();   
        }

        public Site Get(int Id)
        {
            Site site = null;

            using (DbCommand cmd = _db.GetSqlStringCommand(SiteQueries.GetSite))
            {
                _db.AddInParameter(cmd, "@SiteId", DbType.Int32, Id);

                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        site = ReadertoSite(reader);
                        break;
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        SiteUrl siteUrl = ReadertoSiteUrl(reader);
                        if (site != null)
                        {
                            site.Urls.Add(siteUrl);
                        }
                    }

                    return site;
                }
            }
        }

        public Site Add(Site site)
        {
            using (DbCommand cmd = _db.GetSqlStringCommand(SiteQueries.AddSite))
            {
                _db.AddInParameter(cmd, "@Name", DbType.String, site.Name);
                _db.AddInParameter(cmd, "@CreatedBy", DbType.String, site.CreatedBy);

                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        return ReadertoSite(reader);
                    }
                }
            }

            return site;
        }

        public Site Update(Site site)
        {
            using (DbCommand cmd = _db.GetSqlStringCommand(SiteQueries.UpdateSite))
            {
                _db.AddInParameter(cmd, "@Name", DbType.String, site.Name);
                _db.AddInParameter(cmd, "@UpdatedBy", DbType.String, site.UpdatedBy);
                _db.AddInParameter(cmd, "@SiteId", DbType.Int32, site.Id);

                _db.ExecuteNonQuery(cmd);
            }

            return site;
        }

        public void Delete(Site site)
        {
            using (DbCommand cmd = _db.GetSqlStringCommand(SiteQueries.DeleteSite))
            {
                _db.AddInParameter(cmd, "@SiteId", DbType.Int32, site.Id);
                _db.AddInParameter(cmd, "@UpdatedBy", DbType.Int32, site.UpdatedBy);
                _db.ExecuteNonQuery(cmd);
            }
        }

        public SiteUrl AddSiteUrl(SiteUrl url)
        {
            using (DbCommand cmd = _db.GetSqlStringCommand(SiteQueries.AddSiteUrl))
            {
                _db.AddInParameter(cmd, "@Url", DbType.String, url.Url);
                _db.AddInParameter(cmd, "@SiteId", DbType.Int32, url.SiteId);
                _db.AddInParameter(cmd, "@CreatedBy", DbType.String, url.CreatedBy);

                using (IDataReader reader = _db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        return ReadertoSiteUrl(reader);
                    }
                }
            }

            return url;
        }

        public void DeleteSiteUrl(SiteUrl url)
        {
            using (DbCommand cmd = _db.GetSqlStringCommand(SiteQueries.DeleteSiteUrl))
            {
                _db.AddInParameter(cmd, "@SiteurlId", DbType.Int32, url.Id);
                _db.AddInParameter(cmd, "@UpdatedBy", DbType.String, url.UpdatedBy);
                _db.ExecuteNonQuery(cmd);
            }
        }

        public Site ReadertoSite(IDataReader reader)
        {
            var site = new Site();

            if (reader["Id"] != DBNull.Value)
            {
                site.Id = Convert.ToInt32(reader["Id"]);
            }

            if (reader["Name"] != DBNull.Value)
            {
                site.Name = reader["Name"].ToString();
            }

            if (reader["CreatedBy"] != DBNull.Value)
            {
                site.CreatedBy = reader["CreatedBy"].ToString();
            }

            if (reader["UpdatedBy"] != DBNull.Value)
            {
                site.UpdatedBy = reader["UpdatedBy"].ToString();
            }

            if (reader["Created"] != DBNull.Value)
            {
                site.Created = Convert.ToDateTime(reader["Created"]);
            }

            if (reader["Updated"] != DBNull.Value)
            {
                site.Updated = Convert.ToDateTime(reader["Updated"]);
            }

            site.Urls = new List<SiteUrl>();

            return site;
        }

        public SiteUrl ReadertoSiteUrl(IDataReader reader)
        {
            var siteUrl = new SiteUrl();

            if (reader["Id"] != DBNull.Value)
            {
                siteUrl.Id = Convert.ToInt32(reader["Id"]);
            }

            if (reader["SiteId"] != DBNull.Value)
            {
                siteUrl.SiteId = Convert.ToInt32(reader["SiteId"]);
            }

            if (reader["Url"] != DBNull.Value)
            {
                siteUrl.Url = reader["Url"].ToString();
            }

            if (reader["CreatedBy"] != DBNull.Value)
            {
                siteUrl.CreatedBy = reader["CreatedBy"].ToString();
            }

            if (reader["UpdatedBy"] != DBNull.Value)
            {
                siteUrl.UpdatedBy = reader["UpdatedBy"].ToString();
            }

            if (reader["Created"] != DBNull.Value)
            {
                siteUrl.Created = Convert.ToDateTime(reader["Created"]);
            }

            if (reader["Updated"] != DBNull.Value)
            {
                siteUrl.Updated = Convert.ToDateTime(reader["Updated"]);
            }

            return siteUrl;
        }
    }
}
