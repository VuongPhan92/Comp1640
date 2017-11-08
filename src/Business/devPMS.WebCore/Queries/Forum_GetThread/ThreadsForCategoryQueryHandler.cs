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
    public class ThreadsForCategoryQueryHandler : IQueryHandler<ThreadsForCategoryQuery, PagedListResult<Thread>>
    {
        private readonly IDbContextFactory _factory;

        public ThreadsForCategoryQueryHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }

        public PagedListResult<Thread> Handle(ThreadsForCategoryQuery query)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                var ThreadRepo = uow.Repository<Thread>();
                var searchQuery = new SearchQuery<Thread>();
                searchQuery.AddFilter(p => p.IsPublished == true && p.Category.UrlSeo.Equals(query.UrlSeo));

                string includeProperties = string.Join(",", PropertyHelper<Thread>.GetPropertyNames(u => u.Category, u => u.Tags));
                searchQuery.IncludeProperties = includeProperties;// "Category,Tags";

                searchQuery.Skip = (query.PageNo - 1) * query.PageSize;
                searchQuery.Take = query.PageSize;

                ISortCriteria<Thread> sort = new SortCriteria<Thread>(PropertyHelper<Thread>.GetPropertyName(u => u.CreatedDT), SortDirection.Descending);

                searchQuery.SortCriterias.Add(sort);
                return ThreadRepo.Search(searchQuery);
            }
        }
    }
}
