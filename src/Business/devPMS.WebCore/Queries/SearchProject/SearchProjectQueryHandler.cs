using devPMS.Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using SSTVN.Core.Repository.PageResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    public class SearchProjectQueryHandler : IQueryHandler<SearchProjectQuery, PagedListResult<Project_Log>>
    {
        private readonly IDbContextFactory _facotry;

        public SearchProjectQueryHandler(IDbContextFactory facotry)
        {
            _facotry = facotry;
        }

        public PagedListResult<Project_Log> Handle(SearchProjectQuery query)
        {
            using (var uow = _facotry.Create(DBContextName.PMSEntities))
            {
                return uow.Repository<Project_Log>().Search(query.SearchQuery);
            }
        }
    }
}
