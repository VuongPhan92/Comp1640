using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Business
{
    public class UpdateDescriptionCommand 
    {
        public int ID { get; set; }
         
        public string Description { get; set; }
        public bool IncludeScopeofService { get; set; }
    }
}
