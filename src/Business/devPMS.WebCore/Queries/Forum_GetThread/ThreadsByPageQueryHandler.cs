using University;
using Infrastructure.Queries;
using Infrastructure.Repository;
using SSTVN.Core.Repository.PageResult;
using SSTVN.Core.Repository.Sort;
using SSTVN.DDo.Utility.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    public class ThreadsByPageQueryHandler : IQueryHandler<ThreadsByPageQuery, PagedListResult<Thread>>
    {
        private readonly IDbContextFactory _factory;

        public ThreadsByPageQueryHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }

        public PagedListResult<Thread> Handle(ThreadsByPageQuery query)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                var ThreadRepo = uow.Repository<Thread>();
                var searchQuery = new SearchQuery<Thread>();

                if (!query.IsAllThreadIncludePublished)
                {
                    searchQuery.AddBaseFilter(p => p.IsPublished == true);
                }

                string includeProperties = string.Join(",", PropertyHelper<Thread>.GetPropertyNames(u => u.Category, u => u.Tags, u=>u.ThreadLikes));
                searchQuery.IncludeProperties = includeProperties;// "Category,Tags, Threadlikes";

                searchQuery.Skip = (query.PageNo - 1) * query.PageSize;
                searchQuery.Take = query.PageSize;

                ISortCriteria<Thread> sort = new SortCriteria<Thread>(PropertyHelper<Thread>.GetPropertyName(u => u.CreatedDT), SortDirection.Descending);

                searchQuery.SortCriterias.Add(sort);
                return ThreadRepo.Search(searchQuery);
            }
        }
    }
}
