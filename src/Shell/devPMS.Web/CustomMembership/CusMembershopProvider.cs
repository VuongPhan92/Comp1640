using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace devPMS.Web.CustomMembership
{
    // I follow the tutorial from dotnetawesome.com to implement custom membership provider in Forms authentication in ASP MVC5
    // Link: http://www.dotnetawesome.com/2015/06/part2-how-to-implement-custom-forms-authentication-in-aspnet-mvc.html
    // or 
    // https://thebojan.ninja/2015/03/12/custom-membership-provider/
    // https://thebojan.ninja/2015/03/12/custom-role-provider/
    // https://thebojan.ninja/2015/03/12/custom-membership-user/
    public class CusMembershopProvider : MembershipProvider
    {
        #region Class Variables
        //private UnitOfWork unitOfWork = new UnitOfWork();
        private int newPasswordLength = 8;
        private string connectionString;
        private string applicationName;
        private bool enablePasswordReset;
        private bool enablePasswordRetrieval;
        private bool requiresQuestionAndAnswer;
        private bool requiresUniqueEmail;
        private int maxInvalidPasswordAttempts;
        private int passwordAttemptWindow;
        private MembershipPasswordFormat passwordFormat;
        private int minRequiredNonAlphanumericCharacters;
        private int minRequiredPasswordLength;
        private string passwordStrengthRegularExpression;
        //private MachineKeySection machineKey; //Used when determining encryption key values.  
        #endregion

        public override string ApplicationName
        {
            get
            {
                return applicationName;
            }
            set
            {
                applicationName = value;
            }
        }

        #region Properties
        public override bool EnablePasswordReset
        {
            get
            {
                return enablePasswordReset;
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                return enablePasswordRetrieval;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                return maxInvalidPasswordAttempts;
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                return minRequiredNonAlphanumericCharacters;
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                return minRequiredPasswordLength;
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                return passwordAttemptWindow;
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                return passwordFormat;
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                return passwordStrengthRegularExpression;
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                return requiresQuestionAndAnswer;
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                return requiresUniqueEmail;
            }
        }
        #endregion

        #region MembershipProvider overrides

        #endregion
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            //if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword)) return false;

            //if (oldPassword == newPassword) return false;

            //CustomMembershipUser user = GetUser(username);

            //if (user == null) return false;

            //CustomDataDataContext db = new CustomDataDataContext();
            //var RawUser = (from u in db.Users
            //               where u.UserName == user.UserName && u.DeletedOn == null
            //               select u).FirstOrDefault();

            //if (string.IsNullOrWhiteSpace(RawUser.Password)) return false;

            //RawUser.Password = EncodePassword(newPassword);

            //db.SubmitChanges();

            //return true;
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            // Will write code for validate user from our own database
            //var user = unitOfWork.EmployeeRepository.GetById(p => p.Email.Equals(username, StringComparison.OrdinalIgnoreCase));
            //if (user != null)
            //{
            //    var crypto = new SimpleCrypto.PBKDF2();
            //    if (user.Password == crypto.Compute(password, user.PasswordSalt))
            //    {
            //        return true;
            //    }
            //}
            return false;
        }
    }
}