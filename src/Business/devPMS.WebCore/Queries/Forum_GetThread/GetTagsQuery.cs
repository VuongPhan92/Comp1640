using University;
using Infrastructure.Caching;
using Infrastructure.Queries;
using System.Collections.Generic;

namespace devPMS.WebCore.Queries
{
    [CachePolicy(absoluteExpirationInSeconds: 1 * 60 * 2)] // 2mins
    public class GetTagsQuery : IQuery<IEnumerable<Tag>>
    {
        public bool IsIncludeChecked { get; set; }
    }
}
