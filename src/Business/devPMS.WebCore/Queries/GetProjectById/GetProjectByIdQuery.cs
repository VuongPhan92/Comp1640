using devPMS.Data;
using Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    public class GetProjectByIdQuery : IQuery<Project_Log>
    {
        public int Id { get; set; }
        public bool IncludeScopeofService { get; set; }
        public bool IncludeScopeType { get; set; }
    }
}
