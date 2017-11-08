using University;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using SSTVN.PMS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Business
{
    public class LikeDislikeCommandHandler : ICommandHandler<LikeDislikeCommand>
    {
        private readonly IDbContextFactory _factory;
        public LikeDislikeCommandHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }
        public void Handle(LikeDislikeCommand command)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                try
                {
                switch (command.TypeLike)
                {
                    case TypeAndLike.ThreadLike:
                        var entityThread = uow.Repository<ThreadLike>().GetById(p => p.ThreadId == command.Id && p.UserId == command.UserId);
                        if (entityThread != null)
                        {
                            entityThread.Like = !entityThread.Like;
                        }
                        else
                        {
                            uow.Repository<ThreadLike>().Add(new ThreadLike { Like = true, ThreadId = command.Id, UserId = command.UserId });
                        }
                        break;
                    case TypeAndLike.ThreadDislike:
                        //return uow.Repository<ThreadLike>().Count(p => p.ThreadId == query.Id && p.DisLike == true);
                    case TypeAndLike.CommentLike:
                        var entityComment = uow.Repository<CommentLike>().GetById(p => p.CommentId == command.Id && p.UserId == command.UserId);
                        if (entityComment != null)
                        {
                            entityComment.Like = !entityComment.Like;
                        }
                        else
                        {
                            uow.Repository<CommentLike>().Add(new CommentLike { Like = true, CommentId = command.Id, UserId = command.UserId });
                        }
                        break;
                    case TypeAndLike.CommentDislike:
                        //return uow.Repository<CommentLike>().Count(p => p.CommentId == query.Id && p.DisLike == true);
                    case TypeAndLike.ReplyLike:
                        var entityReply = uow.Repository<ReplyLike>().GetById(p => p.ReplyId == command.Id && p.UserId == command.UserId);
                        if (entityReply != null)
                        {
                            entityReply.Like = !entityReply.Like;
                        }
                        else
                        {
                            uow.Repository<ReplyLike>().Add(new ReplyLike { Like = true, ReplyId = command.Id, UserId = command.UserId });
                        }
                        break;
                    case TypeAndLike.ReplyDislike:
                        //return uow.Repository<ReplyLike>().Count(p => p.ReplyId == query.Id && p.DisLike == true);
                    default:
                        return;
                }
                
                    uow.SubmitChanges();
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }
        }
    }
}
