using devPMS.Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    public class GetServiceSubTasksQueryHandler : IQueryHandler<GetServiceSubTasksQuery, SubTask[]>
    {
        private readonly IDbContextFactory _facotry;

        public GetServiceSubTasksQueryHandler(IDbContextFactory facotry)
        {
            _facotry = facotry;
        }

        public SubTask[] Handle(GetServiceSubTasksQuery query)
        {
            using (var uow = _facotry.Create(DBContextName.PMSEntities))
            {
                return uow.Repository<SubTask>().Find(p => p.ScopeID == query.scopeId).ToArray();
            }
        }
    }
}
