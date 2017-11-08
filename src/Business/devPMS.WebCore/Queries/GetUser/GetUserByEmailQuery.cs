using University;
using Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    public class GetUserByEmailQuery : IQuery<Employee>
    {
        public string Email { get; set; }
        // Add properties IsIncluded GroupTeam, or Role here
    }
}
