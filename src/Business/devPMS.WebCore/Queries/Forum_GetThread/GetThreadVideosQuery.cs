using University;
using Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    public class GetThreadVideosQuery : IQuery<IEnumerable<ThreadVideo>>
    {
        public Thread Thread { get; set; }
    }
}
