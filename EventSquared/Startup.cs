using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EventSquared.Startup))]
namespace EventSquared
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
