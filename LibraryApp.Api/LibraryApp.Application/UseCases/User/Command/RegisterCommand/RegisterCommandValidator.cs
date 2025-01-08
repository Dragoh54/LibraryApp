using FluentValidation;

namespace LibraryApp.Application.UseCases.User.Command.RegisterCommand;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(user => user)
            .NotNull()
            .WithMessage("{PropertyName} is required.");
        
        RuleFor(user => user.Nickname)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .WithMessage("{PropertyName} is required.")
            .MaximumLength(256)
            .WithMessage("{PropertyName} must not exceed 256 characters.");
        
        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .WithMessage("{PropertyName} is required.")
            .MaximumLength(256)
            .WithMessage("{PropertyName} must not exceed 256 characters.");
        
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .WithMessage("{PropertyName} is required.")
            .MaximumLength(256)
            .WithMessage("{PropertyName} must not exceed 256 characters.");
    }
}