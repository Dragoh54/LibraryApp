using FluentValidation;
using Microsoft.AspNetCore.Hosting.Server.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryApp.Application.Validators;

public class ValidationFilter : IActionFilter
{
    private readonly IServiceProvider _serviceProvider;
    
    public ValidationFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var parameter in context.ActionArguments)
        {
            var argumentValue = parameter.Value;

            if (argumentValue is null)
            {
                continue;
            }
            
            var validatorType = typeof(IValidator<>).MakeGenericType(argumentValue.GetType());

            if (_serviceProvider.GetService(validatorType) is IValidator validator)
            {
                var validationResult = validator.Validate(new ValidationContext<object>(argumentValue));
                if (!validationResult.IsValid)
                {
                    context.Result = new BadRequestObjectResult(validationResult.Errors.Select(e => e.ErrorMessage));
                    return;
                }
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}