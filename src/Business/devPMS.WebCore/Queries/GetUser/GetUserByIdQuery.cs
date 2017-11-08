using University;
using Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    public class GetUserByIdQuery : IQuery<Employee>
    {
        public int Id { get; set; }
        // Add properties IsIncluded GroupTeam, or Role here
    }
}
