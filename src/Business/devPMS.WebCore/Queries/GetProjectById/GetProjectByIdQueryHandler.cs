using devPMS.Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using SSTVN.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSTVN.DDo.Utility.Extension;

namespace devPMS.WebCore.Queries
{
    public class GetProjectByIdQueryHandler : IQueryHandler<GetProjectByIdQuery, Project_Log>
    {
        private readonly IDbContextFactory _facotry;

        public GetProjectByIdQueryHandler(IDbContextFactory facotry)
        {
            _facotry = facotry;
        }

        public Project_Log Handle(GetProjectByIdQuery query)
        {
            using (var uow = _facotry.Create(DBContextName.PMSEntities))
            {
                List<string> includeProperties = new List<string>();
                if (query.IncludeScopeofService)
                {
                    includeProperties.Add(PropertyHelper<Project_Log>.GetPropertyName(p => p.ScopeOfService));
                }
                if (query.IncludeScopeType)
                {
                    includeProperties.Add(PropertyHelper<Project_Log>.GetPropertyName(p => p.ScopeType));
                }
                //return uow.Repository<Project_Log>().GetById(p => p.ID == query.Id, PropertyHelper<Project_Log>.GetPropertyName(p => p.ScopeOfService));

                return uow.Repository<Project_Log>().GetById(p => p.ID == query.Id, string.Join(",", includeProperties));
            }
        }
    }
}
