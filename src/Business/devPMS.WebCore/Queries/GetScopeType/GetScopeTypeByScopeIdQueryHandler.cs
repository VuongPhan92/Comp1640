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
    public class GetScopeTypeByScopeIdQueryHandler : IQueryHandler<GetScopeTypeByScopeIdQuery, IEnumerable<ScopeType>>
    {
        private readonly IDbContextFactory _facotry;

        public GetScopeTypeByScopeIdQueryHandler(IDbContextFactory facotry)
        {
            _facotry = facotry;
        }

        public IEnumerable<ScopeType> Handle(GetScopeTypeByScopeIdQuery query)
        {
            using (var uow = _facotry.Create(DBContextName.PMSEntities))
            {
                return uow.Repository<ScopeType>()
                    .Find(p=>p.Unload.HasValue == false && p.ScopeId == query.ScopeId)
                    .OrderBy(p=>p.ScopeId)
                    .OrderBy(p=>p.OrderList);
            }
        }
    }
}
