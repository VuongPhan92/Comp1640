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
    public class GetScopeTypeQueryHandler : IQueryHandler<GetScopeTypeQuery, IEnumerable<ScopeType>>
    {
        private readonly IDbContextFactory _facotry;

        public GetScopeTypeQueryHandler(IDbContextFactory facotry)
        {
            _facotry = facotry;
        }

        public IEnumerable<ScopeType> Handle(GetScopeTypeQuery query)
        {
            using (var uow = _facotry.Create(DBContextName.PMSEntities))
            {
                return uow.Repository<ScopeType>()
                    .Find(p=>p.Unload == null)
                    .OrderBy(p=>p.ScopeId)
                    .OrderBy(p=>p.OrderList);
            }
        }
    }
}
