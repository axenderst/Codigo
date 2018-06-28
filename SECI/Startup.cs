using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SECI.Startup))]
namespace SECI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
