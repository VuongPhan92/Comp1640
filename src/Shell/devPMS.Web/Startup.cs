using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(devPMS.Web.Startup))]
namespace devPMS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
