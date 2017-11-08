using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Queries;
using devPMS.Data;

namespace devPMS.WebCore.Queries
{
    public class GetTimeLogByTeamIdQuery: IQuery<TimeLog[]>
    {
        public int teamId { get; set; }
        public DateTime selectedMonth { get; set; }
       
    }
}
