using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShoppingAPI.Api.Validation.FluentValidation;
using ShoppingAPI.Entity.DTO.Category;
using System.Security.Principal;
using System.Xml;

namespace ShoppingAPI.Api.Aspects
{
    public class ValidationFilter:Attribute, IAsyncActionFilter
    {
        private Type _validatorType;

        public ValidationFilter(Type validatorType)
        {
            _validatorType = validatorType;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ValidationHelper.Validate(_validatorType, context.ActionArguments.Values.ToArray());
            var executedContext = await next();
        }
    }
}
