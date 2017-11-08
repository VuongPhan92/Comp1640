using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Business
{
    public class UpdatePriorityIdCommand
    {
        public int ID { get; set; }
        public int? PriorityId { get; set; }
        public bool IncludePriority { get; set; }
    }
}
