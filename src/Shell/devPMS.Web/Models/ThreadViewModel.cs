using devForum.Data;
using devPMS.Data;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace devPMS.Web.Models
{
    public class ThreadsViewModel
    {
        private IList<Thread> _threads;
        public ThreadsViewModel()
        {
            _threads = new List<Thread>();
        }
        public IList<Thread> Threads { get { return _threads; } }
        public long TotalPosts { get; set; }
        public string Search { get; set; }
        public Employee Author { get; set; }


        public int ThreadId { get { return 2; }  }
        public int CategoryId { get { return 1; }  }
        public string Title { get { return "test"; }}
        [AllowHtml]
        public string ShortDescription { get; set; }
        [AllowHtml]
        public string Body { get { return "test"; }  }
        public string Meta { get { return "test"; }  }
        public string UrlSeo { get { return "test"; }  }
        public bool IsPublished { get; set; }
        public int View { get { return 100; }  }
        public System.DateTime CreatedDT { get; set; }
        public Nullable<System.DateTime> ModifiedDT { get; set; }
        public Nullable<System.DateTime> DeletedDT { get; set; }
        public string Image { get; set; }
        public int UserId { get { return 138; } }
    }
}