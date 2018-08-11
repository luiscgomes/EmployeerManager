using EmployeeManager.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
                                Configuration.GetValue<string>("Split"),
                                splitOptions => splitOptions.EnableRetryOnFailure())
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
        }
    }
}