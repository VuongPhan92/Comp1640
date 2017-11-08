using devPMS.Data;
using devPMS.WebCore.Business;
using devPMS.WebCore.Queries;
using Infrastructure.Decorator;
using Infrastructure.Queries;
using Infrastructure.Repository;
using SSTVN.Core.Repository;
using SSTVN.Core.Repository.PageResult;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Services
{
    public class TeamServices: IService
    {
        private readonly IUnitOfWork _uow;
        private readonly IQueryHandler<GetAllTeamsQuery,IEnumerable<GroupTeam>> _getTeamHandler;

        public TeamServices(IUnitOfWork uow,
            IQueryHandler<GetAllTeamsQuery,IEnumerable<GroupTeam>> getTeamHandler
            )
        {
            _uow = uow;
            _getTeamHandler = getTeamHandler;
        }

        public IEnumerable<GroupTeam> GetAllTeams()
        {
            return _getTeamHandler.Handle(new GetAllTeamsQuery { });
        }
        public int Complete()
        {
           return _uow.SubmitChanges();
        }
    }
}
