using devForum.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace devPMS.Web.Models
{
    public class WidgetViewModel
    {
        private IList<Category> _categories = new List<Category>();
        private IList<Tag> _tags = new List<Tag>();
        private IList<Thread> _threads = new List<Thread>();

        public WidgetViewModel()
        {
        }

        public IList<Category> Categories
        {
            get
            {
                return _categories;
            }
            set
            {
                _categories.Clear();
                _categories = value;
            }
        }

        public IList<Tag> Tags
        {
            get
            {
                return _tags;
            }
            set
            {
                _tags.Clear();
                _tags = value;
            }
        }

        public IList<Thread> LatestPosts
        {
            get
            {
                return _threads;
            }
            set
            {
                _threads.Clear();
                _threads = value;
            }
        }

        public void ClearAll()
        {
            _categories.Clear();
            _tags.Clear();
            _threads.Clear();
        }
    }
}