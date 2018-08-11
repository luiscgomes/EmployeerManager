using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace EmployeeManager.Api.Filters
{
    public class ExceptionHandlerFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                Log.Error(context.Exception, "Error handled");
            }
        }
    }
}