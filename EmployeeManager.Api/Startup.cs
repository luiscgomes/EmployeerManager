using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

namespace EmployeeManager.Api
{
    public partial class Startup
    {
        public IConfigurationRoot Configuration { get; }

        private readonly Container _container = new Container();

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void Configure(IApplicationBuilder app)
        {
            //app.UseMiddleware<LoggingMiddleware>();

            UseDependencyInjection(app);

            app.UseMvc();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureMvc(services);
            ConfigureDependencyInjection(services);
        }
    }
}