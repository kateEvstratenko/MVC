using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(guestNetwork.Startup))]
namespace guestNetwork
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
