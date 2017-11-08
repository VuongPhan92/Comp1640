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
    public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, List<Employee>>
    {
        private readonly IDbContextFactory _factory;

        public GetUsersQueryHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }

        public List<Employee> Handle(GetUsersQuery query)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                return uow.Repository<Employee>().GetAll().ToList();
            }
        }
    }
}
