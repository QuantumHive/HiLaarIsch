using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
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

            app.Use<ExceptionHandlingMiddleware>(container);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/account/login"),
                //TODO: cookie options
            });
        }
    }
}