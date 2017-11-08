using devPMS.Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;

namespace devPMS.WebCore.Queries
{
    public class GetAllTeamsQueryHandler : IQueryHandler<GetAllTeamsQuery, IEnumerable<GroupTeam>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllTeamsQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<GroupTeam> Handle(GetAllTeamsQuery query)
        {
            return _uow.Repository<GroupTeam>().GetAll();
        }
    }
}