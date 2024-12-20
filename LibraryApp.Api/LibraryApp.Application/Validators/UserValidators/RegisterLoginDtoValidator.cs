using FluentValidation;
using LibraryApp.Application.User;

namespace LibraryApp.Application.Validators.UserValidators;

public class RegisterLoginDtoValidator<T> : AbstractValidator<T> where T : RegisterLoginUserDto 
{
    public RegisterLoginDtoValidator()
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