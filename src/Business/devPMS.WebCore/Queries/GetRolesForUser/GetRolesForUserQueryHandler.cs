using devPMS.Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    public class GetRolesForUserQueryHandler : IQueryHandler<GetRolesForUserQuery, string[]>
    {
        private readonly IDbContextFactory _factory;

        public GetRolesForUserQueryHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }

        public string[] Handle(GetRolesForUserQuery query)
        {
            using (var uow = _factory.Create(DBContextName.PMSEntities))
            {
                return uow.Repository<Role>()
                    .Find(p => p.Employees.Where(e => e.Email.Equals(query.Email)).Select(c => c.EmployeeID).Contains(p.RoleId))
                    .Select(p => p.RoleName).ToArray();
            }
            
        }

        //using (var db = KPFactory.CreateModel())
        //{
        //    roles = (from r in db.Roles
        //             join ur in db.UserRoles on r.RoleId equals ur.RoleId
        //             join u in db.Employees on ur.UserId equals u.EmployeeID
        //             where u.Email.Equals(username)
        //             select r.RoleName).ToArray<string>();
        //}
    }
}
