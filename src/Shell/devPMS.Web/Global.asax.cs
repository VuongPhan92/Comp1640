using University;
using devPMS.Web.CustomMembership;
using devPMS.Web.Models;
using devPMS.WebCore;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace devPMS.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Code for registering our repository class and DI
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            var bootstrap = new WebCoreBootstrapper(container);
            bootstrap.Boot();

            // This two extension method from integration package
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            DependencyResolver.SetResolver(
                new SimpleInjectorDependencyResolver(container));
        }

        protected void Application_PostAuthenticateRequest()
        {
            HttpCookie authoCookies = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authoCookies != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authoCookies.Value);
                JavaScriptSerializer js = new JavaScriptSerializer();
                UserModel user = js.Deserialize<UserModel>(ticket.UserData);
                if (user != null)
                {
                    CusIdentity customIdentiy = new CusIdentity(user);
                    CusPrincipal customPrincipal = new CusPrincipal(customIdentiy);
                    HttpContext.Current.User = customPrincipal;
                }
                else
                {
                    throw new HttpException();
                }
            }
        }
    }
}
