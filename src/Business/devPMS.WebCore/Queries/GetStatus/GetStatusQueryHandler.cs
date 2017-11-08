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
    public class GetStatusQueryHandler : IQueryHandler<GetStatusQuery, IEnumerable<Status>>
    {
        private readonly IDbContextFactory _facotry;

        public GetStatusQueryHandler(IDbContextFactory facotry)
        {
            _facotry = facotry;
        }

        public IEnumerable<Status> Handle(GetStatusQuery query)
        {
            using (var uow = _facotry.Create(DBContextName.PMSEntities))
            {
                return uow.Repository<Status>()
                    .Find(p=>p.InActive != true)
                    .OrderBy(p=>p.StatusId);
            }
        }
    }
}
