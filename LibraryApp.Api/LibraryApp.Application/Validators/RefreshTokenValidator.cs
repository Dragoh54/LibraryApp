using FluentValidation;
using LibraryApp.Application.Dto;
using LibraryApp.Entities.Models;

namespace LibraryApp.Application.Validators;

public class RefreshTokenValidator<T> : AbstractValidator<T> where T : RefreshTokenDto
{
    public RefreshTokenValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("Id model is required")
            .NotNull()
            .WithMessage("Id model is required");
    }
}