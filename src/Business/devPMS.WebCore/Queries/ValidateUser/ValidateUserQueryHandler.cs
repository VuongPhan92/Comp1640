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
    public class ValidateUserQueryHandler : IQueryHandler<ValidateUserQuery, bool>
    {
        private readonly IDbContextFactory _factory;

        public ValidateUserQueryHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }

        public bool Handle(ValidateUserQuery query)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                var user = uow.Repository<Employee>().GetById(p =>
                   p.Email.Equals(query.UserName, StringComparison.OrdinalIgnoreCase));
                if (user != null)
                {
                    var cryto = new SimpleCrypto.PBKDF2();
                    if (user.Password == cryto.Compute(query.Password, user.PasswordSalt))
                        return true;
                }
                return false;
            }
        }
    }
}
