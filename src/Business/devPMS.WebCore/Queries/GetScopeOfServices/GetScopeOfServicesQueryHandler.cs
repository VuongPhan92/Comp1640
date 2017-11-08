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
    public class GetScopeOfServicesQueryHandler : IQueryHandler<GetScopeOfServicesQuery, IEnumerable<ScopeOfService>>
    {
        private readonly IDbContextFactory _facotry;

        public GetScopeOfServicesQueryHandler(IDbContextFactory facotry)
        {
            _facotry = facotry;
        }

        public IEnumerable<ScopeOfService> Handle(GetScopeOfServicesQuery query)
        {
            using (var uow = _facotry.Create(DBContextName.PMSEntities))
            {
                return uow.Repository<ScopeOfService>()
                    .Find(p=>p.UnLoad.HasValue == false && p.CategoryId != null)
                    .OrderBy(p=>p.CategoryId);
            }
        }
    }
}
