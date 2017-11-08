using University;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;

namespace devPMS.Web.Models
{
    public class ThreadsViewModel
    {
        private IList<ThreadViewModel> _threads;
        public ThreadsViewModel()
        {
            _threads = new List<ThreadViewModel>();
        }
        public IList<ThreadViewModel> Threads { get { return _threads; } }
        public IList<Category> Categories;
        public IList<Tag> Tags;
        public long TotalPosts { get; set; }
        public string Search { get; set; }
        public Employee Author { get; set; }

        // for new thread creation.
        public int ThreadId { get; set; }
        [DisplayName("Category")]
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Title { get; set; }
        [DisplayName("Short Description")]
        [Required]
        public string ShortDescription { get; set; }
        [DisplayName("Description")]
        [AllowHtml]
        [Required]
        public string Body { get; set; }
        public bool IsPublished { get; set; }
        public string Image { get; set; }
        //CreatedBy
        public int UserId { get; set; }
        public DateTime CreatedDT { get; set; }
        //ModifiedBy
        public Nullable<DateTime> ModifiedDT { get; set; }
        //Tags
        [DisplayName("Tags")]
        [Required(ErrorMessage = "The {0} must be at least 1 tag.")]
        public List<string> TagStringIds { get; set; }
    }

    public class ThreadViewModel
    {
        public int ThreadId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Body { get; set; }
        public bool IsPublished { get; set; }
        public string Image { get; set; }
        public string Meta { get; set; }
        public string UrlSeo { get; set; }
        public int View { get; set; }
        public DateTime CreatedDT { get; set; }
        public DateTime? ModifiedDT { get; set; }
        public Category ThreadCategory { get; set; }
        public IList<Tag> Tags { get; set; }
        public IList<ThreadLike> ThreadLikes { get; set; }
        public int Likes { get; set; }
        public Employee Author { get; set; }

        public CommentViewModel CommentViewModel { get; set; }

        public static ThreadViewModel ToConvert(Thread thread)
        {
            ThreadViewModel threadViewModel = new ThreadViewModel();
            threadViewModel.ThreadId = thread.ThreadId;
            threadViewModel.Title = thread.Title;
            threadViewModel.ShortDescription = thread.ShortDescription;
            threadViewModel.Body = thread.Body;
            threadViewModel.IsPublished = thread.IsPublished;
            threadViewModel.Image = thread.Image;
            threadViewModel.Meta = thread.Meta;
            threadViewModel.UrlSeo = thread.UrlSeo;
            threadViewModel.CreatedDT = thread.CreatedDT;
            threadViewModel.ModifiedDT = thread.ModifiedDT;
            threadViewModel.Tags = thread.Tags.ToList();
            threadViewModel.ThreadCategory = thread.Category;
            threadViewModel.View = thread.View;
            threadViewModel.ThreadLikes = thread.ThreadLikes.ToList();
            return threadViewModel;
        }
    }

    public class CommentViewModel
    {
        public CommentViewModel() { }
        public CommentViewModel(Comment comment)
        {
            Comment = comment;
        }
        public Comment Comment { get; set; }
        public IList<CommentViewModel> Comments { get; set; }
        public IList<CommentViewModel> ChildReplies { get; set; }
        // the first comment in the thread
        public int CommentId { get; set; }
        public int ThreadId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPhoto { get; set; }
        public string UrlSeo { get; set; }
        [AllowHtml]
        public string Body { get; set; }
        public int Likes { get; set; }
        public DateTime CreatedDT { get; set; }
        public DateTime ModifiedDT { get; set; }
        public int ParentReplyId { get; set; }
        public IList<CommentLike> CommentLikes { get; set; }

        public static CommentViewModel ToConvert(Comment cm, int likes, IList<Employee> users)
        {
            CommentViewModel cmVM = new CommentViewModel();
            cmVM.CommentId = cm.CommentId;
            cmVM.ThreadId = cm.ThreadId;
            cmVM.UserId = cm.UserId;
            cmVM.Body = cm.Body;
            cmVM.CreatedDT = cm.CreatedDT;
            var user = users.SingleOrDefault(p => p.EmployeeID == cm.UserId);
            if (user != null)
            {
                cmVM.UserName = user.FullName;
                cmVM.UserPhoto = user.Photo;
            }
            cmVM.Likes = likes;
            cmVM.CommentLikes = cm.CommentLikes.ToList();
            return cmVM;
        }
    }
}