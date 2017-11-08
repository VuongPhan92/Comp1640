using University;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using SSTVN.Core.Repository;
using System.Data.Entity.Validation;

namespace devPMS.WebCore.Business
{
    public class UpdateUserPasswordCommandHandler : ICommandHandler<UpdateUserPasswordCommand>
    {
        private readonly IDbContextFactory _factory;

        public UpdateUserPasswordCommandHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }
        public void Handle(UpdateUserPasswordCommand command)
        {
            try
            {
                using (var uow = _factory.Create(DBContextName.UniversityEntities))
                {
                    var crypto = new SimpleCrypto.PBKDF2();
                    var user = uow.Repository<Employee>().GetById(p => p.Email.Equals(command.Email));
                    if (user == null)
                    {
                        command.Success = false;
                        return;
                    }
                    var encrypPass = crypto.Compute(command.Password);
                    user.Password = encrypPass;
                    user.PasswordSalt = crypto.Salt;
                    uow.SubmitChanges();
                    command.Success = true;
                }
            }
            catch (DbEntityValidationException ex)
            {
                command.Success = false;
            }
        }
    }
}