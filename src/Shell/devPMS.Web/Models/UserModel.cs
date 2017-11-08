using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace devPMS.Web.Models
{
    public class UserModel
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public string Image { get; set; }
        public string Position { get; set; }
        public DateTime StartDate { get; set; }
    }
}