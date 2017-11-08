using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Business
{
    public class AddThreadCommentCommand 
    {
        public int ThreadId { get; set; }
        public int UserId { get; set; }
        public string Body { get; set; }
    }
}
