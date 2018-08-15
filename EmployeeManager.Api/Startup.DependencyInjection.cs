using EmployeeManager.Commons.Notifications;
using EmployeeManager.Infrastructure.Data;
using EmployeeManager.Infrastructure.Repositories;
using EmployeeManager.Infrastructure.Repositories.EmployeeWriters;
using EmployeeManager.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using Serilog.Filters;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

namespace EmployeeManager.Api
{
    public partial class Startup
    {
        public void ConfigureDependencyInjection(IServiceCollection services)
        {
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services
                .AddSingleton<IControllerActivator>(
                    new SimpleInjectorControllerActivator(_container))
                .EnableSimpleInjectorCrossWiring(_container);

            services
                .AddDbContext<EmployeeManagerContext>(
                    options =>
                        options
                            .UseSqlServer(
                                Configuration.GetValue<string>("EmployeeManagerDb"),
                                dbOptions => dbOptions.EnableRetryOnFailure())
                            .EnableSensitiveDataLogging());

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.UseSimpleInjectorAspNetRequestScoping(_container);
        }

        public void UseDependencyInjection(IApplicationBuilder app)
        {
            _container.RegisterMvcControllers(app);

            _container.CrossWire<ILoggerFactory>(app);
            _container.CrossWire<EmployeeManagerContext>(app);
            _container.CrossWire<IHostingEnvironment>(app);

            _container.Register<IEmployeeReader, EmployeeReader>();

            _container.Register<IEmployeeWriter, EmployeeWriter>();
            _container.RegisterDecorator<IEmployeeWriter, EmployeeWriterWithEmailAlreadyExistsValidation>();

            _container.Register<INotificationStore, NotificationStore>(Lifestyle.Scoped);

            _container.Register(() =>
            {
                Log.Logger =
                    new LoggerConfiguration()
                        .Enrich.FromLogContext()
                        .Enrich.WithProperty("Env", Configuration.GetValue<string>("Env"))
                        .MinimumLevel.Verbose()
                        .WriteTo.Console()
                        .WriteTo.Logger(lc => lc
                            .Filter
                            .ByExcluding(Matching.FromSource(nameof(Microsoft)))
                            .WriteTo.AzureDocumentDB(
                                Configuration.GetValue<string>("LogUrl"),
                                Configuration.GetValue<string>("LogToken"),
                                Configuration.GetValue<string>("LogDb"),
                                Configuration.GetValue<string>("LogCollection"),
                                LogEventLevel.Information))
                        .CreateLogger();

                LogContext.PushProperty("Application", "EmployeeManager.Api");

                return Log.Logger;
            },
            Lifestyle.Singleton);

            _container.Verify();
        }
    }
}