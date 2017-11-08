using University;
using devPMS.WebCore.Business;
using devPMS.WebCore.Queries;
using Infrastructure.Decorator;
using Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Services
{
    public class CommentService : IService
    {
        IQueryHandler<GetThreadCommentsQuery, IEnumerable<Comment>> _getThreadComments;
        IQueryHandler<GetParentRepliesQuery, IEnumerable<Reply>> _getParentReplies;
        ICommandHandler<AddThreadCommentCommand> _addThreadComment;
        ICommandHandler<UpdateThreadCommentCommand> _updateThreadComment;

        public CommentService(IQueryHandler<GetThreadCommentsQuery, IEnumerable<Comment>> getThreadComments
            , IQueryHandler<GetParentRepliesQuery, IEnumerable<Reply>> getParentReplies
            , ICommandHandler<AddThreadCommentCommand> addThreadComment
            , ICommandHandler<UpdateThreadCommentCommand> updateThreadComment)
        {
            _getThreadComments = getThreadComments;
            _getParentReplies = getParentReplies;
            _addThreadComment = addThreadComment;
            _updateThreadComment = updateThreadComment;
        }

        public List<Comment> GetThreadComments(int threadId, bool isChecked = false)
        {
            return _getThreadComments.Handle(new GetThreadCommentsQuery
                { ThreadId = threadId, IsIncludeChecked = isChecked }).ToList();
        }

        public List<Reply> GetParentReplies(int commentId, bool isChecked = false)
        {
            return _getParentReplies.Handle(new GetParentRepliesQuery
            {
                CommentId = commentId,
                IsIncludeChecked = isChecked
            }).ToList();
        }

        public void AddThreadComment(int threadId, int userId, string commentBody)
        {
            _addThreadComment.Handle(new AddThreadCommentCommand
            {
                ThreadId = threadId,
                UserId = userId,
                Body = commentBody
            });
        }

        public void UpdateThreadComment(int threadId, int userId,int commentId, string commentBody)
        {
            _updateThreadComment.Handle(new UpdateThreadCommentCommand
            {
                Body= commentBody,
                ThreadId = threadId,
                CommentId = commentId,
                UserId = userId
            });
        }



        //List<CommentViewModel> GetParentReplies(Comment comment);
        //List<CommentViewModel> GetChildReplies(Reply parentReply);
        //Reply GetReplyById(string id);
        //bool CommentDeleteCheck(string commentid);
        //bool ReplyDeleteCheck(string replyid);
        //string GetPageIdByComment(string commentId);
        //void UpdateCommentLike(string commentid, string username, string likeordislike);
        //void UpdateReplyLike(string replyid, string username, string likeordislike);
        //Post GetPostByReply(string replyid);
        //string GetUrlSeoByReply(Reply reply);
        //IList<Comment> GetCommentsByPageId(string pageId);
        //IList<Comment> GetComments();
        //IList<Reply> GetReplies();
        //void AddNewComment(Comment comment);
        //void AddNewReply(Reply reply);
        //Comment GetCommentById(string id);
        //void DeleteComment(string commentid);
        //void DeleteReply(string replyid);

    }
}
