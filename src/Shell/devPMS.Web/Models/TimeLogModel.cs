using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace devPMS.Web.Models
{
    public class TimeLogModel
    {
        public string FullName { get; set; }
        public DateTime? WorkdedDate { get; set; }
        public double? Hours { get; set; }
        public string Team { get; set; }
    }
}