using University;
using Infrastructure.Queries;
using Infrastructure.Repository;
using SSTVN.PMS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    public class IsLikeDislikeQueryHandler : IQueryHandler<IsLikeDislikeQuery,bool>
    {
        private readonly IDbContextFactory _factory;
        public IsLikeDislikeQueryHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }

        public bool Handle(IsLikeDislikeQuery query)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                switch (query.TypeLike)
                {
                    case TypeAndLike.ThreadLike:
                        var entityThread= uow.Repository<ThreadLike>().Find(p => p.ThreadId == query.Id && p.UserId == query.UserId && p.Like == true);
                        if (entityThread.Count() != 0)
                        { 
                            return true;
                        }
                        return false;
                    case TypeAndLike.ThreadDislike:
                      //  return uow.Repository<ThreadLike>().Count(p => p.ThreadId == query.Id && p.DisLike == true);
                    case TypeAndLike.CommentLike:
                       // return uow.Repository<CommentLike>().Count(p => p.CommentId == query.Id && p.Like == true);
                        var entityComment= uow.Repository<CommentLike>().Find(p => p.CommentId == query.Id && p.UserId == query.UserId && p.Like == true);
                        if (entityComment.Count() != 0)
                        { 
                            return true;
                        }
                        return false;
                    case TypeAndLike.CommentDislike:
                       // return uow.Repository<CommentLike>().Count(p => p.CommentId == query.Id && p.DisLike == true);
                    case TypeAndLike.ReplyLike:
                       // return uow.Repository<ReplyLike>().Count(p => p.ReplyId == query.Id && p.Like == true);
                        var entityReply= uow.Repository<ReplyLike>().Find(p => p.ReplyId == query.Id && p.UserId == query.UserId && p.Like == true);
                        if (entityReply.Count() != 0)
                        { 
                            return true;
                        }
                        return false;
                    case TypeAndLike.ReplyDislike:
                      //  return uow.Repository<ReplyLike>().Count(p => p.ReplyId == query.Id && p.DisLike == true);
                    default:
                        return false;
                }
            }

        }
    }
}
