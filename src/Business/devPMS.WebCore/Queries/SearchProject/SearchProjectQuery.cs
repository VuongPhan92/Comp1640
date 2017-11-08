using devPMS.Data;
using Infrastructure.Queries;
using SSTVN.Core.Repository.PageResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    public class SearchProjectQuery : IQuery<PagedListResult<Project_Log>>
    {
        public SearchQuery<Project_Log> SearchQuery { get; set; }
    }
}
