using FluentValidation;
using LibraryApp.Application.Validators;

namespace LibraryApp.Application.UseCases.User.Command.RefreshCommand;

public class RefreshCommandValidator : AbstractValidator<RefreshCommand>
{
    public RefreshCommandValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("RefreshCommand is required")
            .NotNull()
            .WithMessage("RefreshCommand is required");
        
        RuleFor(x => x.Token)
            .NotEmpty()
            .WithMessage("Token is required")
            .NotNull()
            .WithMessage("Token is required");
    }
}