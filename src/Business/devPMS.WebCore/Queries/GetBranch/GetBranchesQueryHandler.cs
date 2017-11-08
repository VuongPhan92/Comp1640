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
    public class GetBranchesQueryHandler : IQueryHandler<GetBranchesQuery,IEnumerable<Branch>>
    {
        private readonly IDbContextFactory _factory;

        public GetBranchesQueryHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }

        public IEnumerable<Branch> Handle(GetBranchesQuery query)
        {
            using (var uow = _factory.Create(DBContextName.PMSEntities))
            {
                return uow.Repository<Branch>().GetAll();        
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
