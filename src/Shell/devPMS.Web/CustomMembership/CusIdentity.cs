using devPMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace devPMS.Web.CustomMembership
{
    public class CusIdentity : IIdentity
    {
        public IIdentity Identity { get; set; }

        public UserModel User { get; set; }

        public CusIdentity(UserModel user)
        {
            Identity = new GenericIdentity(user.Email);
            User = user;
        }

        public string AuthenticationType
        {
            get { return Identity.AuthenticationType; }
        }

        public bool IsAuthenticated
        {
            get { return Identity.IsAuthenticated; }
        }

        public string Name
        {
            get { return Identity.Name; }
        }
    }
}