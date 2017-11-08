using devPMS.Data;
using devPMS.WebCore.Queries;
using Infrastructure.Decorator;
using Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Services
{
    public class ScopeServices: IService
    {
        private readonly GetScopeOfServicesQueryHandler _getScopeOfServicesHandler;
        private readonly GetScopeTypeByScopeIdQueryHandler _getScopeTypeHandler;
        private readonly IQueryHandler<GetStatusQuery, IEnumerable<Status>> _getStatus;

        public ScopeServices(
            GetScopeOfServicesQueryHandler getScopeOfServicesHandler,
            GetScopeTypeByScopeIdQueryHandler getScopeTypeQueryHandler,
            IQueryHandler<GetStatusQuery, IEnumerable<Status>> getStatus)
        {
            _getScopeOfServicesHandler = getScopeOfServicesHandler;
            _getScopeTypeHandler = getScopeTypeQueryHandler;
            _getStatus = getStatus;
        }

        public IEnumerable<ScopeOfService> GetScopeOfServices()
        {
            return _getScopeOfServicesHandler.Handle(new GetScopeOfServicesQuery());
        }

        public IEnumerable<ScopeType> GetScopeTypebyScopeId(int scopeId)
        {
            return _getScopeTypeHandler.Handle(new GetScopeTypeByScopeIdQuery { ScopeId = scopeId });
        }

        public IEnumerable<Status> GetStatus()
        {
            return _getStatus.Handle(new GetStatusQuery());
        }
    }
}
