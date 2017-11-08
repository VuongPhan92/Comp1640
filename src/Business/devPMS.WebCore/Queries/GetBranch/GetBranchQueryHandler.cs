using devPMS.Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using SSTVN.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    public class GetBranchQueryHandler : IQueryHandler<GetBranchQuery, IEnumerable<Branch>>
    {
        private readonly IDbContextFactory _factory;

        public GetBranchQueryHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }

        public IEnumerable<Branch> Handle(GetBranchQuery query)
        {
            using (var uow = _factory.Create(DBContextName.PMSEntities))
            {
                return uow.Repository<Branch>().Find(p => p.UnLoad == null);
            }
        }
    }
}
