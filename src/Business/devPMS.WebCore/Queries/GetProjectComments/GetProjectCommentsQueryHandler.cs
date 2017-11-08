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
    public class GetProjectCommentsQueryHandler : IQueryHandler<GetProjectCommentsQuery, IEnumerable<ProjectComment>>
    {
        private readonly IDbContextFactory _facotry;

        public GetProjectCommentsQueryHandler(IDbContextFactory facotry)
        {
            _facotry = facotry;
        }

        public IEnumerable<ProjectComment> Handle(GetProjectCommentsQuery query)
        {
            using (var uow = _facotry.Create(DBContextName.PMSEntities))
            {
                List<string> includeProperties = new List<string>();
                if (query.IncludeEmployee)
                {
                    includeProperties.Add(PropertyHelper<ProjectComment>.GetPropertyName(p => p.Employee));
                }
                return uow.Repository<ProjectComment>().Find(p => p.ProjectId == query.ProjectId, string.Join(",", includeProperties));
            }
        }
    }
}
