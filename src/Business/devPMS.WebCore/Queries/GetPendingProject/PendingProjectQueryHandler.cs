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
    public class PendingProjectQueryHandler: IQueryHandler<PendingProjectQuery,Project_Log[]>
    {
        private readonly IDbContextFactory _facotry;

        public PendingProjectQueryHandler(IDbContextFactory factory)
        {
            _facotry = factory;
        }

        public Project_Log[] Handle(PendingProjectQuery query)
        {
            using (var uow = _facotry.Create(DBContextName.PMSEntities))
            {
                return uow.Repository<Project_Log>().Find(p => p.Status.Equals(query.Status)).ToArray();
            }
        }
    }
}
