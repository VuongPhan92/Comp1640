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
    public class LikeDislikeCountQueryHandler : IQueryHandler<LikeDislikeCountQuery, int>
    {
        private readonly IDbContextFactory _factory;

        public LikeDislikeCountQueryHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }

        public int Handle(LikeDislikeCountQuery query)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                switch(query.TypeLike)
                {
                    case TypeAndLike.ThreadLike:
                        return uow.Repository<ThreadLike>().Count(p => p.ThreadId == query.Id && p.Like == true);
                    case TypeAndLike.ThreadDislike:
                        return uow.Repository<ThreadLike>().Count(p => p.ThreadId == query.Id && p.DisLike == true);
                    case TypeAndLike.CommentLike:
                        return uow.Repository<CommentLike>().Count(p => p.CommentId == query.Id && p.Like == true);
                    case TypeAndLike.CommentDislike:
                        return uow.Repository<CommentLike>().Count(p => p.CommentId == query.Id && p.DisLike == true);
                    case TypeAndLike.ReplyLike:
                        return uow.Repository<ReplyLike>().Count(p => p.ReplyId == query.Id && p.Like == true);
                    case TypeAndLike.ReplyDislike:
                        return uow.Repository<ReplyLike>().Count(p => p.ReplyId == query.Id && p.DisLike == true);
                    default:
                        return 0;
                }
            }
        }
    }
}
