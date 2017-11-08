using devPMS.Data;
using Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    public class GetProjectCommentsQuery : IQuery<IEnumerable<ProjectComment>>
    {
        public int ProjectId { get; set; }
        public bool IncludeEmployee { get; set; }
    }
}
