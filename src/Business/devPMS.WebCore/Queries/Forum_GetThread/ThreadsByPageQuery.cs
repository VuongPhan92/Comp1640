using University;
using Infrastructure.Queries;
using SSTVN.Core.Repository.PageResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    public class ThreadsByPageQuery : IQuery<PagedListResult<Thread>>
    {
        public bool IsAllThreadIncludePublished { get; set; }
        public int PageSize { get; set; }
        public int PageNo { get; set; }
    }
}
