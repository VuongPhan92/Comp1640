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
    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, Employee>
    {
        private readonly IDbContextFactory _factory;

        public GetUserByIdQueryHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }

        public Employee Handle(GetUserByIdQuery query)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                return uow.Repository<Employee>().GetById(p => p.EmployeeID == query.Id);
            }
        }
    }
}
