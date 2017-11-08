using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Business
{
    public class CreateThreadCommand
    {
        //public int ThreadId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Body { get; set; }
        public string Meta { get; set; }
        public bool IsPublished { get; set; }
        public System.DateTime CreatedDT { get; set; }
        public Nullable<System.DateTime> ModifiedDT { get; set; }
        public Nullable<System.DateTime> DeletedDT { get; set; }
        public string Image { get; set; }
        public int UserId { get; set; }
        public List<string> Tags { get; set; }
    }
}
