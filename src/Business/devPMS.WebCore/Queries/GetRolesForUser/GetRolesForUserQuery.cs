using Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    public class GetRolesForUserQuery: IQuery<string[]>
    {
        public string Email { get; set; }
    }
}
