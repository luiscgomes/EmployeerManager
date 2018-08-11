using EmployeeManager.Api.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EmployeeManager.Api
{
    public partial class Startup
    {
        public void ConfigureMvc(IServiceCollection services)
        {
            var mvcBuilder = services.AddMvc(options =>
            {
                options.Filters.Add(new ModelValidationFilterAttribute());
                options.AllowEmptyInputInBodyModelBinding = false;
            });

            mvcBuilder.AddJsonOptions(mvcJsonOptions =>
            {
                var settings = mvcJsonOptions.SerializerSettings;

                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.Converters.Add(new StringEnumConverter());
            });
        }
    }
}