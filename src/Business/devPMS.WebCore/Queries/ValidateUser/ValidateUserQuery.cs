using Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    public class ValidateUserQuery : IQuery<bool>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
