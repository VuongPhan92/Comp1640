using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Security;

namespace devPMS.Web.CustomMembership
{
    public class CusRoleProvider : RoleProvider
    {
        private string applicationName;
        private int m_cacheTimeoutInMinute = 20;

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

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

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return null;
            }

            // check cache
            var cachekey = string.Format("{0}_role", username);
            if (HttpRuntime.Cache[cachekey] != null)
            {
                return (string[])HttpRuntime.Cache[cachekey];
            }

            string[] roles = new string[] { };
            //using (var db = KPFactory.CreateModel())
            //{
            //    roles = (from r in db.Roles
            //             join ur in db.UserRoles on r.RoleId equals ur.RoleId
            //             join u in db.Employees on ur.UserId equals u.EmployeeID
            //             where u.Email.Equals(username)
            //             select r.RoleName).ToArray<string>();
            //    if (roles.Count() > 0)
            //    {
            //        HttpRuntime.Cache.Insert(cachekey, roles, null, DateTime.Now.AddMinutes(m_cacheTimeoutInMinute), Cache.NoSlidingExpiration);
            //    }
            //}
            return roles;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var userRoles = GetRolesForUser(username);
            return userRoles.Contains(roleName);

        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}