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
    public class GetThreadTagsQueryHandler : IQueryHandler<GetThreadTagsQuery, IEnumerable<Tag>>
    {
        private readonly IDbContextFactory _factory;

        public GetThreadTagsQueryHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }

        public IEnumerable<Tag> Handle(GetThreadTagsQuery query)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                return uow.Repository<Tag>().Find(p => p.Threads.Any(t => t.ThreadId == query.Thread.ThreadId));
            }
        }
    }
}
