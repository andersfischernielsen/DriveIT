using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DriveIT_MVC.Startup))]
namespace DriveIT_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
