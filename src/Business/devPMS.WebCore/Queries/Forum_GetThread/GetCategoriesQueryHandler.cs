using University;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    public class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, IEnumerable<Category>>
    {
        private readonly IDbContextFactory _factory;

        public GetCategoriesQueryHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }

        public IEnumerable<Category> Handle(GetCategoriesQuery query)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                if (query.IsIncludeChecked)
                {
                    return uow.Repository<Category>().GetAll();
                }
                else
                {
                    return uow.Repository<Category>().Find(p => p.Checked != true);
                }
            }
        }
    }
}
