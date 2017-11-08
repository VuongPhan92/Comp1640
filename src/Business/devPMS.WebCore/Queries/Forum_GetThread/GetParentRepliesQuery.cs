using University;
using Infrastructure.Caching;
using Infrastructure.Queries;
using System.Collections.Generic;

namespace devPMS.WebCore.Queries
{
    public class GetParentRepliesQuery : IQuery<IEnumerable<Reply>>
    {
        public int CommentId { get; set; }
        public bool IsIncludeChecked { get; set; }
    }
}
