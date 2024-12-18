using FluentValidation;
using LibraryApp.Application.Dto;

namespace LibraryApp.Application.Validators;

public class IdModelValidator<T> : AbstractValidator<T> where T : IdModel
{
    public IdModelValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .NotNull()
            .WithMessage("Id is required.");
    }
}