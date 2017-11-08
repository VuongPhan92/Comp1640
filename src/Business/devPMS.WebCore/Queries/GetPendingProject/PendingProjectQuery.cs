using devPMS.Data;
using Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    public class PendingProjectQuery : IQuery<Project_Log[]>
    {
        public string Status { get { return "Completed"; } }
        public bool IncludeScopeofService { get; set; }
    }
}
