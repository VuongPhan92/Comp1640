using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Business
{
    public class UpdateThreadCommentCommand
    {
        public int CommentId { get; set; }
        public int ThreadId { get; set; }
        public int UserId { get; set; }
        public string Body { get; set; }
    }
}
