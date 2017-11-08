using devPMS.Data;
using Infrastructure.Caching;
using Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    [CachePolicy(absoluteExpirationInSeconds: 1 * 60 * 1)] // 1mins
    public class GetScopeOfServicesQuery : IQuery<IEnumerable<ScopeOfService>>
    {
        
    }
}
