using University;
using devPMS.WebCore.Business;
using devPMS.WebCore.Queries;
using Infrastructure.Decorator;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Services
{
    public class EmployeeServices : IService
    {
        private readonly GetUserByEmailQueryHandler _getUserByEmail;
        private readonly ValidateUserQueryHandler _validateUser;
        private readonly GetUserByIdQueryHandler _getUserById;
        private readonly IQueryHandler<GetUsersQuery, List<Employee>> _getUsers;
        private readonly ICommandHandler<UpdateUserPasswordCommand> _updateUserPassword;

        public EmployeeServices(
            ICommandHandler<UpdateUserPasswordCommand> updateUserPassword,
            GetUserByEmailQueryHandler getUserByEmail,
            ValidateUserQueryHandler validateUser,
            GetUserByIdQueryHandler getUserById,
            IQueryHandler<GetUsersQuery, List<Employee>> getUsers)
        {
            _updateUserPassword = updateUserPassword;
            _validateUser = validateUser;
            _getUserById = getUserById;
            _getUserByEmail = getUserByEmail;
            _getUsers = getUsers;
        }

        public bool UpdateUserPassword(string email, string password)
        {
            var updateUserPasswordCommand = new UpdateUserPasswordCommand { Email = email, Password = password };
            _updateUserPassword.Handle(updateUserPasswordCommand);
            return updateUserPasswordCommand.Success;
        } 

        public bool ValidateUser(string email, string password)
        {
            return _validateUser.Handle(new ValidateUserQuery { UserName = email, Password = password});
        }

        public Employee GetUserByEmail(string email)
        {
            return _getUserByEmail.Handle(new GetUserByEmailQuery { Email = email });
        }

        public Employee GetUserById(int id)
        {
            return _getUserById.Handle(new GetUserByIdQuery { Id = id });
        }
        public IList<Employee> GetUsers()
        {
            return _getUsers.Handle(new GetUsersQuery { });
        }

      
    }
}
