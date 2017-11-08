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
    public class GetTagsQueryHandler : IQueryHandler<GetTagsQuery, IEnumerable<Tag>>
    {
        private readonly IDbContextFactory _factory;

        public GetTagsQueryHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }

        public IEnumerable<Tag> Handle(GetTagsQuery query)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                if (query.IsIncludeChecked)
                {
                    return uow.Repository<Tag>().GetAll();
                }
                else
                {
                    return uow.Repository<Tag>().Find(p => p.Checked != true);
                }
            }
        }
    }
}
