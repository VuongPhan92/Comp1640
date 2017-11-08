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
    public class GetUserByEmailQueryHandler : IQueryHandler<GetUserByEmailQuery, Employee>
    {
        private readonly IDbContextFactory _factory;

        public GetUserByEmailQueryHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }

        public Employee Handle(GetUserByEmailQuery query)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                return uow.Repository<Employee>().GetById(p => p.Email.Equals(query.Email, StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}
