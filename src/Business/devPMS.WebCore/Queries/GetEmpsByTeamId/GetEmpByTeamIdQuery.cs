using devForum.Data;
using Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    public class GetEmpByTeamIdQuery  :IQuery<IEnumerable<Employee>>
    {
        public int teamId { get; set; }
    }
}
