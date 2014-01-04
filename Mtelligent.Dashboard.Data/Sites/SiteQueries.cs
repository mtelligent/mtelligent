using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtelligent.Dashboard.Data
{
    public class SiteQueries
    {
        public const string GetSites = @"Select * from Sites (nolock) where Active=1; Select * from SiteUrls where Active=1";
        public const string GetSite = @"Select * from Sites (nolock) where Id = @SiteId; Select * from SiteUrls where Active=1 and SiteId = @SiteId";
        public const string AddSite =
                @"Insert into Sites (Name, Created, CreatedBy) Values (@Name, getDate(), @CreatedBy);
                select * from Sites (nolock) where Id = scope_Identity()";
        public const string UpdateSite = @"Update Sites Set Name=@Name, Updated=getDate(), UpdatedBy=@UpdatedBy Where Id = @SiteId";
        public const string DeleteSite = @"Update Sites set Active=0, Updated=getDate(), UpdatedBy=@UpdatedBy where Id = @SiteId; Update SiteUrls set Active=0, Updated=getDate(), UpdatedBy=@UpdatedBy where SiteId=@SiteId";

        public const string AddSiteUrl = @"Insert into SiteUrls (Url, SiteId, Created, CreatedBy) Values (@Url, @SiteId, getDate(), @CreatedBy);
                                           select * from SiteUrls (nolock) where Id = scope_Identity()";
        public const string DeleteSiteUrl = @"Update SiteUrls set Active=0, Updated=getDate(), UpdatedBy=@UpdatedBy where Id=@SiteUrlId";


    }
}
