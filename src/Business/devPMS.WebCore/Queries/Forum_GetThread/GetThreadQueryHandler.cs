using University;
using Infrastructure.Queries;
using Infrastructure.Repository;
using SSTVN.DDo.Utility.Extension;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    public class GetThreadQueryHandler : IQueryHandler<GetThreadQuery, Thread>
    {
        private readonly IDbContextFactory _factory;

        public GetThreadQueryHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }

        public Thread Handle(GetThreadQuery query)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                try
                {
                    string includeProperties = string.Join(",", PropertyHelper<Thread>.GetPropertyNames(u => u.Category, u => u.Tags, u=>u.ThreadLikes));

                    var thread = uow.Repository<Thread>().GetById(p => p.CreatedDT.Year == query.Year
                        && p.CreatedDT.Month == query.Month
                        && p.CreatedDT.Day == query.Day
                        && p.UrlSeo.Equals(query.Title), includeProperties);
                    if (thread != null && query.IsIncrementView)
                    {
                        thread.View += 1;
                        uow.SubmitChanges();
                    }
                    return thread;
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                    return null;
                }
            }
        }
    }
}
