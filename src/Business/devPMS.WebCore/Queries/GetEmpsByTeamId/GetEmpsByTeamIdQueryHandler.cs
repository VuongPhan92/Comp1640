using devForum.Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    public class GetEmpsByTeamIdQueryHandler :IQueryHandler<GetEmpByTeamIdQuery,IEnumerable< Employee>>
    {
        private readonly IDbContextFactory _facotry;

        public GetEmpsByTeamIdQueryHandler(IDbContextFactory factory)
        {
            _facotry = factory;
        }
        public IEnumerable<Employee> Handle(GetEmpByTeamIdQuery query)
        {
            using (var uow = _facotry.Create(DBContextName.ForumEtities))
            {
                return uow.Repository<Employee>().Find(p => p.TeamID == query.teamId);
            }
        }
    }
}
