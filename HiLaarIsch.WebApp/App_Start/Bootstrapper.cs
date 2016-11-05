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
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web;
using HiLaarIsch.BusinessLayer.CommandHandlers;
using QuantumHive.EntityFramework.Decorators;
using System.Data.Entity;
using Microsoft.Owin.Security.DataProtection;
using HiLaarIsch.Filters.ActionFilters.Global;

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

            GlobalFilters.Filters.Add(new RouteValuesTransferStateAttribute());
        }

        private static void RegisterServices(this Container container)
        {
        }

        private static void RegisterQueryHandlers(this Container container)
        {
            container.Register(typeof(IQueryHandler<,>), new [] { typeof(GetAllCustomersQueryHandler).Assembly });

            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(LifetimeScopeQueryHandlerProxy<,>), Lifestyle.Singleton);

            container.RegisterSingleton<IQueryProcessor, QueryProcessor>();
        }

        private static void RegisterCommandHandlers(this Container container)
        {
            container.Register(typeof(ICommandHandler<>), new[] { typeof(CreateNewCustomerCommandHandler).Assembly });

            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(SaveChangesCommandHandlerDecorator<>));
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(LifetimeScopeCommandHandlerProxy<>), Lifestyle.Singleton);
        }

        private static void RegisterDataServices(this Container container, string connectionString)
        {
            var databaseContextRegistration = Lifestyle.Scoped.CreateRegistration(() => new HiLaarIschEntities(connectionString), container);

            container.AddRegistration(typeof(HiLaarIschEntities), databaseContextRegistration);
            container.AddRegistration(typeof(DbContext), databaseContextRegistration);

            container.Register(typeof(IRepository<>), typeof(Repository<>));
        }

        private static void RegisterOwinIdentityServices(this Container container, IAppBuilder app)
        {
            container.RegisterSingleton<IAppBuilder>(app);

            container.RegisterSingleton<IPasswordHasher, PasswordHasher>();
            container.RegisterSingleton<IDataProtector>(() => app.GetDataProtectionProvider().Create(purposes: "Identity Data Protection"));
            container.RegisterSingleton<DataProtectorTokenProvider>();
            container.RegisterSingleton<UserManager>();

            container.RegisterPerWebRequest<SignInManager>();
            container.RegisterPerWebRequest(() =>
                container.IsVerifying
                ? new OwinContext().Authentication
                : HttpContext.Current.GetOwinContext().Authentication);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/account/login"),
                //TODO: cookie options
            });
        }
    }
}