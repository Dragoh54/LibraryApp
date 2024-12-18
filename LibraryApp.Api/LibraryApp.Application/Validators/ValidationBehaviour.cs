using FluentValidation;
using FluentValidation.Results;
using LibraryApp.DomainModel.Exceptions;
using MediatR;
using ValidationException = FluentValidation.ValidationException;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace LibraryApp.Application.Validators;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
 
    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
            FluentValidation.Results.ValidationResult[] validationResults =
                await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            
            var errors = validationResults
                .SelectMany(result => result.Errors) 
                .Where(failure => failure != null)  
                .ToArray();
            
            if (errors.Any())
            {
                throw new ValidationException(errors);
            }
        }
 
        return await next();
    }
}