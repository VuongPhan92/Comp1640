using devPMS.Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System.Linq;

namespace devPMS.WebCore.Queries
{
    public class GetTimeLogByTeamIdQueryHandler : IQueryHandler<GetTimeLogByTeamIdQuery, TimeLog[]>
    {
        private readonly IUnitOfWork _uow;
        private readonly IQueryHandler<GetTimeLogByTeamIdQuery,TimeLog[]> _getTimeLogByTeamId;

        public GetTimeLogByTeamIdQueryHandler(IUnitOfWork uow, IQueryHandler<GetTimeLogByTeamIdQuery,TimeLog[]> getTimeLogByTeamId )
        {
            _uow = uow;
            _getTimeLogByTeamId = getTimeLogByTeamId;
        }

        public TimeLog[] Handle(GetTimeLogByTeamIdQuery query)
        {
            return _uow.Repository<TimeLog>().Find(p => p.Employee.TeamID == query.teamId).Where(p => p.WorkedDate.Equals(query.selectedMonth)).ToArray();
        }
    }
}