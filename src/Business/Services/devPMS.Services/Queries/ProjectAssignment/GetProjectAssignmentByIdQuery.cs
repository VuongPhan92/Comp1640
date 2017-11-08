using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDoCommon.Decorator;
using devPMS.Data;

namespace devPMS.Services.Queries
{
    public class GetProjectAssignmentByIdQuery:IQuery<Assignment>
    {
        public int Id { get; set; }
    }
}
