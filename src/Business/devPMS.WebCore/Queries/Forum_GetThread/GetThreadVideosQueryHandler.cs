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
    public class GetThreadVideosQueryHandler : IQueryHandler<GetThreadVideosQuery, IEnumerable<ThreadVideo>>
    {
        private readonly IDbContextFactory _factory;

        public GetThreadVideosQueryHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }

        public IEnumerable<ThreadVideo> Handle(GetThreadVideosQuery query)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                return uow.Repository<ThreadVideo>().Find(p => p.ThreadId == query.Thread.ThreadId);
            }
        }
    }
}
