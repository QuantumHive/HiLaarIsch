using System.Web.Mvc;
using System.Web.Routing;
using HiLaarIsch.Filters;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using QuantumHive.Core;
using SimpleInjector;

[assembly: OwinStartup(typeof(HiLaarIsch.Startup))]
namespace HiLaarIsch
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = Bootstrapper.InitiliazeApplication(app);
            Startup.ConfigureMvcApplication(container);

            app.Use<ExceptionHandlingMiddleware>(container);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/account/login"),
                //TODO: cookie options
            });
        }

        public static void ConfigureMvcApplication(Container container)
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.MapMvcAttributeRoutes();

            BundleConfig.ConfigureAndRegisterBundles();

            GlobalFilters.Filters.Add(new RequireHttpsAttribute());
            GlobalFilters.Filters.Add(new ValidationResultFilter());
            GlobalFilters.Filters.Add(new GlobalExceptionHandlerFilter(container.GetInstance<ILogger>()));
        }
    }
}