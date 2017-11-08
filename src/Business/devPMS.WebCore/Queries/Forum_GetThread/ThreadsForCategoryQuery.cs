using University;
using Infrastructure.Queries;
using SSTVN.Core.Repository.PageResult;

namespace devPMS.WebCore.Queries
{
    public class ThreadsForCategoryQuery : IQuery<PagedListResult<Thread>>
    {
        public string UrlSeo { get; set; }
        public int PageSize { get; set; }
        public int PageNo { get; set; }
    }
}
