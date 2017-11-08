using University;
using devPMS.WebCore.Business;
using devPMS.WebCore.Queries;
using Infrastructure.Decorator;
using Infrastructure.Queries;
using SSTVN.Core.Repository.PageResult;
using SSTVN.PMS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Services
{
    public class ForumServices : IService
    {
        private readonly GetThreadQueryHandler _getThread;
        private readonly ThreadsByPageQueryHandler _getThreadsByPageHandler;
        private readonly ThreadsForCategoryQueryHandler _getThreadsForCategoryHandler;
        private readonly ThreadsForTagQueryHandler _getThreadForTagHandler;
        private readonly IQueryHandler<LikeDislikeCountQuery, int> _likeDislikeCountHandler;
        private readonly ICommandHandler<CreateThreadCommand> _createThreadCommand;
        private readonly IQueryHandler<GetCategoriesQuery, IEnumerable<Category>> _getCategoriesHandler;
        private readonly IQueryHandler<GetTagsQuery, IEnumerable<Tag>> _getTagsHandler;
        private readonly TagService _tagService;
        private readonly CategoryService _categoryService;
        private readonly CommentService _commentService;
        private readonly ICommandHandler<LikeDislikeCommand> _likeDislikeHandler;
        private readonly IQueryHandler<IsLikeDislikeQuery,bool> _isLikeDislikeHandler;
        public ForumServices(
            ICommandHandler<CreateThreadCommand> createThreadCommand,
            TagService tagService,
            CategoryService categoryService,
            IQueryHandler<GetCategoriesQuery, IEnumerable<Category>> getCategoriesHandler,
            IQueryHandler<GetTagsQuery, IEnumerable<Tag>> getTagsHandler,
            GetThreadQueryHandler getThreadHandler,
            ThreadsByPageQueryHandler getThreadsByPageHandler,
            ThreadsForCategoryQueryHandler getThreadsForCategoryHandler,
            ThreadsForTagQueryHandler getThreadForTagHandler,
            IQueryHandler<LikeDislikeCountQuery, int> likeDislikeCountHandler,
            CommentService commentService,
            ICommandHandler<LikeDislikeCommand> likeDislikeHandler,
            IQueryHandler<IsLikeDislikeQuery,bool> isLikeDislikeHandler)
        {
            _createThreadCommand = createThreadCommand;
            _tagService = tagService;
            _categoryService = categoryService;
            _getCategoriesHandler = getCategoriesHandler;
            _getTagsHandler = getTagsHandler;
            _getThread = getThreadHandler;
            _getThreadsByPageHandler = getThreadsByPageHandler;
            _getThreadsForCategoryHandler = getThreadsForCategoryHandler;
            _getThreadForTagHandler = getThreadForTagHandler;
            _likeDislikeCountHandler = likeDislikeCountHandler;
            _commentService = commentService;
            _likeDislikeHandler = likeDislikeHandler;
            _isLikeDislikeHandler = isLikeDislikeHandler;
        }

        public Thread GetThread(int year, int month, int day, string title, bool isIncrementView = false)
        {
            return _getThread.Handle(new GetThreadQuery()
                { Year = year, Month = month, Day = day, Title = title, IsIncrementView = isIncrementView });
        }

        public PagedListResult<Thread> ThreadsByPage(int pageNo, int pageSize = 10)
        {
            return _getThreadsByPageHandler.Handle(new ThreadsByPageQuery() { PageNo = pageNo, PageSize = pageSize });
        }

        public PagedListResult<Thread> ThreadsForTag(string tagSlug, int pageNo, int pageSize = 10)
        {
            return _getThreadForTagHandler.Handle(new ThreadsForTagQuery() { UrlSeo = tagSlug, PageNo = pageNo, PageSize = pageSize });
        }

        public PagedListResult<Thread> ThreadsForCategory(string catSlg, int pageNo, int pageSize = 10)
        {
            return _getThreadsForCategoryHandler.Handle(new ThreadsForCategoryQuery() { UrlSeo = catSlg, PageNo = pageNo, PageSize = pageSize });
        }

        

        #region Categories & Tags
        public List<Category> Categories
        {
            get
            {
                return _getCategoriesHandler.Handle(new GetCategoriesQuery { IsIncludeChecked = false }).ToList();
            }
        }

        public List<Tag> Tags
        {
            get
            {
                return _getTagsHandler.Handle(new GetTagsQuery { IsIncludeChecked = false }).ToList();
            }
        }
        #endregion

        #region Commands
        public void CreateThread(CreateThreadCommand createThreadCommand)
        {
            _createThreadCommand.Handle(createThreadCommand);
        }

        public void CreateTag(CreateTagCommand command)
        {
            _tagService.CreateTag(command);
        }

        public void UpdateTag(UpdateTagCommand command)
        {
            _tagService.UpdateTag(command);
        }

        public void DeleteTag(int id)
        {
            _tagService.DeleteTag(new DeleteTagCommand { TagId = id });
        }

        public void CreateCategory(CreateCategoryCommand command)
        {
            _categoryService.CreateCat(command);
        }

        public void UpdateCategory(UpdateCategoryCommand command)
        {
            _categoryService.UpdateCat(command);
        }

        public void DeleteCategory(int id)
        {
            _categoryService.DeleteCat(new DeleteCategoryCommand { CatId = id });
        }

        public int LikeDislikeCount(TypeAndLike typeLike, int id)
        {
            return _likeDislikeCountHandler.Handle(new LikeDislikeCountQuery { TypeLike = typeLike, Id = id});
        }
        #endregion

        public CommentService ComentService
        {
            get { return _commentService; }
        }

        public void Like(TypeAndLike typeLike, int id, int userId)
        {
            _likeDislikeHandler.Handle(new LikeDislikeCommand { TypeLike = typeLike, Id = id, UserId = userId });
        }

        public bool IsLiked(TypeAndLike typeLike,int id,int userId)
        {
           return _isLikeDislikeHandler.Handle(new IsLikeDislikeQuery { TypeLike = typeLike, Id = id, UserId = userId });
        }
    }
}
