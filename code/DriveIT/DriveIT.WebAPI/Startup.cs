using DriveIT.WebAPI;
using Microsoft.Owin;
using Owin;

//[assembly: OwinStartup(typeof(Startup))]

namespace DriveIT.WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
