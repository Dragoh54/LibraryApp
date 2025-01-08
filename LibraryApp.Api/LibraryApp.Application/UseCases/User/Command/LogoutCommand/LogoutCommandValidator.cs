using FluentValidation;
using LibraryApp.Application.Validators;

namespace LibraryApp.Application.UseCases.User.Command.LogoutCommand;

public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
{
    public LogoutCommandValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("LogoutCommand is required")
            .NotNull()
            .WithMessage("LogoutCommand is required");
        
        RuleFor(x => x.Token)
            .NotEmpty()
            .WithMessage("Token is required")
            .NotNull()
            .WithMessage("Token is required");
    }
}