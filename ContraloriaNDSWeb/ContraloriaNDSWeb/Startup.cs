using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContraloriaNDSWeb.Startup))]
namespace ContraloriaNDSWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
