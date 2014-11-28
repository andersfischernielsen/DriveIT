using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DriveIT.MVC.Startup))]
namespace DriveIT.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
