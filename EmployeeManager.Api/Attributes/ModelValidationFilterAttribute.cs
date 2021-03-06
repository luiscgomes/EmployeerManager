﻿using EmployeeManager.Api.Contracts;
using EmployeeManager.Commons.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManager.Api.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class ModelValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            var errors = CreateErrors(context.ModelState);

            context.Result = new BadRequestObjectResult(new BadRequestModel(errors));
        }

        public static IEnumerable<Notification> CreateErrors(ModelStateDictionary modelState)
        {
            return from key in modelState.Keys
                   from error in modelState[key].Errors
                   select new Notification(
                       GetMessage(error));
        }

        private static string GetMessage(ModelError error)
        {
            if (error.Exception == null)
                return error.ErrorMessage;

            return error.Exception.Message;
        }
    }
}