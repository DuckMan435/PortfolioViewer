using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PortfolioViewer.Client.Startup))]
namespace PortfolioViewer.Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
