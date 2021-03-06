﻿using System;
using System.Reflection;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Owin;
using QuantumHive.Core;
using QuantumHive.SimpleInjector.Services;
using QuantumHive.SimpleInjector.Decorators;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Extensions.LifetimeScoping;
using System.Configuration;
using HiLaarIsch.Domain;
using HiLaarIsch.BusinessLayer.QueryHandlers;
using Microsoft.Owin;
using System.Web;
using QuantumHive.EntityFramework.Decorators;
using System.Data.Entity;
using Microsoft.Owin.Security.DataProtection;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Services;
using System.IO;
using HiLaarIsch.BusinessLayer.CommandHandlers.Customers;
using QuantumHive.Core.Services;
using HiLaarIsch.Domain.Services;
using Microsoft.ApplicationInsights;
using QuantumHive.Core.Logging;

namespace HiLaarIsch
{
    public static class Bootstrapper
    {
        public static Container Container;

        public static Container InitiliazeApplication(IAppBuilder app)
        {
            EntityFrameworkSqlProviderServicesFix.IncludeSqlProviderServicesInstance();

            var settings = Bootstrapper.GetApplicationSettings();
            var container = Bootstrapper.GetInitializedContainer(app, settings);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();

            container.Verify();
            Bootstrapper.Container = container;

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            return container;
        }

        public static HiLaarischSettings GetApplicationSettings()
        {
            var applicationPhase = bool.Parse(ConfigurationManager.AppSettings["testEnvironment"]) ? ApplicationPhase.Test : ApplicationPhase.Production;

            return new HiLaarischSettings(
                connectionString: ConfigurationManager.ConnectionStrings["HiLaarischEntities"].ConnectionString,
                sendGridApiKey: ConfigurationManager.AppSettings["sendgrid-apikey"],
                fromMailAddress: ConfigurationManager.AppSettings["fromEmailAddress"],
                testMailAddress: ConfigurationManager.AppSettings["testEmailAddress"],
                phase: applicationPhase);
        }

        public static Container GetInitializedContainer(IAppBuilder app, HiLaarischSettings settings)
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.RegisterServices(settings);
            container.RegisterQueryHandlers();
            container.RegisterCommandHandlers();
            container.RegisterDataServices(settings.ConnectionString);
            container.RegisterOwinIdentityServices(app);
            container.RegisterLoggers();

            return container;
        }

        private static void RegisterServices(this Container container, HiLaarischSettings settings)
        {
            container.RegisterSingleton<IApplicationDeployment>(() => new ApplicationDeployment(settings.ApplicationPhase));
#if DEBUG
            var fakeMailServicePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "HiLaarIsch Email Confirmation.txt");
            container.RegisterSingleton<IMessageService>(() => new FakeEmailService(fakeMailServicePath));
#else
            container.RegisterSingleton<IMessageService>(() => new SendGridEmailService(container.GetInstance<IApplicationDeployment>(), settings.EmailAddresses, settings.ApiKeys.SendGrid));
#endif
            container.RegisterSingleton<UserManager.CommandHandlers>();
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

            container.RegisterSingleton(typeof(IPromptableMvcCommandHandler<>), typeof(PromptableMvcCommandHandler<>));
        }

        private static void RegisterDataServices(this Container container, string connectionString)
        {
            var scopedLifestyle = new LifetimeScopeLifestyle();
            var databaseContextRegistration = scopedLifestyle.CreateRegistration(() =>
            {
#if DEBUG
                Database.SetInitializer(new DropCreateDatabaseIfModelChangesInitializer());
#else
                Database.SetInitializer<HiLaarischEntities>(null);
#endif
                return new HiLaarischEntities(connectionString);
            }, container);

            container.AddRegistration(typeof(HiLaarischEntities), databaseContextRegistration);
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

            container.Register<IAuthenticationManager<UserView>, AuthenticationManager>(Lifestyle.Scoped);
            container.Register(() => container.IsVerifying
                ? new OwinContext().Authentication
                : HttpContext.Current.GetOwinContext().Authentication, Lifestyle.Scoped);

            container.Register<ExceptionHandlingMiddleware>();
        }

        private static void RegisterLoggers(this Container container)
        {
            container.RegisterSingleton<ILogger>(new ApplicationInsightsLogger(new TelemetryClient()));
        }
    }
}