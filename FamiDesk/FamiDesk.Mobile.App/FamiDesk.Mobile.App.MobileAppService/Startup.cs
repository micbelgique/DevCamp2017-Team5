using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(FamiDesk.Mobile.App.MobileAppService.Startup))]

namespace FamiDesk.Mobile.App.MobileAppService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}