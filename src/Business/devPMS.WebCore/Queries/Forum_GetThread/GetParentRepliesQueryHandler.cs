using University;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    public class GetParentRepliesQueryHandler : IQueryHandler<GetParentRepliesQuery, IEnumerable<Reply>>
    {
        private readonly IDbContextFactory _factory;

        public GetParentRepliesQueryHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }

        public IEnumerable<Reply> Handle(GetParentRepliesQuery query)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                if (query.IsIncludeChecked)
                {
                    return uow.Repository<Reply>().Find(p=> p.CommentId == query.CommentId);
                }
                else
                {
                    return uow.Repository<Reply>().Find(p => p.CommentId == query.CommentId && !p.DeletedDT.HasValue);
                }
            }
        }
    }
}
