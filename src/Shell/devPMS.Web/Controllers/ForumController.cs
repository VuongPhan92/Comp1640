using University;
using devPMS.Web.Models;
using devPMS.WebCore.Business;
using devPMS.WebCore.Services;
using SSTVN.PMS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace devPMS.Web.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {
        public static ThreadsViewModel ThreadVM = new ThreadsViewModel();
        private readonly ForumServices _forumServices;
        private readonly EmployeeServices _employeeServices;

        public ForumController(ForumServices forumServices, EmployeeServices employeeServices)
        {
            _forumServices = forumServices;
            _employeeServices = employeeServices;
            ThreadVM.Tags = _forumServices.Tags;
            ThreadVM.Categories = _forumServices.Categories;
        }

        // GET: Forum
        public ActionResult Index(int p = 1)
        {
            ThreadVM.Threads.Clear();
            var pagedResult = _forumServices.ThreadsByPage(p);

            foreach (var th in pagedResult.Entities)
            {
                ThreadViewModel threadViewModel = ThreadViewModel.ToConvert(th);
                threadViewModel.CommentViewModel = CreateCommentViewModel(th.ThreadId);
                threadViewModel.Author = _employeeServices.GetUserById(th.UserId);
                threadViewModel.Likes = _forumServices.LikeDislikeCount(TypeAndLike.ThreadLike, th.ThreadId);
                ThreadVM.Threads.Add(threadViewModel);
            }
            ThreadVM.TotalPosts = pagedResult.BaseCount;
            ViewBag.Title = "Latest Posts";
            return View(ThreadVM);
        }

        #region Create Thread
        [HttpGet]
        public ActionResult CreateThread()
        {
            return PartialView("_CreateThread");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateThread(ThreadsViewModel threadVM)
        {
            if (ModelState.IsValid)
            {
                var md = new CreateThreadCommand();
                md.UserId = threadVM.UserId;
                md.CategoryId = threadVM.CategoryId;
                md.Title = threadVM.Title;
                md.ShortDescription = threadVM.ShortDescription;
                md.Body = threadVM.Body;
                md.IsPublished = true;
                md.Image = threadVM.Image;
                md.Tags = threadVM.TagStringIds;
                _forumServices.CreateThread(md);
                return RedirectToAction("Index", "Forum");
            }
            return View("_CreateThread", threadVM);
        }
        #endregion

        public ViewResult Thread(int year, int month, int day, string title, string ReturnUrl = "")
        {
            var thread = _forumServices.GetThread(year, month, day, title, true);

            if (thread == null)
            {
                throw new HttpException(404, "thread not found");
            }

            if (thread.IsPublished == false)
            {
                throw new HttpException(401, "The thread is not published");
            }

            ThreadViewModel threadViewModel = ThreadViewModel.ToConvert(thread);
            threadViewModel.CommentViewModel = CreateCommentViewModel(thread.ThreadId);
            threadViewModel.Author = _employeeServices.GetUserById(thread.UserId);
            threadViewModel.Likes = _forumServices.LikeDislikeCount(TypeAndLike.ThreadLike, thread.ThreadId);
            ViewBag.ReturnUrl = ReturnUrl;
            return View(threadViewModel);
        }

        //[ChildActionOnly]
        //public PartialViewResult Sidebars()
        //{
        //    var widgetViewModel = new WidgetViewModel();
        //    //widgetViewModel.Categories = _forumServices.g
        //    return PartialView("_Sidebars", widgetViewModel);
        //}

        #region Manage 
        public ActionResult Manage()
        {
            return View();
        }

        #region Threads CRUD
        public ActionResult _Thread(jQueryDataTableParamModel param)
        {
            var result = _forumServices.ThreadsByPage(1);
            return Json(new
            {
                draw = param.draw,
                //recordsTotal = TotalProjects,
                //recordsFiltered = recordsFilteredCount,
                data = result
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Categories CRUD
        public ActionResult _Category(jQueryDataTableParamModel param)
        {
            var result = _forumServices.Categories;
            return Json(new
            {
                draw = param.draw,
                //recordsTotal = TotalProjects,
                //recordsFiltered = recordsFilteredCount,
                data = result
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CategoryUpdate(int id)
        {
            var tag = _forumServices.Categories.SingleOrDefault(p => p.CategoryId == id);
            return View(tag);
        }

        [HttpPost]
        public ActionResult CategoryUpdate(Category t)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                var command = new UpdateCategoryCommand { Obj = t };
                _forumServices.UpdateCategory(command);
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult CategoryCreate()
        {
            var category = new Category();
            return View(category);
        }

        [HttpPost]
        public ActionResult CategoryCreate(Category t)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                var command = new CreateCategoryCommand { Obj = t };
                _forumServices.CreateCategory(command);
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult CategoryDelete(int id)
        {
            var tag = _forumServices.Categories.SingleOrDefault(p => p.CategoryId == id);
            return View(tag);
        }

        [HttpPost]
        public ActionResult CategoryDelete(int id, FormCollection form)
        {
            bool status = false;
            _forumServices.DeleteCategory(id);
            status = true;
            return new JsonResult { Data = new { status = status } };
        }

        #endregion

        #region Tags CRUD
        public ActionResult _Tag(jQueryDataTableParamModel param)
        {
            var result = _forumServices.Tags;
            return Json(new
            {
                draw = param.draw,
                //recordsTotal = TotalProjects,
                //recordsFiltered = recordsFilteredCount,
                data = result
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult TagUpdate(int id)
        {
            var tag = _forumServices.Tags.SingleOrDefault(p => p.TagId == id);
            return View(tag);
        }

        [HttpPost]
        public ActionResult TagUpdate(Tag t)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                var command = new UpdateTagCommand { Obj = t };
                _forumServices.UpdateTag(command);
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult TagCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TagCreate(Tag t)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                var command = new CreateTagCommand { Obj = t };
                _forumServices.CreateTag(command);
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult TagDelete(int id)
        {
            var tag = _forumServices.Tags.SingleOrDefault(p => p.TagId == id);
            return View(tag);
        }

        [HttpPost]
        public ActionResult TagDelete(int id, FormCollection form)
        {
            bool status = false;
            _forumServices.DeleteTag(id);
            status = true;
            return new JsonResult { Data = new { status = status } };
        }
        #endregion

        #endregion

        #region Comment, reply
        public CommentViewModel LoadAllComments(int threadId)
        {
            CommentViewModel model = new CommentViewModel();
            List<CommentViewModel> commentsVM = new List<CommentViewModel>();
            var comments = _forumServices.ComentService.GetThreadComments(threadId).OrderByDescending(d => d.CreatedDT).ToList();
            var users = _employeeServices.GetUsers();
            foreach (var comment in comments)
            {
                var likes = _forumServices.LikeDislikeCount(TypeAndLike.CommentLike, comment.CommentId);
                commentsVM.Add(CommentViewModel.ToConvert(comment, likes, users));
            }
            model.Comments = commentsVM;
            return model;
        }

        public CommentViewModel CreateCommentViewModel(int threadId)
        {        
            return LoadAllComments(threadId);
        }

        public ActionResult InsertComment(CommentViewModel vm)
        {
            //insert new comment
            _forumServices.ComentService.AddThreadComment(vm.ThreadId, vm.UserId, vm.Body);
            //re-load all comments to render
            var model = LoadAllComments(vm.ThreadId);
            return PartialView("Comments", model);
        }
        
        public ActionResult UpdateComment(CommentViewModel vm)
        {
            _forumServices.ComentService.UpdateThreadComment(vm.ThreadId, vm.UserId, vm.CommentId, vm.Body);
            var model = LoadAllComments(vm.ThreadId);
            return PartialView("Comments",model);
        }

        // check this later
        public PartialViewResult Comment (CommentViewModel vm)
        {
            return PartialView("Comments", vm);
        }


        public ActionResult EditComment(CommentViewModel vm)
        {
            
            return View("EditComment", vm );
        }

       
        #endregion

        #region helpers
        
        public static Dictionary<string, string> CommentIdsForView(int id,string prefix)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            //count_like_comment_@c.CommentId
            dic.Add(prefix, prefix + id);
            return dic;
        }

        public int LikeDislikeCount(TypeAndLike typelike, int id)
        {
            return _forumServices.LikeDislikeCount(typelike, id);
        }
        #endregion

        #region Like Thread
        [HttpGet]
        public JsonResult LikeThread(int userId, int threadId, int authorId)
        {          
            _forumServices.Like(TypeAndLike.ThreadLike, threadId, userId);
            var count = LikeDislikeCount(TypeAndLike.ThreadLike, threadId);
            var like = IsLikedThread(userId,threadId);
            return Json(
                new 
                { 
                    count, threadId, like
                } , JsonRequestBehavior.AllowGet);
           
        }

        private bool IsLikedThread(int userId,int threadId)
        {
           return _forumServices.IsLiked(TypeAndLike.ThreadLike, threadId, userId);
        }  
        #endregion

        #region Like Comment
        [HttpGet]
        public JsonResult LikeComment(int userId, int commentId, int authorId)
        {
            _forumServices.Like(TypeAndLike.CommentLike, commentId, userId);
            var count = LikeDislikeCount(TypeAndLike.CommentLike, commentId);
            var like = IsLikeComment(userId, commentId);
            return Json(new
            {
                count,commentId,authorId,like
            }
            , JsonRequestBehavior.AllowGet);
        }

        private bool IsLikeComment(int userId, int commentId)
        {
            return _forumServices.IsLiked(TypeAndLike.ThreadLike, commentId, userId);
        }
        #endregion
    }
}