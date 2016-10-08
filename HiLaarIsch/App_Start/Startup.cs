using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(HiLaarIsch.Startup))]
namespace HiLaarIsch
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = Bootstrapper.InitiliazeApplication(app);
            Bootstrapper.ConfigureMvcServices(app);
        }
    }
}