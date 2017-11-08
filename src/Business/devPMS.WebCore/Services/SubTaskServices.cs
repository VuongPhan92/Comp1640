using devPMS.Data;
using devPMS.WebCore.Queries;
using Infrastructure.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Services
{
    public class SubTaskServices : IService
    {
        private readonly GetServiceSubTasksQueryHandler _getServiceSubTasksQueryHandler;

        public SubTaskServices(GetServiceSubTasksQueryHandler getServiceSubTasksQueryHandler)
        {
            _getServiceSubTasksQueryHandler = getServiceSubTasksQueryHandler;
        }
        public SubTask[] GetSubTaskServices(GetServiceSubTasksQuery query)
        {
            return _getServiceSubTasksQueryHandler.Handle(query);
        }
    }
}
