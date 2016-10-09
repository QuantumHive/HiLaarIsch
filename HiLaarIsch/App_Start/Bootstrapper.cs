using System;
using System.Reflection;
using System.Web.Mvc;
using HiLaarIsch.Identity;
using Microsoft.AspNet.Identity;
using Owin;
using QuantumHive.Core;
using QuantumHive.SimpleInjector.Services;
using QuantumHive.SimpleInjector.Decorators;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Extensions.LifetimeScoping;
using Microsoft.Owin.Security.Cookies;
using System.Configuration;
using HiLaarIsch.Domain;
using HiLaarIsch.BusinessLayer.QueryHandlers;
using System.Web.Routing;
using Microsoft.Owin;

namespace HiLaarIsch
{
    public static class Bootstrapper
    {
        public static Container Container;

        public static Container InitiliazeApplication(IAppBuilder app)
        {
            EntityFrameworkSqlProviderServicesFix.IncludeSqlProviderServicesInstance();

            var connectionString = ConfigurationManager.ConnectionStrings["HiLaarIschEntities"].ConnectionString;
            var container = Bootstrapper.GetInitializedContainer(app, connectionString);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();

            container.Verify();
            Bootstrapper.Container = container;

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            app.CreatePerOwinContext(() => container.GetInstance<UserManager<IdentityUser, Guid>>());

            return container;
        }

        public static Container GetInitializedContainer(IAppBuilder app, string connectionString)
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = Lifestyle.CreateHybrid(
                () => container.GetCurrentLifetimeScope() == null,
                new WebRequestLifestyle(),
                new LifetimeScopeLifestyle());

            container.RegisterServices();
            container.RegisterQueryHandlers();
            container.RegisterCommandHandlers();
            container.RegisterDataServices(connectionString);
            container.RegisterOwinIdentityServices(app);

            return container;
        }

        public static void ConfigureMvcServices(IAppBuilder app)
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.MapMvcAttributeRoutes();

            BundleConfig.ConfigureAndRegisterBundles();
        }

        private static void RegisterServices(this Container container)
        {
        }

        private static void RegisterQueryHandlers(this Container container)
        {
            container.Register(typeof(IQueryHandler<,>), new [] { typeof(GetAllUsersQueryHandler).Assembly });

            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(LifetimeScopeQueryHandlerProxy<,>), Lifestyle.Singleton);

            container.RegisterSingleton<IQueryProcessor, QueryProcessor>();
        }

        private static void RegisterCommandHandlers(this Container container)
        {
            //container.Register(typeof(ICommandHandler<>), new[] { typeof(TODO).Assembly }, Lifestyle.Scoped);

            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(LifetimeScopeCommandHandlerProxy<>), Lifestyle.Singleton);
        }

        private static void RegisterDataServices(this Container container, string connectionString)
        {
            container.Register(() => new HiLaarIschEntities(connectionString), Lifestyle.Scoped);

            container.Register(typeof(IRepository<>), typeof(Repository<>));
        }

        private static void RegisterOwinIdentityServices(this Container container, IAppBuilder app)
        {
            container.RegisterSingleton<IAppBuilder>(app);

            container.RegisterPerWebRequest<IUserStore<IdentityUser, Guid>, UserStore>();

            container.RegisterPerWebRequest(() => new UserManager<IdentityUser, Guid>(container.GetInstance<IUserStore<IdentityUser, Guid>>()));
            container.RegisterInitializer<UserManager<IdentityUser, Guid>>(userManager =>
            {
                //TODO: usermanager configuration
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                //TODO: cookie options
            });
        }
    }
}