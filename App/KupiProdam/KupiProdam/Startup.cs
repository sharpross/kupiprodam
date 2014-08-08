using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KupiProdam.Startup))]
namespace KupiProdam
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
