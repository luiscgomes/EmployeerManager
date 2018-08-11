using EmployeeManager.Api.Attributes;
using EmployeeManager.Api.Validations;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EmployeeManager.Api
{
    public partial class Startup
    {
        public void ConfigureMvc(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new ModelValidationFilterAttribute());
                options.AllowEmptyInputInBodyModelBinding = false;
            })
            .AddJsonOptions(mvcJsonOptions =>
            {
                var settings = mvcJsonOptions.SerializerSettings;

                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.Converters.Add(new StringEnumConverter());
            })
            .AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<EmployeeCreateModelValidator>();
            });
        }
    }
}