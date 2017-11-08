using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace devPMS.Web.Models
{
    public class TimeTrackingModel
    {
        public long UserId { get; set; }
        public string Fullname { get; set; }
        public DateTime Date { get; set; }
        public string FirstIn { get; set; }
        public string LastOut { get; set; }
        public string EventNote { get; set; }
        public double ValueVacation { get; set; }
        public string TAWeekID { get; set; }
    }
}