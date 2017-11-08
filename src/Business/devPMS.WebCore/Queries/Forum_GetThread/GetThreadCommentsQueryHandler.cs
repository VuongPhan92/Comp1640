using University;
using Infrastructure.Queries;
using Infrastructure.Repository;
using SSTVN.DDo.Utility.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    public class GetThreadCommentsQueryHandler : IQueryHandler<GetThreadCommentsQuery, IEnumerable<Comment>>
    {
        private readonly IDbContextFactory _factory;

        public GetThreadCommentsQueryHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }

        public IEnumerable<Comment> Handle(GetThreadCommentsQuery query)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                string includeProperties = PropertyHelper<Comment>.GetPropertyName(u => u.CommentLikes);// string.Join(",", PropertyHelper<Comment>.GetPropertyNames(u => u.CommentLikes));

                if (query.IsIncludeChecked)
                {
                    return uow.Repository<Comment>().Find(p => p.ThreadId == query.ThreadId, includeProperties);
                }
                else
                {
                    return uow.Repository<Comment>().Find(p => p.ThreadId == query.ThreadId && !p.DeletedDT.HasValue, includeProperties);
                }
            }
        }
    }
}
