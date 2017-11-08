using University;
using Infrastructure.Caching;
using Infrastructure.Queries;
using System.Collections.Generic;

namespace devPMS.WebCore.Queries
{
    public class GetThreadCommentsQuery : IQuery<IEnumerable<Comment>>
    {
        public int ThreadId { get; set; }
        public bool IsIncludeChecked { get; set; }
    }
}
