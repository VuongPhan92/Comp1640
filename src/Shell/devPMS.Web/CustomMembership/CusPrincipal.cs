using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace devPMS.Web.CustomMembership
{
    public class CusPrincipal : IPrincipal
    {
        private readonly CusIdentity customeIdentity;

        public CusPrincipal(CusIdentity _customeIdentity)
        {
            customeIdentity = _customeIdentity;
        }

        public IIdentity Identity
        {
            get
            {
                return customeIdentity;
            }
        }

        public bool IsInRole(string role)
        {
            var result = false;
            if (role.Contains(','))
            {
                var roleArray = role.Split(',');
                foreach(var r in roleArray)
                {
                    result = Roles.IsUserInRole(customeIdentity.Identity.Name, r);
                    if (result) break;
                }
                return result;
            }
            return Roles.IsUserInRole(customeIdentity.Identity.Name, role);
        }
    }
}